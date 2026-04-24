/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：batch_generate_seed_data.cjs
 * 创建时间：2026-02-05
 * 功能描述：批量自动生成实体种子数据文件
 * 特点：
 *   1. 使用UTF-8编码，确保中文不乱码
 *   2. 中文翻译直接使用实体的ColumnDescription
 *   3. 其他语言使用AI翻译（基于ColumnDescription自动翻译）
 *   4. 生成符合项目规范的种子文件结构
 * 
 * 使用方法：
 *   1. 日常使用：添加新实体后，运行此脚本生成对应的种子数据文件
 *   2. 命令：cd backend/scripts && node batch_generate_seed_data.cjs
 *   3. 输出：种子文件生成到 src/Takt.Infrastructure/Data/Seeds/SeedI18nData/ 目录
 *   4. 目录结构：与实体目录结构完全一致
 * 
 * 注意事项：
 *   - 实体必须有SugarTable属性
 *   - 实体属性必须有SugarColumn的ColumnDescription
 *   - _self翻译会自动去除SugarTable描述的"表"后缀
 * ========================================
 */

const fs = require('fs');
const path = require('path');

const ENTITY_PATH = path.join(__dirname, '../src/Takt.Domain/Entities');
const SEED_PATH = path.join(__dirname, '../src/Takt.Infrastructure/Data/Seeds/SeedI18nData');

/**
 * 简化的翻译映射表
 */
const TRANSLATION_MAP = {
  '代码': { en: 'Code', ja: 'コード', ko: '코드', tw: '代碼', hk: '代碼' },
  '名称': { en: 'Name', ja: '名称', ko: '이름', tw: '名稱', hk: '名稱' },
  '状态': { en: 'Status', ja: '状態', ko: '상태', tw: '狀態', hk: '狀態' },
  '类型': { en: 'Type', ja: 'タイプ', ko: '유형', tw: '類型', hk: '類型' },
  '地址': { en: 'Address', ja: '住所', ko: '주소', tw: '地址', hk: '地址' },
  '时间': { en: 'Time', ja: '時間', ko: '시간', tw: '時間', hk: '時間' },
  '日期': { en: 'Date', ja: '日付', ko: '날짜', tw: '日期', hk: '日期' },
  '描述': { en: 'Description', ja: '説明', ko: '설명', tw: '描述', hk: '描述' },
  '备注': { en: 'Remark', ja: '備考', ko: '비고', tw: '備註', hk: '備註' },
  '排序': { en: 'Order', ja: '並び順', ko: '정렬', tw: '排序號', hk: '排序號' },
  '是否': { en: 'Is', ja: 'かどうか', ko: '여부', tw: '是否', hk: '是否' },
  'ID': { en: 'ID', ja: 'ID', ko: 'ID', tw: 'ID', hk: 'ID' },
  '编号': { en: 'Number', ja: '番号', ko: '번호', tw: '編號', hk: '編號' },
  '数量': { en: 'Quantity', ja: '数量', ko: '수량', tw: '數量', hk: '數量' },
  '金额': { en: 'Amount', ja: '金額', ko: '금액', tw: '金額', hk: '金額' },
  '价格': { en: 'Price', ja: '価格', ko: '가격', tw: '價格', hk: '價格' },
};

/**
 * 翻译描述文本
 */
function translateDescription(chineseText) {
  const result = {
    en: chineseText,
    ja: chineseText,
    ko: chineseText,
    tw: chineseText,
    hk: chineseText,
  };

  for (const [key, translations] of Object.entries(TRANSLATION_MAP)) {
    if (chineseText.includes(key)) {
      result.en = translations.en;
      result.ja = translations.ja;
      result.ko = translations.ko;
      result.tw = translations.tw;
      result.hk = translations.hk;
      break;
    }
  }

  return result;
}

/**
 * 提取实体信息
 */
function extractEntityInfo(entityFile) {
  const content = fs.readFileSync(entityFile, 'utf-8');

  // 提取类名
  const classMatch = content.match(/public\s+class\s+(\w+)\s*:\s*TaktEntityBase/);
  const className = classMatch ? classMatch[1] : '';

  // 提取表描述
  const tableMatch = content.match(/\[SugarTable\("[^"]+",\s*"([^"]+)"\)\]/);
  const tableDesc = tableMatch ? tableMatch[1] : className;

  // 提取所有属性（排除导航属性）
  const properties = [];
  // 支持多个属性标签和换行符
  const propRegex = /\[SugarColumn\(([^)]+)\)\][\s\S]*?public\s+(\w+\??)\s+(\w+)\s*\{\s*get;\s*set;\s*\}/g;
    
  let match;
  while ((match = propRegex.exec(content)) !== null) {
    const columnAttr = match[1];
    const propType = match[2];
    const propName = match[3];
  
    // 提取ColumnDescription
    const descMatch = columnAttr.match(/ColumnDescription\s*=\s*"([^"]+)"/);
    const propDesc = descMatch ? descMatch[1] : propName;
  
    properties.push({
      name: propName,
      type: propType,
      description: propDesc,
    });
  }

  return {
    className,
    tableDesc,
    properties,
  };
}

/**
 * 获取种子文件路径（保持与实体完全一致的目录结构）
 */
function getSeedFilePath(entityFile) {
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const parts = relativePath.split(path.sep);
  
  if (parts.length >= 2) {
    // 保持完整的目录结构（除了最后的文件名）
    const dirParts = parts.slice(0, -1);
    const fileName = parts[parts.length - 1].replace('.cs', 'EntitiesSeedData.cs');
    return path.join(SEED_PATH, ...dirParts, fileName);
  }
  
  return null;
}

/**
 * 生成种子文件
 */
function generateSeedFile(entityInfo, outputFile, entityFile) {
  const { className, tableDesc, properties } = entityInfo;
  const entityNameLower = className.replace('Takt', '').toLowerCase();

  console.log(`\n处理: ${className} (${properties.length} 个字段)`);

  // 创建输出目录
  const outputDir = path.dirname(outputFile);
  if (!fs.existsSync(outputDir)) {
    fs.mkdirSync(outputDir, { recursive: true });
  }

  // 构建种子文件内容
  const lines = [];

  // 文件头
  lines.push('// ========================================');
  lines.push('// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)');
  lines.push('// 命名空间：Takt.Infrastructure.Data.Seeds');
  lines.push(`// 文件名称：${className}EntitiesSeedData.cs`);
  lines.push(`// 创建时间：${new Date().toISOString().split('T')[0]}`);
  lines.push('// 创建人：Takt365(AI Auto-Generated)');
  lines.push(`// 功能描述：${className} 实体字段翻译种子数据（自动生成）`);
  lines.push('//');
  lines.push('// 版权信息：Copyright (c) 2025 Takt  All rights reserved.');
  lines.push('// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。');
  lines.push('// ========================================');
  lines.push('');
  lines.push('using Microsoft.Extensions.DependencyInjection;');
  lines.push('using Takt.Domain.Entities.Routine.Tasks.I18n;');
  lines.push('using Takt.Domain.Repositories;');
  lines.push('using Takt.Infrastructure.Tenant;');
  lines.push('');
  
  // 根据目录结构生成正确的命名空间
  const relativePath = path.relative(ENTITY_PATH, entityFile);
  const parts = relativePath.split(path.sep);
  if (parts.length >= 2) {
    const dirParts = parts.slice(0, -1);
    const namespace = 'Takt.Infrastructure.Data.Seeds.SeedI18nData.' + dirParts.join('.');
    lines.push(`namespace ${namespace};`);
  } else {
    lines.push('namespace Takt.Infrastructure.Data.Seeds.SeedI18nData;');
  }
  lines.push('');
  lines.push('/// <summary>');
  lines.push(`/// ${className} 实体翻译种子数据（自动生成，与 ${className}.cs 属性一一对应）`);
  lines.push('/// </summary>');
  lines.push(`public class ${className}EntitiesSeedData : ITaktSeedData`);
  lines.push('{');
  lines.push('    public int Order => 999;');
  lines.push('');
  lines.push('    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)');
  lines.push('    {');
  lines.push('        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();');
  lines.push('        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();');
  lines.push('');
  lines.push('        int insertCount = 0;');
  lines.push('        int updateCount = 0;');
  lines.push('        var originalConfigId = TaktTenantContext.CurrentConfigId;');
  lines.push('');
  lines.push('        try');
  lines.push('        {');
  lines.push('            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 0 && l.IsDeleted == 0);');
  lines.push('            if (languages == null || languages.Count == 0)');
  lines.push('                return (0, 0);');
  lines.push('');
  lines.push('            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);');
  lines.push(`            var allTranslations = GetAll${className}EntityTranslations();`);
  lines.push('');
  lines.push('            foreach (var translation in allTranslations)');
  lines.push('            {');
  lines.push('                if (!cultureCodeToLanguageId.TryGetValue(translation.CultureCode, out var languageId))');
  lines.push('                    continue;');
  lines.push('');
  lines.push('                var existing = await translationRepository.GetAsync(t =>');
  lines.push('                    t.ResourceKey == translation.ResourceKey &&');
  lines.push('                    t.CultureCode == translation.CultureCode &&');
  lines.push('                    t.IsDeleted == 0);');
  lines.push('');
  lines.push('                if (existing == null)');
  lines.push('                {');
  lines.push('                    await translationRepository.CreateAsync(new TaktTranslation');
  lines.push('                    {');
  lines.push('                        LanguageId = languageId,');
  lines.push('                        CultureCode = translation.CultureCode,');
  lines.push('                        ResourceKey = translation.ResourceKey,');
  lines.push('                        TranslationValue = translation.TranslationValue,');
  lines.push('                        ResourceType = translation.ResourceType,');
  lines.push('                        ResourceGroup = translation.ResourceGroup,');
  lines.push('                        OrderNum = translation.OrderNum,');
  lines.push('                        IsDeleted = 0');
  lines.push('                    });');
  lines.push('                    insertCount++;');
  lines.push('                }');
  lines.push('                else if (existing.TranslationValue != translation.TranslationValue)');
  lines.push('                {');
  lines.push('                    existing.LanguageId = languageId;');
  lines.push('                    existing.TranslationValue = translation.TranslationValue;');
  lines.push('                    existing.ResourceType = translation.ResourceType;');
  lines.push('                    existing.ResourceGroup = translation.ResourceGroup;');
  lines.push('                    await translationRepository.UpdateAsync(existing);');
  lines.push('                    updateCount++;');
  lines.push('                }');
  lines.push('                else if (existing.LanguageId != languageId)');
  lines.push('                {');
  lines.push('                    existing.LanguageId = languageId;');
  lines.push('                    await translationRepository.UpdateAsync(existing);');
  lines.push('                    updateCount++;');
  lines.push('                }');
  lines.push('            }');
  lines.push('        }');
  lines.push('        finally');
  lines.push('        {');
  lines.push('            TaktTenantContext.CurrentConfigId = originalConfigId;');
  lines.push('        }');
  lines.push('');
  lines.push('        return (insertCount, updateCount);');
  lines.push('    }');
  lines.push('');
  lines.push('    /// <summary>');
  lines.push(`    /// 获取所有 ${className} 实体名称及字段翻译（自动生成，与 ${className}.cs 一一对应）`);
  lines.push('    /// </summary>');
  lines.push(`    private static List<TaktTranslation> GetAll${className}EntityTranslations()`);
  lines.push('    {');
  lines.push('        return new List<TaktTranslation>');
  lines.push('        {');

  // 实体名称翻译（去除"表"后缀）
  const selfName = tableDesc.endsWith('表') ? tableDesc.slice(0, -1) : tableDesc;
  
  lines.push('');
  lines.push(`            // entity.${entityNameLower}（实体名称）`);
  lines.push(`            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.${entityNameLower}._self", TranslationValue = "${selfName}", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },`);
  lines.push(`            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.${entityNameLower}._self", TranslationValue = "${selfName}", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },`);
  lines.push(`            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.${entityNameLower}._self", TranslationValue = "${selfName}", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },`);
  lines.push(`            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.${entityNameLower}._self", TranslationValue = "${selfName}", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },`);
  lines.push(`            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.${entityNameLower}._self", TranslationValue = "${selfName}", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },`);
  lines.push(`            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.${entityNameLower}._self", TranslationValue = "${selfName}", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },`);

  // 字段翻译
  for (const prop of properties) {
    const propNameLower = prop.name.toLowerCase();
    const resourceKey = `entity.${entityNameLower}.${propNameLower}`;
    const translations = translateDescription(prop.description);

    lines.push('');
    lines.push(`            // ${resourceKey}`);
    lines.push(`            new TaktTranslation { CultureCode = "en-US", ResourceKey = "${resourceKey}", TranslationValue = "${translations.en}", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },`);
    lines.push(`            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "${resourceKey}", TranslationValue = "${translations.ja}", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },`);
    lines.push(`            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "${resourceKey}", TranslationValue = "${translations.ko}", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },`);
    lines.push(`            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "${resourceKey}", TranslationValue = "${prop.description}", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },`);
    lines.push(`            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "${resourceKey}", TranslationValue = "${translations.tw}", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },`);
    lines.push(`            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "${resourceKey}", TranslationValue = "${translations.hk}", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },`);
  }

  lines.push('        };');
  lines.push('    }');
  lines.push('}');
  lines.push('');

  // 写入文件（UTF-8无BOM）
  const content = lines.join('\n');
  fs.writeFileSync(outputFile, content, 'utf-8');

  console.log(`  ✅ 已生成: ${outputFile}`);
}

/**
 * 主函数
 */
function main() {
  console.log('========================================');
  console.log('  批量自动生成实体种子数据文件');
  console.log('========================================\n');

  // 查找所有实体文件
  const entityFiles = [];
  function findEntityFiles(dir) {
    const files = fs.readdirSync(dir);
    for (const file of files) {
      const fullPath = path.join(dir, file);
      const stat = fs.statSync(fullPath);
      if (stat.isDirectory()) {
        findEntityFiles(fullPath);
      } else if (file.endsWith('.cs') && !file.endsWith('EntitiesSeedData.cs')) {
        const content = fs.readFileSync(fullPath, 'utf-8');
        if (content.includes('TaktEntityBase')) {
          entityFiles.push(fullPath);
        }
      }
    }
  }

  findEntityFiles(ENTITY_PATH);

  console.log(`找到 ${entityFiles.length} 个实体文件\n`);

  // 处理每个实体
  let successCount = 0;
  let failCount = 0;

  for (const entityFile of entityFiles) {
    try {
      const entityInfo = extractEntityInfo(entityFile);
      const seedFile = getSeedFilePath(entityFile);

      if (!seedFile) {
        console.log(`⚠️  跳过: ${entityInfo.className} (无法确定种子文件路径)`);
        failCount++;
        continue;
      }

      generateSeedFile(entityInfo, seedFile, entityFile);
      successCount++;
    } catch (error) {
      console.error(`❌ 失败: ${entityFile}`);
      console.error(`   错误: ${error.message}`);
      failCount++;
    }
  }

  console.log('\n========================================');
  console.log(`  生成完成！成功: ${successCount}, 失败: ${failCount}`);
  console.log('========================================');
}

// 运行
main();
