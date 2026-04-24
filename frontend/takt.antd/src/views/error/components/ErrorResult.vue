<template>
  <div class="takt-error-page">
    <div v-if="code" class="takt-error-code">{{ code }}</div>
    <a-result :status="status" :title="t(titleKey)" :sub-title="t(subtitleKey)">
      <template #extra>
        <a-space wrap>
          <a-button v-if="showReload" @click="handleReload">
            {{ t('error.common.reload') }}
          </a-button>
          <a-button v-if="showBack" @click="handleBack">
            {{ t('error.common.goback') }}
          </a-button>
          <a-button v-if="showLoginPrimary" type="primary" @click="handleLogin">
            {{ t('error.common.gologin') }}
          </a-button>
          <a-button
            v-if="showHome"
            :type="showLoginPrimary ? 'default' : 'primary'"
            @click="handleHome"
          >
            {{ t('error.common.gohome') }}
          </a-button>
        </a-space>
      </template>
    </a-result>
  </div>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'

/** 与 Ant Design Vue `a-result` 的 `status` 一致 */
type ErrorResultStatus = 'success' | 'error' | 'info' | 'warning' | '404' | '403' | '500'

const props = withDefaults(
  defineProps<{
    status: ErrorResultStatus
    code?: string
    titleKey: string
    subtitleKey: string
    showBack?: boolean
    showHome?: boolean
    showLoginPrimary?: boolean
    showReload?: boolean
  }>(),
  {
    showBack: false,
    showHome: true,
    showLoginPrimary: false,
    showReload: false
  }
)

const { t } = useI18n()
const router = useRouter()

function handleHome() {
  router.push('/dashboard/workspace')
}

function handleLogin() {
  router.push('/login')
}

function handleBack() {
  if (typeof window !== 'undefined' && window.history.length > 1) {
    router.back()
  } else {
    router.push('/dashboard/workspace')
  }
}

function handleReload() {
  if (typeof window !== 'undefined') {
    window.location.reload()
  }
}
</script>

<style scoped lang="less">
.takt-error-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  min-height: 100%;
  padding: 24px;
}

.takt-error-code {
  margin-bottom: 8px;
  font-size: 42px;
  font-weight: 800;
  line-height: 1;
  color: var(--ant-color-error);
}
</style>
