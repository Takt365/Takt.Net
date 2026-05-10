// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave/specific-engine/attendance-leave
// 文件名称：attendance-leave.ts
// 创建时间：2026-05-06
// 创建人：Takt365
// 功能描述：考勤请假专用 API（假日主题色查询等）
// ========================================

import request from '@/api/request'
import type { Holiday } from '@/types/human-resource/attendance-leave/holiday'
import type { HolidayThemeQuery } from '@/types/human-resource/attendance-leave/specific-engine/attendance-leave'

const attendanceLeaveUrl = '/api/TaktAttendanceLeaves'

/**
 * 获取假日主题色（根据日期和区域获取假日信息）
 * 对应后端：TaktAttendanceLeavesController.GetHolidayThemeAsync
 * @param params 查询参数（date: 日期，region: 区域代码）
 * @returns 假日DTO对象，包含主题色信息
 */
export function getHolidayTheme(params: HolidayThemeQuery = {}): Promise<Holiday> {
  return request({
    url: `${attendanceLeaveUrl}/holiday-theme`,
    method: 'get',
    params
  })
}