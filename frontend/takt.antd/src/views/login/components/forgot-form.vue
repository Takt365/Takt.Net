<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/login/components -->
<!-- 文件名称：forgot-form.vue -->
<!-- 功能描述：忘记密码整页/内嵌布局（与登录页同壳）。由 `login/index.vue` 在 `viewMode==='forget'` 时以 `embedded` 引用并监听 `back`；邮箱 + `forgotPassword`；提交前 `generateCaptcha`，启用则弹窗 Slider/Behavior，通过后 `doForgotPassword`；`goToLogin` 内嵌 emit、独立路由 `router.push('/login')`；挂载请求 `/api/TaktHealth` 预热 Cookie/CSRF。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div
    class="login-page"
    :class="[`position-${layoutPosition}`, `theme-${themeStore.themeMode}`]"
  >
    <!-- 背景层 -->
    <div
      class="login-background"
      :style="{ background: backgroundGradient }"
    />
    <!-- 左上角 Logo -->
    <div class="login-logo">
      <img
        :src="appLogo"
        :alt="$t('common.app.name')"
      >
      <div class="login-app-info">
        <a-typography-text class="login-app-name">
          {{ $t('common.app.name') }}
        </a-typography-text>
        <a-typography-text class="login-product-code">
          {{ productCodeWithVersion }}
        </a-typography-text>
      </div>
    </div>
    <!-- 右上角：颜色 / 表单区位置 / 语言 / 主题 -->
    <div class="button-group-wrapper">
      <a-button-group size="small">
        <a-radio-button size="small">
          <TaktColorToggle type="icon" />
        </a-radio-button>
        <a-radio-button size="small">
          <TaktLayoutToggle v-model:position="layoutPosition" />
        </a-radio-button>
        <a-radio-button size="small">
          <TaktLocaleToggle type="icon" />
        </a-radio-button>
        <a-radio-button size="small">
          <TaktThemeToggle type="icon" />
        </a-radio-button>
      </a-button-group>
    </div>
    <div class="login-container">
      <!-- 非居中：左侧插画 + 文案 -->
      <div
        v-if="layoutPosition !== 'center'"
        class="login-left-panel"
      >
        <div class="login-illustration">
          <div class="login-icon-wrapper">
            <i class="fa-solid fa-robot login-icon" />
          </div>
          <div class="login-slogan">
            <a-typography-text class="login-slogan-text">
              {{ $t('common.app.slogan') }}
            </a-typography-text>
            <a-typography-text class="login-tagline">
              {{ $t('common.app.tagline') }}
            </a-typography-text>
          </div>
        </div>
      </div>
      <!-- 右侧：忘记密码表单 + 验证码弹窗 -->
      <div
        class="login-right-panel"
        :class="`panel-${layoutPosition}`"
        :style="{ background: panelBackgroundGradient }"
      >
        <div class="login-box">
          <div class="login-header">
            <a-typography-title :level="3">
              {{ t('login.forgot.title') }}
            </a-typography-title>
            <a-typography-text class="login-subtitle">
              {{ t('login.forgot.subtitle') }}
            </a-typography-text>
          </div>
          <a-form
            ref="formRef"
            :model="formState"
            :rules="rules"
            class="login-form"
            @finish="handleSubmit"
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
              <a-button
                type="primary"
                html-type="submit"
                block
                size="large"
                :loading="loading"
              >
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
      :mask-closable="false"
      :width="500"
      centered
      :wrap-class-name="captchaModalWrapClassName"
      :body-style="captchaModalBodyStyle"
      @cancel="captchaModalVisible = false"
    >
      <div
        class="login-captcha-modal-body"
        style="width: 100%; height: 100%; display: flex; align-items: center; justify-content: center;"
      >
        <component
          :is="captchaModalComponent"
          v-if="captchaModalData && captchaModalVisible"
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
/**
 * 忘记密码：布局与交互对齐 `login/index.vue`；验证码流程与登录页一致。
 */
import { reactive, ref, onMounted, computed, onBeforeUnmount } from 'vue'
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { storeToRefs } from 'pinia'
import { useThemeStore } from '@/stores/theme'
import { useSettingStore, getThemeColorValue } from '@/stores/setting'
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

/**
 * 与登录页内嵌切换相关的入参。
 */
interface Props {
  /**
   * 为 true 时由 `login/index.vue` 包裹切换，`back` 返回登录主视图；为 false 时整页展示并用路由跳转。
   */
  embedded?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  embedded: false
})

/**
 * 内嵌模式下通知父级切回 `viewMode === 'login'`。
 */
const emit = defineEmits<{
  (e: 'back'): void
}>()

/** 非内嵌时用于跳转 `/login` */
const router = useRouter()
/** 明暗主题，参与渐变背景 */
const themeStore = useThemeStore()
const settingStore = useSettingStore()
const { setting } = storeToRefs(settingStore)
const { t } = useI18n()

/** 主题色解析值，供径向渐变使用 */
const themeColor = computed(() => getThemeColorValue(setting.value?.themeColor ?? { type: 'blue' }))

/** 全页背景径向渐变 */
const backgroundGradient = computed(() => {
  const color = themeColor.value
  if (themeStore.themeMode === 'dark') {
    return `radial-gradient(circle at 750% 50%, ${color} 0%, #000000 100%)`
  }
  return `radial-gradient(circle at 750% 50%, ${color} 0%, #ffffff 100%)`
})

/** 右侧表单区背景渐变 */
const panelBackgroundGradient = computed(() => {
  const color = themeColor.value
  if (themeStore.themeMode === 'dark') {
    return `radial-gradient(circle at 750% 50%, ${color} 0%, #000000 100%)`
  }
  return `radial-gradient(circle at 750% 50%, ${color} 0%, #ffffff 100%)`
})

/** 左上角 Logo 资源 */
const appLogo = computed(() => logoSvg)

/** 产品代号 + 构建版本号 */
const productCodeWithVersion = computed(() => {
  return `${t('common.app.productcode')} V${appVersion}`
})

/** 与登录页共用的表单区左/中/右位置（`localStorage` 键 `loginLayoutPosition`） */
const savedLayoutPosition = localStorage.getItem('loginLayoutPosition') as
  | 'left'
  | 'center'
  | 'right'
  | null
/** 登录容器布局位置 */
const layoutPosition = ref<'left' | 'center' | 'right'>(
  savedLayoutPosition || 'right'
)

/** 忘记密码仅邮箱字段 */
const formState = reactive({
  userEmail: ''
})

/** 验证码弹窗可见性 */
const captchaModalVisible = ref(false)
/** 后端 `generateCaptcha` 返回的初始数据 */
const captchaModalData = ref<CaptchaGenerateResult | null>(null)
/** 弹窗内组件类型 */
const captchaModalType = ref<'Slider' | 'Behavior'>('Behavior')
/** 按类型选择 Slider 或 Behavior 验证码组件 */
const captchaModalComponent = computed(() =>
  captchaModalType.value === 'Slider' ? TaktCaptchaSlider : TaktCaptchaBehavior
)
/** 弹窗 body 固定尺寸，与登录页 forgot 分支一致 */
const captchaModalBodyStyle = {
  width: '500px',
  height: '250px',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  margin: 0
}
/** 弹窗水平位置随 `layoutPosition` 对齐表单区 */
const captchaModalWrapClassName = computed(() => `login-captcha-modal-wrap login-captcha-modal--${layoutPosition.value}`)

/** 提交与发信请求中的 loading */
const loading = ref(false)

/** Ant Design Vue 表单实例 */
const formRef = ref()

/** 邮箱必填与自定义格式（`isValidEmail`） */
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

/** 验证码已关闭或未启用后调用接口发重置邮件 */
async function doForgotPassword() {
  try {
    loading.value = true
    const res = await forgotPassword({ userEmail: formState.userEmail })

    if (!res.success) {
      message.error(res.code === 'ProtectedUser' ? t('login.forgot.protectedUser') : t('login.forgot.emailNotRegistered'))
      return
    }

    message.success(t('login.forgot.success'))
    formState.userEmail = ''
    const delay = 1500
    setTimeout(() => {
      if (props.embedded) emit('back')
      else router.push('/login')
    }, delay)
  } catch (error: any) {
    logger.error('[Forget Password] 发送密码重置邮件失败:', error)
    message.error(error.message || t('login.forgot.fail'))
  } finally {
    loading.value = false
  }
}

/** 弹窗内验证通过：关闭弹窗并执行发信 */
function onCaptchaModalSuccess() {
  captchaModalVisible.value = false
  doForgotPassword()
}

/** 验证码失败提示 */
function onCaptchaModalFail(msg: string) {
  message.error(msg || t('login.fields.captcha.validation.required'))
}

/** 表单 @finish：校验邮箱后拉验证码配置，必要时弹窗 */
const handleSubmit = async () => {
  try {
    await formRef.value?.validateFields(['userEmail'])
  } catch {
    return
  }

  try {
    loading.value = true
    const result = await generateCaptchaApi()
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
  } catch (error: any) {
    logger.error('[Forgot] 获取验证码配置失败', error)
    message.error(error?.message || t('login.fields.captcha.validation.required'))
    loading.value = false
  }
}

/** 返回登录：内嵌 emit，否则路由 */
const goToLogin = () => {
  if (props.embedded) emit('back')
  else router.push('/login')
}

onBeforeUnmount(() => {})

/** 预热 Cookie，与登录页一致 */
onMounted(async () => {
  try {
    const axios = (await import('axios')).default
    await axios.get('/api/TaktHealth', { baseURL: '', withCredentials: true })
  } catch (error: any) {
    logger.warn('[CSRF] 获取 CSRF Token 失败:', error)
  }
})
</script>

<style scoped lang="less">
@import '../login.less';
</style>
<style lang="less">
/* 验证码弹窗 fixed 定位，水平随 layoutPosition（与 login/index.vue 一致） */
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
/* 标题区与表单纵向间距，与 register-form 对齐 */
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
