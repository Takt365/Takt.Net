// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：TaktCompanyService.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt公司应用服务
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using SqlSugar;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Application.Services;
using Takt.Domain.Entities.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// Takt公司应用服务
/// </summary>
public class TaktCompanyService : TaktServiceBase, ITaktCompanyService
{
    private readonly ITaktRepository<TaktCompany> _companyRepository;

    public TaktCompanyService(
        ITaktRepository<TaktCompany> companyRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _companyRepository = companyRepository;
    }

    public async Task<TaktPagedResult<TaktCompanyDto>> GetListAsync(TaktCompanyQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _companyRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCompanyDto>.Create(data.Adapt<List<TaktCompanyDto>>(), total, queryDto.PageIndex, queryDto.PageSize);
    }

    public async Task<TaktCompanyDto?> GetByIdAsync(long id)
    {
        var entity = await _companyRepository.GetByIdAsync(id);
        return entity?.Adapt<TaktCompanyDto>();
    }

    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var list = await _companyRepository.FindAsync(c => c.IsDeleted == 0 && c.CompanyStatus == 0);
        return list.OrderBy(c => c.OrderNum).ThenBy(c => c.CreateTime)
            .Select(c => new TaktSelectOption { DictLabel = c.CompanyName, DictValue = c.Id, ExtLabel = c.CompanyCode, OrderNum = c.OrderNum })
            .ToList();
    }

    public async Task<TaktCompanyDto> CreateAsync(TaktCompanyCreateDto dto)
    {
        if (string.IsNullOrEmpty(dto.CompanyCode) || dto.CompanyCode.Trim().Length != 4)
            throw new TaktBusinessException("公司代码必须为4位");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_companyRepository, c => c.CompanyCode, dto.CompanyCode, null, null, $"公司代码 {dto.CompanyCode} 已存在");
        var entity = dto.Adapt<TaktCompany>();
        entity.CompanyStatus = 0;
        entity = await _companyRepository.CreateAsync(entity);
        return (await GetByIdAsync(entity.Id))!;
    }

    public async Task<TaktCompanyDto> UpdateAsync(long id, TaktCompanyUpdateDto dto)
    {
        var entity = await _companyRepository.GetByIdAsync(id);
        if (entity == null) throw new TaktBusinessException("公司不存在");
        if (string.IsNullOrEmpty(dto.CompanyCode) || dto.CompanyCode.Trim().Length != 4)
            throw new TaktBusinessException("公司代码必须为4位");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_companyRepository, c => c.CompanyCode, dto.CompanyCode, null, id, $"公司代码 {dto.CompanyCode} 已存在");
        dto.Adapt(entity, typeof(TaktCompanyUpdateDto), typeof(TaktCompany));
        entity.UpdateTime = DateTime.Now;
        await _companyRepository.UpdateAsync(entity);
        return (await GetByIdAsync(id))!;
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _companyRepository.GetByIdAsync(id);
        if (entity == null) throw new TaktBusinessException("公司不存在");
        entity.CompanyStatus = 1;
        entity.UpdateTime = DateTime.Now;
        await _companyRepository.UpdateAsync(entity);
        await _companyRepository.DeleteAsync(id);
    }

    public async Task<TaktCompanyDto> UpdateStatusAsync(TaktCompanyStatusDto dto)
    {
        var entity = await _companyRepository.GetByIdAsync(dto.CompanyId);
        if (entity == null) throw new TaktBusinessException("公司不存在");
        entity.CompanyStatus = dto.CompanyStatus;
        entity.UpdateTime = DateTime.Now;
        await _companyRepository.UpdateAsync(entity);
        return entity.Adapt<TaktCompanyDto>();
    }

    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktCompanyTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "公司导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "公司导入模板" : fileName);
    }

    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            var importData = await TaktExcelHelper.ImportAsync<TaktCompanyImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "公司导入模板" : sheetName);

            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.CompanyCode))
                    {
                        errors.Add($"第{index}行：公司代码不能为空");
                        fail++;
                        continue;
                    }
                    if (item.CompanyCode.Trim().Length != 4)
                    {
                        errors.Add($"第{index}行：公司代码必须为4位");
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.CompanyName))
                    {
                        errors.Add($"第{index}行：公司名称不能为空");
                        fail++;
                        continue;
                    }

                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_companyRepository, c => c.CompanyCode, item.CompanyCode, null, null, $"第{index}行：公司代码 {item.CompanyCode} 已存在");

                    var entity = item.Adapt<TaktCompany>();
                    entity.CompanyStatus = item.CompanyStatus >= 0 ? item.CompanyStatus : 0;
                    await _companyRepository.CreateAsync(entity);
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

    public async Task<(string fileName, byte[] content)> ExportAsync(TaktCompanyQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        var list = await _companyRepository.FindAsync(predicate);

        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktCompanyExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "公司数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "公司导出" : fileName);
        }

        var exportData = list.Select(c =>
        {
            var dto = c.Adapt<TaktCompanyExportDto>();
            dto.CompanyStatus = GetCompanyStatusString(c.CompanyStatus);
            return dto;
        }).ToList();

        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "公司数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "公司导出" : fileName);
    }

    private static Expression<Func<TaktCompany, bool>> QueryExpression(TaktCompanyQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktCompany>();
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
            exp = exp.And(x => (x.CompanyCode != null && x.CompanyCode.Contains(queryDto.KeyWords)) || (x.CompanyName != null && x.CompanyName.Contains(queryDto.KeyWords)));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CompanyCode), x => x.CompanyCode != null && x.CompanyCode.Contains(queryDto!.CompanyCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CompanyName), x => x.CompanyName != null && x.CompanyName.Contains(queryDto!.CompanyName!));
        exp = exp.AndIF(queryDto?.CompanyStatus.HasValue == true, x => x.CompanyStatus == queryDto!.CompanyStatus!.Value);
        return exp.ToExpression();
    }

    private static string GetCompanyStatusString(int status) =>
        status switch { 0 => "启用", 1 => "禁用", _ => "未知" };
}
