// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Helpers
// 文件名称：TaktExcelExportFileHelper.cs
// 功能描述：Excel 模板下载与导出响应的 Content-Type 统一约定（与 TaktExcelHelper.ExportAsync 分批 zip 行为一致）
// ========================================

namespace Takt.WebApi.Helpers;

/// <summary>
/// 模板/导出文件响应的 MIME 约定：模板恒为 xlsx；导出在超过 <c>TaktExcelHelper.ExportAsync</c> 行数上限时为 zip，否则为 xlsx。
/// </summary>
public static class TaktExcelExportFileHelper
{
    /// <summary>
    /// Excel（.xlsx）模板或单文件导出使用的 Content-Type。
    /// </summary>
    public const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

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
}
