/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：generate_controllers.cjs
 * 创建时间：2026-04-28
 * 功能描述：根据服务实现自动生成控制器文件（严格依赖链：服务实现 → 控制器）
 *   1. 扫描所有服务实现文件（*TaktService.cs，不含I前缀）
 *   2. 解析服务实现中的公共方法签名
 *   3. 生成标准的RESTful API控制器
 *   4. 自动识别实体路径并生成对应的控制器文件
 * 
 * 依赖链：DTO → 服务接口 → 服务实现 → 控制器
 * 
 * 使用方法：
 *   1. 生成单个实体（默认）：node scripts/generate_controllers.cjs --entity TaktNews
 *   2. 全量生成（谨慎）：node scripts/generate_controllers.cjs --all
 *   3. 预览模式：node scripts/generate_controllers.cjs --entity TaktNews --dry-run
 * ========================================
 */

const fs = require('fs');
const path = require('path');

const SERVICE_IMPL_PATH = path.join(__dirname, '../src/Takt.Application/Services');
const CONTROLLER_PATH = path.join(__dirname, '../src/Takt.WebApi/Controllers');
const ENTITY_PATH = path.join(__dirname, '../src/Takt.Domain/Entities');

// 解析命令行参数
const args = process.argv.slice(2);
const targetEntity = args.find(a => a.startsWith('--entity='))?.split('=')[1];
const generateAll = args.includes('--all');
const dryRun = args.includes('--dry-run');

// 安全检查：必须明确指定实体或使用 --all
if (!targetEntity && !generateAll) {
  console.error('❌ 错误：必须指定实体名称或使用 --all 参数');
  console.error('');
  console.error('使用方法：');
  console.error('  1. 生成单个实体：node scripts/generate_controllers.cjs --entity TaktEmployee');
  console.error('  2. 全量生成（谨慎）：node scripts/generate_controllers.cjs --all');
  console.error('  3. 预览模式：node scripts/generate_controllers.cjs --entity TaktEmployee --dry-run');
  console.error('');
  process.exit(1);
}

/**
 * 检查实体是否应该被排除
 */
function shouldExcludeEntity(entityName) {
  // 排除特殊的DTO文件对应的实体
  const excludedEntities = ['TaktCache', 'TaktServerMonitor'];
  return excludedEntities.includes(entityName);
}

/**
 * 检查文件路径是否在排除目录中（Engine 或其子目录）
 * Engine 目录包含特殊的代码生成引擎、流程引擎等，不能被脚本生成
 * 例如：GeneratorEngine、WordFilterEngine、FileEngine、RuleEngine、FlowEngine、SpecificEngine
 * 也包括如 Identity/SpecificEngine 等子目录
 */
function isInEngineDirectory(filePath) {
  const normalizedPath = filePath.replace(/\\/g, '/');
  // 匹配所有 Engine 目录（/Engine/ 或 /Engine 结尾）：不区分大小写
  return /\/\w*[Ee]ngine($|\/)/.test(normalizedPath);
}

/**
 * 检查文件路径是否在排除目录中（Engine 或其子目录）
 */
function isInExcludedDirectory(filePath) {
  return isInEngineDirectory(filePath);
}

/**
 * 从服务实现文件中提取公共方法签名（排除私有方法和构造函数）
 */
function extractServiceMethods(serviceFile) {
  const content = fs.readFileSync(serviceFile, 'utf-8');
  const methods = [];
  const seenMethods = new Set(); // 防止重复
  
  // 匹配公共方法签名：public async Task<...> MethodName(...)
  // 排除：构造函数、私有方法、静态方法、override方法
  // 使用更宽松的正则来匹配包含逗号、括号的复杂返回类型
  const methodRegex = /public\s+async\s+Task(?:<([\s\S]*?)>)?\s+(\w+Async)\s*\(([^)]*)\)/g;
  let match;
  
  while ((match = methodRegex.exec(content)) !== null) {
    const returnType = match[1] || 'void';
    const methodName = match[2];
    const parameters = match[3];
    
    // 跳过重复的方法，但保持原有顺序
    if (seenMethods.has(methodName)) {
      continue;
    }
    seenMethods.add(methodName);
    
    methods.push({
      returnType,
      methodName,
      parameters
    });
  }
  
  return methods;
}

/**
 * 从服务实现文件名提取实体名
 */
function extractEntityName(serviceFile) {
  const fileName = path.basename(serviceFile);
  // {TaktXxx}Service.cs -> TaktXxx
  const match = fileName.match(/^(Takt\w+)Service\.cs$/);
  return match ? match[1] : null;
}

/**
 * 查找实体文件
 */
function findEntityFile(entityName) {
  function searchDir(dir) {
    if (!fs.existsSync(dir)) return null;
    const files = fs.readdirSync(dir);
    
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      
      if (stat.isDirectory()) {
        const result = searchDir(fullPath);
        if (result) return result;
      } else if (file === `${entityName}.cs`) {
        return fullPath;
      }
    }
    return null;
  }
  
  return searchDir(ENTITY_PATH);
}

/**
 * 获取实体所在的模块路径
 */
function getEntityModulePath(entityFile) {
  if (!entityFile) return null;
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const parts = relativePath.split(path.sep);
  parts.pop();
  return parts.length > 0 ? parts.join(path.sep) : '';
}

/**
 * 获取实体描述
 */
function extractEntityDescription(entityFile) {
  if (!entityFile || !fs.existsSync(entityFile)) return null;
  const content = fs.readFileSync(entityFile, 'utf-8');
  
  const sugarTableMatch = content.match(/SugarTable\([^,]*,\s*"([^"]+)"/);
  if (sugarTableMatch) return sugarTableMatch[1];
  
  const xmlMatch = content.match(/\/\/\/\s*<summary>\s*\n\s*\/\/\/\s*(.+?)\s*\n\s*\/\/\/\s*<\/summary>/s);
  if (xmlMatch) return xmlMatch[1].trim();
  
  return null;
}

/**
 * 获取控制器命名空间
 */
function getControllerNamespace(entityName) {
  const entityFile = findEntityFile(entityName);
  if (!entityFile) return 'Takt.WebApi.Controllers';
  
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const parts = relativePath.split(path.sep);
  parts.pop();
  
  let namespace = 'Takt.WebApi.Controllers';
  for (const part of parts) {
    namespace += `.${part}`;
  }
  
  return namespace;
}

/**
 * 获取服务命名空间
 */
function getServiceNamespace(entityName) {
  const entityFile = findEntityFile(entityName);
  if (!entityFile) return 'Takt.Application.Services';
  
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const parts = relativePath.split(path.sep);
  parts.pop();
  
  let namespace = 'Takt.Application.Services';
  for (const part of parts) {
    namespace += `.${part}`;
  }
  
  return namespace;
}

/**
 * 获取DTO命名空间
 */
function getDtoNamespace(entityName) {
  const entityFile = findEntityFile(entityName);
  if (!entityFile) return 'Takt.Application.Dtos';
  
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const parts = relativePath.split(path.sep);
  parts.pop();
  
  let namespace = 'Takt.Application.Dtos';
  for (const part of parts) {
    namespace += `.${part}`;
  }
  
  return namespace;
}

/**
 * 获取API模块分类和权限前缀
 */
function getApiModuleCategory(entityFile) {
  if (!entityFile) return ['System', '系统管理', null];
  
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const parts = relativePath.split(path.sep);
  parts.pop(); // 移除文件名
  
  // 构建权限前缀：从服务实现文件的目录结构提取
  // 例如：Accounting/Controlling/TaktCostCenterService.cs -> Accounting:Controlling
  const serviceRelativePath = findServiceRelativePath(entityFile);
  let permissionPrefix = null;
  let serviceParts = [];
  
  if (serviceRelativePath) {
    serviceParts = serviceRelativePath.split(path.sep);
    serviceParts.pop(); // 移除文件名
    if (serviceParts.length > 0) {
      permissionPrefix = serviceParts.join(':');
    }
  }
  
  const moduleMap = {
    'HumanResource': ['HumanResource', '人力资源', 'HumanResource'],
    'Organization': ['HumanResource', '人力资源', 'HumanResource'],
    'Personnel': ['HumanResource', '人力资源', 'HumanResource'],
    'AttendanceLeave': ['HumanResource', '人力资源', 'HumanResource'],
    'Logistics': ['Logistics', '物流管理', 'Logistics'],
    'Materials': ['Logistics', '物流管理', 'Logistics'],
    'Manufacturing': ['Logistics', '物流管理', 'Logistics'],
    'Routine': ['Routine', '日常事务', 'Routine'],
    'Business': ['Routine', '日常事务', 'Routine'],
    'Tasks': ['Routine', '日常事务', 'Routine'],
    'Workflow': ['Workflow', '工作流', 'Workflow'],
    'Statistics': ['Statistics', '统计分析', 'Statistics'],
    'Kanban': ['Statistics', '统计分析', 'Statistics'],
    'Logging': ['Statistics', '统计分析', 'Statistics'],
    'Security': ['Security', '安全中心', 'Security'],
    'Code': ['Code', '代码生成', 'Code'],
    'Generator': ['Code', '代码生成', 'Code'],
    'Accounting': ['Accounting', '财务管理', 'Accounting'],
    'Controlling': ['Accounting', '财务管理', 'Accounting:Controlling'],
    'Identity': ['System', '系统管理', 'System'],
    'System': ['System', '系统管理', 'System']
  };
  
  // 优先使用服务实现文件的目录结构
  if (permissionPrefix) {
    // 从目录路径推断模块名称
    const firstPart = serviceParts[0];
    const moduleInfo = moduleMap[firstPart] || ['System', '系统管理', null];
    return [moduleInfo[0], moduleInfo[1], permissionPrefix];
  }
  
  // 回退到实体文件目录
  for (const part of parts) {
    if (moduleMap[part]) return moduleMap[part];
  }
  
  return ['System', '系统管理', null];
}

/**
 * 查找服务实现文件的相对路径
 */
function findServiceRelativePath(entityFile) {
  if (!entityFile) return null;
  
  const entityName = path.basename(entityFile, '.cs');
  const serviceFileName = `${entityName}Service.cs`;
  
  function searchDir(dir) {
    if (!fs.existsSync(dir)) return null;
    const files = fs.readdirSync(dir);
    
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      
      if (stat.isDirectory()) {
        const result = searchDir(fullPath);
        if (result) return result;
      } else if (file === serviceFileName) {
        return path.relative(SERVICE_IMPL_PATH, fullPath);
      }
    }
    return null;
  }
  
  return searchDir(SERVICE_IMPL_PATH);
}

/**
 * 生成控制器文件内容
 */
function generateController(entityName, methods, description, entityFile) {
  const entityShortName = entityName.replace(/^Takt/, '');
  const namespace = getControllerNamespace(entityName);
  const serviceNamespace = getServiceNamespace(entityName);
  const dtoNamespace = getDtoNamespace(entityName);
  const [moduleKey, moduleName, permissionPrefix] = getApiModuleCategory(entityFile);
  
  let content = '';
  
  // 文件头
  content += `// ========================================\n`;
  content += `// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)\n`;
  content += `// 命名空间：${namespace}\n`;
  content += `// 文件名称：${entityName}sController.cs\n`;
  content += `// 创建时间：${new Date().toISOString().split('T')[0]}\n`;
  content += `// 创建人：Takt365(Cursor AI)\n`;
  content += `// 功能描述：${description || entityName}控制器，提供${entityShortName}管理的RESTful API接口\n`;
  content += `//\n`;
  content += `// 版权信息：Copyright (c) ${new Date().getFullYear()} Takt  All rights reserved.\n`;
  content += `// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。\n`;
  content += `// ========================================\n\n`;
  
  // using
  content += `using Microsoft.AspNetCore.Mvc;\n`;
  content += `using ${dtoNamespace};\n`;
  content += `using ${serviceNamespace};\n`;
  content += `using Takt.Domain.Interfaces;\n`;
  content += `using Takt.Infrastructure.Attributes;\n`;
  content += `using Takt.Shared.Models;\n`;
  content += `using Takt.WebApi.Controllers;\n`;
  content += `using Takt.Shared.Helpers;\n\n`;
  content += `namespace ${namespace};\n\n`;
  
  // 类定义
  const permissionBase = permissionPrefix ? (permissionPrefix.toLowerCase().endsWith(`:${entityShortName.toLowerCase()}`) ? permissionPrefix.toLowerCase() : `${permissionPrefix.toLowerCase()}:${entityShortName.toLowerCase()}`) : `${moduleKey.toLowerCase()}:${entityShortName.toLowerCase()}`;
  content += `/// <summary>\n`;
  content += `/// ${description || entityName}控制器\n`;
  content += `/// </summary>\n`;
  content += `[Route("api/[controller]", Name = "${description || entityShortName}")]\n`;
  content += `[ApiModule("${moduleKey}", "${moduleName}")]\n`;
  content += `[TaktPermission("${permissionBase.toLowerCase()}", "${description || entityShortName}管理")]\n`;
  content += `public class ${entityName}sController : TaktControllerBase\n`;
  content += `{\n`;
  content += `    private readonly I${entityName}Service _service;\n\n`;
  
  // 构造函数
  content += `    /// <summary>\n`;
  content += `    /// 构造函数\n`;
  content += `    /// </summary>\n`;
  content += `    public ${entityName}sController(\n`;
  content += `        I${entityName}Service service,\n`;
  content += `        ITaktUserContext? userContext = null,\n`;
  content += `        ITaktTenantContext? tenantContext = null,\n`;
  content += `        ITaktLocalizer? localizer = null)\n`;
  content += `        : base(userContext, tenantContext, localizer)\n`;
  content += `    {\n`;
  content += `        _service = service;\n`;
  content += `    }\n\n`;
  
  // 生成方法
  for (const method of methods) {
    content += generateControllerMethod(method, entityName, entityShortName, permissionBase, description);
  }
  
  content += `}\n`;
  
  return content;
}

/**
 * 智能去重实体名称
 * 例如：CostCenterCostCenter -> CostCenter
 *      UserUser -> User
 *      TaktCostCenterCostCenterStatusDto -> TaktCostCenterStatusDto
 *      UpdateCostCenterCostCenterStatusAsync -> UpdateCostCenterStatusAsync
 */
function deduplicateEntityName(name, entityShortName) {
  if (!name || !entityShortName) return name;
  
  let result = name;
  
  // 去除连续重复的实体短名（CostCenterCostCenter -> CostCenter）
  const shortPattern = new RegExp(`(${entityShortName})\\1+`, 'g');
  result = result.replace(shortPattern, '$1');
  
  // 去除连续重复的完整实体名（TaktCostCenterTaktCostCenter -> TaktCostCenter）
  const entityName = 'Takt' + entityShortName;
  const entityPattern = new RegExp(`(${entityName})\\1+`, 'g');
  result = result.replace(entityPattern, '$1');
  
  return result;
}

/**
 * 生成控制器方法
 */
function generateControllerMethod(method, entityName, entityShortName, permissionBase, entityDescription) {
  const { methodName, returnType, parameters } = method;
  
  let content = '';
  
  // 使用实体描述+英文实体名，如：利润中心表(ProfitCenter)
  const displayName = entityDescription ? `${entityDescription}(${entityShortName})` : entityShortName;
  
  // 智能去重：处理方法名和参数中的重复实体名
  const deduplicatedMethodName = deduplicateEntityName(methodName, entityShortName);
  const deduplicatedParameters = deduplicateEntityName(parameters, entityShortName);
  
  // 按照服务接口的方法顺序，严格匹配
  if (deduplicatedMethodName === 'GetCostCenterListAsync' || deduplicatedMethodName.match(/Get\w+ListAsync$/)) {
    content = generateListEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName === 'GetCostCenterByIdAsync' || deduplicatedMethodName.match(/Get\w+ByIdAsync$/)) {
    content = generateGetByIdEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
    } else if (deduplicatedMethodName.match(/Get\w+TreeOptionsAsync$/)) {
    content = generateTreeOptionsEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName === 'GetCostCenterOptionsAsync' || deduplicatedMethodName.match(/Get\w+OptionsAsync$/)) {
    content = generateOptionsEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName.match(/Get\w+TreeAsync$/)) {
    content = generateTreeEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName.match(/Get\w+ChildrenAsync$/)) {
    content = generateTreeChildrenEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName === 'CreateCostCenterAsync' || deduplicatedMethodName.match(/Create\w+Async$/)) {
    content = generateCreateEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName === 'DeleteCostCenterByIdAsync' || deduplicatedMethodName.match(/Delete\w+ByIdAsync$/)) {
    content = generateDeleteEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName === 'DeleteCostCenterBatchAsync' || deduplicatedMethodName.match(/Delete\w+BatchAsync$/)) {
    content = generateDeleteBatchEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName.match(/Update\w+StatusAsync$/)) {
    // 状态更新 - 必须在普通 Update 之前匹配
    content = generateUpdateStatusEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName.match(/Update\w+SortAsync$/)) {
    // 排序更新 - 必须在普通 Update 之前匹配
    content = generateUpdateSortEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName === 'UpdateCostCenterAsync' || deduplicatedMethodName.match(/^Update\w+Async$/)) {
    // 普通更新（排除 Status 和 Sort）
    content = generateUpdateEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName.match(/Get\w+TemplateAsync$/)) {
    content = generateTemplateEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName.match(/Import\w+Async$/)) {
    content = generateImportEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else if (deduplicatedMethodName.match(/Export\w+Async$/)) {
    content = generateExportEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  } else {
    // 对于其他未识别的方法，生成通用的POST端点
    content = generateGenericEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters);
  }
  
  return content;
}

/**
 * 生成列表端点
 */
function generateListEndpoint(method, entityName, entityShortName, permissionBase, displayName) {
  const paramMatch = method.parameters.match(/(\w+)\s+(\w+)/);
  const paramType = paramMatch ? paramMatch[1] : `${entityName}QueryDto`;
  const paramName = paramMatch ? paramMatch[2] : 'queryDto';
  
  return `
    /// <summary>
    /// 获取${displayName}列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("${permissionBase.toLowerCase()}:list", "查询${displayName}列表")]
    public async Task<ActionResult<TaktPagedResult<${entityName}Dto>>> Get${entityShortName}ListAsync([FromQuery] ${paramType} ${paramName})
    {
        var result = await _service.Get${entityShortName}ListAsync(${paramName});
        return Ok(result);
    }

`;
}

/**
 * 生成根据ID获取端点
 */
function generateGetByIdEndpoint(method, entityName, entityShortName, permissionBase, displayName) {
  return `
    /// <summary>
    /// 根据ID获取${displayName}
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("${permissionBase.toLowerCase()}:query", "查询${displayName}详情")]
    public async Task<ActionResult<${entityName}Dto>> Get${entityShortName}ByIdAsync(long id)
    {
        var item = await _service.Get${entityShortName}ByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

`;
}

/**
 * 生成选项端点
 * 特殊处理：TaktLanguage 和 TaktTranslation 使用 [AllowAnonymous]（不需要登录）
 */
function generateOptionsEndpoint(method, entityName, entityShortName, permissionBase, displayName) {
  // TaktLanguage 和 TaktTranslation 的 Options 端点不需要权限
  const allowAnonymousEntities = ['TaktLanguage', 'TaktTranslation'];
  const isAllowAnonymous = allowAnonymousEntities.includes(entityName);
  
  if (isAllowAnonymous) {
    return `
    /// <summary>
    /// 获取${displayName}选项列表
    /// </summary>
    [HttpGet("options")]
    [AllowAnonymous]
    public async Task<ActionResult<List<TaktSelectOption>>> Get${entityShortName}OptionsAsync()
    {
        var result = await _service.Get${entityShortName}OptionsAsync();
        return Ok(result);
    }

`;
  }
  
  // 其他实体使用正常的权限检查
  return `
    /// <summary>
    /// 获取${displayName}选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("${permissionBase.toLowerCase()}:query", "查询${displayName}选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> Get${entityShortName}OptionsAsync()
    {
        var result = await _service.Get${entityShortName}OptionsAsync();
        return Ok(result);
    }

`;
}

/**
 * 生成创建端点
 */
function generateCreateEndpoint(method, entityName, entityShortName, permissionBase, displayName) {
  return `
    /// <summary>
    /// 创建${displayName}
    /// </summary>
    [HttpPost]
    [TaktPermission("${permissionBase.toLowerCase()}:create", "创建${displayName}")]
    public async Task<ActionResult<${entityName}Dto>> Create${entityShortName}Async([FromBody] ${entityName}CreateDto dto)
    {
        var result = await _service.Create${entityShortName}Async(dto);
        return CreatedAtAction(nameof(Get${entityShortName}ByIdAsync), new { id = result.Id }, result);
    }

`;
}

/**
 * 生成更新端点
 */
function generateUpdateEndpoint(method, entityName, entityShortName, permissionBase, displayName) {
  return `
    /// <summary>
    /// 更新${displayName}
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("${permissionBase.toLowerCase()}:update", "更新${displayName}")]
    public async Task<ActionResult<${entityName}Dto>> Update${entityShortName}Async(long id, [FromBody] ${entityName}UpdateDto dto)
    {
        var result = await _service.Update${entityShortName}Async(id, dto);
        return Ok(result);
    }

`;
}

/**
 * 生成删除端点
 */
function generateDeleteEndpoint(method, entityName, entityShortName, permissionBase, displayName) {
  return `
    /// <summary>
    /// 删除${displayName}
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("${permissionBase.toLowerCase()}:delete", "删除${displayName}")]
    public async Task<ActionResult> Delete${entityShortName}ByIdAsync(long id)
    {
        await _service.Delete${entityShortName}ByIdAsync(id);
        return Ok();
    }

`;
}

/**
 * 生成批量删除端点
 */
function generateDeleteBatchEndpoint(method, entityName, entityShortName, permissionBase, displayName) {
  return `
    /// <summary>
    /// 批量删除${displayName}
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("${permissionBase.toLowerCase()}:delete", "批量删除${displayName}")]
    public async Task<ActionResult> Delete${entityShortName}BatchAsync([FromBody] List<long> ids)
    {
        await _service.Delete${entityShortName}BatchAsync(ids);
        return Ok();
    }

`;
}

/**
 * 生成状态更新端点
 */
function generateUpdateStatusEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters) {
  // 从去重后的方法名提取状态字段名
  // 例如：UpdateCostCenterStatusAsync -> 空（表示普通状态）
  //      UpdateUserMaritalStatusAsync -> Marital
  let statusFieldSuffix = ''; // 默认值：普通状态
  let statusDtoType = `${entityName}StatusDto`; // 默认DTO类型
  
  // 使用去重后的方法名进行提取
  const methodNameToUse = deduplicatedMethodName || method.methodName;
  const parametersToUse = deduplicatedParameters || method.parameters;
  
  if (methodNameToUse) {
    // 从方法名提取：Update{Entity}{Field}StatusAsync
    // 去除 Update + EntityName + StatusAsync，剩下的就是 Field
    const pattern = new RegExp(`^Update${entityShortName}(\\w*)StatusAsync$`);
    const match = methodNameToUse.match(pattern);
    if (match && match[1]) {
      statusFieldSuffix = match[1]; // 例如：Marital, OrderNum
    }
  }
  
  if (parametersToUse) {
    const typeMatch = parametersToUse.match(/(\w+StatusDto)\s+\w+/);
    if (typeMatch) {
      // 对 DTO 类型也进行去重
      statusDtoType = deduplicateEntityName(typeMatch[1], entityShortName);
    }
  }
  
  // 生成URL后缀和显示名称
  const urlSuffix = statusFieldSuffix.toLowerCase();
  const statusDisplayName = statusFieldSuffix || '状态';
  
  // 生成方法名：Update{Entity}{Field}StatusAsync
  const fullMethodName = `Update${entityShortName}${statusFieldSuffix}StatusAsync`;
  
  return `
    /// <summary>
    /// 更新${displayName}${statusDisplayName}
    /// </summary>
    [HttpPut("status${urlSuffix ? '-' + urlSuffix : ''}")]
    [TaktPermission("${permissionBase.toLowerCase()}:update", "更新${displayName}${statusDisplayName}")]
    public async Task<ActionResult<${entityName}Dto>> ${fullMethodName}([FromBody] ${statusDtoType} dto)
    {
        var result = await _service.${fullMethodName}(dto);
        return Ok(result);
    }

`;
}

/**
 * 生成排序更新端点
 */
function generateUpdateSortEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters) {
  // 使用去重后的参数
  const parametersToUse = deduplicatedParameters || method.parameters;
  
  // 提取 DTO 类型并去重
  let sortDtoType = `${entityName}SortDto`;
  if (parametersToUse) {
    const typeMatch = parametersToUse.match(/(\w+SortDto)\s+\w+/);
    if (typeMatch) {
      sortDtoType = deduplicateEntityName(typeMatch[1], entityShortName);
    }
  }
  
  return `
    /// <summary>
    /// 更新${displayName}排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("${permissionBase.toLowerCase()}:update", "更新${displayName}排序")]
    public async Task<ActionResult<${entityName}Dto>> Update${entityShortName}SortAsync([FromBody] ${sortDtoType} dto)
    {
        var result = await _service.Update${entityShortName}SortAsync(dto);
        return Ok(result);
    }

`;
}

/**
 * 生成模板端点
 */
function generateTemplateEndpoint(method, entityName, entityShortName, permissionBase, displayName) {
  return `
    /// <summary>
    /// 获取${displayName}导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("${permissionBase.toLowerCase()}:import", "获取${displayName}导入模板")]
    public async Task<IActionResult> Get${entityShortName}TemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.Get${entityShortName}TemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }

`;
}

/**
 * 生成导入端点
 */
function generateImportEndpoint(method, entityName, entityShortName, permissionBase, displayName) {
  return `
    /// <summary>
    /// 导入${displayName}
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("${permissionBase.toLowerCase()}:import", "导入${displayName}")]
    public async Task<ActionResult<object>> Import${entityShortName}Async(IFormFile file, [FromForm] string? sheetName = null)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
        }

        if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
            !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(GetLocalizedString("validation.importExcelOnlyXlsxOrXls", "Frontend"));
        }

        using var stream = file.OpenReadStream();
        var (success, fail, errors) = await _service.Import${entityShortName}Async(stream, sheetName);
        return Ok(new { success, fail, errors });
    }

`;
}

/**
 * 生成导出端点
 */
function generateExportEndpoint(method, entityName, entityShortName, permissionBase, displayName) {
  return `
    /// <summary>
    /// 导出${displayName}
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("${permissionBase.toLowerCase()}:export", "导出${displayName}")]
    public async Task<IActionResult> Export${entityShortName}Async([FromBody] ${entityName}QueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.Export${entityShortName}Async(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

`;
}

/**
 * 生成树形选项端点
 */
function generateTreeOptionsEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters) {
  return `
    /// <summary>
    /// 获取${displayName}树形选项列表（用于树形下拉框等）
    /// </summary>
    [HttpGet("tree-options")]
    [TaktPermission("${permissionBase.toLowerCase()}:query", "查询${displayName}树形选项")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> Get${entityShortName}TreeOptionsAsync()
    {
        var result = await _service.Get${entityShortName}TreeOptionsAsync();
        return Ok(result);
    }

`;
}

/**
 * 生成树形列表端点
 */
function generateTreeEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters) {
  return `
    /// <summary>
    /// 获取${displayName}树形列表
    /// </summary>
    [HttpGet("tree")]
    [TaktPermission("${permissionBase.toLowerCase()}:query", "查询${displayName}树形")]
    public async Task<ActionResult<List<${entityName}TreeDto>>> Get${entityShortName}TreeAsync([FromQuery] long parentId = 0, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.Get${entityShortName}TreeAsync(parentId, includeDisabled);
        return Ok(result);
    }

`;
}

/**
 * 生成子节点列表端点
 */
function generateTreeChildrenEndpoint(method, entityName, entityShortName, permissionBase, displayName, deduplicatedMethodName, deduplicatedParameters) {
  return `
    /// <summary>
    /// 获取${displayName}子节点列表
    /// </summary>
    [HttpGet("children")]
    [TaktPermission("${permissionBase.toLowerCase()}:query", "查询${displayName}子节点")]
    public async Task<ActionResult<List<${entityName}Dto>>> Get${entityShortName}ChildrenAsync([FromQuery] long parentId, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.Get${entityShortName}ChildrenAsync(parentId, includeDisabled);
        return Ok(result);
    }

`;
}

/**
 * 生成通用端点（用于未识别的方法）
 */
function generateGenericEndpoint(method, entityName, entityShortName, permissionBase, displayName) {
  const { methodName, returnType, parameters } = method;
  
  // 提取参数
  let paramStr = '';
  if (parameters && parameters.trim()) {
    paramStr = parameters.split(',').map(p => {
      p = p.trim();
      const parts = p.split(/\s+/);
      if (parts.length >= 2) {
        const type = parts[0];
        const name = parts[1];
        // 对于 string? 类型，使用 FromQuery
        if (type.includes('string')) {
          return `[FromQuery] ${type} ${name}`;
        }
        return `[FromBody] ${type} ${name}`;
      }
      return p;
    }).join(', ');
  }
  
  // 生成方法名（去掉Async后缀用于显示名）
  const actionName = methodName.replace(/Async$/, '');
  
  // 生成URL：驼峰转中划线
  const urlPath = actionName.replace(/([a-z])([A-Z])/g, '$1-$2').toLowerCase();
  
  return `
    /// <summary>
    /// ${actionName}
    /// </summary>
    [HttpPost("${urlPath}")]
    [TaktPermission("${permissionBase.toLowerCase()}:custom", "${actionName}")]
    public async Task<ActionResult> ${methodName}(${paramStr})
    {
        var result = await _service.${methodName}(${parameters.split(',').map(p => p.trim().split(/\s+/)[1]).join(', ')});
        return Ok(result);
    }

`;
}

/**
 * 查找服务实现文件（不含I前缀）
 */
function findServiceImplementationFiles() {
  const services = [];
  
  function searchDir(dir) {
    if (!fs.existsSync(dir)) return;
    
    const files = fs.readdirSync(dir);
    
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      
      if (stat.isDirectory()) {
        // 排除 Engine 目录
        if (isInEngineDirectory(fullPath)) {
          continue;
        }
        searchDir(fullPath);
      } else if (file.startsWith('Takt') && file.endsWith('Service.cs') && !file.startsWith('ITakt')) {
        services.push(fullPath);
      }
    }
  }
  
  searchDir(SERVICE_IMPL_PATH);
  return services;
}

/**
 * 处理单个服务实现
 */
function processServiceImplementation(serviceFile) {
  const entityName = extractEntityName(serviceFile);
  if (!entityName) {
    console.log(`  ❌ 无法从文件名提取实体名: ${path.basename(serviceFile)}`);
    return false;
  }
  
  // 排除特殊的DTO文件对应的实体
  if (shouldExcludeEntity(entityName)) {
    console.log(`  ⏭️ 跳过排除的实体: ${entityName}`);
    return false;
  }
  
  // 排除 Engine 和 Identity 特殊目录下的所有控制器
  if (isInExcludedDirectory(serviceFile)) {
    console.log(`  ⏭️  跳过排除目录控制器: ${entityName}（${path.relative(SERVICE_IMPL_PATH, serviceFile)}）`);
    return false;
  }
  
  if (targetEntity && !generateAll && entityName !== targetEntity) {
    return false;
  }
  
  console.log(`\n生成: ${entityName}`);
  console.log('='.repeat(60));
  console.log(`  服务实现文件: ${path.relative(SERVICE_IMPL_PATH, serviceFile)}`);
  
  const methods = extractServiceMethods(serviceFile);
  console.log(`  方法数量: ${methods.length}`);
  
  const entityFile = findEntityFile(entityName);
  const description = extractEntityDescription(entityFile);
  
  const content = generateController(entityName, methods, description, entityFile);
  
  // 从服务实现文件获取相对路径，而不是实体文件
  const relativePath = path.relative(SERVICE_IMPL_PATH, serviceFile);
  const parts = relativePath.split(path.sep);
  parts.pop(); // 移除文件名，保留目录路径
  const modulePath = parts.length > 0 ? parts.join(path.sep) : '';
  
  const controllerDir = modulePath 
    ? path.join(CONTROLLER_PATH, modulePath)
    : CONTROLLER_PATH;
  
  const controllerFile = path.join(controllerDir, `${entityName}sController.cs`);
  const relativeOutput = path.relative(CONTROLLER_PATH, controllerFile);
  
  if (dryRun) {
    console.log(`  🔍 [预览模式] 将生成: ${relativeOutput}`);
    return true;
  }
  
  if (!fs.existsSync(controllerDir)) {
    fs.mkdirSync(controllerDir, { recursive: true });
  }
  
  fs.writeFileSync(controllerFile, content, 'utf-8');
  console.log(`  ✅ 已保存: ${relativeOutput}`);
  
  return true;
}

/**
 * 主函数
 */
function main() {
  console.log('========================================');
  console.log('  控制器生成工具（严格依赖：服务实现 → 控制器）');
  console.log('========================================\n');
  
  const serviceFiles = findServiceImplementationFiles();
  console.log(`找到 ${serviceFiles.length} 个服务实现文件\n`);
  
  let successCount = 0;
  let failCount = 0;
  let skipCount = 0;
  
  for (const serviceFile of serviceFiles) {
    const result = processServiceImplementation(serviceFile);
    if (result === true) successCount++;
    else if (result === false) skipCount++;
    else failCount++;
  }
  
  console.log('\n========================================');
  console.log(`  完成！已生成: ${successCount}, 跳过: ${skipCount}, 失败: ${failCount}`);
  console.log('========================================');
}

try {
  main();
} catch (error) {
  console.error('❌ 生成失败:', error.message);
  console.error(error.stack);
  process.exit(1);
}
