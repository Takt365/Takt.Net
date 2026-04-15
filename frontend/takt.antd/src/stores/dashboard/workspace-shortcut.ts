import { defineStore } from 'pinia'
import { ref } from 'vue'

const SHORTCUT_STORAGE_KEY = 'takt-workspace-shortcuts'
/** 默认两行，每行 8 个 */
const MAX_SHORTCUTS = 16

function loadFromStorage(): string[] {
  try {
    const raw = localStorage.getItem(SHORTCUT_STORAGE_KEY)
    const parsed = raw ? JSON.parse(raw) : []
    return Array.isArray(parsed) ? parsed : []
  } catch {
    return []
  }
}

function saveToStorage(list: string[]) {
  localStorage.setItem(SHORTCUT_STORAGE_KEY, JSON.stringify(list))
}

export const useWorkspaceShortcutStore = defineStore('workspaceShortcut', () => {
  const selectedPaths = ref<string[]>(loadFromStorage())

  function setPaths(list: string[]) {
    const unique = Array.from(new Set(list))
    const finalList = unique.slice(0, MAX_SHORTCUTS)
    selectedPaths.value = finalList
    saveToStorage(finalList)
  }

  function initDefaultsIfEmpty(allMenuPaths: string[]) {
    if (allMenuPaths.length === 0) return
    const current = selectedPaths.value
    if (current.length === 0) {
      setPaths(allMenuPaths.slice(0, MAX_SHORTCUTS))
      return
    }
    // 已有数据但不足 16 个时补足（兼容旧版只存了 8 个的情况）
    if (current.length < MAX_SHORTCUTS) {
      const existing = new Set(current)
      const toAdd = allMenuPaths.filter(p => !existing.has(p)).slice(0, MAX_SHORTCUTS - current.length)
      if (toAdd.length > 0) {
        setPaths([...current, ...toAdd])
      }
    }
  }

  function addOrReplace(path: string) {
    if (!path) return
    const current = [...selectedPaths.value]
    const existIndex = current.indexOf(path)
    if (existIndex !== -1) {
      // 已存在则移动到最后（视为最近一次添加）
      current.splice(existIndex, 1)
    }
    if (current.length < MAX_SHORTCUTS) {
      current.push(path)
    } else {
      // 已满时替换最后一个
      current.splice(MAX_SHORTCUTS - 1, 1, path)
    }
    setPaths(current)
  }

  return {
    selectedPaths,
    setPaths,
    initDefaultsIfEmpty,
    addOrReplace
  }
})

