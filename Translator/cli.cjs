#!/usr/bin/env node

/**
 * Takt 智能翻译 CLI
 * 前端后端通用的命令行翻译工具
 */

const { createTranslator, LANG_CONFIG } = require('./src/translator.cjs')

// 解析命令行参数
function parseArgs() {
  const args = process.argv.slice(2)
  const options = {
    sourceDir: null,
    apiUrl: 'https://libretranslate.de',  // 默认使用官方公共 API
    apiKey: null,                          // API 密钥（可选）
    all: false,
    verbose: false,
    help: false,
    delay: 100
  }

  let i = 0
  while (i < args.length) {
    const arg = args[i]
    
    switch (arg) {
      case '--help':
      case '-h':
        options.help = true
        break
      
      case '--all':
      case '-a':
        options.all = true
        break
      
      case '--verbose':
      case '-v':
        options.verbose = true
        break
      
      case '--api':
        options.apiUrl = args[++i]
        break
      
      case '--key':
        options.apiKey = args[++i]
        break
      
      case '--delay':
        options.delay = parseInt(args[++i]) || 100
        break
      
      case '--dir':
      case '--source':
        options.sourceDir = args[++i]
        break
      
      default:
        if (!options.sourceDir) {
          options.sourceDir = arg
        }
    }
    
    i++
  }

  return options
}

// 显示帮助信息
function showHelp() {
  console.log(`
🌍 Takt 智能翻译工具 - 前端后端通用

用法:
  node cli.cjs [选项] [源目录]

选项:
  --help, -h          显示帮助信息
  --all, -a           生成全部 10 种语言（默认 6 种）
  --verbose, -v       显示详细输出
  --api <url>         LibreTranslate API 地址 (默认: https://libretranslate.de)
  --key <key>         API 密钥（可选，用于提高限额）
  --delay <ms>        请求延迟毫秒数 (默认: 100)
  --dir, --source     源文件目录路径

示例:
  # 使用官方公共 API（免费，无需部署）
  node cli.cjs ../frontend/takt.antd/src/locales

  # 使用自定义 API
  node cli.cjs --api http://localhost:5000 ./locales

  # 使用 API Key（提高限额）
  node cli.cjs --key your-api-key ./locales

  # 生成全部语言
  node cli.cjs --all ../frontend/takt.antd/src/locales

支持的语言:
  标准 6 种: en-US, ja-JP, ko-KR, zh-HK, zh-TW
  可选 4 种: fr-FR, ru-RU, ar-SA, es-ES (使用 --all)

前提条件:
  1. 安装 Node.js >= 18.0.0
  2. 使用官方公共 API（免费）或自建服务:
     - 公共 API: https://libretranslate.de (默认)
     - 自建: docker run -ti --rm -p 5000:5000 libretranslate/libretranslate

更多信息:
  https://github.com/LibreTranslate/LibreTranslate
`)
}

// 主函数
async function main() {
  const options = parseArgs()

  // 显示帮助
  if (options.help) {
    showHelp()
    process.exit(0)
  }

  console.log('🌍 Takt 智能翻译工具')
  console.log('='.repeat(60))

  // 检查源目录
  if (!options.sourceDir) {
    console.error('❌ 请指定源目录')
    console.log('用法: node cli.cjs <源目录>')
    console.log('使用 --help 查看详细信息')
    process.exit(1)
  }

  // 配置翻译器
  const targetLangs = options.all
    ? [...LANG_CONFIG.STANDARD, ...LANG_CONFIG.OPTIONAL]
    : LANG_CONFIG.STANDARD

  const translator = createTranslator({
    apiUrl: options.apiUrl,
    apiKey: options.apiKey,
    sourceLang: 'zh-CN',
    targetLangs: targetLangs,
    delay: options.delay,
    verbose: options.verbose || true
  })

  console.log(`📂 源目录: ${options.sourceDir}`)
  console.log(`🔗 API: ${options.apiUrl}`)
  console.log(`🌐 语言: ${targetLangs.join(', ')}`)
  console.log('')

  // 测试 API 连接
  console.log('🔍 测试 API 连接...')
  const connected = await translator.testConnection()
  
  if (!connected) {
    console.error('\n❌ 无法连接到 LibreTranslate API')
    console.error('请检查:')
    console.error('  1. 网络连接是否正常')
    console.error('  2. API 地址是否正确')
    console.error('  3. 公共 API: https://libretranslate.de')
    console.error('  4. 自建服务: docker run -ti --rm -p 5000:5000 libretranslate/libretranslate')
    process.exit(1)
  }

  // 执行翻译
  console.log('\n🚀 开始翻译...')
  const startTime = Date.now()

  try {
    const result = await translator.translateDirectory(options.sourceDir)

    const duration = ((Date.now() - startTime) / 1000).toFixed(2)
    
    console.log('\n' + '='.repeat(60))
    console.log('✨ 翻译完成！')
    console.log('='.repeat(60))
    console.log(`📊 统计:`)
    console.log(`   处理文件: ${result.processed}`)
    console.log(`   成功: ${result.success}`)
    console.log(`   失败: ${result.failed}`)
    console.log(`   耗时: ${duration}秒`)
    console.log('')
    console.log('⚠️  注意事项:')
    console.log('   1. 翻译由 AI 自动生成，建议人工校对')
    console.log('   2. 专业术语可能需要调整')
    console.log('   3. 失败的文件可以重新运行脚本')
    console.log('')

    if (result.failed > 0) {
      process.exit(1)
    }
  } catch (error) {
    console.error('\n❌ 翻译失败:', error.message)
    process.exit(1)
  }
}

// 运行
main().catch(error => {
  console.error('❌ 错误:', error)
  process.exit(1)
})
