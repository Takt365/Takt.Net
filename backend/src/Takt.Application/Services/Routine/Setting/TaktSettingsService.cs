// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Setting
// 文件名称：TaktSettingsService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt设置应用服务，提供设置管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Setting;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Setting;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Setting;

/// <summary>
/// Takt设置应用服务
/// </summary>
public class TaktSettingsService : TaktServiceBase, ITaktSettingsService
{
    private readonly ITaktRepository<TaktSettings> _settingsRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="settingsRepository">设置仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSettingsService(
        ITaktRepository<TaktSettings> settingsRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _settingsRepository = settingsRepository;
    }

    /// <summary>
    /// 获取设置列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSettingsDto>> GetListAsync(TaktSettingsQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _settingsRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSettingsDto>.Create(
            data.Adapt<List<TaktSettingsDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取设置
    /// </summary>
    /// <param name="id">设置ID</param>
    /// <returns>设置DTO</returns>
    public async Task<TaktSettingsDto?> GetByIdAsync(long id)
    {
        var settings = await _settingsRepository.GetByIdAsync(id);
        if (settings == null) return null;

        return settings.Adapt<TaktSettingsDto>();
    }

    /// <summary>
    /// 根据设置键获取设置
    /// </summary>
    /// <param name="settingKey">设置键</param>
    /// <returns>设置DTO</returns>
    public async Task<TaktSettingsDto?> GetByKeyAsync(string settingKey)
    {
        var settings = await _settingsRepository.GetAsync(s => s.SettingKey == settingKey);
        if (settings == null) return null;

        return settings.Adapt<TaktSettingsDto>();
    }

    /// <summary>
    /// 获取设置选项列表（用于下拉框等）
    /// </summary>
    /// <returns>设置选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var settings = await _settingsRepository.FindAsync(s => s.IsDeleted == 0 && s.SettingStatus == 0);
        return settings
            .OrderBy(s => s.OrderNum)
            .ThenBy(s => s.CreateTime)
            .Select(s => new TaktSelectOption
            {
                DictLabel = s.SettingName ?? s.SettingKey,
                DictValue = s.Id,
                ExtLabel = s.SettingKey,
                ExtValue = s.SettingGroup ?? string.Empty,
                OrderNum = s.OrderNum
            })
            .ToList();
    }

    /// <summary>
    /// 创建设置
    /// </summary>
    /// <param name="dto">创建设置DTO</param>
    /// <returns>设置DTO</returns>
    public async Task<TaktSettingsDto> CreateAsync(TaktSettingsCreateDto dto)
    {
        // 查重验证
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_settingsRepository, s => s.SettingKey, dto.SettingKey, null, $"设置键 {dto.SettingKey} 已存在");
        if (!string.IsNullOrWhiteSpace(dto.SettingValue))
        {
            await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_settingsRepository, s => s.SettingValue, dto.SettingValue, null, $"设置值 {dto.SettingValue} 已存在");
        }
        if (!string.IsNullOrWhiteSpace(dto.SettingName))
        {
            await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_settingsRepository, s => s.SettingName, dto.SettingName, null, $"设置名称 {dto.SettingName} 已存在");
        }
        if (!string.IsNullOrWhiteSpace(dto.SettingGroup))
        {
            await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_settingsRepository, s => s.SettingGroup, dto.SettingGroup, null, $"设置分组 {dto.SettingGroup} 已存在");
        }

        // 使用Mapster映射DTO到实体，然后手动设置状态
        var settings = dto.Adapt<TaktSettings>();
        settings.SettingStatus = 0; // 0=启用

        settings = await _settingsRepository.CreateAsync(settings);

        return settings.Adapt<TaktSettingsDto>();
    }

    /// <summary>
    /// 更新设置
    /// </summary>
    /// <param name="id">设置ID</param>
    /// <param name="dto">更新设置DTO</param>
    /// <returns>设置DTO</returns>
    public async Task<TaktSettingsDto> UpdateAsync(long id, TaktSettingsUpdateDto dto)
    {
        var settings = await _settingsRepository.GetByIdAsync(id);
        if (settings == null)
            throw new TaktBusinessException("设置不存在");

        // 查重验证（排除当前记录）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_settingsRepository, s => s.SettingKey, dto.SettingKey, id, $"设置键 {dto.SettingKey} 已存在");
        if (!string.IsNullOrWhiteSpace(dto.SettingValue))
        {
            await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_settingsRepository, s => s.SettingValue, dto.SettingValue, id, $"设置值 {dto.SettingValue} 已存在");
        }
        if (!string.IsNullOrWhiteSpace(dto.SettingName))
        {
            await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_settingsRepository, s => s.SettingName, dto.SettingName, id, $"设置名称 {dto.SettingName} 已存在");
        }
        if (!string.IsNullOrWhiteSpace(dto.SettingGroup))
        {
            await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_settingsRepository, s => s.SettingGroup, dto.SettingGroup, id, $"设置分组 {dto.SettingGroup} 已存在");
        }

        // 使用Mapster更新实体
        dto.Adapt(settings, typeof(TaktSettingsUpdateDto), typeof(TaktSettings));
        settings.UpdateTime = DateTime.Now;

        await _settingsRepository.UpdateAsync(settings);

        return settings.Adapt<TaktSettingsDto>();
    }

    /// <summary>
    /// 删除设置
    /// </summary>
    /// <param name="id">设置ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var settings = await _settingsRepository.GetByIdAsync(id);
        if (settings == null)
            throw new TaktBusinessException("设置不存在");

        // 1. 先将 SettingStatus 置为禁用（1），再软删除（IsDeleted=1）
        settings.SettingStatus = 1;
        settings.UpdateTime = DateTime.Now;
        await _settingsRepository.UpdateAsync(settings);

        // 2. 软删除设置（IsDeleted = 1）
        await _settingsRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除设置
    /// </summary>
    /// <param name="ids">设置ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有设置记录
        var settingsList = await _settingsRepository.FindAsync(s => idList.Contains(s.Id));

        // 1. 先将所有记录的 SettingStatus 置为禁用（1），再软删除（IsDeleted=1）
        foreach (var settings in settingsList)
        {
            settings.SettingStatus = 1;
            settings.UpdateTime = DateTime.Now;
            await _settingsRepository.UpdateAsync(settings);
        }

        // 2. 批量软删除设置（IsDeleted = 1）
        await _settingsRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新设置状态
    /// </summary>
    /// <param name="dto">设置状态DTO</param>
    /// <returns>设置DTO</returns>
    public async Task<TaktSettingsDto> UpdateStatusAsync(TaktSettingsStatusDto dto)
    {
        var settings = await _settingsRepository.GetByIdAsync(dto.SettingId);
        if (settings == null)
            throw new TaktBusinessException("设置不存在");

        settings.SettingStatus = dto.SettingStatus;
        settings.UpdateTime = DateTime.Now;

        await _settingsRepository.UpdateAsync(settings);

        return settings.Adapt<TaktSettingsDto>();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSettingsTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "设置导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "设置导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入设置
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktSettingsImportDto>(
                fileStream, 
                string.IsNullOrWhiteSpace(sheetName) ? "设置导入模板" : sheetName
            );

            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }

            // 批量处理导入数据
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3))) // 第3行开始是数据
            {
                try
                {
                    // 验证必填字段
                    if (string.IsNullOrWhiteSpace(item.SettingKey))
                    {
                        errors.Add($"第{index}行：设置键不能为空");
                        fail++;
                        continue;
                    }

                    // 导入时使用验证器手动验证（SettingKey、SettingValue、SettingName、SettingGroup 任意一个重复都报错）
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_settingsRepository, s => s.SettingKey, item.SettingKey, null, $"第{index}行：设置键 {item.SettingKey} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_settingsRepository, s => s.SettingValue, item.SettingValue, null, $"第{index}行：设置值 {item.SettingValue} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_settingsRepository, s => s.SettingName, item.SettingName, null, $"第{index}行：设置名称 {item.SettingName} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_settingsRepository, s => s.SettingGroup, item.SettingGroup, null, $"第{index}行：设置分组 {item.SettingGroup} 已存在");

                    // 创建设置实体
                    var settings = new TaktSettings
                    {
                        SettingKey = item.SettingKey,
                        SettingValue = string.IsNullOrWhiteSpace(item.SettingValue) ? null : item.SettingValue,
                        SettingName = string.IsNullOrWhiteSpace(item.SettingName) ? null : item.SettingName,
                        SettingGroup = string.IsNullOrWhiteSpace(item.SettingGroup) ? null : item.SettingGroup,
                        IsBuiltIn = item.IsBuiltIn >= 0 ? item.IsBuiltIn : 1,
                        IsEncrypted = item.IsEncrypted >= 0 ? item.IsEncrypted : 1,
                        OrderNum = item.OrderNum,
                        SettingStatus = item.SettingStatus >= 0 ? item.SettingStatus : 0, // 默认为启用（0=启用）
                        Remark = item.Remark
                    };

                    // 保存设置
                    await _settingsRepository.CreateAsync(settings);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            errors.Add($"导入过程发生错误：{ex.Message}");
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出设置
    /// </summary>
    /// <param name="query">设置查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktSettingsQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的设置（不分页）
        List<TaktSettings> settingsList;
        if (predicate != null)
        {
            settingsList = await _settingsRepository.FindAsync(predicate);
        }
        else
        {
            settingsList = await _settingsRepository.GetAllAsync();
        }

        if (settingsList == null || settingsList.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSettingsExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "设置数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "设置导出" : fileName
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = settingsList.Select(s =>
        {
            var dto = s.Adapt<TaktSettingsExportDto>();
            // 处理需要特殊转换的字段
            dto.SettingValue = s.SettingValue ?? string.Empty;
            dto.SettingName = s.SettingName ?? string.Empty;
            dto.SettingGroup = s.SettingGroup ?? string.Empty;
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "设置数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "设置导出" : fileName
        );
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSettings, bool>> QueryExpression(TaktSettingsQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktSettings>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.SettingKey.Contains(queryDto.KeyWords) ||
                              (x.SettingName != null && x.SettingName.Contains(queryDto.KeyWords)));
        }

        // 设置键
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.SettingKey), x => x.SettingKey.Contains(queryDto!.SettingKey!));

        // 设置组
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.SettingGroup), x => x.SettingGroup != null && x.SettingGroup.Contains(queryDto!.SettingGroup!));

        // 设置状态
        exp = exp.AndIF(queryDto?.SettingStatus.HasValue == true, x => x.SettingStatus == queryDto!.SettingStatus!.Value);

        return exp.ToExpression();
    }
}