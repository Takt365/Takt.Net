// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPlantService.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工厂应用服务
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using SqlSugar;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// Takt工厂应用服务
/// </summary>
public class TaktPlantService : TaktServiceBase, ITaktPlantService
{
    private readonly ITaktRepository<TaktPlant> _plantRepository;

    public TaktPlantService(
        ITaktRepository<TaktPlant> plantRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _plantRepository = plantRepository;
    }

    public async Task<TaktPagedResult<TaktPlantDto>> GetListAsync(TaktPlantQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _plantRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPlantDto>.Create(data.Adapt<List<TaktPlantDto>>(), total, queryDto.PageIndex, queryDto.PageSize);
    }

    public async Task<TaktPlantDto?> GetByIdAsync(long id)
    {
        var entity = await _plantRepository.GetByIdAsync(id);
        return entity?.Adapt<TaktPlantDto>();
    }

    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var list = await _plantRepository.FindAsync(p => p.IsDeleted == 0 && p.PlantStatus == 0);
        return list.OrderBy(p => p.OrderNum).ThenBy(p => p.CreateTime)
            .Select(p => new TaktSelectOption { DictLabel = p.PlantName, DictValue = p.Id, ExtLabel = p.PlantCode, OrderNum = p.OrderNum })
            .ToList();
    }

    public async Task<TaktPlantDto> CreateAsync(TaktPlantCreateDto dto)
    {
        if (string.IsNullOrEmpty(dto.PlantCode) || dto.PlantCode.Trim().Length != 4)
            throw new TaktBusinessException("工厂代码必须为4位");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_plantRepository, p => p.PlantCode, dto.PlantCode, null, null, $"工厂代码 {dto.PlantCode} 已存在");
        var entity = dto.Adapt<TaktPlant>();
        entity.PlantStatus = 0;
        entity = await _plantRepository.CreateAsync(entity);
        return (await GetByIdAsync(entity.Id))!;
    }

    public async Task<TaktPlantDto> UpdateAsync(long id, TaktPlantUpdateDto dto)
    {
        var entity = await _plantRepository.GetByIdAsync(id);
        if (entity == null) throw new TaktBusinessException("工厂不存在");
        if (string.IsNullOrEmpty(dto.PlantCode) || dto.PlantCode.Trim().Length != 4)
            throw new TaktBusinessException("工厂代码必须为4位");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_plantRepository, p => p.PlantCode, dto.PlantCode, null, id, $"工厂代码 {dto.PlantCode} 已存在");
        dto.Adapt(entity, typeof(TaktPlantUpdateDto), typeof(TaktPlant));
        entity.UpdateTime = DateTime.Now;
        await _plantRepository.UpdateAsync(entity);
        return (await GetByIdAsync(id))!;
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _plantRepository.GetByIdAsync(id);
        if (entity == null) throw new TaktBusinessException("工厂不存在");
        entity.PlantStatus = 1;
        entity.UpdateTime = DateTime.Now;
        await _plantRepository.UpdateAsync(entity);
        await _plantRepository.DeleteAsync(id);
    }

    public async Task<TaktPlantDto> UpdateStatusAsync(TaktPlantStatusDto dto)
    {
        var entity = await _plantRepository.GetByIdAsync(dto.PlantId);
        if (entity == null) throw new TaktBusinessException("工厂不存在");
        entity.PlantStatus = dto.PlantStatus;
        entity.UpdateTime = DateTime.Now;
        await _plantRepository.UpdateAsync(entity);
        return entity.Adapt<TaktPlantDto>();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPlantTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "工厂导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "工厂导入模板" : fileName);
    }

    /// <summary>
    /// 导入工厂
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
            var importData = await TaktExcelHelper.ImportAsync<TaktPlantImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "工厂导入模板" : sheetName);

            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.PlantCode))
                    {
                        errors.Add($"第{index}行：工厂代码不能为空");
                        fail++;
                        continue;
                    }
                    if (item.PlantCode.Trim().Length != 4)
                    {
                        errors.Add($"第{index}行：工厂代码必须为4位");
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.PlantName))
                    {
                        errors.Add($"第{index}行：工厂名称不能为空");
                        fail++;
                        continue;
                    }

                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_plantRepository, p => p.PlantCode, item.PlantCode, null, null, $"第{index}行：工厂代码 {item.PlantCode} 已存在");

                    var entity = item.Adapt<TaktPlant>();
                    entity.PlantStatus = item.PlantStatus >= 0 ? item.PlantStatus : 0;
                    await _plantRepository.CreateAsync(entity);
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
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出工厂
    /// </summary>
    /// <param name="query">工厂查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktPlantQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        var list = await _plantRepository.FindAsync(predicate);

        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPlantExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "工厂数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "工厂导出" : fileName);
        }

        var exportData = list.Select(p =>
        {
            var dto = p.Adapt<TaktPlantExportDto>();
            dto.PlantStatus = GetPlantStatusString(p.PlantStatus);
            return dto;
        }).ToList();

        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "工厂数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "工厂导出" : fileName);
    }

    private static Expression<Func<TaktPlant, bool>> QueryExpression(TaktPlantQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPlant>();
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
            exp = exp.And(x => (x.PlantCode != null && x.PlantCode.Contains(queryDto.KeyWords)) || (x.PlantName != null && x.PlantName.Contains(queryDto.KeyWords)));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PlantCode), x => x.PlantCode != null && x.PlantCode.Contains(queryDto!.PlantCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PlantName), x => x.PlantName != null && x.PlantName.Contains(queryDto!.PlantName!));
        exp = exp.AndIF(queryDto?.PlantStatus.HasValue == true, x => x.PlantStatus == queryDto!.PlantStatus!.Value);
        return exp.ToExpression();
    }

    private static string GetPlantStatusString(int status) =>
        status switch { 0 => "启用", 1 => "禁用", _ => "未知" };
}
