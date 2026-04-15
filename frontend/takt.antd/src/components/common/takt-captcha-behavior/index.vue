<template>
  <div ref="containerRef" class="takt-captcha-behavior-wrapper">
    <!-- 按登录页视口/表单宽度缩放，与同表单输入框一致 -->
    <div class="takt-captcha-behavior-scaled" :style="scaledStyle">
      <div class="takt-captcha-behavior-inner" :style="innerScaledStyle">
    <!-- 仅无数据时的加载状态（避免刷新/验证时整块切换导致闪烁） -->
    <div v-if="loading && !captchaData" class="captcha-loading">
      <a-spin :tip="$t('components.common.captcha.loading')" />
    </div>

    <!-- 验证码容器 -->
    <div
      v-else-if="captchaData"
      class="takt-captcha-behavior-container"
      :style="{ '--takt-captcha-primary': themeColor }"
    >
      <!-- 刷新/验证时的加载遮罩（不替换 DOM，避免闪烁） -->
      <div v-if="loading" class="captcha-overlay">
        <a-spin :tip="$t('components.common.captcha.loading')" />
      </div>
      <!-- 滑块区域 -->
      <div
        v-if="captchaData.enabled !== false"
        ref="wrapperRef"
        class="captcha-slider"
        :class="{ verified: verified, toLeft: toLeft, 'is-dragging': isDragging && !verified }"
        :style="{
          width: `${props.width}px`,
          height: `${props.height}px`
        }"
        @mouseleave="handleDragOver"
        @mousemove="handleDragMoving"
        @mouseup="handleDragOver"
        @touchend="handleDragOver"
        @touchmove="handleDragMoving"
      >
        <!-- 进度条（主题色背景） -->
        <div
          ref="barRef"
          class="slider-progress"
          :class="{ 'to-left': toLeft }"
          :style="barStyle"
        ></div>

        <!-- 提示文字（居中显示） -->
        <div
          ref="contentRef"
          class="slider-tip-text"
          :class="{ success: verified }"
        >
          <a-typography-text v-if="!verified" class="shine-text">{{ $t('components.common.captcha.slideToTarget', { position: targetPosition }) }}</a-typography-text>
          <a-typography-text v-else class="success-text">{{ $t('components.common.captcha.success') }}</a-typography-text>
        </div>

        <!-- 目标位置指示器 -->
        <div
          v-if="!verified"
          class="target-indicator"
          :style="targetIndicatorStyle"
        >
          <div class="indicator-line"></div>
        </div>

        <!-- 滑块手柄 -->
        <div
          ref="actionRef"
          class="slider-handle"
          :class="{ 
            'is-dragging': isDragging && !verified,
            'rounded-md': isDragging && !verified,
            'to-left': toLeft
          }"
          :style="actionStyle"
          @mousedown.stop="handleDragStart"
          @touchstart.stop="handleDragStart"
        >
          <RiArrowRightDoubleLine v-if="!verified" class="handle-icon" />
          <RiCheckLine v-else class="handle-icon" />
        </div>
      </div>
      <!-- 验证码未启用时的占位区域 -->
      <div
        v-else
        class="captcha-disabled-placeholder"
        :style="{
          width: `${props.width}px`,
          height: `${props.height}px`
        }"
      >
        <a-alert :message="$t('components.common.captcha.disabled')" type="info" show-icon :banner="true" />
      </div>
    </div>

    <!-- 错误状态 -->
    <div v-else-if="errorMessage" class="captcha-error">
      <a-alert :message="errorMessage" type="error" show-icon />
      <a-button type="link" @click="generate">{{ $t('components.common.captcha.regenerate') }}</a-button>
    </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onUnmounted, nextTick } from 'vue'
import { RiCheckLine, RiArrowRightDoubleLine } from '@remixicon/vue'
import { generateCaptcha, verifyCaptcha, type CaptchaGenerateResult, type CaptchaVerifyRequest } from '@/api/identity/captcha'
import { storeToRefs } from 'pinia'
import { useI18n } from 'vue-i18n'
import { useSettingStore, getThemeColorValue } from '@/stores/setting'
import { logger } from '@/utils/logger'

interface Props {
  /** 验证码宽度（像素） */
  width?: number
  /** 验证码高度（像素） */
  height?: number
  /** 是否自动生成验证码 */
  autoGenerate?: boolean
  /** 预填数据（如登录弹窗由外部先拉取验证码时传入，不传则组件内部生成） */
  initialData?: CaptchaGenerateResult | null
}

interface Emits {
  (e: 'success'): void
  (e: 'fail', message: string): void
  (e: 'verified', verified: boolean): void
}

const DESIGN_WIDTH = 400
const DESIGN_HEIGHT = 40

const props = withDefaults(defineProps<Props>(), {
  width: DESIGN_WIDTH,
  height: DESIGN_HEIGHT,
  autoGenerate: true,
  initialData: null
})

const emit = defineEmits<Emits>()
const { t } = useI18n()
const { setting } = storeToRefs(useSettingStore())
const themeColor = computed(() => getThemeColorValue(setting.value?.themeColor ?? { type: 'blue' }))

// 状态
const loading = ref(false)
const captchaData = ref<CaptchaGenerateResult | null>(null)
const verified = ref(false)
const errorMessage = ref('')
const targetPosition = ref(0)

// 拖拽状态
const isDragging = ref(false)
const moveDistance = ref(0)
const toLeft = ref(false)
const startTime = ref(0)
const endTime = ref(0)
const mouseTrajectory = ref<Array<{ x: number; y: number; t: number }>>([])

// Refs
const containerRef = ref<HTMLDivElement>()
const wrapperRef = ref<HTMLDivElement>()
const actionRef = ref<HTMLDivElement>()
const barRef = ref<HTMLDivElement>()
const contentRef = ref<HTMLDivElement>()

// 视口缩放：宽度随容器，高度固定为 DESIGN_HEIGHT（仅横向 scale）
const scale = ref(1)
const scaledStyle = computed(() => ({
  width: `${DESIGN_WIDTH * scale.value}px`,
  height: `${DESIGN_HEIGHT}px`,
  overflow: 'hidden',
  margin: '0 auto'
}))
const innerScaledStyle = computed(() => ({
  transform: `scale(${scale.value}, 1)`,
  transformOrigin: 'top center',
  width: `${DESIGN_WIDTH}px`
}))

// 滑块手柄宽度
const ACTION_WIDTH = 48

// 目标指示器位置样式（按“手柄最右边缘”对应百分比计算，排除左右不可达区域）
const targetIndicatorStyle = computed(() => {
  const wrapperWidth = wrapperRef.value?.offsetWidth ?? props.width
  const actionWidth = ACTION_WIDTH
  // 可达的“手柄右边缘”范围：[minRight, maxRight]
  const minRight = actionWidth
  const maxRight = Math.max(minRight, wrapperWidth)
  const effectiveWidth = Math.max(1, maxRight - minRight)

  // 将后端给出的 0-100% 映射到可达区域 [minRight, maxRight]
  const clampedTarget = Math.min(Math.max(targetPosition.value, 0), 100)
  const rightX = minRight + (clampedTarget / 100) * effectiveWidth

  return {
    left: `${rightX}px`
  }
})

// 滑块手柄样式
const actionStyle = computed(() => {
  const left = actionRef.value?.style.left || '0px'
  return {
    left: left
  }
})

// 进度条样式
const barStyle = computed(() => {
  const width = barRef.value?.style.width || '0px'
  return {
    width: width
  }
})

// 获取事件pageX坐标
function getEventPageX(e: MouseEvent | TouchEvent): number {
  if ('pageX' in e) {
    return e.pageX
  } else if ('touches' in e && e.touches[0]) {
    return e.touches[0].pageX
  }
  return 0
}

// 获取事件pageY坐标
function getEventPageY(e: MouseEvent | TouchEvent): number {
  if ('pageY' in e) {
    return e.pageY
  } else if ('touches' in e && e.touches[0]) {
    return e.touches[0].pageY
  }
  return 0
}

// 获取偏移量
function getOffset() {
  const wrapperWidth = wrapperRef.value?.offsetWidth ?? props.width
  const actionWidth = ACTION_WIDTH
  const offset = wrapperWidth - actionWidth - 6
  return { actionWidth, offset, wrapperWidth }
}

// 设置滑块位置
function setActionLeft(val: string) {
  if (actionRef.value) {
    actionRef.value.style.left = val
  }
}

// 设置进度条宽度
function setBarWidth(val: string) {
  if (barRef.value) {
    barRef.value.style.width = val
  }
}

// 开始拖拽
function handleDragStart(e: MouseEvent | TouchEvent) {
  if (verified.value || !captchaData.value || !actionRef.value) {
    return
  }

  e.preventDefault()
  e.stopPropagation()

  const currentLeft = Number.parseInt(actionRef.value.style.left.replace('px', '') || '0', 10)
  moveDistance.value = getEventPageX(e) - currentLeft
  startTime.value = Date.now()
  isDragging.value = true
  mouseTrajectory.value = []

  // 记录起始点
  if (wrapperRef.value) {
    const rect = wrapperRef.value.getBoundingClientRect()
    const x = getEventPageX(e) - rect.left
    const y = getEventPageY(e) - rect.top
    mouseTrajectory.value.push({ x, y, t: 0 })
  }

  logger.debug('[Captcha Behavior] 开始拖拽', { moveDistance: moveDistance.value, currentLeft })
}

// 拖拽中
function handleDragMoving(e: MouseEvent | TouchEvent) {
  if (!isDragging.value || verified.value || !captchaData.value || !actionRef.value || !barRef.value) return

  e.preventDefault()
  e.stopPropagation()

  const { actionWidth, offset, wrapperWidth } = getOffset()
  const moveX = getEventPageX(e) - moveDistance.value

  // 记录轨迹点
  if (wrapperRef.value) {
    const rect = wrapperRef.value.getBoundingClientRect()
    const relativeX = getEventPageX(e) - rect.left
    const relativeY = getEventPageY(e) - rect.top
    const elapsed = Date.now() - startTime.value
    mouseTrajectory.value.push({
      x: relativeX,
      y: relativeY,
      t: elapsed
    })
  }

  if (moveX > 0 && moveX <= offset) {
    const left = moveX
    setActionLeft(`${left}px`)
    // 进度条宽度到达滑块手柄最右边缘，与滑块完全同步
    setBarWidth(`${left + actionWidth}px`)
  } else if (moveX > offset) {
    // 超过最大位置，限制在最大可拖动位置
    const maxLeft = wrapperWidth - actionWidth
    setActionLeft(`${maxLeft}px`)
    // 进度条宽度到达滑块手柄最右边缘
    setBarWidth(`${maxLeft + actionWidth}px`)
  }
}

// 拖拽结束
function handleDragOver(e: MouseEvent | TouchEvent) {
  if (!isDragging.value || verified.value || loading.value) return

  e.preventDefault()
  e.stopPropagation()

  isDragging.value = false

  if (!actionRef.value || !barRef.value) return

  const moveX = getEventPageX(e) - moveDistance.value
  const { actionWidth, offset, wrapperWidth } = getOffset()

  // 先设置滑块位置和进度条宽度
  let finalLeft = moveX
  if (moveX < 0) {
    finalLeft = 0
  } else if (moveX > offset) {
    finalLeft = wrapperWidth - actionWidth
  }
  
  setActionLeft(`${finalLeft}px`)
  // 进度条宽度到达滑块手柄最右边缘，与滑块完全同步
  setBarWidth(`${finalLeft + actionWidth}px`)

  // 计算当前位置百分比（使用滑块手柄最右边缘），仅在接近目标（±5%）时才触发后端验证
  const actionRight = finalLeft + actionWidth
  const minRight = ACTION_WIDTH
  const maxRight = Math.max(minRight, wrapperWidth)
  const effectiveWidth = Math.max(1, maxRight - minRight)
  const clampedRight = Math.min(Math.max(actionRight, minRight), maxRight)
  const currentPosition = Math.round(((clampedRight - minRight) / effectiveWidth) * 100)
  const targetPos = targetPosition.value
  const positionDiff = Math.abs(currentPosition - targetPos)

  logger.debug('[Captcha Behavior] 拖拽结束，检查位置', {
    moveX,
    finalLeft,
    actionRight,
    currentPosition,
    targetPos,
    positionDiff,
    offset
  })

  // 检查是否到达目标位置附近（允许±5%的误差，增加容错性）
  if (positionDiff <= 5) {
    logger.debug('[Captcha Behavior] 到达目标位置附近，触发后端验证', {
      currentPosition,
      targetPos,
      diff: positionDiff
    })
    checkPass()
  } else {
    logger.debug('[Captcha Behavior] 未到达目标位置，前端复位不触发后端验证', {
      currentPosition,
      targetPos,
      diff: positionDiff
    })
    resume()
  }
}

// 检查是否通过
function checkPass() {
  if (!captchaData.value || loading.value || verified.value) return

  endTime.value = Date.now()
  const timeSpent = (endTime.value - startTime.value) / 1000
  
  // 计算滑块实际到达的位置百分比
  // 使用“滑块手柄最右边缘”相对于整个滑块宽度的百分比
  if (!actionRef.value || !wrapperRef.value) return
  
  const wrapperWidth = wrapperRef.value.offsetWidth
  const actionLeft = Number.parseFloat(actionRef.value.style.left.replace('px', '') || '0')
  const actionWidth = ACTION_WIDTH
  const actionRight = actionLeft + actionWidth

  // 将手柄右边缘位置映射到 0-100%（排除左右不可达区域）
  const minRight = ACTION_WIDTH
  const maxRight = Math.max(minRight, wrapperWidth)
  const effectiveWidth = Math.max(1, maxRight - minRight)
  const clampedRight = Math.min(Math.max(actionRight, minRight), maxRight)
  const position = Math.round(((clampedRight - minRight) / effectiveWidth) * 100)

  logger.debug('[Captcha Behavior] 计算位置并验证', { 
    wrapperWidth, 
    actionLeft, 
    actionWidth, 
    actionRight, 
    position,
    targetPosition: targetPosition.value,
    positionDiff: Math.abs(position - targetPosition.value)
  })

  // 调用后端验证
  verify(position, timeSpent)
}

// 验证验证码
async function verify(position: number, timeSpent: number) {
  if (!captchaData.value) return

  try {
    loading.value = true
    errorMessage.value = ''

    const userInput = {
      position,
      timeSpent,
      mouseTrajectory: mouseTrajectory.value.map(p => ({ x: p.x, y: p.y, t: p.t }))
    }

    const request: CaptchaVerifyRequest = {
      captchaId: captchaData.value.captchaId,
      userInput
    }

    logger.debug('[Captcha Behavior] 开始验证验证码', { 
      captchaId: request.captchaId,
      position,
      timeSpent: timeSpent.toFixed(2),
      trajectoryLength: userInput.mouseTrajectory?.length || 0
    })

    const result = await verifyCaptcha(request)

    if (result.success) {
      logger.info('[Captcha Behavior] 验证码验证成功', { captchaId: request.captchaId, position, timeSpent: timeSpent.toFixed(2) })
      verified.value = true
      isDragging.value = false // 验证成功后停止拖拽
      
      // 验证成功后，将滑块移动到最右侧
      if (actionRef.value && wrapperRef.value) {
        const wrapperWidth = wrapperRef.value.offsetWidth
        const actionWidth = ACTION_WIDTH
        const maxLeft = wrapperWidth - actionWidth
        setActionLeft(`${maxLeft}px`)
        setBarWidth(`${wrapperWidth}px`)
        logger.debug('[Captcha Behavior] 验证成功，滑块移动到最右侧', { maxLeft, wrapperWidth })
      }
      
      emit('success')
      emit('verified', true)
    } else {
      logger.warn('[Captcha Behavior] 验证码验证失败', { captchaId: request.captchaId, position, targetPosition: targetPosition.value, message: result.message })
      errorMessage.value = result.message || t('components.common.captcha.failed')
      emit('fail', errorMessage.value)
      emit('verified', false)
      // 先关闭 loading，等 DOM 恢复滑块后再执行 resume，否则 ref 为空无法重置
      loading.value = false
      await nextTick()
      resume()
      setTimeout(() => {
        generate()
      }, 1500)
    }
  } catch (error: any) {
    logger.error('[Captcha Behavior] 验证码验证异常', error)
    errorMessage.value = error.message || t('components.common.captcha.failed')
    emit('fail', errorMessage.value)
    emit('verified', false)
    loading.value = false
    await nextTick()
    resume()
    setTimeout(() => {
      generate()
    }, 1500)
  } finally {
    loading.value = false
  }
}

// 重置验证码（只重置UI状态，不生成新的验证码）
function resume() {
  logger.debug('[Captcha Behavior] 重置验证码UI状态')
  verified.value = false
  errorMessage.value = ''
  isDragging.value = false
  moveDistance.value = 0
  toLeft.value = false
  startTime.value = 0
  endTime.value = 0
  mouseTrajectory.value = []

  if (!actionRef.value || !barRef.value || !contentRef.value) return

  // 重置进度条宽度
  contentRef.value.style.width = '100%'
  toLeft.value = true

  // 300ms后重置滑块位置
  setTimeout(() => {
    toLeft.value = false
    setActionLeft('0')
    setBarWidth('0')
  }, 300)
}

// 生成验证码
const isGenerating = ref(false)
async function generate() {
  // 防止重复调用
  if (isGenerating.value) {
    logger.debug('[Captcha Behavior] 验证码正在生成中，跳过重复调用')
    return
  }
  
  try {
    isGenerating.value = true
    loading.value = true
    errorMessage.value = ''
    // 只有在未验证成功时才重置验证状态
    // 如果已经验证成功，保持验证状态（只有刷新页面才会重置）
    if (!verified.value) {
      verified.value = false
      // 不在此处清空 captchaData，避免刷新时整块被 loading 替换导致闪烁；仅在请求失败时清空以显示错误块
      resume()
    } else {
      // 如果已经验证成功，不重置状态，直接返回
      logger.debug('[Captcha Behavior] 验证已成功，跳过生成新验证码')
      return
    }

    logger.debug('[Captcha Behavior] 开始生成验证码')
    const result = await generateCaptcha()
    logger.debug('[Captcha Behavior] 验证码生成成功', { 
      captchaId: result.captchaId, 
      type: result.type,
      enabled: result.enabled, 
      targetPosition: result.targetPosition,
      fullResult: result 
    })
    
    // 如果 enabled 为 undefined 或 false，视为未启用
    if (result.enabled === false || result.enabled === undefined) {
      // 验证码未启用，直接通过
      logger.info('[Captcha Behavior] 验证码未启用（后端配置），直接通过验证')
      verified.value = true
      // 设置一个标记数据，让组件显示"已通过"状态
      captchaData.value = { ...result, enabled: false } as CaptchaGenerateResult
      emit('success')
      emit('verified', true)
      return
    }

    captchaData.value = result
    targetPosition.value = result.targetPosition || 0
    logger.debug('[Captcha Behavior] 验证码数据已设置', { targetPosition: targetPosition.value })
  } catch (error: any) {
    logger.error('[Captcha Behavior] 生成验证码失败', error)
    errorMessage.value = error.message || t('components.common.captcha.generateFailed')
    emit('fail', errorMessage.value)
    captchaData.value = null
  } finally {
    loading.value = false
    isGenerating.value = false
  }
}

// 30 秒自动刷新定时器（仅未验证时刷新）
const REFRESH_INTERVAL_MS = 30 * 1000
let refreshTimer: ReturnType<typeof setInterval> | null = null

// 根据容器宽度缩放（参照登录页表单输入框宽度）
function updateScale() {
  const el = containerRef.value
  if (!el) return
  const w = el.offsetWidth || el.clientWidth
  if (w > 0) {
    // 仅缩小不放大，避免 scale>1 时横向拉伸变形；容器≤400px 时与表单同宽
    scale.value = Math.min(1, w / DESIGN_WIDTH)
  }
}

let resizeObserver: ResizeObserver | null = null

// 使用外部传入的预填数据（不请求接口）
function applyInitialData() {
  const data = props.initialData
  if (!data) return false
  loading.value = false
  if (data.enabled === false || data.enabled === undefined) {
    verified.value = true
    captchaData.value = { ...data, enabled: false } as CaptchaGenerateResult
    emit('success')
    emit('verified', true)
    return true
  }
  captchaData.value = data
  targetPosition.value = data.targetPosition ?? 0
  return true
}

// 组件挂载时：有 initialData 则用预填，否则按 autoGenerate 生成并启动 30 秒自动刷新
onMounted(() => {
  updateScale()
  resizeObserver = new ResizeObserver(() => updateScale())
  if (containerRef.value) resizeObserver.observe(containerRef.value)
  if (applyInitialData()) {
    return
  }
  if (props.autoGenerate && !isGenerating.value) {
    generate()
  }
  refreshTimer = setInterval(() => {
    if (!verified.value && captchaData.value && captchaData.value.enabled !== false) {
      generate()
    }
  }, REFRESH_INTERVAL_MS)
})

onUnmounted(() => {
  if (resizeObserver && containerRef.value) {
    resizeObserver.unobserve(containerRef.value)
    resizeObserver = null
  }
  if (refreshTimer) {
    clearInterval(refreshTimer)
    refreshTimer = null
  }
})

// 暴露方法
defineExpose({
  generate,
  resume,
  verified: computed(() => verified.value)
})
</script>

<style scoped lang="less">
.takt-captcha-behavior-wrapper {
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: flex-start;

  .takt-captcha-behavior-scaled {
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  .takt-captcha-behavior-inner {
    flex-shrink: 0;
  }

  .captcha-loading {
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 100px;
    padding: 20px;
  }

  .takt-captcha-behavior-container {
    position: relative;
    display: flex;
    flex-direction: column;
    gap: 8px;
    width: 100%;
    align-items: center;

    .captcha-overlay {
      position: absolute;
      inset: 0;
      z-index: 100;
      display: flex;
      align-items: center;
      justify-content: center;
      background: rgba(255, 255, 255, 0.65);
      border-radius: 4px;
    }

    .captcha-slider {
      position: relative;
      border: 1px solid rgba(0, 0, 0, 0.06);
      border-radius: 4px;
      background: #f5f5f5;
      overflow: hidden;
      text-align: center;

      // 进度条（与主题色一致）
      .slider-progress {
        position: absolute;
        left: 0;
        top: 0;
        height: 100%;
        background: #3de1ad;
        width: 0;
        transition: width 0.2s cubic-bezier(0.4, 0, 0.2, 1);

        &.to-left {
          transition: width 0.3s ease;
          width: 0 !important;
        }
      }
      
      // 拖拽时禁用进度条过渡动画，确保实时跟随
      &.is-dragging .slider-progress {
        transition: none;
      }

      // 提示文字（居中显示）
      .slider-tip-text {
        position: absolute;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        width: 100%;
        height: 100%;
        display: flex;
        align-items: center;
        justify-content: center;
        z-index: 2;
        pointer-events: none;
        user-select: none;
        font-size: 12px;

        .shine-text {
          // 使用主题文本颜色创建渐变效果
          background: linear-gradient(
            90deg,
            color-mix(in srgb, rgba(0, 0, 0, 0.45) 30%, transparent) 0%,
            rgba(0, 0, 0, 0.45) 50%,
            color-mix(in srgb, rgba(0, 0, 0, 0.45) 30%, transparent) 100%
          );
          background-size: 200% 100%;
          background-clip: text;
          -webkit-background-clip: text;
          -webkit-text-fill-color: transparent;
          animation: shine 2s linear infinite;
          display: flex;
          align-items: center;
          height: 100%;
          
          // Ant Design Vue Typography 组件样式覆盖
          :deep(.ant-typography) {
            color: transparent;
            background: inherit;
            background-clip: text;
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            margin: 0;
          }
        }

        .success-text {
          font-weight: 500;
          opacity: 0.95;
          
          // Ant Design Vue Typography 组件样式覆盖
          :deep(.ant-typography) {
            color: #fff;
            margin: 0;
          }
        }

        &.success {
          -webkit-text-fill-color: #fff;
        }
      }

      // 目标位置指示器
      .target-indicator {
        position: absolute;
        top: 0;
        bottom: 0;
        transform: translateX(-50%);
        z-index: 1;
        pointer-events: none;

        .indicator-line {
          width: 2px;
          height: 100%;
          background: #ff4d4f;
          margin: 0 auto;
        }
      }

      // 滑块手柄（整体背景为主题色，与 takt-color-toggle 一致）
      .slider-handle {
        position: absolute;
        left: 0;
        top: 0;
        width: 48px;
        height: 100%;
        background: var(--takt-captcha-primary, #1890ff);
        border: 1px solid var(--takt-captcha-primary, #1890ff);
        border-radius: 4px;
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: e-resize;
        user-select: none;
        z-index: 10;
        transition: left 0.2s cubic-bezier(0.4, 0, 0.2, 1), background 0.2s ease, border-color 0.2s ease;

        &:hover {
          opacity: 0.9;
        }

        &.is-dragging {
          cursor: no-drop;
          transition: none;
        }

        &.rounded-md {
          border-radius: 4px;
        }

        &.to-left {
          transition: left 0.3s ease;
          left: 0 !important;
        }

        .handle-icon {
          color: #fff;
          font-size: 16px;
          width: 16px;
          height: 16px;
          cursor: inherit;
          pointer-events: none;
        }
      }
      
      // 验证成功后禁用整个滑块的交互
      &.verified {
        pointer-events: none;
        cursor: default;
        
        .slider-handle {
          cursor: default;
          pointer-events: none;
        }
      }

      // 验证通过后：进度条为绿色（成功态），手柄与默认一致（主题色 + 白图标）
      &.verified {
        .slider-progress {
          background: #3de1ad;
          width: calc(100% - 48px) !important;
          border-radius: 4px 0 0 4px;
          transition: width 0.3s ease, background 0.3s ease;
        }

        .slider-handle {
          left: calc(100% - 48px) !important;
          border-radius: 0 4px 4px 0;
          transition: left 0.3s ease;
        }
      }
    }

  }

  .captcha-error {
    display: flex;
    flex-direction: column;
    gap: 8px;
    align-items: center;
  }
}

// 暗黑主题
[data-doc-theme='dark'] {
  .takt-captcha-behavior-container {
    .captcha-overlay {
      background: rgba(0, 0, 0, 0.45);
    }

    .captcha-slider {
      border-color: rgba(255, 255, 255, 0.1);
      background: #262626;

      .slider-tip-text {
        .shine-text {
          background: linear-gradient(
            90deg,
            color-mix(in srgb, rgba(255, 255, 255, 0.45) 30%, transparent) 0%,
            rgba(255, 255, 255, 0.45) 50%,
            color-mix(in srgb, rgba(255, 255, 255, 0.45) 30%, transparent) 100%
          );
          background-size: 200% 100%;
          background-clip: text;
          -webkit-background-clip: text;
          -webkit-text-fill-color: transparent;
          animation: shine 2s linear infinite;
          display: flex;
          align-items: center;
          height: 100%;
          
          // Ant Design Vue Typography 组件样式覆盖
          :deep(.ant-typography) {
            color: transparent;
            background: inherit;
            background-clip: text;
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            margin: 0;
          }
        }

        .success-text {
          font-weight: 500;
          opacity: 0.95;
          
          // Ant Design Vue Typography 组件样式覆盖
          :deep(.ant-typography) {
            color: #fff;
            margin: 0;
          }
        }

        &.success {
          -webkit-text-fill-color: #fff;
        }
      }

      .slider-handle {
        background: var(--takt-captcha-primary, #1890ff);
        border-color: var(--takt-captcha-primary, #1890ff);

        &:hover,
        &.is-dragging {
          opacity: 0.9;
        }

        .handle-icon {
          color: #fff;
        }
      }

    }

  }
}

// 添加shine动画
@keyframes shine {
  0% {
    background-position: -200% 0;
  }
  100% {
    background-position: 200% 0;
  }
}
</style>
