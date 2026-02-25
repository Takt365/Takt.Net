/**
 * vue-mess-detector 配置
 * 项目规则：takt-frontend.mdc
 * 规则级别：error | warning | off
 */
module.exports = {
  rules: {
    // if 语句必须加大括号
    ifWithoutCurlyBraces: 'error',
    // 多属性元素每行一个属性 - 暂关闭，后续按规范逐步整改
    multiAttributeElements: 'off',
    // 403/404 为通用 HTTP 状态码，保留文件名
    fullWordComponentName: 'off',
    // 嵌套三元 - 已修复主要位置，暂降级
    nestedTernary: 'warning',
    // 验证码弹窗等 modal 需全局样式定位，login 相关组件保留
    globalStyle: 'off'
  }
}
