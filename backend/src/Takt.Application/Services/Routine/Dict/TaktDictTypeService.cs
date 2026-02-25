// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Dict
// 文件名称：TaktDictTypeService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字典类型应用服务，提供字典类型管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Dict;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Dict;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Dict;

/// <summary>
/// Takt字典类型应用服务
/// </summary>
public class TaktDictTypeService : TaktServiceBase, ITaktDictTypeService
{
    private readonly ITaktRepository<TaktDictType> _dictTypeRepository;
    private readonly ITaktRepository<TaktDictData> _dictDataRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktDictTypeService(
        ITaktRepository<TaktDictType> dictTypeRepository,
        ITaktRepository<TaktDictData> dictDataRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _dictTypeRepository = dictTypeRepository;
        _dictDataRepository = dictDataRepository;
    }

    /// <summary>
    /// 获取字典类型列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktDictTypeDto>> GetListAsync(TaktDictTypeQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _dictTypeRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktDictTypeDto>.Create(
            data.Adapt<List<TaktDictTypeDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取字典类型
    /// </summary>
    /// <param name="id">字典类型ID</param>
    /// <returns>字典类型DTO</returns>
    public async Task<TaktDictTypeDto?> GetByIdAsync(long id)
    {
        var dictType = await _dictTypeRepository.GetByIdAsync(id);
        if (dictType == null) return null;

        var dictTypeDto = dictType.Adapt<TaktDictTypeDto>();
        
        // 加载子表数据（字典数据列表）
        await LoadDictDataListAsync(new List<TaktDictTypeDto> { dictTypeDto }, new List<long> { id });

        return dictTypeDto;
    }

    /// <summary>
    /// 获取字典类型选项列表（用于下拉框等）
    /// </summary>
    /// <returns>字典类型选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var dictTypes = await _dictTypeRepository.FindAsync(d => d.IsDeleted == 0 && d.DictTypeStatus == 0);
        return dictTypes
            .OrderBy(d => d.OrderNum)
            .ThenBy(d => d.CreateTime)
            .Select(d => new TaktSelectOption
            {
                DictLabel = d.DictTypeName,
                DictValue = d.Id,
                ExtLabel = d.DictTypeCode,
                OrderNum = d.OrderNum
            })
            .ToList();
    }

    /// <summary>
    /// 创建字典类型
    /// </summary>
    /// <param name="dto">创建字典类型DTO</param>
    /// <returns>字典类型DTO</returns>
    public async Task<TaktDictTypeDto> CreateAsync(TaktDictTypeCreateDto dto)
    {
        // 验证：如果数据源为SQL查询（DataSource = 1），字典类型编码必须以 sql_ 开头，且必须提供SQL脚本
        if (dto.DataSource == 1)
        {
            if (string.IsNullOrWhiteSpace(dto.DictTypeCode))
            {
                throw new TaktBusinessException("数据源为SQL查询时，字典类型编码不能为空");
            }
            if (!dto.DictTypeCode.StartsWith("sql_", StringComparison.OrdinalIgnoreCase))
            {
                throw new TaktBusinessException($"数据源为SQL查询时，字典类型编码必须以 'sql_' 开头，当前编码：{dto.DictTypeCode}");
            }
            if (string.IsNullOrWhiteSpace(dto.SqlScript))
            {
                throw new TaktBusinessException("数据源为SQL查询时，SQL脚本不能为空");
            }
        }

        // 查重验证（DictTypeCode、DictTypeName、DataSource 任意一个重复都报错）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_dictTypeRepository, d => d.DictTypeCode, dto.DictTypeCode, null, $"字典类型编码 {dto.DictTypeCode} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_dictTypeRepository, d => d.DictTypeName, dto.DictTypeName, null, $"字典类型名称 {dto.DictTypeName} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_dictTypeRepository, d => d.DataSource, dto.DataSource, null, $"数据源 {dto.DataSource} 已存在");

        // 使用Mapster映射DTO到实体，然后手动设置状态
        var dictType = dto.Adapt<TaktDictType>();
        dictType.DictTypeStatus = 0; // 0=启用

        dictType = await _dictTypeRepository.CreateAsync(dictType);

        // 创建子表数据（字典数据列表）
        if (dto.DictDataList != null && dto.DictDataList.Any())
        {
            foreach (var dictDataDto in dto.DictDataList)
            {
                // 根据 DictTypeCode 查找 DictTypeId（已创建的主表）
                var dictData = dictDataDto.Adapt<TaktDictData>();
                dictData.DictTypeId = dictType.Id;
                dictData.DictTypeCode = dictType.DictTypeCode;

                await _dictDataRepository.CreateAsync(dictData);
            }
        }

        return await GetByIdAsync(dictType.Id) ?? dictType.Adapt<TaktDictTypeDto>();
    }

    /// <summary>
    /// 更新字典类型
    /// </summary>
    /// <param name="id">字典类型ID</param>
    /// <param name="dto">更新字典类型DTO</param>
    /// <returns>字典类型DTO</returns>
    public async Task<TaktDictTypeDto> UpdateAsync(long id, TaktDictTypeUpdateDto dto)
    {
        var dictType = await _dictTypeRepository.GetByIdAsync(id);
        if (dictType == null)
            throw new TaktBusinessException("字典类型不存在");

        // 验证：如果数据源为SQL查询（DataSource = 1），字典类型编码必须以 sql_ 开头，且必须提供SQL脚本和数据库配置ID
        if (dto.DataSource == 1)
        {
            if (string.IsNullOrWhiteSpace(dto.DictTypeCode))
            {
                throw new TaktBusinessException("数据源为SQL查询时，字典类型编码不能为空");
            }
            if (!dto.DictTypeCode.StartsWith("sql_", StringComparison.OrdinalIgnoreCase))
            {
                throw new TaktBusinessException($"数据源为SQL查询时，字典类型编码必须以 'sql_' 开头，当前编码：{dto.DictTypeCode}");
            }
            if (string.IsNullOrWhiteSpace(dto.SqlScript))
            {
                throw new TaktBusinessException("数据源为SQL查询时，SQL脚本不能为空");
            }
            if (string.IsNullOrWhiteSpace(dto.DataConfigId))
            {
                throw new TaktBusinessException("数据源为SQL查询时，数据库配置ID不能为空");
            }
        }

        // 查重验证（排除当前记录，DictTypeCode、DictTypeName、DataSource 任意一个重复都报错）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_dictTypeRepository, d => d.DictTypeCode, dto.DictTypeCode, id, $"字典类型编码 {dto.DictTypeCode} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_dictTypeRepository, d => d.DictTypeName, dto.DictTypeName, id, $"字典类型名称 {dto.DictTypeName} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_dictTypeRepository, d => d.DataSource, dto.DataSource, id, $"数据源 {dto.DataSource} 已存在");

        // 使用Mapster更新实体
        dto.Adapt(dictType, typeof(TaktDictTypeUpdateDto), typeof(TaktDictType));
        dictType.UpdateTime = DateTime.Now;

        await _dictTypeRepository.UpdateAsync(dictType);

        // 更新子表数据（字典数据列表）
        if (dto.DictDataList != null)
        {
            // 删除旧的字典数据
            var oldDictDataList = await _dictDataRepository.FindAsync(d => d.DictTypeId == id && d.IsDeleted == 0);
            foreach (var dictData in oldDictDataList)
            {
                await _dictDataRepository.DeleteAsync(dictData.Id);
            }

            // 创建新的字典数据
            if (dto.DictDataList.Any())
            {
                foreach (var dictDataDto in dto.DictDataList)
                {
                    var dictData = dictDataDto.Adapt<TaktDictData>();
                    dictData.DictTypeId = id;
                    dictData.DictTypeCode = dictType.DictTypeCode;

                    await _dictDataRepository.CreateAsync(dictData);
                }
            }
        }

        return await GetByIdAsync(id) ?? dictType.Adapt<TaktDictTypeDto>();
    }

    /// <summary>
    /// 删除字典类型
    /// </summary>
    /// <param name="id">字典类型ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var dictType = await _dictTypeRepository.GetByIdAsync(id);
        if (dictType == null)
            throw new TaktBusinessException("字典类型不存在");

        // 1. 先将 DictTypeStatus 置为禁用（1），再软删除（IsDeleted=1）
        dictType.DictTypeStatus = 1;
        dictType.UpdateTime = DateTime.Now;
        await _dictTypeRepository.UpdateAsync(dictType);

        // 2. 软删除字典类型（IsDeleted = 1）
        await _dictTypeRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除字典类型
    /// </summary>
    /// <param name="ids">字典类型ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有字典类型记录
        var dictTypes = await _dictTypeRepository.FindAsync(dt => idList.Contains(dt.Id));

        // 1. 先将所有记录的 DictTypeStatus 置为禁用（1），再软删除（IsDeleted=1）
        foreach (var dictType in dictTypes)
        {
            dictType.DictTypeStatus = 1;
            dictType.UpdateTime = DateTime.Now;
            await _dictTypeRepository.UpdateAsync(dictType);
        }

        // 2. 批量软删除字典类型（IsDeleted = 1）
        await _dictTypeRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新字典类型状态
    /// </summary>
    /// <param name="dto">字典类型状态DTO</param>
    /// <returns>字典类型DTO</returns>
    public async Task<TaktDictTypeDto> UpdateStatusAsync(TaktDictTypeStatusDto dto)
    {
        var dictType = await _dictTypeRepository.GetByIdAsync(dto.DictTypeId);
        if (dictType == null)
            throw new TaktBusinessException("字典类型不存在");

        dictType.DictTypeStatus = dto.DictTypeStatus;
        dictType.UpdateTime = DateTime.Now;

        await _dictTypeRepository.UpdateAsync(dictType);

        return dictType.Adapt<TaktDictTypeDto>();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktDictTypeTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "字典类型导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "字典类型导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入字典类型
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
            var importData = await TaktExcelHelper.ImportAsync<TaktDictTypeImportDto>(
                fileStream, 
                string.IsNullOrWhiteSpace(sheetName) ? "字典类型导入模板" : sheetName
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
                    if (string.IsNullOrWhiteSpace(item.DictTypeCode))
                    {
                        errors.Add($"第{index}行：字典类型编码不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.DictTypeName))
                    {
                        errors.Add($"第{index}行：字典类型名称不能为空");
                        fail++;
                        continue;
                    }

                    // 验证：如果数据源为SQL查询（DataSource = 1），字典类型编码必须以 sql_ 开头，且必须提供SQL脚本和数据库配置ID
                    var dataSource = item.DataSource >= 0 ? item.DataSource : 0;
                    if (dataSource == 1)
                    {
                        if (string.IsNullOrWhiteSpace(item.DictTypeCode))
                        {
                            errors.Add($"第{index}行：数据源为SQL查询时，字典类型编码不能为空");
                            fail++;
                            continue;
                        }
                        if (!item.DictTypeCode.StartsWith("sql_", StringComparison.OrdinalIgnoreCase))
                        {
                            errors.Add($"第{index}行：数据源为SQL查询时，字典类型编码必须以 'sql_' 开头，当前编码：{item.DictTypeCode}");
                            fail++;
                            continue;
                        }
                        if (string.IsNullOrWhiteSpace(item.SqlScript))
                        {
                            errors.Add($"第{index}行：数据源为SQL查询时，SQL脚本不能为空");
                            fail++;
                            continue;
                        }
                        if (string.IsNullOrWhiteSpace(item.DataConfigId))
                        {
                            errors.Add($"第{index}行：数据源为SQL查询时，数据库配置ID不能为空");
                            fail++;
                            continue;
                        }
                    }

                    // 导入时使用验证器手动验证（DictTypeCode、DictTypeName、DataSource 任意一个重复都报错）
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_dictTypeRepository, dt => dt.DictTypeCode, item.DictTypeCode, null, $"第{index}行：字典类型编码 {item.DictTypeCode} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_dictTypeRepository, dt => dt.DictTypeName, item.DictTypeName, null, $"第{index}行：字典类型名称 {item.DictTypeName} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_dictTypeRepository, dt => dt.DataSource, item.DataSource, null, $"第{index}行：数据源 {item.DataSource} 已存在");

                    // 创建字典类型实体
                    var dictType = new TaktDictType
                    {
                        DictTypeCode = item.DictTypeCode,
                        DictTypeName = item.DictTypeName,
                        DataSource = item.DataSource >= 0 ? item.DataSource : 0,
                        DataConfigId = string.IsNullOrWhiteSpace(item.DataConfigId) ? "0" : item.DataConfigId,
                        SqlScript = string.IsNullOrWhiteSpace(item.SqlScript) ? null : item.SqlScript,
                        IsBuiltIn = item.IsBuiltIn >= 0 ? item.IsBuiltIn : 1,
                        OrderNum = item.OrderNum,
                        DictTypeStatus = item.DictTypeStatus >= 0 ? item.DictTypeStatus : 0, // 默认为启用（0=启用）
                        Remark = item.Remark
                    };

                    // 保存字典类型
                    await _dictTypeRepository.CreateAsync(dictType);
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
    /// 导出字典类型
    /// </summary>
    /// <param name="query">字典类型查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktDictTypeQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的字典类型（不分页）
        List<TaktDictType> dictTypes;
        if (predicate != null)
        {
            dictTypes = await _dictTypeRepository.FindAsync(predicate);
        }
        else
        {
            dictTypes = await _dictTypeRepository.GetAllAsync();
        }

        if (dictTypes == null || dictTypes.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktDictTypeExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "字典类型数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "字典类型导出" : fileName
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = dictTypes.Select(d =>
        {
            var dto = d.Adapt<TaktDictTypeExportDto>();
            // 处理需要特殊转换的字段
            dto.DataSource = GetDataSourceString(d.DataSource);
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "字典类型数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "字典类型导出" : fileName
        );
    }

    /// <summary>
    /// 加载字典数据列表（子表数据）
    /// </summary>
    /// <param name="dictTypeDtos">字典类型DTO列表</param>
    /// <param name="dictTypeIds">字典类型ID列表</param>
    private async Task LoadDictDataListAsync(List<TaktDictTypeDto> dictTypeDtos, List<long> dictTypeIds)
    {
        // 加载字典数据
        var dictDataList = await _dictDataRepository.FindAsync(d => dictTypeIds.Contains(d.DictTypeId) && d.IsDeleted == 0);
        var dictDataDtos = dictDataList
            .OrderBy(d => d.OrderNum)
            .ThenBy(d => d.CreateTime)
            .Select(d => d.Adapt<TaktDictDataDto>())
            .ToList();

        // 关联字典数据到字典类型
        var dictDict = dictDataDtos.GroupBy(d => d.DictTypeId).ToDictionary(g => g.Key, g => g.ToList());
        foreach (var dictTypeDto in dictTypeDtos)
        {
            if (dictDict.TryGetValue(dictTypeDto.DictTypeId, out var dataList))
            {
                dictTypeDto.DictDataList = dataList;
            }
        }
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktDictType, bool>> QueryExpression(TaktDictTypeQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktDictType>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.DictTypeName.Contains(queryDto.KeyWords) ||
                              x.DictTypeCode.Contains(queryDto.KeyWords));
        }

        // 字典类型名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DictTypeName), x => x.DictTypeName.Contains(queryDto!.DictTypeName!));

        // 字典类型编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DictTypeCode), x => x.DictTypeCode.Contains(queryDto!.DictTypeCode!));

        // 字典类型状态
        exp = exp.AndIF(queryDto?.DictTypeStatus.HasValue == true, x => x.DictTypeStatus == queryDto!.DictTypeStatus!.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 获取数据源字符串
    /// </summary>
    private string GetDataSourceString(int dataSource)
    {
        return dataSource switch
        {
            0 => "系统表",
            1 => "SQL查询",
            _ => "未知"
        };
    }
}