# 节拍数字工厂 · Takt Digital Factory (TDF)

> ⚠️ **重要声明**：本项目为 **AI 智能生成**（使用 Cursor AI 等 AI 辅助开发工具），代码由 AI 自动生成并优化。
> 
> 🚫 **不接受任何 Issue**：由于本项目是 AI 生成项目，我们**不接受任何形式的 Issue、Bug 报告或功能请求**。如有需要，请 Fork 后自行修改。

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

本项目采用**“按业务领域自动分库 + 租户行级数据隔离”**的多租户架构。

### 如何使用多租户

#### 1. 配置多租户

在 `appsettings.json` 中配置：

```json
{
  "Tenant": {
    "Enabled": true,  // true=启用多租户，false=单租户模式
    "DefaultConfigId": "0,1,2",  // 默认共享库（所有租户可访问）
    "ConfigIdHeaderName": "X-Config-Id",  // HTTP Header 名称
    "ConfigIdQueryName": "configId"  // Query 参数名称
  }
}
```

#### 2. 请求时传递 ConfigId

**方式一：HTTP Header（推荐）**
```
X-Config-Id: 1
```

**方式二：Query 参数**
```
https://api.example.com/endpoint?configId=1
```

#### 3. ConfigId 说明

| ConfigId | 数据库 | 说明 |
|----------|--------|------|
| 0 | Identity库 | 身份认证 |
| 1 | HR库 | 人力资源 |
| 2 | 日常库 | 日常运营 |
| 3 | 生成器库 | 工作流/代码生成 |
| 4 | 财务库 | 财务管理 |
| 5 | 物流库 | 物流/统计 |

**权限控制规则**：
- `DefaultConfigId` 中配置的 ConfigId（如 `"0,1,2"`）为**共享库**，所有用户均可无条件访问
- 其他 ConfigId（如 `3,4,5`）需要通过租户的 `AllowedConfigIds` 配置授权
- 超级管理员（`UserType=2`）可访问所有 ConfigId

## 许可证

项目采用 MIT License；详见各子项目与文件头声明。
