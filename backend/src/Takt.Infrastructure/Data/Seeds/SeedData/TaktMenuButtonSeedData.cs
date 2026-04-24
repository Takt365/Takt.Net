// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMenuButtonSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 按钮菜单种子数据。
//           遍历所有页面菜单（MenuType=1），按权限前缀匹配按钮模板，
//           为每个菜单生成一组按钮子项（MenuType=2），并写入权限标识与排序。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt 按钮菜单种子数据。
/// <para>
/// 在各级页面菜单（Level1～Level5）种子执行完毕后，由 <see cref="TaktMenuSeedData"/> 调用。
/// 仅处理 <c>MenuType == 1</c> 且未删除的菜单；要求 <c>Permission</c> 非空且以 <c>:list</c> 结尾。
/// </para>
/// </summary>
public class TaktMenuButtonSeedData
{
    /// <summary>
    /// 初始化按钮菜单种子数据。
    /// <para>
    /// 对每个符合条件的页面菜单，根据其权限字符串首段（模块前缀）选择预置按钮组（通用、身份、代码生成、工作流、
    /// 日常事务、人力资源、会计等），生成或更新子按钮记录。
    /// </para>
    /// </summary>
    /// <param name="serviceProvider">服务提供者，用于解析 <see cref="ITaktRepository{TaktMenu}"/>。</param>
    /// <param name="configId">当前租户/数据库配置 ID（种子接口统一传入，本类当前未单独分支使用）。</param>
    /// <returns>元组：(InsertCount, UpdateCount)，分别为本次新增与更新的菜单（按钮）条数。</returns>
    /// <exception cref="InvalidOperationException">当某页面菜单的 Permission 为空或不以 :list 结尾时抛出。</exception>
    public static async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();

        int insertCount = 0;
        int updateCount = 0;

        var menus = await menuRepository.FindAsync(m => m.MenuType == 1);

        foreach (var menu in menus)
        {
            if (string.IsNullOrEmpty(menu.Permission))
            {
                throw new InvalidOperationException(
                    $"菜单 {menu.MenuCode} ({menu.MenuName}) 的 MenuType=1，但 Permission 为空。必须设置 Permission，且必须以 \":list\" 结尾。");
            }

            if (!menu.Permission.EndsWith(":list", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"菜单 {menu.MenuCode} ({menu.MenuName}) 的 Permission={menu.Permission} 必须以 \":list\" 结尾。");
            }

            // 从 Permission 中解析模块前缀（第一段），用于选择预置按钮组；若以 takt: 开头则走默认组
            var modulePrefix = GetModulePrefix(menu.Permission);
            // 为该菜单创建按钮子项（所有 MenuType=1 的菜单都需要按钮）
            var (insert, update) = await CreateButtonsForMenuAsync(menuRepository, menu, modulePrefix);
            insertCount += insert;
            updateCount += update;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 为单个页面菜单创建或更新其下全部按钮子项。
    /// </summary>
    /// <param name="menuRepository">菜单仓储。</param>
    /// <param name="menu">父级页面菜单实体（MenuType=1）。</param>
    /// <param name="modulePrefix">
    /// 从 <see cref="TaktMenu.Permission"/> 解析出的模块前缀（冒号分隔第一段的小写形式）。
    /// 若以 <c>takt</c> 开头则视为空前缀，走默认通用按钮组。
    /// </param>
    /// <returns>元组：(InsertCount, UpdateCount)，本菜单下按钮新增与更新条数。</returns>
    private static async Task<(int InsertCount, int UpdateCount)> CreateButtonsForMenuAsync(
        ITaktRepository<TaktMenu> menuRepository,
        TaktMenu menu,
        string modulePrefix)
    {
        int insertCount = 0;
        int updateCount = 0;

        // 获取按钮配置（按模块前缀匹配：通用 / 身份 / 代码生成 / 工作流 / 日常 / 人力 / 会计等）
        var (buttonNames, buttonPerms) = GetButtonConfig(modulePrefix);
        if (buttonNames == null || buttonPerms == null)
            return (0, 0);

        // 从菜单的权限标识中解析“菜单段”，用于拼接按钮权限；若无法解析出有效段，则使用 MenuCode 的小写形式
        var menuPerm = GetMenuPerm(menu.Permission);
        if (string.IsNullOrEmpty(menuPerm) && !string.IsNullOrEmpty(menu.MenuCode))
            menuPerm = menu.MenuCode.ToLowerInvariant();

        // 生成各按钮
        for (int i = 0; i < buttonNames.Length; i++)
        {
            var buttonName = buttonNames[i];
            var buttonPerm = buttonPerms[i];

            // 生成按钮编码：优先使用 MenuCode_buttonPerm；超过列长度（50）时自动收缩并附加稳定哈希后缀
            var buttonCode = BuildButtonCode(menu.MenuCode, buttonPerm);

            // 生成权限字符串：顶级目录 + … + 业务实体 + 操作
            // 格式一般为 modulePrefix:menuPerm:buttonPerm；若 modulePrefix 为空则为 menuPerm:buttonPerm；若 menuPerm 也为空则为 buttonPerm
            var permission = string.IsNullOrEmpty(modulePrefix)
                ? (string.IsNullOrEmpty(menuPerm)
                    ? buttonPerm.ToLowerInvariant()
                    : $"{menuPerm}:{buttonPerm.ToLowerInvariant()}")
                : $"{modulePrefix.ToLower()}:{menuPerm}:{buttonPerm.ToLowerInvariant()}";

            // 生成本地化键：common.button.{操作后缀}
            var menuL10nKey = $"common.button.{buttonPerm.ToLowerInvariant()}";

            var (insert, update) = await CreateOrUpdateButtonAsync(
                menuRepository, menu.Id, buttonCode, buttonName, permission, menuL10nKey, i + 1);
            insertCount += insert;
            updateCount += update;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 从完整权限字符串中提取模块前缀（第一段），用于 <see cref="GetButtonConfig"/> 分支。
    /// </summary>
    /// <param name="permission">菜单权限字符串，例如 <c>identity:user:list</c>。</param>
    /// <returns>
    /// 第一段的小写形式；若以 <c>takt</c> 开头则返回空字符串，表示使用默认按钮模板。
    /// </returns>
    private static string GetModulePrefix(string permission)
    {
        if (string.IsNullOrEmpty(permission))
            return string.Empty;

        var parts = permission.Split(':');
        if (parts.Length == 0)
            return string.Empty;

        if (parts[0].Equals("takt", StringComparison.OrdinalIgnoreCase))
            return string.Empty;

        return parts[0].ToLower();
    }

    /// <summary>
    /// 从权限字符串中解析“菜单段”用于拼接按钮权限：去掉首段模块名与末段 <c>list</c>，保留中间段。
    /// </summary>
    /// <param name="permission">菜单权限字符串。</param>
    /// <returns>用于拼接按钮权限的中间路径（小写），例如 <c>user</c> 或 <c>a:b</c>。</returns>
    private static string GetMenuPerm(string? permission)
    {
        if (string.IsNullOrEmpty(permission))
            return string.Empty;

        var parts = permission.Split(':');
        if (parts.Length > 2)
            // 截取第一个冒号之后、最后一个冒号之前的部分（支持多级路径）
            return string.Join(":", parts[1..^1]).ToLower();

        if (parts.Length == 2)
            // 仅两段时取第二段（如 module:entity:list 中的 entity）
            return parts[1].ToLower();

        if (parts.Length == 1)
            return parts[0].ToLower();

        return string.Empty;
    }

    /// <summary>
    /// 按模块前缀返回按钮显示名称数组与权限后缀（英文）数组；两者长度一致、一一对应。
    /// </summary>
    /// <param name="modulePrefix">
    /// 模块前缀，如 <c>identity</c>、<c>workflow</c>、<c>routine</c> 等；
    /// 空或未知前缀时使用通用 CRUD 按钮组。
    /// </param>
    /// <returns>
    /// 名称与权限后缀数组；若内部无匹配逻辑返回空引用（此处各分支均返回有效数组）。
    /// </returns>
    private static (string[]? names, string[]? perms) GetButtonConfig(string modulePrefix)
    {
        // 通用按钮（默认模块或未知前缀）
        var buttonNames = new[] { "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销" };
        var buttonPerms = new[] { "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke" };

        // 身份认证（identity）扩展按钮
        var buttonIdNames = new[] { "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "授权", "分配", "重置密码", "变更密码", "重置", "变更", "清空", "截断", "解锁", "禁用" };
        var buttonIdPerms = new[] { "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "authorize", "allocate", "resetpwd", "changepwd", "reset", "change", "empty", "truncate", "unlock", "disable" };

        var buttonGenNames = new[] { "查询", "新增", "修改", "删除", "生成", "预览", "下载", "同步", "导入", "导出", "模板", "字段", "表", "数据库", "初始化", "克隆", "清空", "截断" };
        var buttonGenPerms = new[] { "query", "create", "update", "delete", "generate", "preview", "download", "sync", "import", "export", "template", "columns", "tables", "databases", "initialize", "clone", "empty", "truncate" };

        // 工作流（workflow）：通用操作 + 流程定义/实例/表单等
        var buttonFlowNames = new[]
        {
            "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "复制", "克隆",
            "暂停", "恢复", "提交", "撤回", "转办", "委托", "退回", "催办", "加签", "减签", "进度", "历史",
            "发布", "停用", "启用", "版本", "设计", "配置", "验证",
            "启动", "终止",
            "字段管理", "权限设置", "数据源配置", "主题设置", "表单数据",
            "流转归档", "流转清理"
        };
        var buttonFlowPerms = new[]
        {
            "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "copy", "clone",
            "suspend", "resume", "submit", "withdraw", "transfer", "delegate", "return", "urge", "addsign", "reducesign", "progress", "history",
            "publish", "disable", "enable", "version", "design", "config", "validate",
            "start", "terminate",
            "field", "permission", "datasource", "theme", "data",
            "archive", "clean"
        };

        // 日常事务（routine）：文档/社交/文件/系统等扩展
        var buttonRoutineNames = new[]
        {
            "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销",
            "克隆", "复制",
            "保存草稿", "删除草稿", "发送", "撤回", "转发", "回复", "已读", "未读", "传阅", "签收", "催办", "确认",
            "点赞", "取消点赞", "收藏", "取消收藏", "分享", "取消分享", "评论", "取消评论", "举报", "取消举报", "关注", "取消关注",
            "上传", "下载", "归档", "销毁", "版本",
            "运行", "停止", "重启", "刷新", "重置", "清空"
        };
        var buttonRoutinePerms = new[]
        {
            "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke",
            "clone", "copy",
            "draft", "deletedraft", "send", "withdraw", "forward", "reply", "read", "unread", "circulate", "sign", "urge", "confirm",
            "like", "unlike", "favorite", "unfavorite", "share", "unshare", "comment", "uncomment", "flagging", "unflagging", "follow", "unfollow",
            "upload", "download", "archive", "destroy", "version",
            "run", "stop", "restart", "refresh", "reset", "empty"
        };

        // 人力资源（humanresource）
        var buttonHrmNames = new[] { "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "核算" };
        var buttonHrmPerms = new[] { "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "calculate" };

        // 会计（accounting）：在通用基础上增加财务操作后缀
        var buttonAccountingNames = new[]
        {
            "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "核算",
            "记账", "结账", "对账", "支付", "折旧", "报销", "冲销", "计提", "账期", "结转", "作废"
        };
        var buttonAccountingPerms = new[]
        {
            "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "calculate",
            "book", "closing", "reconcile", "payment", "depreciation", "reimburse", "reversal", "accrual", "period", "carryforward", "cancel"
        };

        // 根据模块前缀（Permission 第一段，如 identity、code、workflow、routine、humanresource、accounting）选择对应按钮组
        switch (modulePrefix.ToLowerInvariant())
        {
            case "identity":
                return (buttonIdNames, buttonIdPerms);
            case "code":
                return (buttonGenNames, buttonGenPerms);
            case "workflow":
                return (buttonFlowNames, buttonFlowPerms);
            case "routine":
                return (buttonRoutineNames, buttonRoutinePerms);
            case "humanresource":
                return (buttonHrmNames, buttonHrmPerms);
            case "accounting":
                return (buttonAccountingNames, buttonAccountingPerms);
            default:
                return (buttonNames, buttonPerms);
        }
    }

    /// <summary>
    /// 按菜单编码唯一键插入或更新一条按钮菜单（MenuType=2）。
    /// </summary>
    /// <param name="menuRepository">菜单仓储。</param>
    /// <param name="parentId">父页面菜单 Id。</param>
    /// <param name="menuCode">按钮全局唯一编码（通常父 MenuCode_操作后缀）。</param>
    /// <param name="menuName">按钮显示名称。</param>
    /// <param name="permission">按钮完整权限标识。</param>
    /// <param name="menuL10nKey">多语言键，形如 <c>common.button.xxx</c>。</param>
    /// <param name="SortOrder">同级排序号。</param>
    /// <returns>元组：(1,0) 表示新建，(0,1) 表示更新。</returns>
    private static async Task<(int InsertCount, int UpdateCount)> CreateOrUpdateButtonAsync(
        ITaktRepository<TaktMenu> menuRepository,
        long parentId,
        string menuCode,
        string menuName,
        string permission,
        string menuL10nKey,
        int SortOrder)
    {
        var button = await menuRepository.GetAsync(m => m.MenuCode == menuCode);

        if (button == null)
        {
            // 不存在则插入
            button = new TaktMenu
            {
                MenuName = menuName,
                MenuCode = menuCode,
                MenuL10nKey = menuL10nKey,
                ParentId = parentId,
                MenuType = 2,
                Permission = permission,
                SortOrder = SortOrder,
                MenuStatus = 1,
                IsVisible = 1,
                IsCache = 0,
                IsExternal = 0,
                IsDeleted = 0
            };
            await menuRepository.CreateAsync(button);
            return (1, 0);
        }

        // 已存在则更新（保持与种子定义一致）
        button.MenuName = menuName;
        button.MenuL10nKey = menuL10nKey;
        button.ParentId = parentId;
        button.MenuType = 2;
        button.Permission = permission;
        button.SortOrder = SortOrder;
        button.MenuStatus = 1;
        button.IsVisible = 1;
        button.IsCache = 0;
        button.IsExternal = 0;
        await menuRepository.UpdateAsync(button);
        return (0, 1);
    }

    /// <summary>
    /// 生成按钮编码（兼容 menu_code nvarchar(200) 限制）。
    /// </summary>
    private static string BuildButtonCode(string? menuCode, string buttonPerm)
    {
        var action = buttonPerm.ToLowerInvariant();
        var full = string.IsNullOrWhiteSpace(menuCode)
            ? action
            : $"{menuCode.ToLowerInvariant()}_{action}";

        const int maxLen = 200;
        if (full.Length <= maxLen)
            return full;

        // 追加 8 位稳定哈希，避免截断后冲突；保留尽可能多的前缀可读性
        var hash = ShortHash(full, 8);
        var reserved = 1 + hash.Length; // "_" + hash
        var prefixLen = Math.Max(1, maxLen - reserved);
        var prefix = full[..prefixLen];
        return $"{prefix}_{hash}";
    }

    private static string ShortHash(string input, int hexLen)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        var hex = Convert.ToHexString(bytes).ToLowerInvariant();
        return hex.Length <= hexLen ? hex : hex[..hexLen];
    }
}
