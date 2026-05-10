---
name: takt-ant-design-vue-expert
description: >-
  Takt 前端 Vue 3 + Ant Design Vue 4、Pinia、Vue Router、Vite 下的视图与组件专家约定。
  在用户或任务涉及 .vue 页面、Ant Design Vue 表单/表格/模态、布局与主题、OpenAPI 类型对接、
  axios/request 封装、雪花 ID 字符串化、或前端目录与 ESLint/Vue TSC 时使用本 Skill；
  与 03-frontend.mdc 及 takt-csharp-sqlsugar-expert 中「F. 前端对接清单」配合：本 Skill 专注 UI 框架与交互模式，不重复后端 C# 规范。
---

# Takt · Ant Design Vue 专家 Skill

> 工程根：`frontend/takt.antd`。依赖版本以该目录 **`package.json`** 为准（当前说明：`vue` ^3.5、`ant-design-vue` ^4.2、`pinia` ^3、`vue-router` ^4、`vite`）。  
> 配套规则：`.cursor/rules/03-frontend.mdc`。全栈 CRUD 流程见 **`takt-admin-crud`**。

## 1. 目录与职责

- **`src/api/`**：按领域拆分请求；列表/导出等优先使用封装的 `request`（`src/api/request.ts`），以统一处理 **`TaktApiResult`**（`code` / `data` / `message`）。
- **`src/views/`**：页面与业务布局；路由懒加载与权限 meta 与后端菜单对齐。
- **`src/stores/`**：Pinia 全局状态（如 `identity/user`、`signalr`）；**token 写入规则以各 store 注释为准**（单一数据源）。
- **`src/types/`**：含 OpenAPI 生成的契约与 `IdS<'...'>` 等工具类型；**大整数 ID 在 TS 侧用 `string`**，避免 `number` 精度丢失。
- **`src/utils/`**：日志、jwt、事件总线等与 UI 解耦工具。

## 2. Ant Design Vue 4 使用要点（项目内）

- **表格**：`a-table` 远程分页与排序时，数据加载与 `loading` 必须异步；列上 long 型 ID 用 **字符串** 展示或 `customRender`，勿把后端雪花当 JS `Number` 运算。
- **表单**：`a-form` + `rules`；提交走 `async`；上传、远程搜索等强制异步（见 `03-frontend.mdc` 异步/同步表）。
- **反馈**：`message` / `Modal` / `notification` 与现有页面风格一致；错误文案优先走 i18n key。
- **布局**：侧栏/顶栏与现有 `layout` 一致，不引入第二套布局体系。

## 3. HTTP 与鉴权

- **常规 API**：使用 `@/api/request`，依赖响应拦截器解包 **`data`**（成功时返回业务体）。
- **裸 `axios` 场景**（如 OAuth `connect/token`、未挂拦截器的 `userinfo`）：响应体为 **`{ code, message, data, success }`** 时必须手动取 **`data`** 再赋给类型（参见 `src/api/identity/auth.ts` 中 `unwrapTaktApiData` 模式）；禁止把整段 `TaktApiResult` 当作业务 DTO。
- **Authorization**：由 `request` 拦截器或 store 持久化策略统一附加；SignalR 使用 `accessTokenFactory`，与后端 Hub `[Authorize]` 一致。

## 4. Vue 3 组合式 API

- 新页面默认 **`<script setup lang="ts">`**；`ref` / `computed` / `watch` 类型明确；异步边界清晰。
- 路由守卫、权限、字典/i18n 加载顺序遵循现有 `router` 与 `App` 入口实现，不擅自改为另一套初始化顺序。

## 5. 国际化与字典

- 文案：`vue-i18n`，key 与后端资源或菜单约定一致；勿硬编码中文到可复用组件。
- 字典与翻译加载逻辑以现有路由守卫与 store 为准（见工程内 `router`、`stores` 实现）。

## 6. 何时改用其它 Skill

- **后端接口、DTO、SqlSugar、完整 CRUD 流程**：**`takt-csharp-sqlsugar-expert`**。
- **RBAC、菜单、权限码、数据范围**：**`takt-rbac-abac-expert`**。
- 本 Skill **不**定义新的 API 命名或后端路由规则；以前端现有实现与 `03-frontend.mdc` 为权威。
