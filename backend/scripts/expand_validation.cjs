/**
 * 将 TaktValidationSeedData.cs 中的 AddValidationKey 辅助方法展开为直接的 TaktTranslation 条目，
 * 并删除 AppendFrontendBusinessMessages 方法，使其风格与 TaktGreetingsI18nSeedData.cs 一致。
 */
const fs = require('fs');
const path = require('path');

const FILE_PATH = path.join(__dirname, 'backend/src/Takt.Infrastructure/Data/Seeds/SeedI18nData/TaktValidationSeedData.cs');

let content = fs.readFileSync(FILE_PATH, 'utf8');

// Step 1: 提取所有 AddValidationKey 调用
const addKeyPattern = /AddValidationKey\("([^"]+)",\s*"([^"]*(?:""[^"]*)*)",\s*"([^"]*)",\s*"([^"]*)"\);/g;
const addKeyCalls = [];
let match;

// We need to extract from the AppendFrontendBusinessMessages method
// Find the method body
const methodStart = content.indexOf('private static void AppendFrontendBusinessMessages');
const methodMatch = content.match(/private static void AppendFrontendBusinessMessages\([^)]*\)\s*\{([\s\S]*?)\n    \}/);

if (!methodMatch) {
    console.log('AppendFrontendBusinessMessages method not found!');
    process.exit(1);
}

const methodBody = methodMatch[1];

// Extract all AddValidationKey calls
let m;
const addKeyRegex = /\s*AddValidationKey\("([^"]+)",\s*"([^"]*(?:""[^"]*)*)",\s*"([^"]*)",\s*"([^"]*)"\);/g;
while ((m = addKeyRegex.exec(methodBody)) !== null) {
    const [, resourceKey, enUs, zhCn, zhTw] = m;
    // Unescape double quotes
    const enUsClean = enUs.replace(/""/g, '"');
    addKeyCalls.push({ resourceKey, enUs: enUsClean, zhCn, zhTw });
}

console.log(`Found ${addKeyCalls.length} AddValidationKey calls`);

// Step 2: Generate the expanded entries
const expandedEntries = [];
for (const { resourceKey, enUs, zhCn, zhTw } of addKeyCalls) {
    // Check if resourceKey contains a dot and group by prefix
    const parts = resourceKey.split('.');
    const groupComment = parts.length > 1 ? parts.slice(0, -1).join('.') : resourceKey;
    
    const entry = `
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "${resourceKey}", TranslationValue = "${enUs.replace(/"/g, '""')}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "${resourceKey}", TranslationValue = "${enUs.replace(/"/g, '""')}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "${resourceKey}", TranslationValue = "${enUs.replace(/"/g, '""')}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "${resourceKey}", TranslationValue = "${zhCn}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "${resourceKey}", TranslationValue = "${zhTw}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "${resourceKey}", TranslationValue = "${zhTw}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },`;
    
    expandedEntries.push(entry);
}

// Step 3: Replace the list declaration from `var list` to `return new`
content = content.replace(
    /var list = new List<TaktTranslation>/,
    'return new List<TaktTranslation>'
);

// Step 4: Find the end of the list (the `};` after the last entry)
// We need to insert the expanded entries before the closing `};`
// The list ends at line ~1274: `        };`
// But we also need to remove:
// - `AppendFrontendBusinessMessages(list);`
// - `return list;`
// - The entire AppendFrontendBusinessMessages method

// Find the pattern: `        };\n\n        AppendFrontendBusinessMessages(list);\n        return list;`
const listEndPattern = /
        \};

        AppendFrontendBusinessMessages\(list\);
        return list;
/;
if (!listEndPattern.test(content)) {
    console.log('Could not find list end pattern');
    process.exit(1);
}

// Build the comment and entries block
let insertText = '\n';
let prevGroup = '';
for (const entry of expandedEntries) {
    // Extract the resource key from the entry
    const keyMatch = entry.match(/ResourceKey = "([^"]+)"/);
    if (keyMatch) {
        const rk = keyMatch[1];
        const parts = rk.split('.');
        const group = parts.length > 2 ? parts[parts.length - 1] : rk;
        
        // Add a blank line and comment for each entry (to match Greetings style)
        insertText += `\n            // ${rk}`;
    }
    insertText += entry;
}

// The last entry should not have a trailing comma
// Find the last comma and remove it
insertText = insertText.trimEnd();
if (insertText.endsWith(',')) {
    insertText = insertText.slice(0, -1);
}

// Replace
content = content.replace(
    listEndPattern,
    insertText + '\n        };\n'
);

// Step 5: Remove the AppendFrontendBusinessMessages method
const methodRegex = /
    \/\/\/ <summary>
    \/\/\/ 追加应用层业务消息：[^
]*
    \/\/\/[\s\S]*?private static void AppendFrontendBusinessMessages\([^)]*\)\s*\{[\s\S]*?
    \}/;
content = content.replace(methodRegex, '');

// Step 6: Write back
fs.writeFileSync(FILE_PATH, content, 'utf8');
console.log('File updated successfully');
console.log(`Expanded ${addKeyCalls.length} AddValidationKey calls into ${addKeyCalls.length * 6} TaktTranslation entries`);
