// ========================================
// 下载引擎 API 使用示例
// ========================================

import { 
  getDownloadableFile, 
  changeFilePublicStatus, 
  updateFileTags, 
  updateFilePermissionConfig, 
  checkFilePermission 
} from '@/api/routine/tasks/file/download-engine/downloads'
import type { FilePublicChange } from '@/types/routine/tasks/file/specific-engine/downloads'

// ========================================
// 示例 1：下载文件（自动统计下载次数）
// ========================================
async function downloadFileExample() {
  const fileId = '123'
  const userIP = '192.168.1.100'
  
  try {
    // 获取文件信息（自动更新 DownloadCount, LastDownloadTime, Location）
    const fileInfo = await getDownloadableFile(fileId, userIP)
    
    console.log('文件信息：', fileInfo)
    console.log('下载次数：', fileInfo.downloadCount)
    console.log '最后下载时间：', fileInfo.lastDownloadTime)
    
    // 实际下载文件
    window.open(fileInfo.filePath)
  } catch (error) {
    console.error('下载失败：', error)
  }
}

// ========================================
// 示例 2：检查权限后下载
// ========================================
async function downloadWithPermissionCheck() {
  const fileId = '123'
  const currentUserId = '456'
  
  try {
    // 1. 检查权限
    const permissionResult = await checkFilePermission(fileId, currentUserId)
    
    if (!permissionResult.hasPermission) {
      alert('您没有权限下载此文件')
      return
    }
    
    // 2. 有权限，执行下载
    const fileInfo = await getDownloadableFile(fileId)
    window.open(fileInfo.filePath)
  } catch (error) {
    console.error('下载失败：', error)
  }
}

// ========================================
// 示例 3：更新文件标签
// ========================================
async function updateTagsExample() {
  const fileId = '123'
  
  try {
    // 设置标签（逗号分隔）
    await updateFileTags(fileId, '重要,已审核,财务,2024年')
    console.log('标签更新成功')
    
    // 清除所有标签
    await updateFileTags(fileId, undefined)
    console.log('标签已清除')
  } catch (error) {
    console.error('更新标签失败：', error)
  }
}

// ========================================
// 示例 4：更新访问权限配置
// ========================================
async function updatePermissionConfigExample() {
  const fileId = '123'
  
  try {
    // 基于角色的权限配置
    const roleConfig = {
      allowedRoles: [1, 2, 3],
      deniedRoles: [4],
      allowedUsers: [100, 101],
      allowedDepts: [10, 20]
    }
    
    await updateFilePermissionConfig(
      fileId, 
      JSON.stringify(roleConfig)
    )
    console.log('权限配置更新成功')
    
    // 清除权限配置（变为完全私有）
    await updateFilePermissionConfig(fileId, undefined)
    console.log('权限配置已清除')
  } catch (error) {
    console.error('更新权限配置失败：', error)
  }
}

// ========================================
// 示例 5：变更文件公开状态
// ========================================
async function changePublicStatusExample() {
  const fileId = '123'
  
  try {
    // 设置为公开
    const publicData: FilePublicChange = {
      fileId,
      isPublic: 1
    }
    await changeFilePublicStatus(publicData)
    console.log('文件已设为公开')
    
    // 设置为私有
    const privateData: FilePublicChange = {
      fileId,
      isPublic: 0
    }
    await changeFilePublicStatus(privateData)
    console.log('文件已设为私有')
  } catch (error) {
    console.error('变更公开状态失败：', error)
  }
}

// ========================================
// 示例 6：完整的管理流程
// ========================================
async function completeManagementExample() {
  const fileId = '123'
  const currentUserId = '456'
  
  try {
    // 1. 检查权限
    const { hasPermission } = await checkFilePermission(fileId, currentUserId)
    if (!hasPermission) {
      throw new Error('无权限操作')
    }
    
    // 2. 更新标签
    await updateFileTags(fileId, '重要,已审核')
    
    // 3. 更新权限配置
    const permissionConfig = {
      allowedRoles: [1, 2],
      allowedUsers: [100, 101, 102]
    }
    await updateFilePermissionConfig(fileId, JSON.stringify(permissionConfig))
    
    // 4. 设置为公开
    await changeFilePublicStatus({ fileId, isPublic: 1 })
    
    // 5. 获取下载信息
    const fileInfo = await getDownloadableFile(fileId, '192.168.1.100')
    console.log('操作完成，文件信息：', fileInfo)
    
  } catch (error) {
    console.error('管理操作失败：', error)
  }
}

// ========================================
// 示例 7：在 Vue 组件中使用
// ========================================
/*
<template>
  <div>
    <button @click="handleDownload">下载文件</button>
    <button @click="handleTogglePublic">切换公开状态</button>
    <button @click="handleUpdateTags">更新标签</button>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { 
  getDownloadableFile, 
  changeFilePublicStatus, 
  updateFileTags,
  checkFilePermission 
} from '@/api/routine/tasks/file/downloads'
import type { FilePublicChange } from '@/types/routine/tasks/file/specific-engine/downloads'

const props = defineProps<{
  fileId: string
}>()

const isPublic = ref(0)

// 下载文件
async function handleDownload() {
  try {
    const fileInfo = await getDownloadableFile(props.fileId)
    window.open(fileInfo.filePath)
  } catch (error) {
    console.error('下载失败：', error)
  }
}

// 切换公开状态
async function handleTogglePublic() {
  try {
    const newStatus = isPublic.value === 1 ? 0 : 1
    const data: FilePublicChange = {
      fileId: props.fileId,
      isPublic: newStatus
    }
    await changeFilePublicStatus(data)
    isPublic.value = newStatus
  } catch (error) {
    console.error('切换失败：', error)
  }
}

// 更新标签
async function handleUpdateTags() {
  try {
    await updateFileTags(props.fileId, '重要,已审核,归档')
  } catch (error) {
    console.error('更新标签失败：', error)
  }
}
</script>
*/
