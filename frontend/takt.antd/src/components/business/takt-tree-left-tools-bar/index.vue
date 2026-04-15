<!-- ======================================== -->
<!-- 命名空间：@/components/business/takt-tree-left-tools-bar -->
<!-- 功能描述：左树工具栏，仅含 查询、展开/收缩 两个按钮，宽度为视口的 1/4 -->
<!-- ======================================== -->

<template>
  <div v-if="show" class="takt-tree-left-tools-bar">
    <a-space :size="8">
      <a-button
        class="takt-button-query"
        :loading="loading"
        @click="handleSearch"
      >
        <template #icon>
          <RiSearchLine />
        </template>
        {{ t('common.button.query') }}
      </a-button>
      <a-button
        class="takt-button-expand"
        :title="expanded ? t('common.button.collapse') : t('common.button.expand')"
        @click="handleToggleExpand"
      >
        <template #icon>
          <RiMenuFoldLine v-if="expanded" />
          <RiMenuUnfoldLine v-else />
        </template>
        {{ expanded ? t('common.button.collapse') : t('common.button.expand') }}
      </a-button>
    </a-space>
  </div>
</template>

<script setup lang="ts">
import { RiSearchLine, RiMenuFoldLine, RiMenuUnfoldLine } from '@remixicon/vue'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()

interface Props {
  /** 是否显示 */
  show?: boolean
  /** 是否已展开（v-model:expanded） */
  expanded?: boolean
  /** 查询按钮加载状态 */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  show: true,
  expanded: true,
  loading: false
})

const emit = defineEmits<{
  'search': []
  'update:expanded': [value: boolean]
}>()

const handleSearch = () => {
  emit('search')
}

const handleToggleExpand = () => {
  const next = !props.expanded
  emit('update:expanded', next)
}
</script>

<style scoped lang="less">
/* 宽度：内容视口的 1/5（20%），与左侧树/查询栏统一 */
.takt-tree-left-tools-bar {
  flex: 0 0 20%;
  width: 20%;
  min-width: 160px;
  max-width: 20%;
  //margin: 4px;
  padding: 4px;
  display: flex;
  align-items: center;
  min-height: 32px;
  box-sizing: border-box;

  :deep(.ant-btn) {
    display: inline-flex;
    align-items: center;
    gap: 4px;
  }
}
</style>
