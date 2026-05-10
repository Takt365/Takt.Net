// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/numbering/rule-engine
// 文件名称：rule.ts
// 功能描述：编码规则生成引擎 API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.Numbering.RuleEngine.TaktRules
// ========================================

import request from '@/api/request'

// ========================================
// 编码规则生成引擎 API（按后端控制器顺序）
// ========================================
const ruleUrl = 'api/TaktRules'

/**
 * 根据规则生成下一个编码
 * 对应后端：GenerateNextAsync
 * @param ruleCode 规则编码（如 PO、ORDER、INVOICE）
 * @param companyCode 公司编码（可选）
 * @param deptCode 部门编码（可选）
 * @param date 生成日期（可选）
 */
export function generateNextCode(
  ruleCode: string,
  companyCode?: string,
  deptCode?: string,
  date?: string
): Promise<string> {
  return request({
    url: `${ruleUrl}/generate`,
    method: 'get',
    params: { ruleCode, companyCode, deptCode, date }
  })
}