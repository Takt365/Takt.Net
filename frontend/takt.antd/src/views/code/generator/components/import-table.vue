<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/code/generator/components -->
<!-- 文件名称：import-table.vue -->
<!-- 功能描述：从数据库导入表 -->
<!-- ======================================== -->

<template>
  <a-form :label-col="{ span: 6 }" :wrapper-col="{ span: 16 }">
    <a-form-item label="数据源">
      <a-select
        v-model:value="configId"
        placeholder="请选择数据源"
        allow-clear
        style="width: 100%"
        @change="handleConfigChange"
      >
        <a-select-option v-for="c in databaseConfigs" :key="c.configId" :value="c.configId">
          {{ c.displayName }} ({{ c.configId }})
        </a-select-option>
      </a-select>
    </a-form-item>
    <a-form-item label="数据表">
      <a-select
        v-model:value="tableName"
        placeholder="请先选择数据源"
        :disabled="!configId"
        :loading="databaseTablesLoading"
        allow-clear
        style="width: 100%"
      >
        <a-select-option v-for="t in databaseTables" :key="t.tableName" :value="t.tableName">
          {{ t.tableName }} {{ t.tableComment ? `- ${t.tableComment}` : '' }}
        </a-select-option>
      </a-select>
    </a-form-item>
    <a-form-item :wrapper-col="{ offset: 6, span: 16 }">
      <a-button type="primary" :loading="importLoading" :disabled="!configId || !tableName" @click="handleSubmit">
        导入
      </a-button>
    </a-form-item>
  </a-form>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import type { DatabaseConfig, DatabaseTableInfo } from '@/api/generator/table'

const props = withDefaults(
  defineProps<{
    open?: boolean
    databaseConfigs: DatabaseConfig[]
    databaseTables: DatabaseTableInfo[]
    databaseTablesLoading?: boolean
    importLoading?: boolean
  }>(),
  { open: false, databaseTablesLoading: false, importLoading: false }
)

const emit = defineEmits<{
  (e: 'config-change', configId: string): void
  (e: 'submit', payload: { configId: string; tableName: string }): void
}>()

const configId = ref<string | undefined>()
const tableName = ref<string | undefined>()

watch(
  () => props.open,
  (isOpen) => {
    if (isOpen) reset()
  }
)

function handleConfigChange() {
  tableName.value = undefined
  if (configId.value) emit('config-change', configId.value)
}

function handleSubmit() {
  if (!configId.value || !tableName.value) return
  emit('submit', { configId: configId.value, tableName: tableName.value })
}

function reset() {
  configId.value = undefined
  tableName.value = undefined
}

defineExpose({ reset })
</script>
