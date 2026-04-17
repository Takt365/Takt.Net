// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：TaktAccountingTitleService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt会计科目应用服务，提供会计科目管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using System.Linq.Expressions;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Application.Services;
using Takt.Domain.Entities.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// Takt会计科目应用服务
/// </summary>
public class TaktAccountingTitleService : TaktServiceBase, ITaktAccountingTitleService
{
    private readonly ITaktRepository<TaktAccountingTitle> _titleRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="titleRepository">会计科目仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAccountingTitleService(
        ITaktRepository<TaktAccountingTitle> titleRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _titleRepository = titleRepository;
    }

    /// <summary>
    /// 获取会计科目列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAccountingTitleDto>> GetListAsync(TaktAccountingTitleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _titleRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAccountingTitleDto>.Create(
            data.Adapt<List<TaktAccountingTitleDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取会计科目
    /// </summary>
    /// <param name="id">科目ID</param>
    /// <returns>会计科目DTO</returns>
    public async Task<TaktAccountingTitleDto?> GetByIdAsync(long id)
    {
        var title = await _titleRepository.GetByIdAsync(id);
        if (title == null) return null;

        return title.Adapt<TaktAccountingTitleDto>();
    }

    /// <summary>
    /// 获取会计科目树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>会计科目树形选项列表</returns>
    public async Task<List<TaktTreeSelectOption>> GetTreeOptionsAsync()
    {
        var titles = await _titleRepository.FindAsync(t => t.IsDeleted == 0 && t.TitleStatus == 0);

        if (titles == null || titles.Count == 0)
        {
            return new List<TaktTreeSelectOption>();
        }

        // 转换为树形选项
        var titleOptions = titles
            .OrderBy(t => t.OrderNum)
            .ThenBy(t => t.CreatedAt)
            .Select(t => new TaktTreeSelectOption
            {
                DictLabel = t.TitleName,
                DictValue = t.Id,
                ExtLabel = t.TitleCode,
                ExtValue = GetTitleTypeString(t.TitleType),
                OrderNum = t.OrderNum
            })
            .ToList();

        // 构建树形结构
        var titleDict = titleOptions.ToDictionary(t => (long)t.DictValue, t => t);
        var titleEntityDict = titles.ToDictionary(t => t.Id, t => t);
        var rootNodes = new List<TaktTreeSelectOption>();

        foreach (var titleOption in titleOptions)
        {
            var titleId = (long)titleOption.DictValue;
            if (titleEntityDict.TryGetValue(titleId, out var titleEntity))
            {
                if (titleEntity.ParentId == 0 || !titleDict.ContainsKey(titleEntity.ParentId))
                {
                    // 根节点或父节点不存在
                    rootNodes.Add(titleOption);
                }
                else
                {
                    // 添加到父节点的Children中
                    var parent = titleDict[titleEntity.ParentId];
                    if (parent.Children == null)
                    {
                        parent.Children = new List<TaktTreeSelectOption>();
                    }
                    parent.Children.Add(titleOption);
                }
            }
        }

        return rootNodes;
    }

    /// <summary>
    /// 获取会计科目树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的科目（默认false）</param>
    /// <returns>会计科目树形列表</returns>
    public async Task<List<TaktAccountingTitleTreeDto>> GetTreeAsync(long parentId = 0, bool includeDisabled = false)
    {
        // 1. 查询所有科目（根据includeDisabled过滤）
        Expression<Func<TaktAccountingTitle, bool>>? predicate = t => t.IsDeleted == 0;
        if (!includeDisabled)
        {
            predicate = t => t.IsDeleted == 0 && t.TitleStatus == 0;
        }

        var allTitles = await _titleRepository.FindAsync(predicate);

        if (allTitles == null || allTitles.Count == 0)
        {
            return new List<TaktAccountingTitleTreeDto>();
        }

        // 转换为DTO
        var titleDtos = allTitles
            .OrderBy(t => t.OrderNum)
            .ThenBy(t => t.CreatedAt)
            .Select(t => t.Adapt<TaktAccountingTitleTreeDto>())
            .ToList();

        // 2. 构建树形结构
        var titleDict = titleDtos.ToDictionary(t => t.TitleId, t => t);
        var rootNodes = new List<TaktAccountingTitleTreeDto>();

        foreach (var title in titleDtos)
        {
            if (title.ParentId == 0 || !titleDict.ContainsKey(title.ParentId))
            {
                // 根节点或父节点不存在（已删除等情况）
                rootNodes.Add(title);
            }
            else
            {
                // 添加到父节点的Children中
                var parent = titleDict[title.ParentId];
                if (parent.Children == null)
                {
                    parent.Children = new List<TaktAccountingTitleTreeDto>();
                }
                parent.Children.Add(title);
            }
        }

        // 3. 如果指定了parentId，只返回该父级下的子树
        if (parentId == 0)
        {
            // 返回所有根节点
            return rootNodes;
        }
        else
        {
            // 查找指定父级ID的节点
            var targetNode = titleDtos.FirstOrDefault(t => t.TitleId == parentId);
            if (targetNode == null)
            {
                return new List<TaktAccountingTitleTreeDto>();
            }
            return new List<TaktAccountingTitleTreeDto> { targetNode };
        }
    }

    /// <summary>
    /// 获取会计科目子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的科目（默认false）</param>
    /// <returns>会计科目子节点列表</returns>
    public async Task<List<TaktAccountingTitleDto>> GetChildrenAsync(long parentId, bool includeDisabled = false)
    {
        // 1. 查询指定父级ID下的直接子节点
        Expression<Func<TaktAccountingTitle, bool>>? predicate = t => t.IsDeleted == 0 && t.ParentId == parentId;
        if (!includeDisabled)
        {
            predicate = t => t.IsDeleted == 0 && t.ParentId == parentId && t.TitleStatus == 0;
        }

        var children = await _titleRepository.FindAsync(predicate);

        if (children == null || children.Count == 0)
        {
            return new List<TaktAccountingTitleDto>();
        }

        // 2. 按OrderNum排序
        return children
            .OrderBy(t => t.OrderNum)
            .ThenBy(t => t.CreatedAt)
            .Select(t => t.Adapt<TaktAccountingTitleDto>())
            .ToList();
    }

    /// <summary>
    /// 创建会计科目
    /// </summary>
    /// <param name="dto">创建会计科目DTO</param>
    /// <returns>会计科目DTO</returns>
    public async Task<TaktAccountingTitleDto> CreateAsync(TaktAccountingTitleCreateDto dto)
    {
        // 查重验证（TitleCode唯一）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_titleRepository, t => t.TitleCode, dto.TitleCode, null, null, $"科目编码 {dto.TitleCode} 已存在");

        // 使用Mapster映射DTO到实体
        var title = dto.Adapt<TaktAccountingTitle>();
        title.TitleStatus = 0; // 0=启用

        // 计算层级
        if (dto.ParentId > 0)
        {
            var parent = await _titleRepository.GetByIdAsync(dto.ParentId);
            if (parent != null)
            {
                title.TitleLevel = parent.TitleLevel + 1;
            }
            else
            {
                title.TitleLevel = 1;
            }
        }
        else
        {
            title.TitleLevel = 1;
        }

        title = await _titleRepository.CreateAsync(title);

        return await GetByIdAsync(title.Id) ?? title.Adapt<TaktAccountingTitleDto>();
    }

    /// <summary>
    /// 更新会计科目
    /// </summary>
    /// <param name="id">科目ID</param>
    /// <param name="dto">更新会计科目DTO</param>
    /// <returns>会计科目DTO</returns>
    public async Task<TaktAccountingTitleDto> UpdateAsync(long id, TaktAccountingTitleUpdateDto dto)
    {
        var title = await _titleRepository.GetByIdAsync(id);
        if (title == null)
            throw new TaktBusinessException("validation.accountingTitleNotFound");

        // 查重验证（排除当前记录，TitleCode唯一）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_titleRepository, t => t.TitleCode, dto.TitleCode, null, id, $"科目编码 {dto.TitleCode} 已存在");

        // 使用Mapster更新实体
        dto.Adapt(title, typeof(TaktAccountingTitleUpdateDto), typeof(TaktAccountingTitle));
        title.UpdatedAt = DateTime.Now;

        // 如果父级ID改变，重新计算层级
        if (dto.ParentId != title.ParentId)
        {
            if (dto.ParentId > 0)
            {
                var parent = await _titleRepository.GetByIdAsync(dto.ParentId);
                if (parent != null)
                {
                    title.TitleLevel = parent.TitleLevel + 1;
                }
                else
                {
                    title.TitleLevel = 1;
                }
            }
            else
            {
                title.TitleLevel = 1;
            }
        }

        await _titleRepository.UpdateAsync(title);

        return await GetByIdAsync(id) ?? title.Adapt<TaktAccountingTitleDto>();
    }

    /// <summary>
    /// 删除会计科目
    /// </summary>
    /// <param name="id">科目ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        // 检查是否有子节点
        var children = await _titleRepository.FindAsync(t => t.ParentId == id && t.IsDeleted == 0);
        if (children.Any())
        {
            throw new TaktBusinessException("validation.accountingTitleHasChildrenCannotDelete");
        }

        // 删除会计科目
        await _titleRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除会计科目
    /// </summary>
    /// <param name="ids">科目ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有会计科目记录
        var titles = await _titleRepository.FindAsync(t => idList.Contains(t.Id));

        // 检查是否有子节点
        var titlesWithChildren = new List<long>();
        foreach (var title in titles)
        {
            var children = await _titleRepository.FindAsync(t => t.ParentId == title.Id && t.IsDeleted == 0);
            if (children.Any())
            {
                titlesWithChildren.Add(title.Id);
            }
        }
        if (titlesWithChildren.Any())
        {
            var titleCodes = string.Join(", ", titles.Where(t => titlesWithChildren.Contains(t.Id)).Select(t => t.TitleCode));
            throw new TaktLocalizedException("validation.accountingTitleBulkHasChildrenCannotDelete", "Frontend", titleCodes);
        }

        // 1. 先将所有记录的 TitleStatus 置为禁用（1），再软删除（IsDeleted=1）
        foreach (var title in titles)
        {
            title.TitleStatus = 1;
            title.UpdatedAt = DateTime.Now;
            await _titleRepository.UpdateAsync(title);
        }

        // 2. 批量软删除会计科目（IsDeleted = 1）
        await _titleRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新会计科目状态
    /// </summary>
    /// <param name="dto">会计科目状态DTO</param>
    /// <returns>会计科目DTO</returns>
    public async Task<TaktAccountingTitleDto> UpdateStatusAsync(TaktAccountingTitleStatusDto dto)
    {
        var title = await _titleRepository.GetByIdAsync(dto.TitleId);
        if (title == null)
            throw new TaktBusinessException("validation.accountingTitleNotFound");

        title.TitleStatus = dto.TitleStatus;
        title.UpdatedAt = DateTime.Now;

        await _titleRepository.UpdateAsync(title);

        return title.Adapt<TaktAccountingTitleDto>();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAccountingTitle));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAccountingTitleTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }

    /// <summary>
    /// 导入会计科目
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
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktAccountingTitle));
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktAccountingTitleImportDto>(
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
                    if (string.IsNullOrWhiteSpace(item.TitleCode))
                    {
                        AddImportError(errors, "validation.importRowAccountingTitleCodeRequired", index);
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.TitleName))
                    {
                        AddImportError(errors, "validation.importRowAccountingTitleNameRequired", index);
                        fail++;
                        continue;
                    }

                    // 导入时使用验证器手动验证（TitleCode唯一）
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_titleRepository, t => t.TitleCode, item.TitleCode, null, null, GetLocalizedString("validation.importRowAccountingTitleCodeExists", "Frontend", index, item.TitleCode));

                    // 创建会计科目实体
                    var title = new TaktAccountingTitle
                    {
                        TitleCode = item.TitleCode,
                        TitleName = item.TitleName,
                        TitleType = item.TitleType >= 0 ? item.TitleType : 0,
                        BalanceDirection = item.BalanceDirection >= 0 ? item.BalanceDirection : 0,
                        IsLeaf = item.IsLeaf >= 0 ? item.IsLeaf : 1,
                        IsAuxiliary = item.IsAuxiliary >= 0 ? item.IsAuxiliary : 0,
                        AuxiliaryType = item.AuxiliaryType >= 0 ? item.AuxiliaryType : 0,
                        IsQuantity = item.IsQuantity >= 0 ? item.IsQuantity : 0,
                        IsCurrency = item.IsCurrency >= 0 ? item.IsCurrency : 0,
                        IsCash = item.IsCash >= 0 ? item.IsCash : 0,
                        IsBank = item.IsBank >= 0 ? item.IsBank : 0,
                        OrderNum = item.OrderNum,
                        TitleStatus = item.TitleStatus >= 0 ? item.TitleStatus : 0, // 默认为启用（0=启用）
                        TitleLevel = 1, // 导入时默认为1级
                        ParentId = 0, // 导入时默认为根节点
                        Remark = item.Remark
                    };

                    // 保存会计科目
                    await _titleRepository.CreateAsync(title);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入会计科目失败（第{index}行）: {ex.Message}");
                    AddImportError(errors, "validation.importRowUnhandledException", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入会计科目异常（第{index}行）: {ex.Message}");
                    AddImportError(errors, "validation.importRowFailedWithReason", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"导入会计科目过程发生错误: {ex.Message}");
            AddImportError(errors, "validation.importProcessFailedWithReason", GetLocalizedExceptionMessage(ex));
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出会计科目
    /// </summary>
    /// <param name="query">会计科目查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktAccountingTitleQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的会计科目（不分页）
        List<TaktAccountingTitle> titles;
        if (predicate != null)
        {
            titles = await _titleRepository.FindAsync(predicate);
        }
        else
        {
            titles = await _titleRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAccountingTitle));
        if (titles == null || titles.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAccountingTitleExportDto>(),
                excelSheet,
                excelFile
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = titles.Select(t =>
        {
            var dto = t.Adapt<TaktAccountingTitleExportDto>();
            // 处理需要特殊转换的字段
            dto.TitleType = GetTitleTypeString(t.TitleType);
            dto.BalanceDirection = GetBalanceDirectionString(t.BalanceDirection);
            dto.IsLeaf = t.IsLeaf == 1 ? "是" : "否";
            dto.IsAuxiliary = t.IsAuxiliary == 1 ? "是" : "否";
            dto.AuxiliaryType = GetAuxiliaryTypeString(t.AuxiliaryType);
            dto.IsQuantity = t.IsQuantity == 1 ? "是" : "否";
            dto.IsCurrency = t.IsCurrency == 1 ? "是" : "否";
            dto.IsCash = t.IsCash == 1 ? "是" : "否";
            dto.IsBank = t.IsBank == 1 ? "是" : "否";
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
    /// 获取科目类型字符串
    /// </summary>
    private string GetTitleTypeString(int titleType)
    {
        return titleType switch
        {
            0 => "资产",
            1 => "负债",
            2 => "所有者权益",
            3 => "收入",
            4 => "费用",
            5 => "成本",
            _ => "未知"
        };
    }

    /// <summary>
    /// 获取余额方向字符串
    /// </summary>
    private string GetBalanceDirectionString(int balanceDirection)
    {
        return balanceDirection switch
        {
            0 => "借方",
            1 => "贷方",
            _ => "未知"
        };
    }

    /// <summary>
    /// 获取辅助核算类型字符串
    /// </summary>
    private string GetAuxiliaryTypeString(int auxiliaryType)
    {
        return auxiliaryType switch
        {
            0 => "无",
            1 => "部门",
            2 => "项目",
            3 => "客户",
            4 => "供应商",
            5 => "员工",
            6 => "自定义",
            _ => "未知"
        };
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktAccountingTitle, bool>> QueryExpression(TaktAccountingTitleQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktAccountingTitle>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.TitleName.Contains(queryDto.KeyWords) ||
                              x.TitleCode.Contains(queryDto.KeyWords));
        }

        // 科目名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.TitleName), x => x.TitleName.Contains(queryDto!.TitleName!));

        // 科目编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.TitleCode), x => x.TitleCode.Contains(queryDto!.TitleCode!));

        // 父级ID
        exp = exp.AndIF(queryDto?.ParentId.HasValue == true, x => x.ParentId == queryDto!.ParentId!.Value);

        // 科目类型
        exp = exp.AndIF(queryDto?.TitleType.HasValue == true, x => x.TitleType == queryDto!.TitleType!.Value);

        // 科目状态
        exp = exp.AndIF(queryDto?.TitleStatus.HasValue == true, x => x.TitleStatus == queryDto!.TitleStatus!.Value);

        return exp.ToExpression();
    }
}
