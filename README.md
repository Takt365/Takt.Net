# 节拍数字工厂 · Takt Digital Factory (TDF)

Takt.Net 为前后端分离的企业级应用：后端 .NET 9+ Web API，前端 Vue 3 + TypeScript + Ant Design Vue。

## 技术栈

| 端 | 技术 |
|----|------|
| 后端 | .NET 9+、ASP.NET Core、SqlSugar、OpenIddict、Serilog、Autofac、FluentValidation、内置 OpenAPI、Scalar |
| 前端 | Vue 3、Vite 7、TypeScript 5（ES2020）、Ant Design Vue 4、Pinia、Vue Router、Vue I18n、Axios、SignalR |

## 仓库结构

```
Takt.Net/
├── backend/                 # 后端
│   ├── Takt.Net.sln         # 解决方案
│   └── src/
│       ├── Takt.Domain/      # 领域：实体、仓储接口、验证
│       ├── Takt.Application/# 应用：DTO、服务接口与实现
│       ├── Takt.Infrastructure/ # 基础设施：数据、仓储、中间件、DI、SignalR
│       ├── Takt.Shared/      # 共享：模型、异常、帮助类
│       └── Takt.WebApi/      # Web API 入口、控制器
├── frontend/
│   └── takt.antd/           # 前端（Vue3 + Ant Design Vue）
│       ├── src/
│       │   ├── api/         # 按后端模块划分的 API
│       │   ├── types/      # 类型定义（对应后端 DTO）
│       │   ├── views/      # 页面
│       │   ├── stores/     # Pinia
│       │   ├── locales/    # 国际化 zh-CN / en-US
│       │   └── ...
│       ├── package.json
│       └── vite.config.ts
└── README.md
```

## 环境要求

- **后端**：.NET 9+ SDK、SQL Server（多库：Identity/Accounting/HumanResource/Logistics/Building/Routine，见 `appsettings` 中 `dbConfigs`）
- **前端**：Node.js 18+、npm

## 后端运行

```bash
cd backend

# 配置：复制示例并修改连接串
# 将 src/Takt.WebApi/appsettings.example.json 复制为 appsettings.json，并填写 dbConfigs 等

# 还原与运行
dotnet restore
dotnet run --project src/Takt.WebApi/Takt.WebApi.csproj
```

在 `appsettings.json` 中将 **`Swagger.Enabled`** 设为 `true` 后，启用 **Scalar** 与 **ASP.NET Core 内置 OpenAPI** 提供的 API 文档（配置节名 `Swagger` 与代码中的 `Swagger:Enabled` 对应）。HTTPS 端口以 `src/Takt.WebApi/Properties/launchSettings.json` 的 `applicationUrl` 为准（模板默认 **`https://localhost:60071`**）：

- **Scalar**：`/swagger`（根路径 `/` 会重定向到此处）
- **分组 OpenAPI JSON**：`/openapi/{documentName}.json`（`documentName`：`Accounting`、`Generator`、`HumanResource`、`Identity`、`Logistics`、`Routine`、`Statistics`、`Workflow`；与控制器 `[ApiModule("模块名", …)]` 一致，未分组接口在 `Identity` 文档中）

详见 `backend/src/Takt.WebApi/配置说明.md` 中的「API 文档」一节。

## 前端运行

```bash
cd frontend/takt.antd

# 安装依赖
npm install

# 开发（默认会代理到后端，需根据 .env.development 中 VITE_API_TARGET 配置）
npm run dev

# 生产构建
npm run build

# 预览生产构建
npm run preview
```

前端开发环境变量见 `.env.development`（如 `VITE_API_TARGET` 指向后端地址、端口、是否 HTTPS 等）。

## 配置说明

- **后端**：`backend/src/Takt.WebApi/appsettings.example.json` 含 Serilog、多库 `dbConfigs`（ConfigId 0~5）、OpenIddict、`Swagger.Enabled`（控制 Scalar/OpenAPI）等示例；复制为 `appsettings.json` 后按需修改。完整说明见同目录下 `配置说明.md`。
- **前端**：`frontend/takt.antd/.env.development` 含 API 基址、超时、开发服务器端口、HTTPS、代理目标等；生产构建使用 `.env.production`。

## 多库多租户架构

本项目采用**"按业务领域自动分库 + 租户行级数据隔离"**的混合架构，符合社区公认的**"启动注册 + 租户上下文"**最佳实践。

### 核心设计

1. **实体命名空间决定物理库**：仓储根据实体命名空间自动路由到对应数据库（如 `.HumanResource` → HR库）
2. **所有租户共享物理库**：通过 `ConfigId` 字段实现行级数据隔离
3. **启动时预注册连接**：应用启动时从 `appsettings.json` 读取所有 `dbConfigs`，预注册到 SqlSugar 租户管理器
4. **请求级上下文切换**：通过 `TaktTenantMiddleware` 中间件在请求生命周期内切换当前 ConfigId 上下文

### 数据库路由规则

| 实体命名空间包含 | 物理数据库 | ConfigId |
|----------------|-----------|----------|
| `.HumanResource` / `.HRM` | Takt_HumanResource_Dev | "1" |
| `.Routine` | Takt_Routine_Dev | "2" |
| `.Generator` / `.Workflow` / `.Code` | Takt_Building_Dev | "3" |
| `.Accounting` | Takt_Accounting_Dev | "4" |
| `.Logistics` / `.Statistics` | Takt_Logistics_Dev | "5" |
| 其他 | Takt_Identity_Dev | "0" |

### 关键概念区分

- **EntityPersistenceConfigId**：实体应存储的物理库（根据命名空间自动确定），仓储操作始终使用此值
- **CurrentConfigId**：当前请求的租户上下文（从 HTTP Header/Query 获取），用于业务逻辑判断

### 架构优势

- ✅ **自动化**：开发者无需关心数据库切换，仓储自动路由
- ✅ **安全性**：实体始终存储在正确的物理库，不会因租户上下文错误而错库
- ✅ **性能**：启动时预注册所有连接，运行时复用连接池，无动态创建开销
- ✅ **隔离性**：租户数据行级隔离，共享物理库，支持无限租户
- ✅ **可扩展**：新增业务领域只需添加映射规则和数据库配置

## 许可证

项目采用 MIT License；详见各子项目与文件头声明。
