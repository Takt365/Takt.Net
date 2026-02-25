<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/login -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：登录页面，包含用户登录、主题切换、语言切换等功能 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <ForgetView
    v-if="viewMode === 'forget'"
    :embedded="true"
    @back="switchView('login')"
  />
  <RegisterView
    v-else-if="viewMode === 'register'"
    :embedded="true"
    @back="switchView('login')"
  />
  <div v-else class="login-page" :class="[`position-${layoutPosition}`, `theme-${themeStore.themeMode}`]">
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
        <!-- 颜色切换 -->
        <a-radio-button size="small"> <TaktColorToggle type="icon" /></a-radio-button>
        <!-- 表单位置切换 -->
        <a-radio-button size="small"> <TaktLayoutToggle v-model:position="layoutPosition" /></a-radio-button>
        <!-- 语言切换 -->
        <a-radio-button size="small"> <TaktLocaleToggle type="icon" /></a-radio-button>
        <!-- 主题切换 -->
        <a-radio-button size="small"> <TaktThemeToggle type="icon" /></a-radio-button>
      </a-button-group>
    </div>
    <!-- 登录容器 -->
    <div class="login-container">
      <!-- 左侧内容区（仅在非居中模式下显示） -->
      <div v-if="layoutPosition !== 'center'" class="login-left-panel">
        <div class="login-illustration">
          <div class="login-svgator-wrap">
            <Svgator />
          </div>
          <div class="login-slogan">
            <a-typography-text class="login-slogan-text">{{ $t('common.app.slogan') }}</a-typography-text>
            <a-typography-text class="login-tagline">{{ $t('common.app.tagline') }}</a-typography-text>
          </div>
        </div>
      </div>
      <!-- 右侧表单区 -->
      <div class="login-right-panel" :class="`panel-${layoutPosition}`" :style="{ background: panelBackgroundGradient }">
        <div class="login-box">
          <div class="login-header">
            <a-typography-title :level="3">{{ $t('login.login.title') }}</a-typography-title>
          </div>
          <a-form
            ref="formRef"
            :model="formState"
            :rules="rules"
            @finish="handleSubmit"
            class="login-form"
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
                <a v-if="showForgotPassword" href="javascript:;" class="login-forgot-link" @click.prevent="switchView('forget')">
                  {{ $t('login.forgot.title') }}
                </a>
              </div>
            </a-form-item>
            <a-form-item>
              <a-button type="primary" html-type="submit" block size="large" :loading="loading">
                <template #icon>
                  <RiLoginBoxLine />
                </template>
                {{ $t('login.login.login') }}
              </a-button>
            </a-form-item>
            <a-form-item v-if="showRegister" class="login-register-row">
              <a href="javascript:;" class="login-register-link" @click.prevent="switchView('register')">
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
      :maskClosable="false"
      :width="captchaModalWidth"
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
import { useRouter, useRoute } from 'vue-router'
import ForgetView from './components/forgot-password/index.vue'
import RegisterView from './components/register-form/index.vue'
import Svgator from './components/svgator-player/index.vue'
import { useI18n } from 'vue-i18n'
import { useUserStore } from '@/stores/identity/user'
import { useThemeStore } from '@/stores/theme'
import { storeToRefs } from 'pinia'
import { defaultSetting, useSettingStore, getEffectiveThemeColorValue } from '@/stores/setting'
import { message, Modal } from 'ant-design-vue'
import { RiUserLine, RiLockLine, RiLoginBoxLine } from '@remixicon/vue'
import type { Rule } from 'ant-design-vue/es/form'
import logoSvg from '@/assets/images/takt.svg'
import TaktCaptchaSlider from '@/components/common/takt-captcha-slider/index.vue'
import TaktCaptchaBehavior from '@/components/common/takt-captcha-behavior/index.vue'
import { generateCaptcha as generateCaptchaApi, type CaptchaGenerateResult } from '@/api/identity/captcha'
import { logger } from '@/utils/logger'
import { appVersion } from '@/utils/appMeta'
const router = useRouter()
const route = useRoute()
const userStore = useUserStore()
const themeStore = useThemeStore()
const settingStore = useSettingStore()
const { setting: settingState } = storeToRefs(settingStore)
const { t } = useI18n()

// 登录页配置：忘记密码、注册入口（仅用 defaultSetting，不受 localStorage 覆盖，保证 setting.ts 未启用时不显示）
const {
  showForgotPassword: showForgotPasswordSetting,
  showRegister: showRegisterSetting
} = defaultSetting
const showForgotPassword = showForgotPasswordSetting === true
const showRegister = showRegisterSetting === true

// 验证码弹窗：点登录后若后端启用验证码则弹出，验证成功后再登录
const captchaModalVisible = ref(false)
const captchaModalData = ref<CaptchaGenerateResult | null>(null)
const captchaModalType = ref<'Slider' | 'Behavior'>('Behavior')

const captchaModalComponent = computed(() =>
  captchaModalType.value === 'Slider' ? TaktCaptchaSlider : TaktCaptchaBehavior
)

// 弹窗尺寸统一 400x200
const captchaModalWidth = 450
const captchaModalBodyStyle = {
  width: '400px',
  height: '200px',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  margin: 0
}

// 弹窗位置跟随登录表单排版（居左/居中/居右）
const captchaModalWrapClassName = computed(() => `login-captcha-modal-wrap login-captcha-modal--${layoutPosition.value}`)

// 登录页内嵌视图：仅通过点击切换，禁止单独路由访问
type LoginViewMode = 'login' | 'forget' | 'register'
const viewMode = ref<LoginViewMode>('login')
function switchView(mode: LoginViewMode) {
  viewMode.value = mode
}

const themeColor = computed(() => getEffectiveThemeColorValue(settingState.value?.themeColor ?? { type: 'blue' }))

// 计算背景径向渐变样式
const backgroundGradient = computed(() => {
  const color = themeColor.value
  if (themeStore.themeMode === 'dark') {
    // 暗黑主题：选中颜色中心，径向渐变到黑色边缘
 return `radial-gradient(circle at 750% 50%, ${color} 0%, #000000 100%)`
  } else {
    // 明亮主题：选中颜色中心，径向渐变到白色边缘
    return `radial-gradient(circle at 750% 50%, ${color} 0%, #ffffff 100%)`
  }
})

// 计算表单区域背景径向渐变样式（与主渐变相反）
const panelBackgroundGradient = computed(() => {
  const color = themeColor.value
  if (themeStore.themeMode === 'dark') {
    // 暗黑主题：选中颜色中心，径向渐变到黑色边缘
    return `radial-gradient(circle at 750% 50%, ${color} 0%, #000000 100%)`
  } else {
    // 明亮主题：选中颜色中心，径向渐变到白色边缘
    return `radial-gradient(circle at 750% 50%, ${color} 0%, #ffffff 100%)`
  }
})

// Logo 路径：直接使用导入的 SVG，确保在开发和生产环境都能正确加载
const appLogo = computed(() => {
  return logoSvg
})

// 拼接 productcode 和 version（版本号来自 package.json，构建时注入）
const productCodeWithVersion = computed(() => {
  return `${t('common.app.productcode')} V${appVersion}`
})

// 登录表单在页面中的位置（默认右对齐）
// 注意：这只是控制登录表单的位置，不是全局布局
const savedLayoutPosition = localStorage.getItem('loginLayoutPosition') as
  | 'left'
  | 'center'
  | 'right'
  | null
const layoutPosition = ref<'left' | 'center' | 'right'>(
  savedLayoutPosition || 'right'
)

// 开发环境默认值（方便开发测试）
const isDev = import.meta.env.DEV || import.meta.env.MODE === 'development'

const formState = reactive({
  username: isDev ? 'admin' : '', // 开发环境默认用户名
  password: isDev ? '123456' : '', // 开发环境默认密码
  rememberMe: false
})

// 登录加载状态
const loading = ref(false)

const formRef = ref()

const rules = computed<Record<string, Rule[]>>(() => ({
  username: [
    { required: true, message: t('login.fields.username.validation.required'), trigger: 'blur' }
  ],
  password: [
    { required: true, message: t('login.fields.password.validation.required'), trigger: 'blur' },
    { min: 6, message: t('login.fields.password.validation.min'), trigger: 'blur' }
  ]
}))

// 实际执行登录（验证码已通过或未启用时调用）
// forceLogin：为 true 时携带 force_login 参数，后端将踢掉已有会话并通知对方
async function doLogin(forceLogin?: boolean) {
  try {
    loading.value = true
    logger.info('[Login] 开始登录，用户名:', formState.username, forceLogin ? '，强制登录' : '')
    const loginParams = {
      username: formState.username,
      password: formState.password,
      rememberMe: formState.rememberMe,
      force_login: forceLogin === true
    }
    await userStore.login(loginParams)

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
    message.success(t('login.login.success'))
    loading.value = false
  } catch (error: any) {
    // 已在其他位置登录：弹窗让用户选择强制登录或取消
    if (error?.code === 'already_logged_in_elsewhere') {
      loading.value = false
      captchaModalVisible.value = false
      Modal.confirm({
        title: t('login.login.alreadyElsewhereTitle'),
        content: error.error_description || t('login.login.alreadyElsewhereContent'),
        okText: t('login.login.forceLogin'),
        cancelText: t('common.button.cancel'),
        onOk: () => doLogin(true)
      })
      return
    }
    logger.error('[Login] 登录失败，用户名:', formState.username, '错误:', error.message || error)
    message.error(error.message || t('login.login.fail'))
    captchaModalVisible.value = false
    loading.value = false
  }
}

// 验证码弹窗内验证成功：关闭弹窗并执行登录
function onCaptchaModalSuccess() {
  captchaModalVisible.value = false
  doLogin()
}

function onCaptchaModalFail(msg: string) {
  message.error(msg || t('login.fields.captcha.validation.required'))
}

const handleSubmit = async () => {
  try {
    await formRef.value?.validateFields(['username', 'password'])
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
      // 未启用验证码，直接登录
      loading.value = false
      await doLogin()
      return
    }

    // 启用验证码：后端必须返回 type，否则提示错误
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

// 组件卸载时清理资源
onBeforeUnmount(() => {})

// 组件挂载时获取 CSRF Token
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