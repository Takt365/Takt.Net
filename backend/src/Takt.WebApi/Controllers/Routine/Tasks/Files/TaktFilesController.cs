// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Routine.Files
// 文件名称：TaktFilesController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt文件控制器，提供文件管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.Files;
using Takt.Application.Services.Routine.Tasks.Files;
using Takt.Application.Services.Routine.Tasks.Files.Engine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.WebApi.Helpers;


namespace Takt.WebApi.Controllers.Routine.Tasks.Files;

/// <summary>
/// 文件控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "文件管理")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:file", "文件管理")]
public class TaktFilesController : TaktControllerBase
{
    private readonly ITaktFileService _fileService;
    private readonly ITaktFileUploadService _fileUploadService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fileService">文件服务</param>
    /// <param name="fileUploadService">文件上传服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFilesController(
        ITaktFileService fileService,
        ITaktFileUploadService fileUploadService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _fileService = fileService;
        _fileUploadService = fileUploadService;
    }

    /// <summary>
    /// 获取文件列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:file:list", "查询文件列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFileDto>>> GetFileListAsync([FromQuery] TaktFileQueryDto queryDto)
    {
        var result = await _fileService.GetFileListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取文件
    /// </summary>
    /// <param name="id">文件ID</param>
    /// <returns>文件DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:file:query", "查询文件详情")]
    public async Task<ActionResult<TaktFileDto>> GetFileByIdAsync(long id)
    {
        var file = await _fileService.GetFileByIdAsync(id);
        if (file == null)
            return NotFound();
        return Ok(file);
    }

    /// <summary>
    /// 根据文件编码获取文件
    /// </summary>
    /// <param name="fileCode">文件编码</param>
    /// <returns>文件DTO</returns>
    [HttpGet("code/{fileCode}")]
    [TaktPermission("routine:tasks:file:query", "根据编码查询文件")]
    public async Task<ActionResult<TaktFileDto>> GetByCodeAsync(string fileCode)
    {
        var file = await _fileService.GetByCodeAsync(fileCode);
        if (file == null)
            return NotFound();
        return Ok(file);
    }

    /// <summary>
    /// 创建文件记录
    /// </summary>
    /// <param name="dto">创建文件DTO</param>
    /// <returns>文件DTO</returns>
    [HttpPost]
    [TaktPermission("routine:tasks:file:create", "创建文件记录")]
    public async Task<ActionResult<TaktFileDto>> CreateFileAsync([FromBody] TaktFileCreateDto dto)
    {
        // 调试：记录接收到的 fileDescription
        LogInformation($"[TaktFilesController] 接收到的 FileDescription: '{dto.FileDescription}', 长度: {dto.FileDescription?.Length ?? 0}");
        
        var file = await _fileService.CreateFileAsync(dto);
        
        // 调试：记录保存后的 fileDescription
        LogInformation($"[TaktFilesController] 保存后的 FileDescription: '{file.FileDescription}', 长度: {file.FileDescription?.Length ?? 0}");
        
        // 使用 Ok 而不是 CreatedAtAction，避免路由匹配问题
        return Ok(file);
    }

    /// <summary>
    /// 更新文件记录
    /// </summary>
    /// <param name="id">文件ID</param>
    /// <param name="dto">更新文件DTO</param>
    /// <returns>文件DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:file:update", "更新文件记录")]
    public async Task<ActionResult<TaktFileDto>> UpdateFileAsync(long id, [FromBody] TaktFileUpdateDto dto)
    {
        try
        {
            var file = await _fileService.UpdateFileAsync(id, dto);
            return Ok(file);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除文件记录（软删除）
    /// </summary>
    /// <param name="id">文件ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:file:delete", "删除文件记录")]
    public async Task<IActionResult> DeleteFileByIdAsync(long id)
    {
        await _fileService.DeleteFileByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除文件记录（软删除）
    /// </summary>
    /// <param name="ids">文件ID列表</param>
    /// <returns>操作结果</returns>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:file:delete", "批量删除文件记录")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] List<long> ids)
    {
        await _fileService.DeleteFileBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 更新文件状态
    /// </summary>
    /// <param name="dto">文件状态DTO</param>
    /// <returns>文件DTO</returns>
    [HttpPut("status")]
    [TaktPermission("routine:tasks:file:status", "更新文件状态")]
    public async Task<ActionResult<TaktFileDto>> UpdateFileStatusAsync([FromBody] TaktFileStatusDto dto)
    {
        try
        {
            var file = await _fileService.UpdateFileStatusAsync(dto);
            return Ok(file);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 切换文件公开/私有状态
    /// </summary>
    /// <param name="dto">文件切换DTO</param>
    /// <returns>文件DTO</returns>
    [HttpPut("change")]
    [TaktPermission("routine:tasks:file:change", "切换文件公开/私有状态")]
    public async Task<ActionResult<TaktFileDto>> ChangeIsPublicAsync([FromBody] TaktFileChangeDto dto)
    {
        try
        {
            var file = await _fileService.ChangeIsPublicAsync(dto);
            return Ok(file);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file">上传的文件</param>
    /// <param name="fileType">文件类型（0=头像，1=图片，2=文件）</param>
    /// <param name="targetFileName">目标文件名（可选，如果提供则使用该名称保存，否则使用默认的 fileCode.扩展名）</param>
    /// <returns>文件上传结果</returns>
    [HttpPost("upload")]
    [TaktPermission("routine:tasks:file:upload", "上传文件")]
    public async Task<ActionResult<FileUploadResult>> UploadAsync(
        IFormFile file, 
        [FromForm] int fileType = 2,
        [FromForm] string? targetFileName = null)
    {
        if (file == null || file.Length == 0)
            return BadRequest(GetLocalizedString("validation.fileRequired", "Frontend"));

        try
        {
            var uploadType = (FileUploadType)fileType;
            var uploadResult = await _fileUploadService.UploadAsync(file.OpenReadStream(), file.FileName, uploadType, targetFileName);
            return Ok(uploadResult);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出文件列表
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:file:export", "导出文件列表")]
    public async Task<IActionResult> ExportFileAsync([FromBody] TaktFileQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _fileService.ExportFileAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 增加下载次数
    /// </summary>
    /// <param name="dto">增加下载次数DTO</param>
    /// <returns>操作结果</returns>
    [HttpPost("increment-download-count")]
    [TaktPermission("routine:tasks:file:download", "增加下载次数")]
    public async Task<IActionResult> IncrementDownloadCountAsync([FromBody] TaktFileIncrementDownloadCountDto dto)
    {
        await _fileService.IncrementDownloadCountAsync(dto);
        return Ok();
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="id">文件ID</param>
    /// <returns>文件内容</returns>
    [HttpGet("{id}/download")]
    [TaktPermission("routine:tasks:file:download", "下载文件")]
    public async Task<IActionResult> DownloadAsync(long id)
    {
        try
        {
            // 先获取文件信息以确定 contentType
            var file = await _fileService.GetFileByIdAsync(id);
            if (file == null)
                return NotFound();

            var (fileName, content) = await _fileService.DownloadAsync(id);
            string contentType = file.FileType ?? "application/octet-stream";
            
            // 增加下载次数（异步执行，不阻塞下载）
            _ = Task.Run(async () =>
            {
                try
                {
                    await _fileService.IncrementDownloadCountAsync(new TaktFileIncrementDownloadCountDto { FileId = id });
                }
                catch (Exception ex)
                {
                    // 记录错误但不影响下载
                    LogError(ex, $"[TaktFilesController] 增加下载次数失败: FileId={id}");
                }
            });

            // 返回文件内容
            return File(content, contentType, fileName);
        }
        catch (Exception ex)
        {
            LogError(ex, $"[TaktFilesController] 下载文件失败: FileId={id}");
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
