/**
 * 状态与 Store · 中文
 * 用于 stores 中用户可见的提示、通知等
 */
export default {
  signalr: {
    connectFail: 'SignalR 连接失败',
    error: 'SignalR 发生错误',
    newMessage: '你有新的消息',
    onlineNotify: '上线通知',
    /** 上线欢迎内容，{name} 显示名，{time} 连接时间 */
    onlineWelcome: '欢迎 {name} 上线！连接成功，当前时间：{time}',
    sendFail: '发送消息失败',
    broadcastFail: '发送广播消息失败',
    broadcastLabel: '广播',
    /** 在别处请求登录：弹窗标题 */
    loginRequestElsewhereTitle: '登录提示',
    /** 在别处请求登录：弹窗内容，{location} 为请求登录位置 */
    loginRequestElsewhereContent: '该用户名在 {location} 请求登录，是否退出当前登录？',
    exitCurrentLogin: '退出当前登录',
    /** 点击退出当前登录后，退出或跳转失败时的提示 */
    exitCurrentLoginFail: '退出当前登录失败，请重试或手动刷新后重新登录'
  }
}
