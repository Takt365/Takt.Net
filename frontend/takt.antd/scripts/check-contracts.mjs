/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：check-contracts.mjs
 * 创建时间：2025-02-02
 * 功能描述：前后端契约一致性检查工具
 *   1. 运行emit-type-shims.mjs生成最新类型定义
 *   2. 检查src/types目录是否有未提交的变更
 *   3. 确保前后端DTO类型同步
 * 
 * 使用方法：
 *   1. CI/CD检查：npm run check:contracts
 *   2. 手动检查：node scripts/check-contracts.mjs
 *   3. 如果有变更，git diff会显示差异并退出码为1
 * 
 * 注意事项：
 *   - 用于CI/CD流程，确保类型定义已提交
 *   - 退出码0表示无变更，1表示有未提交变更
 *   - 依赖emit-type-shims.mjs脚本
 * ========================================
 */

import { execFileSync } from 'node:child_process';
import { resolve, dirname } from 'node:path';
import { fileURLToPath } from 'node:url';

const __dirname = dirname(fileURLToPath(import.meta.url));
const root = resolve(__dirname, '..');

/**
 * 运行Node脚本
 * @param {string} scriptName - 脚本文件名
 */
function runNode(scriptName) {
  const script = resolve(__dirname, scriptName);
  execFileSync(process.execPath, [script], { cwd: root, stdio: 'inherit' });
}

// 步骤1: 生成最新类型定义
runNode('emit-type-shims.mjs');

// 步骤2: 检查是否有未提交的变更
execFileSync('git', ['diff', '--exit-code', '--', 'src/types'], { cwd: root, stdio: 'inherit' });
