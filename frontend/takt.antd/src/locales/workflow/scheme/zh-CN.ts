/**
 * 流程方案 · 中文
 */
export default {
  page: {
      designerlabel: '流程结构（可视化构建）',
      designerlabeledit: '流程结构（编辑）',
      createbutton: '新增流程方案',
      modaltitle: '流程方案',
      jsonplaceholder: 'JSON 格式：nodes、edges 等',
      stepprocessjsonlabel: '流程结构（仅本项目工作流解析字段）',
      loaddetailfailed: '加载方案详情失败',
      /** ProcessContent 与后端校验一致：合法 JSON + nodes/edges；若有 flowTree 根须 nodeType=1 */
      invalidprocesscontent: '流程内容无效：须为合法 JSON，且包含 nodes、edges；若有 flowTree，根须为发起人（nodeType=1）。请回到「流程设计」步骤保存。',
      linkform: '关联表单',
      selectformplaceholder: '请选择关联表单（可选）',
      /** 步骤2 单选：关联表单 / 新建表单 */
      linkformoptionlink: '关联表单',
      linkformoptionnew: '新建表单',
      noformhint: '暂无流程表单，请到「流程表单」中新建。',
      step: {
        /** 步骤1：流程信息 */
        step1flowinfo: '流程信息',
        /** 步骤2：关联表单/表单设计 */
        step2selectform: '关联表单/表单设计',
        /** 步骤3：流程设计 */
        step3flowdesign: '流程设计',
        step1basicinfo: '方案基本信息',
        step4flowpreview: '流程预览',
        basicinfo: '基本信息',
        initialdata: '初始数据',
        processdesign: '流程设计',
        processdesignvisual: '流程结构（可视化构建）',
        processdesignjson: '流程结构（仅本项目工作流解析字段）',
        next: '下一步',
        prev: '上一步',
        done: '完成',
        completerequired: '请完成所有步骤并通过校验后再提交',
        validatefail: '请先完成第 {step} 步'
      }
  }
}
