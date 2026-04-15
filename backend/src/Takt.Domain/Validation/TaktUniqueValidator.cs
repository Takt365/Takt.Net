// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Validation
// 文件名称：TaktUniqueValidator.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：创建/更新/导入时的查重验证（唯一性校验），非对已有数据去重。
// 建议：表建唯一约束，服务层捕获唯一约束异常以防并发竞态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Validators;
using Takt.Domain.Entities;
using Takt.Domain.Repositories;

namespace Takt.Domain.Validation;

/// <summary>
/// 查重验证器：创建/更新/导入时验证字段在数据库中是否已存在（唯一性校验）。
/// </summary>
/// <typeparam name="T">验证对象类型</typeparam>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TProperty">属性类型</typeparam>
public class TaktUniqueValidator<T, TEntity, TProperty> : AsyncPropertyValidator<T, TProperty>
    where TEntity : TaktEntityBase
{
    private readonly ITaktRepository<TEntity> _repository;
    private readonly Expression<Func<TEntity, TProperty>> _fieldSelector;
    private readonly Func<T, long?>? _excludeIdSelector;
    private readonly string? _errorMessage;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">仓储接口</param>
    /// <param name="fieldSelector">字段选择器（从实体到属性）</param>
    /// <param name="excludeIdSelector">排除ID选择器（可选，更新时使用）</param>
    /// <param name="errorMessage">错误消息（可选）</param>
    public TaktUniqueValidator(
        ITaktRepository<TEntity> repository,
        Expression<Func<TEntity, TProperty>> fieldSelector,
        Func<T, long?>? excludeIdSelector = null,
        string? errorMessage = null)
    {
        _repository = repository;
        _fieldSelector = fieldSelector;
        _excludeIdSelector = excludeIdSelector;
        _errorMessage = errorMessage;
    }

    /// <summary>
    /// 验证器名称
    /// </summary>
    public override string Name => "TaktUniqueValidator";

    /// <summary>
    /// 执行异步验证
    /// </summary>
    public override async Task<bool> IsValidAsync(ValidationContext<T> context, TProperty value, CancellationToken cancellation)
    {
        // 如果值为空或空字符串，跳过验证
        if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
            return true;

        var fieldName = TaktUniqueValidatorExtensions.GetMemberName(_fieldSelector);

        // 构建查询表达式：field == value
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(parameter, fieldName);
        var constant = Expression.Constant(value, typeof(TProperty));
        var equals = Expression.Equal(property, constant);
        Expression? combinedExpression = equals;

        // 如果指定了排除ID，添加条件：Id != excludeId
        if (_excludeIdSelector != null)
        {
            var excludeId = _excludeIdSelector(context.InstanceToValidate);
            if (excludeId.HasValue)
            {
                var idProperty = Expression.Property(parameter, "Id");
                var idConstant = Expression.Constant(excludeId.Value);
                var idNotEquals = Expression.NotEqual(idProperty, idConstant);
                combinedExpression = Expression.AndAlso(equals, idNotEquals);
            }
        }

        var predicate = Expression.Lambda<Func<TEntity, bool>>(combinedExpression, parameter);

        // 检查是否存在
        var exists = await _repository.ExistsAsync(predicate);
        
        // 如果存在，设置错误消息
        if (exists)
        {
            var message = _errorMessage ?? $"{fieldName} {value} 已存在";
            context.MessageFormatter.AppendArgument("PropertyName", fieldName);
            context.MessageFormatter.AppendArgument("PropertyValue", value);
            return false;
        }

        return true;
    }

    /// <summary>
    /// 获取默认错误消息模板
    /// </summary>
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return _errorMessage ?? "{PropertyName} {PropertyValue} 已存在";
    }
}

/// <summary>
/// FluentValidation 扩展方法：添加唯一性验证规则
/// </summary>
public static class TaktUniqueValidatorExtensions
{
    /// <summary>
    /// 验证字段在数据库中是否唯一（不存在）
    /// </summary>
    /// <typeparam name="T">验证对象类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="repository">仓储接口</param>
    /// <param name="fieldSelector">字段选择器（从实体到属性）</param>
    /// <param name="excludeIdSelector">排除ID选择器（可选，更新时使用）</param>
    /// <param name="errorMessage">错误消息（可选）</param>
    /// <returns>规则构建器</returns>
    public static IRuleBuilderOptions<T, TProperty> MustBeUniqueAsync<T, TEntity, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder,
        ITaktRepository<TEntity> repository,
        Expression<Func<TEntity, TProperty>> fieldSelector,
        Func<T, long?>? excludeIdSelector = null,
        string? errorMessage = null)
        where TEntity : TaktEntityBase
    {
        return ruleBuilder.MustAsync(async (instance, value, cancellationToken) =>
        {
            // 如果值为空或空字符串，跳过验证
            if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                return true;

            var fieldName = GetMemberName(fieldSelector);

            // 构建查询表达式：field == value
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(parameter, fieldName);
            var constant = Expression.Constant(value, typeof(TProperty));
            var equals = Expression.Equal(property, constant);
            Expression? combinedExpression = equals;

            // 如果指定了排除ID，添加条件：Id != excludeId
            if (excludeIdSelector != null)
            {
                var excludeId = excludeIdSelector(instance);
                if (excludeId.HasValue)
                {
                    var idProperty = Expression.Property(parameter, "Id");
                    var idConstant = Expression.Constant(excludeId.Value);
                    var idNotEquals = Expression.NotEqual(idProperty, idConstant);
                    combinedExpression = Expression.AndAlso(equals, idNotEquals);
                }
            }

            var predicate = Expression.Lambda<Func<TEntity, bool>>(combinedExpression, parameter);

            // 检查是否存在
            var exists = await repository.ExistsAsync(predicate);
            return !exists;
        }).WithMessage(errorMessage ?? "{PropertyName} {PropertyValue} 已存在");
    }

    /// <summary>
    /// 验证字段在数据库中是否唯一（用于导入等手动验证场景）
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    /// <param name="repository">仓储接口</param>
    /// <param name="fieldSelector">字段选择器（从实体到属性）</param>
    /// <param name="value">字段值</param>
    /// <param name="excludeId">排除的ID（可选，更新时使用）</param>
    /// <param name="errorMessage">错误消息（可选）</param>
    /// <returns>验证任务，如果唯一返回true，否则抛出异常</returns>
    public static async Task ValidateUniqueAsync<TEntity, TProperty>(
        ITaktRepository<TEntity> repository,
        Expression<Func<TEntity, TProperty>> fieldSelector,
        TProperty? value,
        long? excludeId = null,
        string? errorMessage = null)
        where TEntity : TaktEntityBase
    {
        // 如果值为空或空字符串，跳过验证
        if (value == null)
            return;
        
        // 如果是字符串且为空，跳过验证
        if (value is string str && string.IsNullOrWhiteSpace(str))
            return;

        var fieldName = GetMemberName(fieldSelector);

        // 构建查询表达式：field == value
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(parameter, fieldName);
        var constant = Expression.Constant(value, typeof(TProperty));
        var equals = Expression.Equal(property, constant);
        Expression? combinedExpression = equals;

        // 如果指定了排除ID，添加条件：Id != excludeId
        if (excludeId.HasValue)
        {
            var idProperty = Expression.Property(parameter, "Id");
            var idConstant = Expression.Constant(excludeId.Value);
            var idNotEquals = Expression.NotEqual(idProperty, idConstant);
            combinedExpression = Expression.AndAlso(equals, idNotEquals);
        }

        var predicate = Expression.Lambda<Func<TEntity, bool>>(combinedExpression, parameter);

        // 检查是否存在
        var exists = await repository.ExistsAsync(predicate);
        if (exists)
        {
            var message = errorMessage ?? $"{fieldName} {value} 已存在";
            throw new Takt.Shared.Exceptions.TaktBusinessException(message);
        }
    }

    /// <summary>
    /// 验证字段在数据库中是否唯一（支持额外条件，用于导入等手动验证场景）
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    /// <param name="repository">仓储接口</param>
    /// <param name="fieldSelector">字段选择器（从实体到属性）</param>
    /// <param name="value">字段值</param>
    /// <param name="additionalCondition">额外条件表达式（可选，如 DictTypeCode、CultureCode 等）</param>
    /// <param name="excludeId">排除的ID（可选，更新时使用）</param>
    /// <param name="errorMessage">错误消息（可选）</param>
    /// <returns>验证任务，如果唯一返回true，否则抛出异常</returns>
    public static async Task ValidateUniqueAsync<TEntity, TProperty>(
        ITaktRepository<TEntity> repository,
        Expression<Func<TEntity, TProperty>> fieldSelector,
        TProperty? value,
        Expression<Func<TEntity, bool>>? additionalCondition = null,
        long? excludeId = null,
        string? errorMessage = null)
        where TEntity : TaktEntityBase
    {
        // 如果值为空或空字符串，跳过验证
        if (value == null)
            return;
        
        // 如果是字符串且为空，跳过验证
        if (value is string str && string.IsNullOrWhiteSpace(str))
            return;

        var fieldName = GetMemberName(fieldSelector);

        // 构建查询表达式：field == value
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(parameter, fieldName);
        var constant = Expression.Constant(value, typeof(TProperty));
        var equals = Expression.Equal(property, constant);
        Expression? combinedExpression = equals;

        // 如果指定了额外条件，添加条件
        if (additionalCondition != null)
        {
            // 将额外条件的参数替换为当前参数
            var visitor = new ParameterReplacer(additionalCondition.Parameters[0], parameter);
            var replacedCondition = (Expression<Func<TEntity, bool>>)visitor.Visit(additionalCondition);
            combinedExpression = Expression.AndAlso(equals, replacedCondition.Body);
        }

        // 如果指定了排除ID，添加条件：Id != excludeId
        if (excludeId.HasValue)
        {
            var idProperty = Expression.Property(parameter, "Id");
            var idConstant = Expression.Constant(excludeId.Value);
            var idNotEquals = Expression.NotEqual(idProperty, idConstant);
            combinedExpression = Expression.AndAlso(combinedExpression, idNotEquals);
        }

        var predicate = Expression.Lambda<Func<TEntity, bool>>(combinedExpression, parameter);

        // 检查是否存在
        var exists = await repository.ExistsAsync(predicate);
        if (exists)
        {
            var message = errorMessage ?? $"{fieldName} {value} 已存在";
            throw new Takt.Shared.Exceptions.TaktBusinessException(message);
        }
    }

    /// <summary>
    /// 查重：按条件表达式验证是否存在记录（通用多字段组合唯一性，创建/更新/导入时使用）。
    /// 若存在则抛出 TaktBusinessException。示例：ValidateUniqueAsync(_repo, x =&gt; x.A == a &amp;&amp; x.B == b, excludeId, "组合已存在")。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="repository">仓储接口</param>
    /// <param name="predicate">条件表达式（由调用方编写，如 u =&gt; u.UserName == name &amp;&amp; u.UserType == type &amp;&amp; u.UserEmail == email）</param>
    /// <param name="excludeId">排除的ID（可选，更新时使用）</param>
    /// <param name="errorMessage">错误消息（可选）</param>
    public static async Task ValidateUniqueAsync<TEntity>(
        ITaktRepository<TEntity> repository,
        Expression<Func<TEntity, bool>> predicate,
        long? excludeId = null,
        string? errorMessage = null)
        where TEntity : TaktEntityBase
    {
        if (predicate == null)
            return;

        var parameter = predicate.Parameters[0];
        Expression combinedExpression = predicate.Body;

        if (excludeId.HasValue)
        {
            var idProperty = Expression.Property(parameter, "Id");
            var idConstant = Expression.Constant(excludeId.Value);
            var idNotEquals = Expression.NotEqual(idProperty, idConstant);
            combinedExpression = Expression.AndAlso(predicate.Body, idNotEquals);
        }

        var lambda = Expression.Lambda<Func<TEntity, bool>>(combinedExpression, parameter);
        var exists = await repository.ExistsAsync(lambda);
        if (exists)
        {
            var message = errorMessage ?? "记录已存在";
            throw new Takt.Shared.Exceptions.TaktBusinessException(message);
        }
    }

    /// <summary>
    /// 从属性访问表达式中解析成员名，支持直接属性访问与带类型转换的 UnaryExpression（避免 InvalidCastException）。
    /// </summary>
    public static string GetMemberName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> selector)
    {
        if (selector?.Body is MemberExpression member)
            return member.Member.Name;
        if (selector?.Body is UnaryExpression unary && unary.Operand is MemberExpression unaryMember)
            return unaryMember.Member.Name;
        throw new ArgumentException("表达式必须是简单的属性访问表达式", nameof(selector));
    }

    /// <summary>
    /// 表达式参数替换访问器
    /// </summary>
    private class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;

        public ParameterReplacer(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _oldParameter ? _newParameter : base.VisitParameter(node);
        }
    }
}
