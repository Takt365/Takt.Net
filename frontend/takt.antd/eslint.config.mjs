import { readFileSync } from 'node:fs'
import path from 'node:path'
import { fileURLToPath } from 'node:url'
import js from '@eslint/js'
import globals from 'globals'
import tseslint from 'typescript-eslint'
import pluginVue from 'eslint-plugin-vue'
import vueParser from 'vue-eslint-parser'

const __filename = fileURLToPath(import.meta.url)
const __dirname = path.dirname(__filename)
const autoImportGlobalsFile = path.resolve(__dirname, '.eslintrc-auto-import.json')
const autoImportGlobals = JSON.parse(readFileSync(autoImportGlobalsFile, 'utf8')).globals || {}

const sharedGlobals = {
  ...globals.browser,
  ...globals.node,
  ...autoImportGlobals
}

const sharedParserOptions = {
  ecmaVersion: 'latest',
  sourceType: 'module',
  projectService: true,
  tsconfigRootDir: __dirname
}

const strictTsRules = {
  'no-unused-vars': 'off',
  '@typescript-eslint/no-explicit-any': 'error',
  '@typescript-eslint/no-unsafe-argument': 'error',
  '@typescript-eslint/no-unsafe-assignment': 'error',
  '@typescript-eslint/no-unsafe-call': 'error',
  '@typescript-eslint/no-unsafe-member-access': 'error',
  '@typescript-eslint/no-unsafe-return': 'error',
  '@typescript-eslint/no-unnecessary-type-assertion': 'error',
  '@typescript-eslint/restrict-template-expressions': 'error',
  '@typescript-eslint/no-meaningless-void-operator': 'error',
  '@typescript-eslint/no-unused-vars': [
    'warn',
    {
      argsIgnorePattern: '^_',
      varsIgnorePattern: '^_'
    }
  ]
}

export default [
  {
    ignores: [
      '**/node_modules/**',
      '**/dist/**',
      '**/.vite/**',
      '**/coverage/**',
      'auto-imports.d.ts',
      'components.d.ts',
      // Node 构建脚本：不参与与 Vue 应用相同的 ESLint/TS 解析（工作区根在仓库根时 glob 也要命中）
      'scripts/**',
      '**/scripts/**',
      '**/check-contracts.cjs'
    ]
  },
  js.configs.recommended,
  ...tseslint.configs.recommended,
  ...tseslint.configs.recommendedTypeChecked,
  ...pluginVue.configs['flat/recommended'],
  {
    files: ['**/*.vue'],
    languageOptions: {
      parser: vueParser,
      parserOptions: {
        parser: tseslint.parser,
        ...sharedParserOptions,
        extraFileExtensions: ['.vue']
      },
      globals: sharedGlobals
    },
    rules: {
      ...strictTsRules,
      'vue/no-ref-as-operand': 'error',
      'vue/require-typed-ref': 'error',
      'vue/multi-word-component-names': 'off'
    }
  },
  {
    files: ['**/*.{js,cjs,mjs,ts,tsx,cts,mts}'],
    ignores: ['scripts/**', '**/scripts/**'],
    languageOptions: {
      parser: tseslint.parser,
      parserOptions: sharedParserOptions,
      globals: sharedGlobals
    },
    rules: strictTsRules
  }
]
