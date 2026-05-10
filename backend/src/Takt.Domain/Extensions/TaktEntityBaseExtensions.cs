// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Extensions
// 文件名称：TaktEntityBaseExtensions.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt实体基类扩展方法，提供扩展字段JSON的类型安全访问功能
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Domain.Entities;
using Takt.Shared.Helpers;

namespace Takt.Domain.Extensions;

/// <summary>
/// Takt实体基类扩展方法
/// </summary>
public static class TaktEntityBaseExtensions
{
    /// <summary>
    /// 获取扩展字段字典
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <returns>扩展字段字典，如果为空则返回空字典</returns>
    public static Dictionary<string, object?> GetExtFields(this TaktEntityBase entity)
    {
        if (string.IsNullOrWhiteSpace(entity.ExtFieldJson))
            return new Dictionary<string, object?>();

        try
        {
            var fields = JsonConvert.DeserializeObject<Dictionary<string, object?>>(entity.ExtFieldJson);
            return fields ?? new Dictionary<string, object?>();
        }
        catch (JsonException ex)
        {
            TaktLogger.Warning(ex, "扩展字段JSON反序列化失败，实体ID: {EntityId}", entity.Id);
            return new Dictionary<string, object?>();
        }
    }

    /// <summary>
    /// 设置扩展字段字典
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <param name="fields">扩展字段字典</param>
    public static void SetExtFields(this TaktEntityBase entity, Dictionary<string, object?> fields)
    {
        if (fields == null || fields.Count == 0)
        {
            entity.ExtFieldJson = null;
            return;
        }

        try
        {
            entity.ExtFieldJson = JsonConvert.SerializeObject(fields, Formatting.None);
        }
        catch (JsonException ex)
        {
            TaktLogger.Error(ex, "扩展字段JSON序列化失败，实体ID: {EntityId}", entity.Id);
            throw;
        }
    }

    /// <summary>
    /// 获取扩展字段值（泛型方法）
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="entity">实体对象</param>
    /// <param name="key">字段键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>字段值，如果不存在则返回默认值</returns>
    public static T? GetExtField<T>(this TaktEntityBase entity, string key, T? defaultValue = default)
    {
        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(entity.ExtFieldJson))
            return defaultValue;

        try
        {
            var fields = JsonConvert.DeserializeObject<Dictionary<string, object?>>(entity.ExtFieldJson);
            if (fields == null || !fields.ContainsKey(key))
                return defaultValue;

            var value = fields[key];
            if (value == null)
                return defaultValue;

            // 如果值已经是目标类型，直接返回
            if (value is T directValue)
                return directValue;

            // 尝试使用 JToken 转换
            var jToken = value as JToken ?? JToken.FromObject(value);
            return jToken.ToObject<T?>() ?? defaultValue;
        }
        catch (Exception ex)
        {
            TaktLogger.Warning(ex, "获取扩展字段值失败，实体ID: {EntityId}, 键: {Key}", entity.Id, key);
            return defaultValue;
        }
    }

    /// <summary>
    /// 获取扩展字段值（字符串类型）
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <param name="key">字段键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>字段值，如果不存在则返回默认值</returns>
    public static string? GetExtFieldString(this TaktEntityBase entity, string key, string? defaultValue = null)
    {
        return entity.GetExtField(key, defaultValue);
    }

    /// <summary>
    /// 获取扩展字段值（整数类型）
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <param name="key">字段键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>字段值，如果不存在则返回默认值</returns>
    public static int GetExtFieldInt(this TaktEntityBase entity, string key, int defaultValue = 0)
    {
        var value = entity.GetExtField<int?>(key, null);
        return value.HasValue ? value.Value : defaultValue;
    }

    /// <summary>
    /// 获取扩展字段值（长整数类型）
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <param name="key">字段键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>字段值，如果不存在则返回默认值</returns>
    public static long GetExtFieldLong(this TaktEntityBase entity, string key, long defaultValue = 0)
    {
        var value = entity.GetExtField<long?>(key, null);
        return value.HasValue ? value.Value : defaultValue;
    }

    /// <summary>
    /// 获取扩展字段值（布尔类型）
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <param name="key">字段键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>字段值，如果不存在则返回默认值</returns>
    public static bool GetExtFieldBool(this TaktEntityBase entity, string key, bool defaultValue = false)
    {
        var value = entity.GetExtField<bool?>(key, null);
        return value.HasValue ? value.Value : defaultValue;
    }

    /// <summary>
    /// 获取扩展字段值（日期时间类型）
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <param name="key">字段键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>字段值，如果不存在则返回默认值</returns>
    public static DateTime? GetExtFieldDateTime(this TaktEntityBase entity, string key, DateTime? defaultValue = null)
    {
        return entity.GetExtField(key, defaultValue);
    }

    /// <summary>
    /// 设置扩展字段值
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <param name="key">字段键</param>
    /// <param name="value">字段值</param>
    public static void SetExtField(this TaktEntityBase entity, string key, object? value)
    {
        if (string.IsNullOrWhiteSpace(key))
            return;

        var fields = entity.GetExtFields();
        
        if (value == null)
        {
            fields.Remove(key);
        }
        else
        {
            fields[key] = value;
        }

        entity.SetExtFields(fields);
    }

    /// <summary>
    /// 删除扩展字段
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <param name="key">字段键</param>
    /// <returns>是否删除成功</returns>
    public static bool RemoveExtField(this TaktEntityBase entity, string key)
    {
        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(entity.ExtFieldJson))
            return false;

        var fields = entity.GetExtFields();
        if (!fields.ContainsKey(key))
            return false;

        fields.Remove(key);
        entity.SetExtFields(fields);
        return true;
    }

    /// <summary>
    /// 检查扩展字段是否存在
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <param name="key">字段键</param>
    /// <returns>是否存在</returns>
    public static bool HasExtField(this TaktEntityBase entity, string key)
    {
        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(entity.ExtFieldJson))
            return false;

        var fields = entity.GetExtFields();
        return fields.ContainsKey(key);
    }

    /// <summary>
    /// 清空所有扩展字段
    /// </summary>
    /// <param name="entity">实体对象</param>
    public static void ClearExtFields(this TaktEntityBase entity)
    {
        entity.ExtFieldJson = null;
    }

    /// <summary>
    /// 批量设置扩展字段
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <param name="fields">扩展字段字典</param>
    public static void SetExtFieldsBatch(this TaktEntityBase entity, Dictionary<string, object?> fields)
    {
        if (fields == null || fields.Count == 0)
            return;

        var existingFields = entity.GetExtFields();
        foreach (var field in fields)
        {
            if (field.Value == null)
            {
                existingFields.Remove(field.Key);
            }
            else
            {
                existingFields[field.Key] = field.Value;
            }
        }

        entity.SetExtFields(existingFields);
    }

    /// <summary>
    /// 获取扩展字段数量
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <returns>扩展字段数量</returns>
    public static int GetExtFieldsCount(this TaktEntityBase entity)
    {
        if (string.IsNullOrWhiteSpace(entity.ExtFieldJson))
            return 0;

        var fields = entity.GetExtFields();
        return fields.Count;
    }

    /// <summary>
    /// 获取扩展字段的所有键
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <returns>扩展字段键列表</returns>
    public static List<string> GetExtFieldKeys(this TaktEntityBase entity)
    {
        if (string.IsNullOrWhiteSpace(entity.ExtFieldJson))
            return new List<string>();

        var fields = entity.GetExtFields();
        return fields.Keys.ToList();
    }
}
