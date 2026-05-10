// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：TaktSamplingSchemeService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：抽样方案表应用服务，提供SamplingScheme管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Quality.Operation;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 抽样方案表应用服务
/// </summary>
public class TaktSamplingSchemeService : TaktServiceBase, ITaktSamplingSchemeService
{
    private readonly ITaktRepository<TaktSamplingScheme> _repository;
    private readonly ITaktRepository<TaktInspectionStandard> _inspectionStandardRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">SamplingScheme仓储</param>
    /// <param name="inspectionStandardRepository">InspectionStandard仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSamplingSchemeService(
        ITaktRepository<TaktSamplingScheme> repository,
        ITaktRepository<TaktInspectionStandard> inspectionStandardRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _inspectionStandardRepository = inspectionStandardRepository;
    }


    /// <summary>
    /// 获取抽样方案表(SamplingScheme)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSamplingSchemeDto>> GetSamplingSchemeListAsync(TaktSamplingSchemeQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSamplingSchemeDto>.Create(
            data.Adapt<List<TaktSamplingSchemeDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取抽样方案表(SamplingScheme)
    /// </summary>
    /// <param name="id">抽样方案表(SamplingScheme)ID</param>
    /// <returns>抽样方案表(SamplingScheme)DTO</returns>
    public async Task<TaktSamplingSchemeDto?> GetSamplingSchemeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktSamplingSchemeDto>();
        
        // 手动加载子表
        dto.InspectionStandards = (await _inspectionStandardRepository.FindAsync(x => x.SamplingSchemeId == id && x.IsDeleted == 0))
            .Adapt<List<TaktInspectionStandardDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取抽样方案表(SamplingScheme)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>抽样方案表(SamplingScheme)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetSamplingSchemeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.SchemeStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.SchemeName ?? string.Empty,
            DictValue = x.SchemeCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建抽样方案表(SamplingScheme)
    /// </summary>
    /// <param name="dto">创建抽样方案表(SamplingScheme)DTO</param>
    /// <returns>抽样方案表(SamplingScheme)DTO</returns>
    public async Task<TaktSamplingSchemeDto> CreateSamplingSchemeAsync(TaktSamplingSchemeCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.SchemeCode, dto.SchemeCode, null, $"抽样方案表编码 {dto.SchemeCode} 已存在");

        var entity = dto.Adapt<TaktSamplingScheme>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建InspectionStandard列表
            if (dto.InspectionStandards != null && dto.InspectionStandards.Count > 0)
            {
                var inspectionStandardList = dto.InspectionStandards.Select(x => {
                    var childEntity = x.Adapt<TaktInspectionStandard>();
                    childEntity.SamplingSchemeId = entity.Id;
                    return childEntity;
                }).ToList();
                await _inspectionStandardRepository.CreateRangeBulkAsync(inspectionStandardList);
            }
        }

        return (await GetSamplingSchemeByIdAsync(entity.Id)) ?? entity.Adapt<TaktSamplingSchemeDto>();
    }


    /// <summary>
    /// 更新抽样方案表(SamplingScheme)
    /// </summary>
    /// <param name="id">抽样方案表(SamplingScheme)ID</param>
    /// <param name="dto">更新抽样方案表(SamplingScheme)DTO</param>
    /// <returns>抽样方案表(SamplingScheme)DTO</returns>
    public async Task<TaktSamplingSchemeDto> UpdateSamplingSchemeAsync(long id, TaktSamplingSchemeUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.samplingschemeNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.SchemeCode, dto.SchemeCode, id, $"抽样方案表编码 {dto.SchemeCode} 已存在");

        dto.Adapt(entity, typeof(TaktSamplingSchemeUpdateDto), typeof(TaktSamplingScheme));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的InspectionStandard列表
        var oldInspectionStandards = await _inspectionStandardRepository.FindAsync(x => x.SamplingSchemeId == id && x.IsDeleted == 0);
        if (oldInspectionStandards != null && oldInspectionStandards.Count > 0)
        {
            foreach (var oldInspectionStandard in oldInspectionStandards)
            {
                oldInspectionStandard.IsDeleted = 1;
            }
            await _inspectionStandardRepository.UpdateRangeBulkAsync(oldInspectionStandards);
        }

        // 创建新的InspectionStandard列表
        if (dto.InspectionStandards != null && dto.InspectionStandards.Count > 0)
        {
            var inspectionStandardList = dto.InspectionStandards.Select(x => {
                var childEntity = x.Adapt<TaktInspectionStandard>();
                childEntity.SamplingSchemeId = id;
                return childEntity;
            }).ToList();
            await _inspectionStandardRepository.CreateRangeBulkAsync(inspectionStandardList);
        }


        return (await GetSamplingSchemeByIdAsync(id)) ?? entity.Adapt<TaktSamplingSchemeDto>();
    }


    /// <summary>
    /// 删除抽样方案表(SamplingScheme)
    /// </summary>
    /// <param name="id">抽样方案表(SamplingScheme)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteSamplingSchemeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.samplingschemeNotFound");
        
        // 级联删除子表数据
        // 级联删除InspectionStandard列表
        var inspectionStandards = await _inspectionStandardRepository.FindAsync(x => x.SamplingSchemeId == id && x.IsDeleted == 0);
        if (inspectionStandards != null && inspectionStandards.Count > 0)
        {
            foreach (var inspectionStandard in inspectionStandards)
            {
                inspectionStandard.IsDeleted = 1;
            }
            await _inspectionStandardRepository.UpdateRangeBulkAsync(inspectionStandards);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.SchemeStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除抽样方案表(SamplingScheme)
    /// </summary>
    /// <param name="ids">抽样方案表(SamplingScheme)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteSamplingSchemeBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktSamplingScheme>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除InspectionStandard列表
        var inspectionStandardsToDelete = new List<TaktInspectionStandard>();
        foreach (var id in idList)
        {
            var inspectionStandards = await _inspectionStandardRepository.FindAsync(x => x.SamplingSchemeId == id && x.IsDeleted == 0);
            if (inspectionStandards != null && inspectionStandards.Count > 0)
            {
                inspectionStandardsToDelete.AddRange(inspectionStandards);
            }
        }
        
        if (inspectionStandardsToDelete.Count > 0)
        {
            foreach (var inspectionStandard in inspectionStandardsToDelete)
            {
                inspectionStandard.IsDeleted = 1;
            }
            await _inspectionStandardRepository.UpdateRangeBulkAsync(inspectionStandardsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 SchemeStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.SchemeStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新抽样方案表(SamplingScheme)状态
    /// </summary>
    /// <param name="dto">抽样方案表(SamplingScheme)状态DTO</param>
    /// <returns>抽样方案表(SamplingScheme)DTO</returns>
    public async Task<TaktSamplingSchemeDto> UpdateSamplingSchemeSchemeStatusAsync(TaktSamplingSchemeSchemeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SamplingSchemeId);
        if (entity == null)
            throw new TaktBusinessException("validation.samplingschemeNotFound");
        entity.SchemeStatus = dto.SchemeStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSamplingSchemeByIdAsync(entity.Id) ?? entity.Adapt<TaktSamplingSchemeDto>();
    }


    /// <summary>
    /// 更新抽样方案表(SamplingScheme)排序
    /// </summary>
    /// <param name="dto">抽样方案表(SamplingScheme)排序DTO</param>
    /// <returns>抽样方案表(SamplingScheme)DTO</returns>
    public async Task<TaktSamplingSchemeDto> UpdateSamplingSchemeSortAsync(TaktSamplingSchemeSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SamplingSchemeId);
        if (entity == null)
            throw new TaktBusinessException("validation.samplingschemeNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSamplingSchemeByIdAsync(entity.Id) ?? entity.Adapt<TaktSamplingSchemeDto>();
    }


    /// <summary>
    /// 获取抽样方案表(SamplingScheme)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetSamplingSchemeTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktSamplingScheme));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSamplingSchemeTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入抽样方案表(SamplingScheme)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportSamplingSchemeAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktSamplingScheme));
        var importData = await TaktExcelHelper.ImportAsync<TaktSamplingSchemeImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktSamplingScheme>();
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
    /// 导出抽样方案表(SamplingScheme)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportSamplingSchemeAsync(TaktSamplingSchemeQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktSamplingSchemeQueryDto());
        List<TaktSamplingScheme> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktSamplingScheme));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSamplingSchemeExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktSamplingSchemeExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建抽样方案表查询表达式
    /// </summary>
    /// <param name="queryDto">抽样方案表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSamplingScheme, bool>> QueryExpression(TaktSamplingSchemeQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktSamplingScheme>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.SchemeCode!.Contains(queryDto.KeyWords) ||
                x.SchemeName!.Contains(queryDto.KeyWords) ||
                x.TransferRuleConfig!.Contains(queryDto.KeyWords) ||
                x.SchemeDescription!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SchemeCode))
        {
            exp = exp.And(x => x.SchemeCode!.Contains(queryDto.SchemeCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SchemeName))
        {
            exp = exp.And(x => x.SchemeName!.Contains(queryDto.SchemeName));
        }

        if (queryDto?.SchemeType.HasValue == true)
        {
            exp = exp.And(x => x.SchemeType == queryDto.SchemeType);
        }

        if (queryDto?.SamplingStandard.HasValue == true)
        {
            exp = exp.And(x => x.SamplingStandard == queryDto.SamplingStandard);
        }

        if (queryDto?.InspectionLevel.HasValue == true)
        {
            exp = exp.And(x => x.InspectionLevel == queryDto.InspectionLevel);
        }

        if (queryDto?.AqlValue.HasValue == true)
        {
            exp = exp.And(x => x.AqlValue == queryDto.AqlValue);
        }

        if (queryDto?.LotSizeMin.HasValue == true)
        {
            exp = exp.And(x => x.LotSizeMin == queryDto.LotSizeMin);
        }

        if (queryDto?.LotSizeMax.HasValue == true)
        {
            exp = exp.And(x => x.LotSizeMax == queryDto.LotSizeMax);
        }

        if (queryDto?.SampleSize.HasValue == true)
        {
            exp = exp.And(x => x.SampleSize == queryDto.SampleSize);
        }

        if (queryDto?.AcceptanceNumber.HasValue == true)
        {
            exp = exp.And(x => x.AcceptanceNumber == queryDto.AcceptanceNumber);
        }

        if (queryDto?.RejectionNumber.HasValue == true)
        {
            exp = exp.And(x => x.RejectionNumber == queryDto.RejectionNumber);
        }

        if (queryDto?.InspectionStrictness.HasValue == true)
        {
            exp = exp.And(x => x.InspectionStrictness == queryDto.InspectionStrictness);
        }

        if (queryDto?.IsTransferRuleEnabled.HasValue == true)
        {
            exp = exp.And(x => x.IsTransferRuleEnabled == queryDto.IsTransferRuleEnabled);
        }

        if (!string.IsNullOrEmpty(queryDto?.TransferRuleConfig))
        {
            exp = exp.And(x => x.TransferRuleConfig!.Contains(queryDto.TransferRuleConfig));
        }

        if (queryDto?.IsEnabled.HasValue == true)
        {
            exp = exp.And(x => x.IsEnabled == queryDto.IsEnabled);
        }

        if (queryDto?.SchemeStatus.HasValue == true)
        {
            exp = exp.And(x => x.SchemeStatus == queryDto.SchemeStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.SchemeDescription))
        {
            exp = exp.And(x => x.SchemeDescription!.Contains(queryDto.SchemeDescription));
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
