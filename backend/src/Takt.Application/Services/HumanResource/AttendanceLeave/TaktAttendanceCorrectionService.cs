// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceCorrectionService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：补卡记录表应用服务，提供AttendanceCorrection管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 补卡记录表应用服务
/// </summary>
public class TaktAttendanceCorrectionService : TaktServiceBase, ITaktAttendanceCorrectionService
{
    private readonly ITaktRepository<TaktAttendanceCorrection> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">AttendanceCorrection仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceCorrectionService(
        ITaktRepository<TaktAttendanceCorrection> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取补卡记录表(AttendanceCorrection)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAttendanceCorrectionDto>> GetAttendanceCorrectionListAsync(TaktAttendanceCorrectionQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAttendanceCorrectionDto>.Create(
            data.Adapt<List<TaktAttendanceCorrectionDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取补卡记录表(AttendanceCorrection)
    /// </summary>
    /// <param name="id">补卡记录表(AttendanceCorrection)ID</param>
    /// <returns>补卡记录表(AttendanceCorrection)DTO</returns>
    public async Task<TaktAttendanceCorrectionDto?> GetAttendanceCorrectionByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAttendanceCorrectionDto>();
    }


    /// <summary>
    /// 获取补卡记录表(AttendanceCorrection)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>补卡记录表(AttendanceCorrection)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetAttendanceCorrectionOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.ApprovalStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Reason ?? string.Empty,
            DictValue = x.Reason

        }).ToList();
    }


    /// <summary>
    /// 创建补卡记录表(AttendanceCorrection)
    /// </summary>
    /// <param name="dto">创建补卡记录表(AttendanceCorrection)DTO</param>
    /// <returns>补卡记录表(AttendanceCorrection)DTO</returns>
    public async Task<TaktAttendanceCorrectionDto> CreateAttendanceCorrectionAsync(TaktAttendanceCorrectionCreateDto dto)
    {
        var entity = dto.Adapt<TaktAttendanceCorrection>();
        entity = await _repository.CreateAsync(entity);
        return (await GetAttendanceCorrectionByIdAsync(entity.Id)) ?? entity.Adapt<TaktAttendanceCorrectionDto>();
    }


    /// <summary>
    /// 更新补卡记录表(AttendanceCorrection)
    /// </summary>
    /// <param name="id">补卡记录表(AttendanceCorrection)ID</param>
    /// <param name="dto">更新补卡记录表(AttendanceCorrection)DTO</param>
    /// <returns>补卡记录表(AttendanceCorrection)DTO</returns>
    public async Task<TaktAttendanceCorrectionDto> UpdateAttendanceCorrectionAsync(long id, TaktAttendanceCorrectionUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendancecorrectionNotFound");

        dto.Adapt(entity, typeof(TaktAttendanceCorrectionUpdateDto), typeof(TaktAttendanceCorrection));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetAttendanceCorrectionByIdAsync(id)) ?? entity.Adapt<TaktAttendanceCorrectionDto>();
    }


    /// <summary>
    /// 删除补卡记录表(AttendanceCorrection)
    /// </summary>
    /// <param name="id">补卡记录表(AttendanceCorrection)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAttendanceCorrectionByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendancecorrectionNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.ApprovalStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除补卡记录表(AttendanceCorrection)
    /// </summary>
    /// <param name="ids">补卡记录表(AttendanceCorrection)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAttendanceCorrectionBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktAttendanceCorrection>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 ApprovalStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.ApprovalStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新补卡记录表(AttendanceCorrection)状态
    /// </summary>
    /// <param name="dto">补卡记录表(AttendanceCorrection)状态DTO</param>
    /// <returns>补卡记录表(AttendanceCorrection)DTO</returns>
    public async Task<TaktAttendanceCorrectionDto> UpdateAttendanceCorrectionApprovalStatusAsync(TaktAttendanceCorrectionApprovalStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.AttendanceCorrectionId);
        if (entity == null)
            throw new TaktBusinessException("validation.attendancecorrectionNotFound");
        entity.ApprovalStatus = dto.ApprovalStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAttendanceCorrectionByIdAsync(entity.Id) ?? entity.Adapt<TaktAttendanceCorrectionDto>();
    }


    /// <summary>
    /// 获取补卡记录表(AttendanceCorrection)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetAttendanceCorrectionTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAttendanceCorrection));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAttendanceCorrectionTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入补卡记录表(AttendanceCorrection)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAttendanceCorrectionAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktAttendanceCorrection));
        var importData = await TaktExcelHelper.ImportAsync<TaktAttendanceCorrectionImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktAttendanceCorrection>();
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
    /// 导出补卡记录表(AttendanceCorrection)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAttendanceCorrectionAsync(TaktAttendanceCorrectionQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAttendanceCorrectionQueryDto());
        List<TaktAttendanceCorrection> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAttendanceCorrection));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAttendanceCorrectionExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktAttendanceCorrectionExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建补卡记录表查询表达式
    /// </summary>
    /// <param name="queryDto">补卡记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktAttendanceCorrection, bool>> QueryExpression(TaktAttendanceCorrectionQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAttendanceCorrection>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ApplicantBy!.Contains(queryDto.KeyWords) ||
                x.DeptName!.Contains(queryDto.KeyWords) ||
                x.Reason!.Contains(queryDto.KeyWords) ||
                x.ApproverBy!.Contains(queryDto.KeyWords) ||
                x.ApproveComment!.Contains(queryDto.KeyWords) ||
                x.HandlingBy!.Contains(queryDto.KeyWords) ||
                x.HandlingComment!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (queryDto?.ApplicantId.HasValue == true)
        {
            exp = exp.And(x => x.ApplicantId == queryDto.ApplicantId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicantBy))
        {
            exp = exp.And(x => x.ApplicantBy!.Contains(queryDto.ApplicantBy));
        }

        if (queryDto?.ApplicationDate.HasValue == true)
        {
            exp = exp.And(x => x.ApplicationDate == queryDto.ApplicationDate);
        }

        if (queryDto?.DeptId.HasValue == true)
        {
            exp = exp.And(x => x.DeptId == queryDto.DeptId);
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptName))
        {
            exp = exp.And(x => x.DeptName!.Contains(queryDto.DeptName));
        }

        if (queryDto?.TargetDate.HasValue == true)
        {
            exp = exp.And(x => x.TargetDate == queryDto.TargetDate);
        }

        if (queryDto?.CorrectionKind.HasValue == true)
        {
            exp = exp.And(x => x.CorrectionKind == queryDto.CorrectionKind);
        }

        if (queryDto?.RequestPunchTime.HasValue == true)
        {
            exp = exp.And(x => x.RequestPunchTime == queryDto.RequestPunchTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.Reason))
        {
            exp = exp.And(x => x.Reason!.Contains(queryDto.Reason));
        }

        if (queryDto?.ApproverId.HasValue == true)
        {
            exp = exp.And(x => x.ApproverId == queryDto.ApproverId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApproverBy))
        {
            exp = exp.And(x => x.ApproverBy!.Contains(queryDto.ApproverBy));
        }

        if (queryDto?.ApproveTime.HasValue == true)
        {
            exp = exp.And(x => x.ApproveTime == queryDto.ApproveTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApproveComment))
        {
            exp = exp.And(x => x.ApproveComment!.Contains(queryDto.ApproveComment));
        }

        if (queryDto?.HandlingId.HasValue == true)
        {
            exp = exp.And(x => x.HandlingId == queryDto.HandlingId);
        }

        if (!string.IsNullOrEmpty(queryDto?.HandlingBy))
        {
            exp = exp.And(x => x.HandlingBy!.Contains(queryDto.HandlingBy));
        }

        if (queryDto?.HandlingTime.HasValue == true)
        {
            exp = exp.And(x => x.HandlingTime == queryDto.HandlingTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.HandlingComment))
        {
            exp = exp.And(x => x.HandlingComment!.Contains(queryDto.HandlingComment));
        }

        if (queryDto?.FlowInstanceId.HasValue == true)
        {
            exp = exp.And(x => x.FlowInstanceId == queryDto.FlowInstanceId);
        }

        if (queryDto?.ApprovalStatus.HasValue == true)
        {
            exp = exp.And(x => x.ApprovalStatus == queryDto.ApprovalStatus);
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

        // ApplicationDate 日期范围查询
        if (queryDto?.ApplicationDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ApplicationDate >= queryDto.ApplicationDateStart);
        }
        if (queryDto?.ApplicationDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ApplicationDate <= queryDto.ApplicationDateEnd);
        }

        // TargetDate 日期范围查询
        if (queryDto?.TargetDateStart.HasValue == true)
        {
            exp = exp.And(x => x.TargetDate >= queryDto.TargetDateStart);
        }
        if (queryDto?.TargetDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.TargetDate <= queryDto.TargetDateEnd);
        }

        // RequestPunchTime 日期范围查询
        if (queryDto?.RequestPunchTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.RequestPunchTime >= queryDto.RequestPunchTimeStart);
        }
        if (queryDto?.RequestPunchTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.RequestPunchTime <= queryDto.RequestPunchTimeEnd);
        }

        // ApproveTime 日期范围查询
        if (queryDto?.ApproveTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ApproveTime >= queryDto.ApproveTimeStart);
        }
        if (queryDto?.ApproveTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ApproveTime <= queryDto.ApproveTimeEnd);
        }

        // HandlingTime 日期范围查询
        if (queryDto?.HandlingTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.HandlingTime >= queryDto.HandlingTimeStart);
        }
        if (queryDto?.HandlingTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.HandlingTime <= queryDto.HandlingTimeEnd);
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
