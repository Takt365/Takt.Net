using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;
using Takt.Shared.Exceptions;

namespace Takt.WebApi.Filters;

/// <summary>
/// FluentValidation 全局执行过滤器：对当前 Action 的参数按类型自动查找并执行验证器。
/// </summary>
public class TaktFluentValidationActionFilter : IAsyncActionFilter
{
    /// <summary>
    /// 执行 Action 前验证参数。
    /// </summary>
    /// <param name="context">执行上下文</param>
    /// <param name="next">后续管道</param>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var failures = new List<ValidationFailure>();

        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument == null)
                continue;

            var argumentType = argument.GetType();
            var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);
            var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

            if (validator == null)
                continue;

            var validationContext = new ValidationContext<object>(argument);
            var validationResult = await validator.ValidateAsync(validationContext, context.HttpContext.RequestAborted);
            if (!validationResult.IsValid)
                failures.AddRange(validationResult.Errors);
        }

        if (failures.Count > 0)
        {
            var message = string.Join("；", failures.Select(f => f.ErrorMessage).Distinct());
            var validationErrors = failures
                .Where(f => !string.IsNullOrWhiteSpace(f.PropertyName) && !string.IsNullOrWhiteSpace(f.ErrorMessage))
                .Select(f => new
                {
                    field = ToCamelCase(f.PropertyName),
                    message = f.ErrorMessage
                })
                .Distinct()
                .ToList();

            var ex = new TaktBusinessException($"参数校验失败: {message}", "ValidationError");
            ex.Data["validationErrors"] = validationErrors;
            throw ex;
        }

        await next();
    }

    private static string ToCamelCase(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;
        if (value.Length == 1)
            return value.ToLowerInvariant();
        return char.ToLowerInvariant(value[0]) + value[1..];
    }
}
