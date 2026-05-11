// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：TaktPcbaOutputService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA日报表应用服务，提供PcbaOutput管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Domain.Entities.Logistics.Manufacturing.Output;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// PCBA日报表应用服务
/// </summary>
public class TaktPcbaOutputService : TaktServiceBase, ITaktPcbaOutputService
{
    private readonly ITaktRepository<TaktPcbaOutput> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktPcbaOutputDetail> _pcbaOutputDetailRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PcbaOutput仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="pcbaOutputDetailRepository">PcbaOutputDetail仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPcbaOutputService(
        ITaktRepository<TaktPcbaOutput> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktPcbaOutputDetail> pcbaOutputDetailRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _pcbaOutputDetailRepository = pcbaOutputDetailRepository;
    }


    /// <summary>
    /// 获取PCBA日报表(PcbaOutput)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPcbaOutputDto>> GetPcbaOutputListAsync(TaktPcbaOutputQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPcbaOutputDto>.Create(
            data.Adapt<List<TaktPcbaOutputDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取PCBA日报表(PcbaOutput)
    /// </summary>
    /// <param name="id">PCBA日报表(PcbaOutput)ID</param>
    /// <returns>PCBA日报表(PcbaOutput)DTO</returns>
    public async Task<TaktPcbaOutputDto?> GetPcbaOutputByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktPcbaOutputDto>();
        
        // 手动加载子表
        dto.PcbaOutputDetails = (await _pcbaOutputDetailRepository.FindAsync(x => x.PcbaOutputId == id && x.IsDeleted == 0))
            .Adapt<List<TaktPcbaOutputDetailDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取PCBA日报表(PcbaOutput)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>PCBA日报表(PcbaOutput)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPcbaOutputOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建PCBA日报表(PcbaOutput)
    /// </summary>
    /// <param name="dto">创建PCBA日报表(PcbaOutput)DTO</param>
    /// <returns>PCBA日报表(PcbaOutput)DTO</returns>
    public async Task<TaktPcbaOutputDto> CreatePcbaOutputAsync(TaktPcbaOutputCreateDto dto)
    {
        var entity = dto.Adapt<TaktPcbaOutput>();
        // 验证工厂编码、ProdCategory、ProdDate、ProdLine、ShiftNo、ProdOrderCode组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.ProdCategory == dto.ProdCategory && x.ProdDate == dto.ProdDate && x.ProdLine == dto.ProdLine && x.ShiftNo == dto.ShiftNo && x.ProdOrderCode == dto.ProdOrderCode);
        if (!isUnique)
            throw new TaktBusinessException($"PCBA日报表工厂编码、ProdCategory、ProdDate、ProdLine、ShiftNo、ProdOrderCode组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建PcbaOutputDetail列表
            if (dto.PcbaOutputDetails != null && dto.PcbaOutputDetails.Count > 0)
            {
                var pcbaOutputDetailList = dto.PcbaOutputDetails.Select(x => {
                    var childEntity = x.Adapt<TaktPcbaOutputDetail>();
                    childEntity.PcbaOutputId = entity.Id;
                    return childEntity;
                }).ToList();
                await _pcbaOutputDetailRepository.CreateRangeBulkAsync(pcbaOutputDetailList);
            }
        }

        return (await GetPcbaOutputByIdAsync(entity.Id)) ?? entity.Adapt<TaktPcbaOutputDto>();
    }


    /// <summary>
    /// 更新PCBA日报表(PcbaOutput)
    /// </summary>
    /// <param name="id">PCBA日报表(PcbaOutput)ID</param>
    /// <param name="dto">更新PCBA日报表(PcbaOutput)DTO</param>
    /// <returns>PCBA日报表(PcbaOutput)DTO</returns>
    public async Task<TaktPcbaOutputDto> UpdatePcbaOutputAsync(long id, TaktPcbaOutputUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbaoutputNotFound");
        // 验证工厂编码、ProdCategory、ProdDate、ProdLine、ShiftNo、ProdOrderCode组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.ProdCategory == dto.ProdCategory && x.ProdDate == dto.ProdDate && x.ProdLine == dto.ProdLine && x.ShiftNo == dto.ShiftNo && x.ProdOrderCode == dto.ProdOrderCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"PCBA日报表工厂编码、ProdCategory、ProdDate、ProdLine、ShiftNo、ProdOrderCode组合已存在");

        dto.Adapt(entity, typeof(TaktPcbaOutputUpdateDto), typeof(TaktPcbaOutput));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的PcbaOutputDetail列表
        var oldPcbaOutputDetails = await _pcbaOutputDetailRepository.FindAsync(x => x.PcbaOutputId == id && x.IsDeleted == 0);
        if (oldPcbaOutputDetails != null && oldPcbaOutputDetails.Count > 0)
        {
            foreach (var oldPcbaOutputDetail in oldPcbaOutputDetails)
            {
                oldPcbaOutputDetail.IsDeleted = 1;
            }
            await _pcbaOutputDetailRepository.UpdateRangeBulkAsync(oldPcbaOutputDetails);
        }

        // 创建新的PcbaOutputDetail列表
        if (dto.PcbaOutputDetails != null && dto.PcbaOutputDetails.Count > 0)
        {
            var pcbaOutputDetailList = dto.PcbaOutputDetails.Select(x => {
                var childEntity = x.Adapt<TaktPcbaOutputDetail>();
                childEntity.PcbaOutputId = id;
                return childEntity;
            }).ToList();
            await _pcbaOutputDetailRepository.CreateRangeBulkAsync(pcbaOutputDetailList);
        }


        return (await GetPcbaOutputByIdAsync(id)) ?? entity.Adapt<TaktPcbaOutputDto>();
    }


    /// <summary>
    /// 删除PCBA日报表(PcbaOutput)
    /// </summary>
    /// <param name="id">PCBA日报表(PcbaOutput)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePcbaOutputByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbaoutputNotFound");
        
        // 级联删除子表数据
        // 级联删除PcbaOutputDetail列表
        var pcbaOutputDetails = await _pcbaOutputDetailRepository.FindAsync(x => x.PcbaOutputId == id && x.IsDeleted == 0);
        if (pcbaOutputDetails != null && pcbaOutputDetails.Count > 0)
        {
            foreach (var pcbaOutputDetail in pcbaOutputDetails)
            {
                pcbaOutputDetail.IsDeleted = 1;
            }
            await _pcbaOutputDetailRepository.UpdateRangeBulkAsync(pcbaOutputDetails);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除PCBA日报表(PcbaOutput)
    /// </summary>
    /// <param name="ids">PCBA日报表(PcbaOutput)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePcbaOutputBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPcbaOutput>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除PcbaOutputDetail列表
        var pcbaOutputDetailsToDelete = new List<TaktPcbaOutputDetail>();
        foreach (var id in idList)
        {
            var pcbaOutputDetails = await _pcbaOutputDetailRepository.FindAsync(x => x.PcbaOutputId == id && x.IsDeleted == 0);
            if (pcbaOutputDetails != null && pcbaOutputDetails.Count > 0)
            {
                pcbaOutputDetailsToDelete.AddRange(pcbaOutputDetails);
            }
        }
        
        if (pcbaOutputDetailsToDelete.Count > 0)
        {
            foreach (var pcbaOutputDetail in pcbaOutputDetailsToDelete)
            {
                pcbaOutputDetail.IsDeleted = 1;
            }
            await _pcbaOutputDetailRepository.UpdateRangeBulkAsync(pcbaOutputDetailsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取PCBA日报表(PcbaOutput)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPcbaOutputTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPcbaOutput));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPcbaOutputTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入PCBA日报表(PcbaOutput)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPcbaOutputAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPcbaOutput));
        var importData = await TaktExcelHelper.ImportAsync<TaktPcbaOutputImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPcbaOutput>();
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
    /// 导出PCBA日报表(PcbaOutput)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPcbaOutputAsync(TaktPcbaOutputQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPcbaOutputQueryDto());
        List<TaktPcbaOutput> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPcbaOutput));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPcbaOutputExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPcbaOutputExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建PCBA日报表查询表达式
    /// </summary>
    /// <param name="queryDto">PCBA日报表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPcbaOutput, bool>> QueryExpression(TaktPcbaOutputQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPcbaOutput>();

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

        if (queryDto?.ProdOrderQty.HasValue == true)
        {
            exp = exp.And(x => x.ProdOrderQty == queryDto.ProdOrderQty);
        }

        if (queryDto?.StdMinutes.HasValue == true)
        {
            exp = exp.And(x => x.StdMinutes == queryDto.StdMinutes);
        }

        if (queryDto?.StdShorts.HasValue == true)
        {
            exp = exp.And(x => x.StdShorts == queryDto.StdShorts);
        }

        if (queryDto?.StdCapacity.HasValue == true)
        {
            exp = exp.And(x => x.StdCapacity == queryDto.StdCapacity);
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
