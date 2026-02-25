<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/components/business/takt-flow-designer -->
<!-- 文件名称：properties-panel.vue -->
<!-- 创建时间：2025-02-21 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程设计器右侧属性面板，按项目实际节点类型（TaktBpmnParser：Process/StartEvent/UserTask/EndEvent/ExclusiveGateway/SequenceFlow）展示属性与节点策略 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- ======================================== -->

<template>
  <aside class="takt-flow-design-properties-panel" :style="{ width: widthPx, minWidth: widthPx }">
    <div class="takt-flow-design-properties">
      <div class="takt-flow-design-properties-header">
        <div class="takt-flow-design-properties-title">{{ t('workflow.scheme.elementProperties') || '流程执行属性' }}</div>
        <div v-if="hasSelection" class="takt-flow-design-properties-context">· {{ selectedElementType }} · {{ selectedId }}</div>
      </div>
      <p class="takt-flow-design-properties-notice">仅当前后端（Takt）执行用到的属性，不含 Flowable/Activiti 的监听器、候选人员、表单 key 等扩展。</p>
      <div v-if="hasSelection" class="takt-flow-design-properties-body">
        <!-- 统一一个 a-tabs，两个 a-tab-pane；pane 内按流程/节点/连线区分内容 -->
        <a-tabs v-model:activeKey="propertyTab" size="small" class="takt-flow-design-property-tabs">
          <a-tab-pane key="basic" tab="基本信息">
            <!-- 流程（bpmn:Process） -->
            <template v-if="elementType === 'bpmn:Process'">
              <section class="takt-flow-design-property-group">
                <div class="takt-flow-design-property-group-title">流程</div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">流程 ID</label>
                  <input :value="selectedId" class="takt-flow-design-property-input readonly" readonly disabled />
                  <span class="takt-flow-design-property-meta">与流程方案 ProcessKey 一致，TaktFlowRuntime 使用</span>
                </div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">流程名称</label>
                  <input :value="selectedName" class="takt-flow-design-property-input" :placeholder="t('workflow.scheme.elementNamePlaceholder')" @input="onNameInput" />
                </div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-checkbox-label">
                    <input type="checkbox" :checked="selectedExecutable" @change="onExecutableChange" />
                    Executable
                  </label>
                  <span class="takt-flow-design-property-meta">BPMN 2.0 标准</span>
                </div>
              </section>
            </template>
            <!-- 开始节点（bpmn:StartEvent → TaktFlowNodeType.Start） -->
            <template v-else-if="elementType === 'bpmn:StartEvent'">
              <section class="takt-flow-design-property-group">
                <div class="takt-flow-design-property-group-title">开始节点</div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">编号</label>
                  <input :value="selectedId" class="takt-flow-design-property-input readonly" readonly disabled />
                  <span class="takt-flow-design-property-meta">TaktFlowNode.Id，连线 from/to 引用</span>
                </div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">名称</label>
                  <input :value="selectedName" class="takt-flow-design-property-input" placeholder="如：开始" @input="onNameInput" />
                </div>
              </section>
            </template>
            <!-- 审批节点（bpmn:UserTask → TaktFlowNodeType.Approval） -->
            <template v-else-if="elementType === 'bpmn:UserTask'">
              <section class="takt-flow-design-property-group">
                <div class="takt-flow-design-property-group-title">审批节点</div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">编号</label>
                  <input :value="selectedId" class="takt-flow-design-property-input readonly" readonly disabled />
                  <span class="takt-flow-design-property-meta">TaktFlowNode.Id</span>
                </div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">名称</label>
                  <input :value="selectedName" class="takt-flow-design-property-input" placeholder="如：部门审批" @input="onNameInput" />
                  <span class="takt-flow-design-property-meta">TaktFlowNode.Name，执行时展示</span>
                </div>
              </section>
            </template>
            <!-- 结束/驳回节点（bpmn:EndEvent → TaktFlowNodeType.End/Rejected） -->
            <template v-else-if="elementType === 'bpmn:EndEvent'">
              <section class="takt-flow-design-property-group">
                <div class="takt-flow-design-property-group-title">结束节点</div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">编号</label>
                  <input :value="selectedId" class="takt-flow-design-property-input readonly" readonly disabled />
                  <span class="takt-flow-design-property-meta">名称含“驳回”/reject 时解析为 Rejected，否则 End</span>
                </div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">名称</label>
                  <input :value="selectedName" class="takt-flow-design-property-input" placeholder="如：结束、驳回" @input="onNameInput" />
                </div>
              </section>
            </template>
            <!-- 网关（bpmn:ExclusiveGateway → TaktFlowNodeType.Other） -->
            <template v-else-if="elementType === 'bpmn:ExclusiveGateway'">
              <section class="takt-flow-design-property-group">
                <div class="takt-flow-design-property-group-title">排他网关</div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">编号</label>
                  <input :value="selectedId" class="takt-flow-design-property-input readonly" readonly disabled />
                </div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">名称</label>
                  <input :value="selectedName" class="takt-flow-design-property-input" placeholder="如：网关" @input="onNameInput" />
                </div>
              </section>
            </template>
            <!-- 其他节点类型（Task/ParallelGateway/InclusiveGateway/SubProcess 等，BPMN 有但解析器未单独处理时统一按节点） -->
            <template v-else-if="isNodeType">
              <section class="takt-flow-design-property-group">
                <div class="takt-flow-design-property-group-title">{{ selectedElementType }}</div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">编号</label>
                  <input :value="selectedId" class="takt-flow-design-property-input readonly" readonly disabled />
                </div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">名称</label>
                  <input :value="selectedName" class="takt-flow-design-property-input" placeholder="节点名称" @input="onNameInput" />
                </div>
              </section>
            </template>
            <!-- 连线（bpmn:SequenceFlow → TaktFlowLine） -->
            <template v-else-if="elementType === 'bpmn:SequenceFlow'">
              <section class="takt-flow-design-property-group">
                <div class="takt-flow-design-property-group-title">连线</div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">连线 ID</label>
                  <input :value="selectedId" class="takt-flow-design-property-input readonly" readonly disabled />
                </div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">连线名称</label>
                  <input :value="selectedName" class="takt-flow-design-property-input" :placeholder="t('workflow.scheme.elementNamePlaceholder')" @input="onNameInput" />
                  <span class="takt-flow-design-property-meta">TaktFlowLine.Name，如：通过、驳回、提交</span>
                </div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">{{ t('workflow.scheme.conditionExpression') }}</label>
                  <input :value="selectedConditionExpression" class="takt-flow-design-property-input" :placeholder="t('workflow.scheme.conditionExpressionPlaceholder')" @input="onConditionExpressionInput" />
                  <span class="takt-flow-design-property-meta">TaktFlowLine.Condition：Pass / Reject / Default</span>
                </div>
                <p class="takt-flow-design-property-hint-inline">{{ t('workflow.scheme.flowNameHint') }}</p>
              </section>
            </template>
          </a-tab-pane>
          <a-tab-pane key="strategy" tab="节点策略">
            <template v-if="elementType === 'bpmn:Process'">
              <div class="takt-flow-design-property-placeholder">流程级配置在流程方案中设置。</div>
            </template>
            <template v-else-if="elementType === 'bpmn:StartEvent'">
              <div class="takt-flow-design-property-placeholder">开始节点无审批策略。</div>
            </template>
            <template v-else-if="elementType === 'bpmn:UserTask'">
              <section class="takt-flow-design-property-group">
                <div class="takt-flow-design-property-group-title">审批节点策略</div>
                <p class="takt-flow-design-property-hint-inline">对应 TaktFlowNodeSetInfo（NodeDesignate、NodeDesignateData 等），在流程方案的 ProcessJson 节点 setInfo 中配置。</p>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">办理角色/用户</label>
                  <input class="takt-flow-design-property-input readonly" placeholder="ProcessJson setInfo 中配置" readonly disabled />
                </div>
                <div class="takt-flow-design-property-row">
                  <label class="takt-flow-design-property-label">节点使用表单</label>
                  <input class="takt-flow-design-property-input readonly" placeholder="ProcessJson setInfo 中配置" readonly disabled />
                </div>
              </section>
            </template>
            <template v-else-if="elementType === 'bpmn:EndEvent'">
              <div class="takt-flow-design-property-placeholder">结束节点无审批策略。</div>
            </template>
            <template v-else-if="elementType === 'bpmn:ExclusiveGateway'">
              <div class="takt-flow-design-property-placeholder">网关通过方式（NodeConfluenceType 等）在流程方案 ProcessJson 节点 setInfo 中配置。</div>
            </template>
            <template v-else-if="isNodeType">
              <div class="takt-flow-design-property-placeholder">该节点策略在流程方案 ProcessJson 中配置。</div>
            </template>
            <template v-else-if="elementType === 'bpmn:SequenceFlow'">
              <div class="takt-flow-design-property-placeholder">连线无节点策略。</div>
            </template>
          </a-tab-pane>
        </a-tabs>
      </div>
      <div v-else class="takt-flow-design-properties-empty">
        {{ t('workflow.scheme.selectElementHint') }}
      </div>
    </div>
  </aside>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { PROPERTIES_PANEL_WIDTH } from '../config/config'

/** 项目实际支持的 BPMN 节点类型（与 TaktBpmnParser 解析一致） */
const ELEMENT_TYPES = {
  Process: 'bpmn:Process',
  StartEvent: 'bpmn:StartEvent',
  UserTask: 'bpmn:UserTask',
  EndEvent: 'bpmn:EndEvent',
  ExclusiveGateway: 'bpmn:ExclusiveGateway',
  SequenceFlow: 'bpmn:SequenceFlow'
} as const

const props = withDefaults(
  defineProps<{
    /** 是否有选中元素 */
    hasSelection?: boolean
    /** 当前元素 BPMN $type（与 TaktBpmnParser 一致），用于按节点类型展示属性 */
    elementType?: string
    /** 选中元素 ID */
    selectedId?: string
    /** 选中元素名称 */
    selectedName?: string
    /** 选中元素类型（展示用，如 i18n 后的“开始事件”） */
    selectedElementType?: string
    /** 连线条件表达式（仅 bpmn:SequenceFlow 有效） */
    selectedConditionExpression?: string
    /** 流程是否可执行（仅 bpmn:Process 有效） */
    selectedExecutable?: boolean
    /** 面板宽度（px） */
    width?: number
  }>(),
  {
    hasSelection: false,
    elementType: '',
    selectedId: '',
    selectedName: '',
    selectedElementType: '',
    selectedConditionExpression: '',
    selectedExecutable: false,
    width: PROPERTIES_PANEL_WIDTH
  }
)

const KNOWN_NODE_TYPES: string[] = [ELEMENT_TYPES.StartEvent, ELEMENT_TYPES.UserTask, ELEMENT_TYPES.EndEvent, ELEMENT_TYPES.ExclusiveGateway]

/** 是否为“其他节点”类型（非 Process/SequenceFlow 且非已单独处理的四类），用于兜底展示 */
const isNodeType = computed(() => {
  const t = props.elementType
  if (!t || t === ELEMENT_TYPES.Process || t === ELEMENT_TYPES.SequenceFlow) return false
  if (KNOWN_NODE_TYPES.includes(t)) return false
  return true
})

const emit = defineEmits<{
  (e: 'name-input', value: string): void
  (e: 'condition-expression-input', value: string): void
  (e: 'executable-change', checked: boolean): void
}>()

const { t } = useI18n()

/** 节点属性 Tab 当前 key，与 a-tab-pane key 一致 */
const propertyTab = ref<string>('basic')

const widthPx = computed(() => `${props.width}px`)

function onNameInput(ev: Event) {
  emit('name-input', (ev.target as HTMLInputElement).value)
}

function onConditionExpressionInput(ev: Event) {
  emit('condition-expression-input', (ev.target as HTMLInputElement).value.trim())
}

function onExecutableChange(ev: Event) {
  emit('executable-change', (ev.target as HTMLInputElement).checked)
}
</script>

<style scoped>
.takt-flow-design-properties-panel {
  position: relative;
  flex: 0 0 v-bind(widthPx);
  height: 100%;
  border-left: 1px solid var(--ant-color-border);
  background: var(--ant-color-bg-container);
  display: flex;
  flex-direction: column;
}
.takt-flow-design-properties {
  flex: 1 1 auto;
  min-width: 0;
  width: 100%;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}
.takt-flow-design-properties-header {
  flex-shrink: 0;
  padding: 12px 12px 8px;
  border-bottom: 1px solid var(--ant-color-border);
}
.takt-flow-design-properties-title {
  font-weight: 600;
  font-size: 14px;
  color: var(--ant-color-text);
}
.takt-flow-design-properties-context {
  margin-top: 4px;
  font-size: 12px;
  color: var(--ant-color-text-tertiary);
}
.takt-flow-design-properties-notice {
  flex-shrink: 0;
  margin: 8px 12px 0;
  padding: 6px 8px;
  font-size: 11px;
  line-height: 1.4;
  color: var(--ant-color-text-tertiary);
  background: var(--ant-color-fill-quaternary);
  border-radius: 4px;
}
.takt-flow-design-properties-body {
  flex: 1 1 auto;
  padding: 12px;
  overflow-y: auto;
  overflow-x: hidden;
}
.takt-flow-design-properties-empty {
  flex: 1;
  padding: 24px 12px;
  font-size: 12px;
  text-align: center;
  color: var(--ant-color-text-tertiary);
}
.takt-flow-design-property-tabs {
  margin-bottom: 12px;
}
.takt-flow-design-property-tabs :deep(.ant-tabs-nav) {
  margin-bottom: 12px;
}
.takt-flow-design-property-group {
  margin-bottom: 16px;
}
.takt-flow-design-property-group:last-child {
  margin-bottom: 0;
}
.takt-flow-design-property-group-title {
  margin-bottom: 10px;
  padding-bottom: 6px;
  font-size: 13px;
  font-weight: 600;
  color: var(--ant-color-text);
  border-bottom: 1px solid var(--ant-color-border);
}
.takt-flow-design-property-row {
  margin-bottom: 12px;
}
.takt-flow-design-property-row:last-child {
  margin-bottom: 0;
}
.takt-flow-design-property-label {
  display: block;
  margin-bottom: 4px;
  font-size: 12px;
  color: var(--ant-color-text-secondary);
}
.takt-flow-design-property-input {
  width: 100%;
  padding: 6px 8px;
  border: 1px solid var(--ant-color-border);
  border-radius: 4px;
  box-sizing: border-box;
  background: var(--ant-color-bg-container);
  color: var(--ant-color-text);
  font-size: 12px;
}
.takt-flow-design-property-input.readonly {
  cursor: default;
  background: var(--ant-color-fill-tertiary);
  color: var(--ant-color-text-secondary);
}
.takt-flow-design-property-meta {
  display: block;
  margin-top: 4px;
  font-size: 11px;
  color: var(--ant-color-text-tertiary);
  line-height: 1.3;
}
.takt-flow-design-property-hint-inline {
  margin: 8px 0 0;
  font-size: 11px;
  color: var(--ant-color-text-tertiary);
  line-height: 1.4;
}
.takt-flow-design-property-checkbox-label {
  display: inline-flex;
  align-items: center;
  gap: 6px;
  cursor: pointer;
  font-weight: normal;
  font-size: 12px;
  color: var(--ant-color-text-secondary);
}
.takt-flow-design-property-checkbox-label input[type='checkbox'] {
  margin: 0;
}
.takt-flow-design-property-placeholder {
  padding: 16px 0;
  font-size: 12px;
  color: var(--ant-color-text-tertiary);
  text-align: center;
}
</style>
