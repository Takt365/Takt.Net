/**
 * 应用元信息：从 virtual:app-info 读取（vite 插件从 package.json 注入）
 */

import appInfoJson from 'virtual:app-info'

export interface AppInfo {
  pkg: { name: string; version: string; dependencies: Record<string, string>; devDependencies: Record<string, string> }
  lastBuildTime: string
}

const empty: AppInfo = {
  pkg: { name: '', version: '', dependencies: {}, devDependencies: {} },
  lastBuildTime: ''
}

const appInfo: AppInfo = (() => {
  try {
    return (typeof appInfoJson === 'string' ? JSON.parse(appInfoJson) : appInfoJson) as AppInfo
  } catch {
    return empty
  }
})()

export const appVersion = appInfo.pkg.version
export const appDependencies = appInfo.pkg.dependencies
export const appDevDependencies = appInfo.pkg.devDependencies
export { appInfo }
