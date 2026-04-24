---
name: takt-csharp-sqlsugar-expert
description: >-
  Takt .NET 后端全栈约定：SqlSugar 多库与仓储、审计与用户上下文、分层架构、DTO/应用服务/WebApi、
  从零新增标准 CRUD 模块（含前端对接清单）。在用户或任务涉及 backend/src、SqlSugar、TaktRepository、
  分库 ConfigId、实体/DTO/Service/Controller、导入导出、种子与 i18n、或完整后台模块时使用本 Skill。
  权限与菜单码、角色数据范围见 takt-rbac-abac-expert；纯前端 Ant Design Vue 见 takt-ant-design-vue-expert。
---

# Takt · C# + SqlSugar + 后端模块专家 Skill

> **适用范围**：`backend/src`（Domain / Application / Infrastructure / WebApi）；新增标准 CRUD 时同时对照前端 `frontend/takt.antd` 清单。  
> **配套规则**：`.cursor/rules/00-project.mdc`、`01-data-design.mdc`、`02-backend.mdc`、`03-frontend.mdc`（前端章节）。  
> **包版本**：以 `Takt.Infrastructure.csproj` 中 **SqlSugarCore** 为准。

---

## A. SqlSugar 多库与仓储（数据访问）

### A.1 多库路由（必读）

- **入口**：`Takt.Infrastructure.Data.TaktSqlSugarDbContext.GetClient(Type? entityType)` — 按实体类型解析 `ISqlSugarClient`。
- **映射**：`Takt.Infrastructure.Data.TaktEntityDatabaseMapping`  
  - `GetConfigIdByEntityNamespace` / `GetPersistenceConfigIdForEntityType`：实体命名空间 → **ConfigId**（与 `appsettings` 中 `dbConfigs` 一致）。
- **实体**：`TaktEntityBase.ConfigId` 表示行所属物理库；**禁止**把「当前登录租户」与「实体持久化 ConfigId」混用；仓储使用 `TaktRepository<TEntity>.EntityPersistenceConfigId`（`TaktRepository.cs`）。
- **新增实体**：命名空间必须落入映射规则，否则可能错误落到主库 `"0"`。

### A.2 仓储基类 `TaktRepository<TEntity>`

- **位置**：`Takt.Infrastructure.Repositories.TaktRepository.cs`。
- **构造**：注入 `TaktSqlSugarDbContext`、`IConfiguration`、`ITaktUserContext`。审计用户**唯一**解析入口：`ITaktUserContext.TryGetPersistenceAuditUser`（`TaktUserProvider` 实现）；**禁止**在仓储内重复解析 `HttpContext`/Claims。
- **审计**：`TryResolveCreatedBySource` → 种子阶段或 `ITaktUserContext`；`TaktOperLog` / `TaktAopLog` 若调用方已写 `CreatedById > 0` 则不再覆盖（避免 `Task.Run` 丢 AsyncLocal）。
- **租户行过滤**：`TaktTenantContext.IsTenantEnabled` 为真时，查询/更新/删除带 `ConfigId == EntityPersistenceConfigId`。
- **雪花**：`Snowflake:Enabled` 与 `Insertable` / `ExecuteReturnSnowflakeId` 见 `CreateAsync`。

### A.3 SqlSugar 使用约定

- **客户端**：统一仓储的 `Db`（`GetClient(typeof(TEntity))`），应用服务**禁止** `new SqlSugarClient`。
- **查询**：`Queryable<T>()` + `Where` + `ToListAsync` / `ToPageListAsync`；软删除 `IsDeleted == 0`。
- **更新**：`Updateable(entity)` 或 `SetColumns` + `Where`；差异日志对非日志实体 `EnableDiffLogEvent()`（与 `Logging:AopLog`、`TaktAutofacModule` 一致）。
- **事务**：同库 `Ado.UseTranAsync`；跨库由业务显式设计。

### A.4 SignalR / 后台线程与差异日志

- `TaktUserContext.CurrentUser`（AsyncLocal）在 Hub / `Task.Run` 中可能为空：Hub 内应先 `await ITaktUserContext.GetCurrentUserAsync()` 再读登录名；差异日志在 `OnDiffLogEvent` 闭包前写入 DTO，与 `ITaktUserContext` 同源。

---

## B. 解决方案结构与分层（原 takt-admin-backend）

- **Takt.Domain**：实体、仓储接口、领域接口、扩展与验证。
- **Takt.Application**：DTO、应用服务接口与实现。
- **Takt.Infrastructure**：SqlSugar、多库、仓储实现、种子、中间件、SignalR、OpenAPI/Scalar 等。
- **Takt.Shared**：通用模型、异常、帮助类。
- **Takt.WebApi**：控制器、`Program`、Scalar 入口（`Swagger.Enabled`）。

**新增业务常规顺序**：Domain 实体 → Application DTO + Service → WebApi 控制器 + `[TaktPermission]`。

**分层依赖**：Domain 不依赖 Application/Infrastructure/WebApi；Application 只依赖 Domain + Shared；控制器只依赖应用服务接口，**禁止**直接依赖仓储或 SqlSugar 客户端。

---

## C. 实体（Domain）

**路径**：`backend/src/Takt.Domain/Entities/{领域}/`

- 继承 `TaktEntityBase`（`Id`、`ConfigId`、审计、软删除）。
- `[SugarTable("takt_领域_模块_表名", "说明")]`；表名/列名**小写下划线**；`[SugarIndex]`。
- long 主键/外键在对外 DTO 路径使用 `[JsonConverter(typeof(ValueToStringConverter))]`（与 `01-data-design.mdc` 一致）。

```csharp
using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Personnel;

[SugarTable("takt_humanresource_employee", "员工表")]
public class TaktEmployee : TaktEntityBase
{
    [SugarColumn(ColumnName = "real_name", Length = 64, IsNullable = false)]
    public string RealName { get; set; } = string.Empty;
}
```

> 主键/外键 long 若需序列化为 string 供前端，按 `01-data-design.mdc` 与现有实体（如 `TaktUser`、`TaktCostCenter`）为 `Id` 及外键列添加 `[JsonConverter(typeof(ValueToStringConverter))]` 及对应 `using`。

---

## D. DTO 与应用服务（Application）

### D.1 DTO 位置与命名

- **路径**：`backend/src/Takt.Application/Dtos/{领域}/`（可多类同文件，如 `TaktXxxDtos.cs`）。
- **命名**：`TaktXxxDto`、`TaktXxxQueryDto`（常继承 `TaktPagedQuery`）、`TaktCreateXxxDto` / `TaktUpdateXxxDto`；导入导出 `TaktXxxTemplateDto`、`TaktXxxImportDto`、`TaktXxxExportDto` 等。
- **继承约束（强制）**：`TaktUpdateXxxDto` 必须继承 `TaktCreateXxxDto`，仅追加更新场景字段（如 `Id`）；禁止复制整套创建字段导致重复维护。

### D.2 应用服务

- 接口：`ITaktXxxService`；实现：`TaktXxxService` 继承 `TaktServiceBase`，注入 `ITaktRepository<T>` 或领域仓储接口。
- **标准方法集合**（接口、实现、控制器三处命名与顺序对齐；与 `02-backend.mdc` 中「实体前缀 + 语义」强制规则一致，以下为逻辑能力分组）：
  1. 分页列表 / 列表  
  2. 详情 `GetXxxByIdAsync`  
  3. `GetXxxOptionsAsync`（下拉/树）  
  4. `CreateXxxAsync`  
  5. `UpdateXxxAsync`  
  6. `DeleteXxxByIdAsync`  
  7. `DeleteXxxBatchAsync`  
  8. `GetXxxTemplateAsync`  
  9. `ImportXxxAsync`  
  10. `ExportXxxAsync`  

- **查询条件**：推荐私有静态 `QueryExpression(TaktXxxQueryDto dto)` + `Expressionable.Create<T>()` + `AndIF` + `ToExpression()`，供列表与导出复用（参照 `TaktCostCenterService` 等现有实现）。
- **异常**：`ThrowBusinessException` / `ThrowBusinessExceptionLocalized`；存在性：`EnsureEntityExists` / `EnsureEntityExistsLocalized`。
- **唯一参照风格**：`Takt.Application/Services/Identity/TaktUserService.cs`（与 `02-backend.mdc` 一致）。

### D.3 文件与 XML 注释（简要）

- 服务：`Takt.Application/Services/{领域}/` 或 `Takt.Application/{领域}/`（与现有模块一致）。
- 文件头、公开 API 的 `///` 注释与「写原因不写废话」原则见 `02-backend.mdc`。

### D.4 实体翻译种子（i18n）

- **一实体一种子类**：`Takt.Infrastructure/Data/Seeds/SeedI18nData/{领域}/TaktXxxEntitiesSeedData.cs`，实现 `ITaktSeedData`。
- **双注册**：`TaktSeedsCollectionExtensions.AddTaktSeeds` 的 **Autofac** 与 **IServiceCollection** 两处注册 `ITaktSeedData`。
- 细则见 `02-backend.mdc`「实体翻译种子」。

---

## E. WebApi 控制器

**路径**：`backend/src/Takt.WebApi/Controllers/{领域}/`

- 继承 `TaktControllerBase`；`[Route("api/[controller]")]`；`[ApiModule(...)]`。
- 权限：`[TaktPermission("module:resource:action", "描述")]` — **四段小写英文、冒号分隔**（与 `02-backend.mdc`、前端 `hasPermission` 一致）。
- 列表分页返回 `TaktPagedResult<T>`；异常走 `HandleExceptionAsync` / `HandleException`。
- REST：`list`、`{id}`、POST、PUT、DELETE；导入导出 `blob` / FormData 与项目现有写法一致。

```csharp
[HttpGet("list")]
[TaktPermission("humanresource:employee:list", "员工列表")]
public async Task<ActionResult<TaktPagedResult<TaktEmployeeDto>>> GetEmployeeListAsync([FromQuery] TaktEmployeeQueryDto query)
{
    return await HandleExceptionAsync(() => _employeeService.GetPagedAsync(query));
}
```

---

## F. 从零新增标准 CRUD 模块（原 takt-admin-crud）

### F.1 总体流程

1. Domain：实体 + 命名空间映射到正确 ConfigId。  
2. Application：DTO + `ITaktXxxService` / `TaktXxxService`。  
3. WebApi：控制器 + 权限。  
4. 运行建表 / 种子；补充 **D.4** i18n 种子。  
5. 前端：`frontend/takt.antd` — types、api、views、菜单与按钮权限码（与 `03-frontend.mdc` 一致）。

### F.2 前端对接清单

- **types**：`types/{模块}/`，ID 用 `string`。  
- **api**：`api/{模块}/`，URL 与控制器路由一致；方法名如 `getXxxList`、`getXxxById`、`createXxx`…  
- **views**：列表 + 搜索 + 编辑弹窗/表单；优先复用项目内表格/表单封装。  
- **菜单与权限**：菜单 `path`/`component`；按钮权限码与 `[TaktPermission]` **完全一致**。  
- **请求**：业务接口用 `@/api/request`；OAuth 等裸 `axios` 须自行解包 `TaktApiResult` 的 `data`（见 `src/api/identity/auth.ts`）。

### F.3 自查清单

- [ ] 实体 `TaktEntityBase` + 表/列命名符合 `01-data-design.mdc`。  
- [ ] DTO 与实体对齐，对外 ID `string`。  
- [ ] Service 异步 + 统一异常；含模板/导入/导出（除非模块明确豁免）。  
- [ ] 控制器 `TaktControllerBase` + 权限码四段。  
- [ ] 前端 api/types/views 与后端对齐；业务请求不经裸 axios 绕过拦截器。

---

## G. 其它 Skill

- **RBAC、菜单、权限码、数据范围**：**`takt-rbac-abac-expert`**。  
- **Vue + Ant Design Vue 组件与交互**：**`takt-ant-design-vue-expert`**。

本 Skill 为原 **`takt-admin-backend`**、**`takt-admin-crud`** 与 **SqlSugar 专家小节** 的合并版；实现细节以仓库真实代码与 `.cursor/rules` 为准。
