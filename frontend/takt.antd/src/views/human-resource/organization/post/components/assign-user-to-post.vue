<template>
  <a-modal
    v-model:open="visible"
    title="分配用户"
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
      <a-form-item label="岗位">
        <a-input
          :value="postInfo"
          disabled
        />
      </a-form-item>
      <a-form-item label="用户">
        <a-transfer
          v-model:target-keys="targetKeys"
          :data-source="dataSource"
          :list-style="{
            width: '250px',
            height: '50vh',
          }"
          :titles="['未分配', '已分配']"
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
import { ref, watch, computed } from 'vue'
import { message } from 'ant-design-vue'
import { getUserOptions } from '@/api/identity/user'
import { getUserPostIds, assignUserPosts } from '@/api/human-resource/organization/post'
import type { Post } from '@/types/human-resource/organization/post'
import type { UserPost } from '@/types/human-resource/organization/user-post'
import type { TaktSelectOption } from '@/types/common'
import { logger } from '@/utils/logger'

interface Props {
  /** 是否显示对话框 */
  open?: boolean
  /** 岗位信息 */
  post?: Post | null
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  post: null
})

const emit = defineEmits<{
  'update:open': [value: boolean]
  'success': []
}>()

const visible = ref(false)
const loading = ref(false)
const optionsLoading = ref(false)
const targetKeys = ref<string[]>([])
const allOptions = ref<TaktSelectOption[]>([])

const postInfo = ref('')

const dataSource = computed(() =>
  allOptions.value.map(item => ({
    key: String((item as any).value ?? item.dictValue),
    title: (item as any).label ?? item.dictLabel ?? ''
  }))
)

watch(() => props.open, (val) => {
  visible.value = val
  if (val && props.post) {
    loadPostUsers()
  }
})

watch(visible, (val) => {
  emit('update:open', val)
})

const loadPostUsers = async () => {
  if (!props.post) return

  try {
    loading.value = true
    optionsLoading.value = true

    const postId = props.post.postId || (props.post as any).PostId || ''
    if (!postId) {
      message.error('岗位ID不存在')
      return
    }

    const postName = props.post.postName || (props.post as any).PostName || ''
    const postCode = props.post.postCode || (props.post as any).PostCode || ''
    postInfo.value = `${postName}${postCode ? `（${postCode}）` : ''}`

    const [allUsers, userPosts] = await Promise.all([
      getUserOptions(),
      getUserPostIds(String(postId))
    ])

    allOptions.value = allUsers

    const selectedIds = userPosts.map((up: UserPost) =>
      String(up.userId || (up as any).UserId || '')
    ).filter((id: string) => id)

    targetKeys.value = selectedIds

    logger.debug('[AssignUserToPost] 加载岗位用户成功:', {
      postId,
      userPosts,
      targetKeys: targetKeys.value
    })
  } catch (error: any) {
    logger.error('[AssignUserToPost] 加载岗位用户失败:', error)
    message.error(error.message || '加载岗位用户失败')
  } finally {
    loading.value = false
    optionsLoading.value = false
  }
}

const handleTransferChange = (_keys: string[], _direction: string, _moveKeys: string[]) => {
  targetKeys.value = _keys
}

const handleSubmit = async () => {
  if (!props.post) {
    message.error('岗位信息不存在')
    return
  }

  try {
    loading.value = true

    const postId = props.post.postId || (props.post as any).PostId || ''
    if (!postId) {
      message.error('岗位ID不存在')
      return
    }

    await assignUserPosts(String(postId), targetKeys.value)

    message.success('分配用户成功')
    emit('success')
    handleCancel()
  } catch (error: any) {
    logger.error('[AssignUserToPost] 分配用户失败:', error)
    message.error(error.message || '分配用户失败')
  } finally {
    loading.value = false
  }
}

const handleCancel = () => {
  visible.value = false
  targetKeys.value = []
  allOptions.value = []
  postInfo.value = ''
}
</script>

<style scoped lang="less">
</style>
