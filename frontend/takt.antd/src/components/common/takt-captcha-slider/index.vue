<template>
  <div ref="containerRef" class="takt-captcha-slider-wrapper">
    <!-- 按登录页视口：图片区 400x100 等比缩放，轨道宽度随图片、轨道高度与输入框一致 -->
    <div class="takt-captcha-slider-scaled" :style="scaledStyle">
      <div class="takt-captcha-slider-inner">
    <!-- 仅无数据时的加载状态（避免刷新/验证时整块切换导致闪烁） -->
    <div v-if="loading && !captchaData" class="captcha-loading">
      <a-spin :tip="$t('components.common.captcha.loading')" />
    </div>

    <!-- 验证码容器 -->
    <div
      v-else-if="captchaData"
      class="takt-captcha-slider-container"
      :style="{ '--takt-captcha-primary': themeColor }"
    >
      <!-- 刷新/验证时的加载遮罩（不替换 DOM，避免闪烁） -->
      <div v-if="loading" class="captcha-overlay">
        <a-spin :tip="$t('components.common.captcha.loading')" />
      </div>
      <!-- 图片区域：按 400x100 等比缩放 -->
      <div v-if="captchaData.enabled !== false" class="takt-captcha-slider-image-wrap" :style="imageWrapStyle">
        <div class="takt-captcha-slider-image-scaled" :style="imageScaledStyle">
        <div
          ref="imageRef"
          class="captcha-image"
          :style="{
            width: `${props.width}px`,
            height: `${props.height}px`
          }"
        >
          <img
            v-if="captchaData.backgroundImage"
            :src="captchaData.backgroundImage"
            :alt="$t('components.common.captcha.slide')"
            class="bg-image"
            :class="{ 'clickable': !verified }"
            @click="refresh"
          />
          <img
            v-if="captchaData.sliderImage"
            :src="captchaData.sliderImage"
            :alt="$t('components.common.captcha.slideToVerify')"
            class="slider-image"
            :style="{ left: `${sliderLeft}px` }"
          />
        </div>
        </div>
      </div>
      <!-- 验证码未启用时的占位区域（同图片区等比缩放） -->
      <div
        v-else
        class="takt-captcha-slider-image-wrap"
        :style="imageWrapStyle"
      >
        <div class="takt-captcha-slider-image-scaled" :style="imageScaledStyle">
        <div
          class="captcha-disabled-placeholder"
          :style="{
            width: `${props.width}px`,
            height: `${props.height}px`
          }"
        >
          <a-alert :message="$t('components.common.captcha.disabled')" type="info" show-icon :banner="true" />
        </div>
        </div>
      </div>

      <!-- 滑块轨道：仅横向缩放，高度与表单输入框一致 -->
      <div
        v-if="captchaData.enabled !== false"
        class="takt-captcha-slider-track-wrap"
        :style="trackWrapStyle"
      >
        <div class="takt-captcha-slider-track-scaled" :style="trackScaledStyle">
        <div
          class="slider-track"
          ref="trackRef"
          :style="{
            width: `${props.width}px`
          }"
        >
        <!-- 背景轨道 -->
        <div class="slider-bg"></div>
        
        <!-- 滑块手柄 -->
        <div
          class="slider-thumb"           
          :class="{ 
            'dragging': isDragging, 
            'success': verified,
            'hover': !isDragging && !verified && thumbLeft === 0
          }"
          :style="{ left: thumbLeft + 'px' }"
          @mousedown="handleDragStart"
          @touchstart="handleDragStart"
        >
          <div class="thumb-icon" >
            <RiArrowRightDoubleLine v-if="!verified" />
            <RiCheckLine v-else />
          </div>
        </div>
        
        <!-- 进度条 -->
        <div 
          class="progress-bar"
          :style="{ width: `${thumbLeft + 48}px` }"
        ></div>
        
        <!-- 提示文字（居中显示） -->
        <div
          class="slider-tip-text"
          :class="{ 
            success: verified,
            'text-left': verified && successTextPosition === 'left',
            'text-right': verified && successTextPosition === 'right',
            'text-center': !verified || successTextPosition === 'center'
          }"
        >
          <a-typography-text v-if="!verified" class="shine-text">{{ $t('components.common.captcha.slideToVerify') }}</a-typography-text>
          <a-typography-text v-else class="success-text">{{ $t('components.common.captcha.success') }}</a-typography-text>
        </div>
        </div>
        </div>
      </div>

      <!-- 错误提示 -->
      <div v-if="errorMessage" class="captcha-tip">
        <a-typography-text class="tip-error">{{ errorMessage }}</a-typography-text>
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
const DESIGN_IMAGE_HEIGHT = 100
// 与登录页 size="large" 输入框高度一致
const FORM_INPUT_HEIGHT = 40

const props = withDefaults(defineProps<Props>(), {
  width: DESIGN_WIDTH,
  height: DESIGN_IMAGE_HEIGHT,
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

// 拖拽状态
const isDragging = ref(false)
const thumbLeft = ref(0)
const maxLeft = ref(0)
const dragStartX = ref(0)
const startTime = ref(0)
const mouseTrajectory = ref<Array<{ x: number; y: number; t: number }>>([])

// Refs
const containerRef = ref<HTMLDivElement>()
const imageRef = ref<HTMLElement>()
const trackRef = ref<HTMLElement>()

// 视口缩放：图片区按 400x100 等比缩放，轨道宽度随图片、轨道高度与表单输入框一致
const scale = ref(1)
const scaledStyle = computed(() => ({
  width: `${DESIGN_WIDTH * scale.value}px`,
  overflow: 'hidden',
  margin: '0 auto'
}))
const imageWrapStyle = computed(() => ({
  width: `${DESIGN_WIDTH * scale.value}px`,
  height: `${DESIGN_IMAGE_HEIGHT * scale.value}px`,
  overflow: 'hidden'
}))
const imageScaledStyle = computed(() => ({
  transform: `scale(${scale.value})`,
  transformOrigin: 'top center',
  width: `${DESIGN_WIDTH}px`,
  height: `${DESIGN_IMAGE_HEIGHT}px`
}))
const trackWrapStyle = computed(() => ({
  width: `${DESIGN_WIDTH * scale.value}px`,
  height: `${FORM_INPUT_HEIGHT}px`,
  overflow: 'hidden'
}))
const trackScaledStyle = computed(() => ({
  transform: `scaleX(${scale.value})`,
  transformOrigin: 'top center',
  width: `${DESIGN_WIDTH}px`,
  height: `${FORM_INPUT_HEIGHT}px`
}))

// 计算滑块图片在背景图片中的位置
const sliderLeft = computed(() => {
  if (!imageRef.value || maxLeft.value === 0) return 0
  const imageWidth = props.width
  const sliderWidth = 48
  return Math.round((thumbLeft.value / maxLeft.value) * (imageWidth - sliderWidth))
})

// 计算验证通过文本的位置（根据手柄位置动态调整，避免被遮挡）
const successTextPosition = computed(() => {
  if (!verified.value || maxLeft.value === 0) return 'center'
  const thumbWidth = 48
  const thumbRight = thumbLeft.value + thumbWidth
  const trackWidth = props.width
  const trackCenter = trackWidth / 2
  
  // 计算手柄中心位置
  const thumbCenter = thumbLeft.value + thumbWidth / 2
  
  // 如果手柄中心在轨道中心左侧，文本显示在右侧（避免被手柄遮挡）
  if (thumbCenter < trackCenter) {
    // 确保文本显示在手柄右侧，有足够空间
    return thumbRight < trackWidth - 80 ? 'right' : 'center'
  }
  // 如果手柄中心在轨道中心右侧，文本显示在左侧（避免被手柄遮挡）
  else {
    // 确保文本显示在手柄左侧，有足够空间
    return thumbLeft.value > 80 ? 'left' : 'center'
  }
})

// 生成验证码
const generate = async () => {
  try {
    loading.value = true
    errorMessage.value = ''
    verified.value = false
    // 不在此处清空 captchaData，避免刷新时整块被 loading 替换导致闪烁；仅在请求失败时清空以显示错误块
    resetDragState()

    logger.debug('[Captcha Slider] 开始生成验证码')
    const result = await generateCaptcha()
    logger.debug('[Captcha Slider] 验证码生成成功', { 
      captchaId: result.captchaId, 
      enabled: result.enabled, 
      hasBackgroundImage: !!result.backgroundImage, 
      hasSliderImage: !!result.sliderImage 
    })
    
    if (!result.enabled) {
      logger.info('[Captcha Slider] 验证码未启用（后端配置），直接通过验证')
      verified.value = true
      captchaData.value = { ...result, enabled: false } as CaptchaGenerateResult
      emit('success')
      emit('verified', true)
      return
    }

    captchaData.value = result
    
    // 初始化滑块轨道尺寸
    await nextTick()
    // 等待 DOM 完全渲染
    await new Promise(resolve => setTimeout(resolve, 50))
    if (trackRef.value) {
      const trackEl = trackRef.value as HTMLElement
      const thumbWidth = 48
      maxLeft.value = Math.max(0, trackEl.clientWidth - thumbWidth)
      logger.debug('[Captcha Slider] 滑块轨道尺寸初始化', {
        trackWidth: trackEl.clientWidth,
        thumbWidth: thumbWidth,
        maxLeft: maxLeft.value
      })
    } else {
      logger.warn('[Captcha Slider] 滑块轨道元素未找到，无法初始化 maxLeft')
      maxLeft.value = props.width - 48 // 使用默认值
    }
  } catch (error: any) {
    logger.error('[Captcha Slider] 生成验证码失败', error)
    errorMessage.value = error.message || t('components.common.captcha.generateFailed')
    emit('fail', errorMessage.value)
    captchaData.value = null
  } finally {
    loading.value = false
  }
}

// 重置拖拽状态
const resetDragState = () => {
  isDragging.value = false
  thumbLeft.value = 0
  dragStartX.value = 0
  startTime.value = 0
  mouseTrajectory.value = []
}

// 获取事件X坐标
const getEventX = (e: MouseEvent | TouchEvent): number => {
  if ('clientX' in e) return e.clientX
  if ('touches' in e && e.touches[0]) return e.touches[0].clientX
  return 0
}

// 获取事件Y坐标
const getEventY = (e: MouseEvent | TouchEvent): number => {
  if ('clientY' in e) return e.clientY
  if ('touches' in e && e.touches[0]) return e.touches[0].clientY
  return 0
}

// 拖拽开始
const handleDragStart = (e: MouseEvent | TouchEvent) => {
  if (verified.value || !captchaData.value) {
    logger.debug('[Captcha Slider] 拖拽被阻止', { verified: verified.value, hasCaptchaData: !!captchaData.value })
    return
  }

  if (maxLeft.value <= 0) {
    logger.warn('[Captcha Slider] maxLeft 未初始化，无法拖动', { maxLeft: maxLeft.value })
    return
  }

  e.preventDefault()
  e.stopPropagation()

  isDragging.value = true
  dragStartX.value = getEventX(e)
  const startThumbLeft = thumbLeft.value
  startTime.value = Date.now()
  mouseTrajectory.value = []

  // 记录起始点
  if (imageRef.value) {
    const rect = imageRef.value.getBoundingClientRect()
    const x = getEventX(e) - rect.left
    const y = getEventY(e) - rect.top
    mouseTrajectory.value.push({ x, y, t: 0 })
    logger.debug('[Captcha Slider] 开始拖拽', { startX: dragStartX.value, startThumbLeft, initialPoint: { x, y } })
  }

  const move = (ev: MouseEvent | TouchEvent) => {
    if (!isDragging.value) return
    
    ev.preventDefault()
    const moveX = getEventX(ev)
    let left = startThumbLeft + (moveX - dragStartX.value)
    left = Math.max(0, Math.min(left, maxLeft.value))
    
    requestAnimationFrame(() => {
      thumbLeft.value = left
      
      // 记录轨迹点
      if (imageRef.value) {
        const rect = imageRef.value.getBoundingClientRect()
        const relativeX = getEventX(ev) - rect.left
        const relativeY = getEventY(ev) - rect.top
        const elapsed = Date.now() - startTime.value
        mouseTrajectory.value.push({ x: relativeX, y: relativeY, t: elapsed })
      }
    })
  }

  const up = async () => {
    if (!isDragging.value) return
    
    isDragging.value = false
    document.removeEventListener('mousemove', move)
    document.removeEventListener('mouseup', up)
    document.removeEventListener('touchmove', move)
    document.removeEventListener('touchend', up)

    // 计算最终位置（百分比）
    // 注意：需要计算滑块图片在背景图片上的百分比位置，而不是滑块手柄在轨道上的百分比
    // 滑块图片的 left 位置 = (thumbLeft / maxLeft) * (imageWidth - sliderWidth)
    // 滑块图片的百分比位置 = (滑块图片的 left 位置 / imageWidth) * 100
    const sliderWidth = 48
    const imageWidth = props.width
    const sliderLeftPosition = maxLeft.value > 0 ? (thumbLeft.value / maxLeft.value) * (imageWidth - sliderWidth) : 0
    const position = Math.round((sliderLeftPosition / imageWidth) * 100)
    const timeSpent = (Date.now() - startTime.value) / 1000

    logger.debug('[Captcha Slider] 拖拽结束', { 
      thumbLeft: thumbLeft.value.toFixed(2),
      maxLeft: maxLeft.value,
      sliderLeftPosition: sliderLeftPosition.toFixed(2),
      finalPosition: position + '%',
      imageWidth: imageWidth,
      timeSpent: timeSpent.toFixed(2),
      trajectoryPoints: mouseTrajectory.value.length 
    })

    // 调用后端验证
    await verify(position, timeSpent)
  }

  document.addEventListener('mousemove', move)
  document.addEventListener('mouseup', up)
  document.addEventListener('touchmove', move, { passive: false })
  document.addEventListener('touchend', up)
}

// 验证验证码
const verify = async (position: number, timeSpent: number) => {
  if (!captchaData.value) return

  try {
    loading.value = true
    errorMessage.value = ''

    const userInput = {
      position,
      timeSpent,
      mouseTrajectory: mouseTrajectory.value.map(p => ({ x: p.x, y: p.y }))
    }

    const request: CaptchaVerifyRequest = {
      captchaId: captchaData.value.captchaId,
      userInput
    }

    logger.debug('[Captcha Slider] 开始验证验证码', { 
      captchaId: request.captchaId,
      position,
      timeSpent: timeSpent.toFixed(2),
      trajectoryLength: userInput.mouseTrajectory?.length || 0
    })

    const result = await verifyCaptcha(request)

    if (result.success) {
      logger.info('[Captcha Slider] 验证码验证成功', { captchaId: request.captchaId, position, timeSpent: timeSpent.toFixed(2) })
      verified.value = true
      // 验证成功后，保持滑块在当前位置（缺口位置），不移动到最右边
      // thumbLeft.value 已经保持在验证成功时的位置，无需修改
      emit('success')
      emit('verified', true)
    } else {
      logger.warn('[Captcha Slider] 验证码验证失败', { captchaId: request.captchaId, position, message: result.message })
      errorMessage.value = result.message || t('components.common.captcha.failed')
      emit('fail', errorMessage.value)
      emit('verified', false)
      // 验证失败后重置
      await resetSlider()
      setTimeout(() => {
        generate()
      }, 1500)
    }
  } catch (error: any) {
    logger.error('[Captcha Slider] 验证码验证异常', error)
    errorMessage.value = error.message || t('components.common.captcha.failed')
    emit('fail', errorMessage.value)
    emit('verified', false)
    await resetSlider()
    setTimeout(() => {
      generate()
    }, 1500)
  } finally {
    loading.value = false
  }
}

// 重置滑块
const resetSlider = async () => {
  const startLeft = thumbLeft.value
  const startTime = Date.now()
  const duration = 300

  const animate = () => {
    const elapsed = Date.now() - startTime
    const progress = Math.min(elapsed / duration, 1)
    const easeOut = 1 - Math.pow(1 - progress, 3)
    thumbLeft.value = startLeft * (1 - easeOut)

    if (progress < 1) {
      requestAnimationFrame(animate)
    }
  }

  animate()
}

// 刷新验证码（验证成功后不允许刷新）
const refresh = async () => {
  // 验证成功后不允许刷新
  if (verified.value) {
    logger.debug('[Captcha Slider] 验证成功后不允许刷新')
    return
  }
  resetDragState()
  await generate()
}

// 重置验证码
const reset = () => {
  logger.debug('[Captcha Slider] 重置验证码')
  verified.value = false
  errorMessage.value = ''
  resetDragState()
  if (props.autoGenerate) {
    generate()
  }
}

// 根据容器宽度缩放（参照登录页表单输入框宽度）
function updateScale() {
  const el = containerRef.value
  if (!el) return
  const w = el.offsetWidth || el.clientWidth
  if (w > 0) {
    scale.value = Math.min(1, w / DESIGN_WIDTH)
  }
}

let resizeObserver: ResizeObserver | null = null

// 30 秒自动刷新定时器（仅未验证时刷新）
const REFRESH_INTERVAL_MS = 30 * 1000
let refreshTimer: ReturnType<typeof setInterval> | null = null

// 使用外部传入的预填数据（不请求接口），并初始化轨道 maxLeft
async function applyInitialData() {
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
  await nextTick()
  await new Promise(resolve => setTimeout(resolve, 50))
  if (trackRef.value) {
    const trackEl = trackRef.value as HTMLElement
    maxLeft.value = Math.max(0, trackEl.clientWidth - 48)
  } else {
    maxLeft.value = props.width - 48
  }
  return true
}

// 组件挂载时：有 initialData 则用预填，否则按 autoGenerate 生成并启动 30 秒自动刷新
onMounted(async () => {
  updateScale()
  resizeObserver = new ResizeObserver(() => updateScale())
  if (containerRef.value) resizeObserver.observe(containerRef.value)
  if (await applyInitialData()) {
    return
  }
  if (props.autoGenerate) {
    generate()
  }
  refreshTimer = setInterval(() => {
    if (!verified.value && captchaData.value && captchaData.value.enabled !== false) {
      generate()
    }
  }, REFRESH_INTERVAL_MS)
})

// 组件卸载时清理定时器与事件监听
onUnmounted(() => {
  if (resizeObserver && containerRef.value) {
    resizeObserver.unobserve(containerRef.value)
    resizeObserver = null
  }
  if (refreshTimer) {
    clearInterval(refreshTimer)
    refreshTimer = null
  }
  document.removeEventListener('mousemove', () => {})
  document.removeEventListener('mouseup', () => {})
  document.removeEventListener('touchmove', () => {})
  document.removeEventListener('touchend', () => {})
})

// 暴露方法
defineExpose({
  generate,
  reset,
  refresh,
  verified: computed(() => verified.value)
})
</script>

<style scoped lang="less">
.takt-captcha-slider-wrapper {
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: flex-start;

  .takt-captcha-slider-scaled {
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  .takt-captcha-slider-inner {
    flex-shrink: 0;
  }

  .takt-captcha-slider-image-wrap {
    flex-shrink: 0;
  }

  .takt-captcha-slider-image-scaled {
    flex-shrink: 0;
  }

  .takt-captcha-slider-track-wrap {
    flex-shrink: 0;
  }

  .takt-captcha-slider-track-scaled {
    flex-shrink: 0;
  }

  .captcha-loading {
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 100px;
    padding: 20px;
  }

  .takt-captcha-slider-container {
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

    .captcha-image {
      position: relative;
      border: 1px solid rgba(0, 0, 0, 0.06);
      border-radius: 4px;
      overflow: hidden;
      text-align: center;

      img {
        display: inline-block;
        vertical-align: middle;
        border-radius: 4px;
      }

      .bg-image {
        width: 100%;
        height: 100%;
        object-fit: none;
        transition: opacity 0.2s ease;
        
        &.clickable {
          cursor: pointer;
          
          &:hover {
            opacity: 0.8;
          }
        }
        
        &:not(.clickable) {
          cursor: default;
        }
      }

      .slider-image {
        position: absolute;
        top: 50%;
        left: 0;
        width: 48px;
        height: 48px;
        transform: translateY(-50%);
        transition: left 0.05s linear;
        will-change: left;
        pointer-events: none;
        z-index: 10;
        border-radius: 4px;
      }
    }

    .slider-track {
      position: relative;
      height: 40px;
      background: #f5f5f5;
      border: 1px solid rgba(0, 0, 0, 0.06);
      border-radius: 4px;
      margin: 0; /* 无上下 margin，与 track-wrap 40px 一致，避免底部被 overflow 裁切 */
    }

    .slider-bg {
      position: absolute;
      top: 0;
      left: 0;
      right: 0;
      bottom: 0;
      background: linear-gradient(90deg, #f5f5f5 0%, #fafafa 100%);
      border-radius: 4px;
      pointer-events: none;
      z-index: 0;
    }

    .slider-thumb {
      position: absolute;
      top: -1px;
      width: 48px;
      height: 40px;
      background: var(--takt-captcha-primary, #1890ff);
      border: 1px solid var(--takt-captcha-primary, #1890ff);
      border-radius: 4px;
      box-sizing: border-box;
      text-align: center;
      cursor: e-resize;
      z-index: 10;
      display: flex;
      align-items: center;
      justify-content: center;
      
      &:hover {
        opacity: 0.9;
      }
      
      &.dragging {
        cursor: no-drop;
        transition: none;
      }
      
      &.success {
        cursor: default;
      }
      
      .thumb-icon {
        color: #fff;
        font-size: 18px;
        pointer-events: none;
        cursor: inherit;
        transition: color 0.3s ease;
      }
    }

    .progress-bar {
      position: absolute;
      top: 0;
      left: 0;
      height: 100%;
      background: var(--takt-captcha-primary, #1890ff);
      border-radius: 4px 0 0 4px;
      transition: width 0.05s cubic-bezier(0.4, 0, 0.2, 1);
      z-index: 1;
      will-change: width;
    }

    .slider-tip-text {
      position: absolute;
      inset: 0;
      display: flex;
      align-items: center;
      justify-content: center;
      z-index: 2;
      pointer-events: none;
      user-select: none;
      font-size: 12px;
      
      &.text-left {
        justify-content: flex-start;
        padding-left: 12px;
      }
      
      &.text-right {
        justify-content: flex-end;
        padding-right: 12px;
      }
      
      &.text-center {
        justify-content: center;
      }

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
        opacity: 0.95;
      }
    }

    .captcha-tip {
      text-align: center;
      font-size: 14px;
      min-height: 20px;

      .tip-error {
        :deep(.ant-typography) {
          color: #ff4d4f;
          margin: 0;
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
  .takt-captcha-slider-container {
    .captcha-overlay {
      background: rgba(0, 0, 0, 0.45);
    }

    .captcha-image {
      border-color: rgba(255, 255, 255, 0.1);
    }

    .slider-track {
      background: #262626;
      border-color: rgba(255, 255, 255, 0.1);
    }

    .slider-bg {
      background: linear-gradient(90deg, #262626 0%, #1f1f1f 100%);
    }

    .slider-thumb {
      background: var(--takt-captcha-primary, #1890ff);
      border-color: var(--takt-captcha-primary, #1890ff);

      .thumb-icon {
        color: #fff;
      }

      &:hover {
        opacity: 0.9;
      }

      &.success {
        cursor: default;
      }
    }

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
        opacity: 0.95;
      }
    }

    .captcha-tip {
      .tip-error {
        :deep(.ant-typography) {
          color: #ff7875;
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
