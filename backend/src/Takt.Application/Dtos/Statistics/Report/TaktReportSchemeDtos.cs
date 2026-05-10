// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Statistics.Report
// 文件名称：TaktReportSchemeDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：报表方案表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Statistics.Report;

/// <summary>
/// 报表方案表Dto
/// </summary>
public partial class TaktReportSchemeDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportSchemeDto()
    {
        ReportCode = string.Empty;
        ReportName = string.Empty;
        ReportCategory = string.Empty;
        ApplicationModule = string.Empty;
        ReportDescription = string.Empty;
        SelectionScreenConfig = string.Empty;
        DataSourceType = string.Empty;
        DataSourceName = string.Empty;
        SqlQuery = string.Empty;
        OutputType = string.Empty;
        AlvColumnConfig = string.Empty;
        DefaultLayoutVariant = string.Empty;
        SubtotalFields = string.Empty;
        SortFields = string.Empty;
        FilterConfig = string.Empty;
        DrillDownReportCode = string.Empty;
        ExportFormats = string.Empty;
        PrintTemplate = string.Empty;
        ApplicablePlantCodes = string.Empty;
        ApplicableCompanyCodes = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicableRoles = string.Empty;
        DevelopmentClass = string.Empty;
        Author = string.Empty;
        Version = string.Empty;
    }

    /// <summary>
    /// 报表方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReportSchemeId { get; set; } = 0;

    /// <summary>
    /// 报表编码
    /// </summary>
    public string ReportCode { get; set; }
    /// <summary>
    /// 报表名称
    /// </summary>
    public string ReportName { get; set; }
    /// <summary>
    /// 报表类别
    /// </summary>
    public string ReportCategory { get; set; }
    /// <summary>
    /// 应用模块
    /// </summary>
    public string ApplicationModule { get; set; }
    /// <summary>
    /// 报表描述
    /// </summary>
    public string ReportDescription { get; set; }
    /// <summary>
    /// 选择屏幕配置
    /// </summary>
    public string SelectionScreenConfig { get; set; }
    /// <summary>
    /// 数据源类型
    /// </summary>
    public string DataSourceType { get; set; }
    /// <summary>
    /// 数据源名称
    /// </summary>
    public string DataSourceName { get; set; }
    /// <summary>
    /// SQL查询语句
    /// </summary>
    public string SqlQuery { get; set; }
    /// <summary>
    /// 输出类型
    /// </summary>
    public string OutputType { get; set; }
    /// <summary>
    /// ALV列配置
    /// </summary>
    public string AlvColumnConfig { get; set; }
    /// <summary>
    /// 默认布局变式
    /// </summary>
    public string DefaultLayoutVariant { get; set; }
    /// <summary>
    /// 是否支持布局变式
    /// </summary>
    public int SupportLayoutVariant { get; set; }
    /// <summary>
    /// 小计字段配置
    /// </summary>
    public string SubtotalFields { get; set; }
    /// <summary>
    /// 排序字段配置
    /// </summary>
    public string SortFields { get; set; }
    /// <summary>
    /// 过滤条件配置
    /// </summary>
    public string FilterConfig { get; set; }
    /// <summary>
    /// 是否支持总计
    /// </summary>
    public int SupportTotal { get; set; }
    /// <summary>
    /// 是否支持小计
    /// </summary>
    public int SupportSubtotal { get; set; }
    /// <summary>
    /// 是否支持汇总
    /// </summary>
    public int SupportAggregation { get; set; }
    /// <summary>
    /// 是否支持钻取
    /// </summary>
    public int SupportDrillDown { get; set; }
    /// <summary>
    /// 钻取目标报表编码
    /// </summary>
    public string DrillDownReportCode { get; set; }
    /// <summary>
    /// 是否支持后台执行
    /// </summary>
    public int SupportBackground { get; set; }
    /// <summary>
    /// 是否支持变式保存
    /// </summary>
    public int SupportVariantSave { get; set; }
    /// <summary>
    /// 默认页大小
    /// </summary>
    public int DefaultPageSize { get; set; }
    /// <summary>
    /// 最大数据行数
    /// </summary>
    public int MaxRowCount { get; set; }
    /// <summary>
    /// 是否支持导出
    /// </summary>
    public int IsExportable { get; set; }
    /// <summary>
    /// 导出格式配置
    /// </summary>
    public string ExportFormats { get; set; }
    /// <summary>
    /// 是否支持打印
    /// </summary>
    public int IsPrintable { get; set; }
    /// <summary>
    /// 打印模板
    /// </summary>
    public string PrintTemplate { get; set; }
    /// <summary>
    /// 适用工厂代码
    /// </summary>
    public string ApplicablePlantCodes { get; set; }
    /// <summary>
    /// 适用公司代码
    /// </summary>
    public string ApplicableCompanyCodes { get; set; }
    /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }
    /// <summary>
    /// 适用角色
    /// </summary>
    public string ApplicableRoles { get; set; }
    /// <summary>
    /// 开发类
    /// </summary>
    public string DevelopmentClass { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }
    /// <summary>
    /// 版本
    /// </summary>
    public string Version { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 报表方案表查询DTO
/// </summary>
public partial class TaktReportSchemeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportSchemeQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 报表编码
    /// </summary>
    public string? ReportCode { get; set; }
    /// <summary>
    /// 报表名称
    /// </summary>
    public string? ReportName { get; set; }
    /// <summary>
    /// 报表类别
    /// </summary>
    public string? ReportCategory { get; set; }
    /// <summary>
    /// 应用模块
    /// </summary>
    public string? ApplicationModule { get; set; }
    /// <summary>
    /// 报表描述
    /// </summary>
    public string? ReportDescription { get; set; }
    /// <summary>
    /// 选择屏幕配置
    /// </summary>
    public string? SelectionScreenConfig { get; set; }
    /// <summary>
    /// 数据源类型
    /// </summary>
    public string? DataSourceType { get; set; }
    /// <summary>
    /// 数据源名称
    /// </summary>
    public string? DataSourceName { get; set; }
    /// <summary>
    /// SQL查询语句
    /// </summary>
    public string? SqlQuery { get; set; }
    /// <summary>
    /// 输出类型
    /// </summary>
    public string? OutputType { get; set; }
    /// <summary>
    /// ALV列配置
    /// </summary>
    public string? AlvColumnConfig { get; set; }
    /// <summary>
    /// 默认布局变式
    /// </summary>
    public string? DefaultLayoutVariant { get; set; }
    /// <summary>
    /// 是否支持布局变式
    /// </summary>
    public int? SupportLayoutVariant { get; set; }
    /// <summary>
    /// 小计字段配置
    /// </summary>
    public string? SubtotalFields { get; set; }
    /// <summary>
    /// 排序字段配置
    /// </summary>
    public string? SortFields { get; set; }
    /// <summary>
    /// 过滤条件配置
    /// </summary>
    public string? FilterConfig { get; set; }
    /// <summary>
    /// 是否支持总计
    /// </summary>
    public int? SupportTotal { get; set; }
    /// <summary>
    /// 是否支持小计
    /// </summary>
    public int? SupportSubtotal { get; set; }
    /// <summary>
    /// 是否支持汇总
    /// </summary>
    public int? SupportAggregation { get; set; }
    /// <summary>
    /// 是否支持钻取
    /// </summary>
    public int? SupportDrillDown { get; set; }
    /// <summary>
    /// 钻取目标报表编码
    /// </summary>
    public string? DrillDownReportCode { get; set; }
    /// <summary>
    /// 是否支持后台执行
    /// </summary>
    public int? SupportBackground { get; set; }
    /// <summary>
    /// 是否支持变式保存
    /// </summary>
    public int? SupportVariantSave { get; set; }
    /// <summary>
    /// 默认页大小
    /// </summary>
    public int? DefaultPageSize { get; set; }
    /// <summary>
    /// 最大数据行数
    /// </summary>
    public int? MaxRowCount { get; set; }
    /// <summary>
    /// 是否支持导出
    /// </summary>
    public int? IsExportable { get; set; }
    /// <summary>
    /// 导出格式配置
    /// </summary>
    public string? ExportFormats { get; set; }
    /// <summary>
    /// 是否支持打印
    /// </summary>
    public int? IsPrintable { get; set; }
    /// <summary>
    /// 打印模板
    /// </summary>
    public string? PrintTemplate { get; set; }
    /// <summary>
    /// 适用工厂代码
    /// </summary>
    public string? ApplicablePlantCodes { get; set; }
    /// <summary>
    /// 适用公司代码
    /// </summary>
    public string? ApplicableCompanyCodes { get; set; }
    /// <summary>
    /// 适用部门
    /// </summary>
    public string? ApplicableDepartment { get; set; }
    /// <summary>
    /// 适用角色
    /// </summary>
    public string? ApplicableRoles { get; set; }
    /// <summary>
    /// 开发类
    /// </summary>
    public string? DevelopmentClass { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    public string? Author { get; set; }
    /// <summary>
    /// 版本
    /// </summary>
    public string? Version { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建报表方案表DTO
/// </summary>
public partial class TaktReportSchemeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportSchemeCreateDto()
    {
        ReportCode = string.Empty;
        ReportName = string.Empty;
        ReportCategory = string.Empty;
        ApplicationModule = string.Empty;
        ReportDescription = string.Empty;
        SelectionScreenConfig = string.Empty;
        DataSourceType = string.Empty;
        DataSourceName = string.Empty;
        SqlQuery = string.Empty;
        OutputType = string.Empty;
        AlvColumnConfig = string.Empty;
        DefaultLayoutVariant = string.Empty;
        SubtotalFields = string.Empty;
        SortFields = string.Empty;
        FilterConfig = string.Empty;
        DrillDownReportCode = string.Empty;
        ExportFormats = string.Empty;
        PrintTemplate = string.Empty;
        ApplicablePlantCodes = string.Empty;
        ApplicableCompanyCodes = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicableRoles = string.Empty;
        DevelopmentClass = string.Empty;
        Author = string.Empty;
        Version = string.Empty;
    }

        /// <summary>
    /// 报表编码
    /// </summary>
    public string ReportCode { get; set; }

        /// <summary>
    /// 报表名称
    /// </summary>
    public string ReportName { get; set; }

        /// <summary>
    /// 报表类别
    /// </summary>
    public string ReportCategory { get; set; }

        /// <summary>
    /// 应用模块
    /// </summary>
    public string ApplicationModule { get; set; }

        /// <summary>
    /// 报表描述
    /// </summary>
    public string ReportDescription { get; set; }

        /// <summary>
    /// 选择屏幕配置
    /// </summary>
    public string SelectionScreenConfig { get; set; }

        /// <summary>
    /// 数据源类型
    /// </summary>
    public string DataSourceType { get; set; }

        /// <summary>
    /// 数据源名称
    /// </summary>
    public string DataSourceName { get; set; }

        /// <summary>
    /// SQL查询语句
    /// </summary>
    public string SqlQuery { get; set; }

        /// <summary>
    /// 输出类型
    /// </summary>
    public string OutputType { get; set; }

        /// <summary>
    /// ALV列配置
    /// </summary>
    public string AlvColumnConfig { get; set; }

        /// <summary>
    /// 默认布局变式
    /// </summary>
    public string DefaultLayoutVariant { get; set; }

        /// <summary>
    /// 是否支持布局变式
    /// </summary>
    public int SupportLayoutVariant { get; set; }

        /// <summary>
    /// 小计字段配置
    /// </summary>
    public string SubtotalFields { get; set; }

        /// <summary>
    /// 排序字段配置
    /// </summary>
    public string SortFields { get; set; }

        /// <summary>
    /// 过滤条件配置
    /// </summary>
    public string FilterConfig { get; set; }

        /// <summary>
    /// 是否支持总计
    /// </summary>
    public int SupportTotal { get; set; }

        /// <summary>
    /// 是否支持小计
    /// </summary>
    public int SupportSubtotal { get; set; }

        /// <summary>
    /// 是否支持汇总
    /// </summary>
    public int SupportAggregation { get; set; }

        /// <summary>
    /// 是否支持钻取
    /// </summary>
    public int SupportDrillDown { get; set; }

        /// <summary>
    /// 钻取目标报表编码
    /// </summary>
    public string DrillDownReportCode { get; set; }

        /// <summary>
    /// 是否支持后台执行
    /// </summary>
    public int SupportBackground { get; set; }

        /// <summary>
    /// 是否支持变式保存
    /// </summary>
    public int SupportVariantSave { get; set; }

        /// <summary>
    /// 默认页大小
    /// </summary>
    public int DefaultPageSize { get; set; }

        /// <summary>
    /// 最大数据行数
    /// </summary>
    public int MaxRowCount { get; set; }

        /// <summary>
    /// 是否支持导出
    /// </summary>
    public int IsExportable { get; set; }

        /// <summary>
    /// 导出格式配置
    /// </summary>
    public string ExportFormats { get; set; }

        /// <summary>
    /// 是否支持打印
    /// </summary>
    public int IsPrintable { get; set; }

        /// <summary>
    /// 打印模板
    /// </summary>
    public string PrintTemplate { get; set; }

        /// <summary>
    /// 适用工厂代码
    /// </summary>
    public string ApplicablePlantCodes { get; set; }

        /// <summary>
    /// 适用公司代码
    /// </summary>
    public string ApplicableCompanyCodes { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用角色
    /// </summary>
    public string ApplicableRoles { get; set; }

        /// <summary>
    /// 开发类
    /// </summary>
    public string DevelopmentClass { get; set; }

        /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

        /// <summary>
    /// 版本
    /// </summary>
    public string Version { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

}

/// <summary>
/// Takt更新报表方案表DTO
/// </summary>
public partial class TaktReportSchemeUpdateDto : TaktReportSchemeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportSchemeUpdateDto()
    {
    }

        /// <summary>
    /// 报表方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReportSchemeId { get; set; } = 0;
}

/// <summary>
/// 报表方案表状态DTO
/// </summary>
public partial class TaktReportSchemeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportSchemeStatusDto()
    {
    }

        /// <summary>
    /// 报表方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReportSchemeId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 报表方案表排序DTO
/// </summary>
public partial class TaktReportSchemeSortDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportSchemeSortDto()
    {
    }

        /// <summary>
    /// 报表方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReportSchemeId { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 报表方案表导入模板DTO
/// </summary>
public partial class TaktReportSchemeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportSchemeTemplateDto()
    {
        ReportCode = string.Empty;
        ReportName = string.Empty;
        ReportCategory = string.Empty;
        ApplicationModule = string.Empty;
        ReportDescription = string.Empty;
        SelectionScreenConfig = string.Empty;
        DataSourceType = string.Empty;
        DataSourceName = string.Empty;
        SqlQuery = string.Empty;
        OutputType = string.Empty;
        AlvColumnConfig = string.Empty;
        DefaultLayoutVariant = string.Empty;
        SubtotalFields = string.Empty;
        SortFields = string.Empty;
        FilterConfig = string.Empty;
        DrillDownReportCode = string.Empty;
        ExportFormats = string.Empty;
        PrintTemplate = string.Empty;
        ApplicablePlantCodes = string.Empty;
        ApplicableCompanyCodes = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicableRoles = string.Empty;
        DevelopmentClass = string.Empty;
        Author = string.Empty;
        Version = string.Empty;
    }

        /// <summary>
    /// 报表编码
    /// </summary>
    public string ReportCode { get; set; }

        /// <summary>
    /// 报表名称
    /// </summary>
    public string ReportName { get; set; }

        /// <summary>
    /// 报表类别
    /// </summary>
    public string ReportCategory { get; set; }

        /// <summary>
    /// 应用模块
    /// </summary>
    public string ApplicationModule { get; set; }

        /// <summary>
    /// 报表描述
    /// </summary>
    public string ReportDescription { get; set; }

        /// <summary>
    /// 选择屏幕配置
    /// </summary>
    public string SelectionScreenConfig { get; set; }

        /// <summary>
    /// 数据源类型
    /// </summary>
    public string DataSourceType { get; set; }

        /// <summary>
    /// 数据源名称
    /// </summary>
    public string DataSourceName { get; set; }

        /// <summary>
    /// SQL查询语句
    /// </summary>
    public string SqlQuery { get; set; }

        /// <summary>
    /// 输出类型
    /// </summary>
    public string OutputType { get; set; }

        /// <summary>
    /// ALV列配置
    /// </summary>
    public string AlvColumnConfig { get; set; }

        /// <summary>
    /// 默认布局变式
    /// </summary>
    public string DefaultLayoutVariant { get; set; }

        /// <summary>
    /// 是否支持布局变式
    /// </summary>
    public int SupportLayoutVariant { get; set; }

        /// <summary>
    /// 小计字段配置
    /// </summary>
    public string SubtotalFields { get; set; }

        /// <summary>
    /// 排序字段配置
    /// </summary>
    public string SortFields { get; set; }

        /// <summary>
    /// 过滤条件配置
    /// </summary>
    public string FilterConfig { get; set; }

        /// <summary>
    /// 是否支持总计
    /// </summary>
    public int SupportTotal { get; set; }

        /// <summary>
    /// 是否支持小计
    /// </summary>
    public int SupportSubtotal { get; set; }

        /// <summary>
    /// 是否支持汇总
    /// </summary>
    public int SupportAggregation { get; set; }

        /// <summary>
    /// 是否支持钻取
    /// </summary>
    public int SupportDrillDown { get; set; }

        /// <summary>
    /// 钻取目标报表编码
    /// </summary>
    public string DrillDownReportCode { get; set; }

        /// <summary>
    /// 是否支持后台执行
    /// </summary>
    public int SupportBackground { get; set; }

        /// <summary>
    /// 是否支持变式保存
    /// </summary>
    public int SupportVariantSave { get; set; }

        /// <summary>
    /// 默认页大小
    /// </summary>
    public int DefaultPageSize { get; set; }

        /// <summary>
    /// 最大数据行数
    /// </summary>
    public int MaxRowCount { get; set; }

        /// <summary>
    /// 是否支持导出
    /// </summary>
    public int IsExportable { get; set; }

        /// <summary>
    /// 导出格式配置
    /// </summary>
    public string ExportFormats { get; set; }

        /// <summary>
    /// 是否支持打印
    /// </summary>
    public int IsPrintable { get; set; }

        /// <summary>
    /// 打印模板
    /// </summary>
    public string PrintTemplate { get; set; }

        /// <summary>
    /// 适用工厂代码
    /// </summary>
    public string ApplicablePlantCodes { get; set; }

        /// <summary>
    /// 适用公司代码
    /// </summary>
    public string ApplicableCompanyCodes { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用角色
    /// </summary>
    public string ApplicableRoles { get; set; }

        /// <summary>
    /// 开发类
    /// </summary>
    public string DevelopmentClass { get; set; }

        /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

        /// <summary>
    /// 版本
    /// </summary>
    public string Version { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 报表方案表导入DTO
/// </summary>
public partial class TaktReportSchemeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportSchemeImportDto()
    {
        ReportCode = string.Empty;
        ReportName = string.Empty;
        ReportCategory = string.Empty;
        ApplicationModule = string.Empty;
        ReportDescription = string.Empty;
        SelectionScreenConfig = string.Empty;
        DataSourceType = string.Empty;
        DataSourceName = string.Empty;
        SqlQuery = string.Empty;
        OutputType = string.Empty;
        AlvColumnConfig = string.Empty;
        DefaultLayoutVariant = string.Empty;
        SubtotalFields = string.Empty;
        SortFields = string.Empty;
        FilterConfig = string.Empty;
        DrillDownReportCode = string.Empty;
        ExportFormats = string.Empty;
        PrintTemplate = string.Empty;
        ApplicablePlantCodes = string.Empty;
        ApplicableCompanyCodes = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicableRoles = string.Empty;
        DevelopmentClass = string.Empty;
        Author = string.Empty;
        Version = string.Empty;
    }

        /// <summary>
    /// 报表编码
    /// </summary>
    public string ReportCode { get; set; }

        /// <summary>
    /// 报表名称
    /// </summary>
    public string ReportName { get; set; }

        /// <summary>
    /// 报表类别
    /// </summary>
    public string ReportCategory { get; set; }

        /// <summary>
    /// 应用模块
    /// </summary>
    public string ApplicationModule { get; set; }

        /// <summary>
    /// 报表描述
    /// </summary>
    public string ReportDescription { get; set; }

        /// <summary>
    /// 选择屏幕配置
    /// </summary>
    public string SelectionScreenConfig { get; set; }

        /// <summary>
    /// 数据源类型
    /// </summary>
    public string DataSourceType { get; set; }

        /// <summary>
    /// 数据源名称
    /// </summary>
    public string DataSourceName { get; set; }

        /// <summary>
    /// SQL查询语句
    /// </summary>
    public string SqlQuery { get; set; }

        /// <summary>
    /// 输出类型
    /// </summary>
    public string OutputType { get; set; }

        /// <summary>
    /// ALV列配置
    /// </summary>
    public string AlvColumnConfig { get; set; }

        /// <summary>
    /// 默认布局变式
    /// </summary>
    public string DefaultLayoutVariant { get; set; }

        /// <summary>
    /// 是否支持布局变式
    /// </summary>
    public int SupportLayoutVariant { get; set; }

        /// <summary>
    /// 小计字段配置
    /// </summary>
    public string SubtotalFields { get; set; }

        /// <summary>
    /// 排序字段配置
    /// </summary>
    public string SortFields { get; set; }

        /// <summary>
    /// 过滤条件配置
    /// </summary>
    public string FilterConfig { get; set; }

        /// <summary>
    /// 是否支持总计
    /// </summary>
    public int SupportTotal { get; set; }

        /// <summary>
    /// 是否支持小计
    /// </summary>
    public int SupportSubtotal { get; set; }

        /// <summary>
    /// 是否支持汇总
    /// </summary>
    public int SupportAggregation { get; set; }

        /// <summary>
    /// 是否支持钻取
    /// </summary>
    public int SupportDrillDown { get; set; }

        /// <summary>
    /// 钻取目标报表编码
    /// </summary>
    public string DrillDownReportCode { get; set; }

        /// <summary>
    /// 是否支持后台执行
    /// </summary>
    public int SupportBackground { get; set; }

        /// <summary>
    /// 是否支持变式保存
    /// </summary>
    public int SupportVariantSave { get; set; }

        /// <summary>
    /// 默认页大小
    /// </summary>
    public int DefaultPageSize { get; set; }

        /// <summary>
    /// 最大数据行数
    /// </summary>
    public int MaxRowCount { get; set; }

        /// <summary>
    /// 是否支持导出
    /// </summary>
    public int IsExportable { get; set; }

        /// <summary>
    /// 导出格式配置
    /// </summary>
    public string ExportFormats { get; set; }

        /// <summary>
    /// 是否支持打印
    /// </summary>
    public int IsPrintable { get; set; }

        /// <summary>
    /// 打印模板
    /// </summary>
    public string PrintTemplate { get; set; }

        /// <summary>
    /// 适用工厂代码
    /// </summary>
    public string ApplicablePlantCodes { get; set; }

        /// <summary>
    /// 适用公司代码
    /// </summary>
    public string ApplicableCompanyCodes { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用角色
    /// </summary>
    public string ApplicableRoles { get; set; }

        /// <summary>
    /// 开发类
    /// </summary>
    public string DevelopmentClass { get; set; }

        /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

        /// <summary>
    /// 版本
    /// </summary>
    public string Version { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 报表方案表导出DTO
/// </summary>
public partial class TaktReportSchemeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportSchemeExportDto()
    {
        CreatedAt = DateTime.Now;
        ReportCode = string.Empty;
        ReportName = string.Empty;
        ReportCategory = string.Empty;
        ApplicationModule = string.Empty;
        ReportDescription = string.Empty;
        SelectionScreenConfig = string.Empty;
        DataSourceType = string.Empty;
        DataSourceName = string.Empty;
        SqlQuery = string.Empty;
        OutputType = string.Empty;
        AlvColumnConfig = string.Empty;
        DefaultLayoutVariant = string.Empty;
        SubtotalFields = string.Empty;
        SortFields = string.Empty;
        FilterConfig = string.Empty;
        DrillDownReportCode = string.Empty;
        ExportFormats = string.Empty;
        PrintTemplate = string.Empty;
        ApplicablePlantCodes = string.Empty;
        ApplicableCompanyCodes = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicableRoles = string.Empty;
        DevelopmentClass = string.Empty;
        Author = string.Empty;
        Version = string.Empty;
    }

        /// <summary>
    /// 报表编码
    /// </summary>
    public string ReportCode { get; set; }

        /// <summary>
    /// 报表名称
    /// </summary>
    public string ReportName { get; set; }

        /// <summary>
    /// 报表类别
    /// </summary>
    public string ReportCategory { get; set; }

        /// <summary>
    /// 应用模块
    /// </summary>
    public string ApplicationModule { get; set; }

        /// <summary>
    /// 报表描述
    /// </summary>
    public string ReportDescription { get; set; }

        /// <summary>
    /// 选择屏幕配置
    /// </summary>
    public string SelectionScreenConfig { get; set; }

        /// <summary>
    /// 数据源类型
    /// </summary>
    public string DataSourceType { get; set; }

        /// <summary>
    /// 数据源名称
    /// </summary>
    public string DataSourceName { get; set; }

        /// <summary>
    /// SQL查询语句
    /// </summary>
    public string SqlQuery { get; set; }

        /// <summary>
    /// 输出类型
    /// </summary>
    public string OutputType { get; set; }

        /// <summary>
    /// ALV列配置
    /// </summary>
    public string AlvColumnConfig { get; set; }

        /// <summary>
    /// 默认布局变式
    /// </summary>
    public string DefaultLayoutVariant { get; set; }

        /// <summary>
    /// 是否支持布局变式
    /// </summary>
    public int SupportLayoutVariant { get; set; }

        /// <summary>
    /// 小计字段配置
    /// </summary>
    public string SubtotalFields { get; set; }

        /// <summary>
    /// 排序字段配置
    /// </summary>
    public string SortFields { get; set; }

        /// <summary>
    /// 过滤条件配置
    /// </summary>
    public string FilterConfig { get; set; }

        /// <summary>
    /// 是否支持总计
    /// </summary>
    public int SupportTotal { get; set; }

        /// <summary>
    /// 是否支持小计
    /// </summary>
    public int SupportSubtotal { get; set; }

        /// <summary>
    /// 是否支持汇总
    /// </summary>
    public int SupportAggregation { get; set; }

        /// <summary>
    /// 是否支持钻取
    /// </summary>
    public int SupportDrillDown { get; set; }

        /// <summary>
    /// 钻取目标报表编码
    /// </summary>
    public string DrillDownReportCode { get; set; }

        /// <summary>
    /// 是否支持后台执行
    /// </summary>
    public int SupportBackground { get; set; }

        /// <summary>
    /// 是否支持变式保存
    /// </summary>
    public int SupportVariantSave { get; set; }

        /// <summary>
    /// 默认页大小
    /// </summary>
    public int DefaultPageSize { get; set; }

        /// <summary>
    /// 最大数据行数
    /// </summary>
    public int MaxRowCount { get; set; }

        /// <summary>
    /// 是否支持导出
    /// </summary>
    public int IsExportable { get; set; }

        /// <summary>
    /// 导出格式配置
    /// </summary>
    public string ExportFormats { get; set; }

        /// <summary>
    /// 是否支持打印
    /// </summary>
    public int IsPrintable { get; set; }

        /// <summary>
    /// 打印模板
    /// </summary>
    public string PrintTemplate { get; set; }

        /// <summary>
    /// 适用工厂代码
    /// </summary>
    public string ApplicablePlantCodes { get; set; }

        /// <summary>
    /// 适用公司代码
    /// </summary>
    public string ApplicableCompanyCodes { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用角色
    /// </summary>
    public string ApplicableRoles { get; set; }

        /// <summary>
    /// 开发类
    /// </summary>
    public string DevelopmentClass { get; set; }

        /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

        /// <summary>
    /// 版本
    /// </summary>
    public string Version { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}