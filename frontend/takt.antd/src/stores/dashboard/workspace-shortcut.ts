import { defineStore } from 'pinia'
import { ref } from 'vue'

const SHORTCUT_STORAGE_KEY = 'takt-workspace-shortcuts'
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
    if (selectedPaths.value.length === 0 && allMenuPaths.length > 0) {
      setPaths(allMenuPaths.slice(0, MAX_SHORTCUTS))
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
      // 已满 16 个时，替换最后一个
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
