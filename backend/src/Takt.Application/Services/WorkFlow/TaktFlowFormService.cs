// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowFormService.cs
// 功能描述：流程表单服务实现
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.IO;
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
/// 流程表单服务：提供流程表单的增删改查、状态更新及 Excel 模板/导入/导出，表单编码唯一
/// </summary>
public class TaktFlowFormService : TaktServiceBase, ITaktFlowFormService
{
    private readonly ITaktRepository<TaktFlowForm> _formRepository;

    /// <summary>
    /// 初始化流程表单服务
    /// </summary>
    public TaktFlowFormService(
        ITaktRepository<TaktFlowForm> formRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _formRepository = formRepository;
    }

    /// <summary>
    /// 分页查询流程表单列表
    /// </summary>
    /// <param name="query">分页及表单编码、名称、分类、状态等筛选</param>
    /// <returns>流程表单分页结果</returns>
    public async Task<TaktPagedResult<TaktFlowFormDto>> GetFlowFormListAsync(TaktFlowFormQueryDto query)
    {
        var exp = Expressionable.Create<TaktFlowForm>()
            .AndIF(!string.IsNullOrWhiteSpace(query.FormCode), x => x.FormCode.Contains(query.FormCode!))
            .AndIF(!string.IsNullOrWhiteSpace(query.FormName), x => x.FormName.Contains(query.FormName!))
            .AndIF(query.FormCategory.HasValue, x => x.FormCategory == query.FormCategory!.Value)
            .AndIF(query.FormStatus.HasValue, x => x.FormStatus == query.FormStatus!.Value)
            .And(x => x.IsDeleted == 0)
            .ToExpression();
        var (data, total) = await _formRepository.GetPagedAsync(query.PageIndex, query.PageSize, exp);
        return TaktPagedResult<TaktFlowFormDto>.Create(
            data.Adapt<List<TaktFlowFormDto>>(),
            total,
            query.PageIndex,
            query.PageSize);
    }

    /// <summary>
    /// 按主键获取流程表单详情
    /// </summary>
    /// <param name="id">表单 ID</param>
    /// <returns>表单 DTO，不存在时返回 null</returns>
    public async Task<TaktFlowFormDto?> GetFlowFormByIdAsync(long id)
    {
        var entity = await _formRepository.GetByIdAsync(id);
        return entity?.Adapt<TaktFlowFormDto>();
    }

    /// <summary>
    /// 按表单编码获取未删除的流程表单
    /// </summary>
    /// <param name="formCode">表单编码</param>
    /// <returns>表单 DTO，不存在时返回 null</returns>
    public async Task<TaktFlowFormDto?> GetFlowFormByFormCodeAsync(string formCode)
    {
        var entity = await _formRepository.GetAsync(x => x.FormCode == formCode && x.IsDeleted == 0);
        return entity?.Adapt<TaktFlowFormDto>();
    }

    /// <summary>
    /// 创建流程表单
    /// </summary>
    /// <param name="dto">创建参数</param>
    /// <returns>创建后的表单 DTO</returns>
    public async Task<TaktFlowFormDto> CreateFlowFormAsync(TaktFlowFormCreateDto dto)
    {
        var existing = await _formRepository.GetAsync(x => x.FormCode == dto.FormCode && x.IsDeleted == 0);
        if (existing != null)
            throw new TaktLocalizedException("validation.flowFormCodeAlreadyExists", "Frontend", dto.FormCode);
        var entity = dto.Adapt<TaktFlowForm>();
        entity.Id = 0;
        await _formRepository.CreateAsync(entity);
        return entity.Adapt<TaktFlowFormDto>();
    }

    /// <summary>
    /// 更新流程表单
    /// </summary>
    /// <param name="id">表单 ID</param>
    /// <param name="dto">更新参数</param>
    /// <returns>更新后的表单 DTO</returns>
    public async Task<TaktFlowFormDto> UpdateFlowFormAsync(long id, TaktFlowFormUpdateDto dto)
    {
        var entity = await _formRepository.GetByIdAsync(id);
        EnsureEntityExists(entity, "validation.flowFormNotFound");
        var sameCode = await _formRepository.GetAsync(x => x.FormCode == dto.FormCode && x.Id != id && x.IsDeleted == 0);
        if (sameCode != null)
            throw new TaktLocalizedException("validation.flowFormCodeUsedByOther", "Frontend", dto.FormCode);
        dto.Adapt(entity!, typeof(TaktFlowFormUpdateDto), typeof(TaktFlowForm));
        entity!.Id = id;
        await _formRepository.UpdateAsync(entity);
        return entity.Adapt<TaktFlowFormDto>();
    }

    /// <summary>
    /// 删除流程表单
    /// </summary>
    /// <param name="id">流程表单ID</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowFormByIdAsync(long id)
    {
        var entity = await _formRepository.GetByIdAsync(id);
        if (entity == null) return;
        entity.IsDeleted = 1;
        entity.DeletedAt = DateTime.Now;
        await _formRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// 批量删除流程表单
    /// </summary>
    /// <param name="ids">流程表单ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowFormBatchAsync(IEnumerable<long> ids)
    {
        if (ids == null) return;
        foreach (var id in ids.Distinct())
        {
            var entity = await _formRepository.GetByIdAsync(id);
            if (entity == null) continue;
            entity.IsDeleted = 1;
            entity.DeletedAt = DateTime.Now;
            await _formRepository.UpdateAsync(entity);
        }
    }

    /// <summary>
    /// 更新流程表单状态（草稿/已发布/已停用）
    /// </summary>
    /// <param name="dto">表单 ID、目标状态及备注</param>
    /// <returns>更新后的表单 DTO</returns>
    public async Task<TaktFlowFormDto> UpdateFlowFormStatusAsync(TaktFlowFormStatusDto dto)
    {
        if (!dto.FormId.HasValue)
            throw new ArgumentException("表单ID不能为空", nameof(dto.FormId));
            
        var entity = await _formRepository.GetByIdAsync(dto.FormId.Value);
        EnsureEntityExists(entity, "validation.flowFormNotFound");
        entity!.FormStatus = dto.FormStatus;
        if (!string.IsNullOrEmpty(dto.Remark))
            entity.Remark = dto.Remark;
        await _formRepository.UpdateAsync(entity);
        return entity.Adapt<TaktFlowFormDto>();
    }

    /// <summary>
    /// 生成流程表单 Excel 导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称，为空时使用默认</param>
    /// <param name="fileName">文件名，为空时使用默认</param>
    /// <returns>文件名与文件二进制内容</returns>
    public async Task<(string fileName, byte[] content)> GetFlowFormTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktFlowForm));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFlowFormTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <summary>
    /// 从 Excel 流导入流程表单
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称，为空时使用默认</param>
    /// <returns>成功数、失败数及错误信息列表</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportFlowFormAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0, fail = 0;
        var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktFlowForm));
        var importData = await TaktExcelHelper.ImportAsync<TaktFlowFormImportDto>(
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
                if (string.IsNullOrWhiteSpace(item.FormCode))
                {
                    AddImportError(errors, "validation.importRowFlowFormCodeRequired", index);
                    fail++;
                    continue;
                }
                var existing = await _formRepository.GetAsync(x => x.FormCode == item.FormCode && x.IsDeleted == 0);
                if (existing != null)
                {
                    AddImportError(errors, "validation.importRowFlowFormCodeExists", index, item.FormCode);
                    fail++;
                    continue;
                }
                var entity = new TaktFlowForm
                {
                    FormCode = item.FormCode,
                    FormName = item.FormName ?? item.FormCode,
                    FormCategory = item.FormCategory,
                    FormType = item.FormType,
                    FormVersion = item.FormVersion ?? "1.0.0",
                    SortOrder = item.SortOrder,
                    FormStatus = item.FormStatus >= 0 ? item.FormStatus : 0
                };
                await _formRepository.CreateAsync(entity);
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
    /// 按查询条件导出流程表单为 Excel
    /// </summary>
    /// <param name="query">筛选条件</param>
    /// <param name="sheetName">工作表名称，为空时使用默认</param>
    /// <param name="fileName">文件名，为空时使用默认</param>
    /// <returns>文件名与文件二进制内容</returns>
    public async Task<(string fileName, byte[] content)> ExportFlowFormAsync(TaktFlowFormQueryDto query, string? sheetName, string? fileName)
    {
        var exp = Expressionable.Create<TaktFlowForm>()
            .AndIF(!string.IsNullOrWhiteSpace(query.FormCode), x => x.FormCode.Contains(query.FormCode!))
            .AndIF(!string.IsNullOrWhiteSpace(query.FormName), x => x.FormName.Contains(query.FormName!))
            .AndIF(query.FormCategory.HasValue, x => x.FormCategory == query.FormCategory!.Value)
            .AndIF(query.FormStatus.HasValue, x => x.FormStatus == query.FormStatus!.Value)
            .And(x => x.IsDeleted == 0)
            .ToExpression();
        var list = await _formRepository.FindAsync(exp);
        var dtos = list?.Adapt<List<TaktFlowFormExportDto>>() ?? new List<TaktFlowFormExportDto>();
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFlowForm));
        return await TaktExcelHelper.ExportAsync(dtos, excelSheet, excelFile);
    }

    /// <summary>
    /// 可绑定实体表 → FormConfig（与领域实体字段一一对应，与种子数据一致）
    /// </summary>
    private static readonly IReadOnlyDictionary<string, string> EntityFormConfigs = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["TaktLeave"] = """
            [{"type":"select","field":"leaveType","title":"请假类型","props":{"options":[{"label":"事假","value":"affair"},{"label":"病假","value":"sick"},{"label":"年假","value":"annual"}]}},{"type":"datePicker","field":"startDate","title":"开始日期"},{"type":"datePicker","field":"endDate","title":"结束日期"},{"type":"textarea","field":"reason","title":"请假事由","props":{"rows":3}},{"type":"textarea","field":"proofAttachmentsJson","title":"证明信息（附件JSON）","props":{"rows":2,"placeholder":"可选，病假/事假等可上传证明后由业务写入JSON"}}]
            """.Trim(),
        ["Reimburse"] = """
            [{"type":"inputNumber","field":"amount","title":"报销金额","props":{"min":0,"precision":2}},{"type":"select","field":"category","title":"报销类型","props":{"options":[{"label":"差旅","value":"travel"},{"label":"餐饮","value":"meal"},{"label":"办公","value":"office"}]}},{"type":"input","field":"invoiceNo","title":"发票号码"},{"type":"textarea","field":"remark","title":"说明","props":{"rows":2}}]
            """.Trim()
    };

    private static readonly IReadOnlyList<TaktFlowFormBindableEntityDto> BindableEntities = new[]
    {
        new TaktFlowFormBindableEntityDto { EntityKey = "TaktLeave", DisplayName = "请假（TaktLeave）" },
        new TaktFlowFormBindableEntityDto { EntityKey = "Reimburse", DisplayName = "费用报销（Reimburse）" }
    };

    /// <inheritdoc />
    public Task<IReadOnlyList<TaktFlowFormBindableEntityDto>> GetFlowFormBindableEntitiesAsync()
    {
        return Task.FromResult(BindableEntities);
    }

    /// <inheritdoc />
    public Task<string?> GetFlowFormConfigByEntityKeyAsync(string entityKey)
    {
        if (string.IsNullOrWhiteSpace(entityKey))
            return Task.FromResult<string?>(null);
        return Task.FromResult(EntityFormConfigs.TryGetValue(entityKey.Trim(), out var config) ? config : null);
    }
}
