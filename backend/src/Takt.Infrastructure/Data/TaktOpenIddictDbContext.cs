// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data
// 文件名称：TaktOpenIddictDbContext.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：OpenIddict数据库上下文，用于存储OpenIddict相关数据
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;

namespace Takt.Infrastructure.Data;

/// <summary>
/// OpenIddict数据库上下文
/// </summary>
public class TaktOpenIddictDbContext : DbContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options">数据库上下文选项</param>
    public TaktOpenIddictDbContext(DbContextOptions<TaktOpenIddictDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// OpenIddict应用
    /// </summary>
    public DbSet<OpenIddictEntityFrameworkCoreApplication> Applications { get; set; } = null!;

    /// <summary>
    /// OpenIddict授权
    /// </summary>
    public DbSet<OpenIddictEntityFrameworkCoreAuthorization> Authorizations { get; set; } = null!;

    /// <summary>
    /// OpenIddict作用域
    /// </summary>
    public DbSet<OpenIddictEntityFrameworkCoreScope> Scopes { get; set; } = null!;

    /// <summary>
    /// OpenIddict令牌
    /// </summary>
    public DbSet<OpenIddictEntityFrameworkCoreToken> Tokens { get; set; } = null!;

    /// <summary>
    /// 配置模型
    /// </summary>
    /// <param name="builder">模型构建器</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // 配置OpenIddict实体
        builder.UseOpenIddict();
    }
}
