// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Organization
// 文件名称：TaktEmployeePostService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：员工岗位关联表应用服务，提供EmployeePost管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// 员工岗位关联表应用服务
/// </summary>
public class TaktEmployeePostService : TaktServiceBase, ITaktEmployeePostService
{
    private readonly ITaktRepository<TaktEmployeePost> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">EmployeePost仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEmployeePostService(
        ITaktRepository<TaktEmployeePost> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取员工岗位关联表(EmployeePost)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEmployeePostDto>> GetEmployeePostListAsync(TaktEmployeePostQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEmployeePostDto>.Create(
            data.Adapt<List<TaktEmployeePostDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取员工岗位关联表(EmployeePost)
    /// </summary>
    /// <param name="id">员工岗位关联表(EmployeePost)ID</param>
    /// <returns>员工岗位关联表(EmployeePost)DTO</returns>
    public async Task<TaktEmployeePostDto?> GetEmployeePostByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEmployeePostDto>();
    }


    /// <summary>
    /// 获取员工岗位关联表(EmployeePost)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>员工岗位关联表(EmployeePost)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEmployeePostOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建员工岗位关联表(EmployeePost)
    /// </summary>
    /// <param name="dto">创建员工岗位关联表(EmployeePost)DTO</param>
    /// <returns>员工岗位关联表(EmployeePost)DTO</returns>
    public async Task<TaktEmployeePostDto> CreateEmployeePostAsync(TaktEmployeePostCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PostId, dto.PostId, null, $"员工岗位关联表编码 {dto.PostId} 已存在");

        var entity = dto.Adapt<TaktEmployeePost>();
        entity = await _repository.CreateAsync(entity);
        return (await GetEmployeePostByIdAsync(entity.Id)) ?? entity.Adapt<TaktEmployeePostDto>();
    }


    /// <summary>
    /// 更新员工岗位关联表(EmployeePost)
    /// </summary>
    /// <param name="id">员工岗位关联表(EmployeePost)ID</param>
    /// <param name="dto">更新员工岗位关联表(EmployeePost)DTO</param>
    /// <returns>员工岗位关联表(EmployeePost)DTO</returns>
    public async Task<TaktEmployeePostDto> UpdateEmployeePostAsync(long id, TaktEmployeePostUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeepostNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PostId, dto.PostId, id, $"员工岗位关联表编码 {dto.PostId} 已存在");

        dto.Adapt(entity, typeof(TaktEmployeePostUpdateDto), typeof(TaktEmployeePost));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetEmployeePostByIdAsync(id)) ?? entity.Adapt<TaktEmployeePostDto>();
    }


    /// <summary>
    /// 删除员工岗位关联表(EmployeePost)
    /// </summary>
    /// <param name="id">员工岗位关联表(EmployeePost)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeePostByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeepostNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除员工岗位关联表(EmployeePost)
    /// </summary>
    /// <param name="ids">员工岗位关联表(EmployeePost)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeePostBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktEmployeePost>();
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
    /// 获取员工岗位关联表(EmployeePost)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetEmployeePostTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEmployeePost));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEmployeePostTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入员工岗位关联表(EmployeePost)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEmployeePostAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktEmployeePost));
        var importData = await TaktExcelHelper.ImportAsync<TaktEmployeePostImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktEmployeePost>();
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
    /// 导出员工岗位关联表(EmployeePost)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEmployeePostAsync(TaktEmployeePostQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEmployeePostQueryDto());
        List<TaktEmployeePost> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployeePost));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEmployeePostExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktEmployeePostExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建员工岗位关联表查询表达式
    /// </summary>
    /// <param name="queryDto">员工岗位关联表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEmployeePost, bool>> QueryExpression(TaktEmployeePostQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktEmployeePost>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (queryDto?.PostId.HasValue == true)
        {
            exp = exp.And(x => x.PostId == queryDto.PostId);
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
