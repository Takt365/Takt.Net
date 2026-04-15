/**
 * 考勤日结结果实体 · 中文（列表占位、列标题、高级查询、出勤状态枚举；可与后端 entity.attendanceresult 合并覆盖）
 * 出勤状态码与后端 TaktAttendanceResultDto 注释一致：0=正常 1=迟到 2=早退 3=缺卡 4=旷工 5=加班
 */
export default {
  _self: '考勤结果',
  keyword: '记录ID、员工ID、考勤日',
  employeeid: '员工ID',
  attendancestatus: '出勤状态',
  /** 键为状态码字符串，与 `t(\`entity.attendanceresult.attendancestatusEnum.${n}\`)` 一致 */
  attendancestatusEnum: {
    '0': '正常',
    '1': '迟到',
    '2': '早退',
    '3': '缺卡',
    '4': '旷工',
    '5': '加班'
  },
  datefrom: '考勤日起',
  dateto: '考勤日止',
  attendancedate: '考勤日',
  shiftscheduleid: '排班ID',
  firstintime: '首次上班',
  lastouttime: '末次下班',
  workminutes: '计出勤分钟',
  calculatedat: '计算完成时间'
}
