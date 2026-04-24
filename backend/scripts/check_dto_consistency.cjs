/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：check_dto_consistency.cjs
 * 创建时间：2026-04-23
 * 功能描述：检查DTO字段与实体字段的一致性
 *   1. 扫描所有实体文件，提取字段名称和类型
 *   2. 扫描对应的DTO文件，提取字段名称和类型
 *   3. 对比差异，报告缺失、多余、类型不匹配的字段
 *   4. 支持多种DTO类型（Dto、CreateDto、UpdateDto、QueryDto）
 * 
 * 使用方法：
 *   1. 全量检查：node scripts/check_dto_consistency.cjs
 *   2. 检查指定实体：node scripts/check_dto_consistency.cjs --entity TaktUser
 *   3. 只检查QueryDto：node scripts/check_dto_consistency.cjs --query-only
 *   4. 生成修复报告：node scripts/check_dto_consistency.cjs --report
 * 
 * 注意事项：
 *   - 实体文件路径：src/Takt.Domain/Entities
 *   - DTO文件路径：src/Takt.Application/Dtos
 *   - 忽略导航属性（SugarColumn(IsIgnore=true)）
 *   - 忽略系统字段（Id、CreatedAt等基类字段）
 *   - QueryDto的字符串字段应该是可空的（string?）
 *   - 使用--report参数会生成UTF-8编码的报告文件
 * ========================================
 */

const fs = require('fs');
const path = require('path');

const ENTITY_PATH = path.join(__dirname, '../src/Takt.Domain/Entities');
const DTO_PATH = path.join(__dirname, '../src/Takt.Application/Dtos');

// 解析命令行参数
const args = process.argv.slice(2);
const targetEntity = args.find(a => a.startsWith('--entity='))?.split('=')[1];
const queryOnly = args.includes('--query-only');
const generateReport = args.includes('--report');

/**
 * 自动修复DTO继承关系
 */
function fixDtoInheritance(dtoFile, dtoInfo) {
  let content = fs.readFileSync(dtoFile, 'utf-8');
  let fixedCount = 0;
  
  for (const [dtoClassName, dtoData] of Object.entries(dtoInfo)) {
    if (dtoClassName.includes('UpdateDto') && dtoData.inheritanceIssue && dtoData.inheritanceIssue.autoFix) {
      const expectedBase = dtoData.inheritanceIssue.expected;
      
      // 替换继承关系
      const regex = new RegExp(`(public\\s+class\\s+${dtoClassName}\\s*:\\s*)${dtoData.inheritanceIssue.current}`);
      const newContent = content.replace(regex, `$1${expectedBase}`);
      
      if (newContent !== content) {
        content = newContent;
        fixedCount++;
        console.log(`    ✅ 已修复: ${dtoClassName} 现在继承 ${expectedBase}`);
      }
    }
  }
  
  if (fixedCount > 0) {
    fs.writeFileSync(dtoFile, content, 'utf-8');
    console.log(`  📝 共修复 ${fixedCount} 个继承关系问题`);
  }
  
  return fixedCount;
}

/**
 * 统一DTO文件名为*Dtos.cs（复数形式）
 */
function normalizeDtoFileNames() {
  let renamedCount = 0;
  
  function scanAndRename(dir) {
    const files = fs.readdirSync(dir);
    
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      
      if (stat.isDirectory()) {
        scanAndRename(fullPath);
      } else if (file.endsWith('Dto.cs') && !file.endsWith('Dtos.cs')) {
        // 重命名为复数形式
        const newName = file.replace(/Dto\.cs$/, 'Dtos.cs');
        const newPath = path.join(dir, newName);
        
        try {
          // 1. 重命名文件
          fs.renameSync(fullPath, newPath);
          
          // 2. 更新文件头部注释中的文件名
          let content = fs.readFileSync(newPath, 'utf-8');
          
          // 匹配各种格式：// 文件名称：xxx.Dto.cs 或 // 文件名称:xxx.Dto.cs
          const oldFileName = file;
          const newFileName = newName;
          
          // 替换文件名称注释
          content = content.replace(
            new RegExp(`(\\/\\/\\s*文件名称[：:]\\s*)${oldFileName.replace(/[.*+?^${}()|[\]\\]/g, '\\$&')}`, 'i'),
            `$1${newFileName}`
          );
          
          fs.writeFileSync(newPath, content, 'utf-8');
          
          console.log(`  ✅ 已重命名: ${path.relative(DTO_PATH, fullPath)} -> ${newName}`);
          renamedCount++;
        } catch (error) {
          console.error(`  ❌ 重命名失败: ${file} - ${error.message}`);
        }
      }
    }
  }
  
  scanAndRename(DTO_PATH);
  
  if (renamedCount > 0) {
    console.log(`\n📝 共重命名 ${renamedCount} 个DTO文件为复数形式\n`);
  }
}

// 报告收集器
const reportLines = [];
function logAndReport(message) {
  console.log(message);
  if (generateReport) {
    reportLines.push(message);
  }
}

/**
 * 提取实体字段信息
 */
function extractEntityFields(entityFile) {
  const content = fs.readFileSync(entityFile, 'utf-8');
  const fields = [];
  
  // 提取类名
  const classMatch = content.match(/public\s+class\s+(\w+)/);
  const className = classMatch ? classMatch[1] : '';
  
  // 提取所有属性（排除导航属性和系统字段）
  const lines = content.split('\n');
  let inIgnoreProperty = false;
  let currentIsNullable = false;
  
  for (let i = 0; i < lines.length; i++) {
    const line = lines[i].trim();
    
    // 检查是否是导航属性（IsIgnore=true）
    if (line.includes('IsIgnore') && line.includes('true')) {
      inIgnoreProperty = true;
      continue;
    }
    
    // 检查IsNullable属性
    if (line.includes('SugarColumn') && line.includes('IsNullable')) {
      currentIsNullable = line.includes('IsNullable = true');
    }
    
    // 匹配属性定义
    const propMatch = line.match(/public\s+(\w+\??)\s+(\w+)\s*\{/);
    if (propMatch && !inIgnoreProperty) {
      const propType = propMatch[1];
      const propName = propMatch[2];
      
      // 排除基类字段
      if (!['Id', 'CreatedAt', 'CreatedBy', 'UpdatedAt', 'UpdatedBy', 'IsDeleted'].includes(propName)) {
        fields.push({ 
          name: propName, 
          type: propType,
          isNullable: currentIsNullable || propType.endsWith('?')
        });
      }
    }
    
    // 遇到下一行重置
    if (line === '' || line.startsWith('///') || line.startsWith('[')) {
      inIgnoreProperty = false;
      if (!line.includes('SugarColumn')) {
        currentIsNullable = false;
      }
    }
  }
  
  return { className, fields };
}

/**
 * 提取DTO字段信息
 */
function extractDtoFields(dtoFile) {
  if (!fs.existsSync(dtoFile)) {
    return null;
  }
  
  const content = fs.readFileSync(dtoFile, 'utf-8');
  const dtos = {};
  
  // 分割多个DTO类
  const classRegex = /public\s+class\s+(\w+Dto\w*)\s*:\s*(\w+)/g;
  let classMatch;
  
  while ((classMatch = classRegex.exec(content)) !== null) {
    const dtoClassName = classMatch[1];
    const baseClassName = classMatch[2];
    const startIndex = classMatch.index;
    
    // 找到类的结束位置
    let braceCount = 0;
    let endIndex = startIndex;
    let started = false;
    
    for (let i = startIndex; i < content.length; i++) {
      if (content[i] === '{') {
        braceCount++;
        started = true;
      } else if (content[i] === '}') {
        braceCount--;
        if (started && braceCount === 0) {
          endIndex = i;
          break;
        }
      }
    }
    
    const classContent = content.substring(startIndex, endIndex + 1);
    const fields = [];
    
    // 提取属性
    const propRegex = /public\s+(\w+\??)\s+(\w+)\s*\{/g;
    let propMatch;
    
    while ((propMatch = propRegex.exec(classContent)) !== null) {
      const propType = propMatch[1];
      const propName = propMatch[2];
      
      // 排除基类字段
      if (!['Id', 'CreatedAt', 'CreatedBy', 'UpdatedAt', 'UpdatedBy', 'IsDeleted'].includes(propName)) {
        fields.push({ name: propName, type: propType });
      }
    }
    
    dtos[dtoClassName] = { fields, baseClass: baseClassName };
  }
  
  return dtos;
}

/**
 * 查找对应的DTO文件
 */
function findDtoFile(entityName) {
  // 移除Takt前缀
  const shortName = entityName.replace(/^Takt/, '');
  
  // 只查找复数形式的DTO文件（已经统一命名）
  const possibleNames = [
    `${entityName}Dtos.cs`,      // TaktFlowSchemeDtos.cs
    `Takt${shortName}Dtos.cs`    // TaktUserDtos.cs
  ];
  
  // 递归搜索DTO目录
  function searchDir(dir) {
    const files = fs.readdirSync(dir);
    
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      
      if (stat.isDirectory()) {
        const result = searchDir(fullPath);
        if (result) return result;
      } else if (possibleNames.includes(file)) {
        return fullPath;
      }
    }
    
    return null;
  }
  
  return searchDir(DTO_PATH);
}

/**
 * 检查字段一致性
 */
function checkConsistency(entityInfo, dtoInfo, entityName) {
  const results = {
    entity: entityName,
    entityFields: entityInfo.fields.length,
    dtoFiles: [],
    issues: []
  };
  
  if (!dtoInfo) {
    results.issues.push({
      type: 'ERROR',
      message: `未找到对应的DTO文件`
    });
    return results;
  }
  
  for (const [dtoClassName, dtoData] of Object.entries(dtoInfo)) {
    const dtoResult = {
      name: dtoClassName,
      fields: dtoData.fields.length,
      missing: [],      // 实体有但DTO没有
      extra: [],        // DTO有但实体没有
      typeMismatch: [],  // 字段名相同但类型不同
      nullableMismatch: [], // 可空性不匹配
      inheritanceIssue: null // 继承关系问题
    };
    
    const dtoFields = dtoData.fields;
    const isQueryDto = dtoClassName.includes('QueryDto');
    const isUpdateDto = dtoClassName.includes('UpdateDto');
    const isCreateDto = dtoClassName.includes('CreateDto');
    
    // 验证UpdateDto必须继承CreateDto
    if (isUpdateDto && dtoData.baseClass && !dtoData.baseClass.includes('CreateDto')) {
      // 查找对应的CreateDto
      const createDtoName = Object.keys(dtoInfo).find(name => name.includes('CreateDto'));
      
      if (createDtoName) {
        dtoResult.inheritanceIssue = {
          current: dtoData.baseClass,
          expected: createDtoName,
          autoFix: true
        };
      }
    }
    
    // 如果UpdateDto正确继承了CreateDto，只验证Id映射字段，跳过其他字段验证
    const shouldSkipFieldCheck = isUpdateDto && dtoData.baseClass && dtoData.baseClass.includes('CreateDto');
    
    if (shouldSkipFieldCheck) {
      // 只检查Id适配字段
      for (const dtoField of dtoFields) {
        const isAdaptField = dtoField.name.match(/^(?:Takt)?\w+Id$/) && dtoField.name !== 'Id';
        if (isAdaptField) {
          // Id适配字段是正确的，不报告任何问题
        }
      }
      // 跳过后续的字段检查
      results.dtoFiles.push(dtoResult);
      continue;
    }
    
    // 检查缺失字段
    for (const entityField of entityInfo.fields) {
      const dtoField = dtoFields.find(f => f.name === entityField.name);
      
      if (!dtoField) {
        dtoResult.missing.push(entityField);
      } else {
        // 检查类型是否匹配（基础类型）
        const entityBaseType = entityField.type.replace('?', '');
        const dtoBaseType = dtoField.type.replace('?', '');
        
        if (entityBaseType !== dtoBaseType) {
          dtoResult.typeMismatch.push({
            entity: entityField,
            dto: dtoField
          });
        }
        
        // 检查可空性
        if (isQueryDto) {
          // QueryDto的所有字段必须是可空的
          if (!dtoField.type.endsWith('?')) {
            dtoResult.nullableMismatch.push({
              name: dtoField.name,
              currentType: dtoField.type,
              expectedType: dtoBaseType + '?',
              reason: 'QueryDto字段必须为可空'
            });
          }
        } else {
          // 其他DTO的可空性必须与实体一致
          const entityIsNullable = entityField.isNullable || entityField.type.endsWith('?');
          const dtoIsNullable = dtoField.type.endsWith('?');
          
          if (entityIsNullable !== dtoIsNullable) {
            dtoResult.nullableMismatch.push({
              name: dtoField.name,
              currentType: dtoField.type,
              expectedType: entityIsNullable ? (dtoBaseType + '?') : dtoBaseType,
              reason: `可空性必须与实体一致（实体IsNullable=${entityIsNullable}）`
            });
          }
        }
      }
    }
    
    // 检查多余字段
    for (const dtoField of dtoFields) {
      const entityField = entityInfo.fields.find(f => f.name === dtoField.name);
      
      // 跳过适配字段（如CostCenterId、TitleId等，它们通过AdaptMember映射到Id）
      const isAdaptField = dtoField.name.match(/^(?:Takt)?\w+Id$/) && dtoField.name !== 'Id';
      
      // 跳过查询范围字段（如*Start、*End、*From、*To等）
      const isQueryRangeField = dtoField.name.match(/(Start|End|From|To)$/);
      
      if (!entityField && !isAdaptField && !isQueryRangeField) {
        dtoResult.extra.push(dtoField);
      }
      
      // QueryDto的多余字段也必须是可空的
      if (isQueryDto && !dtoField.type.endsWith('?') && !isAdaptField && !isQueryRangeField) {
        dtoResult.nullableMismatch.push({
          name: dtoField.name,
          currentType: dtoField.type,
          expectedType: dtoField.type.replace('?', '') + '?',
          reason: 'QueryDto字段必须为可空'
        });
      }
    }
    
    results.dtoFiles.push(dtoResult);
  }
  
  return results;
}

/**
 * 处理单个实体
 */
function processEntity(entityFile) {
  const entityName = path.basename(entityFile, '.cs');
  
  logAndReport(`\n检查: ${entityName}`);
  logAndReport('='.repeat(60));
  
  // 提取实体字段
  const entityInfo = extractEntityFields(entityFile);
  logAndReport(`实体字段数: ${entityInfo.fields.length}`);
  
  // 查找DTO文件
  const dtoFile = findDtoFile(entityName);
  if (!dtoFile) {
    logAndReport(`❌ 未找到DTO文件`);
    return { entity: entityName, status: 'MISSING_DTO' };
  }
  
  logAndReport(`DTO文件: ${path.relative(DTO_PATH, dtoFile)}`);
  
  // 提取DTO字段
  const dtoInfo = extractDtoFields(dtoFile);
  if (!dtoInfo || Object.keys(dtoInfo).length === 0) {
    logAndReport(`❌ DTO文件中未找到DTO类`);
    return { entity: entityName, status: 'NO_DTO_CLASS' };
  }
  
  logAndReport(`DTO类数: ${Object.keys(dtoInfo).length}`);
  for (const [name, data] of Object.entries(dtoInfo)) {
    logAndReport(`  - ${name}: ${data.fields.length} 个字段`);
  }
  
  // 检查并修复UpdateDto继承关系
  let inheritanceFixed = 0;
  for (const [dtoClassName, dtoData] of Object.entries(dtoInfo)) {
    if (dtoClassName.includes('UpdateDto') && dtoData.baseClass && !dtoData.baseClass.includes('CreateDto')) {
      const createDtoName = Object.keys(dtoInfo).find(name => name.includes('CreateDto'));
      if (createDtoName) {
        // 修复继承关系
        const content = fs.readFileSync(dtoFile, 'utf-8');
        const regex = new RegExp(`(public\\s+class\\s+${dtoClassName}\\s*:\\s*)${dtoData.baseClass}`);
        const newContent = content.replace(regex, `$1${createDtoName}`);
        if (newContent !== content) {
          fs.writeFileSync(dtoFile, newContent, 'utf-8');
          console.log(`  ✅ 已修复: ${dtoClassName} 现在继承 ${createDtoName}`);
          inheritanceFixed++;
        }
      }
    }
  }
  
  if (inheritanceFixed > 0) {
    // 重新提取DTO信息
    const newDtoInfo = extractDtoFields(dtoFile);
    if (newDtoInfo) {
      Object.keys(dtoInfo).forEach(key => delete dtoInfo[key]);
      Object.assign(dtoInfo, newDtoInfo);
    }
    console.log(`  📝 共修复 ${inheritanceFixed} 个继承关系问题\n`);
  }
  
  // 检查一致性
  const result = checkConsistency(entityInfo, dtoInfo, entityName);
  
  // 输出结果
  let hasIssues = false;
  for (const dto of result.dtoFiles) {
    if (dto.missing.length > 0 || dto.extra.length > 0 || dto.typeMismatch.length > 0 || dto.nullableMismatch.length > 0 || dto.inheritanceIssue) {
      hasIssues = true;
      logAndReport(`\n  ${dto.name}:`);
      
      if (dto.inheritanceIssue) {
        logAndReport(`    ❌ 继承关系错误: 当前继承 ${dto.inheritanceIssue.current}，应该继承 ${dto.inheritanceIssue.expected}`);
      }
      
      if (dto.missing.length > 0) {
        logAndReport(`    ❌ 缺失字段 (${dto.missing.length}):`);
        dto.missing.forEach(f => logAndReport(`       - ${f.name} (${f.type})`));
      }
      
      if (dto.extra.length > 0) {
        logAndReport(`    ⚠️  多余字段 (${dto.extra.length}):`);
        dto.extra.forEach(f => logAndReport(`       - ${f.name} (${f.type})`));
      }
      
      if (dto.typeMismatch.length > 0) {
        logAndReport(`    ⚠️  类型不匹配 (${dto.typeMismatch.length}):`);
        dto.typeMismatch.forEach(m => logAndReport(`       - ${m.entity.name}: 实体=${m.entity.type}, DTO=${m.dto.type}`));
      }
      
      if (dto.nullableMismatch.length > 0) {
        logAndReport(`    ❌ 可空性错误 (${dto.nullableMismatch.length}):`);
        dto.nullableMismatch.forEach(m => logAndReport(`       - ${m.name}: ${m.currentType} -> ${m.expectedType} (${m.reason})`));
      }
    }
  }
  
  if (!hasIssues) {
    logAndReport(`  ✅ 所有DTO字段与实体完全一致`);
  }
  
  return result;
}

/**
 * 主函数
 */
function main() {
  console.log('========================================');
  console.log('  DTO字段一致性检查工具');
  console.log('========================================\n');
  
  // 第一步：统一DTO文件名为*Dtos.cs
  console.log('📝 步骤1: 统一DTO文件命名...');
  normalizeDtoFileNames();
  
  // 第二步：检查DTO字段一致性
  console.log('🔍 步骤2: 检查DTO字段一致性...\n');
  
  const entityFiles = [];
  
  // 查找实体文件
  function findEntities(dir) {
    const files = fs.readdirSync(dir);
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      
      if (stat.isDirectory()) {
        findEntities(fullPath);
      } else if (file.endsWith('.cs') && file.startsWith('Takt') && !file.includes('EntitiesSeedData')) {
        const content = fs.readFileSync(fullPath, 'utf-8');
        if (content.includes('TaktEntityBase')) {
          entityFiles.push(fullPath);
        }
      }
    }
  }
  
  findEntities(ENTITY_PATH);
  
  // 过滤目标实体
  const targetFiles = targetEntity 
    ? entityFiles.filter(f => path.basename(f, '.cs') === targetEntity)
    : entityFiles;
  
  if (targetFiles.length === 0) {
    console.log(`❌ 未找到实体: ${targetEntity}`);
    return;
  }
  
  logAndReport(`找到 ${targetFiles.length} 个实体文件\n`);
  
  // 处理每个实体
  let successCount = 0;
  let issueCount = 0;
  
  for (const entityFile of targetFiles) {
    const result = processEntity(entityFile);
    if (result.status === 'MISSING_DTO' || result.status === 'NO_DTO_CLASS') {
      issueCount++;
    } else if (result.dtoFiles?.some(d => d.missing.length > 0 || d.typeMismatch.length > 0)) {
      issueCount++;
    } else {
      successCount++;
    }
  }
  
  logAndReport('\n========================================');
  logAndReport(`  检查完成！一致: ${successCount}, 有问题: ${issueCount}`);
  logAndReport('========================================');
  
  // 生成报告文件
  if (generateReport && reportLines.length > 0) {
    const reportFile = path.join(__dirname, 'dto_check_report.txt');
    const reportContent = reportLines.join('\n');
    fs.writeFileSync(reportFile, reportContent, 'utf8');
    console.log(`\n📄 报告已生成: ${reportFile}`);
  }
}

// 运行
try {
  main();
} catch (error) {
  console.error('❌ 检查失败:', error.message);
  console.error(error.stack);
  process.exit(1);
}
