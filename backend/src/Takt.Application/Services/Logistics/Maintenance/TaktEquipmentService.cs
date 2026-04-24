// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Maintenance
// 文件名称：TaktEquipmentService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工厂设备应用服务，定义工厂设备管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Logistics.Maintenance;
using Takt.Domain.Entities.Logistics.Maintenance;
using Takt.Application.Services;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Maintenance;

/// <summary>
/// Takt工厂设备应用服务
/// </summary>
public class TaktEquipmentService : TaktServiceBase, ITaktEquipmentService
{
    private readonly ITaktRepository<TaktEquipment> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEquipmentService(
        ITaktRepository<TaktEquipment> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// 获取工厂设备列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEquipmentDto>> GetEquipmentListAsync(TaktEquipmentQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEquipmentDto>.Create(
            data.Adapt<List<TaktEquipmentDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <summary>
    /// 根据ID获取工厂设备
    /// </summary>
    /// <param name="id">工厂设备ID</param>
    /// <returns>工厂设备DTO</returns>
    public async Task<TaktEquipmentDto?> GetEquipmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity?.Adapt<TaktEquipmentDto>();
    }

    /// <summary>
    /// 获取工厂设备选项列表（用于下拉框等）
    /// </summary>
    /// <returns>选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEquipmentOptionsAsync()
    {
        var list = await _repository.FindAsync(x => x.IsDeleted == 0);
        return list
            .OrderBy(x => x.Id)
            .Select(x => new TaktSelectOption
            {
                DictLabel = (Convert.ToString(x.EquipmentCode) ?? string.Empty).Trim(),
                DictValue = x.Id,
                SortOrder = 0
            })
            .ToList();
    }

    /// <summary>
    /// 创建工厂设备
    /// </summary>
    /// <param name="dto">创建工厂设备DTO</param>
    /// <returns>工厂设备DTO</returns>
    public async Task<TaktEquipmentDto> CreateEquipmentAsync(TaktEquipmentCreateDto dto)
    {
        var entity = dto.Adapt<TaktEquipment>();
        entity = await _repository.CreateAsync(entity);
        return entity.Adapt<TaktEquipmentDto>();
    }

    /// <summary>
    /// 更新工厂设备
    /// </summary>
    /// <param name="id">工厂设备ID</param>
    /// <param name="dto">更新工厂设备DTO</param>
    /// <returns>工厂设备DTO</returns>
    public async Task<TaktEquipmentDto> UpdateEquipmentAsync(long id, TaktEquipmentUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.recordNotFound");

        dto.Adapt(entity, typeof(TaktEquipmentUpdateDto), typeof(TaktEquipment));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return entity.Adapt<TaktEquipmentDto>();
    }

    /// <summary>
    /// 删除工厂设备
    /// </summary>
    /// <param name="id">工厂设备ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEquipmentByIdAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除工厂设备
    /// </summary>
    /// <param name="ids">工厂设备ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEquipmentBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        await _repository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新工厂设备状态
    /// </summary>
    /// <param name="dto">工厂设备状态DTO</param>
    /// <returns>工厂设备DTO</returns>
    public async Task<TaktEquipmentDto> UpdateEquipmentStatusAsync(TaktEquipmentStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.EquipmentId);
        if (entity == null)
            throw new TaktBusinessException("validation.recordNotFound");

        entity.EquipmentStatus = dto.EquipmentStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return entity.Adapt<TaktEquipmentDto>();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetEquipmentTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEquipment));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEquipmentTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }

    /// <summary>
    /// 导入工厂设备
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEquipmentAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktEquipment));
            var importData = await TaktExcelHelper.ImportAsync<TaktEquipmentImportDto>(fileStream, excelSheet);

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    var e = item.Adapt<TaktEquipment>();
                    await _repository.CreateAsync(e);
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
    /// 导出工厂设备
    /// </summary>
    /// <param name="query">工厂设备查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEquipmentAsync(TaktEquipmentQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        var data = predicate != null ? await _repository.FindAsync(predicate) : await _repository.GetAllAsync();
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEquipment));
        if (data == null || data.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEquipmentExportDto>(),
                excelSheet,
                excelFile);
        }

        var dtos = data.Adapt<List<TaktEquipmentExportDto>>();
        return await TaktExcelHelper.ExportAsync(dtos, excelSheet, excelFile);
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static System.Linq.Expressions.Expression<Func<TaktEquipment, bool>> QueryExpression(TaktEquipmentQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktEquipment>();

        // 关键词查询（在可查询的字符串字段中模糊匹配）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.EquipmentCode.Contains(queryDto.KeyWords)
                || x.EquipmentName.Contains(queryDto.KeyWords)
            );
        }

        // 设备编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.EquipmentCode), x => x.EquipmentCode.Contains(queryDto!.EquipmentCode!));
        // 设备名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.EquipmentName), x => x.EquipmentName.Contains(queryDto!.EquipmentName!));
        // 设备类型
        exp = exp.AndIF(queryDto?.EquipmentType.HasValue == true, x => x.EquipmentType == queryDto!.EquipmentType!.Value);
        // 所属车间
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.WorkshopBy), x => x.WorkshopBy!.Contains(queryDto!.WorkshopBy!));
        // 所属产线
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.ProductionLineBy), x => x.ProductionLineBy!.Contains(queryDto!.ProductionLineBy!));
        // 所属部门
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DeptBy), x => x.DeptBy!.Contains(queryDto!.DeptBy!));
        // 保修状态
        exp = exp.AndIF(queryDto?.WarrantyStatus.HasValue == true, x => x.WarrantyStatus == queryDto!.WarrantyStatus!.Value);
        // 设备状态
        exp = exp.AndIF(queryDto?.EquipmentStatus.HasValue == true, x => x.EquipmentStatus == queryDto!.EquipmentStatus!.Value);

        return exp.ToExpression();
    }
}
