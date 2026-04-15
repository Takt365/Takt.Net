// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Helpers
// 文件名称：TaktStringHelper.cs
// 创建时间：2025-01-22
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字符串帮助类，提供通用的字符串处理操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Takt.Shared.Helpers;

/// <summary>
/// Takt字符串帮助类
/// </summary>
public static class TaktStringHelper
{
    /// <summary>
    /// 生成随机字符串
    /// </summary>
    /// <param name="length">字符串长度</param>
    /// <param name="includeNumbers">是否包含数字，默认为true</param>
    /// <param name="includeLowercase">是否包含小写字母，默认为true</param>
    /// <param name="includeUppercase">是否包含大写字母，默认为true</param>
    /// <param name="includeSpecialChars">是否包含特殊字符，默认为false</param>
    /// <returns>随机字符串</returns>
    public static string GenerateRandomString(int length, bool includeNumbers = true, bool includeLowercase = true, bool includeUppercase = true, bool includeSpecialChars = false)
    {
        if (length <= 0)
            throw new ArgumentException("长度必须大于0", nameof(length));

        var chars = new StringBuilder();
        if (includeNumbers) chars.Append("0123456789");
        if (includeLowercase) chars.Append("abcdefghijklmnopqrstuvwxyz");
        if (includeUppercase) chars.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        if (includeSpecialChars) chars.Append("!@#$%^&*()_+-=[]{}|;:,.<>?");

        if (chars.Length == 0)
            throw new ArgumentException("至少需要包含一种字符类型");

        var random = new Random();
        var result = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            result.Append(chars[random.Next(chars.Length)]);
        }

        return result.ToString();
    }

    /// <summary>
    /// 生成GUID字符串（无连字符）
    /// </summary>
    /// <returns>GUID字符串</returns>
    public static string GenerateGuid()
    {
        return Guid.NewGuid().ToString("N");
    }

    /// <summary>
    /// 生成GUID字符串（带连字符）
    /// </summary>
    /// <returns>GUID字符串</returns>
    public static string GenerateGuidWithHyphens()
    {
        return Guid.NewGuid().ToString();
    }

    /// <summary>
    /// 计算字符串的MD5哈希值
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="encoding">编码，默认为UTF-8</param>
    /// <returns>MD5哈希值（小写）</returns>
    public static string ComputeMd5Hash(string input, Encoding? encoding = null)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        encoding ??= Encoding.UTF8;
        var bytes = encoding.GetBytes(input);
        using var md5 = MD5.Create();
        var hashBytes = md5.ComputeHash(bytes);
        var hashString = new StringBuilder();
        foreach (var b in hashBytes)
        {
            hashString.Append(b.ToString("x2"));
        }
        return hashString.ToString();
    }

    /// <summary>
    /// 计算字符串的SHA256哈希值
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="encoding">编码，默认为UTF-8</param>
    /// <returns>SHA256哈希值（小写）</returns>
    public static string ComputeSha256Hash(string input, Encoding? encoding = null)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        encoding ??= Encoding.UTF8;
        var bytes = encoding.GetBytes(input);
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(bytes);
        var hashString = new StringBuilder();
        foreach (var b in hashBytes)
        {
            hashString.Append(b.ToString("x2"));
        }
        return hashString.ToString();
    }

    /// <summary>
    /// 截取字符串（支持中文等宽字符）
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="maxLength">最大长度</param>
    /// <param name="suffix">截断后缀，默认为"..."</param>
    /// <returns>截取后的字符串</returns>
    public static string Truncate(string input, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrWhiteSpace(input) || maxLength <= 0)
            return input ?? string.Empty;

        if (input.Length <= maxLength)
            return input;

        return input.Substring(0, maxLength - suffix.Length) + suffix;
    }

    /// <summary>
    /// 移除HTML标签
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>移除HTML标签后的字符串</returns>
    public static string RemoveHtmlTags(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        return Regex.Replace(input, "<.*?>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
    }

    /// <summary>
    /// 移除空白字符
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>移除空白字符后的字符串</returns>
    public static string RemoveWhitespace(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        return Regex.Replace(input, @"\s+", string.Empty);
    }

    /// <summary>
    /// 转换为驼峰命名（首字母小写）
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>驼峰命名字符串</returns>
    public static string ToCamelCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        if (input.Length == 1)
            return input.ToLowerInvariant();

        return char.ToLowerInvariant(input[0]) + input.Substring(1);
    }

    /// <summary>
    /// 转换为帕斯卡命名（首字母大写）
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>帕斯卡命名字符串</returns>
    public static string ToPascalCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        if (input.Length == 1)
            return input.ToUpperInvariant();

        return char.ToUpperInvariant(input[0]) + input.Substring(1);
    }

    /// <summary>
    /// 转换为下划线命名（snake_case）
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>下划线命名字符串</returns>
    public static string ToSnakeCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        var result = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (char.IsUpper(c))
            {
                if (i > 0) result.Append('_');
                result.Append(char.ToLowerInvariant(c));
            }
            else
            {
                result.Append(c);
            }
        }
        return result.ToString();
    }

    /// <summary>
    /// 转换为短横线命名（kebab-case）
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>短横线命名字符串</returns>
    public static string ToKebabCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        var result = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (char.IsUpper(c))
            {
                if (i > 0) result.Append('-');
                result.Append(char.ToLowerInvariant(c));
            }
            else
            {
                result.Append(c);
            }
        }
        return result.ToString();
    }

    /// <summary>
    /// 提取数字
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>提取的数字字符串</returns>
    public static string ExtractNumbers(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        return Regex.Replace(input, @"[^\d]", string.Empty);
    }

    /// <summary>
    /// 提取字母
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>提取的字母字符串</returns>
    public static string ExtractLetters(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        return Regex.Replace(input, @"[^a-zA-Z]", string.Empty);
    }

    /// <summary>
    /// 提取字母和数字
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>提取的字母和数字字符串</returns>
    public static string ExtractAlphanumeric(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        return Regex.Replace(input, @"[^a-zA-Z0-9]", string.Empty);
    }

    /// <summary>
    /// 检查字符串是否为有效的邮箱地址
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>是否为有效的邮箱地址</returns>
    public static bool IsValidEmail(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        try
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            return emailRegex.IsMatch(input);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 检查字符串是否为有效的手机号码（中国大陆）
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>是否为有效的手机号码</returns>
    public static bool IsValidPhone(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var phoneRegex = new Regex(@"^1[3-9]\d{9}$");
        return phoneRegex.IsMatch(input);
    }

    /// <summary>
    /// 检查字符串是否为有效的URL
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>是否为有效的URL</returns>
    public static bool IsValidUrl(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        return Uri.TryCreate(input, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    /// <summary>
    /// 检查字符串是否为有效的IP地址
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>是否为有效的IP地址</returns>
    public static bool IsValidIpAddress(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        return System.Net.IPAddress.TryParse(input, out _);
    }

    /// <summary>
    /// 掩码处理（隐藏部分字符）
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="startVisible">开始可见字符数</param>
    /// <param name="endVisible">结束可见字符数</param>
    /// <param name="maskChar">掩码字符，默认为'*'</param>
    /// <returns>掩码处理后的字符串</returns>
    public static string Mask(string input, int startVisible = 3, int endVisible = 4, char maskChar = '*')
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        if (input.Length <= startVisible + endVisible)
            return new string(maskChar, input.Length);

        var start = input.Substring(0, startVisible);
        var end = input.Substring(input.Length - endVisible);
        var mask = new string(maskChar, input.Length - startVisible - endVisible);
        return start + mask + end;
    }

    /// <summary>
    /// 掩码邮箱（隐藏部分字符）
    /// </summary>
    /// <param name="email">邮箱地址</param>
    /// <param name="maskChar">掩码字符，默认为'*'</param>
    /// <returns>掩码处理后的邮箱</returns>
    public static string MaskEmail(string email, char maskChar = '*')
    {
        if (string.IsNullOrWhiteSpace(email))
            return email ?? string.Empty;

        var parts = email.Split('@');
        if (parts.Length != 2)
            return email;

        var username = parts[0];
        var domain = parts[1];

        var maskedUsername = username.Length > 2 
            ? username.Substring(0, 1) + new string(maskChar, username.Length - 2) + username.Substring(username.Length - 1)
            : new string(maskChar, username.Length);

        return $"{maskedUsername}@{domain}";
    }

    /// <summary>
    /// 掩码手机号（隐藏中间4位）
    /// </summary>
    /// <param name="phone">手机号码</param>
    /// <param name="maskChar">掩码字符，默认为'*'</param>
    /// <returns>掩码处理后的手机号</returns>
    public static string MaskPhone(string phone, char maskChar = '*')
    {
        if (string.IsNullOrWhiteSpace(phone))
            return phone ?? string.Empty;

        if (phone.Length < 7)
            return new string(maskChar, phone.Length);

        var start = phone.Substring(0, 3);
        var end = phone.Substring(phone.Length - 4);
        return $"{start}{new string(maskChar, 4)}{end}";
    }

    /// <summary>
    /// 首字母大写
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>首字母大写的字符串</returns>
    public static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        if (input.Length == 1)
            return input.ToUpperInvariant();

        return char.ToUpperInvariant(input[0]) + input.Substring(1);
    }

    /// <summary>
    /// 首字母小写
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>首字母小写的字符串</returns>
    public static string LowercaseFirstLetter(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        if (input.Length == 1)
            return input.ToLowerInvariant();

        return char.ToLowerInvariant(input[0]) + input.Substring(1);
    }

    /// <summary>
    /// 反转字符串
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>反转后的字符串</returns>
    public static string Reverse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        var charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    /// <summary>
    /// 去除字符串两端的空白字符（包括全角空格）
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>去除空白后的字符串</returns>
    public static string TrimAll(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        return input.Trim().Trim('\u3000'); // 全角空格
    }

    /// <summary>
    /// 去除字符串中的所有空白字符
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>去除所有空白后的字符串</returns>
    public static string TrimAllWhitespace(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        return Regex.Replace(input, @"\s+", string.Empty);
    }

    /// <summary>
    /// 检查字符串是否包含中文字符
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>是否包含中文字符</returns>
    public static bool ContainsChinese(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        return Regex.IsMatch(input, @"[\u4e00-\u9fa5]");
    }

    /// <summary>
    /// 检查字符串是否全为中文
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>是否全为中文</returns>
    public static bool IsAllChinese(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        return Regex.IsMatch(input, @"^[\u4e00-\u9fa5]+$");
    }

    /// <summary>
    /// 计算字符串的相似度（使用Levenshtein距离）
    /// </summary>
    /// <param name="str1">字符串1</param>
    /// <param name="str2">字符串2</param>
    /// <returns>相似度（0-1之间，1表示完全相同）</returns>
    public static double CalculateSimilarity(string str1, string str2)
    {
        if (string.IsNullOrWhiteSpace(str1) && string.IsNullOrWhiteSpace(str2))
            return 1.0;

        if (string.IsNullOrWhiteSpace(str1) || string.IsNullOrWhiteSpace(str2))
            return 0.0;

        if (str1 == str2)
            return 1.0;

        var maxLength = Math.Max(str1.Length, str2.Length);
        if (maxLength == 0)
            return 1.0;

        var distance = LevenshteinDistance(str1, str2);
        return 1.0 - (double)distance / maxLength;
    }

    /// <summary>
    /// 计算Levenshtein距离
    /// </summary>
    /// <param name="str1">字符串1</param>
    /// <param name="str2">字符串2</param>
    /// <returns>编辑距离</returns>
    private static int LevenshteinDistance(string str1, string str2)
    {
        var n = str1.Length;
        var m = str2.Length;
        var d = new int[n + 1, m + 1];

        if (n == 0) return m;
        if (m == 0) return n;

        for (var i = 0; i <= n; i++) d[i, 0] = i;
        for (var j = 0; j <= m; j++) d[0, j] = j;

        for (var i = 1; i <= n; i++)
        {
            for (var j = 1; j <= m; j++)
            {
                var cost = (str2[j - 1] == str1[i - 1]) ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }

        return d[n, m];
    }

    /// <summary>
    /// 格式化文件大小
    /// </summary>
    /// <param name="bytes">字节数</param>
    /// <returns>格式化后的文件大小字符串</returns>
    public static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }

    /// <summary>
    /// 格式化数字（添加千分位分隔符）
    /// </summary>
    /// <param name="number">数字</param>
    /// <param name="decimals">小数位数，默认为0</param>
    /// <returns>格式化后的数字字符串</returns>
    public static string FormatNumber(decimal number, int decimals = 0)
    {
        return number.ToString($"N{decimals}", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// 格式化百分比
    /// </summary>
    /// <param name="value">数值（0-1之间）</param>
    /// <param name="decimals">小数位数，默认为2</param>
    /// <returns>格式化后的百分比字符串</returns>
    public static string FormatPercent(double value, int decimals = 2)
    {
        return (value * 100).ToString($"F{decimals}", CultureInfo.InvariantCulture) + "%";
    }

    /// <summary>
    /// 分割字符串并去除空白项
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="separator">分隔符</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] SplitAndTrim(string input, char separator = ',')
    {
        if (string.IsNullOrWhiteSpace(input))
            return Array.Empty<string>();

        return input.Split(separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();
    }

    /// <summary>
    /// 分割字符串并去除空白项
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="separators">分隔符数组</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] SplitAndTrim(string input, params char[] separators)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Array.Empty<string>();

        if (separators == null || separators.Length == 0)
            separators = new[] { ',' };

        return input.Split(separators, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();
    }

    /// <summary>
    /// 检查字符串是否为数字
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>是否为数字</returns>
    public static bool IsNumeric(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        return double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// 检查字符串是否为整数
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>是否为整数</returns>
    public static bool IsInteger(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        return long.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// 检查字符串是否为有效的日期时间
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>是否为有效的日期时间</returns>
    public static bool IsValidDateTime(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        return DateTime.TryParse(input, out _);
    }

    /// <summary>
    /// 转换为Base64编码
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="encoding">编码，默认为UTF-8</param>
    /// <returns>Base64编码字符串</returns>
    public static string ToBase64(string input, Encoding? encoding = null)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        encoding ??= Encoding.UTF8;
        var bytes = encoding.GetBytes(input);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// 从Base64解码
    /// </summary>
    /// <param name="base64String">Base64编码字符串</param>
    /// <param name="encoding">编码，默认为UTF-8</param>
    /// <returns>解码后的字符串</returns>
    public static string FromBase64(string base64String, Encoding? encoding = null)
    {
        if (string.IsNullOrWhiteSpace(base64String))
            return string.Empty;

        try
        {
            encoding ??= Encoding.UTF8;
            var bytes = Convert.FromBase64String(base64String);
            return encoding.GetString(bytes);
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 转换为URL编码
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>URL编码字符串</returns>
    public static string UrlEncode(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        return Uri.EscapeDataString(input);
    }

    /// <summary>
    /// 从URL解码
    /// </summary>
    /// <param name="input">URL编码字符串</param>
    /// <returns>解码后的字符串</returns>
    public static string UrlDecode(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        try
        {
            return Uri.UnescapeDataString(input);
        }
        catch
        {
            return input;
        }
    }
}
