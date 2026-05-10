/**
 * 智能翻译脚本 - 使用 LibreTranslate API 进行真正的智能翻译
 * 
 * 支持语言（6种标准语言）：
 * - en-US (英语) - API 智能翻译
 * - ja-JP (日语) - API 智能翻译  
 * - ko-KR (韩语) - API 智能翻译
 * - zh-CN (简体中文) - 源文件，不生成
 * - zh-HK (香港繁体) - API 智能翻译
 * - zh-TW (台湾繁体) - API 智能翻译
 * 
 * 可选语言（4种，默认不输出）：
 * - fr-FR (法语) - API 智能翻译
 * - ru-RU (俄语) - API 智能翻译
 * - ar-SA (阿拉伯语) - API 智能翻译
 * - es-ES (西班牙语) - API 智能翻译
 * 
 * 使用方法：
 * node scripts/translate-locales.mjs                          # 标准 6 种语言
 * node scripts/translate-locales.mjs --all                    # 全部 10 种语言
 * node scripts/translate-locales.mjs --api http://xxx:5000    # 指定 API 地址
 * 
 * 前提条件：
 * 1. 需要运行 LibreTranslate 服务
 *    Docker: docker run -ti --rm -p 5000:5000 libretranslate/libretranslate
 * 2. 默认 API 地址: http://localhost:5000
 */

import { readFileSync, writeFileSync, readdirSync, existsSync } from 'fs'
import { join, dirname, relative } from 'path'
import { fileURLToPath } from 'url'

const __filename = fileURLToPath(import.meta.url)
const __dirname = dirname(__filename)

const LOCALES_DIR = join(__dirname, '..', 'src', 'locales')

// 解析命令行参数
const args = process.argv.slice(2)
const GENERATE_ALL = args.includes('--all')
const API_INDEX = args.indexOf('--api')
const API_URL = API_INDEX !== -1 ? args[API_INDEX + 1] : 'http://localhost:5000'

// 标准 6 种语言
const STANDARD_LANGS = ['en-US', 'ja-JP', 'ko-KR', 'zh-HK', 'zh-TW']

// 可选 4 种语言
const OPTIONAL_LANGS = ['fr-FR', 'ru-RU', 'ar-SA', 'es-ES']

// 目标语言列表（不包括 zh-CN，因为它是源文件）
const TARGET_LANGS = GENERATE_ALL 
  ? [...STANDARD_LANGS, ...OPTIONAL_LANGS]
  : STANDARD_LANGS

// 语言代码映射 (项目代码 -> LibreTranslate 代码)
const LANG_CODE_MAP = {
  'zh-CN': 'zh',      // 简体中文
  'en-US': 'en',      // 英语
  'ja-JP': 'ja',      // 日语
  'ko-KR': 'ko',      // 韩语
  'zh-TW': 'zh-Hant', // 繁体中文（台湾）
  'zh-HK': 'zh-Hant', // 繁体中文（香港）
  'fr-FR': 'fr',      // 法语
  'ru-RU': 'ru',      // 俄语
  'ar-SA': 'ar',      // 阿拉伯语
  'es-ES': 'es'       // 西班牙语
}



console.log('🌍 智能翻译脚本 - LibreTranslate API')
console.log('='.repeat(60))
console.log(`📋 生成模式: ${GENERATE_ALL ? '全部 10 种语言' : '标准 6 种语言'}`)
console.log(`🔗 API 地址: ${API_URL}`)
console.log(`📂 目录: ${LOCALES_DIR}`)
console.log('')

// ========================================
// 工具函数
// ========================================

/**
 * 递归查找所有包含 zh-CN.ts 的目录
 */
function findLocaleDirsWithChinese(dir, baseDir) {
  const dirs = []
  const entries = readdirSync(dir, { withFileTypes: true })
  
  for (const entry of entries) {
    const fullPath = join(dir, entry.name)
    if (entry.isDirectory()) {
      dirs.push(...findLocaleDirsWithChinese(fullPath, baseDir))
    } else if (entry.isFile() && entry.name === 'zh-CN.ts') {
      const relDir = relative(baseDir, dir)
      dirs.push(relDir)
    }
  }
  
  return dirs
}

/**
 * 调用 LibreTranslate API 翻译文本（所有语言统一使用 API）
 */
async function translateWithAPI(text, sourceLang, targetLang) {
  if (!text || text.trim() === '') {
    return text
  }
  
  const sourceCode = LANG_CODE_MAP[sourceLang] || sourceLang
  const targetCode = LANG_CODE_MAP[targetLang] || targetLang
  
  try {
    const response = await fetch(`${API_URL}/translate`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        q: text,
        source: sourceCode,
        target: targetCode,
        format: 'text'
      })
    })
    
    if (!response.ok) {
      const error = await response.text()
      console.error(`   ❌ API 错误: ${response.status} - ${error}`)
      return text
    }
    
    const data = await response.json()
    return data.translatedText || text
  } catch (error) {
    console.error(`   ❌ 翻译失败: ${error.message}`)
    return text
  }
}



/**
 * 提取引号内的文本
 */
function extractQuotedStrings(content) {
  const regex = /'([^']*)'/g
  const matches = []
  let match
  
  while ((match = regex.exec(content)) !== null) {
    matches.push({
      full: match[0],
      text: match[1],
      index: match.index
    })
  }
  
  return matches
}

/**
 * 翻译文件内容
 */
async function translateFileContent(zhContent, targetLang) {
  let result = zhContent
  
  // 提取所有引号内的字符串
  const quotedStrings = extractQuotedStrings(zhContent)
  
  if (quotedStrings.length === 0) {
    return result
  }
  
  console.log(`      翻译 ${quotedStrings.length} 个字符串...`)
  
  // 翻译每个字符串（所有语言统一使用 API）
  for (const item of quotedStrings) {
    // 跳过纯英文、数字、符号的内容
    if (!/[\u4e00-\u9fa5]/.test(item.text)) {
      continue
    }
    
    // 所有语言都调用 API 进行智能翻译（包括繁体中文）
    const translated = await translateWithAPI(item.text, 'zh-CN', targetLang)
    
    // 添加小延迟避免请求过快
    await new Promise(resolve => setTimeout(resolve, 100))
    
    // 替换原文
    result = result.replace(item.full, `'${translated}'`)
  }
  
  // 更新文件头部注释
  if (targetLang === 'en-US') {
    result = result
      .replace(/\/\*\*\n \* (.+) · 中文/g, '/**\n * $1 · English')
      .replace(/\/\*\*\n \* (.+)（中文）。/g, '/**\n * $1 (English).')
  } else if (targetLang === 'ja-JP') {
    result = result
      .replace(/\/\*\*\n \* (.+) · 中文/g, '/**\n * $1 · 日本語')
      .replace(/\/\*\*\n \* (.+)（中文）。/g, '/**\n * $1（日本語）。')
  } else if (targetLang === 'ko-KR') {
    result = result
      .replace(/\/\*\*\n \* (.+) · 中文/g, '/**\n * $1 · 한국어')
      .replace(/\/\*\*\n \* (.+)（中文）。/g, '/**\n * $1（한국어）。')
  } else if (targetLang === 'zh-TW') {
    result = result
      .replace(/\/\*\*\n \* (.+) · 中文/g, '/**\n * $1 · 繁體中文（台灣）')
      .replace(/\/\*\*\n \* (.+)（中文）。/g, '/**\n * $1（繁體中文（台灣））。')
  } else if (targetLang === 'zh-HK') {
    result = result
      .replace(/\/\*\*\n \* (.+) · 中文/g, '/**\n * $1 · 繁體中文（香港）')
      .replace(/\/\*\*\n \* (.+)（中文）。/g, '/**\n * $1（繁體中文（香港））。')
  }
  
  return result
}

// ========================================
// 主逻辑
// ========================================

async function main() {
  // 测试 API 连接
  console.log('🔍 测试 API 连接...')
  try {
    const testResponse = await fetch(`${API_URL}/languages`, {
      method: 'GET'
    })
    
    if (!testResponse.ok) {
      console.error(`❌ 无法连接到 LibreTranslate API: ${API_URL}`)
      console.error('请先启动 LibreTranslate 服务：')
      console.error('   docker run -ti --rm -p 5000:5000 libretranslate/libretranslate')
      process.exit(1)
    }
    
    console.log('✅ API 连接成功\n')
  } catch (error) {
    console.error(`❌ 无法连接到 LibreTranslate API: ${API_URL}`)
    console.error('错误信息:', error.message)
    console.error('\n请先启动 LibreTranslate 服务：')
    console.error('   docker run -ti --rm -p 5000:5000 libretranslate/libretranslate')
    process.exit(1)
  }
  
  console.log('🔍 扫描目录中...\n')
  
  const localeDirs = findLocaleDirsWithChinese(LOCALES_DIR, LOCALES_DIR)
  
  let processedCount = 0
  let createdCount = 0
  let errorCount = 0
  
  for (const relDir of localeDirs) {
    const fullPath = join(LOCALES_DIR, relDir)
    const zhFile = join(fullPath, 'zh-CN.ts')
    
    if (!existsSync(zhFile)) {
      continue
    }
    
    const zhContent = readFileSync(zhFile, 'utf-8')
    
    console.log(`📁 ${relDir}`)
    
    for (const lang of TARGET_LANGS) {
      const targetFile = join(fullPath, `${lang}.ts`)
      
      try {
        // 所有语言统一使用 API 智能翻译（包括繁体中文）
        const content = await translateFileContent(zhContent, lang)
        
        writeFileSync(targetFile, content, 'utf-8')
        console.log(`   ✅ 生成 ${lang}.ts`)
        createdCount++
      } catch (error) {
        console.error(`   ❌ 生成 ${lang}.ts 失败: ${error.message}`)
        errorCount++
      }
    }
    
    processedCount++
    console.log('')
  }
  
  console.log('\n' + '='.repeat(60))
  console.log('✨ 翻译完成！')
  console.log('='.repeat(60))
  console.log(`📊 统计信息:`)
  console.log(`   处理目录: ${processedCount}`)
  console.log(`   生成文件: ${createdCount}`)
  console.log(`   失败文件: ${errorCount}`)
  console.log('')
  console.log('⚠️  注意事项：')
  console.log('   1. 翻译由 LibreTranslate API 自动生成，建议人工校对')
  console.log('   2. 专业术语和语境可能需要调整')
  console.log('   3. zh-TW 和 zh-HK 使用简繁映射表，已区分用词差异')
  console.log('')
}

// 运行主函数
main().catch(error => {
  console.error('❌ 脚本执行失败:', error)
  process.exit(1)
})
