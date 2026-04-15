<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/dict/type -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：字典类型管理页面，包含字典类型列表、查询、新增、编辑、删除等功能（主子表） -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="routine-dict-type">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      placeholder="请输入字典类型编码或名称"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="routine:tasks:dict:create"
      update-permission="routine:tasks:dict:update"
      delete-permission="routine:tasks:dict:delete"
      export-permission="routine:tasks:dict:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-export="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :create-disabled="false"
      :update-disabled="!selectedRow"
      :delete-disabled="selectedRows.length === 0"
      :create-loading="loading"
      :update-loading="loading"
      :delete-loading="loading"
      :export-loading="loading"
      :refresh-loading="loading"
      @create="handleCreate"
      @update="handleUpdate"
      @delete="handleDelete"
      @export="handleExport"
      @advanced-query="handleAdvancedQuery"
      @column-setting="handleColumnSetting"
      @refresh="handleRefresh"
    />

    <!-- 表格 -->
    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="(record: any) => record.dictTypeId || ''"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      :expanded-row-keys="expandedRowKeys"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
      @expand="handleExpand"
    >
      <!-- 自定义列渲染 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'dictTypeCode'">
          <a
            style="color: #1890ff; cursor: pointer"
            @click.stop="handleOpenDictDataWindow(record)"
          >
            {{ record.dictTypeCode }}
          </a>
        </template>
        <template v-else-if="column.key === 'dictTypeStatus'">
          <a-switch
            :checked="record.dictTypeStatus === 0"
            checked-children="启用"
            un-checked-children="禁用"
            @change="(checked: any) => handleStatusChange(record, !!checked)"
          />
        </template>
        <template v-else-if="column.key === 'dataSource'">
          {{ getDataSourceText(record.dataSource) }}
        </template>
        <template v-else-if="column.key === 'isBuiltIn'">
          {{ record.isBuiltIn === 0 ? '是' : '否' }}
        </template>
      </template>
      <!-- 展开行渲染 -->
      <template #expandedRowRender="{ record }">
        <div style="padding: 16px">
          <a-table
            v-if="(dataSource.find(item => item.dictTypeId === record.dictTypeId)?.dictDataList || []).length > 0"
            :columns="dictDataColumns"
            :data-source="dataSource.find(item => item.dictTypeId === record.dictTypeId)?.dictDataList || []"
            :row-key="(r: DictData) => r.dictDataId || ''"
            :pagination="false"
            size="small"
            bordered
          />
          <a-empty v-else />
        </div>
      </template>
    </TaktSingleTable>

    <!-- 分页组件 -->
    <TaktPagination
      v-model:current="currentPage"
      v-model:page-size="pageSize"
      :total="total"
      @change="handlePaginationChange"
      @show-size-change="handlePaginationSizeChange"
    />

    <!-- 新增/编辑对话框 -->
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      :width="1200"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <DictTypeForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>

    <!-- 高级查询抽屉 -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item label="字典类型编码">
        <a-input v-model:value="advancedQueryForm.dictTypeCode" />
      </a-form-item>
      <a-form-item label="字典类型名称">
        <a-input v-model:value="advancedQueryForm.dictTypeName" />
      </a-form-item>
      <a-form-item label="类型状态">
        <TaktSelect
          v-model:value="advancedQueryForm.dictTypeStatus"
          dict-type="sys_status"
          placeholder="请选择状态"
          allow-clear
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 列设置抽屉 -->
    <!-- 审计字段统一在 TaktColumnDrawer 中处理 -->
    <TaktColumnDrawer
      v-model:open="columnDrawerVisible"
      :columns="mergedColumns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'dictTypeCode'"
      :action-column-key="'action'"
      @update:checked-keys="handleColumnKeysChange"
      @reset="handleColumnSettingReset"
    />

    <!-- 字典数据子表窗口 -->
    <DictDataWindow
      v-model:visible="dictDataWindowVisible"
      :dict-type="currentDictType"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { mergeDefaultColumns } from '@/utils/table-columns'
import DictTypeForm from './components/dict-type-form.vue'
import DictDataWindow from './components/dict-data-window.vue'
import * as dictTypeApi from '@/api/routine/tasks/dict/dicttype'
import * as dictDataApi from '@/api/routine/tasks/dict/dictdata'
import type { DictType, DictTypeQuery, DictTypeCreate, DictTypeUpdate } from '@/types/routine/tasks/dict/dicttype'
import type { DictData } from '@/types/routine/tasks/dict/dictdata'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

// ========================================
// 数据定义
// ========================================

const loading = ref(false)
const queryKeyword = ref('')
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const dataSource = ref<DictType[]>([])

// 行选择
const selectedRowKeys = ref<(string | number)[]>([])
const selectedRows = ref<DictType[]>([])
const selectedRow = ref<DictType | null>(null)

// 表单
const formVisible = ref(false)
const formTitle = ref('新增字典类型')
const formLoading = ref(false)
const formData = ref<DictType | null>(null)
const formRef = ref<InstanceType<typeof DictTypeForm> | null>(null)

// 高级查询
const advancedQueryVisible = ref(false)
const advancedQueryForm = reactive<DictTypeQuery>({
  pageIndex: 1,
  pageSize: 20,
  keyWords: '',
  dictTypeCode: '',
  dictTypeName: '',
  dictTypeStatus: undefined
})

// 列设置
const visibleColumnKeys = ref<string[]>([])
const columnDrawerVisible = ref(false)

// 展开行
const expandedRowKeys = ref<(string | number)[]>([])

// 字典数据子表窗口
const dictDataWindowVisible = ref(false)
const currentDictType = ref<DictType | null>(null)

// 字典数据子表列定义（用于展开行显示，与 DictData 接口字段顺序一致）
const dictDataColumns = computed<TableColumnsType<DictData>>(() => [
  {
    title: '字典数据ID',
    dataIndex: 'dictDataId',
    key: 'dictDataId',
    width: 120
  },
  {
    title: '字典类型ID',
    dataIndex: 'dictTypeId',
    key: 'dictTypeId',
    width: 120
  },
  {
    title: '字典类型编码',
    dataIndex: 'dictTypeCode',
    key: 'dictTypeCode',
    width: 150
  },
  {
    title: '字典标签',
    dataIndex: 'dictLabel',
    key: 'dictLabel',
    width: 150
  },
  {
    title: '字典本地化键',
    dataIndex: 'dictL10nKey',
    key: 'dictL10nKey',
    width: 200,
    ellipsis: true
  },
  {
    title: '字典值',
    dataIndex: 'dictValue',
    key: 'dictValue',
    width: 150
  },
  {
    title: 'CSS类名',
    dataIndex: 'cssClass',
    key: 'cssClass',
    width: 100
  },
  {
    title: '列表类名',
    dataIndex: 'listClass',
    key: 'listClass',
    width: 100
  },
  {
    title: '扩展标签',
    dataIndex: 'extLabel',
    key: 'extLabel',
    width: 150,
    ellipsis: true
  },
  {
    title: '扩展值',
    dataIndex: 'extValue',
    key: 'extValue',
    width: 150,
    ellipsis: true
  },
  {
    title: '排序号',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  }
])

// ========================================
// 列定义
// ========================================

const columns = computed<TableColumnsType<DictType>>(() => [
  {
    title: '字典类型ID',
    dataIndex: 'dictTypeId',
    key: 'dictTypeId',
    width: 120,
    fixed: 'left'
  },
  {
    title: '字典类型编码',
    dataIndex: 'dictTypeCode',
    key: 'dictTypeCode',
    width: 150,
    fixed: 'left'
  },
  {
    title: '字典类型名称',
    dataIndex: 'dictTypeName',
    key: 'dictTypeName',
    width: 200
  },
  {
    title: '数据源',
    dataIndex: 'dataSource',
    key: 'dataSource',
    width: 100
  },
  {
    title: '数据库配置ID',
    dataIndex: 'dataConfigId',
    key: 'dataConfigId',
    width: 150,
    ellipsis: true
  },
  {
    title: 'SQL脚本',
    dataIndex: 'sqlScript',
    key: 'sqlScript',
    width: 200,
    ellipsis: true
  },
  {
    title: '是否内置',
    dataIndex: 'isBuiltIn',
    key: 'isBuiltIn',
    width: 100
  },
  {
    title: '排序号',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: '类型状态',
    dataIndex: 'dictTypeStatus',
    key: 'dictTypeStatus',
    width: 100
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: '编辑',
        shape: 'plain',
        icon: RiEditLine,
        permission: 'routine:tasks:dict:update',
        onClick: (record: DictType) => handleEditOne(record)
      },
      {
        key: 'delete',
        label: '删除',
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'routine:tasks:dict:delete',
        onClick: (record: DictType) => handleDeleteOne(record)
      }
    ]
  })
])

// 审计字段统一在 TaktColumnDrawer 中处理

// 合并列配置（包含审计字段）- 参照 file/index.vue 的实现
// 注意：CreateActionColumn 内部使用 h 函数返回 VNode，导致 TypeScript 类型推断过深
// 这是 TypeScript 在处理复杂递归类型时的已知限制，使用类型断言是合理的解决方案
const mergedColumns = computed((): any => {
  return mergeDefaultColumns(columns.value as any, t, true)
})

// 根据可见列过滤显示的列 - 保持原始列的顺序
// 注意：由于 mergedColumns 包含 VNode 返回类型，需要类型断言避免类型推断过深
// 使用 any 类型避免 TypeScript 类型推断过深的问题
const displayColumns = computed((): any => {
  const keys = visibleColumnKeys.value || []
  // 直接对 mergedColumns.value 进行类型断言，避免类型推断
  const merged: any = mergedColumns.value || []
  
  // 如果 keys 为空，返回原始列配置（等待 TaktColumnDrawer 初始化）
  if (keys.length === 0) {
    const cols: any = columns.value
    return cols
  }
  
  // 根据选中的 keys 过滤列，但保持原始列的顺序
  const getColumnKey = (col: any): string => {
    const key = col.key || col.dataIndex || col.title
    return key ? String(key) : ''
  }
  
  // 将 keys 转换为 Set 以便快速查找
  const keysSet = new Set(keys.map(k => String(k)))
  
  // 按照 merged 的原始顺序过滤，只保留选中的列
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

// ========================================
// 方法定义
// ========================================

// 加载数据
const loadData = async () => {
  try {
    loading.value = true
    const query: DictTypeQuery = {
      ...advancedQueryForm,
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      keyWords: queryKeyword.value || undefined
    }
    
    const result = await dictTypeApi.getDictTypeList(query)
    dataSource.value = result.data || []
    total.value = result.total || 0
    
    // 字典数据按需加载（点击展开时加载），不再一次性加载所有数据
  } catch (error) {
    logger.error('[DictType] 加载数据失败', error)
    message.error('加载数据失败')
  } finally {
    loading.value = false
  }
}

// 搜索
const handleSearch = () => {
  currentPage.value = 1
  loadData()
}

// 重置
const handleReset = () => {
  queryKeyword.value = ''
  currentPage.value = 1
  Object.assign(advancedQueryForm, {
    keyWords: '',
    dictTypeCode: '',
    dictTypeName: '',
    dictTypeStatus: undefined
  })
  loadData()
}

// 新增
const handleCreate = () => {
  formTitle.value = '新增字典类型'
  formData.value = null
  formVisible.value = true
}

// 编辑
const handleUpdate = () => {
  if (!selectedRow.value) {
    message.warning('请选择要编辑的记录')
    return
  }
  
  formTitle.value = '编辑字典类型'
  formData.value = { ...selectedRow.value }
  formVisible.value = true
}

// 编辑单条记录（操作列使用）
const handleEditOne = (record: DictType) => {
  selectedRow.value = record
  formTitle.value = '编辑字典类型'
  formData.value = { ...record }
  formVisible.value = true
}

// 删除
const handleDelete = async () => {
  if (selectedRows.value.length === 0) {
    message.warning('请选择要删除的记录')
    return
  }
  
  const ids = selectedRows.value.map(row => row.dictTypeId).filter(Boolean)
  if (ids.length === 0) {
    message.warning('没有有效的记录ID')
    return
  }
  
  try {
    loading.value = true
    if (ids.length === 1) {
      await dictTypeApi.deleteDictTypeById(ids[0])
    } else {
      await dictTypeApi.deleteDictTypeBatch(ids)
    }
    message.success('删除成功')
    await loadData()
    selectedRowKeys.value = []
    selectedRows.value = []
    selectedRow.value = null
  } catch (error) {
    logger.error('[DictType] 删除失败', error)
    message.error('删除失败')
  } finally {
    loading.value = false
  }
}

// 删除单条记录（操作列使用）
const handleDeleteOne = async (record: DictType) => {
  if (!record.dictTypeId) {
    message.warning('没有有效的记录ID')
    return
  }
  
  try {
    loading.value = true
    await dictTypeApi.deleteDictTypeById(record.dictTypeId)
    message.success('删除成功')
    await loadData()
    // 如果删除的是当前选中的行，清除选中状态
    if (selectedRow.value?.dictTypeId === record.dictTypeId) {
      selectedRow.value = null
    }
    selectedRowKeys.value = selectedRowKeys.value.filter(k => k !== record.dictTypeId)
    selectedRows.value = selectedRows.value.filter(r => r.dictTypeId !== record.dictTypeId)
  } catch (error) {
    logger.error('[DictType] 删除失败', error)
    message.error('删除失败')
  } finally {
    loading.value = false
  }
}

// 导出
const handleExport = async () => {
  try {
    loading.value = true
    const query: DictTypeQuery = {
      ...advancedQueryForm,
      pageIndex: 1,
      pageSize: 10000,
      keyWords: queryKeyword.value || undefined
    }
    
    const blob = await dictTypeApi.exportDictTypeData(query, undefined, '字典类型')
    const ts = new Date(); const t = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `字典类型_${ts.getFullYear()}${t(ts.getMonth()+1)}${t(ts.getDate())}${t(ts.getHours())}${t(ts.getMinutes())}${t(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    message.success('导出成功')
  } catch (error) {
    logger.error('[DictType] 导出失败', error)
    message.error('导出失败')
  } finally {
    loading.value = false
  }
}

// 状态切换
const handleStatusChange = async (record: DictType, checked: boolean) => {
  try {
    await dictTypeApi.updateDictTypeStatus({
      dictTypeId: record.dictTypeId,
      dictTypeStatus: checked ? 0 : 1
    })
    message.success('状态更新成功')
    await loadData()
  } catch (error) {
    logger.error('[DictType] 状态更新失败', error)
    message.error('状态更新失败')
  }
}

// 高级查询
const handleAdvancedQuery = () => {
  advancedQueryVisible.value = true
}

const handleAdvancedQuerySubmit = () => {
  advancedQueryVisible.value = false
  currentPage.value = 1
  loadData()
}

const handleAdvancedQueryReset = () => {
  Object.assign(advancedQueryForm, {
    keyWords: '',
    dictTypeCode: '',
    dictTypeName: '',
    dictTypeStatus: undefined
  })
}

// 列设置
const handleColumnSetting = () => {
  columnDrawerVisible.value = true
}

// 列设置变化 - TaktColumnDrawer 传递选中的 keys，更新 visibleColumnKeys
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map(k => String(k))
}

// 列设置重置：TaktColumnDrawer 会自动重置为默认值
const handleColumnSettingReset = () => {
  // TaktColumnDrawer 组件内部会自动处理重置逻辑
  // 这里只需要清空，让组件使用默认值
  visibleColumnKeys.value = []
}

// 刷新
const handleRefresh = () => {
  loadData()
}

// 表单提交
const handleFormSubmit = async () => {
  if (!formRef.value) return
  
  try {
    await formRef.value.validate()
    formLoading.value = true
    
    const formData = formRef.value.getFormData()
    if ('dictTypeId' in formData && formData.dictTypeId) {
      // 更新
      await dictTypeApi.updateDictType(formData.dictTypeId, formData as DictTypeUpdate)
      message.success('更新成功')
    } else {
      // 新增
      await dictTypeApi.createDictType(formData as DictTypeCreate)
      message.success('新增成功')
    }
    
    formVisible.value = false
    await loadData()
  } catch (error: any) {
    if (error?.errorFields) {
      message.warning('请检查表单输入')
    } else {
      logger.error('[DictType] 保存失败', error)
      message.error('保存失败')
    }
  } finally {
    formLoading.value = false
  }
}

// 表单取消
const handleFormCancel = () => {
  formVisible.value = false
  formData.value = null
}

// 表格变化
const handleTableChange = () => {
  // 处理表格变化
}

// 列调整
const handleResizeColumn = () => {
  // 处理列调整
}

// 行选择配置
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: DictType[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: DictType, selected: boolean) => {
    if (selected) {
      selectedRow.value = record
    } else if (selectedRow.value?.dictTypeId === record?.dictTypeId) {
      selectedRow.value = null
    }
  },
  onSelectAll: (selected: boolean, selectedRowsData: DictType[]) => {
    if (selected) {
      selectedRow.value = selectedRowsData.length === 1 ? selectedRowsData[0] : null
    } else {
      selectedRow.value = null
    }
  }
}))

// 行点击处理 - 切换展开状态（手风琴模式：只允许一个展开，保留行选择功能）
const onClickRow = (record: DictType) => {
  return {
    onClick: (event: MouseEvent) => {
      // 如果点击的是复选框或操作列，不处理展开
      const target = event.target as HTMLElement
      if (target.closest('.ant-checkbox-wrapper') || target.closest('.takt-action-column')) {
        return
      }
      
      const key = record.dictTypeId || ''
      // 手风琴模式：切换展开状态
      if (expandedRowKeys.value.includes(key)) {
        // 如果当前行已展开，则收起
        expandedRowKeys.value = []
      } else {
        // 如果当前行未展开，先关闭其他已展开的行，再展开当前行
        expandedRowKeys.value = []
        
        // 确保字典数据已加载，等待加载完成后再展开
        const item = dataSource.value.find(item => item.dictTypeId === record.dictTypeId)
        if (item && (!item.dictDataList || item.dictDataList.length === 0)) {
          // 先加载数据，等待完成后再展开
          loadDictData(record).then(() => {
            expandedRowKeys.value = [key]
          })
        } else {
          expandedRowKeys.value = [key]
        }
      }
    }
  }
}

// 展开/收起处理（手风琴模式：只允许一个展开）
const handleExpand = async (expanded: boolean, record: DictType) => {
  if (expanded && record.dictTypeId) {
    // 手风琴模式：先关闭其他已展开的行
    const currentKey = record.dictTypeId || ''
    if (expandedRowKeys.value.length > 0 && expandedRowKeys.value[0] !== currentKey) {
      expandedRowKeys.value = []
    }
    
    // 检查 dataSource 中是否有数据，如果没有则加载
    const item = dataSource.value.find(item => item.dictTypeId === record.dictTypeId)
    if (item && (!item.dictDataList || item.dictDataList.length === 0)) {
      await loadDictData(record)
    }
    
    // 设置当前行为唯一展开的行
    expandedRowKeys.value = [currentKey]
  } else {
    // 收起时清空
    expandedRowKeys.value = []
  }
}

// 加载字典数据 - 根据 dictTypeId 动态获取
const loadDictData = async (record: DictType) => {
  if (!record.dictTypeId) return
  
  try {
    // 使用 dictDataApi.getList 根据 dictTypeId 查询字典数据（dictTypeId 是唯一标识）
    const result = await dictDataApi.getDictDataList({
      pageIndex: 1,
      pageSize: 10000, // 获取所有数据
      dictTypeId: record.dictTypeId
    })
    
    if (result && result.data) {
      // 更新 dataSource 中对应的记录，确保响应式更新
      const index = dataSource.value.findIndex(item => item.dictTypeId === record.dictTypeId)
      if (index !== -1) {
        // 使用 Vue 的响应式更新方式
        dataSource.value[index] = {
          ...dataSource.value[index],
          dictDataList: result.data
        }
      }
      return result.data
    }
    return []
  } catch (error) {
    logger.error('[DictType] 加载字典数据失败', error)
    message.error('加载字典数据失败')
    return []
  }
}

// 打开字典数据子表窗口
const handleOpenDictDataWindow = async (record: DictType) => {
  if (!record.dictTypeId) {
    message.warning('字典类型ID不存在')
    return
  }
  
  currentDictType.value = record
  dictDataWindowVisible.value = true
}




// 分页变化
const handlePaginationChange = (page: number) => {
  currentPage.value = page
  loadData()
}

const handlePaginationSizeChange = (current: number, size: number) => {
  currentPage.value = current
  pageSize.value = size
  loadData()
}

// 辅助方法
const getDataSourceText = (dataSource: number) => {
  return dataSource === 0 ? '系统表' : dataSource === 1 ? 'SQL查询' : '未知'
}

// ========================================
// 生命周期
// ========================================

onMounted(() => {
  loadData()
})
</script>

<style scoped lang="less">
.routine-dict-type {
  padding: 16px;
}
</style>
