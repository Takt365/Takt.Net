// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：utils
// 文件名称：password.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt密码工具类，使用PBKDF2算法进行密码哈希，与后端TaktEncryptHelper保持一致
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

/**
 * 使用说明：
 * 
 * 1. 登录场景：
 *    - 前端发送原始密码到后端，后端使用 TaktEncryptHelper.VerifyPassword 验证
 *    - 不需要使用此工具类
 * 
 * 2. 注册/修改密码场景：
 *    - 通常由后端处理密码哈希（推荐）
 *    - 如果需要在客户端预哈希，可以使用 hashPassword 方法
 * 
 * 3. 密码验证场景：
 *    - 如果需要在客户端验证密码（如确认密码），可以使用 verifyPassword 方法
 * 
 * 注意：
 * - 此工具类使用 Web Crypto API 的 PBKDF2 实现
 * - 与后端 BouncyCastle 的 PBKDF2 实现参数保持一致（盐值32字节，迭代60000次，密钥32字节）
 * - 使用 SHA-256 作为哈希函数
 */

/**
 * Takt密码工具类
 * 使用PBKDF2算法进行密码哈希，与后端TaktEncryptHelper保持一致
 */
export class TaktPasswordHelper {
  private static readonly SALT_SIZE = 32 // 盐值长度（字节），与后端保持一致
  private static readonly ITERATIONS = 60000 // PBKDF2 迭代次数，与后端保持一致
  private static readonly KEY_LENGTH = 32 // 密钥长度（字节），与后端保持一致

  /**
   * 将字符串转换为UTF-8字节数组
   */
  private static stringToBytes(str: string): Uint8Array {
    return new TextEncoder().encode(str)
  }

  /**
   * 将字节数组转换为Base64字符串
   */
  private static bytesToBase64(bytes: Uint8Array): string {
    const binary = String.fromCharCode(...bytes)
    return btoa(binary)
  }

  /**
   * 将Base64字符串转换为字节数组
   */
  private static base64ToBytes(base64: string): Uint8Array {
    const binary = atob(base64)
    const bytes = new Uint8Array(binary.length)
    for (let i = 0; i < binary.length; i++) {
      bytes[i] = binary.charCodeAt(i)
    }
    return bytes
  }

  /**
   * 使用PBKDF2哈希密码（防彩虹表攻击）
   * @param password 原始密码
   * @returns 哈希后的密码（格式：salt+hash，Base64编码）
   * 
   * @remarks
   * 防彩虹表攻击原理：
   * 1. 存储阶段：每个密码使用32字节随机盐值，确保每个密码都有唯一的盐值
   *    即使两个用户使用相同的密码（如"123456"），由于盐值不同，存储的哈希值也不同
   * 2. 验证阶段：接收原始密码，从存储的哈希值中提取盐值，使用相同的盐值重新计算哈希并比较
   * 3. 攻击者即使获得数据库中的哈希值，也无法使用预计算的彩虹表直接查找原始密码
   *    因为需要为每个不同的盐值重新生成彩虹表，这几乎不可能
   * 
   * 其他安全措施：
   * - 使用PBKDF2慢速哈希算法，增加计算成本
   * - 60000次迭代，大幅增加破解时间
   * - 每个密码使用不同的盐值，使彩虹表失效
   */
  static async hashPassword(password: string): Promise<string> {
    if (!password) {
      throw new Error('密码不能为空')
    }

    // 使用加密安全的随机数生成器生成随机盐值
    // 每个密码使用唯一的盐值，防止彩虹表攻击
    const saltArray = new Uint8Array(this.SALT_SIZE)
    crypto.getRandomValues(saltArray)
    const salt = saltArray.buffer

    // 将密码转换为字节数组
    const passwordBytes = this.stringToBytes(password)

    // 导入密码作为密钥材料
    const keyMaterial = await crypto.subtle.importKey(
      'raw',
      passwordBytes.buffer as ArrayBuffer,
      'PBKDF2',
      false,
      ['deriveBits']
    )

    // 使用PBKDF2生成密钥（慢速哈希，增加破解成本）
    const derivedBits = await crypto.subtle.deriveBits(
      {
        name: 'PBKDF2',
        salt: salt,
        iterations: this.ITERATIONS,
        hash: 'SHA-256' // 使用SHA-256作为哈希函数
      },
      keyMaterial,
      this.KEY_LENGTH * 8 // 密钥长度（位）
    )

    // 将盐值转换回Uint8Array用于组合
    const saltBytes = new Uint8Array(salt)

    // 将ArrayBuffer转换为Uint8Array
    const hash = new Uint8Array(derivedBits)

    // 组合盐值和哈希值：salt+hash
    // 存储格式确保验证时可以提取盐值
    const result = new Uint8Array(this.SALT_SIZE + this.KEY_LENGTH)
    result.set(saltBytes, 0)
    result.set(hash, this.SALT_SIZE)

    return this.bytesToBase64(result)
  }

  /**
   * 验证密码
   * @param password 原始密码（前端发送的明文密码）
   * @param hashedPassword 数据库中存储的哈希密码（格式：salt+hash）
   * @returns 是否匹配
   * 
   * @remarks
   * 验证流程：
   * 1. 从存储的哈希值中提取盐值（前32字节）
   * 2. 使用相同的盐值和参数（60000次迭代）重新计算哈希
   * 3. 比较新计算的哈希与存储的哈希值
   * 
   * 安全措施：
   * - 使用恒定时间比较防止时序攻击
   * - 防彩虹表攻击已在存储阶段完成（每个密码使用唯一盐值）
   */
  static async verifyPassword(password: string, hashedPassword: string): Promise<boolean> {
    if (!password || !hashedPassword) {
      return false
    }

    try {
      // 解码哈希值
      const hashBytes = this.base64ToBytes(hashedPassword)
      if (hashBytes.length !== this.SALT_SIZE + this.KEY_LENGTH) {
        return false
      }

      // 提取盐值和哈希值
      const saltBytes = hashBytes.slice(0, this.SALT_SIZE)
      const salt = saltBytes.buffer
      const storedHash = hashBytes.slice(this.SALT_SIZE, this.SALT_SIZE + this.KEY_LENGTH)

      // 将密码转换为字节数组
      const passwordBytes = this.stringToBytes(password)

      // 导入密码作为密钥材料
      const keyMaterial = await crypto.subtle.importKey(
        'raw',
        passwordBytes.buffer as ArrayBuffer,
        'PBKDF2',
        false,
        ['deriveBits']
      )

      // 使用相同的盐值和参数重新计算哈希
      const derivedBits = await crypto.subtle.deriveBits(
        {
          name: 'PBKDF2',
          salt: salt,
          iterations: this.ITERATIONS,
          hash: 'SHA-256'
        },
        keyMaterial,
        this.KEY_LENGTH * 8
      )

      // 将ArrayBuffer转换为Uint8Array
      const computedHash = new Uint8Array(derivedBits)

      // 使用恒定时间比较防止时序攻击
      return this.constantTimeEquals(computedHash, storedHash)
    } catch {
      return false
    }
  }

  /**
   * 恒定时间比较两个字节数组，防止时序攻击
   * @param a 第一个字节数组
   * @param b 第二个字节数组
   * @returns 是否相等
   */
  private static constantTimeEquals(a: Uint8Array, b: Uint8Array): boolean {
    if (!a || !b || a.length !== b.length) {
      return false
    }

    let result = 0
    for (let i = 0; i < a.length; i++) {
      result |= a[i] ^ b[i]
    }

    return result === 0
  }
}
