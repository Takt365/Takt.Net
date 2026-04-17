// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Code.Generator.CodeEngine
// 文件名称：TaktGenTemplateContext.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：代码生成模板上下文，供 Scriban 模板绑定；与 Domain 实体 TaktGenTable、TaktGenTableColumn 一一对应，可从两实体构建
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json.Linq;
using Takt.Domain.Entities.Code.Generator;

namespace Takt.Application.Services.Code.Generator.CodeEngine;

/// <summary>
/// 控制器 Action 描述符，由 GenFunction 解析后生成，供模板循环渲染，不写死功能关键字。
/// </summary>
public class TaktGenControllerActionDescriptor
{
    /// <summary>Action 摘要说明（如：获取xxx列表（分页））</summary>
    public string Summary { get; set; } = string.Empty;

    /// <summary>HTTP 方法（如 HttpGet、HttpPost）</summary>
    public string HttpMethod { get; set; } = string.Empty;

    /// <summary>路由（如 list、{id}、batch）</summary>
    public string Route { get; set; } = string.Empty;

    /// <summary>权限键（如 app:table:list）</summary>
    public string PermissionKey { get; set; } = string.Empty;

    /// <summary>权限显示名（如：查询列表）</summary>
    public string PermissionName { get; set; } = string.Empty;

    /// <summary>方法名（如 GetListAsync）</summary>
    public string MethodName { get; set; } = string.Empty;

    /// <summary>方法参数签名（如 [FromQuery] xxxQueryDto queryDto）</summary>
    public string Signature { get; set; } = string.Empty;

    /// <summary>返回类型（如 TaktPagedResult&lt;EntityDto&gt;、IActionResult）</summary>
    public string ResponseType { get; set; } = string.Empty;

    /// <summary>方法体代码（多行字符串）</summary>
    public string Body { get; set; } = string.Empty;

    /// <summary>前端 API 方法名（驼峰，如 getList、getById、create、update、remove、deleteBatch）</summary>
    public string FrontendMethodName { get; set; } = string.Empty;

    /// <summary>前端方法参数签名（如 params: DeptQuery、id: string、data: DeptCreate、id: string, data: DeptUpdate、ids: string[]）</summary>
    public string FrontendSignature { get; set; } = string.Empty;

    /// <summary>前端返回类型（如 Promise&lt;TaktPagedResult&lt;Dept&gt;&gt;、Promise&lt;Dept&gt;、Promise&lt;void&gt;）</summary>
    public string FrontendReturnType { get; set; } = string.Empty;

    /// <summary>前端请求体键（params、data、data: ids 或空，供 request 配置）</summary>
    public string FrontendRequestKey { get; set; } = string.Empty;
}

/// <summary>
/// 服务方法描述符，由 GenFunction 解析后生成，供模板循环渲染，不写死功能关键字。
/// </summary>
public class TaktGenServiceMethodDescriptor
{
    /// <summary>方法摘要说明（如：获取xxx列表（分页））</summary>
    public string Summary { get; set; } = string.Empty;

    /// <summary>XML 注释中的 param/returns 片段（供接口声明用）</summary>
    public string ParamsXml { get; set; } = string.Empty;

    /// <summary>方法名（如 GetListAsync、QueryExpression）</summary>
    public string MethodName { get; set; } = string.Empty;

    /// <summary>方法参数签名（如 xxxQueryDto queryDto）</summary>
    public string Signature { get; set; } = string.Empty;

    /// <summary>返回类型（如 Task&lt;TaktPagedResult&lt;EntityDto&gt;&gt;）</summary>
    public string ReturnType { get; set; } = string.Empty;

    /// <summary>方法体代码（多行字符串）</summary>
    public string Body { get; set; } = string.Empty;

    /// <summary>是否为私有方法（如 QueryExpression）</summary>
    public bool IsPrivate { get; set; }
}

/// <summary>
/// DTO 类别描述符，由 DtoCategory JSON 数组项反序列化得到，供模板按 BodyKind/BaseClass 生成，不写死类型名。
/// </summary>
public class TaktDtoCategoryDescriptor
{
    /// <summary>类别名（如 Dto、QueryDto、CreateDto、StatusDto）</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>主体形态：Full=Id+列，NoId=仅列，OnlyId=仅Id（需基类），Query=继承 TaktPagedQuery+列</summary>
    public string BodyKind { get; set; } = "NoId";

    /// <summary>基类全名（可选）；相对名如 CreateDto 会在构建时解析为 EntityClassName+CreateDto</summary>
    public string? BaseClass { get; set; }

    /// <summary>前端 TypeScript 接口名（如 Dept、DeptQuery、DeptCreate、DeptUpdate、DeptStatus）</summary>
    public string TsInterfaceName { get; set; } = string.Empty;

    /// <summary>前端 extends 类型名（如 TaktEntityBase、TaktPagedQuery、DeptCreate；空表示不继承）</summary>
    public string TsExtendsName { get; set; } = string.Empty;
}

/// <summary>翻译 SQL 单行数据，供 menu_and_translation.sql 模板使用，每行 Id 由 SnowFlakeSingle.Instance.NextId() 生成。ResourceGroup：菜单标题为 menu，其它字段为 page。</summary>
public class TaktSqlTranslationRowItem
{
    public long Id { get; set; }
    public string Culture { get; set; } = string.Empty;
    public string ResourceKey { get; set; } = string.Empty;
    public string TranslationValue { get; set; } = string.Empty;
    /// <summary>资源分组：menu=菜单翻译，page=页面字段翻译</summary>
    public string ResourceGroup { get; set; } = "page";
    public int OrderNum { get; set; }
}

/// <summary>
/// 代码生成模板上下文：表配置 + 列配置列表，模板中可使用 Table 与 Columns。
/// 使用 <see cref="From"/> 从 <see cref="TaktGenTable"/>、<see cref="TaktGenTableColumn"/> 构建，充分利用两实体全部字段。
/// </summary>
public class TaktGenTemplateContext
{
    /// <summary>
    /// 表配置（模板中通过 Table 访问，如 {{ Table.TableName }}、{{ Table.EntityClassName }}）
    /// </summary>
    public TaktGenTableTemplateModel Table { get; set; } = new();

    /// <summary>
    /// 列配置列表（模板中通过 Columns 访问，如 {{ for col in Columns }} {{ col.CsharpColumnName }} {{ end }}）
    /// </summary>
    public List<TaktGenColumnTemplateModel> Columns { get; set; } = new();

    /// <summary>
    /// 从 Domain 实体构建模板上下文，充分利用 <see cref="TaktGenTable"/> 与 <see cref="TaktGenTableColumn"/> 全部字段。
    /// </summary>
    /// <param name="table">代码生成表配置实体</param>
    /// <param name="columns">该表下的字段配置列表（可为空）</param>
    /// <returns>供 Scriban 使用的模板上下文</returns>
    public static TaktGenTemplateContext From(TaktGenTable table, IEnumerable<TaktGenTableColumn>? columns = null)
    {
        if (table == null)
            throw new ArgumentNullException(nameof(table));
        var ctx = new TaktGenTemplateContext
        {
            Table = TaktGenTableTemplateModel.From(table) ?? new TaktGenTableTemplateModel(),
            Columns = (columns ?? Array.Empty<TaktGenTableColumn>())
                .OrderBy(c => c.OrderNum)
                .Select(TaktGenColumnTemplateModel.From)
                .ToList()
        };
        return ctx;
    }
}

/// <summary>
/// 表级模板模型，与 <see cref="TaktGenTable"/> 实体字段一一对应，便于 Scriban 绑定并充分利用实体全部属性。
/// </summary>
public class TaktGenTableTemplateModel
{
    /// <summary>数据源（前面是数据库名称，后面是 ConfigId，如：Takt_Identity_Dev:0）</summary>
    public string? DataSource { get; set; }

    /// <summary>数据表名称</summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>表描述（表注释）</summary>
    public string? TableComment { get; set; }

    /// <summary>表注释展示值（TableComment ?? GenBusinessName，模板中直接用 Table.Comment）</summary>
    public string Comment => string.IsNullOrWhiteSpace(TableComment) ? (GenBusinessName ?? string.Empty) : TableComment;

    /// <summary>关联父表名（用于主子表）</summary>
    public string? SubTableName { get; set; }

    /// <summary>本表关联父表的外键名（用于主子表）</summary>
    public string? SubTableFkName { get; set; }

    /// <summary>树编码字段（用于树形结构）</summary>
    public string? TreeCode { get; set; }

    /// <summary>树父编码字段（用于树形结构）</summary>
    public string? TreeParentCode { get; set; }

    /// <summary>树名称字段（用于树形结构）</summary>
    public string? TreeName { get; set; }

    /// <summary>是否在数据库中（1=是库表，0=不是库表）</summary>
    public int InDatabase { get; set; }

    /// <summary>生成模板类型（crud=单表，tree=树表，sub=主子表）</summary>
    public string GenTemplate { get; set; } = "crud";

    /// <summary>命名空间前缀（用于生成类名、方法名等的前缀）</summary>
    public string? NamePrefix { get; set; }

    /// <summary>实体命名空间</summary>
    public string? EntityNamespace { get; set; }

    /// <summary>实体类名称（首字母大写，驼峰命名）</summary>
    public string EntityClassName { get; set; } = string.Empty;

    /// <summary>实体名帕斯卡（前端用，如 TaktDept→Dept）</summary>
    public string EntityNamePascal { get; set; } = string.Empty;

    /// <summary>实体名驼峰（前端用，如 Dept→dept）</summary>
    public string EntityNameCamel { get; set; } = string.Empty;

    /// <summary>实体名 kebab（前端用，如 StandardWageRate→standard-wage-rate，用于表单组件文件名等）</summary>
    public string EntityNameKebab { get; set; } = string.Empty;

    /// <summary>API 基础路径（如 /api/TaktDepts）</summary>
    public string ApiBasePath { get; set; } = string.Empty;

    /// <summary>传输对象 Dto 命名空间</summary>
    public string? DtoNamespace { get; set; }

    /// <summary>传输对象 Dto 类名</summary>
    public string? DtoClassName { get; set; }

    /// <summary>传输对象 Dto 类别（Dto、QueryDto、CreateDto、UpdateDto 等）</summary>
    public string? DtoCategory { get; set; }

    /// <summary>传输对象 Dto 类别列表（由 DtoCategory 按逗号拆分，供模板循环）</summary>
    public List<string> DtoCategories { get; set; } = new();

    /// <summary>传输对象 Dto 类别描述列表（含名称、基类、BodyKind，供模板按数据循环生成，不写死类型名）</summary>
    public List<TaktDtoCategoryDescriptor> DtoCategoryDescriptors { get; set; } = new();

    /// <summary>服务命名空间</summary>
    public string? ServiceNamespace { get; set; }

    /// <summary>服务接口类名称</summary>
    public string? IServiceClassName { get; set; }

    /// <summary>服务类名称</summary>
    public string? ServiceClassName { get; set; }

    /// <summary>控制器命名空间</summary>
    public string? ControllerNamespace { get; set; }

    /// <summary>控制器类名称</summary>
    public string? ControllerClassName { get; set; }

    /// <summary>仓储接口命名空间</summary>
    public string? RepositoryInterfaceNamespace { get; set; }

    /// <summary>仓储接口类名称</summary>
    public string? IRepositoryClassName { get; set; }

    /// <summary>仓储命名空间</summary>
    public string? RepositoryNamespace { get; set; }

    /// <summary>仓储类名称</summary>
    public string? RepositoryClassName { get; set; }

    /// <summary>模块名（功能模块名称）</summary>
    public string? GenModuleName { get; set; }

    /// <summary>ApiModule 顶级业务领域键（GenModuleName 首段 PascalCase，如 accounting_financial→Accounting）</summary>
    public string ApiModuleKey { get; set; } = "Default";

    /// <summary>ApiModule 顶级业务领域名称（与 ApiModuleKey 对应的中文显示名，须与 SwaggerDoc/OpenAPI 分组 Title 一致，如 Accounting→会计核算）</summary>
    public string ApiModuleName { get; set; } = "通用";

    /// <summary>前端模块路径（GenModuleName 转小写且下划线改 /，如 accounting_financial→accounting/financial，用于 import 路径）</summary>
    public string FrontendModulePath { get; set; } = string.Empty;

    /// <summary>API 文件中 import request 的相对路径（根据模块深度，如 ../request 或 ../../request）</summary>
    public string RequestImportPath { get; set; } = "../request";

    /// <summary>业务名（用于类名，如 Company，与模块拼接为 Takt.模块+类名）</summary>
    public string GenBusinessName { get; set; } = string.Empty;

    /// <summary>功能名（用于接口与注释的中文名称，如：公司、部门）</summary>
    public string? GenFunctionName { get; set; }

    /// <summary>生成功能（查询，新增，更新，删除，模板，导入，导出）</summary>
    public string? GenFunction { get; set; }

    /// <summary>是否生成查询相关（0=否，1=是，由 GenFunction 解析）</summary>
    public int IsQuery { get; set; }

    /// <summary>是否生成新增相关（0=否，1=是，由 GenFunction 解析）</summary>
    public int IsCreate { get; set; }

    /// <summary>是否生成更新相关（0=否，1=是，由 GenFunction 解析）</summary>
    public int IsUpdate { get; set; }

    /// <summary>是否生成删除相关（0=否，1=是，由 GenFunction 解析）</summary>
    public int IsDelete { get; set; }

    /// <summary>是否生成模板相关（0=否，1=是，由 GenFunction 解析）</summary>
    public int IsTemplate { get; set; }

    /// <summary>是否生成导入相关（0=否，1=是，由 GenFunction 解析）</summary>
    public int IsImport { get; set; }

    /// <summary>是否生成导出相关（0=否，1=是，由 GenFunction 解析）</summary>
    public int IsExport { get; set; }

    /// <summary>生成代码方式（0=zip 压缩包，1=自定义路径，2=当前项目）</summary>
    public int GenMethod { get; set; } = 0;

    /// <summary>是否生成仓储层（1=是，0=否）</summary>
    public int IsRepository { get; set; } = 1;

    /// <summary>生成路径（默认为项目根目录）</summary>
    public string GenPath { get; set; } = "/";

    /// <summary>上级菜单 ID</summary>
    public long ParentMenuId { get; set; }

    /// <summary>是否生成菜单（1=是，0=否）</summary>
    public int IsGenMenu { get; set; }

    /// <summary>是否生成翻译（1=是，0=否）</summary>
    public int IsGenTranslation { get; set; }

    /// <summary>排序类型（asc=升序，desc=降序）</summary>
    public string SortType { get; set; } = "asc";

    /// <summary>排序字段（实体列名，帕斯卡）</summary>
    public string SortField { get; set; } = string.Empty;

    /// <summary>排序字段驼峰（供前端 API 使用，如 CreatedAt→createdAt）</summary>
    public string TsSortField { get; set; } = string.Empty;

    /// <summary>权限前缀</summary>
    public string PermsPrefix { get; set; } = string.Empty;

    /// <summary>前端模板（1=element plus，2=ant design vue）</summary>
    public int FrontTemplate { get; set; } = 1;

    /// <summary>前端样式（12=一行一列，24=一行两列）</summary>
    public int FrontStyle { get; set; } = 24;

    /// <summary>操作按钮样式（0=文本，1=标准）</summary>
    public int BtnStyle { get; set; } = 1;

    /// <summary>是否生成代码（1=是，0=否）</summary>
    public int IsGenCode { get; set; } = 1;

    /// <summary>代码生成次数（每次生成成功后自增）</summary>
    public int GenCodeCount { get; set; } = 0;

    /// <summary>是否使用 tabs（1=是，0=否）</summary>
    public int IsUseTabs { get; set; } = 1;

    /// <summary>tabs 标签中字段的数量</summary>
    public int TabsFieldCount { get; set; } = 10;

    /// <summary>作者</summary>
    public string GenAuthor { get; set; } = string.Empty;

    /// <summary>SQL 创建人（生成菜单/翻译 SQL 时写入 create_by，如 admin、user01；由生成时当前登录用户名或 GenAuthor 填充）</summary>
    public string SqlCreateBy { get; set; } = "admin";

    /// <summary>菜单 SQL 的雪花 ID（IsGenMenu=0 时由 SnowFlakeSingle.Instance.NextId() 生成，供 INSERT id 列）</summary>
    public long? SqlMenuId { get; set; }

    /// <summary>翻译 SQL 行列表（IsGenTranslation=0 时按模板顺序预生成，每行含雪花 Id、culture、resource_key、translation_value、order_num）</summary>
    public List<TaktSqlTranslationRowItem> SqlTranslationRows { get; set; } = new();

    /// <summary>其他生成选项（JSON 格式）</summary>
    public string? Options { get; set; }

    /// <summary>控制器 Action 列表，由 GenFunction 解析生成，模板仅循环渲染</summary>
    public List<TaktGenControllerActionDescriptor> ControllerActions { get; set; } = new();

    /// <summary>服务方法列表（接口+实现），由 GenFunction 解析生成，模板仅循环渲染</summary>
    public List<TaktGenServiceMethodDescriptor> ServiceMethods { get; set; } = new();

    /// <summary>私有方法列表（如 QueryExpression），由 GenFunction 解析生成</summary>
    public List<TaktGenServiceMethodDescriptor> ServicePrivateMethods { get; set; } = new();

    /// <summary>
    /// 将 DtoCategory 的 JSON 转为描述符列表。支持：JSON 对象 {"主传输对象":"Dto","查询传输对象":"QueryDto",...}（取 value 为 DTO 类后缀）；JSON 数组 ["Dto","QueryDto",...] 或 [{"Name":"x","BodyKind":"NoId"},...]；逗号分隔。相对 BaseClass（不含.）解析为 EntityClassName+BaseClass。TsInterfaceName 由 entityNamePascal 与 Name 推导。
    /// </summary>
    /// <param name="entityClassName">实体类名（用于解析相对基类）</param>
    /// <param name="dtoCategory">DtoCategory 原始字符串（JSON 对象/数组或逗号分隔）</param>
    /// <param name="entityNamePascal">前端实体帕斯卡名（用于 TsInterfaceName，如 Dept）</param>
    /// <returns>DTO 类别描述符列表，供模板循环生成</returns>
    private static List<TaktDtoCategoryDescriptor> BuildDtoCategoryDescriptors(string entityClassName, string? dtoCategory, string entityNamePascal)
    {
        if (string.IsNullOrWhiteSpace(dtoCategory))
            return new List<TaktDtoCategoryDescriptor>();

        var trimmed = dtoCategory.Trim();

        if (trimmed.StartsWith('{'))
        {
            try
            {
                var jobj = JObject.Parse(trimmed);
                var list = new List<TaktDtoCategoryDescriptor>();
                foreach (var prop in jobj.Properties())
                {
                    var name = prop.Value?.Type == JTokenType.String ? prop.Value.ToString() : prop.Value?.ToString() ?? "";
                    if (string.IsNullOrWhiteSpace(name)) continue;
                    var d = InferDescriptor(name.Trim(), entityClassName);
                    d.TsInterfaceName = GetTsInterfaceName(name.Trim(), entityNamePascal);
                    d.TsExtendsName = GetTsExtendsName(d.BodyKind, entityNamePascal);
                    list.Add(d);
                }
                return list;
            }
            catch { }
        }

        if (!trimmed.StartsWith('['))
        {
            var names = trimmed.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).Where(s => s.Length > 0).ToList();
            return names.Select(name =>
            {
                var d = InferDescriptor(name, entityClassName);
                d.TsInterfaceName = GetTsInterfaceName(name, entityNamePascal);
                d.TsExtendsName = GetTsExtendsName(d.BodyKind, entityNamePascal);
                return d;
            }).ToList();
        }

        try
        {
            var arr = JArray.Parse(trimmed);
            var list = new List<TaktDtoCategoryDescriptor>();
            foreach (var el in arr)
            {
                if (el?.Type == JTokenType.String)
                {
                    var name = el.ToString();
                    if (string.IsNullOrEmpty(name)) continue;
                    var d = InferDescriptor(name, entityClassName);
                    d.TsInterfaceName = GetTsInterfaceName(name, entityNamePascal);
                    d.TsExtendsName = GetTsExtendsName(d.BodyKind, entityNamePascal);
                    list.Add(d);
                }
                else if (el is JObject jo)
                {
                    var name = jo["Name"]?.ToString() ?? "";
                    if (string.IsNullOrEmpty(name)) continue;
                    var bodyKind = jo["BodyKind"]?.ToString() ?? "";
                    var baseClass = jo["BaseClass"]?.ToString();
                    if (!string.IsNullOrEmpty(bodyKind) || baseClass != null)
                    {
                        if (!string.IsNullOrEmpty(baseClass) && !baseClass.Contains('.'))
                            baseClass = entityClassName + baseClass;
                        var tsName = GetTsInterfaceName(name, entityNamePascal);
                        var tsExt = GetTsExtendsName(bodyKind, entityNamePascal);
                        list.Add(new TaktDtoCategoryDescriptor { Name = name, BodyKind = bodyKind, BaseClass = baseClass, TsInterfaceName = tsName, TsExtendsName = tsExt });
                    }
                    else
                    {
                        var d = InferDescriptor(name, entityClassName);
                        d.TsInterfaceName = GetTsInterfaceName(name, entityNamePascal);
                        d.TsExtendsName = GetTsExtendsName(d.BodyKind, entityNamePascal);
                        list.Add(d);
                    }
                }
            }
            return list;
        }
        catch
        {
            var names = trimmed.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).Where(s => s.Length > 0).ToList();
            return names.Select(name =>
            {
                var d = InferDescriptor(name, entityClassName);
                d.TsInterfaceName = GetTsInterfaceName(name, entityNamePascal);
                d.TsExtendsName = GetTsExtendsName(d.BodyKind, entityNamePascal);
                return d;
            }).ToList();
        }
    }

    /// <summary>根据 BodyKind 返回前端 extends 类型名。</summary>
    private static string GetTsExtendsName(string bodyKind, string entityNamePascal)
    {
        return bodyKind switch
        {
            "Full" => "TaktEntityBase",
            "Query" => "TaktPagedQuery",
            "OnlyId" => entityNamePascal + "Create",
            _ => ""
        };
    }

    /// <summary>DTO 类别名转前端 TypeScript 接口名（如 Dto→Dept，QueryDto→DeptQuery）。</summary>
    private static string GetTsInterfaceName(string dtoName, string entityNamePascal)
    {
        if (string.IsNullOrEmpty(dtoName)) return entityNamePascal;
        return dtoName switch
        {
            "Dto" => entityNamePascal,
            "QueryDto" => entityNamePascal + "Query",
            "CreateDto" => entityNamePascal + "Create",
            "UpdateDto" => entityNamePascal + "Update",
            "TemplateDto" => entityNamePascal + "Template",
            "ImportDto" => entityNamePascal + "Import",
            "ExportDto" => entityNamePascal + "Export",
            "StatusDto" => entityNamePascal + "Status",
            "SortDto" => entityNamePascal + "Sort",
            _ => entityNamePascal + dtoName.Replace("Dto", "", StringComparison.OrdinalIgnoreCase)
        };
    }

    /// <summary>仅当数组项为字符串时用于推断 DTO 形状。IsCreate/IsUpdate/IsExport/IsQuery/IsUnique 与前端一致；仅 IsList 影响前端。</summary>
    /// <param name="name">DTO 类别名（如 Dto、QueryDto、CreateDto、UpdateDto）</param>
    /// <param name="entityClassName">实体类名（用于解析 UpdateDto 基类等）</param>
    /// <returns>含 Name、BodyKind、BaseClass 的描述符</returns>
    private static TaktDtoCategoryDescriptor InferDescriptor(string name, string entityClassName)
    {
        var (bodyKind, baseClass) = name switch
        {
            "Dto" => ("Full", (string?)null),
            "QueryDto" => ("Query", "TaktPagedQuery"),
            "CreateDto" => ("NoId", (string?)null),
            "UpdateDto" => ("OnlyId", entityClassName + "CreateDto"),
            "TemplateDto" => ("NoId", (string?)null),
            "ImportDto" => ("NoId", (string?)null),
            "ExportDto" => ("Export", (string?)null),
            "StatusDto" => ("NoId", (string?)null),
            "SortDto" => ("NoId", (string?)null),
            _ when name.Contains("Template", StringComparison.OrdinalIgnoreCase) => ("NoId", (string?)null),
            _ => ("Full", (string?)null)
        };
        return new TaktDtoCategoryDescriptor { Name = name, BodyKind = bodyKind, BaseClass = baseClass };
    }

    /// <summary>实体类名转前端帕斯卡名（去掉 Takt 前缀，如 TaktDept→Dept）。</summary>
    private static string ToEntityNamePascal(string? entityClassName)
    {
        if (string.IsNullOrEmpty(entityClassName)) return string.Empty;
        if (entityClassName.StartsWith("Takt", StringComparison.Ordinal) && entityClassName.Length > 4)
            return entityClassName[4..];
        return entityClassName;
    }

    /// <summary>帕斯卡名转驼峰（首字母小写，如 Dept→dept）。</summary>
    private static string ToEntityNameCamel(string pascal)
    {
        if (string.IsNullOrEmpty(pascal)) return string.Empty;
        return char.ToLowerInvariant(pascal[0]) + pascal[1..];
    }

    /// <summary>根据 GenModuleName 生成 API 中 import request 的相对路径（api/{modulePath}/xx.ts 需若干 ../ 回到 api 再取 request）。</summary>
    private static string BuildRequestImportPath(string? genModuleName)
    {
        if (string.IsNullOrWhiteSpace(genModuleName)) return "../request";
        var segmentCount = genModuleName!.Trim().Split(new[] { '_', '/', ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
        return string.Concat(System.Linq.Enumerable.Repeat("../", segmentCount)) + "request";
    }

    /// <summary>帕斯卡名转 kebab（如 StandardWageRate→standard-wage-rate，用于前端表单组件文件名）。</summary>
    private static string ToEntityNameKebab(string? pascal)
    {
        if (string.IsNullOrWhiteSpace(pascal)) return "entity";
        var s = pascal!.Trim();
        if (s.Length == 0) return "entity";
        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < s.Length; i++)
        {
            var c = s[i];
            if (char.IsUpper(c))
            {
                if (i > 0) sb.Append('-');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
                sb.Append(c);
        }
        return sb.ToString();
    }

    /// <summary>排序字段帕斯卡转驼峰（供前端 API，如 CreatedAt→createdAt）。</summary>
    private static string ToTsSortField(string? sortField)
    {
        if (string.IsNullOrEmpty(sortField)) return string.Empty;
        return char.ToLowerInvariant(sortField[0]) + sortField[1..];
    }

    /// <summary>控制器类名转 API 基础路径（去掉 Controller 后缀，如 TaktDeptsController→/api/TaktDepts）。</summary>
    private static string ToApiBasePath(string? controllerClassName)
    {
        if (string.IsNullOrEmpty(controllerClassName)) return "/api";
        var name = controllerClassName.EndsWith("Controller", StringComparison.Ordinal) && controllerClassName.Length >= 10
            ? controllerClassName[..^10]
            : controllerClassName;
        return "/api/" + name;
    }

    private static readonly Dictionary<string, string> ApiModuleTopLevelNames = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Accounting", "会计核算" },
        { "Identity", "身份认证" },
        { "Logistics", "后勤管理" },
        { "Generator", "代码管理" },
        { "Routine", "日常事务" },
        { "Tenant", "租户管理" },
        { "HumanResource", "人力资源" },
        { "Statistics", "统计看板" },
        { "Workflow", "工作流程" },
        { "Controlling", "控制" }
    };

    /// <summary>从 GenModuleName 解析 [ApiModule] 的顶级键与名称（首段 PascalCase + 预定义中文名）。</summary>
    private static (string Key, string Name) GetApiModuleTopLevel(string? genModuleName)
    {
        if (string.IsNullOrWhiteSpace(genModuleName))
            return ("Default", "通用");
        var segment = genModuleName.Trim().Split('_', '.')[0];
        if (string.IsNullOrEmpty(segment))
            return ("Default", "通用");
        var key = char.ToUpperInvariant(segment[0]) + segment[1..].ToLowerInvariant();
        var name = ApiModuleTopLevelNames.TryGetValue(key, out var n) ? n : "通用";
        return (key, name);
    }

    /// <summary>解析 GenFunction 为功能键列表。支持：JSON 对象 {"查看":"View","新增":"Create",...}（取 key 为功能键）；JSON 数组 ["查询","新增",...]；逗号分隔。</summary>
    /// <param name="genFunction">GenFunction 原始字符串（JSON 对象/数组或逗号分隔）</param>
    /// <returns>功能键列表（如 查询、新增、更新、删除）</returns>
    private static List<string> ParseGenFunctionKeys(string? genFunction)
    {
        if (string.IsNullOrWhiteSpace(genFunction))
            return new List<string>();
        var trimmed = genFunction.Trim();

        if (trimmed.StartsWith('{'))
        {
            try
            {
                var jobj = JObject.Parse(trimmed);
                return jobj.Properties()
                    .Select(p => p.Name)
                    .Where(s => s.Length > 0)
                    .ToList();
            }
            catch { }
        }

        if (trimmed.StartsWith('['))
        {
            try
            {
                var arr = JArray.Parse(trimmed);
                return arr
                    .Where(el => el?.Type == JTokenType.String)
                    .Select(el => el.ToString())
                    .Where(s => s.Length > 0)
                    .ToList();
            }
            catch { }
        }
        return trimmed.Split(new[] { ',', '，', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).Where(s => s.Length > 0).ToList();
    }

    /// <summary>功能键列表中是否包含“查询”或“查看”，用于生成查询相关 Action/方法。</summary>
    /// <param name="keys">GenFunction 解析得到的功能键列表</param>
    /// <returns>包含查询/查看时返回 true</returns>
    private static bool HasQueryKey(List<string> keys) =>
        keys.Exists(k => k.Contains("查询", StringComparison.Ordinal) || k.Contains("查看", StringComparison.Ordinal));

    /// <summary>根据 GenFunction 功能键列表，向表模型追加控制器 Action 描述符（GetList、GetById、Create、Update、Delete 等）。</summary>
    /// <param name="m">表级模板模型</param>
    /// <param name="keys">功能键列表（如 查询、新增、更新、删除）</param>
    private static void BuildControllerActions(TaktGenTableTemplateModel m, List<string> keys)
    {
        var permsPrefix = m.PermsPrefix ?? "app";
        var entity = m.EntityClassName;
        var funcName = m.GenFunctionName ?? m.GenBusinessName ?? string.Empty;
        var entityIdName = (m.EntityClassName.StartsWith("Takt", StringComparison.Ordinal) ? m.EntityClassName[4..] : m.EntityClassName) + "Id";

        var ep = m.EntityNamePascal;
        if (HasQueryKey(keys))
        {
            m.ControllerActions.Add(new TaktGenControllerActionDescriptor
            {
                Summary = $"获取{funcName}列表（分页）",
                HttpMethod = "HttpGet",
                Route = "list",
                PermissionKey = $"{permsPrefix}:list",
                PermissionName = $"查询{funcName}列表",
                MethodName = "GetListAsync",
                FrontendMethodName = $"get{ep}List",
                FrontendSignature = $"params: {ep}Query",
                FrontendReturnType = $"Promise<TaktPagedResult<{ep}>>",
                FrontendRequestKey = "params",
                Signature = $"[FromQuery] {entity}QueryDto queryDto",
                ResponseType = $"TaktPagedResult<{entity}Dto>",
                Body = "var result = await _service.GetListAsync(queryDto);\n        return Ok(result);"
            });
            m.ControllerActions.Add(new TaktGenControllerActionDescriptor
            {
                Summary = $"根据ID获取{funcName}",
                HttpMethod = "HttpGet",
                Route = "{id}",
                PermissionKey = $"{permsPrefix}:query",
                PermissionName = $"查询{funcName}详情",
                MethodName = "GetByIdAsync",
                FrontendMethodName = $"get{ep}ById",
                FrontendSignature = "id: string",
                FrontendReturnType = $"Promise<{ep}>",
                FrontendRequestKey = "",
                Signature = "long id",
                ResponseType = $"{entity}Dto",
                Body = "var dto = await _service.GetByIdAsync(id);\n        if (dto == null) return NotFound();\n        return Ok(dto);"
            });
        }
        if (keys.Any(k => k.Contains("新增", StringComparison.Ordinal)))
        {
            m.ControllerActions.Add(new TaktGenControllerActionDescriptor
            {
                Summary = $"创建{funcName}",
                HttpMethod = "HttpPost",
                Route = "",
                PermissionKey = $"{permsPrefix}:create",
                PermissionName = $"创建{funcName}",
                MethodName = "CreateAsync",
                FrontendMethodName = $"create{ep}",
                FrontendSignature = $"data: {ep}Create",
                FrontendReturnType = $"Promise<{ep}>",
                FrontendRequestKey = "data",
                Signature = $"[FromBody] {entity}CreateDto dto",
                ResponseType = $"{entity}Dto",
                Body = "var result = await _service.CreateAsync(dto);\n        return Ok(result);"
            });
        }
        if (keys.Any(k => k.Contains("更新", StringComparison.Ordinal)))
        {
            m.ControllerActions.Add(new TaktGenControllerActionDescriptor
            {
                Summary = $"更新{funcName}",
                HttpMethod = "HttpPut",
                Route = "{id}",
                PermissionKey = $"{permsPrefix}:update",
                PermissionName = $"更新{funcName}",
                MethodName = "UpdateAsync",
                FrontendMethodName = $"update{ep}",
                FrontendSignature = $"id: string, data: {ep}Update",
                FrontendReturnType = $"Promise<{ep}>",
                FrontendRequestKey = "data",
                Signature = $"long id, [FromBody] {entity}UpdateDto dto",
                ResponseType = $"{entity}Dto",
                Body = "try\n        {\n            var result = await _service.UpdateAsync(id, dto);\n            return Ok(result);\n        }\n        catch (TaktBusinessException ex)\n        {\n            return BadRequest(ex.Message);\n        }"
            });
        }
        if (keys.Any(k => k.Contains("删除", StringComparison.Ordinal)))
        {
            m.ControllerActions.Add(new TaktGenControllerActionDescriptor
            {
                Summary = $"删除{funcName}",
                HttpMethod = "HttpDelete",
                Route = "{id}",
                PermissionKey = $"{permsPrefix}:delete",
                PermissionName = $"删除{funcName}",
                MethodName = "DeleteAsync",
                FrontendMethodName = $"delete{ep}ById",
                FrontendSignature = "id: string",
                FrontendReturnType = "Promise<void>",
                FrontendRequestKey = "",
                Signature = "long id",
                ResponseType = "IActionResult",
                Body = "await _service.DeleteAsync(id);\n        return NoContent();"
            });
            m.ControllerActions.Add(new TaktGenControllerActionDescriptor
            {
                Summary = $"批量删除{funcName}",
                HttpMethod = "HttpDelete",
                Route = "batch",
                PermissionKey = $"{permsPrefix}:delete",
                PermissionName = $"批量删除{funcName}",
                MethodName = "DeleteBatchAsync",
                FrontendMethodName = $"delete{ep}Batch",
                FrontendSignature = "ids: string[]",
                FrontendReturnType = "Promise<void>",
                FrontendRequestKey = "data: ids",
                Signature = "[FromBody] IEnumerable<long> ids",
                ResponseType = "IActionResult",
                Body = "await _service.DeleteAsync(ids);\n        return NoContent();"
            });
        }
        if (keys.Any(k => k.Contains("模板", StringComparison.Ordinal)))
        {
            m.ControllerActions.Add(new TaktGenControllerActionDescriptor
            {
                Summary = "获取导入模板",
                HttpMethod = "HttpGet",
                Route = "template",
                PermissionKey = $"{permsPrefix}:import",
                PermissionName = "获取导入模板",
                MethodName = "GetTemplateAsync",
                FrontendMethodName = $"get{ep}Template",
                FrontendSignature = "sheetName?: string, fileName?: string",
                FrontendReturnType = "Promise<Blob>",
                FrontendRequestKey = "params",
                Signature = "[FromQuery] string? sheetName = null, [FromQuery] string? fileName = null",
                ResponseType = "IActionResult",
                Body = "try\n        {\n            var (resultFileName, content) = await _service.GetTemplateAsync(sheetName, fileName);\n            return File(content, \"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet\", resultFileName);\n        }\n        catch (Exception ex)\n        {\n            return BadRequest(ex.Message);\n        }"
            });
        }
        if (keys.Any(k => k.Contains("导入", StringComparison.Ordinal)))
        {
            m.ControllerActions.Add(new TaktGenControllerActionDescriptor
            {
                Summary = $"导入{funcName}",
                HttpMethod = "HttpPost",
                Route = "import",
                PermissionKey = $"{permsPrefix}:import",
                PermissionName = $"导入{funcName}",
                MethodName = "ImportAsync",
                FrontendMethodName = $"import{ep}Data",
                FrontendSignature = "file: File, sheetName?: string",
                FrontendReturnType = "Promise<{ success: number, fail: number, errors: string[] }>",
                FrontendRequestKey = "formData",
                Signature = "IFormFile file, [FromForm] string? sheetName = null",
                ResponseType = "IActionResult",
                Body = "try\n        {\n            if (file == null || file.Length == 0)\n                return BadRequest(\"请选择要导入的Excel文件\");\n            if (!file.FileName.EndsWith(\".xlsx\", StringComparison.OrdinalIgnoreCase) && !file.FileName.EndsWith(\".xls\", StringComparison.OrdinalIgnoreCase))\n                return BadRequest(\"只支持Excel文件（.xlsx或.xls）\");\n            using var stream = file.OpenReadStream();\n            var (success, fail, errors) = await _service.ImportAsync(stream, sheetName);\n            return Ok(new { success, fail, errors });\n        }\n        catch (Exception ex)\n        {\n            return BadRequest(ex.Message);\n        }"
            });
        }
        if (keys.Any(k => k.Contains("导出", StringComparison.Ordinal)))
        {
            m.ControllerActions.Add(new TaktGenControllerActionDescriptor
            {
                Summary = $"导出{funcName}",
                HttpMethod = "HttpPost",
                Route = "export",
                PermissionKey = $"{permsPrefix}:export",
                PermissionName = $"导出{funcName}",
                MethodName = "ExportAsync",
                FrontendMethodName = $"export{ep}Data",
                FrontendSignature = $"query: {ep}Query, sheetName?: string, fileName?: string",
                FrontendReturnType = "Promise<Blob>",
                FrontendRequestKey = "params",
                Signature = $"[FromBody] {entity}QueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null",
                ResponseType = "IActionResult",
                Body = "try\n        {\n            var (resultFileName, content) = await _service.ExportAsync(query, sheetName, fileName);\n            return File(content, \"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet\", resultFileName);\n        }\n        catch (Exception ex)\n        {\n            return BadRequest(ex.Message);\n        }"
            });
        }
    }

    /// <summary>根据 GenFunction 功能键列表，向表模型追加服务方法描述符（接口+实现）及私有方法（如 QueryExpression）。</summary>
    /// <param name="m">表级模板模型</param>
    /// <param name="keys">功能键列表（如 查询、新增、更新、删除）</param>
    private static void BuildServiceMethods(TaktGenTableTemplateModel m, List<string> keys)
    {
        var entity = m.EntityClassName;
        var funcName = m.GenFunctionName ?? m.GenBusinessName ?? string.Empty;

        if (HasQueryKey(keys))
        {
            m.ServiceMethods.Add(new TaktGenServiceMethodDescriptor
            {
                Summary = $"获取{funcName}列表（分页）",
                ParamsXml = "<param name=\"queryDto\">查询DTO</param>\n    /// <returns>分页结果</returns>",
                MethodName = "GetListAsync",
                Signature = $"{entity}QueryDto queryDto",
                ReturnType = $"Task<TaktPagedResult<{entity}Dto>>",
                Body = "var predicate = QueryExpression(queryDto);\n        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);\n        return TaktPagedResult<" + entity + "Dto>.Create(\n            data.Adapt<List<" + entity + "Dto>>(),\n            total,\n            queryDto.PageIndex,\n            queryDto.PageSize);"
            });
            m.ServiceMethods.Add(new TaktGenServiceMethodDescriptor
            {
                Summary = $"根据ID获取{funcName}",
                ParamsXml = $"<param name=\"id\">{funcName}ID</param>\n    /// <returns>{funcName}DTO</returns>",
                MethodName = "GetByIdAsync",
                Signature = "long id",
                ReturnType = $"Task<{entity}Dto?>",
                Body = "var entity = await _repository.GetByIdAsync(id);\n        return entity?.Adapt<" + entity + "Dto>();"
            });
            m.ServicePrivateMethods.Add(new TaktGenServiceMethodDescriptor
            {
                MethodName = "QueryExpression",
                Signature = $"{entity}QueryDto queryDto",
                ReturnType = $"System.Linq.Expressions.Expression<Func<{entity}, bool>>",
                Body = "var exp = Expressionable.Create<" + entity + ">();\n        if (!string.IsNullOrEmpty(queryDto?.KeyWords))\n        {\n            exp = exp.And(x => x.Remark != null && x.Remark.Contains(queryDto.KeyWords));\n        }\n        return exp.ToExpression();",
                IsPrivate = true
            });
        }
        if (keys.Any(k => k.Contains("新增", StringComparison.Ordinal)))
        {
            m.ServiceMethods.Add(new TaktGenServiceMethodDescriptor
            {
                Summary = $"创建{funcName}",
                ParamsXml = $"<param name=\"dto\">创建{funcName}DTO</param>\n    /// <returns>{funcName}DTO</returns>",
                MethodName = "CreateAsync",
                Signature = $"{entity}CreateDto dto",
                ReturnType = $"Task<{entity}Dto>",
                Body = "var entity = dto.Adapt<" + entity + ">();\n        entity = await _repository.CreateAsync(entity);\n        return entity.Adapt<" + entity + "Dto>();"
            });
        }
        if (keys.Any(k => k.Contains("更新", StringComparison.Ordinal)))
        {
            m.ServiceMethods.Add(new TaktGenServiceMethodDescriptor
            {
                Summary = $"更新{funcName}",
                ParamsXml = $"<param name=\"id\">{funcName}ID</param>\n    /// <param name=\"dto\">更新{funcName}DTO</param>\n    /// <returns>{funcName}DTO</returns>",
                MethodName = "UpdateAsync",
                Signature = $"long id, {entity}UpdateDto dto",
                ReturnType = $"Task<{entity}Dto>",
                Body = "var entity = await _repository.GetByIdAsync(id);\n        if (entity == null)\n            throw new TaktBusinessException(\"validation.recordNotFound\");\n        dto.Adapt(entity, typeof(" + entity + "UpdateDto), typeof(" + entity + "));\n        entity.UpdatedAt = DateTime.Now;\n        await _repository.UpdateAsync(entity);\n        return entity.Adapt<" + entity + "Dto>();"
            });
        }
        if (keys.Any(k => k.Contains("删除", StringComparison.Ordinal)))
        {
            m.ServiceMethods.Add(new TaktGenServiceMethodDescriptor
            {
                Summary = $"删除{funcName}",
                ParamsXml = $"<param name=\"id\">{funcName}ID</param>\n    /// <returns>任务</returns>",
                MethodName = "DeleteAsync",
                Signature = "long id",
                ReturnType = "Task",
                Body = "await _repository.DeleteAsync(id);"
            });
            m.ServiceMethods.Add(new TaktGenServiceMethodDescriptor
            {
                Summary = $"批量删除{funcName}",
                ParamsXml = $"<param name=\"ids\">{funcName}ID列表</param>\n    /// <returns>任务</returns>",
                MethodName = "DeleteAsync",
                Signature = "IEnumerable<long> ids",
                ReturnType = "Task",
                Body = "var idList = ids.ToList();\n        if (idList.Count == 0) return;\n        await _repository.DeleteAsync(idList);"
            });
        }
        if (keys.Any(k => k.Contains("模板", StringComparison.Ordinal)))
        {
            m.ServiceMethods.Add(new TaktGenServiceMethodDescriptor
            {
                Summary = $"获取导入模板",
                ParamsXml = "<param name=\"sheetName\">工作表名称</param>\n    /// <param name=\"fileName\">文件名</param>\n    /// <returns>Excel模板文件信息（文件名和内容）</returns>",
                MethodName = "GetTemplateAsync",
                Signature = "string? sheetName, string? fileName",
                ReturnType = "Task<(string fileName, byte[] content)>",
                Body = "var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(" + entity + "));\n        return await TaktExcelHelper.GenerateTemplateAsync<" + entity + "TemplateDto>(\n            sheetName: excelSheet,\n            fileName: excelFile\n        );"
            });
        }
        if (keys.Any(k => k.Contains("导入", StringComparison.Ordinal)))
        {
            m.ServiceMethods.Add(new TaktGenServiceMethodDescriptor
            {
                Summary = $"导入{funcName}",
                ParamsXml = "<param name=\"fileStream\">Excel文件流</param>\n    /// <param name=\"sheetName\">工作表名称</param>\n    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>",
                MethodName = "ImportAsync",
                Signature = "Stream fileStream, string? sheetName",
                ReturnType = "Task<(int success, int fail, List<string> errors)>",
                Body = "var errors = new List<string>();\n        int success = 0;\n        int fail = 0;\n        var excelSheet = ResolveExcelSheetName(sheetName, nameof(" + entity + "));\n        var importData = await TaktExcelHelper.ImportAsync<" + entity + "ImportDto>(fileStream, excelSheet);\n        if (importData == null || importData.Count == 0)\n        {\n            AddImportError(errors, \"validation.importExcelNoData\");\n            return (0, 0, errors);\n        }\n        var rowIndex = 0;\n        foreach (var item in importData)\n        {\n            rowIndex++;\n            try\n            {\n                var e = item.Adapt<" + entity + ">();\n                await _repository.CreateAsync(e);\n                success++;\n            }\n            catch (Exception ex)\n            {\n                fail++;\n                AddImportError(errors, \"validation.importRowUnhandledException\", rowIndex, GetLocalizedExceptionMessage(ex));\n            }\n        }\n        return (success, fail, errors);"
            });
        }
        if (keys.Any(k => k.Contains("导出", StringComparison.Ordinal)))
        {
            m.ServiceMethods.Add(new TaktGenServiceMethodDescriptor
            {
                Summary = $"导出{funcName}",
                ParamsXml = $"<param name=\"query\">{funcName}查询DTO（包含查询条件）</param>\n    /// <param name=\"sheetName\">工作表名称</param>\n    /// <param name=\"fileName\">文件名</param>\n    /// <returns>Excel文件信息（文件名和内容）</returns>",
                MethodName = "ExportAsync",
                Signature = $"{entity}QueryDto query, string? sheetName, string? fileName",
                ReturnType = "Task<(string fileName, byte[] content)>",
                Body = "var predicate = QueryExpression(query);\n        var data = predicate != null ? await _repository.FindAsync(predicate) : await _repository.GetAllAsync();\n        var dtos = data?.Adapt<List<" + entity + "ExportDto>>() ?? new List<" + entity + "ExportDto>();\n        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(" + entity + "));\n        return await TaktExcelHelper.ExportAsync(dtos, excelSheet, excelFile);"
            });
        }
    }

    /// <summary>
    /// 从 <see cref="TaktGenTable"/> 实体构建表级模板模型。
    /// </summary>
    public static TaktGenTableTemplateModel From(TaktGenTable table)
    {
        if (table == null)
            return new TaktGenTableTemplateModel();
        var m = new TaktGenTableTemplateModel
        {
            DataSource = table.DataSource,
            TableName = table.TableName,
            TableComment = table.TableComment,
            SubTableName = table.SubTableName,
            SubTableFkName = table.SubTableFkName,
            TreeCode = table.TreeCode,
            TreeParentCode = table.TreeParentCode,
            TreeName = table.TreeName,
            InDatabase = table.InDatabase,
            GenTemplate = table.GenTemplate,
            NamePrefix = table.NamePrefix,
            EntityNamespace = table.EntityNamespace ?? "Takt.Domain.Entities",
            EntityClassName = table.EntityClassName,
            EntityNamePascal = ToEntityNamePascal(table.EntityClassName),
            EntityNameCamel = ToEntityNameCamel(ToEntityNamePascal(table.EntityClassName)),
            EntityNameKebab = ToEntityNameKebab(ToEntityNamePascal(table.EntityClassName)),
            ApiBasePath = ToApiBasePath(table.ControllerClassName),
            DtoNamespace = table.DtoNamespace ?? "Takt.Application.Dtos",
            DtoClassName = table.DtoClassName,
            DtoCategory = table.DtoCategory,
            DtoCategories = string.IsNullOrWhiteSpace(table.DtoCategory)
                ? new List<string>()
                : table.DtoCategory!.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList(),
            DtoCategoryDescriptors = BuildDtoCategoryDescriptors(table.EntityClassName, table.DtoCategory, ToEntityNamePascal(table.EntityClassName)),
            ServiceNamespace = table.ServiceNamespace ?? "Takt.Application.Services",
            IServiceClassName = table.IServiceClassName,
            ServiceClassName = table.ServiceClassName,
            ControllerNamespace = table.ControllerNamespace ?? "Takt.WebApi.Controllers",
            ControllerClassName = table.ControllerClassName,
            RepositoryInterfaceNamespace = table.RepositoryInterfaceNamespace,
            IRepositoryClassName = table.IRepositoryClassName,
            RepositoryNamespace = table.RepositoryNamespace,
            RepositoryClassName = table.RepositoryClassName,
            GenModuleName = table.GenModuleName,
            ApiModuleKey = GetApiModuleTopLevel(table.GenModuleName).Key,
            ApiModuleName = GetApiModuleTopLevel(table.GenModuleName).Name,
            FrontendModulePath = string.IsNullOrWhiteSpace(table.GenModuleName) ? "module" : table.GenModuleName.Trim().ToLowerInvariant().Replace('_', '/').Replace(".", "/"),
            RequestImportPath = BuildRequestImportPath(table.GenModuleName),
            GenBusinessName = table.GenBusinessName,
            GenFunctionName = table.GenFunctionName,
            GenFunction = table.GenFunction,
            GenMethod = table.GenMethod,
            IsRepository = table.IsRepository,
            GenPath = table.GenPath,
            ParentMenuId = table.ParentMenuId,
            IsGenMenu = table.IsGenMenu,
            IsGenTranslation = table.IsGenTranslation,
            SortType = table.SortType,
            SortField = table.SortField,
            TsSortField = ToTsSortField(table.SortField),
            PermsPrefix = table.PermsPrefix,
            FrontTemplate = table.FrontTemplate,
            FrontStyle = table.FrontStyle,
            BtnStyle = table.BtnStyle,
            IsGenCode = table.IsGenCode,
            GenCodeCount = table.GenCodeCount,
            IsUseTabs = table.IsUseTabs,
            TabsFieldCount = table.TabsFieldCount,
            GenAuthor = table.GenAuthor ?? "Takt365",
            Options = table.Options
        };
        var keys = ParseGenFunctionKeys(table.GenFunction);
        m.IsQuery = HasQueryKey(keys) ? 1 : 0;
        m.IsCreate = keys.Any(k => k.Contains("新增", StringComparison.Ordinal)) ? 1 : 0;
        m.IsUpdate = keys.Any(k => k.Contains("更新", StringComparison.Ordinal)) ? 1 : 0;
        m.IsDelete = keys.Any(k => k.Contains("删除", StringComparison.Ordinal)) ? 1 : 0;
        m.IsTemplate = keys.Any(k => k.Contains("模板", StringComparison.Ordinal)) ? 1 : 0;
        m.IsImport = keys.Any(k => k.Contains("导入", StringComparison.Ordinal)) ? 1 : 0;
        m.IsExport = keys.Any(k => k.Contains("导出", StringComparison.Ordinal)) ? 1 : 0;
        BuildControllerActions(m, keys);
        BuildServiceMethods(m, keys);
        return m;
    }
}

/// <summary>
/// 列级模板模型，与 <see cref="TaktGenTableColumn"/> 实体字段一一对应，供 Scriban 列循环使用并充分利用实体全部属性。
/// </summary>
public class TaktGenColumnTemplateModel
{
    /// <summary>数据库列名称（snake_case）</summary>
    public string DatabaseColumnName { get; set; } = string.Empty;

    /// <summary>列描述（字段注释）</summary>
    public string? ColumnComment { get; set; }

    /// <summary>列注释展示值（ColumnComment ?? CsharpColumnName，模板中直接用 col.Comment）</summary>
    public string Comment => string.IsNullOrWhiteSpace(ColumnComment) ? (CsharpColumnName ?? string.Empty) : ColumnComment;

    /// <summary>数据库数据类型（如 varchar、int、datetime）</summary>
    public string DatabaseDataType { get; set; } = "nvarchar";

    /// <summary>C# 类型（如 string、int、DateTime、decimal）</summary>
    public string CsharpDataType { get; set; } = "string";

    /// <summary>C# 列名（帕斯卡命名）</summary>
    public string CsharpColumnName { get; set; } = string.Empty;

    /// <summary>前端列名（驼峰命名，供 TypeScript/API 使用）</summary>
    public string TsColumnName { get; set; } = string.Empty;

    /// <summary>前端类型（string、number、boolean，供 TypeScript 使用）</summary>
    public string TsDataType { get; set; } = "string";

    /// <summary>C# 长度（字符串长度或数值整数位）</summary>
    public int Length { get; set; }

    /// <summary>C# 小数位数</summary>
    public int DecimalDigits { get; set; }

    /// <summary>是否主键（0=否，1=是）</summary>
    public int IsPk { get; set; }

    /// <summary>是否自增（0=否，1=是）</summary>
    public int IsIncrement { get; set; }

    /// <summary>是否必填（0=否，1=是）</summary>
    public int IsRequired { get; set; }

    /// <summary>是否为新增字段（0=否，1=是）</summary>
    public int IsCreate { get; set; }

    /// <summary>是否更新字段（0=否，1=是）</summary>
    public int IsUpdate { get; set; }

    /// <summary>是否查重字段（0=否，1=是）</summary>
    public int IsUnique { get; set; }

    /// <summary>是否列表字段（0=否，1=是）</summary>
    public int IsList { get; set; }

    /// <summary>是否导出字段（0=否，1=是）</summary>
    public int IsExport { get; set; }

    /// <summary>是否排序字段（0=否，1=是）</summary>
    public int IsSort { get; set; }

    /// <summary>是否查询字段（0=否，1=是）</summary>
    public int IsQuery { get; set; }

    /// <summary>查询方式（EQ=等于，NE=不等于，GT=大于，LT=小于，LIKE=模糊，BETWEEN=范围）</summary>
    public string QueryType { get; set; } = "LIKE";

    /// <summary>显示类型（input、textarea、select、date、switch 等）</summary>
    public string HtmlType { get; set; } = "input";

    /// <summary>字典类型（关联数据字典）</summary>
    public string? DictType { get; set; }

    /// <summary>排序序号</summary>
    public int OrderNum { get; set; }

    /// <summary>帕斯卡转驼峰（供前端列名使用）。</summary>
    private static string ToTsColumnName(string pascal)
    {
        if (string.IsNullOrEmpty(pascal)) return string.Empty;
        return char.ToLowerInvariant(pascal[0]) + pascal[1..];
    }

    /// <summary>C# 类型转 TypeScript 类型（string、number、boolean）。</summary>
    private static string ToTsDataType(string csharpType)
    {
        if (string.IsNullOrEmpty(csharpType)) return "string";
        var t = csharpType.Trim();
        if (t.Equals("bool", StringComparison.OrdinalIgnoreCase)) return "boolean";
        if (t.Equals("int", StringComparison.OrdinalIgnoreCase) || t.Equals("long", StringComparison.OrdinalIgnoreCase)
            || t.Equals("decimal", StringComparison.OrdinalIgnoreCase) || t.Equals("float", StringComparison.OrdinalIgnoreCase)
            || t.Equals("double", StringComparison.OrdinalIgnoreCase)) return "number";
        return "string";
    }

    /// <summary>
    /// 从 <see cref="TaktGenTableColumn"/> 实体构建列级模板模型。
    /// </summary>
    /// <param name="column">代码生成字段配置实体</param>
    /// <returns>列级模板模型，供 Scriban 列循环使用</returns>
    public static TaktGenColumnTemplateModel From(TaktGenTableColumn column)
    {
        return new TaktGenColumnTemplateModel
        {
            DatabaseColumnName = column.DatabaseColumnName,
            ColumnComment = column.ColumnComment,
            DatabaseDataType = column.DatabaseDataType,
            CsharpDataType = column.CsharpDataType,
            CsharpColumnName = column.CsharpColumnName,
            TsColumnName = ToTsColumnName(column.CsharpColumnName),
            TsDataType = ToTsDataType(column.CsharpDataType),
            Length = column.Length,
            DecimalDigits = column.DecimalDigits,
            IsPk = column.IsPk,
            IsIncrement = column.IsIncrement,
            IsRequired = column.IsRequired,
            IsCreate = column.IsCreate,
            IsUpdate = column.IsUpdate,
            IsUnique = column.IsUnique,
            IsList = column.IsList,
            IsExport = column.IsExport,
            IsSort = column.IsSort,
            IsQuery = column.IsQuery,
            QueryType = column.QueryType,
            HtmlType = column.HtmlType,
            DictType = column.DictType,
            OrderNum = column.OrderNum
        };
    }
}
