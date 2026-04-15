<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/login/components -->
<!-- 文件名称：register-form.vue -->
<!-- 功能描述：用户注册整页/内嵌布局。由 `login/index.vue` 在 `viewMode==='register'` 时以 `embedded` 引用并监听 `back`；字段用户名/邮箱/手机，`createUser` 提交默认档案字段；提交前 `generateCaptcha` 与弹窗 Slider/Behavior；成功延迟后内嵌 `emit('back')` 或 `router.push('/login')`；挂载 `/api/TaktHealth` 预热 Cookie。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="login-page" :class="[`position-${layoutPosition}`, `theme-${themeStore.themeMode}`]">
    <div class="login-background" :style="{ background: backgroundGradient }"></div>
    <div class="login-logo">
      <img :src="appLogo" :alt="$t('common.app.name')" />
      <div class="login-app-info">
        <a-typography-text class="login-app-name">{{ $t('common.app.name') }}</a-typography-text>
        <a-typography-text class="login-product-code">{{ productCodeWithVersion }}</a-typography-text>
      </div>
    </div>
    <div class="button-group-wrapper">
      <a-button-group size="small">
        <a-radio-button size="small"> <TaktColorToggle type="icon" /></a-radio-button>
        <a-radio-button size="small"> <TaktLayoutToggle v-model:position="layoutPosition" /></a-radio-button>
        <a-radio-button size="small"> <TaktLocaleToggle type="icon" /></a-radio-button>
        <a-radio-button size="small"> <TaktThemeToggle type="icon" /></a-radio-button>
      </a-button-group>
    </div>
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
            <a-typography-title :level="3">{{ t('login.sign.title') }}</a-typography-title>
            <a-typography-text class="login-subtitle">{{ t('login.sign.subtitle') }}</a-typography-text>
          </div>
          <a-form
            ref="formRef"
            :model="formState"
            :rules="rules"
            @finish="handleSubmit"
            class="login-form"
          >
            <a-form-item name="userName">
              <a-input
                v-model:value="formState.userName"
                :placeholder="t('login.fields.username.placeholder')"
                size="large"
                autocomplete="username"
              >
                <template #prefix>
                  <RiUserLine />
                </template>
              </a-input>
            </a-form-item>
            <a-form-item name="userEmail">
              <a-input
                v-model:value="formState.userEmail"
                :placeholder="t('login.fields.userEmail.placeholder')"
                size="large"
                autocomplete="email"
              >
                <template #prefix>
                  <RiMailLine />
                </template>
              </a-input>
            </a-form-item>
            <a-form-item name="userPhone">
              <a-input
                v-model:value="formState.userPhone"
                :placeholder="t('login.fields.userPhone.placeholder')"
                size="large"
                autocomplete="tel"
              >
                <template #prefix>
                  <RiPhoneLine />
                </template>
              </a-input>
            </a-form-item>
            <a-form-item>
              <a-button type="primary" html-type="submit" block size="large" :loading="loading">
                <template #icon>
                  <RiUserAddLine />
                </template>
                {{ t('login.sign.title') }}
              </a-button>
            </a-form-item>
            <a-form-item>
              <div class="login-footer-links">
                <a @click="goToLogin">{{ t('login.sign.hasAccount') }}</a>
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
/**
 * 自助注册：组装 `createUser` 默认字段；验证码流程与登录/忘记密码一致。
 */
import { reactive, ref, onMounted, computed, onBeforeUnmount } from 'vue'
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { storeToRefs } from 'pinia'
import { useThemeStore } from '@/stores/theme'
import { useSettingStore, getThemeColorValue } from '@/stores/setting'
import { message } from 'ant-design-vue'
import { RiUserLine, RiUserAddLine, RiMailLine, RiPhoneLine } from '@remixicon/vue'
import type { Rule } from 'ant-design-vue/es/form'
import logoSvg from '@/assets/images/takt.svg'
import TaktCaptchaSlider from '@/components/common/takt-captcha-slider/index.vue'
import TaktCaptchaBehavior from '@/components/common/takt-captcha-behavior/index.vue'
import { generateCaptcha as generateCaptchaApi, type CaptchaGenerateResult } from '@/api/identity/captcha'
import { createUser } from '@/api/identity/user'
import type { UserCreate } from '@/types/identity/user'
import { logger } from '@/utils/logger'
import { appVersion } from '@/utils/appMeta'

/**
 * 与登录页内嵌切换相关的入参。
 */
interface Props {
  /**
   * 为 true 时由 `login/index.vue` 内嵌展示，成功或「已有账号」返回时 `emit('back')`。
   */
  embedded?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  embedded: false
})

/**
 * 内嵌模式下通知父级切回登录主视图。
 */
const emit = defineEmits<{
  (e: 'back'): void
}>()

/** 非内嵌时跳转登录 */
const router = useRouter()
const themeStore = useThemeStore()
const settingStore = useSettingStore()
const { setting } = storeToRefs(settingStore)
const { t } = useI18n()

/** 主题色，用于背景渐变 */
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

/** Logo 资源 */
const appLogo = computed(() => logoSvg)

/** 产品代号 + 版本 */
const productCodeWithVersion = computed(() => {
  return `${t('common.app.productcode')} V${appVersion}`
})

/** 与登录页共享的 `loginLayoutPosition` */
const savedLayoutPosition = localStorage.getItem('loginLayoutPosition') as
  | 'left'
  | 'center'
  | 'right'
  | null
const layoutPosition = ref<'left' | 'center' | 'right'>(
  savedLayoutPosition || 'right'
)

/** 注册三字段 */
const formState = reactive({
  userName: '',
  userEmail: '',
  userPhone: ''
})

/** 验证码弹窗可见 */
const captchaModalVisible = ref(false)
/** 验证码组件初始数据 */
const captchaModalData = ref<CaptchaGenerateResult | null>(null)
/** Slider 或 Behavior */
const captchaModalType = ref<'Slider' | 'Behavior'>('Behavior')
/** 动态验证码子组件 */
const captchaModalComponent = computed(() =>
  captchaModalType.value === 'Slider' ? TaktCaptchaSlider : TaktCaptchaBehavior
)
/** 弹窗 body 固定宽高 */
const captchaModalBodyStyle = {
  width: '500px',
  height: '250px',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  margin: 0
}
/** 弹窗水平对齐 login 布局 */
const captchaModalWrapClassName = computed(() => `login-captcha-modal-wrap login-captcha-modal--${layoutPosition.value}`)

/** 注册请求 loading */
const loading = ref(false)

/** 表单 ref */
const formRef = ref()

/** 用户名长度、邮箱 type、手机号大陆号段 */
const rules = computed<Record<string, Rule[]>>(() => ({
  userName: [
    { required: true, message: t('login.fields.username.validation.required'), trigger: 'blur' },
    { min: 3, message: t('login.fields.username.validation.min'), trigger: 'blur' },
    { max: 20, message: t('login.fields.username.validation.max'), trigger: 'blur' }
  ],
  userEmail: [
    { required: true, message: t('login.fields.userEmail.validation.required'), trigger: 'blur' },
    { type: 'email', message: t('login.fields.userEmail.validation.format'), trigger: 'blur' }
  ],
  userPhone: [
    { required: true, message: t('login.fields.userPhone.validation.required'), trigger: 'blur' },
    { pattern: /^1[3-9]\d{9}$/, message: t('login.fields.userPhone.validation.format'), trigger: 'blur' }
  ]
}))

/** 调用 `createUser` 写入开放注册默认档案（密码由后端初始策略处理） */
async function doRegister() {
  try {
    loading.value = true
    const userName = formState.userName.trim()
    const registerData: UserCreate = {
      employeeId: '',
      userName,
      nickName: userName,
      userType: 0,
      userEmail: formState.userEmail.trim(),
      userPhone: formState.userPhone.trim(),
      passwordHash: '',
      userStatus: 0,
      roleIds: [],
      deptIds: [],
      postIds: [],
      tenantIds: [],
      remark: ''
    }
    await createUser(registerData)
    message.success(t('login.sign.successInitialPassword'))
    setTimeout(() => {
      if (props.embedded) emit('back')
      else router.push('/login')
    }, 1500)
  } catch (error: any) {
    logger.error('[Register] 注册失败:', error)
    message.error(error.message || t('login.sign.fail'))
    captchaModalVisible.value = false
  } finally {
    loading.value = false
  }
}

/** 验证码通过后关闭弹窗并注册 */
function onCaptchaModalSuccess() {
  captchaModalVisible.value = false
  doRegister()
}

/** 验证码失败 */
function onCaptchaModalFail(msg: string) {
  message.error(msg || t('login.fields.captcha.validation.required'))
}

/** 校验三字段后走验证码分支 */
const handleSubmit = async () => {
  try {
    await formRef.value?.validateFields(['userName', 'userEmail', 'userPhone'])
  } catch {
    return
  }

  try {
    loading.value = true
    const result = await generateCaptchaApi() as CaptchaGenerateResult
    if (!result) {
      message.error(t('login.fields.captcha.validation.required'))
      loading.value = false
      return
    }

    if (result.enabled === false || result.enabled === undefined) {
      loading.value = false
      await doRegister()
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
    logger.error('[Register] 获取验证码配置失败', error)
    message.error(error?.message || t('login.fields.captcha.validation.required'))
    loading.value = false
  }
}

/** 返回登录 */
const goToLogin = () => {
  if (props.embedded) emit('back')
  else router.push('/login')
}

onBeforeUnmount(() => {})

/** 预热 Cookie */
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
/* 标题与表单项间距 */
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
