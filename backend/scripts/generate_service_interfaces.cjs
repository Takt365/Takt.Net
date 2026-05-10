/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：generate_service_interfaces.cjs
 * 创建时间：2026-04-28
 * 功能描述：根据DTO自动生成服务接口文件（严格依赖链：DTO → 服务接口）
 *   1. 扫描所有DTO文件，提取DTO类信息
 *   2. 根据DTO类型（Dto、CreateDto、UpdateDto、QueryDto、StatusDto、SortDto）生成标准服务接口
 *   3. 支持10个标准操作：List、ById、Options、Create、Update、Delete、Status、Sort、Template、Import、Export
 *   4. 自动识别实体路径并生成对应的服务接口文件
 * 
 * 依赖链：DTO → 服务接口 → 服务实现 → 控制器
 * 
 * 使用方法：
 *   1. 生成单个实体（默认）：node scripts/generate_service_interfaces.cjs --entity TaktUser
 *   2. 全量生成（谨慎）：node scripts/generate_service_interfaces.cjs --all
 *   3. 预览模式：node scripts/generate_service_interfaces.cjs --entity TaktUser --dry-run
 * ========================================
 */

const fs = require('fs');
const path = require('path');

const DTO_PATH = path.join(__dirname, '../src/Takt.Application/Dtos');
const SERVICE_PATH = path.join(__dirname, '../src/Takt.Application/Services');
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
  console.error('  1. 生成单个实体：node scripts/generate_service_interfaces.cjs --entity TaktEmployee');
  console.error('  2. 全量生成（谨慎）：node scripts/generate_service_interfaces.cjs --all');
  console.error('  3. 预览模式：node scripts/generate_service_interfaces.cjs --entity TaktEmployee --dry-run');
  console.error('');
  process.exit(1);
}

/**
 * 检查DTO文件是否应该被排除
 */
function shouldExcludeDto(dtoFile) {
  const fileName = path.basename(dtoFile);
  // 排除特殊的DTO文件
  const excludedFiles = ['TaktCacheDtos.cs', 'TaktServerMonitorDtos.cs'];
  return excludedFiles.includes(fileName);
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
 * 提取DTO文件中的DTO类信息
 */
function extractDtoInfo(dtoFile) {
  const content = fs.readFileSync(dtoFile, 'utf-8');
  const dtos = {
    base: null,      // TaktXxxDto
    query: null,     // TaktXxxQueryDto
    create: null,    // TaktXxxCreateDto
    update: null,    // TaktXxxUpdateDto
    statuses: [],    // TaktXxxStatusDto 数组（支持多个）
    sort: null,      // TaktXxxSortDto
    tree: null,      // TaktXxxTreeDto
    template: null,  // TaktXxxTemplateDto
    import: null,     // TaktXxxImportDto
    export: null,    // TaktXxxExportDto
    entityName: null // TaktXxx
  };

  // 匹配所有DTO类（冒号可选，因为有些DTO不继承基类）
  const classRegex = /public\s+(partial\s+)?class\s+(\w+Dto\w*)\s*(:|\{)/g;
  let match;

  while ((match = classRegex.exec(content)) !== null) {
    const className = match[2];
    
    if (className.includes('QueryDto')) {
      dtos.query = className;
      dtos.entityName = className.replace('QueryDto', '');
    } else if (className.includes('CreateDto')) {
      dtos.create = className;
      if (!dtos.entityName) dtos.entityName = className.replace('CreateDto', '');
    } else if (className.includes('UpdateDto')) {
      dtos.update = className;
    } else if (className.includes('StatusDto')) {
      dtos.statuses.push(className);  // 添加到数组，而不是覆盖
    } else if (className.includes('SortDto')) {
      dtos.sort = className;
    } else if (className.includes('TreeDto')) {
      dtos.tree = className;
      if (!dtos.entityName) dtos.entityName = className.replace('TreeDto', '');
    } else if (className.includes('TemplateDto')) {
      dtos.template = className;
    } else if (className.includes('ImportDto')) {
      dtos.import = className;
    } else if (className.includes('ExportDto')) {
      dtos.export = className;
    } else {
      dtos.base = className;
      if (!dtos.entityName) dtos.entityName = className.replace('Dto', '');
    }
  }

  return dtos;
}

/**
 * 查找实体文件路径
 */
function findEntityFile(entityName) {
  function searchDir(dir) {
    const files = fs.readdirSync(dir);
    
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      
      if (stat.isDirectory()) {
        // 排除 Engine 目录
        if (isInEngineDirectory(fullPath)) {
          continue;
        }
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
 * 检查DTO文件中是否包含TreeDto
 */
function checkTreeDto(dtoFile) {
  if (!dtoFile || !fs.existsSync(dtoFile)) {
    return false;
  }
  const content = fs.readFileSync(dtoFile, 'utf-8');
  return /class\s+\w*TreeDto\s*[:{]/.test(content);
}

/**
 * 从实体文件提取描述信息
 */
function extractEntityDescription(entityFile) {
  if (!entityFile || !fs.existsSync(entityFile)) {
    return null;
  }
  
  const content = fs.readFileSync(entityFile, 'utf-8');
  
  // 提取SugarTable描述
  const sugarTableMatch = content.match(/SugarTable\([^,]*,\s*"([^"]+)"/);
  if (sugarTableMatch) {
    return sugarTableMatch[1];
  }
  
  // 提取XML注释
  const xmlMatch = content.match(/\/\/\/\s*<summary>\s*\n\s*\/\/\/\s*(.+?)\s*\n\s*\/\/\/\s*<\/summary>/s);
  if (xmlMatch) {
    return xmlMatch[1].trim();
  }
  
  return null;
}

/**
 * 从实体文件识别CRUD类型
 */
function identifyCrudTypeFromEntity(entityFile) {
  if (!entityFile || !fs.existsSync(entityFile)) {
    return 'Single';
  }
  
  const content = fs.readFileSync(entityFile, 'utf-8');
  
  // 检查是否有Navigate导航属性（主子表）
  const hasNavigation = /\[Navigate\(/.test(content);
  if (hasNavigation) {
    return 'MasterDetail';
  }
  
  // 检查是否有ParentId字段（Tree表）
  const hasParentId = /public\s+\w+\s+ParentId\s*\{/.test(content);
  if (hasParentId) {
    return 'Tree';
  }
  
  return 'Single';
}

/**
 * 获取实体所在的模块路径
 */
function getEntityModulePath(entityFile) {
  if (!entityFile) return null;
  
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const parts = relativePath.split(path.sep);
  
  // 移除文件名，保留目录路径
  parts.pop();
  
  return parts.length > 0 ? parts.join(path.sep) : '';
}

/**
 * 生成服务接口文件内容
 */
function generateServiceInterface(entityName, dtoInfo, description, entityFile) {
  const entityShortName = entityName.replace(/^Takt/, '');
  const namespace = getNamespace(entityName);
  const dtoNamespace = getDtoNamespace(entityName);
  const crudType = identifyCrudTypeFromEntity(entityFile);
  const hasTreeDto = dtoInfo.tree !== null;
  
  let content = '';
  
  // 文件头注释
  content += `// ========================================\n`;
  content += `// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) \n`;
  content += `// 命名空间：${namespace}\n`;
  content += `// 文件名称：I${entityName}Service.cs\n`;
  content += `// 创建时间：${new Date().toISOString().split('T')[0]}\n`;
  content += `// 创建人：Takt365\n`;
  
  // 根据CRUD类型生成功能描述
  const typeDesc = crudType === 'Tree' ? '（树形结构）' : crudType === 'MasterDetail' ? '（主子表）' : '';
  content += `// 功能描述：${description || entityName}应用服务接口${typeDesc}，定义${entityShortName}管理的业务操作\n`;
  content += `// \n`;
  content += `// 版权信息：Copyright (c) ${new Date().getFullYear()} Takt  All rights reserved.\n`;
  content += `// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。\n`;
  content += `// ========================================\n\n`;
  
  // using语句
  content += `using ${dtoNamespace};\n`;
  content += `using Takt.Shared.Models;\n\n`;
  content += `namespace ${namespace};\n\n`;
  
  // 接口定义
  content += `/// <summary>\n`;
  content += `/// ${description || entityName}应用服务接口${typeDesc}\n`;
  content += `/// </summary>\n`;
  content += `public interface I${entityName}Service\n`;
  content += `{\n`;
  
  // 1. 获取列表（分页）
  if (dtoInfo.base && dtoInfo.query) {
    content += `    /// <summary>\n`;
    content += `    /// 获取${description}列表（分页）\n`;
    content += `    /// </summary>\n`;
    content += `    /// <param name="queryDto">查询DTO</param>\n`;
    content += `    /// <returns>分页结果</returns>\n`;
    content += `    Task<TaktPagedResult<${dtoInfo.base}>> Get${entityShortName}ListAsync(${dtoInfo.query} queryDto);\n\n`;
  }
  
  // 2. 根据ID获取
  if (dtoInfo.base) {
    const getByIdDesc = crudType === 'MasterDetail' ? '（包含子表数据）' : '';
    content += `    /// <summary>\n`;
    content += `    /// 根据ID获取${description}${getByIdDesc}\n`;
    content += `    /// </summary>\n`;
    content += `    /// <param name="id">${description}ID</param>\n`;
    content += `    /// <returns>${description}DTO</returns>\n`;
    content += `    Task<${dtoInfo.base}?> Get${entityShortName}ByIdAsync(long id);\n\n`;
  }
  
  // 3. 获取选项列表
  if (dtoInfo.base) {
    content += `    /// <summary>\n`;
    content += `    /// 获取${description}选项列表（用于下拉框等）\n`;
    content += `    /// </summary>\n`;
    content += `    /// <returns>${description}选项列表</returns>\n`;
    content += `    Task<List<TaktSelectOption>> Get${entityShortName}OptionsAsync();\n\n`;
  }
  
  // 4. Tree表：树形服务
  if (crudType === 'Tree' || hasTreeDto) {
    content += `    // ==================== 树形服务 ====================\n\n`;
    content += `    /// <summary>\n`;
    content += `    /// 获取${entityShortName}树形选项列表（用于树形下拉框等）\n`;
    content += `    /// </summary>\n`;
    content += `    /// <returns>${entityShortName}树形选项列表</returns>\n`;
    content += `    Task<List<TaktTreeSelectOption>> Get${entityShortName}TreeOptionsAsync();\n\n`;
    
    content += `    /// <summary>\n`;
    content += `    /// 获取${entityShortName}树形列表\n`;
    content += `    /// </summary>\n`;
    content += `    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>\n`;
    content += `    /// <param name="includeDisabled">是否包含禁用的${description || entityShortName}（默认false）</param>\n`;
    content += `    /// <returns>${entityShortName}树形列表</returns>\n`;
    content += `    Task<List<${entityName}TreeDto>> Get${entityShortName}TreeAsync(long parentId = 0, bool includeDisabled = false);\n\n`;
    
    content += `    /// <summary>\n`;
    content += `    /// 获取${entityShortName}子节点列表\n`;
    content += `    /// </summary>\n`;
    content += `    /// <param name="parentId">父级ID（0表示根节点）</param>\n`;
    content += `    /// <param name="includeDisabled">是否包含禁用的${description || entityShortName}（默认false）</param>\n`;
    content += `    /// <returns>${entityShortName}子节点列表</returns>\n`;
    content += `    Task<List<${entityName}Dto>> Get${entityShortName}ChildrenAsync(long parentId, bool includeDisabled = false);\n\n`;
    content += `    // ==================== 树形服务 ====================\n\n`;
  }
  
  // 5. 创建
  if (dtoInfo.create) {
    const createDesc = crudType === 'MasterDetail' ? '（包含子表数据）' : '';
    content += `    /// <summary>\n`;
    content += `    /// 创建${description}${createDesc}\n`;
    content += `    /// </summary>\n`;
    content += `    /// <param name="dto">创建${description}DTO</param>\n`;
    content += `    /// <returns>${description}DTO</returns>\n`;
    content += `    Task<${dtoInfo.base}> Create${entityShortName}Async(${dtoInfo.create} dto);\n\n`;
  }
  
  // 6. 更新
  if (dtoInfo.update) {
    const updateDesc = crudType === 'MasterDetail' ? '（包含子表数据）' : '';
    content += `    /// <summary>\n`;
    content += `    /// 更新${description}${updateDesc}\n`;
    content += `    /// </summary>\n`;
    content += `    /// <param name="id">${description}ID</param>\n`;
    content += `    /// <param name="dto">更新${description}DTO</param>\n`;
    content += `    /// <returns>${description}DTO</returns>\n`;
    content += `    Task<${dtoInfo.base}> Update${entityShortName}Async(long id, ${dtoInfo.update} dto);\n\n`;
  }
  
  // 7. 删除
  if (dtoInfo.base) {
    const deleteDesc = crudType === 'Tree' ? '（禁止有子节点时删除）' : crudType === 'MasterDetail' ? '（级联删除子表）' : '';
    content += `    /// <summary>\n`;
    content += `    /// 删除${description}(${entityShortName})${deleteDesc}\n`;
    content += `    /// </summary>\n`;
    content += `    /// <param name="id">${description}(${entityShortName})ID</param>\n`;
    content += `    /// <returns>任务</returns>\n`;
    content += `    Task Delete${entityShortName}ByIdAsync(long id);\n\n`;
    
    content += `    /// <summary>\n`;
    content += `    /// 批量删除${description}(${entityShortName})${deleteDesc}\n`;
    content += `    /// </summary>\n`;
    content += `    /// <param name="ids">${description}(${entityShortName})ID列表</param>\n`;
    content += `    /// <returns>任务</returns>\n`;
    content += `    Task Delete${entityShortName}BatchAsync(IEnumerable<long> ids);\n\n`;
  }
  
  // 8. 更新状态（支持多个StatusDto）
  if (dtoInfo.statuses && dtoInfo.statuses.length > 0) {
    for (const statusDto of dtoInfo.statuses) {
      // 从 StatusDto 类名中提取字段名部分，并去重
      // 例如：TaktEmployeeStatusDto -> EmployeeStatus -> 去重 -> Status -> 方法名：UpdateEmployeeStatusAsync
      //      TaktEmployeeMaritalStatusDto -> MaritalStatus -> 不去重 -> 方法名：UpdateEmployeeMaritalStatusAsync
      //      TaktPcbaInspectionDetailInspectionStatusDto -> InspectionStatus -> 不去重 -> 方法名：UpdatePcbaInspectionDetailInspectionStatusAsync
      //      TaktPcbaInspectionDetailStatusDto -> Status -> 不去重 -> 方法名：UpdatePcbaInspectionDetailStatusAsync
      let statusField = statusDto.replace(entityName, '').replace('StatusDto', 'Status');
      
      // 去重：如果字段名以实体短名开头，去除重复部分
      const entityShortName = entityName.replace('Takt', '');
      if (statusField.startsWith(entityShortName)) {
        statusField = statusField.substring(entityShortName.length);
      }
      
      content += `    /// <summary>\n`;
      content += `    /// 更新${description}(${entityShortName})${statusField}\n`;
      content += `    /// </summary>\n`;
      content += `    /// <param name="dto">${description}(${entityShortName})${statusField}DTO</param>\n`;
      content += `    /// <returns>${description}(${entityShortName})DTO</returns>\n`;
      content += `    Task<${dtoInfo.base}> Update${entityShortName}${statusField}Async(${statusDto} dto);\n\n`;
    }
  }
  
  // 9. 更新排序
  if (dtoInfo.sort) {
    content += `    /// <summary>\n`;
    content += `    /// 更新${description}(${entityShortName})排序\n`;
    content += `    /// </summary>\n`;
    content += `    /// <param name="dto">${description}(${entityShortName})排序DTO</param>\n`;
    content += `    /// <returns>${description}(${entityShortName})DTO</returns>\n`;
    content += `    Task<${dtoInfo.base}> Update${entityShortName}SortAsync(${dtoInfo.sort} dto);\n\n`;
  }
  
  // 10. 获取导入模板
  if (dtoInfo.base) {
    content += `    /// <summary>\n`;
    content += `    /// 获取导入模板\n`;
    content += `    /// </summary>\n`;
    content += `    /// <param name="sheetName">工作表名称</param>\n`;
    content += `    /// <param name="fileName">文件名</param>\n`;
    content += `    /// <returns>Excel模板文件信息（文件名和内容）</returns>\n`;
    content += `    Task<(string fileName, byte[] content)> Get${entityShortName}TemplateAsync(string? sheetName, string? fileName);\n\n`;
  }
  
  // 11. 导入
  if (dtoInfo.base) {
    content += `    /// <summary>\n`;
    content += `    /// 导入${description}(${entityShortName})\n`;
    content += `    /// </summary>\n`;
    content += `    /// <param name="fileStream">Excel文件流</param>\n`;
    content += `    /// <param name="sheetName">工作表名称</param>\n`;
    content += `    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>\n`;
    content += `    Task<(int success, int fail, List<string> errors)> Import${entityShortName}Async(Stream fileStream, string? sheetName);\n\n`;
  }
  
  // 12. 导出
  if (dtoInfo.base && dtoInfo.query) {
    content += `    /// <summary>\n`;
    content += `    /// 导出${description}(${entityShortName})\n`;
    content += `    /// </summary>\n`;
    content += `    /// <param name="query">${description}(${entityShortName})查询DTO（包含查询条件）</param>\n`;
    content += `    /// <param name="sheetName">工作表名称</param>\n`;
    content += `    /// <param name="fileName">文件名</param>\n`;
    content += `    /// <returns>Excel文件信息（文件名和内容）</returns>\n`;
    content += `    Task<(string fileName, byte[] content)> Export${entityShortName}Async(${dtoInfo.query} query, string? sheetName, string? fileName);\n`;
  }
  
  content += `}\n\n`;
  return content;
}

/**
 * 获取服务接口命名空间
 */
function getNamespace(entityName) {
  const entityFile = findEntityFile(entityName);
  if (!entityFile) {
    return `Takt.Application.Services`;
  }
  
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const parts = relativePath.split(path.sep);
  
  // 构建服务命名空间 - 直接使用实体目录路径
  let namespace = 'Takt.Application.Services';
  
  for (let i = 0; i < parts.length - 1; i++) {
    namespace += `.${parts[i]}`;
  }
  
  return namespace;
}

/**
 * 获取DTO命名空间
 */
function getDtoNamespace(entityName) {
  const entityFile = findEntityFile(entityName);
  if (!entityFile) {
    return `Takt.Application.Dtos`;
  }
  
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const parts = relativePath.split(path.sep);
  
  // 构建DTO命名空间 - 直接使用实体目录路径
  let namespace = 'Takt.Application.Dtos';
  
  for (let i = 0; i < parts.length - 1; i++) {
    namespace += `.${parts[i]}`;
  }
  
  return namespace;
}

/**
 * 查找DTO文件
 */
function findDtoFile(entityName) {
  function searchDir(dir) {
    const files = fs.readdirSync(dir);
    
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      
      if (stat.isDirectory()) {
        // 排除 Engine 目录
        if (isInEngineDirectory(fullPath)) {
          continue;
        }
        const result = searchDir(fullPath);
        if (result) return result;
      } else if (file === `${entityName}Dtos.cs`) {
        return fullPath;
      }
    }
    
    return null;
  }
  
  return searchDir(DTO_PATH);
}

/**
 * 处理单个实体
 */
function processEntity(entityName) {
  console.log(`\n生成: ${entityName}`);
  console.log('='.repeat(60));
  
  // 查找DTO文件
  const dtoFile = findDtoFile(entityName);
  if (!dtoFile) {
    console.log(`  ❌ 未找到DTO文件: ${entityName}Dtos.cs`);
    return false;
  }
  
  console.log(`  DTO文件: ${path.relative(DTO_PATH, dtoFile)}`);
  
  // 提取DTO信息
  const dtoInfo = extractDtoInfo(dtoFile);
  if (!dtoInfo.entityName) {
    console.log(`  ❌ DTO文件中未找到有效的DTO类`);
    return false;
  }
  
  console.log(`  实体: ${dtoInfo.entityName}`);
  console.log(`  Dto: ${dtoInfo.base || '无'}`);
  console.log(`  QueryDto: ${dtoInfo.query || '无'}`);
  console.log(`  CreateDto: ${dtoInfo.create || '无'}`);
  console.log(`  UpdateDto: ${dtoInfo.update || '无'}`);
  console.log(`  StatusDto: ${dtoInfo.statuses.length > 0 ? dtoInfo.statuses.join(', ') : '无'}`);
  console.log(`  SortDto: ${dtoInfo.sort || '无'}`);
  
  // 查找实体文件
  const entityFile = findEntityFile(dtoInfo.entityName);
  const description = extractEntityDescription(entityFile);
  
  // 生成服务接口内容
  const content = generateServiceInterface(dtoInfo.entityName, dtoInfo, description, entityFile);
  
  // 确定输出路径
  const modulePath = getEntityModulePath(entityFile);
  const outputDir = modulePath 
    ? path.join(SERVICE_PATH, modulePath)
    : SERVICE_PATH;
  
  const outputFile = path.join(outputDir, `I${dtoInfo.entityName}Service.cs`);
  
  // 排除 Engine 和其子目录下的所有服务接口
  if (isInEngineDirectory(outputFile)) {
    console.log(`  ⏭️ 跳过: ${dtoInfo.entityName} (Engine目录下的特殊服务)`);
    return true;
  }
  
  if (dryRun) {
    console.log(`  🔍 [预览模式] 将生成: ${path.relative(SERVICE_PATH, outputFile)}`);
    return true;
  }
  
  // 确保目录存在
  if (!fs.existsSync(outputDir)) {
    fs.mkdirSync(outputDir, { recursive: true });
  }
  
  // 写入文件
  fs.writeFileSync(outputFile, content, 'utf-8');
  console.log(`  ✅ 已保存: ${path.relative(SERVICE_PATH, outputFile)}`);
  
  return true;
}

/**
 * 主函数
 */
function main() {
  console.log('========================================');
  console.log('  服务接口生成工具');
  console.log('========================================\n');
  
  // 查找所有DTO文件
  const dtoFiles = [];
  
  function findDtoFiles(dir) {
    const files = fs.readdirSync(dir);
    
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      
      if (stat.isDirectory()) {
        // 排除所有包含 Engine 的目录
        if (isInEngineDirectory(fullPath)) {
          continue;
        }
        findDtoFiles(fullPath);
      } else if (file.endsWith('Dtos.cs')) {
        // 排除特殊的DTO文件
        if (shouldExcludeDto(fullPath)) {
          continue;
        }
        dtoFiles.push(fullPath);
      }
    }
  }
  
  findDtoFiles(DTO_PATH);
  
  console.log(`找到 ${dtoFiles.length} 个DTO文件\n`);
  
  // 处理每个DTO文件
  let successCount = 0;
  let failCount = 0;
  
  for (const dtoFile of dtoFiles) {
    const fileName = path.basename(dtoFile, 'Dtos.cs');
    
    // 如果指定了实体，只处理该实体（除非使用 --all）
    if (targetEntity && !generateAll && fileName !== targetEntity) {
      continue;
    }
    
    const success = processEntity(fileName);
    if (success) {
      successCount++;
    } else {
      failCount++;
    }
  }
  
  console.log('\n========================================');
  console.log(`  完成！已生成: ${successCount}, 失败: ${failCount}`);
  console.log('========================================');
}

// 运行
try {
  main();
} catch (error) {
  console.error('❌ 生成失败:', error.message);
  console.error(error.stack);
  process.exit(1);
}
