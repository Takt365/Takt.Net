<template>
  <div class="error-page">
    <a-result
      status="error"
      title="401"
      :sub-title="t('error.subTitle.401')"
    >
      <template #icon>
        <RiLockLine size="72px" class="error-page-icon" />
      </template>
      <template #extra>
        <a-button type="primary" @click="goLogin">
          {{ t('error.goLogin') }}
        </a-button>
      </template>
    </a-result>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { RiLockLine } from '@remixicon/vue'

defineOptions({ name: 'ErrorUnauthorized' })

const { t } = useI18n()
const router = useRouter()

const goLogin = () => {
  router.push('/login')
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
