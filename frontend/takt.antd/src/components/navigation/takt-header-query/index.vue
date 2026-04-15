<template>
  <a-button type="text" class="takt-header-query" @click="handleClick">
    <template #icon>
      <RiSearchLine />
    </template>
  </a-button>
  <a-modal
    v-model:open="visible"
    :footer="null"
    :closable="false"
    :mask="true"
    :mask-closable="true"
    :width="520"
    :centered="false"
    :style="{ top: '20vh', paddingBottom: 0 }"
    class="takt-header-query-modal"
    @cancel="handleClose"
  >
    <template #title>
      <div class="takt-query-header">
        <span class="takt-query-title">添加快捷入口</span>
        <a-button type="text" class="takt-query-close" @click="handleClose">
          <template #icon>
            <CloseOutlined />
          </template>
        </a-button>
      </div>
    </template>
    <div class="takt-query-content">
      <TaktSelect
        v-model="selectedPath"
        :options="selectOptions"
        placeholder="输入关键词搜索，选择菜单添加到工作台快捷入口"
        show-search
        allow-clear
        size="large"
        class="takt-query-select"
        @change="onSelectChange"
      />
    </div>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted, onUnmounted } from 'vue'
import { CloseOutlined } from '@ant-design/icons-vue'
import { RiSearchLine } from '@remixicon/vue'
import TaktSelect from '@/components/business/takt-select/index.vue'
import { useMenuStore } from '@/stores/identity/menu'
import { useWorkspaceShortcutStore } from '@/stores/dashboard/workspace-shortcut'

const menuStore = useMenuStore()
const shortcutStore = useWorkspaceShortcutStore()

const visible = ref(false)
const selectedPath = ref<string | undefined>(undefined)

// 仅 menuType=1 的扁平列表，转为 TaktSelect 的 options（label / value）
const selectOptions = computed(() =>
  (menuStore.leafMenuItems || []).map((x: { path: string; title: string }) => ({
    label: x.title,
    value: x.path
  }))
)

watch(visible, (isOpen) => {
  if (!isOpen) {
    selectedPath.value = undefined
  }
})

const handleClick = () => {
  visible.value = true
}

const handleClose = () => {
  visible.value = false
}

function onSelectChange(value: string | number | (string | number)[] | undefined) {
  const path = value == null ? undefined : String(value)
  if (path) {
    shortcutStore.addOrReplace(path)
    selectedPath.value = undefined
    handleClose()
  }
}

const handleGlobalKeyDown = (e: KeyboardEvent) => {
  if (visible.value && e.key === 'Escape') {
    handleClose()
  }
}

onMounted(() => {
  document.addEventListener('keydown', handleGlobalKeyDown)
})

onUnmounted(() => {
  document.removeEventListener('keydown', handleGlobalKeyDown)
})
</script>

<style scoped lang="less">
.takt-query-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  .takt-query-title {
    font-weight: 500;
  }
  .takt-query-close {
    margin-right: -8px;
  }
}
.takt-query-content {
  padding: 8px 0;
}
.takt-query-select {
  width: 100%;
}
</style>
