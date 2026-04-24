// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：TaktEmployeeService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工应用服务，提供员工选项及员工维度的部门/岗位维护（人事侧）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using Mapster;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Application.Services;
using Takt.Application.Services.Routine.Tasks.NumberingRule.RuleEngine;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// Takt员工应用服务
/// </summary>
public class TaktEmployeeService : TaktServiceBase, ITaktEmployeeService
{
    private const string RuleEmployeeMale = "EMPLOYEE_MALE";
    private const string RuleEmployeeFemale = "EMPLOYEE_FEMALE";
    private const string RuleEmployeeSystem = "EMPLOYEE_SYSTEM";

    private readonly ITaktRepository<TaktEmployee> _employeeRepository;
    private readonly ITaktNumberingRuleEngine _numberingRuleEngine;
    private readonly ITaktRepository<TaktEmployeeDept> _employeeDeptRepository;
    private readonly ITaktRepository<TaktEmployeePost> _employeePostRepository;
    private readonly ITaktRepository<TaktDept> _deptRepository;
    private readonly ITaktRepository<TaktPost> _postRepository;
    private readonly ITaktRepository<TaktUser> _userRepository;
    private readonly ITaktRepository<TaktEmployeeDelegate> _employeeDelegateRepository;
    private readonly ITaktRepository<TaktDeptDelegate> _deptDelegateRepository;
    private readonly ITaktRepository<TaktPostDelegate> _postDelegateRepository;
    private readonly ITaktRepository<TaktEmployeeCareer> _employeeCareerRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="employeeRepository">员工仓储</param>
    /// <param name="employeeDeptRepository">员工部门关联仓储</param>
    /// <param name="employeePostRepository">员工岗位关联仓储</param>
    /// <param name="deptRepository">部门仓储</param>
    /// <param name="postRepository">岗位仓储</param>
    /// <param name="userRepository">用户仓储（用于过滤已绑定员工）</param>
    /// <param name="employeeDelegateRepository">员工代理仓储</param>
    /// <param name="deptDelegateRepository">部门代理仓储（清理直接代理员工引用）</param>
    /// <param name="postDelegateRepository">岗位代理仓储（清理直接代理员工引用）</param>
    /// <param name="employeeCareerRepository">员工履历仓储（用于统计工龄）</param>
    /// <param name="numberingRuleEngine">编码规则引擎（员工编号：男前缀 1 / 女前缀 2 / 系统前缀 9）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEmployeeService(
        ITaktRepository<TaktEmployee> employeeRepository,
        ITaktRepository<TaktEmployeeDept> employeeDeptRepository,
        ITaktRepository<TaktEmployeePost> employeePostRepository,
        ITaktRepository<TaktDept> deptRepository,
        ITaktRepository<TaktPost> postRepository,
        ITaktRepository<TaktUser> userRepository,
        ITaktRepository<TaktEmployeeDelegate> employeeDelegateRepository,
        ITaktRepository<TaktDeptDelegate> deptDelegateRepository,
        ITaktRepository<TaktPostDelegate> postDelegateRepository,
        ITaktRepository<TaktEmployeeCareer> employeeCareerRepository,
        ITaktNumberingRuleEngine numberingRuleEngine,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _employeeRepository = employeeRepository;
        _employeeDeptRepository = employeeDeptRepository;
        _employeePostRepository = employeePostRepository;
        _deptRepository = deptRepository;
        _postRepository = postRepository;
        _userRepository = userRepository;
        _employeeDelegateRepository = employeeDelegateRepository;
        _deptDelegateRepository = deptDelegateRepository;
        _postDelegateRepository = postDelegateRepository;
        _employeeCareerRepository = employeeCareerRepository;
        _numberingRuleEngine = numberingRuleEngine;
    }

    #region 员工主档 CRUD

    /// <summary>
    /// 获取员工列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEmployeeDto>> GetEmployeeListAsync(TaktEmployeeQueryDto queryDto)
        {
        var predicate = QueryExpression(queryDto);
            var (data, total) = await _employeeRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
            var dtos = data.OrderBy(e => e.EmployeeCode).Adapt<List<TaktEmployeeDto>>();
            return TaktPagedResult<TaktEmployeeDto>.Create(dtos, total, queryDto.PageIndex, queryDto.PageSize);
    }

    /// <summary>
    /// 根据 ID 获取员工
    /// </summary>
    /// <param name="id">员工 ID</param>
    /// <returns>员工 DTO，不存在或已删除时返回 null</returns>
    public async Task<TaktEmployeeDto?> GetEmployeeByIdAsync(long id)
    {
            var entity = await _employeeRepository.GetByIdAsync(id);
            if (entity == null || entity.IsDeleted != 0)
                return null;
        return entity.Adapt<TaktEmployeeDto>();
    }

    /// <summary>
    /// 创建员工
    /// </summary>
    /// <param name="dto">创建员工DTO</param>
    /// <returns>员工DTO</returns>
    public async Task<TaktEmployeeDto> CreateEmployeeAsync(TaktEmployeeCreateDto dto)
    {
        if (dto.IsSystemEmployeeCode == true)
        {
            var t = _userContext?.GetCurrentUserType();
            if (t != 1 && t != 2)
                throw new TaktBusinessException("validation.employeeOnlyAdminCanCreateSystemCode");
        }

        var code = await GenerateEmployeeCodeAsync(dto.Gender, dto.IsSystemEmployeeCode == true);
        var idCard = dto.IdCard ?? string.Empty;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _employeeRepository,
            e => e.EmployeeCode == code && (e.IdCard ?? "") == idCard,
            null,
            "员工编码+身份证号组合已存在");

        var entity = dto.Adapt<TaktEmployee>();
        entity.EmployeeCode = code;
        entity = await _employeeRepository.CreateAsync(entity);
        return await GetEmployeeByIdAsync(entity.Id) ?? entity.Adapt<TaktEmployeeDto>();
    }

    /// <summary>
    /// 按 TaktNumberingRuleEngine 与种子规则 EMPLOYEE_MALE / EMPLOYEE_FEMALE / EMPLOYEE_SYSTEM 生成员工编码。
    /// </summary>
    private async Task<string> GenerateEmployeeCodeAsync(int gender, bool isSystemEmployeeCode)
    {
        if (isSystemEmployeeCode)
            return await _numberingRuleEngine.GenerateNextAsync(RuleEmployeeSystem);

        if (gender == 1)
            return await _numberingRuleEngine.GenerateNextAsync(RuleEmployeeMale);
        if (gender == 2)
            return await _numberingRuleEngine.GenerateNextAsync(RuleEmployeeFemale);

        throw new TaktBusinessException("validation.employeeNonSystemGenderRequired");
    }

    /// <summary>
    /// 更新员工
    /// </summary>
    /// <param name="id">员工ID</param>
    /// <param name="dto">更新员工DTO</param>
    /// <returns>员工DTO</returns>
    public async Task<TaktEmployeeDto> UpdateEmployeeAsync(long id, TaktEmployeeUpdateDto dto)
    {
            var entity = await _employeeRepository.GetByIdAsync(id);
            if (entity == null || entity.IsDeleted != 0)
                ThrowBusinessException("validation.hrEmployeeNotFound");

        // 去重（排除当前记录）：员工编码+身份证号 组合唯一
        var code = dto.EmployeeCode ?? string.Empty;
        var idCard = dto.IdCard ?? string.Empty;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _employeeRepository,
            e => e.EmployeeCode == code && (e.IdCard ?? "") == idCard,
            id,
            "员工编码+身份证号组合已存在");

            dto.Adapt(entity!, typeof(TaktEmployeeUpdateDto), typeof(TaktEmployee));
            await _employeeRepository.UpdateAsync(entity!);
        return await GetEmployeeByIdAsync(id) ?? entity!.Adapt<TaktEmployeeDto>();
    }

    /// <summary>
    /// 删除员工
    /// </summary>
    /// <param name="id">员工ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeByIdAsync(long id)
    {
        await DeleteEmployeeBatchAsync(new[] { id });
    }

    /// <summary>
    /// 批量删除员工
    /// </summary>
    /// <param name="ids">员工ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeBatchAsync(IEnumerable<long> ids)
    {
            var list = await _employeeRepository.FindAsync(e => ids.Contains(e.Id));
            if (!list.Any())
                return;

            var empIds = list.Select(e => e.Id).ToList();
            var edByOwner = (await _employeeDelegateRepository.FindAsync(x => empIds.Contains(x.EmployeeId))).Select(x => x.Id).ToList();
            var edByTarget = (await _employeeDelegateRepository.FindAsync(x => x.DelegateEmployeeId.HasValue && empIds.Contains(x.DelegateEmployeeId.Value))).Select(x => x.Id).ToList();
            var allEdIds = edByOwner.Union(edByTarget).Distinct().ToList();
            if (allEdIds.Count > 0)
                await _employeeDelegateRepository.DeleteAsync(allEdIds);

            var ddIds = (await _deptDelegateRepository.FindAsync(x => x.DelegateEmployeeId.HasValue && empIds.Contains(x.DelegateEmployeeId.Value))).Select(x => x.Id).ToList();
            if (ddIds.Count > 0)
                await _deptDelegateRepository.DeleteAsync(ddIds);

            var pdIds = (await _postDelegateRepository.FindAsync(x => x.DelegateEmployeeId.HasValue && empIds.Contains(x.DelegateEmployeeId.Value))).Select(x => x.Id).ToList();
            if (pdIds.Count > 0)
                await _postDelegateRepository.DeleteAsync(pdIds);

            foreach (var e in list)
            {
                e.IsDeleted = 1;
                e.UpdatedAt = DateTime.Now;
            }

        await _employeeRepository.UpdateRangeBulkAsync(list);
    }

    /// <summary>
    /// 导出员工数据
    /// </summary>
    /// <param name="query">查询 DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与内容</returns>
    public async Task<(string fileName, byte[] content)> ExportEmployeeAsync(TaktEmployeeQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        var list = await _employeeRepository.FindAsync(predicate);
        list = list.OrderBy(e => e.EmployeeCode).ToList();
        var exportRows = list.Adapt<List<TaktEmployeeDto>>().Adapt<List<TaktEmployeeExportDto>>();
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployee));
        return await TaktExcelHelper.ExportAsync(
            exportRows,
            excelSheet,
            excelFile);
    }

    /// <summary>
    /// 获取员工导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与内容</returns>
    public async Task<(string fileName, byte[] content)> GetEmployeeTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEmployee));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEmployeeTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <summary>
    /// 导入员工数据
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEmployeeAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktEmployee));
            var importData = await TaktExcelHelper.ImportAsync<TaktEmployeeImportDto>(fileStream, excelSheet);
            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            // 先判断本次导入总记录数：超过1000条直接拒绝导入
            const int maxImportRowsPerFile = 1000;
            if (importData.Count > maxImportRowsPerFile)
            {
                AddImportError(errors, "validation.importRecordCountExceedsLimit", importData.Count, maxImportRowsPerFile);
                return (0, importData.Count, errors);
            }

            // 预加载已存在组合键：员工编码 + 身份证号（与 CreateAsync 唯一性规则一致）
            var existingEmployees = await _employeeRepository.FindAsync(e => e.IsDeleted == 0);
            var existingKeys = existingEmployees
                .Select(e => (
                    (e.EmployeeCode ?? string.Empty).Trim().ToUpperInvariant(),
                    (e.IdCard ?? string.Empty).Trim().ToUpperInvariant()))
                .ToHashSet();
            var addedKeys = new HashSet<(string, string)>();
            var employeesToInsert = new List<TaktEmployee>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                if (IsSkippableEmployeeImportRow(item))
                    continue;

                try
                {
                    var createDto = BuildCreateDtoFromImport(item);
                    if (createDto.Gender != 1 && createDto.Gender != 2)
                    {
                        AddImportError(errors, "validation.importRowEmployeeGenderInvalid", index);
                        fail++;
                        continue;
                    }

                    var code = await GenerateEmployeeCodeAsync(createDto.Gender, createDto.IsSystemEmployeeCode == true);
                    var idCard = (createDto.IdCard ?? string.Empty).Trim().ToUpperInvariant();
                    var key = (code.Trim().ToUpperInvariant(), idCard);

                    if (existingKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowEmployeeCodeIdDuplicate", index);
                        fail++;
                        continue;
                    }
                    if (addedKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowEmployeeCodeIdDuplicateInFile", index);
                        fail++;
                        continue;
                    }

                    var entity = createDto.Adapt<TaktEmployee>();
                    entity.EmployeeCode = code;

                    employeesToInsert.Add(entity);
                    addedKeys.Add(key);
                }
                catch (Exception ex)
                {
                    AddImportError(errors, "validation.importRowUnhandledException", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
            }

            for (var i = 0; i < employeesToInsert.Count; i += importBatchSize)
            {
                var batch = employeesToInsert.Skip(i).Take(importBatchSize).ToList();
                try
                {
                    await _employeeRepository.CreateRangeBulkAsync(batch);
                    success += batch.Count;
                }
                catch (Exception ex)
                {
                    fail += batch.Count;
                    AddImportError(errors, "validation.importBatchInsertFailed", i + 1, i + batch.Count, GetLocalizedExceptionMessage(ex));
                }
            }
        }
        catch (Exception ex)
        {
            AddImportError(errors, "validation.importProcessFailedWithReason", GetLocalizedExceptionMessage(ex));
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 模板中未填任何关键字段的空行（Excel 尾部常出现），跳过不计入成功/失败。
    /// </summary>
    private static bool IsSkippableEmployeeImportRow(TaktEmployeeImportDto item)
    {
        static bool Has(string? s) => !string.IsNullOrWhiteSpace(s);
        if (Has(item.RealName)) return false;
        if (Has(item.IdCard) || Has(item.Phone) || Has(item.Email)) return false;
        if (Has(item.NativePlace) || Has(item.CurrentAddress) || Has(item.Remark) || Has(item.EmployeeCode)) return false;
        if (item.Gender != 0 || item.EmployeeStatus != 0 || item.BirthDate != null) return false;
        return true;
    }

    /// <summary>
    /// 将导入行映射为创建 DTO（员工编号由 <see cref="CreateEmployeeAsync"/> 按性别规则生成，模板中 EmployeeCode 列不参与创建）。
    /// </summary>
    private static TaktEmployeeCreateDto BuildCreateDtoFromImport(TaktEmployeeImportDto item)
    {
        var realName = item.RealName?.Trim() ?? string.Empty;

        return new TaktEmployeeCreateDto
        {
            IsSystemEmployeeCode = false,
            RealName = realName,
            Gender = item.Gender,
            BirthDate = item.BirthDate,
            IdCard = item.IdCard?.Trim() ?? string.Empty,
            Phone = string.IsNullOrWhiteSpace(item.Phone) ? null : item.Phone.Trim(),
            Email = string.IsNullOrWhiteSpace(item.Email) ? null : item.Email.Trim(),
            NativePlace = string.IsNullOrWhiteSpace(item.NativePlace) ? null : item.NativePlace.Trim(),
            CurrentAddress = string.IsNullOrWhiteSpace(item.CurrentAddress) ? null : item.CurrentAddress.Trim(),
            EmployeeStatus = item.EmployeeStatus,
            Remark = string.IsNullOrWhiteSpace(item.Remark) ? null : item.Remark.Trim()
        };
    }

    /// <summary>
    /// 构建员工查询条件表达式
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEmployee, bool>> QueryExpression(TaktEmployeeQueryDto queryDto)
    {
        var exp = SqlSugar.Expressionable.Create<TaktEmployee>();
        exp = exp.And(e => e.IsDeleted == 0);
        if (!string.IsNullOrWhiteSpace(queryDto.EmployeeCode))
            exp = exp.And(e => e.EmployeeCode.Contains(queryDto.EmployeeCode.Trim()));
        if (!string.IsNullOrWhiteSpace(queryDto.RealName))
            exp = exp.And(e => e.RealName.Contains(queryDto.RealName.Trim()));
        if (!string.IsNullOrWhiteSpace(queryDto.Phone))
            exp = exp.And(e => e.Phone != null && e.Phone.Contains(queryDto.Phone.Trim()));
        if (queryDto.EmployeeStatus.HasValue)
            exp = exp.And(e => e.EmployeeStatus == queryDto.EmployeeStatus.Value);
        return exp.ToExpression();
    }

    #endregion

    /// <summary>
    /// 获取员工选项列表（用于下拉框等，仅在职、未删除）
    /// </summary>
    /// <param name="excludeBoundToUser">是否排除已被用户表关联（TaktUser.EmployeeId）的员工</param>
    /// <returns>员工选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEmployeeOptionsAsync(bool excludeBoundToUser = false)
    {
        var employees = await _employeeRepository.FindAsync(e => e.IsDeleted == 0 && e.EmployeeStatus == 0);

        IEnumerable<TaktEmployee> filtered = employees;
        if (excludeBoundToUser)
        {
            var boundEmployeeIds = (await _userRepository.FindAsync(u => u.IsDeleted == 0))
                .Select(u => u.EmployeeId)
                .ToHashSet();
            filtered = filtered.Where(e => !boundEmployeeIds.Contains(e.Id));
        }

        return filtered
            .OrderBy(e => e.EmployeeCode)
            .ThenBy(e => e.RealName)
            .Select(e => new TaktSelectOption
            {
                DictLabel = e.RealName?.Trim() ?? e.EmployeeCode ?? string.Empty,
                DictValue = e.Id,
                ExtLabel = e.EmployeeCode,
                SortOrder = 0
            })
            .ToList();
    }

    /// <summary>
    /// 获取员工的部门列表（人事侧）
    /// </summary>
    /// <param name="employeeId">员工 ID</param>
    /// <returns>员工部门关联列表</returns>
    public async Task<List<TaktEmployeeDeptDto>> GetEmployeeDeptsAsync(long employeeId)
    {
        var employeeDepts = await _employeeDeptRepository.FindAsync(ed => ed.EmployeeId == employeeId && ed.IsDeleted == 0);
        if (employeeDepts == null || employeeDepts.Count == 0)
            return new List<TaktEmployeeDeptDto>();

        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee == null)
            return new List<TaktEmployeeDeptDto>();

        var deptIds = employeeDepts.Select(ed => ed.DeptId).Distinct().ToList();
        var depts = await _deptRepository.FindAsync(d => deptIds.Contains(d.Id) && d.IsDeleted == 0);
        var deptDict = depts.ToDictionary(d => d.Id, d => d);

        var result = new List<TaktEmployeeDeptDto>();
        foreach (var ed in employeeDepts)
        {
            if (deptDict.TryGetValue(ed.DeptId, out var dept))
            {
                result.Add(new TaktEmployeeDeptDto
                {
                    EmployeeDeptId = ed.Id,
                    EmployeeId = employee.Id,
                    EmployeeName = employee.RealName ?? string.Empty,
                    EmployeeCode = employee.EmployeeCode ?? string.Empty,
                    DeptId = dept.Id,
                    DeptName = dept.DeptName,
                    DeptCode = dept.DeptCode,
                    ConfigId = ed.ConfigId,
                    CreatedAt = ed.CreatedAt,
                    UpdatedAt = ed.UpdatedAt,
                    IsDeleted = ed.IsDeleted
                });
            }
        }
        return result;
    }

    /// <summary>
    /// 分配员工部门（人事侧，替换该员工当前部门关联）
    /// </summary>
    /// <param name="employeeId">员工 ID</param>
    /// <param name="deptIds">部门 ID 数组</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignEmployeeDeptsAsync(long employeeId, long[] deptIds)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee == null)
            throw new TaktBusinessException("validation.hrEmployeeNotFound");

        if (deptIds != null && deptIds.Length > 0)
        {
            var depts = await _deptRepository.FindAsync(d => deptIds.Contains(d.Id) && d.IsDeleted == 0);
            if (depts.Count != deptIds.Length)
                throw new TaktBusinessException("validation.partialDeptsNotFound");
        }

        var existing = await _employeeDeptRepository.FindAsync(ed => ed.EmployeeId == employeeId);
        var toDelete = existing.Where(ed => !(deptIds ?? Array.Empty<long>()).Contains(ed.DeptId) && ed.IsDeleted == 0).ToList();
        if (toDelete.Any())
            await _employeeDeptRepository.DeleteAsync(toDelete.Select(ed => ed.Id));

        var toRestore = existing.Where(ed => (deptIds ?? Array.Empty<long>()).Contains(ed.DeptId) && ed.IsDeleted == 1).ToList();
        foreach (var ed in toRestore)
        {
            ed.IsDeleted = 0;
            ed.UpdatedAt = DateTime.Now;
            await _employeeDeptRepository.UpdateAsync(ed);
        }

        var existingDeptIds = existing.Select(ed => ed.DeptId).ToList();
        var toAdd = (deptIds ?? Array.Empty<long>()).Where(did => !existingDeptIds.Contains(did))
            .Select(did => new TaktEmployeeDept { EmployeeId = employeeId, DeptId = did }).ToList();
        if (toAdd.Any())
            await _employeeDeptRepository.CreateRangeAsync(toAdd);

        return true;
    }

    /// <summary>
    /// 获取员工的岗位列表（人事侧）
    /// </summary>
    /// <param name="employeeId">员工 ID</param>
    /// <returns>员工岗位关联列表</returns>
    public async Task<List<TaktEmployeePostDto>> GetEmployeePostsAsync(long employeeId)
    {
        var employeePosts = await _employeePostRepository.FindAsync(ep => ep.EmployeeId == employeeId && ep.IsDeleted == 0);
        if (employeePosts == null || employeePosts.Count == 0)
            return new List<TaktEmployeePostDto>();

        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee == null)
            return new List<TaktEmployeePostDto>();

        var postIds = employeePosts.Select(ep => ep.PostId).Distinct().ToList();
        var posts = await _postRepository.FindAsync(p => postIds.Contains(p.Id) && p.IsDeleted == 0);
        var postDict = posts.ToDictionary(p => p.Id, p => p);

        var result = new List<TaktEmployeePostDto>();
        foreach (var ep in employeePosts)
        {
            if (postDict.TryGetValue(ep.PostId, out var post))
            {
                result.Add(new TaktEmployeePostDto
                {
                    EmployeePostId = ep.Id,
                    EmployeeId = employee.Id,
                    EmployeeName = employee.RealName ?? string.Empty,
                    EmployeeCode = employee.EmployeeCode ?? string.Empty,
                    PostId = post.Id,
                    PostName = post.PostName,
                    PostCode = post.PostCode,
                    ConfigId = ep.ConfigId,
                    CreatedAt = ep.CreatedAt,
                    UpdatedAt = ep.UpdatedAt,
                    IsDeleted = ep.IsDeleted
                });
            }
        }
        return result;
    }

    /// <summary>
    /// 分配员工岗位（人事侧，替换该员工当前岗位关联）
    /// </summary>
    /// <param name="employeeId">员工 ID</param>
    /// <param name="postIds">岗位 ID 数组</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignEmployeePostsAsync(long employeeId, long[] postIds)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee == null)
            throw new TaktBusinessException("validation.hrEmployeeNotFound");

        if (postIds != null && postIds.Length > 0)
        {
            var posts = await _postRepository.FindAsync(p => postIds.Contains(p.Id) && p.IsDeleted == 0);
            if (posts.Count != postIds.Length)
                throw new TaktBusinessException("validation.partialPostsNotFound");
        }

        var existing = await _employeePostRepository.FindAsync(ep => ep.EmployeeId == employeeId);
        var toDelete = existing.Where(ep => !(postIds ?? Array.Empty<long>()).Contains(ep.PostId) && ep.IsDeleted == 0).ToList();
        if (toDelete.Any())
            await _employeePostRepository.DeleteAsync(toDelete.Select(ep => ep.Id));

        var toRestore = existing.Where(ep => (postIds ?? Array.Empty<long>()).Contains(ep.PostId) && ep.IsDeleted == 1).ToList();
        foreach (var ep in toRestore)
        {
            ep.IsDeleted = 0;
            ep.UpdatedAt = DateTime.Now;
            await _employeePostRepository.UpdateAsync(ep);
        }

        var existingPostIds = existing.Select(ep => ep.PostId).ToList();
        var toAdd = (postIds ?? Array.Empty<long>()).Where(pid => !existingPostIds.Contains(pid))
            .Select(pid => new TaktEmployeePost { EmployeeId = employeeId, PostId = pid }).ToList();
        if (toAdd.Any())
            await _employeePostRepository.CreateRangeAsync(toAdd);

        return true;
    }

    #region 员工统计分析

    /// <summary>
    /// 统计人员总数
    /// </summary>
    public async Task<long> GetEmployeeCountAsync()
    {
        return await _employeeRepository.CountAsync(e => e.IsDeleted == 0);
    }

    /// <summary>
    /// 统计人员平均年龄
    /// </summary>
    public async Task<double?> GetAverageAgeAsync()
    {
        var employees = await _employeeRepository.FindAsync(e => e.IsDeleted == 0 && e.Age.HasValue);
        if (!employees.Any()) return null;
        return employees.Average(e => e.Age!.Value);
    }

    /// <summary>
    /// 统计人员平均工龄
    /// </summary>
    public async Task<double?> GetAverageWorkYearsAsync()
    {
        // 通过员工履历表获取入职日期
        var careers = await _employeeCareerRepository.FindAsync(c => c.IsDeleted == 0 && c.JoinDate.HasValue);
        if (!careers.Any()) return null;

        var now = DateTime.Now;
        var workYearsList = careers
            .Where(c => c.JoinDate.HasValue)
            .Select(c => (now - c.JoinDate!.Value).TotalDays / 365.25)
            .ToList();

        return workYearsList.Any() ? workYearsList.Average() : null;
    }

    /// <summary>
    /// 统计最大年龄
    /// </summary>
    public async Task<int?> GetMaxAgeAsync()
    {
        var employees = await _employeeRepository.FindAsync(e => e.IsDeleted == 0 && e.Age.HasValue);
        if (!employees.Any()) return null;
        return employees.Max(e => e.Age!.Value);
    }

    /// <summary>
    /// 统计最小年龄
    /// </summary>
    public async Task<int?> GetMinAgeAsync()
    {
        var employees = await _employeeRepository.FindAsync(e => e.IsDeleted == 0 && e.Age.HasValue);
        if (!employees.Any()) return null;
        return employees.Min(e => e.Age!.Value);
    }

    /// <summary>
    /// 统计最长工龄
    /// </summary>
    public async Task<int?> GetMaxWorkYearsAsync()
    {
        var careers = await _employeeCareerRepository.FindAsync(c => c.IsDeleted == 0 && c.JoinDate.HasValue);
        if (!careers.Any()) return null;

        var now = DateTime.Now;
        var maxYears = careers
            .Where(c => c.JoinDate.HasValue)
            .Max(c => (int)((now - c.JoinDate!.Value).TotalDays / 365.25));

        return maxYears;
    }

    /// <summary>
    /// 统计最短工龄
    /// </summary>
    public async Task<int?> GetMinWorkYearsAsync()
    {
        var careers = await _employeeCareerRepository.FindAsync(c => c.IsDeleted == 0 && c.JoinDate.HasValue);
        if (!careers.Any()) return null;

        var now = DateTime.Now;
        var minYears = careers
            .Where(c => c.JoinDate.HasValue)
            .Min(c => (int)((now - c.JoinDate!.Value).TotalDays / 365.25));

        return minYears;
    }

    /// <summary>
    /// 按籍贯统计人员分布
    /// </summary>
    public async Task<Dictionary<string, int>> GetEmployeeCountByNativePlaceAsync()
    {
        var employees = await _employeeRepository.FindAsync(e => e.IsDeleted == 0);
        
        return employees
            .Where(e => !string.IsNullOrEmpty(e.NativePlace))
            .GroupBy(e => e.NativePlace!)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    #endregion
}
