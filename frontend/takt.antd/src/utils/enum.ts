// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/utils/enum
// 文件名称：enum.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：通用枚举定义（运行时值），对应后端 Takt.Shared.Enums
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

/**
 * Takt 结果代码枚举（对应后端 Takt.Shared.Enums.TaktResultCode）
 * 注意：enum 是运行时值，必须放在 .ts 文件中，不能放在 .d.ts 文件中
 */
export enum TaktResultCode {
  /** 成功 */
  Success = 200,
  /** 失败 */
  Failed = 400,
  /** 未授权 */
  Unauthorized = 401,
  /** 禁止访问 */
  Forbidden = 403,
  /** 未找到 */
  NotFound = 404,
  /** 服务器错误 */
  ServerError = 500,
  /** 业务错误 */
  BusinessError = 1000,
  /** 参数错误 */
  ParameterError = 1001,
  /** 系统错误 */
  SystemError = 1002
}
