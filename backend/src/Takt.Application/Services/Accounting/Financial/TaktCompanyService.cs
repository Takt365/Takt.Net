// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：TaktCompanyService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：公司信息表应用服务，提供Company管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

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
/// 公司信息表应用服务
/// </summary>
public class TaktCompanyService : TaktServiceBase, ITaktCompanyService
{
    private readonly ITaktRepository<TaktCompany> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Company仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCompanyService(
        ITaktRepository<TaktCompany> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取公司信息表(Company)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktCompanyDto>> GetCompanyListAsync(TaktCompanyQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCompanyDto>.Create(
            data.Adapt<List<TaktCompanyDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取公司信息表(Company)
    /// </summary>
    /// <param name="id">公司信息表(Company)ID</param>
    /// <returns>公司信息表(Company)DTO</returns>
    public async Task<TaktCompanyDto?> GetCompanyByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktCompanyDto>();
    }


    /// <summary>
    /// 获取公司信息表(Company)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>公司信息表(Company)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetCompanyOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.CompanyStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.CompanyName ?? string.Empty,
            DictValue = x.CompanyCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建公司信息表(Company)
    /// </summary>
    /// <param name="dto">创建公司信息表(Company)DTO</param>
    /// <returns>公司信息表(Company)DTO</returns>
    public async Task<TaktCompanyDto> CreateCompanyAsync(TaktCompanyCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.CompanyCode, dto.CompanyCode, null, $"公司信息表编码 {dto.CompanyCode} 已存在");

        var entity = dto.Adapt<TaktCompany>();
        entity = await _repository.CreateAsync(entity);
        return (await GetCompanyByIdAsync(entity.Id)) ?? entity.Adapt<TaktCompanyDto>();
    }


    /// <summary>
    /// 更新公司信息表(Company)
    /// </summary>
    /// <param name="id">公司信息表(Company)ID</param>
    /// <param name="dto">更新公司信息表(Company)DTO</param>
    /// <returns>公司信息表(Company)DTO</returns>
    public async Task<TaktCompanyDto> UpdateCompanyAsync(long id, TaktCompanyUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.companyNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.CompanyCode, dto.CompanyCode, id, $"公司信息表编码 {dto.CompanyCode} 已存在");

        dto.Adapt(entity, typeof(TaktCompanyUpdateDto), typeof(TaktCompany));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetCompanyByIdAsync(id)) ?? entity.Adapt<TaktCompanyDto>();
    }


    /// <summary>
    /// 删除公司信息表(Company)
    /// </summary>
    /// <param name="id">公司信息表(Company)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteCompanyByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.companyNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.CompanyStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除公司信息表(Company)
    /// </summary>
    /// <param name="ids">公司信息表(Company)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteCompanyBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktCompany>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 CompanyStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.CompanyStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新公司信息表(Company)状态
    /// </summary>
    /// <param name="dto">公司信息表(Company)状态DTO</param>
    /// <returns>公司信息表(Company)DTO</returns>
    public async Task<TaktCompanyDto> UpdateCompanyStatusAsync(TaktCompanyStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CompanyId);
        if (entity == null)
            throw new TaktBusinessException("validation.companyNotFound");
        entity.CompanyStatus = dto.CompanyStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCompanyByIdAsync(entity.Id) ?? entity.Adapt<TaktCompanyDto>();
    }


    /// <summary>
    /// 更新公司信息表(Company)排序
    /// </summary>
    /// <param name="dto">公司信息表(Company)排序DTO</param>
    /// <returns>公司信息表(Company)DTO</returns>
    public async Task<TaktCompanyDto> UpdateCompanySortAsync(TaktCompanySortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CompanyId);
        if (entity == null)
            throw new TaktBusinessException("validation.companyNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCompanyByIdAsync(entity.Id) ?? entity.Adapt<TaktCompanyDto>();
    }


    /// <summary>
    /// 获取公司信息表(Company)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetCompanyTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktCompany));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktCompanyTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入公司信息表(Company)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportCompanyAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktCompany));
        var importData = await TaktExcelHelper.ImportAsync<TaktCompanyImportDto>(fileStream, excelSheet);
        
        var successCount = 0;
        var failCount = 0;
        var errors = new List<string>();
        var rowIndex = 0;

        foreach (var item in importData)
        {
            rowIndex++;
            try
            {
                // TODO: 添加必要的验证逻辑
                var entity = item.Adapt<TaktCompany>();
                await _repository.CreateAsync(entity);
                successCount++;
            }
            catch (Exception ex)
            {
                errors.Add($"行{rowIndex}: {ex.Message}");
                failCount++;
            }
        }

        return (successCount, failCount, errors);
    }


    /// <summary>
    /// 导出公司信息表(Company)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportCompanyAsync(TaktCompanyQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktCompanyQueryDto());
        List<TaktCompany> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktCompany));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktCompanyExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktCompanyExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建公司信息表查询表达式
    /// </summary>
    /// <param name="queryDto">公司信息表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktCompany, bool>> QueryExpression(TaktCompanyQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktCompany>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CompanyCode!.Contains(queryDto.KeyWords) ||
                x.CompanyName!.Contains(queryDto.KeyWords) ||
                x.CompanyShortName!.Contains(queryDto.KeyWords) ||
                x.RegistrationRegion!.Contains(queryDto.KeyWords) ||
                x.RegistrationProvince!.Contains(queryDto.KeyWords) ||
                x.RegistrationCity!.Contains(queryDto.KeyWords) ||
                x.RegistrationAddress!.Contains(queryDto.KeyWords) ||
                x.BusinessRegion!.Contains(queryDto.KeyWords) ||
                x.BusinessProvince!.Contains(queryDto.KeyWords) ||
                x.BusinessCity!.Contains(queryDto.KeyWords) ||
                x.BusinessAddress!.Contains(queryDto.KeyWords) ||
                x.CompanyPhone!.Contains(queryDto.KeyWords) ||
                x.CompanyEmail!.Contains(queryDto.KeyWords) ||
                x.CompanyFax!.Contains(queryDto.KeyWords) ||
                x.CompanyWebsite!.Contains(queryDto.KeyWords) ||
                x.UnifiedSocialCreditCode!.Contains(queryDto.KeyWords) ||
                x.TaxRegistrationNumber!.Contains(queryDto.KeyWords) ||
                x.LegalRepresentative!.Contains(queryDto.KeyWords) ||
                x.CompanyManager!.Contains(queryDto.KeyWords) ||
                x.EnterpriseNature!.Contains(queryDto.KeyWords) ||
                x.IndustryAttribute!.Contains(queryDto.KeyWords) ||
                x.EnterpriseScale!.Contains(queryDto.KeyWords) ||
                x.BusinessScope!.Contains(queryDto.KeyWords) ||
                x.RelatedPlant!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyCode))
        {
            exp = exp.And(x => x.CompanyCode!.Contains(queryDto.CompanyCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyName))
        {
            exp = exp.And(x => x.CompanyName!.Contains(queryDto.CompanyName));
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyShortName))
        {
            exp = exp.And(x => x.CompanyShortName!.Contains(queryDto.CompanyShortName));
        }

        if (!string.IsNullOrEmpty(queryDto?.RegistrationRegion))
        {
            exp = exp.And(x => x.RegistrationRegion!.Contains(queryDto.RegistrationRegion));
        }

        if (!string.IsNullOrEmpty(queryDto?.RegistrationProvince))
        {
            exp = exp.And(x => x.RegistrationProvince!.Contains(queryDto.RegistrationProvince));
        }

        if (!string.IsNullOrEmpty(queryDto?.RegistrationCity))
        {
            exp = exp.And(x => x.RegistrationCity!.Contains(queryDto.RegistrationCity));
        }

        if (!string.IsNullOrEmpty(queryDto?.RegistrationAddress))
        {
            exp = exp.And(x => x.RegistrationAddress!.Contains(queryDto.RegistrationAddress));
        }

        if (!string.IsNullOrEmpty(queryDto?.BusinessRegion))
        {
            exp = exp.And(x => x.BusinessRegion!.Contains(queryDto.BusinessRegion));
        }

        if (!string.IsNullOrEmpty(queryDto?.BusinessProvince))
        {
            exp = exp.And(x => x.BusinessProvince!.Contains(queryDto.BusinessProvince));
        }

        if (!string.IsNullOrEmpty(queryDto?.BusinessCity))
        {
            exp = exp.And(x => x.BusinessCity!.Contains(queryDto.BusinessCity));
        }

        if (!string.IsNullOrEmpty(queryDto?.BusinessAddress))
        {
            exp = exp.And(x => x.BusinessAddress!.Contains(queryDto.BusinessAddress));
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyPhone))
        {
            exp = exp.And(x => x.CompanyPhone!.Contains(queryDto.CompanyPhone));
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyEmail))
        {
            exp = exp.And(x => x.CompanyEmail!.Contains(queryDto.CompanyEmail));
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyFax))
        {
            exp = exp.And(x => x.CompanyFax!.Contains(queryDto.CompanyFax));
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyWebsite))
        {
            exp = exp.And(x => x.CompanyWebsite!.Contains(queryDto.CompanyWebsite));
        }

        if (!string.IsNullOrEmpty(queryDto?.UnifiedSocialCreditCode))
        {
            exp = exp.And(x => x.UnifiedSocialCreditCode!.Contains(queryDto.UnifiedSocialCreditCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.TaxRegistrationNumber))
        {
            exp = exp.And(x => x.TaxRegistrationNumber!.Contains(queryDto.TaxRegistrationNumber));
        }

        if (!string.IsNullOrEmpty(queryDto?.LegalRepresentative))
        {
            exp = exp.And(x => x.LegalRepresentative!.Contains(queryDto.LegalRepresentative));
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyManager))
        {
            exp = exp.And(x => x.CompanyManager!.Contains(queryDto.CompanyManager));
        }

        if (queryDto?.RegisteredCapital.HasValue == true)
        {
            exp = exp.And(x => x.RegisteredCapital == queryDto.RegisteredCapital);
        }

        if (queryDto?.EstablishmentDate.HasValue == true)
        {
            exp = exp.And(x => x.EstablishmentDate == queryDto.EstablishmentDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.EnterpriseNature))
        {
            exp = exp.And(x => x.EnterpriseNature!.Contains(queryDto.EnterpriseNature));
        }

        if (!string.IsNullOrEmpty(queryDto?.IndustryAttribute))
        {
            exp = exp.And(x => x.IndustryAttribute!.Contains(queryDto.IndustryAttribute));
        }

        if (!string.IsNullOrEmpty(queryDto?.EnterpriseScale))
        {
            exp = exp.And(x => x.EnterpriseScale!.Contains(queryDto.EnterpriseScale));
        }

        if (!string.IsNullOrEmpty(queryDto?.BusinessScope))
        {
            exp = exp.And(x => x.BusinessScope!.Contains(queryDto.BusinessScope));
        }

        if (!string.IsNullOrEmpty(queryDto?.RelatedPlant))
        {
            exp = exp.And(x => x.RelatedPlant!.Contains(queryDto.RelatedPlant));
        }

        if (queryDto?.CompanyStatus.HasValue == true)
        {
            exp = exp.And(x => x.CompanyStatus == queryDto.CompanyStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.Remark))
        {
            exp = exp.And(x => x.Remark!.Contains(queryDto.Remark));
        }

        if (!string.IsNullOrEmpty(queryDto?.ExtFieldJson))
        {
            exp = exp.And(x => x.ExtFieldJson!.Contains(queryDto.ExtFieldJson));
        }

        if (queryDto?.CreatedById.HasValue == true)
        {
            exp = exp.And(x => x.CreatedById == queryDto.CreatedById);
        }

        if (!string.IsNullOrEmpty(queryDto?.CreatedBy))
        {
            exp = exp.And(x => x.CreatedBy!.Contains(queryDto.CreatedBy));
        }

        if (queryDto?.CreatedAt.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt == queryDto.CreatedAt);
        }

        // EstablishmentDate 日期范围查询
        if (queryDto?.EstablishmentDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EstablishmentDate >= queryDto.EstablishmentDateStart);
        }
        if (queryDto?.EstablishmentDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EstablishmentDate <= queryDto.EstablishmentDateEnd);
        }

        // CreatedAt 日期范围查询
        if (queryDto?.CreatedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt >= queryDto.CreatedAtStart);
        }
        if (queryDto?.CreatedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt <= queryDto.CreatedAtEnd);
        }

        return exp.ToExpression();
    }
}
