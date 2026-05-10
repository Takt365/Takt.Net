/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：generate_service_implementations.cjs
 * 创建时间：2026-04-28
 * 功能描述：根据服务接口自动生成服务实现文件（严格依赖链：服务接口 → 服务实现）
 *   1. 扫描所有服务接口文件（I*TaktService.cs）
 *   2. 解析接口方法签名
 *   3. 根据接口方法生成标准的服务实现
 *   4. 自动识别实体路径并生成对应的服务实现文件
 * 
 * 依赖链：DTO → 服务接口 → 服务实现 → 控制器
 * 
 * 使用方法：
 *   1. 生成单个实体（默认）：node scripts/generate_service_implementations.cjs --entity TaktEmployee
 *   2. 全量生成（谨慎）：node scripts/generate_service_implementations.cjs --all
 *   3. 预览模式：node scripts/generate_service_implementations.cjs --entity TaktEmployee --dry-run
 * ========================================
 */

const fs = require('fs');
const path = require('path');

const SERVICE_INTERFACE_PATH = path.join(__dirname, '../src/Takt.Application/Services');
const SERVICE_IMPL_PATH = path.join(__dirname, '../src/Takt.Application/Services');
const ENTITY_PATH = path.join(__dirname, '../src/Takt.Domain/Entities');
const DTO_PATH = path.join(__dirname, '../src/Takt.Application/Dtos');

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
  console.error('  1. 生成单个实体：node scripts/generate_service_implementations.cjs --entity TaktEmployee');
  console.error('  2. 全量生成（谨慎）：node scripts/generate_service_implementations.cjs --all');
  console.error('  3. 预览模式：node scripts/generate_service_implementations.cjs --entity TaktEmployee --dry-run');
  console.error('');
  process.exit(1);
}

/**
 * 检查DTO文件是否应该被排除
 */
function shouldExcludeDto(entityName) {
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
 * 从接口文件中提取方法签名
 */
function extractInterfaceMethods(interfaceFile) {
  const content = fs.readFileSync(interfaceFile, 'utf-8');
  const methods = [];
  
  // 排除 Engine 目录下的所有服务实现
  if (isInEngineDirectory(interfaceFile)) {
    return methods;
  }
  
  // 移除所有XML注释，只保留方法声明
  const withoutComments = content.replace(/\/\/\/[^\n]*\n/g, '').replace(/\/\*[\s\S]*?\*\//g, '');
  
  // 匹配方法签名：支持嵌套泛型如 Task<TaktPagedResult<TaktGenTableDto>>
  // 使用更宽松的正则：Task<任意内容> MethodName(...) 或 Task MethodName(...)
  const methodRegex = /Task(?:<([\s\S]+?)>)?\s+(\w+Async)\s*\(([^)]*)\)\s*;/g;
  let match;
  
  while ((match = methodRegex.exec(withoutComments)) !== null) {
    const returnType = match[1] ? match[1].trim() : 'void';
    const methodName = match[2];
    const parameters = match[3].trim();
    
    methods.push({
      returnType,
      methodName,
      parameters
    });
  }
  
  return methods;
}

/**
 * 查找 DTO 文件
 */
function findDtoFile(entityName) {
  const dtoFileName = `${entityName}Dtos.cs`;
  
  function searchDir(dir) {
    if (!fs.existsSync(dir)) return null;
    
    const entries = fs.readdirSync(dir, { withFileTypes: true });
    
    for (const entry of entries) {
      const fullPath = path.join(dir, entry.name);
      
      if (entry.isDirectory()) {
        const result = searchDir(fullPath);
        if (result) return result;
      } else if (entry.name === dtoFileName) {
        return fullPath;
      }
    }
    
    return null;
  }
  
  return searchDir(DTO_PATH);
}

/**
 * 从接口文件名提取实体名
 */
function extractEntityName(interfaceFile) {
  const fileName = path.basename(interfaceFile);
  // I{TaktXxx}Service.cs -> TaktXxx
  const match = fileName.match(/^I(Takt\w+)Service\.cs$/);
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
 * 查找 QueryDto 文件
 */
function findQueryDtoFile(entityName) {
  const queryDtoName = `${entityName}QueryDto`;
  
  function searchDir(dir) {
    if (!fs.existsSync(dir)) return null;
    
    const files = fs.readdirSync(dir);
    
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      
      if (stat.isDirectory()) {
        const result = searchDir(fullPath);
        if (result) return result;
      } else if (file.endsWith('.cs')) {
        // 检查文件是否包含目标 QueryDto 完整类定义（有继承的 partial class 或独立 class）
        // 优先匹配有继承的类（如继承 TaktPagedQuery），这才是完整的 QueryDto 定义
        try {
          const content = fs.readFileSync(fullPath, 'utf-8');
          // 匹配完整的类定义：有继承的 partial class 或 class
          const fullClassRegex = new RegExp(`public\\s+(partial\\s+)?class\\s+${queryDtoName}\\s*:`);
          if (fullClassRegex.test(content)) {
            return fullPath;
          }
        } catch (e) {
          // 忽略读取错误的文件
        }
      }
    }
    
    return null;
  }
  
  return searchDir(DTO_PATH);
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
 * 获取实体描述
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
 * 获取服务命名空间
 */
function getServiceNamespace(entityName) {
  const entityFile = findEntityFile(entityName);
  if (!entityFile) {
    return 'Takt.Application.Services';
  }
  
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const parts = relativePath.split(path.sep);
  
  // 直接使用实体目录路径
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
    return 'Takt.Application.Dtos';
  }
  
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const parts = relativePath.split(path.sep);
  
  // 直接使用实体目录路径
  let namespace = 'Takt.Application.Dtos';
  
  for (let i = 0; i < parts.length - 1; i++) {
    namespace += `.${parts[i]}`;
  }
  
  return namespace;
}

/**
 * 判断是否包含唯一字段（用于验证）
 */
function findUniqueField(entityFile) {
  if (!entityFile || !fs.existsSync(entityFile)) {
    return null;
  }
  
  const content = fs.readFileSync(entityFile, 'utf-8');
  
  // 查找带有唯一索引的字段
  const uniqueIndexMatch = content.match(/SugarIndex\("ix_\w+_(\w+)",\s*nameof\((\w+)\),.*true\)/);
  if (uniqueIndexMatch) {
    return uniqueIndexMatch[2];
  }
  
  // 查找常见的唯一字段名
  const commonFields = ['Code', 'No', 'Number'];
  for (const field of commonFields) {
    const regex = new RegExp(`public\\s+string\\s+\\w+${field}\\s*{`, 'g');
    if (regex.test(content)) {
      const match = content.match(new RegExp(`public\\s+string\\s+(\\w+${field})`));
      if (match) return match[1];
    }
  }
  
  return null;
}

/**
 * 判断实体是否包含状态字段
 */
function hasStatusField(entityFile, entityName) {
  if (!entityFile || !fs.existsSync(entityFile)) {
    return false;
  }
  
  const content = fs.readFileSync(entityFile, 'utf-8');
  const shortName = entityName.replace(/^Takt/, '');
  
  // 检查是否有 {ShortName}Status 字段
  const statusRegex = new RegExp(`public\\s+int\\s+${shortName}Status\\s+{`);
  return statusRegex.test(content);
}

/**
 * 从实体文件中提取所有字段
 */
function extractAllFields(entityFile) {
  if (!entityFile || !fs.existsSync(entityFile)) {
    return { strings: [], ints: [], hasIsDeleted: false };
  }
  
  const content = fs.readFileSync(entityFile, 'utf-8');
  const result = {
    strings: [],
    ints: [],
    hasIsDeleted: false
  };
  
  // 匹配所有 string 类型的字段
  const stringRegex = /public\s+string\s+(\w+)\s*{\s*get;\s*set;\s*}/g;
  let match;
  while ((match = stringRegex.exec(content)) !== null) {
    result.strings.push(match[1]);
    if (match[1] === 'IsDeleted') {
      result.hasIsDeleted = true;
    }
  }
  
  // 匹配所有 int 类型的字段
  const intRegex = /public\s+int\s+(\w+)\s*{\s*get;\s*set;\s*}/g;
  while ((match = intRegex.exec(content)) !== null) {
    if (match[1] === 'IsDeleted') {
      result.hasIsDeleted = true;
    } else {
      result.ints.push(match[1]);
    }
  }
  
  return result;
}

/**
 * 查找主子表的子表实体
 * 从实体文件的 [Navigate(NavigateType.OneToMany, ...)] 属性中提取子表信息
 * 同时从 DTO 文件中读取对应的导航属性名称
 */
function findChildEntities(parentEntityName, parentEntityFile) {
  if (!parentEntityFile || !fs.existsSync(parentEntityFile)) {
    return [];
  }
  
  const content = fs.readFileSync(parentEntityFile, 'utf-8');
  const childEntities = [];
  const processedEntities = new Set(); // 防止重复添加
  
  // 匹配 [Navigate(NavigateType.OneToMany, nameof(ChildEntity.ForeignKey))]
  // 或 [Navigate(NavigateType.OneToMany, nameof(ChildEntities))]
  // 提取 ChildEntity 部分和导航属性名
  // 注意：[Navigate] 和 public List 之间可能有 XML 注释，使用 [\s\S]*? 匹配
  const navigateRegex = /\[Navigate\(NavigateType\.OneToMany,\s*nameof\((\w+)\.(\w+)\)\)\][\s\S]*?public\s+List<[^>]+>\??\s+(\w+)\s*\{/g;
  let match;
  
  while ((match = navigateRegex.exec(content)) !== null) {
    const childEntityName = match[1];
    const foreignKey = match[2];
    const navPropertyName = match[3]; // 导航属性名（如 Executions, Items, ChangeLogs）
    
    // 排除自身和已处理的实体
    if (childEntityName === parentEntityName || processedEntities.has(childEntityName)) continue;
    processedEntities.add(childEntityName);
    
    // 查找子表实体文件
    const childEntityFile = findEntityFile(childEntityName);
    if (childEntityFile) {
      const childEntityShortName = childEntityName.replace(/^Takt/, '');
      const varName = childEntityShortName.charAt(0).toLowerCase() + childEntityShortName.slice(1);
      
      // 从 DTO 文件中查找对应的导航属性名
      const dtoFile = findDtoFile(parentEntityName.replace(/^Takt/, ''));
      let dtoNavPropertyName = navPropertyName; // 默认使用实体中的名称
      
      if (dtoFile && fs.existsSync(dtoFile)) {
        const dtoContent = fs.readFileSync(dtoFile, 'utf-8');
        // 查找 List<TaktXxxDto> 类型的属性
        const childDtoName = `${childEntityName}Dto`;
        const dtoNavRegex = new RegExp(`public\\s+List<${childDtoName}>\\?\\s+(\\w+)\\s*\\{`, 'g');
        const dtoNavMatch = dtoNavRegex.exec(dtoContent);
        if (dtoNavMatch) {
          dtoNavPropertyName = dtoNavMatch[1];
        }
      }
      
      childEntities.push({
        entityName: childEntityName,
        entityShortName: childEntityShortName,
        varName: varName,
        entityFile: childEntityFile,
        foreignKey: foreignKey,
        navPropertyName: navPropertyName, // 实体中的导航属性名
        dtoNavPropertyName: dtoNavPropertyName // DTO 中的导航属性名
      });
    }
  }
  
  return childEntities;
}

/**
 * 获取实体的名称字段名（优先 Name 后缀，否则取第一个非标准 string 字段）
 */
function getNameFieldName(entityFile) {
  const { strings } = extractAllFields(entityFile);
  // 标准字段（需要排除）
  const standardFields = ['Id', 'ParentId', 'CreatedBy', 'UpdatedBy', 'TenantId', 'ConfigId', 'Remark', 'ExtFieldJson', 'IsDeleted'];
  // 优先找包含 Name 后缀的字段
  const nameField = strings.find(f => f.endsWith('Name'));
  if (nameField && !standardFields.includes(nameField)) return nameField;
  // 找不到则返回第一个非标准的 string 字段
  const nonStandard = strings.find(f => !standardFields.includes(f));
  if (nonStandard) return nonStandard;
  // 最后返回第一个 string 字段
  return strings[0] || 'Id';
}

/**
 * 获取实体的状态字段名（必须同时满足：字段名以Status结尾 + ColumnDescription包含"状态"）
 */
function getStatusFieldName(entityFile) {
  if (!entityFile || !fs.existsSync(entityFile)) {
    return null;
  }
  
  const content = fs.readFileSync(entityFile, 'utf-8');
  
  // 匹配所有 int 类型的字段及其 SugarColumn 特性
  // 关键：ColumnDescription 和字段声明之间只能有 XML 注释和空白，不能有其他字段
  // 例如：
  // [SugarColumn(ColumnName = "language_status", ColumnDescription = "语言状态", ...)]
  // /// <summary>
  // /// 员工状态
  // /// </summary>
  // public int LanguageStatus { get; set; }
  const intFieldRegex = /\[SugarColumn\([^\]]*ColumnDescription\s*=\s*"([^"]*)"[^\]]*\](?:\s*\/\/\/[^\n]*)*\s*public\s+int\s+(\w+)\s*\{\s*get;\s*set;\s*\}/g;
  let match;
  
  while ((match = intFieldRegex.exec(content)) !== null) {
    const description = match[1];
    const fieldName = match[2];
    
    // 必须同时满足：字段名以Status结尾 + ColumnDescription包含"状态"
    if (fieldName.endsWith('Status') && description.includes('状态')) {
      return fieldName;
    }
  }
  
  return null;
}

/**
 * 获取实体的软删除字段名
 */
function getIsDeletedFieldName(entityFile) {
  const { hasIsDeleted } = extractAllFields(entityFile);
  return hasIsDeleted ? 'IsDeleted' : null;
}

/**
 * 判断实体是否有 SortOrder 字段
 */
function hasSortOrderField(entityFile) {
  if (!entityFile || !fs.existsSync(entityFile)) {
    return false;
  }
  const content = fs.readFileSync(entityFile, 'utf-8');
  return /public\s+int\s+SortOrder\s*{\s*get;\s*set;\s*}/.test(content);
}

/**
 * 获取实体的编码字段名（用于 DictValue）
 */
function getCodeFieldName(entityFile) {
  const { strings } = extractAllFields(entityFile);
  // 找第一个包含 Code 后缀的 string 字段
  const codeField = strings.find(f => f.endsWith('Code'));
  return codeField || 'Id';
}

/**
 * 生成服务实现文件内容
 */
function generateServiceImpl(entityName, methods, description, entityFile) {
  const entityShortName = entityName.replace(/^Takt/, '');
  const namespace = getServiceNamespace(entityName);
  const dtoNamespace = getDtoNamespace(entityName);
  const crudType = identifyCrudTypeFromEntity(entityFile);
  const uniqueField = findUniqueField(entityFile);
  const hasStatus = hasStatusField(entityFile, entityName);
  const statusField = getStatusFieldName(entityFile); // 可能为 null
  const nameField = getNameFieldName(entityFile);
  const isDeletedField = getIsDeletedFieldName(entityFile);
  const codeField = getCodeFieldName(entityFile);
  const hasSortOrder = hasSortOrderField(entityFile);
  
  // 对于主子表，需要查找子表实体
  let childEntities = [];
  if (crudType === 'MasterDetail') {
    childEntities = findChildEntities(entityName, entityFile);
  }
  
  let content = '';
  
  // 文件头注释
  content += `// ========================================\n`;
  content += `// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)\n`;
  content += `// 命名空间：${namespace}\n`;
  content += `// 文件名称：${entityName}Service.cs\n`;
  content += `// 创建时间：${new Date().toISOString().split('T')[0]}\n`;
  content += `// 创建人：Takt365(Cursor AI)\n`;
  content += `// 功能描述：${description || entityName}应用服务，提供${entityShortName}管理的业务逻辑\n`;
  content += `//\n`;
  content += `// 版权信息：Copyright (c) ${new Date().getFullYear()} Takt  All rights reserved.\n`;
  content += `// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。\n`;
  content += `// ========================================\n\n`;
  
  // using语句
  content += `using SqlSugar;\n`;
  content += `using ${dtoNamespace};\n`;
  content += `using Takt.Application.Services;\n`;
  content += `using ${entityFile ? getEntityNamespace(entityFile) : 'Takt.Domain.Entities'};\n`;
  content += `using Takt.Domain.Interfaces;\n`;
  content += `using Takt.Domain.Repositories;\n`;
  content += `using Takt.Domain.Validation;\n`;
  content += `using Takt.Shared.Exceptions;\n`;
  content += `using Takt.Shared.Helpers;\n`;
  content += `using Takt.Shared.Models;\n\n`;
  content += `namespace ${namespace};\n\n`;
  
  // 类定义
  content += `/// <summary>\n`;
  content += `/// ${description || entityName}应用服务\n`;
  content += `/// </summary>\n`;
  content += `public class ${entityName}Service : TaktServiceBase, I${entityName}Service\n`;
  content += `{\n`;
  content += `    private readonly ITaktRepository<${entityName}> _repository;\n`;
  
  // 主子表：添加子表仓储
  if (crudType === 'MasterDetail' && childEntities.length > 0) {
    for (const child of childEntities) {
      content += `    private readonly ITaktRepository<${child.entityName}> _${child.varName}Repository;\n`;
    }
  }
  content += `\n`;
  
  // 构造函数
  content += `    /// <summary>\n`;
  content += `    /// 构造函数\n`;
  content += `    /// </summary>\n`;
  content += `    /// <param name="repository">${entityShortName}仓储</param>\n`;
  
  // 主子表：添加子表仓储参数
  if (crudType === 'MasterDetail' && childEntities.length > 0) {
    for (const child of childEntities) {
      content += `    /// <param name="${child.varName}Repository">${child.entityShortName}仓储</param>\n`;
    }
  }
  
  content += `    /// <param name="userContext">用户上下文（可选）</param>\n`;
  content += `    /// <param name="tenantContext">租户上下文（可选）</param>\n`;
  content += `    /// <param name="localizer">本地化器（可选）</param>\n`;
  content += `    public ${entityName}Service(\n`;
  content += `        ITaktRepository<${entityName}> repository,\n`;
  
  // 主子表：添加子表仓储注入
  if (crudType === 'MasterDetail' && childEntities.length > 0) {
    for (let i = 0; i < childEntities.length; i++) {
      const child = childEntities[i];
      const isLast = i === childEntities.length - 1;
      content += `        ITaktRepository<${child.entityName}> ${child.varName}Repository,\n`;
    }
  }
  
  content += `        ITaktUserContext? userContext = null,\n`;
  content += `        ITaktTenantContext? tenantContext = null,\n`;
  content += `        ITaktLocalizer? localizer = null)\n`;
  content += `        : base(userContext, tenantContext, localizer)\n`;
  content += `    {\n`;
  content += `        _repository = repository;\n`;
  
  // 主子表：赋值子表仓储
  if (crudType === 'MasterDetail' && childEntities.length > 0) {
    for (const child of childEntities) {
      content += `        _${child.varName}Repository = ${child.varName}Repository;\n`;
    }
  }
  
  content += `    }\n\n`;
  
  // 生成各个公共方法实现
  for (const method of methods) {
    content += generateMethodImplementation(method, entityName, entityShortName, description, uniqueField, hasStatus, statusField, nameField, isDeletedField, codeField, hasSortOrder, crudType, childEntities);
  }
  
  // 只有当接口中包含 GetListAsync 或 ExportAsync 方法时，才生成 QueryExpression 辅助方法
  const needsQueryExpression = methods.some(m => 
    m.methodName === 'GetListAsync' || 
    m.methodName === `Get${entityShortName}ListAsync` ||
    m.methodName === 'ExportAsync' || 
    m.methodName === `Export${entityShortName}Async`
  );
  
  if (needsQueryExpression) {
    content += `\n`;
    
    // 查找 QueryDto 文件路径
    const queryDtoFile = findQueryDtoFile(entityName);
    content += generateQueryExpressionMethod(entityName, entityShortName, description, queryDtoFile);
  }
  
  content += `}\n`;
  
  return content;
}

/**
 * 获取实体命名空间
 */
function getEntityNamespace(entityFile) {
  const content = fs.readFileSync(entityFile, 'utf-8');
  const nsMatch = content.match(/namespace\s+([\w.]+);/);
  return nsMatch ? nsMatch[1] : 'Takt.Domain.Entities';
}

/**
 * 生成单个方法的实现
 */
function generateMethodImplementation(method, entityName, entityShortName, description, uniqueField, hasStatus, statusField, nameField, isDeletedField, codeField, hasSortOrder, crudType, childEntities) {
  const { methodName, returnType, parameters } = method;
  
  let content = '';
  
  // 根据方法名生成不同的实现
  if (methodName === 'GetListAsync' || methodName === `Get${entityShortName}ListAsync`) {
    content = generateListMethod(method, entityName, entityShortName, description);
  } else if (methodName === 'GetByIdAsync' || methodName === `Get${entityShortName}ByIdAsync`) {
    content = generateGetByIdMethod(method, entityName, entityShortName, description, crudType, childEntities);
  } else if (methodName === 'GetOptionsAsync' || methodName === `Get${entityShortName}OptionsAsync`) {
    content = generateOptionsMethod(method, entityName, entityShortName, description, statusField, nameField, isDeletedField, codeField, hasSortOrder);
  } else if (methodName === `Get${entityShortName}TreeOptionsAsync`) {
    content = generateTreeOptionsMethod(method, entityName, entityShortName, description, statusField, nameField, isDeletedField, codeField, hasSortOrder);
  } else if (methodName === `Get${entityShortName}TreeAsync`) {
    content = generateTreeMethod(method, entityName, entityShortName, description, statusField, hasSortOrder, codeField);
  } else if (methodName === `Get${entityShortName}ChildrenAsync`) {
    content = generateTreeChildrenMethod(method, entityName, entityShortName, description, statusField, hasSortOrder, codeField);
  } else if (methodName === 'CreateAsync' || methodName === `Create${entityShortName}Async`) {
    content = generateCreateMethod(method, entityName, entityShortName, description, uniqueField, crudType, childEntities);
  } else if (methodName === 'UpdateAsync' || methodName === `Update${entityShortName}Async`) {
    content = generateUpdateMethod(method, entityName, entityShortName, description, uniqueField, crudType, childEntities);
  } else if (methodName === 'DeleteByIdAsync' || methodName === `Delete${entityShortName}ByIdAsync`) {
    content = generateDeleteByIdMethod(method, entityName, entityShortName, description, crudType, childEntities, statusField);
  } else if (methodName === 'DeleteBatchAsync' || methodName === `Delete${entityShortName}BatchAsync`) {
    content = generateDeleteBatchMethod(method, entityName, entityShortName, description, crudType, childEntities, statusField);
  } else if (methodName.match(/^Update.*StatusAsync$/)) {
    // 匹配所有 UpdateXxxStatusAsync 方法
    // 查找 DTO 文件
    const dtoFile = findDtoFile(entityName);
    content = generateUpdateStatusMethod(method, entityName, entityShortName, description, dtoFile, methodName);
  } else if (methodName === 'UpdateSortAsync' || methodName === `Update${entityShortName}SortAsync`) {
    content = generateUpdateSortMethod(method, entityName, entityShortName, description);
  } else if (methodName === 'GetTemplateAsync' || methodName === `Get${entityShortName}TemplateAsync`) {
    content = generateTemplateMethod(method, entityName, entityShortName, description);
  } else if (methodName === 'ImportAsync' || methodName === `Import${entityShortName}Async`) {
    content = generateImportMethod(method, entityName, entityShortName, description, uniqueField);
  } else if (methodName === 'ExportAsync' || methodName === `Export${entityShortName}Async`) {
    content = generateExportMethod(method, entityName, entityShortName, description);
  }
  
  return content;
}

/**
 * 生成列表查询方法
 */
function generateListMethod(method, entityName, entityShortName, description) {
  const paramMatch = method.parameters.match(/(\w+)\s+(\w+)/);
  const paramType = paramMatch ? paramMatch[1] : `${entityName}QueryDto`;
  const paramName = paramMatch ? paramMatch[2] : 'queryDto';
  
  return `
    /// <summary>
    /// 获取${description}(${entityShortName})列表（分页）
    /// </summary>
    /// <param name="${paramName}">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<${entityName}Dto>> Get${entityShortName}ListAsync(${paramType} ${paramName})
    {
        var predicate = QueryExpression(${paramName});
        var (data, total) = await _repository.GetPagedAsync(${paramName}.PageIndex, ${paramName}.PageSize, predicate);
        return TaktPagedResult<${entityName}Dto>.Create(
            data.Adapt<List<${entityName}Dto>>(),
            total,
            ${paramName}.PageIndex,
            ${paramName}.PageSize
        );
    }

`;
}

/**
 * 生成根据ID获取方法
 */
function generateGetByIdMethod(method, entityName, entityShortName, description, crudType, childEntities) {
  // 主子表：需要手动加载子表
  if (crudType === 'MasterDetail' && childEntities && childEntities.length > 0) {
    let loadChildCode = '\n        var dto = entity.Adapt<' + entityName + 'Dto>();\n        \n';
    loadChildCode += '        // 手动加载子表\n';
    
    for (const child of childEntities) {
      loadChildCode += `        dto.${child.dtoNavPropertyName} = (await _${child.varName}Repository.FindAsync(x => x.${child.foreignKey} == id && x.IsDeleted == 0))\n`;
      loadChildCode += `            .Adapt<List<${child.entityName}Dto>>();\n`;
    }
    
    loadChildCode += '        \n        return dto;';
    
    return `
    /// <summary>
    /// 根据ID获取${description}(${entityShortName})
    /// </summary>
    /// <param name="id">${description}(${entityShortName})ID</param>
    /// <returns>${description}(${entityShortName})DTO</returns>
    public async Task<${entityName}Dto?> Get${entityShortName}ByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;${loadChildCode}
    }

`;
  }
  
  // 普通表或树表
  return `
    /// <summary>
    /// 根据ID获取${description}(${entityShortName})
    /// </summary>
    /// <param name="id">${description}(${entityShortName})ID</param>
    /// <returns>${description}(${entityShortName})DTO</returns>
    public async Task<${entityName}Dto?> Get${entityShortName}ByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<${entityName}Dto>();
    }

`;
}

/**
 * 生成选项列表方法
 */
function generateOptionsMethod(method, entityName, entityShortName, description, statusField, nameField, isDeletedField, codeField, hasSortOrder) {
  const deletedCondition = isDeletedField ? `x.${isDeletedField} == 0` : 'x.IsDeleted == 0';
  const sortOrderLine = hasSortOrder ? `            SortOrder = x.SortOrder,` : '';
  // 状态字段：1=启用，0=禁用（查询启用的记录）
  const whereCondition = statusField 
    ? `${deletedCondition} && x.${statusField} == 1` 
    : deletedCondition;
  
  // DictLabel 使用 Name 字段
  // DictValue 使用 Code 字段，如果没有 Code 字段则使用 Name 字段
  const dictLabelField = `x.${nameField}${nameField === 'Id' ? '.ToString()' : ''}`;
  const dictValueField = codeField && codeField !== 'Id' && codeField !== nameField 
    ? `x.${codeField}` 
    : dictLabelField;
  
  return `
    /// <summary>
    /// 获取${description}(${entityShortName})选项列表（用于下拉框等）
    /// </summary>
    /// <returns>${description}(${entityShortName})选项列表</returns>
    public async Task<List<TaktSelectOption>> Get${entityShortName}OptionsAsync()
    {
        var all = await _repository.FindAsync(x => ${whereCondition});
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = ${dictLabelField} ?? string.Empty,
            DictValue = ${dictValueField}${sortOrderLine ? ',' : ''}
${sortOrderLine}
        })${hasSortOrder ? '.OrderBy(x => x.SortOrder)' : ''}.ToList();
    }

`;
}

/**
 * 生成创建方法
 */
function generateCreateMethod(method, entityName, entityShortName, description, uniqueField, crudType, childEntities) {
  let validationCode = '';
  if (uniqueField) {
    validationCode = `
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.${uniqueField}, dto.${uniqueField}, null, $"${description}编码 {dto.${uniqueField}} 已存在");
`;
  }
  
  // 主子表：创建子表数据
  let createChildCode = '';
  if (crudType === 'MasterDetail' && childEntities && childEntities.length > 0) {
    createChildCode = `
        
        // 创建子表数据
        if (entity.Id > 0)
        {
`;
    for (const child of childEntities) {
      createChildCode += `            // 创建${child.entityShortName}列表
`;
      createChildCode += `            if (dto.${child.dtoNavPropertyName} != null && dto.${child.dtoNavPropertyName}.Count > 0)
`;
      createChildCode += `            {
`;
      createChildCode += `                var ${child.varName}List = dto.${child.dtoNavPropertyName}.Select(x => {
`;
      createChildCode += `                    var childEntity = x.Adapt<${child.entityName}>();
`;
      createChildCode += `                    childEntity.${child.foreignKey} = entity.Id;
`;
      createChildCode += `                    return childEntity;
`;
      createChildCode += `                }).ToList();
`;
      createChildCode += `                await _${child.varName}Repository.CreateRangeBulkAsync(${child.varName}List);
`;
      createChildCode += `            }
`;
    }
    createChildCode += `        }
`;
  }
  
  return `
    /// <summary>
    /// 创建${description}(${entityShortName})
    /// </summary>
    /// <param name="dto">创建${description}(${entityShortName})DTO</param>
    /// <returns>${description}(${entityShortName})DTO</returns>
    public async Task<${entityName}Dto> Create${entityShortName}Async(${entityName}CreateDto dto)
    {${validationCode}
        var entity = dto.Adapt<${entityName}>();
        entity = await _repository.CreateAsync(entity);${createChildCode}
        return (await Get${entityShortName}ByIdAsync(entity.Id)) ?? entity.Adapt<${entityName}Dto>();
    }

`;
}

/**
 * 生成更新方法
 */
function generateUpdateMethod(method, entityName, entityShortName, description, uniqueField, crudType, childEntities) {
  let validationCode = '';
  if (uniqueField) {
    validationCode = `
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.${uniqueField}, dto.${uniqueField}, id, $"${description}编码 {dto.${uniqueField}} 已存在");
`;
  }
  
  // 主子表：更新子表数据（删旧建新）
  let updateChildCode = '';
  if (crudType === 'MasterDetail' && childEntities && childEntities.length > 0) {
    updateChildCode = `
        
        // 更新子表数据（删旧建新）
`;
    for (const child of childEntities) {
      updateChildCode += `        // 删除旧的${child.entityShortName}列表
`;
      updateChildCode += `        var old${child.entityShortName}s = await _${child.varName}Repository.FindAsync(x => x.${child.foreignKey} == id && x.IsDeleted == 0);
`;
      updateChildCode += `        if (old${child.entityShortName}s != null && old${child.entityShortName}s.Count > 0)
`;
      updateChildCode += `        {
`;
      updateChildCode += `            foreach (var old${child.entityShortName} in old${child.entityShortName}s)
`;
      updateChildCode += `            {
`;
      updateChildCode += `                old${child.entityShortName}.IsDeleted = 1;
`;
      updateChildCode += `            }
`;
      updateChildCode += `            await _${child.varName}Repository.UpdateRangeBulkAsync(old${child.entityShortName}s);
`;
      updateChildCode += `        }
`;
      updateChildCode += `
`;
      updateChildCode += `        // 创建新的${child.entityShortName}列表
`;
      updateChildCode += `        if (dto.${child.dtoNavPropertyName} != null && dto.${child.dtoNavPropertyName}.Count > 0)
`;
      updateChildCode += `        {
`;
      updateChildCode += `            var ${child.varName}List = dto.${child.dtoNavPropertyName}.Select(x => {
`;
      updateChildCode += `                var childEntity = x.Adapt<${child.entityName}>();
`;
      updateChildCode += `                childEntity.${child.foreignKey} = id;
`;
      updateChildCode += `                return childEntity;
`;
      updateChildCode += `            }).ToList();
`;
      updateChildCode += `            await _${child.varName}Repository.CreateRangeBulkAsync(${child.varName}List);
`;
      updateChildCode += `        }
`;
    }
  }
  
  return `
    /// <summary>
    /// 更新${description}(${entityShortName})
    /// </summary>
    /// <param name="id">${description}(${entityShortName})ID</param>
    /// <param name="dto">更新${description}(${entityShortName})DTO</param>
    /// <returns>${description}(${entityShortName})DTO</returns>
    public async Task<${entityName}Dto> Update${entityShortName}Async(long id, ${entityName}UpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.${entityShortName.toLowerCase()}NotFound");
${validationCode}
        dto.Adapt(entity, typeof(${entityName}UpdateDto), typeof(${entityName}));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);${updateChildCode}

        return (await Get${entityShortName}ByIdAsync(id)) ?? entity.Adapt<${entityName}Dto>();
    }

`;
}

/**
 * 生成删除方法
 */
function generateDeleteByIdMethod(method, entityName, entityShortName, description, crudType, childEntities, statusField) {
  // 树表：检查子节点
  let treeCheckCode = '';
  if (crudType === 'Tree') {
    treeCheckCode = `
        
        // 树表：检查是否有子节点
        var hasChildren = await _repository.ExistsAsync(x => x.ParentId == id && x.IsDeleted == 0);
        if (hasChildren)
        {
            throw new TaktBusinessException("validation.cannotDeleteWithChildren", "存在子节点，无法删除");
        }
`;
  }
  
  // 主子表：级联删除子表
  let cascadeDeleteCode = '';
  if (crudType === 'MasterDetail' && childEntities && childEntities.length > 0) {
    cascadeDeleteCode = `
        
        // 级联删除子表数据
`;
    for (const child of childEntities) {
      cascadeDeleteCode += `        // 级联删除${child.entityShortName}列表
`;
      cascadeDeleteCode += `        var ${child.varName}s = await _${child.varName}Repository.FindAsync(x => x.${child.foreignKey} == id && x.IsDeleted == 0);
`;
      cascadeDeleteCode += `        if (${child.varName}s != null && ${child.varName}s.Count > 0)
`;
      cascadeDeleteCode += `        {
`;
      cascadeDeleteCode += `            foreach (var ${child.varName} in ${child.varName}s)
`;
      cascadeDeleteCode += `            {
`;
      cascadeDeleteCode += `                ${child.varName}.IsDeleted = 1;
`;
      cascadeDeleteCode += `            }
`;
      cascadeDeleteCode += `            await _${child.varName}Repository.UpdateRangeBulkAsync(${child.varName}s);
`;
      cascadeDeleteCode += `        }
`;
    }
  }
  
  return `
    /// <summary>
    /// 删除${description}(${entityShortName})
    /// </summary>
    /// <param name="id">${description}(${entityShortName})ID</param>
    /// <returns>任务</returns>
    public async Task Delete${entityShortName}ByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.${entityShortName.toLowerCase()}NotFound");${treeCheckCode}${cascadeDeleteCode}
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;
${statusField ? `
        // 同步更新状态字段为禁用状态（1）
        entity.${statusField} = 1;
` : ''}
        await _repository.UpdateAsync(entity);
    }

`;
}

/**
 * 生成批量删除方法
 */
function generateDeleteBatchMethod(method, entityName, entityShortName, description, crudType, childEntities, statusField) {
  // 树表：批量检查子节点
  let treeBatchCheckCode = '';
  if (crudType === 'Tree') {
    treeBatchCheckCode = `        
        // 树表：检查是否有子节点
        foreach (var id in idList)
        {
            var hasChildren = await _repository.ExistsAsync(x => x.ParentId == id && x.IsDeleted == 0);
            if (hasChildren)
            {
                throw new TaktBusinessException("validation.cannotDeleteWithChildren", $"存在子节点，无法删除ID {id}");
            }
        }
        
`;
  }
  
  // 主子表：批量级联删除子表
  let cascadeBatchDeleteCode = '';
  if (crudType === 'MasterDetail' && childEntities && childEntities.length > 0) {
    cascadeBatchDeleteCode = `        
        // 批量级联删除子表数据
`;
    for (const child of childEntities) {
      cascadeBatchDeleteCode += `        // 批量级联删除${child.entityShortName}列表
`;
      cascadeBatchDeleteCode += `        var ${child.varName}sToDelete = new List<${child.entityName}>();
`;
      cascadeBatchDeleteCode += `        foreach (var id in idList)
`;
      cascadeBatchDeleteCode += `        {
`;
      cascadeBatchDeleteCode += `            var ${child.varName}s = await _${child.varName}Repository.FindAsync(x => x.${child.foreignKey} == id && x.IsDeleted == 0);
`;
      cascadeBatchDeleteCode += `            if (${child.varName}s != null && ${child.varName}s.Count > 0)
`;
      cascadeBatchDeleteCode += `            {
`;
      cascadeBatchDeleteCode += `                ${child.varName}sToDelete.AddRange(${child.varName}s);
`;
      cascadeBatchDeleteCode += `            }
`;
      cascadeBatchDeleteCode += `        }
`;
      cascadeBatchDeleteCode += `        
`;
      cascadeBatchDeleteCode += `        if (${child.varName}sToDelete.Count > 0)
`;
      cascadeBatchDeleteCode += `        {
`;
      cascadeBatchDeleteCode += `            foreach (var ${child.varName} in ${child.varName}sToDelete)
`;
      cascadeBatchDeleteCode += `            {
`;
      cascadeBatchDeleteCode += `                ${child.varName}.IsDeleted = 1;
`;
      cascadeBatchDeleteCode += `            }
`;
      cascadeBatchDeleteCode += `            await _${child.varName}Repository.UpdateRangeBulkAsync(${child.varName}sToDelete);
`;
      cascadeBatchDeleteCode += `        }
`;
    }
  }
  
  return `
    /// <summary>
    /// 批量删除${description}(${entityShortName})
    /// </summary>
    /// <param name="ids">${description}(${entityShortName})ID列表</param>
    /// <returns>任务</returns>
    public async Task Delete${entityShortName}BatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;${treeBatchCheckCode}
        // 获取所有要删除的实体
        var entities = new List<${entityName}>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;${cascadeBatchDeleteCode}
        
        // 批量更新：设置 IsDeleted = 1${statusField ? `，并同步更新 ${statusField} = 1（禁用）` : ''}
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
${statusField ? `            entity.${statusField} = 1;
` : ''}        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }

`;
}

/**
 * 生成状态更新方法
 */
function generateUpdateStatusMethod(method, entityName, entityShortName, description, dtoFile, methodName) {
  // 从接口方法签名中提取实际的 StatusDto 类型
  let statusDtoType = `${entityName}StatusDto`; // 默认值
  let statusFieldName = entityShortName + 'Status'; // 默认值
  
  if (method && method.parameters) {
    // 匹配参数类型：如 "TaktEmployeeEmployeeStatusDto dto" 中的 "TaktEmployeeEmployeeStatusDto"
    const typeMatch = method.parameters.match(/(\w+StatusDto)\s+\w+/);
    if (typeMatch) {
      statusDtoType = typeMatch[1];
      
      // 从 DTO 文件中读取 StatusDto 类的实际字段名
      if (dtoFile && fs.existsSync(dtoFile)) {
        const dtoContent = fs.readFileSync(dtoFile, 'utf-8');
        // 匹配 StatusDto 类中的 int 类型字段（除了 Id 字段）
        const statusDtoClassRegex = new RegExp(`class\\s+${statusDtoType}\\s*[:{]`, 'g');
        const classMatch = statusDtoClassRegex.exec(dtoContent);
        
        if (classMatch) {
          // 从类定义开始查找 int 类型的字段
          const classStart = classMatch.index;
          let braceCount = 0;
          let inClass = false;
          let classEnd = dtoContent.length;
          
          for (let i = classStart; i < dtoContent.length; i++) {
            if (dtoContent[i] === '{') {
              braceCount++;
              inClass = true;
            } else if (dtoContent[i] === '}') {
              braceCount--;
              if (inClass && braceCount === 0) {
                classEnd = i;
                break;
              }
            }
          }
          
          // 在类范围内查找 int 字段
          const classContent = dtoContent.substring(classStart, classEnd);
          const cleanClassContent = classContent.replace(/\[[^\]]*\]\s*\n\s*/g, '');
          const intFieldRegex = /public\s+int\s+(\w+)\s*\{\s*get;\s*set;\s*\}/g;
          intFieldRegex.lastIndex = classStart;
          let fieldMatch;
          
          while ((fieldMatch = intFieldRegex.exec(dtoContent)) !== null) {
            if (fieldMatch.index > classEnd) break;
            const fieldName = fieldMatch[1];
            // 排除 Id 字段
            if (!fieldName.endsWith('Id')) {
              statusFieldName = fieldName;
              break;
            }
          }
        }
      }
    }
  }
  
  return `
    /// <summary>
    /// 更新${description}(${entityShortName})状态
    /// </summary>
    /// <param name="dto">${description}(${entityShortName})状态DTO</param>
    /// <returns>${description}(${entityShortName})DTO</returns>
    public async Task<${entityName}Dto> ${methodName}(${statusDtoType} dto)
    {
        var entity = await _repository.GetByIdAsync(dto.${entityShortName}Id);
        if (entity == null)
            throw new TaktBusinessException("validation.${entityShortName.toLowerCase()}NotFound");
        entity.${statusFieldName} = dto.${statusFieldName};
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await Get${entityShortName}ByIdAsync(entity.Id) ?? entity.Adapt<${entityName}Dto>();
    }

`;
}

/**
 * 生成排序更新方法
 */
function generateUpdateSortMethod(method, entityName, entityShortName, description) {
  // SortDto 统一使用 {entityName}SortDto 命名
  const sortDtoType = `${entityName}SortDto`;
  
  return `
    /// <summary>
    /// 更新${description}(${entityShortName})排序
    /// </summary>
    /// <param name="dto">${description}(${entityShortName})排序DTO</param>
    /// <returns>${description}(${entityShortName})DTO</returns>
    public async Task<${entityName}Dto> Update${entityShortName}SortAsync(${sortDtoType} dto)
    {
        var entity = await _repository.GetByIdAsync(dto.${entityShortName}Id);
        if (entity == null)
            throw new TaktBusinessException("validation.${entityShortName.toLowerCase()}NotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await Get${entityShortName}ByIdAsync(entity.Id) ?? entity.Adapt<${entityName}Dto>();
    }

`;
}

/**
 * 生成模板方法
 */
function generateTemplateMethod(method, entityName, entityShortName, description) {
  return `
    /// <summary>
    /// 获取${description}(${entityShortName})导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> Get${entityShortName}TemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(${entityName}));
        return await TaktExcelHelper.GenerateTemplateAsync<${entityName}TemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }

`;
}

/**
 * 生成导入方法
 */
function generateImportMethod(method, entityName, entityShortName, description, uniqueField) {
  return `
    /// <summary>
    /// 导入${description}(${entityShortName})
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> Import${entityShortName}Async(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(${entityName}));
        var importData = await TaktExcelHelper.ImportAsync<${entityName}ImportDto>(fileStream, excelSheet);
        
        var successCount = 0;
        var failCount = 0;
        var errors = new List<string>();
        var rowIndex = 0;

        foreach (var item in importData)
        {
            rowIndex++;
            try
            {
                // TODO: 添加必要的验证逻辑
                var entity = item.Adapt<${entityName}>();
                await _repository.CreateAsync(entity);
                successCount++;
            }
            catch (Exception ex)
            {
                errors.Add($"行{rowIndex}: {ex.Message}");
                failCount++;
            }
        }

        return (successCount, failCount, errors);
    }

`;
}

/**
 * 生成导出方法
 */
function generateExportMethod(method, entityName, entityShortName, description) {
  return `
    /// <summary>
    /// 导出${description}(${entityShortName})
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> Export${entityShortName}Async(${entityName}QueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new ${entityName}QueryDto());
        List<${entityName}> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(${entityName}));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<${entityName}ExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<${entityName}ExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }

`;
}

/**
 * 生成树形选项列表方法
 */
function generateTreeOptionsMethod(method, entityName, entityShortName, description, statusField, nameField, isDeletedField, codeField, hasSortOrder) {
  const deletedCondition = isDeletedField ? `x.${isDeletedField} == 0` : 'x.IsDeleted == 0';
  const sortOrderLine = hasSortOrder ? `                SortOrder = item.SortOrder,` : '';
  // 状态字段：1=启用，0=禁用（查询启用的记录）
  const whereCondition = statusField 
    ? `${deletedCondition} && x.${statusField} == 1` 
    : deletedCondition;
  return `
    /// <summary>
    /// 获取${description}(${entityShortName})树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>树形选项列表</returns>
    public async Task<List<TaktTreeSelectOption>> Get${entityShortName}TreeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => ${whereCondition});
        return BuildTreeOptions(all.ToList(), 0);
    }

    /// <summary>
    /// 构建树形选项列表（递归）
    /// </summary>
    private List<TaktTreeSelectOption> BuildTreeOptions(List<${entityName}> all, long parentId)
    {
        var result = new List<TaktTreeSelectOption>();
        var children = all.Where(x => x.ParentId == parentId)${hasSortOrder ? '.OrderBy(x => x.SortOrder)' : ''};
        
        foreach (var item in children)
        {
            var option = new TaktTreeSelectOption
            {
                DictValue = item.${codeField},
                DictLabel = item.${nameField}${nameField === 'Id' ? '.ToString()' : ''} ?? string.Empty${sortOrderLine ? ',' : ''}
${sortOrderLine}
            };
            var childOptions = BuildTreeOptions(all, item.Id);
            if (childOptions.Count > 0)
            {
                option.Children = childOptions;
            }
            result.Add(option);
        }
        
        return result;
    }

`;
}

/**
 * 生成树形列表方法（优化版：一次查询+内存构建树，避免N+1查询问题）
 */
function generateTreeMethod(method, entityName, entityShortName, description, statusField, hasSortOrder, codeField) {
  // 状态字段：1=启用，0=禁用
  const statusCondition = statusField 
    ? `x.${statusField} == 1` 
    : 'true';
  return `
    /// <summary>
    /// 获取${description}(${entityShortName})树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的${description || entityShortName}</param>
    /// <returns>树形列表</returns>
    public async Task<List<${entityName}TreeDto>> Get${entityShortName}TreeAsync(long parentId = 0, bool includeDisabled = false)
    {
        var startTime = DateTime.Now;
        
        // 优化：一次查询所有数据，然后在内存中构建树，避免 N+1 查询问题
        var allRecords = await _repository.GetAllAsync();
        
        // 过滤条件
        var filteredRecords = includeDisabled
            ? allRecords
            : allRecords.Where(x => ${statusCondition});
        
        var buildTreeStart = DateTime.Now;
        
        // 在内存中构建树形结构
        var treeList = Build${entityShortName}Tree(filteredRecords.ToList(), parentId);
        
        var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
        var buildElapsed = (DateTime.Now - buildTreeStart).TotalMilliseconds;
        
        TaktLogger.Information("[性能] ${entityShortName}树构建完成 - 总耗时: {Elapsed}ms, 查询耗时: {QueryElapsed}ms, 构建树耗时: {BuildElapsed}ms, 总数: {TotalCount}, 过滤后: {FilteredCount}, 树节点数: {TreeCount}",
            elapsed,
            (buildTreeStart - startTime).TotalMilliseconds,
            buildElapsed,
            allRecords.Count,
            filteredRecords.Count(),
            treeList.Count);
        
        return treeList;
    }
    
    /// <summary>
    /// 在内存中构建${entityShortName}树（递归）
    /// </summary>
    private List<${entityName}TreeDto> Build${entityShortName}Tree(List<${entityName}> allRecords, long parentId)
    {
        var children = allRecords
            .Where(x => x.ParentId == parentId)
            ${hasSortOrder ? '.OrderBy(x => x.SortOrder)' : ''}
            .ToList();
        
        var treeList = new List<${entityName}TreeDto>();
        
        foreach (var item in children)
        {
            var treeDto = item.Adapt<${entityName}TreeDto>();
            // 递归构建子节点（内存操作，非常快）
            var childTree = Build${entityShortName}Tree(allRecords, item.Id);
            if (childTree.Count > 0)
            {
                treeDto.Children = childTree;
            }
            treeList.Add(treeDto);
        }
        
        return treeList;
    }

`;
}

/**
 * 生成子节点列表方法
 */
function generateTreeChildrenMethod(method, entityName, entityShortName, description, statusField, hasSortOrder, codeField) {
  // 状态字段：1=启用，0=禁用
  const statusCondition = statusField 
    ? `x.${statusField} == 1` 
    : 'true';
  return `
    /// <summary>
    /// 获取${description}(${entityShortName})子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的${description || entityShortName}</param>
    /// <returns>子节点DTO列表</returns>
    public async Task<List<${entityName}Dto>> Get${entityShortName}ChildrenAsync(long parentId, bool includeDisabled = false)
    {
        // 构建查询条件
        Expression<Func<${entityName}, bool>> predicate = includeDisabled
            ? x => x.ParentId == parentId
            : x => x.ParentId == parentId && ${statusCondition};
        
        var children = await _repository.FindAsync(predicate);
                return children${hasSortOrder ? '.OrderBy(x => x.SortOrder)' : ''}.Select(x => x.Adapt<${entityName}Dto>()).ToList();
    }

`;
}

/**
 * 从 QueryDto 文件中提取字段列表
 */
function extractQueryDtoFields(queryDtoFile, entityName) {
  if (!fs.existsSync(queryDtoFile)) return [];
  
  const content = fs.readFileSync(queryDtoFile, 'utf-8');
  const fields = [];
  const fieldNames = new Set(); // 用于去重
  
  // 找到 QueryDto 类的范围
  const queryDtoClassName = `${entityName}QueryDto`;
  const classRegex = new RegExp(`class\\s+${queryDtoClassName}\\s*[:{]`, 'g');
  const classMatch = classRegex.exec(content);
  
  if (!classMatch) return [];
  
  // 从类定义开始查找字段，需要确定类的边界（通过大括号计数）
  let braceCount = 0;
  let inClass = false;
  let classStart = classMatch.index;
  let classEnd = content.length;
  
  // 找到类的结束位置（通过大括号计数）
  for (let i = classStart; i < content.length; i++) {
    if (content[i] === '{') {
      braceCount++;
      inClass = true;
    } else if (content[i] === '}') {
      braceCount--;
      if (inClass && braceCount === 0) {
        classEnd = i;
        break;
      }
    }
  }
  
  // 匹配属性：支持可空类型，如 string?、int?、long?，忽略特性行（如 [JsonConverter]）
  // 先移除多行特性（以 [ 开头到下一个 public 之间的内容）
  const classContent = content.substring(classStart, classEnd);
  const cleanClassContent = classContent.replace(/\[[^\]]*\]\s*\n\s*/g, '');
  const propertyRegex = /public\s+(string|int|long|bool|decimal|double|float|DateTime|Guid)\??\s+(\w+)\s*\{\s*get;\s*set;\s*\}/g;
  let match;
  
  propertyRegex.lastIndex = 0;
  
  while ((match = propertyRegex.exec(cleanClassContent)) !== null) {
    // 如果超出了类的范围，停止搜索
    if (match.index > classEnd) break;
    
    const fieldType = match[1];
    const fieldName = match[2];
    
    // 跳过不需要生成查询条件的字段
    const skipFields = ['KeyWords', 'PageIndex', 'PageSize', 'SortField', 'SortOrder'];
    if (skipFields.includes(fieldName)) continue;
    // 跳过日期范围字段（Start/End），后面会特殊处理
    if (fieldName.endsWith('Start') || fieldName.endsWith('End')) continue;
    
    // 去重：如果字段已经添加过，跳过
    if (fieldNames.has(fieldName)) continue;
    
    fieldNames.add(fieldName);
    fields.push({
      type: fieldType.replace('?', ''), // 移除可空标记
      name: fieldName,
      isNullable: fieldType.includes('?')
    });
  }
  
  return fields;
}

/**
 * 从 QueryDto 文件中提取日期范围字段
 */
function extractDateRangeFields(queryDtoFile) {
  if (!fs.existsSync(queryDtoFile)) return [];
  
  const content = fs.readFileSync(queryDtoFile, 'utf-8');
  const dateRanges = [];
  
  // 匹配所有 DateTime? 类型的字段，忽略特性行
  const cleanContent = content.replace(/\[[^\]]*\]\s*\n\s*/g, '');
  const dateRegex = /public\s+DateTime\?\s+(\w+)\s*\{\s*get;\s*set;\s*\}/g;
  let match;
  const dateFields = [];
  
  while ((match = dateRegex.exec(cleanContent)) !== null) {
    dateFields.push(match[1]);
  }
  
  // 配对 Start/End 字段
  for (const field of dateFields) {
    if (field.endsWith('Start')) {
      const baseName = field.slice(0, -5); // 移除 'Start'
      const endField = baseName + 'End';
      
      if (dateFields.includes(endField)) {
        dateRanges.push({
          baseName: baseName,
          startField: field,
          endField: endField
        });
      }
    }
  }
  
  return dateRanges;
}

/**
 * 生成查询表达式辅助方法（根据 QueryDto 字段自动生成查询条件）
 */
function generateQueryExpressionMethod(entityName, entityShortName, description, queryDtoFile) {
  const fields = extractQueryDtoFields(queryDtoFile, entityName);
  const dateRanges = extractDateRangeFields(queryDtoFile);
  
  let fieldConditions = '';
  
  // 生成 KeyWords 全局关键词查询（搜索所有字符串字段）
  fieldConditions += `        if (!string.IsNullOrEmpty(queryDto?.KeyWords))\n`;
  fieldConditions += `        {\n`;
  
  // 收集所有字符串字段用于关键词搜索
  const stringFields = fields.filter(f => f.type === 'string');
  
  if (stringFields.length > 0) {
    // 生成 OR 条件搜索所有字符串字段（分行格式化）
    fieldConditions += `            exp = exp.And(x =>\n`;
    for (let i = 0; i < stringFields.length; i++) {
      const field = stringFields[i];
      const isLast = i === stringFields.length - 1;
      fieldConditions += `                x.${field.name}!.Contains(queryDto.KeyWords)`;
      if (!isLast) {
        fieldConditions += ` ||`;
      }
      fieldConditions += `\n`;
    }
    fieldConditions += `            );\n`;
  } else {
    // 如果没有字符串字段，才使用 ID
    fieldConditions += `            exp = exp.And(x => x.Id.ToString().Contains(queryDto.KeyWords));\n`;
  }
  
  fieldConditions += `        }\n\n`;
  
  // 为每个字段生成查询条件
  for (const field of fields) {
    const entityFieldName = field.name;
    
    if (field.type === 'string') {
      // 字符串类型：使用 Contains 模糊查询
      fieldConditions += `        if (!string.IsNullOrEmpty(queryDto?.${entityFieldName}))\n`;
      fieldConditions += `        {\n`;
      fieldConditions += `            exp = exp.And(x => x.${entityFieldName}!.Contains(queryDto.${entityFieldName}));\n`;
      fieldConditions += `        }\n\n`;
    } else if (field.type === 'long' || field.type === 'int' || field.type === 'DateTime' || field.type === 'decimal' || field.type === 'bool') {
      // 数值类型、日期类型、布尔类型：精确匹配（使用 HasValue 判断）
      fieldConditions += `        if (queryDto?.${entityFieldName}.HasValue == true)\n`;
      fieldConditions += `        {\n`;
      fieldConditions += `            exp = exp.And(x => x.${entityFieldName} == queryDto.${entityFieldName});\n`;
      fieldConditions += `        }\n\n`;
    }
  }
  
  // 为每个日期范围字段生成查询条件
  for (const dateRange of dateRanges) {
    const entityFieldName = dateRange.baseName;
    fieldConditions += `        // ${entityFieldName} 日期范围查询\n`;
    fieldConditions += `        if (queryDto?.${dateRange.startField}.HasValue == true)\n`;
    fieldConditions += `        {\n`;
    fieldConditions += `            exp = exp.And(x => x.${entityFieldName} >= queryDto.${dateRange.startField});\n`;
    fieldConditions += `        }\n`;
    fieldConditions += `        if (queryDto?.${dateRange.endField}.HasValue == true)\n`;
    fieldConditions += `        {\n`;
    fieldConditions += `            exp = exp.And(x => x.${entityFieldName} <= queryDto.${dateRange.endField});\n`;
    fieldConditions += `        }\n\n`;
  }
  
  return `
    /// <summary>
    /// 构建${description}查询表达式
    /// </summary>
    /// <param name="queryDto">${description}查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<${entityName}, bool>> QueryExpression(${entityName}QueryDto? queryDto)
    {
        var exp = Expressionable.Create<${entityName}>();

${fieldConditions}        return exp.ToExpression();
    }
`;
}

/**
 * 查找服务接口文件
 */
function findInterfaceFiles() {
  const interfaces = [];
  
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
      } else if (file.startsWith('ITakt') && file.endsWith('Service.cs')) {
        interfaces.push(fullPath);
      }
    }
  }
  
  searchDir(SERVICE_INTERFACE_PATH);
  return interfaces;
}

/**
 * 处理单个服务接口
 */
function processInterface(interfaceFile) {
  const entityName = extractEntityName(interfaceFile);
  if (!entityName) {
    console.log(`  ❌ 无法从文件名提取实体名: ${path.basename(interfaceFile)}`);
    return false;
  }
  
  // 排除特殊的DTO文件对应的实体
  if (shouldExcludeDto(entityName)) {
    console.log(`  ⏭️ 跳过排除的实体: ${entityName}`);
    return false;
  }
  
  // 排除 Engine 和 Identity 特殊目录下的所有服务实现
  if (isInExcludedDirectory(interfaceFile)) {
    console.log(`  ⏭️  跳过排除目录服务: ${entityName}（${path.relative(SERVICE_INTERFACE_PATH, interfaceFile)}）`);
    return false;
  }
  
  if (targetEntity && !generateAll && entityName !== targetEntity) {
    return false;
  }
  
  console.log(`\n生成: ${entityName}`);
  console.log('='.repeat(60));
  console.log(`  接口文件: ${path.relative(SERVICE_INTERFACE_PATH, interfaceFile)}`);
  
  // 提取方法
  const methods = extractInterfaceMethods(interfaceFile);
  console.log(`  方法数量: ${methods.length}`);
  
  // 查找实体文件
  const entityFile = findEntityFile(entityName);
  const description = extractEntityDescription(entityFile);
  
  // 生成服务实现
  const content = generateServiceImpl(entityName, methods, description, entityFile);
  
  // 确定输出路径 - 只替换文件名开头的 'I'，不影响目录名
  const relativePath = path.relative(SERVICE_INTERFACE_PATH, interfaceFile);
  const relativeDir = path.dirname(relativePath);
  const interfaceFileName = path.basename(interfaceFile);
  const implFileName = interfaceFileName.startsWith('I') ? interfaceFileName.substring(1) : interfaceFileName;
  const outputFile = path.join(SERVICE_IMPL_PATH, relativeDir, implFileName);
  const outputDir = path.dirname(outputFile);
  
  if (dryRun) {
    console.log(`  🔍 [预览模式] 将生成: ${path.relative(SERVICE_IMPL_PATH, outputFile)}`);
    return true;
  }
  
  // 确保目录存在
  if (!fs.existsSync(outputDir)) {
    fs.mkdirSync(outputDir, { recursive: true });
  }
  
  // 写入文件
  fs.writeFileSync(outputFile, content, 'utf-8');
  console.log(`  ✅ 已保存: ${path.relative(SERVICE_IMPL_PATH, outputFile)}`);
  
  return true;
}

/**
 * 主函数
 */
function main() {
  console.log('========================================');
  console.log('  服务实现生成工具');
  console.log('========================================\n');
  
  // 查找所有服务接口文件
  const interfaceFiles = findInterfaceFiles();
  
  console.log(`找到 ${interfaceFiles.length} 个服务接口文件\n`);
  
  // 处理每个接口文件
  let successCount = 0;
  let failCount = 0;
  let skipCount = 0;
  
  for (const interfaceFile of interfaceFiles) {
    const result = processInterface(interfaceFile);
    if (result === true) {
      successCount++;
    } else if (result === false) {
      skipCount++;
    } else {
      failCount++;
    }
  }
  
  console.log('\n========================================');
  console.log(`  完成！已生成: ${successCount}, 跳过: ${skipCount}, 失败: ${failCount}`);
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
