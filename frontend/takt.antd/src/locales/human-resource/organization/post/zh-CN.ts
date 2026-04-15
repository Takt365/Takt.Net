/**
 * 岗位模块 · 中文
 * - 页面标题用 entity.post + common.action.management 拼接
 * - required 用 common.form.placeholder.required/select + entity.post.xxx 拼接；此处仅保留 format 及无法拼接的
 */
export default {
  /** 本模块 format 校验文案（required 用 common+entity 拼接） */
  fields: {
    postName: {
      validation: {
        format: '岗位名称格式不正确，2-50个字符'
      }
    },
    postCode: {
      validation: {
        format: '岗位编码格式不正确，仅允许字母、数字、下划线，2-30位'
      }
    }
  },

  /** 分配用户（与 common.button.allocate + entity 区分时可保留） */
  assignUser: '分配用户',

  /** 无法用 common 拼接的消息 */
  msg: {
    loadUserFail: '加载岗位用户失败',
    postIdRequired: '岗位ID不存在',
    postRequired: '岗位信息不存在'
  }
}
