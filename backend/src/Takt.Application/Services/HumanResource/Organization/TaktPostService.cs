// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Organization
// 文件名称：TaktPostService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt岗位应用服务，提供岗位管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// Takt岗位应用服务
/// </summary>
public class TaktPostService : TaktServiceBase, ITaktPostService
{
    private readonly ITaktRepository<TaktPost> _postRepository;
    private readonly ITaktRepository<TaktUserPost> _postUserRepository;
    private readonly ITaktRepository<TaktUser> _userRepository;
    private readonly ITaktRepository<TaktEmployee> _employeeRepository;
    private readonly ITaktRepository<TaktPostDelegate> _postDelegateRepository;
    private readonly ITaktRepository<TaktDeptDelegate> _deptDelegateRepository;
    private readonly ITaktRepository<TaktEmployeeDelegate> _employeeDelegateRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="postRepository">岗位仓储</param>
    /// <param name="postUserRepository">岗位用户关联仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="employeeRepository">员工仓储（用于展示名）</param>
    /// <param name="postDelegateRepository">岗位代理仓储</param>
    /// <param name="deptDelegateRepository">部门代理仓储（清理岗位规则引用）</param>
    /// <param name="employeeDelegateRepository">员工代理仓储（清理岗位规则引用）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPostService(
        ITaktRepository<TaktPost> postRepository,
        ITaktRepository<TaktUserPost> postUserRepository,
        ITaktRepository<TaktUser> userRepository,
        ITaktRepository<TaktEmployee> employeeRepository,
        ITaktRepository<TaktPostDelegate> postDelegateRepository,
        ITaktRepository<TaktDeptDelegate> deptDelegateRepository,
        ITaktRepository<TaktEmployeeDelegate> employeeDelegateRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _postRepository = postRepository;
        _postUserRepository = postUserRepository;
        _userRepository = userRepository;
        _employeeRepository = employeeRepository;
        _postDelegateRepository = postDelegateRepository;
        _deptDelegateRepository = deptDelegateRepository;
        _employeeDelegateRepository = employeeDelegateRepository;
    }

    /// <summary>
    /// 获取岗位列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPostDto>> GetPostListAsync(TaktPostQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _postRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPostDto>.Create(
            data.Adapt<List<TaktPostDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取岗位
    /// </summary>
    /// <param name="id">岗位ID</param>
    /// <returns>岗位DTO</returns>
    public async Task<TaktPostDto?> GetPostByIdAsync(long id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null) return null;

        var postDto = post.Adapt<TaktPostDto>();

        // 获取岗位用户
        var postUsers = await _postUserRepository.FindAsync(pu => pu.PostId == id && pu.IsDeleted == 0);
        postDto.UserIds = postUsers.Select(pu => pu.UserId).ToList();

        return postDto;
    }

    /// <summary>
    /// 获取岗位选项列表（用于下拉框等）
    /// </summary>
    /// <returns>岗位选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPostOptionsAsync()
    {
        var posts = await _postRepository.FindAsync(p => p.IsDeleted == 0 && p.PostStatus == 0);
        return posts
            .OrderBy(p => p.OrderNum)
            .ThenBy(p => p.CreatedAt)
            .Select(p => new TaktSelectOption
            {
                DictLabel = p.PostName,
                DictValue = p.Id,
                ExtLabel = p.PostCode,
                ExtValue = p.DeptId,
                OrderNum = p.OrderNum
            })
            .ToList();
    }

    /// <summary>
    /// 创建岗位
    /// </summary>
    /// <param name="dto">创建岗位DTO</param>
    /// <returns>岗位DTO</returns>
    public async Task<TaktPostDto> CreatePostAsync(TaktPostCreateDto dto)
    {
        // 查重：岗位名称+岗位编码+岗位类别+岗位级别 组合唯一
        var postName = dto.PostName;
        var postCode = dto.PostCode;
        var postCategory = dto.PostCategory;
        var postLevel = dto.PostLevel;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _postRepository,
            p => p.PostName == postName && p.PostCode == postCode && p.PostCategory == postCategory && p.PostLevel == postLevel,
            null,
            "岗位名称+岗位编码+岗位类别+岗位级别组合已存在");

        // 使用Mapster映射DTO到实体，然后手动设置状态
        var post = dto.Adapt<TaktPost>();
        post.PostStatus = 0; // 0=启用

        post = await _postRepository.CreateAsync(post);

        // 添加岗位用户关联
        if (dto.UserIds != null && dto.UserIds.Any())
        {
            // 验证用户是否存在
            var users = await _userRepository.FindAsync(u => dto.UserIds.Contains(u.Id) && u.IsDeleted == 0);
            if (users.Count != dto.UserIds.Count)
                throw new TaktBusinessException("validation.partialUsersNotFound");

            var postUsers = dto.UserIds.Select(userId => new TaktUserPost
            {
                UserId = userId,
                PostId = post.Id
            }).ToList();

            await _postUserRepository.CreateRangeAsync(postUsers);
        }

        return await GetPostByIdAsync(post.Id) ?? post.Adapt<TaktPostDto>();
    }

    /// <summary>
    /// 更新岗位
    /// </summary>
    /// <param name="id">岗位ID</param>
    /// <param name="dto">更新岗位DTO</param>
    /// <returns>岗位DTO</returns>
    public async Task<TaktPostDto> UpdatePostAsync(long id, TaktPostUpdateDto dto)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            throw new TaktBusinessException("validation.postNotFound");

        // 查重（排除当前记录）：岗位名称+岗位编码+岗位类别+岗位级别 组合唯一
        var postName = dto.PostName;
        var postCode = dto.PostCode;
        var postCategory = dto.PostCategory;
        var postLevel = dto.PostLevel;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _postRepository,
            p => p.PostName == postName && p.PostCode == postCode && p.PostCategory == postCategory && p.PostLevel == postLevel,
            id,
            "岗位名称+岗位编码+岗位类别+岗位级别组合已存在");

        // 使用Mapster更新实体
        dto.Adapt(post, typeof(TaktPostUpdateDto), typeof(TaktPost));
        post.UpdatedAt = DateTime.Now;

        await _postRepository.UpdateAsync(post);

        // 更新岗位用户关联
        if (dto.UserIds != null)
        {
            await AssignUserPostsAsync(id, dto.UserIds.ToArray());
        }

        return await GetPostByIdAsync(id) ?? post.Adapt<TaktPostDto>();
    }

    /// <summary>
    /// 删除岗位
    /// </summary>
    /// <param name="id">岗位ID</param>
    /// <returns>任务</returns>
    public async Task DeletePostByIdAsync(long id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            throw new TaktBusinessException("validation.postNotFound");

        // 1. 先将 PostStatus 置为禁用（1），再软删除（IsDeleted=1）
        post.PostStatus = 1;
        post.UpdatedAt = DateTime.Now;
        await _postRepository.UpdateAsync(post);

        await DeletePostDelegateRowsForPostIdAsync(id);

        // 2. 删除岗位用户关联
        var postUserIds = (await _postUserRepository.FindAsync(pu => pu.PostId == id)).Select(pu => pu.Id).ToList();
        if (postUserIds.Any())
        {
            await _postUserRepository.DeleteAsync(postUserIds);
        }

        // 3. 软删除岗位（IsDeleted = 1）
        await _postRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除岗位
    /// </summary>
    /// <param name="ids">岗位ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePostBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有岗位记录
        var posts = await _postRepository.FindAsync(p => idList.Contains(p.Id));

        // 1. 先将所有记录的 PostStatus 置为禁用（1），再软删除（IsDeleted=1）
        foreach (var post in posts)
        {
            post.PostStatus = 1;
            post.UpdatedAt = DateTime.Now;
            await _postRepository.UpdateAsync(post);
        }

        foreach (var pid in idList)
            await DeletePostDelegateRowsForPostIdAsync(pid);

        // 2. 批量删除岗位用户关联
        var allPostUserIds = (await _postUserRepository.FindAsync(pu => idList.Contains(pu.PostId))).Select(pu => pu.Id).ToList();
        if (allPostUserIds.Any())
        {
            await _postUserRepository.DeleteAsync(allPostUserIds);
        }

        // 3. 批量软删除岗位（IsDeleted = 1）
        await _postRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 删除与岗位相关的代理行：本表 post_id、部门/员工代理中「岗位规则」引用该岗位。
    /// </summary>
    private async Task DeletePostDelegateRowsForPostIdAsync(long postId)
    {
        var ownIds = (await _postDelegateRepository.FindAsync(x => x.PostId == postId)).Select(x => x.Id).ToList();
        if (ownIds.Count > 0)
            await _postDelegateRepository.DeleteAsync(ownIds);

        var deptDelIds = (await _deptDelegateRepository.FindAsync(x => x.DelegatePostId == postId)).Select(x => x.Id).ToList();
        if (deptDelIds.Count > 0)
            await _deptDelegateRepository.DeleteAsync(deptDelIds);

        var empDelIds = (await _employeeDelegateRepository.FindAsync(x => x.DelegatePostId == postId)).Select(x => x.Id).ToList();
        if (empDelIds.Count > 0)
            await _employeeDelegateRepository.DeleteAsync(empDelIds);
    }

    /// <summary>
    /// 更新岗位状态
    /// </summary>
    /// <param name="dto">岗位状态DTO</param>
    /// <returns>岗位DTO</returns>
    public async Task<TaktPostDto> UpdatePostStatusAsync(TaktPostStatusDto dto)
    {
        var post = await _postRepository.GetByIdAsync(dto.PostId);
        if (post == null)
            throw new TaktBusinessException("validation.postNotFound");

        post.PostStatus = dto.PostStatus;
        post.UpdatedAt = DateTime.Now;

        await _postRepository.UpdateAsync(post);

        return post.Adapt<TaktPostDto>();
    }

    /// <summary>
    /// 获取岗位用户列表
    /// </summary>
    /// <param name="postId">岗位ID</param>
    /// <returns>岗位用户列表</returns>
    public async Task<List<TaktUserPostDto>> GetUserPostIdsAsync(long postId)
    {
        // 查询岗位用户关联
        var postUsers = await _postUserRepository.FindAsync(pu => pu.PostId == postId && pu.IsDeleted == 0);
        if (postUsers == null || postUsers.Count == 0)
            return new List<TaktUserPostDto>();

        // 获取岗位信息
        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null)
            return new List<TaktUserPostDto>();

        // 获取所有用户ID
        var userIds = postUsers.Select(pu => pu.UserId).Distinct().ToList();

        // 批量查询用户信息
        var users = await _userRepository.FindAsync(u => userIds.Contains(u.Id) && u.IsDeleted == 0);
        var userDict = users.ToDictionary(u => u.Id, u => u);
        var employeeIds = users.Select(u => u.EmployeeId).Distinct().ToList();
        var employeeById = employeeIds.Count > 0
            ? (await _employeeRepository.FindAsync(e => employeeIds.Contains(e.Id) && e.IsDeleted == 0)).ToDictionary(e => e.Id)
            : new Dictionary<long, TaktEmployee>();
        var employeesByUserId = users.Where(u => employeeById.ContainsKey(u.EmployeeId))
            .ToDictionary(u => u.Id, u => employeeById[u.EmployeeId]);

        // 组装DTO
        var result = new List<TaktUserPostDto>();
        foreach (var postUser in postUsers)
        {
            if (userDict.TryGetValue(postUser.UserId, out var user))
            {
                var displayName = employeesByUserId.TryGetValue(user.Id, out var emp) && !string.IsNullOrWhiteSpace(emp.RealName) ? emp.RealName.Trim() : user.UserName;
                result.Add(new TaktUserPostDto
                {
                    UserPostId = postUser.Id,
                    PostId = post.Id,
                    PostName = post.PostName,
                    PostCode = post.PostCode,
                    UserId = user.Id,
                    UserName = user.UserName,
                    RealName = displayName,
                    ConfigId = postUser.ConfigId,
                    CreatedAt = postUser.CreatedAt,
                    UpdatedAt = postUser.UpdatedAt,
                    IsDeleted = postUser.IsDeleted
                });
            }
        }

        return result;
    }

    /// <summary>
    /// 分配用户岗位
    /// </summary>
    /// <param name="postId">岗位ID</param>
    /// <param name="userIds">用户ID集合</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignUserPostsAsync(long postId, long[] userIds)
    {
        // 验证岗位是否存在
        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null)
            throw new TaktBusinessException("validation.postNotFound");

        // 验证用户是否存在
        if (userIds != null && userIds.Length > 0)
        {
            var users = await _userRepository.FindAsync(u => userIds.Contains(u.Id) && u.IsDeleted == 0);
            if (users.Count != userIds.Length)
                throw new TaktBusinessException("validation.partialUsersNotFound");
        }

        // 获取岗位现有关联的用户（包括已删除的）
        var existingPostUsers = await _postUserRepository.FindAsync(pu => pu.PostId == postId);
        var userIdsArray = userIds ?? Array.Empty<long>();

        // 1. 找出需要标记删除的关联（在现有关联中但不在新的用户列表中）
        var usersToDelete = existingPostUsers.Where(pu => !userIdsArray.Contains(pu.UserId) && pu.IsDeleted == 0).ToList();
        if (usersToDelete.Any())
        {
            await _postUserRepository.DeleteAsync(usersToDelete.Select(pu => pu.Id));
        }

        // 2. 处理需要恢复的关联（在新的用户列表中且已存在但被标记为删除）
        var usersToRestore = existingPostUsers.Where(pu => userIdsArray.Contains(pu.UserId) && pu.IsDeleted == 1).ToList();
        if (usersToRestore.Any())
        {
            foreach (var postUser in usersToRestore)
            {
                postUser.IsDeleted = 0;
                postUser.UpdatedAt = DateTime.Now;
                await _postUserRepository.UpdateAsync(postUser);
            }
        }

        // 3. 找出需要新增的关联（在新的用户列表中且不存在任何记录）
        var existingUserIds = existingPostUsers.Select(pu => pu.UserId).ToList();
        var usersToAdd = userIdsArray.Where(userId => !existingUserIds.Contains(userId))
            .Select(userId => new TaktUserPost
            {
                PostId = postId,
                UserId = userId
            }).ToList();

        if (usersToAdd.Any())
        {
            await _postUserRepository.CreateRangeAsync(usersToAdd);
        }

        return true;
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPostTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPost));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPostTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }

    /// <summary>
    /// 导入岗位
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPostAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktPost));
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktPostImportDto>(
                fileStream,
                excelSheet
            );

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

            // 预加载已有：岗位名称+岗位编码+岗位类别+岗位级别 组合唯一
            var existingPosts = await _postRepository.FindAsync(p => p.IsDeleted == 0);
            var existingKeys = existingPosts
                .Where(p => !string.IsNullOrWhiteSpace(p.PostName) && !string.IsNullOrWhiteSpace(p.PostCode))
                .Select(p => (p.PostName!.Trim().ToUpperInvariant(), p.PostCode!.Trim().ToUpperInvariant(), (p.PostCategory ?? "").Trim().ToUpperInvariant(), p.PostLevel))
                .ToHashSet();
            var addedKeys = new HashSet<(string, string, string, int)>();
            var postsToInsert = new List<TaktPost>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.PostName))
                    {
                        AddImportError(errors, "validation.importRowPostNameRequired", index);
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.PostCode))
                    {
                        AddImportError(errors, "validation.importRowPostCodeRequired", index);
                        fail++;
                        continue;
                    }

                    var name = item.PostName.Trim();
                    var code = item.PostCode.Trim();
                    var category = item.PostCategory ?? string.Empty;
                    var level = item.PostLevel;
                    var key = (name.ToUpperInvariant(), code.ToUpperInvariant(), category.ToUpperInvariant(), level);
                    if (existingKeys.Contains(key) || addedKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowPostDuplicateComposite", index);
                        fail++;
                        continue;
                    }

                    var post = new TaktPost
                    {
                        PostName = item.PostName,
                        PostCode = item.PostCode,
                        DeptId = item.DeptId,
                        PostCategory = item.PostCategory ?? string.Empty,
                        PostLevel = item.PostLevel,
                        PostDuty = item.PostDuty,
                        OrderNum = item.OrderNum,
                        DataScope = item.DataScope,
                        PostStatus = item.PostStatus >= 0 ? item.PostStatus : 0,
                        Remark = item.Remark
                    };

                    postsToInsert.Add(post);
                    addedKeys.Add(key);
                }
                catch (TaktBusinessException ex)
                {
                    AddImportError(errors, "validation.importRowUnhandledException", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
                catch (Exception ex)
                {
                    AddImportError(errors, "validation.importRowFailedWithReason", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
            }

            for (var i = 0; i < postsToInsert.Count; i += importBatchSize)
            {
                var batch = postsToInsert.Skip(i).Take(importBatchSize).ToList();
                try
                {
                    await _postRepository.CreateRangeBulkAsync(batch);
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
    /// 导出岗位
    /// </summary>
    /// <param name="query">岗位查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPostAsync(TaktPostQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的岗位（不分页）
        List<TaktPost> posts;
        if (predicate != null)
        {
            posts = await _postRepository.FindAsync(predicate);
        }
        else
        {
            posts = await _postRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPost));
        if (posts == null || posts.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPostExportDto>(),
                excelSheet,
                excelFile
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = posts.Select(p =>
        {
            var dto = p.Adapt<TaktPostExportDto>();
            // 处理需要特殊转换的字段
            dto.PostCategory = p.PostCategory ?? string.Empty;
            dto.DataScope = GetDataScopeString(p.DataScope);
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPost, bool>> QueryExpression(TaktPostQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktPost>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.PostName.Contains(queryDto.KeyWords) ||
                              x.PostCode.Contains(queryDto.KeyWords));
        }

        // 岗位名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PostName), x => x.PostName.Contains(queryDto!.PostName!));

        // 岗位编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PostCode), x => x.PostCode.Contains(queryDto!.PostCode!));

        // 部门ID
        exp = exp.AndIF(queryDto?.DeptId.HasValue == true, x => x.DeptId == queryDto!.DeptId!.Value);

        // 岗位状态
        exp = exp.AndIF(queryDto?.PostStatus.HasValue == true, x => x.PostStatus == queryDto!.PostStatus!.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 获取数据范围字符串
    /// </summary>
    private string GetDataScopeString(int dataScope)
    {
        return dataScope switch
        {
            0 => "全部数据",
            1 => "本部门数据",
            2 => "本部门及以下数据",
            3 => "仅本人数据",
            4 => "自定义数据范围",
            _ => "未知"
        };
    }
}