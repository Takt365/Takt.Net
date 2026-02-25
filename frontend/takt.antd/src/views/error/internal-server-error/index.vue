<template>
  <div class="error-page">
    <a-result
      status="500"
      title="500"
      :sub-title="t('error.subTitle.500')"
    >
      <template #icon>
        <RiServerLine size="72px" class="error-page-icon" />
      </template>
      <template #extra>
        <a-button type="primary" @click="goHome">
          {{ t('error.backHome') }}
        </a-button>
      </template>
    </a-result>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { RiServerLine } from '@remixicon/vue'

defineOptions({ name: 'ErrorInternalServerError' })

const { t } = useI18n()
const router = useRouter()

const goHome = () => {
  router.push('/')
}
</script>

<style scoped lang="less">
.error-page {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;

  .error-page-icon {
    color: #ff4d4f;
  }
}
</style>
