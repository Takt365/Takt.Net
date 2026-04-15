# 敏感词库说明

## 文件位置
敏感词库文件位于：`wwwroot/Words/` 目录

## 当前词库文件
- `sensitive-words.txt` - 基础敏感词库（示例）

## 如何获取更多敏感词库

### 方法1：从 GitHub 项目下载

#### Sensitive-lexicon（推荐）
项目地址：https://github.com/konsheng/Sensitive-lexicon

**下载步骤：**
1. 访问 https://github.com/konsheng/Sensitive-lexicon
2. 进入 `Vocabulary/` 目录
3. 下载需要的词库文件（.txt格式），例如：
   - 色情词库.txt
   - 暴恐词库.txt
   - 政治类型.txt
   - 反动词库.txt
   - 其他词库.txt
4. 将下载的文件放到 `wwwroot/Words/` 目录
5. 在代码中使用 `TaktWordFilterHelper.LoadFromFile()` 加载

#### textfilter 项目
项目地址：https://github.com/observerss/textfilter
- 包含约1万词的敏感词库
- 使用 DFA 算法实现

#### sensitive-word-filter 项目
项目地址：https://github.com/gaohuifeng/sensitive-word-filter
- 包含1万+敏感词
- 有 `keywords` 文件可直接使用

### 方法2：使用 Git 克隆（推荐）

```bash
# 克隆 Sensitive-lexicon 项目
cd wwwroot/Words
git clone https://github.com/konsheng/Sensitive-lexicon.git

# 或者只下载 Vocabulary 目录
git clone --depth 1 --filter=blob:none --sparse https://github.com/konsheng/Sensitive-lexicon.git
cd Sensitive-lexicon
git sparse-checkout set Vocabulary
```

### 方法3：手动合并多个词库

可以将多个词库文件合并成一个：

```csharp
// 示例：合并多个词库文件
var allWords = new List<string>();
var files = new[] { 
    "色情词库.txt", 
    "暴恐词库.txt", 
    "政治类型.txt" 
};

foreach (var file in files)
{
    var path = Path.Combine("wwwroot", "Words", file);
    if (File.Exists(path))
    {
        var words = File.ReadAllLines(path, Encoding.UTF8)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Where(line => !line.StartsWith("#"))
            .Select(line => line.Trim());
        allWords.AddRange(words);
    }
}

// 去重并保存
var uniqueWords = allWords.Distinct().ToList();
File.WriteAllLines("wwwroot/Words/sensitive-words-merged.txt", uniqueWords, Encoding.UTF8);

// 加载合并后的词库
TaktWordFilterHelper.LoadFromFile("wwwroot/Words/sensitive-words-merged.txt");
```

## 使用方式

### 加载单个词库文件
```csharp
var filePath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "Words", "sensitive-words.txt");
TaktWordFilterHelper.LoadFromFile(filePath);
```

### 加载多个词库文件并合并
```csharp
var words = new List<string>();
var files = Directory.GetFiles(
    Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "Words"), 
    "*.txt"
);

foreach (var file in files)
{
    var fileWords = File.ReadAllLines(file, Encoding.UTF8)
        .Where(line => !string.IsNullOrWhiteSpace(line))
        .Where(line => !line.StartsWith("#"))
        .Select(line => line.Trim())
        .Distinct();
    words.AddRange(fileWords);
}

TaktWordFilterHelper.Initialize(words.Distinct().ToList());
```

## 词库文件格式

- 每行一个敏感词
- 支持中文、英文、数字等
- 以 `#` 开头的行被视为注释，会被自动忽略
- 空行会被自动忽略
- 自动去重

## 注意事项

1. 敏感词库需要根据业务需求定期更新
2. 建议定期从开源项目同步最新的敏感词库
3. 可以根据实际情况调整敏感词库的内容
4. 注意遵守相关法律法规
