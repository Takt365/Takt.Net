// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Maintenance
// 文件名称：TaktEquipmentService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：工厂设备表应用服务，提供Equipment管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Maintenance;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Maintenance;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Maintenance;

/// <summary>
/// 工厂设备表应用服务
/// </summary>
public class TaktEquipmentService : TaktServiceBase, ITaktEquipmentService
{
    private readonly ITaktRepository<TaktEquipment> _repository;
    private readonly ITaktRepository<TaktMaintenance> _maintenanceRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Equipment仓储</param>
    /// <param name="maintenanceRepository">Maintenance仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEquipmentService(
        ITaktRepository<TaktEquipment> repository,
        ITaktRepository<TaktMaintenance> maintenanceRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _maintenanceRepository = maintenanceRepository;
    }


    /// <summary>
    /// 获取工厂设备表(Equipment)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEquipmentDto>> GetEquipmentListAsync(TaktEquipmentQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEquipmentDto>.Create(
            data.Adapt<List<TaktEquipmentDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取工厂设备表(Equipment)
    /// </summary>
    /// <param name="id">工厂设备表(Equipment)ID</param>
    /// <returns>工厂设备表(Equipment)DTO</returns>
    public async Task<TaktEquipmentDto?> GetEquipmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktEquipmentDto>();
        
        // 手动加载子表
        dto.MaintenanceRecords = (await _maintenanceRepository.FindAsync(x => x.EquipmentId == id && x.IsDeleted == 0))
            .Adapt<List<TaktMaintenanceDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取工厂设备表(Equipment)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工厂设备表(Equipment)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEquipmentOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.WarrantyStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.EquipmentName ?? string.Empty,
            DictValue = x.EquipmentCode

        }).ToList();
    }


    /// <summary>
    /// 创建工厂设备表(Equipment)
    /// </summary>
    /// <param name="dto">创建工厂设备表(Equipment)DTO</param>
    /// <returns>工厂设备表(Equipment)DTO</returns>
    public async Task<TaktEquipmentDto> CreateEquipmentAsync(TaktEquipmentCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.EquipmentCode, dto.EquipmentCode, null, $"工厂设备表编码 {dto.EquipmentCode} 已存在");

        var entity = dto.Adapt<TaktEquipment>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建Maintenance列表
            if (dto.MaintenanceRecords != null && dto.MaintenanceRecords.Count > 0)
            {
                var maintenanceList = dto.MaintenanceRecords.Select(x => {
                    var childEntity = x.Adapt<TaktMaintenance>();
                    childEntity.EquipmentId = entity.Id;
                    return childEntity;
                }).ToList();
                await _maintenanceRepository.CreateRangeBulkAsync(maintenanceList);
            }
        }

        return (await GetEquipmentByIdAsync(entity.Id)) ?? entity.Adapt<TaktEquipmentDto>();
    }


    /// <summary>
    /// 更新工厂设备表(Equipment)
    /// </summary>
    /// <param name="id">工厂设备表(Equipment)ID</param>
    /// <param name="dto">更新工厂设备表(Equipment)DTO</param>
    /// <returns>工厂设备表(Equipment)DTO</returns>
    public async Task<TaktEquipmentDto> UpdateEquipmentAsync(long id, TaktEquipmentUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.equipmentNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.EquipmentCode, dto.EquipmentCode, id, $"工厂设备表编码 {dto.EquipmentCode} 已存在");

        dto.Adapt(entity, typeof(TaktEquipmentUpdateDto), typeof(TaktEquipment));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的Maintenance列表
        var oldMaintenances = await _maintenanceRepository.FindAsync(x => x.EquipmentId == id && x.IsDeleted == 0);
        if (oldMaintenances != null && oldMaintenances.Count > 0)
        {
            foreach (var oldMaintenance in oldMaintenances)
            {
                oldMaintenance.IsDeleted = 1;
            }
            await _maintenanceRepository.UpdateRangeBulkAsync(oldMaintenances);
        }

        // 创建新的Maintenance列表
        if (dto.MaintenanceRecords != null && dto.MaintenanceRecords.Count > 0)
        {
            var maintenanceList = dto.MaintenanceRecords.Select(x => {
                var childEntity = x.Adapt<TaktMaintenance>();
                childEntity.EquipmentId = id;
                return childEntity;
            }).ToList();
            await _maintenanceRepository.CreateRangeBulkAsync(maintenanceList);
        }


        return (await GetEquipmentByIdAsync(id)) ?? entity.Adapt<TaktEquipmentDto>();
    }


    /// <summary>
    /// 删除工厂设备表(Equipment)
    /// </summary>
    /// <param name="id">工厂设备表(Equipment)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEquipmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.equipmentNotFound");
        
        // 级联删除子表数据
        // 级联删除Maintenance列表
        var maintenances = await _maintenanceRepository.FindAsync(x => x.EquipmentId == id && x.IsDeleted == 0);
        if (maintenances != null && maintenances.Count > 0)
        {
            foreach (var maintenance in maintenances)
            {
                maintenance.IsDeleted = 1;
            }
            await _maintenanceRepository.UpdateRangeBulkAsync(maintenances);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.WarrantyStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除工厂设备表(Equipment)
    /// </summary>
    /// <param name="ids">工厂设备表(Equipment)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEquipmentBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktEquipment>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除Maintenance列表
        var maintenancesToDelete = new List<TaktMaintenance>();
        foreach (var id in idList)
        {
            var maintenances = await _maintenanceRepository.FindAsync(x => x.EquipmentId == id && x.IsDeleted == 0);
            if (maintenances != null && maintenances.Count > 0)
            {
                maintenancesToDelete.AddRange(maintenances);
            }
        }
        
        if (maintenancesToDelete.Count > 0)
        {
            foreach (var maintenance in maintenancesToDelete)
            {
                maintenance.IsDeleted = 1;
            }
            await _maintenanceRepository.UpdateRangeBulkAsync(maintenancesToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 WarrantyStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.WarrantyStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新工厂设备表(Equipment)状态
    /// </summary>
    /// <param name="dto">工厂设备表(Equipment)状态DTO</param>
    /// <returns>工厂设备表(Equipment)DTO</returns>
    public async Task<TaktEquipmentDto> UpdateEquipmentWarrantyStatusAsync(TaktEquipmentWarrantyStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.EquipmentId);
        if (entity == null)
            throw new TaktBusinessException("validation.equipmentNotFound");
        entity.WarrantyStatus = dto.WarrantyStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetEquipmentByIdAsync(entity.Id) ?? entity.Adapt<TaktEquipmentDto>();
    }


    /// <summary>
    /// 更新工厂设备表(Equipment)状态
    /// </summary>
    /// <param name="dto">工厂设备表(Equipment)状态DTO</param>
    /// <returns>工厂设备表(Equipment)DTO</returns>
    public async Task<TaktEquipmentDto> UpdateEquipmentStatusAsync(TaktEquipmentStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.EquipmentId);
        if (entity == null)
            throw new TaktBusinessException("validation.equipmentNotFound");
        entity.EquipmentStatus = dto.EquipmentStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetEquipmentByIdAsync(entity.Id) ?? entity.Adapt<TaktEquipmentDto>();
    }


    /// <summary>
    /// 获取工厂设备表(Equipment)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetEquipmentTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEquipment));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEquipmentTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入工厂设备表(Equipment)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEquipmentAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktEquipment));
        var importData = await TaktExcelHelper.ImportAsync<TaktEquipmentImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktEquipment>();
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
    /// 导出工厂设备表(Equipment)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEquipmentAsync(TaktEquipmentQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEquipmentQueryDto());
        List<TaktEquipment> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEquipment));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEquipmentExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktEquipmentExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建工厂设备表查询表达式
    /// </summary>
    /// <param name="queryDto">工厂设备表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEquipment, bool>> QueryExpression(TaktEquipmentQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktEquipment>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.EquipmentCode!.Contains(queryDto.KeyWords) ||
                x.EquipmentName!.Contains(queryDto.KeyWords) ||
                x.EquipmentModel!.Contains(queryDto.KeyWords) ||
                x.EquipmentSpecification!.Contains(queryDto.KeyWords) ||
                x.EquipmentBrand!.Contains(queryDto.KeyWords) ||
                x.Manufacturer!.Contains(queryDto.KeyWords) ||
                x.DealerBy!.Contains(queryDto.KeyWords) ||
                x.SerialNumber!.Contains(queryDto.KeyWords) ||
                x.WorkshopBy!.Contains(queryDto.KeyWords) ||
                x.ProductionLineBy!.Contains(queryDto.KeyWords) ||
                x.WorkstationBy!.Contains(queryDto.KeyWords) ||
                x.DeptBy!.Contains(queryDto.KeyWords) ||
                x.EquipmentLocation!.Contains(queryDto.KeyWords) ||
                x.ResponsibleUserBy!.Contains(queryDto.KeyWords) ||
                x.OperatorBy!.Contains(queryDto.KeyWords) ||
                x.TechnicalParameters!.Contains(queryDto.KeyWords) ||
                x.EquipmentImages!.Contains(queryDto.KeyWords) ||
                x.EquipmentDocuments!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.EquipmentCode))
        {
            exp = exp.And(x => x.EquipmentCode!.Contains(queryDto.EquipmentCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.EquipmentName))
        {
            exp = exp.And(x => x.EquipmentName!.Contains(queryDto.EquipmentName));
        }

        if (queryDto?.EquipmentType.HasValue == true)
        {
            exp = exp.And(x => x.EquipmentType == queryDto.EquipmentType);
        }

        if (!string.IsNullOrEmpty(queryDto?.EquipmentModel))
        {
            exp = exp.And(x => x.EquipmentModel!.Contains(queryDto.EquipmentModel));
        }

        if (!string.IsNullOrEmpty(queryDto?.EquipmentSpecification))
        {
            exp = exp.And(x => x.EquipmentSpecification!.Contains(queryDto.EquipmentSpecification));
        }

        if (!string.IsNullOrEmpty(queryDto?.EquipmentBrand))
        {
            exp = exp.And(x => x.EquipmentBrand!.Contains(queryDto.EquipmentBrand));
        }

        if (!string.IsNullOrEmpty(queryDto?.Manufacturer))
        {
            exp = exp.And(x => x.Manufacturer!.Contains(queryDto.Manufacturer));
        }

        if (!string.IsNullOrEmpty(queryDto?.DealerBy))
        {
            exp = exp.And(x => x.DealerBy!.Contains(queryDto.DealerBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.SerialNumber))
        {
            exp = exp.And(x => x.SerialNumber!.Contains(queryDto.SerialNumber));
        }

        if (!string.IsNullOrEmpty(queryDto?.WorkshopBy))
        {
            exp = exp.And(x => x.WorkshopBy!.Contains(queryDto.WorkshopBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductionLineBy))
        {
            exp = exp.And(x => x.ProductionLineBy!.Contains(queryDto.ProductionLineBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.WorkstationBy))
        {
            exp = exp.And(x => x.WorkstationBy!.Contains(queryDto.WorkstationBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptBy))
        {
            exp = exp.And(x => x.DeptBy!.Contains(queryDto.DeptBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.EquipmentLocation))
        {
            exp = exp.And(x => x.EquipmentLocation!.Contains(queryDto.EquipmentLocation));
        }

        if (!string.IsNullOrEmpty(queryDto?.ResponsibleUserBy))
        {
            exp = exp.And(x => x.ResponsibleUserBy!.Contains(queryDto.ResponsibleUserBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperatorBy))
        {
            exp = exp.And(x => x.OperatorBy!.Contains(queryDto.OperatorBy));
        }

        if (queryDto?.PurchaseDate.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseDate == queryDto.PurchaseDate);
        }

        if (queryDto?.InstallationDate.HasValue == true)
        {
            exp = exp.And(x => x.InstallationDate == queryDto.InstallationDate);
        }

        if (queryDto?.StartDate.HasValue == true)
        {
            exp = exp.And(x => x.StartDate == queryDto.StartDate);
        }

        if (queryDto?.WarrantyStartDate.HasValue == true)
        {
            exp = exp.And(x => x.WarrantyStartDate == queryDto.WarrantyStartDate);
        }

        if (queryDto?.WarrantyEndDate.HasValue == true)
        {
            exp = exp.And(x => x.WarrantyEndDate == queryDto.WarrantyEndDate);
        }

        if (queryDto?.EquipmentOriginalValue.HasValue == true)
        {
            exp = exp.And(x => x.EquipmentOriginalValue == queryDto.EquipmentOriginalValue);
        }

        if (!string.IsNullOrEmpty(queryDto?.TechnicalParameters))
        {
            exp = exp.And(x => x.TechnicalParameters!.Contains(queryDto.TechnicalParameters));
        }

        if (!string.IsNullOrEmpty(queryDto?.EquipmentImages))
        {
            exp = exp.And(x => x.EquipmentImages!.Contains(queryDto.EquipmentImages));
        }

        if (!string.IsNullOrEmpty(queryDto?.EquipmentDocuments))
        {
            exp = exp.And(x => x.EquipmentDocuments!.Contains(queryDto.EquipmentDocuments));
        }

        if (queryDto?.IsCritical.HasValue == true)
        {
            exp = exp.And(x => x.IsCritical == queryDto.IsCritical);
        }

        if (queryDto?.WarrantyStatus.HasValue == true)
        {
            exp = exp.And(x => x.WarrantyStatus == queryDto.WarrantyStatus);
        }

        if (queryDto?.EquipmentStatus.HasValue == true)
        {
            exp = exp.And(x => x.EquipmentStatus == queryDto.EquipmentStatus);
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

        // PurchaseDate 日期范围查询
        if (queryDto?.PurchaseDateStart.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseDate >= queryDto.PurchaseDateStart);
        }
        if (queryDto?.PurchaseDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseDate <= queryDto.PurchaseDateEnd);
        }

        // InstallationDate 日期范围查询
        if (queryDto?.InstallationDateStart.HasValue == true)
        {
            exp = exp.And(x => x.InstallationDate >= queryDto.InstallationDateStart);
        }
        if (queryDto?.InstallationDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.InstallationDate <= queryDto.InstallationDateEnd);
        }

        // StartDate 日期范围查询
        if (queryDto?.StartDateStart.HasValue == true)
        {
            exp = exp.And(x => x.StartDate >= queryDto.StartDateStart);
        }
        if (queryDto?.StartDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.StartDate <= queryDto.StartDateEnd);
        }

        // WarrantyStartDate 日期范围查询
        if (queryDto?.WarrantyStartDateStart.HasValue == true)
        {
            exp = exp.And(x => x.WarrantyStartDate >= queryDto.WarrantyStartDateStart);
        }
        if (queryDto?.WarrantyStartDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.WarrantyStartDate <= queryDto.WarrantyStartDateEnd);
        }

        // WarrantyEndDate 日期范围查询
        if (queryDto?.WarrantyEndDateStart.HasValue == true)
        {
            exp = exp.And(x => x.WarrantyEndDate >= queryDto.WarrantyEndDateStart);
        }
        if (queryDto?.WarrantyEndDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.WarrantyEndDate <= queryDto.WarrantyEndDateEnd);
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
