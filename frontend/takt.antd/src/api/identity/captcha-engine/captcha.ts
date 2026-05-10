// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity/captcha-engine
// 文件名称：captcha.ts
// 功能描述：验证码API，对应后端 TaktCaptchasController
// 路由前缀：api/TaktCaptchas
// ========================================

import request from '@/api/request'

/** 验证码验证结果 */
export interface CaptchaVerifyResult {
  success: boolean
  score?: number
  message: string
}

/** 验证码生成结果 */
export interface CaptchaGenerateResult {
  captchaId: string
  type: string
  backgroundImage: string
  sliderImage?: string
  targetPosition?: number
  enabled: boolean
}

/** 滑块验证码输入 */
export interface SliderCaptchaInput {
  targetPosition: number
  track: string
}

/** 行为验证码输入 */
export interface BehaviorCaptchaInput {
 轨迹数据?: unknown[]
}

/** 验证码验证请求 */
export interface CaptchaVerifyRequest {
  captchaId: string
  userInput?: SliderCaptchaInput | BehaviorCaptchaInput | Record<string, unknown>
}

// ========================================
// 验证码API
// ========================================
const captchaUrl = 'api/TaktCaptchas'

/**
 * 生成验证码
 * 对应后端：GenerateAsync
 */
export function generateCaptcha(): Promise<{
  success: boolean
  data: CaptchaGenerateResult
}> {
  return request({
    url: `${captchaUrl}/generate`,
    method: 'post'
  })
}

/**
 * 验证验证码
 * 对应后端：VerifyAsync
 */
export function verifyCaptcha(data: CaptchaVerifyRequest): Promise<{
  success: boolean
  data: CaptchaVerifyResult
}> {
  return request({
    url: `${captchaUrl}/verify`,
    method: 'post',
    data
  })
}