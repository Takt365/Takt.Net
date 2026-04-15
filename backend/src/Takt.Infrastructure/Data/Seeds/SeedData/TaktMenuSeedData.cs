// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMenuSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt菜单种子数据协调器，统一协调处理所有层级的菜单初始化
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt菜单种子数据协调器
/// </summary>
public class TaktMenuSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（菜单在用户之后初始化）
    /// </summary>
    public int Order => 3;

    /// <summary>
    /// 初始化菜单种子数据（统一协调处理所有层级）
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        int totalInsertCount = 0;
        int totalUpdateCount = 0;

        // ========== 第一级：顶级菜单（ParentId = 0） ==========
        var (insert1, update1) = await TaktMenuLevel1SeedData.SeedAsync(serviceProvider, configId);
        totalInsertCount += insert1;
        totalUpdateCount += update1;

        // ========== 第二级：二级菜单（依赖顶级菜单） ==========
        var (insert2, update2) = await TaktMenuLevel2SeedData.SeedAsync(serviceProvider, configId);
        totalInsertCount += insert2;
        totalUpdateCount += update2;

        // ========== 第三级：三级菜单（依赖二级菜单） ==========
        var (insert3, update3) = await TaktMenuLevel3SeedData.SeedAsync(serviceProvider, configId);
        totalInsertCount += insert3;
        totalUpdateCount += update3;

        // ========== 第四级：四级菜单（依赖三级菜单） ==========
        var (insert4, update4) = await TaktMenuLevel4SeedData.SeedAsync(serviceProvider, configId);
        totalInsertCount += insert4;
        totalUpdateCount += update4;

        // ========== 第五级：五级菜单（依赖四级菜单） ==========
        var (insert5, update5) = await TaktMenuLevel5SeedData.SeedAsync(serviceProvider, configId);
        totalInsertCount += insert5;
        totalUpdateCount += update5;

        // ========== 按钮：按钮菜单（MenuType = 2） ==========
        var (insert6, update6) = await TaktMenuButtonSeedData.SeedAsync(serviceProvider, configId);
        totalInsertCount += insert6;
        totalUpdateCount += update6;

        return (totalInsertCount, totalUpdateCount);
    }
}
