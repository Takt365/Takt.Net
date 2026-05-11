// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaRepairService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA改修日报表应用服务，提供PcbaRepair管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Domain.Entities.Logistics.Manufacturing.Defect;

namespace Takt.Application.Services.Logistics.Manufacturing.Defect;

/// <summary>
/// PCBA改修日报表应用服务
/// </summary>
public class TaktPcbaRepairService : TaktServiceBase, ITaktPcbaRepairService
{
    private readonly ITaktRepository<TaktPcbaRepair> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktPcbaRepairDetail> _pcbaRepairDetailRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PcbaRepair仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="pcbaRepairDetailRepository">PcbaRepairDetail仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPcbaRepairService(
        ITaktRepository<TaktPcbaRepair> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktPcbaRepairDetail> pcbaRepairDetailRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _pcbaRepairDetailRepository = pcbaRepairDetailRepository;
    }


    /// <summary>
    /// 获取PCBA改修日报表(PcbaRepair)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPcbaRepairDto>> GetPcbaRepairListAsync(TaktPcbaRepairQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPcbaRepairDto>.Create(
            data.Adapt<List<TaktPcbaRepairDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取PCBA改修日报表(PcbaRepair)
    /// </summary>
    /// <param name="id">PCBA改修日报表(PcbaRepair)ID</param>
    /// <returns>PCBA改修日报表(PcbaRepair)DTO</returns>
    public async Task<TaktPcbaRepairDto?> GetPcbaRepairByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktPcbaRepairDto>();
        
        // 手动加载子表
        dto.PcbaRepairDetails = (await _pcbaRepairDetailRepository.FindAsync(x => x.PcbaRepairId == id && x.IsDeleted == 0))
            .Adapt<List<TaktPcbaRepairDetailDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取PCBA改修日报表(PcbaRepair)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>PCBA改修日报表(PcbaRepair)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPcbaRepairOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建PCBA改修日报表(PcbaRepair)
    /// </summary>
    /// <param name="dto">创建PCBA改修日报表(PcbaRepair)DTO</param>
    /// <returns>PCBA改修日报表(PcbaRepair)DTO</returns>
    public async Task<TaktPcbaRepairDto> CreatePcbaRepairAsync(TaktPcbaRepairCreateDto dto)
    {
        var entity = dto.Adapt<TaktPcbaRepair>();
        // 验证工厂编码、ProdCategory、ProdDate、ProdLine、ShiftNo、ProdOrderCode组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.ProdCategory == dto.ProdCategory && x.ProdDate == dto.ProdDate && x.ProdLine == dto.ProdLine && x.ShiftNo == dto.ShiftNo && x.ProdOrderCode == dto.ProdOrderCode);
        if (!isUnique)
            throw new TaktBusinessException($"PCBA改修日报表工厂编码、ProdCategory、ProdDate、ProdLine、ShiftNo、ProdOrderCode组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建PcbaRepairDetail列表
            if (dto.PcbaRepairDetails != null && dto.PcbaRepairDetails.Count > 0)
            {
                var pcbaRepairDetailList = dto.PcbaRepairDetails.Select(x => {
                    var childEntity = x.Adapt<TaktPcbaRepairDetail>();
                    childEntity.PcbaRepairId = entity.Id;
                    return childEntity;
                }).ToList();
                await _pcbaRepairDetailRepository.CreateRangeBulkAsync(pcbaRepairDetailList);
            }
        }

        return (await GetPcbaRepairByIdAsync(entity.Id)) ?? entity.Adapt<TaktPcbaRepairDto>();
    }


    /// <summary>
    /// 更新PCBA改修日报表(PcbaRepair)
    /// </summary>
    /// <param name="id">PCBA改修日报表(PcbaRepair)ID</param>
    /// <param name="dto">更新PCBA改修日报表(PcbaRepair)DTO</param>
    /// <returns>PCBA改修日报表(PcbaRepair)DTO</returns>
    public async Task<TaktPcbaRepairDto> UpdatePcbaRepairAsync(long id, TaktPcbaRepairUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbarepairNotFound");
        // 验证工厂编码、ProdCategory、ProdDate、ProdLine、ShiftNo、ProdOrderCode组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.ProdCategory == dto.ProdCategory && x.ProdDate == dto.ProdDate && x.ProdLine == dto.ProdLine && x.ShiftNo == dto.ShiftNo && x.ProdOrderCode == dto.ProdOrderCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"PCBA改修日报表工厂编码、ProdCategory、ProdDate、ProdLine、ShiftNo、ProdOrderCode组合已存在");

        dto.Adapt(entity, typeof(TaktPcbaRepairUpdateDto), typeof(TaktPcbaRepair));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的PcbaRepairDetail列表
        var oldPcbaRepairDetails = await _pcbaRepairDetailRepository.FindAsync(x => x.PcbaRepairId == id && x.IsDeleted == 0);
        if (oldPcbaRepairDetails != null && oldPcbaRepairDetails.Count > 0)
        {
            foreach (var oldPcbaRepairDetail in oldPcbaRepairDetails)
            {
                oldPcbaRepairDetail.IsDeleted = 1;
            }
            await _pcbaRepairDetailRepository.UpdateRangeBulkAsync(oldPcbaRepairDetails);
        }

        // 创建新的PcbaRepairDetail列表
        if (dto.PcbaRepairDetails != null && dto.PcbaRepairDetails.Count > 0)
        {
            var pcbaRepairDetailList = dto.PcbaRepairDetails.Select(x => {
                var childEntity = x.Adapt<TaktPcbaRepairDetail>();
                childEntity.PcbaRepairId = id;
                return childEntity;
            }).ToList();
            await _pcbaRepairDetailRepository.CreateRangeBulkAsync(pcbaRepairDetailList);
        }


        return (await GetPcbaRepairByIdAsync(id)) ?? entity.Adapt<TaktPcbaRepairDto>();
    }


    /// <summary>
    /// 删除PCBA改修日报表(PcbaRepair)
    /// </summary>
    /// <param name="id">PCBA改修日报表(PcbaRepair)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePcbaRepairByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbarepairNotFound");
        
        // 级联删除子表数据
        // 级联删除PcbaRepairDetail列表
        var pcbaRepairDetails = await _pcbaRepairDetailRepository.FindAsync(x => x.PcbaRepairId == id && x.IsDeleted == 0);
        if (pcbaRepairDetails != null && pcbaRepairDetails.Count > 0)
        {
            foreach (var pcbaRepairDetail in pcbaRepairDetails)
            {
                pcbaRepairDetail.IsDeleted = 1;
            }
            await _pcbaRepairDetailRepository.UpdateRangeBulkAsync(pcbaRepairDetails);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除PCBA改修日报表(PcbaRepair)
    /// </summary>
    /// <param name="ids">PCBA改修日报表(PcbaRepair)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePcbaRepairBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPcbaRepair>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除PcbaRepairDetail列表
        var pcbaRepairDetailsToDelete = new List<TaktPcbaRepairDetail>();
        foreach (var id in idList)
        {
            var pcbaRepairDetails = await _pcbaRepairDetailRepository.FindAsync(x => x.PcbaRepairId == id && x.IsDeleted == 0);
            if (pcbaRepairDetails != null && pcbaRepairDetails.Count > 0)
            {
                pcbaRepairDetailsToDelete.AddRange(pcbaRepairDetails);
            }
        }
        
        if (pcbaRepairDetailsToDelete.Count > 0)
        {
            foreach (var pcbaRepairDetail in pcbaRepairDetailsToDelete)
            {
                pcbaRepairDetail.IsDeleted = 1;
            }
            await _pcbaRepairDetailRepository.UpdateRangeBulkAsync(pcbaRepairDetailsToDelete);
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
    /// 更新PCBA改修日报表(PcbaRepair)状态
    /// </summary>
    /// <param name="dto">PCBA改修日报表(PcbaRepair)状态DTO</param>
    /// <returns>PCBA改修日报表(PcbaRepair)DTO</returns>
    public async Task<TaktPcbaRepairDto> UpdatePcbaRepairStatusAsync(TaktPcbaRepairStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PcbaRepairId);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbarepairNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPcbaRepairByIdAsync(entity.Id) ?? entity.Adapt<TaktPcbaRepairDto>();
    }


    /// <summary>
    /// 获取PCBA改修日报表(PcbaRepair)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPcbaRepairTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPcbaRepair));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPcbaRepairTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入PCBA改修日报表(PcbaRepair)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPcbaRepairAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPcbaRepair));
        var importData = await TaktExcelHelper.ImportAsync<TaktPcbaRepairImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPcbaRepair>();
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
    /// 导出PCBA改修日报表(PcbaRepair)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPcbaRepairAsync(TaktPcbaRepairQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPcbaRepairQueryDto());
        List<TaktPcbaRepair> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPcbaRepair));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPcbaRepairExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPcbaRepairExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建PCBA改修日报表查询表达式
    /// </summary>
    /// <param name="queryDto">PCBA改修日报表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPcbaRepair, bool>> QueryExpression(TaktPcbaRepairQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPcbaRepair>();

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
