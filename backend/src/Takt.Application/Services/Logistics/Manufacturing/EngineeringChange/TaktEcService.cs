// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：设变主表应用服务，提供Ec管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变主表应用服务
/// </summary>
public class TaktEcService : TaktServiceBase, ITaktEcService
{
    private readonly ITaktRepository<TaktEc> _repository;
    private readonly ITaktRepository<TaktEcDetail> _ecDetailRepository;
    private readonly ITaktRepository<TaktEcAttachment> _ecAttachmentRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Ec仓储</param>
    /// <param name="ecDetailRepository">EcDetail仓储</param>
    /// <param name="ecAttachmentRepository">EcAttachment仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEcService(
        ITaktRepository<TaktEc> repository,
        ITaktRepository<TaktEcDetail> ecDetailRepository,
        ITaktRepository<TaktEcAttachment> ecAttachmentRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _ecDetailRepository = ecDetailRepository;
        _ecAttachmentRepository = ecAttachmentRepository;
    }


    /// <summary>
    /// 获取设变主表(Ec)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEcDto>> GetEcListAsync(TaktEcQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEcDto>.Create(
            data.Adapt<List<TaktEcDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取设变主表(Ec)
    /// </summary>
    /// <param name="id">设变主表(Ec)ID</param>
    /// <returns>设变主表(Ec)DTO</returns>
    public async Task<TaktEcDto?> GetEcByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktEcDto>();
        
        // 手动加载子表
        dto.EcnDetails = (await _ecDetailRepository.FindAsync(x => x.EcnId == id && x.IsDeleted == 0))
            .Adapt<List<TaktEcDetailDto>>();
        dto.Attachments = (await _ecAttachmentRepository.FindAsync(x => x.EcnId == id && x.IsDeleted == 0))
            .Adapt<List<TaktEcAttachmentDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取设变主表(Ec)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>设变主表(Ec)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEcOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.ChangeStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建设变主表(Ec)
    /// </summary>
    /// <param name="dto">创建设变主表(Ec)DTO</param>
    /// <returns>设变主表(Ec)DTO</returns>
    public async Task<TaktEcDto> CreateEcAsync(TaktEcCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.EcnNo, dto.EcnNo, null, $"设变主表编码 {dto.EcnNo} 已存在");

        var entity = dto.Adapt<TaktEc>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建EcDetail列表
            if (dto.EcnDetails != null && dto.EcnDetails.Count > 0)
            {
                var ecDetailList = dto.EcnDetails.Select(x => {
                    var childEntity = x.Adapt<TaktEcDetail>();
                    childEntity.EcnId = entity.Id;
                    return childEntity;
                }).ToList();
                await _ecDetailRepository.CreateRangeBulkAsync(ecDetailList);
            }
            // 创建EcAttachment列表
            if (dto.Attachments != null && dto.Attachments.Count > 0)
            {
                var ecAttachmentList = dto.Attachments.Select(x => {
                    var childEntity = x.Adapt<TaktEcAttachment>();
                    childEntity.EcnId = entity.Id;
                    return childEntity;
                }).ToList();
                await _ecAttachmentRepository.CreateRangeBulkAsync(ecAttachmentList);
            }
        }

        return (await GetEcByIdAsync(entity.Id)) ?? entity.Adapt<TaktEcDto>();
    }


    /// <summary>
    /// 更新设变主表(Ec)
    /// </summary>
    /// <param name="id">设变主表(Ec)ID</param>
    /// <param name="dto">更新设变主表(Ec)DTO</param>
    /// <returns>设变主表(Ec)DTO</returns>
    public async Task<TaktEcDto> UpdateEcAsync(long id, TaktEcUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ecNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.EcnNo, dto.EcnNo, id, $"设变主表编码 {dto.EcnNo} 已存在");

        dto.Adapt(entity, typeof(TaktEcUpdateDto), typeof(TaktEc));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的EcDetail列表
        var oldEcDetails = await _ecDetailRepository.FindAsync(x => x.EcnId == id && x.IsDeleted == 0);
        if (oldEcDetails != null && oldEcDetails.Count > 0)
        {
            foreach (var oldEcDetail in oldEcDetails)
            {
                oldEcDetail.IsDeleted = 1;
            }
            await _ecDetailRepository.UpdateRangeBulkAsync(oldEcDetails);
        }

        // 创建新的EcDetail列表
        if (dto.EcnDetails != null && dto.EcnDetails.Count > 0)
        {
            var ecDetailList = dto.EcnDetails.Select(x => {
                var childEntity = x.Adapt<TaktEcDetail>();
                childEntity.EcnId = id;
                return childEntity;
            }).ToList();
            await _ecDetailRepository.CreateRangeBulkAsync(ecDetailList);
        }
        // 删除旧的EcAttachment列表
        var oldEcAttachments = await _ecAttachmentRepository.FindAsync(x => x.EcnId == id && x.IsDeleted == 0);
        if (oldEcAttachments != null && oldEcAttachments.Count > 0)
        {
            foreach (var oldEcAttachment in oldEcAttachments)
            {
                oldEcAttachment.IsDeleted = 1;
            }
            await _ecAttachmentRepository.UpdateRangeBulkAsync(oldEcAttachments);
        }

        // 创建新的EcAttachment列表
        if (dto.Attachments != null && dto.Attachments.Count > 0)
        {
            var ecAttachmentList = dto.Attachments.Select(x => {
                var childEntity = x.Adapt<TaktEcAttachment>();
                childEntity.EcnId = id;
                return childEntity;
            }).ToList();
            await _ecAttachmentRepository.CreateRangeBulkAsync(ecAttachmentList);
        }


        return (await GetEcByIdAsync(id)) ?? entity.Adapt<TaktEcDto>();
    }


    /// <summary>
    /// 删除设变主表(Ec)
    /// </summary>
    /// <param name="id">设变主表(Ec)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEcByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ecNotFound");
        
        // 级联删除子表数据
        // 级联删除EcDetail列表
        var ecDetails = await _ecDetailRepository.FindAsync(x => x.EcnId == id && x.IsDeleted == 0);
        if (ecDetails != null && ecDetails.Count > 0)
        {
            foreach (var ecDetail in ecDetails)
            {
                ecDetail.IsDeleted = 1;
            }
            await _ecDetailRepository.UpdateRangeBulkAsync(ecDetails);
        }
        // 级联删除EcAttachment列表
        var ecAttachments = await _ecAttachmentRepository.FindAsync(x => x.EcnId == id && x.IsDeleted == 0);
        if (ecAttachments != null && ecAttachments.Count > 0)
        {
            foreach (var ecAttachment in ecAttachments)
            {
                ecAttachment.IsDeleted = 1;
            }
            await _ecAttachmentRepository.UpdateRangeBulkAsync(ecAttachments);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.ChangeStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除设变主表(Ec)
    /// </summary>
    /// <param name="ids">设变主表(Ec)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEcBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktEc>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除EcDetail列表
        var ecDetailsToDelete = new List<TaktEcDetail>();
        foreach (var id in idList)
        {
            var ecDetails = await _ecDetailRepository.FindAsync(x => x.EcnId == id && x.IsDeleted == 0);
            if (ecDetails != null && ecDetails.Count > 0)
            {
                ecDetailsToDelete.AddRange(ecDetails);
            }
        }
        
        if (ecDetailsToDelete.Count > 0)
        {
            foreach (var ecDetail in ecDetailsToDelete)
            {
                ecDetail.IsDeleted = 1;
            }
            await _ecDetailRepository.UpdateRangeBulkAsync(ecDetailsToDelete);
        }
        // 批量级联删除EcAttachment列表
        var ecAttachmentsToDelete = new List<TaktEcAttachment>();
        foreach (var id in idList)
        {
            var ecAttachments = await _ecAttachmentRepository.FindAsync(x => x.EcnId == id && x.IsDeleted == 0);
            if (ecAttachments != null && ecAttachments.Count > 0)
            {
                ecAttachmentsToDelete.AddRange(ecAttachments);
            }
        }
        
        if (ecAttachmentsToDelete.Count > 0)
        {
            foreach (var ecAttachment in ecAttachmentsToDelete)
            {
                ecAttachment.IsDeleted = 1;
            }
            await _ecAttachmentRepository.UpdateRangeBulkAsync(ecAttachmentsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 ChangeStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.ChangeStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新设变主表(Ec)状态
    /// </summary>
    /// <param name="dto">设变主表(Ec)状态DTO</param>
    /// <returns>设变主表(Ec)DTO</returns>
    public async Task<TaktEcDto> UpdateEcChangeStatusAsync(TaktEcChangeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.EcId);
        if (entity == null)
            throw new TaktBusinessException("validation.ecNotFound");
        entity.ChangeStatus = dto.ChangeStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetEcByIdAsync(entity.Id) ?? entity.Adapt<TaktEcDto>();
    }


    /// <summary>
    /// 更新设变主表(Ec)状态
    /// </summary>
    /// <param name="dto">设变主表(Ec)状态DTO</param>
    /// <returns>设变主表(Ec)DTO</returns>
    public async Task<TaktEcDto> UpdateEcStatusAsync(TaktEcStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.EcId);
        if (entity == null)
            throw new TaktBusinessException("validation.ecNotFound");
        entity.EcStatus = dto.EcStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetEcByIdAsync(entity.Id) ?? entity.Adapt<TaktEcDto>();
    }


    /// <summary>
    /// 获取设变主表(Ec)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetEcTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEc));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEcTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入设变主表(Ec)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEcAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktEc));
        var importData = await TaktExcelHelper.ImportAsync<TaktEcImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktEc>();
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
    /// 导出设变主表(Ec)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEcAsync(TaktEcQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEcQueryDto());
        List<TaktEc> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEc));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEcExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktEcExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建设变主表查询表达式
    /// </summary>
    /// <param name="queryDto">设变主表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEc, bool>> QueryExpression(TaktEcQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktEc>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.EcnNo!.Contains(queryDto.KeyWords) ||
                x.EcnTitle!.Contains(queryDto.KeyWords) ||
                x.EcnDetailText!.Contains(queryDto.KeyWords) ||
                x.EcnLeader!.Contains(queryDto.KeyWords) ||
                x.EcnDistinction!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnNo))
        {
            exp = exp.And(x => x.EcnNo!.Contains(queryDto.EcnNo));
        }

        if (queryDto?.EcnIssueDate.HasValue == true)
        {
            exp = exp.And(x => x.EcnIssueDate == queryDto.EcnIssueDate);
        }

        if (queryDto?.ChangeStatus.HasValue == true)
        {
            exp = exp.And(x => x.ChangeStatus == queryDto.ChangeStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnTitle))
        {
            exp = exp.And(x => x.EcnTitle!.Contains(queryDto.EcnTitle));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnDetailText))
        {
            exp = exp.And(x => x.EcnDetailText!.Contains(queryDto.EcnDetailText));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnLeader))
        {
            exp = exp.And(x => x.EcnLeader!.Contains(queryDto.EcnLeader));
        }

        if (queryDto?.EcnLossAmount.HasValue == true)
        {
            exp = exp.And(x => x.EcnLossAmount == queryDto.EcnLossAmount);
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnDistinction))
        {
            exp = exp.And(x => x.EcnDistinction!.Contains(queryDto.EcnDistinction));
        }

        if (queryDto?.EffectiveDate.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate == queryDto.EffectiveDate);
        }

        if (queryDto?.EcnEntryDate.HasValue == true)
        {
            exp = exp.And(x => x.EcnEntryDate == queryDto.EcnEntryDate);
        }

        if (queryDto?.FlowInstanceId.HasValue == true)
        {
            exp = exp.And(x => x.FlowInstanceId == queryDto.FlowInstanceId);
        }

        if (queryDto?.EcStatus.HasValue == true)
        {
            exp = exp.And(x => x.EcStatus == queryDto.EcStatus);
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

        // EcnIssueDate 日期范围查询
        if (queryDto?.EcnIssueDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EcnIssueDate >= queryDto.EcnIssueDateStart);
        }
        if (queryDto?.EcnIssueDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EcnIssueDate <= queryDto.EcnIssueDateEnd);
        }

        // EffectiveDate 日期范围查询
        if (queryDto?.EffectiveDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate >= queryDto.EffectiveDateStart);
        }
        if (queryDto?.EffectiveDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate <= queryDto.EffectiveDateEnd);
        }

        // EcnEntryDate 日期范围查询
        if (queryDto?.EcnEntryDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EcnEntryDate >= queryDto.EcnEntryDateStart);
        }
        if (queryDto?.EcnEntryDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EcnEntryDate <= queryDto.EcnEntryDateEnd);
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
