// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：品质业务主表应用服务，提供QualityOperation管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Domain.Entities.Logistics.Quality.Cost;

namespace Takt.Application.Services.Logistics.Quality.Cost;

/// <summary>
/// 品质业务主表应用服务
/// </summary>
public class TaktQualityOperationService : TaktServiceBase, ITaktQualityOperationService
{
    private readonly ITaktRepository<TaktQualityOperation> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktQualityOperationIncoming> _qualityOperationIncomingRepository;
    private readonly ITaktRepository<TaktQualityOperationFirstArticle> _qualityOperationFirstArticleRepository;
    private readonly ITaktRepository<TaktQualityOperationCalibration> _qualityOperationCalibrationRepository;
    private readonly ITaktRepository<TaktQualityOperationOther> _qualityOperationOtherRepository;
    private readonly ITaktRepository<TaktQualityOperationOutgoing> _qualityOperationOutgoingRepository;
    private readonly ITaktRepository<TaktQualityOperationReliability> _qualityOperationReliabilityRepository;
    private readonly ITaktRepository<TaktQualityOperationCustomerResponse> _qualityOperationCustomerResponseRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">QualityOperation仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="qualityOperationIncomingRepository">QualityOperationIncoming仓储</param>
    /// <param name="qualityOperationFirstArticleRepository">QualityOperationFirstArticle仓储</param>
    /// <param name="qualityOperationCalibrationRepository">QualityOperationCalibration仓储</param>
    /// <param name="qualityOperationOtherRepository">QualityOperationOther仓储</param>
    /// <param name="qualityOperationOutgoingRepository">QualityOperationOutgoing仓储</param>
    /// <param name="qualityOperationReliabilityRepository">QualityOperationReliability仓储</param>
    /// <param name="qualityOperationCustomerResponseRepository">QualityOperationCustomerResponse仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktQualityOperationService(
        ITaktRepository<TaktQualityOperation> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktQualityOperationIncoming> qualityOperationIncomingRepository,
        ITaktRepository<TaktQualityOperationFirstArticle> qualityOperationFirstArticleRepository,
        ITaktRepository<TaktQualityOperationCalibration> qualityOperationCalibrationRepository,
        ITaktRepository<TaktQualityOperationOther> qualityOperationOtherRepository,
        ITaktRepository<TaktQualityOperationOutgoing> qualityOperationOutgoingRepository,
        ITaktRepository<TaktQualityOperationReliability> qualityOperationReliabilityRepository,
        ITaktRepository<TaktQualityOperationCustomerResponse> qualityOperationCustomerResponseRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _qualityOperationIncomingRepository = qualityOperationIncomingRepository;
        _qualityOperationFirstArticleRepository = qualityOperationFirstArticleRepository;
        _qualityOperationCalibrationRepository = qualityOperationCalibrationRepository;
        _qualityOperationOtherRepository = qualityOperationOtherRepository;
        _qualityOperationOutgoingRepository = qualityOperationOutgoingRepository;
        _qualityOperationReliabilityRepository = qualityOperationReliabilityRepository;
        _qualityOperationCustomerResponseRepository = qualityOperationCustomerResponseRepository;
    }


    /// <summary>
    /// 获取品质业务主表(QualityOperation)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktQualityOperationDto>> GetQualityOperationListAsync(TaktQualityOperationQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktQualityOperationDto>.Create(
            data.Adapt<List<TaktQualityOperationDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取品质业务主表(QualityOperation)
    /// </summary>
    /// <param name="id">品质业务主表(QualityOperation)ID</param>
    /// <returns>品质业务主表(QualityOperation)DTO</returns>
    public async Task<TaktQualityOperationDto?> GetQualityOperationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktQualityOperationDto>();
        
        // 手动加载子表
        dto.IncomingItems = (await _qualityOperationIncomingRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0))
            .Adapt<List<TaktQualityOperationIncomingDto>>();
        dto.FirstArticleItems = (await _qualityOperationFirstArticleRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0))
            .Adapt<List<TaktQualityOperationFirstArticleDto>>();
        dto.CalibrationItems = (await _qualityOperationCalibrationRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0))
            .Adapt<List<TaktQualityOperationCalibrationDto>>();
        dto.OtherItems = (await _qualityOperationOtherRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0))
            .Adapt<List<TaktQualityOperationOtherDto>>();
        dto.OutgoingItems = (await _qualityOperationOutgoingRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0))
            .Adapt<List<TaktQualityOperationOutgoingDto>>();
        dto.ReliabilityItems = (await _qualityOperationReliabilityRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0))
            .Adapt<List<TaktQualityOperationReliabilityDto>>();
        dto.CustomerResponseItems = (await _qualityOperationCustomerResponseRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0))
            .Adapt<List<TaktQualityOperationCustomerResponseDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取品质业务主表(QualityOperation)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>品质业务主表(QualityOperation)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetQualityOperationOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建品质业务主表(QualityOperation)
    /// </summary>
    /// <param name="dto">创建品质业务主表(QualityOperation)DTO</param>
    /// <returns>品质业务主表(QualityOperation)DTO</returns>
    public async Task<TaktQualityOperationDto> CreateQualityOperationAsync(TaktQualityOperationCreateDto dto)
    {
        var entity = dto.Adapt<TaktQualityOperation>();
        // 验证工厂编码、QualityOperationCode、OperationMonth、CustomerName、DebitNoteNo组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.QualityOperationCode == dto.QualityOperationCode && x.OperationMonth == dto.OperationMonth && x.CustomerName == dto.CustomerName && x.DebitNoteNo == dto.DebitNoteNo);
        if (!isUnique)
            throw new TaktBusinessException($"品质业务主表工厂编码、QualityOperationCode、OperationMonth、CustomerName、DebitNoteNo组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建QualityOperationIncoming列表
            if (dto.IncomingItems != null && dto.IncomingItems.Count > 0)
            {
                var qualityOperationIncomingList = dto.IncomingItems.Select(x => {
                    var childEntity = x.Adapt<TaktQualityOperationIncoming>();
                    childEntity.QualityOperationId = entity.Id;
                    return childEntity;
                }).ToList();
                await _qualityOperationIncomingRepository.CreateRangeBulkAsync(qualityOperationIncomingList);
            }
            // 创建QualityOperationFirstArticle列表
            if (dto.FirstArticleItems != null && dto.FirstArticleItems.Count > 0)
            {
                var qualityOperationFirstArticleList = dto.FirstArticleItems.Select(x => {
                    var childEntity = x.Adapt<TaktQualityOperationFirstArticle>();
                    childEntity.QualityOperationId = entity.Id;
                    return childEntity;
                }).ToList();
                await _qualityOperationFirstArticleRepository.CreateRangeBulkAsync(qualityOperationFirstArticleList);
            }
            // 创建QualityOperationCalibration列表
            if (dto.CalibrationItems != null && dto.CalibrationItems.Count > 0)
            {
                var qualityOperationCalibrationList = dto.CalibrationItems.Select(x => {
                    var childEntity = x.Adapt<TaktQualityOperationCalibration>();
                    childEntity.QualityOperationId = entity.Id;
                    return childEntity;
                }).ToList();
                await _qualityOperationCalibrationRepository.CreateRangeBulkAsync(qualityOperationCalibrationList);
            }
            // 创建QualityOperationOther列表
            if (dto.OtherItems != null && dto.OtherItems.Count > 0)
            {
                var qualityOperationOtherList = dto.OtherItems.Select(x => {
                    var childEntity = x.Adapt<TaktQualityOperationOther>();
                    childEntity.QualityOperationId = entity.Id;
                    return childEntity;
                }).ToList();
                await _qualityOperationOtherRepository.CreateRangeBulkAsync(qualityOperationOtherList);
            }
            // 创建QualityOperationOutgoing列表
            if (dto.OutgoingItems != null && dto.OutgoingItems.Count > 0)
            {
                var qualityOperationOutgoingList = dto.OutgoingItems.Select(x => {
                    var childEntity = x.Adapt<TaktQualityOperationOutgoing>();
                    childEntity.QualityOperationId = entity.Id;
                    return childEntity;
                }).ToList();
                await _qualityOperationOutgoingRepository.CreateRangeBulkAsync(qualityOperationOutgoingList);
            }
            // 创建QualityOperationReliability列表
            if (dto.ReliabilityItems != null && dto.ReliabilityItems.Count > 0)
            {
                var qualityOperationReliabilityList = dto.ReliabilityItems.Select(x => {
                    var childEntity = x.Adapt<TaktQualityOperationReliability>();
                    childEntity.QualityOperationId = entity.Id;
                    return childEntity;
                }).ToList();
                await _qualityOperationReliabilityRepository.CreateRangeBulkAsync(qualityOperationReliabilityList);
            }
            // 创建QualityOperationCustomerResponse列表
            if (dto.CustomerResponseItems != null && dto.CustomerResponseItems.Count > 0)
            {
                var qualityOperationCustomerResponseList = dto.CustomerResponseItems.Select(x => {
                    var childEntity = x.Adapt<TaktQualityOperationCustomerResponse>();
                    childEntity.QualityOperationId = entity.Id;
                    return childEntity;
                }).ToList();
                await _qualityOperationCustomerResponseRepository.CreateRangeBulkAsync(qualityOperationCustomerResponseList);
            }
        }

        return (await GetQualityOperationByIdAsync(entity.Id)) ?? entity.Adapt<TaktQualityOperationDto>();
    }


    /// <summary>
    /// 更新品质业务主表(QualityOperation)
    /// </summary>
    /// <param name="id">品质业务主表(QualityOperation)ID</param>
    /// <param name="dto">更新品质业务主表(QualityOperation)DTO</param>
    /// <returns>品质业务主表(QualityOperation)DTO</returns>
    public async Task<TaktQualityOperationDto> UpdateQualityOperationAsync(long id, TaktQualityOperationUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityoperationNotFound");
        // 验证工厂编码、QualityOperationCode、OperationMonth、CustomerName、DebitNoteNo组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.QualityOperationCode == dto.QualityOperationCode && x.OperationMonth == dto.OperationMonth && x.CustomerName == dto.CustomerName && x.DebitNoteNo == dto.DebitNoteNo, id);
        if (!isUnique)
            throw new TaktBusinessException($"品质业务主表工厂编码、QualityOperationCode、OperationMonth、CustomerName、DebitNoteNo组合已存在");

        dto.Adapt(entity, typeof(TaktQualityOperationUpdateDto), typeof(TaktQualityOperation));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的QualityOperationIncoming列表
        var oldQualityOperationIncomings = await _qualityOperationIncomingRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (oldQualityOperationIncomings != null && oldQualityOperationIncomings.Count > 0)
        {
            foreach (var oldQualityOperationIncoming in oldQualityOperationIncomings)
            {
                oldQualityOperationIncoming.IsDeleted = 1;
            }
            await _qualityOperationIncomingRepository.UpdateRangeBulkAsync(oldQualityOperationIncomings);
        }

        // 创建新的QualityOperationIncoming列表
        if (dto.IncomingItems != null && dto.IncomingItems.Count > 0)
        {
            var qualityOperationIncomingList = dto.IncomingItems.Select(x => {
                var childEntity = x.Adapt<TaktQualityOperationIncoming>();
                childEntity.QualityOperationId = id;
                return childEntity;
            }).ToList();
            await _qualityOperationIncomingRepository.CreateRangeBulkAsync(qualityOperationIncomingList);
        }
        // 删除旧的QualityOperationFirstArticle列表
        var oldQualityOperationFirstArticles = await _qualityOperationFirstArticleRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (oldQualityOperationFirstArticles != null && oldQualityOperationFirstArticles.Count > 0)
        {
            foreach (var oldQualityOperationFirstArticle in oldQualityOperationFirstArticles)
            {
                oldQualityOperationFirstArticle.IsDeleted = 1;
            }
            await _qualityOperationFirstArticleRepository.UpdateRangeBulkAsync(oldQualityOperationFirstArticles);
        }

        // 创建新的QualityOperationFirstArticle列表
        if (dto.FirstArticleItems != null && dto.FirstArticleItems.Count > 0)
        {
            var qualityOperationFirstArticleList = dto.FirstArticleItems.Select(x => {
                var childEntity = x.Adapt<TaktQualityOperationFirstArticle>();
                childEntity.QualityOperationId = id;
                return childEntity;
            }).ToList();
            await _qualityOperationFirstArticleRepository.CreateRangeBulkAsync(qualityOperationFirstArticleList);
        }
        // 删除旧的QualityOperationCalibration列表
        var oldQualityOperationCalibrations = await _qualityOperationCalibrationRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (oldQualityOperationCalibrations != null && oldQualityOperationCalibrations.Count > 0)
        {
            foreach (var oldQualityOperationCalibration in oldQualityOperationCalibrations)
            {
                oldQualityOperationCalibration.IsDeleted = 1;
            }
            await _qualityOperationCalibrationRepository.UpdateRangeBulkAsync(oldQualityOperationCalibrations);
        }

        // 创建新的QualityOperationCalibration列表
        if (dto.CalibrationItems != null && dto.CalibrationItems.Count > 0)
        {
            var qualityOperationCalibrationList = dto.CalibrationItems.Select(x => {
                var childEntity = x.Adapt<TaktQualityOperationCalibration>();
                childEntity.QualityOperationId = id;
                return childEntity;
            }).ToList();
            await _qualityOperationCalibrationRepository.CreateRangeBulkAsync(qualityOperationCalibrationList);
        }
        // 删除旧的QualityOperationOther列表
        var oldQualityOperationOthers = await _qualityOperationOtherRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (oldQualityOperationOthers != null && oldQualityOperationOthers.Count > 0)
        {
            foreach (var oldQualityOperationOther in oldQualityOperationOthers)
            {
                oldQualityOperationOther.IsDeleted = 1;
            }
            await _qualityOperationOtherRepository.UpdateRangeBulkAsync(oldQualityOperationOthers);
        }

        // 创建新的QualityOperationOther列表
        if (dto.OtherItems != null && dto.OtherItems.Count > 0)
        {
            var qualityOperationOtherList = dto.OtherItems.Select(x => {
                var childEntity = x.Adapt<TaktQualityOperationOther>();
                childEntity.QualityOperationId = id;
                return childEntity;
            }).ToList();
            await _qualityOperationOtherRepository.CreateRangeBulkAsync(qualityOperationOtherList);
        }
        // 删除旧的QualityOperationOutgoing列表
        var oldQualityOperationOutgoings = await _qualityOperationOutgoingRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (oldQualityOperationOutgoings != null && oldQualityOperationOutgoings.Count > 0)
        {
            foreach (var oldQualityOperationOutgoing in oldQualityOperationOutgoings)
            {
                oldQualityOperationOutgoing.IsDeleted = 1;
            }
            await _qualityOperationOutgoingRepository.UpdateRangeBulkAsync(oldQualityOperationOutgoings);
        }

        // 创建新的QualityOperationOutgoing列表
        if (dto.OutgoingItems != null && dto.OutgoingItems.Count > 0)
        {
            var qualityOperationOutgoingList = dto.OutgoingItems.Select(x => {
                var childEntity = x.Adapt<TaktQualityOperationOutgoing>();
                childEntity.QualityOperationId = id;
                return childEntity;
            }).ToList();
            await _qualityOperationOutgoingRepository.CreateRangeBulkAsync(qualityOperationOutgoingList);
        }
        // 删除旧的QualityOperationReliability列表
        var oldQualityOperationReliabilitys = await _qualityOperationReliabilityRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (oldQualityOperationReliabilitys != null && oldQualityOperationReliabilitys.Count > 0)
        {
            foreach (var oldQualityOperationReliability in oldQualityOperationReliabilitys)
            {
                oldQualityOperationReliability.IsDeleted = 1;
            }
            await _qualityOperationReliabilityRepository.UpdateRangeBulkAsync(oldQualityOperationReliabilitys);
        }

        // 创建新的QualityOperationReliability列表
        if (dto.ReliabilityItems != null && dto.ReliabilityItems.Count > 0)
        {
            var qualityOperationReliabilityList = dto.ReliabilityItems.Select(x => {
                var childEntity = x.Adapt<TaktQualityOperationReliability>();
                childEntity.QualityOperationId = id;
                return childEntity;
            }).ToList();
            await _qualityOperationReliabilityRepository.CreateRangeBulkAsync(qualityOperationReliabilityList);
        }
        // 删除旧的QualityOperationCustomerResponse列表
        var oldQualityOperationCustomerResponses = await _qualityOperationCustomerResponseRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (oldQualityOperationCustomerResponses != null && oldQualityOperationCustomerResponses.Count > 0)
        {
            foreach (var oldQualityOperationCustomerResponse in oldQualityOperationCustomerResponses)
            {
                oldQualityOperationCustomerResponse.IsDeleted = 1;
            }
            await _qualityOperationCustomerResponseRepository.UpdateRangeBulkAsync(oldQualityOperationCustomerResponses);
        }

        // 创建新的QualityOperationCustomerResponse列表
        if (dto.CustomerResponseItems != null && dto.CustomerResponseItems.Count > 0)
        {
            var qualityOperationCustomerResponseList = dto.CustomerResponseItems.Select(x => {
                var childEntity = x.Adapt<TaktQualityOperationCustomerResponse>();
                childEntity.QualityOperationId = id;
                return childEntity;
            }).ToList();
            await _qualityOperationCustomerResponseRepository.CreateRangeBulkAsync(qualityOperationCustomerResponseList);
        }


        return (await GetQualityOperationByIdAsync(id)) ?? entity.Adapt<TaktQualityOperationDto>();
    }


    /// <summary>
    /// 删除品质业务主表(QualityOperation)
    /// </summary>
    /// <param name="id">品质业务主表(QualityOperation)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityOperationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityoperationNotFound");
        
        // 级联删除子表数据
        // 级联删除QualityOperationIncoming列表
        var qualityOperationIncomings = await _qualityOperationIncomingRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (qualityOperationIncomings != null && qualityOperationIncomings.Count > 0)
        {
            foreach (var qualityOperationIncoming in qualityOperationIncomings)
            {
                qualityOperationIncoming.IsDeleted = 1;
            }
            await _qualityOperationIncomingRepository.UpdateRangeBulkAsync(qualityOperationIncomings);
        }
        // 级联删除QualityOperationFirstArticle列表
        var qualityOperationFirstArticles = await _qualityOperationFirstArticleRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (qualityOperationFirstArticles != null && qualityOperationFirstArticles.Count > 0)
        {
            foreach (var qualityOperationFirstArticle in qualityOperationFirstArticles)
            {
                qualityOperationFirstArticle.IsDeleted = 1;
            }
            await _qualityOperationFirstArticleRepository.UpdateRangeBulkAsync(qualityOperationFirstArticles);
        }
        // 级联删除QualityOperationCalibration列表
        var qualityOperationCalibrations = await _qualityOperationCalibrationRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (qualityOperationCalibrations != null && qualityOperationCalibrations.Count > 0)
        {
            foreach (var qualityOperationCalibration in qualityOperationCalibrations)
            {
                qualityOperationCalibration.IsDeleted = 1;
            }
            await _qualityOperationCalibrationRepository.UpdateRangeBulkAsync(qualityOperationCalibrations);
        }
        // 级联删除QualityOperationOther列表
        var qualityOperationOthers = await _qualityOperationOtherRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (qualityOperationOthers != null && qualityOperationOthers.Count > 0)
        {
            foreach (var qualityOperationOther in qualityOperationOthers)
            {
                qualityOperationOther.IsDeleted = 1;
            }
            await _qualityOperationOtherRepository.UpdateRangeBulkAsync(qualityOperationOthers);
        }
        // 级联删除QualityOperationOutgoing列表
        var qualityOperationOutgoings = await _qualityOperationOutgoingRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (qualityOperationOutgoings != null && qualityOperationOutgoings.Count > 0)
        {
            foreach (var qualityOperationOutgoing in qualityOperationOutgoings)
            {
                qualityOperationOutgoing.IsDeleted = 1;
            }
            await _qualityOperationOutgoingRepository.UpdateRangeBulkAsync(qualityOperationOutgoings);
        }
        // 级联删除QualityOperationReliability列表
        var qualityOperationReliabilitys = await _qualityOperationReliabilityRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (qualityOperationReliabilitys != null && qualityOperationReliabilitys.Count > 0)
        {
            foreach (var qualityOperationReliability in qualityOperationReliabilitys)
            {
                qualityOperationReliability.IsDeleted = 1;
            }
            await _qualityOperationReliabilityRepository.UpdateRangeBulkAsync(qualityOperationReliabilitys);
        }
        // 级联删除QualityOperationCustomerResponse列表
        var qualityOperationCustomerResponses = await _qualityOperationCustomerResponseRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
        if (qualityOperationCustomerResponses != null && qualityOperationCustomerResponses.Count > 0)
        {
            foreach (var qualityOperationCustomerResponse in qualityOperationCustomerResponses)
            {
                qualityOperationCustomerResponse.IsDeleted = 1;
            }
            await _qualityOperationCustomerResponseRepository.UpdateRangeBulkAsync(qualityOperationCustomerResponses);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除品质业务主表(QualityOperation)
    /// </summary>
    /// <param name="ids">品质业务主表(QualityOperation)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityOperationBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktQualityOperation>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除QualityOperationIncoming列表
        var qualityOperationIncomingsToDelete = new List<TaktQualityOperationIncoming>();
        foreach (var id in idList)
        {
            var qualityOperationIncomings = await _qualityOperationIncomingRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
            if (qualityOperationIncomings != null && qualityOperationIncomings.Count > 0)
            {
                qualityOperationIncomingsToDelete.AddRange(qualityOperationIncomings);
            }
        }
        
        if (qualityOperationIncomingsToDelete.Count > 0)
        {
            foreach (var qualityOperationIncoming in qualityOperationIncomingsToDelete)
            {
                qualityOperationIncoming.IsDeleted = 1;
            }
            await _qualityOperationIncomingRepository.UpdateRangeBulkAsync(qualityOperationIncomingsToDelete);
        }
        // 批量级联删除QualityOperationFirstArticle列表
        var qualityOperationFirstArticlesToDelete = new List<TaktQualityOperationFirstArticle>();
        foreach (var id in idList)
        {
            var qualityOperationFirstArticles = await _qualityOperationFirstArticleRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
            if (qualityOperationFirstArticles != null && qualityOperationFirstArticles.Count > 0)
            {
                qualityOperationFirstArticlesToDelete.AddRange(qualityOperationFirstArticles);
            }
        }
        
        if (qualityOperationFirstArticlesToDelete.Count > 0)
        {
            foreach (var qualityOperationFirstArticle in qualityOperationFirstArticlesToDelete)
            {
                qualityOperationFirstArticle.IsDeleted = 1;
            }
            await _qualityOperationFirstArticleRepository.UpdateRangeBulkAsync(qualityOperationFirstArticlesToDelete);
        }
        // 批量级联删除QualityOperationCalibration列表
        var qualityOperationCalibrationsToDelete = new List<TaktQualityOperationCalibration>();
        foreach (var id in idList)
        {
            var qualityOperationCalibrations = await _qualityOperationCalibrationRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
            if (qualityOperationCalibrations != null && qualityOperationCalibrations.Count > 0)
            {
                qualityOperationCalibrationsToDelete.AddRange(qualityOperationCalibrations);
            }
        }
        
        if (qualityOperationCalibrationsToDelete.Count > 0)
        {
            foreach (var qualityOperationCalibration in qualityOperationCalibrationsToDelete)
            {
                qualityOperationCalibration.IsDeleted = 1;
            }
            await _qualityOperationCalibrationRepository.UpdateRangeBulkAsync(qualityOperationCalibrationsToDelete);
        }
        // 批量级联删除QualityOperationOther列表
        var qualityOperationOthersToDelete = new List<TaktQualityOperationOther>();
        foreach (var id in idList)
        {
            var qualityOperationOthers = await _qualityOperationOtherRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
            if (qualityOperationOthers != null && qualityOperationOthers.Count > 0)
            {
                qualityOperationOthersToDelete.AddRange(qualityOperationOthers);
            }
        }
        
        if (qualityOperationOthersToDelete.Count > 0)
        {
            foreach (var qualityOperationOther in qualityOperationOthersToDelete)
            {
                qualityOperationOther.IsDeleted = 1;
            }
            await _qualityOperationOtherRepository.UpdateRangeBulkAsync(qualityOperationOthersToDelete);
        }
        // 批量级联删除QualityOperationOutgoing列表
        var qualityOperationOutgoingsToDelete = new List<TaktQualityOperationOutgoing>();
        foreach (var id in idList)
        {
            var qualityOperationOutgoings = await _qualityOperationOutgoingRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
            if (qualityOperationOutgoings != null && qualityOperationOutgoings.Count > 0)
            {
                qualityOperationOutgoingsToDelete.AddRange(qualityOperationOutgoings);
            }
        }
        
        if (qualityOperationOutgoingsToDelete.Count > 0)
        {
            foreach (var qualityOperationOutgoing in qualityOperationOutgoingsToDelete)
            {
                qualityOperationOutgoing.IsDeleted = 1;
            }
            await _qualityOperationOutgoingRepository.UpdateRangeBulkAsync(qualityOperationOutgoingsToDelete);
        }
        // 批量级联删除QualityOperationReliability列表
        var qualityOperationReliabilitysToDelete = new List<TaktQualityOperationReliability>();
        foreach (var id in idList)
        {
            var qualityOperationReliabilitys = await _qualityOperationReliabilityRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
            if (qualityOperationReliabilitys != null && qualityOperationReliabilitys.Count > 0)
            {
                qualityOperationReliabilitysToDelete.AddRange(qualityOperationReliabilitys);
            }
        }
        
        if (qualityOperationReliabilitysToDelete.Count > 0)
        {
            foreach (var qualityOperationReliability in qualityOperationReliabilitysToDelete)
            {
                qualityOperationReliability.IsDeleted = 1;
            }
            await _qualityOperationReliabilityRepository.UpdateRangeBulkAsync(qualityOperationReliabilitysToDelete);
        }
        // 批量级联删除QualityOperationCustomerResponse列表
        var qualityOperationCustomerResponsesToDelete = new List<TaktQualityOperationCustomerResponse>();
        foreach (var id in idList)
        {
            var qualityOperationCustomerResponses = await _qualityOperationCustomerResponseRepository.FindAsync(x => x.QualityOperationId == id && x.IsDeleted == 0);
            if (qualityOperationCustomerResponses != null && qualityOperationCustomerResponses.Count > 0)
            {
                qualityOperationCustomerResponsesToDelete.AddRange(qualityOperationCustomerResponses);
            }
        }
        
        if (qualityOperationCustomerResponsesToDelete.Count > 0)
        {
            foreach (var qualityOperationCustomerResponse in qualityOperationCustomerResponsesToDelete)
            {
                qualityOperationCustomerResponse.IsDeleted = 1;
            }
            await _qualityOperationCustomerResponseRepository.UpdateRangeBulkAsync(qualityOperationCustomerResponsesToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取品质业务主表(QualityOperation)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetQualityOperationTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktQualityOperation));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktQualityOperationTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入品质业务主表(QualityOperation)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportQualityOperationAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktQualityOperation));
        var importData = await TaktExcelHelper.ImportAsync<TaktQualityOperationImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktQualityOperation>();
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
    /// 导出品质业务主表(QualityOperation)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportQualityOperationAsync(TaktQualityOperationQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktQualityOperationQueryDto());
        List<TaktQualityOperation> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktQualityOperation));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktQualityOperationExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktQualityOperationExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建品质业务主表查询表达式
    /// </summary>
    /// <param name="queryDto">品质业务主表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktQualityOperation, bool>> QueryExpression(TaktQualityOperationQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktQualityOperation>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.QualityOperationCode!.Contains(queryDto.KeyWords) ||
                x.OperationMonth!.Contains(queryDto.KeyWords) ||
                x.CustomerName!.Contains(queryDto.KeyWords) ||
                x.DebitNoteNo!.Contains(queryDto.KeyWords) ||
                x.Recorder!.Contains(queryDto.KeyWords) ||
                x.CostCurrency!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.QualityOperationCode))
        {
            exp = exp.And(x => x.QualityOperationCode!.Contains(queryDto.QualityOperationCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperationMonth))
        {
            exp = exp.And(x => x.OperationMonth!.Contains(queryDto.OperationMonth));
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerName))
        {
            exp = exp.And(x => x.CustomerName!.Contains(queryDto.CustomerName));
        }

        if (!string.IsNullOrEmpty(queryDto?.DebitNoteNo))
        {
            exp = exp.And(x => x.DebitNoteNo!.Contains(queryDto.DebitNoteNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.Recorder))
        {
            exp = exp.And(x => x.Recorder!.Contains(queryDto.Recorder));
        }

        if (queryDto?.TotalQualityCost.HasValue == true)
        {
            exp = exp.And(x => x.TotalQualityCost == queryDto.TotalQualityCost);
        }

        if (!string.IsNullOrEmpty(queryDto?.CostCurrency))
        {
            exp = exp.And(x => x.CostCurrency!.Contains(queryDto.CostCurrency));
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
