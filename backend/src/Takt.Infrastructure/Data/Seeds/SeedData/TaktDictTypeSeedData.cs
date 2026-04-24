// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktDictTypeSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字典类型种子数据，初始化系统内置字典类型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.Dict;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt字典类型种子数据
/// </summary>
public class TaktDictTypeSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（字典类型应该在字典数据之前初始化）
    /// </summary>
    public int Order => 100;

    /// <summary>
    /// 初始化字典类型种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var dictTypeRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktDictType>>();

        int insertCount = 0;
        int updateCount = 0;

        // 定义系统内置字典类型
        var dictTypes = new[]
        {
            // 按 DictTypeCode 字典编码字母序（Ordinal）；SortOrder 为 1..94
            new { Code = "gen_button_category", Name = "代码生成操作后缀", Remark = "对应 TaktGenTable.MenuButtonGroup；DictValue 为完整权限码第四段英文 key（前缀为三段规范化的 PermsPrefixCanonical，见 TaktGenTable.PermsPrefix 注释）；多选逗号；TaktCodeGenWorkflowService.BuildSqlMenuButtonRowsAsync 生成 basePerm:sfx 与 MenuL10nKey=common.button.*；DictLabel 为中文名。已合并原 sys_button_category。曾用类型名「按钮权限后缀」片面。原编码 gen_menu_button。", SortOrder = 1 },
            new { Code = "gen_button_style", Name = "操作按钮样式", Remark = "代码生成表 TaktGenTable.FrontBtnStyle（front_btn_style）。0=文本，1=标准。原编码 sys_button_style。", SortOrder = 2 },
            new { Code = "gen_csharp_data_type", Name = "C#数据类型", Remark = "代码生成列 CsharpDataType。对应 string、int、long、DateTime、decimal、bool、Guid 等。C# 数据类型。原编码 sys_csharp_type。", SortOrder = 3 },
            new { Code = "gen_display_type", Name = "显示类型", Remark = "代码生成列 HtmlType/显示类型。input、select、checkbox 等。原编码 sys_display_type。", SortOrder = 4 },
            new { Code = "gen_frontend_form_layout", Name = "前端表单布局", Remark = "代码生成表 FrontFormLayout。12=一行一列，24=一行两列。原编码 sys_frontend_style。", SortOrder = 5 },
            new { Code = "gen_frontend_ui", Name = "前端UI框架", Remark = "代码生成表 FrontUi。1=element plus，2=ant design vue。原编码 sys_frontend_template。", SortOrder = 6 },
            new { Code = "gen_function", Name = "生成功能", Remark = "生成功能。查询，新增，更新，删除，状态，排序，模板，导入，导出", SortOrder = 7 },
            new { Code = "gen_method", Name = "生成方式", Remark = "代码生成方式。0=zip 压缩包，1=自定义路径，2=当前项目", SortOrder = 8 },
            new { Code = "gen_query_type", Name = "查询方式", Remark = "代码生成列 QueryType。EQ/NE/GT/GTE/LT/LTE/LIKE/BETWEEN。原编码 sys_query_type。", SortOrder = 9 },
            new { Code = "gen_template_type", Name = "生成模板类型", Remark = "生成模板类型。对应 TaktGenTable.GenTemplateCategory。crud=单表操作，tree=树表操作，sub=主子表操作", SortOrder = 11 },
            new { Code = "hr_attendance_correction_approval", Name = "补卡审批状态", Remark = "补卡审批状态（与 TaktAttendanceCorrection.approval_status 一致）。0=草稿，1=待审，2=已通过，3=已驳回", SortOrder = 12 },
            new { Code = "hr_attendance_correction_kind", Name = "补卡类型", Remark = "补卡类型（与 TaktAttendanceCorrection.correction_kind 一致）。1=上班，2=下班", SortOrder = 13 },
            new { Code = "hr_attendance_device_brand", Name = "考勤设备品牌", Remark = "设备品牌（与多品牌 SDK 路由一致）。Hikvision=海康威视，Deli=得力，ZKTeco=中控", SortOrder = 14 },
            new { Code = "hr_attendance_device_status", Name = "考勤设备状态", Remark = "设备状态（与 TaktAttendanceDevice.device_status 一致）。0=停用，1=正常，2=故障", SortOrder = 15 },
            new { Code = "hr_attendance_exception_handle_status", Name = "考勤异常处理状态", Remark = "处理状态（与 TaktAttendanceException.handle_status 一致）。0=待处理，1=已处理，2=已忽略", SortOrder = 16 },
            new { Code = "hr_attendance_exception_type", Name = "考勤异常类型", Remark = "异常类型（与 TaktAttendanceException.exception_type 一致）。1=上班缺卡，2=下班缺卡，3=迟到，4=早退，5=旷工，9=其他", SortOrder = 17 },
            new { Code = "hr_attendance_punch_source", Name = "打卡来源", Remark = "打卡来源（与 TaktAttendancePunch.punch_source 一致）。0=后台录入，1=移动端，2=导入", SortOrder = 18 },
            new { Code = "hr_attendance_punch_type", Name = "打卡类型", Remark = "打卡类型（与 TaktAttendancePunch.punch_type 一致）。1=上班，2=下班，3=外勤", SortOrder = 19 },
            new { Code = "hr_attendance_result_status", Name = "出勤状态", Remark = "考勤日结出勤状态（与 TaktAttendanceResult.attendance_status 一致）。0=正常，1=迟到，2=早退，3=缺卡，4=旷工，5=加班", SortOrder = 20 },
            new { Code = "hr_attendance_verify_mode", Name = "考勤验证方式", Remark = "验证方式（与 TaktAttendanceSource.verify_mode 一致）。0=未知，1=指纹，2=人脸，3=密码，4=卡", SortOrder = 21 },
            new { Code = "hr_delegate_mode", Name = "人事代理模式", Remark = "部门/岗位/员工代理子表 delegate_mode。0=直接员工，1=部门规则，2=岗位规则", SortOrder = 22 },
            new { Code = "hr_employee_status", Name = "员工状态", Remark = "员工状态。0=在职，1=离职，2=停薪留职，3=退休", SortOrder = 23 },
            new { Code = "hr_ethnic_group", Name = "民族", Remark = "民族（56 个民族）。DictValue=序号 1～56，与国家标准排序一致", SortOrder = 24 },
            new { Code = "hr_holiday_is_working_day", Name = "假日是否工作日", Remark = "是否工作日（假日表 is_working_day）。0=非工作日，1=工作日，2=半天等；与 TaktHoliday.IsWorkingDay 一致", SortOrder = 25 },
            new { Code = "hr_holiday_type", Name = "假日类型", Remark = "假日类型（假日表 holiday_type）。0=法定，1=调休，2=公司", SortOrder = 26 },
            new { Code = "hr_leave_status", Name = "请假状态", Remark = "请假状态（与 TaktLeave.leave_status 一致）。0=草稿，1=审批中，2=已通过，3=已驳回，4=已撤回", SortOrder = 27 },
            new { Code = "hr_marital_status", Name = "婚姻状况", Remark = "婚姻状况。0=未婚，1=已婚，2=离异，3=丧偶", SortOrder = 28 },
            new { Code = "hr_native_place", Name = "籍贯", Remark = "籍贯（省级行政区，GB 区划代码前两位+0000）。用于下拉选择与员工档案 native_place 对照", SortOrder = 29 },
            new { Code = "hr_overtime_status", Name = "加班状态", Remark = "加班状态（与 TaktOvertime.overtime_status 一致）。0=草稿，1=已提交，2=已通过，3=已驳回", SortOrder = 30 },
            new { Code = "hr_overtime_type", Name = "加班类型", Remark = "加班类型（与 TaktOvertime.overtime_type 一致）。0=工作日加班，1=休息日加班，2=法定节假日加班", SortOrder = 31 },
            new { Code = "hr_political_status", Name = "政治面貌", Remark = "政治面貌（国家标准十三类）。0=群众，1=共青团员，2=中共党员，3=中共预备党员，4=民革党员，5=民盟盟员，6=民建会员，7=民进会员，8=农工党党员，9=致公党党员，10=九三学社社员，11=台盟盟员，12=无党派民主人士", SortOrder = 32 },
            new { Code = "hr_schedule_type", Name = "排班类别", Remark = "排班类别（与 TaktShiftSchedule.schedule_type 一致）。0=部门，1=人员", SortOrder = 33 },
            new { Code = "hr_transfer_status", Name = "调动状态", Remark = "调动状态。0=草稿，1=审批中，2=已通过，3=已驳回，4=已撤回", SortOrder = 34 },
            new { Code = "hr_transfer_type", Name = "调动类型", Remark = "员工调动类型。0=转岗，1=调岗", SortOrder = 35 },
            new { Code = "prod_aoi_inspection_line", Name = "AOI线别", Remark = "AOI 检测线别，如 AOI1、AOI2 等。原编码 aoi_inspection_line。", SortOrder = 36 },
            new { Code = "prod_assy_location", Name = "Assy个所", Remark = "组立个所。自插、部品、设计、修正、加工、手插、组立、SMT、其他", SortOrder = 37 },
            new { Code = "prod_defect_category", Name = "不良区分", Remark = "组立不良区分/类别，用于不良明细分类。原编码 defect_category。", SortOrder = 38 },
            new { Code = "prod_defect_location", Name = "不良个所", Remark = "不良个所/发生场所，用于不良明细发生位置。原编码 defect_location。", SortOrder = 39 },
            new { Code = "prod_ec_distinction", Name = "设变管理区分", Remark = "设变管理区分。1=全仕向，2=部管，3=内部，4=技术。原编码 sys_ec_distinction。", SortOrder = 40 },
            new { Code = "prod_ec_status", Name = "设变状态", Remark = "设变（ECN）改号状态。1=工作的，2=取消的，3=发行的，4=P.P中变更的，5=固定的，6=挂起的，7=拒绝的。原编码 sys_ec_status。", SortOrder = 41 },
            new { Code = "prod_equipment_type", Name = "设备类型", Remark = "设备类型。0=生产设备，1=检测设备，2=包装设备，3=物流设备，4=辅助设备", SortOrder = 42 },
            new { Code = "prod_equipment_status", Name = "设备状态", Remark = "设备状态。0=运行中，1=停机，2=维修中，3=故障，4=待报废，5=已报废", SortOrder = 43 },
            new { Code = "prod_maintenance_type", Name = "维护类型", Remark = "维护类型。0=定期保养，1=故障维修，2=大修，3=改造升级，4=其他", SortOrder = 44 },
            new { Code = "prod_warranty_status", Name = "保修状态", Remark = "保修状态。0=无保修，1=保修期内，2=保修期外，3=延保中", SortOrder = 45 },
            new { Code = "prod_nonachievement_reason", Name = "未达成原因", Remark = "未达成原因。清机、测试慢/测试修理机、修理试机、转机、人员欠缺、部品不良/欠料、ST差异大、仪器设备/设置/调试/检查/故障/切换、请假/旷工、其他、切换机种/仕向、组立慢/加工多/工程多/下机慢/作业困难/升级慢、改修、坏机多/不良多、人员借调、返工、下机慢、学习中/新人员学习/开会、正常", SortOrder = 46 },
            new { Code = "prod_pcb_location", Name = "PCB个所", Remark = "PCB个所。翘脚、生锡、锡量過多、空焊、漏件、发黄、IC PIN竖立/浮高、连锡、異物付着、底下有部品、基板不良、红胶不良、反面、位置偏移、部品不良/破損、立碑、翻面、撞件、错料、侧立、反向、PCB不良、焊接不良、極性相違、多件、锡少等", SortOrder = 47 },
            new { Code = "prod_pcba_function_category", Name = "PCBA功能类别", Remark = "PCBA 功能类别。A、ADOC、ANA、AUDIO、B、BOTTOM、BTICE、C、DSPL、ENC、FRONT、INPUT、IO、JACK、L、LCD、MAIN、PANEL、POWER、REAR、RMN-1、SATA、SEQ、SYS、TOP、USB。原编码 pcba_function_category。", SortOrder = 48 },
            new { Code = "prod_pcba_panel_category", Name = "PCBA板位类别", Remark = "PCBA 板位类别。电源、前板、IO板等。原编码 pcba_panel_category。", SortOrder = 49 },
            new { Code = "prod_pcba_side_category", Name = "PCBA面别", Remark = "PCBA 面别。B面、T面。原编码 pcba_side_category。", SortOrder = 50 },
            new { Code = "prod_shift_category", Name = "生产班别", Remark = "生产班别。早、中、晚、白班、夜班", SortOrder = 51 },
            new { Code = "prod_stop_reason", Name = "停线原因", Remark = "停线原因。切换停止时间、周会、其他、欠料、停电、班会、切换机种、早会、组立、学习、仪设、清洁", SortOrder = 52 },
            new { Code = "prod_visual_inspection_line", Name = "目视线别", Remark = "目视检查线别，如 L1、L2 等。原编码 visual_inspection_line。", SortOrder = 53 },
            new { Code = "sys_data_scope", Name = "数据权限", Remark = "数据范围。0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围", SortOrder = 54 },
            new { Code = "sys_data_source", Name = "数据源", Remark = "字典数据源（与 TaktDictType.data_source 一致）。0=系统表，1=SQL查询", SortOrder = 55 },
            new { Code = "sys_db_data_type", Name = "数据库数据类型", Remark = "数据库数据类型。基于数据库的数据类型，如：varchar、int、datetime、decimal等。原编码 sys_db_type。", SortOrder = 56 },
            new { Code = "sys_dept_type", Name = "部门类型", Remark = "部门类型。0=直接，1=间接", SortOrder = 57 },
            new { Code = "sys_file_category", Name = "文件分类", Remark = "文件分类。0=文档，1=图片，2=视频，3=音频，4=压缩包，5=其他", SortOrder = 58 },
            new { Code = "sys_file_status", Name = "文件状态", Remark = "文件状态。0=正常，1=已锁定，2=已归档，3=已删除", SortOrder = 59 },
            new { Code = "sys_flow_category", Name = "流程分类", Remark = "流程分类。0=通用流程，1=业务流程，2=系统流程", SortOrder = 60 },
            new { Code = "sys_flow_status", Name = "流程状态", Remark = "流程实例运行状态。0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回，5=草稿", SortOrder = 61 },
            new { Code = "sys_form_category", Name = "表单分类", Remark = "表单分类。0=通用表单，1=业务表单，2=系统表单", SortOrder = 62 },
            new { Code = "sys_form_type", Name = "表单类型", Remark = "表单类型。0=动态表单，1=静态表单，2=自定义表单", SortOrder = 63 },
            new { Code = "sys_ftp_provider", Name = "FTP服务提供商", Remark = "FTP服务提供商类型。teac_cn=TEAC FTP中国（ftp.teac.com.cn），teac_jp=TEAC FTP日本（rosu2.teac.co.jp）", SortOrder = 64 },
            new { Code = "sys_is_builtin", Name = "是否内置", Remark = "是否内置标志。1=是/内置，0=否/自定义", SortOrder = 65 },
            new { Code = "sys_is_default", Name = "是否默认", Remark = "通用默认标志。1=是/默认，0=否/非默认", SortOrder = 66 },
            new { Code = "sys_is_public", Name = "是否公开", Remark = "是否公开标志。0=公开，1=私有", SortOrder = 67 },
            new { Code = "sys_language_code", Name = "语言编码", Remark = "语言编码。ISO 639-1/639-2，如：zh-CN、en-US", SortOrder = 68 },
            new { Code = "sys_leave_category", Name = "请假类型", Remark = "请假类型（与请假表 leave_type 一致）。affair=事假，sick=病假，annual=年假，marriage=婚假，maternity=产假，paternity=陪产假，bereavement=丧假，compensatory=调休，personal=私假，other=其他，可扩展", SortOrder = 69 },
            new { Code = "sys_list_class", Name = "列表类名", Remark = "列表类名。用于前端样式控制", SortOrder = 70 },
            new { Code = "sys_mail_status", Name = "邮件状态", Remark = "邮件状态。0=草稿，1=已发送，2=发送失败，3=已撤回，4=定时发送中", SortOrder = 71 },
            new { Code = "sys_mail_type", Name = "邮件类型", Remark = "邮件类型。0=普通邮件，1=系统邮件，2=通知邮件，3=提醒邮件", SortOrder = 72 },
            new { Code = "sys_menu_type", Name = "菜单类型", Remark = "菜单类型。0=目录，1=菜单，2=按钮", SortOrder = 73 },
            new { Code = "sys_message_group", Name = "消息分组", Remark = "消息分组。Chat=聊天，Notification=通知，Alert=提醒", SortOrder = 74 },
            new { Code = "sys_message_type", Name = "消息类型", Remark = "消息类型。Text=文本，Image=图片，File=文件，System=系统消息", SortOrder = 75 },
            new { Code = "sys_news_category", Name = "新闻分类", Remark = "新闻分类。0=公司新闻，1=行业动态，2=技术分享，3=产品发布，4=活动资讯，5=其他", SortOrder = 76 },
            new { Code = "sys_news_status", Name = "新闻状态", Remark = "新闻状态。0=草稿，1=已发布，2=已撤回，3=已过期", SortOrder = 77 },
            new { Code = "sys_normal_disable", Name = "默认状态", Remark = "通用默认状态。1=启用，0=禁用，2=锁定", SortOrder = 78 },
            new { Code = "sys_notice_status", Name = "公告状态", Remark = "公告状态。0=草稿，1=已发布，2=已撤回，3=已过期", SortOrder = 79 },
            new { Code = "sys_notice_type", Name = "公告类型", Remark = "公告类型。0=通知，1=公告，2=新闻，3=活动", SortOrder = 80 },
            new { Code = "sys_online_status", Name = "在线状态", Remark = "在线状态。0=在线，1=离线，2=离开", SortOrder = 81 },
            new { Code = "sys_oper_type", Name = "操作类型", Remark = "系统操作类型。1=新增，2=修改，3=删除，4=查询，5=导出，6=导入，7=授权，8=强退，9=生成代码，10=清空数据", SortOrder = 82 },
            new { Code = "sys_oss_provider", Name = "OSS提供商类型", Remark = "OSS对象存储提供商类型。aliyun=阿里云OSS，tencent=腾讯云COS，huawei=华为云OBS，aws=AWS S3", SortOrder = 83 },
            new { Code = "sys_post_category", Name = "岗位类别", Remark = "岗位类别。管理类、技术类、业务类、支持类", SortOrder = 84 },
            new { Code = "sys_post_level", Name = "岗位级别", Remark = "岗位级别。1=初级，2=中级，3=高级，4=专家，5=资深", SortOrder = 85 },
            new { Code = "sys_priority", Name = "优先级", Remark = "优先级。0=低，1=中，2=高，3=紧急", SortOrder = 86 },
            new { Code = "sys_publish_scope", Name = "发布范围", Remark = "发布范围。0=全部，1=指定部门，2=指定用户，3=指定角色", SortOrder = 87 },
            new { Code = "sys_read_status", Name = "读取状态", Remark = "读取状态。0=未读，1=已读", SortOrder = 88 },
            new { Code = "sys_resource_type", Name = "资源类型", Remark = "资源类型。Frontend=前端，Backend=后端", SortOrder = 89 },
            new { Code = "sys_scheme_status", Name = "方案状态", Remark = "流程/表单方案状态。0=草稿，1=已发布，2=已禁用", SortOrder = 90 },
            new { Code = "sys_setting_group", Name = "设置分组", Remark = "设置分组。backend=后端，frontend=前端", SortOrder = 91 },
            new { Code = "sys_sort_type", Name = "排序类型", Remark = "排序类型。asc=升序，desc=降序", SortOrder = 92 },
            new { Code = "sys_storage_directory", Name = "存储目录", Remark = "存储目录。用于文件分类存储", SortOrder = 93 },
            new { Code = "sys_storage_naming", Name = "存储命名规则", Remark = "存储命名规则。0=原文件+哈希值，1=自动生成，2=自定义", SortOrder = 94 },
            new { Code = "sys_storage_type", Name = "存储方式", Remark = "存储方式。0=本地存储，1=OSS对象存储，2=FTP，3=其他", SortOrder = 95 },
            new { Code = "sys_urgency_level", Name = "紧急程度", Remark = "是否紧急。0=一般，1=紧急，2=非常紧急", SortOrder = 96 },
            new { Code = "sys_user_gender", Name = "用户性别", Remark = "用户性别。0=未知，1=男，2=女", SortOrder = 97 },
            new { Code = "sys_user_type", Name = "用户类型", Remark = "用户类型。0=普通用户，1=管理员，2=超级管理员", SortOrder = 98 },
            new { Code = "sys_word_category", Name = "敏感词词性类别", Remark = "敏感词分类（与 TaktSensitiveWords.word_category 一致）。1=政治敏感，2=暴力恐怖，3=色情低俗，4=广告营销，5=辱骂歧视", SortOrder = 99 },
            new { Code = "sys_word_filter_level", Name = "敏感词过滤等级", Remark = "敏感词过滤等级（与 TaktSensitiveWords.filter_level 一致）。1=低，2=中，3=高", SortOrder = 100 },
            new { Code = "sys_yes_no", Name = "是否", Remark = "通用布尔标志。1=是/启用，0=否/禁用", SortOrder = 101 },
        };

        // 初始化每个字典类型
        foreach (var dictType in dictTypes)
        {
            var existing = await dictTypeRepository.GetAsync(t => t.DictTypeCode == dictType.Code);

            if (existing == null)
            {
                // 不存在则插入
                var newDictType = new TaktDictType
                {
                    DictTypeCode = dictType.Code,
                    DictTypeName = dictType.Name,
                    Remark = dictType.Remark,
                    DataSource = 0, // 0=系统表
                    IsBuiltIn = 1, // 1=是（内置）
                    SortOrder = dictType.SortOrder,
                    DictTypeStatus = 0, // 0=启用
                    IsDeleted = 0
                };
                await dictTypeRepository.CreateAsync(newDictType);
                insertCount++;
            }
            else
            {
                // 存在则更新
                existing.DictTypeName = dictType.Name;
                existing.Remark = dictType.Remark;
                existing.SortOrder = dictType.SortOrder;
                existing.DictTypeStatus = 0;
                await dictTypeRepository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
