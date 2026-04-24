// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Files
// 文件名称：TaktFileService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt文件应用服务，提供文件管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Takt.Application.Dtos.Routine.Tasks.Files;
using Takt.Domain.Entities.Routine.Tasks.Files;
using Takt.Domain.Interfaces;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;

namespace Takt.Application.Services.Routine.Tasks.Files;

/// <summary>
/// Takt文件应用服务
/// </summary>
public class TaktFileService : TaktServiceBase, ITaktFileService
{
    private readonly ITaktRepository<TaktFile> _fileRepository;
    private readonly IWebHostEnvironment? _webHostEnvironment;
    private readonly IHttpContextAccessor? _httpContextAccessor;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fileRepository">文件仓储</param>
    /// <param name="webHostEnvironment">Web主机环境（可选，用于删除物理文件）</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器（可选，用于生成访问地址）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFileService(
        ITaktRepository<TaktFile> fileRepository,
        IWebHostEnvironment? webHostEnvironment = null,
        IHttpContextAccessor? httpContextAccessor = null,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _fileRepository = fileRepository;
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 获取文件列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktFileDto>> GetFileListAsync(TaktFileQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _fileRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktFileDto>.Create(
            data.Adapt<List<TaktFileDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取文件
    /// </summary>
    /// <param name="id">文件ID</param>
    /// <returns>文件DTO</returns>
    public async Task<TaktFileDto?> GetFileByIdAsync(long id)
    {
        var file = await _fileRepository.GetByIdAsync(id);
        if (file == null) return null;

        return file.Adapt<TaktFileDto>();
    }

    /// <summary>
    /// 根据文件编码获取文件
    /// </summary>
    /// <param name="fileCode">文件编码</param>
    /// <returns>文件DTO</returns>
    public async Task<TaktFileDto?> GetByCodeAsync(string fileCode)
    {
        var file = await _fileRepository.GetAsync(f => f.FileCode == fileCode && f.IsDeleted == 0);
        if (file == null) return null;

        return file.Adapt<TaktFileDto>();
    }

    /// <summary>
    /// 创建文件记录
    /// </summary>
    /// <param name="dto">创建文件DTO</param>
    /// <returns>文件DTO</returns>
    public async Task<TaktFileDto> CreateFileAsync(TaktFileCreateDto dto)
    {
        // 查重：文件名称+文件类型 组合唯一
        var fileName = dto.FileName ?? string.Empty;
        var fileType = dto.FileType ?? string.Empty;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _fileRepository,
            f => (f.FileName ?? "") == fileName && (f.FileType ?? "") == fileType,
            null,
            "文件名称+文件类型组合已存在");

        // 使用Mapster映射DTO到实体
        var file = dto.Adapt<TaktFile>();
        file.FileStatus = 0; // 0=正常

        // 设置ConfigId
        var configId = GetCurrentConfigId();
        if (!string.IsNullOrEmpty(configId))
        {
            file.ConfigId = configId;
        }

        // 如果没有提供访问地址，则根据存储类型自动生成
        if (string.IsNullOrWhiteSpace(file.AccessUrl))
        {
            file.AccessUrl = GenerateAccessUrl(file);
        }

        // 如果没有提供IP地址，则从HTTP上下文自动获取
        if (string.IsNullOrWhiteSpace(file.IpAddress))
        {
            file.IpAddress = GetClientIpAddress();
        }

        // 如果提供了IP地址但没有位置信息，则根据IP地址自动获取位置
        if (!string.IsNullOrWhiteSpace(file.IpAddress) && string.IsNullOrWhiteSpace(file.Location))
        {
            file.Location = await GetLocationByIpAsync(file.IpAddress);
        }

        // 根据前端的 storageNaming 命名规则，确保磁盘文件名与数据库中的 fileName 一致
        // 前端命名规则（storageNaming）：
        //   0 = 原文件+哈希值：格式为 "原文件名_哈希值.扩展名"（例如：GPO_fs04_fef6ce15838b47869ef99bc8f71c9eb0.html）
        //   1 = 自动生成：格式为 "fileCode.扩展名"（例如：678f1b096a1947dda74c080d672b3d34.pdf）
        //   2 = 自定义命名：使用用户自定义的文件名
        // 
        // 上传时，后端使用 fileCode 作为临时文件名保存到磁盘（例如：upload/files/2026/01/26/fileCode.扩展名）
        // 前端根据命名规则生成最终的 fileName，提交给后端
        // 后端需要将磁盘文件重命名为前端指定的 fileName，确保两者一致
        if (!string.IsNullOrWhiteSpace(file.FileName) && !string.IsNullOrWhiteSpace(file.FilePath))
        {
            try
            {
                // 从 filePath 中提取当前文件名（磁盘上的文件名，通常是 fileCode.扩展名）
                var currentFileName = Path.GetFileName(file.FilePath);

                // 如果 fileName（前端根据命名规则生成的）与当前文件名不一致，需要重命名
                // 对于 storageNaming=0（原文件+哈希值）和 storageNaming=2（自定义命名），通常需要重命名
                // 对于 storageNaming=1（自动生成），如果 fileName 就是 fileCode.扩展名，则不需要重命名
                if (!string.Equals(file.FileName, currentFileName, StringComparison.OrdinalIgnoreCase))
                {
                    // 构建完整路径
                    if (_webHostEnvironment != null)
                    {
                        var currentFullPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", file.FilePath.Replace('/', Path.DirectorySeparatorChar));

                        // 构建新路径（保持目录结构，只改变文件名）
                        var directory = Path.GetDirectoryName(currentFullPath);
                        if (directory != null && File.Exists(currentFullPath))
                        {
                            // 确保 fileName 包含扩展名（前端应该已经包含，这里做验证）
                            var newFileName = file.FileName;
                            if (string.IsNullOrEmpty(Path.GetExtension(newFileName)) && !string.IsNullOrEmpty(file.FileExtension))
                            {
                                // 如果 fileName 没有扩展名，但 FileExtension 存在，则添加扩展名
                                newFileName = $"{newFileName}.{file.FileExtension}";
                                file.FileName = newFileName;
                            }

                            var newFullPath = Path.Combine(directory, newFileName);

                            // 检查目标文件是否已存在（避免覆盖）
                            if (File.Exists(newFullPath) && !string.Equals(currentFullPath, newFullPath, StringComparison.OrdinalIgnoreCase))
                            {
                                TaktLogger.Warning("[TaktFileService] 目标文件已存在，跳过重命名: {NewFileName}", newFileName);
                            }
                            else
                            {
                                // 重命名文件
                                await TaktFileHelper.MoveFileAsync(currentFullPath, newFullPath, overwrite: false);

                                // 更新 filePath 以反映新的文件名
                                var relativePath = Path.GetRelativePath(
                                    Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot"),
                                    newFullPath
                                ).Replace(Path.DirectorySeparatorChar, '/');

                                file.FilePath = relativePath;

                                TaktLogger.Information("[TaktFileService] 文件重命名成功（符合前端命名规则）: {OldFileName} -> {NewFileName}", currentFileName, newFileName);
                            }
                        }
                        else if (!File.Exists(currentFullPath))
                        {
                            TaktLogger.Warning("[TaktFileService] 源文件不存在，无法重命名: {FilePath}", file.FilePath);
                        }
                    }
                    else
                    {
                        TaktLogger.Warning("[TaktFileService] IWebHostEnvironment 未注入，无法重命名文件: {FilePath} -> {FileName}", file.FilePath, file.FileName);
                    }
                }
                else
                {
                    // 文件名已一致，无需重命名（通常发生在 storageNaming=1 且 fileName 就是 fileCode.扩展名的情况）
                    TaktLogger.Debug("[TaktFileService] 文件名已一致，无需重命名: {FileName}", file.FileName);
                }
            }
            catch (Exception ex)
            {
                TaktLogger.Error(ex, "[TaktFileService] 重命名文件失败: {FilePath} -> {FileName}", file.FilePath, file.FileName);
                // 不抛出异常，允许继续创建文件记录，但记录错误
                // 这样即使重命名失败，文件记录仍然可以创建，只是磁盘文件名可能与数据库不一致
            }
        }

        file = await _fileRepository.CreateAsync(file);

        return await GetFileByIdAsync(file.Id) ?? file.Adapt<TaktFileDto>();
    }

    /// <summary>
    /// 更新文件记录
    /// </summary>
    /// <param name="id">文件ID</param>
    /// <param name="dto">更新文件DTO</param>
    /// <returns>文件DTO</returns>
    public async Task<TaktFileDto> UpdateFileAsync(long id, TaktFileUpdateDto dto)
    {
        var file = await _fileRepository.GetByIdAsync(id);
        if (file == null)
            throw new TaktBusinessException("validation.storedFileNotFound");

        // 查重（排除当前记录）：文件名称+文件类型 组合唯一
        var fileName = dto.FileName ?? string.Empty;
        var fileType = dto.FileType ?? string.Empty;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _fileRepository,
            f => (f.FileName ?? "") == fileName && (f.FileType ?? "") == fileType,
            id,
            "文件名称+文件类型组合已存在");

        // 使用Mapster更新实体
        dto.Adapt(file, typeof(TaktFileUpdateDto), typeof(TaktFile));
        file.UpdatedAt = DateTime.Now;

        // 如果没有提供访问地址，则根据存储类型自动生成
        if (string.IsNullOrWhiteSpace(file.AccessUrl))
        {
            file.AccessUrl = GenerateAccessUrl(file);
        }

        // 如果没有提供IP地址，则从HTTP上下文自动获取
        if (string.IsNullOrWhiteSpace(file.IpAddress))
        {
            file.IpAddress = GetClientIpAddress();
        }

        // 如果提供了IP地址但没有位置信息，则根据IP地址自动获取位置
        if (!string.IsNullOrWhiteSpace(file.IpAddress) && string.IsNullOrWhiteSpace(file.Location))
        {
            file.Location = await GetLocationByIpAsync(file.IpAddress);
        }

        await _fileRepository.UpdateAsync(file);

        return await GetFileByIdAsync(id) ?? file.Adapt<TaktFileDto>();
    }

    /// <summary>
    /// 删除文件记录（软删除）并删除存储位置的物理文件
    /// </summary>
    /// <param name="id">文件ID</param>
    /// <returns>任务</returns>
    public async Task DeleteFileByIdAsync(long id)
    {
        var file = await _fileRepository.GetByIdAsync(id);
        if (file == null)
            throw new TaktBusinessException("validation.storedFileNotFound");

        // 1. 先将 FileStatus 置为禁用（1），再软删除（IsDeleted=1）
        file.FileStatus = 1;
        file.UpdatedAt = DateTime.Now;
        await _fileRepository.UpdateAsync(file);

        // 2. 删除存储位置的物理文件（即使失败也不影响数据库软删除）
        await DeletePhysicalFileAsync(file);

        // 3. 软删除数据库记录（IsDeleted = 1）
        await _fileRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除文件记录（软删除）并删除存储位置的物理文件
    /// </summary>
    /// <param name="ids">文件ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteFileBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有文件记录
        var files = await _fileRepository.FindAsync(f => idList.Contains(f.Id));

        // 1. 先将所有记录的 FileStatus 置为禁用（1），再软删除（IsDeleted=1）
        foreach (var file in files)
        {
            file.FileStatus = 1;
            file.UpdatedAt = DateTime.Now;
            await _fileRepository.UpdateAsync(file);
        }

        // 2. 删除所有存储位置的物理文件（即使失败也不影响数据库软删除）
        foreach (var file in files)
        {
            await DeletePhysicalFileAsync(file);
        }

        // 3. 软删除数据库记录（IsDeleted = 1）
        await _fileRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新文件状态
    /// </summary>
    /// <param name="dto">文件状态DTO</param>
    /// <returns>文件DTO</returns>
    public async Task<TaktFileDto> UpdateFileStatusAsync(TaktFileStatusDto dto)
    {
        var file = await _fileRepository.GetByIdAsync(dto.FileId);
        if (file == null)
            throw new TaktBusinessException("validation.storedFileNotFound");

        file.FileStatus = dto.FileStatus;
        file.UpdatedAt = DateTime.Now;

        await _fileRepository.UpdateAsync(file);

        return await GetFileByIdAsync(dto.FileId) ?? file.Adapt<TaktFileDto>();
    }

    /// <summary>
    /// 切换文件公开/私有状态
    /// </summary>
    /// <param name="dto">文件切换DTO</param>
    /// <returns>文件DTO</returns>
    public async Task<TaktFileDto> ChangeIsPublicAsync(TaktFilePublicChangeDto dto)
    {
        var file = await _fileRepository.GetByIdAsync(dto.FileId);
        if (file == null)
            throw new TaktBusinessException("validation.storedFileNotFound");

        file.IsPublic = dto.IsPublic;
        file.UpdatedAt = DateTime.Now;

        await _fileRepository.UpdateAsync(file);

        return await GetFileByIdAsync(dto.FileId) ?? file.Adapt<TaktFileDto>();
    }

    /// <summary>
    /// 增加下载次数
    /// </summary>
    /// <param name="dto">增加下载次数DTO</param>
    /// <returns>任务</returns>
    public async Task IncrementDownloadCountAsync(TaktFileIncrementDownloadCountDto dto)
    {
        var file = await _fileRepository.GetByIdAsync(dto.FileId);
        if (file == null)
            throw new TaktBusinessException("validation.storedFileNotFound");

        file.DownloadCount++;
        file.LastDownloadTime = DateTime.Now;
        file.UpdatedAt = DateTime.Now;

        await _fileRepository.UpdateAsync(file);
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="id">文件ID</param>
    /// <returns>文件名和文件内容</returns>
    public async Task<(string fileName, byte[] content)> DownloadAsync(long id)
    {
        var file = await _fileRepository.GetByIdAsync(id);
        if (file == null)
            throw new TaktBusinessException("validation.storedFileNotFound");

        if (string.IsNullOrWhiteSpace(file.FilePath))
            throw new TaktBusinessException("validation.storedFilePathNotFound");

        Stream? fileStream = null;
        string fileName = file.FileOriginalName ?? file.FileName ?? "download";

        try
        {
            // 根据存储类型读取文件
            switch (file.StorageType)
            {
                case 0: // 本地存储
                    fileStream = await DownloadLocalFileAsync(file.FilePath);
                    break;
                case 1: // OSS对象存储
                    throw new TaktBusinessException("validation.fileDownloadOssNotImplemented");
                case 2: // FTP
                    throw new TaktBusinessException("validation.fileDownloadFtpNotImplemented");
                default:
                    throw new TaktLocalizedException("validation.fileStorageTypeUnknown", "Frontend", file.StorageType);
            }

            if (fileStream == null)
                throw new TaktBusinessException("validation.fileStreamEmpty");

            // 将 Stream 读取为 byte[]
            using (var memoryStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream);
                byte[] content = memoryStream.ToArray();
                return (fileName, content);
            }
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFileService] 下载文件失败: FileId={FileId}, FileCode={FileCode}, FilePath={FilePath}",
                file.Id, file.FileCode, file.FilePath);
            throw;
        }
        finally
        {
            fileStream?.Dispose();
        }
    }

    /// <summary>
    /// 下载本地文件
    /// </summary>
    /// <param name="filePath">文件路径（相对路径或完整路径）</param>
    /// <returns>文件流</returns>
    private Task<Stream> DownloadLocalFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new TaktBusinessException("validation.filePathEmpty");

        string fullPath;

        // 判断是相对路径还是绝对路径
        if (Path.IsPathRooted(filePath))
        {
            // 绝对路径，直接使用
            fullPath = filePath;
        }
        else
        {
            // 相对路径，需要构建完整路径
            if (_webHostEnvironment == null)
                throw new TaktBusinessException("validation.webHostEnvironmentNotInjected");

            // 构建完整路径（相对于 wwwroot）
            fullPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot",
                filePath.Replace('/', Path.DirectorySeparatorChar));
        }

        // 检查文件是否存在
        if (!File.Exists(fullPath))
            throw new TaktLocalizedException("validation.storedFileNotFoundWithPath", "Frontend", filePath);

        // 打开文件流
        return Task.FromResult<Stream>(new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read));
    }

    /// <summary>
    /// 导出文件列表
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名和文件内容</returns>
    public async Task<(string fileName, byte[] content)> ExportFileAsync(TaktFileQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的文件（不分页）
        List<TaktFile> files;
        if (predicate != null)
        {
            files = await _fileRepository.FindAsync(predicate);
        }
        else
        {
            files = await _fileRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFile));
        if (files == null || files.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktFileExportDto>(),
                excelSheet,
                excelFile
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = files.Select(f =>
        {
            var dto = f.Adapt<TaktFileExportDto>();
            // 处理需要特殊转换的字段
            dto.FileCategoryString = GetFileCategoryString(f.FileCategory);
            dto.StorageTypeString = GetStorageTypeString(f.StorageType);
            dto.FileStatusString = GetFileStatusString(f.FileStatus);
            dto.IsPublicString = GetIsPublicString(f.IsPublic);
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }

    /// <summary>
    /// 获取文件分类字符串
    /// </summary>
    private string GetFileCategoryString(int fileCategory)
    {
        return fileCategory switch
        {
            0 => "文档",
            1 => "图片",
            2 => "视频",
            3 => "音频",
            4 => "压缩包",
            5 => "其他",
            _ => "未知"
        };
    }

    /// <summary>
    /// 获取存储方式字符串
    /// </summary>
    private string GetStorageTypeString(int storageType)
    {
        return storageType switch
        {
            0 => "本地存储",
            1 => "OSS对象存储",
            2 => "FTP",
            3 => "其他",
            _ => "未知"
        };
    }

    /// <summary>
    /// 获取文件状态字符串
    /// </summary>
    private string GetFileStatusString(int fileStatus)
    {
        return fileStatus switch
        {
            0 => "正常",
            1 => "禁用",
            _ => "未知"
        };
    }

    /// <summary>
    /// 获取是否公开字符串
    /// </summary>
    private string GetIsPublicString(int isPublic)
    {
        return isPublic switch
        {
            0 => "公开",
            1 => "私有",
            _ => "未知"
        };
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktFile, bool>> QueryExpression(TaktFileQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktFile>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.FileCode.Contains(queryDto.KeyWords) ||
                              x.FileName.Contains(queryDto.KeyWords) ||
                              x.FileOriginalName.Contains(queryDto.KeyWords));
        }

        // 文件编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.FileCode), x => x.FileCode.Contains(queryDto!.FileCode!));

        // 文件名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.FileName), x => x.FileName.Contains(queryDto!.FileName!));

        // 文件扩展名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.FileExtension), x => x.FileExtension == queryDto!.FileExtension!);

        // 文件分类
        exp = exp.AndIF(queryDto?.FileCategory.HasValue == true, x => x.FileCategory == queryDto!.FileCategory!.Value);

        // 存储类型
        exp = exp.AndIF(queryDto?.StorageType.HasValue == true, x => x.StorageType == queryDto!.StorageType!.Value);

        // 文件状态
        exp = exp.AndIF(queryDto?.FileStatus.HasValue == true, x => x.FileStatus == queryDto!.FileStatus!.Value);

        // 是否公开
        exp = exp.AndIF(queryDto?.IsPublic.HasValue == true, x => x.IsPublic == queryDto!.IsPublic!.Value);

        // 创建时间范围
        exp = exp.AndIF(queryDto?.CreatedAtStart.HasValue == true, x => x.CreatedAt >= queryDto!.CreatedAtStart!.Value);
        exp = exp.AndIF(queryDto?.CreatedAtEnd.HasValue == true, x => x.CreatedAt <= queryDto!.CreatedAtEnd!.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 删除物理文件
    /// </summary>
    /// <param name="file">文件实体</param>
    /// <returns>任务</returns>
    private async Task DeletePhysicalFileAsync(TaktFile file)
    {
        if (string.IsNullOrWhiteSpace(file.FilePath))
        {
            TaktLogger.Warning("[TaktFileService] 文件路径为空，跳过物理文件删除: FileId={FileId}, FileCode={FileCode}",
                file.Id, file.FileCode);
            return;
        }

        try
        {
            // 根据存储类型处理
            switch (file.StorageType)
            {
                case 0: // 本地存储
                    await DeleteLocalFileAsync(file.FilePath);
                    break;
                case 1: // OSS对象存储
                    TaktLogger.Warning("[TaktFileService] OSS对象存储删除功能未实现: FileId={FileId}, FileCode={FileCode}, FilePath={FilePath}",
                        file.Id, file.FileCode, file.FilePath);
                    // TODO: 实现OSS删除逻辑
                    break;
                case 2: // FTP
                    TaktLogger.Warning("[TaktFileService] FTP存储删除功能未实现: FileId={FileId}, FileCode={FileCode}, FilePath={FilePath}",
                        file.Id, file.FileCode, file.FilePath);
                    // TODO: 实现FTP删除逻辑
                    break;
                default:
                    TaktLogger.Warning("[TaktFileService] 未知的存储类型: StorageType={StorageType}, FileId={FileId}, FileCode={FileCode}",
                        file.StorageType, file.Id, file.FileCode);
                    break;
            }
        }
        catch (Exception ex)
        {
            // 记录错误但不抛出异常，避免影响数据库删除
            TaktLogger.Error(ex, "[TaktFileService] 删除物理文件失败: FileId={FileId}, FileCode={FileCode}, FilePath={FilePath}",
                file.Id, file.FileCode, file.FilePath);
        }
    }

    /// <summary>
    /// 删除本地文件（使用 TaktFileHelper）
    /// </summary>
    /// <param name="filePath">文件路径（相对路径或完整路径）</param>
    /// <returns>任务</returns>
    private async Task DeleteLocalFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return;

        string fullPath;

        // 判断是相对路径还是绝对路径
        if (Path.IsPathRooted(filePath))
        {
            // 绝对路径，直接使用
            fullPath = filePath;
        }
        else
        {
            // 相对路径，需要构建完整路径
            if (_webHostEnvironment == null)
            {
                TaktLogger.Warning("[TaktFileService] IWebHostEnvironment 未注入，无法删除相对路径文件: {FilePath}", filePath);
                return;
            }

            // 构建完整路径（相对于 wwwroot）
            fullPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot",
                filePath.Replace('/', Path.DirectorySeparatorChar));
        }

        // 直接使用 TaktFileHelper 删除文件（已包含完整的日志记录）
        await TaktFileHelper.DeleteFileAsync(fullPath, throwIfNotExists: false);
    }

    /// <summary>
    /// 生成访问地址
    /// </summary>
    /// <param name="file">文件实体</param>
    /// <returns>访问地址</returns>
    private string? GenerateAccessUrl(TaktFile file)
    {
        if (string.IsNullOrWhiteSpace(file.FilePath))
            return null;

        try
        {
            switch (file.StorageType)
            {
                case 0: // 本地存储
                    return GenerateLocalAccessUrl(file.FilePath);
                case 1: // OSS对象存储
                    return GenerateOssAccessUrl(file);
                case 2: // FTP
                    return GenerateFtpAccessUrl(file);
                default:
                    // 其他存储方式，默认使用本地存储方式生成
                    return GenerateLocalAccessUrl(file.FilePath);
            }
        }
        catch (Exception ex)
        {
            TaktLogger.Warning(ex, "[TaktFileService] 生成访问地址失败: FileId={FileId}, FileCode={FileCode}, StorageType={StorageType}",
                file.Id, file.FileCode, file.StorageType);
            return null;
        }
    }

    /// <summary>
    /// 生成本地存储访问地址
    /// </summary>
    /// <param name="filePath">文件路径（相对路径）</param>
    /// <returns>访问地址</returns>
    private string? GenerateLocalAccessUrl(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return null;

        // 如果已经是完整URL，直接返回
        if (Uri.TryCreate(filePath, UriKind.Absolute, out _))
            return filePath;

        // 使用HTTP上下文生成完整URL
        if (_httpContextAccessor?.HttpContext != null)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var scheme = request.Scheme;
            var host = request.Host.Value;
            var basePath = request.PathBase.Value?.TrimEnd('/') ?? string.Empty;

            // 确保文件路径以 / 开头
            var normalizedPath = filePath.StartsWith('/') ? filePath : $"/{filePath}";

            return $"{scheme}://{host}{basePath}{normalizedPath}";
        }

        // 如果没有HTTP上下文，返回相对路径（前端可以自行处理）
        return filePath.StartsWith('/') ? filePath : $"/{filePath}";
    }

    /// <summary>
    /// 生成OSS对象存储访问地址
    /// </summary>
    /// <param name="file">文件实体</param>
    /// <returns>访问地址</returns>
    private string? GenerateOssAccessUrl(TaktFile file)
    {
        if (string.IsNullOrWhiteSpace(file.StorageConfig))
            return null;

        try
        {
            var config = JsonConvert.DeserializeObject<Dictionary<string, object>>(file.StorageConfig);
            if (config == null || !config.ContainsKey("provider"))
                return null;

            // TODO: 根据OSS提供商和配置生成访问地址
            // 这里需要根据实际的OSS配置（如阿里云、腾讯云等）来生成URL
            // 示例：https://bucket-name.oss-region.aliyuncs.com/file-path

            TaktLogger.Warning("[TaktFileService] OSS访问地址生成功能未完全实现: FileId={FileId}, FileCode={FileCode}",
                file.Id, file.FileCode);

            return null;
        }
        catch (Exception ex)
        {
            TaktLogger.Warning(ex, "[TaktFileService] 解析OSS配置失败: FileId={FileId}, FileCode={FileCode}",
                file.Id, file.FileCode);
            return null;
        }
    }

    /// <summary>
    /// 生成FTP访问地址
    /// </summary>
    /// <param name="file">文件实体</param>
    /// <returns>访问地址</returns>
    private string? GenerateFtpAccessUrl(TaktFile file)
    {
        if (string.IsNullOrWhiteSpace(file.StorageConfig))
            return null;

        try
        {
            var config = JsonConvert.DeserializeObject<Dictionary<string, object>>(file.StorageConfig);
            if (config == null)
                return null;

            // TODO: 根据FTP配置生成访问地址
            // 这里需要根据实际的FTP配置来生成URL
            // 示例：ftp://ftp.example.com/file-path 或 http://example.com/ftp-proxy/file-path

            TaktLogger.Warning("[TaktFileService] FTP访问地址生成功能未完全实现: FileId={FileId}, FileCode={FileCode}",
                file.Id, file.FileCode);

            return null;
        }
        catch (Exception ex)
        {
            TaktLogger.Warning(ex, "[TaktFileService] 解析FTP配置失败: FileId={FileId}, FileCode={FileCode}",
                file.Id, file.FileCode);
            return null;
        }
    }

    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    /// <returns>客户端IP地址</returns>
    private string? GetClientIpAddress()
    {
        if (_httpContextAccessor?.HttpContext == null)
            return null;

        var httpContext = _httpContextAccessor.HttpContext;

        // 优先从 X-Forwarded-For 头获取（适用于反向代理场景）
        var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            // X-Forwarded-For 可能包含多个IP，取第一个
            var ips = forwardedFor.Split(',');
            if (ips.Length > 0)
            {
                return ips[0].Trim();
            }
        }

        // 从 X-Real-IP 头获取
        var realIp = httpContext.Request.Headers["X-Real-IP"].FirstOrDefault();
        if (!string.IsNullOrEmpty(realIp))
        {
            return realIp;
        }

        // 从连接信息获取
        return httpContext.Connection.RemoteIpAddress?.ToString();
    }

    /// <summary>
    /// 根据IP地址获取位置信息
    /// </summary>
    /// <param name="ipAddress">IP地址</param>
    /// <returns>位置信息（格式化地址字符串）</returns>
    private async Task<string?> GetLocationByIpAsync(string ipAddress)
    {
        if (string.IsNullOrWhiteSpace(ipAddress))
            return null;

        try
        {
            // 使用 TaktLocationHelper 查询IP位置信息
            var locationResult = await TaktLocationHelper.SearchAsync(ipAddress);
            if (locationResult != null)
            {
                // 返回格式化后的地址信息
                return locationResult.FormattedAddress;
            }
        }
        catch (Exception ex)
        {
            // IP定位失败不影响主流程，只记录日志
            TaktLogger.Warning(ex, "[TaktFileService] IP定位失败: {IpAddress}", ipAddress);
        }

        return null;
    }
}
