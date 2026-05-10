// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.Dict.DictEngine
// 文件名称：TaktDictEngineService.cs
// 创建时间：2026-05-08
// 创建人：Takt365(Qoder AI)
// 功能描述：字典引擎服务实现，提供字典数据查询的核心引擎能力
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Tasks.Dict;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.Dict.DictEngine;

/// <summary>
/// 字典引擎服务实现
/// </summary>
public class TaktDictEngineService : TaktServiceBase, ITaktDictEngineService
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
    public TaktDictEngineService(
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
    /// 获取字典数据选项列表（用于下拉框等）
    /// </summary>
    /// <param name="dictTypeCode">字典类型编码</param>
    /// <returns>字典数据选项列表</returns>
    public async Task<List<TaktSelectOption>> GetDictOptionsAsync(string? dictTypeCode = null)
    {
        LogInformation($"[TaktDictEngineService] GetDictOptionsAsync 开始执行，dictTypeCode: {dictTypeCode ?? "(null)"}");
        
        var result = new List<TaktSelectOption>();

        // 如果指定了字典类型编码，需要检查数据源类型
        if (!string.IsNullOrWhiteSpace(dictTypeCode))
        {
            // 获取字典类型信息
            var dictType = await _dictTypeRepository.GetAsync(dt => dt.DictTypeCode == dictTypeCode && dt.IsDeleted == 0 && dt.DictTypeStatus == 0);
            
            if (dictType == null)
            {
                // 字典类型不存在，返回空列表
                LogWarning($"[TaktDictEngineService] 字典类型不存在，dictTypeCode: {dictTypeCode}");
                return result;
            }

            LogInformation($"[TaktDictEngineService] 找到字典类型，dictTypeCode: {dictTypeCode}, DataSource: {dictType.DataSource}, SqlScript: {(string.IsNullOrWhiteSpace(dictType.SqlScript) ? "(null)" : "已设置")}");

            // 根据数据源类型处理
            if (dictType.DataSource == 1 && !string.IsNullOrWhiteSpace(dictType.SqlScript))
            {
                // 数据源为SQL查询，通过仓储执行SQL脚本
                // 使用 DataConfigId 指定的数据库连接执行SQL（用于跨库查询）
                LogInformation($"[TaktDictEngineService] 使用 SQL 查询数据源，dictTypeCode: {dictTypeCode}");
                result = await _dictDataRepository.GetSelectOptionsFromSqlAsync(
                    dictType.SqlScript, 
                    dictTypeCode, 
                    null,  // entityType
                    dictType.DataConfigId  // configId：指定在哪个数据库连接上执行SQL
                );
                LogInformation($"[TaktDictEngineService] SQL 查询完成，返回 {result.Count} 个选项");
            }
            else
            {
                // 数据源为系统表，从字典数据表查询（必须完整返回所有字段）
                LogInformation($"[TaktDictEngineService] 使用系统表数据源，dictTypeCode: {dictTypeCode}");
                var dictDataList = await _dictDataRepository.FindAsync(d => d.DictTypeCode == dictTypeCode && d.IsDeleted == 0);
                
                LogInformation($"[TaktDictEngineService] 从数据库查询到 {dictDataList.Count} 条原始数据");
                
                // 输出原始数据的详细信息（只输出第1条以清楚展示字典数据的关系）
                if (dictDataList.Count > 0)
                {
                    var item = dictDataList.First();
                    LogInformation($"[TaktDictEngineService] 原始数据 - Id: {item.Id}, DictLabel: {item.DictLabel}, DictValue: {item.DictValue}, ExtLabel: {item.ExtLabel}, ExtValue: {item.ExtValue}");
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
                
                LogInformation($"[TaktDictEngineService] 映射完成，返回 {result.Count} 个选项");
                
                // 输出映射后的结果详细信息（只输出第1条以清楚展示字典数据的关系）
                if (result.Count > 0)
                {
                    var option = result.First();
                    LogInformation($"[TaktDictEngineService] 映射结果 - DictLabel: {option.DictLabel}, DictValue: {option.DictValue}, ExtLabel: {option.ExtLabel}, ExtValue: {option.ExtValue}");
                }
            }
        }
        else
        {
            // 未指定字典类型编码，获取所有字典数据（只处理系统表数据源，必须完整返回所有字段）
            // 注意：批量加载时，使用 DictL10nKey 临时存储 DictTypeCode 用于前端分组，保持 ExtLabel 和 ExtValue 为原始值
            LogInformation($"[TaktDictEngineService] 未指定 dictTypeCode，获取所有字典数据（批量加载模式）");
            var dictDataList = await _dictDataRepository.FindAsync(d => d.IsDeleted == 0);
            
            LogInformation($"[TaktDictEngineService] 从数据库查询到 {dictDataList.Count} 条原始数据");
            
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
            
            LogInformation($"[TaktDictEngineService] 映射完成，返回 {result.Count} 个选项，分为 {dictDataList.Select(d => d.DictTypeCode).Distinct().Count()} 个字典类型");
        }

        LogInformation($"[TaktDictEngineService] GetDictOptionsAsync 执行完成，最终返回 {result.Count} 个选项");
        return result;
    }
}
