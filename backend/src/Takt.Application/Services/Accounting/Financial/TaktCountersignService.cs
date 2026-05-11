// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：TaktCountersignService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：会签单表应用服务，提供Countersign管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Domain.Entities.Accounting.Financial;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// 会签单表应用服务
/// </summary>
public class TaktCountersignService : TaktServiceBase, ITaktCountersignService
{
    private readonly ITaktRepository<TaktCountersign> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Countersign仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCountersignService(
        ITaktRepository<TaktCountersign> repository,
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
    /// 获取会签单表(Countersign)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktCountersignDto>> GetCountersignListAsync(TaktCountersignQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCountersignDto>.Create(
            data.Adapt<List<TaktCountersignDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取会签单表(Countersign)
    /// </summary>
    /// <param name="id">会签单表(Countersign)ID</param>
    /// <returns>会签单表(Countersign)DTO</returns>
    public async Task<TaktCountersignDto?> GetCountersignByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktCountersignDto>();
    }


    /// <summary>
    /// 获取会签单表(Countersign)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>会签单表(Countersign)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetCountersignOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.CountersignStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.CountersignCode ?? string.Empty,
            DictValue = x.CountersignCode

        }).ToList();
    }


    /// <summary>
    /// 创建会签单表(Countersign)
    /// </summary>
    /// <param name="dto">创建会签单表(Countersign)DTO</param>
    /// <returns>会签单表(Countersign)DTO</returns>
    public async Task<TaktCountersignDto> CreateCountersignAsync(TaktCountersignCreateDto dto)
    {
        var entity = dto.Adapt<TaktCountersign>();
        // 验证CountersignCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CountersignCode, dto.CountersignCode);
        if (!isUnique)
            throw new TaktBusinessException($"会签单表CountersignCode {dto.CountersignCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetCountersignByIdAsync(entity.Id)) ?? entity.Adapt<TaktCountersignDto>();
    }


    /// <summary>
    /// 更新会签单表(Countersign)
    /// </summary>
    /// <param name="id">会签单表(Countersign)ID</param>
    /// <param name="dto">更新会签单表(Countersign)DTO</param>
    /// <returns>会签单表(Countersign)DTO</returns>
    public async Task<TaktCountersignDto> UpdateCountersignAsync(long id, TaktCountersignUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.countersignNotFound");
        // 验证CountersignCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CountersignCode, dto.CountersignCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"会签单表CountersignCode {dto.CountersignCode} 已存在");

        dto.Adapt(entity, typeof(TaktCountersignUpdateDto), typeof(TaktCountersign));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetCountersignByIdAsync(id)) ?? entity.Adapt<TaktCountersignDto>();
    }


    /// <summary>
    /// 删除会签单表(Countersign)
    /// </summary>
    /// <param name="id">会签单表(Countersign)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteCountersignByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.countersignNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.CountersignStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除会签单表(Countersign)
    /// </summary>
    /// <param name="ids">会签单表(Countersign)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteCountersignBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktCountersign>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 CountersignStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.CountersignStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新会签单表(Countersign)状态
    /// </summary>
    /// <param name="dto">会签单表(Countersign)状态DTO</param>
    /// <returns>会签单表(Countersign)DTO</returns>
    public async Task<TaktCountersignDto> UpdateCountersignStatusAsync(TaktCountersignStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CountersignId);
        if (entity == null)
            throw new TaktBusinessException("validation.countersignNotFound");
        entity.CountersignStatus = dto.CountersignStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCountersignByIdAsync(entity.Id) ?? entity.Adapt<TaktCountersignDto>();
    }


    /// <summary>
    /// 获取会签单表(Countersign)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetCountersignTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktCountersign));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktCountersignTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入会签单表(Countersign)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportCountersignAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktCountersign));
        var importData = await TaktExcelHelper.ImportAsync<TaktCountersignImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktCountersign>();
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
    /// 导出会签单表(Countersign)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportCountersignAsync(TaktCountersignQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktCountersignQueryDto());
        List<TaktCountersign> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktCountersign));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktCountersignExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktCountersignExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建会签单表查询表达式
    /// </summary>
    /// <param name="queryDto">会签单表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktCountersign, bool>> QueryExpression(TaktCountersignQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktCountersign>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CompanyCode!.Contains(queryDto.KeyWords) ||
                x.CountersignCode!.Contains(queryDto.KeyWords) ||
                x.CountersignDepts!.Contains(queryDto.KeyWords) ||
                x.FinanceDept!.Contains(queryDto.KeyWords) ||
                x.BudgetReviewComment!.Contains(queryDto.KeyWords) ||
                x.ExecutiveOffice!.Contains(queryDto.KeyWords) ||
                x.ApplicantBy!.Contains(queryDto.KeyWords) ||
                x.ApplicationDept!.Contains(queryDto.KeyWords) ||
                x.CostBearerDept!.Contains(queryDto.KeyWords) ||
                x.BudgetItem!.Contains(queryDto.KeyWords) ||
                x.CountersignTitle!.Contains(queryDto.KeyWords) ||
                x.ApplicationReason!.Contains(queryDto.KeyWords) ||
                x.BudgetUsageDescription!.Contains(queryDto.KeyWords) ||
                x.TargetAndExpectedBenefit!.Contains(queryDto.KeyWords) ||
                x.Attachments!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyCode))
        {
            exp = exp.And(x => x.CompanyCode!.Contains(queryDto.CompanyCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.CountersignCode))
        {
            exp = exp.And(x => x.CountersignCode!.Contains(queryDto.CountersignCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.CountersignDepts))
        {
            exp = exp.And(x => x.CountersignDepts!.Contains(queryDto.CountersignDepts));
        }

        if (!string.IsNullOrEmpty(queryDto?.FinanceDept))
        {
            exp = exp.And(x => x.FinanceDept!.Contains(queryDto.FinanceDept));
        }

        if (!string.IsNullOrEmpty(queryDto?.BudgetReviewComment))
        {
            exp = exp.And(x => x.BudgetReviewComment!.Contains(queryDto.BudgetReviewComment));
        }

        if (!string.IsNullOrEmpty(queryDto?.ExecutiveOffice))
        {
            exp = exp.And(x => x.ExecutiveOffice!.Contains(queryDto.ExecutiveOffice));
        }

        if (queryDto?.ApprovalDate.HasValue == true)
        {
            exp = exp.And(x => x.ApprovalDate == queryDto.ApprovalDate);
        }

        if (queryDto?.ApplicationDate.HasValue == true)
        {
            exp = exp.And(x => x.ApplicationDate == queryDto.ApplicationDate);
        }

        if (queryDto?.ApplicantId.HasValue == true)
        {
            exp = exp.And(x => x.ApplicantId == queryDto.ApplicantId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicantBy))
        {
            exp = exp.And(x => x.ApplicantBy!.Contains(queryDto.ApplicantBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicationDept))
        {
            exp = exp.And(x => x.ApplicationDept!.Contains(queryDto.ApplicationDept));
        }

        if (!string.IsNullOrEmpty(queryDto?.CostBearerDept))
        {
            exp = exp.And(x => x.CostBearerDept!.Contains(queryDto.CostBearerDept));
        }

        if (queryDto?.IsBudget.HasValue == true)
        {
            exp = exp.And(x => x.IsBudget == queryDto.IsBudget);
        }

        if (!string.IsNullOrEmpty(queryDto?.BudgetItem))
        {
            exp = exp.And(x => x.BudgetItem!.Contains(queryDto.BudgetItem));
        }

        if (queryDto?.BudgetAmount.HasValue == true)
        {
            exp = exp.And(x => x.BudgetAmount == queryDto.BudgetAmount);
        }

        if (queryDto?.ApplicationAmount.HasValue == true)
        {
            exp = exp.And(x => x.ApplicationAmount == queryDto.ApplicationAmount);
        }

        if (!string.IsNullOrEmpty(queryDto?.CountersignTitle))
        {
            exp = exp.And(x => x.CountersignTitle!.Contains(queryDto.CountersignTitle));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicationReason))
        {
            exp = exp.And(x => x.ApplicationReason!.Contains(queryDto.ApplicationReason));
        }

        if (!string.IsNullOrEmpty(queryDto?.BudgetUsageDescription))
        {
            exp = exp.And(x => x.BudgetUsageDescription!.Contains(queryDto.BudgetUsageDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.TargetAndExpectedBenefit))
        {
            exp = exp.And(x => x.TargetAndExpectedBenefit!.Contains(queryDto.TargetAndExpectedBenefit));
        }

        if (!string.IsNullOrEmpty(queryDto?.Attachments))
        {
            exp = exp.And(x => x.Attachments!.Contains(queryDto.Attachments));
        }

        if (queryDto?.FlowInstanceId.HasValue == true)
        {
            exp = exp.And(x => x.FlowInstanceId == queryDto.FlowInstanceId);
        }

        if (queryDto?.CountersignStatus.HasValue == true)
        {
            exp = exp.And(x => x.CountersignStatus == queryDto.CountersignStatus);
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

        // ApprovalDate 日期范围查询
        if (queryDto?.ApprovalDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ApprovalDate >= queryDto.ApprovalDateStart);
        }
        if (queryDto?.ApprovalDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ApprovalDate <= queryDto.ApprovalDateEnd);
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
