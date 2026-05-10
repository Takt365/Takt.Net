// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Filters
// 文件名称：TaktApiResultResponseFilter.cs
// 创建时间：2026-05-06
// 创建人：Takt365(Cursor AI)
// 功能描述：自动将控制器返回值包装为 TaktApiResult 统一响应格式
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Takt.Shared.Enums;
using Takt.Shared.Models;

namespace Takt.WebApi.Filters;

/// <summary>
/// 统一响应格式过滤器：自动将 ActionResult 的返回值包装为 TaktApiResult 格式。
/// 这样控制器可以直接返回业务数据，无需手动包装 TaktApiResult。
/// </summary>
/// <remarks>
/// 工作原理：
/// 1. 拦截控制器的 OnResultExecuting 事件
/// 2. 检查返回结果是否为 ObjectResult（即 Ok(result)、BadRequest(result) 等）
/// 3. 如果返回的数据不是 TaktApiResult 类型，自动包装为 TaktApiResult
/// 4. 如果已经是 TaktApiResult 类型，则不处理（避免重复包装）
/// 
/// 优势：
/// - 控制器代码更简洁：return Ok(data) 而非 return Ok(TaktApiResult<T>.Ok(data))
/// - 统一响应格式：所有接口都返回 { code, message, data, success }
/// - 前端处理一致：拦截器统一提取 data 字段
/// </remarks>
public class TaktApiResultResponseFilter : IResultFilter
{
    /// <summary>
    /// 在结果执行前包装响应数据为 TaktApiResult 格式
    /// </summary>
    public void OnResultExecuting(ResultExecutingContext context)
    {
        // 只处理 ObjectResult（Ok、BadRequest、NotFound 等返回的对象）
        if (context.Result is ObjectResult objectResult)
        {
            var value = objectResult.Value;
            
            // 如果值为 null，不处理
            if (value == null)
            {
                return;
            }

            var valueType = value.GetType();
            
            // 如果已经是 TaktApiResult 或 TaktApiResult<T> 类型，不重复包装
            if (IsTaktApiResultType(valueType))
            {
                return;
            }

            // 检查是否为问题详情（ProblemDetails）或验证问题（ValidationProblemDetails）
            // 这些是 ASP.NET Core 标准错误格式，不应该包装
            if (value is Microsoft.AspNetCore.Mvc.ProblemDetails || 
                value is Microsoft.AspNetCore.Mvc.ValidationProblemDetails)
            {
                return;
            }

            // 根据 HTTP 状态码决定包装结果
            var statusCode = objectResult.StatusCode ?? context.HttpContext.Response.StatusCode;
            
            // 成功状态码（2xx）包装为成功结果
            if (statusCode >= 200 && statusCode < 300)
            {
                // 使用反射创建 TaktApiResult<T>
                var wrappedResult = CreateSuccessResult(value, valueType);
                if (wrappedResult != null)
                {
                    objectResult.Value = wrappedResult;
                }
            }
            // 错误状态码包装为失败结果
            else
            {
                var wrappedResult = CreateFailResult(value, statusCode);
                if (wrappedResult != null)
                {
                    objectResult.Value = wrappedResult;
                }
            }
        }
    }

    /// <summary>
    /// 结果执行后（无需处理）
    /// </summary>
    public void OnResultExecuted(ResultExecutedContext context)
    {
        // 无需处理
    }

    /// <summary>
    /// 检查类型是否为 TaktApiResult 或 TaktApiResult<T>
    /// </summary>
    private static bool IsTaktApiResultType(Type type)
    {
        // 检查是否为 TaktApiResult（非泛型）
        if (type == typeof(TaktApiResult))
        {
            return true;
        }

        // 检查是否为 TaktApiResult<T>（泛型）
        if (type.IsGenericType)
        {
            var genericDefinition = type.GetGenericTypeDefinition();
            if (genericDefinition == typeof(TaktApiResult<>))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 创建成功结果包装
    /// </summary>
    private static object? CreateSuccessResult(object data, Type dataType)
    {
        try
        {
            // 获取 TaktApiResult<T>.Ok 方法
            var genericType = typeof(TaktApiResult<>).MakeGenericType(dataType);
            var okMethod = genericType.GetMethod("Ok", new[] { dataType, typeof(string) });
            
            if (okMethod != null)
            {
                // 调用 TaktApiResult<T>.Ok(data, "操作成功")
                return okMethod.Invoke(null, new object?[] { data, "操作成功" });
            }
        }
        catch (Exception ex)
        {
            // 记录错误但不影响正常流程
            System.Diagnostics.Debug.WriteLine($"[TaktApiResultResponseFilter] 创建成功结果包装失败: {ex.Message}");
        }
        
        return null;
    }

    /// <summary>
    /// 创建失败结果包装
    /// </summary>
    private static object? CreateFailResult(object data, int statusCode)
    {
        try
        {
            // 根据状态码映射到 TaktResultCode
            var resultCode = MapStatusCodeToResultCode(statusCode);
            
            // 提取错误消息
            string message = ExtractErrorMessage(data) ?? "操作失败";
            
            // 创建 TaktApiResult（非泛型）
            var failMethod = typeof(TaktApiResult).GetMethod("Fail", new[] { typeof(string), typeof(TaktResultCode) });
            
            if (failMethod != null)
            {
                return failMethod.Invoke(null, new object?[] { message, resultCode });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[TaktApiResultResponseFilter] 创建失败结果包装失败: {ex.Message}");
        }
        
        return null;
    }

    /// <summary>
    /// 将 HTTP 状态码映射到 TaktResultCode
    /// </summary>
    private static TaktResultCode MapStatusCodeToResultCode(int statusCode)
    {
        return statusCode switch
        {
            400 => TaktResultCode.Failed,
            401 => TaktResultCode.Unauthorized,
            403 => TaktResultCode.Forbidden,
            404 => TaktResultCode.NotFound,
            500 => TaktResultCode.ServerError,
            _ => TaktResultCode.BusinessError
        };
    }

    /// <summary>
    /// 从数据中提取错误消息
    /// </summary>
    private static string? ExtractErrorMessage(object data)
    {
        // 如果是字符串，直接返回
        if (data is string str)
        {
            return str;
        }

        // 如果是对象，尝试获取 Message 属性
        var messageType = data.GetType().GetProperty("Message");
        if (messageType != null)
        {
            return messageType.GetValue(data)?.ToString();
        }

        // 尝试获取 message 属性（camelCase）
        var messageProperty = data.GetType().GetProperty("message");
        if (messageProperty != null)
        {
            return messageProperty.GetValue(data)?.ToString();
        }

        // 尝试获取 Title 属性（ProblemDetails）
        var titleProperty = data.GetType().GetProperty("Title");
        if (titleProperty != null)
        {
            return titleProperty.GetValue(data)?.ToString();
        }

        return null;
    }
}
