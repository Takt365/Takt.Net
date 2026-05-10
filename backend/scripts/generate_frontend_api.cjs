/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：generate_frontend_api.cjs
 * 创建时间：2026-05-03
 * 功能描述：根据后端控制器自动生成前端API文件
 * 使用方法：
 *   1. 生成单个控制器（默认）：node scripts/generate_frontend_api.cjs --controller TaktCostCenters
 *   2. 全量生成（谨慎）：node scripts/generate_frontend_api.cjs --all
 *   3. 预览模式：node scripts/generate_frontend_api.cjs --controller TaktCostCenters --dry-run
 * ========================================
 */

const fs = require('fs');
const path = require('path');
const { promisify } = require('util');

const readFileAsync = promisify(fs.readFile);
const writeFileAsync = promisify(fs.writeFile);
const mkdirAsync = promisify(fs.mkdir);

const CONTROLLER_PATH = path.join(__dirname, '../src/Takt.WebApi/Controllers');
const FRONTEND_API_PATH = path.join(__dirname, '../../frontend/takt.antd/src/api');

const EXCLUDED_CONTROLLERS = [
  'TaktControllerBase',  // 基类，不生成API
  'TaktCaches',  // 缓存管理控制器，特殊功能不生成前端API
  'TaktServerMonitors',  // 服务器监控控制器，特殊功能不生成前端API
];

// 注意：所有 *Engine 目录下的控制器（包括 TaktHealth、TaktAuths、TaktCaptchas）
// 会通过 isInEngineDirectory() 函数自动排除，无需静态维护列表

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
  console.error('  1. 生成单个实体：node scripts/generate_frontend_api.cjs --entity TaktEmployee');
  console.error('  2. 全量生成（谨慎）：node scripts/generate_frontend_api.cjs --all');
  console.error('  3. 预览模式：node scripts/generate_frontend_api.cjs --entity TaktEmployee --dry-run');
  console.error('');
  process.exit(1);
}

/**
 * 检查控制器是否在Engine目录或其子目录中
 */
function isInEngineDirectory(controllerPath) {
  const normalizedPath = controllerPath.replace(/\\/g, '/');
  // 匹配所有 Engine 目录（/Engine/ 或 /Engine 结尾）：不区分大小写
  return /\/\w*[Ee]ngine($|\/)/.test(normalizedPath);
}

function toKebabCase(pascal) {
  if (!pascal) return 'entity';
  return pascal.replace(/([a-z0-9])([A-Z])/g, '$1-$2').toLowerCase();
}

function toCamelCase(pascal) {
  if (!pascal) return 'entity';
  return pascal.charAt(0).toLowerCase() + pascal.slice(1);
}

function getEntityName(controllerName) {
  let name = controllerName.replace(/^Takt/, '').replace(/Controller$/, '');
  
  // 处理特殊不规则复数形式（优先于通用规则）
  const specialPlurals = {
    'Leaves': 'Leave',    // 请假（避免被 ves->fe 规则误处理）
    'Lives': 'Life',      // 生活/生命（例外情况）
  };
  
  if (specialPlurals[name]) {
    return specialPlurals[name];
  }
  
  // 处理常见不规则复数形式
  const irregularPlurals = {
    'ies': 'y',      // Families -> Family
    'ves': 'fe',     // Wives -> Wife (但 Lives -> Life 例外)
    'ses': 's',      // Classes -> Class
    'xes': 'x',      // Boxes -> Box
    'zes': 'z',      // Buzzes -> Buzz
    'ches': 'ch',    // Matches -> Match
    'shes': 'sh',    // Dishes -> Dish
  };
  
  // 先检查不规则复数
  for (const [suffix, replacement] of Object.entries(irregularPlurals)) {
    if (name.endsWith(suffix) && name.length > suffix.length) {
      return name.slice(0, -suffix.length) + replacement;
    }
  }
  
  // 常规情况：移除末尾的 s
  if (name.endsWith('s')) {
    name = name.slice(0, -1);
  }
  
  return name;
}

function getFrontendModulePath(controllerPath) {
  const relativePath = path.relative(CONTROLLER_PATH, controllerPath);
  const dirParts = relativePath.split(path.sep);
  const moduleParts = [];
  for (let i = 0; i < dirParts.length - 1; i++) {
    const part = dirParts[i];
    if (part && part !== 'Controllers') {
      moduleParts.push(toKebabCase(part));
    }
  }
  return moduleParts.join('/');
}

/**
 * 解析控制器，提取所有HTTP方法及其对应的C#方法
 */
function parseController(content, controllerName) {
  const entityName = getEntityName(controllerName);
  const info = {
    controllerName,
    entityName,
    namespace: '',
    route: '',
    methods: []
  };

  // 提取命名空间
  const namespaceMatch = content.match(/namespace\s+([^;]+);/);
  if (namespaceMatch) {
    info.namespace = namespaceMatch[1].trim();
  }

  // 提取路由
  const routeMatch = content.match(/\[Route\("([^"]+)"/);
  if (routeMatch) {
    const rawRoute = routeMatch[1].replace('\[controller\]', controllerName.replace(/Controller$/, ''));
    // 必须带前导斜杠
    info.route = rawRoute.startsWith('/') ? rawRoute : '/' + rawRoute;
  } else {
    info.route = `/api/${controllerName.replace(/Controller$/, '')}`;
  }

  // 逐行解析，找到每个HTTP属性对应的方法
  const lines = content.split('\n');
  
  for (let i = 0; i < lines.length; i++) {
    const line = lines[i].trim();
    
    // 匹配HTTP属性：[HttpGet("list")], [HttpPost], [HttpPut("{id}")] 等
    const httpMatch = line.match(/\[(HttpGet|HttpPost|HttpPut|HttpDelete|HttpPatch)(?:\("([^"]*)"\))?\]/);
    if (!httpMatch) continue;
    
    const httpMethodMap = {
      'HttpGet': 'get',
      'HttpPost': 'post',
      'HttpPut': 'put',
      'HttpDelete': 'delete',
      'HttpPatch': 'patch'
    };
    
    const httpMethod = httpMethodMap[httpMatch[1]];
    const routePart = httpMatch[2] || '';
    
    // 从HTTP属性行之后查找方法签名（最多查找20行）
    let methodName = '';
    let returnType = 'void';
    
    for (let j = i + 1; j < Math.min(i + 20, lines.length); j++) {
      const nextLine = lines[j].trim();
      
      // 跳过空行、注释、其他属性
      if (!nextLine || nextLine.startsWith('//') || nextLine.startsWith('[') || nextLine.startsWith('///')) {
        continue;
      }
      
      // 匹配方法签名：public async Task<...> MethodNameAsync(
      const methodMatch = nextLine.match(/public\s+async\s+Task.*?(\w+Async)\s*\(/);
      if (methodMatch) {
        methodName = methodMatch[1];
        
        // 提取返回类型（处理嵌套泛型）
        const taskIdx = nextLine.indexOf('Task<');
        if (taskIdx !== -1) {
          let depth = 1;
          let endIdx = taskIdx + 5;
          for (let k = endIdx; k < nextLine.length; k++) {
            if (nextLine[k] === '<') depth++;
            if (nextLine[k] === '>') {
              depth--;
              if (depth === 0) {
                endIdx = k;
                break;
              }
            }
          }
          returnType = nextLine.substring(taskIdx + 5, endIdx);
        }
        
        break;
      }
      
      // 遇到类定义或其他方法，停止查找
      if (nextLine.startsWith('public class') || nextLine.startsWith('private') || nextLine.startsWith('protected')) {
        break;
      }
    }
    
    if (methodName) {
      // 处理返回类型
      let frontendReturnType = returnType;
      
      // 剥莖 ActionResult<...>
      const actionResultIdx = frontendReturnType.indexOf('ActionResult<');
      if (actionResultIdx !== -1) {
        let depth = 1;
        let endIdx = actionResultIdx + 13;
        for (let k = endIdx; k < frontendReturnType.length; k++) {
          if (frontendReturnType[k] === '<') depth++;
          if (frontendReturnType[k] === '>') {
            depth--;
            if (depth === 0) {
              endIdx = k;
              break;
            }
          }
        }
        frontendReturnType = frontendReturnType.substring(actionResultIdx + 13, endIdx);
      }
      
      // 处理 TaktPagedResult<...>
      if (frontendReturnType.startsWith('TaktPagedResult<')) {
        const pagedIdx = frontendReturnType.indexOf('TaktPagedResult<');
        let depth = 1;
        let endIdx = pagedIdx + 16;
        for (let k = endIdx; k < frontendReturnType.length; k++) {
          if (frontendReturnType[k] === '<') depth++;
          if (frontendReturnType[k] === '>') {
            depth--;
            if (depth === 0) {
              endIdx = k;
              break;
            }
          }
        }
        const dtoName = frontendReturnType.substring(pagedIdx + 16, endIdx)
          .replace(/Dto$/, '')
          .replace(/^Takt/, '');
        frontendReturnType = `TaktPagedResult<${dtoName}>`;
      } else if (frontendReturnType === 'List<TaktSelectOption>') {
        frontendReturnType = 'TaktSelectOption[]';
      }
      
      const fullRoute = routePart ? `${info.route}/${routePart}` : info.route;
      
      info.methods.push({
        httpMethod,
        routePart,
        fullRoute,
        returnType,
        frontendReturnType,
        methodName,
        requestKey: (httpMethod === 'post' || httpMethod === 'put') ? 'data' : 'params'
      });
    }
  }

  return info;
}

/**
 * 根据控制器信息生成前端API文件
 */
function generateFrontendApiFile(controllerInfo) {
  const { controllerName, entityName, namespace, route, methods } = controllerInfo;
  const kebabEntityName = toKebabCase(entityName);
  const camelEntityName = toCamelCase(entityName);
  
  const namespaceParts = namespace.split('.').slice(3);
  const frontendNamespace = namespaceParts.map(part => toKebabCase(part)).join('/');
  
  // 动态构建需要的类型
  const neededTypes = new Set();
  if (methods.find(m => m.routePart === 'list' && m.httpMethod === 'get')) neededTypes.add(entityName + 'Query');
  if (methods.find(m => !m.routePart && m.httpMethod === 'post')) neededTypes.add(entityName + 'Create');
  if (methods.find(m => m.routePart === '{id}' && m.httpMethod === 'put')) neededTypes.add(entityName + 'Update');
  if (methods.find(m => m.routePart === 'status' && m.httpMethod === 'put')) neededTypes.add(entityName + 'Status');
  if (methods.find(m => m.routePart === 'sort' && m.httpMethod === 'put')) neededTypes.add(entityName + 'Sort');
  // Tree 相关：Tree 方法使用 {EntityName}Tree 类型
  if (methods.find(m => m.routePart === 'tree' && m.httpMethod === 'get')) neededTypes.add(entityName + 'Tree');
  
  // 添加 entityName 到 neededTypes
  neededTypes.add(entityName);
  
  // 动态构建类型数组
  const typeArray = Array.from(neededTypes);
  
  // 检查是否有 tree 方法，决定是否导入 TaktTreeSelectOption
  const hasTree = methods.some(m => m.routePart === 'tree' && m.httpMethod === 'get');
  const commonTypes = hasTree
    ? 'TaktPagedResult, TaktSelectOption, TaktTreeSelectOption'
    : 'TaktPagedResult, TaktSelectOption';
  let importStatements = `import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { ${commonTypes} } from '@/types/common'`;
  
  // 类型排序函数：Entity, Tree, Query, Create, Update, Status, Sort...
  const sortTypes = (arr) => {
    const order = {}
    const suffixOrder = ['Tree', 'Query', 'Create', 'Update', 'Status', 'Sort', 'Template', 'Import', 'Export']
    suffixOrder.forEach((s, i) => { order[s] = i + 1 })
    return [...arr].sort((a, b) => {
      const isAEntity = a === entityName
      const isBEntity = b === entityName
      if (isAEntity && !isBEntity) return -1
      if (!isAEntity && isBEntity) return 1
      const pa = order[a.slice(entityName.length)] ?? 999
      const pb = order[b.slice(entityName.length)] ?? 999
      return pa - pb || a.localeCompare(b)
    })
  }
  
  if (typeArray.length > 0) {
    const sortedTypes = sortTypes(typeArray)
    importStatements += `
import type {
  ${sortedTypes.join(',\n  ')}
} from '@/types/${frontendNamespace}/${kebabEntityName}'`;
  } else {
    importStatements += `
import type { ${entityName} } from '@/types/${frontendNamespace}/${kebabEntityName}'`;
  }
  
  const urlConstant = `const ${camelEntityName}Url = '${route}';`;
  const exportFunctions = [];

  // GetList
  const listMethod = methods.find(m => m.routePart === 'list' && m.httpMethod === 'get');
  if (listMethod) {
    exportFunctions.push(`
/**
 * 获取${entityName}列表（分页）
 * 对应后端：${listMethod.methodName}
 */
export function get${entityName}List(params: ${entityName}Query): Promise<${listMethod.frontendReturnType}> {
  return request({
    url: \`\${${camelEntityName}Url}/list\`,
    method: 'get',
    params
  })
}`);
  }

  // GetById
  const getByIdMethod = methods.find(m => m.routePart === '{id}' && m.httpMethod === 'get');
  if (getByIdMethod) {
    exportFunctions.push(`
/**
 * 根据ID获取${entityName}
 * 对应后端：${getByIdMethod.methodName}
 */
export function get${entityName}ById(id: string): Promise<${entityName}> {
  return request({
    url: \`\${${camelEntityName}Url}/\${id}\`,
    method: 'get'
  })
}`);
  }

  // GetOptions
  const getOptionsMethod = methods.find(m => m.routePart === 'options' && m.httpMethod === 'get');
  if (getOptionsMethod) {
    exportFunctions.push(`
/**
 * 获取${entityName}选项列表（用于下拉框等）
 * 对应后端：${getOptionsMethod.methodName}
 */
export function get${entityName}Options(): Promise<TaktSelectOption[]> {
  return request({
    url: \`\${${camelEntityName}Url}/options\`,
    method: 'get'
  })
}`);
  }
  
  // TreeOptions - 树形选项列表
  const treeOptionsMethod = methods.find(m => m.routePart === 'tree-options' && m.httpMethod === 'get');
  if (treeOptionsMethod) {
    exportFunctions.push(`
/**
 * 获取${entityName}树形选项列表（用于树形下拉框等）
 * 对应后端：${treeOptionsMethod.methodName}
 */
export function get${entityName}TreeOptions(): Promise<TaktTreeSelectOption[]> {
  return request({
    url: \`\${${camelEntityName}Url}/tree-options\`,
    method: 'get'
  })
}`);
  }
  
  // Tree - 树形列表
  const treeMethod = methods.find(m => m.routePart === 'tree' && m.httpMethod === 'get');
  if (treeMethod) {
    exportFunctions.push(`
/**
 * 获取${entityName}树形列表
 * 对应后端：${treeMethod.methodName}
 */
export function get${entityName}Tree(parentId: number = 0, includeDisabled: boolean = false): Promise<${entityName}Tree[]> {
  return request({
    url: \`\${${camelEntityName}Url}/tree\`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}`);
  }
  
  // Children - 子节点列表
  const childrenMethod = methods.find(m => m.routePart === 'children' && m.httpMethod === 'get');
  if (childrenMethod) {
    exportFunctions.push(`
/**
 * 获取${entityName}子节点列表
 * 对应后端：${childrenMethod.methodName}
 */
export function get${entityName}Children(parentId: number, includeDisabled: boolean = false): Promise<${entityName}[]> {
  return request({
    url: \`\${${camelEntityName}Url}/children\`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}`);
  }
  
  // Create
  const createMethod = methods.find(m => !m.routePart && m.httpMethod === 'post');
  if (createMethod) {
    exportFunctions.push(`
/**
 * 创建${entityName}
 * 对应后端：${createMethod.methodName}
 */
export function create${entityName}(data: ${entityName}Create): Promise<${entityName}> {
  return request({
    url: ${camelEntityName}Url,
    method: 'post',
    data
  })
}`);
  }

  // Update
  const updateMethod = methods.find(m => m.routePart === '{id}' && m.httpMethod === 'put');
  if (updateMethod) {
    exportFunctions.push(`
/**
 * 更新${entityName}
 * 对应后端：${updateMethod.methodName}
 */
export function update${entityName}(id: string, data: ${entityName}Update): Promise<${entityName}> {
  return request({
    url: \`\${${camelEntityName}Url}/\${id}\`,
    method: 'put',
    data
  })
}`);
  }

  // DeleteById
  const deleteByIdMethod = methods.find(m => m.routePart === '{id}' && m.httpMethod === 'delete');
  if (deleteByIdMethod) {
    exportFunctions.push(`
/**
 * 删除${entityName}（单条）
 * 对应后端：${deleteByIdMethod.methodName}
 */
export function delete${entityName}ById(id: string): Promise<void> {
  return request({
    url: \`\${${camelEntityName}Url}/\${id}\`,
    method: 'delete'
  })
}`);
  }

  // DeleteBatch
  const deleteBatchMethod = methods.find(m => m.routePart === 'batch' && m.httpMethod === 'delete');
  if (deleteBatchMethod) {
    exportFunctions.push(`
/**
 * 批量删除${entityName}
 * 对应后端：${deleteBatchMethod.methodName}
 */
export function delete${entityName}Batch(ids: string[]): Promise<void> {
  return request({
    url: \`\${${camelEntityName}Url}/batch\`,
    method: 'delete',
    data: ids
  })
}`);
  }

  // UpdateStatus
  const updateStatusMethod = methods.find(m => m.routePart === 'status' && m.httpMethod === 'put');
  if (updateStatusMethod) {
    exportFunctions.push(`
/**
 * 更新${entityName}状态
 * 对应后端：${updateStatusMethod.methodName}
 */
export function update${entityName}Status(data: ${entityName}Status): Promise<${entityName}Status> {
  return request({
    url: \`\${${camelEntityName}Url}/status\`,
    method: 'put',
    data
  })
}`);
  }

  // UpdateSort
  const updateSortMethod = methods.find(m => m.routePart === 'sort' && m.httpMethod === 'put');
  if (updateSortMethod) {
    exportFunctions.push(`
/**
 * 更新${entityName}排序
 * 对应后端：${updateSortMethod.methodName}
 */
export function update${entityName}Sort(data: ${entityName}Sort): Promise<${entityName}Sort> {
  return request({
    url: \`\${${camelEntityName}Url}/sort\`,
    method: 'put',
    data
  })
}`);
  }

  // GetTemplate
  const getTemplateMethod = methods.find(m => m.routePart === 'template' && m.httpMethod === 'get');
  if (getTemplateMethod) {
    exportFunctions.push(`
/**
 * 获取导入模板
 * 对应后端：${getTemplateMethod.methodName}；fileName 仅传名称不含后缀
 */
export function get${entityName}Template(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: \`\${${camelEntityName}Url}/template\`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}`);
  }

  // Import
  const importMethod = methods.find(m => m.routePart === 'import' && m.httpMethod === 'post');
  if (importMethod) {
    exportFunctions.push(`
/**
 * 导入${entityName}
 * 对应后端：${importMethod.methodName}
 */
export function import${entityName}Data(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: \`\${${camelEntityName}Url}/import\`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}`);
  }

  // Export
  const exportMethod = methods.find(m => m.routePart === 'export' && m.httpMethod === 'post');
  if (exportMethod) {
    exportFunctions.push(`
/**
 * 导出${entityName}
 * 对应后端：${exportMethod.methodName}；fileName 仅传名称不含后缀
 */
export function export${entityName}Data(query: ${entityName}Query, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: \`\${${camelEntityName}Url}/export\`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}`);
  }
  
  return `// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/${frontendNamespace}
// 文件名称：${kebabEntityName}.ts
// 功能描述：${entityName} API，对应后端 ${namespace}.${controllerName}
// ========================================

${importStatements}

// ========================================
// ${entityName}相关 API（按后端控制器顺序）
// ========================================
${urlConstant}
${exportFunctions.join('\n')}
`;
}

/**
 * 主函数
 */
async function main() {
  console.log('========================================');
  console.log('  前端 API 生成工具');
  console.log('========================================\n');
  
  if (dryRun) {
    console.log('📋 模式: 预览模式（不会写入文件）\n');
  }
  
  const controllerFiles = [];
  
  function collectControllerFiles(dirPath) {
    const files = fs.readdirSync(dirPath);
    for (const file of files) {
      const filePath = path.join(dirPath, file);
      const stat = fs.statSync(filePath);
      
      if (stat.isDirectory()) {
        // 排除 Engine 目录
        if (isInEngineDirectory(filePath)) {
          continue;
        }
        collectControllerFiles(filePath);
      } else if (file.endsWith('Controller.cs') && !file.endsWith('ControllerBase.cs')) {
        const controllerName = file.replace(/Controller\.cs$/, '');
        // 排除列表检查 + Engine目录动态排除
        if (!EXCLUDED_CONTROLLERS.includes(controllerName) && !isInEngineDirectory(filePath)) {
          // 将实体名转换为控制器名（TaktEmployee -> TaktEmployees）
          const expectedControllerName = targetEntity ? targetEntity.replace('Takt', '') + 's' : null;
          // 只处理指定的控制器（除非使用 --all）
          if (generateAll || !targetEntity || controllerName === expectedControllerName) {
            controllerFiles.push({ path: filePath, name: controllerName });
          }
        }
      }
    }
  }
  
  console.log('🔍 扫描控制器文件...');
  collectControllerFiles(CONTROLLER_PATH);
  
  if (controllerFiles.length === 0) {
    console.log('❌ 未找到符合条件的控制器文件');
    process.exit(1);
  }
  
  console.log(`✅ 找到 ${controllerFiles.length} 个控制器\n`);
  console.log('----------------------------------------');
  
  let successCount = 0;
  let failCount = 0;
  
  for (const controllerFile of controllerFiles) {
    try {
      console.log(`\n🔍 处理: ${controllerFile.name}`);
      
      const content = await readFileAsync(controllerFile.path, 'utf8');
      const controllerInfo = parseController(content, controllerFile.name);
      console.log(`   📊 解析到 ${controllerInfo.methods.length} 个方法`);
      
      if (controllerInfo.methods.length > 0) {
        console.log(`   📝 方法列表:`);
        controllerInfo.methods.forEach(m => {
          console.log(`      - ${m.methodName} [${m.httpMethod.toUpperCase()} ${m.routePart || '(root)'}]`);
        });
      }
      
      const apiContent = generateFrontendApiFile(controllerInfo);
      
      const frontendModulePath = getFrontendModulePath(controllerFile.path);
      const frontendApiDir = path.join(FRONTEND_API_PATH, frontendModulePath);
      const frontendApiFile = path.join(frontendApiDir, `${toKebabCase(controllerInfo.entityName)}.ts`);
      
      if (dryRun) {
        console.log(`   📄 预览路径: ${frontendApiFile}`);
        console.log(`   📏 生成大小: ${apiContent.length} 字节`);
        console.log(`   📝 前3行预览:`);
        const firstLines = apiContent.split('\n').slice(0, 3).map(l => `      ${l}`);
        console.log(firstLines.join('\n'));
        successCount++;
      } else {
        await mkdirAsync(frontendApiDir, { recursive: true });
        await writeFileAsync(frontendApiFile, apiContent);
        console.log(`   ✅ 已生成: ${frontendApiFile}`);
        successCount++;
      }
    } catch (error) {
      console.error(`   ❌ 错误: ${error.message}`);
      failCount++;
    }
  }
  
  console.log('\n----------------------------------------');
  console.log('📊 生成统计:');
  console.log(`   ✅ 成功: ${successCount}`);
  console.log(`   ❌ 失败: ${failCount}`);
  console.log(`   📁 总计: ${controllerFiles.length}`);
  console.log('----------------------------------------');
  console.log('\n========================================');
  console.log('  生成完成！');
  console.log('========================================');
}

if (require.main === module) {
  main();
}

module.exports = { main, parseController, generateFrontendApiFile };
