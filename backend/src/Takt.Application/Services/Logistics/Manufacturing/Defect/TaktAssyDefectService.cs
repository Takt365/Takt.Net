// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Defect
// 文件名称：TaktAssyDefectService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：组立不良日报表应用服务，提供AssyDefect管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Manufacturing.Defect;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Defect;

/// <summary>
/// 组立不良日报表应用服务
/// </summary>
public class TaktAssyDefectService : TaktServiceBase, ITaktAssyDefectService
{
    private readonly ITaktRepository<TaktAssyDefect> _repository;
    private readonly ITaktRepository<TaktAssyDefectDetail> _assyDefectDetailRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">AssyDefect仓储</param>
    /// <param name="assyDefectDetailRepository">AssyDefectDetail仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAssyDefectService(
        ITaktRepository<TaktAssyDefect> repository,
        ITaktRepository<TaktAssyDefectDetail> assyDefectDetailRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _assyDefectDetailRepository = assyDefectDetailRepository;
    }


    /// <summary>
    /// 获取组立不良日报表(AssyDefect)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAssyDefectDto>> GetAssyDefectListAsync(TaktAssyDefectQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAssyDefectDto>.Create(
            data.Adapt<List<TaktAssyDefectDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取组立不良日报表(AssyDefect)
    /// </summary>
    /// <param name="id">组立不良日报表(AssyDefect)ID</param>
    /// <returns>组立不良日报表(AssyDefect)DTO</returns>
    public async Task<TaktAssyDefectDto?> GetAssyDefectByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktAssyDefectDto>();
        
        // 手动加载子表
        dto.AssyDefectDetails = (await _assyDefectDetailRepository.FindAsync(x => x.AssyDefectId == id && x.IsDeleted == 0))
            .Adapt<List<TaktAssyDefectDetailDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取组立不良日报表(AssyDefect)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>组立不良日报表(AssyDefect)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetAssyDefectOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建组立不良日报表(AssyDefect)
    /// </summary>
    /// <param name="dto">创建组立不良日报表(AssyDefect)DTO</param>
    /// <returns>组立不良日报表(AssyDefect)DTO</returns>
    public async Task<TaktAssyDefectDto> CreateAssyDefectAsync(TaktAssyDefectCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PlantCode, dto.PlantCode, null, $"组立不良日报表编码 {dto.PlantCode} 已存在");

        var entity = dto.Adapt<TaktAssyDefect>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建AssyDefectDetail列表
            if (dto.AssyDefectDetails != null && dto.AssyDefectDetails.Count > 0)
            {
                var assyDefectDetailList = dto.AssyDefectDetails.Select(x => {
                    var childEntity = x.Adapt<TaktAssyDefectDetail>();
                    childEntity.AssyDefectId = entity.Id;
                    return childEntity;
                }).ToList();
                await _assyDefectDetailRepository.CreateRangeBulkAsync(assyDefectDetailList);
            }
        }

        return (await GetAssyDefectByIdAsync(entity.Id)) ?? entity.Adapt<TaktAssyDefectDto>();
    }


    /// <summary>
    /// 更新组立不良日报表(AssyDefect)
    /// </summary>
    /// <param name="id">组立不良日报表(AssyDefect)ID</param>
    /// <param name="dto">更新组立不良日报表(AssyDefect)DTO</param>
    /// <returns>组立不良日报表(AssyDefect)DTO</returns>
    public async Task<TaktAssyDefectDto> UpdateAssyDefectAsync(long id, TaktAssyDefectUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.assydefectNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PlantCode, dto.PlantCode, id, $"组立不良日报表编码 {dto.PlantCode} 已存在");

        dto.Adapt(entity, typeof(TaktAssyDefectUpdateDto), typeof(TaktAssyDefect));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的AssyDefectDetail列表
        var oldAssyDefectDetails = await _assyDefectDetailRepository.FindAsync(x => x.AssyDefectId == id && x.IsDeleted == 0);
        if (oldAssyDefectDetails != null && oldAssyDefectDetails.Count > 0)
        {
            foreach (var oldAssyDefectDetail in oldAssyDefectDetails)
            {
                oldAssyDefectDetail.IsDeleted = 1;
            }
            await _assyDefectDetailRepository.UpdateRangeBulkAsync(oldAssyDefectDetails);
        }

        // 创建新的AssyDefectDetail列表
        if (dto.AssyDefectDetails != null && dto.AssyDefectDetails.Count > 0)
        {
            var assyDefectDetailList = dto.AssyDefectDetails.Select(x => {
                var childEntity = x.Adapt<TaktAssyDefectDetail>();
                childEntity.AssyDefectId = id;
                return childEntity;
            }).ToList();
            await _assyDefectDetailRepository.CreateRangeBulkAsync(assyDefectDetailList);
        }


        return (await GetAssyDefectByIdAsync(id)) ?? entity.Adapt<TaktAssyDefectDto>();
    }


    /// <summary>
    /// 删除组立不良日报表(AssyDefect)
    /// </summary>
    /// <param name="id">组立不良日报表(AssyDefect)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAssyDefectByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.assydefectNotFound");
        
        // 级联删除子表数据
        // 级联删除AssyDefectDetail列表
        var assyDefectDetails = await _assyDefectDetailRepository.FindAsync(x => x.AssyDefectId == id && x.IsDeleted == 0);
        if (assyDefectDetails != null && assyDefectDetails.Count > 0)
        {
            foreach (var assyDefectDetail in assyDefectDetails)
            {
                assyDefectDetail.IsDeleted = 1;
            }
            await _assyDefectDetailRepository.UpdateRangeBulkAsync(assyDefectDetails);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除组立不良日报表(AssyDefect)
    /// </summary>
    /// <param name="ids">组立不良日报表(AssyDefect)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAssyDefectBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktAssyDefect>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除AssyDefectDetail列表
        var assyDefectDetailsToDelete = new List<TaktAssyDefectDetail>();
        foreach (var id in idList)
        {
            var assyDefectDetails = await _assyDefectDetailRepository.FindAsync(x => x.AssyDefectId == id && x.IsDeleted == 0);
            if (assyDefectDetails != null && assyDefectDetails.Count > 0)
            {
                assyDefectDetailsToDelete.AddRange(assyDefectDetails);
            }
        }
        
        if (assyDefectDetailsToDelete.Count > 0)
        {
            foreach (var assyDefectDetail in assyDefectDetailsToDelete)
            {
                assyDefectDetail.IsDeleted = 1;
            }
            await _assyDefectDetailRepository.UpdateRangeBulkAsync(assyDefectDetailsToDelete);
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
    /// 更新组立不良日报表(AssyDefect)状态
    /// </summary>
    /// <param name="dto">组立不良日报表(AssyDefect)状态DTO</param>
    /// <returns>组立不良日报表(AssyDefect)DTO</returns>
    public async Task<TaktAssyDefectDto> UpdateAssyDefectStatusAsync(TaktAssyDefectStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.AssyDefectId);
        if (entity == null)
            throw new TaktBusinessException("validation.assydefectNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAssyDefectByIdAsync(entity.Id) ?? entity.Adapt<TaktAssyDefectDto>();
    }


    /// <summary>
    /// 获取组立不良日报表(AssyDefect)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetAssyDefectTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAssyDefect));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAssyDefectTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入组立不良日报表(AssyDefect)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAssyDefectAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktAssyDefect));
        var importData = await TaktExcelHelper.ImportAsync<TaktAssyDefectImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktAssyDefect>();
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
    /// 导出组立不良日报表(AssyDefect)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAssyDefectAsync(TaktAssyDefectQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAssyDefectQueryDto());
        List<TaktAssyDefect> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAssyDefect));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAssyDefectExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktAssyDefectExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建组立不良日报表查询表达式
    /// </summary>
    /// <param name="queryDto">组立不良日报表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktAssyDefect, bool>> QueryExpression(TaktAssyDefectQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAssyDefect>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.ProdCategory!.Contains(queryDto.KeyWords) ||
                x.ProdLine!.Contains(queryDto.KeyWords) ||
                x.ProdOrderCode!.Contains(queryDto.KeyWords) ||
                x.ModelCode!.Contains(queryDto.KeyWords) ||
                x.BatchNo!.Contains(queryDto.KeyWords) ||
                x.MaterialCode!.Contains(queryDto.KeyWords) ||
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

        if (queryDto?.ShiftNo.HasValue == true)
        {
            exp = exp.And(x => x.ShiftNo == queryDto.ShiftNo);
        }

        if (!string.IsNullOrEmpty(queryDto?.ProdOrderCode))
        {
            exp = exp.And(x => x.ProdOrderCode!.Contains(queryDto.ProdOrderCode));
        }

        if (queryDto?.ProdOrderQty.HasValue == true)
        {
            exp = exp.And(x => x.ProdOrderQty == queryDto.ProdOrderQty);
        }

        if (!string.IsNullOrEmpty(queryDto?.ModelCode))
        {
            exp = exp.And(x => x.ModelCode!.Contains(queryDto.ModelCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.BatchNo))
        {
            exp = exp.And(x => x.BatchNo!.Contains(queryDto.BatchNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialCode))
        {
            exp = exp.And(x => x.MaterialCode!.Contains(queryDto.MaterialCode));
        }

        if (queryDto?.ProdActualQty.HasValue == true)
        {
            exp = exp.And(x => x.ProdActualQty == queryDto.ProdActualQty);
        }

        if (queryDto?.GoodQuantity.HasValue == true)
        {
            exp = exp.And(x => x.GoodQuantity == queryDto.GoodQuantity);
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
