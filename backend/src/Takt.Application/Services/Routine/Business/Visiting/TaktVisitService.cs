// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.Visiting
// 文件名称：TaktVisitService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：参访公司表应用服务，提供Visit管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Business.Visiting;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Business.Visiting;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.Visiting;

/// <summary>
/// 参访公司表应用服务
/// </summary>
public class TaktVisitService : TaktServiceBase, ITaktVisitService
{
    private readonly ITaktRepository<TaktVisit> _repository;
    private readonly ITaktRepository<TaktVisitPerson> _visitPersonRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Visit仓储</param>
    /// <param name="visitPersonRepository">VisitPerson仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktVisitService(
        ITaktRepository<TaktVisit> repository,
        ITaktRepository<TaktVisitPerson> visitPersonRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _visitPersonRepository = visitPersonRepository;
    }


    /// <summary>
    /// 获取参访公司表(Visit)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktVisitDto>> GetVisitListAsync(TaktVisitQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktVisitDto>.Create(
            data.Adapt<List<TaktVisitDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取参访公司表(Visit)
    /// </summary>
    /// <param name="id">参访公司表(Visit)ID</param>
    /// <returns>参访公司表(Visit)DTO</returns>
    public async Task<TaktVisitDto?> GetVisitByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktVisitDto>();
        
        // 手动加载子表
        dto.Persons = (await _visitPersonRepository.FindAsync(x => x.VisitId == id && x.IsDeleted == 0))
            .Adapt<List<TaktVisitPersonDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取参访公司表(Visit)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>参访公司表(Visit)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetVisitOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.CompanyName ?? string.Empty,
            DictValue = x.CompanyName

        }).ToList();
    }


    /// <summary>
    /// 创建参访公司表(Visit)
    /// </summary>
    /// <param name="dto">创建参访公司表(Visit)DTO</param>
    /// <returns>参访公司表(Visit)DTO</returns>
    public async Task<TaktVisitDto> CreateVisitAsync(TaktVisitCreateDto dto)
    {
        var entity = dto.Adapt<TaktVisit>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建VisitPerson列表
            if (dto.Persons != null && dto.Persons.Count > 0)
            {
                var visitPersonList = dto.Persons.Select(x => {
                    var childEntity = x.Adapt<TaktVisitPerson>();
                    childEntity.VisitId = entity.Id;
                    return childEntity;
                }).ToList();
                await _visitPersonRepository.CreateRangeBulkAsync(visitPersonList);
            }
        }

        return (await GetVisitByIdAsync(entity.Id)) ?? entity.Adapt<TaktVisitDto>();
    }


    /// <summary>
    /// 更新参访公司表(Visit)
    /// </summary>
    /// <param name="id">参访公司表(Visit)ID</param>
    /// <param name="dto">更新参访公司表(Visit)DTO</param>
    /// <returns>参访公司表(Visit)DTO</returns>
    public async Task<TaktVisitDto> UpdateVisitAsync(long id, TaktVisitUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.visitNotFound");

        dto.Adapt(entity, typeof(TaktVisitUpdateDto), typeof(TaktVisit));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的VisitPerson列表
        var oldVisitPersons = await _visitPersonRepository.FindAsync(x => x.VisitId == id && x.IsDeleted == 0);
        if (oldVisitPersons != null && oldVisitPersons.Count > 0)
        {
            foreach (var oldVisitPerson in oldVisitPersons)
            {
                oldVisitPerson.IsDeleted = 1;
            }
            await _visitPersonRepository.UpdateRangeBulkAsync(oldVisitPersons);
        }

        // 创建新的VisitPerson列表
        if (dto.Persons != null && dto.Persons.Count > 0)
        {
            var visitPersonList = dto.Persons.Select(x => {
                var childEntity = x.Adapt<TaktVisitPerson>();
                childEntity.VisitId = id;
                return childEntity;
            }).ToList();
            await _visitPersonRepository.CreateRangeBulkAsync(visitPersonList);
        }


        return (await GetVisitByIdAsync(id)) ?? entity.Adapt<TaktVisitDto>();
    }


    /// <summary>
    /// 删除参访公司表(Visit)
    /// </summary>
    /// <param name="id">参访公司表(Visit)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteVisitByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.visitNotFound");
        
        // 级联删除子表数据
        // 级联删除VisitPerson列表
        var visitPersons = await _visitPersonRepository.FindAsync(x => x.VisitId == id && x.IsDeleted == 0);
        if (visitPersons != null && visitPersons.Count > 0)
        {
            foreach (var visitPerson in visitPersons)
            {
                visitPerson.IsDeleted = 1;
            }
            await _visitPersonRepository.UpdateRangeBulkAsync(visitPersons);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除参访公司表(Visit)
    /// </summary>
    /// <param name="ids">参访公司表(Visit)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteVisitBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktVisit>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除VisitPerson列表
        var visitPersonsToDelete = new List<TaktVisitPerson>();
        foreach (var id in idList)
        {
            var visitPersons = await _visitPersonRepository.FindAsync(x => x.VisitId == id && x.IsDeleted == 0);
            if (visitPersons != null && visitPersons.Count > 0)
            {
                visitPersonsToDelete.AddRange(visitPersons);
            }
        }
        
        if (visitPersonsToDelete.Count > 0)
        {
            foreach (var visitPerson in visitPersonsToDelete)
            {
                visitPerson.IsDeleted = 1;
            }
            await _visitPersonRepository.UpdateRangeBulkAsync(visitPersonsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取参访公司表(Visit)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetVisitTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktVisit));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktVisitTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入参访公司表(Visit)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportVisitAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktVisit));
        var importData = await TaktExcelHelper.ImportAsync<TaktVisitImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktVisit>();
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
    /// 导出参访公司表(Visit)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportVisitAsync(TaktVisitQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktVisitQueryDto());
        List<TaktVisit> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktVisit));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktVisitExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktVisitExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建参访公司表查询表达式
    /// </summary>
    /// <param name="queryDto">参访公司表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktVisit, bool>> QueryExpression(TaktVisitQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktVisit>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CompanyName!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyName))
        {
            exp = exp.And(x => x.CompanyName!.Contains(queryDto.CompanyName));
        }

        if (queryDto?.VisitStartTime.HasValue == true)
        {
            exp = exp.And(x => x.VisitStartTime == queryDto.VisitStartTime);
        }

        if (queryDto?.VisitEndTime.HasValue == true)
        {
            exp = exp.And(x => x.VisitEndTime == queryDto.VisitEndTime);
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

        // VisitStartTime 日期范围查询
        if (queryDto?.VisitStartTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.VisitStartTime >= queryDto.VisitStartTimeStart);
        }
        if (queryDto?.VisitStartTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.VisitStartTime <= queryDto.VisitStartTimeEnd);
        }

        // VisitEndTime 日期范围查询
        if (queryDto?.VisitEndTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.VisitEndTime >= queryDto.VisitEndTimeStart);
        }
        if (queryDto?.VisitEndTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.VisitEndTime <= queryDto.VisitEndTimeEnd);
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
