// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPermissionSeedData.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：权限种子数据，根据菜单类型为1的菜单按「权限标识组」生成一组权限（list + 模块按钮权限）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 权限种子数据：先获取菜单类型为1的所有菜单，再为每个菜单按「权限标识组」添加一组权限（list + 通用/认证/工作流/日常等按钮权限）
/// </summary>
public static class TaktPermissionSeedData
{
    /// <summary>
    /// 初始化权限种子数据（基于 MenuType=1 的菜单，按权限标识组生成）
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public static async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();
        var permissionRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktPermission>>();

        int insertCount = 0;
        int updateCount = 0;

        var menus = await menuRepository.FindAsync(m => m.MenuType == 1 && m.IsDeleted == 0);
        if (menus == null || menus.Count == 0)
            return (insertCount, updateCount);

        foreach (var menu in menus)
        {
            var baseCode = DeriveBasePermissionCodeFromMenuCode(menu.MenuCode);
            var module = baseCode.Split(':').FirstOrDefault() ?? string.Empty;
            var group = GetButtonGroupForMenuCode(menu.MenuCode);

            // 先确保 list（列表/页面）权限存在
            var listCode = baseCode + ":list";
            var existingList = await permissionRepository.GetAsync(p => p.PermissionCode == listCode && p.IsDeleted == 0);
            if (existingList == null)
            {
                await permissionRepository.CreateAsync(new TaktPermission
                {
                    PermissionCode = listCode,
                    PermissionName = menu.MenuName ?? listCode,
                    Module = module,
                    MenuId = menu.Id,
                    OrderNum = menu.OrderNum,
                    PermissionStatus = 0,
                    ConfigId = configId,
                    IsDeleted = 0
                });
                insertCount++;
            }

            // 再按权限标识组添加按钮权限
            foreach (var (perm, name) in group)
            {
                if (perm == "list") continue; // 已处理
                var permissionCode = baseCode + ":" + perm;
                var existing = await permissionRepository.GetAsync(p => p.PermissionCode == permissionCode && p.IsDeleted == 0);
                if (existing == null)
                {
                    await permissionRepository.CreateAsync(new TaktPermission
                    {
                        PermissionCode = permissionCode,
                        PermissionName = (menu.MenuName ?? baseCode) + " " + name,
                        Module = module,
                        MenuId = menu.Id,
                        OrderNum = menu.OrderNum,
                        PermissionStatus = 0,
                        ConfigId = configId,
                        IsDeleted = 0
                    });
                    insertCount++;
                }
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 从菜单编码推导权限基础码（小写 + 下划线改冒号，不含 :list）
    /// </summary>
    private static string DeriveBasePermissionCodeFromMenuCode(string menuCode)
    {
        if (string.IsNullOrWhiteSpace(menuCode))
            return "menu:unknown";
        var parts = menuCode.Trim().ToLowerInvariant().Split('_', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length > 0 ? string.Join(":", parts) : "menu:unknown";
    }

    /// <summary>
    /// 根据菜单编码首段选择权限标识组（返回 perm, displayName）
    /// </summary>
    private static IEnumerable<(string Perm, string Name)> GetButtonGroupForMenuCode(string menuCode)
    {
        var prefix = menuCode.Split('_', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.ToUpperInvariant() ?? string.Empty;

        return prefix switch
        {
            "IDENTITY" => ButtonGroupIdentity(),
            "CODE" => ButtonGroupCodeGen(),
            "WORKFLOW" => ButtonGroupWorkflow(),
            "ROUTINE" or "DASHBOARD" or "STATISTICS" or "KANBAN" => ButtonGroupRoutine(),
            "HUMAN" or "HR" => ButtonGroupHrm(),   // HUMAN_RESOURCE 首段为 HUMAN
            "ACCOUNTING" or "LOGISTICS" => ButtonGroupAccounting(),
            _ => ButtonGroupCommon()
        };
    }

    /// <summary>
    /// 通用按钮权限标识组
    /// </summary>
    private static IEnumerable<(string Perm, string Name)> ButtonGroupCommon()
    {
        var names = new[] { "列表", "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销" };
        var perms = new[] { "list", "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke" };
        return names.Zip(perms, (n, p) => (p, n));
    }

    /// <summary>
    /// 认证通用按钮权限标识组
    /// </summary>
    private static IEnumerable<(string Perm, string Name)> ButtonGroupIdentity()
    {
        var names = new[] { "列表", "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "授权", "分配", "重置密码", "变更密码", "清空", "截断", "解锁", "禁用" };
        var perms = new[] { "list", "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "authorize", "allocate", "resetpwd", "changepwd", "empty", "truncate", "unlock", "disable" };
        return names.Zip(perms, (n, p) => (p, n));
    }

    /// <summary>
    /// 代码生成按钮权限标识组
    /// </summary>
    private static IEnumerable<(string Perm, string Name)> ButtonGroupCodeGen()
    {
        var names = new[] { "列表", "查询", "新增", "修改", "删除", "生成", "预览", "下载", "同步", "导入", "导出", "模板", "字段", "表", "数据库", "初始化", "克隆", "清空", "截断" };
        var perms = new[] { "list", "query", "create", "update", "delete", "generate", "preview", "download", "sync", "import", "export", "template", "columns", "tables", "databases", "initialize", "clone", "empty", "truncate" };
        return names.Zip(perms, (n, p) => (p, n));
    }

    /// <summary>
    /// 工作流按钮权限标识组
    /// </summary>
    private static IEnumerable<(string Perm, string Name)> ButtonGroupWorkflow()
    {
        var names = new[]
        {
            "列表", "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "复制", "克隆",
            "暂停", "恢复", "提交", "撤回", "转办", "委托", "退回", "催办", "加签", "减签", "进度", "历史",
            "发布", "停用", "启用", "版本", "设计", "配置", "验证",
            "启动", "终止",
            "字段管理", "权限设置", "数据源配置", "主题设置", "表单数据",
            "流转归档", "流转清理"
        };
        var perms = new[]
        {
            "list", "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "copy", "clone",
            "suspend", "resume", "submit", "withdraw", "transfer", "delegate", "return", "urge", "addsign", "subsign", "progress", "history",
            "publish", "disable", "enable", "version", "design", "config", "validate",
            "start", "terminate",
            "field", "permission", "datasource", "theme", "data",
            "archive", "clean"
        };
        return names.Zip(perms, (n, p) => (p, n));
    }

    /// <summary>
    /// 日常事务通用按钮权限标识组
    /// </summary>
    private static IEnumerable<(string Perm, string Name)> ButtonGroupRoutine()
    {
        var names = new[]
        {
            "列表", "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "克隆", "复制",
            "保存草稿", "删除草稿", "发送", "撤回", "转发", "回复", "已读", "未读", "传阅", "签收", "催办", "确认",
            "点赞", "取消点赞", "收藏", "取消收藏", "分享", "取消分享", "评论", "取消评论", "举报", "取消举报", "关注", "取消关注",
            "上传", "下载", "归档", "销毁", "版本",
            "运行", "停止", "重启", "刷新", "重置", "清空",
            "状态", "生成"
        };
        var perms = new[]
        {
            "list", "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "clone", "copy",
            "draft", "deletedraft", "send", "withdraw", "forward", "reply", "read", "unread", "circulate", "sign", "urge", "confirm",
            "like", "unlike", "favorite", "unfavorite", "share", "unshare", "comment", "uncomment", "flagging", "unflagging", "follow", "unfollow",
            "upload", "download", "archive", "destroy", "version",
            "run", "stop", "restart", "refresh", "reset", "empty",
            "status", "generate"
        };
        return names.Zip(perms, (n, p) => (p, n));
    }

    /// <summary>
    /// 人力资源通用按钮权限标识组
    /// </summary>
    private static IEnumerable<(string Perm, string Name)> ButtonGroupHrm()
    {
        var names = new[] { "列表", "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "核算" };
        var perms = new[] { "list", "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "calculate" };
        return names.Zip(perms, (n, p) => (p, n));
    }

    /// <summary>
    /// 会计通用按钮权限标识组
    /// </summary>
    private static IEnumerable<(string Perm, string Name)> ButtonGroupAccounting()
    {
        var names = new[]
        {
            "列表", "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "核算",
            "记账", "结账", "对账", "支付", "折旧", "报销", "冲销", "计提", "账期", "结转", "作废"
        };
        var perms = new[]
        {
            "list", "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "calculate",
            "book", "closing", "reconcile", "payment", "depreciation", "reimburse", "reversal", "accrual", "period", "carryforward", "cancel"
        };
        return names.Zip(perms, (n, p) => (p, n));
    }
}
