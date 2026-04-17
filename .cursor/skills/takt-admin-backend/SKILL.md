---
name: takt-admin-backend
description: Takt .NET 后端开发规范与模块架构。用于在 Takt 项目中新增实体/DTO/应用服务/WebApi 控制器、配置权限与多库时参考。
---

# Takt 后端开发 Skill（架构与基本用法）

> 适用范围：`backend/src` 下所有 C# 代码，包含 Domain / Application / Infrastructure / WebApi。  
> 配套规则：`.cursor/rules/00-project.mdc`、`01-data-design.mdc`、`02-backend.mdc`。

## 1. 解决方案结构速查

- **Takt.Domain**：实体 (`Entities`)、仓储接口 (`Repositories`)、领域接口、扩展与验证。
- **Takt.Application**：DTO (`Dtos`)、应用服务接口与实现 (`Services`)。
- **Takt.Infrastructure**：SqlSugar 配置、多库路由 (`TaktEntityDatabaseMapping`)、仓储实现、种子数据、日志、内置 OpenAPI 与 Scalar 等。
- **Takt.Shared**：通用模型、异常 (`TaktBusinessException`)、帮助类。
- **Takt.WebApi**：控制器 (`Controllers`)、Program、Scalar/OpenAPI 文档入口（`Swagger.Enabled`）。

新增业务时，通常涉及这几层：

1. Domain：新建实体（数据库表）。
2. Application：DTO + 应用服务接口与实现。
3. WebApi：控制器，调用应用服务并加上 `[TaktPermission]`。

## 2. 新增实体（Domain 层）

**文件位置**：`backend/src/Takt.Domain/Entities/{领域}/`  
**规则要点（来自 `01-data-design.mdc`）**：

- 实体继承 `TaktEntityBase`，包含 `Id(long)`、`ConfigId`、审计/软删除字段。
- 使用 SqlSugar 注解：
  - `[SugarTable("takt_领域_模块_表名", "说明")]`
  - 长整型主键/外键属性必须加 `[JsonConverter(typeof(ValueToStringConverter))]`，以便前端使用 `string`。
- 表名与列名全部小写下划线；索引用 `[SugarIndex]`。
- 多库：命名空间必须能被 `TaktEntityDatabaseMapping.GetConfigIdByEntityNamespace` 正确路由到 ConfigId。

### 示例（简化）

```csharp
using Newtonsoft.Json;
using SqlSugar;
using Takt.Domain.Entities.Base;
using Takt.Shared.Json;

[SugarTable("takt_humanresource_personnel_employee", "员工表")]
public class TaktEmployee : TaktEntityBase
{
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    [SugarColumn(Length = 64, IsNullable = false)]
    public string RealName { get; set; } = string.Empty;
}
```

## 3. DTO 与应用服务（Application 层）

### 3.1 DTO 放在哪里

- 位置：`backend/src/Takt.Application/Dtos/{领域}/`（可以一个文件多个 DTO）。
- 约定命名：
  - `TaktXxxDto`：列表/详情 DTO。
  - `TaktXxxQueryDto`：查询 DTO，通常继承 `TaktPagedQuery`。
  - `TaktCreateXxxDto` / `TaktUpdateXxxDto`：新增/修改。

### 3.2 应用服务接口与实现（统一 QueryExpression）

- 接口命名：`ITaktXxxService`，位置与领域一致。
- 实现类继承 `TaktServiceBase`，通过构造函数注入仓储接口或 `ITaktRepository<T>`。
- **标准方法命名与顺序**（接口、实现、控制器三处统一）：
  1. `GetListAsync` / `GetPagedAsync`
  2. `GetByIdAsync`
  3. `GetOptionsAsync`（下拉/树形选项）
  4. `CreateAsync`
  5. `UpdateAsync`
  6. `DeleteAsync`（单条）
  7. `BatchDeleteAsync`（批量删除）
  8. `GetTemplateAsync`（导出模板）
  9. `ImportAsync`
  10. `ExportAsync`

```csharp
public interface ITaktEmployeeService
{
    Task<TaktPagedResult<TaktEmployeeDto>> GetListAsync(TaktEmployeeQueryDto queryDto);
    Task<TaktEmployeeDto?> GetByIdAsync(string id);
    Task<IReadOnlyList<TaktSelectOption>> GetOptionsAsync();
    Task CreateAsync(TaktEmployeeCreateDto dto);
    Task UpdateAsync(string id, TaktEmployeeUpdateDto dto);
    Task DeleteAsync(string id);
    Task BatchDeleteAsync(IEnumerable<string> ids);
    Task<byte[]> GetTemplateAsync();
    Task ImportAsync(IFormFile file);
    Task<byte[]> ExportAsync(TaktEmployeeQueryDto queryDto);
}
```

> 建议在服务实现中统一使用 **私有静态方法 `QueryExpression(TaktXxxQueryDto queryDto)`** 来构建查询表达式，类似 `TaktCostCenterService` 中的实现：通过 `Expressionable.Create<T>()` 按查询 DTO 各字段逐项 `AndIF` 拼接条件，最后 `ToExpression()` 返回 `Expression<Func<TEntity, bool>>`，供 `GetListAsync` 与 `ExportAsync` 复用，避免到处散落手写 `Where` 逻辑。

> 异常使用 `ThrowBusinessException(...)` / `ThrowBusinessExceptionLocalized(...)`，不要直接抛 `new Exception()`。

## 3.3 后端文件与注释规范（简要统一）

> 详细见 `.cursor/rules/02-backend.mdc` 中的「后端文档与注释规则」。

- **文件命名与位置**
  - 实体：`Takt.Domain/Entities/{领域}/TaktXxx.cs`。
  - DTO：`Takt.Application/Dtos/{领域}/TaktXxxDtos.cs`（允许多个相关 DTO 同文件）。
  - 服务接口/实现：`Takt.Application/Services/{领域}/ITaktXxxService.cs` / `TaktXxxService.cs`。
  - 控制器：`Takt.WebApi/Controllers/{领域}/TaktXxxController.cs` 或 `TaktXxxsController.cs`。
- **文件头（推荐但非必须）**
  - 保留统一文件头：项目名称、命名空间、文件名、功能描述、版权/免责声明，与现有文件风格一致。
- **XML 注释**
  - 对外公开的类、接口、公共方法应使用 `/// <summary>`、`<param>`、`<returns>` 注释，简要说明业务含义（中文）。
  - 私有方法/局部变量仅在含义不明显时添加简短 `//` 注释，避免“解释代码在做什么”的冗余注释。
- **注释风格**
  - 注释重点写「为什么 / 约束 / 边界条件」，而不是逐行翻译代码。
  - 修改逻辑时必须同步更新相关注释，禁止遗留与实现不符的注释。

### 3.4 实体翻译种子（本地化强制）

> 详细与体例见 `.cursor/rules/02-backend.mdc`「租户与种子初始化 → 实体翻译种子」。

- **一实体一种子类**：新增 `Takt.Domain/.../TaktXxx.cs` 时，必须新增 **`Takt.Infrastructure/Data/Seeds/SeedI18nData/TaktXxxEntitiesSeedData.cs`**（命名与实体 `TaktXxx` 一一对应），实现 `ITaktSeedData`。
- **禁止**一个种子类承载多个实体的 `entity.*` 键；**禁止**用集中式 Executor 替代「每实体一个种子文件」的维护方式（与仓库现有 `TaktRoleDeptEntitiesSeedData` 等风格一致）。
- **必须双注册**：在 `TaktSeedsCollectionExtensions.AddTaktSeeds` 的 **Autofac** 与 **`IServiceCollection`** 两处注册 `ITaktSeedData`，否则翻译不会进库。

## 4. WebApi 控制器

**位置**：`backend/src/Takt.WebApi/Controllers/{领域}/`  
**约定**：

- 继承 `TaktControllerBase`。
- 路由采用 `[Route("api/[controller]")]` 或显式指定模块前缀。
- 权限使用 `[TaktPermission("module:resource:action", "描述")]`，与前端按钮权限一致。

```csharp
[ApiController]
[Route("api/[controller]")]
[ApiModule("人力资源-员工")]
public class TaktEmployeesController : TaktControllerBase
{
    private readonly ITaktEmployeeService _employeeService;

    public TaktEmployeesController(ITaktEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("list")]
    [TaktPermission("humanresource:employee:list", "员工列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeDto>>> GetListAsync([FromQuery] TaktEmployeeQueryDto query)
    {
        return await HandleExceptionAsync(() => _employeeService.GetPagedAsync(query));
    }
}
```

## 5. 常用规则速查

- **分层依赖**：Domain 不依赖 Application/Infrastructure/WebApi；Application 只依赖 Domain + Shared；WebApi 只依赖 Application。
- **异步**：所有访问数据库或外部服务的方法优先使用 `async/await` + 仓储异步 API。
- **映射**：实体与 DTO 映射优先用 Mapster 的 `Adapt<T>()` 或集中配置，不在控制器写大段手动映射。
- **权限码格式**：`module:resource:action`（例如 `workflow:scheme:list`），后端 `[TaktPermission]` 与前端 `hasPermission` 一致。

## 6. 开发本 Skill 时的使用方式

在本项目里如果你需要：

- 新增一个业务模块的后端接口 → 按 **2 + 3 + 4** 的顺序创建实体、DTO、Service、Controller，并参考 `.cursor/rules/02-backend.mdc`。
- 对已有模块补充字段或方法 → 先检查实体与 DTO 是否符合 `01-data-design.mdc`，再更新 Service 与 Controller。

本 Skill 只给出 Takt 专用约定，所有实现细节都必须对照真实代码与规则文档再确认。+
