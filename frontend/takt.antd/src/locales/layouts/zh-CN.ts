/**
 * 布局与路由 · 中文
 * 用于 layouts、router、会话过期等文案
 */
export default {
  page: {
      route: {
        logintitle: '登录',
        loadfail: '路由模块加载失败，请检查开发服务器是否正常运行，或刷新页面重试。'
      },
      session: {
        autologout: '由于长时间未操作，系统已自动登出',
        canceltext: '立即登出',
        content: '您已长时间未操作，系统将在 {minutes} 分钟后自动登出。请点击"继续使用"保持登录状态。',
        oktext: '继续使用',
        title: '会话即将过期'
      }
  }
}
