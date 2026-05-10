# Takt Translator - 智能翻译工具

前端后端通用的智能翻译工具，基于 LibreTranslate API 实现语义级翻译。

## ✨ 特性

- 🌍 **支持 10 种语言**: 英语、日语、韩语、繁体中文（台湾/香港）、法语、俄语、阿拉伯语、西班牙语
- 🤖 **真正的智能翻译**: 基于 AI 语义理解，不是硬编码替换
- 🔄 **前后端通用**: 可用于前端 locales 和后端资源文件
- 📦 **独立工具**: 不依赖项目，可在任何目录使用
- ⚙️ **灵活配置**: 支持自定义 API 地址、延迟、语言选择

## 📦 安装

### 1. 安装 Node.js

确保已安装 Node.js >= 18.0.0

```bash
node --version
```

### 2. 选择 API 服务（二选一）

#### 方式一：使用官方公共 API（推荐，免费）
- ✅ **无需部署**，直接使用
- ✅ API 地址: `https://libretranslate.de`
- ✅ 默认已配置，开箱即用
- ⚠️ 有请求限额，大量翻译建议申请 API Key

#### 方式二：自建服务（适合大量翻译）
```bash
docker run -ti --rm -p 5000:5000 libretranslate/libretranslate
```
首次启动会下载语言模型（约 10GB），需要等待几分钟。

### 3. 测试 API（可选）

```bash
# 测试公共 API
curl https://libretranslate.de/languages

# 或测试本地服务
curl http://localhost:5000/languages
```

## 🚀 快速开始

### 基础用法

```bash
# 进入 Translator 目录
cd d:\GitHub\Takt.Net\Translator

# 使用官方公共 API（默认，免费）
node cli.cjs ../frontend/takt.antd/src/locales

# 翻译后端资源文件
node cli.cjs ../backend/src/Takt.WebApi/Resources

# 生成全部 10 种语言
node cli.cjs --all ../frontend/takt.antd/src/locales
```

### 高级用法

```bash
# 使用本地自建服务
node cli.cjs --api http://localhost:5000 ./locales

# 使用 API Key（提高限额）
node cli.cjs --key your-api-key ./locales

# 调整请求延迟（毫秒）
node cli.cjs --delay 200 ./locales

# 显示详细日志
node cli.cjs --verbose ./locales

# 查看帮助
node cli.cjs --help
```

## 📋 命令行参数

| 参数 | 简写 | 说明 | 默认值 |
|------|------|------|--------|
| `--help` | `-h` | 显示帮助信息 | - |
| `--all` | `-a` | 生成全部 10 种语言 | false (6种) |
| `--verbose` | `-v` | 显示详细输出 | false |
| `--api <url>` | - | LibreTranslate API 地址 | https://libretranslate.de |
| `--key <key>` | - | API 密钥（提高限额） | null |
| `--delay <ms>` | - | 请求延迟（毫秒） | 100 |
| `--dir <path>` | - | 源文件目录 | 必需参数 |

## 🌐 支持的语言

### 标准 6 种（默认）
- ✅ en-US (英语)
- ✅ ja-JP (日语)
- ✅ ko-KR (韩语)
- ✅ zh-TW (台湾繁体)
- ✅ zh-HK (香港繁体)
- ⏭️ zh-CN (简体中文) - 源文件

### 可选 4 种（使用 `--all`）
- 🌍 fr-FR (法语)
- 🌍 ru-RU (俄语)
- 🌍 ar-SA (阿拉伯语)
- 🌍 es-ES (西班牙语)

## 📁 项目结构

```
Translator/
├── cli.js                 # CLI 命令行工具
├── package.json           # 包配置
├── README.md             # 使用说明
└── src/
    └── translator.js     # 翻译器核心模块
```

## 🔧 作为模块使用

```javascript
import { createTranslator } from './src/translator.js'

const translator = createTranslator({
  apiUrl: 'https://libretranslate.de',  // 官方公共 API
  // 或 apiUrl: 'http://localhost:5000', // 本地自建服务
  apiKey: 'your-api-key',               // 可选，提高限额
  sourceLang: 'zh-CN',
  targetLangs: ['en-US', 'ja-JP', 'ko-KR'],
  delay: 100,
  verbose: true
})

// 测试连接
await translator.testConnection()

// 翻译单个文件
const content = await translator.translateFile('./zh-CN.json', 'en-US')

// 批量翻译目录
const result = await translator.translateDirectory('./locales')
```

## 💡 使用示例

### 前端翻译

```bash
# 翻译 Vue/React locales
node cli.js ../frontend/takt.antd/src/locales
```

### 后端翻译

```bash
# 翻译 .NET Resources
node cli.js ../backend/src/Takt.WebApi/Resources

# 翻译 JSON 配置
node cli.js ../backend/config/i18n
```

### 自定义项目

```bash
# 任何包含 zh-CN 文件的目录
node cli.js ./my-project/i18n
```

## ⚠️ 注意事项

1. **翻译质量**
   - 翻译由 AI 自动生成，建议人工校对
   - 专业术语可能需要调整
   - 特定语境可能需要手动优化

2. **性能**
   - 每个字符串有 100ms 延迟
   - 可通过 `--delay` 参数调整
   - 大量文件可能需要较长时间

3. **失败处理**
   - API 调用失败会保留原文
   - 可重新运行脚本继续翻译

4. **文件覆盖**
   - 脚本会覆盖已有的翻译文件
   - 建议先备份重要文件

## 🐛 故障排除

### API 连接失败

```
❌ 无法连接到 LibreTranslate API
```

**解决方案：**
```bash
# 1. 检查网络连接
ping libretranslate.de

# 2. 测试公共 API
curl https://libretranslate.de/languages

# 3. 如果使用本地服务，检查 Docker
docker ps | grep libretranslate

# 4. 重新启动本地服务
docker run -ti --rm -p 5000:5000 libretranslate/libretranslate
```

### 翻译质量不佳

**解决方案：**
1. 尝试使用其他公共 API 节点（见下方列表）
2. 自建服务获取更好质量
3. 手动调整翻译结果

### 内存不足（仅自建服务）

LibreTranslate 自建服务首次启动需要下载约 10GB 模型。

**解决方案：**
1. 确保有足够的磁盘空间（建议 20GB+）
2. 或使用公共 API（无需下载）

## 📝 开发

### 添加新语言

编辑 `src/translator.js`:

```javascript
export const LANG_CONFIG = {
  CODE_MAP: {
    // ... 现有语言
    'de-DE': 'de',  // 添加德语
  }
}
```

### 自定义提取规则

```javascript
const translator = createTranslator({
  // ...
})

await translator.translateFile('./source.json', 'en-US', {
  extractPattern: /"([^"]*)"/g,  // 自定义正则
  headerComment: 'English'
})
```

## 📄 许可证

MIT License

## 🔗 相关链接

- [LibreTranslate](https://github.com/LibreTranslate/LibreTranslate)
- [LibreTranslate API](https://libretranslate.com/docs)
- [Takt.Net 项目](https://github.com/your-org/Takt.Net)

## 🌐 公共 API 节点列表

| 节点 | 地址 | 说明 |
|------|------|------|
| 官方（德国） | https://libretranslate.de | 默认，稳定 |
| 官方（美国） | https://translate.api.libretranslate.com | 需要 API Key |
| 社区节点 | https://libretranslate.net | 备用节点 |
| 自建服务 | http://localhost:5000 | 无限制 |

**注意：** 公共 API 有请求限额，如需大量翻译建议：
1. 申请 API Key: https://portal.libretranslate.com/
2. 或使用自建服务
