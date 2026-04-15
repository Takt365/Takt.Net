---
name: takt-admin-permission
description: Takt 项目 RBAC 权限与菜单配置指南。用于配置角色/菜单/按钮权限、TaktPermission 特性以及与前端权限指令的对应关系。
---

# Takt 权限与菜单 Skill

> 适用：在 Takt 中新增/修改权限码、菜单、按钮权限，或为新接口加权限控制时使用。  
> 参考规则：`.cursor/rules/01-data-design.mdc`、`02-backend.mdc`。

## 1. 权限模型概览

核心表（见 `01-data-design.mdc`）：

- `takt_identity_user`：登录用户。
- `takt_identity_role`：角色。
- `takt_identity_menu`：菜单 / 按钮（树形）。
- `takt_identity_role_menu`：角色-菜单多对多（实际权限绑定）。
- `takt_humanresource_organization_roledept`：角色-部门数据权限。

权限流转：**User → Role → Menu(Permission)**。  
用户通过角色获得菜单及其下按钮的权限码。

## 2. 权限码格式

统一使用：

```text
module:resource:action
```

示例：

- `workflow:scheme:list`：流程方案列表。
- `workflow:scheme:create`：新建流程方案。
- `humanresource:employee:update`：修改员工档案。

约定：

- `module`：大模块，如 `identity`、`humanresource`、`workflow`。
- `resource`：资源名，如 `user`、`role`、`employee`、`scheme`。
- `action`：统一动作（见下节）。

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
[TaktPermission("workflow:scheme:list", "流程方案列表")]
public async Task<ActionResult<TaktPagedResult<TaktFlowSchemeDto>>> GetListAsync([FromQuery] TaktFlowSchemeQueryDto query)
{
    return await HandleExceptionAsync(() => _schemeService.GetPagedAsync(query));
}
```

- 第一个参数：权限码，必须与菜单中按钮的 `Permission` 字段一致。
- 第二个参数：中文说明，用于生成权限/菜单文档或 Swagger 描述。
- 不需要权限但需要登录时使用 `[AllowAnonymous]` 之外的项目自定义特性（若有），或在业务中显式允许。

## 4. 菜单与按钮配置

在菜单管理中为每个页面和按钮配置：

- **目录/菜单**：
  - `type`：目录/菜单。
  - `path`：前端路由。
  - `component`：前端组件路径（如 `workflow/scheme/index`）。
  - 一般不需要权限码（也可以只给访问权限）。
- **按钮**：
  - `type`：按钮。
  - `permission`：按钮对应权限码，例如 `workflow:scheme:create`。
  - 该按钮在前端通过权限指令控制显隐，后端通过 `[TaktPermission]` 控制访问。

## 5. 前后端权限对应

约定：**同一权限码在三处一致**：

1. 数据库 `takt_identity_menu.permission`（按钮）。
2. 后端控制器 `[TaktPermission("xxx:yyy:zzz")]`。
3. 前端 `hasPermission('xxx:yyy:zzz')` 或权限指令。

如果其中任一处拼写不一致，将导致按钮不显示或接口被拒绝。

## 6. 多租户与数据权限（简要）

- 多租户：实体通过 `ConfigId` 与命名空间进行分库路由（见 `01-data-design.mdc`），权限本身不区分租户，由当前登录用户所在租户与角色决定。
- 部门数据权限：通过 `takt_humanresource_organization_roledept` 建模，不直接写死在代码中；应用服务按当前用户角色和部门关系过滤数据。

## 7. 使用本 Skill 的典型场景

- 为新接口加权限：
  1. 决定权限码 `module:resource:action`。
  2. 在控制器方法上加 `[TaktPermission]`。
  3. 在菜单中为对应按钮配置同样的权限码。
  4. 前端使用 `hasPermission` 控制按钮显隐。

- 新增一个模块的菜单与权限：
  1. 为模块创建目录 → 菜单 → 按钮（list/create/update/delete）。
  2. 为每个按钮定义权限码，并与控制器方法对齐。
  3. 把菜单授权给角色，用户通过角色获得权限。

以上规则完全基于当前 Takt 工程与 `.cursor/rules/01-data-design.mdc` / `02-backend.mdc` 的定义，不涉及 NestJS 或其他项目实现。+
