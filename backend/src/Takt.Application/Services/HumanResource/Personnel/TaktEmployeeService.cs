// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：TaktEmployeeService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：员工信息表应用服务，提供Employee管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工信息表应用服务
/// </summary>
public class TaktEmployeeService : TaktServiceBase, ITaktEmployeeService
{
    private readonly ITaktRepository<TaktEmployee> _repository;
    private readonly ITaktRepository<TaktEmployeeDelegate> _employeeDelegateRepository;
    private readonly ITaktRepository<TaktEmployeeCareer> _employeeCareerRepository;
    private readonly ITaktRepository<TaktEmployeeAttachment> _employeeAttachmentRepository;
    private readonly ITaktRepository<TaktEmployeeContract> _employeeContractRepository;
    private readonly ITaktRepository<TaktEmployeeEducation> _employeeEducationRepository;
    private readonly ITaktRepository<TaktEmployeeFamily> _employeeFamilyRepository;
    private readonly ITaktRepository<TaktEmployeeSkill> _employeeSkillRepository;
    private readonly ITaktRepository<TaktEmployeeTransfer> _employeeTransferRepository;
    private readonly ITaktRepository<TaktEmployeeWork> _employeeWorkRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Employee仓储</param>
    /// <param name="employeeDelegateRepository">EmployeeDelegate仓储</param>
    /// <param name="employeeCareerRepository">EmployeeCareer仓储</param>
    /// <param name="employeeAttachmentRepository">EmployeeAttachment仓储</param>
    /// <param name="employeeContractRepository">EmployeeContract仓储</param>
    /// <param name="employeeEducationRepository">EmployeeEducation仓储</param>
    /// <param name="employeeFamilyRepository">EmployeeFamily仓储</param>
    /// <param name="employeeSkillRepository">EmployeeSkill仓储</param>
    /// <param name="employeeTransferRepository">EmployeeTransfer仓储</param>
    /// <param name="employeeWorkRepository">EmployeeWork仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEmployeeService(
        ITaktRepository<TaktEmployee> repository,
        ITaktRepository<TaktEmployeeDelegate> employeeDelegateRepository,
        ITaktRepository<TaktEmployeeCareer> employeeCareerRepository,
        ITaktRepository<TaktEmployeeAttachment> employeeAttachmentRepository,
        ITaktRepository<TaktEmployeeContract> employeeContractRepository,
        ITaktRepository<TaktEmployeeEducation> employeeEducationRepository,
        ITaktRepository<TaktEmployeeFamily> employeeFamilyRepository,
        ITaktRepository<TaktEmployeeSkill> employeeSkillRepository,
        ITaktRepository<TaktEmployeeTransfer> employeeTransferRepository,
        ITaktRepository<TaktEmployeeWork> employeeWorkRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _employeeDelegateRepository = employeeDelegateRepository;
        _employeeCareerRepository = employeeCareerRepository;
        _employeeAttachmentRepository = employeeAttachmentRepository;
        _employeeContractRepository = employeeContractRepository;
        _employeeEducationRepository = employeeEducationRepository;
        _employeeFamilyRepository = employeeFamilyRepository;
        _employeeSkillRepository = employeeSkillRepository;
        _employeeTransferRepository = employeeTransferRepository;
        _employeeWorkRepository = employeeWorkRepository;
    }


    /// <summary>
    /// 获取员工信息表(Employee)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEmployeeDto>> GetEmployeeListAsync(TaktEmployeeQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEmployeeDto>.Create(
            data.Adapt<List<TaktEmployeeDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取员工信息表(Employee)
    /// </summary>
    /// <param name="id">员工信息表(Employee)ID</param>
    /// <returns>员工信息表(Employee)DTO</returns>
    public async Task<TaktEmployeeDto?> GetEmployeeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktEmployeeDto>();
        
        // 手动加载子表
        dto.EmployeeDelegates = (await _employeeDelegateRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0))
            .Adapt<List<TaktEmployeeDelegateDto>>();
        dto.EmployeeCareers = (await _employeeCareerRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0))
            .Adapt<List<TaktEmployeeCareerDto>>();
        dto.EmployeeAttachments = (await _employeeAttachmentRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0))
            .Adapt<List<TaktEmployeeAttachmentDto>>();
        dto.EmployeeContracts = (await _employeeContractRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0))
            .Adapt<List<TaktEmployeeContractDto>>();
        dto.EmployeeEducations = (await _employeeEducationRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0))
            .Adapt<List<TaktEmployeeEducationDto>>();
        dto.EmployeeFamilies = (await _employeeFamilyRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0))
            .Adapt<List<TaktEmployeeFamilyDto>>();
        dto.EmployeeSkills = (await _employeeSkillRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0))
            .Adapt<List<TaktEmployeeSkillDto>>();
        dto.EmployeeTransfers = (await _employeeTransferRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0))
            .Adapt<List<TaktEmployeeTransferDto>>();
        dto.EmployeeWorks = (await _employeeWorkRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0))
            .Adapt<List<TaktEmployeeWorkDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取员工信息表(Employee)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>员工信息表(Employee)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEmployeeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.EmployeeStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.RealName ?? string.Empty,
            DictValue = x.EmployeeCode

        }).ToList();
    }


    /// <summary>
    /// 创建员工信息表(Employee)
    /// </summary>
    /// <param name="dto">创建员工信息表(Employee)DTO</param>
    /// <returns>员工信息表(Employee)DTO</returns>
    public async Task<TaktEmployeeDto> CreateEmployeeAsync(TaktEmployeeCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.EmployeeCode, dto.EmployeeCode, null, $"员工信息表编码 {dto.EmployeeCode} 已存在");

        var entity = dto.Adapt<TaktEmployee>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建EmployeeDelegate列表
            if (dto.EmployeeDelegates != null && dto.EmployeeDelegates.Count > 0)
            {
                var employeeDelegateList = dto.EmployeeDelegates.Select(x => {
                    var childEntity = x.Adapt<TaktEmployeeDelegate>();
                    childEntity.EmployeeId = entity.Id;
                    return childEntity;
                }).ToList();
                await _employeeDelegateRepository.CreateRangeBulkAsync(employeeDelegateList);
            }
            // 创建EmployeeCareer列表
            if (dto.EmployeeCareers != null && dto.EmployeeCareers.Count > 0)
            {
                var employeeCareerList = dto.EmployeeCareers.Select(x => {
                    var childEntity = x.Adapt<TaktEmployeeCareer>();
                    childEntity.EmployeeId = entity.Id;
                    return childEntity;
                }).ToList();
                await _employeeCareerRepository.CreateRangeBulkAsync(employeeCareerList);
            }
            // 创建EmployeeAttachment列表
            if (dto.EmployeeAttachments != null && dto.EmployeeAttachments.Count > 0)
            {
                var employeeAttachmentList = dto.EmployeeAttachments.Select(x => {
                    var childEntity = x.Adapt<TaktEmployeeAttachment>();
                    childEntity.EmployeeId = entity.Id;
                    return childEntity;
                }).ToList();
                await _employeeAttachmentRepository.CreateRangeBulkAsync(employeeAttachmentList);
            }
            // 创建EmployeeContract列表
            if (dto.EmployeeContracts != null && dto.EmployeeContracts.Count > 0)
            {
                var employeeContractList = dto.EmployeeContracts.Select(x => {
                    var childEntity = x.Adapt<TaktEmployeeContract>();
                    childEntity.EmployeeId = entity.Id;
                    return childEntity;
                }).ToList();
                await _employeeContractRepository.CreateRangeBulkAsync(employeeContractList);
            }
            // 创建EmployeeEducation列表
            if (dto.EmployeeEducations != null && dto.EmployeeEducations.Count > 0)
            {
                var employeeEducationList = dto.EmployeeEducations.Select(x => {
                    var childEntity = x.Adapt<TaktEmployeeEducation>();
                    childEntity.EmployeeId = entity.Id;
                    return childEntity;
                }).ToList();
                await _employeeEducationRepository.CreateRangeBulkAsync(employeeEducationList);
            }
            // 创建EmployeeFamily列表
            if (dto.EmployeeFamilies != null && dto.EmployeeFamilies.Count > 0)
            {
                var employeeFamilyList = dto.EmployeeFamilies.Select(x => {
                    var childEntity = x.Adapt<TaktEmployeeFamily>();
                    childEntity.EmployeeId = entity.Id;
                    return childEntity;
                }).ToList();
                await _employeeFamilyRepository.CreateRangeBulkAsync(employeeFamilyList);
            }
            // 创建EmployeeSkill列表
            if (dto.EmployeeSkills != null && dto.EmployeeSkills.Count > 0)
            {
                var employeeSkillList = dto.EmployeeSkills.Select(x => {
                    var childEntity = x.Adapt<TaktEmployeeSkill>();
                    childEntity.EmployeeId = entity.Id;
                    return childEntity;
                }).ToList();
                await _employeeSkillRepository.CreateRangeBulkAsync(employeeSkillList);
            }
            // 创建EmployeeTransfer列表
            if (dto.EmployeeTransfers != null && dto.EmployeeTransfers.Count > 0)
            {
                var employeeTransferList = dto.EmployeeTransfers.Select(x => {
                    var childEntity = x.Adapt<TaktEmployeeTransfer>();
                    childEntity.EmployeeId = entity.Id;
                    return childEntity;
                }).ToList();
                await _employeeTransferRepository.CreateRangeBulkAsync(employeeTransferList);
            }
            // 创建EmployeeWork列表
            if (dto.EmployeeWorks != null && dto.EmployeeWorks.Count > 0)
            {
                var employeeWorkList = dto.EmployeeWorks.Select(x => {
                    var childEntity = x.Adapt<TaktEmployeeWork>();
                    childEntity.EmployeeId = entity.Id;
                    return childEntity;
                }).ToList();
                await _employeeWorkRepository.CreateRangeBulkAsync(employeeWorkList);
            }
        }

        return (await GetEmployeeByIdAsync(entity.Id)) ?? entity.Adapt<TaktEmployeeDto>();
    }


    /// <summary>
    /// 更新员工信息表(Employee)
    /// </summary>
    /// <param name="id">员工信息表(Employee)ID</param>
    /// <param name="dto">更新员工信息表(Employee)DTO</param>
    /// <returns>员工信息表(Employee)DTO</returns>
    public async Task<TaktEmployeeDto> UpdateEmployeeAsync(long id, TaktEmployeeUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.EmployeeCode, dto.EmployeeCode, id, $"员工信息表编码 {dto.EmployeeCode} 已存在");

        dto.Adapt(entity, typeof(TaktEmployeeUpdateDto), typeof(TaktEmployee));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的EmployeeDelegate列表
        var oldEmployeeDelegates = await _employeeDelegateRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (oldEmployeeDelegates != null && oldEmployeeDelegates.Count > 0)
        {
            foreach (var oldEmployeeDelegate in oldEmployeeDelegates)
            {
                oldEmployeeDelegate.IsDeleted = 1;
            }
            await _employeeDelegateRepository.UpdateRangeBulkAsync(oldEmployeeDelegates);
        }

        // 创建新的EmployeeDelegate列表
        if (dto.EmployeeDelegates != null && dto.EmployeeDelegates.Count > 0)
        {
            var employeeDelegateList = dto.EmployeeDelegates.Select(x => {
                var childEntity = x.Adapt<TaktEmployeeDelegate>();
                childEntity.EmployeeId = id;
                return childEntity;
            }).ToList();
            await _employeeDelegateRepository.CreateRangeBulkAsync(employeeDelegateList);
        }
        // 删除旧的EmployeeCareer列表
        var oldEmployeeCareers = await _employeeCareerRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (oldEmployeeCareers != null && oldEmployeeCareers.Count > 0)
        {
            foreach (var oldEmployeeCareer in oldEmployeeCareers)
            {
                oldEmployeeCareer.IsDeleted = 1;
            }
            await _employeeCareerRepository.UpdateRangeBulkAsync(oldEmployeeCareers);
        }

        // 创建新的EmployeeCareer列表
        if (dto.EmployeeCareers != null && dto.EmployeeCareers.Count > 0)
        {
            var employeeCareerList = dto.EmployeeCareers.Select(x => {
                var childEntity = x.Adapt<TaktEmployeeCareer>();
                childEntity.EmployeeId = id;
                return childEntity;
            }).ToList();
            await _employeeCareerRepository.CreateRangeBulkAsync(employeeCareerList);
        }
        // 删除旧的EmployeeAttachment列表
        var oldEmployeeAttachments = await _employeeAttachmentRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (oldEmployeeAttachments != null && oldEmployeeAttachments.Count > 0)
        {
            foreach (var oldEmployeeAttachment in oldEmployeeAttachments)
            {
                oldEmployeeAttachment.IsDeleted = 1;
            }
            await _employeeAttachmentRepository.UpdateRangeBulkAsync(oldEmployeeAttachments);
        }

        // 创建新的EmployeeAttachment列表
        if (dto.EmployeeAttachments != null && dto.EmployeeAttachments.Count > 0)
        {
            var employeeAttachmentList = dto.EmployeeAttachments.Select(x => {
                var childEntity = x.Adapt<TaktEmployeeAttachment>();
                childEntity.EmployeeId = id;
                return childEntity;
            }).ToList();
            await _employeeAttachmentRepository.CreateRangeBulkAsync(employeeAttachmentList);
        }
        // 删除旧的EmployeeContract列表
        var oldEmployeeContracts = await _employeeContractRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (oldEmployeeContracts != null && oldEmployeeContracts.Count > 0)
        {
            foreach (var oldEmployeeContract in oldEmployeeContracts)
            {
                oldEmployeeContract.IsDeleted = 1;
            }
            await _employeeContractRepository.UpdateRangeBulkAsync(oldEmployeeContracts);
        }

        // 创建新的EmployeeContract列表
        if (dto.EmployeeContracts != null && dto.EmployeeContracts.Count > 0)
        {
            var employeeContractList = dto.EmployeeContracts.Select(x => {
                var childEntity = x.Adapt<TaktEmployeeContract>();
                childEntity.EmployeeId = id;
                return childEntity;
            }).ToList();
            await _employeeContractRepository.CreateRangeBulkAsync(employeeContractList);
        }
        // 删除旧的EmployeeEducation列表
        var oldEmployeeEducations = await _employeeEducationRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (oldEmployeeEducations != null && oldEmployeeEducations.Count > 0)
        {
            foreach (var oldEmployeeEducation in oldEmployeeEducations)
            {
                oldEmployeeEducation.IsDeleted = 1;
            }
            await _employeeEducationRepository.UpdateRangeBulkAsync(oldEmployeeEducations);
        }

        // 创建新的EmployeeEducation列表
        if (dto.EmployeeEducations != null && dto.EmployeeEducations.Count > 0)
        {
            var employeeEducationList = dto.EmployeeEducations.Select(x => {
                var childEntity = x.Adapt<TaktEmployeeEducation>();
                childEntity.EmployeeId = id;
                return childEntity;
            }).ToList();
            await _employeeEducationRepository.CreateRangeBulkAsync(employeeEducationList);
        }
        // 删除旧的EmployeeFamily列表
        var oldEmployeeFamilys = await _employeeFamilyRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (oldEmployeeFamilys != null && oldEmployeeFamilys.Count > 0)
        {
            foreach (var oldEmployeeFamily in oldEmployeeFamilys)
            {
                oldEmployeeFamily.IsDeleted = 1;
            }
            await _employeeFamilyRepository.UpdateRangeBulkAsync(oldEmployeeFamilys);
        }

        // 创建新的EmployeeFamily列表
        if (dto.EmployeeFamilies != null && dto.EmployeeFamilies.Count > 0)
        {
            var employeeFamilyList = dto.EmployeeFamilies.Select(x => {
                var childEntity = x.Adapt<TaktEmployeeFamily>();
                childEntity.EmployeeId = id;
                return childEntity;
            }).ToList();
            await _employeeFamilyRepository.CreateRangeBulkAsync(employeeFamilyList);
        }
        // 删除旧的EmployeeSkill列表
        var oldEmployeeSkills = await _employeeSkillRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (oldEmployeeSkills != null && oldEmployeeSkills.Count > 0)
        {
            foreach (var oldEmployeeSkill in oldEmployeeSkills)
            {
                oldEmployeeSkill.IsDeleted = 1;
            }
            await _employeeSkillRepository.UpdateRangeBulkAsync(oldEmployeeSkills);
        }

        // 创建新的EmployeeSkill列表
        if (dto.EmployeeSkills != null && dto.EmployeeSkills.Count > 0)
        {
            var employeeSkillList = dto.EmployeeSkills.Select(x => {
                var childEntity = x.Adapt<TaktEmployeeSkill>();
                childEntity.EmployeeId = id;
                return childEntity;
            }).ToList();
            await _employeeSkillRepository.CreateRangeBulkAsync(employeeSkillList);
        }
        // 删除旧的EmployeeTransfer列表
        var oldEmployeeTransfers = await _employeeTransferRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (oldEmployeeTransfers != null && oldEmployeeTransfers.Count > 0)
        {
            foreach (var oldEmployeeTransfer in oldEmployeeTransfers)
            {
                oldEmployeeTransfer.IsDeleted = 1;
            }
            await _employeeTransferRepository.UpdateRangeBulkAsync(oldEmployeeTransfers);
        }

        // 创建新的EmployeeTransfer列表
        if (dto.EmployeeTransfers != null && dto.EmployeeTransfers.Count > 0)
        {
            var employeeTransferList = dto.EmployeeTransfers.Select(x => {
                var childEntity = x.Adapt<TaktEmployeeTransfer>();
                childEntity.EmployeeId = id;
                return childEntity;
            }).ToList();
            await _employeeTransferRepository.CreateRangeBulkAsync(employeeTransferList);
        }
        // 删除旧的EmployeeWork列表
        var oldEmployeeWorks = await _employeeWorkRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (oldEmployeeWorks != null && oldEmployeeWorks.Count > 0)
        {
            foreach (var oldEmployeeWork in oldEmployeeWorks)
            {
                oldEmployeeWork.IsDeleted = 1;
            }
            await _employeeWorkRepository.UpdateRangeBulkAsync(oldEmployeeWorks);
        }

        // 创建新的EmployeeWork列表
        if (dto.EmployeeWorks != null && dto.EmployeeWorks.Count > 0)
        {
            var employeeWorkList = dto.EmployeeWorks.Select(x => {
                var childEntity = x.Adapt<TaktEmployeeWork>();
                childEntity.EmployeeId = id;
                return childEntity;
            }).ToList();
            await _employeeWorkRepository.CreateRangeBulkAsync(employeeWorkList);
        }


        return (await GetEmployeeByIdAsync(id)) ?? entity.Adapt<TaktEmployeeDto>();
    }


    /// <summary>
    /// 删除员工信息表(Employee)
    /// </summary>
    /// <param name="id">员工信息表(Employee)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeNotFound");
        
        // 级联删除子表数据
        // 级联删除EmployeeDelegate列表
        var employeeDelegates = await _employeeDelegateRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (employeeDelegates != null && employeeDelegates.Count > 0)
        {
            foreach (var employeeDelegate in employeeDelegates)
            {
                employeeDelegate.IsDeleted = 1;
            }
            await _employeeDelegateRepository.UpdateRangeBulkAsync(employeeDelegates);
        }
        // 级联删除EmployeeCareer列表
        var employeeCareers = await _employeeCareerRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (employeeCareers != null && employeeCareers.Count > 0)
        {
            foreach (var employeeCareer in employeeCareers)
            {
                employeeCareer.IsDeleted = 1;
            }
            await _employeeCareerRepository.UpdateRangeBulkAsync(employeeCareers);
        }
        // 级联删除EmployeeAttachment列表
        var employeeAttachments = await _employeeAttachmentRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (employeeAttachments != null && employeeAttachments.Count > 0)
        {
            foreach (var employeeAttachment in employeeAttachments)
            {
                employeeAttachment.IsDeleted = 1;
            }
            await _employeeAttachmentRepository.UpdateRangeBulkAsync(employeeAttachments);
        }
        // 级联删除EmployeeContract列表
        var employeeContracts = await _employeeContractRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (employeeContracts != null && employeeContracts.Count > 0)
        {
            foreach (var employeeContract in employeeContracts)
            {
                employeeContract.IsDeleted = 1;
            }
            await _employeeContractRepository.UpdateRangeBulkAsync(employeeContracts);
        }
        // 级联删除EmployeeEducation列表
        var employeeEducations = await _employeeEducationRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (employeeEducations != null && employeeEducations.Count > 0)
        {
            foreach (var employeeEducation in employeeEducations)
            {
                employeeEducation.IsDeleted = 1;
            }
            await _employeeEducationRepository.UpdateRangeBulkAsync(employeeEducations);
        }
        // 级联删除EmployeeFamily列表
        var employeeFamilys = await _employeeFamilyRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (employeeFamilys != null && employeeFamilys.Count > 0)
        {
            foreach (var employeeFamily in employeeFamilys)
            {
                employeeFamily.IsDeleted = 1;
            }
            await _employeeFamilyRepository.UpdateRangeBulkAsync(employeeFamilys);
        }
        // 级联删除EmployeeSkill列表
        var employeeSkills = await _employeeSkillRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (employeeSkills != null && employeeSkills.Count > 0)
        {
            foreach (var employeeSkill in employeeSkills)
            {
                employeeSkill.IsDeleted = 1;
            }
            await _employeeSkillRepository.UpdateRangeBulkAsync(employeeSkills);
        }
        // 级联删除EmployeeTransfer列表
        var employeeTransfers = await _employeeTransferRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (employeeTransfers != null && employeeTransfers.Count > 0)
        {
            foreach (var employeeTransfer in employeeTransfers)
            {
                employeeTransfer.IsDeleted = 1;
            }
            await _employeeTransferRepository.UpdateRangeBulkAsync(employeeTransfers);
        }
        // 级联删除EmployeeWork列表
        var employeeWorks = await _employeeWorkRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
        if (employeeWorks != null && employeeWorks.Count > 0)
        {
            foreach (var employeeWork in employeeWorks)
            {
                employeeWork.IsDeleted = 1;
            }
            await _employeeWorkRepository.UpdateRangeBulkAsync(employeeWorks);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.EmployeeStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除员工信息表(Employee)
    /// </summary>
    /// <param name="ids">员工信息表(Employee)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktEmployee>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除EmployeeDelegate列表
        var employeeDelegatesToDelete = new List<TaktEmployeeDelegate>();
        foreach (var id in idList)
        {
            var employeeDelegates = await _employeeDelegateRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
            if (employeeDelegates != null && employeeDelegates.Count > 0)
            {
                employeeDelegatesToDelete.AddRange(employeeDelegates);
            }
        }
        
        if (employeeDelegatesToDelete.Count > 0)
        {
            foreach (var employeeDelegate in employeeDelegatesToDelete)
            {
                employeeDelegate.IsDeleted = 1;
            }
            await _employeeDelegateRepository.UpdateRangeBulkAsync(employeeDelegatesToDelete);
        }
        // 批量级联删除EmployeeCareer列表
        var employeeCareersToDelete = new List<TaktEmployeeCareer>();
        foreach (var id in idList)
        {
            var employeeCareers = await _employeeCareerRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
            if (employeeCareers != null && employeeCareers.Count > 0)
            {
                employeeCareersToDelete.AddRange(employeeCareers);
            }
        }
        
        if (employeeCareersToDelete.Count > 0)
        {
            foreach (var employeeCareer in employeeCareersToDelete)
            {
                employeeCareer.IsDeleted = 1;
            }
            await _employeeCareerRepository.UpdateRangeBulkAsync(employeeCareersToDelete);
        }
        // 批量级联删除EmployeeAttachment列表
        var employeeAttachmentsToDelete = new List<TaktEmployeeAttachment>();
        foreach (var id in idList)
        {
            var employeeAttachments = await _employeeAttachmentRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
            if (employeeAttachments != null && employeeAttachments.Count > 0)
            {
                employeeAttachmentsToDelete.AddRange(employeeAttachments);
            }
        }
        
        if (employeeAttachmentsToDelete.Count > 0)
        {
            foreach (var employeeAttachment in employeeAttachmentsToDelete)
            {
                employeeAttachment.IsDeleted = 1;
            }
            await _employeeAttachmentRepository.UpdateRangeBulkAsync(employeeAttachmentsToDelete);
        }
        // 批量级联删除EmployeeContract列表
        var employeeContractsToDelete = new List<TaktEmployeeContract>();
        foreach (var id in idList)
        {
            var employeeContracts = await _employeeContractRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
            if (employeeContracts != null && employeeContracts.Count > 0)
            {
                employeeContractsToDelete.AddRange(employeeContracts);
            }
        }
        
        if (employeeContractsToDelete.Count > 0)
        {
            foreach (var employeeContract in employeeContractsToDelete)
            {
                employeeContract.IsDeleted = 1;
            }
            await _employeeContractRepository.UpdateRangeBulkAsync(employeeContractsToDelete);
        }
        // 批量级联删除EmployeeEducation列表
        var employeeEducationsToDelete = new List<TaktEmployeeEducation>();
        foreach (var id in idList)
        {
            var employeeEducations = await _employeeEducationRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
            if (employeeEducations != null && employeeEducations.Count > 0)
            {
                employeeEducationsToDelete.AddRange(employeeEducations);
            }
        }
        
        if (employeeEducationsToDelete.Count > 0)
        {
            foreach (var employeeEducation in employeeEducationsToDelete)
            {
                employeeEducation.IsDeleted = 1;
            }
            await _employeeEducationRepository.UpdateRangeBulkAsync(employeeEducationsToDelete);
        }
        // 批量级联删除EmployeeFamily列表
        var employeeFamilysToDelete = new List<TaktEmployeeFamily>();
        foreach (var id in idList)
        {
            var employeeFamilys = await _employeeFamilyRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
            if (employeeFamilys != null && employeeFamilys.Count > 0)
            {
                employeeFamilysToDelete.AddRange(employeeFamilys);
            }
        }
        
        if (employeeFamilysToDelete.Count > 0)
        {
            foreach (var employeeFamily in employeeFamilysToDelete)
            {
                employeeFamily.IsDeleted = 1;
            }
            await _employeeFamilyRepository.UpdateRangeBulkAsync(employeeFamilysToDelete);
        }
        // 批量级联删除EmployeeSkill列表
        var employeeSkillsToDelete = new List<TaktEmployeeSkill>();
        foreach (var id in idList)
        {
            var employeeSkills = await _employeeSkillRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
            if (employeeSkills != null && employeeSkills.Count > 0)
            {
                employeeSkillsToDelete.AddRange(employeeSkills);
            }
        }
        
        if (employeeSkillsToDelete.Count > 0)
        {
            foreach (var employeeSkill in employeeSkillsToDelete)
            {
                employeeSkill.IsDeleted = 1;
            }
            await _employeeSkillRepository.UpdateRangeBulkAsync(employeeSkillsToDelete);
        }
        // 批量级联删除EmployeeTransfer列表
        var employeeTransfersToDelete = new List<TaktEmployeeTransfer>();
        foreach (var id in idList)
        {
            var employeeTransfers = await _employeeTransferRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
            if (employeeTransfers != null && employeeTransfers.Count > 0)
            {
                employeeTransfersToDelete.AddRange(employeeTransfers);
            }
        }
        
        if (employeeTransfersToDelete.Count > 0)
        {
            foreach (var employeeTransfer in employeeTransfersToDelete)
            {
                employeeTransfer.IsDeleted = 1;
            }
            await _employeeTransferRepository.UpdateRangeBulkAsync(employeeTransfersToDelete);
        }
        // 批量级联删除EmployeeWork列表
        var employeeWorksToDelete = new List<TaktEmployeeWork>();
        foreach (var id in idList)
        {
            var employeeWorks = await _employeeWorkRepository.FindAsync(x => x.EmployeeId == id && x.IsDeleted == 0);
            if (employeeWorks != null && employeeWorks.Count > 0)
            {
                employeeWorksToDelete.AddRange(employeeWorks);
            }
        }
        
        if (employeeWorksToDelete.Count > 0)
        {
            foreach (var employeeWork in employeeWorksToDelete)
            {
                employeeWork.IsDeleted = 1;
            }
            await _employeeWorkRepository.UpdateRangeBulkAsync(employeeWorksToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 EmployeeStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.EmployeeStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新员工信息表(Employee)状态
    /// </summary>
    /// <param name="dto">员工信息表(Employee)状态DTO</param>
    /// <returns>员工信息表(Employee)DTO</returns>
    public async Task<TaktEmployeeDto> UpdateEmployeeStatusAsync(TaktEmployeeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.EmployeeId);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeNotFound");
        entity.EmployeeStatus = dto.EmployeeStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetEmployeeByIdAsync(entity.Id) ?? entity.Adapt<TaktEmployeeDto>();
    }


    /// <summary>
    /// 获取员工信息表(Employee)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetEmployeeTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEmployee));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEmployeeTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入员工信息表(Employee)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEmployeeAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktEmployee));
        var importData = await TaktExcelHelper.ImportAsync<TaktEmployeeImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktEmployee>();
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
    /// 导出员工信息表(Employee)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEmployeeAsync(TaktEmployeeQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEmployeeQueryDto());
        List<TaktEmployee> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployee));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEmployeeExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktEmployeeExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建员工信息表查询表达式
    /// </summary>
    /// <param name="queryDto">员工信息表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEmployee, bool>> QueryExpression(TaktEmployeeQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktEmployee>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.EmployeeCode!.Contains(queryDto.KeyWords) ||
                x.RealName!.Contains(queryDto.KeyWords) ||
                x.FormerName!.Contains(queryDto.KeyWords) ||
                x.FullName!.Contains(queryDto.KeyWords) ||
                x.NativeName!.Contains(queryDto.KeyWords) ||
                x.DisplayName!.Contains(queryDto.KeyWords) ||
                x.IdCard!.Contains(queryDto.KeyWords) ||
                x.Phone!.Contains(queryDto.KeyWords) ||
                x.Email!.Contains(queryDto.KeyWords) ||
                x.Avatar!.Contains(queryDto.KeyWords) ||
                x.NativePlace!.Contains(queryDto.KeyWords) ||
                x.CurrentAddress!.Contains(queryDto.KeyWords) ||
                x.RegisteredAddress!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.EmployeeCode))
        {
            exp = exp.And(x => x.EmployeeCode!.Contains(queryDto.EmployeeCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.RealName))
        {
            exp = exp.And(x => x.RealName!.Contains(queryDto.RealName));
        }

        if (!string.IsNullOrEmpty(queryDto?.FormerName))
        {
            exp = exp.And(x => x.FormerName!.Contains(queryDto.FormerName));
        }

        if (!string.IsNullOrEmpty(queryDto?.FullName))
        {
            exp = exp.And(x => x.FullName!.Contains(queryDto.FullName));
        }

        if (!string.IsNullOrEmpty(queryDto?.NativeName))
        {
            exp = exp.And(x => x.NativeName!.Contains(queryDto.NativeName));
        }

        if (!string.IsNullOrEmpty(queryDto?.DisplayName))
        {
            exp = exp.And(x => x.DisplayName!.Contains(queryDto.DisplayName));
        }

        if (queryDto?.Gender.HasValue == true)
        {
            exp = exp.And(x => x.Gender == queryDto.Gender);
        }

        if (queryDto?.BirthDate.HasValue == true)
        {
            exp = exp.And(x => x.BirthDate == queryDto.BirthDate);
        }

        if (queryDto?.Age.HasValue == true)
        {
            exp = exp.And(x => x.Age == queryDto.Age);
        }

        if (!string.IsNullOrEmpty(queryDto?.IdCard))
        {
            exp = exp.And(x => x.IdCard!.Contains(queryDto.IdCard));
        }

        if (!string.IsNullOrEmpty(queryDto?.Phone))
        {
            exp = exp.And(x => x.Phone!.Contains(queryDto.Phone));
        }

        if (!string.IsNullOrEmpty(queryDto?.Email))
        {
            exp = exp.And(x => x.Email!.Contains(queryDto.Email));
        }

        if (!string.IsNullOrEmpty(queryDto?.Avatar))
        {
            exp = exp.And(x => x.Avatar!.Contains(queryDto.Avatar));
        }

        if (queryDto?.Nationality.HasValue == true)
        {
            exp = exp.And(x => x.Nationality == queryDto.Nationality);
        }

        if (queryDto?.Political.HasValue == true)
        {
            exp = exp.And(x => x.Political == queryDto.Political);
        }

        if (queryDto?.Marital.HasValue == true)
        {
            exp = exp.And(x => x.Marital == queryDto.Marital);
        }

        if (!string.IsNullOrEmpty(queryDto?.NativePlace))
        {
            exp = exp.And(x => x.NativePlace!.Contains(queryDto.NativePlace));
        }

        if (!string.IsNullOrEmpty(queryDto?.CurrentAddress))
        {
            exp = exp.And(x => x.CurrentAddress!.Contains(queryDto.CurrentAddress));
        }

        if (!string.IsNullOrEmpty(queryDto?.RegisteredAddress))
        {
            exp = exp.And(x => x.RegisteredAddress!.Contains(queryDto.RegisteredAddress));
        }

        if (queryDto?.EmployeeStatus.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeStatus == queryDto.EmployeeStatus);
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

        // BirthDate 日期范围查询
        if (queryDto?.BirthDateStart.HasValue == true)
        {
            exp = exp.And(x => x.BirthDate >= queryDto.BirthDateStart);
        }
        if (queryDto?.BirthDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.BirthDate <= queryDto.BirthDateEnd);
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
