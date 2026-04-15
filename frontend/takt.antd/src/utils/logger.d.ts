import { type ConsolaInstance } from 'consola';
/**
 * 脱敏处理对象（用于日志输出）
 * @param obj 要脱敏的对象
 * @returns 脱敏后的对象
 */
export declare function sanitizeForLogging(obj: any): any;
/**
 * API 日志工具类（扩展 Consola 功能）
 */
declare class ApiLogger {
    private logger;
    constructor(tag?: string);
    /**
     * API 请求日志
     */
    apiRequest(method: string, url: string, config?: any): void;
    /**
     * API 响应日志
     */
    apiResponse(status: number, method: string, url: string, data?: any, message?: string): void;
    /**
     * API 错误日志
     */
    apiError(status: number, method: string, url: string, error: any): void;
    /**
     * 网络错误日志
     */
    networkError(method: string, url: string, error: any): void;
    /**
     * 健康检查错误日志
     */
    healthCheckError(error: any): void;
}
/**
 * 统一日志导出接口（兼容原有代码）
 */
export declare const logger: {
    debug: any;
    info: any;
    warn: any;
    error: any;
    success: any;
    apiRequest: any;
    apiResponse: any;
    apiError: any;
    networkError: any;
    healthCheckError: any;
    withTag: (tag: string) => ConsolaInstance;
    consola: ConsolaInstance;
};
/**
 * 导出类型
 */
export type { ConsolaInstance };
/**
 * 导出 ApiLogger 类供扩展使用
 */
export { ApiLogger };
/**
 * Vite 插件：在开发服务器终端输出 API 请求和响应日志
 * 格式：[YYYY-MM-DD HH:mm:ss] [状态码 状态] 方法 URL (耗时ms)
 */
export declare function vitePluginLogger(): import('vite').Plugin;
