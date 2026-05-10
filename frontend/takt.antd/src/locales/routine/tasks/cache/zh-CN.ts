/**
 * 缓存管理页静态补全（vue-i18n）；与后端 TaktTranslation 无对应 entity 时放此处。
 */
export default {
  page: {
    title: '缓存管理',
  },
  descriptions: {
    configTitle: '缓存配置',
    statsTitle: '缓存统计',
  },
  labels: {
    provider: '提供者',
    defaultExpirationMinutes: '默认过期时间（分钟）',
    slidingExpiration: '滑动过期',
    multiLevelCache: '多级缓存',
    redisInstancePrefix: 'Redis 实例前缀',
    note: '说明',
    currentEntryCount: '当前缓存项数量',
    totalHits: '命中次数',
    totalMisses: '未命中次数',
    hitRate: '命中率',
    estimatedSizeBytes: '当前估算大小（字节）',
    loadingHint: '加载中…',
    cacheKey: '缓存键',
    keyOps: '按键操作',
  },
  placeholders: {
    cacheKey: '请输入缓存键',
  },
  rules: {
    cacheKeyRequired: '请输入缓存键',
  },
  buttons: {
    checkExists: '检查是否存在',
    remove: '移除',
  },
  alerts: {
    keyExists: '该键存在',
    keyNotExists: '该键不存在',
  },
  messages: {
    loadInfoFail: '获取缓存信息失败',
    checkFail: '检查失败',
    removeSuccess: '已移除',
    removeFail: '移除失败',
  },
}
