// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/identity/captcha
// 文件名称：captcha.ts
// 创建时间：2025-01-27
// 创建人：Takt365(Cursor AI)
// 功能描述：验证码 API 接口，提供验证码生成和验证功能
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '@/api/request'

const captchaUrl = '/api/TaktCaptcha'

/**
 * 验证码生成结果
 */
export interface CaptchaGenerateResult {
  /** 验证码ID（用于后续验证） */
  captchaId: string
  /** 验证码类型：Slider（滑块验证码）或 Behavior（行为验证码） */
  type?: 'Slider' | 'Behavior'
  /** 背景图片（Base64编码） */
  backgroundImage: string
  /** 滑块图片（Base64编码，仅滑块验证码） */
  sliderImage?: string
  /** 目标位置（百分比，仅滑块验证码） */
  targetPosition?: number
  /** 是否启用验证码 */
  enabled?: boolean
}

/**
 * 验证码验证请求
 */
export interface CaptchaVerifyRequest {
  /** 验证码ID */
  captchaId: string
  /** 用户输入的位置（滑块验证码）或行为数据（行为验证码） */
  userInput: number | {
    position: number
    timeSpent: number
    mouseTrajectory?: Array<{ x: number; y: number; t?: number }>
  }
}

/**
 * 验证码验证结果
 */
export interface CaptchaVerifyResult {
  /** 是否验证通过 */
  success: boolean
  /** 验证消息 */
  message: string
  /** 验证分数（行为验证码） */
  score?: number
}

/**
 * 生成验证码
 * 对应后端：POST /api/TaktCaptcha/generate
 */
export function generateCaptcha(): Promise<CaptchaGenerateResult> {
  return request({
    url: `${captchaUrl}/generate`,
    method: 'post'
  }) as unknown as Promise<CaptchaGenerateResult>
}

/**
 * 验证验证码
 * 对应后端：POST /api/TaktCaptcha/verify
 */
export function verifyCaptcha(params: CaptchaVerifyRequest): Promise<CaptchaVerifyResult> {
  return request({
    url: `${captchaUrl}/verify`,
    method: 'post',
    data: params
  }) as unknown as Promise<CaptchaVerifyResult>
}
