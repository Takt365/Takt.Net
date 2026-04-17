<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/login -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：登录主壳页。`viewMode` 在登录 / `forgot-form` / `register-form` 间切换；引用 `showcase-form` 作左侧插画；`defaultSetting` 控制忘记密码与注册入口；提交先 `generateCaptcha` 再弹窗或 `doLogin`；`userStore.login` 成功后清动态路由、重置权限 store、跳转 `redirect`；验证码弹窗 `wrap-class` 随 `loginLayoutPosition`。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <ForgotForm
    v-if="viewMode === 'forget'"
    :embedded="true"
    @back="switchView('login')"
  />
  <RegisterForm
    v-else-if="viewMode === 'register'"
    :embedded="true"
    @back="switchView('login')"
  />
  <div
    v-else
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
    <!-- 右上角按钮组 -->
    <div class="button-group-wrapper">
      <a-button-group size="small">
        <!-- 颜色切换 -->
        <a-radio-button size="small">
          <TaktColorToggle type="icon" />
        </a-radio-button>
        <!-- 表单位置切换 -->
        <a-radio-button size="small">
          <TaktLayoutToggle v-model:position="layoutPosition" />
        </a-radio-button>
        <!-- 语言切换 -->
        <a-radio-button size="small">
          <TaktLocaleToggle type="icon" />
        </a-radio-button>
        <!-- 主题切换 -->
        <a-radio-button size="small">
          <TaktThemeToggle type="icon" />
        </a-radio-button>
      </a-button-group>
    </div>
    <!-- 登录容器 -->
    <div class="login-container">
      <!-- 左侧内容区（仅在非居中模式下显示） -->
      <div
        v-if="layoutPosition !== 'center'"
        class="login-left-panel"
      >
        <div class="login-illustration">
          <ShowcaseForm />
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
      <!-- 右侧表单区 -->
      <div
        class="login-right-panel"
        :class="`panel-${layoutPosition}`"
        :style="{ background: panelBackgroundGradient }"
      >
        <div class="login-box">
          <div class="login-header">
            <a-typography-title :level="3">
              {{ $t('login.login.title') }}
            </a-typography-title>
          </div>
          <a-form
            ref="formRef"
            :model="formState"
            :rules="rules"
            class="login-form"
            @finish="handleSubmit"
          >
            <a-form-item name="username">
              <a-input
                v-model:value="formState.username"
                :placeholder="$t('login.fields.username.placeholder')"
                size="large"
                autocomplete="username"
              >
                <template #prefix>
                  <RiUserLine />
                </template>
              </a-input>
            </a-form-item>
            <a-form-item name="password">
              <a-input-password
                v-model:value="formState.password"
                :placeholder="$t('login.fields.password.placeholder')"
                size="large"
                autocomplete="current-password"
                :input-attrs="{ autocomplete: 'current-password' }"
              >
                <template #prefix>
                  <RiLockLine />
                </template>
              </a-input-password>
            </a-form-item>
            <a-form-item>
              <div class="login-form-options">
                <a-checkbox v-model:checked="formState.rememberMe">
                  {{ $t('login.login.rememberMe') }}
                </a-checkbox>
                <a
                  v-if="showForgotPassword"
                  href="javascript:;"
                  class="login-forgot-link"
                  @click.prevent="switchView('forget')"
                >
                  {{ $t('login.forgot.title') }}
                </a>
              </div>
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
                  <RiLoginBoxLine />
                </template>
                {{ $t('login.login.login') }}
              </a-button>
            </a-form-item>
            <a-form-item
              v-if="showRegister"
              class="login-register-row"
            >
              <a
                href="javascript:;"
                class="login-register-link"
                @click.prevent="switchView('register')"
              >
                {{ $t('login.login.noAccountRegister', { register: $t('login.sign.title') }) }}
              </a>
            </a-form-item>
          </a-form>
        </div>
      </div>
    </div>

    <!-- 验证码弹窗：位置跟随登录表单（居左/居中/居右），固定 500x250 -->
    <a-modal
      v-model:open="captchaModalVisible"
      :title="$t('login.fields.captcha.validation.required')"
      :footer="null"
      :closable="true"
      :mask-closable="false"
      :width="captchaModalWidth"
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
 * 登录页：表单校验、验证码门禁、登录成功后的路由与权限清理。
 */
import { reactive, ref, onMounted, computed, onBeforeUnmount } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import ForgotForm from './components/forgot-form.vue'
import RegisterForm from './components/register-form.vue'
import ShowcaseForm from './components/showcase-form.vue'
import { useI18n } from 'vue-i18n'
import { useUserStore } from '@/stores/identity/user'
import { useThemeStore } from '@/stores/theme'
import { storeToRefs } from 'pinia'
import { defaultSetting, useSettingStore, getThemeColorValue } from '@/stores/setting'
import { message } from 'ant-design-vue'
import { RiUserLine, RiLockLine, RiLoginBoxLine } from '@remixicon/vue'
import type { Rule } from 'ant-design-vue/es/form'
import logoSvg from '@/assets/images/takt.svg'
import TaktCaptchaSlider from '@/components/common/takt-captcha-slider/index.vue'
import TaktCaptchaBehavior from '@/components/common/takt-captcha-behavior/index.vue'
import { generateCaptcha as generateCaptchaApi, type CaptchaGenerateResult } from '@/api/identity/captcha'
import { logger } from '@/utils/logger'
import { appVersion } from '@/utils/appMeta'

/** Vue Router 实例，登录成功后 `push(redirect)` */
const router = useRouter()
/** 当前路由，读取 `query.redirect` */
const route = useRoute()
/** 身份与登录 action */
const userStore = useUserStore()
/** 明暗主题，参与背景渐变 */
const themeStore = useThemeStore()
const settingStore = useSettingStore()
/** 设置中的主题色配置 */
const { setting: settingState } = storeToRefs(settingStore)
const { t } = useI18n()

/**
 * 忘记密码 / 注册入口开关：仅读 `defaultSetting`，避免 localStorage 覆盖导致与后端策略不一致。
 */
const {
  showForgotPassword: showForgotPasswordSetting,
  showRegister: showRegisterSetting
} = defaultSetting
/** 是否显示「忘记密码」链接 */
const showForgotPassword = showForgotPasswordSetting === true
/** 是否显示「注册」链接 */
const showRegister = showRegisterSetting === true

/** 登录验证码弹窗可见 */
const captchaModalVisible = ref(false)
/** 验证码组件初始数据 */
const captchaModalData = ref<CaptchaGenerateResult | null>(null)
/** Slider 或 Behavior */
const captchaModalType = ref<'Slider' | 'Behavior'>('Behavior')

/** 动态验证码子组件 */
const captchaModalComponent = computed(() =>
  captchaModalType.value === 'Slider' ? TaktCaptchaSlider : TaktCaptchaBehavior
)

/** 弹窗宽度（px），与 body 区配合 */
const captchaModalWidth = 450
/** 弹窗 body 固定尺寸，容纳验证码组件 */
const captchaModalBodyStyle = {
  width: '400px',
  height: '200px',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  margin: 0
}

/** 弹窗水平位置随 `layoutPosition` 与表单列对齐 */
const captchaModalWrapClassName = computed(() => `login-captcha-modal-wrap login-captcha-modal--${layoutPosition.value}`)

/** 当前子视图：主登录 / 忘记密码 / 注册 */
type LoginViewMode = 'login' | 'forget' | 'register'
/** 内嵌切换状态，不设独立路由 */
const viewMode = ref<LoginViewMode>('login')
/** 切换 `viewMode`（忘记密码、注册、返回登录） */
function switchView(mode: LoginViewMode) {
  viewMode.value = mode
}

/** 解析后的主题主色 */
const themeColor = computed(() => getThemeColorValue(settingState.value?.themeColor ?? { type: 'blue' }))

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

/** Logo 静态资源 */
const appLogo = computed(() => {
  return logoSvg
})

/** 产品代号 + 构建版本 */
const productCodeWithVersion = computed(() => {
  return `${t('common.app.productcode')} V${appVersion}`
})

/** 与 `TaktLayoutToggle` 同步的 `localStorage` 键 */
const savedLayoutPosition = localStorage.getItem('loginLayoutPosition') as
  | 'left'
  | 'center'
  | 'right'
  | null
/** 登录区左/中/右布局 */
const layoutPosition = ref<'left' | 'center' | 'right'>(
  savedLayoutPosition || 'right'
)

/** 开发环境用于本地默认账号密码 */
const isDev = import.meta.env.DEV || import.meta.env.MODE === 'development'

/** 登录表单模型，与 `userStore.login` 约定一致 */
const formState = reactive({
  username: isDev ? 'admin' : '',
  password: isDev ? '123456' : '',
  rememberMe: false
})

/** 提交与 `doLogin` 过程中的 loading */
const loading = ref(false)

/** 登录表单 ref */
const formRef = ref()

/** 用户名、密码校验规则 */
const rules = computed<Record<string, Rule[]>>(() => ({
  username: [
    { required: true, message: t('login.fields.username.validation.required'), trigger: 'blur' }
  ],
  password: [
    { required: true, message: t('login.fields.password.validation.required'), trigger: 'blur' },
    { min: 6, message: t('login.fields.password.validation.min'), trigger: 'blur' }
  ]
}))

/** 验证码关闭或未启用后执行 `userStore.login` 并处理路由与权限 */
async function doLogin() {
  try {
    loading.value = true
    logger.info('[Login] 开始登录，用户名:', formState.username)
    await userStore.login(formState)

    try {
      const { clearDynamicRoutes } = await import('@/router')
      clearDynamicRoutes()
    } catch (error) {
      logger.error('[Login] 清除路由失败:', error)
    }

    const { usePermissionStore } = await import('@/stores/identity/permission')
    const permissionStore = usePermissionStore()
    permissionStore.reset()
    logger.info('[Login] 登录成功，已清除路由并重置权限路由状态，用户名:', formState.username)

    try {
      const { resetRedirectingToLoginFlag } = await import('@/api/request')
      resetRedirectingToLoginFlag()
    } catch (error) {
      logger.error('[Login] 重置跳转登录页标志失败:', error)
    }

    const redirect = (route.query.redirect as string) || '/dashboard/workspace'
    await router.push(redirect)
    message.success('登录成功')
    loading.value = false
  } catch (error: any) {
    logger.error('[Login] 登录失败，用户名:', formState.username, '错误:', error.message || error)
    message.error(error.message || '登录失败')
    captchaModalVisible.value = false
    loading.value = false
  }
}

/** 验证码通过后关弹窗并登录 */
function onCaptchaModalSuccess() {
  captchaModalVisible.value = false
  doLogin()
}

/** 验证码校验失败提示 */
function onCaptchaModalFail(msg: string) {
  message.error(msg || t('login.fields.captcha.validation.required'))
}

/** 表单提交：先校验再拉验证码配置 */
const handleSubmit = async () => {
  try {
    await formRef.value?.validateFields(['username', 'password'])
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
      await doLogin()
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
    logger.error('[Login] 获取验证码配置失败', error)
    message.error(error?.message || t('login.fields.captcha.validation.required'))
    loading.value = false
  }
}

onBeforeUnmount(() => {})

/** 挂载时请求健康检查以携带 Cookie，利于 CSRF 场景 */
onMounted(async () => {
  try {
    const axios = (await import('axios')).default
    await axios.get('/api/TaktHealth', {
      baseURL: '',
      withCredentials: true
    })
  } catch (error: any) {
    logger.warn('[CSRF] 获取 CSRF Token 失败:', error)
  }
})
</script>

<style scoped lang="less">
@import './login.less';
</style>

<!-- 验证码弹窗位置跟随表单：上下始终居中，左右随表单（弹窗在 body，需全局样式） -->
<style lang="less">
/* 覆盖 ant-modal 默认定位，改为 fixed + 明确 left/top，上下始终居中 */
.login-captcha-modal-wrap.ant-modal-wrap .ant-modal {
  position: fixed !important;
  top: 50% !important;
  margin: 0 !important;
  padding-bottom: 0;
  display: block !important;
  vertical-align: unset !important;
  transform: translate(-50%, -50%) !important;
}
/* 居左：与表单左侧 1/3 区域对齐 */
.login-captcha-modal--left.ant-modal-wrap .ant-modal {
  left: 16.666% !important;
}
/* 居中：与表单中间对齐 */
.login-captcha-modal--center.ant-modal-wrap .ant-modal {
  left: 50% !important;
}
/* 居右：与表单右侧 1/3 区域对齐 */
.login-captcha-modal--right.ant-modal-wrap .ant-modal {
  left: 83.333% !important;
}
</style>