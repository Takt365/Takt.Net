/**
 * Cache admin page static strings (vue-i18n fallback).
 */
export default {
  page: {
    title: 'Cache',
  },
  descriptions: {
    configTitle: 'Cache configuration',
    statsTitle: 'Cache statistics',
  },
  labels: {
    provider: 'Provider',
    defaultExpirationMinutes: 'Default expiration (minutes)',
    slidingExpiration: 'Sliding expiration',
    multiLevelCache: 'Multi-level cache',
    redisInstancePrefix: 'Redis instance prefix',
    note: 'Note',
    currentEntryCount: 'Current entries',
    totalHits: 'Total hits',
    totalMisses: 'Total misses',
    hitRate: 'Hit rate',
    estimatedSizeBytes: 'Estimated size (bytes)',
    loadingHint: 'Loading…',
    cacheKey: 'Cache key',
    keyOps: 'Key operations',
  },
  placeholders: {
    cacheKey: 'Enter cache key',
  },
  rules: {
    cacheKeyRequired: 'Cache key is required',
  },
  buttons: {
    checkExists: 'Check exists',
    remove: 'Remove',
  },
  alerts: {
    keyExists: 'Key exists',
    keyNotExists: 'Key does not exist',
  },
  messages: {
    loadInfoFail: 'Failed to load cache info',
    checkFail: 'Check failed',
    removeSuccess: 'Removed',
    removeFail: 'Remove failed',
  },
}
