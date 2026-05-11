// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi
// 文件名称：GlobalUsings.cs
// 创建时间：2026-05-07
// 创建人：Takt365
// 功能描述：Takt.WebApi 全局 using，定义 WebApi 层全局命名空间
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

// System 命名空间
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;

// ASP.NET Core 命名空间
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;

// Takt 共享层
global using Takt.Shared.Models;
global using Takt.Shared.Helpers;
global using Takt.Shared.Exceptions;

// Takt 领域接口
global using Takt.Domain.Interfaces;

// Takt 基础设施
global using Takt.Infrastructure.Attributes;
global using Takt.Infrastructure.Tenant;
global using Takt.Infrastructure.User;

// Takt WebApi
global using Takt.WebApi.Controllers;
