<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/about -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：关于页面，系统信息与依赖列表（表头分组） -->
<!-- ======================================== -->

<template>
  <div class="about">
    <a-descriptions :column="1" bordered>
      <a-descriptions-item label="系统名称">{{ $t('common.app.name') }}</a-descriptions-item>
      <a-descriptions-item label="系统版本">V{{ appInfo.pkg.version }}</a-descriptions-item>
      <a-descriptions-item label="产品代码">{{ $t('common.app.productcode') }}</a-descriptions-item>
      <a-descriptions-item label="项目文档">
        <a :href="repoUrl" target="_blank" rel="noopener noreferrer" class="about-link"><span class="about-link-inner"><RiFileTextLine />项目文档</span></a>
        <a :href="licenseUrl" target="_blank" rel="noopener noreferrer" class="about-link"><span class="about-link-inner"><RiCopyrightLine />开源许可</span></a>
        <router-link to="/privacy" class="about-link"><span class="about-link-inner"><RiShieldLine />隐私政策</span></router-link>
        <router-link to="/terms" class="about-link"><span class="about-link-inner"><RiFileList3Line />服务条款</span></router-link>
      </a-descriptions-item>
    </a-descriptions>

    <a-table
      :columns="columns"
      :data-source="tableData"
      bordered
      size="small"
      :pagination="false"
      class="dep-table"
    />
  </div>
</template>

<script setup lang="ts">
import type { TableColumnsType } from 'ant-design-vue'
import { computed } from 'vue'
import { RiFileTextLine, RiCopyrightLine, RiShieldLine, RiFileList3Line } from '@remixicon/vue'
import { appInfo } from '@/utils/appMeta'

const repoUrl = 'https://github.com/Lean365/Takt.Net'
const licenseUrl = 'https://github.com/Lean365/Takt.Net/blob/master/LICENSE'

const columns: TableColumnsType = [
  {
    title: '生产依赖',
    children: [
      { title: '名称', dataIndex: 'depName1', key: 'depName1', ellipsis: true },
      { title: '版本', dataIndex: 'depVer1', key: 'depVer1', ellipsis: true },
      { title: '名称', dataIndex: 'depName2', key: 'depName2', ellipsis: true },
      { title: '版本', dataIndex: 'depVer2', key: 'depVer2', ellipsis: true }
    ]
  },
  {
    title: '开发依赖',
    children: [
      { title: '名称', dataIndex: 'devName1', key: 'devName1', ellipsis: true },
      { title: '版本', dataIndex: 'devVer1', key: 'devVer1', ellipsis: true },
      { title: '名称', dataIndex: 'devName2', key: 'devName2', ellipsis: true },
      { title: '版本', dataIndex: 'devVer2', key: 'devVer2', ellipsis: true }
    ]
  }
]

function toRows(record: Record<string, string>) {
  const list = Object.entries(record).map(([name, version]) => ({ name, version }))
  const rows: { name1: string; version1: string; name2: string; version2: string }[] = []
  for (let i = 0; i < list.length; i += 2) {
    const a = list[i]
    const b = list[i + 1]
    rows.push({
      name1: a?.name ?? '',
      version1: a?.version ?? '',
      name2: b?.name ?? '',
      version2: b?.version ?? ''
    })
  }
  return rows
}

const tableData = computed(() => {
  const depRows = toRows(appInfo.pkg.dependencies)
  const devRows = toRows(appInfo.pkg.devDependencies)
  const len = Math.max(depRows.length, devRows.length, 1)
  return Array.from({ length: len }, (_, i) => {
    const dep = depRows[i] ?? { name1: '', version1: '', name2: '', version2: '' }
    const dev = devRows[i] ?? { name1: '', version1: '', name2: '', version2: '' }
    return {
      key: i,
      depName1: dep.name1,
      depVer1: dep.version1,
      depName2: dep.name2,
      depVer2: dep.version2,
      devName1: dev.name1,
      devVer1: dev.version1,
      devName2: dev.name2,
      devVer2: dev.version2
    }
  })
})
</script>

<style scoped lang="less">
.about {
  padding: 24px;
}
.about-link-inner {
  display: inline-flex;
  align-items: center;
  gap: 8px;
}
.about :deep(.ant-descriptions-item-content) a + a {
  margin-left: 12px;
}
.dep-table {
  margin-top: 16px;
  :deep(.ant-table) {
    font-size: 12px;
  }
}
</style>
