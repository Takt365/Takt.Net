// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：TaktBankService.cs
// 功能描述：Takt银行应用服务
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
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// Takt银行应用服务
/// </summary>
public class TaktBankService : TaktServiceBase, ITaktBankService
{
    private readonly ITaktRepository<TaktBank> _bankRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="bankRepository">银行仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktBankService(
        ITaktRepository<TaktBank> bankRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _bankRepository = bankRepository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktBankDto>> GetListAsync(TaktBankQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _bankRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktBankDto>.Create(
            data.Adapt<List<TaktBankDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktBankDto?> GetByIdAsync(long id)
    {
        var entity = await _bankRepository.GetByIdAsync(id);
        return entity?.Adapt<TaktBankDto>();
    }

    /// <inheritdoc />
    public async Task<List<TaktSelectOption>> GetOptionsAsync(string? companyCode = null)
    {
        Expression<Func<TaktBank, bool>>? predicate = x => x.IsDeleted == 0 && x.BankStatus == 0;
        if (!string.IsNullOrWhiteSpace(companyCode))
            predicate = x => x.IsDeleted == 0 && x.BankStatus == 0 && x.CompanyCode == companyCode;
        var list = await _bankRepository.FindAsync(predicate);
        return (list ?? new List<TaktBank>())
            .OrderBy(x => x.OrderNum)
            .ThenBy(x => x.CreateTime)
            .Select(x => new TaktSelectOption { DictLabel = x.BankName, DictValue = x.Id, ExtLabel = x.BankCode, OrderNum = x.OrderNum })
            .ToList();
    }

    /// <inheritdoc />
    public async Task<TaktBankDto> CreateAsync(TaktBankCreateDto dto)
    {
        if (string.IsNullOrEmpty(dto.CompanyCode) || dto.CompanyCode.Trim().Length != 4)
            throw new TaktBusinessException("公司代码必须为4位");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_bankRepository, x => x.BankCode, dto.BankCode, null, null, $"银行编码 {dto.BankCode} 已存在");
        var entity = dto.Adapt<TaktBank>();
        entity.BankStatus = entity.BankStatus >= 0 ? entity.BankStatus : 0;
        entity = await _bankRepository.CreateAsync(entity);
        return (await GetByIdAsync(entity.Id))!;
    }

    /// <inheritdoc />
    public async Task<TaktBankDto> UpdateAsync(long id, TaktBankUpdateDto dto)
    {
        var entity = await _bankRepository.GetByIdAsync(id);
        if (entity == null) throw new TaktBusinessException("银行不存在");
        if (string.IsNullOrEmpty(dto.CompanyCode) || dto.CompanyCode.Trim().Length != 4)
            throw new TaktBusinessException("公司代码必须为4位");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_bankRepository, x => x.BankCode, dto.BankCode, null, id, $"银行编码 {dto.BankCode} 已存在");
        dto.Adapt(entity, typeof(TaktBankUpdateDto), typeof(TaktBank));
        entity.UpdateTime = DateTime.Now;
        await _bankRepository.UpdateAsync(entity);
        return (await GetByIdAsync(id))!;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(long id)
    {
        await _bankRepository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        await _bankRepository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<TaktBankDto> UpdateStatusAsync(TaktBankStatusDto dto)
    {
        var entity = await _bankRepository.GetByIdAsync(dto.BankId);
        if (entity == null) throw new TaktBusinessException("银行不存在");
        entity.BankStatus = dto.BankStatus;
        entity.UpdateTime = DateTime.Now;
        await _bankRepository.UpdateAsync(entity);
        return (await GetByIdAsync(dto.BankId))!;
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktBankTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "银行导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "银行导入模板" : fileName);
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;
        try
        {
            var importData = await TaktExcelHelper.ImportAsync<TaktBankImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "银行导入模板" : sheetName);
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
                    if (string.IsNullOrWhiteSpace(item.BankCode))
                    {
                        errors.Add($"第{index}行：银行编码不能为空");
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.BankName))
                    {
                        errors.Add($"第{index}行：银行名称不能为空");
                        fail++;
                        continue;
                    }
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_bankRepository, x => x.BankCode, item.BankCode.Trim(), null, null, $"第{index}行：银行编码 {item.BankCode} 已存在");
                    var entity = new TaktBank
                    {
                        CompanyCode = item.CompanyCode.Trim(),
                        BankCode = item.BankCode.Trim(),
                        BankName = item.BankName.Trim(),
                        ShortName = string.IsNullOrWhiteSpace(item.ShortName) ? null : item.ShortName.Trim(),
                        SwiftCode = string.IsNullOrWhiteSpace(item.SwiftCode) ? null : item.SwiftCode.Trim(),
                        Address = string.IsNullOrWhiteSpace(item.Address) ? null : item.Address.Trim(),
                        ContactPhone = string.IsNullOrWhiteSpace(item.ContactPhone) ? null : item.ContactPhone.Trim(),
                        OrderNum = item.OrderNum,
                        BankStatus = item.BankStatus >= 0 ? item.BankStatus : 0
                    };
                    await _bankRepository.CreateAsync(entity);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入银行失败（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入银行异常（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"导入银行过程发生错误: {ex.Message}");
            errors.Add($"导入过程发生错误：{ex.Message}");
        }
        return (success, fail, errors);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktBankQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        var list = predicate != null
            ? await _bankRepository.FindAsync(predicate)
            : (await _bankRepository.GetAllAsync()) ?? new List<TaktBank>();
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktBankExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "银行数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "银行导出" : fileName);
        }
        var exportData = list.Select(x =>
        {
            var dto = x.Adapt<TaktBankExportDto>();
            dto.BankStatus = GetBankStatusString(x.BankStatus);
            return dto;
        }).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "银行数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "银行导出" : fileName);
    }

    private static string GetBankStatusString(int status) =>
        status switch { 0 => "启用", 1 => "禁用", _ => "未知" };

    /// <summary>
    /// 构建查询条件
    /// </summary>
    private static Expression<Func<TaktBank, bool>>? QueryExpression(TaktBankQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktBank>();
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
            exp = exp.And(x => (x.BankCode != null && x.BankCode.Contains(queryDto.KeyWords)) || (x.BankName != null && x.BankName.Contains(queryDto.KeyWords)));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CompanyCode), x => x.CompanyCode == queryDto!.CompanyCode!);
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.BankCode), x => x.BankCode != null && x.BankCode.Contains(queryDto!.BankCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.BankName), x => x.BankName != null && x.BankName.Contains(queryDto!.BankName!));
        exp = exp.AndIF(queryDto?.BankStatus.HasValue == true, x => x.BankStatus == queryDto!.BankStatus!.Value);
        return exp.ToExpression();
    }
}
