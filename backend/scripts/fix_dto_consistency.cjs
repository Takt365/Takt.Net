/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：fix_dto_consistency.cjs
 * 创建时间：2026-04-23
 * 功能描述：根据实体自动生成/修复完整的DTO文件
 *   1. 基础Dto（查询结果DTO，带Id映射）
 *   2. 查询Dto（带Id映射，所有字段可空，时间范围字段）
 *   3. 创建Dto（无Id映射）
 *   4. 更新Dto（继承CreateDto，仅Id映射）
 *   5. StatusDto（如果实体有Status字段）
 *   6. SortDto（如果实体有OrderNum字段）
 *   7. 模板Dto（导入模板）
 *   8. 导入Dto（包含ExtFieldJson和Remark）
 *   9. 导出Dto
 * 
 * 使用方法：
 *   1. 全量修复：node scripts/fix_dto_consistency.cjs
 *   2. 修复指定实体：node scripts/fix_dto_consistency.cjs --entity TaktUser
 *   3. 预览模式（不实际修改）：node scripts/fix_dto_consistency.cjs --dry-run
 * ========================================
 */

const fs = require('fs');
const path = require('path');

const ENTITY_PATH = path.join(__dirname, '../src/Takt.Domain/Entities');
const DTO_PATH = path.join(__dirname, '../src/Takt.Application/Dtos');

const args = process.argv.slice(2);
const targetEntity = args.find(a => a.startsWith('--entity='))?.split('=')[1];
const dryRun = args.includes('--dry-run');

/**
 * 提取实体字段信息
 */
function extractEntityFields(entityFile) {
  const content = fs.readFileSync(entityFile, 'utf-8');
  const fields = [];
  const navigationProperties = [];
  
  const classMatch = content.match(/public\s+class\s+(\w+)/);
  const className = classMatch ? classMatch[1] : '';
  
  let entityDescription = className;
  const sugarTableMatch = content.match(/SugarTable\([^,]*,\s*"([^"]+)"/);
  if (sugarTableMatch) {
    entityDescription = sugarTableMatch[1];
  }
  
  fields.push({
    name: 'Id',
    type: 'long',
    isNullable: false,
    description: entityDescription + 'ID'
  });
  
  const lines = content.split('\n');
  let inIgnoreProperty = false;
  let currentIsNullable = false;
  let currentSummary = '';
  let columnDescription = '';
  let isNavigationProperty = false;
  
  for (let i = 0; i < lines.length; i++) {
    const line = lines[i].trim();
    
    // 检测导航属性 [Navigate(...)]
    if (line.includes('[Navigate(')) {
      isNavigationProperty = true;
      continue;
    }
    
    if (line.includes('IsIgnore') && line.includes('true')) {
      inIgnoreProperty = true;
      continue;
    }
    
    if (line.includes('SugarColumn') && line.includes('IsNullable')) {
      currentIsNullable = line.includes('IsNullable = true');
    }
    
    if (line.includes('SugarColumn') && line.includes('ColumnDescription')) {
      const descMatch = line.match(/ColumnDescription\s*=\s*["'](.*?)["']/);
      if (descMatch) {
        columnDescription = descMatch[1];
      }
    }
    
    if (line.startsWith('/// <summary>')) {
      currentSummary = '';
    } else if (line.startsWith('///') && !line.includes('</summary>')) {
      currentSummary += line + '\n';
    } else if (line.includes('</summary>')) {
      currentSummary += line;
    }
    
    const propMatch = line.match(/public\s+(\w+[\w<>?,\s]+)\s+(\w+)\s*\{/);
    if (propMatch && !inIgnoreProperty) {
      const propType = propMatch[1];
      const propName = propMatch[2];
      
      // 处理导航属性
      if (isNavigationProperty) {
        // 提取导航属性类型，如 List<TaktPostDelegate>
        const listMatch = propType.match(/List<(\w+)>/);
        if (listMatch) {
          const relatedEntity = listMatch[1];
          // 推断Ids字段名，如 PostDelegates -> PostIds
          const idsName = propName.replace(/s$/, 'Ids');
          navigationProperties.push({
            name: propName,
            type: propType,
            idsName: idsName,
            relatedEntity: relatedEntity,
            description: currentSummary ? currentSummary.replace(/<\/?summary>/g, '').replace(/\/\/\/\s*/g, '').trim() : propName
          });
        }
        isNavigationProperty = false;
      }
      
      if (!['Id', 'CreatedAt', 'CreatedBy', 'UpdatedAt', 'UpdatedBy', 'IsDeleted'].includes(propName)) {
        let fieldDescription = columnDescription;
        if (!fieldDescription && currentSummary) {
          const descMatch = currentSummary.match(/<summary>([\s\S]*?)<\/summary>/);
          if (descMatch) {
            fieldDescription = descMatch[1].trim().replace(/\s+/g, ' ');
          }
        }
        if (!fieldDescription) {
          fieldDescription = propName;
        }
        
        fields.push({ 
          name: propName, 
          type: propType,
          isNullable: currentIsNullable || propType.endsWith('?'),
          description: fieldDescription
        });
      }
    }
    
    if (line === '') {
      inIgnoreProperty = false;
      currentIsNullable = false;
      columnDescription = '';
      currentSummary = '';
    } else if (line.startsWith('[')) {
      inIgnoreProperty = false;
      currentSummary = '';
    } else if (line.startsWith('///')) {
      // 注释行，不重置
    } else if (propMatch) {
      // 属性行
    } else {
      inIgnoreProperty = false;
      currentIsNullable = false;
      columnDescription = '';
      currentSummary = '';
      isNavigationProperty = false;
    }
  }
  
  return { className, fields, entityDescription, navigationProperties };
}

/**
 * 生成文件头
 */
function generateFileHeader(namespace, entityName, entityDescription) {
  const now = new Date();
  const dateStr = now.toISOString().split('T')[0];
  const shortName = entityName.replace(/^Takt/, '');
  
  return `// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：${namespace}
// 文件名称：${entityName}Dtos.cs
// 创建时间：${dateStr}
// 创建人：Takt365
// 功能描述：${entityDescription}DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace ${namespace};

`;
}

/**
 * 获取命名空间
 */
function getNamespace(entityFile) {
  const relativePath = path.relative(ENTITY_PATH, path.dirname(entityFile));
  const namespaceParts = relativePath.split(path.sep).map(p => {
    return p.charAt(0).toUpperCase() + p.slice(1);
  });
  return `Takt.Application.Dtos.${namespaceParts.join('.')}`;
}

/**
 * 生成字段属性
 */
function generateFieldProperty(field, isQueryDto = false, needJsonConverter = false, isCreateDto = false) {
  const { name, type, isNullable, description } = field;
  const finalType = isQueryDto ? (type.endsWith('?') ? type : type + '?') : type;
  
  let code = '';
  code += `    /// <summary>\n    /// ${description}\n    /// </summary>\n`;
  
  if (needJsonConverter) {
    code += `    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]\n`;
  }
  
  code += `    public ${finalType} ${name} { get; set; }`;
  return code;
}

/**
 * 生成Id映射字段
 */
function generateIdMappingField(entityName, description) {
  const shortName = entityName.replace(/^Takt/, '');
  const idFieldName = `${shortName}Id`;
  
  let code = '';
  code += `    /// <summary>\n    /// ${description}（适配字段，序列化为string以避免Javascript精度问题）\n    /// </summary>\n`;
  code += `    [AdaptMember("Id")]\n`;
  code += `    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]\n`;
  code += `    public long ${idFieldName} { get; set; }`;
  
  return code;
}

/**
 * 查找DTO文件
 */
function findDtoFile(entityName, entityFile) {
  const shortName = entityName.replace(/^Takt/, '');
  const possibleNames = [`${entityName}Dtos.cs`, `Takt${shortName}Dtos.cs`];
  
  // 从实体文件路径推断DTO文件路径
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const pathParts = relativePath.split(path.sep);
  // pathParts[0]应该是类似'Accounting'的文件夹，直接将Entities替换为Dtos
  const dtoDir = path.join(DTO_PATH, ...pathParts.slice(0, -1));
  
  for (const name of possibleNames) {
    const fullPath = path.join(dtoDir, name);
    if (fs.existsSync(fullPath)) {
      return fullPath;
    }
  }
  
  // 如果不存在，返回应该的路径
  return path.join(dtoDir, possibleNames[0]);
}

/**
 * 生成完整的DTO文件
 */
function generateCompleteDtoFile(entityInfo, entityFile) {
  const namespace = getNamespace(entityFile);
  let content = generateFileHeader(namespace, entityInfo.className, entityInfo.entityDescription);
  
  // 1. 基础Dto
  content += generateBaseDto(entityInfo);
  content += '\n\n';
  
  // 2. TreeDto（如果有ParentId字段）- 紧跟基础Dto
  const hasParentId = entityInfo.fields.some(f => f.name === 'ParentId');
  if (hasParentId) {
    content += generateTreeDto(entityInfo);
    content += '\n\n';
  }
  
  // 3. 查询Dto
  content += generateQueryDto(entityInfo);
  content += '\n\n';
  
  // 3. 创建Dto
  content += generateCreateDto(entityInfo);
  content += '\n\n';
  
  // 4. 更新Dto
  content += generateUpdateDto(entityInfo);
  content += '\n\n';
  
  // 5. StatusDto（为每个以Status结尾的字段生成独立的StatusDto）
  const statusFields = entityInfo.fields.filter(f => f.name.endsWith('Status'));
  for (const statusField of statusFields) {
    content += generateStatusDto(entityInfo, statusField);
    content += '\n\n';
  }
  
  // 6. SortDto（如果有OrderNum字段）
  const hasOrderNum = entityInfo.fields.some(f => f.name === 'OrderNum');
  if (hasOrderNum) {
    content += generateSortDto(entityInfo);
    content += '\n\n';
  }
  
  // 7. 模板Dto
  content += generateTemplateDto(entityInfo);
  content += '\n\n';
  
  // 8. 导入Dto
  content += generateImportDto(entityInfo);
  content += '\n\n';
  
  // 9. 导出Dto
  content += generateExportDto(entityInfo);
  
  return content;
}

/**
 * 生成基础Dto
 */
function generateBaseDto(entityInfo) {
  const className = `${entityInfo.className}Dto`;
  // 只有非空字符串字段才需要在构造函数中初始化
  const nonNullableStrings = entityInfo.fields.filter(f => 
    f.type === 'string' && !f.isNullable && f.name !== 'Id'
  );
  
  let code = `/// <summary>\n/// ${entityInfo.entityDescription}Dto\n/// </summary>\n`;
  code += `public partial class ${className} : TaktDtosEntityBase\n{\n`;
  
  // 构造函数 - 只初始化非空字符串字段
  if (nonNullableStrings.length > 0) {
    code += `    /// <summary>\n    /// 构造函数\n    /// </summary>\n`;
    code += `    public ${className}()\n    {\n`;
    for (const field of nonNullableStrings) {
      code += `        ${field.name} = string.Empty;\n`;
    }
    code += `    }\n\n`;
  }
  
  // Id映射
  code += `${generateIdMappingField(entityInfo.className, entityInfo.entityDescription)}\n`;
  
  // 其他字段
  for (const field of entityInfo.fields) {
    if (field.name === 'Id') continue;
    if (!['string', 'DateTime', 'decimal', 'long', 'int', 'bool'].includes(field.type.replace('?', ''))) continue;
    
    const needJsonConverter = (field.type === 'long' || field.type === 'long?') && field.name.endsWith('Id') && field.name !== 'Id';
    code += `\n${generateFieldProperty(field, false, needJsonConverter)}`;
  }
  
  // 添加导航属性的Ids字段（如 PostDelegates -> PostIds）
  if (entityInfo.navigationProperties && entityInfo.navigationProperties.length > 0) {
    for (const nav of entityInfo.navigationProperties) {
      // 清理XML标签，生成简洁注释
      const cleanDesc = nav.description
        .replace(/<see\s+cref="[^"]+"\s*\/>/g, '')  // 移除 <see cref="..." />
        .replace(/\n+/g, ' ')                          // 移除换行
        .replace(/\s+/g, ' ')                          // 合并多余空格
        .trim();
      code += `\n\n    /// <summary>\n    /// ${cleanDesc.replace(/列表$/, 'ID列表')}\n    /// </summary>\n`;
      code += `    public List<long>? ${nav.idsName} { get; set; }`;
    }
  }
  
  code += '\n}';
  return code;
}

/**
 * 生成查询Dto
 */
function generateQueryDto(entityInfo) {
  const className = `${entityInfo.className}QueryDto`;
  
  let code = `/// <summary>\n/// ${entityInfo.entityDescription}查询DTO\n/// </summary>\n`;
  code += `public partial class ${className} : TaktPagedQuery\n{\n`;
  
  // 构造函数
  code += `    /// <summary>\n    /// 构造函数\n    /// </summary>\n`;
  code += `    public ${className}()\n    {\n    }\n\n`;
  code += `    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询\n`;
  
  // Id映射
  code += `\n${generateIdMappingField(entityInfo.className, entityInfo.entityDescription)}\n`;
  
  // 其他字段（全部可空）
  for (const field of entityInfo.fields) {
    if (field.name === 'Id') continue;
    if (!['string', 'DateTime', 'decimal', 'long', 'int', 'bool'].includes(field.type.replace('?', ''))) continue;
    
    const needJsonConverter = (field.type === 'long' || field.type === 'long?') && field.name.endsWith('Id') && field.name !== 'Id';
    code += `\n${generateFieldProperty(field, true, needJsonConverter)}`;
    
    // 如果是DateTime字段，紧跟生成起止时间字段
    if (field.type === 'DateTime' || field.type === 'DateTime?') {
      code += `\n\n    /// <summary>\n    /// ${field.description}开始时间\n    /// </summary>\n    public DateTime? ${field.name}Start { get; set; }`;
      code += `\n    /// <summary>\n    /// ${field.description}结束时间\n    /// </summary>\n    public DateTime? ${field.name}End { get; set; }`;
    }
  }
  
  // 固定添加CreatedById和CreatedBy和CreatedAt查询字段（用于按创建人和创建时间查询）
  code += `\n\n    /// <summary>\n    /// 创建人ID\n    /// </summary>\n    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]\n    public long? CreatedById { get; set; }`;
  code += `\n    /// <summary>\n    /// 创建人\n    /// </summary>\n    public long? CreatedBy { get; set; }`;
  code += `\n    /// <summary>\n    /// 创建时间\n    /// </summary>\n    public DateTime? CreatedAt { get; set; }`;
  code += `\n    /// <summary>\n    /// 创建时间开始\n    /// </summary>\n    public DateTime? CreatedAtStart { get; set; }`;
  code += `\n    /// <summary>\n    /// 创建时间结束\n    /// </summary>\n    public DateTime? CreatedAtEnd { get; set; }`;
  
  code += '\n}';
  return code;
}

/**
 * 生成创建Dto
 */
function generateCreateDto(entityInfo) {
  const className = `${entityInfo.className}CreateDto`;
  const nonNullableStrings = entityInfo.fields.filter(f => 
    f.type === 'string' && !f.isNullable && f.name !== 'Id'
  );
  
  let code = `/// <summary>\n/// Takt创建${entityInfo.entityDescription}DTO\n/// </summary>\n`;
  code += `public partial class ${className}\n{\n`;
  
  // 构造函数
  if (nonNullableStrings.length > 0) {
    code += `    /// <summary>\n    /// 构造函数\n    /// </summary>\n`;
    code += `    public ${className}()\n    {\n`;
    for (const field of nonNullableStrings) {
      code += `        ${field.name} = string.Empty;\n`;
    }
    code += `    }\n\n`;
  }
  
  // 实体字段
  for (const field of entityInfo.fields) {
    if (field.name === 'Id') continue;
    if (!['string', 'DateTime', 'decimal', 'long', 'int', 'bool'].includes(field.type.replace('?', ''))) continue;
    
    const needJsonConverter = (field.type === 'long' || field.type === 'long?') && field.name.endsWith('Id') && field.name !== 'Id';
    code += `    ${generateFieldProperty(field, false, needJsonConverter, true)}\n\n`;
  }
  
  // ExtFieldJson和Remark
  code += `    /// <summary>\n    /// 扩展字段JSON\n    /// </summary>\n    public string? ExtFieldJson { get; set; }\n\n`;
  code += `    /// <summary>\n    /// 备注\n    /// </summary>\n    public string? Remark { get; set; }\n`;
  
  code += '}';
  return code;
}

/**
 * 生成更新Dto
 */
function generateUpdateDto(entityInfo) {
  const className = `${entityInfo.className}UpdateDto`;
  const createClassName = `${entityInfo.className}CreateDto`;
  
  let code = `/// <summary>\n/// Takt更新${entityInfo.entityDescription}DTO\n/// </summary>\n`;
  code += `public partial class ${className} : ${createClassName}\n{\n`;
  
  // 构造函数
  code += `    /// <summary>\n    /// 构造函数\n    /// </summary>\n`;
  code += `    public ${className}()\n    {\n    }\n\n`;
  
  // Id映射
  code += `    ${generateIdMappingField(entityInfo.className, entityInfo.entityDescription)}\n`;
  
  code += '}';
  return code;
}

/**
 * 生成StatusDto
 */
function generateStatusDto(entityInfo, statusField) {
  // 智能生成类名：如果Status字段名已经包含实体名后缀，则不再重复添加
  // 例如：TaktUser + UserStatus -> TaktUserStatusDto (不是 TaktUserUserStatusDto)
  let statusFieldName = statusField.name;
  if (statusFieldName.endsWith('Status')) {
    // 移除字段名末尾的'Status'，检查是否与实体类名重复
    const statusPrefix = statusFieldName.slice(0, -6); // 移除 'Status'
    if (entityInfo.className.endsWith(statusPrefix)) {
      // 实体类名已经包含了这个前缀，直接使用 Status
      statusFieldName = 'Status';
    }
  }
  
  const className = `${entityInfo.className}${statusFieldName}Dto`;
  
  let code = `/// <summary>\n/// ${entityInfo.entityDescription}${statusField.description}DTO\n/// </summary>\n`;
  code += `public partial class ${className}\n{\n`;
  
  code += `    /// <summary>\n    /// 构造函数\n    /// </summary>\n`;
  code += `    public ${className}()\n    {\n    }\n\n`;
  
  code += `    ${generateIdMappingField(entityInfo.className, entityInfo.entityDescription)}\n\n`;
  code += `    /// <summary>\n    /// ${statusField.description}（0=禁用，1=启用）\n    /// </summary>\n    public int ${statusField.name} { get; set; }\n`;
  
  code += '}';
  return code;
}

/**
 * 生成SortDto
 */
function generateSortDto(entityInfo) {
  const className = `${entityInfo.className}SortDto`;
  
  let code = `/// <summary>\n/// ${entityInfo.entityDescription}排序DTO\n/// </summary>\n`;
  code += `public partial class ${className}\n{\n`;
  
  code += `    /// <summary>\n    /// 构造函数\n    /// </summary>\n`;
  code += `    public ${className}()\n    {\n    }\n\n`;
  
  code += `    ${generateIdMappingField(entityInfo.className, entityInfo.entityDescription)}\n\n`;
  code += `    /// <summary>\n    /// 排序号\n    /// </summary>\n    public int OrderNum { get; set; }\n`;
  
  code += '}';
  return code;
}

/**
 * 生成TreeDto（用于树形结构）
 */
function generateTreeDto(entityInfo) {
  const className = `${entityInfo.className}TreeDto`;
  const baseClassName = `${entityInfo.className}Dto`;
  
  let code = `/// <summary>\n/// ${entityInfo.entityDescription}树形DTO\n/// </summary>\n`;
  code += `public partial class ${className} : ${baseClassName}\n{\n`;
  
  code += `    /// <summary>\n    /// 构造函数\n    /// </summary>\n`;
  code += `    public ${className}()\n    {\n`;
  code += `        Children = new List<${className}>();\n`;
  code += `    }\n\n`;
  
  code += `    /// <summary>\n    /// 子节点列表\n    /// </summary>\n`;
  code += `    public List<${className}> Children { get; set; }\n`;
  
  code += '}';
  return code;
}

/**
 * 生成模板Dto
 */
function generateTemplateDto(entityInfo) {
  const className = `${entityInfo.className}TemplateDto`;
  const nonNullableStrings = entityInfo.fields.filter(f => 
    f.type === 'string' && !f.isNullable && f.name !== 'Id'
  );
  
  let code = `/// <summary>\n/// ${entityInfo.entityDescription}导入模板DTO\n/// </summary>\n`;
  code += `public partial class ${className}\n{\n`;
  
  if (nonNullableStrings.length > 0) {
    code += `    /// <summary>\n    /// 构造函数\n    /// </summary>\n`;
    code += `    public ${className}()\n    {\n`;
    for (const field of nonNullableStrings) {
      code += `        ${field.name} = string.Empty;\n`;
    }
    code += `    }\n\n`;
  }
  
  for (const field of entityInfo.fields) {
    if (['Id', 'CreatedAt', 'CreatedBy', 'UpdatedAt', 'UpdatedBy', 'IsDeleted'].includes(field.name)) continue;
    if (!['string', 'DateTime', 'decimal', 'long', 'int', 'bool'].includes(field.type.replace('?', ''))) continue;
    
    code += `    ${generateFieldProperty(field)}\n\n`;
  }
  
  code += `    /// <summary>\n    /// 扩展字段JSON\n    /// </summary>\n    public string? ExtFieldJson { get; set; }\n\n`;
  code += `    /// <summary>\n    /// 备注\n    /// </summary>\n    public string? Remark { get; set; }\n`;
  
  code += '}';
  return code;
}

/**
 * 生成导入Dto
 */
function generateImportDto(entityInfo) {
  const className = `${entityInfo.className}ImportDto`;
  const nonNullableStrings = entityInfo.fields.filter(f => 
    f.type === 'string' && !f.isNullable && f.name !== 'Id'
  );
  
  let code = `/// <summary>\n/// ${entityInfo.entityDescription}导入DTO\n/// </summary>\n`;
  code += `public partial class ${className}\n{\n`;
  
  if (nonNullableStrings.length > 0) {
    code += `    /// <summary>\n    /// 构造函数\n    /// </summary>\n`;
    code += `    public ${className}()\n    {\n`;
    for (const field of nonNullableStrings) {
      code += `        ${field.name} = string.Empty;\n`;
    }
    code += `    }\n\n`;
  }
  
  for (const field of entityInfo.fields) {
    if (['Id', 'CreatedAt', 'CreatedBy', 'UpdatedAt', 'UpdatedBy', 'IsDeleted'].includes(field.name)) continue;
    if (!['string', 'DateTime', 'decimal', 'long', 'int', 'bool'].includes(field.type.replace('?', ''))) continue;
    
    code += `    ${generateFieldProperty(field)}\n\n`;
  }
  
  code += `    /// <summary>\n    /// 扩展字段JSON\n    /// </summary>\n    public string? ExtFieldJson { get; set; }\n\n`;
  code += `    /// <summary>\n    /// 备注\n    /// </summary>\n    public string? Remark { get; set; }\n`;
  
  code += '}';
  return code;
}

/**
 * 生成导出Dto
 */
function generateExportDto(entityInfo) {
  const className = `${entityInfo.className}ExportDto`;
  // 只有非空字符串字段才需要在构造函数中初始化
  const nonNullableStrings = entityInfo.fields.filter(f => 
    f.type === 'string' && !f.isNullable && f.name !== 'Id' && !['CreatedAt', 'CreatedBy', 'UpdatedAt', 'UpdatedBy', 'IsDeleted', 'ExtFieldJson'].includes(f.name)
  );
  
  let code = `/// <summary>\n/// ${entityInfo.entityDescription}导出DTO\n/// </summary>\n`;
  code += `public partial class ${className}\n{\n`;
  
  // 构造函数 - 初始化非空字符串字段和CreatedAt
  code += `    /// <summary>\n    /// 构造函数\n    /// </summary>\n`;
  code += `    public ${className}()\n    {\n`;
  code += `        CreatedAt = DateTime.Now;\n`;
  for (const field of nonNullableStrings) {
    code += `        ${field.name} = string.Empty;\n`;
  }
  code += `    }\n\n`;
  
  for (const field of entityInfo.fields) {
    if (['Id', 'CreatedAt', 'CreatedBy', 'UpdatedAt', 'UpdatedBy', 'IsDeleted', 'ExtFieldJson'].includes(field.name)) continue;
    if (!['string', 'DateTime', 'decimal', 'long', 'int', 'bool'].includes(field.type.replace('?', ''))) continue;
    
    code += `    ${generateFieldProperty(field)}\n\n`;
  }
  
  code += `    /// <summary>\n    /// 创建时间\n    /// </summary>\n    public DateTime CreatedAt { get; set; }\n`;
  
  code += '}';
  return code;
}

/**
 * 修复单个DTO文件
 */
function fixDtoFile(dtoFile, entityInfo, entityFile) {
  console.log(`  📝 生成完整DTO文件`);
  
  const content = generateCompleteDtoFile(entityInfo, entityFile);
  
  if (!dryRun) {
    fs.writeFileSync(dtoFile, content, 'utf-8');
    console.log(`  ✅ 已保存: ${path.relative(DTO_PATH, dtoFile)}`);
  } else {
    console.log(`  🔍 [预览模式] 将生成 ${path.relative(DTO_PATH, dtoFile)}`);
  }
  
  return true;
}

/**
 * 主函数
 */
function main() {
  console.log('========================================');
  console.log('  DTO自动生成工具');
  console.log('========================================\n');
  
  if (dryRun) {
    console.log('🔍 预览模式：不会实际修改文件\n');
  }
  
  const entityFiles = [];
  function findEntities(dir) {
    const files = fs.readdirSync(dir);
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      if (stat.isDirectory()) {
        findEntities(fullPath);
      } else if (file.endsWith('.cs') && file.startsWith('Takt') && !file.includes('EntitiesSeedData') && file !== 'TaktEntityBase.cs') {
        const content = fs.readFileSync(fullPath, 'utf-8');
        if (content.includes('TaktEntityBase')) {
          entityFiles.push(fullPath);
        }
      }
    }
  }
  findEntities(ENTITY_PATH);
  
  const targetFiles = targetEntity 
    ? entityFiles.filter(f => path.basename(f, '.cs') === targetEntity)
    : entityFiles;
  
  if (targetFiles.length === 0) {
    console.log(`❌ 未找到实体: ${targetEntity}`);
    return;
  }
  
  console.log(`找到 ${targetFiles.length} 个实体\n`);
  
  let fixedCount = 0;
  
  for (const entityFile of targetFiles) {
    const entityName = path.basename(entityFile, '.cs');
    console.log(`\n生成: ${entityName}`);
    console.log('='.repeat(60));
    
    const entityInfo = extractEntityFields(entityFile);
    console.log(`实体字段数: ${entityInfo.fields.length}`);
    
    const dtoFile = findDtoFile(entityName, entityFile);
    
    // 确保目录存在
    const dtoDir = path.dirname(dtoFile);
    if (!fs.existsSync(dtoDir)) {
      fs.mkdirSync(dtoDir, { recursive: true });
    }
    
    if (fixDtoFile(dtoFile, entityInfo, entityFile)) {
      fixedCount++;
    }
  }
  
  console.log('\n========================================');
  console.log(`  完成！已生成: ${fixedCount}`);
  console.log('========================================');
}

try {
  main();
} catch (error) {
  console.error('❌ 失败:', error.message);
  console.error(error.stack);
  process.exit(1);
}
