// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Dict
// 文件名称：TaktDictDataService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字典数据应用服务，提供字典数据管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Tasks.Dict;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Tasks.Dict;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.Dict;

/// <summary>
/// Takt字典数据应用服务
/// </summary>
public class TaktDictDataService : TaktServiceBase, ITaktDictDataService
{
    private readonly ITaktRepository<TaktDictData> _dictDataRepository;
    private readonly ITaktRepository<TaktDictType> _dictTypeRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktDictDataService(
        ITaktRepository<TaktDictData> dictDataRepository,
        ITaktRepository<TaktDictType> dictTypeRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _dictDataRepository = dictDataRepository;
        _dictTypeRepository = dictTypeRepository;
    }

    /// <summary>
    /// 获取字典数据列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktDictDataDto>> GetDictDataListAsync(TaktDictDataQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _dictDataRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktDictDataDto>.Create(
            data.Adapt<List<TaktDictDataDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <returns>字典数据DTO</returns>
    public async Task<TaktDictDataDto?> GetDictDataByIdAsync(long id)
    {
        var dictData = await _dictDataRepository.GetByIdAsync(id);
        if (dictData == null) return null;

        return dictData.Adapt<TaktDictDataDto>();
    }

    /// <summary>
    /// 获取字典数据选项列表（用于下拉框等）
    /// </summary>
    /// <param name="dictTypeCode">字典类型编码</param>
    /// <returns>字典数据选项列表</returns>
    public async Task<List<TaktSelectOption>> GetDictDataOptionsAsync(string? dictTypeCode = null)
    {
        LogInformation($"[TaktDictDataService] GetDictDataOptionsAsync 开始执行，dictTypeCode: {dictTypeCode ?? "(null)"}");
        
        var result = new List<TaktSelectOption>();

        // 如果指定了字典类型编码，需要检查数据源类型
        if (!string.IsNullOrWhiteSpace(dictTypeCode))
        {
            // 获取字典类型信息
            var dictType = await _dictTypeRepository.GetAsync(dt => dt.DictTypeCode == dictTypeCode && dt.IsDeleted == 0 && dt.DictTypeStatus == 0);
            
            if (dictType == null)
            {
                // 字典类型不存在，返回空列表
                LogWarning($"[TaktDictDataService] 字典类型不存在，dictTypeCode: {dictTypeCode}");
                return result;
            }

            LogInformation($"[TaktDictDataService] 找到字典类型，dictTypeCode: {dictTypeCode}, DataSource: {dictType.DataSource}, SqlScript: {(string.IsNullOrWhiteSpace(dictType.SqlScript) ? "(null)" : "已设置")}");

            // 根据数据源类型处理
            if (dictType.DataSource == 1 && !string.IsNullOrWhiteSpace(dictType.SqlScript))
            {
                // 数据源为SQL查询，通过仓储执行SQL脚本
                // 使用 DataConfigId 指定的数据库连接执行SQL（用于跨库查询）
                LogInformation($"[TaktDictDataService] 使用 SQL 查询数据源，dictTypeCode: {dictTypeCode}");
                result = await _dictTypeRepository.GetSelectOptionsFromSqlAsync(
                    dictType.SqlScript, 
                    dictTypeCode, 
                    null,  // entityType
                    dictType.DataConfigId  // configId：指定在哪个数据库连接上执行SQL
                );
                LogInformation($"[TaktDictDataService] SQL 查询完成，返回 {result.Count} 个选项");
            }
            else
            {
                // 数据源为系统表，从字典数据表查询（必须完整返回所有字段）
                LogInformation($"[TaktDictDataService] 使用系统表数据源，dictTypeCode: {dictTypeCode}");
                var dictDataList = await _dictDataRepository.FindAsync(d => d.DictTypeCode == dictTypeCode && d.IsDeleted == 0);
                
                LogInformation($"[TaktDictDataService] 从数据库查询到 {dictDataList.Count} 条原始数据");
                
                // 输出原始数据的详细信息（只输出第1条以清楚展示字典数据的关系）
                if (dictDataList.Count > 0)
                {
                    var item = dictDataList.First();
                    LogInformation($"[TaktDictDataService] 原始数据 - Id: {item.Id}, DictLabel: {item.DictLabel}, DictValue: {item.DictValue}, ExtLabel: {item.ExtLabel}, ExtValue: {item.ExtValue}");
                }
                
                result = dictDataList
                    .OrderBy(d => d.SortOrder)
                    .ThenBy(d => d.CreatedAt)
                    .Select(d => new TaktSelectOption
                    {
                        DictLabel = d.DictLabel,
                        DictValue = d.DictValue,  // 字典值（如 "0", "1", "2"）
                        ExtLabel = d.ExtLabel,  // 扩展标签
                        ExtValue = d.ExtValue,  // 扩展值
                        DictL10nKey = d.DictL10nKey,
                        CssClass = d.CssClass,
                        ListClass = d.ListClass,
                        SortOrder = d.SortOrder
                    })
                    .ToList();
                
                LogInformation($"[TaktDictDataService] 映射完成，返回 {result.Count} 个选项");
                
                // 输出映射后的结果详细信息（只输出第1条以清楚展示字典数据的关系）
                if (result.Count > 0)
                {
                    var option = result.First();
                    LogInformation($"[TaktDictDataService] 映射结果 - DictLabel: {option.DictLabel}, DictValue: {option.DictValue}, ExtLabel: {option.ExtLabel}, ExtValue: {option.ExtValue}");
                }
            }
        }
        else
        {
            // 未指定字典类型编码，获取所有字典数据（只处理系统表数据源，必须完整返回所有字段）
            // 注意：批量加载时，使用 DictL10nKey 临时存储 DictTypeCode 用于前端分组，保持 ExtLabel 和 ExtValue 为原始值
            LogInformation($"[TaktDictDataService] 未指定 dictTypeCode，获取所有字典数据（批量加载模式）");
            var dictDataList = await _dictDataRepository.FindAsync(d => d.IsDeleted == 0);
            
            LogInformation($"[TaktDictDataService] 从数据库查询到 {dictDataList.Count} 条原始数据");
            
            result = dictDataList
                .OrderBy(d => d.DictTypeCode)
                .ThenBy(d => d.SortOrder)
                .ThenBy(d => d.CreatedAt)
                .Select(d => new TaktSelectOption
                {
                    DictLabel = d.DictLabel,
                    DictValue = d.DictValue,  // 字典值（如 "0", "1", "2"）
                    ExtLabel = d.ExtLabel,  // 扩展标签：保持原始值
                    ExtValue = d.ExtValue,  // 扩展值：保持原始值
                    DictL10nKey = d.DictL10nKey,  // 字典本地化键：保持原始值
                    DictTypeCode = d.DictTypeCode,  // 字典类型编码：用于前端按字典类型分组
                    CssClass = d.CssClass,
                    ListClass = d.ListClass,
                    SortOrder = d.SortOrder
                })
                .ToList();
            
            LogInformation($"[TaktDictDataService] 映射完成，返回 {result.Count} 个选项，分为 {dictDataList.Select(d => d.DictTypeCode).Distinct().Count()} 个字典类型");
        }

        LogInformation($"[TaktDictDataService] GetDictDataOptionsAsync 执行完成，最终返回 {result.Count} 个选项");
        return result;
    }


    /// <summary>
    /// 创建字典数据
    /// </summary>
    /// <param name="dto">创建字典数据DTO</param>
    /// <returns>字典数据DTO</returns>
    public async Task<TaktDictDataDto> CreateDictDataAsync(TaktDictDataCreateDto dto)
    {
        // 根据 DictTypeCode 查找 DictTypeId
        var dictType = await _dictTypeRepository.GetAsync(dt => dt.DictTypeCode == dto.DictTypeCode && dt.IsDeleted == 0);
        if (dictType == null)
            throw new TaktLocalizedException("validation.dictTypeNotFoundByCode", "Frontend", dto.DictTypeCode);

        // 查重：字典类型ID+字典类型编码+字典标签 组合唯一
        var dictTypeId = dictType.Id;
        var dictTypeCode = dto.DictTypeCode ?? string.Empty;
        var dictLabel = dto.DictLabel ?? string.Empty;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _dictDataRepository,
            d => d.DictTypeId == dictTypeId && (d.DictTypeCode ?? "") == dictTypeCode && (d.DictLabel ?? "") == dictLabel,
            null,
            "字典类型ID+字典类型编码+字典标签组合已存在");

        // 使用Mapster映射DTO到实体
        var dictData = dto.Adapt<TaktDictData>();
        
        // 设置外键 DictTypeId
        dictData.DictTypeId = dictType.Id;

        dictData = await _dictDataRepository.CreateAsync(dictData);

        return dictData.Adapt<TaktDictDataDto>();
    }

    /// <summary>
    /// 更新字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <param name="dto">更新字典数据DTO</param>
    /// <returns>字典数据DTO</returns>
    public async Task<TaktDictDataDto> UpdateDictDataAsync(long id, TaktDictDataUpdateDto dto)
    {
        var dictData = await _dictDataRepository.GetByIdAsync(id);
        if (dictData == null)
            throw new TaktBusinessException("validation.dictDataNotFound");

        // 根据 DictTypeCode 查找 DictTypeId（如果 DictTypeCode 有变化）
        if (dictData.DictTypeCode != dto.DictTypeCode)
        {
            var dictType = await _dictTypeRepository.GetAsync(dt => dt.DictTypeCode == dto.DictTypeCode && dt.IsDeleted == 0);
            if (dictType == null)
                throw new TaktLocalizedException("validation.dictTypeNotFoundByCode", "Frontend", dto.DictTypeCode);
            dictData.DictTypeId = dictType.Id;
        }

        // 查重（排除当前记录）：字典类型ID+字典类型编码+字典标签 组合唯一
        var dictTypeId = dictData.DictTypeId;
        var dictTypeCode = dto.DictTypeCode ?? string.Empty;
        var dictLabel = dto.DictLabel ?? string.Empty;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _dictDataRepository,
            d => d.DictTypeId == dictTypeId && (d.DictTypeCode ?? "") == dictTypeCode && (d.DictLabel ?? "") == dictLabel,
            id,
            "字典类型ID+字典类型编码+字典标签组合已存在");

        // 使用Mapster更新实体
        dto.Adapt(dictData, typeof(TaktDictDataUpdateDto), typeof(TaktDictData));
        dictData.UpdatedAt = DateTime.Now;

        await _dictDataRepository.UpdateAsync(dictData);

        return dictData.Adapt<TaktDictDataDto>();
    }

    /// <summary>
    /// 删除字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <returns>任务</returns>
    public async Task DeleteDictDataByIdAsync(long id)
    {
        await _dictDataRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除字典数据
    /// </summary>
    /// <param name="ids">字典数据ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteDictDataBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 批量软删除字典数据（IsDeleted = 1）
        await _dictDataRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetDictDataTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktDictData));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktDictDataTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }

    /// <summary>
    /// 导入字典数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportDictDataAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktDictData));
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktDictDataImportDto>(
                fileStream,
                excelSheet
            );

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var existingDictData = await _dictDataRepository.FindAsync(d => d.IsDeleted == 0);
            var existingKeys = existingDictData
                .Where(d => !string.IsNullOrWhiteSpace(d.DictTypeCode) && !string.IsNullOrWhiteSpace(d.DictLabel))
                .Select(d => (d.DictTypeId, (d.DictTypeCode ?? "").Trim().ToUpperInvariant(), (d.DictLabel ?? "").Trim().ToUpperInvariant()))
                .ToHashSet();
            var addedKeys = new HashSet<(long, string, string)>();

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.DictTypeCode))
                    {
                        AddImportError(errors, "validation.importRowDictDataTypeCodeRequired", index);
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.DictLabel))
                    {
                        AddImportError(errors, "validation.importRowDictDataLabelRequired", index);
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.DictValue))
                    {
                        AddImportError(errors, "validation.importRowDictDataValueRequired", index);
                        fail++;
                        continue;
                    }

                    // 根据 DictTypeCode 查找 DictTypeId
                    var dictType = await _dictTypeRepository.GetAsync(dt => dt.DictTypeCode == item.DictTypeCode && dt.IsDeleted == 0);
                    if (dictType == null)
                    {
                        AddImportError(errors, "validation.importRowDictDataTypeNotFound", index, item.DictTypeCode);
                        fail++;
                        continue;
                    }

                    var dictTypeId = dictType.Id;
                    var dictTypeCodeVal = item.DictTypeCode.Trim().ToUpperInvariant();
                    var dictLabelVal = item.DictLabel.Trim().ToUpperInvariant();
                    var key = (dictTypeId, dictTypeCodeVal, dictLabelVal);
                    if (existingKeys.Contains(key) || addedKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowDictDataDuplicateComposite", index);
                        fail++;
                        continue;
                    }

                    // 创建字典数据实体
                    var dictData = new TaktDictData
                    {
                        DictTypeId = dictType.Id,
                        DictTypeCode = item.DictTypeCode,
                        DictLabel = item.DictLabel,
                        DictL10nKey = string.IsNullOrWhiteSpace(item.DictL10nKey) ? null : item.DictL10nKey,
                        DictValue = item.DictValue,
                        CssClass = item.CssClass >= 0 ? item.CssClass : 0,
                        ListClass = item.ListClass >= 0 ? item.ListClass : 0,
                        ExtLabel = string.IsNullOrWhiteSpace(item.ExtLabel) ? null : item.ExtLabel,
                        ExtValue = string.IsNullOrWhiteSpace(item.ExtValue) ? null : item.ExtValue,
                        SortOrder = item.SortOrder,
                        Remark = item.Remark
                    };

                    await _dictDataRepository.CreateAsync(dictData);
                    addedKeys.Add(key);
                    success++;
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
        }
        catch (Exception ex)
        {
            AddImportError(errors, "validation.importProcessFailedWithReason", GetLocalizedExceptionMessage(ex));
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出字典数据
    /// </summary>
    /// <param name="query">字典数据查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportDictDataAsync(TaktDictDataQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的字典数据（不分页）
        List<TaktDictData> dictDataList;
        if (predicate != null)
        {
            dictDataList = await _dictDataRepository.FindAsync(predicate);
        }
        else
        {
            dictDataList = await _dictDataRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktDictData));
        if (dictDataList == null || dictDataList.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktDictDataExportDto>(),
                excelSheet,
                excelFile
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = dictDataList.Select(d =>
        {
            var dto = d.Adapt<TaktDictDataExportDto>();
            // 处理需要特殊转换的字段
            dto.ExtLabel = d.ExtLabel ?? string.Empty;
            dto.ExtValue = d.ExtValue ?? string.Empty;
            return dto;
        }).ToList();

        // 导出Excel
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
    private Expression<Func<TaktDictData, bool>> QueryExpression(TaktDictDataQueryDto queryDto)
    {
        // 调试日志：输出查询参数
        TaktLogger.Information("========== [TaktDictDataService.QueryExpression] 开始构建查询表达式 ==========");
        TaktLogger.Information("[查询参数] PageIndex: {PageIndex}, PageSize: {PageSize}", 
            queryDto?.PageIndex ?? 0, queryDto?.PageSize ?? 0);
        TaktLogger.Information("[查询参数] KeyWords: {KeyWords}", 
            string.IsNullOrEmpty(queryDto?.KeyWords) ? "(null)" : queryDto.KeyWords);
        
        // 处理 DictTypeId：前端传递的是字符串，需要正确转换
        long? dictTypeIdValue = null;
        if (queryDto?.DictTypeId.HasValue == true)
        {
            dictTypeIdValue = queryDto.DictTypeId.Value;
            TaktLogger.Information("[查询参数] DictTypeId (已转换): {DictTypeId}", dictTypeIdValue);
        }
        else
        {
            TaktLogger.Information("[查询参数] DictTypeId: (null) - HasValue: {HasValue}, 值: {Value}", 
                queryDto?.DictTypeId.HasValue ?? false, queryDto?.DictTypeId?.ToString() ?? "(null)");
        }
        
        TaktLogger.Information("[查询参数] DictTypeCode: {DictTypeCode}", 
            string.IsNullOrEmpty(queryDto?.DictTypeCode) ? "(null)" : queryDto.DictTypeCode);
        TaktLogger.Information("[查询参数] DictLabel: {DictLabel}", 
            string.IsNullOrEmpty(queryDto?.DictLabel) ? "(null)" : queryDto.DictLabel);
        TaktLogger.Information("[查询参数] DictValue: {DictValue}", 
            string.IsNullOrEmpty(queryDto?.DictValue) ? "(null)" : queryDto.DictValue);
        TaktLogger.Information("================================================================");

        var exp = Expressionable.Create<TaktDictData>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            TaktLogger.Information("[查询条件] 添加关键词查询: {KeyWords}", queryDto.KeyWords);
            exp = exp.And(x => x.DictLabel.Contains(queryDto.KeyWords) ||
                              x.DictValue.Contains(queryDto.KeyWords));
        }

        // 字典类型ID（优先使用，因为它是唯一标识）
        // 注意：前端传递的是字符串，通过 JsonConverter 转换为 long?，这里直接使用转换后的值
        if (dictTypeIdValue.HasValue)
        {
            var idValue = dictTypeIdValue.Value; // 避免闭包问题
            TaktLogger.Information("[查询条件] 添加字典类型ID查询: {DictTypeId}", idValue);
            exp = exp.And(x => x.DictTypeId == idValue);
        }

        // 字典类型编码
        if (!string.IsNullOrEmpty(queryDto?.DictTypeCode))
        {
            TaktLogger.Information("[查询条件] 添加字典类型编码查询: {DictTypeCode}", queryDto.DictTypeCode);
        }
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DictTypeCode), x => x.DictTypeCode == queryDto!.DictTypeCode!);

        // 字典标签
        if (!string.IsNullOrEmpty(queryDto?.DictLabel))
        {
            TaktLogger.Information("[查询条件] 添加字典标签模糊查询: {DictLabel}", queryDto.DictLabel);
        }
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DictLabel), x => x.DictLabel.Contains(queryDto!.DictLabel!));

        // 字典值
        if (!string.IsNullOrEmpty(queryDto?.DictValue))
        {
            TaktLogger.Information("[查询条件] 添加字典值模糊查询: {DictValue}", queryDto.DictValue);
        }
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DictValue), x => x.DictValue.Contains(queryDto!.DictValue!));

        TaktLogger.Information("[查询表达式] 查询表达式构建完成");
        TaktLogger.Information("================================================================");

        return exp.ToExpression();
    }
}