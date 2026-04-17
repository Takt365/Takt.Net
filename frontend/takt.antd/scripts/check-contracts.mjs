/**
 * 重新生成契约与薄封装后，对 `git diff --exit-code` 校验应提交产物是否一致。
 * 覆盖：`src/types/generated/` 以及薄封装路径（与 MODULE_EXPORTS / emitSpecial 同源）。
 */
import { execFileSync } from 'node:child_process'
import path from 'node:path'
import { fileURLToPath } from 'node:url'
import { getShimGitDiffPathsFromRepoRoot } from './emit-type-shims.mjs'

const __dirname = path.dirname(fileURLToPath(import.meta.url))
const root = path.resolve(__dirname, '..')

function runNode(scriptName) {
  const script = path.join(__dirname, scriptName)
  execFileSync(process.execPath, [script], { cwd: root, stdio: 'inherit' })
}

runNode('generate-openapi-types.mjs')
runNode('emit-type-shims.mjs')

const shimFiles = getShimGitDiffPathsFromRepoRoot()
const diffArgs = ['diff', '--exit-code', '--', 'src/types/generated', ...shimFiles]
execFileSync('git', diffArgs, { cwd: root, stdio: 'inherit' })
