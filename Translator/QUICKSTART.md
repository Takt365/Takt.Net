# Takt Translator - 快速开始指南

## 🚀 5 分钟快速上手

### 步骤 1: 选择 API 服务（二选一）

#### 方式一：使用官方公共 API（推荐）
- ✅ **无需部署**，直接使用
- ✅ API 地址: `https://libretranslate.de`
- ✅ 默认已配置，开箱即用
- ⚠️ 有请求限额，大量翻译建议申请 API Key 或自建服务

#### 方式二：自建服务（适合大量翻译）
```bash
docker run -ti --rm -p 5000:5000 libretranslate/libretranslate
```
等待服务启动完成（首次需要下载模型，约 5-10 分钟）。

### 步骤 2: 测试服务（可选）

```bash
# 测试公共 API
curl https://libretranslate.de/languages

# 或测试本地服务
curl http://localhost:5000/languages
```

### 步骤 3: 使用翻译工具

#### 方式一：前端翻译

```bash
cd d:\GitHub\Takt.Net\frontend\takt.antd
node scripts/translate.mjs
```

#### 方式二：后端翻译

```bash
cd d:\GitHub\Takt.Net\backend
node scripts/translate.mjs
```

#### 方式三：直接使用 Translator

```bash
cd d:\GitHub\Takt.Net\Translator

# 翻译前端
node cli.cjs ../frontend/takt.antd/src/locales

# 翻译后端
node cli.cjs ../backend/src/Takt.WebApi/Resources

# 生成全部 10 种语言
node cli.cjs --all ../frontend/takt.antd/src/locales
```

## 📋 常用命令

```bash
# 查看帮助
node cli.cjs --help

# 使用公共 API（默认，无需部署）
node cli.cjs ./locales

# 使用本地服务
node cli.cjs --api http://localhost:5000 ./locales

# 使用 API Key（提高限额）
node cli.cjs --key your-api-key ./locales

# 全部 10 种语言
node cli.cjs --all ./locales

# 调整延迟
node cli.cjs --delay 200 ./locales

# 详细输出
node cli.cjs --verbose ./locales
```

## 🌐 支持的语言

### 默认 6 种
- en-US (英语)
- ja-JP (日语)
- ko-KR (韩语)
- zh-TW (台湾繁体)
- zh-HK (香港繁体)

### 可选 4 种 (使用 --all)
- fr-FR (法语)
- ru-RU (俄语)
- ar-SA (阿拉伯语)
- es-ES (西班牙语)

## ⚠️ 注意事项

1. **公共 API 限额**: 免费公共 API 有请求限额，大量翻译建议申请 Key 或自建
2. **翻译需校对**: AI 翻译结果建议人工检查专业术语

## 🐛 常见问题

### Q: 提示 "无法连接到 API"
A: 检查以下几点:
1. 网络连接是否正常: `ping libretranslate.de`
2. 测试公共 API: `curl https://libretranslate.de/languages`
3. 如果使用本地服务: `docker ps | grep libretranslate`

### Q: 翻译很慢
A: 正常现象，每个字符串有 100ms 延迟，可通过 `--delay` 调整。使用公共 API 可能更慢。

### Q: 可以中断吗
A: 可以，已翻译的文件会保留，重新运行会继续

## 📚 更多信息

完整文档: [README.md](./README.md)
