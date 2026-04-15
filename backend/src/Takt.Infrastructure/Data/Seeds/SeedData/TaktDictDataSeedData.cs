// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktDictDataSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字典数据种子数据，初始化系统内置字典数据
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.Dict;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt字典数据种子数据
/// </summary>
public class TaktDictDataSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（字典数据应该在字典类型之后初始化）
    /// </summary>
    public int Order => 101;

    /// <summary>
    /// 初始化字典数据种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var dictDataRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktDictData>>();
        var dictTypeRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktDictType>>();

        int insertCount = 0;
        int updateCount = 0;

        // 定义系统内置字典数据（按字典类型顺序）
        var dictDataList = new[]
        {
            // sys_normal_disable - 默认状态：1=启用，0=禁用，2=锁定
            new { DictTypeCode = "sys_normal_disable", DictLabel = "启用", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "启用状态" },
            new { DictTypeCode = "sys_normal_disable", DictLabel = "禁用", DictValue = "0", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "禁用状态" },
            new { DictTypeCode = "sys_normal_disable", DictLabel = "锁定", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "锁定状态" },
            
            // sys_yes_no - 是否：1=是/启用，0=否/禁用
            new { DictTypeCode = "sys_yes_no", DictLabel = "是", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "是" },
            new { DictTypeCode = "sys_yes_no", DictLabel = "否", DictValue = "0", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "否" },
            
            // sys_is_default - 是否默认：1=是/默认，0=否/非默认
            new { DictTypeCode = "sys_is_default", DictLabel = "是", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "是默认" },
            new { DictTypeCode = "sys_is_default", DictLabel = "否", DictValue = "0", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "非默认" },

            // sys_is_builtin - 是否内置：1=是/内置，0=否/自定义
            new { DictTypeCode = "sys_is_builtin", DictLabel = "是", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "内置" },
            new { DictTypeCode = "sys_is_builtin", DictLabel = "否", DictValue = "0", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "自定义" },
            
            // sys_is_public - 是否公开：0=公开，1=私有
            new { DictTypeCode = "sys_is_public", DictLabel = "公开", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "公开" },
            new { DictTypeCode = "sys_is_public", DictLabel = "私有", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "私有" },
            
            // sys_oper_type - 操作类型：1=新增，2=修改，3=删除，4=查询，5=导出，6=导入，7=授权，8=强退，9=生成代码，10=清空数据
            new { DictTypeCode = "sys_oper_type", DictLabel = "新增", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "新增操作" },
            new { DictTypeCode = "sys_oper_type", DictLabel = "修改", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "修改操作" },
            new { DictTypeCode = "sys_oper_type", DictLabel = "删除", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "删除操作" },
            new { DictTypeCode = "sys_oper_type", DictLabel = "查询", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "查询操作" },
            new { DictTypeCode = "sys_oper_type", DictLabel = "导出", DictValue = "5", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "导出操作" },
            new { DictTypeCode = "sys_oper_type", DictLabel = "导入", DictValue = "6", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "导入操作" },
            new { DictTypeCode = "sys_oper_type", DictLabel = "授权", DictValue = "7", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "授权操作" },
            new { DictTypeCode = "sys_oper_type", DictLabel = "强退", DictValue = "8", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "强退操作" },
            new { DictTypeCode = "sys_oper_type", DictLabel = "生成代码", DictValue = "9", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "生成代码操作" },
            new { DictTypeCode = "sys_oper_type", DictLabel = "清空数据", DictValue = "10", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "清空数据操作" },
            
            // sys_user_gender - 用户性别：0=未知，1=男，2=女
            new { DictTypeCode = "sys_user_gender", DictLabel = "未知", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "未知性别" },
            new { DictTypeCode = "sys_user_gender", DictLabel = "男", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "男性" },
            new { DictTypeCode = "sys_user_gender", DictLabel = "女", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "女性" },
            
            // sys_user_type - 用户类型：0=普通用户，1=管理员，2=超级管理员
            new { DictTypeCode = "sys_user_type", DictLabel = "普通用户", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "普通用户" },
            new { DictTypeCode = "sys_user_type", DictLabel = "管理员", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "管理员" },
            new { DictTypeCode = "sys_user_type", DictLabel = "超级管理员", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "超级管理员" },
            
            // sys_data_scope - 数据范围：0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围
            new { DictTypeCode = "sys_data_scope", DictLabel = "全部数据", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "全部数据" },
            new { DictTypeCode = "sys_data_scope", DictLabel = "本部门数据", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "本部门数据" },
            new { DictTypeCode = "sys_data_scope", DictLabel = "本部门及以下数据", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "本部门及以下数据" },
            new { DictTypeCode = "sys_data_scope", DictLabel = "仅本人数据", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "仅本人数据" },
            new { DictTypeCode = "sys_data_scope", DictLabel = "自定义数据范围", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "自定义数据范围" },
            
            // sys_menu_type - 菜单类型：0=目录，1=菜单，2=按钮
            new { DictTypeCode = "sys_menu_type", DictLabel = "目录", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "目录" },
            new { DictTypeCode = "sys_menu_type", DictLabel = "菜单", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "菜单" },
            new { DictTypeCode = "sys_menu_type", DictLabel = "按钮", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "按钮" },
            
            // sys_dept_type - 部门类型：0=直接，1=间接
            new { DictTypeCode = "sys_dept_type", DictLabel = "直接", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "直接" },
            new { DictTypeCode = "sys_dept_type", DictLabel = "间接", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "间接" },
            
            // sys_post_category - 岗位类别：管理类、技术类、业务类、支持类
            new { DictTypeCode = "sys_post_category", DictLabel = "管理类", DictValue = "management", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "管理类岗位" },
            new { DictTypeCode = "sys_post_category", DictLabel = "技术类", DictValue = "technical", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "技术类岗位" },
            new { DictTypeCode = "sys_post_category", DictLabel = "业务类", DictValue = "business", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "业务类岗位" },
            new { DictTypeCode = "sys_post_category", DictLabel = "支持类", DictValue = "support", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "支持类岗位" },
            
            // sys_post_level - 岗位级别：1=初级，2=中级，3=高级，4=专家，5=资深
            new { DictTypeCode = "sys_post_level", DictLabel = "初级", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "初级岗位" },
            new { DictTypeCode = "sys_post_level", DictLabel = "中级", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "中级岗位" },
            new { DictTypeCode = "sys_post_level", DictLabel = "高级", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "高级岗位" },
            new { DictTypeCode = "sys_post_level", DictLabel = "专家", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "专家岗位" },
            new { DictTypeCode = "sys_post_level", DictLabel = "资深", DictValue = "5", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "资深岗位" },
            
            // sys_notice_type - 公告类型：0=通知，1=公告，2=新闻，3=活动
            new { DictTypeCode = "sys_notice_type", DictLabel = "通知", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "通知" },
            new { DictTypeCode = "sys_notice_type", DictLabel = "公告", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "公告" },
            new { DictTypeCode = "sys_notice_type", DictLabel = "新闻", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "新闻" },
            new { DictTypeCode = "sys_notice_type", DictLabel = "活动", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "活动" },
            
            // sys_publish_scope - 发布范围：0=全部，1=指定部门，2=指定用户，3=指定角色
            new { DictTypeCode = "sys_publish_scope", DictLabel = "全部", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "全部" },
            new { DictTypeCode = "sys_publish_scope", DictLabel = "指定部门", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "指定部门" },
            new { DictTypeCode = "sys_publish_scope", DictLabel = "指定用户", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "指定用户" },
            new { DictTypeCode = "sys_publish_scope", DictLabel = "指定角色", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "指定角色" },
            
            // sys_urgency_level - 是否紧急：0=一般，1=紧急，2=非常紧急
            new { DictTypeCode = "sys_urgency_level", DictLabel = "一般", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "一般" },
            new { DictTypeCode = "sys_urgency_level", DictLabel = "紧急", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "紧急" },
            new { DictTypeCode = "sys_urgency_level", DictLabel = "非常紧急", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "非常紧急" },
            
            // sys_notice_status - 公告状态：0=草稿，1=已发布，2=已撤回，3=已过期
            new { DictTypeCode = "sys_notice_status", DictLabel = "草稿", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "草稿" },
            new { DictTypeCode = "sys_notice_status", DictLabel = "已发布", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "已发布" },
            new { DictTypeCode = "sys_notice_status", DictLabel = "已撤回", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "已撤回" },
            new { DictTypeCode = "sys_notice_status", DictLabel = "已过期", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "已过期" },
            
            // sys_list_class - 列表类名（用于前端样式控制，这里不预设具体值，由业务系统动态管理）
            
            // sys_storage_type - 存储方式：0=本地存储，1=OSS对象存储，2=FTP
            new { DictTypeCode = "sys_storage_type", DictLabel = "本地存储", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "本地存储" },
            new { DictTypeCode = "sys_storage_type", DictLabel = "OSS对象存储", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "OSS对象存储" },
            new { DictTypeCode = "sys_storage_type", DictLabel = "FTP", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "FTP" },
            
            // sys_file_category - 文件分类：0=文档，1=图片，2=视频，3=音频，4=压缩包，5=其他
            new { DictTypeCode = "sys_file_category", DictLabel = "文档", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "文档" },
            new { DictTypeCode = "sys_file_category", DictLabel = "图片", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "图片" },
            new { DictTypeCode = "sys_file_category", DictLabel = "视频", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "视频" },
            new { DictTypeCode = "sys_file_category", DictLabel = "音频", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "音频" },
            new { DictTypeCode = "sys_file_category", DictLabel = "压缩包", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "压缩包" },
            new { DictTypeCode = "sys_file_category", DictLabel = "其他", DictValue = "5", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "其他" },
            
            // sys_file_status - 文件状态：0=正常，1=已锁定，2=已归档，3=已删除
            new { DictTypeCode = "sys_file_status", DictLabel = "正常", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "正常" },
            new { DictTypeCode = "sys_file_status", DictLabel = "已锁定", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "已锁定" },
            new { DictTypeCode = "sys_file_status", DictLabel = "已归档", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "已归档" },
            new { DictTypeCode = "sys_file_status", DictLabel = "已删除", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "已删除" },
            
            // sys_language_code - 语言编码：ISO 639-1/639-2，按 DictValue 字母排序（联合国6种官方语言+日语+韩语）
            new { DictTypeCode = "sys_language_code", DictLabel = "العربية", DictValue = "ar-SA", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "阿拉伯语" },
            new { DictTypeCode = "sys_language_code", DictLabel = "English", DictValue = "en-US", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "英语" },
            new { DictTypeCode = "sys_language_code", DictLabel = "Español", DictValue = "es-ES", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "西班牙语" },
            new { DictTypeCode = "sys_language_code", DictLabel = "Français", DictValue = "fr-FR", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "法语" },
            new { DictTypeCode = "sys_language_code", DictLabel = "日本語", DictValue = "ja-JP", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "日语" },
            new { DictTypeCode = "sys_language_code", DictLabel = "한국어", DictValue = "ko-KR", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "韩语" },
            new { DictTypeCode = "sys_language_code", DictLabel = "Русский", DictValue = "ru-RU", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "俄语" },
            new { DictTypeCode = "sys_language_code", DictLabel = "简体中文", DictValue = "zh-CN", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "中文（简体）" },
            new { DictTypeCode = "sys_language_code", DictLabel = "繁体中文", DictValue = "zh-TW", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "中文（繁体）" },
            
            // sys_resource_type - 资源类型：Frontend=前端，Backend=后端
            new { DictTypeCode = "sys_resource_type", DictLabel = "前端", DictValue = "Frontend", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "前端" },
            new { DictTypeCode = "sys_resource_type", DictLabel = "后端", DictValue = "Backend", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "后端" },
            
            // sys_mail_type - 邮件类型：0=普通邮件，1=系统邮件，2=通知邮件，3=提醒邮件
            new { DictTypeCode = "sys_mail_type", DictLabel = "普通邮件", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "普通邮件" },
            new { DictTypeCode = "sys_mail_type", DictLabel = "系统邮件", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "系统邮件" },
            new { DictTypeCode = "sys_mail_type", DictLabel = "通知邮件", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "通知邮件" },
            new { DictTypeCode = "sys_mail_type", DictLabel = "提醒邮件", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "提醒邮件" },
            
            // sys_mail_status - 邮件状态：0=草稿，1=已发送，2=发送失败，3=已撤回，4=定时发送中
            new { DictTypeCode = "sys_mail_status", DictLabel = "草稿", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "草稿" },
            new { DictTypeCode = "sys_mail_status", DictLabel = "已发送", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "已发送" },
            new { DictTypeCode = "sys_mail_status", DictLabel = "发送失败", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "发送失败" },
            new { DictTypeCode = "sys_mail_status", DictLabel = "已撤回", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "已撤回" },
            new { DictTypeCode = "sys_mail_status", DictLabel = "定时发送中", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "定时发送中" },
            
            // sys_news_category - 新闻分类：0=公司新闻，1=行业动态，2=技术分享，3=产品发布，4=活动资讯，5=其他
            new { DictTypeCode = "sys_news_category", DictLabel = "公司新闻", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "公司新闻" },
            new { DictTypeCode = "sys_news_category", DictLabel = "行业动态", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "行业动态" },
            new { DictTypeCode = "sys_news_category", DictLabel = "技术分享", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "技术分享" },
            new { DictTypeCode = "sys_news_category", DictLabel = "产品发布", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "产品发布" },
            new { DictTypeCode = "sys_news_category", DictLabel = "活动资讯", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "活动资讯" },
            new { DictTypeCode = "sys_news_category", DictLabel = "其他", DictValue = "5", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "其他" },
            
            // sys_news_status - 新闻状态：0=草稿，1=已发布，2=已撤回，3=已过期
            new { DictTypeCode = "sys_news_status", DictLabel = "草稿", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "草稿" },
            new { DictTypeCode = "sys_news_status", DictLabel = "已发布", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "已发布" },
            new { DictTypeCode = "sys_news_status", DictLabel = "已撤回", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "已撤回" },
            new { DictTypeCode = "sys_news_status", DictLabel = "已过期", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "已过期" },
            
            // sys_setting_group - 设置分组：backend=后端，frontend=前端
            new { DictTypeCode = "sys_setting_group", DictLabel = "后端", DictValue = "backend", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "后端" },
            new { DictTypeCode = "sys_setting_group", DictLabel = "前端", DictValue = "frontend", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "前端" },
            
            // sys_message_type - 消息类型：Text=文本，Image=图片，File=文件，System=系统消息
            new { DictTypeCode = "sys_message_type", DictLabel = "文本", DictValue = "Text", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "文本" },
            new { DictTypeCode = "sys_message_type", DictLabel = "图片", DictValue = "Image", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "图片" },
            new { DictTypeCode = "sys_message_type", DictLabel = "文件", DictValue = "File", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "文件" },
            new { DictTypeCode = "sys_message_type", DictLabel = "系统消息", DictValue = "Takt365", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "系统消息" },
            
            // sys_message_group - 消息分组：Chat=聊天，Notification=通知，Alert=提醒
            new { DictTypeCode = "sys_message_group", DictLabel = "聊天", DictValue = "Chat", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "聊天" },
            new { DictTypeCode = "sys_message_group", DictLabel = "通知", DictValue = "Notification", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "通知" },
            new { DictTypeCode = "sys_message_group", DictLabel = "提醒", DictValue = "Alert", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "提醒" },
            
            // sys_read_status - 读取状态：0=未读，1=已读
            new { DictTypeCode = "sys_read_status", DictLabel = "未读", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "未读" },
            new { DictTypeCode = "sys_read_status", DictLabel = "已读", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "已读" },
            
            // sys_online_status - 在线状态：0=在线，1=离线，2=离开
            new { DictTypeCode = "sys_online_status", DictLabel = "在线", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "在线" },
            new { DictTypeCode = "sys_online_status", DictLabel = "离线", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "离线" },
            new { DictTypeCode = "sys_online_status", DictLabel = "离开", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "离开" },
            
            // sys_form_category - 表单分类：0=通用表单，1=业务表单，2=系统表单
            new { DictTypeCode = "sys_form_category", DictLabel = "通用表单", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "通用表单" },
            new { DictTypeCode = "sys_form_category", DictLabel = "业务表单", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "业务表单" },
            new { DictTypeCode = "sys_form_category", DictLabel = "系统表单", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "系统表单" },
            
            // sys_form_type - 表单类型：0=动态表单，1=静态表单，2=自定义表单
            new { DictTypeCode = "sys_form_type", DictLabel = "动态表单", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "动态表单" },
            new { DictTypeCode = "sys_form_type", DictLabel = "静态表单", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "静态表单" },
            new { DictTypeCode = "sys_form_type", DictLabel = "自定义表单", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "自定义表单" },
            
            // sys_scheme_status - 方案状态（流程/表单方案共用）：0=草稿，1=已发布，2=已禁用
            new { DictTypeCode = "sys_scheme_status", DictLabel = "草稿", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "草稿" },
            new { DictTypeCode = "sys_scheme_status", DictLabel = "已发布", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "已发布" },
            new { DictTypeCode = "sys_scheme_status", DictLabel = "已禁用", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "已禁用" },
            
            // sys_priority - 优先级：0=低，1=中，2=高，3=紧急
            new { DictTypeCode = "sys_priority", DictLabel = "低", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "低" },
            new { DictTypeCode = "sys_priority", DictLabel = "中", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "中" },
            new { DictTypeCode = "sys_priority", DictLabel = "高", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "高" },
            new { DictTypeCode = "sys_priority", DictLabel = "紧急", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "紧急" },
            
            // sys_flow_category - 流程分类：0=通用流程，1=业务流程，2=系统流程
            new { DictTypeCode = "sys_flow_category", DictLabel = "通用流程", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "通用流程" },
            new { DictTypeCode = "sys_flow_category", DictLabel = "业务流程", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "业务流程" },
            new { DictTypeCode = "sys_flow_category", DictLabel = "系统流程", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "系统流程" },
            
            // sys_flow_status - 流程状态：0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回，5=草稿
            new { DictTypeCode = "sys_flow_status", DictLabel = "运行中", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "运行中" },
            new { DictTypeCode = "sys_flow_status", DictLabel = "已完成", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "已完成" },
            new { DictTypeCode = "sys_flow_status", DictLabel = "已终止", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "已终止" },
            new { DictTypeCode = "sys_flow_status", DictLabel = "已挂起", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "已挂起" },
            new { DictTypeCode = "sys_flow_status", DictLabel = "已撤回", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "已撤回" },
            new { DictTypeCode = "sys_flow_status", DictLabel = "草稿",   DictValue = "5", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "草稿" },
            
            // sys_gen_template_type - 生成模板类型：crud=单表操作，tree=树表操作，sub=主子表操作
            new { DictTypeCode = "sys_gen_template_type", DictLabel = "单表操作", DictValue = "crud", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "单表操作" },
            new { DictTypeCode = "sys_gen_template_type", DictLabel = "树表操作", DictValue = "tree", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "树表操作" },
            new { DictTypeCode = "sys_gen_template_type", DictLabel = "主子表操作", DictValue = "sub", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "主子表操作" },

            // sys_gen_method - 生成方式：0=zip 压缩包，1=自定义路径，2=当前项目
            new { DictTypeCode = "sys_gen_method", DictLabel = "zip 压缩包", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "zip 压缩包" },
            new { DictTypeCode = "sys_gen_method", DictLabel = "自定义路径", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "自定义路径" },
            new { DictTypeCode = "sys_gen_method", DictLabel = "当前项目", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "当前项目" },

            // sys_sort_type - 排序类型：asc=升序，desc=降序
            new { DictTypeCode = "sys_sort_type", DictLabel = "升序", DictValue = "asc", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "升序" },
            new { DictTypeCode = "sys_sort_type", DictLabel = "降序", DictValue = "desc", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "降序" },

            // sys_dto_category - 传输对象类别：Dto,QueryDto,CreateDto,UpdateDto,TemplateDto,ImportDto,ExportDto等
            new { DictTypeCode = "sys_dto_category", DictLabel = "Dto", DictValue = "Dto", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "Dto" },
            new { DictTypeCode = "sys_dto_category", DictLabel = "QueryDto", DictValue = "QueryDto", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "QueryDto" },
            new { DictTypeCode = "sys_dto_category", DictLabel = "CreateDto", DictValue = "CreateDto", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "CreateDto" },
            new { DictTypeCode = "sys_dto_category", DictLabel = "UpdateDto", DictValue = "UpdateDto", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "UpdateDto" },
            new { DictTypeCode = "sys_dto_category", DictLabel = "TemplateDto", DictValue = "TemplateDto", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "TemplateDto" },
            new { DictTypeCode = "sys_dto_category", DictLabel = "ImportDto", DictValue = "ImportDto", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "ImportDto" },
            new { DictTypeCode = "sys_dto_category", DictLabel = "ExportDto", DictValue = "ExportDto", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "ExportDto" },
            
            // sys_gen_function - 生成功能：查询，新增，更新，删除，模板，导入，导出（DictValue 与后端 ParseGenFunctionKeys 期望的中文键一致）
            new { DictTypeCode = "sys_gen_function", DictLabel = "查询", DictValue = "查询", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "查询" },
            new { DictTypeCode = "sys_gen_function", DictLabel = "新增", DictValue = "新增", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "新增" },
            new { DictTypeCode = "sys_gen_function", DictLabel = "更新", DictValue = "更新", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "更新" },
            new { DictTypeCode = "sys_gen_function", DictLabel = "删除", DictValue = "删除", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "删除" },
            new { DictTypeCode = "sys_gen_function", DictLabel = "模板", DictValue = "模板", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "模板" },
            new { DictTypeCode = "sys_gen_function", DictLabel = "导入", DictValue = "导入", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "导入" },
            new { DictTypeCode = "sys_gen_function", DictLabel = "导出", DictValue = "导出", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "导出" },
            
            // sys_frontend_template - 前端模板：1=element plus，2=ant design vue
            new { DictTypeCode = "sys_frontend_template", DictLabel = "element plus", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "element plus" },
            new { DictTypeCode = "sys_frontend_template", DictLabel = "ant design vue", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "ant design vue" },
            
            // sys_frontend_style - 前端样式：12=一行一列，24=一行两列
            new { DictTypeCode = "sys_frontend_style", DictLabel = "一行一列", DictValue = "12", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "一行一列" },
            new { DictTypeCode = "sys_frontend_style", DictLabel = "一行两列", DictValue = "24", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "一行两列" },
            
            // sys_button_style - 操作按钮样式：0=文本，1=标准
            new { DictTypeCode = "sys_button_style", DictLabel = "文本", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "文本" },
            new { DictTypeCode = "sys_button_style", DictLabel = "标准", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "标准" },
            
            // sys_csharp_type - C#类型：对应C#数据类型，按 DictValue 字母排序
            new { DictTypeCode = "sys_csharp_type", DictLabel = "bool", DictValue = "bool", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "bool" },
            new { DictTypeCode = "sys_csharp_type", DictLabel = "byte", DictValue = "byte", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "byte" },
            new { DictTypeCode = "sys_csharp_type", DictLabel = "DateTime", DictValue = "DateTime", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "DateTime" },
            new { DictTypeCode = "sys_csharp_type", DictLabel = "decimal", DictValue = "decimal", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "decimal" },
            new { DictTypeCode = "sys_csharp_type", DictLabel = "double", DictValue = "double", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "double" },
            new { DictTypeCode = "sys_csharp_type", DictLabel = "float", DictValue = "float", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "float" },
            new { DictTypeCode = "sys_csharp_type", DictLabel = "Guid", DictValue = "Guid", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "Guid" },
            new { DictTypeCode = "sys_csharp_type", DictLabel = "int", DictValue = "int", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "int" },
            new { DictTypeCode = "sys_csharp_type", DictLabel = "long", DictValue = "long", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "long" },
            new { DictTypeCode = "sys_csharp_type", DictLabel = "string", DictValue = "string", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "string" },
            
            // sys_db_type - 数据库数据类型：按 DictValue 字母排序
            new { DictTypeCode = "sys_db_type", DictLabel = "bigint", DictValue = "bigint", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "bigint" },
            new { DictTypeCode = "sys_db_type", DictLabel = "bit", DictValue = "bit", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "bit" },
            new { DictTypeCode = "sys_db_type", DictLabel = "datetime", DictValue = "datetime", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "datetime" },
            new { DictTypeCode = "sys_db_type", DictLabel = "decimal", DictValue = "decimal", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "decimal" },
            new { DictTypeCode = "sys_db_type", DictLabel = "int", DictValue = "int", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "int" },
            new { DictTypeCode = "sys_db_type", DictLabel = "ntext", DictValue = "ntext", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "ntext" },
            new { DictTypeCode = "sys_db_type", DictLabel = "nvarchar", DictValue = "nvarchar", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "nvarchar" },
            new { DictTypeCode = "sys_db_type", DictLabel = "text", DictValue = "text", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "text" },
            new { DictTypeCode = "sys_db_type", DictLabel = "uniqueidentifier", DictValue = "uniqueidentifier", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "uniqueidentifier" },
            new { DictTypeCode = "sys_db_type", DictLabel = "varchar", DictValue = "varchar", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "varchar" },
            
            // sys_query_type - 查询方式：EQ=等于，NE=不等于，GT=大于，GTE=大于等于，LT=小于，LTE=小于等于，LIKE=模糊，BETWEEN=范围
            new { DictTypeCode = "sys_query_type", DictLabel = "等于", DictValue = "EQ", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "等于" },
            new { DictTypeCode = "sys_query_type", DictLabel = "不等于", DictValue = "NE", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "不等于" },
            new { DictTypeCode = "sys_query_type", DictLabel = "大于", DictValue = "GT", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "大于" },
            new { DictTypeCode = "sys_query_type", DictLabel = "大于等于", DictValue = "GTE", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "大于等于" },
            new { DictTypeCode = "sys_query_type", DictLabel = "小于", DictValue = "LT", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "小于" },
            new { DictTypeCode = "sys_query_type", DictLabel = "小于等于", DictValue = "LTE", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "小于等于" },
            new { DictTypeCode = "sys_query_type", DictLabel = "模糊", DictValue = "LIKE", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "模糊" },
            new { DictTypeCode = "sys_query_type", DictLabel = "范围", DictValue = "BETWEEN", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "范围" },
            
            // sys_display_type - 显示类型：input=文本框，InputNumber=数字输入框，select=下拉框，checkbox=复选框，radio=单选框，date=日期控件，time=时间控件，image=图片上传，file=文件上传，slider=滑块，switch=开关，Rate=评分，textarea=文本域，editor=富文本编辑器
            new { DictTypeCode = "sys_display_type", DictLabel = "文本框", DictValue = "input", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "文本框" },
            new { DictTypeCode = "sys_display_type", DictLabel = "数字输入框", DictValue = "InputNumber", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "数字输入框" },
            new { DictTypeCode = "sys_display_type", DictLabel = "下拉框", DictValue = "select", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "下拉框" },
            new { DictTypeCode = "sys_display_type", DictLabel = "复选框", DictValue = "checkbox", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "复选框" },
            new { DictTypeCode = "sys_display_type", DictLabel = "单选框", DictValue = "radio", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "单选框" },
            new { DictTypeCode = "sys_display_type", DictLabel = "日期控件", DictValue = "date", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "日期控件" },
            new { DictTypeCode = "sys_display_type", DictLabel = "时间控件", DictValue = "time", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "时间控件" },
            new { DictTypeCode = "sys_display_type", DictLabel = "图片上传", DictValue = "image", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "图片上传" },
            new { DictTypeCode = "sys_display_type", DictLabel = "文件上传", DictValue = "file", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "文件上传" },
            new { DictTypeCode = "sys_display_type", DictLabel = "滑块", DictValue = "slider", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "滑块" },
            new { DictTypeCode = "sys_display_type", DictLabel = "开关", DictValue = "switch", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "开关" },
            new { DictTypeCode = "sys_display_type", DictLabel = "评分", DictValue = "Rate", OrderNum = 12, CssClass = 12, ListClass = 12, Remark = "评分" },
            new { DictTypeCode = "sys_display_type", DictLabel = "文本域", DictValue = "textarea", OrderNum = 13, CssClass = 13, ListClass = 13, Remark = "文本域" },
            new { DictTypeCode = "sys_display_type", DictLabel = "富文本编辑器", DictValue = "editor", OrderNum = 14, CssClass = 14, ListClass = 14, Remark = "富文本编辑器" },
            
            // sys_storage_naming - 存储命名规则：0=原文件+哈希值，1=自动生成，2=自定义
            new { DictTypeCode = "sys_storage_naming", DictLabel = "原文件+哈希值", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "原文件+哈希值" },
            new { DictTypeCode = "sys_storage_naming", DictLabel = "自动生成", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "自动生成" },
            new { DictTypeCode = "sys_storage_naming", DictLabel = "自定义", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "自定义" },
            
            // sys_storage_directory - 存储目录：用于文件分类存储
            new { DictTypeCode = "sys_storage_directory", DictLabel = "默认目录", DictValue = "default", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "默认存储目录" },
            new { DictTypeCode = "sys_storage_directory", DictLabel = "文档目录", DictValue = "documents", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "文档存储目录" },
            new { DictTypeCode = "sys_storage_directory", DictLabel = "图片目录", DictValue = "images", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "图片存储目录" },
            new { DictTypeCode = "sys_storage_directory", DictLabel = "视频目录", DictValue = "videos", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "视频存储目录" },
            new { DictTypeCode = "sys_storage_directory", DictLabel = "音频目录", DictValue = "audios", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "音频存储目录" },
            new { DictTypeCode = "sys_storage_directory", DictLabel = "压缩包目录", DictValue = "archives", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "压缩包存储目录" },
            new { DictTypeCode = "sys_storage_directory", DictLabel = "临时目录", DictValue = "temp", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "临时文件存储目录" },
            
            // sys_oss_provider - OSS提供商类型：aliyun=阿里云OSS，tencent=腾讯云COS，huawei=华为云OBS，aws=AWS S3
            new { DictTypeCode = "sys_oss_provider", DictLabel = "阿里云OSS", DictValue = "aliyun", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "阿里云对象存储OSS" },
            new { DictTypeCode = "sys_oss_provider", DictLabel = "腾讯云COS", DictValue = "tencent", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "腾讯云对象存储COS" },
            new { DictTypeCode = "sys_oss_provider", DictLabel = "华为云OBS", DictValue = "huawei", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "华为云对象存储OBS" },
            new { DictTypeCode = "sys_oss_provider", DictLabel = "AWS S3", DictValue = "aws", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "亚马逊云对象存储S3" },
            
            // sys_ftp_provider - FTP服务提供商：teac_cn=TEAC FTP中国（ftp.teac.com.cn），teac_jp=TEAC FTP日本（rosu2.teac.co.jp）
            new { DictTypeCode = "sys_ftp_provider", DictLabel = "TEAC FTP中国", DictValue = "teac_cn", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "TEAC FTP服务（ftp.teac.com.cn）" },
            new { DictTypeCode = "sys_ftp_provider", DictLabel = "TEAC FTP日本", DictValue = "teac_jp", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "TEAC FTP服务（rosu2.teac.co.jp）" },

            // sys_leave_category - 请假类型（与 TaktLeave.leave_type 一致）
            new { DictTypeCode = "sys_leave_category", DictLabel = "事假", DictValue = "affair", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "事假" },
            new { DictTypeCode = "sys_leave_category", DictLabel = "病假", DictValue = "sick", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "病假" },
            new { DictTypeCode = "sys_leave_category", DictLabel = "年假", DictValue = "annual", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "年假" },
            new { DictTypeCode = "sys_leave_category", DictLabel = "婚假", DictValue = "marriage", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "婚假" },
            new { DictTypeCode = "sys_leave_category", DictLabel = "产假", DictValue = "maternity", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "产假" },
            new { DictTypeCode = "sys_leave_category", DictLabel = "陪产假", DictValue = "paternity", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "陪产假" },
            new { DictTypeCode = "sys_leave_category", DictLabel = "丧假", DictValue = "bereavement", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "丧假" },
            new { DictTypeCode = "sys_leave_category", DictLabel = "调休", DictValue = "compensatory", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "调休" },
            new { DictTypeCode = "sys_leave_category", DictLabel = "私假", DictValue = "personal", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "私假" },
            new { DictTypeCode = "sys_leave_category", DictLabel = "其他", DictValue = "other", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "其他" },

            // txkt_ec_status - 设变状态（改号状态 1～7）
            new { DictTypeCode = "sys_ec_status", DictLabel = "工作的", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "工作的" },
            new { DictTypeCode = "sys_ec_status", DictLabel = "取消的", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "取消的" },
            new { DictTypeCode = "sys_ec_status", DictLabel = "发行的", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "发行的" },
            new { DictTypeCode = "sys_ec_status", DictLabel = "P.P中变更的", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "P.P中变更的" },
            new { DictTypeCode = "sys_ec_status", DictLabel = "固定的", DictValue = "5", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "固定的" },
            new { DictTypeCode = "sys_ec_status", DictLabel = "挂起的", DictValue = "6", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "挂起的" },
            new { DictTypeCode = "sys_ec_status", DictLabel = "拒绝的", DictValue = "7", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "拒绝的" },

            // txkt_ec_distinction - 设变管理区分：1=全仕向，2=部管，3=内部，4=技术
            new { DictTypeCode = "sys_ec_distinction", DictLabel = "全仕向", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "全仕向" },
            new { DictTypeCode = "sys_ec_distinction", DictLabel = "部管", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "部管" },
            new { DictTypeCode = "sys_ec_distinction", DictLabel = "内部", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "内部" },
            new { DictTypeCode = "sys_ec_distinction", DictLabel = "技术", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "技术" },

            // visual_inspection_line - 目视线别（仅 1、2）
            new { DictTypeCode = "visual_inspection_line", DictLabel = "1", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "目视线1" },
            new { DictTypeCode = "visual_inspection_line", DictLabel = "2", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "目视线2" },

            // aoi_inspection_line - AOI线别（仅 1、2、1A）
            new { DictTypeCode = "aoi_inspection_line", DictLabel = "1",  DictValue = "1",  OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "AOI线1" },
            new { DictTypeCode = "aoi_inspection_line", DictLabel = "2",  DictValue = "2",  OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "AOI线2" },
            new { DictTypeCode = "aoi_inspection_line", DictLabel = "1A", DictValue = "1A", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "AOI线1A" },

            // pcba_panel_category - PCBA板位类别（标准）
            new { DictTypeCode = "pcba_panel_category", DictLabel = "A2IO", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "A2IO" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "A2IO B", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "A2IO B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "A2IO T", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "A2IO T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "A4IN B", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "A4IN B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "A4IN T", DictValue = "5", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "A4IN T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "A4OUT B", DictValue = "6", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "A4OUT B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "A4OUT T", DictValue = "7", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "A4OUT T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AD04 T", DictValue = "8", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "AD04 T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ADDA B", DictValue = "9", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "ADDA B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ADDA B/T", DictValue = "10", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "ADDA B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ADDA T", DictValue = "11", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "ADDA T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ADOC", DictValue = "12", OrderNum = 12, CssClass = 12, ListClass = 12, Remark = "ADOC" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ADOC B", DictValue = "13", OrderNum = 13, CssClass = 13, ListClass = 13, Remark = "ADOC B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ADOC B/T", DictValue = "14", OrderNum = 14, CssClass = 14, ListClass = 14, Remark = "ADOC B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ADOC T", DictValue = "15", OrderNum = 15, CssClass = 15, ListClass = 15, Remark = "ADOC T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AES4 B", DictValue = "16", OrderNum = 16, CssClass = 16, ListClass = 16, Remark = "AES4 B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AES4 B/T", DictValue = "17", OrderNum = 17, CssClass = 17, ListClass = 17, Remark = "AES4 B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AES4 T", DictValue = "18", OrderNum = 18, CssClass = 18, ListClass = 18, Remark = "AES4 T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ANA", DictValue = "19", OrderNum = 19, CssClass = 19, ListClass = 19, Remark = "ANA" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ANA A", DictValue = "24", OrderNum = 24, CssClass = 24, ListClass = 24, Remark = "ANA A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ANA B", DictValue = "25", OrderNum = 25, CssClass = 25, ListClass = 25, Remark = "ANA B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ANA B/T", DictValue = "26", OrderNum = 26, CssClass = 26, ListClass = 26, Remark = "ANA B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ANA T", DictValue = "27", OrderNum = 27, CssClass = 27, ListClass = 27, Remark = "ANA T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "APNEL T", DictValue = "28", OrderNum = 28, CssClass = 28, ListClass = 28, Remark = "APNEL T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO", DictValue = "29", OrderNum = 29, CssClass = 29, ListClass = 29, Remark = "AUDIO" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO A", DictValue = "30", OrderNum = 30, CssClass = 30, ListClass = 30, Remark = "AUDIO A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO ALT B", DictValue = "31", OrderNum = 31, CssClass = 31, ListClass = 31, Remark = "AUDIO ALT B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO ALT T", DictValue = "32", OrderNum = 32, CssClass = 32, ListClass = 32, Remark = "AUDIO ALT T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO B", DictValue = "33", OrderNum = 33, CssClass = 33, ListClass = 33, Remark = "AUDIO B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO B/T", DictValue = "34", OrderNum = 34, CssClass = 34, ListClass = 34, Remark = "AUDIO B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO T", DictValue = "35", OrderNum = 35, CssClass = 35, ListClass = 35, Remark = "AUDIO T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO-00-B", DictValue = "36", OrderNum = 36, CssClass = 36, ListClass = 36, Remark = "AUDIO-00-B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO-00-T", DictValue = "37", OrderNum = 37, CssClass = 37, ListClass = 37, Remark = "AUDIO-00-T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO-10-B", DictValue = "38", OrderNum = 38, CssClass = 38, ListClass = 38, Remark = "AUDIO-10-B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO-10-T", DictValue = "39", OrderNum = 39, CssClass = 39, ListClass = 39, Remark = "AUDIO-10-T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO-20-B", DictValue = "40", OrderNum = 40, CssClass = 40, ListClass = 40, Remark = "AUDIO-20-B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "AUDIO-20-T", DictValue = "41", OrderNum = 41, CssClass = 41, ListClass = 41, Remark = "AUDIO-20-T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "BOTTOM B", DictValue = "42", OrderNum = 42, CssClass = 42, ListClass = 42, Remark = "BOTTOM B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CCL B", DictValue = "43", OrderNum = 43, CssClass = 43, ListClass = 43, Remark = "CCL B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CCL B/T", DictValue = "44", OrderNum = 44, CssClass = 44, ListClass = 44, Remark = "CCL B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CCL T", DictValue = "45", OrderNum = 45, CssClass = 45, ListClass = 45, Remark = "CCL T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CD B", DictValue = "46", OrderNum = 46, CssClass = 46, ListClass = 46, Remark = "CD B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CD T", DictValue = "47", OrderNum = 47, CssClass = 47, ListClass = 47, Remark = "CD T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CD-MAIN", DictValue = "48", OrderNum = 48, CssClass = 48, ListClass = 48, Remark = "CD-MAIN" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CD-MAIN B", DictValue = "49", OrderNum = 49, CssClass = 49, ListClass = 49, Remark = "CD-MAIN B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CDMCU", DictValue = "50", OrderNum = 50, CssClass = 50, ListClass = 50, Remark = "CDMCU" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CDMCU B", DictValue = "51", OrderNum = 51, CssClass = 51, ListClass = 51, Remark = "CDMCU B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CDMCU B/T", DictValue = "52", OrderNum = 52, CssClass = 52, ListClass = 52, Remark = "CDMCU B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CDMCU T", DictValue = "53", OrderNum = 53, CssClass = 53, ListClass = 53, Remark = "CDMCU T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "COMB B", DictValue = "54", OrderNum = 54, CssClass = 54, ListClass = 54, Remark = "COMB B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "COMB T", DictValue = "55", OrderNum = 55, CssClass = 55, ListClass = 55, Remark = "COMB T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "COMBO B", DictValue = "56", OrderNum = 56, CssClass = 56, ListClass = 56, Remark = "COMBO B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "COMBO T", DictValue = "57", OrderNum = 57, CssClass = 57, ListClass = 57, Remark = "COMBO T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CONN", DictValue = "58", OrderNum = 58, CssClass = 58, ListClass = 58, Remark = "CONN" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CONN A", DictValue = "59", OrderNum = 59, CssClass = 59, ListClass = 59, Remark = "CONN A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CONN B", DictValue = "60", OrderNum = 60, CssClass = 60, ListClass = 60, Remark = "CONN B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CONN B/T", DictValue = "61", OrderNum = 61, CssClass = 61, ListClass = 61, Remark = "CONN B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CONN T", DictValue = "62", OrderNum = 62, CssClass = 62, ListClass = 62, Remark = "CONN T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "CONTACT", DictValue = "63", OrderNum = 63, CssClass = 63, ListClass = 63, Remark = "CONTACT" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DA", DictValue = "64", OrderNum = 64, CssClass = 64, ListClass = 64, Remark = "DA" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DA B", DictValue = "65", OrderNum = 65, CssClass = 65, ListClass = 65, Remark = "DA B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DA T", DictValue = "66", OrderNum = 66, CssClass = 66, ListClass = 66, Remark = "DA T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DA T/B", DictValue = "67", OrderNum = 67, CssClass = 67, ListClass = 67, Remark = "DA T/B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DANY B", DictValue = "68", OrderNum = 68, CssClass = 68, ListClass = 68, Remark = "DANY B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DSP B", DictValue = "70", OrderNum = 70, CssClass = 70, ListClass = 70, Remark = "DSP B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DSP T", DictValue = "71", OrderNum = 71, CssClass = 71, ListClass = 71, Remark = "DSP T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DSPL  T", DictValue = "72", OrderNum = 72, CssClass = 72, ListClass = 72, Remark = "DSPL  T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DSPL A", DictValue = "73", OrderNum = 73, CssClass = 73, ListClass = 73, Remark = "DSPL A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DSPL B", DictValue = "74", OrderNum = 74, CssClass = 74, ListClass = 74, Remark = "DSPL B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DSPL B/T", DictValue = "75", OrderNum = 75, CssClass = 75, ListClass = 75, Remark = "DSPL B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DSPL T", DictValue = "76", OrderNum = 76, CssClass = 76, ListClass = 76, Remark = "DSPL T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DSUB B", DictValue = "77", OrderNum = 77, CssClass = 77, ListClass = 77, Remark = "DSUB B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DSUB T", DictValue = "78", OrderNum = 78, CssClass = 78, ListClass = 78, Remark = "DSUB T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DYNA B", DictValue = "79", OrderNum = 79, CssClass = 79, ListClass = 79, Remark = "DYNA B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DYNA T", DictValue = "80", OrderNum = 80, CssClass = 80, ListClass = 80, Remark = "DYNA T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "DYNA T/B", DictValue = "81", OrderNum = 81, CssClass = 81, ListClass = 81, Remark = "DYNA T/B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ENCODER", DictValue = "82", OrderNum = 82, CssClass = 82, ListClass = 82, Remark = "ENCODER" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ENCOGER", DictValue = "83", OrderNum = 83, CssClass = 83, ListClass = 83, Remark = "ENCOGER" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ETHER", DictValue = "84", OrderNum = 84, CssClass = 84, ListClass = 84, Remark = "ETHER" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ETHER B", DictValue = "85", OrderNum = 85, CssClass = 85, ListClass = 85, Remark = "ETHER B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "ETHER T", DictValue = "86", OrderNum = 86, CssClass = 86, ListClass = 86, Remark = "ETHER T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "EURO", DictValue = "87", OrderNum = 87, CssClass = 87, ListClass = 87, Remark = "EURO" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "EURO B", DictValue = "88", OrderNum = 88, CssClass = 88, ListClass = 88, Remark = "EURO B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "EURO B/T", DictValue = "89", OrderNum = 89, CssClass = 89, ListClass = 89, Remark = "EURO B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "EURO T", DictValue = "90", OrderNum = 90, CssClass = 90, ListClass = 90, Remark = "EURO T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FADER B", DictValue = "91", OrderNum = 91, CssClass = 91, ListClass = 91, Remark = "FADER B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FADER B/T", DictValue = "92", OrderNum = 92, CssClass = 92, ListClass = 92, Remark = "FADER B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FADER T", DictValue = "93", OrderNum = 93, CssClass = 93, ListClass = 93, Remark = "FADER T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FAETHER B", DictValue = "94", OrderNum = 94, CssClass = 94, ListClass = 94, Remark = "FAETHER B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FAETHER T", DictValue = "95", OrderNum = 95, CssClass = 95, ListClass = 95, Remark = "FAETHER T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FRONT", DictValue = "96", OrderNum = 96, CssClass = 96, ListClass = 96, Remark = "FRONT" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FRONT A", DictValue = "97", OrderNum = 97, CssClass = 97, ListClass = 97, Remark = "FRONT A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FRONT B", DictValue = "98", OrderNum = 98, CssClass = 98, ListClass = 98, Remark = "FRONT B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FRONT B/T", DictValue = "99", OrderNum = 99, CssClass = 99, ListClass = 99, Remark = "FRONT B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FRONT SYS T", DictValue = "100", OrderNum = 100, CssClass = 100, ListClass = 100, Remark = "FRONT SYS T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FRONT T", DictValue = "101", OrderNum = 101, CssClass = 101, ListClass = 101, Remark = "FRONT T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FRONT-A", DictValue = "102", OrderNum = 102, CssClass = 102, ListClass = 102, Remark = "FRONT-A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "FROTN B", DictValue = "103", OrderNum = 103, CssClass = 103, ListClass = 103, Remark = "FROTN B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "GATHER", DictValue = "104", OrderNum = 104, CssClass = 104, ListClass = 104, Remark = "GATHER" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "GATHER A", DictValue = "105", OrderNum = 105, CssClass = 105, ListClass = 105, Remark = "GATHER A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "GATHER ALT B", DictValue = "106", OrderNum = 106, CssClass = 106, ListClass = 106, Remark = "GATHER ALT B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "GATHER ALT T", DictValue = "107", OrderNum = 107, CssClass = 107, ListClass = 107, Remark = "GATHER ALT T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "GATHER B", DictValue = "108", OrderNum = 108, CssClass = 108, ListClass = 108, Remark = "GATHER B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "GATHER B/T", DictValue = "109", OrderNum = 109, CssClass = 109, ListClass = 109, Remark = "GATHER B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "GATHER C", DictValue = "110", OrderNum = 110, CssClass = 110, ListClass = 110, Remark = "GATHER C" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "GATHER T", DictValue = "111", OrderNum = 111, CssClass = 111, ListClass = 111, Remark = "GATHER T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "GATHER-C", DictValue = "112", OrderNum = 112, CssClass = 112, ListClass = 112, Remark = "GATHER-C" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "GATHER-J", DictValue = "113", OrderNum = 113, CssClass = 113, ListClass = 113, Remark = "GATHER-J" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "IF", DictValue = "114", OrderNum = 114, CssClass = 114, ListClass = 114, Remark = "IF" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "IF B", DictValue = "117", OrderNum = 117, CssClass = 117, ListClass = 117, Remark = "IF B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "IF T", DictValue = "118", OrderNum = 118, CssClass = 118, ListClass = 118, Remark = "IF T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "INPUT", DictValue = "119", OrderNum = 119, CssClass = 119, ListClass = 119, Remark = "INPUT" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "IO", DictValue = "120", OrderNum = 120, CssClass = 120, ListClass = 120, Remark = "IO" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "IO B", DictValue = "120", OrderNum = 120, CssClass = 120, ListClass = 120, Remark = "IO B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "IO B/T", DictValue = "121", OrderNum = 121, CssClass = 121, ListClass = 121, Remark = "IO B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "IO T", DictValue = "122", OrderNum = 122, CssClass = 122, ListClass = 122, Remark = "IO T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK", DictValue = "123", OrderNum = 123, CssClass = 123, ListClass = 123, Remark = "JACK" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK A", DictValue = "124", OrderNum = 124, CssClass = 124, ListClass = 124, Remark = "JACK A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK B", DictValue = "125", OrderNum = 125, CssClass = 125, ListClass = 125, Remark = "JACK B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK B/T", DictValue = "126", OrderNum = 126, CssClass = 126, ListClass = 126, Remark = "JACK B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK T", DictValue = "127", OrderNum = 127, CssClass = 127, ListClass = 127, Remark = "JACK T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK-00 B", DictValue = "128", OrderNum = 128, CssClass = 128, ListClass = 128, Remark = "JACK-00 B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK-00 T", DictValue = "132", OrderNum = 132, CssClass = 132, ListClass = 132, Remark = "JACK-00 T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK-10 B", DictValue = "133", OrderNum = 133, CssClass = 133, ListClass = 133, Remark = "JACK-10 B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK-10 T", DictValue = "134", OrderNum = 134, CssClass = 134, ListClass = 134, Remark = "JACK-10 T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK-20 B", DictValue = "135", OrderNum = 135, CssClass = 135, ListClass = 135, Remark = "JACK-20 B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK-20 T", DictValue = "136", OrderNum = 136, CssClass = 136, ListClass = 136, Remark = "JACK-20 T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK-30 B", DictValue = "137", OrderNum = 137, CssClass = 137, ListClass = 137, Remark = "JACK-30 B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JACK-30 T", DictValue = "138", OrderNum = 138, CssClass = 138, ListClass = 138, Remark = "JACK-30 T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JOIN", DictValue = "139", OrderNum = 139, CssClass = 139, ListClass = 139, Remark = "JOIN" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JOINTC A", DictValue = "140", OrderNum = 140, CssClass = 140, ListClass = 140, Remark = "JOINTC A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JOINTC B", DictValue = "141", OrderNum = 141, CssClass = 141, ListClass = 141, Remark = "JOINTC B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JOINTC T", DictValue = "142", OrderNum = 142, CssClass = 142, ListClass = 142, Remark = "JOINTC T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JOINTF A", DictValue = "143", OrderNum = 143, CssClass = 143, ListClass = 143, Remark = "JOINTF A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JOINTF B", DictValue = "144", OrderNum = 144, CssClass = 144, ListClass = 144, Remark = "JOINTF B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JOINTF T", DictValue = "145", OrderNum = 145, CssClass = 145, ListClass = 145, Remark = "JOINTF T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "JOINTS", DictValue = "146", OrderNum = 146, CssClass = 146, ListClass = 146, Remark = "JOINTS" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "KEY", DictValue = "147", OrderNum = 147, CssClass = 147, ListClass = 147, Remark = "KEY" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "KEY B", DictValue = "148", OrderNum = 148, CssClass = 148, ListClass = 148, Remark = "KEY B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "KEY B/T", DictValue = "149", OrderNum = 149, CssClass = 149, ListClass = 149, Remark = "KEY B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "KEY T", DictValue = "150", OrderNum = 150, CssClass = 150, ListClass = 150, Remark = "KEY T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "LCD A", DictValue = "151", OrderNum = 151, CssClass = 151, ListClass = 151, Remark = "LCD A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "LCD B", DictValue = "152", OrderNum = 152, CssClass = 152, ListClass = 152, Remark = "LCD B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "LCD B/T", DictValue = "153", OrderNum = 153, CssClass = 153, ListClass = 153, Remark = "LCD B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "LCD EX", DictValue = "154", OrderNum = 154, CssClass = 154, ListClass = 154, Remark = "LCD EX" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "LCD EX B", DictValue = "155", OrderNum = 155, CssClass = 155, ListClass = 155, Remark = "LCD EX B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "LCD EX B/T", DictValue = "156", OrderNum = 156, CssClass = 156, ListClass = 156, Remark = "LCD EX B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "LCD EX T", DictValue = "157", OrderNum = 157, CssClass = 157, ListClass = 157, Remark = "LCD EX T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "LCD T", DictValue = "157", OrderNum = 157, CssClass = 157, ListClass = 157, Remark = "LCD T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MADI B", DictValue = "158", OrderNum = 158, CssClass = 158, ListClass = 158, Remark = "MADI B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MADI B/T", DictValue = "161", OrderNum = 161, CssClass = 161, ListClass = 161, Remark = "MADI B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MADI T", DictValue = "162", OrderNum = 162, CssClass = 162, ListClass = 162, Remark = "MADI T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MAFAD A", DictValue = "163", OrderNum = 163, CssClass = 163, ListClass = 163, Remark = "MAFAD A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MAFAD B", DictValue = "164", OrderNum = 164, CssClass = 164, ListClass = 164, Remark = "MAFAD B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MA-FAD B", DictValue = "165", OrderNum = 165, CssClass = 165, ListClass = 165, Remark = "MA-FAD B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MAFAD B/T", DictValue = "166", OrderNum = 166, CssClass = 166, ListClass = 166, Remark = "MAFAD B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MAFAD T", DictValue = "166", OrderNum = 166, CssClass = 166, ListClass = 166, Remark = "MAFAD T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MA-FAD T", DictValue = "167", OrderNum = 167, CssClass = 167, ListClass = 167, Remark = "MA-FAD T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MAIN", DictValue = "168", OrderNum = 168, CssClass = 168, ListClass = 168, Remark = "MAIN" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MAIN A", DictValue = "171", OrderNum = 171, CssClass = 171, ListClass = 171, Remark = "MAIN A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MAIN ALT B", DictValue = "172", OrderNum = 172, CssClass = 172, ListClass = 172, Remark = "MAIN ALT B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MAIN ALT T", DictValue = "173", OrderNum = 173, CssClass = 173, ListClass = 173, Remark = "MAIN ALT T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MAIN B", DictValue = "174", OrderNum = 174, CssClass = 174, ListClass = 174, Remark = "MAIN B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MAIN B/T", DictValue = "175", OrderNum = 175, CssClass = 175, ListClass = 175, Remark = "MAIN B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MAIN T", DictValue = "175", OrderNum = 175, CssClass = 175, ListClass = 175, Remark = "MAIN T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MATHER B/T", DictValue = "176", OrderNum = 176, CssClass = 176, ListClass = 176, Remark = "MATHER B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "METER", DictValue = "179", OrderNum = 179, CssClass = 179, ListClass = 179, Remark = "METER" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "MIC", DictValue = "180", OrderNum = 180, CssClass = 180, ListClass = 180, Remark = "MIC" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "NAUB B", DictValue = "181", OrderNum = 181, CssClass = 181, ListClass = 181, Remark = "NAUB B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PANEL", DictValue = "182", OrderNum = 182, CssClass = 182, ListClass = 182, Remark = "PANEL" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PANEL A", DictValue = "183", OrderNum = 183, CssClass = 183, ListClass = 183, Remark = "PANEL A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PANEL B", DictValue = "184", OrderNum = 184, CssClass = 184, ListClass = 184, Remark = "PANEL B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PANEL B/T", DictValue = "185", OrderNum = 185, CssClass = 185, ListClass = 185, Remark = "PANEL B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PANEL L", DictValue = "186", OrderNum = 186, CssClass = 186, ListClass = 186, Remark = "PANEL L" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PANEL R", DictValue = "187", OrderNum = 187, CssClass = 187, ListClass = 187, Remark = "PANEL R" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PANEL T", DictValue = "188", OrderNum = 188, CssClass = 188, ListClass = 188, Remark = "PANEL T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PHONE", DictValue = "189", OrderNum = 189, CssClass = 189, ListClass = 189, Remark = "PHONE" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "POWER", DictValue = "190", OrderNum = 190, CssClass = 190, ListClass = 190, Remark = "POWER" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "POWER A", DictValue = "191", OrderNum = 191, CssClass = 191, ListClass = 191, Remark = "POWER A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "POWER B", DictValue = "192", OrderNum = 192, CssClass = 192, ListClass = 192, Remark = "POWER B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "POWER B/T", DictValue = "193", OrderNum = 193, CssClass = 193, ListClass = 193, Remark = "POWER B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "POWER T", DictValue = "194", OrderNum = 194, CssClass = 194, ListClass = 194, Remark = "POWER T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PRM B", DictValue = "195", OrderNum = 195, CssClass = 195, ListClass = 195, Remark = "PRM B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PRM B/T", DictValue = "196", OrderNum = 196, CssClass = 196, ListClass = 196, Remark = "PRM B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PRM T", DictValue = "197", OrderNum = 197, CssClass = 197, ListClass = 197, Remark = "PRM T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PSL", DictValue = "198", OrderNum = 198, CssClass = 198, ListClass = 198, Remark = "PSL" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PSL B", DictValue = "199", OrderNum = 199, CssClass = 199, ListClass = 199, Remark = "PSL B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PSL B/T", DictValue = "200", OrderNum = 200, CssClass = 200, ListClass = 200, Remark = "PSL B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PSL T", DictValue = "201", OrderNum = 201, CssClass = 201, ListClass = 201, Remark = "PSL T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PTST", DictValue = "202", OrderNum = 202, CssClass = 202, ListClass = 202, Remark = "PTST" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PTST B", DictValue = "203", OrderNum = 203, CssClass = 203, ListClass = 203, Remark = "PTST B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PTST B/T", DictValue = "204", OrderNum = 204, CssClass = 204, ListClass = 204, Remark = "PTST B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PTST T", DictValue = "205", OrderNum = 205, CssClass = 205, ListClass = 205, Remark = "PTST T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "PWRSUB", DictValue = "206", OrderNum = 206, CssClass = 206, ListClass = 206, Remark = "PWRSUB" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "REAR", DictValue = "207", OrderNum = 207, CssClass = 207, ListClass = 207, Remark = "REAR" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "REAR A", DictValue = "208", OrderNum = 208, CssClass = 208, ListClass = 208, Remark = "REAR A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "REAR B", DictValue = "209", OrderNum = 209, CssClass = 209, ListClass = 209, Remark = "REAR B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "REAR T", DictValue = "210", OrderNum = 210, CssClass = 210, ListClass = 210, Remark = "REAR T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "RELAY", DictValue = "211", OrderNum = 211, CssClass = 211, ListClass = 211, Remark = "RELAY" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "RFP A", DictValue = "212", OrderNum = 212, CssClass = 212, ListClass = 212, Remark = "RFP A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "RFP B", DictValue = "213", OrderNum = 213, CssClass = 213, ListClass = 213, Remark = "RFP B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "RFP B/T", DictValue = "214", OrderNum = 214, CssClass = 214, ListClass = 214, Remark = "RFP B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "RFP T", DictValue = "215", OrderNum = 215, CssClass = 215, ListClass = 215, Remark = "RFP T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "RMN B", DictValue = "216", OrderNum = 216, CssClass = 216, ListClass = 216, Remark = "RMN B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "RMN B/T", DictValue = "217", OrderNum = 217, CssClass = 217, ListClass = 217, Remark = "RMN B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "RMN T", DictValue = "218", OrderNum = 218, CssClass = 218, ListClass = 218, Remark = "RMN T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "RMT", DictValue = "219", OrderNum = 219, CssClass = 219, ListClass = 219, Remark = "RMT" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "RSB B", DictValue = "220", OrderNum = 220, CssClass = 220, ListClass = 220, Remark = "RSB B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "RSB B/T", DictValue = "221", OrderNum = 221, CssClass = 221, ListClass = 221, Remark = "RSB B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "RSB T", DictValue = "222", OrderNum = 222, CssClass = 222, ListClass = 222, Remark = "RSB T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SATA", DictValue = "223", OrderNum = 223, CssClass = 223, ListClass = 223, Remark = "SATA" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SBTY", DictValue = "224", OrderNum = 224, CssClass = 224, ListClass = 224, Remark = "SBTY" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SEQ", DictValue = "225", OrderNum = 225, CssClass = 225, ListClass = 225, Remark = "SEQ" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SLOT", DictValue = "226", OrderNum = 226, CssClass = 226, ListClass = 226, Remark = "SLOT" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SLOT A", DictValue = "227", OrderNum = 227, CssClass = 227, ListClass = 227, Remark = "SLOT A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SLOT B", DictValue = "228", OrderNum = 228, CssClass = 228, ListClass = 228, Remark = "SLOT B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SLOT B/T", DictValue = "229", OrderNum = 229, CssClass = 229, ListClass = 229, Remark = "SLOT B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SLOT T", DictValue = "230", OrderNum = 230, CssClass = 230, ListClass = 230, Remark = "SLOT T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SPL T", DictValue = "231", OrderNum = 231, CssClass = 231, ListClass = 231, Remark = "SPL T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "STBY", DictValue = "232", OrderNum = 232, CssClass = 232, ListClass = 232, Remark = "STBY" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "STS B", DictValue = "233", OrderNum = 233, CssClass = 233, ListClass = 233, Remark = "STS B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SWUSB", DictValue = "234", OrderNum = 234, CssClass = 234, ListClass = 234, Remark = "SWUSB" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SWUSB AKM B", DictValue = "235", OrderNum = 235, CssClass = 235, ListClass = 235, Remark = "SWUSB AKM B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SWUSB AKM B/T", DictValue = "236", OrderNum = 236, CssClass = 236, ListClass = 236, Remark = "SWUSB AKM B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SWUSB AKM T", DictValue = "237", OrderNum = 237, CssClass = 237, ListClass = 237, Remark = "SWUSB AKM T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SWUSB B", DictValue = "238", OrderNum = 238, CssClass = 238, ListClass = 238, Remark = "SWUSB B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SWUSB B/T", DictValue = "239", OrderNum = 239, CssClass = 239, ListClass = 239, Remark = "SWUSB B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SWUSB T", DictValue = "240", OrderNum = 240, CssClass = 240, ListClass = 240, Remark = "SWUSB T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SYS B", DictValue = "241", OrderNum = 241, CssClass = 241, ListClass = 241, Remark = "SYS B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "SYS T", DictValue = "242", OrderNum = 242, CssClass = 242, ListClass = 242, Remark = "SYS T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "TOP", DictValue = "243", OrderNum = 243, CssClass = 243, ListClass = 243, Remark = "TOP" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "USB B", DictValue = "244", OrderNum = 244, CssClass = 244, ListClass = 244, Remark = "USB B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "USB B/T", DictValue = "245", OrderNum = 245, CssClass = 245, ListClass = 245, Remark = "USB B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "USB T", DictValue = "245", OrderNum = 245, CssClass = 245, ListClass = 245, Remark = "USB T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "XLR", DictValue = "246", OrderNum = 246, CssClass = 246, ListClass = 246, Remark = "XLR" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "XLR A", DictValue = "249", OrderNum = 249, CssClass = 249, ListClass = 249, Remark = "XLR A" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "XLR B", DictValue = "250", OrderNum = 250, CssClass = 250, ListClass = 250, Remark = "XLR B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "XLR T", DictValue = "251", OrderNum = 251, CssClass = 251, ListClass = 251, Remark = "XLR T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "XLRIN B", DictValue = "252", OrderNum = 252, CssClass = 252, ListClass = 252, Remark = "XLRIN B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "XLRIN B/T", DictValue = "253", OrderNum = 253, CssClass = 253, ListClass = 253, Remark = "XLRIN B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "XLRIN T", DictValue = "254", OrderNum = 254, CssClass = 254, ListClass = 254, Remark = "XLRIN T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "XLRIO B", DictValue = "255", OrderNum = 255, CssClass = 255, ListClass = 255, Remark = "XLRIO B" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "XLRIO B/T", DictValue = "256", OrderNum = 256, CssClass = 256, ListClass = 256, Remark = "XLRIO B/T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "XLRIO T", DictValue = "257", OrderNum = 257, CssClass = 257, ListClass = 257, Remark = "XLRIO T" },
            new { DictTypeCode = "pcba_panel_category", DictLabel = "XLROUT", DictValue = "258", OrderNum = 258, CssClass = 258, ListClass = 258, Remark = "XLROUT" },
            // pcba_side_category - PCBA面别：B面、T面
            new { DictTypeCode = "pcba_side_category", DictLabel = "B面", DictValue = "B", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "B面" },
            new { DictTypeCode = "pcba_side_category", DictLabel = "T面", DictValue = "T", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "T面" },

            // pcba_function_category - PCBA功能类别
            new { DictTypeCode = "pcba_function_category", DictLabel = "A", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "A" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "ADOC", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "ADOC" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "ANA", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "ANA" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "AUDIO", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "AUDIO" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "B", DictValue = "5", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "B" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "BOTTOM", DictValue = "6", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "BOTTOM" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "BTICE", DictValue = "7", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "BTICE" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "C", DictValue = "8", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "C" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "DSPL", DictValue = "9", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "DSPL" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "ENC", DictValue = "10", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "ENC" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "FRONT", DictValue = "11", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "FRONT" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "INPUT", DictValue = "12", OrderNum = 12, CssClass = 12, ListClass = 12, Remark = "INPUT" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "IO", DictValue = "13", OrderNum = 13, CssClass = 13, ListClass = 13, Remark = "IO" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "JACK", DictValue = "14", OrderNum = 14, CssClass = 14, ListClass = 14, Remark = "JACK" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "L", DictValue = "15", OrderNum = 15, CssClass = 15, ListClass = 15, Remark = "L" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "LCD", DictValue = "16", OrderNum = 16, CssClass = 16, ListClass = 16, Remark = "LCD" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "MAIN", DictValue = "17", OrderNum = 17, CssClass = 17, ListClass = 17, Remark = "MAIN" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "PANEL", DictValue = "18", OrderNum = 18, CssClass = 18, ListClass = 18, Remark = "PANEL" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "POWER", DictValue = "19", OrderNum = 19, CssClass = 19, ListClass = 19, Remark = "POWER" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "REAR", DictValue = "20", OrderNum = 20, CssClass = 20, ListClass = 20, Remark = "REAR" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "RMN-1", DictValue = "21", OrderNum = 21, CssClass = 21, ListClass = 21, Remark = "RMN-1" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "SATA", DictValue = "22", OrderNum = 22, CssClass = 22, ListClass = 22, Remark = "SATA" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "SEQ", DictValue = "23", OrderNum = 23, CssClass = 23, ListClass = 23, Remark = "SEQ" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "SYS", DictValue = "24", OrderNum = 24, CssClass = 24, ListClass = 24, Remark = "SYS" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "TOP", DictValue = "25", OrderNum = 25, CssClass = 25, ListClass = 25, Remark = "TOP" },
            new { DictTypeCode = "pcba_function_category", DictLabel = "USB", DictValue = "26", OrderNum = 26, CssClass = 26, ListClass = 26, Remark = "USB" },

            // prod_stop_reason - 停线原因
            new { DictTypeCode = "prod_stop_reason", DictLabel = "切换停止时间", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "切换停止时间" },
            new { DictTypeCode = "prod_stop_reason", DictLabel = "周会", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "周会" },
            new { DictTypeCode = "prod_stop_reason", DictLabel = "其他", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "其他" },
            new { DictTypeCode = "prod_stop_reason", DictLabel = "欠料", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "欠料" },
            new { DictTypeCode = "prod_stop_reason", DictLabel = "停电", DictValue = "5", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "停电" },
            new { DictTypeCode = "prod_stop_reason", DictLabel = "班会", DictValue = "6", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "班会" },
            new { DictTypeCode = "prod_stop_reason", DictLabel = "切换机种", DictValue = "7", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "切换机种" },
            new { DictTypeCode = "prod_stop_reason", DictLabel = "早会", DictValue = "8", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "早会" },
            new { DictTypeCode = "prod_stop_reason", DictLabel = "组立", DictValue = "9", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "组立" },
            new { DictTypeCode = "prod_stop_reason", DictLabel = "学习", DictValue = "10", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "学习" },
            new { DictTypeCode = "prod_stop_reason", DictLabel = "仪设", DictValue = "11", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "仪设" },
            new { DictTypeCode = "prod_stop_reason", DictLabel = "清洁", DictValue = "12", OrderNum = 12, CssClass = 12, ListClass = 12, Remark = "清洁" },

            // prod_nonachievement_reason - 未达成原因
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "清机", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "清机" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "测试慢,测试修理机", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "测试慢,测试修理机" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "修理试机", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "修理试机" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "转机", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "转机" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "人员欠缺", DictValue = "5", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "人员欠缺" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "部品不良,欠料", DictValue = "6", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "部品不良,欠料" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "ST差异大", DictValue = "7", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "ST差异大" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "仪器设备,设置,调试,检查,故障,切换", DictValue = "8", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "仪器设备,设置,调试,检查,故障,切换" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "请假,旷工", DictValue = "9", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "请假,旷工" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "其他", DictValue = "10", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "其他" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "切换机种,仕向", DictValue = "11", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "切换机种,仕向" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "组立慢,加工多,工程多,下机慢,作业困难,升级慢", DictValue = "12", OrderNum = 12, CssClass = 12, ListClass = 12, Remark = "组立慢,加工多,工程多,下机慢,作业困难,升级慢" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "改修", DictValue = "13", OrderNum = 13, CssClass = 13, ListClass = 13, Remark = "改修" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "坏机多,不良多", DictValue = "14", OrderNum = 14, CssClass = 14, ListClass = 14, Remark = "坏机多,不良多" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "人员借调", DictValue = "15", OrderNum = 15, CssClass = 15, ListClass = 15, Remark = "人员借调" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "返工", DictValue = "16", OrderNum = 16, CssClass = 16, ListClass = 16, Remark = "返工" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "下机慢", DictValue = "17", OrderNum = 17, CssClass = 17, ListClass = 17, Remark = "下机慢" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "学习中,新人员学习,开会", DictValue = "18", OrderNum = 18, CssClass = 18, ListClass = 18, Remark = "学习中,新人员学习,开会" },
            new { DictTypeCode = "prod_nonachievement_reason", DictLabel = "正常", DictValue = "19", OrderNum = 19, CssClass = 19, ListClass = 19, Remark = "正常" },

            // prod_shift_category - 生产班别
            new { DictTypeCode = "prod_shift_category", DictLabel = "早", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "早" },
            new { DictTypeCode = "prod_shift_category", DictLabel = "中", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "中" },
            new { DictTypeCode = "prod_shift_category", DictLabel = "晚", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "晚" },
            new { DictTypeCode = "prod_shift_category", DictLabel = "白班", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "白班" },
            new { DictTypeCode = "prod_shift_category", DictLabel = "夜班", DictValue = "5", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "夜班" },

            // prod_assy_location - Assy个所（组立个所）
            new { DictTypeCode = "prod_assy_location", DictLabel = "自插", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "自插" },
            new { DictTypeCode = "prod_assy_location", DictLabel = "部品", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "部品" },
            new { DictTypeCode = "prod_assy_location", DictLabel = "设计", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "设计" },
            new { DictTypeCode = "prod_assy_location", DictLabel = "修正", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "修正" },
            new { DictTypeCode = "prod_assy_location", DictLabel = "加工", DictValue = "5", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "加工" },
            new { DictTypeCode = "prod_assy_location", DictLabel = "手插", DictValue = "6", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "手插" },
            new { DictTypeCode = "prod_assy_location", DictLabel = "组立", DictValue = "7", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "组立" },
            new { DictTypeCode = "prod_assy_location", DictLabel = "SMT", DictValue = "8", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "SMT" },
            new { DictTypeCode = "prod_assy_location", DictLabel = "其他", DictValue = "9", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "其他" },

            // prod_pcb_location - PCB个所
            new { DictTypeCode = "prod_pcb_location", DictLabel = "翘脚", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "翘脚" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "生锡", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "生锡" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "锡量过多", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "锡量过多" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "空焊", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "空焊" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "漏件", DictValue = "5", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "漏件" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "发黄", DictValue = "6", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "发黄" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "IC PIN 竖立", DictValue = "7", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "IC PIN 竖立" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "连锡", DictValue = "8", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "连锡" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "异物附着", DictValue = "9", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "异物附着" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "底下有部品", DictValue = "10", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "底下有部品" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "基板不良", DictValue = "11", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "基板不良" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "IC PIN 浮高", DictValue = "12", OrderNum = 12, CssClass = 12, ListClass = 12, Remark = "IC PIN 浮高" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "红胶不良", DictValue = "13", OrderNum = 13, CssClass = 13, ListClass = 13, Remark = "红胶不良" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "反面", DictValue = "14", OrderNum = 14, CssClass = 14, ListClass = 14, Remark = "反面" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "位置偏移", DictValue = "15", OrderNum = 15, CssClass = 15, ListClass = 15, Remark = "位置偏移" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "部品不良", DictValue = "16", OrderNum = 16, CssClass = 16, ListClass = 16, Remark = "部品不良" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "部品破损", DictValue = "17", OrderNum = 17, CssClass = 17, ListClass = 17, Remark = "部品破损" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "立碑", DictValue = "18", OrderNum = 18, CssClass = 18, ListClass = 18, Remark = "立碑" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "翻面", DictValue = "19", OrderNum = 19, CssClass = 19, ListClass = 19, Remark = "翻面" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "撞件", DictValue = "20", OrderNum = 20, CssClass = 20, ListClass = 20, Remark = "撞件" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "错料", DictValue = "21", OrderNum = 21, CssClass = 21, ListClass = 21, Remark = "错料" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "侧立", DictValue = "22", OrderNum = 22, CssClass = 22, ListClass = 22, Remark = "侧立" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "反向", DictValue = "23", OrderNum = 23, CssClass = 23, ListClass = 23, Remark = "反向" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "PCB不良", DictValue = "24", OrderNum = 24, CssClass = 24, ListClass = 24, Remark = "PCB不良" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "焊接不良", DictValue = "25", OrderNum = 25, CssClass = 25, ListClass = 25, Remark = "焊接不良" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "极性相违", DictValue = "26", OrderNum = 26, CssClass = 26, ListClass = 26, Remark = "极性相违" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "多件", DictValue = "27", OrderNum = 27, CssClass = 27, ListClass = 27, Remark = "多件" },
            new { DictTypeCode = "prod_pcb_location", DictLabel = "锡少", DictValue = "28", OrderNum = 28, CssClass = 28, ListClass = 28, Remark = "锡少" },

            // hr_political_status - 政治面貌（国家标准十三类，DictValue=0～12 与 int 存储一致）
            new { DictTypeCode = "hr_political_status", DictLabel = "群众", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "群众" },
            new { DictTypeCode = "hr_political_status", DictLabel = "共青团员", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "共青团员" },
            new { DictTypeCode = "hr_political_status", DictLabel = "中共党员", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "中共党员" },
            new { DictTypeCode = "hr_political_status", DictLabel = "中共预备党员", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "中共预备党员" },
            new { DictTypeCode = "hr_political_status", DictLabel = "民革党员", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "中国国民党革命委员会会员" },
            new { DictTypeCode = "hr_political_status", DictLabel = "民盟盟员", DictValue = "5", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "中国民主同盟盟员" },
            new { DictTypeCode = "hr_political_status", DictLabel = "民建会员", DictValue = "6", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "中国民主建国会会员" },
            new { DictTypeCode = "hr_political_status", DictLabel = "民进会员", DictValue = "7", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "中国民主促进会会员" },
            new { DictTypeCode = "hr_political_status", DictLabel = "农工党党员", DictValue = "8", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "中国农工民主党党员" },
            new { DictTypeCode = "hr_political_status", DictLabel = "致公党党员", DictValue = "9", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "中国致公党党员" },
            new { DictTypeCode = "hr_political_status", DictLabel = "九三学社社员", DictValue = "10", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "九三学社社员" },
            new { DictTypeCode = "hr_political_status", DictLabel = "台盟盟员", DictValue = "11", OrderNum = 12, CssClass = 12, ListClass = 12, Remark = "台湾民主自治同盟盟员" },
            new { DictTypeCode = "hr_political_status", DictLabel = "无党派民主人士", DictValue = "12", OrderNum = 13, CssClass = 13, ListClass = 13, Remark = "无党派民主人士" },

            // hr_marital_status - 婚姻状况：0=未婚，1=已婚，2=离异，3=丧偶
            new { DictTypeCode = "hr_marital_status", DictLabel = "未婚", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "未婚" },
            new { DictTypeCode = "hr_marital_status", DictLabel = "已婚", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "已婚" },
            new { DictTypeCode = "hr_marital_status", DictLabel = "离异", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "离异" },
            new { DictTypeCode = "hr_marital_status", DictLabel = "丧偶", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "丧偶" },

            // hr_native_place - 籍贯（中国省级行政区，GB/T 2260 省级代码）
            new { DictTypeCode = "hr_native_place", DictLabel = "北京市", DictValue = "110000", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "北京市" },
            new { DictTypeCode = "hr_native_place", DictLabel = "天津市", DictValue = "120000", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "天津市" },
            new { DictTypeCode = "hr_native_place", DictLabel = "河北省", DictValue = "130000", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "河北省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "山西省", DictValue = "140000", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "山西省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "内蒙古自治区", DictValue = "150000", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "内蒙古自治区" },
            new { DictTypeCode = "hr_native_place", DictLabel = "辽宁省", DictValue = "210000", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "辽宁省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "吉林省", DictValue = "220000", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "吉林省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "黑龙江省", DictValue = "230000", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "黑龙江省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "上海市", DictValue = "310000", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "上海市" },
            new { DictTypeCode = "hr_native_place", DictLabel = "江苏省", DictValue = "320000", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "江苏省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "浙江省", DictValue = "330000", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "浙江省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "安徽省", DictValue = "340000", OrderNum = 12, CssClass = 12, ListClass = 12, Remark = "安徽省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "福建省", DictValue = "350000", OrderNum = 13, CssClass = 13, ListClass = 13, Remark = "福建省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "江西省", DictValue = "360000", OrderNum = 14, CssClass = 14, ListClass = 14, Remark = "江西省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "山东省", DictValue = "370000", OrderNum = 15, CssClass = 15, ListClass = 15, Remark = "山东省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "河南省", DictValue = "410000", OrderNum = 16, CssClass = 16, ListClass = 16, Remark = "河南省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "湖北省", DictValue = "420000", OrderNum = 17, CssClass = 17, ListClass = 17, Remark = "湖北省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "湖南省", DictValue = "430000", OrderNum = 18, CssClass = 18, ListClass = 18, Remark = "湖南省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "广东省", DictValue = "440000", OrderNum = 19, CssClass = 19, ListClass = 19, Remark = "广东省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "广西壮族自治区", DictValue = "450000", OrderNum = 20, CssClass = 20, ListClass = 20, Remark = "广西壮族自治区" },
            new { DictTypeCode = "hr_native_place", DictLabel = "海南省", DictValue = "460000", OrderNum = 21, CssClass = 21, ListClass = 21, Remark = "海南省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "重庆市", DictValue = "500000", OrderNum = 22, CssClass = 22, ListClass = 22, Remark = "重庆市" },
            new { DictTypeCode = "hr_native_place", DictLabel = "四川省", DictValue = "510000", OrderNum = 23, CssClass = 23, ListClass = 23, Remark = "四川省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "贵州省", DictValue = "520000", OrderNum = 24, CssClass = 24, ListClass = 24, Remark = "贵州省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "云南省", DictValue = "530000", OrderNum = 25, CssClass = 25, ListClass = 25, Remark = "云南省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "西藏自治区", DictValue = "540000", OrderNum = 26, CssClass = 26, ListClass = 26, Remark = "西藏自治区" },
            new { DictTypeCode = "hr_native_place", DictLabel = "陕西省", DictValue = "610000", OrderNum = 27, CssClass = 27, ListClass = 27, Remark = "陕西省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "甘肃省", DictValue = "620000", OrderNum = 28, CssClass = 28, ListClass = 28, Remark = "甘肃省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "青海省", DictValue = "630000", OrderNum = 29, CssClass = 29, ListClass = 29, Remark = "青海省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "宁夏回族自治区", DictValue = "640000", OrderNum = 30, CssClass = 30, ListClass = 30, Remark = "宁夏回族自治区" },
            new { DictTypeCode = "hr_native_place", DictLabel = "新疆维吾尔自治区", DictValue = "650000", OrderNum = 31, CssClass = 31, ListClass = 31, Remark = "新疆维吾尔自治区" },
            new { DictTypeCode = "hr_native_place", DictLabel = "台湾省", DictValue = "710000", OrderNum = 32, CssClass = 32, ListClass = 32, Remark = "台湾省" },
            new { DictTypeCode = "hr_native_place", DictLabel = "香港特别行政区", DictValue = "810000", OrderNum = 33, CssClass = 33, ListClass = 33, Remark = "香港特别行政区" },
            new { DictTypeCode = "hr_native_place", DictLabel = "澳门特别行政区", DictValue = "820000", OrderNum = 34, CssClass = 34, ListClass = 34, Remark = "澳门特别行政区" },

            // hr_employee_status - 员工状态：0=在职，1=离职，2=停薪留职，3=退休
            new { DictTypeCode = "hr_employee_status", DictLabel = "在职", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "在职" },
            new { DictTypeCode = "hr_employee_status", DictLabel = "离职", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "离职" },
            new { DictTypeCode = "hr_employee_status", DictLabel = "停薪留职", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "停薪留职" },
            new { DictTypeCode = "hr_employee_status", DictLabel = "退休", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "退休" },

            // hr_ethnic_group - 民族（56 个民族，国家标准排序，DictValue=1～56）
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "汉族", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "汉族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "蒙古族", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "蒙古族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "回族", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "回族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "藏族", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "藏族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "维吾尔族", DictValue = "5", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "维吾尔族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "苗族", DictValue = "6", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "苗族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "彝族", DictValue = "7", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "彝族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "壮族", DictValue = "8", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "壮族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "布依族", DictValue = "9", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "布依族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "朝鲜族", DictValue = "10", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "朝鲜族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "满族", DictValue = "11", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "满族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "侗族", DictValue = "12", OrderNum = 12, CssClass = 12, ListClass = 12, Remark = "侗族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "瑶族", DictValue = "13", OrderNum = 13, CssClass = 13, ListClass = 13, Remark = "瑶族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "白族", DictValue = "14", OrderNum = 14, CssClass = 14, ListClass = 14, Remark = "白族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "土家族", DictValue = "15", OrderNum = 15, CssClass = 15, ListClass = 15, Remark = "土家族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "哈尼族", DictValue = "16", OrderNum = 16, CssClass = 16, ListClass = 16, Remark = "哈尼族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "哈萨克族", DictValue = "17", OrderNum = 17, CssClass = 17, ListClass = 17, Remark = "哈萨克族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "傣族", DictValue = "18", OrderNum = 18, CssClass = 18, ListClass = 18, Remark = "傣族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "黎族", DictValue = "19", OrderNum = 19, CssClass = 19, ListClass = 19, Remark = "黎族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "傈僳族", DictValue = "20", OrderNum = 20, CssClass = 20, ListClass = 20, Remark = "傈僳族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "佤族", DictValue = "21", OrderNum = 21, CssClass = 21, ListClass = 21, Remark = "佤族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "畲族", DictValue = "22", OrderNum = 22, CssClass = 22, ListClass = 22, Remark = "畲族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "高山族", DictValue = "23", OrderNum = 23, CssClass = 23, ListClass = 23, Remark = "高山族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "拉祜族", DictValue = "24", OrderNum = 24, CssClass = 24, ListClass = 24, Remark = "拉祜族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "水族", DictValue = "25", OrderNum = 25, CssClass = 25, ListClass = 25, Remark = "水族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "东乡族", DictValue = "26", OrderNum = 26, CssClass = 26, ListClass = 26, Remark = "东乡族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "纳西族", DictValue = "27", OrderNum = 27, CssClass = 27, ListClass = 27, Remark = "纳西族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "景颇族", DictValue = "28", OrderNum = 28, CssClass = 28, ListClass = 28, Remark = "景颇族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "柯尔克孜族", DictValue = "29", OrderNum = 29, CssClass = 29, ListClass = 29, Remark = "柯尔克孜族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "土族", DictValue = "30", OrderNum = 30, CssClass = 30, ListClass = 30, Remark = "土族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "达斡尔族", DictValue = "31", OrderNum = 31, CssClass = 31, ListClass = 31, Remark = "达斡尔族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "仫佬族", DictValue = "32", OrderNum = 32, CssClass = 32, ListClass = 32, Remark = "仫佬族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "羌族", DictValue = "33", OrderNum = 33, CssClass = 33, ListClass = 33, Remark = "羌族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "布朗族", DictValue = "34", OrderNum = 34, CssClass = 34, ListClass = 34, Remark = "布朗族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "撒拉族", DictValue = "35", OrderNum = 35, CssClass = 35, ListClass = 35, Remark = "撒拉族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "毛南族", DictValue = "36", OrderNum = 36, CssClass = 36, ListClass = 36, Remark = "毛南族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "仡佬族", DictValue = "37", OrderNum = 37, CssClass = 37, ListClass = 37, Remark = "仡佬族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "锡伯族", DictValue = "38", OrderNum = 38, CssClass = 38, ListClass = 38, Remark = "锡伯族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "阿昌族", DictValue = "39", OrderNum = 39, CssClass = 39, ListClass = 39, Remark = "阿昌族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "普米族", DictValue = "40", OrderNum = 40, CssClass = 40, ListClass = 40, Remark = "普米族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "塔吉克族", DictValue = "41", OrderNum = 41, CssClass = 41, ListClass = 41, Remark = "塔吉克族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "怒族", DictValue = "42", OrderNum = 42, CssClass = 42, ListClass = 42, Remark = "怒族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "乌孜别克族", DictValue = "43", OrderNum = 43, CssClass = 43, ListClass = 43, Remark = "乌孜别克族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "俄罗斯族", DictValue = "44", OrderNum = 44, CssClass = 44, ListClass = 44, Remark = "俄罗斯族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "鄂温克族", DictValue = "45", OrderNum = 45, CssClass = 45, ListClass = 45, Remark = "鄂温克族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "德昂族", DictValue = "46", OrderNum = 46, CssClass = 46, ListClass = 46, Remark = "德昂族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "保安族", DictValue = "47", OrderNum = 47, CssClass = 47, ListClass = 47, Remark = "保安族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "裕固族", DictValue = "48", OrderNum = 48, CssClass = 48, ListClass = 48, Remark = "裕固族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "京族", DictValue = "49", OrderNum = 49, CssClass = 49, ListClass = 49, Remark = "京族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "塔塔尔族", DictValue = "50", OrderNum = 50, CssClass = 50, ListClass = 50, Remark = "塔塔尔族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "独龙族", DictValue = "51", OrderNum = 51, CssClass = 51, ListClass = 51, Remark = "独龙族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "鄂伦春族", DictValue = "52", OrderNum = 52, CssClass = 52, ListClass = 52, Remark = "鄂伦春族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "赫哲族", DictValue = "53", OrderNum = 53, CssClass = 53, ListClass = 53, Remark = "赫哲族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "门巴族", DictValue = "54", OrderNum = 54, CssClass = 54, ListClass = 54, Remark = "门巴族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "珞巴族", DictValue = "55", OrderNum = 55, CssClass = 55, ListClass = 55, Remark = "珞巴族" },
            new { DictTypeCode = "hr_ethnic_group", DictLabel = "基诺族", DictValue = "56", OrderNum = 56, CssClass = 56, ListClass = 56, Remark = "基诺族" },

            // hr_transfer_type - 调动类型：0=转岗，1=调岗
            new { DictTypeCode = "hr_transfer_type", DictLabel = "转岗", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "转岗" },
            new { DictTypeCode = "hr_transfer_type", DictLabel = "调岗", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "调岗" },

            // hr_transfer_status - 调动状态：0=草稿，1=审批中，2=已通过，3=已驳回，4=已撤回
            new { DictTypeCode = "hr_transfer_status", DictLabel = "草稿", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "草稿" },
            new { DictTypeCode = "hr_transfer_status", DictLabel = "审批中", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "审批中" },
            new { DictTypeCode = "hr_transfer_status", DictLabel = "已通过", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "已通过" },
            new { DictTypeCode = "hr_transfer_status", DictLabel = "已驳回", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "已驳回" },
            new { DictTypeCode = "hr_transfer_status", DictLabel = "已撤回", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "已撤回" },

            // hr_holiday_type - 假日类型：0=法定，1=调休，2=公司（与 TaktHoliday.holiday_type 一致）
            new { DictTypeCode = "hr_holiday_type", DictLabel = "法定", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "法定节假日" },
            new { DictTypeCode = "hr_holiday_type", DictLabel = "调休", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "调休" },
            new { DictTypeCode = "hr_holiday_type", DictLabel = "公司", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "公司假日" },

            // hr_holiday_is_working_day - 是否工作日：0=非工作日，1=工作日，2=半天等（与 TaktHoliday.is_working_day 一致）
            new { DictTypeCode = "hr_holiday_is_working_day", DictLabel = "非工作日", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "非工作日" },
            new { DictTypeCode = "hr_holiday_is_working_day", DictLabel = "工作日", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "工作日" },
            new { DictTypeCode = "hr_holiday_is_working_day", DictLabel = "半天等", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "半天等" },

            // hr_delegate_mode - 人事代理模式（与 TaktDeptDelegate / TaktPostDelegate / TaktEmployeeDelegate.delegate_mode 一致）
            new { DictTypeCode = "hr_delegate_mode", DictLabel = "直接员工", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "指定具体员工作为代理人" },
            new { DictTypeCode = "hr_delegate_mode", DictLabel = "部门规则", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "按引用部门解析代理人" },
            new { DictTypeCode = "hr_delegate_mode", DictLabel = "岗位规则", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "按引用岗位解析代理人" },

            // hr_schedule_type - 排班类别：0=部门，1=人员（与 TaktShiftSchedule.schedule_type 一致）
            new { DictTypeCode = "hr_schedule_type", DictLabel = "部门", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "按部门排班" },
            new { DictTypeCode = "hr_schedule_type", DictLabel = "人员", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "按人员排班" },

            // hr_overtime_type - 加班类型：0=工作日加班，1=休息日加班，2=法定节假日加班（与 TaktOvertime.overtime_type 一致）
            new { DictTypeCode = "hr_overtime_type", DictLabel = "工作日加班", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "工作日加班" },
            new { DictTypeCode = "hr_overtime_type", DictLabel = "休息日加班", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "休息日加班" },
            new { DictTypeCode = "hr_overtime_type", DictLabel = "法定节假日加班", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "法定节假日加班" },

            // hr_leave_status - 请假状态：0=草稿，1=审批中，2=已通过，3=已驳回，4=已撤回（与 TaktLeave.leave_status 一致）
            new { DictTypeCode = "hr_leave_status", DictLabel = "草稿", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "草稿" },
            new { DictTypeCode = "hr_leave_status", DictLabel = "审批中", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "审批中" },
            new { DictTypeCode = "hr_leave_status", DictLabel = "已通过", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "已通过" },
            new { DictTypeCode = "hr_leave_status", DictLabel = "已驳回", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "已驳回" },
            new { DictTypeCode = "hr_leave_status", DictLabel = "已撤回", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "已撤回" },

            // hr_overtime_status - 与 TaktOvertime.overtime_status 一致
            new { DictTypeCode = "hr_overtime_status", DictLabel = "草稿", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "草稿" },
            new { DictTypeCode = "hr_overtime_status", DictLabel = "已提交", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "已提交" },
            new { DictTypeCode = "hr_overtime_status", DictLabel = "已通过", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "已通过" },
            new { DictTypeCode = "hr_overtime_status", DictLabel = "已驳回", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "已驳回" },

            // hr_attendance_result_status - 与 TaktAttendanceResult.attendance_status 一致
            new { DictTypeCode = "hr_attendance_result_status", DictLabel = "正常", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "正常" },
            new { DictTypeCode = "hr_attendance_result_status", DictLabel = "迟到", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "迟到" },
            new { DictTypeCode = "hr_attendance_result_status", DictLabel = "早退", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "早退" },
            new { DictTypeCode = "hr_attendance_result_status", DictLabel = "缺卡", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "缺卡" },
            new { DictTypeCode = "hr_attendance_result_status", DictLabel = "旷工", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "旷工" },
            new { DictTypeCode = "hr_attendance_result_status", DictLabel = "加班", DictValue = "5", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "加班" },

            // hr_attendance_correction_approval - 与 TaktAttendanceCorrection.approval_status 一致
            new { DictTypeCode = "hr_attendance_correction_approval", DictLabel = "草稿", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "草稿" },
            new { DictTypeCode = "hr_attendance_correction_approval", DictLabel = "待审", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "待审" },
            new { DictTypeCode = "hr_attendance_correction_approval", DictLabel = "已通过", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "已通过" },
            new { DictTypeCode = "hr_attendance_correction_approval", DictLabel = "已驳回", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "已驳回" },

            // hr_attendance_device_status - 与 TaktAttendanceDevice.device_status 一致
            new { DictTypeCode = "hr_attendance_device_status", DictLabel = "停用", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "停用" },
            new { DictTypeCode = "hr_attendance_device_status", DictLabel = "正常", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "正常" },
            new { DictTypeCode = "hr_attendance_device_status", DictLabel = "故障", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "故障" },

            // hr_attendance_device_brand - 设备品牌（与 SDK 路由值一致）
            new { DictTypeCode = "hr_attendance_device_brand", DictLabel = "海康威视", DictValue = "Hikvision", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "海康威视" },
            new { DictTypeCode = "hr_attendance_device_brand", DictLabel = "得力", DictValue = "Deli", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "得力" },
            new { DictTypeCode = "hr_attendance_device_brand", DictLabel = "中控", DictValue = "ZKTeco", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "中控" },

            // hr_attendance_correction_kind - 与 TaktAttendanceCorrection.correction_kind 一致
            new { DictTypeCode = "hr_attendance_correction_kind", DictLabel = "上班", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "上班补卡" },
            new { DictTypeCode = "hr_attendance_correction_kind", DictLabel = "下班", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "下班补卡" },

            // hr_attendance_exception_type - 与 TaktAttendanceException.exception_type 一致
            new { DictTypeCode = "hr_attendance_exception_type", DictLabel = "上班缺卡", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "上班缺卡" },
            new { DictTypeCode = "hr_attendance_exception_type", DictLabel = "下班缺卡", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "下班缺卡" },
            new { DictTypeCode = "hr_attendance_exception_type", DictLabel = "迟到", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "迟到" },
            new { DictTypeCode = "hr_attendance_exception_type", DictLabel = "早退", DictValue = "4", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "早退" },
            new { DictTypeCode = "hr_attendance_exception_type", DictLabel = "旷工", DictValue = "5", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "旷工" },
            new { DictTypeCode = "hr_attendance_exception_type", DictLabel = "其他", DictValue = "9", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "其他" },

            // hr_attendance_exception_handle_status - 与 TaktAttendanceException.handle_status 一致
            new { DictTypeCode = "hr_attendance_exception_handle_status", DictLabel = "待处理", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "待处理" },
            new { DictTypeCode = "hr_attendance_exception_handle_status", DictLabel = "已处理", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "已处理" },
            new { DictTypeCode = "hr_attendance_exception_handle_status", DictLabel = "已忽略", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "已忽略" },

            // hr_attendance_punch_type - 与 TaktAttendancePunch.punch_type 一致
            new { DictTypeCode = "hr_attendance_punch_type", DictLabel = "上班", DictValue = "1", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "上班" },
            new { DictTypeCode = "hr_attendance_punch_type", DictLabel = "下班", DictValue = "2", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "下班" },
            new { DictTypeCode = "hr_attendance_punch_type", DictLabel = "外勤", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "外勤" },

            // hr_attendance_punch_source - 与 TaktAttendancePunch.punch_source 一致
            new { DictTypeCode = "hr_attendance_punch_source", DictLabel = "后台录入", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "后台录入" },
            new { DictTypeCode = "hr_attendance_punch_source", DictLabel = "移动端", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "移动端" },
            new { DictTypeCode = "hr_attendance_punch_source", DictLabel = "导入", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "导入" },

            // hr_attendance_verify_mode - 与 TaktAttendanceSource.verify_mode 一致
            new { DictTypeCode = "hr_attendance_verify_mode", DictLabel = "未知", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "未知" },
            new { DictTypeCode = "hr_attendance_verify_mode", DictLabel = "指纹", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "指纹" },
            new { DictTypeCode = "hr_attendance_verify_mode", DictLabel = "人脸", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "人脸" },
            new { DictTypeCode = "hr_attendance_verify_mode", DictLabel = "密码", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "密码" },
            new { DictTypeCode = "hr_attendance_verify_mode", DictLabel = "卡", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "卡" }
        }
        .Select(item => new
        {
            item.DictTypeCode,
            item.DictLabel,
            item.DictValue,
            DictL10nKey = $"dict.{item.DictTypeCode.ToLower()}.{item.DictValue.ToLower()}",
            item.OrderNum,
            item.CssClass,
            item.ListClass,
            item.Remark
        })
        .ToArray();

        // 初始化每个字典数据项
        foreach (var dictData in dictDataList)
        {
            // 检查字典类型是否存在
            var dictType = await dictTypeRepository.GetAsync(dt => dt.DictTypeCode == dictData.DictTypeCode);
            if (dictType == null)
            {
                continue;
            }

            var existing = await dictDataRepository.GetAsync(dd => dd.DictTypeCode == dictData.DictTypeCode && dd.DictLabel == dictData.DictLabel);

            if (existing == null)
            {
                // 不存在则插入
                var newDictData = new TaktDictData
                {
                    DictTypeId = dictType.Id, // 设置字典类型ID（外键）
                    DictTypeCode = dictData.DictTypeCode,
                    DictLabel = dictData.DictLabel,
                    DictValue = dictData.DictValue,
                    DictL10nKey = dictData.DictL10nKey, // 设置本地化键
                    OrderNum = dictData.OrderNum,
                    CssClass = dictData.CssClass,
                    ListClass = dictData.ListClass,
                    ExtLabel = dictData.Remark, // 使用ExtLabel存储备注
                    IsDeleted = 0
                };
                await dictDataRepository.CreateAsync(newDictData);
                insertCount++;
            }
            else
            {
                // 存在则更新
                existing.DictTypeId = dictType.Id; // 确保DictTypeId正确（外键）
                existing.DictValue = dictData.DictValue;
                existing.DictL10nKey = dictData.DictL10nKey; // 更新本地化键
                existing.OrderNum = dictData.OrderNum;
                existing.CssClass = dictData.CssClass;
                existing.ListClass = dictData.ListClass;
                existing.ExtLabel = dictData.Remark;
                await dictDataRepository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
