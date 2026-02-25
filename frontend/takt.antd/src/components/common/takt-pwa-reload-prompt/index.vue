<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/components/common/takt-pwa-reload-prompt -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：PWA 更新提示，当 Service Worker 检测到新版本时显示 -->
<!-- ======================================== -->

<template>
  <Teleport to="body">
    <Transition name="pwa-toast">
      <div v-if="needRefresh || offlineReady" class="pwa-toast" role="alert">
        <div class="pwa-toast-message">
          {{ offlineReady ? t('common.pwa.offlineReady') : t('common.pwa.needRefresh') }}
        </div>
        <div class="pwa-toast-actions">
          <a-button v-if="needRefresh" type="primary" size="small" @click="updateServiceWorker(true)">
            {{ t('common.pwa.reload') }}
          </a-button>
          <a-button size="small" @click="close">{{ t('common.pwa.close') }}</a-button>
        </div>
      </div>
    </Transition>
  </Teleport>
</template>

<script setup lang="ts">
import { useRegisterSW } from 'virtual:pwa-register/vue'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()

const { offlineReady, needRefresh, updateServiceWorker } = useRegisterSW({
  immediate: true,
  onRegisteredSW(_swScriptUrl: string, registration: ServiceWorkerRegistration | undefined) {
    // 每 1 小时检查一次更新
    const interval = 60 * 60 * 1000
    registration && setInterval(() => registration.update(), interval)
  }
})

async function close() {
  offlineReady.value = false
  needRefresh.value = false
}
</script>

<style scoped>
.pwa-toast {
  position: fixed;
  right: 16px;
  bottom: 16px;
  margin: 0;
  padding: 12px 16px;
  border: 1px solid rgba(0, 0, 0, 0.1);
  border-radius: 8px;
  z-index: 9999;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  background: var(--ant-color-bg-elevated, #fff);
}

.pwa-toast-message {
  margin-bottom: 8px;
  font-size: 14px;
}

.pwa-toast-actions {
  display: flex;
  gap: 8px;
}

.pwa-toast-enter-active,
.pwa-toast-leave-active {
  transition: opacity 0.3s, transform 0.3s;
}

.pwa-toast-enter-from,
.pwa-toast-leave-to {
  opacity: 0;
  transform: translateY(10px);
}
</style>
