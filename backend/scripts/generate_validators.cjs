/**
 * 根据实体文件自动生成 FluentValidation 验证器
 * 
 * 功能：
 * 1. 扫描实体目录，读取实体的 SugarColumn 特性
 * 2. 根据 IsNullable、Length、DefaultValue 等生成验证规则
 * 3. 自动识别特殊字段（Email、Phone、Password、IdCard 等）并添加正则验证
 * 4. 生成 CreateDtoValidator 和 UpdateDtoValidator
 * 5. 自动创建正确的目录结构和文件头注释
 * 
 * 使用方式：
 *   node scripts/generate_validators.cjs [实体相对路径]
 *   例如：node scripts/generate_validators.cjs HumanResource/Personnel/TaktEmployee.cs
 *   不传参数则生成所有实体的验证器
 */

const fs = require('fs');
const path = require('path');

const ENTITIES_DIR = path.join(__dirname, '../src/Takt.Domain/Entities');
const VALIDATORS_DIR = path.join(__dirname, '../src/Takt.WebApi/Validators');

// 特殊字段的验证规则映射
const SPECIAL_FIELD_RULES = {
  // 邮箱字段
  email: {
    pattern: 'TaktRegexHelper.IsValidEmail',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternEmail", "{resourceKey}")',
    when: true
  },
  useremail: {
    pattern: 'TaktRegexHelper.IsValidEmail',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternEmail", "{resourceKey}")',
    when: true
  },
  
  // 手机/电话字段
  phone: {
    pattern: 'TaktRegexHelper.IsValidPhone',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternPhone", "{resourceKey}")',
    when: true
  },
  userphone: {
    pattern: 'TaktRegexHelper.IsValidPhone',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternPhone", "{resourceKey}")',
    when: true
  },
  mobile: {
    pattern: 'TaktRegexHelper.IsValidPhone',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternPhone", "{resourceKey}")',
    when: true
  },
  
  // 身份证号
  idcard: {
    required: true,
    pattern: 'TaktRegexHelper.IsValidIdCard',
    message: 'TaktValidationMessages.IdCardInvalid(localizer, "{resourceKey}")'
  },
  
  // 密码字段
  password: {
    pattern: 'TaktRegexHelper.IsValidPassword',
    message: 'TaktValidationMessages.PatternPasswordStrong(localizer, "{resourceKey}")',
    when: true
  },
  passwordhash: {
    pattern: 'TaktRegexHelper.IsValidPassword',
    message: 'TaktValidationMessages.PatternPasswordStrong(localizer, "{resourceKey}")',
    when: true
  },
  
  // 姓名字段
  realname: {
    required: true,
    pattern: 'TaktRegexHelper.IsValidFullName',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternFullName", "{resourceKey}")'
  },
  username: {
    pattern: 'TaktRegexHelper.IsValidFullName',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternFullName", "{resourceKey}")'
  },
  nickname: {
    pattern: 'v => string.IsNullOrWhiteSpace(v) || TaktRegexHelper.NickName.IsMatch(v.Trim())',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternNickName", "{resourceKey}")'
  },
  
  // 角色编码
  rolecode: {
    required: true,
    pattern: 'v => TaktRegexHelper.IsMatch(TaktRegexHelper.RoleCode, v)',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternRoleCode", "{resourceKey}")'
  },
  
  // 编码字段
  employeecode: {
    required: true,
    maxLength: 50
  },
  deptcode: {
    required: true,
    maxLength: 50
  },
  postcode: {
    required: true,
    maxLength: 50
  },
  tenantcode: {
    required: true,
    maxLength: 50
  },
  plantcode: {
    required: true,
    pattern: 'TaktRegexHelper.IsValidPlantCode',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternPlantCode", "{resourceKey}")'
  },
  companycode: {
    required: true,
    pattern: 'TaktRegexHelper.IsValidCompanyCode',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternCompanyCode", "{resourceKey}")'
  },
  profitcentercode: {
    required: true,
    pattern: 'TaktRegexHelper.IsValidProfitCenterCode',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternProfitCenterCode", "{resourceKey}")'
  },
  costelementcode: {
    required: true,
    pattern: 'TaktRegexHelper.IsValidCostElementCode',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternCostElementCode", "{resourceKey}")'
  },
  costcentercode: {
    required: true,
    pattern: 'TaktRegexHelper.IsValidCostCenterCode',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternCostCenterCode", "{resourceKey}")'
  },
  titlecode: {
    required: true,
    pattern: 'TaktRegexHelper.IsValidTitleCode',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternTitleCode", "{resourceKey}")'
  },
  assetcode: {
    required: true,
    pattern: 'TaktRegexHelper.IsValidAssetCode',
    message: 'TaktValidationMessages.Pattern(localizer, "validation.patternAssetCode", "{resourceKey}")'
  }
};

// 解析实体文件
function parseEntityFile(filePath) {
  const content = fs.readFileSync(filePath, 'utf8');
  const lines = content.split('\n');
  
  // 提取命名空间
  const namespaceMatch = content.match(/namespace\s+([^;]+);/);
  if (!namespaceMatch) return null;
  const namespace_ = namespaceMatch[1];
  
  // 提取类名
  const classMatch = content.match(/public\s+class\s+(\w+)\s*:/);
  if (!classMatch) return null;
  const className = classMatch[1];
  
  // 提取字段
  const fields = [];
  let inClass = false;
  let braceCount = 0;
  
  for (let i = 0; i < lines.length; i++) {
    const line = lines[i].trim();
    
    // 检测类开始
    if (line.match(/public\s+class\s+\w+/)) {
      inClass = true;
    }
    
    if (inClass) {
      // 计算大括号
      for (const char of line) {
        if (char === '{') braceCount++;
        if (char === '}') braceCount--;
      }
      
      // 类结束
      if (braceCount === 0 && line.includes('}')) {
        break;
      }
      
      // 解析字段
      const fieldInfo = parseField(lines, i);
      if (fieldInfo) {
        fields.push(fieldInfo);
        i = fieldInfo.endLine; // 跳过已处理的行
      }
    }
  }
  
  return {
    namespace: namespace_,
    className,
    fields,
    filePath
  };
}

// 解析单个字段
function parseField(lines, startLine) {
  let endLine = startLine;
  let sugarColumn = null;
  let summary = '';
  
  const line = lines[startLine].trim();
  
  // 跳过多行注释
  if (line.startsWith('///')) {
    let j = startLine;
    while (j < lines.length && lines[j].trim().startsWith('///')) {
      summary += lines[j].trim();
      j++;
    }
    endLine = j - 1;
  }
  
  // 查找 SugarColumn 特性
  let k = endLine + 1;
  while (k < lines.length && lines[k].trim().startsWith('[')) {
    const sugarMatch = lines[k].trim().match(/\[SugarColumn\((.+)\)\]/);
    if (sugarMatch) {
      sugarColumn = parseSugarColumn(sugarMatch[1]);
    }
    endLine = k;
    k++;
  }
  
  // 解析属性声明
  const propLine = lines[endLine + 1];
  if (!propLine) return null;
  
  const propMatch = propLine.trim().match(/public\s+(\S+)\s+(\w+)\s*{\s*get;\s*set;\s*}/);
  if (!propMatch) return null;
  
  const type = propMatch[1];
  const name = propMatch[2];
  const isNullable = type.endsWith('?') || type === 'string?';
  
  return {
    name,
    type: type.replace('?', ''),
    isNullable,
    sugarColumn,
    summary,
    endLine: endLine + 1
  };
}

// 解析 SugarColumn 特性
function parseSugarColumn(argsStr) {
  const result = {};
  
  // ColumnName
  const nameMatch = argsStr.match(/ColumnName\s*=\s*"([^"]+)"/);
  if (nameMatch) result.columnName = nameMatch[1];
  
  // ColumnDescription
  const descMatch = argsStr.match(/ColumnDescription\s*=\s*"([^"]+)"/);
  if (descMatch) result.columnDescription = descMatch[1];
  
  // Length
  const lengthMatch = argsStr.match(/Length\s*=\s*(\d+)/);
  if (lengthMatch) result.length = parseInt(lengthMatch[1]);
  
  // IsNullable
  const nullableMatch = argsStr.match(/IsNullable\s*=\s*(true|false)/);
  if (nullableMatch) result.isNullable = nullableMatch[1] === 'true';
  
  // DefaultValue（提取默认值）
  const defaultMatch = argsStr.match(/DefaultValue\s*=\s*"([^"]+)"/);
  if (defaultMatch) result.defaultValue = defaultMatch[1];
  
  return result;
}

// 生成资源键
function generateResourceKey(className, fieldName) {
  // TaktEmployee -> entity.employee
  const entityPart = className.replace('Takt', '').toLowerCase();
  const words = entityPart.match(/[A-Z][a-z]+/g) || [entityPart];
  const entityKey = words.join('.');
  
  return `entity.${entityKey}.${fieldName.toLowerCase()}`;
}

// 从注释中提取枚举值范围（如："0=未知，1=男，2=女" → {min: 0, max: 2}）
function extractEnumRangeFromSummary(summary) {
  if (!summary) return null;
  
  // 匹配模式：数字=xxx，数字=xxx
  const enumMatches = summary.match(/(\d+)=[^，,，]+/g);
  if (!enumMatches || enumMatches.length === 0) return null;
  
  const values = enumMatches.map(m => parseInt(m.match(/(\d+)/)[1]));
  const min = Math.min(...values);
  const max = Math.max(...values);
  
  return { min, max, values };
}

// 判断字段是否需要验证（只要 IsNullable = false 就需要验证）
function isFieldRequired(field) {
  // 可空字段不需要必填验证
  if (field.isNullable) return false;
  
  // string 类型且 IsNullable = false → 需要验证
  if (field.type === 'string') return true;
  
  // int/long 类型 → 检查注释中是否有枚举值
  if ((field.type === 'int' || field.type === 'long') && field.summary) {
    const enumRange = extractEnumRangeFromSummary(field.summary);
    if (enumRange) return true;
  }
  
  // DateTime 等其它类型 → 不验证
  return false;
}

// 生成验证规则
function generateValidationRules(entity, fields, isUpdate = false) {
  const rules = [];
  
  for (const field of fields) {
    // 跳过基类字段和导航属性
    if (['Id', 'ConfigId', 'CreatedBy', 'CreatedById', 'CreatedAt', 'UpdatedBy', 'UpdatedAt', 'IsDeleted', 'Remark'].includes(field.name)) {
      continue;
    }
    
    // 跳过列表类型
    if (field.type.startsWith('List<')) {
      continue;
    }
    
    const resourceKey = generateResourceKey(entity.className, field.name);
    const lowerName = field.name.toLowerCase();
    
    // 查找特殊字段规则
    const specialRule = SPECIAL_FIELD_RULES[lowerName];
    
    if (specialRule) {
      // 特殊字段规则
      if (specialRule.required && !isUpdate) {
        // 强制必填的特殊字段（如 IdCard、RealName 等）
        rules.push(`
        RuleFor(x => x.${field.name})
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "${resourceKey}"))`);
        
        if (specialRule.pattern) {
          rules[rules.length - 1] += `
            .Must(${specialRule.pattern}).WithMessage(${specialRule.message.replace('{resourceKey}', resourceKey)})`;
        }
        
        if (specialRule.maxLength) {
          rules[rules.length - 1] += `
            .MaximumLength(${specialRule.maxLength}).WithMessage(TaktValidationMessages.LengthMax(localizer, "${resourceKey}", ${specialRule.maxLength}))`;
        }
        
        rules[rules.length - 1] += ';';
      } else if (specialRule.when) {
        // 可选但有格式要求（Email、Phone 等）
        rules.push(`
        RuleFor(x => x.${field.name})
            .Must(${specialRule.pattern}).WithMessage(${specialRule.message.replace('{resourceKey}', resourceKey)})`);
        
        if (field.sugarColumn?.length) {
          rules[rules.length - 1] += `
            .MaximumLength(${field.sugarColumn.length}).WithMessage(TaktValidationMessages.LengthMax(localizer, "${resourceKey}", ${field.sugarColumn.length}))`;
        }
        
        rules[rules.length - 1] += `
            .When(x => !string.IsNullOrWhiteSpace(x.${field.name}));`;
      }
    } else {
      // 通用字段规则 - 根据实体 IsNullable 自动判断
      const fieldRequired = isFieldRequired(field);
      
      if (fieldRequired && !isUpdate) {
        // IsNullable = false 的 string 字段需要验证（无论是否有默认值）
        if (field.type === 'string') {
          rules.push(`
        RuleFor(x => x.${field.name})
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "${resourceKey}"))`);
          
          if (field.sugarColumn?.length) {
            rules[rules.length - 1] += `
            .Length(1, ${field.sugarColumn.length}).WithMessage(TaktValidationMessages.LengthBetween(localizer, "${resourceKey}", 1, ${field.sugarColumn.length}))`;
          }
          
          rules[rules.length - 1] += ';';
        } else if (field.type === 'int' || field.type === 'long') {
          // int/long 类型 → 检查注释中的枚举值范围
          const enumRange = extractEnumRangeFromSummary(field.summary);
          if (enumRange) {
            rules.push(`
        RuleFor(x => x.${field.name})
            .InclusiveBetween(${enumRange.min}, ${enumRange.max})
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "${resourceKey}"));`);
          }
        }
      } else if (field.type === 'string') {
        // IsNullable = true 的字符串，只验证最大长度
        if (field.sugarColumn?.length) {
          rules.push(`
        RuleFor(x => x.${field.name})
            .MaximumLength(${field.sugarColumn.length}).WithMessage(TaktValidationMessages.LengthMax(localizer, "${resourceKey}", ${field.sugarColumn.length}))
            .When(x => !string.IsNullOrWhiteSpace(x.${field.name}));`);
        }
      }
      // DateTime 等其它类型不生成验证规则
    }
  }
  
  return rules.join('\n');
}

// 生成验证器文件
function generateValidatorFile(entity, dtoNamespace) {
  const entityName = entity.className.replace('Takt', '');
  const entityKey = entityName.toLowerCase();
  
  const createDtoName = `Takt${entityName}CreateDto`;
  const updateDtoName = `Takt${entityName}UpdateDto`;
  
  // 获取 CreateDto 和 UpdateDto 的字段（排除 UpdateDto 特有的 ID 字段）
  const createFields = entity.fields.filter(f => !['Id'].includes(f.name));
  const updateFields = entity.fields;
  
  const createRules = generateValidationRules(entity, createFields, false);
  const updateRules = generateValidationRules(entity, updateFields, true);
  
  // 生成正确的命名空间路径（用于 see cref）
  const entityFullName = `${entity.namespace}.${entity.className}`;
  
  const content = `// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.${entity.namespace.replace('Takt.Domain.Entities.', '')}
// 文件名称：Takt${entityName}Validators.cs
// 创建时间：${new Date().toISOString().split('T')[0]}
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：${entityName} DTO 验证器（根据实体 ${entity.className} 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using ${dtoNamespace};
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.${entity.namespace.replace('Takt.Domain.Entities.', '')};

/// <summary>
/// ${entityName}创建 DTO 验证器（与 <see cref="${entityFullName}"/> 字段对齐）。
/// </summary>
public class ${createDtoName}Validator : AbstractValidator<${createDtoName}>
{
    public ${createDtoName}Validator(ITaktLocalizer? localizer = null)
    {${createRules}
    }
}

/// <summary>
/// ${entityName}更新 DTO 验证器。
/// </summary>
public class ${updateDtoName}Validator : AbstractValidator<${updateDtoName}>
{
    public ${updateDtoName}Validator(ITaktLocalizer? localizer = null)
    {
        Include(new ${createDtoName}Validator(localizer));

        RuleFor(x => x.${entityName}Id)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "${generateResourceKey(entity.className, entityName + 'Id')}"));
${updateRules}
    }
}
`;
  
  return content;
}

// 主函数
function main() {
  const args = process.argv.slice(2);
  const targetEntity = args[0];
  
  console.log('🔍 开始生成验证器...\n');
  
  let entityFiles = [];
  
  if (targetEntity) {
    // 生成指定实体
    const fullPath = path.join(ENTITIES_DIR, targetEntity);
    if (!fs.existsSync(fullPath)) {
      console.error(`❌ 实体文件不存在: ${fullPath}`);
      process.exit(1);
    }
    entityFiles = [fullPath];
  } else {
    // 扫描所有实体
    function scanDir(dir) {
      const files = fs.readdirSync(dir);
      files.forEach(file => {
        const fullPath = path.join(dir, file);
        const stat = fs.statSync(fullPath);
        if (stat.isDirectory()) {
          scanDir(fullPath);
        } else if (file.endsWith('.cs') && file.startsWith('Takt') && !file.includes('Base')) {
          entityFiles.push(fullPath);
        }
      });
    }
    scanDir(ENTITIES_DIR);
  }
  
  let generated = 0;
  
  for (const entityFile of entityFiles) {
    console.log(`📄 解析实体: ${path.relative(ENTITIES_DIR, entityFile)}`);
    
    const entity = parseEntityFile(entityFile);
    if (!entity) {
      console.log('   ⚠️  跳过（无法解析）\n');
      continue;
    }
    
    // 确定 DTO 命名空间
    const dtoNamespace = entity.namespace
      .replace('Takt.Domain.Entities', 'Takt.Application.Dtos');
    
    // 确定验证器输出路径
    const relativePath = path.relative(ENTITIES_DIR, entityFile);
    const validatorPath = path.join(VALIDATORS_DIR, relativePath.replace('.cs', 'Validators.cs'));
    
    // 创建目录
    const validatorDir = path.dirname(validatorPath);
    if (!fs.existsSync(validatorDir)) {
      fs.mkdirSync(validatorDir, { recursive: true });
    }
    
    // 生成验证器
    const content = generateValidatorFile(entity, dtoNamespace);
    fs.writeFileSync(validatorPath, content, 'utf8');
    
    console.log(`   ✅ 生成: ${path.relative(VALIDATORS_DIR, validatorPath)}\n`);
    generated++;
  }
  
  console.log(`\n✨ 完成！共生成 ${generated} 个验证器文件`);
}

main();
