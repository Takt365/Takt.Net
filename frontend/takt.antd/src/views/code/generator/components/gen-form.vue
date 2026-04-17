<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/code/generator/components -->
<!-- 文件名称：gen-form.vue -->
<!-- 功能描述：代码生成表配置表单（表配置多 Tab 一行一列 + 字段配置表格） -->
<!-- ======================================== -->

<template>
  <div class="gen-form-root">
    <a-tabs v-model:active-key="activeTab">
      <!-- 表配置：拆成多 Tab，每块一行一列 -->
      <a-tab-pane
        key="table"
        tab="表配置"
        force-render
      >
        <a-form
          ref="formRef"
          :model="formState"
          :label-col="{ span: 6 }"
          :wrapper-col="{ span: 18 }"
          layout="horizontal"
        >
          <a-tabs
            v-model:active-key="tableSubTab"
            type="card"
            size="small"
          >
            <a-tab-pane
              key="basic"
              tab="基本信息"
              force-render
            >
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="数据源"
                    name="configId"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-select
                      :value="parseSelectToOptionalString(formState.configId)"
                      placeholder="请选择数据源"
                      allow-clear
                      style="width: 100%"
                      :options="databaseConfigOptions"
                      :disabled="isEditMode"
                      @update:value="
                        (v: unknown) => {
                          let id: string | undefined
                          if (v == null) id = undefined
                          else if (typeof v === 'string' || typeof v === 'number') id = String(v)
                          else if (typeof v === 'object' && 'value' in (v as object)) {
                            const x = (v as { value: unknown }).value
                            id = x == null ? undefined : String(x)
                          }
                          formState.configId = id
                          handleConfigChange(id)
                        }
                      "
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="数据表"
                    name="tableName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="tableNameRules"
                  >
                    <a-input
                      v-if="!isEditMode"
                      :value="formState.tableName ?? ''"
                      placeholder="小写下划线格式，如：xxx_xxx_xxx"
                      :disabled="!formState.configId"
                      allow-clear
                      @update:value="(v: string) => { formState.tableName = v === '' ? undefined : v }"
                    />
                    <a-select
                      v-else
                      :value="formState.tableName ?? undefined"
                      placeholder="请先选择数据源"
                      disabled
                      style="width: 100%"
                      :options="databaseTableOptions"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="表描述"
                    name="tableComment"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请填写表描述')"
                  >
                    <a-input
                      :value="formState.tableComment ?? ''"
                      placeholder="表注释"
                      allow-clear
                      @update:value="(v: string) => { formState.tableComment = v === '' ? undefined : v }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="生成模板"
                    name="genTemplate"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请选择生成模板')"
                  >
                    <TaktSelect
                      :model-value="formState.genTemplate ?? ''"
                      dict-type="sys_gen_template_type"
                      placeholder="请选择生成模板"
                      allow-clear
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.genTemplate = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="是否库表"
                    name="inDatabase"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <TaktSelect
                      :model-value="formState.inDatabase ?? ''"
                      dict-type="sys_yes_no"
                      placeholder="请选择是否库表"
                      style="width: 100%"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <!-- 主子表：仅当 genTemplate === 'sub' 时显示 -->
              <a-row
                v-if="formState.genTemplate === 'sub'"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="关联父表"
                    name="subTableName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="subTableNameRules"
                  >
                    <a-select
                      :value="formState.subTableName ?? undefined"
                      placeholder="请选择父表"
                      allow-clear
                      style="width: 100%"
                      :options="subTableNameOptions"
                      @update:value="(v: unknown) => { formState.subTableName = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row
                v-if="formState.genTemplate === 'sub'"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="关联外键"
                    name="subTableFkName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="subTableFkNameRules"
                  >
                    <a-select
                      :value="formState.subTableFkName ?? undefined"
                      placeholder="请选择外键列"
                      allow-clear
                      style="width: 100%"
                      :options="columnSelectOptions"
                      @update:value="(v: unknown) => { formState.subTableFkName = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <!-- 树表：仅当 genTemplate === 'tree' 时显示 -->
              <a-row
                v-if="formState.genTemplate === 'tree'"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="树编码字段"
                    name="treeCode"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="treeCodeRules"
                  >
                    <a-select
                      :value="formState.treeCode ?? undefined"
                      placeholder="请选择树编码列"
                      allow-clear
                      style="width: 100%"
                      :options="columnSelectOptions"
                      @update:value="(v: unknown) => { formState.treeCode = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row
                v-if="formState.genTemplate === 'tree'"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="树父编码"
                    name="treeParentCode"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="treeParentCodeRules"
                  >
                    <a-select
                      :value="formState.treeParentCode ?? undefined"
                      placeholder="请选择树父编码列"
                      allow-clear
                      style="width: 100%"
                      :options="columnSelectOptions"
                      @update:value="(v: unknown) => { formState.treeParentCode = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row
                v-if="formState.genTemplate === 'tree'"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="树名称字段"
                    name="treeName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="treeNameRules"
                  >
                    <a-select
                      :value="formState.treeName ?? undefined"
                      placeholder="请选择树名称列"
                      allow-clear
                      style="width: 100%"
                      :options="columnSelectOptions"
                      @update:value="(v: unknown) => { formState.treeName = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
            </a-tab-pane>
            <a-tab-pane
              key="business"
              tab="业务模块"
              force-render
            >
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="命名空间前缀"
                    name="namePrefix"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="namePrefixPascalRules"
                  >
                    <a-input
                      :value="formState.namePrefix ?? ''"
                      placeholder="项目名称，默认 Takt，修改后所有命名空间同步"
                      allow-clear
                      @update:value="(v: string) => { formState.namePrefix = v === '' ? undefined : v }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="权限前缀"
                    name="permsPrefix"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请填写权限前缀')"
                  >
                    <a-input
                      :value="formState.permsPrefix ?? ''"
                      placeholder="由模块名+业务名自动生成，如 accounting:controlling:standard:wage:rate"
                      allow-clear
                      @update:value="(v: string) => { formState.permsPrefix = v === '' ? undefined : v }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="模块名"
                    name="genModuleName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请选择模块名')"
                  >
                    <TaktTreeSelect
                      :value="formState.genModuleName ?? ''"
                      :tree-data="moduleOptionsTree"
                      placeholder="请选择模块（目录）或手动输入，如：Generator、HumanResource.Organization"
                      allow-clear
                      style="width: 100%"
                      :field-names="{ label: 'dictLabel', value: 'dictValue' }"
                      :loading="moduleOptionsLoading"
                      @update:value="(v: unknown) => { formState.genModuleName = parseTreeSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="业务名"
                    name="genBusinessName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请填写业务名')"
                  >
                    <a-input
                      :value="formState.genBusinessName ?? ''"
                      :placeholder="formState.inDatabase === 1 ? '由表名自动生成' : '用于生成实体/服务/控制器等类名及接口注释，如：设置、部门'"
                      :disabled="formState.inDatabase === 1"
                      allow-clear
                      @update:value="(v: string) => { formState.genBusinessName = v === '' ? undefined : v }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="功能名"
                    name="genFunctionName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-input
                      :value="formState.genFunctionName ?? ''"
                      placeholder="由表描述自动带出，仅读"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
            </a-tab-pane>
            <a-tab-pane
              key="entity"
              tab="实体与传输对象"
              force-render
            >
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="实体命名空间"
                    name="entityNamespace"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请填写实体命名空间')"
                  >
                    <a-input
                      :value="formState.entityNamespace ?? ''"
                      placeholder="由模块名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="实体类名"
                    name="entityClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="[{ required: true, message: '请输入实体类名' }]"
                  >
                    <a-input
                      :value="formState.entityClassName ?? ''"
                      placeholder="由业务名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="Dto 类名"
                    name="dtoClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请填写 Dto 类名')"
                  >
                    <a-input
                      :value="formState.dtoClassName ?? ''"
                      placeholder="由业务名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="Dto 命名空间"
                    name="dtoNamespace"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-input
                      :value="formState.dtoNamespace ?? ''"
                      placeholder="由模块名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <!-- Dto 类别：仅收集 a-checkbox-group；全选与分隔线放入 a-form-item-rest 避免 Form.Item 收集多个控件 -->
                  <a-form-item
                    label="Dto 类别"
                    name="dtoCategory"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请选择 Dto 类别')"
                  >
                    <a-form-item-rest>
                      <div>
                        <a-checkbox
                          v-model:checked="dtoCategoryCheckAll"
                          :indeterminate="dtoCategoryIndeterminate"
                          @change="onDtoCategoryCheckAllChange"
                        >
                          全选
                        </a-checkbox>
                      </div>
                      <a-divider style="margin: 8px 0" />
                    </a-form-item-rest>
                    <a-checkbox-group
                      v-model:value="dtoCategorySelect"
                      :options="dtoCategoryOptions"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
            </a-tab-pane>
            <a-tab-pane
              key="service"
              tab="服务与控制器"
              force-render
            >
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="服务命名空间"
                    name="serviceNamespace"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-input
                      :value="formState.serviceNamespace ?? ''"
                      placeholder="由模块名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="服务接口类名"
                    name="iServiceClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请填写服务接口类名')"
                  >
                    <a-input
                      :value="formState.iServiceClassName ?? ''"
                      placeholder="由业务名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="服务类名"
                    name="serviceClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请填写服务类名')"
                  >
                    <a-input
                      :value="formState.serviceClassName ?? ''"
                      placeholder="由业务名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="控制器命名空间"
                    name="controllerNamespace"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请填写控制器命名空间')"
                  >
                    <a-input
                      :value="formState.controllerNamespace ?? ''"
                      placeholder="由模块名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="控制器类名"
                    name="controllerClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-input
                      :value="formState.controllerClassName ?? ''"
                      placeholder="由业务名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="是否生成仓储"
                    name="isRepository"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请选择是否生成仓储')"
                  >
                    <a-radio-group
                      :value="formState.isRepository ?? undefined"
                      :options="sysYesNoOptions"
                      @update:value="(v: unknown) => { formState.isRepository = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <!-- 仓储相关字段：仅当「是否生成仓储」为「是」(0) 时显示，与「否」相斥 -->
              <a-row
                v-if="formState.isRepository === 0"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="仓储接口命名空间"
                    name="repositoryInterfaceNamespace"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="repositoryInterfaceNamespaceRules"
                  >
                    <a-input
                      :value="formState.repositoryInterfaceNamespace ?? ''"
                      placeholder="由模块名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row
                v-if="formState.isRepository === 0"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="仓储接口类名"
                    name="iRepositoryClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="iRepositoryClassNameRules"
                  >
                    <a-input
                      :value="formState.iRepositoryClassName ?? ''"
                      placeholder="由业务名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row
                v-if="formState.isRepository === 0"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="仓储命名空间"
                    name="repositoryNamespace"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="repositoryNamespaceRules"
                  >
                    <a-input
                      :value="formState.repositoryNamespace ?? ''"
                      placeholder="由模块名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row
                v-if="formState.isRepository === 0"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="仓储类名"
                    name="repositoryClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="repositoryClassNameRules"
                  >
                    <a-input
                      :value="formState.repositoryClassName ?? ''"
                      placeholder="由业务名自动生成"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
            </a-tab-pane>
            <a-tab-pane
              key="generate"
              tab="生成"
              force-render
            >
              <a-row :gutter="24">
                <a-col :span="24">
                  <!-- 生成功能：仅收集 a-checkbox-group；全选与分隔线放入 a-form-item-rest 避免 Form.Item 收集多个控件 -->
                  <a-form-item
                    label="生成功能"
                    name="genFunction"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-form-item-rest>
                      <div>
                        <a-checkbox
                          v-model:checked="genFunctionCheckAll"
                          :indeterminate="genFunctionIndeterminate"
                          @change="onGenFunctionCheckAllChange"
                        >
                          全选
                        </a-checkbox>
                      </div>
                      <a-divider style="margin: 8px 0" />
                    </a-form-item-rest>
                    <a-checkbox-group
                      v-model:value="genFunctionSelect"
                      :options="genFunctionOptions"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="生成方式"
                    name="genMethod"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请选择生成方式')"
                  >
                    <TaktSelect
                      :model-value="formState.genMethod ?? ''"
                      dict-type="sys_gen_method"
                      placeholder="请选择生成方式"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.genMethod = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <!-- 生成路径：仅当「生成方式」为「自定义路径」(1) 时显示；zip(0)、当前项目(2) 不显示 -->
              <a-row
                v-if="formState.genMethod === 1"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="生成路径"
                    name="genPath"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="genPathRules"
                  >
                    <a-input
                      :value="formState.genPath ?? ''"
                      placeholder="/"
                      allow-clear
                      @update:value="(v: string) => { formState.genPath = v === '' ? undefined : v }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <!-- 当前项目路径：仅当「生成方式」为「当前项目」(2) 时显示，自动从后端获取 -->
              <a-row
                v-if="formState.genMethod === 2"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="当前项目路径"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-input
                      :value="currentProjectPathDisplay"
                      readonly
                      :placeholder="currentProjectPathLoading ? '正在获取…' : '请选择生成方式为当前项目后自动获取'"
                    >
                      <template #suffix>
                        <a-spin
                          v-if="currentProjectPathLoading"
                          size="small"
                        />
                      </template>
                    </a-input>
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="是否生成菜单"
                    name="isGenMenu"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <TaktSelect
                      :model-value="formState.isGenMenu ?? ''"
                      dict-type="sys_yes_no"
                      placeholder="请选择"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.isGenMenu = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <!-- 上级菜单：仅当「是否生成菜单」为「是」(0) 时显示，与「否」相斥 -->
              <a-row
                v-if="formState.isGenMenu == 0"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="上级菜单"
                    name="parentMenuId"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="parentMenuIdRules"
                  >
                    <TaktTreeSelect
                      :value="formState.parentMenuId ?? ''"
                      api-url="/api/TaktMenus/tree-options"
                      placeholder="请选择上级菜单（不选为根）"
                      allow-clear
                      style="width: 100%"
                      :field-names="{ label: 'dictLabel', value: 'dictValue' }"
                      @update:value="(v: unknown) => { formState.parentMenuId = parseTreeSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="是否生成翻译"
                    name="isGenTranslation"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请选择是否生成翻译')"
                  >
                    <TaktSelect
                      :model-value="formState.isGenTranslation ?? ''"
                      dict-type="sys_yes_no"
                      placeholder="请选择"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.isGenTranslation = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="排序类型"
                    name="sortType"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请选择排序类型')"
                  >
                    <TaktSelect
                      :model-value="formState.sortType ?? ''"
                      dict-type="sys_sort_type"
                      placeholder="请选择排序类型"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.sortType = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="排序字段"
                    name="sortField"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请选择排序字段')"
                  >
                    <a-select
                      :value="formState.sortField ?? undefined"
                      placeholder="请选择排序字段"
                      allow-clear
                      style="width: 100%"
                      :options="columnSelectOptions"
                      @update:value="(v: unknown) => { formState.sortField = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
            </a-tab-pane>
            <a-tab-pane
              key="front"
              tab="前端与样式"
              force-render
            >
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="前端模板"
                    name="frontTemplate"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <TaktSelect
                      :model-value="formState.frontTemplate ?? ''"
                      dict-type="sys_frontend_template"
                      placeholder="请选择前端模板"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.frontTemplate = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="前端样式"
                    name="frontStyle"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请选择前端样式')"
                  >
                    <TaktSelect
                      :model-value="formState.frontStyle ?? ''"
                      dict-type="sys_frontend_style"
                      placeholder="请选择前端样式"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.frontStyle = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="按钮样式"
                    name="btnStyle"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <TaktSelect
                      :model-value="formState.btnStyle ?? ''"
                      dict-type="sys_button_style"
                      placeholder="请选择按钮样式"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.btnStyle = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="是否使用 Tabs"
                    name="isUseTabs"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请选择是否使用 Tabs')"
                  >
                    <TaktSelect
                      :model-value="formState.isUseTabs ?? ''"
                      dict-type="sys_yes_no"
                      placeholder="请选择"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.isUseTabs = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <!-- Tabs 字段数：仅当「是否使用 Tabs」为「是」(0) 时显示，与「否」相斥 -->
              <a-row
                v-if="formState.isUseTabs == 0"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    label="Tabs 字段数"
                    name="tabsFieldCount"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="tabsFieldCountRules"
                  >
                    <a-input-number
                      :value="formState.tabsFieldCount ?? 0"
                      @update:value="(v: string | number | null) => { formState.tabsFieldCount = parseSelectToOptionalNumber(v) ?? 0 }"
                      :min="1"
                      style="width: 100%"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="作者"
                    name="genAuthor"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="ruleRequired('请填写作者')"
                  >
                    <a-input
                      :value="formState.genAuthor ?? ''"
                      disabled
                      placeholder="当前登录用户"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    label="其他选项(JSON)"
                    name="options"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-textarea
                      :value="formState.options ?? ''"
                      :rows="4"
                      allow-clear
                      @update:value="(v: string) => { formState.options = v === '' ? undefined : v }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
            </a-tab-pane>
          </a-tabs>
        </a-form>
      </a-tab-pane>

      <!-- 字段配置：表格行内编辑，横向滚动仅在此表格容器内 -->
      <a-tab-pane
        key="column"
        tab="字段配置"
        force-render
      >
        <div class="column-toolbar">
          <a-button
            type="primary"
            size="small"
            @click="addColumnRow"
          >
            新增行
          </a-button>
        </div>
        <div class="column-table-wrap">
          <a-table
            :columns="columnTableColumns"
            :data-source="columnList"
            :loading="columnLoading"
            :row-key="(r: GenTableColumnRow) => String(r.columnId ?? '')"
            :custom-row="(r: GenTableColumnRow, i?: number) => columnTableCustomRow(r, i ?? 0)"
            :pagination="false"
            size="small"
            bordered
          >
            <template #bodyCell="{ column, record }">
              <!-- 拖拽把手：仅此格可拖拽，整行可放置 -->
              <template v-if="column.key === 'dragSort'">
                <span
                  class="column-drag-handle"
                  draggable="true"
                  @dragstart="(e: DragEvent) => onColumnDragStart(e, record as GenTableColumnRow)"
                  @dragover="onColumnDragOver"
                >
                  <HolderOutlined />
                </span>
              </template>
              <!-- 列名：行内输入 -->
              <template v-else-if="column.key === 'databaseColumnName'">
                <a-input
                  :value="record.databaseColumnName ?? ''"
                  size="small"
                  allow-clear
                  class="column-cell-input"
                  @update:value="(v: string) => { record.databaseColumnName = v === '' ? undefined : v }"
                />
              </template>
              <!-- 描述：行内输入 -->
              <template v-else-if="column.key === 'columnComment'">
                <a-input
                  :value="record.columnComment ?? ''"
                  size="small"
                  allow-clear
                  class="column-cell-input"
                  @update:value="(v: string) => { record.columnComment = v === '' ? undefined : v }"
                />
              </template>
              <!-- DB类型：字典 sys_db_type，选中后级联 C#类型 -->
              <template v-else-if="column.key === 'databaseDataType'">
                <TaktSelect
                  :model-value="record.databaseDataType ?? ''"
                  dict-type="sys_db_type"
                  placeholder="DB类型"
                  allow-clear
                  size="small"
                  class="column-cell-select"
                  style="width: 100%"
                  @update:model-value="(v: unknown) => { record.databaseDataType = parseSelectToOptionalString(v) }"
                  @change="(v: string | number | (string | number)[] | undefined) => onColumnDbTypeChange(record as GenTableColumnRow, v != null ? String(v) : '')"
                />
              </template>
              <!-- C#类型：与 DB类型级联，仅显示当前 DB类型对应的 C#类型选项；切换时按类型清空长度/精度 -->
              <template v-else-if="column.key === 'csharpDataType'">
                <a-select
                  :value="record.csharpDataType ?? undefined"
                  :options="getCsharpTypeOptionsForRow(record.databaseDataType)"
                  placeholder="C#类型"
                  allow-clear
                  size="small"
                  class="column-cell-select"
                  style="width: 100%"
                  @update:value="(v: unknown) => { record.csharpDataType = parseSelectToOptionalString(v) }"
                  @change="(value) => onColumnCsharpTypeChange(record as GenTableColumnRow, value != null ? String(value) : '')"
                />
              </template>
              <!-- C#列名：行内输入 -->
              <template v-else-if="column.key === 'csharpColumnName'">
                <a-input
                  :value="record.csharpColumnName ?? ''"
                  size="small"
                  allow-clear
                  class="column-cell-input"
                  @update:value="(v: string) => { record.csharpColumnName = v === '' ? undefined : v }"
                />
              </template>
              <!-- 长度：仅 string/decimal 类型显示（字符串长度或 decimal 整数位数） -->
              <template v-else-if="column.key === 'length'">
                <a-input-number
                  v-if="needLengthForCsharpType(record.csharpDataType)"
                  :value="record.length ?? undefined"
                  size="small"
                  :min="0"
                  class="column-cell-input"
                  style="width: 100%"
                  @update:value="(v: unknown) => { record.length = parseSelectToOptionalNumber(v) }"
                />
                <span
                  v-else
                  class="column-cell-muted"
                >—</span>
              </template>
              <!-- 精度：仅 decimal 类型显示（小数位数） -->
              <template v-else-if="column.key === 'decimalDigits'">
                <a-input-number
                  v-if="needDecimalDigitsForCsharpType(record.csharpDataType)"
                  :value="record.decimalDigits ?? undefined"
                  size="small"
                  :min="0"
                  class="column-cell-input"
                  style="width: 100%"
                  @update:value="(v: unknown) => { record.decimalDigits = parseSelectToOptionalNumber(v) }"
                />
                <span
                  v-else
                  class="column-cell-muted"
                >—</span>
              </template>
              <!-- 主键/自增/必填/查询/新增/更新/查重/列表/导出/排序：行内开关（后端约定 1=是、0=否）；是否查询为否时清空查询方式 -->
              <template v-else-if="column.key === 'isPk' || column.key === 'isIncrement' || column.key === 'isRequired' || column.key === 'isQuery' || column.key === 'isCreate' || column.key === 'isUpdate' || column.key === 'isUnique' || column.key === 'isList' || column.key === 'isExport' || column.key === 'isSort'">
                <a-switch
                  :checked="record[String(column.key)] === 1"
                  size="small"
                  :checked-children="'是'"
                  :un-checked-children="'否'"
                  @change="(checked: unknown) => {
                    const key = String(column.key)
                    const isOn = checked === true || checked === 1 || checked === '1'
                    record[key] = isOn ? 1 : 0
                    if (key === 'isQuery' && !isOn) onColumnIsQueryChange(record)
                  }"
                />
              </template>
              <!-- 查询方式：仅当「是否查询」为是时显示，字典 sys_query_type -->
              <template v-else-if="column.key === 'queryType'">
                <TaktSelect
                  v-if="record.isQuery === 1"
                  :model-value="record.queryType ?? undefined"
                  dict-type="sys_query_type"
                  placeholder="查询方式"
                  allow-clear
                  size="small"
                  class="column-cell-select"
                  style="width: 100%"
                  @update:model-value="(v: unknown) => { record.queryType = parseSelectToOptionalString(v) }"
                />
                <span
                  v-else
                  class="column-cell-muted"
                >—</span>
              </template>
              <!-- 显示类型：字典 sys_display_type（下拉框/复选框/单选框时需配合字典列绑定选项） -->
              <template v-else-if="column.key === 'htmlType'">
                <TaktSelect
                  :model-value="record.htmlType ?? undefined"
                  dict-type="sys_display_type"
                  placeholder="显示类型"
                  allow-clear
                  size="small"
                  class="column-cell-select"
                  style="width: 100%"
                  @update:model-value="(v: unknown) => { record.htmlType = parseSelectToOptionalString(v) }"
                  @change="(v: string | number | (string | number)[] | undefined) => onColumnHtmlTypeChange(record as GenTableColumnRow, v)"
                />
              </template>
              <!-- 字典：仅当显示类型为下拉框/复选框/单选框时显示，用于绑定字典类型选项 -->
              <template v-else-if="column.key === 'dictType'">
                <TaktSelect
                  v-if="needDictTypeForHtmlType(record.htmlType)"
                  :model-value="record.dictType ?? undefined"
                  :options="dictTypeOptions"
                  :field-names="{ label: 'dictLabel', value: 'extLabel' }"
                  placeholder="选择字典类型"
                  allow-clear
                  size="small"
                  class="column-cell-select"
                  style="width: 100%"
                  @update:model-value="(v: unknown) => { record.dictType = parseSelectToOptionalString(v) }"
                />
                <span
                  v-else
                  class="column-cell-muted"
                >—</span>
              </template>
              <!-- 排序：从 1 开始，支持拖拽调整顺序 -->
              <template v-else-if="column.key === 'orderNum'">
                <a-input-number
                  :value="record.orderNum ?? undefined"
                  size="small"
                  :min="1"
                  class="column-cell-input"
                  style="width: 100%"
                  @update:value="(v: unknown) => { record.orderNum = parseSelectToOptionalNumber(v) }"
                />
              </template>
              <!-- 操作：删除行 -->
              <template v-else-if="column.key === 'action'">
                <a-button
                  type="link"
                  danger
                  size="small"
                  @click="removeColumnRow(record as GenTableColumnRow)"
                >
                  删除
                </a-button>
              </template>
            </template>
            <template #emptyText>
              <a-empty
                v-if="!formData?.tableId"
                description="请先保存表配置后再管理字段"
              />
              <a-empty
                v-else
                description="暂无字段数据"
              />
            </template>
          </a-table>
        </div>
      </a-tab-pane>
    </a-tabs>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, computed, onMounted } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import type { GenTable } from '@/types/generator/table'
import type { GenTableColumn } from '@/types/generator/table-column'
import { getColumnsByTableId } from '@/api/generator/table-column'
import { getDefaultGenPath } from '@/api/generator/table'
import type { DatabaseConfig, DatabaseTableInfo } from '@/api/generator/table'
import { getDictTypeOptions } from '@/api/routine/tasks/dict/dicttype'
import type { TaktSelectOption, TaktTreeSelectOption } from '@/types/common'
import TaktSelect from '@/components/business/takt-select/index.vue'
import TaktTreeSelect from '@/components/business/takt-tree-select/index.vue'
import { getModuleNameOptions } from '@/api/identity/menu'
import type { MenuTree } from '@/types/identity/menu'
import { HolderOutlined } from '@ant-design/icons-vue'
import { useDictDataStore } from '@/stores/routine/dict/dictdata'
import { useUserStore } from '@/stores/identity/user'

type OptionalText = string | undefined

interface GenFormState {
  tableId: string | undefined
  configId: string | undefined
  dataSource: string | undefined
  tableName: string | undefined
  tableComment: string | undefined
  subTableName: string | undefined
  subTableFkName: string | undefined
  treeCode: string | undefined
  treeParentCode: string | undefined
  treeName: string | undefined
  inDatabase: number | undefined
  genTemplate: string | undefined
  namePrefix: string | undefined
  entityNamespace: string | undefined
  entityClassName: string | undefined
  dtoNamespace: string | undefined
  dtoClassName: string | undefined
  dtoCategory: string | undefined
  serviceNamespace: string | undefined
  iServiceClassName: string | undefined
  serviceClassName: string | undefined
  controllerNamespace: string | undefined
  controllerClassName: string | undefined
  repositoryInterfaceNamespace: string | undefined
  iRepositoryClassName: string | undefined
  repositoryNamespace: string | undefined
  repositoryClassName: string | undefined
  genModuleName: string | undefined
  genBusinessName: string | undefined
  genFunctionName: string | undefined
  genFunction: string | undefined
  genMethod: number | undefined
  isRepository: number | undefined
  genPath: string | undefined
  parentMenuId: number | undefined
  isGenMenu: number | undefined
  isGenTranslation: number | undefined
  sortType: string | undefined
  sortField: string | undefined
  permsPrefix: string | undefined
  frontTemplate: number | undefined
  frontStyle: number | undefined
  btnStyle: number | undefined
  isUseTabs: number | undefined
  tabsFieldCount: number | undefined
  genAuthor: string | undefined
  options: string | undefined
  columns: GenTableColumnRow[] | undefined
}

interface GenTableColumnRow extends Record<string, unknown> {
  columnId: number | string | undefined
  tableId: number | string | undefined
  databaseColumnName: OptionalText
  columnComment: OptionalText
  databaseDataType: OptionalText
  csharpDataType: OptionalText
  csharpColumnName: OptionalText
  length: number | undefined
  decimalDigits: number | undefined
  isPk: number | undefined
  isIncrement: number | undefined
  isRequired: number | undefined
  isCreate: number | undefined
  isUpdate: number | undefined
  isUnique: number | undefined
  isList: number | undefined
  isExport: number | undefined
  isSort: number | undefined
  isQuery: number | undefined
  queryType: OptionalText
  htmlType: OptionalText
  dictType: OptionalText
  orderNum: number | undefined
}

const props = withDefaults(
  defineProps<{
    formData?: Partial<GenTable> | null
    databaseConfigs?: DatabaseConfig[]
    databaseTables?: DatabaseTableInfo[]
    databaseTablesLoading?: boolean
  }>(),
  { formData: null, databaseConfigs: () => [], databaseTables: () => [], databaseTablesLoading: false }
)

const emit = defineEmits<{
  (e: 'config-change', configId: string): void
}>()

const activeTab = ref('table')
const tableSubTab = ref('basic')
const formRef = ref<FormInstance>()
const formState = ref<GenFormState>(defaultFormState())
const columnList = ref<GenTableColumnRow[]>([])
/** 本地新增列行：临时 columnId（负整数），与后端 int64 正 id 区分；提交时 getValues 按新列处理 */
let clientTempColumnSeq = 0
const columnLoading = ref(false)
const dictDataStore = useDictDataStore()
const userStore = useUserStore()
/** 默认生成路径（当前项目路径），由后端接口返回，新建时作为 genPath 默认值；GenMethod=2 时也用于展示当前项目路径 */
const defaultGenPath = ref('')
/** 正在获取当前项目路径（GenMethod=2 时选中后自动拉取） */
const currentProjectPathLoading = ref(false)
/** 字典类型选项（来自 /api/TaktDictTypes/options，供字段配置「字典」列选择要绑定的字典类型编码） */
const dictTypeOptions = ref<TaktSelectOption[]>([])
/** 模块名称选项（来自 /api/TaktMenus/module-name-options，仅目录树形），用于模块名选择 */
const moduleOptionsTree = ref<TaktTreeSelectOption[]>([])
const moduleOptionsLoading = ref(false)

/** 主表 id：优先 tableId，其次兼容历史字段 id（string | number） */
function readGenTablePrimaryId(row: Partial<GenTable> | GenFormState): string | undefined {
  if (row.tableId != null && String(row.tableId).trim() !== '') return String(row.tableId)
  const legacy = 'id' in row ? row.id : undefined
  if (typeof legacy === 'string' || typeof legacy === 'number') return String(legacy)
  return undefined
}

/** OpenAPI 常见 string | null；AntD Select/Input 的 value 不接受 null */
function parseSelectToOptionalString(v: unknown): string | undefined {
  if (v == null) return undefined
  if (typeof v === 'string' || typeof v === 'number') return String(v)
  if (typeof v === 'object' && 'value' in v) {
    return parseSelectToOptionalString((v as { value: unknown }).value)
  }
  return undefined
}

function parseSelectToOptionalNumber(v: unknown): number | undefined {
  if (v == null || v === '') return undefined
  if (typeof v === 'number') return Number.isFinite(v) ? v : undefined
  if (typeof v === 'string') {
    const n = Number(v)
    return Number.isFinite(n) ? n : undefined
  }
  if (typeof v === 'object' && 'value' in v) {
    return parseSelectToOptionalNumber((v as { value: unknown }).value)
  }
  return undefined
}

/** TaktTreeSelect 单选：模块路径等 string 字段 */
function parseTreeSelectToOptionalString(v: unknown): string | undefined {
  if (v == null) return undefined
  if (Array.isArray(v)) {
    const first = v[0]
    return first == null ? undefined : String(first)
  }
  if (typeof v === 'string' || typeof v === 'number') return String(v)
  return undefined
}

/** TaktTreeSelect 单选：菜单 id（int64）等数字字段 */
function parseTreeSelectToOptionalNumber(v: unknown): number | undefined {
  if (v == null) return undefined
  if (Array.isArray(v)) return parseTreeSelectToOptionalNumber(v[0])
  return parseSelectToOptionalNumber(v)
}

/** Dto 类别：选项与选中值仅用于 formState.dtoCategory，勿与生成功能混用 */
const dtoCategoryOptions = computed(() => dictDataStore.getDictOptions('sys_dto_category'))
/** 生成功能：选项与选中值仅用于 formState.genFunction，勿与 Dto 类别混用 */
const genFunctionOptions = computed(() => dictDataStore.getDictOptions('sys_gen_function'))

const sysYesNoOptions = computed(() =>
  dictDataStore.getDictOptions('sys_yes_no').map(opt => ({
    label: opt.label,
    value: Number(opt.value)
  }))
)

/** GenMethod=2 时展示的当前项目路径（与 defaultGenPath 一致，选中 2 时会自动拉取） */
const currentProjectPathDisplay = computed(() => defaultGenPath.value || '')

/** 是否编辑模式：只判断主表 ID —— 有 tableId 为编辑，无则为新增（watch 已把 formData.id 并入 formState.tableId） */
const isEditMode = computed(() => !!formState.value.tableId)

const databaseConfigOptions = computed(() =>
  (props.databaseConfigs ?? []).map(c => ({ value: c.configId, label: `${c.displayName} (${c.configId})` }))
)
const databaseTableOptions = computed(() =>
  (props.databaseTables ?? []).map(t => ({
    value: t.tableName,
    label: t.tableComment ? `${t.tableName} - ${t.tableComment}` : t.tableName
  }))
)

/** 当前表列选项（用于排序字段、外键列、树编码/父编码/名称），依赖字段配置加载后才有数据 */
const columnSelectOptions = computed(() =>
  columnList.value.map(col => {
    const name = col.databaseColumnName ?? col.csharpColumnName ?? ''
    const label = col.columnComment ? `${name} - ${col.columnComment}` : name
    return { value: name, label }
  })
)

/** 关联父表选项：同数据源下的表列表，排除当前表（仅主子表时用） */
const subTableNameOptions = computed(() => {
  const current = formState.value.tableName
  return (props.databaseTables ?? [])
    .filter(t => t.tableName !== current)
    .map(t => ({
      value: t.tableName,
      label: t.tableComment ? `${t.tableName} - ${t.tableComment}` : t.tableName
    }))
})

/** Dto 类别多选：仅与 formState.dtoCategory 双向同步，供「Dto 类别」a-checkbox-group 使用，选中值为字典 sys_dto_category 的 value */
const dtoCategorySelect = computed({
  get() {
    const s = formState.value.dtoCategory
    if (!s || typeof s !== 'string') return []
    return s.split(/[,，\s]+/).map((x: string) => x.trim()).filter(Boolean)
  },
  set(v: (string | number)[] | undefined) {
    const arr = Array.isArray(v) ? v.map(x => String(x)).filter(Boolean) : []
    formState.value.dtoCategory = arr.length ? arr.join(',') : undefined
  }
})

/** 生成功能多选：仅与 formState.genFunction 双向同步，供「生成功能」a-checkbox-group 使用，选中值为字典 sys_gen_function 的 value */
const genFunctionSelect = computed({
  get() {
    const s = formState.value.genFunction
    if (!s || typeof s !== 'string') return []
    return s.split(/[,，\s]+/).map((x: string) => x.trim()).filter(Boolean)
  },
  set(v: (string | number)[] | undefined) {
    const arr = Array.isArray(v) ? v.map(x => String(x)).filter(Boolean) : []
    formState.value.genFunction = arr.length ? arr.join(',') : undefined
  }
})

/** Dto 类别：全选勾选态与半选态（与官方 a-checkbox-group 示例一致，选中值 = options 的 value 数组）；默认全选 */
const dtoCategoryCheckAll = ref(true)
const dtoCategoryIndeterminate = ref(false)
function onDtoCategoryCheckAllChange(e: { target: { checked: boolean } }) {
  const checked = e.target.checked
  const opts = dtoCategoryOptions.value
  formState.value.dtoCategory = checked && opts.length ? opts.map(o => String(o.value)).join(',') : undefined
  dtoCategoryIndeterminate.value = false
  dtoCategoryCheckAll.value = checked
}
watch(
  [() => formState.value.dtoCategory, dtoCategoryOptions],
  () => {
    const list = (formState.value.dtoCategory ?? '')
      ? (formState.value.dtoCategory as string).split(/[,，\s]+/).map((x: string) => x.trim()).filter(Boolean)
      : []
    const total = dtoCategoryOptions.value.length
    dtoCategoryIndeterminate.value = list.length > 0 && list.length < total
    dtoCategoryCheckAll.value = total > 0 && list.length === total
  },
  { immediate: true }
)

/** 生成功能：全选勾选态与半选态（与官方 a-checkbox-group 示例一致，选中值 = options 的 value 数组）；默认全选 */
const genFunctionCheckAll = ref(true)
const genFunctionIndeterminate = ref(false)
function onGenFunctionCheckAllChange(e: { target: { checked: boolean } }) {
  const checked = e.target.checked
  const opts = genFunctionOptions.value
  formState.value.genFunction = checked && opts.length ? opts.map(o => String(o.value)).join(',') : undefined
  genFunctionIndeterminate.value = false
  genFunctionCheckAll.value = checked
}
watch(
  [() => formState.value.genFunction, genFunctionOptions],
  () => {
    const list = (formState.value.genFunction ?? '')
      ? (formState.value.genFunction as string).split(/[,，\s]+/).map((x: string) => x.trim()).filter(Boolean)
      : []
    const total = genFunctionOptions.value.length
    genFunctionIndeterminate.value = list.length > 0 && list.length < total
    genFunctionCheckAll.value = total > 0 && list.length === total
  },
  { immediate: true }
)

onMounted(async () => {
  await dictDataStore.loadAllDictData()
  if (!formState.value.tableId) {
    const dtoOpts = dictDataStore.getDictOptions('sys_dto_category')
    const genOpts = dictDataStore.getDictOptions('sys_gen_function')
    if (dtoOpts.length > 0) formState.value.dtoCategory = dtoOpts.map(o => String(o.value)).join(',')
    if (genOpts.length > 0) formState.value.genFunction = genOpts.map(o => String(o.value)).join(',')
  }
  try {
    dictTypeOptions.value = await getDictTypeOptions()
  } catch {
    dictTypeOptions.value = []
  }
  try {
    const res = await getDefaultGenPath()
    if (res?.path) {
      defaultGenPath.value = res.path
      if (!props.formData && formState.value.genPath === '/') formState.value.genPath = res.path
    }
  } catch {
    defaultGenPath.value = ''
  }
  try {
    moduleOptionsLoading.value = true
    const raw = await getModuleNameOptions()
    const list = (raw as unknown) as MenuTree[] | undefined
    moduleOptionsTree.value = mapMenuTreeToTreeSelectOption(Array.isArray(list) ? list : [])
  } catch {
    moduleOptionsTree.value = []
  } finally {
    moduleOptionsLoading.value = false
  }
})

/**
 * 将菜单 Path 还原为模块名称（帕斯卡加点）。
 * 例如：/accounting/controlling → Accounting.Controlling
 */
function pathToModuleName(path: string | undefined): string {
  if (path == null || String(path).trim() === '') return ''
  const segments = String(path)
    .replace(/^\/+|\/+$/g, '')
    .split('/')
    .filter(Boolean)
  return segments
    .map(s => (s.length > 0 ? s.charAt(0).toUpperCase() + s.slice(1).toLowerCase() : s))
    .join('.')
}

/**
 * 将 menuCode（如 ACCOUNTING_FINANCIAL）转为模块名格式 Accounting.Financial
 */
function menuCodeToModuleName(menuCode: string | undefined): string {
  if (menuCode == null || String(menuCode).trim() === '') return ''
  const segments = String(menuCode).trim().split(/[._-]+/).filter(Boolean)
  return segments
    .map(s => (s.length > 0 ? s.charAt(0).toUpperCase() + s.slice(1).toLowerCase() : s))
    .join('.')
}

/** 将模块名称选项 API 返回的菜单树转为 TaktTreeSelectOption（dictLabel=menuName, dictValue=由 path 还原的模块名，保证非空；字段均为小驼峰） */
function mapMenuTreeToTreeSelectOption(trees: MenuTree[]): TaktTreeSelectOption[] {
  return trees.map((node): TaktTreeSelectOption => {
    const path = node.path ?? undefined
    const menuCode = node.menuCode ?? undefined
    const dictVal = node.dictValue
    const idFallback = node.menuId != null ? String(node.menuId) : ''
    const moduleName =
      pathToModuleName(path) ||
      menuCodeToModuleName(menuCode) ||
      (dictVal != null && dictVal !== '' ? String(dictVal) : '') ||
      idFallback
    return {
      dictLabel: node.menuName ?? node.dictLabel ?? '',
      dictValue: moduleName,
      orderNum: node.orderNum ?? 0,
      ...(node.children?.length ? { children: mapMenuTreeToTreeSelectOption(node.children) } : {})
    }
  })
}

watch(defaultGenPath, (path) => {
  if (path && !props.formData && formState.value.genPath === '/') formState.value.genPath = path
})

/** 选中「当前项目」(2) 时自动获取当前项目路径 */
watch(
  () => formState.value.genMethod,
  async (method) => {
    if (Number(method) !== 2) return
    if (defaultGenPath.value) return
    currentProjectPathLoading.value = true
    try {
      const res = await getDefaultGenPath()
      if (res?.path) defaultGenPath.value = res.path
    } catch {
      defaultGenPath.value = ''
    } finally {
      currentProjectPathLoading.value = false
    }
  }
)

function handleConfigChange(value: unknown) {
  const configId = value != null ? String(value) : undefined
  if (configId) {
    const config = props.databaseConfigs?.find(c => c.configId === configId)
    if (config) formState.value.dataSource = `${config.displayName}:${config.configId}`
    emit('config-change', configId)
  } else {
    formState.value.dataSource = undefined
  }
  formState.value.tableName = undefined
  formState.value.tableComment = undefined
}

/** DB类型 -> C#类型 级联映射（与后端 MapDbTypeToCsharp 一致） */
const DB_TYPE_TO_CSHARP: Record<string, string> = {
  bigint: 'long',
  bit: 'bool',
  datetime: 'DateTime',
  decimal: 'decimal',
  int: 'int',
  ntext: 'string',
  nvarchar: 'string',
  text: 'string',
  uniqueidentifier: 'Guid',
  varchar: 'string'
}

/** 全部 C#类型选项（来自字典 sys_csharp_type） */
const columnCsharpTypeOptions = computed(() =>
  dictDataStore.getDictOptions('sys_csharp_type').map(o => ({ label: o.label, value: o.value }))
)

/** 根据当前行 DB类型 得到 C#类型 下拉选项：选中 DB 类型时仅显示对应 C# 类型，未选时显示全部 */
function getCsharpTypeOptionsForRow(dbType: string | undefined) {
  const all = columnCsharpTypeOptions.value
  if (!dbType || !DB_TYPE_TO_CSHARP[dbType]) return all
  const mapped = DB_TYPE_TO_CSHARP[dbType]
  const single = all.filter(o => String(o.value) === mapped)
  return single.length ? single : all
}

/** 按 C# 类型的长度/精度默认值（与后端约定一致） */
const DEFAULT_LENGTH_STRING = 64
const DEFAULT_LENGTH_DECIMAL = 18
const DEFAULT_DECIMAL_DIGITS = 2

/** 需要「长度」的 C# 类型：string（字符串长度）、decimal（整数位数） */
function needLengthForCsharpType(csharpType: string | number | undefined): boolean {
  if (csharpType == null) return false
  const t = String(csharpType).trim()
  return t === 'string' || t === 'decimal'
}

/** 需要「精度」的 C# 类型：仅 decimal（小数位数） */
function needDecimalDigitsForCsharpType(csharpType: string | number | undefined): boolean {
  if (csharpType == null) return false
  return String(csharpType).trim() === 'decimal'
}

/** 根据 C# 类型为 record 设置长度/精度默认值（string→64，decimal→18,2） */
function applyLengthDecimalDefaults(record: GenTableColumnRow, csharpType: string) {
  const t = csharpType.trim()
  if (t === 'string') {
    record.length = DEFAULT_LENGTH_STRING
    record.decimalDigits = undefined
  } else if (t === 'decimal') {
    record.length = DEFAULT_LENGTH_DECIMAL
    record.decimalDigits = DEFAULT_DECIMAL_DIGITS
  } else {
    record.length = undefined
    record.decimalDigits = undefined
  }
}

/** C#类型变更时：按类型清空或设为默认（string→64，decimal→18,2） */
function onColumnCsharpTypeChange(record: GenTableColumnRow, csharpType: string) {
  if (!record || typeof record !== 'object') return
  const r = record
  applyLengthDecimalDefaults(r, csharpType)
}

/** DB类型变更时：级联 C#类型 并应用长度/精度默认值 */
function onColumnDbTypeChange(record: GenTableColumnRow, dbType: string) {
  const mapped = dbType ? DB_TYPE_TO_CSHARP[dbType] : undefined
  if (mapped === undefined || !record || typeof record !== 'object') return
  const r = record
  r.csharpDataType = mapped
  applyLengthDecimalDefaults(r, mapped)
}

/** 需要绑定字典的显示类型（与 sys_display_type 的 DictValue 一致） */
const HTML_TYPES_NEED_DICT = ['select', 'checkbox', 'radio']

/** 当前显示类型是否需要填写字典：仅下拉框/复选框/单选框需要 */
function needDictTypeForHtmlType(htmlType: string | number | undefined): boolean {
  if (htmlType == null) return false
  return HTML_TYPES_NEED_DICT.includes(String(htmlType))
}

/** 显示类型变更时：若非下拉/复选框/单选框则清空字典 */
function onColumnHtmlTypeChange(record: GenTableColumnRow, value: string | number | (string | number)[] | undefined) {
  const v = Array.isArray(value) ? value[0] : value
  if (!record || typeof record !== 'object') return
  if (!needDictTypeForHtmlType(v)) (record).dictType = undefined
}

/** 是否查询改为「否」时清空查询方式 */
function onColumnIsQueryChange(record: GenTableColumn  ) {
  if (record && typeof record === 'object') (record).queryType = undefined
}

/** 下划线命名（Snake Case）：小写字母、数字、下划线，如 column_1、user_name */
const SNAKE_CASE_REGEX = /^[a-z][a-z0-9]*(_[a-z0-9]+)*$/

function isSnakeCase(s: string | undefined): boolean {
  if (s == null || String(s).trim() === '') return true
  return SNAKE_CASE_REGEX.test(String(s).trim())
}

/** 大驼峰命名（Pascal Case）：首字母大写，其余字母或数字，如 Column1、UserName、Takt */
const PASCAL_CASE_REGEX = /^[A-Z][a-zA-Z0-9]*$/

function isPascalCase(s: string | undefined): boolean {
  if (s == null || String(s).trim() === '') return true
  return PASCAL_CASE_REGEX.test(String(s).trim())
}

/** 通用必填规则（除 其他选项(JSON) 外所有字段非空） */
const ruleRequired = (message: string) => [{ required: true, message }]

/** 数据表名：必填 + 小写下划线格式（xxxx_xxxx_xxx） */
const tableNameRules = [
  { required: true, message: '请选择或输入数据表名' },
  {
    validator: (_rule: unknown, v: string) =>
      !v || isSnakeCase(v)
        ? Promise.resolve()
        : Promise.reject(new Error('数据表名须为小写下划线格式，如：xxx_xxx_xxx'))
  }
]

/** 命名空间前缀校验：必填 + 帕斯卡命名 */
const namePrefixPascalRules = [
  { required: true, message: '请填写命名空间前缀' },
  {
    validator: (_rule: unknown, v: string) =>
      !v || isPascalCase(v) ? Promise.resolve() : Promise.reject(new Error('须为帕斯卡命名，如 Takt（首字母大写，仅字母数字）'))
  }
]

/** 根据 genTemplate 决定：主子表时 subTableName、subTableFkName 必填 */
const subTableNameRules = computed(() =>
  formState.value.genTemplate === 'sub' ? [{ required: true, message: '请选择关联父表' }] : []
)
const subTableFkNameRules = computed(() =>
  formState.value.genTemplate === 'sub' ? [{ required: true, message: '请选择关联外键' }] : []
)
/** 根据 genTemplate 决定：树表时 treeCode、treeName、treeParentCode 必填 */
const treeCodeRules = computed(() =>
  formState.value.genTemplate === 'tree' ? [{ required: true, message: '请选择树编码字段' }] : []
)
const treeNameRules = computed(() =>
  formState.value.genTemplate === 'tree' ? [{ required: true, message: '请选择树名称字段' }] : []
)
const treeParentCodeRules = computed(() =>
  formState.value.genTemplate === 'tree' ? [{ required: true, message: '请选择树父编码' }] : []
)
/** 生成方式：选中「自定义路径」(1) 时生成路径必填，zip(0)、当前项目(2) 时可空 */
const genPathRules = computed(() =>
  (Number(formState.value.genMethod) === 1) ? [{ required: true, message: '请填写生成路径' }] : []
)
/** 是否生成菜单：根据当前实际——选「是」(0) 时上级菜单必填，选「否」(1) 时可空 */
const parentMenuIdRules = computed(() =>
  Number(formState.value.isGenMenu) === 0 ? [{ required: true, message: '请选择上级菜单' }] : []
)
/** 是否生成仓储：根据当前实际——选「是」(0) 时仓储相关字段必填 */
const repositoryInterfaceNamespaceRules = computed(() =>
  Number(formState.value.isRepository) === 0 ? [{ required: true, message: '请填写仓储接口命名空间' }] : []
)
const iRepositoryClassNameRules = computed(() =>
  Number(formState.value.isRepository) === 0 ? [{ required: true, message: '请填写仓储接口类名' }] : []
)
const repositoryNamespaceRules = computed(() =>
  Number(formState.value.isRepository) === 0 ? [{ required: true, message: '请填写仓储命名空间' }] : []
)
const repositoryClassNameRules = computed(() =>
  Number(formState.value.isRepository) === 0 ? [{ required: true, message: '请填写仓储类名' }] : []
)
/** 是否使用 Tabs：根据当前实际——选「是」(0) 时 Tabs 字段数必填 */
const tabsFieldCountRules = computed(() =>
  Number(formState.value.isUseTabs) === 0 ? [{ required: true, message: '请填写 Tabs 字段数' }] : []
)

function addColumnRow() {
  const tidRaw = formState.value.tableId
  const tableIdNum =
    tidRaw != null && String(tidRaw).trim() !== '' && !Number.isNaN(Number(tidRaw)) ? Number(tidRaw) : undefined
  const nextNum = columnList.value.length + 1
  const defaultDbName = `column_${nextNum}`
  const defaultCsharpName = `Column${nextNum}`
  clientTempColumnSeq += 1
  const newRow: GenTableColumnRow = {
    columnId: -clientTempColumnSeq,
    tableId: tableIdNum,
    databaseColumnName: defaultDbName,
    columnComment: defaultCsharpName,
    databaseDataType: 'nvarchar',
    csharpDataType: 'string',
    csharpColumnName: defaultCsharpName,
    length: DEFAULT_LENGTH_STRING,
    decimalDigits: undefined,
    isPk: 0,
    isIncrement: 0,
    isRequired: 1,
    isCreate: 1,
    isUpdate: 1,
    isUnique: 0,
    isList: 1,
    isExport: 1,
    isSort: 0,
    isQuery: 0,
    queryType: 'LIKE',
    htmlType: 'input',
    dictType: undefined,
    orderNum: columnList.value.length + 1
  }
  columnList.value = [...columnList.value, newRow]
}

function removeColumnRow(record: GenTableColumnRow) {
  const id = (record).columnId
  if (id == null) return
  const nextList = columnList.value.filter(r => r.columnId !== id)
  nextList.forEach((row, index) => {
    row.orderNum = index + 1
  })
  columnList.value = nextList
}

/** 将字段列表的 orderNum 规范为从 1 开始的连续序号（按当前 orderNum 排序后重排） */
function normalizeColumnOrderNum(list: GenTableColumnRow[]) {
  const sorted = [...list].sort((a, b) => Number(a.orderNum ?? 0) - Number(b.orderNum ?? 0))
  sorted.forEach((row, i) => {
    row.orderNum = i + 1
  })
  return sorted
}

/** 拖拽排序：当前拖拽中的行索引 */
const columnDragRowIndex = ref<number | null>(null)

function getColumnRowIndex(record: GenTableColumnRow): number {
  return columnList.value.findIndex(r => r.columnId === record.columnId)
}

function onColumnDragStart(e: DragEvent, record: GenTableColumnRow) {
  const index = getColumnRowIndex(record)
  if (index < 0) return
  columnDragRowIndex.value = index
  e.dataTransfer!.effectAllowed = 'move'
  e.dataTransfer!.setData('text/plain', String(index))
  const tr = (e.target as HTMLElement).closest('tr')
  if (tr) e.dataTransfer!.setDragImage(tr, 0, 0)
}

function onColumnDragOver(e: DragEvent) {
  e.preventDefault()
  e.dataTransfer!.dropEffect = 'move'
}

function onColumnDrop(e: DragEvent, dropRecord: GenTableColumnRow) {
  e.preventDefault()
  const dragIndex = columnDragRowIndex.value
  columnDragRowIndex.value = null
  if (dragIndex == null) return
  const dropIndex = getColumnRowIndex(dropRecord)
  if (dragIndex === dropIndex) return
  const list = [...columnList.value]
  const [removed] = list.splice(dragIndex, 1)
  if (removed == null) return
  list.splice(dropIndex, 0, removed)
  list.forEach((row, i) => {
    row.orderNum = i + 1
  })
  columnList.value = list
}

function columnTableCustomRow(record: GenTableColumnRow, index: number) {
  return {
    class: columnDragRowIndex.value === index ? 'column-row-dragging' : '',
    onDragover: onColumnDragOver,
    onDrop: (e: DragEvent) => onColumnDrop(e, record)
  }
}

const columnTableColumns: TableColumnsType = [
  { title: '', key: 'dragSort', width: 36, align: 'center', class: 'column-drag-cell' },
  { title: '列名', dataIndex: 'databaseColumnName', key: 'databaseColumnName', width: 130, ellipsis: true },
  { title: '描述', dataIndex: 'columnComment', key: 'columnComment', width: 100 },
  { title: 'DB类型', dataIndex: 'databaseDataType', key: 'databaseDataType', width: 88 },
  { title: 'C#类型', dataIndex: 'csharpDataType', key: 'csharpDataType', width: 88 },
  { title: 'C#列名', dataIndex: 'csharpColumnName', key: 'csharpColumnName', width: 110 },
  { title: '长度', dataIndex: 'length', key: 'length', width: 64 },
  { title: '精度', dataIndex: 'decimalDigits', key: 'decimalDigits', width: 64 },
  { title: '主键', dataIndex: 'isPk', key: 'isPk', width: 64 },
  { title: '自增', dataIndex: 'isIncrement', key: 'isIncrement', width: 64 },
  { title: '必填', dataIndex: 'isRequired', key: 'isRequired', width: 64 },
  { title: '查询', dataIndex: 'isQuery', key: 'isQuery', width: 64 },
  { title: '新增', dataIndex: 'isCreate', key: 'isCreate', width: 64 },
  { title: '更新', dataIndex: 'isUpdate', key: 'isUpdate', width: 64 },
  { title: '查重', dataIndex: 'isUnique', key: 'isUnique', width: 64 },
  { title: '列表', dataIndex: 'isList', key: 'isList', width: 64 },
  { title: '导出', dataIndex: 'isExport', key: 'isExport', width: 64 },
  { title: '排序', dataIndex: 'isSort', key: 'isSort', width: 64 },
  { title: '查询方式', dataIndex: 'queryType', key: 'queryType', width: 88 },
  { title: '显示类型', dataIndex: 'htmlType', key: 'htmlType', width: 88 },
  { title: '字典', dataIndex: 'dictType', key: 'dictType', width: 95 },
  { title: '序号', dataIndex: 'orderNum', key: 'orderNum', width: 72 },
  { title: '操作', key: 'action', width: 72, fixed: 'right' }
]

function defaultFormState(): GenFormState {
  return {
    tableId: undefined,
    configId: undefined,
    dataSource: undefined,
    tableName: undefined,
    tableComment: undefined,
    subTableName: undefined,
    subTableFkName: undefined,
    treeCode: undefined,
    treeParentCode: undefined,
    treeName: undefined,
    inDatabase: 1,
    genTemplate: 'crud',
    namePrefix: 'Takt',
    entityNamespace: 'Takt.Domain.Entities',
    entityClassName: undefined,
    dtoNamespace: 'Takt.Application.Dtos',
    dtoClassName: undefined,
    dtoCategory: 'Dto,QueryDto,CreateDto,UpdateDto,TemplateDto,ImportDto,ExportDto',
    serviceNamespace: 'Takt.Application.Services',
    iServiceClassName: undefined,
    serviceClassName: undefined,
    controllerNamespace: 'Takt.WebApi.Controllers',
    controllerClassName: undefined,
    repositoryInterfaceNamespace: 'Takt.Domain.Repositories',
    iRepositoryClassName: undefined,
    repositoryNamespace: 'Takt.Infrastructure.Repositories',
    repositoryClassName: undefined,
    genModuleName: undefined,
    genBusinessName: undefined,
    genFunctionName: undefined,
    genFunction: '查询,新增,更新,删除,模板,导入,导出',
    genMethod: 1,
    isRepository: 1,
    genPath: '/',
    parentMenuId: undefined,
    isGenMenu: 0,
    isGenTranslation: 0,
    sortType: 'asc',
    sortField: undefined,
    permsPrefix: undefined,
    frontTemplate: 2,
    frontStyle: 24,
    btnStyle: 1,
    isUseTabs: 0,
    tabsFieldCount: 10,
    genAuthor: undefined,
    options: undefined,
    columns: undefined
  }
}

async function loadColumns(tableId: string) {
  columnLoading.value = true
  try {
    const list = await getColumnsByTableId(tableId)
    const rows = (list ?? []) as GenTableColumnRow[]
    columnList.value = normalizeColumnOrderNum(rows)
  } catch {
    columnList.value = []
  } finally {
    columnLoading.value = false
  }
}

/** genTemplate 与 sub/tree 字段相斥：切换模板时清空另一类字段 */
watch(
  () => formState.value.genTemplate,
  (next, prev) => {
    if (prev === undefined) return
    if (next !== 'sub') {
      formState.value.subTableName = undefined
      formState.value.subTableFkName = undefined
    }
    if (next !== 'tree') {
      formState.value.treeCode = undefined
      formState.value.treeParentCode = undefined
      formState.value.treeName = undefined
    }
  }
)

/** isRepository 与仓储相关字段相斥：选「否」(1) 时清空仓储命名空间/类名 */
watch(
  () => formState.value.isRepository,
  (next) => {
    if (next === 1) {
      formState.value.repositoryInterfaceNamespace = undefined
      formState.value.iRepositoryClassName = undefined
      formState.value.repositoryNamespace = undefined
      formState.value.repositoryClassName = undefined
    }
  }
)

/** isGenMenu 与上级菜单相斥：选「否」(1) 时清空上级菜单 */
watch(
  () => formState.value.isGenMenu,
  (next) => {
    if (next === 1) formState.value.parentMenuId = undefined
  }
)

/**
 * 将用户输入的「模块名称」转为命名空间后缀（帕斯卡，支持多段如 HumanResource.Organization）。
 */
function toNamespaceSuffix(val: string | null | undefined): string {
  if (val == null || String(val).trim() === '') return ''
  const raw = String(val).trim()
  const parts = raw.split(/[.\s_-]+/).filter(Boolean)
  return parts
    .map(p => (p.length > 0 ? p.charAt(0).toUpperCase() + p.slice(1).toLowerCase() : p))
    .join('.')
}

/** 根据数据表名生成业务名：整表名按帕斯卡命名法（下划线分段，每段首字母大写后拼接）；若首段为 takt 则去掉避免实体类名重复 Takt 前缀 */
function tableNameToBusinessName(tableName: string | undefined): string {
  if (tableName == null || String(tableName).trim() === '') return ''
  let parts = String(tableName).trim().split('_').filter(Boolean)
  if (parts.length > 1 && parts[0]?.toLowerCase() === 'takt') parts = parts.slice(1)
  return parts.map(p => (p.length > 0 ? p.charAt(0).toUpperCase() + p.slice(1).toLowerCase() : p)).join('')
}

/** 模块名转权限段：Accounting.Controlling -> accounting:controlling（小写、点改冒号） */
function moduleNameToPermsSegment(genModuleName: string | null | undefined): string {
  if (genModuleName == null || String(genModuleName).trim() === '') return ''
  return String(genModuleName)
    .trim()
    .split('.')
    .map(s => s.trim().toLowerCase())
    .filter(Boolean)
    .join(':')
}

/** 业务名（PascalCase）转权限段：StandardWageRate -> standard:wage:rate（按大写拆分后小写、冒号连接） */
function businessNameToPermsSegment(genBusinessName: string | null | undefined): string {
  if (genBusinessName == null || String(genBusinessName).trim() === '') return ''
  const s = String(genBusinessName).trim()
  if (s.length === 0) return ''
  const parts: string[] = []
  let current = ''
  for (let i = 0; i < s.length; i++) {
    const c = s.charAt(i)
    if (c >= 'A' && c <= 'Z' && current.length > 0) {
      parts.push(current.toLowerCase())
      current = c
    } else {
      current += c
    }
  }
  if (current.length > 0) parts.push(current.toLowerCase())
  return parts.join(':')
}

/** 权限前缀自动生成：由 genModuleName + genBusinessName 拼接，如 accounting:controlling:standard:wage:rate */
function applyPermsPrefix() {
  const modulePart = moduleNameToPermsSegment(formState.value.genModuleName)
  const businessPart = businessNameToPermsSegment(formState.value.genBusinessName)
  const parts = [modulePart, businessPart].filter(Boolean)
  formState.value.permsPrefix = parts.length > 0 ? parts.join(':') : undefined
}

/** DDD 业务类型（命名空间中间段）：前缀 + 本段 + 后缀 = 完整命名空间 */
const DDD_NAMESPACE_SEGMENTS: Record<string, string> = {
  entityNamespace: 'Domain.Entities',
  dtoNamespace: 'Application.Dtos',
  serviceNamespace: 'Application.Services',
  controllerNamespace: 'WebApi.Controllers',
  repositoryNamespace: 'Infrastructure.Repositories',
  repositoryInterfaceNamespace: 'Domain.Repositories'
}

/**
 * 按「前缀（命名前缀）+ DDD业务类型 + 后缀（模块名称）」正拼接并更新所有命名空间。
 * 前缀为空时使用 Takt；有模块名称时追加 .模块后缀，无则仅 前缀.DDD类型。
 */
function applyNamespacesFromPrefixAndModule() {
  const prefix = (formState.value.namePrefix ?? '').trim() || 'Takt'
  const raw = (formState.value.genModuleName ?? '').trim()
  const moduleSuffix = /^\d+$/.test(raw) ? '' : toNamespaceSuffix(formState.value.genModuleName)
  const suffixPart = moduleSuffix ? `.${moduleSuffix}` : ''

  formState.value.entityNamespace = `${prefix}.${DDD_NAMESPACE_SEGMENTS.entityNamespace}${suffixPart}`
  formState.value.dtoNamespace = `${prefix}.${DDD_NAMESPACE_SEGMENTS.dtoNamespace}${suffixPart}`
  formState.value.serviceNamespace = `${prefix}.${DDD_NAMESPACE_SEGMENTS.serviceNamespace}${suffixPart}`
  formState.value.controllerNamespace = `${prefix}.${DDD_NAMESPACE_SEGMENTS.controllerNamespace}${suffixPart}`
  formState.value.repositoryNamespace = `${prefix}.${DDD_NAMESPACE_SEGMENTS.repositoryNamespace}${suffixPart}`
  formState.value.repositoryInterfaceNamespace = `${prefix}.${DDD_NAMESPACE_SEGMENTS.repositoryInterfaceNamespace}${suffixPart}`
}

/** 模块名称（后缀）变更时，重新驱动命名空间与权限前缀 */
watch(
  () => formState.value.genModuleName,
  () => {
    applyNamespacesFromPrefixAndModule()
    applyPermsPrefix()
  }
)

/** 命名前缀变更时，按 前缀+DDD类型+后缀 重新驱动命名空间 */
watch(
  () => formState.value.namePrefix,
  () => applyNamespacesFromPrefixAndModule()
)

/** 数据表名变更时，仅「新建」时根据表名生成业务名（编辑时保留后端返回的 genBusinessName，避免被覆盖）；并刷新权限前缀 */
watch(
  () => formState.value.tableName,
  (tableName) => {
    const tableId = readGenTablePrimaryId(formState.value)
    if (!tableId) {
      formState.value.genBusinessName = tableName ? tableNameToBusinessName(tableName) : undefined
    }
    applyPermsPrefix()
  }
)

/** 业务名称变更时，同步更新所有类名与权限前缀 */
watch(
  () => formState.value.genBusinessName,
  (businessName) => {
    const base = businessName != null ? String(businessName).trim() : ''
    if (base !== '') {
      const entityName = `Takt${base}`
      formState.value.entityClassName = entityName
      formState.value.dtoClassName = `${entityName}Dto`
      formState.value.iServiceClassName = `I${entityName}Service`
      formState.value.serviceClassName = `${entityName}Service`
      formState.value.controllerClassName = `${entityName}Controller`
      formState.value.iRepositoryClassName = `I${entityName}Repository`
      formState.value.repositoryClassName = `${entityName}Repository`
    }
    applyPermsPrefix()
  }
)

/** 生成方式与生成路径相斥：选「zip 压缩包」(0) 时清空生成路径 */
watch(
  () => formState.value.genMethod,
  (next) => {
    if (next === 0) formState.value.genPath = undefined
  }
)

/** 是否使用 Tabs 与 Tabs 字段数相斥：选「否」(1) 时清空 Tabs 字段数 */
watch(
  () => formState.value.isUseTabs,
  (next) => {
    if (next === 1) formState.value.tabsFieldCount = undefined
  }
)

/** 功能名由表描述驱动，与表描述一致（只读） */
watch(
  () => formState.value.tableComment,
  (tableComment) => {
    formState.value.genFunctionName = tableComment != null ? String(tableComment).trim() || undefined : undefined
  }
)

watch(
  () => props.formData,
  (val) => {
    if (val) {
      const tableId = readGenTablePrimaryId(val)
      formState.value = { ...defaultFormState(), ...(val as Record<string, unknown>) } as GenFormState
      if (tableId) formState.value.tableId = String(tableId)
      /** 展开赋值时若显式带上 tabsFieldCount: undefined 会盖掉默认值；与 defaultFormState（10）及「使用 Tabs」展示逻辑一致 */
      if (Number(formState.value.isUseTabs) === 0 && formState.value.tabsFieldCount == null) {
        formState.value.tabsFieldCount = 10
      }
      formState.value.genFunctionName = formState.value.tableComment != null ? String(formState.value.tableComment).trim() || undefined : undefined
      if (!formState.value.genAuthor) formState.value.genAuthor = userStore.userInfo?.realName || userStore.userInfo?.userName || ''
      applyNamespacesFromPrefixAndModule()
      applyPermsPrefix()
      const configs = props.databaseConfigs ?? []
      const valConfigId = parseSelectToOptionalString(val.configId)
      if (valConfigId) {
        emit('config-change', valConfigId)
      } else if (val.dataSource && configs.length > 0) {
        const parts = (val.dataSource as string).split(':')
        const configIdFromSource = parts.length > 1 ? parts[parts.length - 1] : ''
        const matched = configs.find(
          c => c.configId === configIdFromSource || val.dataSource === `${c.displayName}:${c.configId}`
        )
        if (matched) {
          formState.value.configId = matched.configId
          emit('config-change', matched.configId)
        }
      }

      if (val.columns != null && Array.isArray(val.columns) && val.columns.length > 0) {
        columnList.value = normalizeColumnOrderNum(val.columns as GenTableColumnRow[])
      } else if (tableId) {
        loadColumns(String(tableId))
      } else {
        columnList.value = []
      }
    } else {
      formState.value = defaultFormState()
      formState.value.genPath = defaultGenPath.value || '/'
      formState.value.genAuthor = userStore.userInfo?.realName || userStore.userInfo?.userName || ''
      const dtoOpts = dictDataStore.getDictOptions('sys_dto_category')
      const genOpts = dictDataStore.getDictOptions('sys_gen_function')
      if (dtoOpts.length > 0) formState.value.dtoCategory = dtoOpts.map(o => String(o.value)).join(',')
      if (genOpts.length > 0) formState.value.genFunction = genOpts.map(o => String(o.value)).join(',')
      dtoCategoryCheckAll.value = true
      dtoCategoryIndeterminate.value = false
      genFunctionCheckAll.value = true
      genFunctionIndeterminate.value = false
      columnList.value = []
    }
  },
  { immediate: true }
)


/** 校验字段配置表格：列名须为下划线命名法，C#列名须为大驼峰命名法 */
function validateColumnListNaming(): void {
  const list = columnList.value
  for (let i = 0; i < list.length; i++) {
    const row = list[i]
    if (!row) continue
    const rowNum = i + 1
    const colName = row.databaseColumnName
    const csharpName = row.csharpColumnName
    if (colName != null && String(colName).trim() !== '' && !isSnakeCase(String(colName))) {
      throw new Error(`字段配置第 ${rowNum} 行：列名须为下划线命名法（如 column_1、user_name），当前为「${colName}」`)
    }
    if (csharpName != null && String(csharpName).trim() !== '' && !isPascalCase(String(csharpName))) {
      throw new Error(`字段配置第 ${rowNum} 行：C#列名须为大驼峰命名法（如 Column1、UserName），当前为「${csharpName}」`)
    }
  }
}

async function doValidate() {
  await formRef.value?.validate()
  validateColumnListNaming()
}

function getValues(): GenFormState {
  const rows = normalizeColumnOrderNum(columnList.value)
  // 只判断两个表 ID：主表有 tableId = 更新，无 = 创建；列有 columnId 且不为 0 = 已有列
  const isTableUpdate = !!formState.value.tableId
  const columns = rows.map(col => {
    const rawColId = col.columnId
    const id = rawColId != null ? String(rawColId) : ''
    const n = Number(id)
    /** 后端正 id 为正整数；本地临时行为负整数，一律按新列提交 */
    const hasPersistedColumnId = id !== '' && id !== '0' && Number.isFinite(n) && n > 0
    // 创建表：columnId 传数字 0；更新表：已有列传字符串 id，新列传 "0"
    const columnId = isTableUpdate ? (hasPersistedColumnId ? id : '0') : 0
    // 创建表时列没有 tableId，后端 CreateAsync 会设；传 "0" 避免空字符串导致 ValueToStringConverter 反序列化 400
    const tableId = isTableUpdate ? (col.tableId != null ? String(col.tableId) : '0') : '0'
    const row = { ...col, columnId, tableId } as GenTableColumnRow & { columnId: number | string; tableId: string }
    return row
  })
  const raw = { ...formState.value, columns } as GenFormState
  const gm = Number(raw.genMethod ?? 0)
  raw.genMethod = Number.isFinite(gm) ? gm : 0
  /** OpenAPI 为 int64 number；无上级菜单时传 0，与后端 long 反序列化约定一致 */
  if (raw.parentMenuId != null && String(raw.parentMenuId).trim() !== '') {
    const pm = Number(raw.parentMenuId)
    raw.parentMenuId = Number.isFinite(pm) ? pm : 0
  } else {
    raw.parentMenuId = 0
  }
  return raw
}

/** 整个组件重置：表单、字段列表、Tab、全选状态等全部恢复初始，供取消/提交后由父组件调用 */
function reset() {
  formState.value = defaultFormState()
  formState.value.genPath = defaultGenPath.value || '/'
  formState.value.genAuthor = userStore.userInfo?.realName || userStore.userInfo?.userName || ''
  const dtoOpts = dictDataStore.getDictOptions('sys_dto_category')
  const genOpts = dictDataStore.getDictOptions('sys_gen_function')
  if (dtoOpts.length > 0) formState.value.dtoCategory = dtoOpts.map(o => String(o.value)).join(',')
  if (genOpts.length > 0) formState.value.genFunction = genOpts.map(o => String(o.value)).join(',')
  columnList.value = []
  activeTab.value = 'table'
  tableSubTab.value = 'basic'
  columnDragRowIndex.value = null
  dtoCategoryCheckAll.value = true
  dtoCategoryIndeterminate.value = false
  genFunctionCheckAll.value = true
  genFunctionIndeterminate.value = false
  formRef.value?.clearValidate()
}

defineExpose({ validate: doValidate, getValues, reset })
</script>

<style scoped lang="less">
.gen-form-root {
  overflow-x: hidden;
}
:deep(.ant-tabs-card .ant-tabs-content) {
  padding-top: 12px;
}
.column-toolbar {
  margin-bottom: 8px;
}
/* 字段配置表格：横向滚动仅在此容器内，不撑开弹窗 */
.column-table-wrap {
  width: 100%;
  overflow-x: auto;
}
.column-cell-input,
.column-cell-select {
  min-width: 0;
}
.column-cell-muted {
  color: rgba(0, 0, 0, 0.25);
}
/* 拖拽把手列 */
.column-drag-cell {
  padding: 4px 8px !important;
}
.column-drag-handle {
  cursor: move;
  color: rgba(0, 0, 0, 0.35);
  display: inline-flex;
  padding: 2px;
}
.column-row-dragging {
  opacity: 0.6;
  background: var(--ant-color-primary-bg, #e6f4ff);
}
</style>
