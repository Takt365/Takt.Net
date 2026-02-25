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
using Takt.Domain.Entities.Routine.Dict;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

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
            // sys_normal_disable - 用户状态：0=启用，1=禁用，3=锁定
            new { DictTypeCode = "sys_normal_disable", DictLabel = "启用", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "启用状态" },
            new { DictTypeCode = "sys_normal_disable", DictLabel = "禁用", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "禁用状态" },
            new { DictTypeCode = "sys_normal_disable", DictLabel = "锁定", DictValue = "3", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "锁定状态" },
            
            // sys_yes_no - 是否：0=是/启用，1=否/禁用
            new { DictTypeCode = "sys_yes_no", DictLabel = "是", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "是" },
            new { DictTypeCode = "sys_yes_no", DictLabel = "否", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "否" },
            
            // sys_is_default - 是否默认：0=是/默认，1=否/非默认
            new { DictTypeCode = "sys_is_default", DictLabel = "是", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "是默认" },
            new { DictTypeCode = "sys_is_default", DictLabel = "否", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "非默认" },
            
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
            
            // sys_form_type - 表单类型：0=动态表单，1=静态表单
            new { DictTypeCode = "sys_form_type", DictLabel = "动态表单", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "动态表单" },
            new { DictTypeCode = "sys_form_type", DictLabel = "静态表单", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "静态表单" },
            
            // sys_priority - 优先级：0=低，1=中，2=高，3=紧急
            new { DictTypeCode = "sys_priority", DictLabel = "低", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "低" },
            new { DictTypeCode = "sys_priority", DictLabel = "中", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "中" },
            new { DictTypeCode = "sys_priority", DictLabel = "高", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "高" },
            new { DictTypeCode = "sys_priority", DictLabel = "紧急", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "紧急" },
            
            // sys_flow_category - 流程分类：0=通用流程，1=业务流程，2=系统流程
            new { DictTypeCode = "sys_flow_category", DictLabel = "通用流程", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "通用流程" },
            new { DictTypeCode = "sys_flow_category", DictLabel = "业务流程", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "业务流程" },
            new { DictTypeCode = "sys_flow_category", DictLabel = "系统流程", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "系统流程" },
            
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
            
            // sys_gen_function - 生成功能（DictValue 英文，与后端 ParseGenFunctionKeys 英文键一致；DictLabel 中文用于界面展示）
            new { DictTypeCode = "sys_gen_function", DictLabel = "查询", DictValue = "Query", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "查询" },
            new { DictTypeCode = "sys_gen_function", DictLabel = "新增", DictValue = "Create", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "新增" },
            new { DictTypeCode = "sys_gen_function", DictLabel = "更新", DictValue = "Update", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "更新" },
            new { DictTypeCode = "sys_gen_function", DictLabel = "删除", DictValue = "Delete", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "删除" },
            new { DictTypeCode = "sys_gen_function", DictLabel = "模板", DictValue = "Template", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "模板" },
            new { DictTypeCode = "sys_gen_function", DictLabel = "导入", DictValue = "Import", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "导入" },
            new { DictTypeCode = "sys_gen_function", DictLabel = "导出", DictValue = "Export", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "导出" },
            
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

            // hr_holiday_type - 假日类型：0=法定，1=调休，2=公司
            new { DictTypeCode = "hr_holiday_type", DictLabel = "法定", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "法定假日" },
            new { DictTypeCode = "hr_holiday_type", DictLabel = "调休", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "调休（补班/补休）" },
            new { DictTypeCode = "hr_holiday_type", DictLabel = "公司", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "公司假日" },

            // hr_holiday_is_working_day - 是否工作日：0=非工作日，1=工作日，2=半天
            new { DictTypeCode = "hr_holiday_is_working_day", DictLabel = "非工作日", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "非工作日" },
            new { DictTypeCode = "hr_holiday_is_working_day", DictLabel = "工作日", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "工作日（调休补班）" },
            new { DictTypeCode = "hr_holiday_is_working_day", DictLabel = "半天", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "半天" },

            // hr_holiday_greeting - 假日问候语（按 TaktHolidaySeedData 实际使用的问候语）
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "新年快乐", DictValue = "新年快乐", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "元旦/春节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "清明时节", DictValue = "清明时节", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "清明节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "劳动光荣", DictValue = "劳动光荣", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "劳动节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "端午安康", DictValue = "端午安康", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "端午节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "阖家团圆", DictValue = "阖家团圆", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "中秋节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "祝祖国繁荣昌盛", DictValue = "祝祖国繁荣昌盛", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "国庆节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "六一快乐", DictValue = "六一快乐", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "儿童节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "教师节快乐", DictValue = "教师节快乐", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "教师节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "妇女节快乐", DictValue = "妇女节快乐", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "妇女节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "绿色家园", DictValue = "绿色家园", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "植树节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "青年节快乐", DictValue = "青年节快乐", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "青年节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "致敬白衣天使", DictValue = "致敬白衣天使", OrderNum = 12, CssClass = 12, ListClass = 12, Remark = "护士节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "致敬军人", DictValue = "致敬军人", OrderNum = 13, CssClass = 13, ListClass = 13, Remark = "建军节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "元宵节快乐", DictValue = "元宵节快乐", OrderNum = 14, CssClass = 14, ListClass = 14, Remark = "元宵节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "敬老爱老", DictValue = "敬老爱老", OrderNum = 15, CssClass = 15, ListClass = 15, Remark = "重阳节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "成人快乐", DictValue = "成人快乐", OrderNum = 16, CssClass = 16, ListClass = 16, Remark = "日本成人日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "建国纪念日", DictValue = "建国纪念日", OrderNum = 17, CssClass = 17, ListClass = 17, Remark = "日本建国纪念日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "天皇诞生日", DictValue = "天皇诞生日", OrderNum = 18, CssClass = 18, ListClass = 18, Remark = "日本天皇诞生日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "春分", DictValue = "春分", OrderNum = 19, CssClass = 19, ListClass = 19, Remark = "春分日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "昭和之日", DictValue = "昭和之日", OrderNum = 20, CssClass = 20, ListClass = 20, Remark = "日本昭和之日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "宪法纪念日", DictValue = "宪法纪念日", OrderNum = 21, CssClass = 21, ListClass = 21, Remark = "日本宪法纪念日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "儿童节快乐", DictValue = "儿童节快乐", OrderNum = 22, CssClass = 22, ListClass = 22, Remark = "日本儿童节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "海之日", DictValue = "海之日", OrderNum = 23, CssClass = 23, ListClass = 23, Remark = "日本海之日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "山之日", DictValue = "山之日", OrderNum = 24, CssClass = 24, ListClass = 24, Remark = "日本山之日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "秋分", DictValue = "秋分", OrderNum = 25, CssClass = 25, ListClass = 25, Remark = "秋分日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "体育之日", DictValue = "体育之日", OrderNum = 26, CssClass = 26, ListClass = 26, Remark = "日本体育之日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "文化日", DictValue = "文化日", OrderNum = 27, CssClass = 27, ListClass = 27, Remark = "日本文化日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "勤劳感谢日", DictValue = "勤劳感谢日", OrderNum = 28, CssClass = 28, ListClass = 28, Remark = "日本勤劳感谢日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "马丁路德金纪念日", DictValue = "马丁路德金纪念日", OrderNum = 29, CssClass = 29, ListClass = 29, Remark = "美国马丁·路德·金纪念日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "总统日", DictValue = "总统日", OrderNum = 30, CssClass = 30, ListClass = 30, Remark = "美国总统日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "铭记英雄", DictValue = "铭记英雄", OrderNum = 31, CssClass = 31, ListClass = 31, Remark = "美国阵亡将士纪念日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "六月节", DictValue = "六月节", OrderNum = 32, CssClass = 32, ListClass = 32, Remark = "美国六月节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "独立日快乐", DictValue = "独立日快乐", OrderNum = 33, CssClass = 33, ListClass = 33, Remark = "美国独立日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "哥伦布日", DictValue = "哥伦布日", OrderNum = 34, CssClass = 34, ListClass = 34, Remark = "美国哥伦布日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "感恩节快乐", DictValue = "感恩节快乐", OrderNum = 35, CssClass = 35, ListClass = 35, Remark = "美国感恩节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "圣诞节快乐", DictValue = "圣诞节快乐", OrderNum = 36, CssClass = 36, ListClass = 36, Remark = "圣诞节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "三一节", DictValue = "三一节", OrderNum = 37, CssClass = 37, ListClass = 37, Remark = "韩国三一节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "佛诞日", DictValue = "佛诞日", OrderNum = 38, CssClass = 38, ListClass = 38, Remark = "韩国佛诞日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "光复节", DictValue = "光复节", OrderNum = 39, CssClass = 39, ListClass = 39, Remark = "韩国光复节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "开天节", DictValue = "开天节", OrderNum = 40, CssClass = 40, ListClass = 40, Remark = "韩国开天节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "韩文日", DictValue = "韩文日", OrderNum = 41, CssClass = 41, ListClass = 41, Remark = "韩国韩文日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "耶稣受难节", DictValue = "耶稣受难节", OrderNum = 42, CssClass = 42, ListClass = 42, Remark = "香港耶稣受难节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "复活节快乐", DictValue = "复活节快乐", OrderNum = 43, CssClass = 43, ListClass = 43, Remark = "复活节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "香港特别行政区成立纪念日", DictValue = "香港特别行政区成立纪念日", OrderNum = 44, CssClass = 44, ListClass = 44, Remark = "香港回归纪念日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "东正教圣诞节快乐", DictValue = "东正教圣诞节快乐", OrderNum = 45, CssClass = 45, ListClass = 45, Remark = "俄罗斯东正教圣诞节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "俄罗斯日", DictValue = "俄罗斯日", OrderNum = 46, CssClass = 46, ListClass = 46, Remark = "俄罗斯国庆节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "人民团结日", DictValue = "人民团结日", OrderNum = 47, CssClass = 47, ListClass = 47, Remark = "俄罗斯人民团结日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "耶稣升天节", DictValue = "耶稣升天节", OrderNum = 48, CssClass = 48, ListClass = 48, Remark = "复活节后第40天" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "圣灵降临节", DictValue = "圣灵降临节", OrderNum = 49, CssClass = 49, ListClass = 49, Remark = "复活节后第50天" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "巴士底日快乐", DictValue = "巴士底日快乐", OrderNum = 50, CssClass = 50, ListClass = 50, Remark = "法国国庆日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "圣母升天节", DictValue = "圣母升天节", OrderNum = 51, CssClass = 51, ListClass = 51, Remark = "天主教圣母升天节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "万圣节", DictValue = "万圣节", OrderNum = 52, CssClass = 52, ListClass = 52, Remark = "All Saints' Day" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "铭记和平", DictValue = "铭记和平", OrderNum = 53, CssClass = 53, ListClass = 53, Remark = "一战停战日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "三王节快乐", DictValue = "三王节快乐", OrderNum = 54, CssClass = 54, ListClass = 54, Remark = "西班牙主显节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "西班牙国庆日", DictValue = "西班牙国庆日", OrderNum = 55, CssClass = 55, ListClass = 55, Remark = "西班牙国庆节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "宪法日", DictValue = "宪法日", OrderNum = 56, CssClass = 56, ListClass = 56, Remark = "西班牙宪法日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "圣母无染原罪节", DictValue = "圣母无染原罪节", OrderNum = 57, CssClass = 57, ListClass = 57, Remark = "天主教圣母无染原罪节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "斋月吉庆", DictValue = "斋月吉庆", OrderNum = 58, CssClass = 58, ListClass = 58, Remark = "伊斯兰斋月" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "开斋节快乐", DictValue = "开斋节快乐", OrderNum = 59, CssClass = 59, ListClass = 59, Remark = "伊斯兰开斋节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "阿拉法特日", DictValue = "阿拉法特日", OrderNum = 60, CssClass = 60, ListClass = 60, Remark = "朝觐阿拉法特日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "宰牲节快乐", DictValue = "宰牲节快乐", OrderNum = 61, CssClass = 61, ListClass = 61, Remark = "伊斯兰宰牲节/古尔邦节" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "伊斯兰新年快乐", DictValue = "伊斯兰新年快乐", OrderNum = 62, CssClass = 62, ListClass = 62, Remark = "伊斯兰教历新年" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "阿舒拉节", DictValue = "阿舒拉节", OrderNum = 63, CssClass = 63, ListClass = 63, Remark = "伊斯兰教历1月10日" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "先知诞辰", DictValue = "先知诞辰", OrderNum = 64, CssClass = 64, ListClass = 64, Remark = "穆罕默德诞辰" },
            new { DictTypeCode = "hr_holiday_greeting", DictLabel = "沙特国庆日", DictValue = "沙特国庆日", OrderNum = 65, CssClass = 65, ListClass = 65, Remark = "沙特阿拉伯建国纪念日" },

            // hr_holiday_theme_color - 假日主题色（对应 themeColorMap key）
            new { DictTypeCode = "hr_holiday_theme_color", DictLabel = "马尔斯绿", DictValue = "green", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "green" },
            new { DictTypeCode = "hr_holiday_theme_color", DictLabel = "蒂芙尼蓝", DictValue = "cyan", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "cyan" },
            new { DictTypeCode = "hr_holiday_theme_color", DictLabel = "中国红", DictValue = "red", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "red" },
            new { DictTypeCode = "hr_holiday_theme_color", DictLabel = "提香红", DictValue = "orange", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "orange" },
            new { DictTypeCode = "hr_holiday_theme_color", DictLabel = "勃艮第红", DictValue = "purple", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "purple" },
            new { DictTypeCode = "hr_holiday_theme_color", DictLabel = "波尔多红", DictValue = "pink", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "pink" },
            new { DictTypeCode = "hr_holiday_theme_color", DictLabel = "克莱因蓝", DictValue = "blue", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "blue" },
            new { DictTypeCode = "hr_holiday_theme_color", DictLabel = "凡戴克棕", DictValue = "brown", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "brown" },
            new { DictTypeCode = "hr_holiday_theme_color", DictLabel = "普鲁士蓝", DictValue = "indigo", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "indigo" },
            new { DictTypeCode = "hr_holiday_theme_color", DictLabel = "申布伦黄", DictValue = "yellow", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "yellow" },
            new { DictTypeCode = "hr_holiday_theme_color", DictLabel = "纪念灰", DictValue = "gray", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "gray" },

            // acct_wage_rate_type - 工资率类别：0=标准，1=预算，2=实际
            new { DictTypeCode = "acct_wage_rate_type", DictLabel = "标准", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "标准工资率" },
            new { DictTypeCode = "acct_wage_rate_type", DictLabel = "预算", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "预算工资率" },
            new { DictTypeCode = "acct_wage_rate_type", DictLabel = "实际", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "实际工资率" },

            // acct_cost_center_type - 成本中心类型（SAP按组织职能分类）：0=生产，1=管理，2=销售与分销，3=研发，4=其他
            new { DictTypeCode = "acct_cost_center_type", DictLabel = "生产", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "直接参与产品制造或提供核心服务的部门。生产车间、制造单元" },
            new { DictTypeCode = "acct_cost_center_type", DictLabel = "管理", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "负责企业行政管理和决策支持的部门。财务部、人事部、行政部" },
            new { DictTypeCode = "acct_cost_center_type", DictLabel = "销售与分销", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "负责市场推广和产品销售。销售部、市场部" },
            new { DictTypeCode = "acct_cost_center_type", DictLabel = "研发", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "负责新产品、新技术的开发。研发中心、实验室" },
            new { DictTypeCode = "acct_cost_center_type", DictLabel = "其他", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "无法归入上述类别的辅助部门。食堂、安保、维修" },

            // acct_title_type - 科目类型（会计要素）：0=资产，1=负债，2=所有者权益，3=收入，4=费用，5=成本
            new { DictTypeCode = "acct_title_type", DictLabel = "资产", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "资产类科目" },
            new { DictTypeCode = "acct_title_type", DictLabel = "负债", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "负债类科目" },
            new { DictTypeCode = "acct_title_type", DictLabel = "所有者权益", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "所有者权益类科目" },
            new { DictTypeCode = "acct_title_type", DictLabel = "收入", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "收入类科目" },
            new { DictTypeCode = "acct_title_type", DictLabel = "费用", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "费用类科目" },
            new { DictTypeCode = "acct_title_type", DictLabel = "成本", DictValue = "5", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "成本类科目" },

            // acct_asset_type - 资产类型：0=固定资产，1=无形资产，2=流动资产，3=长期投资
            new { DictTypeCode = "acct_asset_type", DictLabel = "固定资产", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "房屋、设备等长期资产" },
            new { DictTypeCode = "acct_asset_type", DictLabel = "无形资产", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "专利、商标等无形资产" },
            new { DictTypeCode = "acct_asset_type", DictLabel = "流动资产", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "存货、应收账款等" },
            new { DictTypeCode = "acct_asset_type", DictLabel = "长期投资", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "长期股权投资、持有至到期投资等" },

            // acct_fixed_asset_type - 固定资产类型细分：0=房屋建筑物，1=机器设备，2=运输工具，3=电子设备，4=办公家具，5=其他
            new { DictTypeCode = "acct_fixed_asset_type", DictLabel = "房屋建筑物", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "厂房、办公楼、仓库等" },
            new { DictTypeCode = "acct_fixed_asset_type", DictLabel = "机器设备", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "生产线、机床、专用设备等" },
            new { DictTypeCode = "acct_fixed_asset_type", DictLabel = "运输工具", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "车辆、船舶等" },
            new { DictTypeCode = "acct_fixed_asset_type", DictLabel = "电子设备", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "电脑、服务器、测试仪器等" },
            new { DictTypeCode = "acct_fixed_asset_type", DictLabel = "办公家具", DictValue = "4", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "桌椅、文件柜等" },
            new { DictTypeCode = "acct_fixed_asset_type", DictLabel = "其他", DictValue = "5", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "无法归入上述类别的固定资产" },

            // acct_industry_type - 行业类别（《国民经济行业分类》GB/T 4754—2017 门类）：用于公司、客户等
            new { DictTypeCode = "acct_industry_type", DictLabel = "农、林、牧、渔业", DictValue = "A", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "本门类包括01～05大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "采矿业", DictValue = "B", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "本门类包括06～12大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "制造业", DictValue = "C", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "本门类包括13～43大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "电力、热力、燃气及水生产和供应业", DictValue = "D", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "本门类包括44～46大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "建筑业", DictValue = "E", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "本门类包括47～50大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "批发和零售业", DictValue = "F", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "本门类包括51～52大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "交通运输、仓储和邮政业", DictValue = "G", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "本门类包括53～60大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "住宿和餐饮业", DictValue = "H", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "本门类包括61～62大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "信息传输、软件和信息技术服务业", DictValue = "I", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "本门类包括63～65大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "金融业", DictValue = "J", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "本门类包括66～69大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "房地产业", DictValue = "K", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "本门类包括70大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "租赁和商务服务业", DictValue = "L", OrderNum = 12, CssClass = 12, ListClass = 12, Remark = "本门类包括71～72大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "科学研究和技术服务业", DictValue = "M", OrderNum = 13, CssClass = 13, ListClass = 13, Remark = "本门类包括73～75大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "水利、环境和公共设施管理业", DictValue = "N", OrderNum = 14, CssClass = 14, ListClass = 14, Remark = "本门类包括76～78大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "居民服务、修理和其他服务业", DictValue = "O", OrderNum = 15, CssClass = 15, ListClass = 15, Remark = "本门类包括79～81大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "教育", DictValue = "P", OrderNum = 16, CssClass = 16, ListClass = 16, Remark = "本门类包括82大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "卫生和社会工作", DictValue = "Q", OrderNum = 17, CssClass = 17, ListClass = 17, Remark = "本门类包括83～84大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "文化、体育和娱乐业", DictValue = "R", OrderNum = 18, CssClass = 18, ListClass = 18, Remark = "本门类包括85～89大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "公共管理、社会保障和社会组织", DictValue = "S", OrderNum = 19, CssClass = 19, ListClass = 19, Remark = "本门类包括90～95大类" },
            new { DictTypeCode = "acct_industry_type", DictLabel = "国际组织", DictValue = "T", OrderNum = 20, CssClass = 20, ListClass = 20, Remark = "本门类包括96大类" },

            // acct_enterprise_registration_type - 企业性质（《关于划分企业登记注册类型的规定》国统字〔2011〕86号）
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "国有企业", DictValue = "110", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "内资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "集体企业", DictValue = "120", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "内资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "股份合作企业", DictValue = "130", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "内资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "联营企业", DictValue = "140", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "内资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "有限责任公司", DictValue = "150", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "内资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "股份有限公司", DictValue = "160", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "内资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "私营企业", DictValue = "170", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "内资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "其他内资企业", DictValue = "190", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "内资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "合资经营企业（港或澳、台资）", DictValue = "210", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "港、澳、台商投资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "合作经营企业（港或澳、台资）", DictValue = "220", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "港、澳、台商投资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "港、澳、台商独资经营企业", DictValue = "230", OrderNum = 11, CssClass = 11, ListClass = 11, Remark = "港、澳、台商投资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "港、澳、台商投资股份有限公司", DictValue = "240", OrderNum = 12, CssClass = 12, ListClass = 12, Remark = "港、澳、台商投资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "其他港、澳、台商投资企业", DictValue = "290", OrderNum = 13, CssClass = 13, ListClass = 13, Remark = "港、澳、台商投资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "中外合资经营企业", DictValue = "310", OrderNum = 14, CssClass = 14, ListClass = 14, Remark = "外商投资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "中外合作经营企业", DictValue = "320", OrderNum = 15, CssClass = 15, ListClass = 15, Remark = "外商投资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "外资企业", DictValue = "330", OrderNum = 16, CssClass = 16, ListClass = 16, Remark = "外商投资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "外商投资股份有限公司", DictValue = "340", OrderNum = 17, CssClass = 17, ListClass = 17, Remark = "外商投资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "其他外商投资企业", DictValue = "390", OrderNum = 18, CssClass = 18, ListClass = 18, Remark = "外商投资企业" },
            new { DictTypeCode = "acct_enterprise_registration_type", DictLabel = "个体工商户", DictValue = "410", OrderNum = 19, CssClass = 19, ListClass = 19, Remark = "个体经营" },

            // acct_enterprise_size - 企业规模（《统计上大中小微型企业划分办法(2017)》）：分行业按从业人数、营业收入、资产总额划分
            new { DictTypeCode = "acct_enterprise_size", DictLabel = "大型", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "工业：从业≥1000人或营收≥40000万元。批发/零售等行业标准不同" },
            new { DictTypeCode = "acct_enterprise_size", DictLabel = "中型", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "工业：300-999人或2000-39999万元。批发/零售等行业标准不同" },
            new { DictTypeCode = "acct_enterprise_size", DictLabel = "小型", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "工业：20-299人或300-1999万元。批发/零售等行业标准不同" },
            new { DictTypeCode = "acct_enterprise_size", DictLabel = "微型", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "工业：从业<20人或营收<300万元。批发/零售等行业标准不同" },

            // sys_base_unit - 国际标准单位（UN/CEFACT Rec.20-21、SI）；DictValue 存入物料表 base_unit
            new { DictTypeCode = "sys_base_unit", DictLabel = "件", DictValue = "PCE", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "件（piece, UN/CEFACT）" },
            new { DictTypeCode = "sys_base_unit", DictLabel = "套", DictValue = "SET", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "套（set, UN/CEFACT）" },
            new { DictTypeCode = "sys_base_unit", DictLabel = "个", DictValue = "EA", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "个（each, UN/CEFACT）" },
            new { DictTypeCode = "sys_base_unit", DictLabel = "台", DictValue = "NAR", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "台（number of articles, UN/CEFACT）" },
            new { DictTypeCode = "sys_base_unit", DictLabel = "箱", DictValue = "BX", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "箱（box, UN/CEFACT）" },
            new { DictTypeCode = "sys_base_unit", DictLabel = "千克", DictValue = "KGM", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "千克（kilogram, SI）" },
            new { DictTypeCode = "sys_base_unit", DictLabel = "米", DictValue = "MTR", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "米（metre, SI）" },
            new { DictTypeCode = "sys_base_unit", DictLabel = "升", DictValue = "LTR", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "升（litre, SI）" },

            // material_purchase_type - 采购类型（int）
            new { DictTypeCode = "material_purchase_type", DictLabel = "内部采购", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "内部采购" },
            new { DictTypeCode = "material_purchase_type", DictLabel = "外部采购", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "外部采购" },
            new { DictTypeCode = "material_purchase_type", DictLabel = "委外加工", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "委外加工" },
            new { DictTypeCode = "material_purchase_type", DictLabel = "其他", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "其他" },

            // material_brand - 物料品牌（可扩展）
            new { DictTypeCode = "material_brand", DictLabel = "其他", DictValue = "OTHER", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "其他品牌" },

            // material_special_procurement - 特殊采购（int）
            new { DictTypeCode = "material_special_procurement", DictLabel = "标准采购", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "标准采购" },
            new { DictTypeCode = "material_special_procurement", DictLabel = "寄售", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "寄售" },
            new { DictTypeCode = "material_special_procurement", DictLabel = "库存转移", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "库存转移" },
            new { DictTypeCode = "material_special_procurement", DictLabel = "其他", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "其他" },

            // material_price_control - 价格控制（int）
            new { DictTypeCode = "material_price_control", DictLabel = "标准价格", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "标准价格" },
            new { DictTypeCode = "material_price_control", DictLabel = "移动平均价格", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "移动平均价格" },
            new { DictTypeCode = "material_price_control", DictLabel = "其他", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "其他" },

            // material_valuation_category - 评估类别（4 位码）：原材料、半成品、成品、其它
            new { DictTypeCode = "material_valuation_category", DictLabel = "原材料", DictValue = "0001", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "原材料" },
            new { DictTypeCode = "material_valuation_category", DictLabel = "半成品", DictValue = "0002", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "半成品" },
            new { DictTypeCode = "material_valuation_category", DictLabel = "成品", DictValue = "0003", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "成品" },
            new { DictTypeCode = "material_valuation_category", DictLabel = "其它", DictValue = "0004", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "其它" },

            // material_stock_location - 库存地点（仓库名称，用于生产/采购库存地点，可扩展）
            new { DictTypeCode = "material_stock_location", DictLabel = "原料仓", DictValue = "原料仓", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "原料仓库" },
            new { DictTypeCode = "material_stock_location", DictLabel = "半成品仓", DictValue = "半成品仓", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "半成品仓库" },
            new { DictTypeCode = "material_stock_location", DictLabel = "成品仓", DictValue = "成品仓", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "成品仓库" },
            new { DictTypeCode = "material_stock_location", DictLabel = "其他仓", DictValue = "其他仓", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "其他仓库" },

            // material_attributes - 物料属性（int）
            new { DictTypeCode = "material_attributes", DictLabel = "标准", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "标准" },
            new { DictTypeCode = "material_attributes", DictLabel = "定制", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "定制" },
            new { DictTypeCode = "material_attributes", DictLabel = "关键件", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "关键件" },
            new { DictTypeCode = "material_attributes", DictLabel = "其他", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "其他" },

            // material_is_end_of_life - 停产状态（EOL）
            new { DictTypeCode = "material_is_end_of_life", DictLabel = "采购/仓库已锁定", DictValue = "01", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "01" },
            new { DictTypeCode = "material_is_end_of_life", DictLabel = "任务清单/BOM已锁定", DictValue = "02", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "02" },
            new { DictTypeCode = "material_is_end_of_life", DictLabel = "计划物料", DictValue = "Z0", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "Z0" },
            new { DictTypeCode = "material_is_end_of_life", DictLabel = "当前库存需确认", DictValue = "ZM", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "ZM" },
            new { DictTypeCode = "material_is_end_of_life", DictLabel = "制造中止", DictValue = "ZP", OrderNum = 5, CssClass = 5, ListClass = 5, Remark = "ZP" },
            new { DictTypeCode = "material_is_end_of_life", DictLabel = "生产结束（产品）", DictValue = "ZQ", OrderNum = 6, CssClass = 6, ListClass = 6, Remark = "ZQ" },
            new { DictTypeCode = "material_is_end_of_life", DictLabel = "PC MRP对象外", DictValue = "ZW", OrderNum = 7, CssClass = 7, ListClass = 7, Remark = "ZW" },
            new { DictTypeCode = "material_is_end_of_life", DictLabel = "PC 中介专用品", DictValue = "ZX", OrderNum = 8, CssClass = 8, ListClass = 8, Remark = "ZX" },
            new { DictTypeCode = "material_is_end_of_life", DictLabel = "PC 断开连接", DictValue = "ZY", OrderNum = 9, CssClass = 9, ListClass = 9, Remark = "ZY" },
            new { DictTypeCode = "material_is_end_of_life", DictLabel = "PC 有替代物料", DictValue = "ZZ", OrderNum = 10, CssClass = 10, ListClass = 10, Remark = "ZZ" },

            // sys_numbering_document_type - 单据类型（与编码规则 RuleCode 对应）
            new { DictTypeCode = "sys_numbering_document_type", DictLabel = "采购单", DictValue = "PurchaseOrder", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "采购订单" },
            new { DictTypeCode = "sys_numbering_document_type", DictLabel = "销售单", DictValue = "SalesOrder", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "销售订单" },
            new { DictTypeCode = "sys_numbering_document_type", DictLabel = "公告", DictValue = "Announcement", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "公告" },
            new { DictTypeCode = "sys_numbering_document_type", DictLabel = "流程实例", DictValue = "FlowInstance", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "流程实例编号" },
            new { DictTypeCode = "sys_numbering_document_type", DictLabel = "其他", DictValue = "Other", OrderNum = 99, CssClass = 99, ListClass = 99, Remark = "其他单据" },

            // sys_numbering_reset_cycle - 流水号重置周期：0=不重置，1=按日，2=按月，3=按年
            new { DictTypeCode = "sys_numbering_reset_cycle", DictLabel = "不重置", DictValue = "0", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "流水号不按周期归零" },
            new { DictTypeCode = "sys_numbering_reset_cycle", DictLabel = "按日", DictValue = "1", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "每日零点归零" },
            new { DictTypeCode = "sys_numbering_reset_cycle", DictLabel = "按月", DictValue = "2", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "每月1日归零" },
            new { DictTypeCode = "sys_numbering_reset_cycle", DictLabel = "按年", DictValue = "3", OrderNum = 4, CssClass = 4, ListClass = 4, Remark = "每年1月1日归零" },

            // sys_numbering_date_format - 日期部分格式：为空表示不包含日期；yyyy、yyyyMM、yyyyMMdd 等
            new { DictTypeCode = "sys_numbering_date_format", DictLabel = "不包含日期", DictValue = "", OrderNum = 0, CssClass = 0, ListClass = 0, Remark = "编号中不包含日期部分" },
            new { DictTypeCode = "sys_numbering_date_format", DictLabel = "yyyy（年）", DictValue = "yyyy", OrderNum = 1, CssClass = 1, ListClass = 1, Remark = "四位年份" },
            new { DictTypeCode = "sys_numbering_date_format", DictLabel = "yyyyMM（年月）", DictValue = "yyyyMM", OrderNum = 2, CssClass = 2, ListClass = 2, Remark = "年月6位" },
            new { DictTypeCode = "sys_numbering_date_format", DictLabel = "yyyyMMdd（年月日）", DictValue = "yyyyMMdd", OrderNum = 3, CssClass = 3, ListClass = 3, Remark = "年月日8位" }
        };

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

            // 生成本地化键：dict.{DictTypeCode}.{value}（必须为小写，必须非空）
            var valuePart = !string.IsNullOrWhiteSpace(dictData.DictValue) ? dictData.DictValue : dictData.DictLabel;
            var dictL10nKey = string.IsNullOrWhiteSpace(valuePart)
                ? $"dict.{dictData.DictTypeCode.ToLowerInvariant()}.{dictData.OrderNum}"
                : $"dict.{dictData.DictTypeCode.ToLowerInvariant()}.{valuePart.ToLowerInvariant()}";

            // ExtLabel=Remark，ExtValue 不设
            var extLabel = dictData.Remark;
            string? extValue = null;

            if (existing == null)
            {
                // 不存在则插入
                var newDictData = new TaktDictData
                {
                    DictTypeId = dictType.Id, // 设置字典类型ID（外键）
                    DictTypeCode = dictData.DictTypeCode,
                    DictLabel = dictData.DictLabel,
                    DictValue = dictData.DictValue,
                    DictL10nKey = dictL10nKey, // 设置本地化键
                    OrderNum = dictData.OrderNum,
                    CssClass = dictData.CssClass,
                    ListClass = dictData.ListClass,
                    ExtLabel = extLabel,
                    ExtValue = extValue,
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
                existing.DictL10nKey = dictL10nKey; // 更新本地化键
                existing.OrderNum = dictData.OrderNum;
                existing.CssClass = dictData.CssClass;
                existing.ListClass = dictData.ListClass;
                existing.ExtLabel = extLabel;
                existing.ExtValue = extValue;
                await dictDataRepository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
