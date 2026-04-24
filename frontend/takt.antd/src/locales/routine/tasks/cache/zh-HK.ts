/**
 * 緩存管理頁靜態補全 · 繁體中文
 */
export default {
  page: {
    title: '緩存管理',
  },
  descriptions: {
    configTitle: '緩存配置',
    statsTitle: '緩存統計',
  },
  labels: {
    provider: '提供者',
    defaultExpirationMinutes: '默認過期時間（分鐘）',
    slidingExpiration: '滑動過期',
    multiLevelCache: '多級緩存',
    redisInstancePrefix: 'Redis 實例前綴',
    note: '說明',
    currentEntryCount: '當前緩存項數量',
    totalHits: '命中次數',
    totalMisses: '未命中次數',
    hitRate: '命中率',
    estimatedSizeBytes: '當前估算大小（字節）',
    loadingHint: '加載中…',
    cacheKey: '緩存鍵',
    keyOps: '按鍵操作',
  },
  placeholders: {
    cacheKey: '請輸入緩存鍵',
  },
  rules: {
    cacheKeyRequired: '請輸入緩存鍵',
  },
  buttons: {
    checkExists: '檢查是否存在',
    remove: '移除',
  },
  alerts: {
    keyExists: '該鍵存在',
    keyNotExists: '該鍵不存在',
  },
  messages: {
    loadInfoFail: '獲取緩存信息失敗',
    checkFail: '檢查失敗',
    removeSuccess: '已移除',
    removeFail: '移除失敗',
  },
}
