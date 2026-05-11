// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcAttachmentService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：设变附件表应用服务，提供EcAttachment管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange;

namespace Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变附件表应用服务
/// </summary>
public class TaktEcAttachmentService : TaktServiceBase, ITaktEcAttachmentService
{
    private readonly ITaktRepository<TaktEcAttachment> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">EcAttachment仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEcAttachmentService(
        ITaktRepository<TaktEcAttachment> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
    }


    /// <summary>
    /// 获取设变附件表(EcAttachment)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEcAttachmentDto>> GetEcAttachmentListAsync(TaktEcAttachmentQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEcAttachmentDto>.Create(
            data.Adapt<List<TaktEcAttachmentDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取设变附件表(EcAttachment)
    /// </summary>
    /// <param name="id">设变附件表(EcAttachment)ID</param>
    /// <returns>设变附件表(EcAttachment)DTO</returns>
    public async Task<TaktEcAttachmentDto?> GetEcAttachmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEcAttachmentDto>();
    }


    /// <summary>
    /// 获取设变附件表(EcAttachment)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>设变附件表(EcAttachment)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEcAttachmentOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.FileName ?? string.Empty,
            DictValue = x.FileName,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建设变附件表(EcAttachment)
    /// </summary>
    /// <param name="dto">创建设变附件表(EcAttachment)DTO</param>
    /// <returns>设变附件表(EcAttachment)DTO</returns>
    public async Task<TaktEcAttachmentDto> CreateEcAttachmentAsync(TaktEcAttachmentCreateDto dto)
    {
        var entity = dto.Adapt<TaktEcAttachment>();
        // 验证EcnNo的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.EcnNo, dto.EcnNo);
        if (!isUnique)
            throw new TaktBusinessException($"设变附件表EcnNo {dto.EcnNo} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetEcAttachmentByIdAsync(entity.Id)) ?? entity.Adapt<TaktEcAttachmentDto>();
    }


    /// <summary>
    /// 更新设变附件表(EcAttachment)
    /// </summary>
    /// <param name="id">设变附件表(EcAttachment)ID</param>
    /// <param name="dto">更新设变附件表(EcAttachment)DTO</param>
    /// <returns>设变附件表(EcAttachment)DTO</returns>
    public async Task<TaktEcAttachmentDto> UpdateEcAttachmentAsync(long id, TaktEcAttachmentUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ecattachmentNotFound");
        // 验证EcnNo的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.EcnNo, dto.EcnNo, id);
        if (!isUnique)
            throw new TaktBusinessException($"设变附件表EcnNo {dto.EcnNo} 已存在");

        dto.Adapt(entity, typeof(TaktEcAttachmentUpdateDto), typeof(TaktEcAttachment));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetEcAttachmentByIdAsync(id)) ?? entity.Adapt<TaktEcAttachmentDto>();
    }


    /// <summary>
    /// 删除设变附件表(EcAttachment)
    /// </summary>
    /// <param name="id">设变附件表(EcAttachment)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEcAttachmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ecattachmentNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除设变附件表(EcAttachment)
    /// </summary>
    /// <param name="ids">设变附件表(EcAttachment)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEcAttachmentBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktEcAttachment>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新设变附件表(EcAttachment)排序
    /// </summary>
    /// <param name="dto">设变附件表(EcAttachment)排序DTO</param>
    /// <returns>设变附件表(EcAttachment)DTO</returns>
    public async Task<TaktEcAttachmentDto> UpdateEcAttachmentSortAsync(TaktEcAttachmentSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.EcAttachmentId);
        if (entity == null)
            throw new TaktBusinessException("validation.ecattachmentNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetEcAttachmentByIdAsync(entity.Id) ?? entity.Adapt<TaktEcAttachmentDto>();
    }


    /// <summary>
    /// 获取设变附件表(EcAttachment)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetEcAttachmentTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEcAttachment));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEcAttachmentTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入设变附件表(EcAttachment)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEcAttachmentAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktEcAttachment));
        var importData = await TaktExcelHelper.ImportAsync<TaktEcAttachmentImportDto>(fileStream, excelSheet);
        
        var successCount = 0;
        var failCount = 0;
        var errors = new List<string>();
        var rowIndex = 0;

        foreach (var item in importData)
        {
            rowIndex++;
            try
            {
                // TODO: 添加必要的验证逻辑
                var entity = item.Adapt<TaktEcAttachment>();
                await _repository.CreateAsync(entity);
                successCount++;
            }
            catch (Exception ex)
            {
                errors.Add($"行{rowIndex}: {ex.Message}");
                failCount++;
            }
        }

        return (successCount, failCount, errors);
    }


    /// <summary>
    /// 导出设变附件表(EcAttachment)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEcAttachmentAsync(TaktEcAttachmentQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEcAttachmentQueryDto());
        List<TaktEcAttachment> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEcAttachment));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEcAttachmentExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktEcAttachmentExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建设变附件表查询表达式
    /// </summary>
    /// <param name="queryDto">设变附件表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEcAttachment, bool>> QueryExpression(TaktEcAttachmentQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktEcAttachment>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.EcnNo!.Contains(queryDto.KeyWords) ||
                x.AttachmentType!.Contains(queryDto.KeyWords) ||
                x.DocNo!.Contains(queryDto.KeyWords) ||
                x.FileName!.Contains(queryDto.KeyWords) ||
                x.AccessUrl!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EcnId.HasValue == true)
        {
            exp = exp.And(x => x.EcnId == queryDto.EcnId);
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnNo))
        {
            exp = exp.And(x => x.EcnNo!.Contains(queryDto.EcnNo));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.AttachmentType))
        {
            exp = exp.And(x => x.AttachmentType!.Contains(queryDto.AttachmentType));
        }

        if (!string.IsNullOrEmpty(queryDto?.DocNo))
        {
            exp = exp.And(x => x.DocNo!.Contains(queryDto.DocNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.FileName))
        {
            exp = exp.And(x => x.FileName!.Contains(queryDto.FileName));
        }

        if (!string.IsNullOrEmpty(queryDto?.AccessUrl))
        {
            exp = exp.And(x => x.AccessUrl!.Contains(queryDto.AccessUrl));
        }

        if (!string.IsNullOrEmpty(queryDto?.Remark))
        {
            exp = exp.And(x => x.Remark!.Contains(queryDto.Remark));
        }

        if (!string.IsNullOrEmpty(queryDto?.ExtFieldJson))
        {
            exp = exp.And(x => x.ExtFieldJson!.Contains(queryDto.ExtFieldJson));
        }

        if (queryDto?.CreatedById.HasValue == true)
        {
            exp = exp.And(x => x.CreatedById == queryDto.CreatedById);
        }

        if (!string.IsNullOrEmpty(queryDto?.CreatedBy))
        {
            exp = exp.And(x => x.CreatedBy!.Contains(queryDto.CreatedBy));
        }

        if (queryDto?.CreatedAt.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt == queryDto.CreatedAt);
        }

        // CreatedAt 日期范围查询
        if (queryDto?.CreatedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt >= queryDto.CreatedAtStart);
        }
        if (queryDto?.CreatedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt <= queryDto.CreatedAtEnd);
        }

        return exp.ToExpression();
    }
}
