const fs = require('fs');
const path = require('path');

const typesDir = path.join(__dirname, 'frontend/takt.antd/src/types');
const results = [];

function checkQueryInterface(filePath) {
  const content = fs.readFileSync(filePath, 'utf8');
  const lines = content.split('\n');
  
  // 查找所有 extends TaktPagedQuery 的接口
  let inQueryInterface = false;
  let interfaceName = '';
  let braceCount = 0;
  let issues = [];
  let startLine = 0;
  
  for (let i = 0; i < lines.length; i++) {
    const line = lines[i];
    const trimmedLine = line.trim();
    
    // 检测 Query 接口开始
    const interfaceMatch = trimmedLine.match(/export\s+interface\s+(\w*Query\w*)\s+extends\s+TaktPagedQuery/);
    if (interfaceMatch) {
      inQueryInterface = true;
      interfaceName = interfaceMatch[1];
      issues = [];
      braceCount = 0;
      startLine = i + 1;
    }
    
    if (inQueryInterface) {
      // 计算大括号
      for (const char of line) {
        if (char === '{') braceCount++;
        if (char === '}') braceCount--;
      }
      
      // 检测接口结束
      if (braceCount === 0 && line.includes('}')) {
        if (issues.length > 0) {
          results.push({
            file: path.relative(__dirname, filePath),
            interface: interfaceName,
            startLine,
            issues
          });
        }
        inQueryInterface = false;
        continue;
      }
      
      // 在 Query 接口中查找非空字符串属性
      // 匹配: propertyName: string  但不匹配: propertyName?: string 或 propertyName: string | undefined
      if (trimmedLine && !trimmedLine.startsWith('//') && !trimmedLine.startsWith('*') && !trimmedLine.startsWith('/**')) {
        const propMatch = trimmedLine.match(/^(\w+):\s*string\s*[;]?$/);
        if (propMatch && !trimmedLine.includes('?:') && !trimmedLine.includes('string |') && !trimmedLine.includes('undefined')) {
          issues.push({
            line: i + 1,
            property: propMatch[1],
            code: trimmedLine
          });
        }
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
    } else if (file.endsWith('.d.ts') || file.endsWith('.ts')) {
      checkQueryInterface(fullPath);
    }
  });
}

scanDirectory(typesDir);

// 输出结果
if (results.length === 0) {
  console.log('✅ 所有前端 Query 接口的字符串字段都是可选的 (propertyName?: string)');
} else {
  console.log(`❌ 发现 ${results.length} 个 Query 接口存在必填字符串字段：\n`);
  
  results.forEach(({ file, interface: interfaceName, startLine, issues }) => {
    console.log(`📄 ${file} (从第 ${startLine} 行开始)`);
    console.log(`   接口: ${interfaceName}`);
    issues.forEach(({ line, property, code }) => {
      console.log(`   ⚠️  第 ${line} 行: ${property}`);
      console.log(`      ${code}`);
    });
    console.log('');
  });
  
  console.log(`\n总计需要修复: ${results.reduce((sum, r) => sum + r.issues.length, 0)} 个字段`);
}
