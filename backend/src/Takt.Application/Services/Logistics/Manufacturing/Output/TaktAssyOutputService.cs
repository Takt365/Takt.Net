// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：TaktAssyOutputService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：组立日报表应用服务，提供AssyOutput管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// 组立日报表应用服务
/// </summary>
public class TaktAssyOutputService : TaktServiceBase, ITaktAssyOutputService
{
    private readonly ITaktRepository<TaktAssyOutput> _repository;
    private readonly ITaktRepository<TaktAssyOutputDetail> _assyOutputDetailRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">AssyOutput仓储</param>
    /// <param name="assyOutputDetailRepository">AssyOutputDetail仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAssyOutputService(
        ITaktRepository<TaktAssyOutput> repository,
        ITaktRepository<TaktAssyOutputDetail> assyOutputDetailRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _assyOutputDetailRepository = assyOutputDetailRepository;
    }


    /// <summary>
    /// 获取组立日报表(AssyOutput)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAssyOutputDto>> GetAssyOutputListAsync(TaktAssyOutputQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAssyOutputDto>.Create(
            data.Adapt<List<TaktAssyOutputDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取组立日报表(AssyOutput)
    /// </summary>
    /// <param name="id">组立日报表(AssyOutput)ID</param>
    /// <returns>组立日报表(AssyOutput)DTO</returns>
    public async Task<TaktAssyOutputDto?> GetAssyOutputByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktAssyOutputDto>();
        
        // 手动加载子表
        dto.AssyOutputDetails = (await _assyOutputDetailRepository.FindAsync(x => x.AssyOutputId == id && x.IsDeleted == 0))
            .Adapt<List<TaktAssyOutputDetailDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取组立日报表(AssyOutput)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>组立日报表(AssyOutput)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetAssyOutputOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建组立日报表(AssyOutput)
    /// </summary>
    /// <param name="dto">创建组立日报表(AssyOutput)DTO</param>
    /// <returns>组立日报表(AssyOutput)DTO</returns>
    public async Task<TaktAssyOutputDto> CreateAssyOutputAsync(TaktAssyOutputCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PlantCode, dto.PlantCode, null, $"组立日报表编码 {dto.PlantCode} 已存在");

        var entity = dto.Adapt<TaktAssyOutput>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建AssyOutputDetail列表
            if (dto.AssyOutputDetails != null && dto.AssyOutputDetails.Count > 0)
            {
                var assyOutputDetailList = dto.AssyOutputDetails.Select(x => {
                    var childEntity = x.Adapt<TaktAssyOutputDetail>();
                    childEntity.AssyOutputId = entity.Id;
                    return childEntity;
                }).ToList();
                await _assyOutputDetailRepository.CreateRangeBulkAsync(assyOutputDetailList);
            }
        }

        return (await GetAssyOutputByIdAsync(entity.Id)) ?? entity.Adapt<TaktAssyOutputDto>();
    }


    /// <summary>
    /// 更新组立日报表(AssyOutput)
    /// </summary>
    /// <param name="id">组立日报表(AssyOutput)ID</param>
    /// <param name="dto">更新组立日报表(AssyOutput)DTO</param>
    /// <returns>组立日报表(AssyOutput)DTO</returns>
    public async Task<TaktAssyOutputDto> UpdateAssyOutputAsync(long id, TaktAssyOutputUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.assyoutputNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PlantCode, dto.PlantCode, id, $"组立日报表编码 {dto.PlantCode} 已存在");

        dto.Adapt(entity, typeof(TaktAssyOutputUpdateDto), typeof(TaktAssyOutput));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的AssyOutputDetail列表
        var oldAssyOutputDetails = await _assyOutputDetailRepository.FindAsync(x => x.AssyOutputId == id && x.IsDeleted == 0);
        if (oldAssyOutputDetails != null && oldAssyOutputDetails.Count > 0)
        {
            foreach (var oldAssyOutputDetail in oldAssyOutputDetails)
            {
                oldAssyOutputDetail.IsDeleted = 1;
            }
            await _assyOutputDetailRepository.UpdateRangeBulkAsync(oldAssyOutputDetails);
        }

        // 创建新的AssyOutputDetail列表
        if (dto.AssyOutputDetails != null && dto.AssyOutputDetails.Count > 0)
        {
            var assyOutputDetailList = dto.AssyOutputDetails.Select(x => {
                var childEntity = x.Adapt<TaktAssyOutputDetail>();
                childEntity.AssyOutputId = id;
                return childEntity;
            }).ToList();
            await _assyOutputDetailRepository.CreateRangeBulkAsync(assyOutputDetailList);
        }


        return (await GetAssyOutputByIdAsync(id)) ?? entity.Adapt<TaktAssyOutputDto>();
    }


    /// <summary>
    /// 删除组立日报表(AssyOutput)
    /// </summary>
    /// <param name="id">组立日报表(AssyOutput)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAssyOutputByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.assyoutputNotFound");
        
        // 级联删除子表数据
        // 级联删除AssyOutputDetail列表
        var assyOutputDetails = await _assyOutputDetailRepository.FindAsync(x => x.AssyOutputId == id && x.IsDeleted == 0);
        if (assyOutputDetails != null && assyOutputDetails.Count > 0)
        {
            foreach (var assyOutputDetail in assyOutputDetails)
            {
                assyOutputDetail.IsDeleted = 1;
            }
            await _assyOutputDetailRepository.UpdateRangeBulkAsync(assyOutputDetails);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除组立日报表(AssyOutput)
    /// </summary>
    /// <param name="ids">组立日报表(AssyOutput)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAssyOutputBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktAssyOutput>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除AssyOutputDetail列表
        var assyOutputDetailsToDelete = new List<TaktAssyOutputDetail>();
        foreach (var id in idList)
        {
            var assyOutputDetails = await _assyOutputDetailRepository.FindAsync(x => x.AssyOutputId == id && x.IsDeleted == 0);
            if (assyOutputDetails != null && assyOutputDetails.Count > 0)
            {
                assyOutputDetailsToDelete.AddRange(assyOutputDetails);
            }
        }
        
        if (assyOutputDetailsToDelete.Count > 0)
        {
            foreach (var assyOutputDetail in assyOutputDetailsToDelete)
            {
                assyOutputDetail.IsDeleted = 1;
            }
            await _assyOutputDetailRepository.UpdateRangeBulkAsync(assyOutputDetailsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 Status = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.Status = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新组立日报表(AssyOutput)状态
    /// </summary>
    /// <param name="dto">组立日报表(AssyOutput)状态DTO</param>
    /// <returns>组立日报表(AssyOutput)DTO</returns>
    public async Task<TaktAssyOutputDto> UpdateAssyOutputStatusAsync(TaktAssyOutputStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.AssyOutputId);
        if (entity == null)
            throw new TaktBusinessException("validation.assyoutputNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAssyOutputByIdAsync(entity.Id) ?? entity.Adapt<TaktAssyOutputDto>();
    }


    /// <summary>
    /// 获取组立日报表(AssyOutput)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetAssyOutputTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAssyOutput));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAssyOutputTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入组立日报表(AssyOutput)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAssyOutputAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktAssyOutput));
        var importData = await TaktExcelHelper.ImportAsync<TaktAssyOutputImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktAssyOutput>();
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
    /// 导出组立日报表(AssyOutput)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAssyOutputAsync(TaktAssyOutputQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAssyOutputQueryDto());
        List<TaktAssyOutput> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAssyOutput));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAssyOutputExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktAssyOutputExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建组立日报表查询表达式
    /// </summary>
    /// <param name="queryDto">组立日报表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktAssyOutput, bool>> QueryExpression(TaktAssyOutputQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAssyOutput>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.ProdCategory!.Contains(queryDto.KeyWords) ||
                x.ProdLine!.Contains(queryDto.KeyWords) ||
                x.ProdOrderType!.Contains(queryDto.KeyWords) ||
                x.ProdOrderCode!.Contains(queryDto.KeyWords) ||
                x.ModelCode!.Contains(queryDto.KeyWords) ||
                x.MaterialCode!.Contains(queryDto.KeyWords) ||
                x.BatchNo!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProdCategory))
        {
            exp = exp.And(x => x.ProdCategory!.Contains(queryDto.ProdCategory));
        }

        if (queryDto?.ProdDate.HasValue == true)
        {
            exp = exp.And(x => x.ProdDate == queryDto.ProdDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.ProdLine))
        {
            exp = exp.And(x => x.ProdLine!.Contains(queryDto.ProdLine));
        }

        if (queryDto?.DirectLabor.HasValue == true)
        {
            exp = exp.And(x => x.DirectLabor == queryDto.DirectLabor);
        }

        if (queryDto?.IndirectLabor.HasValue == true)
        {
            exp = exp.And(x => x.IndirectLabor == queryDto.IndirectLabor);
        }

        if (queryDto?.ShiftNo.HasValue == true)
        {
            exp = exp.And(x => x.ShiftNo == queryDto.ShiftNo);
        }

        if (!string.IsNullOrEmpty(queryDto?.ProdOrderType))
        {
            exp = exp.And(x => x.ProdOrderType!.Contains(queryDto.ProdOrderType));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProdOrderCode))
        {
            exp = exp.And(x => x.ProdOrderCode!.Contains(queryDto.ProdOrderCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ModelCode))
        {
            exp = exp.And(x => x.ModelCode!.Contains(queryDto.ModelCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialCode))
        {
            exp = exp.And(x => x.MaterialCode!.Contains(queryDto.MaterialCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.BatchNo))
        {
            exp = exp.And(x => x.BatchNo!.Contains(queryDto.BatchNo));
        }

        if (queryDto?.ProdOrderQty.HasValue == true)
        {
            exp = exp.And(x => x.ProdOrderQty == queryDto.ProdOrderQty);
        }

        if (queryDto?.StdMinutes.HasValue == true)
        {
            exp = exp.And(x => x.StdMinutes == queryDto.StdMinutes);
        }

        if (queryDto?.StdCapacity.HasValue == true)
        {
            exp = exp.And(x => x.StdCapacity == queryDto.StdCapacity);
        }

        if (queryDto?.Status.HasValue == true)
        {
            exp = exp.And(x => x.Status == queryDto.Status);
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

        // ProdDate 日期范围查询
        if (queryDto?.ProdDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ProdDate >= queryDto.ProdDateStart);
        }
        if (queryDto?.ProdDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ProdDate <= queryDto.ProdDateEnd);
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
