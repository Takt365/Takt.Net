// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktWorkflowSchemeSeedData.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程方案种子数据，初始化公告发布、文控文档审批等流程方案（ConfigId=5）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Workflow;
using Takt.Domain.Repositories;
using Takt.Shared.Constants;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 工作流流程方案种子数据（类名含 Workflow，映射 ConfigId=5）
/// </summary>
public class TaktWorkflowSchemeSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序
    /// </summary>
    public int Order => 51;

    /// <summary>
    /// 初始化流程方案种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID（应为 5）</param>
    /// <returns>插入数、更新数</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        if (configId != "5")
            return (0, 0);

        var repository = serviceProvider.GetRequiredService<ITaktRepository<TaktFlowScheme>>();
        int insertCount = 0;
        int updateCount = 0;

        var schemes = new[]
        {
            new
            {
                ProcessKey = TaktWorkflowProcessKeys.ProcessKeyAnnouncement,
                ProcessName = "公告发布审批",
                ProcessCategory = 1,
                ProcessStatus = 1,
                ProcessDescription = "公告提交后经审批通过方可发布",
                IsRevocable = 1
            },
            new
            {
                ProcessKey = TaktWorkflowProcessKeys.ProcessKeyDocument,
                ProcessName = "文控文档审批",
                ProcessCategory = 1,
                ProcessStatus = 1,
                ProcessDescription = "文控文档发布/变更/废止需经审批",
                IsRevocable = 1
            }
        };

        foreach (var s in schemes)
        {
            var existing = await repository.GetAsync(x => x.ProcessKey == s.ProcessKey && x.IsDeleted == 0).ConfigureAwait(false);
            if (existing != null)
                continue;

            await repository.CreateAsync(new TaktFlowScheme
            {
                ProcessKey = s.ProcessKey,
                ProcessName = s.ProcessName,
                ProcessCategory = s.ProcessCategory,
                ProcessVersion = 1,
                ProcessDescription = s.ProcessDescription,
                ProcessStatus = s.ProcessStatus,
                IsRevocable = s.IsRevocable,
                IsSuspendable = 1,
                IsTransferable = 0,
                IsReturnable = 1,
                OrderNum = 0
            }).ConfigureAwait(false);
            insertCount++;
        }

        return (insertCount, updateCount);
    }
}
