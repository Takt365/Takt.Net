/**
 * 节拍数字工厂 · Takt Digital Factory (TDF)
 * @file config.ts
 * @description 流程设计器配置：默认 BPMN XML、常量、元素类型与 i18n key 映射。
 * 实现参考 vue-bpmn-flowable（自定义属性面板 + selection.changed / element.changed + modeling.updateProperties），
 * 但本项目后端为 Takt（非 Flowable），仅使用标准 BPMN 2.0，不引入 Flowable moddle。
 * 后端 TaktBpmnParser 实际使用的 BPMN 属性：节点 id/name；连线 sourceRef/targetRef/name、conditionExpression.body。
 *
 * --- 常见 Flowable/Activiti 风格「用户任务」属性面板与本项目对比（为何本设计器不包含下列字段）---
 * | 截图/参考面板字段     | Takt 是否从 BPMN 解析 | 说明 |
 * | 节点 id              | 是 → TaktFlowNode.Id | 本面板展示（只读） |
 * | 节点名称             | 是 → TaktFlowNode.Name | 本面板展示并可编辑 |
 * | 节点颜色             | 否 | 仅视觉，执行无关；不展示 |
 * | 执行监听器           | 否 | Flowable/Activiti 扩展，Takt 无此概念；不展示 |
 * | 任务监听器           | 否 | 同上；不展示 |
 * | 人员类型/候选人员    | 否 | 执行人由流程方案、ProcessJson、TaktFlowNodeSetInfo 等配置，不从 BPMN 解析；不展示 |
 * | 多实例               | 否 | Flowable 扩展；不展示 |
 * | 异步                 | 否 | Flowable 扩展；不展示 |
 * | 优先级               | 否 | Flowable 扩展；不展示 |
 * | 表单标识 key         | 否 | 表单由流程/业务配置，TaktBpmnParser 不读 BPMN 扩展；不展示 |
 * | 跳过表达式           | 否 | Flowable 扩展；不展示 |
 * | 到期时间             | 否 | Flowable 扩展；不展示 |
 * Copyright (c) 2025 Takt  All rights reserved.
 */

/**
 * 默认 BPMN 2.0 XML：仅一个开始事件（标准 BPMN 2.0）
 */
export const DEFAULT_BPMN_XML = `<?xml version="1.0" encoding="UTF-8"?>
<bpmn:definitions xmlns:bpmn="http://www.omg.org/spec/BPMN/20100524/MODEL" xmlns:bpmndi="http://www.omg.org/spec/BPMN/20100524/DI" xmlns:dc="http://www.omg.org/spec/DD/20100524/DC" id="Definitions_1" targetNamespace="http://bpmn.io/schema/bpmn">
  <bpmn:process id="Process_1" isExecutable="false">
    <bpmn:startEvent id="StartEvent_1" name="开始"/>
  </bpmn:process>
  <bpmndi:BPMNDiagram id="BPMNDiagram_1">
    <bpmndi:BPMNPlane id="BPMNPlane_1" bpmnElement="Process_1">
      <bpmndi:BPMNShape id="_BPMNShape_StartEvent_2" bpmnElement="StartEvent_1">
        <dc:Bounds x="152" y="102" width="36" height="36"/>
      </bpmndi:BPMNShape>
    </bpmndi:BPMNPlane>
  </bpmndi:BPMNDiagram>
</bpmn:definitions>`

/** 设计器默认最小高度（px） */
export const DEFAULT_MIN_HEIGHT = 400

/** 属性面板固定宽度（px），参考 xtj116/bpmn-designer */
export const PROPERTIES_PANEL_WIDTH = 380

/**
 * BPMN 元素 $type 与 i18n key 的映射（workflow.scheme 下）
 * 用于属性面板展示、与默认文字标签（每个元素都需要）。覆盖调色板可创建的全部类型及替换菜单常见类型。
 */
export const ELEMENT_TYPE_I18N_KEYS: Record<string, string> = {
  'bpmn:Process': 'workflow.scheme.typeProcess',
  'bpmn:StartEvent': 'workflow.scheme.typeStartEvent',
  'bpmn:IntermediateThrowEvent': 'workflow.scheme.typeIntermediateThrowEvent',
  'bpmn:EndEvent': 'workflow.scheme.typeEndEvent',
  'bpmn:ExclusiveGateway': 'workflow.scheme.typeExclusiveGateway',
  'bpmn:ParallelGateway': 'workflow.scheme.typeParallelGateway',
  'bpmn:InclusiveGateway': 'workflow.scheme.typeInclusiveGateway',
  'bpmn:Task': 'workflow.scheme.typeTask',
  'bpmn:UserTask': 'workflow.scheme.typeUserTask',
  'bpmn:SubProcess': 'workflow.scheme.typeSubProcess',
  'bpmn:SequenceFlow': 'workflow.scheme.typeSequenceFlow',
  'bpmn:DataObjectReference': 'workflow.scheme.typeDataObjectReference',
  'bpmn:DataStoreReference': 'workflow.scheme.typeDataStoreReference',
  'bpmn:Group': 'workflow.scheme.typeGroup',
  'bpmn:Participant': 'workflow.scheme.typeParticipant'
}
