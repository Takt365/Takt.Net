# 节拍数字工厂 · Takt Digital Factory (TDF)

Takt.Net 为前后端分离的企业级应用：后端 .NET 8 Web API，前端 Vue 3 + TypeScript + Ant Design Vue。

## 技术栈

| 端 | 技术 |
|----|------|
| 后端 | .NET 8、ASP.NET Core、SqlSugar、OpenIddict、Serilog、Autofac、FluentValidation、Swagger |
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

- **后端**：.NET 8 SDK、SQL Server（多库：Identity/Accounting/HumanResource/Logistics/Building/Routine，见 `appsettings` 中 `dbConfigs`）
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

默认启动后可通过 Swagger 访问 API 文档（具体端口见 `launchSettings.json`）。

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

- **后端**：`backend/src/Takt.WebApi/appsettings.example.json` 含 Serilog、多库 `dbConfigs`（ConfigId 0~5）、OpenIddict 等示例；复制为 `appsettings.json` 后按需修改。
- **前端**：`frontend/takt.antd/.env.development` 含 API 基址、超时、开发服务器端口、HTTPS、代理目标等；生产构建使用 `.env.production`。

## 许可证

项目采用 MIT License；详见各子项目与文件头声明。
