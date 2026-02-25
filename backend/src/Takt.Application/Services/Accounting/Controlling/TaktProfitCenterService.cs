// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：TaktProfitCenterService.cs
// 功能描述：Takt利润中心应用服务
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using SqlSugar;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services;
using Takt.Domain.Entities.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// Takt利润中心应用服务
/// </summary>
public class TaktProfitCenterService : TaktServiceBase, ITaktProfitCenterService
{
    private readonly ITaktRepository<TaktProfitCenter> _profitCenterRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterService(
        ITaktRepository<TaktProfitCenter> profitCenterRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _profitCenterRepository = profitCenterRepository;
    }

    /// <summary>
    /// 获取利润中心列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktProfitCenterDto>> GetListAsync(TaktProfitCenterQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _profitCenterRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktProfitCenterDto>.Create(
            data.Adapt<List<TaktProfitCenterDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取利润中心
    /// </summary>
    /// <param name="id">利润中心ID</param>
    /// <returns>利润中心DTO</returns>
    public async Task<TaktProfitCenterDto?> GetByIdAsync(long id)
    {
        var entity = await _profitCenterRepository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktProfitCenterDto>();
    }

    /// <summary>
    /// 获取利润中心选项列表（用于下拉框等）
    /// </summary>
    /// <returns>利润中心选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var list = await _profitCenterRepository.FindAsync(c => c.IsDeleted == 0 && c.ProfitCenterStatus == 0);
        return list
            .OrderBy(c => c.OrderNum)
            .ThenBy(c => c.CreateTime)
            .Select(c => new TaktSelectOption
            {
                DictLabel = c.ProfitCenterName,
                DictValue = c.Id,
                ExtLabel = c.ProfitCenterCode,
                OrderNum = c.OrderNum
            })
            .ToList();
    }

    /// <summary>
    /// 创建利润中心
    /// </summary>
    /// <param name="dto">创建利润中心DTO</param>
    /// <returns>利润中心DTO</returns>
    public async Task<TaktProfitCenterDto> CreateAsync(TaktProfitCenterCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_profitCenterRepository, c => c.ProfitCenterCode, dto.ProfitCenterCode, null, null, $"利润中心编码 {dto.ProfitCenterCode} 已存在");

        var entity = dto.Adapt<TaktProfitCenter>();
        entity.ProfitCenterStatus = 0;
        entity = await _profitCenterRepository.CreateAsync(entity);
        return await GetByIdAsync(entity.Id) ?? entity.Adapt<TaktProfitCenterDto>();
    }

    /// <summary>
    /// 更新利润中心
    /// </summary>
    /// <param name="id">利润中心ID</param>
    /// <param name="dto">更新利润中心DTO</param>
    /// <returns>利润中心DTO</returns>
    public async Task<TaktProfitCenterDto> UpdateAsync(long id, TaktProfitCenterUpdateDto dto)
    {
        var entity = await _profitCenterRepository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("利润中心不存在");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_profitCenterRepository, c => c.ProfitCenterCode, dto.ProfitCenterCode, null, id, $"利润中心编码 {dto.ProfitCenterCode} 已存在");

        dto.Adapt(entity, typeof(TaktProfitCenterUpdateDto), typeof(TaktProfitCenter));
        entity.UpdateTime = DateTime.Now;
        await _profitCenterRepository.UpdateAsync(entity);
        return await GetByIdAsync(id) ?? entity.Adapt<TaktProfitCenterDto>();
    }

    /// <summary>
    /// 删除利润中心
    /// </summary>
    /// <param name="id">利润中心ID</param>
    public async Task DeleteAsync(long id)
    {
        var children = await _profitCenterRepository.FindAsync(c => c.ParentId == id && c.IsDeleted == 0);
        if (children.Any())
            throw new TaktBusinessException("存在子利润中心，无法删除");

        await _profitCenterRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除利润中心
    /// </summary>
    /// <param name="ids">利润中心ID列表</param>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;

        var entities = await _profitCenterRepository.FindAsync(c => idList.Contains(c.Id));
        foreach (var entity in entities)
        {
            var children = await _profitCenterRepository.FindAsync(c => c.ParentId == entity.Id && c.IsDeleted == 0);
            if (children.Any())
                throw new TaktBusinessException($"利润中心 {entity.ProfitCenterCode} 存在子节点，无法删除");
        }

        await _profitCenterRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新利润中心状态
    /// </summary>
    /// <param name="dto">利润中心状态DTO</param>
    /// <returns>利润中心DTO</returns>
    public async Task<TaktProfitCenterDto> UpdateStatusAsync(TaktProfitCenterStatusDto dto)
    {
        var entity = await _profitCenterRepository.GetByIdAsync(dto.ProfitCenterId);
        if (entity == null)
            throw new TaktBusinessException("利润中心不存在");

        entity.ProfitCenterStatus = dto.ProfitCenterStatus;
        entity.UpdateTime = DateTime.Now;
        await _profitCenterRepository.UpdateAsync(entity);
        return entity.Adapt<TaktProfitCenterDto>();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktProfitCenterTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "利润中心导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "利润中心导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入利润中心
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
            var importData = await TaktExcelHelper.ImportAsync<TaktProfitCenterImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "利润中心导入模板" : sheetName
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
                    if (string.IsNullOrWhiteSpace(item.ProfitCenterCode))
                    {
                        errors.Add($"第{index}行：利润中心编码不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.ProfitCenterName))
                    {
                        errors.Add($"第{index}行：利润中心名称不能为空");
                        fail++;
                        continue;
                    }

                    // 导入时使用验证器手动验证（ProfitCenterCode唯一）
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_profitCenterRepository, c => c.ProfitCenterCode, item.ProfitCenterCode, null, null, $"第{index}行：利润中心编码 {item.ProfitCenterCode} 已存在");

                    // 创建利润中心实体
                    var profitCenter = new TaktProfitCenter
                    {
                        ProfitCenterCode = item.ProfitCenterCode,
                        ProfitCenterName = item.ProfitCenterName,
                        ManagerName = item.ManagerName,
                        DeptName = item.DeptName,
                        OrderNum = item.OrderNum,
                        ProfitCenterStatus = item.ProfitCenterStatus >= 0 ? item.ProfitCenterStatus : 0,
                        ParentId = 0,
                        Remark = item.Remark
                    };

                    // 保存利润中心
                    await _profitCenterRepository.CreateAsync(profitCenter);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入利润中心失败（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入利润中心异常（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"导入利润中心过程发生错误: {ex.Message}");
            errors.Add($"导入过程发生错误：{ex.Message}");
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出利润中心
    /// </summary>
    /// <param name="query">利润中心查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktProfitCenterQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的利润中心（不分页）
        List<TaktProfitCenter> profitCenters;
        if (predicate != null)
        {
            profitCenters = await _profitCenterRepository.FindAsync(predicate);
        }
        else
        {
            profitCenters = await _profitCenterRepository.GetAllAsync();
        }

        if (profitCenters == null || profitCenters.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktProfitCenterExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "利润中心数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "利润中心导出" : fileName
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = profitCenters.Select(c =>
        {
            var dto = c.Adapt<TaktProfitCenterExportDto>();
            // 处理需要特殊转换的字段
            dto.ManagerName = c.ManagerName ?? string.Empty;
            dto.DeptName = c.DeptName ?? string.Empty;
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "利润中心数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "利润中心导出" : fileName
        );
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    private static Expression<Func<TaktProfitCenter, bool>> QueryExpression(TaktProfitCenterQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktProfitCenter>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => (x.ProfitCenterName != null && x.ProfitCenterName.Contains(queryDto.KeyWords)) ||
                              (x.ProfitCenterCode != null && x.ProfitCenterCode.Contains(queryDto.KeyWords)));
        }
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.ProfitCenterName), x => x.ProfitCenterName != null && x.ProfitCenterName.Contains(queryDto!.ProfitCenterName!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.ProfitCenterCode), x => x.ProfitCenterCode != null && x.ProfitCenterCode.Contains(queryDto!.ProfitCenterCode!));
        exp = exp.AndIF(queryDto?.ParentId.HasValue == true, x => x.ParentId == queryDto!.ParentId!.Value);
        exp = exp.AndIF(queryDto?.ProfitCenterStatus.HasValue == true, x => x.ProfitCenterStatus == queryDto!.ProfitCenterStatus!.Value);

        return exp.ToExpression();
    }
}
