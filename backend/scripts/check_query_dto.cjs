const fs = require('fs');
const path = require('path');

const dtosDir = path.join(__dirname, 'backend/src/Takt.Application/Dtos');
const results = [];

function checkQueryDto(filePath) {
  const content = fs.readFileSync(filePath, 'utf8');
  const lines = content.split('\n');
  
  // 查找所有 QueryDto 类
  let inQueryDtoClass = false;
  let className = '';
  let issues = [];
  
  for (let i = 0; i < lines.length; i++) {
    const line = lines[i].trim();
    
    // 检测 QueryDto 类开始
    const classMatch = line.match(/public\s+class\s+(\w+QueryDto)\s*:\s*TaktPagedQuery/);
    if (classMatch) {
      inQueryDtoClass = true;
      className = classMatch[1];
      issues = [];
      continue;
    }
    
    // 检测类结束
    if (inQueryDtoClass && line === '}') {
      if (issues.length > 0) {
        results.push({
          file: path.relative(__dirname, filePath),
          class: className,
          issues
        });
      }
      inQueryDtoClass = false;
      continue;
    }
    
    // 在 QueryDto 类中查找非空字符串属性
    if (inQueryDtoClass) {
      // 匹配: public string PropertyName { get; set; } 或 public string PropertyName { get; set; } = string.Empty;
      // 但不匹配: public string? PropertyName { get; set; }
      const propMatch = line.match(/public\s+string\s+(\w+)\s*{\s*get;\s*set;\s*}/);
      const propMatch2 = line.match(/public\s+string\s+(\w+)\s*{\s*get;\s*set;\s*}\s*=\s*string\.Empty;/);
      
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
}

function scanDirectory(dir) {
  const files = fs.readdirSync(dir);
  
  files.forEach(file => {
    const fullPath = path.join(dir, file);
    const stat = fs.statSync(fullPath);
    
    if (stat.isDirectory()) {
      scanDirectory(fullPath);
    } else if (file.endsWith('.cs')) {
      checkQueryDto(fullPath);
    }
  });
}

scanDirectory(dtosDir);

// 输出结果
if (results.length === 0) {
  console.log('✅ 所有 QueryDto 的字符串字段都是可空的 (string?)');
} else {
  console.log(`❌ 发现 ${results.length} 个 QueryDto 类存在非空字符串字段：\n`);
  
  results.forEach(({ file, class: className, issues }) => {
    console.log(`📄 ${file}`);
    console.log(`   类名: ${className}`);
    issues.forEach(({ line, property, code }) => {
      console.log(`   ⚠️  第 ${line} 行: ${property}`);
      console.log(`      ${code.trim()}`);
    });
    console.log('');
  });
  
  console.log(`\n总计需要修复: ${results.reduce((sum, r) => sum + r.issues.length, 0)} 个字段`);
}
