// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting
// 文件名称：TaktCostCenterService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt成本中心应用服务，提供成本中心管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using System.Linq.Expressions;
using Takt.Application.Dtos.Accounting;
using Takt.Application.Services;
using Takt.Domain.Entities.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting;

/// <summary>
/// Takt成本中心应用服务
/// </summary>
public class TaktCostCenterService : TaktServiceBase, ITaktCostCenterService
{
    private readonly ITaktRepository<TaktCostCenter> _costCenterRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="costCenterRepository">成本中心仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCostCenterService(
        ITaktRepository<TaktCostCenter> costCenterRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _costCenterRepository = costCenterRepository;
    }

    /// <summary>
    /// 获取成本中心列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktCostCenterDto>> GetCostCenterListAsync(TaktCostCenterQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _costCenterRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCostCenterDto>.Create(
            data.Adapt<List<TaktCostCenterDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取成本中心
    /// </summary>
    /// <param name="id">成本中心ID</param>
    /// <returns>成本中心DTO</returns>
    public async Task<TaktCostCenterDto?> GetCostCenterByIdAsync(long id)
    {
        var costCenter = await _costCenterRepository.GetByIdAsync(id);
        if (costCenter == null) return null;

        return costCenter.Adapt<TaktCostCenterDto>();
    }

    /// <summary>
    /// 获取成本中心选项列表（用于下拉框等）
    /// </summary>
    /// <returns>成本中心选项列表</returns>
    public async Task<List<TaktSelectOption>> GetCostCenterOptionsAsync()
    {
        var costCenters = await _costCenterRepository.FindAsync(c => c.IsDeleted == 0 && c.CostCenterStatus == 0);
        return costCenters
            .OrderBy(c => c.OrderNum)
            .ThenBy(c => c.CreatedAt)
            .Select(c => new TaktSelectOption
            {
                DictLabel = c.CostCenterName,
                DictValue = c.CostCenterCode,
                ExtLabel = c.CostCenterCode,
                OrderNum = c.OrderNum
            })
            .ToList();
    }

    /// <summary>
    /// 创建成本中心
    /// </summary>
    /// <param name="dto">创建成本中心DTO</param>
    /// <returns>成本中心DTO</returns>
    public async Task<TaktCostCenterDto> CreateCostCenterAsync(TaktCostCenterCreateDto dto)
    {
        // 查重验证（CostCenterCode唯一）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_costCenterRepository, c => c.CostCenterCode, dto.CostCenterCode, null, null, $"成本中心编码 {dto.CostCenterCode} 已存在");

        // 使用Mapster映射DTO到实体
        var costCenter = dto.Adapt<TaktCostCenter>();
        costCenter.CostCenterStatus = 0; // 0=启用

        // 计算层级
        if (dto.ParentId > 0)
        {
            var parent = await _costCenterRepository.GetByIdAsync(dto.ParentId);
            if (parent != null)
            {
                costCenter.CostCenterLevel = parent.CostCenterLevel + 1;
            }
            else
            {
                costCenter.CostCenterLevel = 1;
            }
        }
        else
        {
            costCenter.CostCenterLevel = 1;
        }

        costCenter = await _costCenterRepository.CreateAsync(costCenter);

        return await GetCostCenterByIdAsync(costCenter.Id) ?? costCenter.Adapt<TaktCostCenterDto>();
    }

    /// <summary>
    /// 更新成本中心
    /// </summary>
    /// <param name="id">成本中心ID</param>
    /// <param name="dto">更新成本中心DTO</param>
    /// <returns>成本中心DTO</returns>
    public async Task<TaktCostCenterDto> UpdateCostCenterAsync(long id, TaktCostCenterUpdateDto dto)
    {
        var costCenter = await _costCenterRepository.GetByIdAsync(id);
        if (costCenter == null)
            throw new TaktBusinessException("validation.costCenterNotFound");

        // 查重验证（排除当前记录，CostCenterCode唯一）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_costCenterRepository, c => c.CostCenterCode, dto.CostCenterCode, null, id, $"成本中心编码 {dto.CostCenterCode} 已存在");

        // 使用Mapster更新实体
        dto.Adapt(costCenter, typeof(TaktCostCenterUpdateDto), typeof(TaktCostCenter));
        costCenter.UpdatedAt = DateTime.Now;

        // 如果父级ID改变，重新计算层级
        if (dto.ParentId != costCenter.ParentId)
        {
            if (dto.ParentId > 0)
            {
                var parent = await _costCenterRepository.GetByIdAsync(dto.ParentId);
                if (parent != null)
                {
                    costCenter.CostCenterLevel = parent.CostCenterLevel + 1;
                }
                else
                {
                    costCenter.CostCenterLevel = 1;
                }
            }
            else
            {
                costCenter.CostCenterLevel = 1;
            }
        }

        await _costCenterRepository.UpdateAsync(costCenter);

        return await GetCostCenterByIdAsync(id) ?? costCenter.Adapt<TaktCostCenterDto>();
    }

    /// <summary>
    /// 删除成本中心
    /// </summary>
    /// <param name="id">成本中心ID</param>
    /// <returns>任务</returns>
    public async Task DeleteCostCenterByIdAsync(long id)
    {
        // 检查是否有子节点
        var children = await _costCenterRepository.FindAsync(c => c.ParentId == id && c.IsDeleted == 0);
        if (children.Any())
        {
            throw new TaktBusinessException("validation.costCenterHasChildrenCannotDelete");
        }

        // 删除成本中心
        await _costCenterRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除成本中心
    /// </summary>
    /// <param name="ids">成本中心ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteCostCenterBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有成本中心记录
        var costCenters = await _costCenterRepository.FindAsync(c => idList.Contains(c.Id));

        // 检查是否有子节点
        var costCentersWithChildren = new List<long>();
        foreach (var costCenter in costCenters)
        {
            var children = await _costCenterRepository.FindAsync(c => c.ParentId == costCenter.Id && c.IsDeleted == 0);
            if (children.Any())
            {
                costCentersWithChildren.Add(costCenter.Id);
            }
        }
        if (costCentersWithChildren.Any())
        {
            var costCenterCodes = string.Join(", ", costCenters.Where(c => costCentersWithChildren.Contains(c.Id)).Select(c => c.CostCenterCode));
            throw new TaktLocalizedException("validation.costCenterBulkHasChildrenCannotDelete", "Frontend", costCenterCodes);
        }

        // 1. 先将所有记录的 CostCenterStatus 置为禁用（1），再软删除（IsDeleted=1）
        foreach (var costCenter in costCenters)
        {
            costCenter.CostCenterStatus = 1;
            costCenter.UpdatedAt = DateTime.Now;
            await _costCenterRepository.UpdateAsync(costCenter);
        }

        // 2. 批量软删除成本中心（IsDeleted = 1）
        await _costCenterRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新成本中心状态
    /// </summary>
    /// <param name="dto">成本中心状态DTO</param>
    /// <returns>成本中心DTO</returns>
    public async Task<TaktCostCenterDto> UpdateCostCenterStatusAsync(TaktCostCenterStatusDto dto)
    {
        var costCenter = await _costCenterRepository.GetByIdAsync(dto.CostCenterId);
        if (costCenter == null)
            throw new TaktBusinessException("validation.costCenterNotFound");

        costCenter.CostCenterStatus = dto.CostCenterStatus;
        costCenter.UpdatedAt = DateTime.Now;

        await _costCenterRepository.UpdateAsync(costCenter);

        return costCenter.Adapt<TaktCostCenterDto>();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetCostCenterTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktCostCenter));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktCostCenterTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }

    /// <summary>
    /// 导入成本中心
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportCostCenterAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktCostCenter));
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktCostCenterImportDto>(
                fileStream,
                excelSheet
            );

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            // 批量处理导入数据
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3))) // 第3行开始是数据
            {
                try
                {
                    // 验证必填字段
                    if (string.IsNullOrWhiteSpace(item.CostCenterCode))
                    {
                        AddImportError(errors, "validation.importRowCostCenterCodeRequired", index);
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.CostCenterName))
                    {
                        AddImportError(errors, "validation.importRowCostCenterNameRequired", index);
                        fail++;
                        continue;
                    }

                    // 导入时使用验证器手动验证（CostCenterCode唯一）
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_costCenterRepository, c => c.CostCenterCode, item.CostCenterCode, null, null, GetLocalizedString("validation.importRowCostCenterCodeExists", "Frontend", index, item.CostCenterCode));

                    // 创建成本中心实体
                    var costCenter = new TaktCostCenter
                    {
                        CostCenterCode = item.CostCenterCode,
                        CostCenterName = item.CostCenterName,
                        CostCenterType = item.CostCenterType >= 0 ? item.CostCenterType : 0,
                        ManagerName = item.ManagerName,
                        DeptName = item.DeptName,
                        OrderNum = item.OrderNum,
                        CostCenterStatus = item.CostCenterStatus >= 0 ? item.CostCenterStatus : 0, // 默认为启用（0=启用）
                        CostCenterLevel = 1, // 导入时默认为1级
                        ParentId = 0, // 导入时默认为根节点
                        Remark = item.Remark
                    };

                    // 保存成本中心
                    await _costCenterRepository.CreateAsync(costCenter);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入成本中心失败（第{index}行）: {ex.Message}");
                    AddImportError(errors, "validation.importRowUnhandledException", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入成本中心异常（第{index}行）: {ex.Message}");
                    AddImportError(errors, "validation.importRowFailedWithReason", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"导入成本中心过程发生错误: {ex.Message}");
            AddImportError(errors, "validation.importProcessFailedWithReason", GetLocalizedExceptionMessage(ex));
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出成本中心
    /// </summary>
    /// <param name="query">成本中心查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportCostCenterAsync(TaktCostCenterQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的成本中心（不分页）
        List<TaktCostCenter> costCenters;
        if (predicate != null)
        {
            costCenters = await _costCenterRepository.FindAsync(predicate);
        }
        else
        {
            costCenters = await _costCenterRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktCostCenter));
        if (costCenters == null || costCenters.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktCostCenterExportDto>(),
                excelSheet,
                excelFile
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = costCenters.Select(c =>
        {
            var dto = c.Adapt<TaktCostCenterExportDto>();
            // 处理需要特殊转换的字段
            dto.CostCenterType = GetCostCenterTypeString(c.CostCenterType);
            dto.ManagerName = c.ManagerName ?? string.Empty;
            dto.DeptName = c.DeptName ?? string.Empty;
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
    /// 获取成本中心类型字符串
    /// </summary>
    private string GetCostCenterTypeString(int costCenterType)
    {
        return costCenterType switch
        {
            0 => "成本中心",
            1 => "利润中心",
            2 => "投资中心",
            _ => "未知"
        };
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktCostCenter, bool>> QueryExpression(TaktCostCenterQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktCostCenter>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.CostCenterName.Contains(queryDto.KeyWords) ||
                              x.CostCenterCode.Contains(queryDto.KeyWords));
        }

        // 成本中心名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CostCenterName), x => x.CostCenterName.Contains(queryDto!.CostCenterName!));

        // 成本中心编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CostCenterCode), x => x.CostCenterCode.Contains(queryDto!.CostCenterCode!));

        // 父级ID
        exp = exp.AndIF(queryDto?.ParentId.HasValue == true, x => x.ParentId == queryDto!.ParentId!.Value);

        // 成本中心类型
        exp = exp.AndIF(queryDto?.CostCenterType.HasValue == true, x => x.CostCenterType == queryDto!.CostCenterType!.Value);

        // 成本中心状态
        exp = exp.AndIF(queryDto?.CostCenterStatus.HasValue == true, x => x.CostCenterStatus == queryDto!.CostCenterStatus!.Value);

        return exp.ToExpression();
    }
}
