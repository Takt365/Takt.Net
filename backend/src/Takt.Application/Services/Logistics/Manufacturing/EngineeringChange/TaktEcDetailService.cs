// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDetailService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：设变明细表应用服务，提供EcDetail管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange;

namespace Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变明细表应用服务
/// </summary>
public class TaktEcDetailService : TaktServiceBase, ITaktEcDetailService
{
    private readonly ITaktRepository<TaktEcDetail> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktEcDept> _ecDeptRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">EcDetail仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="ecDeptRepository">EcDept仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEcDetailService(
        ITaktRepository<TaktEcDetail> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktEcDept> ecDeptRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _ecDeptRepository = ecDeptRepository;
    }


    /// <summary>
    /// 获取设变明细表(EcDetail)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEcDetailDto>> GetEcDetailListAsync(TaktEcDetailQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEcDetailDto>.Create(
            data.Adapt<List<TaktEcDetailDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取设变明细表(EcDetail)
    /// </summary>
    /// <param name="id">设变明细表(EcDetail)ID</param>
    /// <returns>设变明细表(EcDetail)DTO</returns>
    public async Task<TaktEcDetailDto?> GetEcDetailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktEcDetailDto>();
        
        // 手动加载子表
        dto.DeptRecords = (await _ecDeptRepository.FindAsync(x => x.EcnDetailId == id && x.IsDeleted == 0))
            .Adapt<List<TaktEcDeptDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取设变明细表(EcDetail)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>设变明细表(EcDetail)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEcDetailOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.EcnNo ?? string.Empty,
            DictValue = x.EcnNo

        }).ToList();
    }


    /// <summary>
    /// 创建设变明细表(EcDetail)
    /// </summary>
    /// <param name="dto">创建设变明细表(EcDetail)DTO</param>
    /// <returns>设变明细表(EcDetail)DTO</returns>
    public async Task<TaktEcDetailDto> CreateEcDetailAsync(TaktEcDetailCreateDto dto)
    {
        var entity = dto.Adapt<TaktEcDetail>();
        // 验证EcnNo的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.EcnNo, dto.EcnNo);
        if (!isUnique)
            throw new TaktBusinessException($"设变明细表EcnNo {dto.EcnNo} 已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建EcDept列表
            if (dto.DeptRecords != null && dto.DeptRecords.Count > 0)
            {
                var ecDeptList = dto.DeptRecords.Select(x => {
                    var childEntity = x.Adapt<TaktEcDept>();
                    childEntity.EcnDetailId = entity.Id;
                    return childEntity;
                }).ToList();
                await _ecDeptRepository.CreateRangeBulkAsync(ecDeptList);
            }
        }

        return (await GetEcDetailByIdAsync(entity.Id)) ?? entity.Adapt<TaktEcDetailDto>();
    }


    /// <summary>
    /// 更新设变明细表(EcDetail)
    /// </summary>
    /// <param name="id">设变明细表(EcDetail)ID</param>
    /// <param name="dto">更新设变明细表(EcDetail)DTO</param>
    /// <returns>设变明细表(EcDetail)DTO</returns>
    public async Task<TaktEcDetailDto> UpdateEcDetailAsync(long id, TaktEcDetailUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ecdetailNotFound");
        // 验证EcnNo的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.EcnNo, dto.EcnNo, id);
        if (!isUnique)
            throw new TaktBusinessException($"设变明细表EcnNo {dto.EcnNo} 已存在");

        dto.Adapt(entity, typeof(TaktEcDetailUpdateDto), typeof(TaktEcDetail));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的EcDept列表
        var oldEcDepts = await _ecDeptRepository.FindAsync(x => x.EcnDetailId == id && x.IsDeleted == 0);
        if (oldEcDepts != null && oldEcDepts.Count > 0)
        {
            foreach (var oldEcDept in oldEcDepts)
            {
                oldEcDept.IsDeleted = 1;
            }
            await _ecDeptRepository.UpdateRangeBulkAsync(oldEcDepts);
        }

        // 创建新的EcDept列表
        if (dto.DeptRecords != null && dto.DeptRecords.Count > 0)
        {
            var ecDeptList = dto.DeptRecords.Select(x => {
                var childEntity = x.Adapt<TaktEcDept>();
                childEntity.EcnDetailId = id;
                return childEntity;
            }).ToList();
            await _ecDeptRepository.CreateRangeBulkAsync(ecDeptList);
        }


        return (await GetEcDetailByIdAsync(id)) ?? entity.Adapt<TaktEcDetailDto>();
    }


    /// <summary>
    /// 删除设变明细表(EcDetail)
    /// </summary>
    /// <param name="id">设变明细表(EcDetail)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEcDetailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ecdetailNotFound");
        
        // 级联删除子表数据
        // 级联删除EcDept列表
        var ecDepts = await _ecDeptRepository.FindAsync(x => x.EcnDetailId == id && x.IsDeleted == 0);
        if (ecDepts != null && ecDepts.Count > 0)
        {
            foreach (var ecDept in ecDepts)
            {
                ecDept.IsDeleted = 1;
            }
            await _ecDeptRepository.UpdateRangeBulkAsync(ecDepts);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除设变明细表(EcDetail)
    /// </summary>
    /// <param name="ids">设变明细表(EcDetail)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEcDetailBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktEcDetail>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除EcDept列表
        var ecDeptsToDelete = new List<TaktEcDept>();
        foreach (var id in idList)
        {
            var ecDepts = await _ecDeptRepository.FindAsync(x => x.EcnDetailId == id && x.IsDeleted == 0);
            if (ecDepts != null && ecDepts.Count > 0)
            {
                ecDeptsToDelete.AddRange(ecDepts);
            }
        }
        
        if (ecDeptsToDelete.Count > 0)
        {
            foreach (var ecDept in ecDeptsToDelete)
            {
                ecDept.IsDeleted = 1;
            }
            await _ecDeptRepository.UpdateRangeBulkAsync(ecDeptsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取设变明细表(EcDetail)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetEcDetailTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEcDetail));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEcDetailTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入设变明细表(EcDetail)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEcDetailAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktEcDetail));
        var importData = await TaktExcelHelper.ImportAsync<TaktEcDetailImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktEcDetail>();
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
    /// 导出设变明细表(EcDetail)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEcDetailAsync(TaktEcDetailQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEcDetailQueryDto());
        List<TaktEcDetail> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEcDetail));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEcDetailExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktEcDetailExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建设变明细表查询表达式
    /// </summary>
    /// <param name="queryDto">设变明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEcDetail, bool>> QueryExpression(TaktEcDetailQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktEcDetail>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.EcnNo!.Contains(queryDto.KeyWords) ||
                x.EcnModel!.Contains(queryDto.KeyWords) ||
                x.EcnBomItem!.Contains(queryDto.KeyWords) ||
                x.EcnBomSubItem!.Contains(queryDto.KeyWords) ||
                x.EcnBomNo!.Contains(queryDto.KeyWords) ||
                x.EcnChange!.Contains(queryDto.KeyWords) ||
                x.EcnLocal!.Contains(queryDto.KeyWords) ||
                x.EcnNote!.Contains(queryDto.KeyWords) ||
                x.EcnProcess!.Contains(queryDto.KeyWords) ||
                x.EcnOldItem!.Contains(queryDto.KeyWords) ||
                x.EcnOldText!.Contains(queryDto.KeyWords) ||
                x.EcnOldSet!.Contains(queryDto.KeyWords) ||
                x.EcnNewItem!.Contains(queryDto.KeyWords) ||
                x.EcnNewText!.Contains(queryDto.KeyWords) ||
                x.EcnNewSet!.Contains(queryDto.KeyWords) ||
                x.EcnWarehouse!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EcnId.HasValue == true)
        {
            exp = exp.And(x => x.EcnId == queryDto.EcnId);
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnNo))
        {
            exp = exp.And(x => x.EcnNo!.Contains(queryDto.EcnNo));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnModel))
        {
            exp = exp.And(x => x.EcnModel!.Contains(queryDto.EcnModel));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnBomItem))
        {
            exp = exp.And(x => x.EcnBomItem!.Contains(queryDto.EcnBomItem));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnBomSubItem))
        {
            exp = exp.And(x => x.EcnBomSubItem!.Contains(queryDto.EcnBomSubItem));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnBomNo))
        {
            exp = exp.And(x => x.EcnBomNo!.Contains(queryDto.EcnBomNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnChange))
        {
            exp = exp.And(x => x.EcnChange!.Contains(queryDto.EcnChange));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnLocal))
        {
            exp = exp.And(x => x.EcnLocal!.Contains(queryDto.EcnLocal));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnNote))
        {
            exp = exp.And(x => x.EcnNote!.Contains(queryDto.EcnNote));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnProcess))
        {
            exp = exp.And(x => x.EcnProcess!.Contains(queryDto.EcnProcess));
        }

        if (queryDto?.EcnBomDate.HasValue == true)
        {
            exp = exp.And(x => x.EcnBomDate == queryDto.EcnBomDate);
        }

        if (queryDto?.EcnEntryDate.HasValue == true)
        {
            exp = exp.And(x => x.EcnEntryDate == queryDto.EcnEntryDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnOldItem))
        {
            exp = exp.And(x => x.EcnOldItem!.Contains(queryDto.EcnOldItem));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnOldText))
        {
            exp = exp.And(x => x.EcnOldText!.Contains(queryDto.EcnOldText));
        }

        if (queryDto?.EcnOldQty.HasValue == true)
        {
            exp = exp.And(x => x.EcnOldQty == queryDto.EcnOldQty);
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnOldSet))
        {
            exp = exp.And(x => x.EcnOldSet!.Contains(queryDto.EcnOldSet));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnNewItem))
        {
            exp = exp.And(x => x.EcnNewItem!.Contains(queryDto.EcnNewItem));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnNewText))
        {
            exp = exp.And(x => x.EcnNewText!.Contains(queryDto.EcnNewText));
        }

        if (queryDto?.EcnNewQty.HasValue == true)
        {
            exp = exp.And(x => x.EcnNewQty == queryDto.EcnNewQty);
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnNewSet))
        {
            exp = exp.And(x => x.EcnNewSet!.Contains(queryDto.EcnNewSet));
        }

        if (queryDto?.IsProcurement.HasValue == true)
        {
            exp = exp.And(x => x.IsProcurement == queryDto.IsProcurement);
        }

        if (queryDto?.IsCheck.HasValue == true)
        {
            exp = exp.And(x => x.IsCheck == queryDto.IsCheck);
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnWarehouse))
        {
            exp = exp.And(x => x.EcnWarehouse!.Contains(queryDto.EcnWarehouse));
        }

        if (queryDto?.IsEndOfLine.HasValue == true)
        {
            exp = exp.And(x => x.IsEndOfLine == queryDto.IsEndOfLine);
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

        // EcnBomDate 日期范围查询
        if (queryDto?.EcnBomDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EcnBomDate >= queryDto.EcnBomDateStart);
        }
        if (queryDto?.EcnBomDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EcnBomDate <= queryDto.EcnBomDateEnd);
        }

        // EcnEntryDate 日期范围查询
        if (queryDto?.EcnEntryDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EcnEntryDate >= queryDto.EcnEntryDateStart);
        }
        if (queryDto?.EcnEntryDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EcnEntryDate <= queryDto.EcnEntryDateEnd);
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
