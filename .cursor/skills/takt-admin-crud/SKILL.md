---
name: takt-admin-crud
description: 在 Takt 项目中新增完整 CRUD 模块的流程指引，覆盖实体、DTO、应用服务、WebApi 控制器以及前端 API/页面。
---

# Takt 新增 CRUD 模块 Skill

> 适用：在 Takt 中新增一个标准后台管理模块（如“员工档案”、“假期类型”、“成本中心”等）。  
> 相关规则：`.cursor/rules/01-data-design.mdc`、`02-backend.mdc`、`03-frontend.mdc`。

## 总体流程

1. Domain：新增实体（映射到数据库表）。
2. Application：新增 DTO + 应用服务接口/实现。
3. WebApi：新增控制器 + 权限标注。
4. 数据库：确认 `TaktEntityDatabaseMapping` 与自动建表/种子。
5. 前端：API 模块 + types + 页面（列表/表单） + 菜单与按钮权限。

## 1. 实体与数据库（Domain）

1. 在 `Takt.Domain/Entities/{领域}` 下新增实体类，继承 `TaktEntityBase`。
2. 使用 `[SugarTable]` 设置表名，遵守 `takt_{领域}_{模块}_{表}` 全小写下划线规则。
3. 所有对外暴露的 long 主键/外键加 `[JsonConverter(typeof(ValueToStringConverter))]`。
4. 若是新领域路径，确认命名空间能被 `TaktEntityDatabaseMapping.GetConfigIdByEntityNamespace` 正确路由到某个 ConfigId。

**完成后**：运行项目，`TaktTableInitializer` 会自动建表；如有种子数据，再在 Infrastructure 的种子初始化中补充。

## 2. DTO 设计（Application.Dtos）

在 `Takt.Application/Dtos/{领域}` 下新增/更新 DTO 文件：

- 查询：`TaktXxxQueryDto : TaktPagedQuery`
- 列表/详情：`TaktXxxDto`
- 创建/更新：`TaktXxxCreateDto` / `TaktXxxUpdateDto`
- 导入模板：`TaktXxxTemplateDto`
- 导入/导出：`TaktXxxImportDto` / `TaktXxxExportDto`


字段与实体对齐，ID 使用 `string`（对应后端 long）。

## 3. 应用服务（Application.Services）

1. 在 `Takt.Application/Services/{领域}` 或 `Takt.Application/{领域}` 下创建接口 `ITaktXxxService` 与实现 `TaktXxxService`。
2. `TaktXxxService` 继承 `TaktServiceBase`，通过构造函数注入仓储接口或 `ITaktRepository<TaktEntity>`。
3. 提供标准方法（接口、实现、控制器统一命名与顺序）：

```csharp
Task<TaktPagedResult<TaktXxxDto>> GetListAsync(TaktXxxQueryDto queryDto);
Task<TaktXxxDto?> GetByIdAsync(string id);
Task<IReadOnlyList<TaktSelectOption>> GetOptionsAsync();
Task CreateAsync(TaktXxxCreateDto dto);
Task UpdateAsync(string id, TaktXxxUpdateDto dto);
Task DeleteAsync(string id);
Task BatchDeleteAsync(IEnumerable<string> ids);
Task<byte[]> GetTemplateAsync();
Task ImportAsync(IFormFile file);
Task<byte[]> ExportAsync(TaktXxxQueryDto queryDto);
```

4. 业务异常统一使用 `ThrowBusinessException(...)`，不要返回魔法字符串。

## 4. 控制器（WebApi）

1. 在 `Takt.WebApi/Controllers/{领域}` 下创建 `TaktXxxController(s)`。
2. 必须继承 `TaktControllerBase`，并添加模块标识特性：`[ApiModule("模块分组", "模块说明")]`，再使用 `[Route("api/[controller]")]` 或显式路由。
3. 列表/详情/增删改方法统一加 `[TaktPermission("module:resource:action", "描述")]`：

```csharp
[HttpGet("list")]
[TaktPermission("humanresource:employee:list", "员工列表")]
public async Task<ActionResult<TaktPagedResult<TaktEmployeeDto>>> GetListAsync([FromQuery] TaktEmployeeQueryDto query)
{
    return await HandleExceptionAsync(() => _employeeService.GetPagedAsync(query));
}
```

4. 删除支持批量 ID（前端传字符串 ID 列表）。

## 5. 前端对接

> 前端路径根：`frontend/takt.antd/src`，规则见 `03-frontend.mdc`。

1. **types**：在 `types/{模块}` 下新增 `Xxx.ts`，与 DTO 对齐（ID 用 `string`）。
2. **api**：在 `api/{模块}` 下新增 `xxx.ts`：
   - URL 与后端路由一致，如 `/api/TaktEmployees/list`、`/api/TaktEmployees/{id}`。
   - 方法命名：`getXxxList`、`getXxxById`、`createXxx`、`updateXxx`、`deleteXxx`。
3. **views**：在 `views/{模块}/xxx` 下新增页面：
   - 列表页：表格 + 搜索（使用通用表格/查询条组件，如项目已有封装）。
   - 编辑弹窗/表单组件：只负责表单字段与校验。
4. **菜单与权限**：
   - 在菜单管理中增加菜单项，`path`、`component` 指向新页面。
   - 为列表、详情、新增、修改、删除按钮配置权限码，格式 `module:resource:action`，必须与后端 `[TaktPermission]` 保持一致。

## 6. 统一文件 / 注释规范（前后端）

- **后端 C#**
  - 文件位置与命名遵循本 Skill 与 `02-backend.mdc` 中的分层规则（Domain / Application / WebApi）。
  - 推荐保留统一文件头；公开类、接口和公共方法使用 XML 注释（`/// <summary>` 等）说明业务含义。
  - 注释只写「为什么 / 约束 / 边界条件」，不要逐行解释实现；修改逻辑时同步更新注释。

- **前端 TS / Vue**
  - TS/组件文件可在顶部用 JSDoc 风格块注释（`/** ... */`）说明文件职责，与 `03-frontend.mdc` 约定一致。
  - 对公共 API 函数、通用 hooks/composables 使用 JSDoc 标注参数与返回值；页面内部代码只在复杂逻辑处加少量行内注释。
  - 文件路径与模块一一对应：`api/`、`types/`、`views/`、`locales/` 与后端模块、控制器、DTO 命名保持一致，便于查找与维护。

## 7. 验证与自查清单

- [ ] 实体继承 `TaktEntityBase`，表/列命名符合 `01-data-design.mdc`。
- [ ] DTO 与实体字段对齐，ID 为 `string`。
- [ ] 应用服务接口/实现均已创建，方法为异步，异常通过 `TaktServiceBase` 抛出。
- [ ] 控制器继承 `TaktControllerBase`，REST 路由规范，权限码与菜单配置一致。
- [ ] 前端 `api/`、`types/`、`views/` 均已建立，对应后端接口，没有直接使用裸 axios。

按上面顺序执行，可以在 Takt 内快速新增一个规范的 CRUD 模块。+
