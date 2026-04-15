// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktLeaveService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：请假应用服务（CRUD + 提交流程）。提交时：先落库 TaktLeave → 再调流程 StartAsync(ProcessKey=Leave) → 把返回的 InstanceId 写回 TaktLeave.FlowInstanceId。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using SqlSugar;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Dtos.Workflow;
using Takt.Application.Services;
using Takt.Application.Services.Workflow;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 请假应用服务。提交时匹配流程：ProcessKey=Leave，BusinessKey=请假Id，BusinessType=Leave，并回写 FlowInstanceId。
/// </summary>
public class TaktLeaveService : TaktServiceBase, ITaktLeaveService
{
    private readonly ITaktRepository<TaktLeave> _leaveRepository;
    private readonly ITaktFlowInstanceService _flowInstanceService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="leaveRepository">请假仓储</param>
    /// <param name="flowInstanceService">流程实例服务（用于提交请假流程）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktLeaveService(
        ITaktRepository<TaktLeave> leaveRepository,
        ITaktFlowInstanceService flowInstanceService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _leaveRepository = leaveRepository;
        _flowInstanceService = flowInstanceService;
    }

    /// <summary>
    /// 获取请假列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktLeaveDto>> GetListAsync(TaktLeaveQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _leaveRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktLeaveDto>.Create(
            data.Adapt<List<TaktLeaveDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取请假
    /// </summary>
    /// <param name="id">请假ID</param>
    /// <returns>请假DTO</returns>
    public async Task<TaktLeaveDto?> GetByIdAsync(long id)
    {
        var leave = await _leaveRepository.GetByIdAsync(id);
        if (leave == null)
            return null;
        return leave.Adapt<TaktLeaveDto>();
    }

    /// <summary>
    /// 创建请假（草稿，不发起流程）
    /// </summary>
    /// <param name="dto">创建请假DTO</param>
    /// <returns>请假DTO</returns>
    public async Task<TaktLeaveDto> CreateAsync(TaktLeaveCreateDto dto)
    {
        // 查重验证（同一员工、同一请假类型、同一开始日期仅允许一条，与 Import 去重一致）
        var exists = await _leaveRepository.ExistsAsync(l =>
            l.EmployeeId == dto.EmployeeId &&
            l.LeaveType == dto.LeaveType &&
            l.StartDate == dto.StartDate &&
            l.IsDeleted == 0);
        if (exists)
            throw new TaktLocalizedException("validation.leaveDuplicateForEmployeeDateType", "Frontend", dto.StartDate.ToString("yyyy-MM-dd"), dto.LeaveType);

        var leave = dto.Adapt<TaktLeave>();
        leave.LeaveStatus = 0; // 草稿
        leave = await _leaveRepository.CreateAsync(leave);
        return await GetByIdAsync(leave.Id) ?? leave.Adapt<TaktLeaveDto>();
    }

    /// <summary>
    /// 更新请假
    /// </summary>
    /// <param name="id">请假ID</param>
    /// <param name="dto">更新请假DTO</param>
    /// <returns>请假DTO</returns>
    public async Task<TaktLeaveDto> UpdateAsync(long id, TaktLeaveUpdateDto dto)
    {
        var leave = await _leaveRepository.GetByIdAsync(id);
        if (leave == null)
            throw new TaktBusinessException("validation.leaveRecordNotFound");

        dto.Adapt(leave, typeof(TaktLeaveUpdateDto), typeof(TaktLeave));
        leave.UpdatedAt = DateTime.Now;
        await _leaveRepository.UpdateAsync(leave);
        return await GetByIdAsync(id) ?? leave.Adapt<TaktLeaveDto>();
    }

    /// <summary>
    /// 删除请假
    /// </summary>
    /// <param name="id">请假ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var leave = await _leaveRepository.GetByIdAsync(id);
        if (leave == null)
            throw new TaktBusinessException("validation.leaveRecordNotFound");
        await _leaveRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除请假
    /// </summary>
    /// <param name="ids">请假ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _leaveRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新请假状态（与流程实例同步）
    /// </summary>
    /// <param name="dto">请假状态DTO</param>
    /// <returns>请假DTO</returns>
    public async Task<TaktLeaveDto> UpdateStatusAsync(TaktLeaveStatusDto dto)
    {
        var leave = await _leaveRepository.GetByIdAsync(dto.LeaveId);
        if (leave == null)
            throw new TaktBusinessException("validation.leaveRecordNotFound");

        if (dto.FlowInstanceId.HasValue && leave.FlowInstanceId.HasValue && leave.FlowInstanceId != dto.FlowInstanceId)
            throw new TaktBusinessException("validation.leaveFlowInstanceMismatch");

        leave.LeaveStatus = dto.LeaveStatus;
        leave.UpdatedAt = DateTime.Now;
        await _leaveRepository.UpdateAsync(leave);
        return await GetByIdAsync(leave.Id) ?? leave.Adapt<TaktLeaveDto>();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktLeave));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktLeaveTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }

    /// <summary>
    /// 导入请假数据
    /// </summary>
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktLeave));
            var importData = await TaktExcelHelper.ImportAsync<TaktLeaveImportDto>(
                fileStream,
                excelSheet
            );

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            // 预加载已有（员工+类型+开始日），与本批已添加集合一致（参照 TaktUserService）
            var existingLeaves = await _leaveRepository.FindAsync(l => l.IsDeleted == 0);
            var existingKeys = existingLeaves
                .Select(l => (l.EmployeeId, LeaveType: l.LeaveType ?? string.Empty, l.StartDate.Date))
                .ToHashSet();
            var addedKeys = new HashSet<(long EmployeeId, string LeaveType, DateTime StartDate)>();
            var leavesToInsert = new List<TaktLeave>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 2)))
            {
                try
                {
                    if (item.EmployeeId <= 0)
                    {
                        AddImportError(errors, "validation.importRowLeaveEmployeeIdRequired", index);
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.LeaveType))
                    {
                        AddImportError(errors, "validation.importRowLeaveTypeRequired", index);
                        fail++;
                        continue;
                    }
                    if (item.EndDate < item.StartDate)
                    {
                        AddImportError(errors, "validation.importRowLeaveEndBeforeStart", index);
                        fail++;
                        continue;
                    }

                    var leaveType = item.LeaveType.Trim();
                    var startDate = item.StartDate.Date;
                    var key = (item.EmployeeId, leaveType, startDate);
                    if (existingKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowLeaveDuplicate", index, startDate.ToString("yyyy-MM-dd"), leaveType);
                        fail++;
                        continue;
                    }
                    if (addedKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowLeaveDuplicateInFile", index, startDate.ToString("yyyy-MM-dd"), leaveType);
                        fail++;
                        continue;
                    }

                    var entity = item.Adapt<TaktLeave>();
                    entity.LeaveStatus = 0;
                    leavesToInsert.Add(entity);
                    addedKeys.Add(key);
                }
                catch (TaktBusinessException ex)
                {
                    AddImportError(errors, "validation.importRowUnhandledException", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
                catch (Exception ex)
                {
                    AddImportError(errors, "validation.importRowFailedWithReason", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
            }

            for (var i = 0; i < leavesToInsert.Count; i += importBatchSize)
            {
                var batch = leavesToInsert.Skip(i).Take(importBatchSize).ToList();
                try
                {
                    await _leaveRepository.CreateRangeBulkAsync(batch);
                    success += batch.Count;
                }
                catch (Exception ex)
                {
                    fail += batch.Count;
                    AddImportError(errors, "validation.importBatchInsertFailed", i + 1, i + batch.Count, GetLocalizedExceptionMessage(ex));
                }
            }
        }
        catch (Exception ex)
        {
            AddImportError(errors, "validation.importProcessFailedWithReason", GetLocalizedExceptionMessage(ex));
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出请假
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktLeaveQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktLeaveQueryDto());
        var leaves = await _leaveRepository.FindAsync(predicate);

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktLeave));
        if (leaves == null || leaves.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktLeaveExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = leaves.Adapt<List<TaktLeaveExportDto>>();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }

    /// <summary>
    /// 提交请假：创建请假记录 → 发起流程（ProcessKey=Leave）→ 将实例ID写回请假.FlowInstanceId
    /// </summary>
    public async Task<TaktLeaveSubmitResultDto> SubmitLeaveAsync(TaktLeaveSubmitDto dto)
    {
        var user = GetCurrentUser();
        if (user == null)
            throw new TaktBusinessException("validation.loginRequiredFirst");

        var leave = dto.Adapt<TaktLeave>();
        leave.LeaveStatus = 0;
        leave = await _leaveRepository.CreateAsync(leave);

        var processTitle = !string.IsNullOrWhiteSpace(dto.ProcessTitle)
            ? dto.ProcessTitle
            : $"请假-{leave.LeaveType}-{leave.StartDate:yyyy-MM-dd}";

        var startResult = await _flowInstanceService.StartAsync(new TaktFlowStartDto
        {
            ProcessKey = "Leave",
            BusinessKey = leave.Id.ToString(),
            BusinessType = "Leave",
            ProcessTitle = processTitle,
            FrmData = dto.FrmData
        });

        leave.FlowInstanceId = startResult.InstanceId;
        leave.LeaveStatus = 1; // 审批中
        await _leaveRepository.UpdateAsync(leave);

        return new TaktLeaveSubmitResultDto
        {
            LeaveId = leave.Id,
            FlowInstanceId = startResult.InstanceId,
            InstanceCode = startResult.InstanceCode,
            ProcessKey = startResult.ProcessKey,
            ProcessName = startResult.ProcessName
        };
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktLeave, bool>> QueryExpression(TaktLeaveQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktLeave>();

        // 未删除
        exp = exp.And(x => x.IsDeleted == 0);

        // 员工
        exp = exp.AndIF(queryDto.EmployeeId.HasValue, x => x.EmployeeId == queryDto.EmployeeId!.Value);

        // 请假类型
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto.LeaveType), x => x.LeaveType == queryDto.LeaveType);

        // 请假状态
        exp = exp.AndIF(queryDto.LeaveStatus.HasValue, x => x.LeaveStatus == queryDto.LeaveStatus!.Value);

        // 开始日期范围
        exp = exp.AndIF(queryDto.StartDateFrom.HasValue, x => x.StartDate >= queryDto.StartDateFrom!.Value);
        exp = exp.AndIF(queryDto.StartDateTo.HasValue, x => x.StartDate <= queryDto.StartDateTo!.Value);

        // 关键词（事由、请假类型）
        if (!string.IsNullOrEmpty(queryDto.KeyWords))
        {
            exp = exp.And(x =>
                (x.Reason != null && x.Reason.Contains(queryDto.KeyWords)) ||
                (x.LeaveType != null && x.LeaveType.Contains(queryDto.KeyWords)));
        }

        return exp.ToExpression();
    }
}
