<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/login/components/forgot-password -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：忘记密码页面 -->
<!-- ======================================== -->

<template>
  <div class="login-page" :class="[`position-${layoutPosition}`, `theme-${themeStore.themeMode}`]">
    <!-- 背景层 -->
    <div class="login-background" :style="{ background: backgroundGradient }"></div>
    <!-- 左上角 Logo -->
    <div class="login-logo">
      <img :src="appLogo" :alt="$t('common.app.name')" />
      <div class="login-app-info">
        <a-typography-text class="login-app-name">{{ $t('common.app.name') }}</a-typography-text>
        <a-typography-text class="login-product-code">{{ productCodeWithVersion }}</a-typography-text>
      </div>
    </div>
    <!-- 右上角按钮组 -->
    <div class="button-group-wrapper">
      <a-button-group size="small">
        <a-radio-button size="small"> <TaktColorToggle type="icon" /></a-radio-button>
        <a-radio-button size="small"> <TaktLayoutToggle v-model:position="layoutPosition" /></a-radio-button>
        <a-radio-button size="small"> <TaktLocaleToggle type="icon" /></a-radio-button>
        <a-radio-button size="small"> <TaktThemeToggle type="icon" /></a-radio-button>
      </a-button-group>
    </div>
    <!-- 登录容器 -->
    <div class="login-container">
      <div v-if="layoutPosition !== 'center'" class="login-left-panel">
        <div class="login-illustration">
          <div class="login-icon-wrapper">
            <i class="fa-solid fa-robot login-icon"></i>
          </div>
          <div class="login-slogan">
            <a-typography-text class="login-slogan-text">{{ $t('common.app.slogan') }}</a-typography-text>
            <a-typography-text class="login-tagline">{{ $t('common.app.tagline') }}</a-typography-text>
          </div>
        </div>
      </div>
      <div class="login-right-panel" :class="`panel-${layoutPosition}`" :style="{ background: panelBackgroundGradient }">
        <div class="login-box">
          <div class="login-header">
            <a-typography-title :level="3">{{ t('login.forgot.title') }}</a-typography-title>
            <a-typography-text class="login-subtitle">{{ t('login.forgot.subtitle') }}</a-typography-text>
          </div>
          <a-form
            ref="formRef"
            :model="formState"
            :rules="rules"
            @finish="handleSubmit"
            class="login-form"
          >
            <a-form-item name="userEmail">
              <a-input
                v-model:value="formState.userEmail"
                :placeholder="t('login.fields.userEmail.placeholderForgot')"
                size="large"
                autocomplete="email"
              >
                <template #prefix>
                  <RiMailLine />
                </template>
              </a-input>
            </a-form-item>
            <a-form-item>
              <a-button type="primary" html-type="submit" block size="large" :loading="loading">
                <template #icon>
                  <RiLockPasswordLine />
                </template>
                {{ t('login.forgot.submit') }}
              </a-button>
            </a-form-item>
            <a-form-item>
              <div class="login-footer-links">
                <a @click="goToLogin">{{ t('login.forgot.backToLogin') }}</a>
              </div>
            </a-form-item>
          </a-form>
        </div>
      </div>
    </div>

    <a-modal
      v-model:open="captchaModalVisible"
      :title="$t('login.fields.captcha.validation.required')"
      :footer="null"
      :closable="true"
      :maskClosable="false"
      :width="500"
      centered
      :wrap-class-name="captchaModalWrapClassName"
      :body-style="captchaModalBodyStyle"
      @cancel="captchaModalVisible = false"
    >
      <div class="login-captcha-modal-body" style="width: 100%; height: 100%; display: flex; align-items: center; justify-content: center;">
        <component
          v-if="captchaModalData && captchaModalVisible"
          :is="captchaModalComponent"
          :initial-data="captchaModalData"
          :auto-generate="false"
          @success="onCaptchaModalSuccess"
          @fail="onCaptchaModalFail"
        />
      </div>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref, onMounted, computed, onBeforeUnmount } from 'vue'
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { storeToRefs } from 'pinia'
import { useThemeStore } from '@/stores/theme'
import { useSettingStore, getEffectiveThemeColorValue } from '@/stores/setting'
import { message } from 'ant-design-vue'
import { RiMailLine, RiLockPasswordLine } from '@remixicon/vue'
import type { Rule } from 'ant-design-vue/es/form'
import logoSvg from '@/assets/images/takt.svg'
import TaktCaptchaSlider from '@/components/common/takt-captcha-slider/index.vue'
import TaktCaptchaBehavior from '@/components/common/takt-captcha-behavior/index.vue'
import { generateCaptcha as generateCaptchaApi, type CaptchaGenerateResult } from '@/api/identity/captcha'
import { forgotPassword } from '@/api/identity/user'
import { isValidEmail } from '@/utils/regex'
import { logger } from '@/utils/logger'
import { appVersion } from '@/utils/appMeta'

defineOptions({ name: 'LoginForgotPassword' })

const props = defineProps<{ embedded?: boolean }>()
const emit = defineEmits<{ (e: 'back'): void }>()

const router = useRouter()
const themeStore = useThemeStore()
const settingStore = useSettingStore()
const { setting } = storeToRefs(settingStore)
const { t } = useI18n()

const themeColor = computed(() => getEffectiveThemeColorValue(setting.value?.themeColor ?? { type: 'blue' }))

const backgroundGradient = computed(() => {
  const color = themeColor.value
  if (themeStore.themeMode === 'dark') {
    return `radial-gradient(circle at 750% 50%, ${color} 0%, #000000 100%)`
  } else {
    return `radial-gradient(circle at 750% 50%, ${color} 0%, #ffffff 100%)`
  }
})

const panelBackgroundGradient = computed(() => {
  const color = themeColor.value
  if (themeStore.themeMode === 'dark') {
    return `radial-gradient(circle at 750% 50%, ${color} 0%, #000000 100%)`
  } else {
    return `radial-gradient(circle at 750% 50%, ${color} 0%, #ffffff 100%)`
  }
})

const appLogo = computed(() => logoSvg)

const productCodeWithVersion = computed(() => {
  return `${t('common.app.productcode')} V${appVersion}`
})

const savedLayoutPosition = localStorage.getItem('loginLayoutPosition') as 'left' | 'center' | 'right' | null
const layoutPosition = ref<'left' | 'center' | 'right'>(savedLayoutPosition || 'right')

const formState = reactive({ userEmail: '' })

const captchaModalVisible = ref(false)
const captchaModalData = ref<CaptchaGenerateResult | null>(null)
const captchaModalType = ref<'Slider' | 'Behavior'>('Behavior')
const captchaModalComponent = computed(() =>
  captchaModalType.value === 'Slider' ? TaktCaptchaSlider : TaktCaptchaBehavior
)
const captchaModalBodyStyle = {
  width: '500px',
  height: '250px',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  margin: 0
}
const captchaModalWrapClassName = computed(() => `login-captcha-modal-wrap login-captcha-modal--${layoutPosition.value}`)

const loading = ref(false)
const formRef = ref()

const rules = computed<Record<string, Rule[]>>(() => ({
  userEmail: [
    { required: true, message: t('login.fields.userEmail.validation.required'), trigger: 'blur' },
    {
      validator: (_rule: Rule, value: string) => {
        if (!value) {
          return Promise.resolve()
        }
        if (!isValidEmail(value)) {
          return Promise.reject(t('login.fields.userEmail.validation.format'))
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ]
}))

async function doForgotPassword() {
  try {
    loading.value = true
    const response = await forgotPassword({ userEmail: formState.userEmail })

    if (!response.success) {
      message.error(response.code === 'ProtectedUser' ? t('login.forgot.protectedUser') : t('login.forgot.emailNotRegistered'))
      return
    }

    message.success(t('login.forgot.success'))
    formState.userEmail = ''
    const delay = 1500
    setTimeout(() => {
      if (props.embedded) {
        emit('back')
      } else {
        router.push('/login')
      }
    }, delay)
  } catch (error: unknown) {
    logger.error('[Forget Password] 发送密码重置邮件失败:', error)
    message.error((error as Error).message || t('login.forgot.fail'))
  } finally {
    loading.value = false
  }
}

function onCaptchaModalSuccess() {
  captchaModalVisible.value = false
  doForgotPassword()
}

function onCaptchaModalFail(msg: string) {
  message.error(msg || t('login.fields.captcha.validation.required'))
}

const handleSubmit = async () => {
  try {
    await formRef.value?.validateFields(['userEmail'])
  } catch {
    return
  }

  try {
    loading.value = true
    const result = (await generateCaptchaApi()) as CaptchaGenerateResult
    if (!result) {
      message.error(t('login.fields.captcha.validation.required'))
      loading.value = false
      return
    }

    if (result.enabled === false || result.enabled === undefined) {
      loading.value = false
      await doForgotPassword()
      return
    }

    if (result.type !== 'Slider' && result.type !== 'Behavior') {
      message.error(t('login.fields.captcha.validation.typeRequired'))
      loading.value = false
      return
    }
    captchaModalType.value = result.type
    captchaModalData.value = result
    captchaModalVisible.value = true
    loading.value = false
  } catch (error: unknown) {
    logger.error('[Forgot] 获取验证码配置失败', error)
    message.error((error as Error).message || t('login.fields.captcha.validation.required'))
    loading.value = false
  }
}

const goToLogin = () => {
  if (props.embedded) {
    emit('back')
  } else {
    router.push('/login')
  }
}

onBeforeUnmount(() => {})

onMounted(async () => {
  try {
    const axios = (await import('axios')).default
    await axios.get('/api/TaktHealth', { baseURL: '', withCredentials: true })
  } catch (error: unknown) {
    logger.warn('[CSRF] 获取 CSRF Token 失败:', error)
  }
})
</script>

<style scoped lang="less">
@import '../../login.less';
</style>
<style lang="less">
.login-captcha-modal-wrap.ant-modal-wrap .ant-modal {
  position: fixed !important;
  top: 50% !important;
  margin: 0 !important;
  padding-bottom: 0;
  display: block !important;
  vertical-align: unset !important;
  transform: translate(-50%, -50%) !important;
}
.login-captcha-modal--left.ant-modal-wrap .ant-modal { left: 16.666% !important; }
.login-captcha-modal--center.ant-modal-wrap .ant-modal { left: 50% !important; }
.login-captcha-modal--right.ant-modal-wrap .ant-modal { left: 83.333% !important; }
</style>
<style scoped lang="less">
.login-box {
  gap: 64px;
}

.login-subtitle {
  display: block;
  margin-top: 8px;
  font-size: 14px;
  color: rgba(0, 0, 0, 0.45);

  .theme-dark & {
    color: rgba(255, 255, 255, 0.65);
  }
}

.login-footer-links {
  width: 100%;
  text-align: center;

  a {
    color: var(--ant-primary-color);
    text-decoration: none;

    &:hover {
      text-decoration: underline;
    }
  }
}
</style>
