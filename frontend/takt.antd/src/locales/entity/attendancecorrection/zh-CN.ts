/**
 * 补卡实体 · 中文（与后端 entity.attendancecorrection 可合并）
 */
export default {
  _self: '补卡',
  keyword: '原因、备注',
  employeeid: '员工ID',
  targetdatefrom: '归属日期起',
  targetdateto: '归属日期止',
  targetdate: '归属日期',
  correctionkind: '补卡类型',
  requestpunchtime: '申请打卡时间',
  reason: '原因',
  approvalstatus: {
    label: '审批状态',
    '0': '草稿',
    '1': '待审',
    '2': '已通过',
    '3': '已驳回'
  }
}
