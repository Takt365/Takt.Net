// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPlantService.cs
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：工厂表应用服务，由 DtoCategory 配置驱动，按 type 判断输出
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
/// 工厂表应用服务
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
    /// 创建工厂表
    /// </summary>
    /// <param name="dto">创建工厂表DTO</param>
    /// <returns>工厂表DTO</returns>
    public async Task<TaktPlantDto> CreatePlantAsync(TaktPlantCreateDto dto)
    {


        // 查重：唯一字段组合校验
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x =>

                x.PlantCode == dto.PlantCode
&& x.PlantShortName == dto.PlantShortName
,
            null,
            "工厂表唯一字段组合已存在");

        var entity = dto.Adapt<TaktPlant>();
        entity = await _repository.CreateAsync(entity);
        return entity.Adapt<TaktPlantDto>();
    }

    /// <summary>
    /// 更新工厂表
    /// </summary>
    /// <param name="id">工厂表ID</param>
    /// <param name="dto">更新工厂表DTO</param>
    /// <returns>工厂表DTO</returns>
    public async Task<TaktPlantDto> UpdatePlantAsync(long id, TaktPlantUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.plantNotFound");


        // 查重（排除当前记录）：唯一字段组合校验
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x =>

                x.PlantCode == dto.PlantCode
&& x.PlantShortName == dto.PlantShortName
,
            id,
            "工厂表唯一字段组合已存在");

        dto.Adapt(entity, typeof(TaktPlantUpdateDto), typeof(TaktPlant));
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
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPlantAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;
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


                // 导入查重：唯一字段组合校验
                await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
                    _repository,
                    x =>

                        x.PlantCode == item.PlantCode
&& x.PlantShortName == item.PlantShortName
,
                    null,
                    GetLocalizedString("validation.importPlantUniqueCombinationExists", "Frontend"));

                var e = item.Adapt<TaktPlant>();
                await _repository.CreateAsync(e);
                success++;
            }
            catch (Exception ex)
            {
                fail++;
                AddImportError(errors, "validation.importRowUnhandledException", index, GetLocalizedExceptionMessage(ex));
            }
        }
        return (success, fail, errors);
    }

    /// <summary>
    /// 导出工厂表
    /// </summary>
    /// <param name="query">工厂表查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPlantAsync(TaktPlantQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        var data = predicate != null ? await _repository.FindAsync(predicate) : await _repository.GetAllAsync();
        var dtos = data?.Adapt<List<TaktPlantExportDto>>() ?? new List<TaktPlantExportDto>();
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPlant));
        return await TaktExcelHelper.ExportAsync(dtos,
            excelSheet,
            excelFile);
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
|| x.PlantShortName.Contains(queryDto.KeyWords)

            );
        }

        // 工厂代码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PlantCode), x => x.PlantCode.Contains(queryDto!.PlantCode!));
        // 工厂名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PlantName), x => x.PlantName.Contains(queryDto!.PlantName!));
        // 工厂简称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PlantShortName), x => x.PlantShortName.Contains(queryDto!.PlantShortName!));

        return exp.ToExpression();
    }

}
