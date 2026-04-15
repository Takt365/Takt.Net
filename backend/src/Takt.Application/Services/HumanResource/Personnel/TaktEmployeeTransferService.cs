// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：TaktEmployeeTransferService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工调动应用服务（转岗/调岗 CRUD + 状态更新 + 导出，与流程审批关联）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
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
/// 员工调动应用服务（转岗/调岗，与流程审批关联；流程回调时调用 UpdateEmployeeTransferStatusAsync）
/// </summary>
public class TaktEmployeeTransferService : TaktServiceBase, ITaktEmployeeTransferService
{
    private readonly ITaktRepository<TaktEmployeeTransfer> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">员工调动仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEmployeeTransferService(
        ITaktRepository<TaktEmployeeTransfer> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取员工调动列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEmployeeTransferDto>> GetEmployeeTransferListAsync(TaktEmployeeTransferQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEmployeeTransferDto>.Create(
            data.Adapt<List<TaktEmployeeTransferDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取员工调动
    /// </summary>
    /// <param name="id">员工调动ID</param>
    /// <returns>员工调动DTO</returns>
    public async Task<TaktEmployeeTransferDto?> GetEmployeeTransferByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return null;
        return entity.Adapt<TaktEmployeeTransferDto>();
    }

    /// <summary>
    /// 创建员工调动（草稿，不发起流程）
    /// </summary>
    /// <param name="dto">创建员工调动DTO</param>
    /// <returns>员工调动DTO</returns>
    public async Task<TaktEmployeeTransferDto> CreateEmployeeTransferAsync(TaktEmployeeTransferCreateDto dto)
    {
        // 去重：员工ID+原部门ID+原岗位ID+目标部门ID+目标岗位ID 组合唯一
        var employeeId = dto.EmployeeId;
        var fromDeptId = dto.FromDeptId;
        var fromPostId = dto.FromPostId;
        var toDeptId = dto.ToDeptId;
        var toPostId = dto.ToPostId;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.EmployeeId == employeeId && x.FromDeptId == fromDeptId && x.FromPostId == fromPostId && x.ToDeptId == toDeptId && x.ToPostId == toPostId,
            null,
            "员工ID+原部门ID+原岗位ID+目标部门ID+目标岗位ID组合已存在");
        var entity = dto.Adapt<TaktEmployeeTransfer>();
        entity.TransferStatus = 0; // 草稿
        entity = await _repository.CreateAsync(entity);
        return await GetEmployeeTransferByIdAsync(entity.Id) ?? entity.Adapt<TaktEmployeeTransferDto>();
    }

    /// <summary>
    /// 更新员工调动
    /// </summary>
    /// <param name="id">员工调动ID</param>
    /// <param name="dto">更新员工调动DTO</param>
    /// <returns>员工调动DTO</returns>
    public async Task<TaktEmployeeTransferDto> UpdateEmployeeTransferAsync(long id, TaktEmployeeTransferUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeTransferNotFound");

        // 去重（排除当前记录）：员工ID+原部门ID+原岗位ID+目标部门ID+目标岗位ID 组合唯一
        var employeeId = dto.EmployeeId;
        var fromDeptId = dto.FromDeptId;
        var fromPostId = dto.FromPostId;
        var toDeptId = dto.ToDeptId;
        var toPostId = dto.ToPostId;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.EmployeeId == employeeId && x.FromDeptId == fromDeptId && x.FromPostId == fromPostId && x.ToDeptId == toDeptId && x.ToPostId == toPostId,
            id,
            "员工ID+原部门ID+原岗位ID+目标部门ID+目标岗位ID组合已存在");

        dto.Adapt(entity, typeof(TaktEmployeeTransferUpdateDto), typeof(TaktEmployeeTransfer));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetEmployeeTransferByIdAsync(id) ?? entity.Adapt<TaktEmployeeTransferDto>();
    }

    /// <summary>
    /// 删除员工调动
    /// </summary>
    /// <param name="id">员工调动ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeTransferByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeTransferNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除员工调动
    /// </summary>
    /// <param name="ids">员工调动ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeTransferBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新员工调动状态（流程回调时更新 TransferStatus）
    /// </summary>
    /// <param name="dto">员工调动状态DTO</param>
    /// <returns>员工调动DTO</returns>
    public async Task<TaktEmployeeTransferDto> UpdateEmployeeTransferStatusAsync(TaktEmployeeTransferStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.TransferId);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeTransferNotFound");

        entity.TransferStatus = dto.TransferStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetEmployeeTransferByIdAsync(entity.Id) ?? entity.Adapt<TaktEmployeeTransferDto>();
    }

    /// <summary>
    /// 导出员工调动
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEmployeeTransferAsync(TaktEmployeeTransferQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEmployeeTransferQueryDto());

        List<TaktEmployeeTransfer> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployeeTransfer));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEmployeeTransferExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Adapt<List<TaktEmployeeTransferExportDto>>();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEmployeeTransfer, bool>> QueryExpression(TaktEmployeeTransferQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktEmployeeTransfer>();

        // 未删除
        exp = exp.And(x => x.IsDeleted == 0);

        // 员工
        exp = exp.AndIF(queryDto.EmployeeId.HasValue, x => x.EmployeeId == queryDto.EmployeeId!.Value);

        // 调动类型、状态
        exp = exp.AndIF(queryDto.TransferType.HasValue, x => x.TransferType == queryDto.TransferType!.Value);
        exp = exp.AndIF(queryDto.TransferStatus.HasValue, x => x.TransferStatus == queryDto.TransferStatus!.Value);

        // 生效日期范围
        exp = exp.AndIF(queryDto.EffectiveDateFrom.HasValue, x => x.EffectiveDate >= queryDto.EffectiveDateFrom!.Value);
        exp = exp.AndIF(queryDto.EffectiveDateTo.HasValue, x => x.EffectiveDate <= queryDto.EffectiveDateTo!.Value);

        return exp.ToExpression();
    }
}
