// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowSchemeService.cs
// 创建时间：2025-02-26
// 功能描述：流程方案服务实现
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Text.Json;
using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Workflow;
using Takt.Application.Services;
using Takt.Domain.Entities.Workflow;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程方案服务：提供流程定义的增删改查、状态更新及 Excel 模板/导入/导出
/// </summary>
public class TaktFlowSchemeService : TaktServiceBase, ITaktFlowSchemeService
{
    private readonly ITaktRepository<TaktFlowScheme> _schemeRepository;

    /// <summary>
    /// 初始化流程方案服务
    /// </summary>
    public TaktFlowSchemeService(
        ITaktRepository<TaktFlowScheme> schemeRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _schemeRepository = schemeRepository;
    }

    /// <summary>
    /// 分页查询流程方案列表
    /// </summary>
    /// <param name="query">分页及流程 Key、名称、状态、分类等筛选</param>
    /// <returns>流程方案分页结果</returns>
    public async Task<TaktPagedResult<TaktFlowSchemeDto>> GetListAsync(TaktFlowSchemeQueryDto query)
    {
        var exp = Expressionable.Create<TaktFlowScheme>()
            .AndIF(!string.IsNullOrWhiteSpace(query.ProcessKey), x => x.ProcessKey.Contains(query.ProcessKey!))
            .AndIF(!string.IsNullOrWhiteSpace(query.ProcessName), x => x.ProcessName.Contains(query.ProcessName!))
            .AndIF(query.ProcessStatus.HasValue, x => x.ProcessStatus == query.ProcessStatus!.Value)
            .AndIF(query.ProcessCategory.HasValue, x => x.ProcessCategory == query.ProcessCategory!.Value)
            .AndIF(!string.IsNullOrWhiteSpace(query.FormCode), x => x.FormCode != null && x.FormCode == query.FormCode)
            .And(x => x.IsDeleted == 0)
            .ToExpression();
        var (data, total) = await _schemeRepository.GetPagedAsync(query.PageIndex, query.PageSize, exp);
        return TaktPagedResult<TaktFlowSchemeDto>.Create(
            data.Adapt<List<TaktFlowSchemeDto>>(),
            total,
            query.PageIndex,
            query.PageSize);
    }

    /// <summary>
    /// 按主键获取流程方案详情
    /// </summary>
    /// <param name="id">方案 ID</param>
    /// <returns>方案 DTO，不存在时返回 null</returns>
    public async Task<TaktFlowSchemeDto?> GetByIdAsync(long id)
    {
        var entity = await _schemeRepository.GetByIdAsync(id);
        return entity?.Adapt<TaktFlowSchemeDto>();
    }

    /// <summary>
    /// 按流程 Key 获取已发布且未删除的流程方案
    /// </summary>
    /// <param name="processKey">流程 Key</param>
    /// <returns>方案 DTO，不存在时返回 null</returns>
    public async Task<TaktFlowSchemeDto?> GetByProcessKeyAsync(string processKey)
    {
        var entity = await _schemeRepository.GetAsync(x => x.ProcessKey == processKey && x.IsDeleted == 0);
        return entity?.Adapt<TaktFlowSchemeDto>();
    }

    /// <summary>
    /// 创建流程方案
    /// </summary>
    /// <param name="dto">创建参数</param>
    /// <returns>创建后的方案 DTO</returns>
    public async Task<TaktFlowSchemeDto> CreateAsync(TaktFlowSchemeCreateDto dto)
    {
        ValidateProcessContentOrThrow(dto.ProcessContent);
        var entity = dto.Adapt<TaktFlowScheme>();
        entity.Id = 0;
        entity.ProcessVersion = 1;
        await _schemeRepository.CreateAsync(entity);
        return entity.Adapt<TaktFlowSchemeDto>();
    }

    /// <summary>
    /// 更新流程方案
    /// </summary>
    /// <param name="id">方案 ID</param>
    /// <param name="dto">更新参数</param>
    /// <returns>更新后的方案 DTO</returns>
    public async Task<TaktFlowSchemeDto> UpdateAsync(long id, TaktFlowSchemeUpdateDto dto)
    {
        ValidateProcessContentOrThrow(dto.ProcessContent);
        var existing = await _schemeRepository.GetByIdAsync(id);
        if (existing == null)
            throw new TaktBusinessException("validation.flowSchemeNotFound");
        dto.Adapt(existing, typeof(TaktFlowSchemeUpdateDto), typeof(TaktFlowScheme));
        existing.Id = id;
        await _schemeRepository.UpdateAsync(existing);
        return existing.Adapt<TaktFlowSchemeDto>();
    }

    /// <summary>
    /// 删除流程方案
    /// </summary>
    /// <param name="id">流程方案ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var entity = await _schemeRepository.GetByIdAsync(id);
        if (entity == null) return;
        entity.IsDeleted = 1;
        entity.DeletedAt = DateTime.Now;
        await _schemeRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// 批量删除流程方案
    /// </summary>
    /// <param name="ids">流程方案ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        if (ids == null) return;
        foreach (var id in ids.Distinct())
        {
            var entity = await _schemeRepository.GetByIdAsync(id);
            if (entity == null) continue;
            entity.IsDeleted = 1;
            entity.DeletedAt = DateTime.Now;
            await _schemeRepository.UpdateAsync(entity);
        }
    }

    /// <summary>
    /// 更新流程方案状态（草稿/已发布/已停用）
    /// </summary>
    /// <param name="dto">方案 ID、目标状态及备注</param>
    /// <returns>更新后的方案 DTO</returns>
    public async Task<TaktFlowSchemeDto> UpdateStatusAsync(TaktFlowSchemeStatusDto dto)
    {
        var entity = await _schemeRepository.GetByIdAsync(dto.SchemeId);
        EnsureEntityExists(entity, "validation.flowSchemeNotFound");
        entity!.ProcessStatus = dto.ProcessStatus;
        if (!string.IsNullOrEmpty(dto.Remark))
            entity.Remark = dto.Remark;
        await _schemeRepository.UpdateAsync(entity);
        return entity.Adapt<TaktFlowSchemeDto>();
    }

    /// <summary>
    /// 生成流程方案 Excel 导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称，为空时使用默认</param>
    /// <param name="fileName">文件名，为空时使用默认</param>
    /// <returns>文件名与文件二进制内容</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktFlowScheme));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFlowSchemeTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <summary>
    /// 从 Excel 流导入流程方案（仅元数据，不含 ProcessContent）
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称，为空时使用默认</param>
    /// <returns>成功数、失败数及错误信息列表</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0, fail = 0;
        var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktFlowScheme));
        var importData = await TaktExcelHelper.ImportAsync<TaktFlowSchemeImportDto>(
            fileStream, excelSheet);
        if (importData == null || importData.Count == 0)
        {
            AddImportError(errors, "validation.importExcelNoData");
            return (0, 0, errors);
        }
        foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.ProcessKey))
                {
                    AddImportError(errors, "validation.importRowFlowProcessKeyRequired", index);
                    fail++;
                    continue;
                }
                var existing = await _schemeRepository.GetAsync(x => x.ProcessKey == item.ProcessKey && x.IsDeleted == 0);
                if (existing != null)
                {
                    AddImportError(errors, "validation.importRowFlowProcessKeyExists", index, item.ProcessKey);
                    fail++;
                    continue;
                }
                var entity = new TaktFlowScheme
                {
                    ProcessKey = item.ProcessKey,
                    ProcessName = item.ProcessName ?? item.ProcessKey,
                    ProcessCategory = item.ProcessCategory,
                    ProcessDescription = item.ProcessDescription,
                    FormCode = item.FormCode,
                    OrderNum = item.OrderNum,
                    ProcessStatus = item.ProcessStatus >= 0 ? item.ProcessStatus : 0,
                    ProcessVersion = 1,
                    ProcessContent = null
                };
                await _schemeRepository.CreateAsync(entity);
                success++;
            }
            catch (Exception ex)
            {
                AddImportError(errors, "validation.importRowUnhandledException", index, GetLocalizedExceptionMessage(ex));
                fail++;
            }
        }
        return (success, fail, errors);
    }

    /// <summary>
    /// 按查询条件导出流程方案为 Excel
    /// </summary>
    /// <param name="query">筛选条件</param>
    /// <param name="sheetName">工作表名称，为空时使用默认</param>
    /// <param name="fileName">文件名，为空时使用默认</param>
    /// <returns>文件名与文件二进制内容</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktFlowSchemeQueryDto query, string? sheetName, string? fileName)
    {
        var exp = Expressionable.Create<TaktFlowScheme>()
            .AndIF(!string.IsNullOrWhiteSpace(query.ProcessKey), x => x.ProcessKey.Contains(query.ProcessKey!))
            .AndIF(!string.IsNullOrWhiteSpace(query.ProcessName), x => x.ProcessName.Contains(query.ProcessName!))
            .AndIF(query.ProcessStatus.HasValue, x => x.ProcessStatus == query.ProcessStatus!.Value)
            .AndIF(query.ProcessCategory.HasValue, x => x.ProcessCategory == query.ProcessCategory!.Value)
            .And(x => x.IsDeleted == 0)
            .ToExpression();
        var list = await _schemeRepository.FindAsync(exp);
        var dtos = list?.Adapt<List<TaktFlowSchemeExportDto>>() ?? new List<TaktFlowSchemeExportDto>();
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFlowScheme));
        return await TaktExcelHelper.ExportAsync(dtos, excelSheet, excelFile);
    }

    /// <summary>
    /// 校验 ProcessContent 为合法 JSON，且含设计器/引擎所需的 nodes、edges；
    /// 若存在 flowTree，则根节点 nodeType 须为 1（发起人），与前端 takt-flow-antflow-designer 一致。
    /// </summary>
    private void ValidateProcessContentOrThrow(string? processContent)
    {
        if (string.IsNullOrWhiteSpace(processContent))
            return;
        try
        {
            using var doc = JsonDocument.Parse(processContent.Trim());
            var root = doc.RootElement;
            if (root.ValueKind != JsonValueKind.Object)
                ThrowBusinessException("validation.flowProcessContentRootMustBeJsonObject");
            if (!root.TryGetProperty("nodes", out var nodesEl) || nodesEl.ValueKind != JsonValueKind.Array)
                ThrowBusinessException("validation.flowProcessContentMustContainNodesArray");
            if (!root.TryGetProperty("edges", out var edgesEl) || edgesEl.ValueKind != JsonValueKind.Array)
                ThrowBusinessException("validation.flowProcessContentMustContainEdgesArray");
            if (!root.TryGetProperty("flowTree", out var ft))
                return;
            if (ft.ValueKind != JsonValueKind.Object)
                ThrowBusinessException("validation.flowTreeMustBeJsonObject");
            if (!ft.TryGetProperty("nodeType", out var nt))
                return;
            var kind = nt.ValueKind;
            if (kind == JsonValueKind.Number && nt.GetInt32() != 1)
                ThrowBusinessException("validation.flowTreeRootNodeTypeMustBeStarter");
            if (kind == JsonValueKind.String && int.TryParse(nt.GetString(), out var iv) && iv != 1)
                ThrowBusinessException("validation.flowTreeRootNodeTypeMustBeStarter");
        }
        catch (System.Text.Json.JsonException ex)
        {
            throw new TaktLocalizedException("validation.flowProcessContentInvalidJson", "Frontend", ex.Message);
        }
    }
}
