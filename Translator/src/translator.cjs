/**
 * Takt 智能翻译器 - 核心模块
 * 前端后端通用，基于 LibreTranslate API
 */

const { readFileSync, writeFileSync, readdirSync, existsSync, statSync, mkdirSync } = require('fs')
const { join, dirname, relative, extname } = require('path')

// 语言配置
const LANG_CONFIG = {
  // 标准 6 种语言
  STANDARD: ['en-US', 'ja-JP', 'ko-KR', 'zh-HK', 'zh-TW'],
  
  // 可选 4 种语言
  OPTIONAL: ['fr-FR', 'ru-RU', 'ar-SA', 'es-ES'],
  
  // LibreTranslate 语言代码映射
  CODE_MAP: {
    'zh-CN': 'zh',
    'en-US': 'en',
    'ja-JP': 'ja',
    'ko-KR': 'ko',
    'zh-TW': 'zh-Hant',
    'zh-HK': 'zh-Hant',
    'fr-FR': 'fr',
    'ru-RU': 'ru',
    'ar-SA': 'ar',
    'es-ES': 'es'
  }
}

/**
 * 翻译器类
 */
class Translator {
  constructor(options = {}) {
    // 默认使用官方公共 API（免费，无需部署）
    this.apiUrl = options.apiUrl || 'https://libretranslate.de'
    this.sourceLang = options.sourceLang || 'zh-CN'
    this.targetLangs = options.targetLangs || LANG_CONFIG.STANDARD
    this.delay = options.delay || 100 // 请求延迟（毫秒）
    this.verbose = options.verbose || false
    this.apiKey = options.apiKey || null // API 密钥（可选）
  }

  /**
   * 测试 API 连接
   */
  async testConnection() {
    try {
      const response = await fetch(`${this.apiUrl}/languages`, {
        method: 'GET'
      })
      
      if (!response.ok) {
        throw new Error(`HTTP ${response.status}`)
      }
      
      const languages = await response.json()
      this.log(`✅ API 连接成功，支持 ${languages.length} 种语言`)
      return true
    } catch (error) {
      this.log(`❌ API 连接失败: ${error.message}`)
      return false
    }
  }

  /**
   * 翻译文本
   */
  async translateText(text, targetLang) {
    if (!text || text.trim() === '') {
      return text
    }

    const sourceCode = LANG_CONFIG.CODE_MAP[this.sourceLang]
    const targetCode = LANG_CONFIG.CODE_MAP[targetLang]

    if (!sourceCode || !targetCode) {
      throw new Error(`不支持的语言: ${this.sourceLang} -> ${targetLang}`)
    }

    const body = {
      q: text,
      source: sourceCode,
      target: targetCode,
      format: 'text'
    }

    // 如果有 API Key，添加到请求中
    if (this.apiKey) {
      body.api_key = this.apiKey
    }

    try {
      const response = await fetch(`${this.apiUrl}/translate`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
      })

      if (!response.ok) {
        const error = await response.text()
        throw new Error(`API 错误: ${response.status} - ${error}`)
      }

      const data = await response.json()
      return data.translatedText || text
    } catch (error) {
      this.log(`   ⚠️  翻译失败: ${error.message}`)
      return text // 失败时返回原文
    }
  }

  /**
   * 提取文件中的文本并翻译
   */
  async translateFile(sourceFile, targetLang, options = {}) {
    const {
      extractPattern = /'([^']*)'/g,  // 提取模式
      replacePattern = true,           // 是否替换
      headerComment = null            // 头部注释替换
    } = options

    const content = readFileSync(sourceFile, 'utf-8')
    let result = content

    // 提取所有匹配的文本
    const matches = []
    let match
    
    const regex = new RegExp(extractPattern.source, extractPattern.flags)
    while ((match = regex.exec(content)) !== null) {
      matches.push({
        full: match[0],
        text: match[1],
        index: match.index
      })
    }

    if (matches.length === 0) {
      this.log(`   ℹ️  未找到可翻译的文本`)
      return content
    }

    this.log(`   📝 翻译 ${matches.length} 个字符串...`)

    // 翻译每个字符串
    for (const item of matches) {
      // 跳过非中文内容
      if (!/[\u4e00-\u9fa5]/.test(item.text)) {
        continue
      }

      const translated = await this.translateText(item.text, targetLang)
      
      if (replacePattern) {
        result = result.replace(item.full, `'${translated}'`)
      }

      // 延迟避免请求过快
      await this.sleep(this.delay)
    }

    // 更新头部注释
    if (headerComment) {
      result = this.updateHeaderComment(result, headerComment)
    }

    return result
  }

  /**
   * 更新文件头部注释
   */
  updateHeaderComment(content, commentTemplate) {
    // 根据不同的注释格式进行替换
    const patterns = [
      [/\/\*\*\n \* (.+) · 中文/g, `/**\n * ${commentTemplate}`],
      [/\/\*\*\n \* (.+)（中文）。/g, `/**\n * ${commentTemplate}。`],
      [/\/\/ (.+) - 中文/g, `// ${commentTemplate}`]
    ]

    let result = content
    for (const [pattern, replacement] of patterns) {
      result = result.replace(pattern, replacement)
    }

    return result
  }

  /**
   * 批量翻译目录
   */
  async translateDirectory(sourceDir, options = {}) {
    const {
      filePattern = '**/zh-CN.*',     // 文件匹配模式
      outputDir = null,               // 输出目录（默认同目录）
      recursive = true                // 是否递归
    } = options

    const files = this.findFiles(sourceDir, filePattern, recursive)
    
    if (files.length === 0) {
      this.log(`⚠️  未找到源文件: ${filePattern}`)
      return { processed: 0, success: 0, failed: 0 }
    }

    this.log(`📂 找到 ${files.length} 个源文件`)

    let processed = 0
    let success = 0
    let failed = 0

    for (const sourceFile of files) {
      this.log(`\n📄 ${relative(sourceDir, sourceFile)}`)

      for (const targetLang of this.targetLangs) {
        try {
          const targetFile = this.getTargetFilePath(sourceFile, targetLang, outputDir)
          
          const translatedContent = await this.translateFile(sourceFile, targetLang, {
            headerComment: this.getLanguageName(targetLang)
          })

          const dir = dirname(targetFile)
          if (!existsSync(dir)) {
            mkdirSync(dir, { recursive: true })
          }

          writeFileSync(targetFile, translatedContent, 'utf-8')
          this.log(`   ✅ ${targetLang}`)
          success++
        } catch (error) {
          this.log(`   ❌ ${targetLang}: ${error.message}`)
          failed++
        }
      }

      processed++
    }

    return { processed, success, failed }
  }

  /**
   * 查找文件
   */
  findFiles(dir, pattern, recursive = true) {
    const files = []
    const entries = readdirSync(dir, { withFileTypes: true })

    for (const entry of entries) {
      const fullPath = join(dir, entry.name)
      
      if (entry.isDirectory() && recursive) {
        files.push(...this.findFiles(fullPath, pattern, recursive))
      } else if (entry.isFile()) {
        // 简单的文件名匹配
        const fileName = entry.name
        if (fileName.includes('zh-CN') || fileName === 'zh-CN.ts' || fileName === 'zh-CN.json') {
          files.push(fullPath)
        }
      }
    }

    return files
  }

  /**
   * 获取目标文件路径
   */
  getTargetFilePath(sourceFile, targetLang, outputDir = null) {
    const dir = outputDir || dirname(sourceFile)
    const ext = extname(sourceFile)
    const baseName = sourceFile.replace(/zh-CN/, '').replace(ext, '')
    
    return join(dir, `${baseName}${targetLang}${ext}`)
  }

  /**
   * 获取语言名称
   */
  getLanguageName(langCode) {
    const names = {
      'en-US': 'English',
      'ja-JP': '日本語',
      'ko-KR': '한국어',
      'zh-TW': '繁體中文（台灣）',
      'zh-HK': '繁體中文（香港）',
      'fr-FR': 'Français',
      'ru-RU': 'Русский',
      'ar-SA': 'العربية',
      'es-ES': 'Español'
    }
    return names[langCode] || langCode
  }

  /**
   * 延迟函数
   */
  sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms))
  }

  /**
   * 日志输出
   */
  log(message) {
    if (this.verbose) {
      console.log(message)
    }
  }
}

/**
 * 创建翻译器实例
 */
function createTranslator(options = {}) {
  return new Translator(options)
}

module.exports = {
  Translator,
  createTranslator,
  LANG_CONFIG
}
