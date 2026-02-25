// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktUserSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户种子数据，初始化默认用户数据
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// Takt用户种子数据
/// </summary>
public class TaktUserSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（用户应该在租户之后初始化）
    /// </summary>
    public int Order => 2;

    /// <summary>
    /// 初始化用户种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var userRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktUser>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        int insertCount = 0;
        int updateCount = 0;

        // 从配置中获取默认密码（必须存在，不允许硬编码）
        var defaultPassword = configuration["PasswordPolicy:DefaultPassword"];
        if (string.IsNullOrWhiteSpace(defaultPassword))
        {
            throw new InvalidOperationException(
                "配置项 'PasswordPolicy:DefaultPassword' 未设置或为空。请在 appsettings.json 中配置默认密码。");
        }

        // 定义用户数据（使用配置的默认密码）
        var users = new[]
        {
            new { UserName = "admin", RealName = "系统管理员", FullName = "系统管理员", NickName = "管理员", EnglishName = "Administrator", UserEmail = "adc@teac.com.cn", UserPhone = "13800000000", Password = defaultPassword, UserStatus = 0, UserType = 2 },
            new { UserName = "guest", RealName = "访客", FullName = "访客", NickName = "访客", EnglishName = "Guest", UserEmail = "guest@takt.com", UserPhone = "13900000000", Password = defaultPassword, UserStatus = 0, UserType = 0 },
            new { UserName = "user", RealName = "普通用户", FullName = "普通用户", NickName = "用户", EnglishName = "User", UserEmail = "itsup@teac.com.cn", UserPhone = "13700000000", Password = defaultPassword, UserStatus = 0, UserType = 1 }
        };

        // 初始化每个用户
        foreach (var user in users)
        {
            var existing = await userRepository.GetAsync(u => u.UserName == user.UserName);

            var passwordHash = TaktEncryptHelper.HashPassword(user.Password);
            if (existing == null)
            {
                // 不存在则插入
                var newUser = new TaktUser
                {
                    UserName = user.UserName,
                    RealName = user.RealName,
                    FullName = user.FullName,
                    NickName = user.NickName,
                    EnglishName = user.EnglishName,
                    UserEmail = user.UserEmail,
                    UserPhone = user.UserPhone,
                    PasswordHash = passwordHash,
                    UserStatus = user.UserStatus,
                    UserType = user.UserType,
                    IsDeleted = 0
                };
                await userRepository.CreateAsync(newUser);
                insertCount++;
            }
            else
            {
                // 存在则更新
                existing.RealName = user.RealName;
                existing.FullName = user.FullName;
                existing.NickName = user.NickName;
                existing.EnglishName = user.EnglishName;
                existing.UserEmail = user.UserEmail;
                existing.UserPhone = user.UserPhone;
                existing.PasswordHash = passwordHash;
                existing.UserStatus = user.UserStatus;
                existing.UserType = user.UserType;
                await userRepository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
