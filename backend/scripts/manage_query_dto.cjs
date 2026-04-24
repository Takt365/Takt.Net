/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：manage_query_dto.cjs
 * 创建时间：2026-02-05
 * 功能描述：一体化管理后端QueryDto类的字符串字段可空性
 *   1. 检查：查找所有非空字符串字段（string）
 *   2. 修改：自动修复为可空字符串（string?）
 *   3. 验证：确认所有字段都已修复
 * 重要性：确保前后端契约一致性，避免400错误
 * 
 * 使用方法：
 *   1. 前后端契约治理：修改DTO后运行，确保QueryDto字段都是可空的
 *   2. 命令：cd backend/scripts && node manage_query_dto.cjs
 *   3. 执行流程：
 *      阶段1 - 检查所有QueryDto的非空字符串字段
 *      阶段2 - 自动修复（string → string?）
 *      阶段3 - 全量验证修复结果
 * 
 * 注意事项：
 *   - 前端类型文件是自动生成的，不需要单独检查
 *   - 后端DTO修改后，前端会通过其它脚本自动更新
 *   - 所有QueryDto的字符串字段必须是string?（可空）
 *   - 避免前端传null值导致后端400错误
 * ========================================
 */

const fs = require('fs');
const path = require('path');

const dtosDir = path.join(__dirname, '../src/Takt.Application/Dtos');

/**
 * 查找所有QueryDto文件
 * @param {string} dir - 目录路径
 * @returns {string[]} 文件路径数组
 */
function findQueryDtoFiles(dir) {
  const queryDtoFiles = [];
  
  function scan(currentDir) {
    const files = fs.readdirSync(currentDir);
    for (const file of files) {
      const fullPath = path.join(currentDir, file);
      const stat = fs.statSync(fullPath);
      
      if (stat.isDirectory()) {
        scan(fullPath);
      } else if (file.endsWith('.cs')) {
        const content = fs.readFileSync(fullPath, 'utf8');
        // 只处理包含QueryDto且继承TaktPagedQuery的文件
        if (content.includes('QueryDto') && content.includes('TaktPagedQuery')) {
          queryDtoFiles.push(fullPath);
        }
      }
    }
  }
  
  scan(dir);
  return queryDtoFiles;
}

/**
 * 检查单个QueryDto文件中的非空字符串字段
 * @param {string} filePath - 文件路径
 * @returns {Array} 问题字段列表
 */
function checkQueryDto(filePath) {
  const content = fs.readFileSync(filePath, 'utf8');
  const lines = content.split('\n');
  
  const issues = [];
  let inQueryDtoClass = false;
  let className = '';
  
  for (let i = 0; i < lines.length; i++) {
    const line = lines[i].trim();
    
    // 检测 QueryDto 类开始
    const classMatch = line.match(/public\s+class\s+(\w+QueryDto)\s*:\s*TaktPagedQuery/);
    if (classMatch) {
      inQueryDtoClass = true;
      className = classMatch[1];
      continue;
    }
    
    // 检测类结束
    if (inQueryDtoClass && line === '}') {
      inQueryDtoClass = false;
      continue;
    }
    
    // 在 QueryDto 类中查找非空字符串属性
    if (inQueryDtoClass) {
      // 匹配: public string PropertyName { get; set; }
      // 或: public string PropertyName { get; set; } = string.Empty;
      // 但不匹配: public string? PropertyName { get; set; }
      const propMatch = line.match(/public\s+string\s+(\w+)\s*\{\s*get;\s*set;\s*\}/);
      const propMatch2 = line.match(/public\s+string\s+(\w+)\s*\{\s*get;\s*set;\s*\}\s*=\s*string\.Empty;/);
      
      if (propMatch && !line.includes('string?')) {
        issues.push({
          line: i + 1,
          property: propMatch[1],
          code: line
        });
      } else if (propMatch2 && !line.includes('string?')) {
        issues.push({
          line: i + 1,
          property: propMatch2[1],
          code: line
        });
      }
    }
  }
  
  return { className, issues };
}

/**
 * 修复QueryDto文件中的非空字符串字段
 * @param {string} filePath - 文件路径
 * @param {Array} issues - 问题字段列表
 * @returns {number} 修复数量
 */
function fixQueryDto(filePath, issues) {
  if (issues.length === 0) return 0;
  
  let content = fs.readFileSync(filePath, 'utf8');
  let fixCount = 0;
  
  // 按行号倒序处理，避免行号偏移
  const sortedIssues = [...issues].sort((a, b) => b.line - a.line);
  
  for (const issue of sortedIssues) {
    const lineIndex = issue.line - 1;
    const lines = content.split('\n');
    const originalLine = lines[lineIndex];
    
    // 修复: string -> string?
    let fixedLine = originalLine.replace(
      /public\s+string\s+(\w+)/,
      'public string? $1'
    );
    
    if (fixedLine !== originalLine) {
      lines[lineIndex] = fixedLine;
      fixCount++;
    }
  }
  
  content = lines.join('\n');
  fs.writeFileSync(filePath, content, 'utf8');
  
  return fixCount;
}

/**
 * 验证QueryDto文件
 * @param {string} filePath - 文件路径
 * @returns {boolean} 是否通过验证
 */
function verifyQueryDto(filePath) {
  const { issues } = checkQueryDto(filePath);
  return issues.length === 0;
}

/**
 * 主函数
 */
function main() {
  console.log('========================================');
  console.log('  QueryDto字符串字段一体化管理工具');
  console.log('========================================\n');

  // 查找所有QueryDto文件
  const queryDtoFiles = findQueryDtoFiles(dtosDir);
  console.log(`找到 ${queryDtoFiles.length} 个QueryDto文件\n`);

  // ========== 阶段1: 检查 ==========
  console.log('【阶段1】检查非空字符串字段');
  console.log('----------------------------------------');
  
  const allIssues = [];
  let totalIssues = 0;

  for (const filePath of queryDtoFiles) {
    const { className, issues } = checkQueryDto(filePath);
    
    if (issues.length > 0) {
      const relativePath = path.relative(path.join(__dirname, '..'), filePath);
      allIssues.push({ filePath, className, issues, relativePath });
      totalIssues += issues.length;
      
      console.log(`  ⚠️  ${className}: ${issues.length} 个非空字段`);
      issues.forEach(issue => {
        console.log(`     第 ${issue.line} 行: ${issue.property}`);
      });
    }
  }

  if (totalIssues === 0) {
    console.log('  ✅ 所有QueryDto的字符串字段都是可空的 (string?)');
    console.log('\n========================================');
    console.log('  无需修复，验证通过！');
    console.log('========================================');
    return;
  }

  console.log(`\n  检查完成: 共发现 ${totalIssues} 个非空字段\n`);

  // ========== 阶段2: 修改 ==========
  console.log('【阶段2】自动修复非空字段');
  console.log('----------------------------------------');
  
  let totalFixed = 0;

  for (const { filePath, className, issues } of allIssues) {
    const fixedCount = fixQueryDto(filePath, issues);
    totalFixed += fixedCount;
    
    if (fixedCount > 0) {
      console.log(`  ✅ ${className}: 修复 ${fixedCount} 个字段`);
    }
  }

  console.log(`\n  修复完成: 共修复 ${totalFixed} 个字段\n`);

  // ========== 阶段3: 验证 ==========
  console.log('【阶段3】验证修复结果');
  console.log('----------------------------------------');
  
  let passCount = 0;
  let failCount = 0;
  const failedFiles = [];

  for (const { filePath, className, relativePath } of allIssues) {
    const isPassed = verifyQueryDto(filePath);
    
    if (isPassed) {
      passCount++;
    } else {
      failCount++;
      failedFiles.push({ className, relativePath });
      console.log(`  ❌ ${className}: 验证失败`);
    }
  }

  console.log(`\n  验证完成: ${passCount} 个通过, ${failCount} 个失败\n`);

  if (failCount > 0) {
    console.log('  失败详情:');
    failedFiles.forEach((item, index) => {
      console.log(`    ${index + 1}. ${item.className}`);
      console.log(`       文件: ${item.relativePath}`);
    });
    console.log('');
  }

  // ========== 总结 ==========
  console.log('========================================');
  console.log('  全部完成！');
  console.log('========================================');
  console.log(`  检查发现: ${totalIssues} 个非空字段`);
  console.log(`  自动修复: ${totalFixed} 个字段`);
  console.log(`  验证结果: ${passCount}/${allIssues.length} 个文件通过`);
  
  if (failCount === 0) {
    console.log('  ✅ 所有QueryDto的字符串字段都是可空的 (string?)');
  } else {
    console.log(`  ⚠️  发现 ${failCount} 个文件验证失败，请检查上方详情！`);
  }
  console.log('========================================');
}

// 运行
main();
