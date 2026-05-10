/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：generate_all.cjs
 * 创建时间：2026-04-28
 * 功能描述：按照严格依赖链全量生成代码（DTO → 服务接口 → 服务实现 → 控制器 → 前端API）
 * 
 * 依赖链：
 *   1. DTO（手动编写）
 *   2. 服务接口（根据DTO生成）
 *   3. 服务实现（根据服务接口生成）
 *   4. 控制器（根据服务实现生成）
 *   5. 前端API（根据控制器生成）
 * 
 * 使用方法：
 *   1. 全量生成：node scripts/generate_all.cjs
 *   2. 生成指定实体：node scripts/generate_all.cjs --entity TaktNews
 *   3. 预览模式（不实际修改）：node scripts/generate_all.cjs --dry-run
 * ========================================
 */

const { execSync } = require('child_process');
const path = require('path');

const SCRIPTS_DIR = path.join(__dirname);

// 解析命令行参数
const args = process.argv.slice(2);
const entityArg = args.find(a => a.startsWith('--entity='));
const dryRunArg = args.includes('--dry-run');

const extraArgs = [];
if (entityArg) extraArgs.push(entityArg);
if (dryRunArg) extraArgs.push('--dry-run');

const argsString = extraArgs.length > 0 ? extraArgs.join(' ') : '';

console.log('========================================');
console.log('  全量代码生成工具（严格依赖链）');
console.log('========================================\n');

const steps = [
  {
    name: 'DTO生成',
    script: 'generate_dto.cjs',
    description: '实体 → DTO',
    dependency: '实体'
  },
  {
    name: '服务接口生成',
    script: 'generate_service_interfaces.cjs',
    description: 'DTO → 服务接口',
    dependency: 'DTO'
  },
  {
    name: '服务实现生成',
    script: 'generate_service_implementations.cjs',
    description: '服务接口 → 服务实现',
    dependency: '服务接口'
  },
  {
    name: '控制器生成',
    script: 'generate_controllers.cjs',
    description: '服务实现 → 控制器',
    dependency: '服务实现'
  },
  {
    name: '前端API生成',
    script: 'generate_frontend_api.cjs',
    description: '控制器 → 前端API',
    dependency: '控制器'
  }
];

let currentStep = 0;
let totalSteps = steps.length;

for (const step of steps) {
  currentStep++;
  console.log(`\n${'='.repeat(60)}`);
  console.log(`  步骤 ${currentStep}/${totalSteps}: ${step.name}`);
  console.log(`  依赖: ${step.dependency}`);
  console.log(`  脚本: ${step.script}`);
  console.log('='.repeat(60));
  
  try {
    const scriptPath = path.join(SCRIPTS_DIR, step.script);
    const command = `node "${scriptPath}" ${argsString}`;
    
    console.log(`\n执行命令: ${command}\n`);
    
    execSync(command, {
      stdio: 'inherit',
      cwd: path.join(__dirname, '..')
    });
    
    console.log(`\n✅ 步骤 ${currentStep} 完成: ${step.name}`);
  } catch (error) {
    console.error(`\n❌ 步骤 ${currentStep} 失败: ${step.name}`);
    console.error(`错误信息: ${error.message}`);
    console.error('\n终止执行！请修复错误后重新运行。');
    process.exit(1);
}

  // 新增步骤：验证器生成
  currentStep++;
  totalSteps++;
  console.log(`\n${'='.repeat(60)}`);
  console.log(`  步骤 ${currentStep}/${totalSteps}: 验证器生成`);
  console.log(`  依赖: DTO`);
  console.log(`  脚本: generate_validators.cjs`);
  console.log('='.repeat(60));
  
  try {
    const scriptPath = path.join(SCRIPTS_DIR, 'generate_validators.cjs');
    const command = `node "${scriptPath}" ${argsString}`;
    
    console.log(`\n执行命令: ${command}\n`);
    
    execSync(command, {
      stdio: 'inherit',
      cwd: path.join(__dirname, '..')
    });
    
    console.log(`\n✅ 步骤 ${currentStep} 完成: 验证器生成`);
  } catch (error) {
    console.error(`\n❌ 步骤 ${currentStep} 失败: 验证器生成`);
    console.error(`错误信息: ${error.message}`);
    console.error('\n终止执行！请修复错误后重新运行。');
    process.exit(1);
  }

  // 新增步骤：实体种子数据批量生成
  currentStep++;
  totalSteps++;
  console.log(`\n${'='.repeat(60)}`);
  console.log(`  步骤 ${currentStep}/${totalSteps}: 实体种子数据批量生成`);
  console.log(`  依赖: DTO`);
  console.log(`  脚本: generate_entities_seed_data.cjs`);
  console.log('='.repeat(60));
  
  try {
    const scriptPath = path.join(SCRIPTS_DIR, 'generate_entities_seed_data.cjs');
    const command = `node "${scriptPath}" ${argsString}`;
    
    console.log(`\n执行命令: ${command}\n`);
    
    execSync(command, {
      stdio: 'inherit',
      cwd: path.join(__dirname, '..')
    });
    
    console.log(`\n✅ 步骤 ${currentStep} 完成: 实体种子数据批量生成`);
  } catch (error) {
    console.error(`\n❌ 步骤 ${currentStep} 失败: 实体种子数据批量生成`);
    console.error(`错误信息: ${error.message}`);
    console.error('\n终止执行！请修复错误后重新运行。');
    process.exit(1);
  }

console.log('\n' + '='.repeat(60));
console.log('  🎉 全量代码生成完成！');
console.log('='.repeat(60));
console.log('\n生成链路：');
console.log('  DTO → 服务接口 → 服务实现 → 控制器 → 前端API → 验证器 → 实体种子数据批量生成');
console.log('\n提示：');
console.log('  - 请检查生成的文件是否符合预期');
console.log('  - 如有必要，手动调整TODO标记的代码');
console.log('  - 编译项目以确保没有错误');
console.log('');
}