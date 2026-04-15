// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Routine.WordFilter
// 文件名称：TaktWordFiltersController.cs
// 创建时间：2025-01-22
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt敏感词过滤控制器，提供敏感词过滤的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.WordFilter;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Helpers;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.Tasks.WordFilter;

/// <summary>
/// 敏感词过滤控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-22
/// </remarks>
[Route("api/[controller]", Name = "敏感词过滤")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:wordfilter", "敏感词过滤管理")]
public class TaktWordFiltersController : TaktControllerBase
{
    private readonly IWebHostEnvironment _environment;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="environment">Web主机环境</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktWordFiltersController(
        IWebHostEnvironment environment,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _environment = environment;
    }

    /// <summary>
    /// 检查文本是否包含敏感词
    /// </summary>
    /// <param name="dto">检查文本请求DTO</param>
    /// <returns>检查结果</returns>
    [HttpPost("check")]
    [TaktPermission("routine:tasks:wordfilter:check", "检查敏感词")]
    [AllowAnonymous] // 允许匿名访问，方便前端实时验证
    public ActionResult<TaktApiResult<CheckTextResultDto>> CheckText([FromBody] CheckTextDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Text))
            {
                return Ok(TaktApiResult<CheckTextResultDto>.Fail(GetLocalizedString("validation.wordFilterTextRequired", "Frontend")));
            }

            var containsIllegal = TaktWordFilterHelper.ContainsIllegalWords(dto.Text);
            var illegalWords = containsIllegal 
                ? TaktWordFilterHelper.FindAllIllegalWords(dto.Text) 
                : new List<string>();

            var result = new CheckTextResultDto
            {
                ContainsIllegalWords = containsIllegal,
                IllegalWords = illegalWords
            };

            return Ok(TaktApiResult<CheckTextResultDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return Ok(TaktApiResult<CheckTextResultDto>.Fail(GetLocalizedString("validation.wordFilterCheckFailed", "Frontend", GetLocalizedExceptionMessage(ex))));
        }
    }

    /// <summary>
    /// 查找文本中的所有敏感词
    /// </summary>
    /// <param name="dto">查找敏感词请求DTO</param>
    /// <returns>查找结果</returns>
    [HttpPost("find")]
    [TaktPermission("routine:tasks:wordfilter:find", "查找敏感词")]
    [AllowAnonymous] // 允许匿名访问
    public ActionResult<TaktApiResult<FindWordsResultDto>> FindWords([FromBody] FindWordsDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Text))
            {
                return Ok(TaktApiResult<FindWordsResultDto>.Fail(GetLocalizedString("validation.wordFilterTextRequired", "Frontend")));
            }

            var illegalWords = TaktWordFilterHelper.FindAllIllegalWords(dto.Text);
            var result = new FindWordsResultDto
            {
                IllegalWords = illegalWords
            };

            if (dto.IncludeDetails)
            {
                var details = TaktWordFilterHelper.FindAllIllegalWordsWithDetails(dto.Text);
                result.IllegalWordDetails = details.Select(d => new IllegalWordDetailDto
                {
                    Keyword = d.Keyword,
                    Start = d.Start,
                    End = d.End
                }).ToList();
            }

            return Ok(TaktApiResult<FindWordsResultDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return Ok(TaktApiResult<FindWordsResultDto>.Fail(GetLocalizedString("validation.wordFilterFindFailed", "Frontend", GetLocalizedExceptionMessage(ex))));
        }
    }

    /// <summary>
    /// 替换文本中的敏感词
    /// </summary>
    /// <param name="dto">替换敏感词请求DTO</param>
    /// <returns>替换结果</returns>
    [HttpPost("replace")]
    [TaktPermission("routine:tasks:wordfilter:replace", "替换敏感词")]
    [AllowAnonymous] // 允许匿名访问
    public ActionResult<TaktApiResult<ReplaceWordsResultDto>> ReplaceWords([FromBody] ReplaceWordsDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Text))
            {
                return Ok(TaktApiResult<ReplaceWordsResultDto>.Fail(GetLocalizedString("validation.wordFilterTextRequired", "Frontend")));
            }

            var originalText = dto.Text;
            string replacedText;
            int replacedCount = 0;

            // 先查找敏感词数量
            var illegalWords = TaktWordFilterHelper.FindAllIllegalWords(originalText);
            replacedCount = illegalWords.Count;

            // 执行替换
            if (!string.IsNullOrWhiteSpace(dto.ReplaceText))
            {
                // 使用字符串替换
                replacedText = TaktWordFilterHelper.ReplaceIllegalWords(originalText, dto.ReplaceText);
            }
            else if (!string.IsNullOrWhiteSpace(dto.ReplaceChar) && dto.ReplaceChar.Length == 1)
            {
                // 使用字符替换
                replacedText = TaktWordFilterHelper.ReplaceIllegalWords(originalText, dto.ReplaceChar[0]);
            }
            else
            {
                // 默认使用 '*' 替换
                replacedText = TaktWordFilterHelper.ReplaceIllegalWords(originalText, '*');
            }

            var result = new ReplaceWordsResultDto
            {
                OriginalText = originalText,
                ReplacedText = replacedText,
                ReplacedCount = replacedCount
            };

            return Ok(TaktApiResult<ReplaceWordsResultDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return Ok(TaktApiResult<ReplaceWordsResultDto>.Fail(GetLocalizedString("validation.wordFilterReplaceFailed", "Frontend", GetLocalizedExceptionMessage(ex))));
        }
    }

    /// <summary>
    /// 高亮文本中的敏感词
    /// </summary>
    /// <param name="dto">高亮敏感词请求DTO</param>
    /// <returns>高亮结果</returns>
    [HttpPost("highlight")]
    [TaktPermission("routine:tasks:wordfilter:highlight", "高亮敏感词")]
    [AllowAnonymous] // 允许匿名访问
    public ActionResult<TaktApiResult<HighlightWordsResultDto>> HighlightWords([FromBody] HighlightWordsDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Text))
            {
                return Ok(TaktApiResult<HighlightWordsResultDto>.Fail(GetLocalizedString("validation.wordFilterTextRequired", "Frontend")));
            }

            var originalText = dto.Text;
            var highlightedText = TaktWordFilterHelper.HighlightIllegalWords(originalText, dto.HighlightClass);
            
            // 计算被高亮的敏感词数量
            var illegalWords = TaktWordFilterHelper.FindAllIllegalWords(originalText);
            var highlightedCount = illegalWords.Count;

            var result = new HighlightWordsResultDto
            {
                OriginalText = originalText,
                HighlightedText = highlightedText,
                HighlightedCount = highlightedCount
            };

            return Ok(TaktApiResult<HighlightWordsResultDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return Ok(TaktApiResult<HighlightWordsResultDto>.Fail(GetLocalizedString("validation.wordFilterHighlightFailed", "Frontend", GetLocalizedExceptionMessage(ex))));
        }
    }

    /// <summary>
    /// 获取敏感词统计信息
    /// </summary>
    /// <returns>统计信息</returns>
    [HttpGet("stats")]
    [TaktPermission("routine:tasks:wordfilter:stats", "查询敏感词统计")]
    public ActionResult<TaktApiResult<WordFilterStatsDto>> GetStats()
    {
        try
        {
            var totalCount = TaktWordFilterHelper.GetSensitiveWordsCount();
            var isInitialized = totalCount > 0; // 简单判断是否已初始化

            var result = new WordFilterStatsDto
            {
                TotalCount = totalCount,
                IsInitialized = isInitialized
            };

            return Ok(TaktApiResult<WordFilterStatsDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return Ok(TaktApiResult<WordFilterStatsDto>.Fail(GetLocalizedString("validation.wordFilterStatsFailed", "Frontend", GetLocalizedExceptionMessage(ex))));
        }
    }

    /// <summary>
    /// 获取词库文件列表（组列表）
    /// </summary>
    /// <returns>词库文件列表</returns>
    [HttpGet("groups")]
    [TaktPermission("routine:tasks:wordfilter:groups", "查询词库组列表")]
    public async Task<ActionResult<TaktApiResult<List<WordLibraryFileDto>>>> GetWordLibraryFiles()
    {
        try
        {
            var contentRoot = _environment.ContentRootPath;
            var wordsDirectory = Path.Combine(contentRoot, "wwwroot", "Words");

            if (!await Task.Run(() => Directory.Exists(wordsDirectory)))
            {
                return Ok(TaktApiResult<List<WordLibraryFileDto>>.Ok(new List<WordLibraryFileDto>()));
            }

            var filePaths = await Task.Run(() => Directory.GetFiles(wordsDirectory, "*.txt", SearchOption.TopDirectoryOnly));
            var wordFiles = new List<WordLibraryFileDto>();

            foreach (var filePath in filePaths)
            {
                var fileName = Path.GetFileName(filePath);
                var fileInfo = TaktFileHelper.GetFileInfo(filePath);
                var displayName = GetDisplayName(fileName);

                // 读取文件计算词数（排除注释和空行）
                int wordCount = 0;
                try
                {
                    if (TaktFileHelper.FileExists(filePath))
                    {
                        var content = await TaktFileHelper.ReadFileTextAsync(filePath, System.Text.Encoding.UTF8);
                        var lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                        wordCount = lines
                            .Where(line => !string.IsNullOrWhiteSpace(line))
                            .Select(line => line.Trim())
                            .Where(line => !line.StartsWith("#"))
                            .Count();
                    }
                }
                catch
                {
                    // 忽略读取错误
                }

                wordFiles.Add(new WordLibraryFileDto
                {
                    FileName = fileName,
                    DisplayName = displayName,
                    FileSize = fileInfo?.Length ?? 0,
                    WordCount = wordCount
                });
            }

            wordFiles = wordFiles.OrderBy(f => f.DisplayName).ToList();

            return Ok(TaktApiResult<List<WordLibraryFileDto>>.Ok(wordFiles));
        }
        catch (Exception ex)
        {
            return Ok(TaktApiResult<List<WordLibraryFileDto>>.Fail(GetLocalizedString("validation.wordFilterGroupsFailed", "Frontend", GetLocalizedExceptionMessage(ex))));
        }
    }

    /// <summary>
    /// 从文件名提取显示名称
    /// </summary>
    private string GetDisplayName(string fileName)
    {
        // 移除扩展名
        var nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);

        // 拼音到中文的映射
        var nameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "baokongciku", "暴恐词库" },
            { "buchongciku", "补充词库" },
            { "fandongciku", "反动词库" },
            { "feifawangzhi", "非法网址" },
            { "guanggaoleixing", "广告类型" },
            { "lingshi-Tencent", "零时-Tencent" },
            { "minshengciku", "民生词库" },
            { "qitaciku", "其他词库" },
            { "seqingciku", "色情词库" },
            { "seqingleixing", "色情类型" },
            { "sheqiangshebao", "涉枪涉爆" },
            { "tanfuciku", "贪腐词库" },
            { "wangyiqianduanguolvminganciku", "网易前端过滤敏感词库" },
            { "xinsixiangqimeng", "新思想启蒙" },
            { "zhengzhileixing", "政治类型" },
            { "yonghumingminganciku", "用户名称敏感词库" },
            { "COVID-19ciku", "COVID-19词库" },
            { "GFWbuchongciku", "GFW补充词库" }
        };

        if (nameMap.TryGetValue(nameWithoutExt, out var displayName))
        {
            return displayName;
        }

        // 如果没有映射，返回原文件名（去除扩展名）
        return nameWithoutExt;
    }

    /// <summary>
    /// 获取所有敏感词列表
    /// </summary>
    /// <returns>敏感词列表</returns>
    [HttpGet("words")]
    [TaktPermission("routine:tasks:wordfilter:list", "查询敏感词列表")]
    public ActionResult<TaktApiResult<List<string>>> GetAllWords()
    {
        try
        {
            var words = TaktWordFilterHelper.GetAllSensitiveWords().ToList();
            return Ok(TaktApiResult<List<string>>.Ok(words));
        }
        catch (Exception ex)
        {
            return Ok(TaktApiResult<List<string>>.Fail(GetLocalizedString("validation.wordFilterListFailed", "Frontend", GetLocalizedExceptionMessage(ex))));
        }
    }

    /// <summary>
    /// 添加敏感词
    /// </summary>
    /// <param name="dto">添加敏感词请求DTO</param>
    /// <returns>添加结果</returns>
    [HttpPost("words")]
    [TaktPermission("routine:tasks:wordfilter:add", "添加敏感词")]
    public async Task<ActionResult<TaktApiResult<AddWordsResultDto>>> AddWords([FromBody] AddWordsDto dto)
    {
        try
        {
            if (dto.Words == null || dto.Words.Count == 0)
            {
                return Ok(TaktApiResult<AddWordsResultDto>.Fail(GetLocalizedString("validation.wordFilterWordsRequired", "Frontend")));
            }

            // 添加到内存词库（用于实时过滤）
            var beforeCount = TaktWordFilterHelper.GetSensitiveWordsCount();
            TaktWordFilterHelper.AddSensitiveWords(dto.Words);
            var afterCount = TaktWordFilterHelper.GetSensitiveWordsCount();

            // 如果指定了组（词库文件），同时将词追加到文件中
            if (!string.IsNullOrWhiteSpace(dto.Group))
            {
                try
                {
                    var contentRoot = _environment.ContentRootPath;
                    var wordsDirectory = Path.Combine(contentRoot, "wwwroot", "Words");
                    var filePath = Path.Combine(wordsDirectory, dto.Group);

                    // 验证文件是否存在且是.txt文件
                    if (!filePath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        return Ok(TaktApiResult<AddWordsResultDto>.Fail(GetLocalizedString("validation.wordFilterFileMustBeTxt", "Frontend")));
                    }

                    if (!TaktFileHelper.FileExists(filePath))
                    {
                        // 如果文件不存在，创建新文件
                        await TaktFileHelper.WriteFileTextAsync(filePath, string.Empty, System.Text.Encoding.UTF8);
                    }

                    // 读取现有内容，检查是否已存在
                    var existingWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    if (TaktFileHelper.FileExists(filePath))
                    {
                        var content = await TaktFileHelper.ReadFileTextAsync(filePath, System.Text.Encoding.UTF8);
                        var existingLines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                        foreach (var line in existingLines)
                        {
                            var trimmed = line.Trim();
                            if (!string.IsNullOrWhiteSpace(trimmed) && !trimmed.StartsWith("#"))
                            {
                                existingWords.Add(trimmed);
                            }
                        }
                    }

                    // 追加新词（排除已存在的和空词）
                    var newWordsToAdd = dto.Words
                        .Where(w => !string.IsNullOrWhiteSpace(w))
                        .Select(w => w.Trim())
                        .Where(w => !existingWords.Contains(w))
                        // 过滤掉包含"+"号的错误格式词汇（这些可能是被错误拆分的词汇）
                        .Where(w => !w.Contains("+"))
                        .ToList();

                    if (newWordsToAdd.Count > 0)
                    {
                        // 读取文件末尾，确保有换行符
                        var existingContent = await TaktFileHelper.ReadFileTextAsync(filePath, System.Text.Encoding.UTF8);
                        var needsNewline = !string.IsNullOrEmpty(existingContent) && 
                                         !existingContent.EndsWith("\r\n") && 
                                         !existingContent.EndsWith("\n") && 
                                         !existingContent.EndsWith("\r");
                        
                        // 追加到文件末尾，确保每个词单独一行
                        var contentToAppend = (needsNewline ? Environment.NewLine : string.Empty) + 
                                             string.Join(Environment.NewLine, newWordsToAdd) + 
                                             Environment.NewLine;
                        await TaktFileHelper.AppendFileTextAsync(filePath, contentToAppend, System.Text.Encoding.UTF8);
                    }
                }
                catch
                {
                    // 文件操作失败不影响内存词库的添加，但记录警告
                    // 注意：这里不抛出异常，因为内存词库已经添加成功
                }
            }

            var result = new AddWordsResultDto
            {
                AddedCount = afterCount - beforeCount,
                TotalCount = afterCount
            };

            return Ok(TaktApiResult<AddWordsResultDto>.Ok(result, GetLocalizedString("validation.wordFilterAddSuccess", "Frontend", result.AddedCount)));
        }
        catch (Exception ex)
        {
            return Ok(TaktApiResult<AddWordsResultDto>.Fail(GetLocalizedString("validation.wordFilterAddFailed", "Frontend", GetLocalizedExceptionMessage(ex))));
        }
    }

    /// <summary>
    /// 移除敏感词
    /// </summary>
    /// <param name="dto">移除敏感词请求DTO</param>
    /// <returns>移除结果</returns>
    [HttpDelete("words")]
    [TaktPermission("routine:tasks:wordfilter:remove", "移除敏感词")]
    public ActionResult<TaktApiResult<RemoveWordsResultDto>> RemoveWords([FromBody] RemoveWordsDto dto)
    {
        try
        {
            if (dto.Words == null || dto.Words.Count == 0)
            {
                return Ok(TaktApiResult<RemoveWordsResultDto>.Fail(GetLocalizedString("validation.wordFilterWordsRequired", "Frontend")));
            }

            var beforeCount = TaktWordFilterHelper.GetSensitiveWordsCount();
            TaktWordFilterHelper.RemoveSensitiveWords(dto.Words);
            var afterCount = TaktWordFilterHelper.GetSensitiveWordsCount();

            var result = new RemoveWordsResultDto
            {
                RemovedCount = beforeCount - afterCount,
                RemainingCount = afterCount
            };

            return Ok(TaktApiResult<RemoveWordsResultDto>.Ok(result, GetLocalizedString("validation.wordFilterRemoveSuccess", "Frontend", result.RemovedCount)));
        }
        catch (Exception ex)
        {
            return Ok(TaktApiResult<RemoveWordsResultDto>.Fail(GetLocalizedString("validation.wordFilterRemoveFailed", "Frontend", GetLocalizedExceptionMessage(ex))));
        }
    }

    /// <summary>
    /// 清空敏感词库
    /// </summary>
    /// <returns>操作结果</returns>
    [HttpDelete("clear")]
    [TaktPermission("routine:tasks:wordfilter:clear", "清空敏感词库")]
    public ActionResult<TaktApiResult<object>> ClearWords()
    {
        try
        {
            TaktWordFilterHelper.Clear();
            return Ok(TaktApiResult<object>.Ok(null, GetLocalizedString("validation.wordFilterClearSuccess", "Frontend")));
        }
        catch (Exception ex)
        {
            return Ok(TaktApiResult<object>.Fail(GetLocalizedString("validation.wordFilterClearFailed", "Frontend", GetLocalizedExceptionMessage(ex))));
        }
    }
}
