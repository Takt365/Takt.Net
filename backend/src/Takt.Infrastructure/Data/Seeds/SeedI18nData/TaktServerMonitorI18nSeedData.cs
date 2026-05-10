// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktServerMonitorI18nSeedData.cs
// 创建时间：2026-05-06
// 创建人：Takt365
// 功能描述：服务监控本地化种子数据
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData;

/// <summary>
/// 服务监控本地化种子数据
/// </summary>
public class TaktServerMonitorI18nSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序
    /// </summary>
    public int Order => 999;

    /// <summary>
    /// 初始化服务监控本地化种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();

        int insertCount = 0;
        int updateCount = 0;
        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 1 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllServerMonitorTranslations();

            foreach (var translation in allTranslations)
            {
                if (!cultureCodeToLanguageId.TryGetValue(translation.CultureCode, out var languageId))
                    continue;

                var existing = await translationRepository.GetAsync(t =>
                    t.ResourceKey == translation.ResourceKey &&
                    t.CultureCode == translation.CultureCode &&
                    t.IsDeleted == 0);

                if (existing == null)
                {
                    await translationRepository.CreateAsync(new TaktTranslation
                    {
                        LanguageId = languageId,
                        CultureCode = translation.CultureCode,
                        ResourceKey = translation.ResourceKey,
                        TranslationValue = translation.TranslationValue,
                        ResourceType = translation.ResourceType,
                        ResourceGroup = translation.ResourceGroup,
                        SortOrder = translation.SortOrder,
                        IsDeleted = 0
                    });
                    insertCount++;
                }
                else if (existing.TranslationValue != translation.TranslationValue)
                {
                    existing.LanguageId = languageId;
                    existing.TranslationValue = translation.TranslationValue;
                    existing.ResourceType = translation.ResourceType;
                    existing.ResourceGroup = translation.ResourceGroup;
                    await translationRepository.UpdateAsync(existing);
                    updateCount++;
                }
                else if (existing.LanguageId != languageId)
                {
                    existing.LanguageId = languageId;
                    await translationRepository.UpdateAsync(existing);
                    updateCount++;
                }
            }
        }
        finally
        {
            TaktTenantContext.CurrentConfigId = originalConfigId;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取所有服务监控翻译
    /// </summary>
    private static List<TaktTranslation> GetAllServerMonitorTranslations()
    {
        return new List<TaktTranslation>
        {
            // ========== 服务监控页面标题 ==========
            // page.servermonitor.title
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "page.servermonitor.title", TranslationValue = "Server Monitor", ResourceType = "Frontend", ResourceGroup = "Page", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "page.servermonitor.title", TranslationValue = "サーバーモニター", ResourceType = "Frontend", ResourceGroup = "Page", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "page.servermonitor.title", TranslationValue = "서버 모니터링", ResourceType = "Frontend", ResourceGroup = "Page", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "page.servermonitor.title", TranslationValue = "服务监控", ResourceType = "Frontend", ResourceGroup = "Page", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "page.servermonitor.title", TranslationValue = "服務監控", ResourceType = "Frontend", ResourceGroup = "Page", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "page.servermonitor.title", TranslationValue = "服務監控", ResourceType = "Frontend", ResourceGroup = "Page", SortOrder = 0 },

            // ========== 服务器硬件信息 ==========
            // servermonitor.hardware
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.hardware.title", TranslationValue = "Hardware Info", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.hardware.title", TranslationValue = "ハードウェア情報", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.hardware.title", TranslationValue = "하드웨어 정보", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.hardware.title", TranslationValue = "硬件信息", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.hardware.title", TranslationValue = "硬體資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.hardware.title", TranslationValue = "硬體資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.hardware.os
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.hardware.os", TranslationValue = "Operating System", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.hardware.os", TranslationValue = "オペレーティングシステム", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.hardware.os", TranslationValue = "운영 체제", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.hardware.os", TranslationValue = "操作系统", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.hardware.os", TranslationValue = "作業系統", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.hardware.os", TranslationValue = "作業系統", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.hardware.cpu
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.hardware.cpu", TranslationValue = "CPU", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.hardware.cpu", TranslationValue = "CPU", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.hardware.cpu", TranslationValue = "CPU", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.hardware.cpu", TranslationValue = "处理器", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.hardware.cpu", TranslationValue = "處理器", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.hardware.cpu", TranslationValue = "處理器", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.hardware.memory
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.hardware.memory", TranslationValue = "Memory", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.hardware.memory", TranslationValue = "メモリ", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.hardware.memory", TranslationValue = "메모리", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.hardware.memory", TranslationValue = "内存", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.hardware.memory", TranslationValue = "記憶體", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.hardware.memory", TranslationValue = "記憶體", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.hardware.disk
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.hardware.disk", TranslationValue = "Disk", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.hardware.disk", TranslationValue = "ディスク", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.hardware.disk", TranslationValue = "디스크", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.hardware.disk", TranslationValue = "磁盘", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.hardware.disk", TranslationValue = "磁碟", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.hardware.disk", TranslationValue = "磁碟", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.hardware.network
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.hardware.network", TranslationValue = "Network", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.hardware.network", TranslationValue = "ネットワーク", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.hardware.network", TranslationValue = "네트워크", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.hardware.network", TranslationValue = "网络", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.hardware.network", TranslationValue = "網路", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.hardware.network", TranslationValue = "網路", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // ========== 应用运行状态 ==========
            // servermonitor.appstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.appstatus.title", TranslationValue = "Application Status", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.appstatus.title", TranslationValue = "アプリケーション状態", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.appstatus.title", TranslationValue = "애플리케이션 상태", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.appstatus.title", TranslationValue = "应用状态", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.appstatus.title", TranslationValue = "應用狀態", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.appstatus.title", TranslationValue = "應用狀態", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.appstatus.version
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.appstatus.version", TranslationValue = "Version", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.appstatus.version", TranslationValue = "バージョン", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.appstatus.version", TranslationValue = "버전", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.appstatus.version", TranslationValue = "版本", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.appstatus.version", TranslationValue = "版本", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.appstatus.version", TranslationValue = "版本", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.appstatus.uptime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.appstatus.uptime", TranslationValue = "Uptime", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.appstatus.uptime", TranslationValue = "稼働時間", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.appstatus.uptime", TranslationValue = "가동 시간", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.appstatus.uptime", TranslationValue = "运行时长", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.appstatus.uptime", TranslationValue = "運行時長", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.appstatus.uptime", TranslationValue = "運行時長", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.appstatus.environment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.appstatus.environment", TranslationValue = "Environment", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.appstatus.environment", TranslationValue = "環境", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.appstatus.environment", TranslationValue = "환경", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.appstatus.environment", TranslationValue = "运行环境", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.appstatus.environment", TranslationValue = "運行環境", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.appstatus.environment", TranslationValue = "運行環境", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // ========== 操作按钮 ==========
            // servermonitor.action.refresh
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.action.refresh", TranslationValue = "Refresh", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.action.refresh", TranslationValue = "更新", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.action.refresh", TranslationValue = "새로 고침", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.action.refresh", TranslationValue = "刷新", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.action.refresh", TranslationValue = "重新整理", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.action.refresh", TranslationValue = "重新整理", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.action.refreshCache
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.action.refreshCache", TranslationValue = "Refresh Cache", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.action.refreshCache", TranslationValue = "キャッシュを更新", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.action.refreshCache", TranslationValue = "캐시 새로 고침", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.action.refreshCache", TranslationValue = "刷新缓存", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.action.refreshCache", TranslationValue = "重新整理快取", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.action.refreshCache", TranslationValue = "重新整理快取", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // ========== 提示信息 ==========
            // servermonitor.message.refreshSuccess
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.message.refreshsuccess", TranslationValue = "Cache refreshed successfully", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.message.refreshsuccess", TranslationValue = "キャッシュの更新に成功しました", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.message.refreshsuccess", TranslationValue = "캐시가 성공적으로 새로 고쳐졌습니다", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.message.refreshsuccess", TranslationValue = "缓存刷新成功", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.message.refreshsuccess", TranslationValue = "快取重新整理成功", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.message.refreshsuccess", TranslationValue = "快取重新整理成功", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // ========== CPU详细字段 ==========
            // servermonitor.cpu.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.cpu.name", TranslationValue = "CPU Name", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.cpu.name", TranslationValue = "CPU名", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.cpu.name", TranslationValue = "CPU 이름", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.cpu.name", TranslationValue = "CPU名称", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.cpu.name", TranslationValue = "CPU名稱", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.cpu.name", TranslationValue = "CPU名稱", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.cpu.manufacturer
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.cpu.manufacturer", TranslationValue = "Manufacturer", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.cpu.manufacturer", TranslationValue = "製造元", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.cpu.manufacturer", TranslationValue = "제조사", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.cpu.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.cpu.manufacturer", TranslationValue = "製造商", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.cpu.manufacturer", TranslationValue = "製造商", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.cpu.cores
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.cpu.cores", TranslationValue = "Cores", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.cpu.cores", TranslationValue = "コア数", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.cpu.cores", TranslationValue = "코어 수", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.cpu.cores", TranslationValue = "核心数", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.cpu.cores", TranslationValue = "核心數", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.cpu.cores", TranslationValue = "核心數", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.cpu.logicalprocessors
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.cpu.logicalprocessors", TranslationValue = "Logical Processors", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.cpu.logicalprocessors", TranslationValue = "論理プロセッサ数", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.cpu.logicalprocessors", TranslationValue = "논리 프로세서 수", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.cpu.logicalprocessors", TranslationValue = "逻辑处理器数", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.cpu.logicalprocessors", TranslationValue = "邏輯處理器數", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.cpu.logicalprocessors", TranslationValue = "邏輯處理器數", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.cpu.processorid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.cpu.processorid", TranslationValue = "Processor ID", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.cpu.processorid", TranslationValue = "プロセッサID", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.cpu.processorid", TranslationValue = "프로세서 ID", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.cpu.processorid", TranslationValue = "处理器ID", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.cpu.processorid", TranslationValue = "處理器ID", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.cpu.processorid", TranslationValue = "處理器ID", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // ========== 内存详细字段 ==========
            // servermonitor.memory.totalphysical
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.memory.totalphysical", TranslationValue = "Total Physical Memory", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.memory.totalphysical", TranslationValue = "総物理メモリ", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.memory.totalphysical", TranslationValue = "총 물리적 메모리", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.memory.totalphysical", TranslationValue = "总物理内存", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.memory.totalphysical", TranslationValue = "總實體記憶體", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.memory.totalphysical", TranslationValue = "總實體記憶體", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.memory.availablephysical
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.memory.availablephysical", TranslationValue = "Available Physical Memory", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.memory.availablephysical", TranslationValue = "利用可能物理メモリ", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.memory.availablephysical", TranslationValue = "사용 가능한 물리적 메모리", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.memory.availablephysical", TranslationValue = "可用物理内存", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.memory.availablephysical", TranslationValue = "可用實體記憶體", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.memory.availablephysical", TranslationValue = "可用實體記憶體", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.memory.usedphysical
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.memory.usedphysical", TranslationValue = "Used Physical Memory", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.memory.usedphysical", TranslationValue = "使用中物理メモリ", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.memory.usedphysical", TranslationValue = "사용 중인 물리적 메모리", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.memory.usedphysical", TranslationValue = "已用物理内存", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.memory.usedphysical", TranslationValue = "已用實體記憶體", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.memory.usedphysical", TranslationValue = "已用實體記憶體", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.memory.totalvirtual
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.memory.totalvirtual", TranslationValue = "Total Virtual Memory", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.memory.totalvirtual", TranslationValue = "総仮想メモリ", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.memory.totalvirtual", TranslationValue = "총 가상 메모리", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.memory.totalvirtual", TranslationValue = "总虚拟内存", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.memory.totalvirtual", TranslationValue = "總虛擬記憶體", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.memory.totalvirtual", TranslationValue = "總虛擬記憶體", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.memory.usagepercent
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.memory.usagepercent", TranslationValue = "Memory Usage %", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.memory.usagepercent", TranslationValue = "メモリ使用率", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.memory.usagepercent", TranslationValue = "메모리 사용률", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.memory.usagepercent", TranslationValue = "内存使用率", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.memory.usagepercent", TranslationValue = "記憶體使用率", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.memory.usagepercent", TranslationValue = "記憶體使用率", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // ========== 磁盘详细字段 ==========
            // servermonitor.drive.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.drive.name", TranslationValue = "Drive", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.drive.name", TranslationValue = "ドライブ", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.drive.name", TranslationValue = "드라이브", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.drive.name", TranslationValue = "驱动器", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.drive.name", TranslationValue = "磁碟機", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.drive.name", TranslationValue = "磁碟機", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.drive.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.drive.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.drive.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.drive.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.drive.type", TranslationValue = "类型", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.drive.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.drive.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.drive.filesystem
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.drive.filesystem", TranslationValue = "File System", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.drive.filesystem", TranslationValue = "ファイルシステム", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.drive.filesystem", TranslationValue = "파일 시스템", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.drive.filesystem", TranslationValue = "文件系统", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.drive.filesystem", TranslationValue = "檔案系統", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.drive.filesystem", TranslationValue = "檔案系統", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.drive.totalsize
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.drive.totalsize", TranslationValue = "Total Size", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.drive.totalsize", TranslationValue = "総容量", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.drive.totalsize", TranslationValue = "총 용량", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.drive.totalsize", TranslationValue = "总容量", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.drive.totalsize", TranslationValue = "總容量", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.drive.totalsize", TranslationValue = "總容量", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.drive.freespace
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.drive.freespace", TranslationValue = "Free Space", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.drive.freespace", TranslationValue = "空き容量", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.drive.freespace", TranslationValue = "사용 가능한 공간", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.drive.freespace", TranslationValue = "可用空间", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.drive.freespace", TranslationValue = "可用空間", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.drive.freespace", TranslationValue = "可用空間", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.drive.usedspace
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.drive.usedspace", TranslationValue = "Used Space", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.drive.usedspace", TranslationValue = "使用中容量", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.drive.usedspace", TranslationValue = "사용 중인 공간", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.drive.usedspace", TranslationValue = "已用空间", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.drive.usedspace", TranslationValue = "已用空間", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.drive.usedspace", TranslationValue = "已用空間", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.drive.usagepercent
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.drive.usagepercent", TranslationValue = "Disk Usage %", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.drive.usagepercent", TranslationValue = "ディスク使用率", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.drive.usagepercent", TranslationValue = "디스크 사용률", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.drive.usagepercent", TranslationValue = "磁盘使用率", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.drive.usagepercent", TranslationValue = "磁碟使用率", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.drive.usagepercent", TranslationValue = "磁碟使用率", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // ========== 网络适配器详细字段 ==========
            // servermonitor.network.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.network.name", TranslationValue = "Adapter Name", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.network.name", TranslationValue = "アダプタ名", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.network.name", TranslationValue = "어댑터 이름", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.network.name", TranslationValue = "适配器名称", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.network.name", TranslationValue = "配接器名稱", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.network.name", TranslationValue = "配接器名稱", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.network.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.network.description", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.network.description", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.network.description", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.network.description", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.network.description", TranslationValue = "說明", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.network.description", TranslationValue = "說明", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.network.macaddress
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.network.macaddress", TranslationValue = "MAC Address", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.network.macaddress", TranslationValue = "MACアドレス", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.network.macaddress", TranslationValue = "MAC 주소", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.network.macaddress", TranslationValue = "MAC地址", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.network.macaddress", TranslationValue = "MAC位址", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.network.macaddress", TranslationValue = "MAC位址", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.network.speed
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.network.speed", TranslationValue = "Speed", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.network.speed", TranslationValue = "速度", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.network.speed", TranslationValue = "속도", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.network.speed", TranslationValue = "速度", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.network.speed", TranslationValue = "速度", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.network.speed", TranslationValue = "速度", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.network.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.network.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.network.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.network.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.network.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.network.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.network.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // ========== 应用状态详细字段 ==========
            // servermonitor.appstatus.appname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.appstatus.appname", TranslationValue = "Application Name", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.appstatus.appname", TranslationValue = "アプリケーション名", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.appstatus.appname", TranslationValue = "애플리케이션 이름", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.appstatus.appname", TranslationValue = "应用名称", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.appstatus.appname", TranslationValue = "應用名稱", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.appstatus.appname", TranslationValue = "應用名稱", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.appstatus.appversion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.appstatus.appversion", TranslationValue = "Application Version", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.appstatus.appversion", TranslationValue = "アプリケーションバージョン", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.appstatus.appversion", TranslationValue = "애플리케이션 버전", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.appstatus.appversion", TranslationValue = "应用版本", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.appstatus.appversion", TranslationValue = "應用版本", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.appstatus.appversion", TranslationValue = "應用版本", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.appstatus.machinename
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.appstatus.machinename", TranslationValue = "Machine Name", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.appstatus.machinename", TranslationValue = "マシン名", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.appstatus.machinename", TranslationValue = "컴퓨터 이름", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.appstatus.machinename", TranslationValue = "机器名称", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.appstatus.machinename", TranslationValue = "機器名稱", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.appstatus.machinename", TranslationValue = "機器名稱", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.appstatus.starttime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.appstatus.starttime", TranslationValue = "Start Time", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.appstatus.starttime", TranslationValue = "起動時間", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.appstatus.starttime", TranslationValue = "시작 시간", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.appstatus.starttime", TranslationValue = "启动时间", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.appstatus.starttime", TranslationValue = "啟動時間", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.appstatus.starttime", TranslationValue = "啟動時間", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.appstatus.dotnetversion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.appstatus.dotnetversion", TranslationValue = ".NET Version", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.appstatus.dotnetversion", TranslationValue = ".NETバージョン", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.appstatus.dotnetversion", TranslationValue = ".NET 버전", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.appstatus.dotnetversion", TranslationValue = ".NET版本", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.appstatus.dotnetversion", TranslationValue = ".NET版本", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.appstatus.dotnetversion", TranslationValue = ".NET版本", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.appstatus.workingset
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.appstatus.workingset", TranslationValue = "Working Set Memory", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.appstatus.workingset", TranslationValue = "ワーキングセットメモリ", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.appstatus.workingset", TranslationValue = "작업 집합 메모리", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.appstatus.workingset", TranslationValue = "工作集内存", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.appstatus.workingset", TranslationValue = "工作集記憶體", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.appstatus.workingset", TranslationValue = "工作集記憶體", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.appstatus.processorcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.appstatus.processorcount", TranslationValue = "Processor Count", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.appstatus.processorcount", TranslationValue = "プロセッサ数", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.appstatus.processorcount", TranslationValue = "프로세서 수", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.appstatus.processorcount", TranslationValue = "处理器数量", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.appstatus.processorcount", TranslationValue = "處理器數量", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.appstatus.processorcount", TranslationValue = "處理器數量", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // ========== 操作系统语言字段 ==========
            // servermonitor.os.currentculture
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.os.currentculture", TranslationValue = "Current Culture", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.os.currentculture", TranslationValue = "現在のカルチャ", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.os.currentculture", TranslationValue = "현재 문화", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.os.currentculture", TranslationValue = "当前文化", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.os.currentculture", TranslationValue = "目前文化", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.os.currentculture", TranslationValue = "目前文化", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.os.osversion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.os.osversion", TranslationValue = "OS Version", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.os.osversion", TranslationValue = "OSバージョン", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.os.osversion", TranslationValue = "OS 버전", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.os.osversion", TranslationValue = "操作系统版本", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.os.osversion", TranslationValue = "作業系統版本", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.os.osversion", TranslationValue = "作業系統版本", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // ========== 对象名称（用于前端表格/卡片标题） ==========
            // servermonitor.object.cpuinfo
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.object.cpuinfo", TranslationValue = "CPU Information", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.object.cpuinfo", TranslationValue = "CPU情報", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.object.cpuinfo", TranslationValue = "CPU 정보", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.object.cpuinfo", TranslationValue = "CPU信息", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.object.cpuinfo", TranslationValue = "CPU資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.object.cpuinfo", TranslationValue = "CPU資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.object.memoryinfo
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.object.memoryinfo", TranslationValue = "Memory Information", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.object.memoryinfo", TranslationValue = "メモリ情報", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.object.memoryinfo", TranslationValue = "메모리 정보", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.object.memoryinfo", TranslationValue = "内存信息", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.object.memoryinfo", TranslationValue = "記憶體資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.object.memoryinfo", TranslationValue = "記憶體資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.object.driveinfo
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.object.driveinfo", TranslationValue = "Disk Information", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.object.driveinfo", TranslationValue = "ディスク情報", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.object.driveinfo", TranslationValue = "디스크 정보", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.object.driveinfo", TranslationValue = "磁盘信息", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.object.driveinfo", TranslationValue = "磁碟資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.object.driveinfo", TranslationValue = "磁碟資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.object.networkadapterinfo
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.object.networkadapterinfo", TranslationValue = "Network Adapter Information", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.object.networkadapterinfo", TranslationValue = "ネットワークアダプタ情報", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.object.networkadapterinfo", TranslationValue = "네트워크 어댑터 정보", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.object.networkadapterinfo", TranslationValue = "网络适配器信息", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.object.networkadapterinfo", TranslationValue = "網路配接器資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.object.networkadapterinfo", TranslationValue = "網路配接器資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.object.oslanguageinfo
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.object.oslanguageinfo", TranslationValue = "OS Language Information", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.object.oslanguageinfo", TranslationValue = "OS言語情報", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.object.oslanguageinfo", TranslationValue = "OS 언어 정보", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.object.oslanguageinfo", TranslationValue = "操作系统语言信息", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.object.oslanguageinfo", TranslationValue = "作業系統語言資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.object.oslanguageinfo", TranslationValue = "作業系統語言資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.object.installedlanguage
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.object.installedlanguage", TranslationValue = "Installed Language", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.object.installedlanguage", TranslationValue = "インストール済み言語", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.object.installedlanguage", TranslationValue = "설치된 언어", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.object.installedlanguage", TranslationValue = "已安装语言", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.object.installedlanguage", TranslationValue = "已安裝語言", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.object.installedlanguage", TranslationValue = "已安裝語言", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.object.serverhardwareinfo
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.object.serverhardwareinfo", TranslationValue = "Server Hardware Information", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.object.serverhardwareinfo", TranslationValue = "サーバーハードウェア情報", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.object.serverhardwareinfo", TranslationValue = "서버 하드웨어 정보", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.object.serverhardwareinfo", TranslationValue = "服务器硬件信息", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.object.serverhardwareinfo", TranslationValue = "伺服器硬體資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.object.serverhardwareinfo", TranslationValue = "伺服器硬體資訊", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },

            // servermonitor.object.appstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "servermonitor.object.appstatus", TranslationValue = "Application Status", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "servermonitor.object.appstatus", TranslationValue = "アプリケーション状態", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "servermonitor.object.appstatus", TranslationValue = "애플리케이션 상태", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "servermonitor.object.appstatus", TranslationValue = "应用运行状态", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "servermonitor.object.appstatus", TranslationValue = "應用執行狀態", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "servermonitor.object.appstatus", TranslationValue = "應用執行狀態", ResourceType = "Frontend", ResourceGroup = "ServerMonitor", SortOrder = 0 },
        };
    }
}
