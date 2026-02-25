// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Financial
// 文件名称：TaktBanksController.cs
// 功能描述：Takt银行控制器，提供银行管理的RESTful API接口（含导入、导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Application.Services.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;

namespace Takt.WebApi.Controllers.Accounting.Financial;

/// <summary>
/// 银行控制器
/// </summary>
[Route("api/[controller]", Name = "银行")]
[ApiModule("Financial", "财务会计")]
[TaktPermission("accounting:financial:bank", "银行管理")]
public class TaktBanksController : TaktControllerBase
{
    private readonly ITaktBankService _bankService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="bankService">银行服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktBanksController(
        ITaktBankService bankService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _bankService = bankService;
    }

    /// <summary>
    /// 获取银行列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("accounting:financial:bank:list", "查询银行列表")]
    public async Task<ActionResult<TaktPagedResult<TaktBankDto>>> GetListAsync([FromQuery] TaktBankQueryDto queryDto)
    {
        var result = await _bankService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取银行
    /// </summary>
    /// <param name="id">银行ID</param>
    /// <returns>银行DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("accounting:financial:bank:query", "查询银行详情")]
    public async Task<ActionResult<TaktBankDto>> GetByIdAsync(long id)
    {
        var entity = await _bankService.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    /// <summary>
    /// 获取银行选项列表（用于下拉框等）
    /// </summary>
    /// <param name="companyCode">公司代码（可选，筛选该公司下的银行）</param>
    /// <returns>银行选项列表</returns>
    [HttpGet("options")]
    [TaktPermission("accounting:financial:bank:list", "查询银行选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync([FromQuery] string? companyCode = null)
    {
        var options = await _bankService.GetOptionsAsync(companyCode);
        return Ok(options);
    }

    /// <summary>
    /// 创建银行
    /// </summary>
    /// <param name="dto">创建银行DTO</param>
    /// <returns>银行DTO</returns>
    [HttpPost]
    [TaktPermission("accounting:financial:bank:create", "创建银行")]
    public async Task<ActionResult<TaktBankDto>> CreateAsync([FromBody] TaktBankCreateDto dto)
    {
        try
        {
            var entity = await _bankService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = entity.BankId }, entity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 更新银行
    /// </summary>
    /// <param name="id">银行ID</param>
    /// <param name="dto">更新银行DTO</param>
    /// <returns>银行DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("accounting:financial:bank:update", "更新银行")]
    public async Task<ActionResult<TaktBankDto>> UpdateAsync(long id, [FromBody] TaktBankUpdateDto dto)
    {
        try
        {
            var entity = await _bankService.UpdateAsync(id, dto);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除银行
    /// </summary>
    /// <param name="id">银行ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:financial:bank:delete", "删除银行")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _bankService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除银行
    /// </summary>
    /// <param name="ids">银行ID列表</param>
    /// <returns>操作结果</returns>
    [HttpDelete("batch")]
    [TaktPermission("accounting:financial:bank:delete", "批量删除银行")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] IEnumerable<long> ids)
    {
        try
        {
            await _bankService.DeleteAsync(ids);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 更新银行状态
    /// </summary>
    /// <param name="dto">银行状态DTO</param>
    /// <returns>银行DTO</returns>
    [HttpPut("status")]
    [TaktPermission("accounting:financial:bank:status", "更新银行状态")]
    public async Task<ActionResult<TaktBankDto>> UpdateStatusAsync([FromBody] TaktBankStatusDto dto)
    {
        try
        {
            var entity = await _bankService.UpdateStatusAsync(dto);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("accounting:financial:bank:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _bankService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入银行
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("accounting:financial:bank:import", "导入银行")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("请选择要导入的Excel文件");
            }

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
                !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            }

            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _bankService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出银行
    /// </summary>
    /// <param name="query">银行查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpPost("export")]
    [TaktPermission("accounting:financial:bank:export", "导出银行")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktBankQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _bankService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
