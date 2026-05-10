// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：TaktEmployeeCareerService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：员工职业表应用服务，提供EmployeeCareer管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工职业表应用服务
/// </summary>
public class TaktEmployeeCareerService : TaktServiceBase, ITaktEmployeeCareerService
{
    private readonly ITaktRepository<TaktEmployeeCareer> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">EmployeeCareer仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEmployeeCareerService(
        ITaktRepository<TaktEmployeeCareer> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取员工职业表(EmployeeCareer)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEmployeeCareerDto>> GetEmployeeCareerListAsync(TaktEmployeeCareerQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEmployeeCareerDto>.Create(
            data.Adapt<List<TaktEmployeeCareerDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取员工职业表(EmployeeCareer)
    /// </summary>
    /// <param name="id">员工职业表(EmployeeCareer)ID</param>
    /// <returns>员工职业表(EmployeeCareer)DTO</returns>
    public async Task<TaktEmployeeCareerDto?> GetEmployeeCareerByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEmployeeCareerDto>();
    }


    /// <summary>
    /// 获取员工职业表(EmployeeCareer)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>员工职业表(EmployeeCareer)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEmployeeCareerOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.DeptName ?? string.Empty,
            DictValue = x.DeptName

        }).ToList();
    }


    /// <summary>
    /// 创建员工职业表(EmployeeCareer)
    /// </summary>
    /// <param name="dto">创建员工职业表(EmployeeCareer)DTO</param>
    /// <returns>员工职业表(EmployeeCareer)DTO</returns>
    public async Task<TaktEmployeeCareerDto> CreateEmployeeCareerAsync(TaktEmployeeCareerCreateDto dto)
    {
        var entity = dto.Adapt<TaktEmployeeCareer>();
        entity = await _repository.CreateAsync(entity);
        return (await GetEmployeeCareerByIdAsync(entity.Id)) ?? entity.Adapt<TaktEmployeeCareerDto>();
    }


    /// <summary>
    /// 更新员工职业表(EmployeeCareer)
    /// </summary>
    /// <param name="id">员工职业表(EmployeeCareer)ID</param>
    /// <param name="dto">更新员工职业表(EmployeeCareer)DTO</param>
    /// <returns>员工职业表(EmployeeCareer)DTO</returns>
    public async Task<TaktEmployeeCareerDto> UpdateEmployeeCareerAsync(long id, TaktEmployeeCareerUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeecareerNotFound");

        dto.Adapt(entity, typeof(TaktEmployeeCareerUpdateDto), typeof(TaktEmployeeCareer));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetEmployeeCareerByIdAsync(id)) ?? entity.Adapt<TaktEmployeeCareerDto>();
    }


    /// <summary>
    /// 删除员工职业表(EmployeeCareer)
    /// </summary>
    /// <param name="id">员工职业表(EmployeeCareer)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeCareerByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeecareerNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除员工职业表(EmployeeCareer)
    /// </summary>
    /// <param name="ids">员工职业表(EmployeeCareer)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeCareerBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktEmployeeCareer>();
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
    /// 获取员工职业表(EmployeeCareer)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetEmployeeCareerTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEmployeeCareer));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEmployeeCareerTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入员工职业表(EmployeeCareer)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEmployeeCareerAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktEmployeeCareer));
        var importData = await TaktExcelHelper.ImportAsync<TaktEmployeeCareerImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktEmployeeCareer>();
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
    /// 导出员工职业表(EmployeeCareer)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEmployeeCareerAsync(TaktEmployeeCareerQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEmployeeCareerQueryDto());
        List<TaktEmployeeCareer> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployeeCareer));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEmployeeCareerExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktEmployeeCareerExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建员工职业表查询表达式
    /// </summary>
    /// <param name="queryDto">员工职业表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEmployeeCareer, bool>> QueryExpression(TaktEmployeeCareerQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktEmployeeCareer>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.DeptName!.Contains(queryDto.KeyWords) ||
                x.PostName!.Contains(queryDto.KeyWords) ||
                x.JobLevel!.Contains(queryDto.KeyWords) ||
                x.JobTitle!.Contains(queryDto.KeyWords) ||
                x.WorkLocation!.Contains(queryDto.KeyWords) ||
                x.DirectManagerName!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (queryDto?.DeptId.HasValue == true)
        {
            exp = exp.And(x => x.DeptId == queryDto.DeptId);
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptName))
        {
            exp = exp.And(x => x.DeptName!.Contains(queryDto.DeptName));
        }

        if (queryDto?.PostId.HasValue == true)
        {
            exp = exp.And(x => x.PostId == queryDto.PostId);
        }

        if (!string.IsNullOrEmpty(queryDto?.PostName))
        {
            exp = exp.And(x => x.PostName!.Contains(queryDto.PostName));
        }

        if (!string.IsNullOrEmpty(queryDto?.JobLevel))
        {
            exp = exp.And(x => x.JobLevel!.Contains(queryDto.JobLevel));
        }

        if (!string.IsNullOrEmpty(queryDto?.JobTitle))
        {
            exp = exp.And(x => x.JobTitle!.Contains(queryDto.JobTitle));
        }

        if (queryDto?.JoinDate.HasValue == true)
        {
            exp = exp.And(x => x.JoinDate == queryDto.JoinDate);
        }

        if (queryDto?.RegularizationDate.HasValue == true)
        {
            exp = exp.And(x => x.RegularizationDate == queryDto.RegularizationDate);
        }

        if (queryDto?.LeaveDate.HasValue == true)
        {
            exp = exp.And(x => x.LeaveDate == queryDto.LeaveDate);
        }

        if (queryDto?.WorkYears.HasValue == true)
        {
            exp = exp.And(x => x.WorkYears == queryDto.WorkYears);
        }

        if (!string.IsNullOrEmpty(queryDto?.WorkLocation))
        {
            exp = exp.And(x => x.WorkLocation!.Contains(queryDto.WorkLocation));
        }

        if (queryDto?.WorkNature.HasValue == true)
        {
            exp = exp.And(x => x.WorkNature == queryDto.WorkNature);
        }

        if (queryDto?.EmploymentType.HasValue == true)
        {
            exp = exp.And(x => x.EmploymentType == queryDto.EmploymentType);
        }

        if (queryDto?.IsPrimary.HasValue == true)
        {
            exp = exp.And(x => x.IsPrimary == queryDto.IsPrimary);
        }

        if (queryDto?.DirectManagerId.HasValue == true)
        {
            exp = exp.And(x => x.DirectManagerId == queryDto.DirectManagerId);
        }

        if (!string.IsNullOrEmpty(queryDto?.DirectManagerName))
        {
            exp = exp.And(x => x.DirectManagerName!.Contains(queryDto.DirectManagerName));
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

        // JoinDate 日期范围查询
        if (queryDto?.JoinDateStart.HasValue == true)
        {
            exp = exp.And(x => x.JoinDate >= queryDto.JoinDateStart);
        }
        if (queryDto?.JoinDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.JoinDate <= queryDto.JoinDateEnd);
        }

        // RegularizationDate 日期范围查询
        if (queryDto?.RegularizationDateStart.HasValue == true)
        {
            exp = exp.And(x => x.RegularizationDate >= queryDto.RegularizationDateStart);
        }
        if (queryDto?.RegularizationDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.RegularizationDate <= queryDto.RegularizationDateEnd);
        }

        // LeaveDate 日期范围查询
        if (queryDto?.LeaveDateStart.HasValue == true)
        {
            exp = exp.And(x => x.LeaveDate >= queryDto.LeaveDateStart);
        }
        if (queryDto?.LeaveDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.LeaveDate <= queryDto.LeaveDateEnd);
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
