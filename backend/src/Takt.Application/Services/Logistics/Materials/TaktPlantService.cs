// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPlantService.cs
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：工厂表应用服务，由 DtoCategory 配置驱动。校验分层与 Identity.TaktUserService 一致：① WebApi 入参由 TaktFluentValidationActionFilter + 本模块 TaktPlantValidators.cs（Create/Update DTO）校验；② 本类无 IValidator 注入，仅含 TaktUniqueValidatorExtensions、TaktBusinessException 键、以及导入时 AddImportError / GetLocalizedExceptionMessage（同 TaktUserService.ImportUserAsync 结构）。跨表/敏感词等须在生成后手写。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Domain.Entities.Logistics.Materials;
using Takt.Application.Services;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 工厂表应用服务（与 <c>TaktUserService</c> 相同约定：不在 Application 注入 FluentValidation；DTO 字段校验在 WebApi 过滤器 + 同模块 Validators；此处为仓储查重与 Excel 导入导出流程。）
/// </summary>
public class TaktPlantService : TaktServiceBase, ITaktPlantService
{
    private readonly ITaktRepository<TaktPlant> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPlantService(
        ITaktRepository<TaktPlant> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// 获取工厂表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPlantDto>> GetPlantListAsync(TaktPlantQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPlantDto>.Create(
            data.Adapt<List<TaktPlantDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <summary>
    /// 根据ID获取工厂表
    /// </summary>
    /// <param name="id">工厂表ID</param>
    /// <returns>工厂表DTO</returns>
    public async Task<TaktPlantDto?> GetPlantByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity?.Adapt<TaktPlantDto>();
    }

    /// <summary>
    /// 获取工厂表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPlantOptionsAsync()
    {
        var list = await _repository.FindAsync(x => x.IsDeleted == 0);
        return list
            .OrderBy(x => x.Id)
            .Select(x => new TaktSelectOption
            {
                DictLabel = (Convert.ToString(x.PlantCode) ?? string.Empty).Trim(),
                DictValue = x.Id,
                SortOrder = 0
            })
            .ToList();
    }

    /// <summary>
    /// 创建工厂表
    /// </summary>
    /// <remarks>
    /// 与 <c>TaktUserService.CreateUserAsync</c> 分工一致：经 Controller 调用时，<c>TaktPlantCreateDtoValidator</c>（见同模块 Validators）已由 <c>TaktFluentValidationActionFilter</c> 对 <c>CreateDto</c> 执行；本方法内不注入 <c>IValidator</c>，仅保留 <c>TaktUniqueValidatorExtensions</c> 与写库。模块特有校验请在生成代码中补充。
    /// </remarks>
    /// <param name="dto">创建工厂表DTO</param>
    /// <returns>工厂表DTO</returns>
    public async Task<TaktPlantDto> CreatePlantAsync(TaktPlantCreateDto dto)
    {

        var entity = dto.Adapt<TaktPlant>();
        entity = await _repository.CreateAsync(entity);
        return entity.Adapt<TaktPlantDto>();
    }

    /// <summary>
    /// 更新工厂表
    /// </summary>
    /// <remarks>
    /// 与创建相同：WebApi 已对 <c>UpdateDto</c> 执行 <c>TaktPlantUpdateDtoValidator</c>（含 Create 规则时通过 <c>Include</c>）；此处负责存在性（<c>validation.recordNotFound</c>）、可选唯一性校验与 <c>UpdatedAt</c> 更新。
    /// </remarks>
    /// <param name="id">工厂表ID</param>
    /// <param name="dto">更新工厂表DTO</param>
    /// <returns>工厂表DTO</returns>
    public async Task<TaktPlantDto> UpdatePlantAsync(long id, TaktPlantUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.recordNotFound");

        dto.Adapt(entity, typeof(TaktPlantUpdateDto), typeof(TaktPlant));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return entity.Adapt<TaktPlantDto>();
    }

    /// <summary>
    /// 删除工厂表
    /// </summary>
    /// <param name="id">工厂表ID</param>
    /// <returns>任务</returns>
    public async Task DeletePlantByIdAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除工厂表
    /// </summary>
    /// <param name="ids">工厂表ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePlantBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        await _repository.DeleteAsync(idList);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <remarks>与 <c>TaktUserService.GetUserTemplateAsync</c> 相同：<c>ResolveExcelImportTemplateNamesAsync</c> + <c>GenerateTemplateAsync</c>。</remarks>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPlantTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPlant));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPlantTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }

    /// <summary>
    /// 导入工厂表
    /// </summary>
    /// <remarks>
    /// 与 <c>TaktUserService.ImportUserAsync</c> 相同结构：外层 <c>try</c>、<c>ResolveExcelSheetName</c>、空数据 <c>AddImportError(validation.importExcelNoData)</c>、按行 <c>index + 3</c>、<c>TaktBusinessException</c> 与 <c>Exception</c> 分支、<c>GetLocalizedExceptionMessage</c>、过程失败 <c>validation.importProcessFailedWithReason</c>。行级业务校验键需与种子 <c>validation.import*</c> 一致时请生成后补全。
    /// </remarks>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPlantAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktPlant));
            var importData = await TaktExcelHelper.ImportAsync<TaktPlantImportDto>(fileStream, excelSheet);

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {

                    var e = item.Adapt<TaktPlant>();
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
    /// 导出工厂表
    /// </summary>
    /// <remarks>与 <c>TaktUserService.ExportUserAsync</c> 相同：<c>ResolveExcelExportNamesAsync</c>，无数据时仍导出空表。</remarks>
    /// <param name="query">工厂表查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPlantAsync(TaktPlantQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        var data = predicate != null ? await _repository.FindAsync(predicate) : await _repository.GetAllAsync();
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPlant));
        if (data == null || data.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPlantExportDto>(),
                excelSheet,
                excelFile);
        }

        var dtos = data.Adapt<List<TaktPlantExportDto>>();
        return await TaktExcelHelper.ExportAsync(dtos, excelSheet, excelFile);
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static System.Linq.Expressions.Expression<Func<TaktPlant, bool>> QueryExpression(TaktPlantQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktPlant>();

        // 关键词查询（在可查询的字符串字段中模糊匹配）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>

                x.PlantCode.Contains(queryDto.KeyWords)
|| x.PlantName.Contains(queryDto.KeyWords)

            );
        }

        // 工厂代码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PlantCode), x => x.PlantCode.Contains(queryDto!.PlantCode!));
        // 工厂名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PlantName), x => x.PlantName.Contains(queryDto!.PlantName!));

        return exp.ToExpression();
    }

}
