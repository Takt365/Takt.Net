import { defineStore } from 'pinia'
import { ref, watch, nextTick } from 'vue'
import { useSettingStore } from './setting'

export type ThemeMode = 'light' | 'dark'

// View Transitions API 类型定义
interface ViewTransition {
  finished: Promise<void>
  ready: Promise<void>
  updateCallbackDone: Promise<void>
  skipTransition(): void
}

// 检查浏览器是否支持 View Transitions API
// 参考：https://developer.chrome.com/docs/web-platform/view-transitions/same-document
function supportsViewTransitions(): boolean {
  if (typeof document === 'undefined') return false
  
  // 检查 API 是否存在
  if (!('startViewTransition' in document)) return false
  
  // 检查页面是否可见（如果页面不可见，View Transitions 会被跳过）
  if (document.visibilityState === 'hidden') return false
  
  // 检查用户是否偏好减少动画
  if (window.matchMedia('(prefers-reduced-motion: reduce)').matches) {
    return false
  }
  
  return true
}

// 直接更新 DOM 主题属性（同步操作）- 使用 Ant Design Vue 官方示例的参数
function updateThemeAttribute(mode: ThemeMode) {
  document.documentElement.setAttribute('data-doc-theme', mode)
  localStorage.setItem('themeMode', mode)
  useSettingStore().setSetting({ theme: mode })
}

// 使用 View Transitions API 更新主题
// 参考：https://developer.chrome.com/docs/web-platform/view-transitions/same-document
function updateThemeWithTransition(
  updateFn: () => void | Promise<void>
): ViewTransition | null {
  if (supportsViewTransitions()) {
    try {
      // 使用 View Transitions API
      // 根据 Chrome 文档，回调函数应该尽可能快，避免阻塞
      const transition = (document as any).startViewTransition(
        async (): Promise<void> => {
          try {
            // 执行更新函数（同步更新 DOM 属性）
            const result = updateFn()
            // 如果返回 Promise，等待它完成
            if (result instanceof Promise) {
              await result
            }
            // 等待 Vue 的响应式更新完成，确保所有 DOM 变化都被捕获
            await nextTick()
          } catch (error) {
            // 如果更新失败，View Transitions API 会自动跳过过渡
            console.error('[Theme] 主题更新失败:', error)
            throw error
          }
        }
      )
      
      // 处理过渡失败的情况（根据 Chrome 文档最佳实践）
      transition.ready.catch(() => {
        // 过渡准备失败，但 DOM 更新应该已经完成
        console.warn('[Theme] View Transition 准备失败，但主题已更新')
      })
      
      transition.finished.catch(() => {
        // 过渡完成失败（通常不会发生，但为了完整性）
        console.warn('[Theme] View Transition 完成时出错')
      })
      
      return transition as ViewTransition
    } catch (error) {
      // 如果 startViewTransition 本身失败，降级为直接更新
      console.warn('[Theme] View Transition 启动失败，使用降级方案:', error)
      const result = updateFn()
      if (result instanceof Promise) {
        result.catch((err) => {
          console.error('[Theme] 主题更新失败:', err)
        })
      }
      return null
    }
  } else {
    // 降级处理：不支持 View Transitions API 时直接更新
    const result = updateFn()
    if (result instanceof Promise) {
      result.catch((error) => {
        console.error('[Theme] 主题更新失败:', error)
      })
    }
    return null
  }
}

export const useThemeStore = defineStore('theme', () => {
  const themeMode = ref<ThemeMode>(
    (localStorage.getItem('themeMode') as ThemeMode) || 'dark'
  )

  // 监听主题变化（用于非 View Transitions 的情况，如初始化）
  watch(
    themeMode,
    (newMode) => {
      updateThemeAttribute(newMode)
    },
    { immediate: true }
  )

  // 切换主题（使用 View Transitions API，支持圆形展开动画）
  const toggleTheme = (event?: MouseEvent): ViewTransition | null => {
    const newMode: ThemeMode = themeMode.value === 'light' ? 'dark' : 'light'
    return startThemeTransition(newMode, event)
  }

  // 设置主题（使用 View Transitions API，支持圆形展开动画）
  const setTheme = (mode: ThemeMode, event?: MouseEvent): ViewTransition | null => {
    if (mode === themeMode.value) return null
    return startThemeTransition(mode, event)
  }

  // 启动主题切换过渡（支持圆形展开动画）
  const startThemeTransition = (
    newMode: ThemeMode,
    event?: MouseEvent
  ): ViewTransition | null => {
    // 检查浏览器是否支持 View Transitions API
    if (!supportsViewTransitions() || !event) {
      // 不支持或没有事件对象时，使用简单切换
      updateThemeAttribute(newMode)
      themeMode.value = newMode
      return null
    }

    try {
      const x = event.clientX
      const y = event.clientY
      const endRadius = Math.hypot(
        Math.max(x, window.innerWidth - x),
        Math.max(y, window.innerHeight - y)
      )

      // 启动 View Transition
      const transition = (document as any).startViewTransition(
        async (): Promise<void> => {
          try {
            // 同步更新 DOM 属性
            updateThemeAttribute(newMode)
            // 更新响应式状态
            themeMode.value = newMode
            // 等待 Vue 的响应式更新完成
            await nextTick()
          } catch (error) {
            console.error('[Theme] 主题更新失败:', error)
            throw error
          }
        }
      )

      // 在过渡准备就绪后，应用圆形展开动画
      transition.ready.then(() => {
        const isDark = newMode === 'dark'
        const clipPath = [
          `circle(0px at ${x}px ${y}px)`,
          `circle(${endRadius}px at ${x}px ${y}px)`
        ]

        document.documentElement.animate(
          {
            clipPath: isDark ? clipPath : clipPath.reverse()
          },
          {
            duration: 600,
            easing: 'cubic-bezier(.76,.32,.29,.99)',
            pseudoElement: isDark
              ? '::view-transition-new(root)'
              : '::view-transition-old(root)'
          }
        )
      })

      // 错误处理
      transition.ready.catch(() => {
        console.warn('[Theme] View Transition 准备失败，但主题已更新')
      })

      transition.finished.catch(() => {
        console.warn('[Theme] View Transition 完成时出错')
      })

      return transition as ViewTransition
    } catch (error) {
      // 如果启动失败，降级为直接更新
      console.warn('[Theme] View Transition 启动失败，使用降级方案:', error)
      updateThemeAttribute(newMode)
      themeMode.value = newMode
      return null
    }
  }

  return {
    themeMode,
    toggleTheme,
    setTheme
  }
})
