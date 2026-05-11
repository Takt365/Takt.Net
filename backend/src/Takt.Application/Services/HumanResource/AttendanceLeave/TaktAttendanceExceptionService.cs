// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceExceptionService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤异常表应用服务，提供AttendanceException管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤异常表应用服务
/// </summary>
public class TaktAttendanceExceptionService : TaktServiceBase, ITaktAttendanceExceptionService
{
    private readonly ITaktRepository<TaktAttendanceException> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">AttendanceException仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceExceptionService(
        ITaktRepository<TaktAttendanceException> repository,
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
    /// 获取考勤异常表(AttendanceException)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAttendanceExceptionDto>> GetAttendanceExceptionListAsync(TaktAttendanceExceptionQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAttendanceExceptionDto>.Create(
            data.Adapt<List<TaktAttendanceExceptionDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取考勤异常表(AttendanceException)
    /// </summary>
    /// <param name="id">考勤异常表(AttendanceException)ID</param>
    /// <returns>考勤异常表(AttendanceException)DTO</returns>
    public async Task<TaktAttendanceExceptionDto?> GetAttendanceExceptionByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAttendanceExceptionDto>();
    }


    /// <summary>
    /// 获取考勤异常表(AttendanceException)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>考勤异常表(AttendanceException)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetAttendanceExceptionOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.HandleStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Summary ?? string.Empty,
            DictValue = x.Summary

        }).ToList();
    }


    /// <summary>
    /// 创建考勤异常表(AttendanceException)
    /// </summary>
    /// <param name="dto">创建考勤异常表(AttendanceException)DTO</param>
    /// <returns>考勤异常表(AttendanceException)DTO</returns>
    public async Task<TaktAttendanceExceptionDto> CreateAttendanceExceptionAsync(TaktAttendanceExceptionCreateDto dto)
    {
        var entity = dto.Adapt<TaktAttendanceException>();
        entity = await _repository.CreateAsync(entity);
        return (await GetAttendanceExceptionByIdAsync(entity.Id)) ?? entity.Adapt<TaktAttendanceExceptionDto>();
    }


    /// <summary>
    /// 更新考勤异常表(AttendanceException)
    /// </summary>
    /// <param name="id">考勤异常表(AttendanceException)ID</param>
    /// <param name="dto">更新考勤异常表(AttendanceException)DTO</param>
    /// <returns>考勤异常表(AttendanceException)DTO</returns>
    public async Task<TaktAttendanceExceptionDto> UpdateAttendanceExceptionAsync(long id, TaktAttendanceExceptionUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceexceptionNotFound");
        dto.Adapt(entity, typeof(TaktAttendanceExceptionUpdateDto), typeof(TaktAttendanceException));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetAttendanceExceptionByIdAsync(id)) ?? entity.Adapt<TaktAttendanceExceptionDto>();
    }


    /// <summary>
    /// 删除考勤异常表(AttendanceException)
    /// </summary>
    /// <param name="id">考勤异常表(AttendanceException)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAttendanceExceptionByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceexceptionNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.HandleStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除考勤异常表(AttendanceException)
    /// </summary>
    /// <param name="ids">考勤异常表(AttendanceException)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAttendanceExceptionBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktAttendanceException>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 HandleStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.HandleStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新考勤异常表(AttendanceException)状态
    /// </summary>
    /// <param name="dto">考勤异常表(AttendanceException)状态DTO</param>
    /// <returns>考勤异常表(AttendanceException)DTO</returns>
    public async Task<TaktAttendanceExceptionDto> UpdateAttendanceExceptionHandleStatusAsync(TaktAttendanceExceptionHandleStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.AttendanceExceptionId);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceexceptionNotFound");
        entity.HandleStatus = dto.HandleStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAttendanceExceptionByIdAsync(entity.Id) ?? entity.Adapt<TaktAttendanceExceptionDto>();
    }


    /// <summary>
    /// 获取考勤异常表(AttendanceException)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetAttendanceExceptionTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAttendanceException));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAttendanceExceptionTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入考勤异常表(AttendanceException)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAttendanceExceptionAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktAttendanceException));
        var importData = await TaktExcelHelper.ImportAsync<TaktAttendanceExceptionImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktAttendanceException>();
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
    /// 导出考勤异常表(AttendanceException)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAttendanceExceptionAsync(TaktAttendanceExceptionQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAttendanceExceptionQueryDto());
        List<TaktAttendanceException> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAttendanceException));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAttendanceExceptionExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktAttendanceExceptionExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建考勤异常表查询表达式
    /// </summary>
    /// <param name="queryDto">考勤异常表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktAttendanceException, bool>> QueryExpression(TaktAttendanceExceptionQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAttendanceException>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.Summary!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (queryDto?.ExceptionDate.HasValue == true)
        {
            exp = exp.And(x => x.ExceptionDate == queryDto.ExceptionDate);
        }

        if (queryDto?.ExceptionType.HasValue == true)
        {
            exp = exp.And(x => x.ExceptionType == queryDto.ExceptionType);
        }

        if (!string.IsNullOrEmpty(queryDto?.Summary))
        {
            exp = exp.And(x => x.Summary!.Contains(queryDto.Summary));
        }

        if (queryDto?.HandleStatus.HasValue == true)
        {
            exp = exp.And(x => x.HandleStatus == queryDto.HandleStatus);
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

        // ExceptionDate 日期范围查询
        if (queryDto?.ExceptionDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ExceptionDate >= queryDto.ExceptionDateStart);
        }
        if (queryDto?.ExceptionDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ExceptionDate <= queryDto.ExceptionDateEnd);
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
