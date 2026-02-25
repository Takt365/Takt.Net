// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Organization
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

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="postRepository">岗位仓储</param>
    /// <param name="postUserRepository">岗位用户关联仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPostService(
        ITaktRepository<TaktPost> postRepository,
        ITaktRepository<TaktUserPost> postUserRepository,
        ITaktRepository<TaktUser> userRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _postRepository = postRepository;
        _postUserRepository = postUserRepository;
        _userRepository = userRepository;
    }

    /// <summary>
    /// 获取岗位列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPostDto>> GetListAsync(TaktPostQueryDto queryDto)
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
    public async Task<TaktPostDto?> GetByIdAsync(long id)
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
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var posts = await _postRepository.FindAsync(p => p.IsDeleted == 0 && p.PostStatus == 0);
        return posts
            .OrderBy(p => p.OrderNum)
            .ThenBy(p => p.CreateTime)
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
    public async Task<TaktPostDto> CreateAsync(TaktPostCreateDto dto)
    {
        // 查重验证（PostName、PostCode、PostCategory、PostLevel 任意一个重复都报错）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_postRepository, p => p.PostName, dto.PostName, null, null, $"岗位名称 {dto.PostName} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_postRepository, p => p.PostCode, dto.PostCode, null, null, $"岗位编码 {dto.PostCode} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_postRepository, p => p.PostCategory, dto.PostCategory, null, null, $"岗位类别 {dto.PostCategory} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_postRepository, p => p.PostLevel, dto.PostLevel, null, null, $"岗位级别 {dto.PostLevel} 已存在");

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
                throw new TaktBusinessException("部分用户不存在");

            var postUsers = dto.UserIds.Select(userId => new TaktUserPost
            {
                UserId = userId,
                PostId = post.Id
            }).ToList();

            await _postUserRepository.CreateRangeAsync(postUsers);
        }

        return await GetByIdAsync(post.Id) ?? post.Adapt<TaktPostDto>();
    }

    /// <summary>
    /// 更新岗位
    /// </summary>
    /// <param name="id">岗位ID</param>
    /// <param name="dto">更新岗位DTO</param>
    /// <returns>岗位DTO</returns>
    public async Task<TaktPostDto> UpdateAsync(long id, TaktPostUpdateDto dto)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            throw new TaktBusinessException("岗位不存在");

        // 查重验证（排除当前记录，PostName、PostCode、PostCategory、PostLevel 任意一个重复都报错）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_postRepository, p => p.PostName, dto.PostName, null, id, $"岗位名称 {dto.PostName} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_postRepository, p => p.PostCode, dto.PostCode, null, id, $"岗位编码 {dto.PostCode} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_postRepository, p => p.PostCategory, dto.PostCategory, null, id, $"岗位类别 {dto.PostCategory} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_postRepository, p => p.PostLevel, dto.PostLevel, null, id, $"岗位级别 {dto.PostLevel} 已存在");

        // 使用Mapster更新实体
        dto.Adapt(post, typeof(TaktPostUpdateDto), typeof(TaktPost));
        post.UpdateTime = DateTime.Now;

        await _postRepository.UpdateAsync(post);

        // 更新岗位用户关联
        if (dto.UserIds != null)
        {
            await AssignUserPostsAsync(id, dto.UserIds.ToArray());
        }

        return await GetByIdAsync(id) ?? post.Adapt<TaktPostDto>();
    }

    /// <summary>
    /// 删除岗位
    /// </summary>
    /// <param name="id">岗位ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            throw new TaktBusinessException("岗位不存在");

        // 1. 先将 PostStatus 置为禁用（1），再软删除（IsDeleted=1）
        post.PostStatus = 1;
        post.UpdateTime = DateTime.Now;
        await _postRepository.UpdateAsync(post);

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
    public async Task DeleteAsync(IEnumerable<long> ids)
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
            post.UpdateTime = DateTime.Now;
            await _postRepository.UpdateAsync(post);
        }

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
    /// 更新岗位状态
    /// </summary>
    /// <param name="dto">岗位状态DTO</param>
    /// <returns>岗位DTO</returns>
    public async Task<TaktPostDto> UpdateStatusAsync(TaktPostStatusDto dto)
    {
        var post = await _postRepository.GetByIdAsync(dto.PostId);
        if (post == null)
            throw new TaktBusinessException("岗位不存在");

        post.PostStatus = dto.PostStatus;
        post.UpdateTime = DateTime.Now;

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

        // 组装DTO
        var result = new List<TaktUserPostDto>();
        foreach (var postUser in postUsers)
        {
            if (userDict.TryGetValue(postUser.UserId, out var user))
            {
                result.Add(new TaktUserPostDto
                {
                    UserPostId = postUser.Id,
                    PostId = post.Id,
                    PostName = post.PostName,
                    PostCode = post.PostCode,
                    UserId = user.Id,
                    UserName = user.UserName,
                    RealName = user.RealName,
                    ConfigId = postUser.ConfigId,
                    CreateTime = postUser.CreateTime,
                    UpdateTime = postUser.UpdateTime,
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
            throw new TaktBusinessException("岗位不存在");

        // 验证用户是否存在
        if (userIds != null && userIds.Length > 0)
        {
            var users = await _userRepository.FindAsync(u => userIds.Contains(u.Id) && u.IsDeleted == 0);
            if (users.Count != userIds.Length)
                throw new TaktBusinessException("部分用户不存在");
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
                postUser.UpdateTime = DateTime.Now;
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
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPostTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "岗位导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "岗位导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入岗位
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
            var importData = await TaktExcelHelper.ImportAsync<TaktPostImportDto>(
                fileStream, 
                string.IsNullOrWhiteSpace(sheetName) ? "岗位导入模板" : sheetName
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
                    if (string.IsNullOrWhiteSpace(item.PostName))
                    {
                        errors.Add($"第{index}行：岗位名称不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.PostCode))
                    {
                        errors.Add($"第{index}行：岗位编码不能为空");
                        fail++;
                        continue;
                    }

                    // 导入时使用验证器手动验证（PostName、PostCode、PostCategory、PostLevel 任意一个重复都报错）
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_postRepository, p => p.PostName, item.PostName, null, null, $"第{index}行：岗位名称 {item.PostName} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_postRepository, p => p.PostCode, item.PostCode, null, null, $"第{index}行：岗位编码 {item.PostCode} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_postRepository, p => p.PostCategory, item.PostCategory, null, null, $"第{index}行：岗位类别 {item.PostCategory} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_postRepository, p => p.PostLevel, item.PostLevel, null, null, $"第{index}行：岗位级别 {item.PostLevel} 已存在");

                    // 创建岗位实体
                    var post = new TaktPost
                    {
                        PostName = item.PostName,
                        PostCode = item.PostCode,
                        DeptId = item.DeptId,
                        PostCategory = item.PostCategory,
                        PostLevel = item.PostLevel,
                        PostDuty = item.PostDuty,
                        OrderNum = item.OrderNum,
                        DataScope = item.DataScope,
                        PostStatus = item.PostStatus >= 0 ? item.PostStatus : 0, // 默认为启用（0=启用）
                        Remark = item.Remark
                    };

                    // 保存岗位
                    await _postRepository.CreateAsync(post);
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
    /// 导出岗位
    /// </summary>
    /// <param name="query">岗位查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktPostQueryDto query, string? sheetName, string? fileName)
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

        if (posts == null || posts.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPostExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "岗位数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "岗位导出" : fileName
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
            string.IsNullOrWhiteSpace(sheetName) ? "岗位数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "岗位导出" : fileName
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