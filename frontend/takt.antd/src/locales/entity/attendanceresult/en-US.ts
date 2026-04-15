/**
 * Attendance result entity · English (list placeholder, columns, advanced query, status enum; merge with backend entity.attendanceresult if present)
 * Status codes align with backend TaktAttendanceResultDto: 0=Normal 1=Late 2=Early leave 3=Missing punch 4=Absenteeism 5=Overtime
 */
export default {
  _self: 'Attendance Result',
  keyword: 'Record ID, employee ID, attendance date',
  employeeid: 'Employee ID',
  attendancestatus: 'Attendance status',
  attendancestatusEnum: {
    '0': 'Normal',
    '1': 'Late',
    '2': 'Early leave',
    '3': 'Missing punch',
    '4': 'Absenteeism',
    '5': 'Overtime'
  },
  datefrom: 'Attendance date from',
  dateto: 'Attendance date to',
  attendancedate: 'Attendance date',
  shiftscheduleid: 'Shift schedule ID',
  firstintime: 'First clock-in',
  lastouttime: 'Last clock-out',
  workminutes: 'Work minutes',
  calculatedat: 'Calculated at'
}
