// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Tenant
// 文件名称：TaktTenantService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt租户应用服务，提供租户管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt租户应用服务
/// </summary>
public class TaktTenantService : TaktServiceBase, ITaktTenantService
{
    private readonly ITaktRepository<TaktTenant> _tenantRepository;
    private readonly ITaktRepository<TaktUser> _userRepository;
    private readonly ITaktRepository<TaktUserTenant> _userTenantRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="tenantRepository">租户仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="userTenantRepository">用户租户关联仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktTenantService(
        ITaktRepository<TaktTenant> tenantRepository,
        ITaktRepository<TaktUser> userRepository,
        ITaktRepository<TaktUserTenant> userTenantRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _tenantRepository = tenantRepository;
        _userRepository = userRepository;
        _userTenantRepository = userTenantRepository;
    }

    /// <summary>
    /// 获取租户列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktTenantDto>> GetListAsync(TaktTenantQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _tenantRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktTenantDto>.Create(
            data.Adapt<List<TaktTenantDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>租户DTO</returns>
    public async Task<TaktTenantDto?> GetByIdAsync(long id)
    {
        var tenant = await _tenantRepository.GetByIdAsync(id);
        if (tenant == null) return null;

        return tenant.Adapt<TaktTenantDto>();
    }

    /// <summary>
    /// 获取租户选项列表（用于下拉框等）
    /// </summary>
    /// <returns>租户选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var tenants = await _tenantRepository.FindAsync(t => t.IsDeleted == 0 && t.TenantStatus == 0);
        return tenants
            .OrderBy(t => t.TenantName)
            .ThenBy(t => t.CreateTime)
            .Select(t => new TaktSelectOption
            {
                DictLabel = t.TenantName,
                DictValue = t.Id,
                ExtLabel = t.TenantCode,
                ExtValue = t.ConfigId,
                OrderNum = 0
            })
            .ToList();
    }

    /// <summary>
    /// 创建租户
    /// </summary>
    /// <param name="dto">创建租户DTO</param>
    /// <returns>租户DTO</returns>
    public async Task<TaktTenantDto> CreateAsync(TaktTenantCreateDto dto)
    {
        // 查重验证
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_tenantRepository, t => t.TenantName, dto.TenantName, null, $"租户名称 {dto.TenantName} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_tenantRepository, t => t.TenantCode, dto.TenantCode, null, $"租户编码 {dto.TenantCode} 已存在");

        // 验证 ConfigId 和 TenantCode 的组合唯一性（如果存在）
        var existingTenant = await _tenantRepository.GetAsync(t => t.ConfigId == dto.ConfigId && t.TenantCode == dto.TenantCode && t.IsDeleted == 0);
        if (existingTenant != null)
        {
            throw new TaktBusinessException($"租户编码 {dto.TenantCode} 在配置ID {dto.ConfigId} 下已存在");
        }

        // 使用Mapster映射DTO到实体，然后手动设置状态和时间
        var tenant = dto.Adapt<TaktTenant>();
        tenant.TenantStatus = 0; // 0=启用
        tenant.StartTime = dto.StartTime ?? DateTime.Now;
        tenant.EndTime = dto.EndTime ?? new DateTime(9999, 12, 31);

        tenant = await _tenantRepository.CreateAsync(tenant);

        return tenant.Adapt<TaktTenantDto>();
    }

    /// <summary>
    /// 更新租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <param name="dto">更新租户DTO</param>
    /// <returns>租户DTO</returns>
    public async Task<TaktTenantDto> UpdateAsync(long id, TaktTenantUpdateDto dto)
    {
        var tenant = await _tenantRepository.GetByIdAsync(id);
        if (tenant == null)
            throw new TaktBusinessException("租户不存在");

        // 查重验证（排除当前记录）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_tenantRepository, t => t.TenantName, dto.TenantName, id, $"租户名称 {dto.TenantName} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_tenantRepository, t => t.TenantCode, dto.TenantCode, id, $"租户编码 {dto.TenantCode} 已存在");

        // 验证 ConfigId 和 TenantCode 的组合唯一性（如果存在，排除当前记录）
        var existingTenant = await _tenantRepository.GetAsync(t => t.ConfigId == dto.ConfigId && t.TenantCode == dto.TenantCode && t.Id != id && t.IsDeleted == 0);
        if (existingTenant != null)
        {
            throw new TaktBusinessException($"租户编码 {dto.TenantCode} 在配置ID {dto.ConfigId} 下已存在");
        }

        // 使用Mapster更新实体
        dto.Adapt(tenant, typeof(TaktTenantUpdateDto), typeof(TaktTenant));
        if (dto.StartTime.HasValue)
            tenant.StartTime = dto.StartTime.Value;
        if (dto.EndTime.HasValue)
            tenant.EndTime = dto.EndTime.Value;
        tenant.UpdateTime = DateTime.Now;

        await _tenantRepository.UpdateAsync(tenant);

        return tenant.Adapt<TaktTenantDto>();
    }

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var tenant = await _tenantRepository.GetByIdAsync(id);
        if (tenant == null)
            throw new TaktBusinessException("租户不存在");

        // 1. 先将 TenantStatus 置为禁用（1），再软删除（IsDeleted=1）
        tenant.TenantStatus = 1;
        tenant.UpdateTime = DateTime.Now;
        await _tenantRepository.UpdateAsync(tenant);

        // 2. 软删除租户（IsDeleted = 1）
        await _tenantRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除租户
    /// </summary>
    /// <param name="ids">租户ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有租户记录
        var tenants = await _tenantRepository.FindAsync(t => idList.Contains(t.Id));

        // 1. 先将所有记录的 TenantStatus 置为禁用（1），再软删除（IsDeleted=1）
        foreach (var tenant in tenants)
        {
            tenant.TenantStatus = 1;
            tenant.UpdateTime = DateTime.Now;
            await _tenantRepository.UpdateAsync(tenant);
        }

        // 2. 批量软删除租户（IsDeleted = 1）
        await _tenantRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新租户状态
    /// </summary>
    /// <param name="dto">租户状态DTO</param>
    /// <returns>租户DTO</returns>
    public async Task<TaktTenantDto> UpdateStatusAsync(TaktTenantStatusDto dto)
    {
        var tenant = await _tenantRepository.GetByIdAsync(dto.TenantId);
        if (tenant == null)
            throw new TaktBusinessException("租户不存在");

        tenant.TenantStatus = dto.TenantStatus;
        tenant.UpdateTime = DateTime.Now;

        await _tenantRepository.UpdateAsync(tenant);

        return tenant.Adapt<TaktTenantDto>();
    }

    /// <summary>
    /// 获取租户用户列表
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <returns>租户用户列表</returns>
    public async Task<List<TaktUserTenantDto>> GetUserTenantIdsAsync(long tenantId)
    {
        // 查询租户用户关联
        var userTenants = await _userTenantRepository.FindAsync(ut => ut.TenantId == tenantId && ut.IsDeleted == 0);
        if (userTenants == null || userTenants.Count == 0)
            return new List<TaktUserTenantDto>();

        // 获取租户信息
        var tenant = await _tenantRepository.GetByIdAsync(tenantId);
        if (tenant == null)
            return new List<TaktUserTenantDto>();

        // 获取所有用户ID
        var userIds = userTenants.Select(ut => ut.UserId).Distinct().ToList();

        // 批量查询用户信息
        var users = await _userRepository.FindAsync(u => userIds.Contains(u.Id) && u.IsDeleted == 0);
        var userDict = users.ToDictionary(u => u.Id, u => u);

        // 组装DTO
        var result = new List<TaktUserTenantDto>();
        foreach (var userTenant in userTenants)
        {
            if (userDict.TryGetValue(userTenant.UserId, out var user))
            {
                result.Add(new TaktUserTenantDto
                {
                    UserTenantId = userTenant.Id,
                    UserId = user.Id,
                    UserName = user.UserName,
                    RealName = user.RealName,
                    TenantId = tenant.Id,
                    TenantName = tenant.TenantName,
                    TenantCode = tenant.TenantCode,
                    TenantConfigId = tenant.ConfigId,
                    TenantStatus = tenant.TenantStatus,
                    StartTime = tenant.StartTime,
                    EndTime = tenant.EndTime,
                    ConfigId = userTenant.ConfigId,
                    CreateTime = userTenant.CreateTime,
                    UpdateTime = userTenant.UpdateTime,
                    IsDeleted = userTenant.IsDeleted
                });
            }
        }

        return result;
    }

    /// <summary>
    /// 分配租户用户
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <param name="userIds">用户ID数组</param>
    /// <returns>是否分配成功</returns>
    public async Task<bool> AssignUserTenantsAsync(long tenantId, long[] userIds)
    {
        // 验证租户是否存在
        var tenant = await _tenantRepository.GetByIdAsync(tenantId);
        if (tenant == null)
            throw new TaktBusinessException("租户不存在");

        // 验证用户是否存在
        if (userIds != null && userIds.Length > 0)
        {
            var users = await _userRepository.FindAsync(u => userIds.Contains(u.Id) && u.IsDeleted == 0);
            if (users.Count != userIds.Length)
                throw new TaktBusinessException("部分用户不存在");

            // 禁止对管理员用户进行租户分配（admin、guest）
            var protectedUsers = users.Where(u => IsProtectedUser(u.UserName)).ToList();
            if (protectedUsers.Any())
            {
                var protectedNames = string.Join("、", protectedUsers.Select(u => u.UserName));
                throw new TaktBusinessException($"管理员用户（{protectedNames}）不允许进行租户分配！");
            }
        }

        // 使用软删除+恢复+新增策略
        await UpdateTenantUsersAsync(tenantId, userIds ?? Array.Empty<long>());

        return true;
    }

    /// <summary>
    /// 更新租户用户关联（软删除+恢复+新增策略）
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <param name="userIds">用户ID数组</param>
    private async Task UpdateTenantUsersAsync(long tenantId, long[] userIds)
    {
        // 查询现有的关联（包括已删除的）
        var existingUserTenants = await _userTenantRepository.FindAsync(ut => ut.TenantId == tenantId);
        var existingDict = existingUserTenants.ToDictionary(ut => ut.UserId, ut => ut);

        // 要保留的用户ID集合
        var userIdSet = new HashSet<long>(userIds);

        // 处理现有关联
        foreach (var existing in existingUserTenants)
        {
            if (userIdSet.Contains(existing.UserId))
            {
                // 如果已删除，则恢复
                if (existing.IsDeleted == 1)
                {
                    existing.IsDeleted = 0;
                    existing.UpdateTime = DateTime.Now;
                    await _userTenantRepository.UpdateAsync(existing);
                }
            }
            else
            {
                // 如果不在新列表中，则软删除
                if (existing.IsDeleted == 0)
                {
                    existing.IsDeleted = 1;
                    existing.UpdateTime = DateTime.Now;
                    await _userTenantRepository.UpdateAsync(existing);
                }
            }
        }

        // 添加新的关联
        foreach (var userId in userIds)
        {
            if (!existingDict.ContainsKey(userId))
            {
                var userTenant = new TaktUserTenant
                {
                    TenantId = tenantId,
                    UserId = userId,
                    ConfigId = "0", // 用户租户关联表统一存储在主库
                    CreateTime = DateTime.Now,
                    IsDeleted = 0
                };
                await _userTenantRepository.CreateAsync(userTenant);
            }
        }
    }

    /// <summary>
    /// 判断是否为受保护用户（不允许删除或修改）
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns>是否为受保护用户</returns>
    private bool IsProtectedUser(string userName)
    {
        return userName.Equals("admin", StringComparison.OrdinalIgnoreCase) ||
               userName.Equals("guest", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktTenantTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "租户导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "租户导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入租户
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktTenantImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "租户导入模板" : sheetName
            );

            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }

            // 批量处理导入数据
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3))) // 第3行开始是数据
            {
                try
                {
                    // 验证必填字段
                    if (string.IsNullOrWhiteSpace(item.TenantName))
                    {
                        errors.Add($"第{index}行：租户名称不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.TenantCode))
                    {
                        errors.Add($"第{index}行：租户编码不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.ConfigId))
                    {
                        errors.Add($"第{index}行：配置ID不能为空");
                        fail++;
                        continue;
                    }

                    // 导入时使用验证器手动验证
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_tenantRepository, t => t.TenantName, item.TenantName, null, $"第{index}行：租户名称 {item.TenantName} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_tenantRepository, t => t.TenantCode, item.TenantCode, null, $"第{index}行：租户编码 {item.TenantCode} 已存在");

                    // 验证 ConfigId 和 TenantCode 的组合唯一性
                    var existingTenant = await _tenantRepository.GetAsync(t => t.ConfigId == item.ConfigId && t.TenantCode == item.TenantCode && t.IsDeleted == 0);
                    if (existingTenant != null)
                    {
                        errors.Add($"第{index}行：租户编码 {item.TenantCode} 在配置ID {item.ConfigId} 下已存在");
                        fail++;
                        continue;
                    }

                    // 创建租户实体
                    var tenant = new TaktTenant
                    {
                        TenantName = item.TenantName,
                        TenantCode = item.TenantCode,
                        ConfigId = item.ConfigId,
                        StartTime = item.StartTime,
                        EndTime = item.EndTime,
                        TenantStatus = item.TenantStatus >= 0 ? item.TenantStatus : 0 // 默认为启用（0=启用）
                    };

                    // 保存租户
                    await _tenantRepository.CreateAsync(tenant);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            errors.Add($"导入过程发生错误：{ex.Message}");
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出租户
    /// </summary>
    /// <param name="query">租户查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktTenantQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的租户（不分页）
        List<TaktTenant> tenants;
        if (predicate != null)
        {
            tenants = await _tenantRepository.FindAsync(predicate);
        }
        else
        {
            tenants = await _tenantRepository.GetAllAsync();
        }

        if (tenants == null || tenants.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktTenantExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "租户数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "租户导出" : fileName
            );
        }

        // 转换为导出DTO（使用 Adapt 进行映射）
        var exportData = tenants.Adapt<List<TaktTenantExportDto>>();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "租户数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "租户导出" : fileName
        );
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktTenant, bool>> QueryExpression(TaktTenantQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktTenant>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.TenantName.Contains(queryDto.KeyWords) ||
                              x.TenantCode.Contains(queryDto.KeyWords));
        }

        // 租户名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.TenantName), x => x.TenantName.Contains(queryDto!.TenantName!));

        // 租户编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.TenantCode), x => x.TenantCode.Contains(queryDto!.TenantCode!));

        // 配置ID
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.ConfigId), x => x.ConfigId == queryDto!.ConfigId!);

        // 租户状态
        exp = exp.AndIF(queryDto?.TenantStatus.HasValue == true, x => x.TenantStatus == queryDto!.TenantStatus!.Value);

        return exp.ToExpression();
    }
}
