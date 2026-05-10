// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Cost
// 文件名称：TaktQualityIssueService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：品质问题应对主表应用服务，提供QualityIssue管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Quality.Cost;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Cost;

/// <summary>
/// 品质问题应对主表应用服务
/// </summary>
public class TaktQualityIssueService : TaktServiceBase, ITaktQualityIssueService
{
    private readonly ITaktRepository<TaktQualityIssue> _repository;
    private readonly ITaktRepository<TaktQualityIssueMeeting> _qualityIssueMeetingRepository;
    private readonly ITaktRepository<TaktQualityIssueAssyRework> _qualityIssueAssyReworkRepository;
    private readonly ITaktRepository<TaktQualityIssuePcbaRework> _qualityIssuePcbaReworkRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">QualityIssue仓储</param>
    /// <param name="qualityIssueMeetingRepository">QualityIssueMeeting仓储</param>
    /// <param name="qualityIssueAssyReworkRepository">QualityIssueAssyRework仓储</param>
    /// <param name="qualityIssuePcbaReworkRepository">QualityIssuePcbaRework仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktQualityIssueService(
        ITaktRepository<TaktQualityIssue> repository,
        ITaktRepository<TaktQualityIssueMeeting> qualityIssueMeetingRepository,
        ITaktRepository<TaktQualityIssueAssyRework> qualityIssueAssyReworkRepository,
        ITaktRepository<TaktQualityIssuePcbaRework> qualityIssuePcbaReworkRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _qualityIssueMeetingRepository = qualityIssueMeetingRepository;
        _qualityIssueAssyReworkRepository = qualityIssueAssyReworkRepository;
        _qualityIssuePcbaReworkRepository = qualityIssuePcbaReworkRepository;
    }


    /// <summary>
    /// 获取品质问题应对主表(QualityIssue)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktQualityIssueDto>> GetQualityIssueListAsync(TaktQualityIssueQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktQualityIssueDto>.Create(
            data.Adapt<List<TaktQualityIssueDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取品质问题应对主表(QualityIssue)
    /// </summary>
    /// <param name="id">品质问题应对主表(QualityIssue)ID</param>
    /// <returns>品质问题应对主表(QualityIssue)DTO</returns>
    public async Task<TaktQualityIssueDto?> GetQualityIssueByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktQualityIssueDto>();
        
        // 手动加载子表
        dto.MeetingItems = (await _qualityIssueMeetingRepository.FindAsync(x => x.QualityIssueId == id && x.IsDeleted == 0))
            .Adapt<List<TaktQualityIssueMeetingDto>>();
        dto.AssyReworkItems = (await _qualityIssueAssyReworkRepository.FindAsync(x => x.QualityIssueId == id && x.IsDeleted == 0))
            .Adapt<List<TaktQualityIssueAssyReworkDto>>();
        dto.PcbaReworkItems = (await _qualityIssuePcbaReworkRepository.FindAsync(x => x.QualityIssueId == id && x.IsDeleted == 0))
            .Adapt<List<TaktQualityIssuePcbaReworkDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取品质问题应对主表(QualityIssue)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>品质问题应对主表(QualityIssue)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetQualityIssueOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建品质问题应对主表(QualityIssue)
    /// </summary>
    /// <param name="dto">创建品质问题应对主表(QualityIssue)DTO</param>
    /// <returns>品质问题应对主表(QualityIssue)DTO</returns>
    public async Task<TaktQualityIssueDto> CreateQualityIssueAsync(TaktQualityIssueCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.IssueNo, dto.IssueNo, null, $"品质问题应对主表编码 {dto.IssueNo} 已存在");

        var entity = dto.Adapt<TaktQualityIssue>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建QualityIssueMeeting列表
            if (dto.MeetingItems != null && dto.MeetingItems.Count > 0)
            {
                var qualityIssueMeetingList = dto.MeetingItems.Select(x => {
                    var childEntity = x.Adapt<TaktQualityIssueMeeting>();
                    childEntity.QualityIssueId = entity.Id;
                    return childEntity;
                }).ToList();
                await _qualityIssueMeetingRepository.CreateRangeBulkAsync(qualityIssueMeetingList);
            }
            // 创建QualityIssueAssyRework列表
            if (dto.AssyReworkItems != null && dto.AssyReworkItems.Count > 0)
            {
                var qualityIssueAssyReworkList = dto.AssyReworkItems.Select(x => {
                    var childEntity = x.Adapt<TaktQualityIssueAssyRework>();
                    childEntity.QualityIssueId = entity.Id;
                    return childEntity;
                }).ToList();
                await _qualityIssueAssyReworkRepository.CreateRangeBulkAsync(qualityIssueAssyReworkList);
            }
            // 创建QualityIssuePcbaRework列表
            if (dto.PcbaReworkItems != null && dto.PcbaReworkItems.Count > 0)
            {
                var qualityIssuePcbaReworkList = dto.PcbaReworkItems.Select(x => {
                    var childEntity = x.Adapt<TaktQualityIssuePcbaRework>();
                    childEntity.QualityIssueId = entity.Id;
                    return childEntity;
                }).ToList();
                await _qualityIssuePcbaReworkRepository.CreateRangeBulkAsync(qualityIssuePcbaReworkList);
            }
        }

        return (await GetQualityIssueByIdAsync(entity.Id)) ?? entity.Adapt<TaktQualityIssueDto>();
    }


    /// <summary>
    /// 更新品质问题应对主表(QualityIssue)
    /// </summary>
    /// <param name="id">品质问题应对主表(QualityIssue)ID</param>
    /// <param name="dto">更新品质问题应对主表(QualityIssue)DTO</param>
    /// <returns>品质问题应对主表(QualityIssue)DTO</returns>
    public async Task<TaktQualityIssueDto> UpdateQualityIssueAsync(long id, TaktQualityIssueUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityissueNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.IssueNo, dto.IssueNo, id, $"品质问题应对主表编码 {dto.IssueNo} 已存在");

        dto.Adapt(entity, typeof(TaktQualityIssueUpdateDto), typeof(TaktQualityIssue));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的QualityIssueMeeting列表
        var oldQualityIssueMeetings = await _qualityIssueMeetingRepository.FindAsync(x => x.QualityIssueId == id && x.IsDeleted == 0);
        if (oldQualityIssueMeetings != null && oldQualityIssueMeetings.Count > 0)
        {
            foreach (var oldQualityIssueMeeting in oldQualityIssueMeetings)
            {
                oldQualityIssueMeeting.IsDeleted = 1;
            }
            await _qualityIssueMeetingRepository.UpdateRangeBulkAsync(oldQualityIssueMeetings);
        }

        // 创建新的QualityIssueMeeting列表
        if (dto.MeetingItems != null && dto.MeetingItems.Count > 0)
        {
            var qualityIssueMeetingList = dto.MeetingItems.Select(x => {
                var childEntity = x.Adapt<TaktQualityIssueMeeting>();
                childEntity.QualityIssueId = id;
                return childEntity;
            }).ToList();
            await _qualityIssueMeetingRepository.CreateRangeBulkAsync(qualityIssueMeetingList);
        }
        // 删除旧的QualityIssueAssyRework列表
        var oldQualityIssueAssyReworks = await _qualityIssueAssyReworkRepository.FindAsync(x => x.QualityIssueId == id && x.IsDeleted == 0);
        if (oldQualityIssueAssyReworks != null && oldQualityIssueAssyReworks.Count > 0)
        {
            foreach (var oldQualityIssueAssyRework in oldQualityIssueAssyReworks)
            {
                oldQualityIssueAssyRework.IsDeleted = 1;
            }
            await _qualityIssueAssyReworkRepository.UpdateRangeBulkAsync(oldQualityIssueAssyReworks);
        }

        // 创建新的QualityIssueAssyRework列表
        if (dto.AssyReworkItems != null && dto.AssyReworkItems.Count > 0)
        {
            var qualityIssueAssyReworkList = dto.AssyReworkItems.Select(x => {
                var childEntity = x.Adapt<TaktQualityIssueAssyRework>();
                childEntity.QualityIssueId = id;
                return childEntity;
            }).ToList();
            await _qualityIssueAssyReworkRepository.CreateRangeBulkAsync(qualityIssueAssyReworkList);
        }
        // 删除旧的QualityIssuePcbaRework列表
        var oldQualityIssuePcbaReworks = await _qualityIssuePcbaReworkRepository.FindAsync(x => x.QualityIssueId == id && x.IsDeleted == 0);
        if (oldQualityIssuePcbaReworks != null && oldQualityIssuePcbaReworks.Count > 0)
        {
            foreach (var oldQualityIssuePcbaRework in oldQualityIssuePcbaReworks)
            {
                oldQualityIssuePcbaRework.IsDeleted = 1;
            }
            await _qualityIssuePcbaReworkRepository.UpdateRangeBulkAsync(oldQualityIssuePcbaReworks);
        }

        // 创建新的QualityIssuePcbaRework列表
        if (dto.PcbaReworkItems != null && dto.PcbaReworkItems.Count > 0)
        {
            var qualityIssuePcbaReworkList = dto.PcbaReworkItems.Select(x => {
                var childEntity = x.Adapt<TaktQualityIssuePcbaRework>();
                childEntity.QualityIssueId = id;
                return childEntity;
            }).ToList();
            await _qualityIssuePcbaReworkRepository.CreateRangeBulkAsync(qualityIssuePcbaReworkList);
        }


        return (await GetQualityIssueByIdAsync(id)) ?? entity.Adapt<TaktQualityIssueDto>();
    }


    /// <summary>
    /// 删除品质问题应对主表(QualityIssue)
    /// </summary>
    /// <param name="id">品质问题应对主表(QualityIssue)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityIssueByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityissueNotFound");
        
        // 级联删除子表数据
        // 级联删除QualityIssueMeeting列表
        var qualityIssueMeetings = await _qualityIssueMeetingRepository.FindAsync(x => x.QualityIssueId == id && x.IsDeleted == 0);
        if (qualityIssueMeetings != null && qualityIssueMeetings.Count > 0)
        {
            foreach (var qualityIssueMeeting in qualityIssueMeetings)
            {
                qualityIssueMeeting.IsDeleted = 1;
            }
            await _qualityIssueMeetingRepository.UpdateRangeBulkAsync(qualityIssueMeetings);
        }
        // 级联删除QualityIssueAssyRework列表
        var qualityIssueAssyReworks = await _qualityIssueAssyReworkRepository.FindAsync(x => x.QualityIssueId == id && x.IsDeleted == 0);
        if (qualityIssueAssyReworks != null && qualityIssueAssyReworks.Count > 0)
        {
            foreach (var qualityIssueAssyRework in qualityIssueAssyReworks)
            {
                qualityIssueAssyRework.IsDeleted = 1;
            }
            await _qualityIssueAssyReworkRepository.UpdateRangeBulkAsync(qualityIssueAssyReworks);
        }
        // 级联删除QualityIssuePcbaRework列表
        var qualityIssuePcbaReworks = await _qualityIssuePcbaReworkRepository.FindAsync(x => x.QualityIssueId == id && x.IsDeleted == 0);
        if (qualityIssuePcbaReworks != null && qualityIssuePcbaReworks.Count > 0)
        {
            foreach (var qualityIssuePcbaRework in qualityIssuePcbaReworks)
            {
                qualityIssuePcbaRework.IsDeleted = 1;
            }
            await _qualityIssuePcbaReworkRepository.UpdateRangeBulkAsync(qualityIssuePcbaReworks);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除品质问题应对主表(QualityIssue)
    /// </summary>
    /// <param name="ids">品质问题应对主表(QualityIssue)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityIssueBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktQualityIssue>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除QualityIssueMeeting列表
        var qualityIssueMeetingsToDelete = new List<TaktQualityIssueMeeting>();
        foreach (var id in idList)
        {
            var qualityIssueMeetings = await _qualityIssueMeetingRepository.FindAsync(x => x.QualityIssueId == id && x.IsDeleted == 0);
            if (qualityIssueMeetings != null && qualityIssueMeetings.Count > 0)
            {
                qualityIssueMeetingsToDelete.AddRange(qualityIssueMeetings);
            }
        }
        
        if (qualityIssueMeetingsToDelete.Count > 0)
        {
            foreach (var qualityIssueMeeting in qualityIssueMeetingsToDelete)
            {
                qualityIssueMeeting.IsDeleted = 1;
            }
            await _qualityIssueMeetingRepository.UpdateRangeBulkAsync(qualityIssueMeetingsToDelete);
        }
        // 批量级联删除QualityIssueAssyRework列表
        var qualityIssueAssyReworksToDelete = new List<TaktQualityIssueAssyRework>();
        foreach (var id in idList)
        {
            var qualityIssueAssyReworks = await _qualityIssueAssyReworkRepository.FindAsync(x => x.QualityIssueId == id && x.IsDeleted == 0);
            if (qualityIssueAssyReworks != null && qualityIssueAssyReworks.Count > 0)
            {
                qualityIssueAssyReworksToDelete.AddRange(qualityIssueAssyReworks);
            }
        }
        
        if (qualityIssueAssyReworksToDelete.Count > 0)
        {
            foreach (var qualityIssueAssyRework in qualityIssueAssyReworksToDelete)
            {
                qualityIssueAssyRework.IsDeleted = 1;
            }
            await _qualityIssueAssyReworkRepository.UpdateRangeBulkAsync(qualityIssueAssyReworksToDelete);
        }
        // 批量级联删除QualityIssuePcbaRework列表
        var qualityIssuePcbaReworksToDelete = new List<TaktQualityIssuePcbaRework>();
        foreach (var id in idList)
        {
            var qualityIssuePcbaReworks = await _qualityIssuePcbaReworkRepository.FindAsync(x => x.QualityIssueId == id && x.IsDeleted == 0);
            if (qualityIssuePcbaReworks != null && qualityIssuePcbaReworks.Count > 0)
            {
                qualityIssuePcbaReworksToDelete.AddRange(qualityIssuePcbaReworks);
            }
        }
        
        if (qualityIssuePcbaReworksToDelete.Count > 0)
        {
            foreach (var qualityIssuePcbaRework in qualityIssuePcbaReworksToDelete)
            {
                qualityIssuePcbaRework.IsDeleted = 1;
            }
            await _qualityIssuePcbaReworkRepository.UpdateRangeBulkAsync(qualityIssuePcbaReworksToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取品质问题应对主表(QualityIssue)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetQualityIssueTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktQualityIssue));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktQualityIssueTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入品质问题应对主表(QualityIssue)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportQualityIssueAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktQualityIssue));
        var importData = await TaktExcelHelper.ImportAsync<TaktQualityIssueImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktQualityIssue>();
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
    /// 导出品质问题应对主表(QualityIssue)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportQualityIssueAsync(TaktQualityIssueQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktQualityIssueQueryDto());
        List<TaktQualityIssue> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktQualityIssue));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktQualityIssueExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktQualityIssueExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建品质问题应对主表查询表达式
    /// </summary>
    /// <param name="queryDto">品质问题应对主表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktQualityIssue, bool>> QueryExpression(TaktQualityIssueQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktQualityIssue>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.IssueNo!.Contains(queryDto.KeyWords) ||
                x.Model!.Contains(queryDto.KeyWords) ||
                x.Lot!.Contains(queryDto.KeyWords) ||
                x.QualityProblemsResponse!.Contains(queryDto.KeyWords) ||
                x.ReworkDueToDefects!.Contains(queryDto.KeyWords) ||
                x.NeedRework!.Contains(queryDto.KeyWords) ||
                x.CostCurrency!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.IssueNo))
        {
            exp = exp.And(x => x.IssueNo!.Contains(queryDto.IssueNo));
        }

        if (queryDto?.IssueDate.HasValue == true)
        {
            exp = exp.And(x => x.IssueDate == queryDto.IssueDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.Model))
        {
            exp = exp.And(x => x.Model!.Contains(queryDto.Model));
        }

        if (!string.IsNullOrEmpty(queryDto?.Lot))
        {
            exp = exp.And(x => x.Lot!.Contains(queryDto.Lot));
        }

        if (!string.IsNullOrEmpty(queryDto?.QualityProblemsResponse))
        {
            exp = exp.And(x => x.QualityProblemsResponse!.Contains(queryDto.QualityProblemsResponse));
        }

        if (!string.IsNullOrEmpty(queryDto?.ReworkDueToDefects))
        {
            exp = exp.And(x => x.ReworkDueToDefects!.Contains(queryDto.ReworkDueToDefects));
        }

        if (!string.IsNullOrEmpty(queryDto?.NeedRework))
        {
            exp = exp.And(x => x.NeedRework!.Contains(queryDto.NeedRework));
        }

        if (queryDto?.TotalTimeMinutes.HasValue == true)
        {
            exp = exp.And(x => x.TotalTimeMinutes == queryDto.TotalTimeMinutes);
        }

        if (queryDto?.TotalCost.HasValue == true)
        {
            exp = exp.And(x => x.TotalCost == queryDto.TotalCost);
        }

        if (!string.IsNullOrEmpty(queryDto?.CostCurrency))
        {
            exp = exp.And(x => x.CostCurrency!.Contains(queryDto.CostCurrency));
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

        // IssueDate 日期范围查询
        if (queryDto?.IssueDateStart.HasValue == true)
        {
            exp = exp.And(x => x.IssueDate >= queryDto.IssueDateStart);
        }
        if (queryDto?.IssueDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.IssueDate <= queryDto.IssueDateEnd);
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
