// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Helpers
// 文件名称：TaktExcelHelper.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt Excel导入导出帮助类，基于EPPlus实现通用的Excel文件导入导出功能
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.Style;
using System.ComponentModel;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;
using System.Xml;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;

namespace Takt.Shared.Helpers;

/// <summary>
/// Excel导入导出帮助类
/// </summary>
public class TaktExcelHelper
{
    private static TaktExcelOptions? _options;
    private const int MaxImportRowsPerFile = 1000;
    /// <summary>
    /// Excel（.xlsx）模板或单文件导出使用的 Content-Type。
    /// </summary>
    public const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    /// <summary>
    /// 时间戳格式：年月日时分秒（_yyyyMMddHHmmss），模板下载与导出统一使用
    /// </summary>
    private const string TimestampFormat = "yyyyMMddHHmmss";

    private const string ImportRowLimitExceededKey = "validation.importRowLimitExceeded";
    private const string ExcelHelperNotConfiguredKey = "validation.excelHelperNotConfigured";
    private const string ExportMultiSheetAtLeastOneSheetKey = "validation.exportMultiSheetAtLeastOneSheet";
    private const string ExportMultiSheetNameRequiredKey = "validation.exportMultiSheetNameRequired";
    private const string ExportMultiSheetDataRequiredKey = "validation.exportMultiSheetDataRequired";
    private const string ExcelSheetNotFoundOrEmptyKey = "validation.excelSheetNotFoundOrEmpty";
    private const string ExcelZipNoFilesKey = "validation.excelZipNoFiles";

    /// <summary>
    /// 静态构造函数，设置EPPlus许可证
    /// </summary>
    static TaktExcelHelper()
    {
        // EPPlus 8.0+ 设置非商业许可证（官方API）
        // 根据EPPlus 8官方文档：https://www.epplussoftware.com/
        ExcelPackage.License.SetNonCommercialPersonal("Takt Digital Factory (TDF) ");
    }

    /// <summary>
    /// 获取当前时间戳字符串（yyyyMMddHHmmss），使用 InvariantCulture 保证格式一致
    /// </summary>
    private static string GetTimestampString() =>
        DateTime.Now.ToString(TimestampFormat, CultureInfo.InvariantCulture);

    /// <summary>
    /// 去掉文件名中的 .xlsx / .xls / .zip 后缀（前后端约定只传名称，不传后缀）
    /// </summary>
    private static string NormalizeFileNameBase(string? name)
    {
        if (string.IsNullOrWhiteSpace(name)) return name ?? string.Empty;
        var s = name.Trim();
        const StringComparison cmp = StringComparison.OrdinalIgnoreCase;
        if (s.EndsWith(".xlsx", cmp)) return s[..^5];
        if (s.EndsWith(".xls", cmp)) return s[..^4];
        if (s.EndsWith(".zip", cmp)) return s[..^4];
        return s;
    }

    /// <summary>
    /// 生成带时间戳的 Excel 文件名：名称_yyyyMMddHHmmss.xlsx（前端只传名称，后缀由本方法拼接）
    /// </summary>
    /// <param name="baseFileName">基础文件名（不含后缀，若带 .xlsx/.xls 会自动去掉）</param>
    /// <returns>带时间戳的文件名，如 用户数据_20250129143022.xlsx</returns>
    private static string GenerateTimestampFileName(string? baseFileName)
    {
        var baseName = NormalizeFileNameBase(baseFileName);
        if (string.IsNullOrWhiteSpace(baseName)) baseName = "Data";
        return $"{baseName}_{GetTimestampString()}.xlsx";
    }

    /// <summary>
    /// 生成带时间戳的 Zip 文件名：名称_yyyyMMddHHmmss.zip
    /// </summary>
    private static string GenerateTimestampZipFileName(string? baseFileName)
    {
        var baseName = NormalizeFileNameBase(baseFileName);
        if (string.IsNullOrWhiteSpace(baseName)) baseName = "Data";
        return $"{baseName}_{GetTimestampString()}.zip";
    }

    /// <summary>
    /// 根据服务端生成的最终文件名选择导出 Content-Type（<c>.zip</c> 为分批打包，否则为 Excel）。
    /// </summary>
    /// <param name="fileName">含扩展名的文件名</param>
    public static string GetExportContentType(string fileName)
    {
        ArgumentException.ThrowIfNullOrEmpty(fileName);
        return fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
            ? "application/zip"
            : ExcelContentType;
    }

    /// <summary>
    /// 设置Excel配置
    /// </summary>
    /// <param name="options">Excel配置选项</param>
    public static void Configure(TaktExcelOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        _options = options;
        TaktLogger.Information("[TaktExcelHelper] Excel配置已初始化，作者: {Author}, 标题: {Title}", options.Author, options.Title);
    }

    /// <summary>
    /// 设置工作簿属性（确保所有导出的Excel文件都包含完整的TaktExcelOptions信息）
    /// </summary>
    private static void SetWorkbookProperties(ExcelWorkbook workbook)
    {
        ArgumentNullException.ThrowIfNull(workbook);

        // 配置必须存在，不允许硬编码默认值
        if (_options == null)
        {
            TaktLogger.Error("[TaktExcelHelper] Excel配置未初始化，请在应用程序启动时调用 TaktExcelHelper.Configure() 方法");
            throw new TaktLocalizedException(ExcelHelperNotConfiguredKey, "Frontend");
        }

        var props = workbook.Properties;
        props.Author = _options.Author;
        props.Title = _options.Title;
        props.Subject = _options.Subject;
        props.Category = _options.Category;
        props.Keywords = _options.Keywords;
        props.Comments = _options.Comments;
        props.Status = _options.Status;
        props.Application = _options.Application;
        props.Company = _options.Company;
        props.Manager = _options.Manager;
        props.LastModifiedBy = _options.Author;
        props.Created = DateTime.Now;
        props.Modified = DateTime.Now;
    }

    #region 单Sheet导入导出

    /// <summary>
    /// 导出Excel(单个Sheet，支持超大数据分批导出多个文件)
    /// </summary>
    /// <typeparam name="T">要导出的数据类型</typeparam>
    /// <param name="data">数据集合</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名（仅名称，不含 .xlsx；为空时用 sheetName；后端自动拼接 名称_yyyyMMddHHmmss.xlsx）</param>
    /// <returns>最终文件名与内容（fileName 已含时间戳与 .xlsx）</returns>
    public static async Task<(string fileName, byte[] content)> ExportAsync<T>(
        IEnumerable<T> data, 
        string sheetName = "Data", 
        string? fileName = null) where T : class
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentException.ThrowIfNullOrEmpty(sheetName);

        try
        {
            const int maxRows = 10000;
            var dataList = data.ToList();
            int total = dataList.Count;

            if (total == 0)
            {
                // 生成只有表头的空Excel
                using var package = new ExcelPackage();
                SetWorkbookProperties(package.Workbook);
                await Task.Run(() => ExportToSheetAsync(package, dataList, sheetName));
                var actualFileName = GenerateTimestampFileName(fileName ?? sheetName);
                var content = await package.GetAsByteArrayAsync();
                TaktLogger.Information("[TaktExcelHelper] 导出Excel成功（空数据），Sheet: {SheetName}, 文件名: {FileName}", sheetName, actualFileName);
                return (actualFileName, content);
            }

            if (total <= maxRows)
            {
                // 只生成一个Excel
                using var package = new ExcelPackage();
                SetWorkbookProperties(package.Workbook);
                await Task.Run(() => ExportToSheetAsync(package, dataList, sheetName));
                var actualFileName = GenerateTimestampFileName(fileName ?? sheetName);
                var content = await package.GetAsByteArrayAsync();
                TaktLogger.Information("[TaktExcelHelper] 导出Excel成功，Sheet: {SheetName}, 文件名: {FileName}, 数据行数: {RowCount}", sheetName, actualFileName, total);
                return (actualFileName, content);
            }
            else
            {
                // 超过10000，分批生成多个Excel并打包zip
                int fileCount = (int)Math.Ceiling(total / (double)maxRows);
                var fileList = new List<(string fileName, byte[] content)>();
                var exportBaseName = NormalizeFileNameBase(fileName ?? sheetName);
                if (string.IsNullOrWhiteSpace(exportBaseName)) exportBaseName = sheetName;
                for (int i = 0; i < fileCount; i++)
                {
                    var batch = dataList.Skip(i * maxRows).Take(maxRows).ToList();
                    using var package = new ExcelPackage();
                    SetWorkbookProperties(package.Workbook);
                    await Task.Run(() => ExportToSheetAsync(package, batch, sheetName));
                    var batchFileName = GenerateTimestampFileName($"{exportBaseName}_{i + 1}");
                    var batchContent = await package.GetAsByteArrayAsync();
                    fileList.Add((batchFileName, batchContent));
                }

                // 打包为zip
                using var ms = new MemoryStream();
                using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (var (batchFileName, batchContent) in fileList)
                    {
                        var entry = zip.CreateEntry(batchFileName, System.IO.Compression.CompressionLevel.Optimal);
                        using var entryStream = entry.Open();
                        await entryStream.WriteAsync(batchContent, 0, batchContent.Length);
                    }
                }
                ms.Position = 0;
                var zipName = GenerateTimestampZipFileName(fileName ?? sheetName);
                TaktLogger.Information("[TaktExcelHelper] 导出Excel成功（分批打包），Sheet: {SheetName}, 文件名: {FileName}, 总数据行数: {RowCount}, 文件数: {FileCount}", sheetName, zipName, total, fileCount);
                return (zipName, ms.ToArray());
            }
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktExcelHelper] 导出Excel失败，Sheet: {SheetName}, 文件名: {FileName}", sheetName, fileName ?? sheetName);
            throw;
        }
    }

    /// <summary>
    /// 导入Excel(单个Sheet)
    /// </summary>
    /// <typeparam name="T">要导入的数据类型</typeparam>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>数据集合</returns>
    public static Task<List<T>> ImportAsync<T>(
        Stream fileStream, 
        string sheetName = "Data") where T : class, new()
    {
        ArgumentNullException.ThrowIfNull(fileStream);
        ArgumentException.ThrowIfNullOrEmpty(sheetName);

        try
        {
            using var package = new ExcelPackage(fileStream);
            var result = ImportFromSheet<T>(package, sheetName);
            if (result.Count > MaxImportRowsPerFile)
                throw new TaktLocalizedException(
                    ImportRowLimitExceededKey,
                    "Frontend",
                    result.Count,
                    MaxImportRowsPerFile);
            TaktLogger.Information("[TaktExcelHelper] 导入Excel成功，Sheet: {SheetName}, 数据类型: {TypeName}, 数据行数: {RowCount}", sheetName, typeof(T).Name, result.Count);
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktExcelHelper] 导入Excel失败，Sheet: {SheetName}, 数据类型: {TypeName}", sheetName, typeof(T).Name);
            throw;
        }
    }

    #endregion 单Sheet导入导出

    #region 多Sheet导入导出

    /// <summary>
    /// 导出Excel(多个Sheet)
    /// </summary>
    /// <param name="sheets">Sheet数据字典，key为sheet名称，value为数据集合</param>
    /// <returns>包含文件名和内容的元组</returns>
    public static async Task<(string fileName, byte[] content)> ExportMultiSheetAsync<T>(
        Dictionary<string, IEnumerable<T>> sheets) where T : class
    {
        ArgumentNullException.ThrowIfNull(sheets);
        if (!sheets.Any()) throw new TaktLocalizedException(ExportMultiSheetAtLeastOneSheetKey, "Frontend");

        try
        {
            using var package = new ExcelPackage();
            SetWorkbookProperties(package.Workbook);

            int totalRows = 0;
            foreach (var sheet in sheets)
            {
                if (string.IsNullOrEmpty(sheet.Key))
                    throw new TaktLocalizedException(ExportMultiSheetNameRequiredKey, "Frontend");
                if (sheet.Value == null)
                    throw new TaktLocalizedException(ExportMultiSheetDataRequiredKey, "Frontend", sheet.Key);

                var sheetData = sheet.Value.ToList();
                totalRows += sheetData.Count;
                ExportToSheetAsync(package, sheetData, sheet.Key);
            }

            var fileName = GenerateTimestampFileName("多Sheet数据");
            var content = await package.GetAsByteArrayAsync();
            TaktLogger.Information("[TaktExcelHelper] 导出多Sheet Excel成功，Sheet数量: {SheetCount}, 总数据行数: {RowCount}, 文件名: {FileName}", sheets.Count, totalRows, fileName);
            return (fileName, content);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktExcelHelper] 导出多Sheet Excel失败，Sheet数量: {SheetCount}", sheets.Count);
            throw;
        }
    }

    /// <summary>
    /// 导入Excel(多个Sheet)
    /// </summary>
    /// <typeparam name="T">要导入的数据类型</typeparam>
    /// <param name="fileStream">Excel文件流</param>
    /// <returns>数据字典，key为sheet名称，value为数据集合</returns>
    public static Task<Dictionary<string, List<T>>> ImportMultiSheetAsync<T>(
        Stream fileStream) where T : class, new()
    {
        ArgumentNullException.ThrowIfNull(fileStream);

        try
        {
            using var package = new ExcelPackage(fileStream);
            var result = new Dictionary<string, List<T>>();

            foreach (var worksheet in package.Workbook.Worksheets)
            {
                if (worksheet?.Name == null) continue;
                result[worksheet.Name] = ImportFromSheet<T>(package, worksheet.Name);
            }

            var totalRows = result.Values.Sum(list => list.Count);
            TaktLogger.Information("[TaktExcelHelper] 导入多Sheet Excel成功，Sheet数量: {SheetCount}, 数据类型: {TypeName}, 总数据行数: {RowCount}", result.Count, typeof(T).Name, totalRows);
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktExcelHelper] 导入多Sheet Excel失败，数据类型: {TypeName}", typeof(T).Name);
            throw;
        }
    }

    #endregion 多Sheet导入导出

    #region 模板导入导出

    /// <summary>
    /// 生成Excel导入模板
    /// </summary>
    /// <typeparam name="T">模板类型</typeparam>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名（仅名称，不含 .xlsx；为空时用 <paramref name="sheetName"/> + <c>Template</c> 作基础设施兜底，不含业务文案；业务下载应由应用层传入已本地化基名；后端自动拼接 名称_yyyyMMddHHmmss.xlsx）</param>
    /// <returns>最终文件名与内容（fileName 已含时间戳与 .xlsx）</returns>
    public static async Task<(string fileName, byte[] content)> GenerateTemplateAsync<T>(
        string sheetName = "Data", 
        string? fileName = null) where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(sheetName);

        using var package = new ExcelPackage();
        SetWorkbookProperties(package.Workbook);
        var worksheet = package.Workbook.Worksheets.Add(sheetName);

        // 获取属性信息
        var properties = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false && p.Name != "Id")
            .ToList();

        // 读取XML注释
        var xmlDoc = new XmlDocument();
        string? xmlPath = null;
        try
        {
            var asm = typeof(T).Assembly;
            xmlPath = Path.ChangeExtension(asm.Location, ".xml");
            if (File.Exists(xmlPath))
                xmlDoc.Load(xmlPath);
        }
        catch { }

        // 两行表头
        var headers = new string[properties.Count];     // XML注释
        var fields = new string[properties.Count];      // 字段名

        for (int i = 0; i < properties.Count; i++)
        {
            fields[i] = properties[i].Name;
            string summary = properties[i].Name;
            if (xmlDoc.DocumentElement != null)
            {
                var memberName = $"P:{typeof(T).FullName}.{properties[i].Name}";
                var node = xmlDoc.SelectSingleNode($"//member[@name='{memberName}']/summary");
                if (node != null && !string.IsNullOrWhiteSpace(node.InnerText))
                    summary = node.InnerText.Trim();
            }
            headers[i] = summary;
        }

        worksheet.Cells[1, 1].LoadFromArrays(new[] { headers });  // 第一行：XML注释
        worksheet.Cells[2, 1].LoadFromArrays(new[] { fields });   // 第二行：字段名

        // 根据属性类型添加数据验证，从第3行开始
        for (int i = 0; i < properties.Count; i++)
        {
            var propertyType = properties[i].PropertyType;
            var column = worksheet.Cells[3, i + 1, 100, i + 1].Address;

            if (propertyType == typeof(string))
            {
                // 字符串类型添加长度验证
                var validation = worksheet.DataValidations.AddTextLengthValidation(column);
                if (validation != null)
                {
                    validation.ShowErrorMessage = true;
                    validation.Error = "请输入有效的文本";
                    validation.Operator = ExcelDataValidationOperator.between;
                    validation.Formula.Value = 0;
                    validation.Formula2.Value = 255;
                }
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                // 日期类型添加日期验证
                var validation = worksheet.DataValidations.AddDateTimeValidation(column);
                if (validation != null)
                {
                    validation.ShowErrorMessage = true;
                    validation.Error = "请输入有效的日期";
                    validation.Operator = ExcelDataValidationOperator.between;
                    validation.Formula.Value = new DateTime(1900, 1, 1);
                    validation.Formula2.Value = new DateTime(9999, 12, 31);
                }
                worksheet.Column(i + 1).Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
            }
            else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?) ||
                     propertyType == typeof(double) || propertyType == typeof(double?) ||
                     propertyType == typeof(float) || propertyType == typeof(float?))
            {
                // 数值类型添加数值验证
                var validation = worksheet.DataValidations.AddDecimalValidation(column);
                if (validation != null)
                {
                    validation.ShowErrorMessage = true;
                    validation.Error = "请输入有效的数值";
                    validation.Operator = ExcelDataValidationOperator.between;
                    validation.Formula.Value = -999999999.0;
                    validation.Formula2.Value = 999999999.0;
                }
                worksheet.Column(i + 1).Style.Numberformat.Format = "#,##0.00";
            }
            else if (propertyType == typeof(int) || propertyType == typeof(int?) ||
                     propertyType == typeof(long) || propertyType == typeof(long?))
            {
                // 整数类型添加整数验证
                var validation = worksheet.DataValidations.AddIntegerValidation(column);
                if (validation != null)
                {
                    validation.ShowErrorMessage = true;
                    validation.Error = "请输入有效的整数";
                    validation.Operator = ExcelDataValidationOperator.between;
                    validation.Formula.Value = -2147483648;
                    validation.Formula2.Value = 2147483647;
                }
                worksheet.Column(i + 1).Style.Numberformat.Format = "#,##0";
            }
        }

        // 自动调整列宽
        if (worksheet.Dimension != null)
        {
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
        }

        // 自动调整列宽
        if (worksheet.Dimension != null)
        {
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
        }

        var content = await package.GetAsByteArrayAsync();
        // 无本地化上下文时的技术兜底；业务文件名由应用层传入。
        var baseName = string.IsNullOrWhiteSpace(fileName) ? $"{sheetName}Template" : fileName.Trim();
        var finalFileName = GenerateTimestampFileName(baseName);
        TaktLogger.Information("[TaktExcelHelper] 生成Excel模板成功，Sheet: {SheetName}, 数据类型: {TypeName}, 文件名: {FileName}", sheetName, typeof(T).Name, finalFileName);
        return (finalFileName, content);
    }

    #endregion 模板导入导出

    #region 私有辅助方法

    /// <summary>
    /// 导出数据到指定Sheet
    /// </summary>
    private static void ExportToSheetAsync<T>(ExcelPackage package, IEnumerable<T> data, string sheetName) where T : class
    {
        ArgumentNullException.ThrowIfNull(package);
        ArgumentNullException.ThrowIfNull(data);
        ArgumentException.ThrowIfNullOrEmpty(sheetName);

        var worksheet = package.Workbook.Worksheets.Add(sheetName);

        // 获取属性信息
        var properties = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false && p.Name != "Id")
            .ToList();

        // 读取XML注释
        var xmlDoc = new XmlDocument();
        string? xmlPath = null;
        try
        {
            var asm = typeof(T).Assembly;
            xmlPath = Path.ChangeExtension(asm.Location, ".xml");
            if (File.Exists(xmlPath))
                xmlDoc.Load(xmlPath);
        }
        catch { }

        // 优化：使用数组存储表头
        var headers = new string[properties.Count];
        for (int i = 0; i < properties.Count; i++)
        {
            // 优先使用XML注释
            string header = properties[i].Name;
            if (xmlDoc.DocumentElement != null)
            {
                var memberName = $"P:{typeof(T).FullName}.{properties[i].Name}";
                var node = xmlDoc.SelectSingleNode($"//member[@name='{memberName}']/summary");
                if (node != null && !string.IsNullOrWhiteSpace(node.InnerText))
                    header = node.InnerText.Trim();
            }
            headers[i] = header;
        }

        // 优化：批量写入表头
        worksheet.Cells[1, 1].LoadFromArrays(new[] { headers });

        // 优化：使用数组批量写入数据
        var dataArray = new List<object[]>();
        foreach (var item in data)
        {
            var row = new object[properties.Count];
            for (int i = 0; i < properties.Count; i++)
            {
                row[i] = properties[i].GetValue(item) ?? DBNull.Value;
            }
            dataArray.Add(row);
        }

        if (dataArray.Any())
        {
            worksheet.Cells[2, 1].LoadFromArrays(dataArray);
        }

        // 优化：设置列格式
        for (int i = 0; i < properties.Count; i++)
        {
            var propertyType = properties[i].PropertyType;
            var column = worksheet.Column(i + 1);
            if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                column.Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
            else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
                column.Style.Numberformat.Format = "#,##0.00";
        }

        // 优化：自动调整列宽
        if (worksheet.Dimension != null)
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        // 冻结首行
        worksheet.View.FreezePanes(2, 1);
    }

    /// <summary>
    /// 从指定 Sheet 解析行数据（EPPlus 内存访问，无 await 型 I/O；与黄金法则一致：纯内存用同步）。
    /// </summary>
    private static List<T> ImportFromSheet<T>(ExcelPackage package, string sheetName) where T : class, new()
    {
        ArgumentNullException.ThrowIfNull(package);
        ArgumentException.ThrowIfNullOrEmpty(sheetName);

        var worksheet = package.Workbook.Worksheets[sheetName];
        if (worksheet == null || worksheet.Dimension == null)
            throw new TaktLocalizedException(ExcelSheetNotFoundOrEmptyKey, "Frontend", sheetName);

        var result = new List<T>();
        var properties = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false && p.Name != "Id")
            .ToList();

        // 获取表头与属性的映射关系
        var headerMap = new Dictionary<string, PropertyInfo>();
        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
        {
            // 从第二行（字段名）获取表头
            var headerValue = worksheet.Cells[2, col].Value?.ToString();
            if (string.IsNullOrEmpty(headerValue)) continue;

            var property = properties.FirstOrDefault(p => p.Name == headerValue);
            if (property != null)
                headerMap[headerValue] = property;
        }

        // 读取数据（从第3行开始，前两行是表头）
        for (int row = 3; row <= worksheet.Dimension.End.Row; row++)
        {
            var item = new T();
            foreach (var header in headerMap)
            {
                var property = header.Value;
                var col = GetColumnByHeader(worksheet, header.Key);
                if (col == -1) continue;

                var cell = worksheet.Cells[row, col];
                var cellValue = cell.Value?.ToString();
                if (string.IsNullOrEmpty(cellValue)) continue;

                var value = ConvertValue(cellValue, property.PropertyType);
                if (value != null)
                {
                    property.SetValue(item, value);
                }
            }

            result.Add(item);
        }

        return result;
    }

    /// <summary>
    /// 根据表头获取列号
    /// </summary>
    private static int GetColumnByHeader(ExcelWorksheet worksheet, string headerText)
    {
        if (worksheet.Dimension == null) return -1;
        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
        {
            // 查找第二行（字段名）
            if (worksheet.Cells[2, col].Value?.ToString() == headerText)
                return col;
        }
        return -1;
    }

    /// <summary>
    /// 转换值
    /// </summary>
    private static object? ConvertValue(string value, Type targetType)
    {
        if (string.IsNullOrEmpty(value)) 
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;

        try
        {
            if (targetType == typeof(string))
                return value;

            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (underlyingType.IsEnum)
                return Enum.Parse(underlyingType, value);

            if (underlyingType == typeof(DateTime))
            {
                return DateTime.Parse(value);
            }

            if (underlyingType == typeof(bool))
            {
                var strValue = value.ToLower();
                return strValue == "是" || strValue == "1" || strValue == "true";
            }

            return Convert.ChangeType(value, underlyingType, CultureInfo.CurrentCulture);
        }
        catch (Exception ex)
        {
            TaktLogger.Debug(ex, "[TaktExcelHelper] 值转换失败，值: {Value}, 目标类型: {TargetType}", value, targetType.Name);
            return null;
        }
    }

    /// <summary>
    /// 将多个Excel文件打包为zip并返回
    /// </summary>
    /// <param name="files">要打包的文件列表</param>
    /// <param name="zipFileName">zip 文件名（仅名称，不含 .zip；后端自动拼接 名称_yyyyMMddHHmmss.zip）</param>
    /// <returns>最终 zip 文件名与内容</returns>
    public static async Task<(string fileName, byte[] content)> PackToZipAsync(
        List<(string fileName, byte[] content)> files, 
        string zipFileName)
    {
        if (files == null || files.Count == 0)
            throw new TaktLocalizedException(ExcelZipNoFilesKey, "Frontend");
        
        using var ms = new MemoryStream();
        using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
        {
            foreach (var (fileName, content) in files)
            {
                var entry = zip.CreateEntry(fileName, System.IO.Compression.CompressionLevel.Optimal);
                using var entryStream = entry.Open();
                await entryStream.WriteAsync(content, 0, content.Length);
            }
        }
        ms.Position = 0;
        var zipName = GenerateTimestampZipFileName(zipFileName);
        return (zipName, ms.ToArray());
    }

    #endregion 私有辅助方法
}
