// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Workflow
// 文件名称：TaktFlowTimeoutHostedService.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流超时自动推进后台服务；按方案 TimeoutConfig 扫描运行中实例并自动通过/驳回
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Takt.Application.Services.Workflow;
using Takt.Domain.Entities.Workflow;
using Takt.Domain.Repositories;
using Takt.Shared.Enums;

namespace Takt.Infrastructure.Workflow;

/// <summary>
/// 超时配置 JSON 结构（与 TaktFlowScheme.TimeoutConfig 对应）
/// </summary>
internal class TimeoutConfigDto
{
    /// <summary>超时小时数（与 Minutes 二选一）</summary>
    public int? Hours { get; set; }
    /// <summary>超时分钟数（与 Hours 二选一）</summary>
    public int? Minutes { get; set; }
    /// <summary>超时后 true=自动通过，false=自动驳回</summary>
    public bool Pass { get; set; } = true;
}

/// <summary>
/// 工作流超时自动推进后台服务
/// </summary>
public class TaktFlowTimeoutHostedService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<TaktFlowTimeoutHostedService> _logger;
    private static readonly TimeSpan Interval = TimeSpan.FromMinutes(5);

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowTimeoutHostedService(
        IServiceScopeFactory scopeFactory,
        ILogger<TaktFlowTimeoutHostedService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await RunTimeoutScanAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[FlowTimeout] 超时扫描异常: {Message}", ex.Message);
            }

            await Task.Delay(Interval, stoppingToken).ConfigureAwait(false);
        }
    }

    private async Task RunTimeoutScanAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var schemeRepository = scope.ServiceProvider.GetRequiredService<ITaktRepository<TaktFlowScheme>>();
        var instanceRepository = scope.ServiceProvider.GetRequiredService<ITaktRepository<TaktFlowInstance>>();
        var instanceService = scope.ServiceProvider.GetRequiredService<ITaktFlowInstanceService>();

        var schemes = await schemeRepository.FindAsync(s =>
            s.IsDeleted == 0 &&
            !string.IsNullOrWhiteSpace(s.TimeoutConfig)
        ).ConfigureAwait(false);

        foreach (var scheme in schemes)
        {
            TimeoutConfigDto? config;
            try
            {
                config = JsonSerializer.Deserialize<TimeoutConfigDto>(scheme.TimeoutConfig!);
            }
            catch
            {
                continue;
            }

            if (config == null || (!config.Hours.HasValue && !config.Minutes.HasValue))
                continue;

            var timeoutSpan = config.Minutes.HasValue
                ? TimeSpan.FromMinutes(config.Minutes.Value)
                : TimeSpan.FromHours(config.Hours!.Value);
            var cutoff = DateTime.Now - timeoutSpan;

            var instances = await instanceRepository.FindAsync(i =>
                i.SchemeId == scheme.Id &&
                i.IsDeleted == 0 &&
                i.InstanceStatus == (int)TaktFlowInstanceStatus.Running &&
                (i.UpdateTime == null || i.UpdateTime < cutoff)
            ).ConfigureAwait(false);

            foreach (var inst in instances)
            {
                try
                {
                    await instanceService.AdvanceByTimeoutAsync(inst.Id, config.Pass).ConfigureAwait(false);
                    _logger.LogInformation("[FlowTimeout] 实例 {InstanceId} 超时自动{Action}", inst.Id, config.Pass ? "通过" : "驳回");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "[FlowTimeout] 实例 {InstanceId} 超时推进失败: {Message}", inst.Id, ex.Message);
                }
            }
        }
    }
}
