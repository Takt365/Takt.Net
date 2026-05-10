/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：register_seed_data.cjs
 * 创建时间：2026-02-21
 * 功能描述：自动注册新生成的实体种子数据到TaktSeedsCollectionExtensions.cs
 *   1. 扫描SeedI18nData目录下所有*EntitiesSeedData.cs文件
 *   2. 检查是否已在TaktSeedsCollectionExtensions.cs中注册
 *   3. 自动添加缺失的注册代码
 *   4. 保持using语句和注册代码的有序性
 * 
 * 使用方法：
 *   1. 生成种子文件后运行：node scripts/register_seed_data.cjs
 *   2. 自动扫描并注册所有未注册的种子数据类
 *   3. 同时更新ContainerBuilder和IServiceCollection两个方法
 * 
 * 注意事项：
 *   - 备份原文件到TaktSeedsCollectionExtensions.cs.bak
 *   - 按照命名空间分组添加using语句
 *   - 注册代码按类名字母顺序插入
 * ========================================
 */

const fs = require('fs');
const path = require('path');

const SEED_PATH = path.join(__dirname, '../src/Takt.Infrastructure/Data/Seeds/SeedI18nData');
const EXTENSIONS_FILE = path.join(__dirname, '../src/Takt.Infrastructure/Extensions/TaktSeedsCollectionExtensions.cs');
const BACKUP_FILE = EXTENSIONS_FILE + '.bak';

/**
 * 扫描所有种子数据类
 */
function scanSeedDataClasses() {
  const seedClasses = [];
  
  // 扫描 SeedI18nData 目录（实体翻译种子）
  function scanSeedI18nData(dir, namespace) {
    const files = fs.readdirSync(dir);
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      
      if (stat.isDirectory()) {
        const subNamespace = namespace ? `${namespace}.${file}` : file;
        scanSeedI18nData(fullPath, subNamespace);
      } else if (file.endsWith('EntitiesSeedData.cs')) {
        const className = file.replace('.cs', '');
        const relPath = path.relative(SEED_PATH, dir);
        const ns = `Takt.Infrastructure.Data.Seeds.SeedI18nData.${relPath.replace(/\\/g, '.')}`;
        seedClasses.push({ className, namespace: ns, hasUsing: true });
      }
    }
  }
  
  scanSeedI18nData(SEED_PATH, '');
  
  // 扫描 SeedData 目录（基础数据种子）
  const SEEDDATA_PATH = path.join(__dirname, '../src/Takt.Infrastructure/Data/Seeds/SeedData');
  if (fs.existsSync(SEEDDATA_PATH)) {
    const seedDataFiles = fs.readdirSync(SEEDDATA_PATH);
    for (const file of seedDataFiles) {
      if (file.endsWith('SeedData.cs') && !file.endsWith('EntitiesSeedData.cs')) {
        const className = file.replace('.cs', '');
        
        // 排除菜单层级类（它们被TaktMenuSeedData统一协调，不需要单独注册）
        if (className.match(/^TaktMenu(Level[1-5]|Button)SeedData$/)) {
          continue;
        }
        
        seedClasses.push({ 
          className, 
          namespace: 'Takt.Infrastructure.Data.Seeds.SeedData',
          hasUsing: false  // SeedData命名空间已经在基础using中
        });
      }
    }
  }
  
  // 扫描根 Seeds 目录（其他种子）
  const ROOT_SEEDS_PATH = path.join(__dirname, '../src/Takt.Infrastructure/Data/Seeds');
  if (fs.existsSync(ROOT_SEEDS_PATH)) {
    const rootFiles = fs.readdirSync(ROOT_SEEDS_PATH);
    for (const file of rootFiles) {
      if (file.endsWith('SeedData.cs') && !file.includes('EntitiesSeedData')) {
        const className = file.replace('.cs', '');
        // 排除接口和抽象类
        if (className.startsWith('ITakt') || className.startsWith('TaktSeedDataBase')) {
          continue;
        }
        seedClasses.push({ 
          className, 
          namespace: 'Takt.Infrastructure.Data.Seeds',
          hasUsing: false
        });
      }
    }
  }
  
  return seedClasses.sort((a, b) => a.className.localeCompare(b.className));
}

/**
 * 读取并解析扩展文件
 */
function parseExtensionsFile() {
  const content = fs.readFileSync(EXTENSIONS_FILE, 'utf-8');
  
  // 提取using语句
  const usingRegex = /using\s+([^;]+);/g;
  const usings = [];
  let match;
  while ((match = usingRegex.exec(content)) !== null) {
    usings.push(match[1]);
  }
  
  // 提取已注册的种子类（支持带命名空间和不带命名空间两种情况）
  const registerRegex = /(?:builder\.RegisterType<|services\.AddScoped<ITaktSeedData,\s*)(?:Takt\.Infrastructure\.Data\.Seeds\.SeedI18nData\.[\w.]+\.)?(\w+EntitiesSeedData)[>\(]/g;
  const registeredClasses = new Set();
  while ((match = registerRegex.exec(content)) !== null) {
    const className = match[1];
    if (className) registeredClasses.add(className);
  }
  
  return { content, usings, registeredClasses };
}

/**
 * 生成using语句
 */
function generateUsings(seedClasses, existingUsings) {
  const namespaces = [...new Set(seedClasses.map(s => s.namespace))];
  const newUsings = namespaces.filter(ns => !existingUsings.includes(ns));
  
  if (newUsings.length === 0) return '';
  
  return newUsings
    .sort()
    .map(ns => `using ${ns};`)
    .join('\n') + '\n';
}

/**
 * 生成ContainerBuilder注册代码
 */
function generateContainerBuilderRegistrations(seedClasses, registeredClasses) {
  const newClasses = seedClasses.filter(s => !registeredClasses.has(s.className));
  
  if (newClasses.length === 0) return '';
  
  const lines = newClasses.map(s => 
    `        builder.RegisterType<${s.namespace}.${s.className}>().As<ITaktSeedData>().InstancePerLifetimeScope();`
  );
  
  return lines.join('\n') + '\n';
}

/**
 * 生成IServiceCollection注册代码
 */
function generateServiceCollectionRegistrations(seedClasses, registeredClasses) {
  const newClasses = seedClasses.filter(s => !registeredClasses.has(s.className));
  
  if (newClasses.length === 0) return '';
  
  const lines = newClasses.map(s => 
    `        services.AddScoped<ITaktSeedData, ${s.namespace}.${s.className}>();`
  );
  
  return lines.join('\n') + '\n';
}

/**
 * 更新扩展文件
 */
function updateExtensionsFile() {
  console.log('========================================');
  console.log('  自动注册实体种子数据');
  console.log('========================================\n');
  
  // 备份原文件
  fs.copyFileSync(EXTENSIONS_FILE, BACKUP_FILE);
  console.log(`✅ 已备份: ${BACKUP_FILE}\n`);
  
  // 扫描种子类
  const seedClasses = scanSeedDataClasses();
  console.log(`找到 ${seedClasses.length} 个种子数据类\n`);
  
  // 读取原文件
  let content = fs.readFileSync(EXTENSIONS_FILE, 'utf-8');
  
  // 提取现有using语句（不包括SeedI18nData子命名空间）
  const usingRegex = /using\s+([^;]+);/g;
  const existingUsings = [];
  let match;
  while ((match = usingRegex.exec(content)) !== null) {
    const ns = match[1];
    // 保留非SeedI18nData子命名空间的using
    if (!ns.startsWith('Takt.Infrastructure.Data.Seeds.SeedI18nData.') || ns === 'Takt.Infrastructure.Data.Seeds.SeedI18nData') {
      existingUsings.push(ns);
    }
  }
  
  // 收集所有需要的命名空间（只添加hasUsing: true的）
  const neededNamespaces = [...new Set(seedClasses.filter(s => s.hasUsing).map(s => s.namespace))];
  const allUsings = [...existingUsings, ...neededNamespaces].sort();
  
  // 生成新的using部分
  const usingSection = allUsings.map(ns => `using ${ns};`).join('\n');
  
  // 替换using部分（从第一个using到namespace之前的所有using）
  const firstUsingIndex = content.indexOf('using ');
  const namespaceIndex = content.indexOf('\nnamespace ');
  content = content.slice(0, firstUsingIndex) + usingSection + content.slice(namespaceIndex);
  
  // 生成所有ContainerBuilder注册代码（按正确顺序）
  // 1. 基础数据种子（有Order值，有依赖关系）
  // 2. 实体翻译种子（Order=999，在最后）
  const orderedSeedClasses = seedClasses.sort((a, b) => {
    // 实体翻译种子（EntitiesSeedData）排在最后
    const aIsEntity = a.className.endsWith('EntitiesSeedData');
    const bIsEntity = b.className.endsWith('EntitiesSeedData');
    
    if (aIsEntity && !bIsEntity) return 1;  // a是实体翻译，b不是，a排后
    if (!aIsEntity && bIsEntity) return -1; // a不是实体翻译，b是，a排前
    
    // 同类别内按字母顺序
    return a.className.localeCompare(b.className);
  });
  
  const containerBuilderLines = orderedSeedClasses.map(s => {
    const fullClassName = s.hasUsing ? s.className : `${s.namespace}.${s.className}`;
    return `        builder.RegisterType<${fullClassName}>().As<ITaktSeedData>().InstancePerLifetimeScope();`;
  });
  const containerBuilderCode = containerBuilderLines.join('\n');
  
  // 生成所有IServiceCollection注册代码（同样顺序）
  const serviceCollectionLines = orderedSeedClasses.map(s => {
    const fullClassName = s.hasUsing ? s.className : `${s.namespace}.${s.className}`;
    return `        services.AddScoped<ITaktSeedData, ${fullClassName}>();`;
  });
  const serviceCollectionCode = serviceCollectionLines.join('\n');
  
  // 替换ContainerBuilder方法中的注册代码
  const addTaktSeedsContainerStart = content.indexOf('    public static ContainerBuilder AddTaktSeeds(this ContainerBuilder builder)');
  const addTaktSeedsContainerEnd = content.indexOf('        return builder;', addTaktSeedsContainerStart);
  const containerMethodBodyStart = content.indexOf('{', addTaktSeedsContainerStart) + 1;
  content = content.slice(0, containerMethodBodyStart) + '\n        // 注册所有种子数据提供者\n' + containerBuilderCode + '\n\n' + content.slice(addTaktSeedsContainerEnd);
  
  // 替换IServiceCollection方法中的注册代码
  const addTaktSeedsServiceStart = content.indexOf('    public static IServiceCollection AddTaktSeeds(this IServiceCollection services)');
  const addTaktSeedsServiceEnd = content.indexOf('        return services;', addTaktSeedsServiceStart);
  const serviceMethodBodyStart = content.indexOf('{', addTaktSeedsServiceStart) + 1;
  content = content.slice(0, serviceMethodBodyStart) + '\n        // 注册所有种子数据提供者\n' + serviceCollectionCode + '\n\n' + content.slice(addTaktSeedsServiceEnd);
  
  // 写入文件
  fs.writeFileSync(EXTENSIONS_FILE, content, 'utf-8');
  console.log('========================================');
  console.log(`  注册完成！共 ${seedClasses.length} 个种子数据类`);
  console.log('========================================');
  console.log(`\n📄 已更新: ${EXTENSIONS_FILE}`);
  console.log(`💾 备份文件: ${BACKUP_FILE}`);
}

// 运行
try {
  updateExtensionsFile();
} catch (error) {
  console.error('❌ 更新失败:', error.message);
  console.error(error.stack);
  process.exit(1);
}
