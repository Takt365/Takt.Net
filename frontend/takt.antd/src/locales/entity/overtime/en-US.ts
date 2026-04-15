/**
 * Overtime entity · English (fallback when backend entity.overtime is absent)
 * - overtimestatus: column / drawer label for status (same role as entity.leave.leavestatus on leave page)
 * - overtimestatusEnum: labels for OvertimeStatus 0–3
 */
export default {
  _self: 'Overtime',
  keyword: 'reason, remark',
  employeeid: 'Employee ID',
  datefrom: 'Date from',
  dateto: 'Date to',
  overtimedate: 'Overtime date',
  plannedhours: 'Planned hours',
  actualhours: 'Actual hours',
  reason: 'Reason',
  overtimestatus: 'Status',
  overtimestatusEnum: {
    '0': 'Draft',
    '1': 'Submitted',
    '2': 'Approved',
    '3': 'Rejected'
  }
}
