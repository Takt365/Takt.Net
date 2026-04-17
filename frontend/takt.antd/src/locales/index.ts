import { App } from 'vue'
import { unref } from 'vue'
import { createI18n } from 'vue-i18n'
import type { I18n } from 'vue-i18n'
import { logger } from '@/utils/logger'
type MessageRecord = Record<string, unknown>
type TranslationModule = { default: MessageRecord }
const isRecord = (value: unknown): value is MessageRecord =>
  typeof value === 'object' && value !== null && !Array.isArray(value)

/**
 * 自动导入所有翻译文件
 * 使用 Vite 的 glob 功能自动扫描 locales 目录下的所有语言文件
 * 文件路径格式：./模块/子模块/.../语言代码.ts
 * 例如：./common/zh-CN.ts, ./identity/user/zh-CN.ts
 *
 * 支持的语言列表由 getSupportedLocalesFromModules() 从扫描到的路径动态收集，无需在此硬编码。
 *
 * 如何添加新语言与翻译：
 * 1) 静态文案：在对应模块下新增「语言代码.ts」文件即可被自动加载（如 common/de-DE.ts）。
 *    语言代码格式为 xx-YY（如 zh-CN、en-US、ar-SA）。新增后无需改 index.ts，列表会自动包含该语言。
 * 2) 语言切换下拉（takt-locale-toggle）：选项来自后端「语言/字典」接口（localeStore.enabledLanguages）。
 *    若要在切换器中展示新语言，需在后端维护该语言并设为启用；前端仅负责已有静态文件的合并与展示。
 * 3) 后端翻译（如 entity.xxx）：通过种子或管理端维护 TaktTranslation，ResourceKey 与前端 t('key') 一致。
 */
const translationModules = import.meta.glob<TranslationModule>('./**/*.ts', {
  eager: true
})

/**
 * 根据文件路径构建嵌套的消息结构
 * @param path 文件路径，例如：./common/zh-CN.ts 或 ./identity/user/zh-CN.ts
 * @param messages 翻译消息对象
 * @returns 嵌套的消息结构
 */
function buildNestedMessages(path: string, messages: MessageRecord): MessageRecord {
  // 移除 ./ 前缀和文件扩展名（包括语言代码）
  // 使用动态正则表达式匹配所有语言代码格式（如：zh-CN, en-US, ar-SA, es-ES, fr-FR, ja-JP, ru-RU）
  // 例如：./common/zh-CN.ts -> common
  // 例如：./identity/user/en-US.ts -> identity/user
  const cleanPath = path.replace(/^\.\//, '').replace(/\/([a-z]{2}-[A-Z]{2})\.ts$/i, '')
  
  // 如果路径为空（根级别文件），直接返回消息
  if (!cleanPath) {
    return messages
  }
  
  // 分割路径为数组，例如：['common'] 或 ['identity', 'user']（去掉空段，避免 // 产生 undefined 语义）
  const pathParts = cleanPath.split('/').filter((segment): segment is string => segment.length > 0)
  
  // 构建嵌套对象
  const result: MessageRecord = {}
  let current: MessageRecord = result
  
  // 使用 entries()：在 noUncheckedIndexedAccess 下 pathParts[i] 为 string | undefined，不能作 Record 索引
  for (const [i, part] of pathParts.entries()) {
    if (i === pathParts.length - 1) {
      // 最后一个部分，直接赋值消息
      current[part] = messages
    } else {
      // 中间部分，创建嵌套对象
      current[part] = {}
      current = current[part] as MessageRecord
    }
  }
  
  return result
}

/**
 * 深度合并对象（用于合并翻译数据）
 */
function deepMerge(target: MessageRecord, source: MessageRecord): MessageRecord {
  const result = { ...target }
  for (const key in source) {
    const sourceValue = source[key]
    const targetValue = target[key]
    if (isRecord(sourceValue)) {
      result[key] = deepMerge(isRecord(targetValue) ? targetValue : {}, sourceValue)
    } else {
      result[key] = sourceValue
    }
  }
  return result
}

/**
 * 从文件路径提取语言代码
 * @param path 文件路径，例如：./common/zh-CN.ts
 * @returns 语言代码，如果无法提取则返回 null
 */
function extractLocaleFromPath(path: string): string | null {
  // 匹配路径中的语言代码（格式：语言代码.ts）
  // 例如：./common/zh-CN.ts -> zh-CN
  // 例如：./identity/user/en-US.ts -> en-US
  const localeMatch = path.match(/\/([a-z]{2}-[A-Z]{2})\.ts$/i)
  const code = localeMatch?.[1]
  return typeof code === 'string' && code.length > 0 ? code : null
}

/**
 * 从已扫描的翻译模块路径中动态收集支持的语言代码（不硬编码）
 * 凡在 locales 目录下存在「语言代码.ts」的文件（如 common/zh-CN.ts、identity/user/en-US.ts），
 * 其语言代码会被自动加入列表。takt-locale-toggle 的选项来自后端 localeStore.enabledLanguages，
 * 与此处静态文案语言列表相互独立：此处决定「有哪些语言的静态文案可被加载」，后端决定「下拉里展示哪些语言」。
 */
function getSupportedLocalesFromModules(): string[] {
  const set = new Set<string>()
  for (const path of Object.keys(translationModules)) {
    if (path.includes('/index.ts')) continue
    const locale = extractLocaleFromPath(path)
    if (locale) set.add(locale)
  }
  // 稳定顺序：zh-CN 优先，其余按字母
  const list = Array.from(set)
  const zhCN = list.filter(l => l === 'zh-CN')
  const rest = list.filter(l => l !== 'zh-CN').sort()
  return [...zhCN, ...rest]
}

/**
 * 自动加载所有静态翻译文件
 * @param supportedLocales 支持的语言代码列表（由 getSupportedLocalesFromModules 动态得到）
 */
function loadStaticMessages(supportedLocales: string[]): Record<string, MessageRecord> {
  const messages: Record<string, MessageRecord> = {}

  supportedLocales.forEach(locale => {
    messages[locale] = {}
  })

  for (const [path, module] of Object.entries(translationModules)) {
    if (path.includes('/index.ts')) continue
    const locale = extractLocaleFromPath(path)
    if (!locale || !supportedLocales.includes(locale)) continue
    const moduleMessages = module.default || {}
    const nestedMessages = buildNestedMessages(path, moduleMessages)
    messages[locale] = deepMerge(messages[locale] ?? {}, nestedMessages)
  }

  return messages
}

// 初始化：从 glob 扫描结果动态得到语言列表并加载静态翻译
const supportedLocales: string[] = getSupportedLocalesFromModules()
const staticMessages = loadStaticMessages(supportedLocales)

// 创建 i18n 实例
const i18n = createI18n({
  legacy: false,
  locale: localStorage.getItem('locale') || 'zh-CN',
  // 缺失翻译统一回退到英文（en-US）；若英文也缺失，再由 missing 返回键名
  fallbackLocale: 'en-US',
  messages: staticMessages as unknown as Record<string, never>,
  missingWarn: import.meta.env.DEV,
  fallbackWarn: false,
  missing: (_locale: string, key: string) => key
}) as I18n

/**
 * 从后端加载翻译数据并合并到 i18n
 * @param cultureCode 语言编码（如：zh-CN、en-US）
 * @param resourceType 资源类型（Frontend=前端，Backend=后端）
 */
export async function loadTranslationsFromBackend(cultureCode: string, resourceType: string = 'Frontend'): Promise<void> {
  try {
    const { useTranslationStore } = await import('@/stores/routine/localization/translation')
    const translationStore = useTranslationStore()
    
    // 从后端加载翻译数据
    const translations = await translationStore.loadTranslations(cultureCode, resourceType)
    
    if (Object.keys(translations).length > 0) {
      // 将后端翻译数据转换为嵌套对象结构
      // 后端返回格式：{ resourceKey: translationValue }
      // 如果 resourceKey 包含点号（如：common.app.htmlTitle），则转换为嵌套结构
      const backendMessages: MessageRecord = {}
      
      // 按路径长度排序：先处理长路径（更具体的，如 menu.logistics.material.purchasing），
      // 再处理短路径（更通用的，如 menu.logistics），避免短路径覆盖嵌套结构
      const sortedEntries = Object.entries(translations).sort(([keyA], [keyB]) => {
        const depthA = keyA.split('.').length
        const depthB = keyB.split('.').length
        return depthB - depthA // 降序：长路径在前
      })
      
      sortedEntries.forEach(([key, value]) => {
        const keys = key.split('.')
        let current: MessageRecord = backendMessages
        for (let i = 0; i < keys.length - 1; i++) {
          const k = keys[i]
          if (k === undefined) break
          const existing = current[k]
          // 若该节点已是基本类型（如 menu.logistics 为 "采购管理"），不能在其上挂子键
          // 改为空对象并继续，避免 "Cannot create property 'supplier' on string '采购管理'"
          // 注意：由于已按路径长度排序，长路径会先处理，所以这种情况应该较少发生
          if (existing === undefined || existing === null || typeof existing !== 'object' || Array.isArray(existing)) {
            if (import.meta.env.DEV && existing !== undefined && existing !== null) {
              logger.debug(`[I18n] 后端翻译路径冲突，将 "${keys.slice(0, i + 1).join('.')}" 由基本类型改为嵌套对象，以便挂载 "${key}"`)
            }
            current[k] = {}
          }
          current = current[k] as MessageRecord
        }
        // 如果目标位置已经是对象（来自更长的路径），且当前值是字符串，则保留对象结构
        const lastKey = keys[keys.length - 1]
        if (lastKey === undefined) return
        if (typeof current[lastKey] === 'object' && current[lastKey] !== null && !Array.isArray(current[lastKey])) {
          // 目标位置已经是对象，说明有更长的路径已经创建了嵌套结构，短路径忽略（后端实体名已改为 entity.xxx._self，此处仅兼容旧数据）
          if (import.meta.env.DEV) {
            logger.debug(`[I18n] 后端翻译路径冲突，忽略短路径 "${key}"，因为已有更具体的嵌套路径`)
          }
        } else {
          current[lastKey] = value
        }
      })
      
      // 合并到 i18n messages（后端翻译优先于静态翻译）
      // 使用深度合并，确保嵌套结构正确合并
      // 使用 unref 安全地获取 messages 的值
      const messagesValue = unref(i18n.global.messages) as Record<string, unknown>
      const currentMessages = isRecord(messagesValue[cultureCode]) ? messagesValue[cultureCode] : {}
      const mergedMessages = deepMerge(currentMessages, backendMessages)
      
      // 设置翻译消息，这会触发 vue-i18n 的响应式更新
      i18n.global.setLocaleMessage(cultureCode, mergedMessages)
      
      // 确保 locale 正确设置，触发组件重新渲染
      const localeRef = i18n.global.locale as { value: string }
      if (localeRef.value !== cultureCode) {
        localeRef.value = cultureCode
      }
      
      // 调试日志：输出合并后的消息结构
      if (import.meta.env.DEV) {
        logger.debug(`[I18n] 翻译消息已设置 (${cultureCode}):`, {
          messageKeys: Object.keys(mergedMessages).slice(0, 10),
          totalKeys: Object.keys(translations).length
        })
      }
      
      logger.info(`[I18n] 已加载后端翻译数据 (${cultureCode}, ${resourceType}):`, Object.keys(translations).length, '条')
    } else {
      logger.warn(`[I18n] 未找到后端翻译数据 (${cultureCode}, ${resourceType})`)
    }
  } catch (error) {
    logger.error(`[I18n] 加载后端翻译数据失败 (${cultureCode}, ${resourceType}):`, error)
  }
}

/**
 * 初始化 i18n（在应用启动时调用）
 * @param app Vue 应用实例
 */
export function setupI18n(app: App) {
  app.use(i18n)
  
  // 静态翻译已在初始化时加载完成
  // 后端翻译数据将在用户登录并获取权限后加载
  logger.info(`[I18n] 已加载支持的语言:`, supportedLocales)
}

/**
 * 切换语言并加载对应的翻译数据
 * @param locale 语言编码
 */
export async function setLocale(locale: string) {
  // 使用类型断言设置 locale，因为在实际运行时它总是 WritableComputedRef
  const localeRef = i18n.global.locale as { value: string }
  localeRef.value = locale
  localStorage.setItem('locale', locale)
  
  // 加载新语言的翻译数据
  await loadTranslationsFromBackend(locale, 'Frontend')
}

export default i18n
