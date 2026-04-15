/**
 * 加班实体 · 中文（列表占位、字段与状态枚举；可与后端 entity.overtime 合并覆盖）
 * - overtimestatus：列表/高级查询中「状态」列标题（与请假页 entity.leave.leavestatus 用法一致）
 * - overtimestatusEnum：状态枚举文案，键 0–3 对应 OvertimeStatus
 */
export default {
  _self: '加班',
  keyword: '原因、备注等',
  employeeid: '员工ID',
  datefrom: '日期起',
  dateto: '日期止',
  overtimedate: '加班日期',
  plannedhours: '计划小时',
  actualhours: '实际小时',
  reason: '原因',
  overtimestatus: '状态',
  overtimestatusEnum: {
    '0': '草稿',
    '1': '已提交',
    '2': '已通过',
    '3': '已驳回'
  }
}
