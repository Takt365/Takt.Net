/**
 * WangEditor 类型补充
 * - editor-for-vue：包 exports 导致类型解析失败时在此声明，保证 TS 不报隐式 any
 * - 扩展 @wangeditor/editor 的 Slate 节点类型见 https://www.wangeditor.com/v5/for-ts.html
 */
declare module '@wangeditor/editor-for-vue' {
  import type { Component } from 'vue'
  export const Editor: Component
  export const Toolbar: Component
}
