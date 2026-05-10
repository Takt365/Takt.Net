// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers
// 文件名称：TaktFileUploadsController.cs
// 创建时间：2026-04-29
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktFileUpload控制器，提供FileUpload管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos;
using Takt.Application.Dtos.Routine.Tasks.File;
using Takt.Application.Services;
using Takt.Application.Services.Routine.Tasks.File;
using Takt.Application.Services.Routine.Tasks.File.UploadEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Constants;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers;

/// <summary>
/// TaktFileUpload控制器
/// </summary>
[Route("api/[controller]", Name = "FileUpload")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:file:upload", "FileUpload管理")]
public class TaktUploadsController : TaktControllerBase
{
    private readonly ITaktUploadEngineService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUploadsController(
        ITaktUploadEngineService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="fileType">文件类型</param>
    /// <param name="targetFileName">目标文件名（可选）</param>
    /// <returns>文件上传结果</returns>
    [HttpPost("upload")]
   [TaktPermission("routine:tasks:file:upload", "上传文件")]
    public async Task<ActionResult<FileUploadResultDto>> UploadAsync(
        IFormFile file,
        [FromQuery] TaktFileConstants.FileUploadType fileType,
        [FromQuery] string? targetFileName = null)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("文件不能为空");
        }

        try
        {
            using var stream = file.OpenReadStream();
            var result = await _service.UploadAsync(stream, file.FileName, fileType, targetFileName);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 批量上传文件
    /// </summary>
    /// <param name="files">文件列表</param>
    /// <param name="fileType">文件类型</param>
    /// <returns>文件上传结果列表</returns>
    [HttpPost("upload/batch")]
   [TaktPermission("routine:tasks:file:uploadbatch", "批量上传文件")]
    public async Task<ActionResult<List<FileUploadResultDto>>> UploadBatchAsync(
        List<IFormFile> files,
        [FromQuery] TaktFileConstants.FileUploadType fileType)
    {
        if (files == null || files.Count == 0)
        {
            return BadRequest("文件列表不能为空");
        }

        try
        {
            var fileStreams = new List<(Stream stream, string fileName)>();
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    fileStreams.Add((file.OpenReadStream(), file.FileName));
                }
            }

            var results = await _service.UploadBatchAsync(fileStreams, fileType);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePath">文件路径（相对路径）</param>
    /// <returns>操作结果</returns>
    [HttpDelete("delete")]
   [TaktPermission("routine:tasks:file:delete", "删除文件")]
    public async Task<ActionResult> DeleteAsync([FromQuery] string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return BadRequest("文件路径不能为空");
        }

        try
        {
            await _service.DeleteAsync(filePath);
            return Ok(new { message = "文件删除成功" });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
