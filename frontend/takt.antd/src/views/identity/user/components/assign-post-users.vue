<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/identity/user/components -->
<!-- 文件名称：assign-post-users.vue -->
<!-- 功能描述：分配用户岗位弹窗。由 user/index.vue 引用；v-model:open 同步；Transfer + getPostOptions / getUserPostIds / assignUserPosts；emit success。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-modal
    v-model:open="visible"
    :title="t('common.button.allocate') + t('entity.post._self')"
    :width="'33.333vw'"
    :confirm-loading="loading"
    :centered="true"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
      layout="horizontal"
    >
      <a-form-item :label="t('entity.user._self')">
        <a-input :value="userInfo" disabled />
      </a-form-item>
      <a-form-item :label="t('entity.post._self')">
        <a-transfer
          v-model:target-keys="targetKeys"
          :data-source="dataSource"
          :list-style="{
            width: '250px',
            height: '50vh',
          }"
          :titles="[t('common.action.transferUnassigned'), t('common.action.transferAssigned')]"
          show-search
          :loading="optionsLoading"
          :render="item => item.title"
          @change="handleTransferChange"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
/**
 * 分配用户岗位弹窗：岗位列表 Transfer，提交 assignUserPosts。
 */
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { getPostOptions } from '@/api/human-resource/organization/post'
import { getUserPostIds, assignUserPosts } from '@/api/identity/user'
import type { User } from '@/types/identity/user'
import type { UserPost } from '@/types/human-resource/organization/user-post'
import type { TaktSelectOption } from '@/types/common'
import { logger } from '@/utils/logger'

/**
 * 组件入参。
 */
interface Props {
  /** 是否显示对话框 */
  open?: boolean
  /** 被分配岗位的用户 */
  user?: User | null
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  user: null
})

const emit = defineEmits<{
  'update:open': [value: boolean]
  'success': []
}>()

const { t } = useI18n()

/** 弹窗显隐，与父 v-model:open 同步 */
const visible = ref(false)
/** 提交或加载 loading */
const loading = ref(false)
/** 岗位选项加载 */
const optionsLoading = ref(false)
/** Transfer 右侧已选岗位 id */
const targetKeys = ref<string[]>([])
/** 全量岗位选项 */
const allOptions = ref<TaktSelectOption[]>([])

/** 只读用户展示文案 */
const userInfo = ref('')

/** Transfer 左侧/右侧列表数据源 */
const dataSource = computed(() => 
  allOptions.value.map(item => ({
    key: String((item as any).value ?? item.dictValue),
    title: (item as any).label ?? item.dictLabel ?? ''
  }))
)

/** 监听 props.open：打开时拉取用户已分配岗位 */
watch(() => props.open, (val) => {
  visible.value = val
  if (val && props.user) {
    loadUserPosts()
  }
})

/** 监听 visible：emit update:open */
watch(visible, (val) => {
  emit('update:open', val)
})

/** 并行加载岗位选项与用户已绑 postId */
const loadUserPosts = async () => {
  if (!props.user) return

  try {
    loading.value = true
    optionsLoading.value = true

    const u = props.user as User & { UserId?: string; UserName?: string; NickName?: string }
    const userId = u.userId || u.UserId || ''
    if (!userId) {
      message.error(t('common.msg.entityIdRequired', { entity: t('entity.user._self') }))
      return
    }

    userInfo.value = `${u.userName || u.UserName || ''}（${u.nickName || u.NickName || ''}）`

    // 并行加载所有岗位和用户已分配的岗位
    const [allPosts, userPosts] = await Promise.all([
      getPostOptions(),
      getUserPostIds(String(userId))
    ])
    
    allOptions.value = allPosts
    
    // 提取已分配的岗位ID
    const selectedIds = userPosts.map((post: UserPost) => {
      return String(post.postId || (post as any).PostId || '')
    }).filter((id: string) => id)
    
    targetKeys.value = selectedIds

    logger.debug('[AssignPostUsers] 加载用户岗位成功:', {
      userId,
      userPosts,
      targetKeys: targetKeys.value
    })
  } catch (error: any) {
    logger.error('[AssignPostUsers] 加载用户岗位失败:', error)
    message.error(error.message || t('common.msg.loadTargetFail', { target: t('entity.user._self') + t('entity.post._self') }))
  } finally {
    loading.value = false
    optionsLoading.value = false
  }
}

/** Transfer 勾选变化：同步 targetKeys */
const handleTransferChange = (keys: string[], direction: string, moveKeys: string[]) => {
  targetKeys.value = keys
}

/** 确定：assignUserPosts，成功 emit success */
const handleSubmit = async () => {
  if (!props.user) {
    message.error(t('common.msg.entityNotFound', { entity: t('entity.user._self') }))
    return
  }

  try {
    loading.value = true

    // 获取用户ID
    const userId = props.user.userId || (props.user as any).UserId || ''
    if (!userId) {
      message.error(t('common.msg.entityIdRequired', { entity: t('entity.user._self') }))
      return
    }

    // 调用分配API
    await assignUserPosts(String(userId), targetKeys.value)
    
    message.success(t('common.msg.assignSuccess', { target: t('entity.post._self') }))
    emit('success')
    handleCancel()
  } catch (error: any) {
    logger.error('[AssignPostUsers] 分配岗位失败:', error)
    message.error(error.message || t('common.msg.assignFail', { target: t('entity.post._self') }))
  } finally {
    loading.value = false
  }
}

/** 关闭并重置选择 */
const handleCancel = () => {
  visible.value = false
  targetKeys.value = []
  allOptions.value = []
  userInfo.value = ''
}
</script>

<style scoped lang="less">
/* Transfer 滚动条等由全局 index.less 统一配置 */
</style>
