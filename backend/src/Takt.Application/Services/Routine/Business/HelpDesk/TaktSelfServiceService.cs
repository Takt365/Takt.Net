// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.HelpDesk
// 文件名称：TaktSelfServiceService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：自助服务表应用服务，提供SelfService管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Domain.Entities.Routine.Business.HelpDesk;

namespace Takt.Application.Services.Routine.Business.HelpDesk;

/// <summary>
/// 自助服务表应用服务
/// </summary>
public class TaktSelfServiceService : TaktServiceBase, ITaktSelfServiceService
{
    private readonly ITaktRepository<TaktSelfService> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">SelfService仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSelfServiceService(
        ITaktRepository<TaktSelfService> repository,
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
    /// 获取自助服务表(SelfService)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSelfServiceDto>> GetSelfServiceListAsync(TaktSelfServiceQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSelfServiceDto>.Create(
            data.Adapt<List<TaktSelfServiceDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取自助服务表(SelfService)
    /// </summary>
    /// <param name="id">自助服务表(SelfService)ID</param>
    /// <returns>自助服务表(SelfService)DTO</returns>
    public async Task<TaktSelfServiceDto?> GetSelfServiceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktSelfServiceDto>();
    }


    /// <summary>
    /// 获取自助服务表(SelfService)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>自助服务表(SelfService)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetSelfServiceOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.SelfServiceStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ServiceName ?? string.Empty,
            DictValue = x.ServiceName,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建自助服务表(SelfService)
    /// </summary>
    /// <param name="dto">创建自助服务表(SelfService)DTO</param>
    /// <returns>自助服务表(SelfService)DTO</returns>
    public async Task<TaktSelfServiceDto> CreateSelfServiceAsync(TaktSelfServiceCreateDto dto)
    {
        var entity = dto.Adapt<TaktSelfService>();
        entity = await _repository.CreateAsync(entity);
        return (await GetSelfServiceByIdAsync(entity.Id)) ?? entity.Adapt<TaktSelfServiceDto>();
    }


    /// <summary>
    /// 更新自助服务表(SelfService)
    /// </summary>
    /// <param name="id">自助服务表(SelfService)ID</param>
    /// <param name="dto">更新自助服务表(SelfService)DTO</param>
    /// <returns>自助服务表(SelfService)DTO</returns>
    public async Task<TaktSelfServiceDto> UpdateSelfServiceAsync(long id, TaktSelfServiceUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.selfserviceNotFound");
        dto.Adapt(entity, typeof(TaktSelfServiceUpdateDto), typeof(TaktSelfService));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetSelfServiceByIdAsync(id)) ?? entity.Adapt<TaktSelfServiceDto>();
    }


    /// <summary>
    /// 删除自助服务表(SelfService)
    /// </summary>
    /// <param name="id">自助服务表(SelfService)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteSelfServiceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.selfserviceNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.SelfServiceStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除自助服务表(SelfService)
    /// </summary>
    /// <param name="ids">自助服务表(SelfService)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteSelfServiceBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktSelfService>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 SelfServiceStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.SelfServiceStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新自助服务表(SelfService)状态
    /// </summary>
    /// <param name="dto">自助服务表(SelfService)状态DTO</param>
    /// <returns>自助服务表(SelfService)DTO</returns>
    public async Task<TaktSelfServiceDto> UpdateSelfServiceStatusAsync(TaktSelfServiceStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SelfServiceId);
        if (entity == null)
            throw new TaktBusinessException("validation.selfserviceNotFound");
        entity.SelfServiceStatus = dto.SelfServiceStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSelfServiceByIdAsync(entity.Id) ?? entity.Adapt<TaktSelfServiceDto>();
    }


    /// <summary>
    /// 更新自助服务表(SelfService)排序
    /// </summary>
    /// <param name="dto">自助服务表(SelfService)排序DTO</param>
    /// <returns>自助服务表(SelfService)DTO</returns>
    public async Task<TaktSelfServiceDto> UpdateSelfServiceSortAsync(TaktSelfServiceSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SelfServiceId);
        if (entity == null)
            throw new TaktBusinessException("validation.selfserviceNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSelfServiceByIdAsync(entity.Id) ?? entity.Adapt<TaktSelfServiceDto>();
    }


    /// <summary>
    /// 获取自助服务表(SelfService)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetSelfServiceTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktSelfService));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSelfServiceTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入自助服务表(SelfService)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportSelfServiceAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktSelfService));
        var importData = await TaktExcelHelper.ImportAsync<TaktSelfServiceImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktSelfService>();
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
    /// 导出自助服务表(SelfService)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportSelfServiceAsync(TaktSelfServiceQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktSelfServiceQueryDto());
        List<TaktSelfService> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktSelfService));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSelfServiceExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktSelfServiceExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建自助服务表查询表达式
    /// </summary>
    /// <param name="queryDto">自助服务表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSelfService, bool>> QueryExpression(TaktSelfServiceQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktSelfService>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ServiceName!.Contains(queryDto.KeyWords) ||
                x.Description!.Contains(queryDto.KeyWords) ||
                x.LinkOrCode!.Contains(queryDto.KeyWords) ||
                x.IconUrl!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.ServiceName))
        {
            exp = exp.And(x => x.ServiceName!.Contains(queryDto.ServiceName));
        }

        if (queryDto?.ServiceType.HasValue == true)
        {
            exp = exp.And(x => x.ServiceType == queryDto.ServiceType);
        }

        if (!string.IsNullOrEmpty(queryDto?.Description))
        {
            exp = exp.And(x => x.Description!.Contains(queryDto.Description));
        }

        if (!string.IsNullOrEmpty(queryDto?.LinkOrCode))
        {
            exp = exp.And(x => x.LinkOrCode!.Contains(queryDto.LinkOrCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.IconUrl))
        {
            exp = exp.And(x => x.IconUrl!.Contains(queryDto.IconUrl));
        }

        if (queryDto?.SelfServiceStatus.HasValue == true)
        {
            exp = exp.And(x => x.SelfServiceStatus == queryDto.SelfServiceStatus);
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
