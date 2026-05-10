// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPlantService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：工厂表应用服务，提供Plant管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 工厂表应用服务
/// </summary>
public class TaktPlantService : TaktServiceBase, ITaktPlantService
{
    private readonly ITaktRepository<TaktPlant> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Plant仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPlantService(
        ITaktRepository<TaktPlant> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取工厂表(Plant)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPlantDto>> GetPlantListAsync(TaktPlantQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPlantDto>.Create(
            data.Adapt<List<TaktPlantDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取工厂表(Plant)
    /// </summary>
    /// <param name="id">工厂表(Plant)ID</param>
    /// <returns>工厂表(Plant)DTO</returns>
    public async Task<TaktPlantDto?> GetPlantByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPlantDto>();
    }


    /// <summary>
    /// 获取工厂表(Plant)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工厂表(Plant)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPlantOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.PlantStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantName ?? string.Empty,
            DictValue = x.PlantCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建工厂表(Plant)
    /// </summary>
    /// <param name="dto">创建工厂表(Plant)DTO</param>
    /// <returns>工厂表(Plant)DTO</returns>
    public async Task<TaktPlantDto> CreatePlantAsync(TaktPlantCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PlantCode, dto.PlantCode, null, $"工厂表编码 {dto.PlantCode} 已存在");

        var entity = dto.Adapt<TaktPlant>();
        entity = await _repository.CreateAsync(entity);
        return (await GetPlantByIdAsync(entity.Id)) ?? entity.Adapt<TaktPlantDto>();
    }


    /// <summary>
    /// 更新工厂表(Plant)
    /// </summary>
    /// <param name="id">工厂表(Plant)ID</param>
    /// <param name="dto">更新工厂表(Plant)DTO</param>
    /// <returns>工厂表(Plant)DTO</returns>
    public async Task<TaktPlantDto> UpdatePlantAsync(long id, TaktPlantUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.plantNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PlantCode, dto.PlantCode, id, $"工厂表编码 {dto.PlantCode} 已存在");

        dto.Adapt(entity, typeof(TaktPlantUpdateDto), typeof(TaktPlant));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPlantByIdAsync(id)) ?? entity.Adapt<TaktPlantDto>();
    }


    /// <summary>
    /// 删除工厂表(Plant)
    /// </summary>
    /// <param name="id">工厂表(Plant)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePlantByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.plantNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.PlantStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除工厂表(Plant)
    /// </summary>
    /// <param name="ids">工厂表(Plant)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePlantBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPlant>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 PlantStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.PlantStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新工厂表(Plant)状态
    /// </summary>
    /// <param name="dto">工厂表(Plant)状态DTO</param>
    /// <returns>工厂表(Plant)DTO</returns>
    public async Task<TaktPlantDto> UpdatePlantStatusAsync(TaktPlantStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PlantId);
        if (entity == null)
            throw new TaktBusinessException("validation.plantNotFound");
        entity.PlantStatus = dto.PlantStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPlantByIdAsync(entity.Id) ?? entity.Adapt<TaktPlantDto>();
    }


    /// <summary>
    /// 更新工厂表(Plant)排序
    /// </summary>
    /// <param name="dto">工厂表(Plant)排序DTO</param>
    /// <returns>工厂表(Plant)DTO</returns>
    public async Task<TaktPlantDto> UpdatePlantSortAsync(TaktPlantSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PlantId);
        if (entity == null)
            throw new TaktBusinessException("validation.plantNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPlantByIdAsync(entity.Id) ?? entity.Adapt<TaktPlantDto>();
    }


    /// <summary>
    /// 获取工厂表(Plant)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPlantTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPlant));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPlantTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入工厂表(Plant)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPlantAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPlant));
        var importData = await TaktExcelHelper.ImportAsync<TaktPlantImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPlant>();
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
    /// 导出工厂表(Plant)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPlantAsync(TaktPlantQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPlantQueryDto());
        List<TaktPlant> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPlant));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPlantExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPlantExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建工厂表查询表达式
    /// </summary>
    /// <param name="queryDto">工厂表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPlant, bool>> QueryExpression(TaktPlantQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPlant>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.PlantName!.Contains(queryDto.KeyWords) ||
                x.PlantShortName!.Contains(queryDto.KeyWords) ||
                x.RegistrationAddress!.Contains(queryDto.KeyWords) ||
                x.RegistrationRegion!.Contains(queryDto.KeyWords) ||
                x.RegistrationProvince!.Contains(queryDto.KeyWords) ||
                x.RegistrationCity!.Contains(queryDto.KeyWords) ||
                x.BusinessRegion!.Contains(queryDto.KeyWords) ||
                x.BusinessProvince!.Contains(queryDto.KeyWords) ||
                x.BusinessCity!.Contains(queryDto.KeyWords) ||
                x.BusinessAddress!.Contains(queryDto.KeyWords) ||
                x.PlantAddress!.Contains(queryDto.KeyWords) ||
                x.PlantPhone!.Contains(queryDto.KeyWords) ||
                x.PlantEmail!.Contains(queryDto.KeyWords) ||
                x.PlantManager!.Contains(queryDto.KeyWords) ||
                x.EnterpriseNature!.Contains(queryDto.KeyWords) ||
                x.IndustryAttribute!.Contains(queryDto.KeyWords) ||
                x.EnterpriseScale!.Contains(queryDto.KeyWords) ||
                x.BusinessScope!.Contains(queryDto.KeyWords) ||
                x.RelatedCompany!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantName))
        {
            exp = exp.And(x => x.PlantName!.Contains(queryDto.PlantName));
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantShortName))
        {
            exp = exp.And(x => x.PlantShortName!.Contains(queryDto.PlantShortName));
        }

        if (!string.IsNullOrEmpty(queryDto?.RegistrationAddress))
        {
            exp = exp.And(x => x.RegistrationAddress!.Contains(queryDto.RegistrationAddress));
        }

        if (!string.IsNullOrEmpty(queryDto?.RegistrationRegion))
        {
            exp = exp.And(x => x.RegistrationRegion!.Contains(queryDto.RegistrationRegion));
        }

        if (!string.IsNullOrEmpty(queryDto?.RegistrationProvince))
        {
            exp = exp.And(x => x.RegistrationProvince!.Contains(queryDto.RegistrationProvince));
        }

        if (!string.IsNullOrEmpty(queryDto?.RegistrationCity))
        {
            exp = exp.And(x => x.RegistrationCity!.Contains(queryDto.RegistrationCity));
        }

        if (!string.IsNullOrEmpty(queryDto?.BusinessRegion))
        {
            exp = exp.And(x => x.BusinessRegion!.Contains(queryDto.BusinessRegion));
        }

        if (!string.IsNullOrEmpty(queryDto?.BusinessProvince))
        {
            exp = exp.And(x => x.BusinessProvince!.Contains(queryDto.BusinessProvince));
        }

        if (!string.IsNullOrEmpty(queryDto?.BusinessCity))
        {
            exp = exp.And(x => x.BusinessCity!.Contains(queryDto.BusinessCity));
        }

        if (!string.IsNullOrEmpty(queryDto?.BusinessAddress))
        {
            exp = exp.And(x => x.BusinessAddress!.Contains(queryDto.BusinessAddress));
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantAddress))
        {
            exp = exp.And(x => x.PlantAddress!.Contains(queryDto.PlantAddress));
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantPhone))
        {
            exp = exp.And(x => x.PlantPhone!.Contains(queryDto.PlantPhone));
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantEmail))
        {
            exp = exp.And(x => x.PlantEmail!.Contains(queryDto.PlantEmail));
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantManager))
        {
            exp = exp.And(x => x.PlantManager!.Contains(queryDto.PlantManager));
        }

        if (!string.IsNullOrEmpty(queryDto?.EnterpriseNature))
        {
            exp = exp.And(x => x.EnterpriseNature!.Contains(queryDto.EnterpriseNature));
        }

        if (!string.IsNullOrEmpty(queryDto?.IndustryAttribute))
        {
            exp = exp.And(x => x.IndustryAttribute!.Contains(queryDto.IndustryAttribute));
        }

        if (!string.IsNullOrEmpty(queryDto?.EnterpriseScale))
        {
            exp = exp.And(x => x.EnterpriseScale!.Contains(queryDto.EnterpriseScale));
        }

        if (!string.IsNullOrEmpty(queryDto?.BusinessScope))
        {
            exp = exp.And(x => x.BusinessScope!.Contains(queryDto.BusinessScope));
        }

        if (!string.IsNullOrEmpty(queryDto?.RelatedCompany))
        {
            exp = exp.And(x => x.RelatedCompany!.Contains(queryDto.RelatedCompany));
        }

        if (queryDto?.PlantStatus.HasValue == true)
        {
            exp = exp.And(x => x.PlantStatus == queryDto.PlantStatus);
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
