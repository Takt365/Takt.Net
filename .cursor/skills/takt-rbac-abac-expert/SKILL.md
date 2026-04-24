---
name: takt-rbac-abac-expert
description: >-
  Takt 后台 RBAC（用户-角色-菜单/按钮权限码）与数据范围（角色-部门等）配置专家指南。
  在用户或任务涉及 TaktPermission、菜单种子、角色授权、前后端权限码对齐、或按部门/角色过滤数据
  （与 RBAC 配套的上下文/资源级策略）时使用本 Skill。纯 C#+SqlSugar 分层与 CRUD 见 takt-csharp-sqlsugar-expert。
---

# Takt · RBAC / 数据权限专家 Skill

> **适用**：在 Takt 中新增或修改权限码、菜单、按钮权限，或为新接口加访问控制；配置角色-菜单、角色-部门等数据范围。  
> **参考规则**：`.cursor/rules/01-data-design.mdc`、`02-backend.mdc`。

## 1. 权限模型概览（RBAC）

核心表（见 `01-data-design.mdc`）：

- `takt_identity_user`：登录用户。
- `takt_identity_role`：角色。
- `takt_identity_menu`：菜单 / 按钮（树形）。
- `takt_identity_role_menu`：角色-菜单多对多（实际功能权限绑定）。
- `takt_humanresource_organization_roledept`：角色-部门（**数据范围**，与接口功能权限配合）。

权限流转：**User → Role → Menu(Permission)**。  
用户通过角色获得菜单及其下按钮的权限码；**部门类数据权限**通过角色-部门关系在应用层过滤（资源/上下文维度，与纯 RBAC 菜单码互补）。

## 2. 权限码格式

统一使用**四段**、**小写**、**冒号**分隔（段内不要下划线）：

```text
领域:目录:实体:key
```

示例：

- `workflow:code:scheme:list`：流程方案列表。
- `workflow:code:scheme:create`：新建流程方案。
- `humanresource:organization:employee:update`：修改员工档案。

约定：

- 前三段与代码生成表配置 `perms_prefix`（规范化后 `perms_prefix_canonical`）对齐；第四段为统一动作（见下节）。
- `key` / `action`：列表、详情、增删改等（见下节）。

### 常用动作

- `list`：列表页访问/查询。
- `query` / `info`：详情查询。
- `create`：新增。
- `update`：修改。
- `delete`：删除。

## 3. 后端权限标注（TaktPermission）

在 WebApi 控制器中使用 `[TaktPermission]`：

```csharp
[HttpGet("list")]
[TaktPermission("workflow:code:scheme:list", "流程方案列表")]
public async Task<ActionResult<TaktPagedResult<TaktFlowSchemeDto>>> GetFlowSchemeListAsync([FromQuery] TaktFlowSchemeQueryDto query)
{
    return await HandleExceptionAsync(() => _schemeService.GetPagedAsync(query));
}
```

- 第一个参数：权限码，必须与菜单中按钮的 `Permission` 字段一致。
- 第二个参数：中文说明，用于生成权限/菜单文档或 OpenAPI 中的操作说明。
- 不需要权限但需要登录时：按项目约定使用 `[AllowAnonymous]` 或显式放行（以 `02-backend.mdc` 为准）。

## 4. 菜单与按钮配置

在菜单管理中为每个页面和按钮配置：

- **目录/菜单**：
  - `type`：目录/菜单。
  - `path`：前端路由。
  - `component`：前端组件路径（如 `workflow/scheme/index`）。
  - 一般不需要权限码（也可以只给访问权限）。
- **按钮**：
  - `type`：按钮。
  - `permission`：按钮对应权限码，例如 `workflow:code:scheme:create`。
  - 该按钮在前端通过权限指令控制显隐，后端通过 `[TaktPermission]` 控制访问。

## 5. 前后端权限对应

约定：**同一权限码在三处一致**：

1. 数据库 `takt_identity_menu.permission`（按钮）。
2. 后端控制器 `[TaktPermission("a:b:c:key")]`（四段）。
3. 前端 `hasPermission('a:b:c:key')` 或权限指令。

如果其中任一处拼写不一致，将导致按钮不显示或接口被拒绝。

## 6. 多租户与数据范围（与 RBAC 配合）

- **多租户 / 分库**：实体通过 `ConfigId` 与命名空间分库路由（见 `01-data-design.mdc`、`takt-csharp-sqlsugar-expert`）；功能权限码本身不替代租户隔离。
- **部门数据范围**：通过 `takt_humanresource_organization_roledept` 建模；应用服务按当前用户**角色与部门关系**过滤数据，属于**基于属性的访问控制**在 Takt 中的落地方式之一，须在服务层显式实现，勿与菜单权限码混为一谈。

## 7. 使用本 Skill 的典型场景

- 为新接口加权限：
  1. 决定权限码 `领域:目录:实体:key`。
  2. 在控制器方法上加 `[TaktPermission]`。
  3. 在菜单中为对应按钮配置同样的权限码。
  4. 前端使用 `hasPermission` 控制按钮显隐。

- 新增一个模块的菜单与权限：
  1. 为模块创建目录 → 菜单 → 按钮（list/create/update/delete）。
  2. 为每个按钮定义权限码，并与控制器方法对齐。
  3. 把菜单授权给角色，用户通过角色获得权限。

以上规则以当前 Takt 工程与 `.cursor/rules/01-data-design.mdc` / `02-backend.mdc` 为准。
