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
        :tab="tf('tab.table')"
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
              :tab="tf('tab.basic')"
              force-render
            >
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.datasource')"
                    name="configId"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-select
                      :value="parseSelectToOptionalString(formState.configId)"
                      :placeholder="tf('placeholder.configid')"
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
                    :label="t('entity.gentable.tablename')"
                    name="tableName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="tableNameRules"
                  >
                    <a-input
                      v-if="!isEditMode"
                      :value="formState.tableName ?? ''"
                      :placeholder="tf('placeholder.tablenamenew')"
                      :disabled="!formState.configId"
                      allow-clear
                      @update:value="(v: string) => { formState.tableName = v === '' ? undefined : v }"
                    />
                    <a-select
                      v-else
                      :value="formState.tableName ?? undefined"
                      :placeholder="tf('placeholder.tablenameedit')"
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
                    :label="t('entity.gentable.tablecomment')"
                    name="tableComment"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('tablecomment')"
                  >
                    <a-input
                      :value="formState.tableComment ?? ''"
                      :placeholder="tf('placeholder.tablecomment')"
                      allow-clear
                      @update:value="(v: string) => { formState.tableComment = v === '' ? undefined : v }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.gentemplatecategory')"
                    name="genTemplateCategory"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('gentemplatecategory')"
                  >
                    <TaktSelect
                      :model-value="formState.genTemplateCategory ?? ''"
                      dict-type="gen_template_type"
                      :placeholder="tf('placeholder.gentemplatecategory')"
                      allow-clear
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.genTemplateCategory = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.indatabase')"
                    name="inDatabase"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <TaktSelect
                      :model-value="formState.inDatabase ?? ''"
                      dict-type="sys_yes_no"
                      :placeholder="tf('placeholder.indatabase')"
                      style="width: 100%"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <!-- 主子表：仅当 genTemplateCategory === 'sub' 时显示 -->
              <a-row
                v-if="formState.genTemplateCategory === 'sub'"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.subtablename')"
                    name="subTableName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="subTableNameRules"
                  >
                    <a-select
                      :value="formState.subTableName ?? undefined"
                      :placeholder="tf('placeholder.subtablename')"
                      allow-clear
                      style="width: 100%"
                      :options="subTableNameOptions"
                      @update:value="(v: unknown) => { formState.subTableName = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row
                v-if="formState.genTemplateCategory === 'sub'"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.subtablefkname')"
                    name="subTableFkName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="subTableFkNameRules"
                  >
                    <a-select
                      :value="formState.subTableFkName ?? undefined"
                      :placeholder="tf('placeholder.subtablefkname')"
                      allow-clear
                      style="width: 100%"
                      :options="columnSelectOptions"
                      @update:value="(v: unknown) => { formState.subTableFkName = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <!-- 树表：仅当 genTemplateCategory === 'tree' 时显示 -->
              <a-row
                v-if="formState.genTemplateCategory === 'tree'"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.treecode')"
                    name="treeCode"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="treeCodeRules"
                  >
                    <a-select
                      :value="formState.treeCode ?? undefined"
                      :placeholder="tf('placeholder.treecode')"
                      allow-clear
                      style="width: 100%"
                      :options="columnSelectOptions"
                      @update:value="(v: unknown) => { formState.treeCode = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row
                v-if="formState.genTemplateCategory === 'tree'"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.treeparentcode')"
                    name="treeParentCode"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="treeParentCodeRules"
                  >
                    <a-select
                      :value="formState.treeParentCode ?? undefined"
                      :placeholder="tf('placeholder.treeparentcode')"
                      allow-clear
                      style="width: 100%"
                      :options="columnSelectOptions"
                      @update:value="(v: unknown) => { formState.treeParentCode = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row
                v-if="formState.genTemplateCategory === 'tree'"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.treename')"
                    name="treeName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="treeNameRules"
                  >
                    <a-select
                      :value="formState.treeName ?? undefined"
                      :placeholder="tf('placeholder.treename')"
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
              :tab="tf('tab.business')"
              force-render
            >
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.nameprefix')"
                    name="namePrefix"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="namePrefixPascalRules"
                  >
                    <a-input
                      :value="formState.namePrefix ?? ''"
                      :placeholder="tf('placeholder.nameprefix')"
                      allow-clear
                      @update:value="(v: string) => { formState.namePrefix = v === '' ? undefined : v }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.permsprefix')"
                    name="permsPrefix"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('permsprefix')"
                  >
                    <a-input
                      :value="formState.permsPrefix ?? ''"
                      :placeholder="tf('placeholder.permsprefix')"
                      allow-clear
                      @update:value="(v: string) => { formState.permsPrefix = v === '' ? undefined : v }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.menubuttongroup')"
                    name="menuButtonGroup"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('menubuttongroup')"
                  >
                    <a-form-item-rest>
                      <div>
                        <a-checkbox
                          v-model:checked="menuButtonGroupCheckAll"
                          :indeterminate="menuButtonGroupIndeterminate"
                          @change="onMenuButtonGroupCheckAllChange"
                        >
                          {{ t('common.button.checkall') }}
                        </a-checkbox>
                      </div>
                      <a-divider style="margin: 8px 0" />
                    </a-form-item-rest>
                    <a-checkbox-group
                      v-model:value="menuButtonGroupSelect"
                      :options="menuButtonGroupOptions"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.genmodulename')"
                    name="genModuleName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('genmodulename')"
                  >
                    <TaktTreeSelect
                      :value="formState.genModuleName ?? ''"
                      :tree-data="moduleOptionsTree"
                      :placeholder="tf('placeholder.genmodulename')"
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
                    :label="t('entity.gentable.genbusinessname')"
                    name="genBusinessName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('genbusinessname')"
                  >
                    <a-input
                      :value="formState.genBusinessName ?? ''"
                      :placeholder="
                        formState.inDatabase === 1
                          ? tf('placeholder.genbusinessnamefromtable')
                          : tf('placeholder.genbusinessnamemanual')
                      "
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
                    :label="t('entity.gentable.genfunctionname')"
                    name="genFunctionName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-input
                      :value="formState.genFunctionName ?? ''"
                      :placeholder="tf('placeholder.genfunctionname')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
            </a-tab-pane>
            <a-tab-pane
              key="entity"
              :tab="tf('tab.entitydto')"
              force-render
            >
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.entitynamespace')"
                    name="entityNamespace"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('entitynamespace')"
                  >
                    <a-input
                      :value="formState.entityNamespace ?? ''"
                      :placeholder="tf('placeholder.autofrommodule')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.entityclassname')"
                    name="entityClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('entityclassname')"
                  >
                    <a-input
                      :value="formState.entityClassName ?? ''"
                      :placeholder="tf('placeholder.autofrombusiness')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.dtoclassname')"
                    name="dtoClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('dtoclassname')"
                  >
                    <a-input
                      :value="formState.dtoClassName ?? ''"
                      :placeholder="tf('placeholder.autofrombusiness')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.dtonamespace')"
                    name="dtoNamespace"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-input
                      :value="formState.dtoNamespace ?? ''"
                      :placeholder="tf('placeholder.autofrommodule')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
            </a-tab-pane>
            <a-tab-pane
              key="service"
              :tab="tf('tab.service')"
              force-render
            >
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.servicenamespace')"
                    name="serviceNamespace"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-input
                      :value="formState.serviceNamespace ?? ''"
                      :placeholder="tf('placeholder.autofrommodule')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.iserviceclassname')"
                    name="iServiceClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('iserviceclassname')"
                  >
                    <a-input
                      :value="formState.iServiceClassName ?? ''"
                      :placeholder="tf('placeholder.autofrombusiness')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.serviceclassname')"
                    name="serviceClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('serviceclassname')"
                  >
                    <a-input
                      :value="formState.serviceClassName ?? ''"
                      :placeholder="tf('placeholder.autofrombusiness')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.controllernamespace')"
                    name="controllerNamespace"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('controllernamespace')"
                  >
                    <a-input
                      :value="formState.controllerNamespace ?? ''"
                      :placeholder="tf('placeholder.autofrommodule')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.controllerclassname')"
                    name="controllerClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-input
                      :value="formState.controllerClassName ?? ''"
                      :placeholder="tf('placeholder.autofrombusiness')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.isrepository')"
                    name="isRepository"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('isrepository')"
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
                v-if="formState.isRepository === 1"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.repositoryinterfacenamespace')"
                    name="repositoryInterfaceNamespace"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="repositoryInterfaceNamespaceRules"
                  >
                    <a-input
                      :value="formState.repositoryInterfaceNamespace ?? ''"
                      :placeholder="tf('placeholder.autofrommodule')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row
                v-if="formState.isRepository === 1"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.irepositoryclassname')"
                    name="iRepositoryClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="iRepositoryClassNameRules"
                  >
                    <a-input
                      :value="formState.iRepositoryClassName ?? ''"
                      :placeholder="tf('placeholder.autofrombusiness')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row
                v-if="formState.isRepository === 1"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.repositorynamespace')"
                    name="repositoryNamespace"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="repositoryNamespaceRules"
                  >
                    <a-input
                      :value="formState.repositoryNamespace ?? ''"
                      :placeholder="tf('placeholder.autofrommodule')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row
                v-if="formState.isRepository === 1"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.repositoryclassname')"
                    name="repositoryClassName"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="repositoryClassNameRules"
                  >
                    <a-input
                      :value="formState.repositoryClassName ?? ''"
                      :placeholder="tf('placeholder.autofrombusiness')"
                      disabled
                    />
                  </a-form-item>
                </a-col>
              </a-row>
            </a-tab-pane>
            <a-tab-pane
              key="generate"
              :tab="tf('tab.generate')"
              force-render
            >
              <a-row :gutter="24">
                <a-col :span="24">
                  <!-- 生成功能：仅收集 a-checkbox-group；全选与分隔线放入 a-form-item-rest 避免 Form.Item 收集多个控件 -->
                  <a-form-item
                    :label="t('entity.gentable.genfunction')"
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
                          {{ t('common.button.checkall') }}
                        </a-checkbox>
                      </div>
                      <a-divider style="margin: 8px 0" />
                    </a-form-item-rest>
                    <a-checkbox-group
                      v-model:value="genFunctionSelect"
                      :options="filteredGenFunctionOptions"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.genmethod')"
                    name="genMethod"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('genmethod')"
                  >
                    <TaktSelect
                      :model-value="formState.genMethod ?? ''"
                      dict-type="gen_method"
                      :placeholder="tf('placeholder.genmethod')"
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
                    :label="t('entity.gentable.genpath')"
                    name="genPath"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="genPathRules"
                  >
                    <a-input
                      :value="formState.genPath ?? ''"
                      :placeholder="tf('placeholder.genpath')"
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
                    :label="tf('label.currentprojectpath')"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-input
                      :value="currentProjectPathDisplay"
                      readonly
                      :placeholder="
                        currentProjectPathLoading
                          ? tf('placeholder.currentprojectloading')
                          : tf('placeholder.currentprojectidle')
                      "
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
                    :label="t('entity.gentable.isgenmenu')"
                    name="isGenMenu"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <TaktSelect
                      :model-value="formState.isGenMenu ?? ''"
                      dict-type="sys_yes_no"
                      :placeholder="t('common.form.placeholder.selectonly')"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.isGenMenu = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <!-- 上级菜单：仅当「是否生成菜单」为「是」(1) 时显示，与「否」相斥 -->
              <a-row
                v-if="formState.isGenMenu == 1"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.parentmenuid')"
                    name="parentMenuId"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="parentMenuIdRules"
                  >
                    <TaktTreeSelect
                      :value="formState.parentMenuId ?? ''"
                      :tree-data="parentMenuOptionsTree"
                      :placeholder="tf('placeholder.parentmenuid')"
                      allow-clear
                      style="width: 100%"
                      :field-names="{ label: 'dictLabel', value: 'dictValue' }"
                      @update:value="(v: unknown) => { formState.parentMenuId = parseTreeSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.isgentranslation')"
                    name="isGenTranslation"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('isgentranslation')"
                  >
                    <TaktSelect
                      :model-value="formState.isGenTranslation ?? ''"
                      dict-type="sys_yes_no"
                      :placeholder="t('common.form.placeholder.selectonly')"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.isGenTranslation = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.sortfield')"
                    name="sortField"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('sortfield')"
                  >
                    <a-select
                      :value="formState.sortField ?? undefined"
                      :placeholder="tf('placeholder.sortfield')"
                      allow-clear
                      style="width: 100%"
                      :options="columnSelectOptions"
                      @update:value="(v: unknown) => { formState.sortField = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.sorttype')"
                    name="sortType"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('sorttype')"
                  >
                    <TaktSelect
                      :model-value="formState.sortType ?? ''"
                      dict-type="sys_sort_type"
                      :placeholder="tf('placeholder.sorttype')"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.sortType = parseSelectToOptionalString(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
            </a-tab-pane>
            <a-tab-pane
              key="front"
              :tab="tf('tab.front')"
              force-render
            >
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.frontui')"
                    name="frontUi"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <TaktSelect
                      :model-value="formState.frontUi ?? ''"
                      dict-type="gen_frontend_ui"
                      :placeholder="tf('placeholder.frontui')"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.frontUi = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.frontformlayout')"
                    name="frontFormLayout"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('frontformlayout')"
                  >
                    <TaktSelect
                      :model-value="formState.frontFormLayout ?? ''"
                      dict-type="gen_frontend_form_layout"
                      :placeholder="tf('placeholder.frontformlayout')"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.frontFormLayout = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.frontbtnstyle')"
                    name="frontBtnStyle"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <TaktSelect
                      :model-value="formState.frontBtnStyle ?? ''"
                      dict-type="gen_frontend_btn_style"
                      :placeholder="tf('placeholder.frontbtnstyle')"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.frontBtnStyle = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.isusetabs')"
                    name="isUseTabs"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('isusetabs')"
                  >
                    <TaktSelect
                      :model-value="formState.isUseTabs ?? ''"
                      dict-type="sys_yes_no"
                      :placeholder="t('common.form.placeholder.selectonly')"
                      style="width: 100%"
                      @update:model-value="(v: unknown) => { formState.isUseTabs = parseSelectToOptionalNumber(v) }"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <!-- Tabs 字段数：仅当「是否使用 Tabs」为「是」(0) 时显示，与「否」相斥 -->
              <a-row
                v-if="formState.isUseTabs == 1"
                :gutter="24"
              >
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.tabsfieldcount')"
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
                    :label="t('entity.gentable.genauthor')"
                    name="genAuthor"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                    :rules="rq('genauthor')"
                  >
                    <a-input
                      :value="formState.genAuthor ?? ''"
                      disabled
                      :placeholder="tf('placeholder.genauthor')"
                    />
                  </a-form-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :span="24">
                  <a-form-item
                    :label="t('entity.gentable.othergenoptions')"
                    name="otherGenOptions"
                    :label-col="{ span: 3 }"
                    :wrapper-col="{ span: 0 }"
                  >
                    <a-textarea
                      :value="formState.otherGenOptions ?? ''"
                      :rows="4"
                      allow-clear
                      @update:value="(v: string) => { formState.otherGenOptions = v === '' ? undefined : v }"
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
        :tab="tf('tab.column')"
        force-render
      >
        <div class="column-toolbar">
          <a-button
            type="primary"
            size="small"
            @click="addColumnRow"
          >
            {{ t('common.button.createrow') }}
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
                  :placeholder="tf('placeholder.columndbtype')"
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
                  :placeholder="tf('placeholder.columncsharptype')"
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
                  :checked-children="t('common.button.yes')"
                  :un-checked-children="t('common.button.no')"
                  @change="(checked: unknown) => {
                    const key = String(column.key)
                    const isOn = checked === true || checked === 1 || checked === '1'
                    record[key] = isOn ? 1 : 0
                    if (key === 'isQuery' && !isOn) onColumnIsQueryChange(record as GenTableColumnRow)
                  }"
                />
              </template>
              <!-- 查询方式：仅当「是否查询」为是时显示，字典 gen_query_type -->
              <template v-else-if="column.key === 'queryType'">
                <TaktSelect
                  v-if="record.isQuery === 1"
                  :model-value="record.queryType ?? undefined"
                  dict-type="gen_query_type"
                  :placeholder="tf('placeholder.columnquerytype')"
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
              <!-- 显示类型：字典 gen_display_type（下拉框/复选框/单选框时需配合字典列绑定选项） -->
              <template v-else-if="column.key === 'htmlType'">
                <TaktSelect
                  :model-value="record.htmlType ?? undefined"
                  dict-type="gen_display_type"
                  :placeholder="tf('placeholder.columnhtmltype')"
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
                  :placeholder="tf('placeholder.columndicttype')"
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
                  {{ t('common.button.delete') }}
                </a-button>
              </template>
            </template>
            <template #emptyText>
              <a-empty
                v-if="!formData?.tableId"
                :description="tf('column.emptysavefirst')"
              />
              <a-empty
                v-else
                :description="tf('column.emptynodata')"
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
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import type { GenTable } from '@/types/code/generator/gen-table'
import { getColumnsByTableId } from '@/api/code/generator/table-column'
import { getDefaultGenPath } from '@/api/code/generator/table'
import type { DatabaseConfig, DatabaseTableInfo } from '@/api/code/generator/table'
import { getDictTypeOptions } from '@/api/routine/tasks/dict/dicttype'
import type { TaktSelectOption, TaktTreeSelectOption } from '@/types/common'
import TaktSelect from '@/components/business/takt-select/index.vue'
import TaktTreeSelect from '@/components/business/takt-tree-select/index.vue'
import { getMenuTreeOptions, getModuleNameOptions } from '@/api/identity/menu'
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
  genTemplateCategory: string | undefined
  namePrefix: string | undefined
  entityNamespace: string | undefined
  entityClassName: string | undefined
  dtoNamespace: string | undefined
  dtoClassName: string | undefined
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
  parentMenuId: string | undefined
  isGenMenu: number | undefined
  isGenTranslation: number | undefined
  sortType: string | undefined
  sortField: string | undefined
  permsPrefix: string | undefined
  menuButtonGroup: string | undefined
  frontUi: number | undefined
  frontFormLayout: number | undefined
  frontBtnStyle: number | undefined
  isUseTabs: number | undefined
  tabsFieldCount: number | undefined
  genAuthor: string | undefined
  otherGenOptions: string | undefined
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

const { t } = useI18n()
const FORM = 'code.generator.page.form'
function rq(ruleKey: string) {
  return [{ required: true, message: t(`${FORM}.rules.${ruleKey}`) }]
}

function tf(suffix: string) {
  return t(`${FORM}.${suffix}`)
}

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
/** 上级菜单选项（来自 GET /api/TaktMenus/tree-options，后端已排除按钮 MenuType=2） */
const parentMenuOptionsTree = ref<TaktTreeSelectOption[]>([])
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
  if (typeof v === 'object' && 'value' in v) {
    return parseTreeSelectToOptionalString((v as { value: unknown }).value)
  }
  if (typeof v === 'string' || typeof v === 'number') return String(v)
  return undefined
}

/** 菜单权限组：选项与选中值仅用于 formState.menuButtonGroup，与生成功能同为字典多选 */
const menuButtonGroupOptions = computed(() => dictDataStore.getDictOptions('gen_button_category'))
/** 生成功能：选项与选中值仅用于 formState.genFunction */
const genFunctionOptions = computed(() => dictDataStore.getDictOptions('gen_function'))

/** 检测列中是否有 Status 字段（不区分大小写） */
const hasStatusColumn = computed(() => {
  return columnList.value.some(col => {
    const name = (col.csharpColumnName ?? col.databaseColumnName ?? '').toLowerCase()
    return name === 'status'
  })
})

/** 检测列中是否有 OrderNum/SORT 字段（不区分大小写） */
const hasSortColumn = computed(() => {
  return columnList.value.some(col => {
    const name = (col.csharpColumnName ?? col.databaseColumnName ?? '').toLowerCase()
    return name === 'ordernum' || name === 'sort'
  })
})

/** 生成功能选项（根据列字段动态禁用 Status 和 Sort） */
const filteredGenFunctionOptions = computed(() => {
  const opts = genFunctionOptions.value
  return opts.map(opt => {
    const value = String(opt.value)
    // 如果没有 Status 字段，禁用 Status 功能
    if (value === 'Status' && !hasStatusColumn.value) {
      return { ...opt, disabled: true }
    }
    // 如果没有 Sort 字段，禁用 Sort 功能
    if (value === 'Sort' && !hasSortColumn.value) {
      return { ...opt, disabled: true }
    }
    return opt
  })
})

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

/** 菜单权限组多选：仅与 formState.menuButtonGroup 双向同步，选中值为字典 gen_button_category 的 value */
const menuButtonGroupSelect = computed({
  get() {
    const s = formState.value.menuButtonGroup
    if (!s || typeof s !== 'string') return []
    return s.split(/[,，\s]+/).map((x: string) => x.trim()).filter(Boolean)
  },
  set(v: (string | number)[] | undefined) {
    const arr = Array.isArray(v) ? v.map(x => String(x)).filter(Boolean) : []
    formState.value.menuButtonGroup = arr.length ? arr.join(',') : undefined
  }
})

/** 生成功能多选：仅与 formState.genFunction 双向同步，供「生成功能」a-checkbox-group 使用，选中值为字典 gen_function 的 value */
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

/** 菜单权限组全选：全选勾选态与半选态；默认全选 */
const menuButtonGroupCheckAll = ref(true)
const menuButtonGroupIndeterminate = ref(false)
function onMenuButtonGroupCheckAllChange(e: { target: { checked: boolean } }) {
  const checked = e.target.checked
  const opts = menuButtonGroupOptions.value
  formState.value.menuButtonGroup = checked && opts.length ? opts.map(o => String(o.value)).join(',') : undefined
  menuButtonGroupIndeterminate.value = false
  menuButtonGroupCheckAll.value = checked
}
watch(
  [() => formState.value.menuButtonGroup, menuButtonGroupOptions],
  () => {
    const list = (formState.value.menuButtonGroup ?? '')
      ? (formState.value.menuButtonGroup as string).split(/[,，\s]+/).map((x: string) => x.trim()).filter(Boolean)
      : []
    const total = menuButtonGroupOptions.value.length
    menuButtonGroupIndeterminate.value = list.length > 0 && list.length < total
    menuButtonGroupCheckAll.value = total > 0 && list.length === total
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
    const genOpts = dictDataStore.getDictOptions('gen_function')
    const btnOpts = dictDataStore.getDictOptions('gen_button_category')
    if (genOpts.length > 0) formState.value.genFunction = genOpts.map(o => String(o.value)).join(',')
    if (btnOpts.length > 0) formState.value.menuButtonGroup = btnOpts.map(o => String(o.value)).join(',')
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
  try {
    const rawMenuTree = await getMenuTreeOptions()
    parentMenuOptionsTree.value = Array.isArray(rawMenuTree) ? rawMenuTree : []
  } catch {
    parentMenuOptionsTree.value = []
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

/** OpenAPI/shim 字段类型较宽，收窄为可赋给表单 string 的安全字符串 */
function asTrimmedString(v: unknown): string {
  if (v == null) return ''
  if (typeof v === 'string') return v.trim()
  if (typeof v === 'number' || typeof v === 'boolean') return String(v)
  return ''
}

function optionalStringParam(v: unknown): string | undefined {
  const s = asTrimmedString(v)
  return s === '' ? undefined : s
}

/**
 * 模块名/菜单树下拉 API 实际返回字段（自动生成 `MenuTree` 目前仅含 children，与 TaktMenuTreeDto 不一致处在此补全）。
 */
type MenuTreeWithSelectExtras = {
  menuId?: string
  menuName?: string
  menuCode?: string
  path?: string
  orderNum?: number
  dictValue?: string | number
  dictLabel?: string | number
  children?: MenuTree[]
}

/** 将模块名称选项 API 返回的菜单树转为 TaktTreeSelectOption（dictLabel=menuName, dictValue=由 path 还原的模块名，保证非空；字段均为小驼峰） */
function mapMenuTreeToTreeSelectOption(trees: MenuTree[]): TaktTreeSelectOption[] {
  return trees.map((node): TaktTreeSelectOption => {
    const n = node as MenuTreeWithSelectExtras
    const path = n.path ?? undefined
    const menuCode = n.menuCode ?? undefined
    const dictVal = n.dictValue
    const idFallback = n.menuId != null ? String(n.menuId) : ''
    const moduleName =
      pathToModuleName(optionalStringParam(path)) ||
      menuCodeToModuleName(optionalStringParam(menuCode)) ||
      (dictVal != null && dictVal !== '' ? String(dictVal) : '') ||
      idFallback
    return {
      dictLabel: n.menuName ?? (n.dictLabel != null ? String(n.dictLabel) : ''),
      dictValue: moduleName,
      orderNum: n.orderNum ?? 0,
      ...(n.children?.length
        ? { children: mapMenuTreeToTreeSelectOption(n.children as MenuTree[]) }
        : {})
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

/** 全部 C#类型选项（来自字典 gen_csharp_type） */
const columnCsharpTypeOptions = computed(() =>
  dictDataStore.getDictOptions('gen_csharp_type').map(o => ({ label: o.label, value: o.value }))
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

/** 需要绑定字典的显示类型（与 gen_display_type 的 DictValue 一致） */
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
  if (!needDictTypeForHtmlType(v)) record.dictType = undefined
}

/** 是否查询改为「否」时清空查询方式 */
function onColumnIsQueryChange(record: GenTableColumnRow) {
  if (!record || typeof record !== 'object') return
  record.queryType = undefined
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

/** 数据表名：必填 + 小写下划线格式（xxxx_xxxx_xxx） */
const tableNameRules = computed(() => [
  { required: true, message: t(`${FORM}.rules.tablename`) },
  {
    validator: (_rule: unknown, v: string) =>
      !v || isSnakeCase(v)
        ? Promise.resolve()
        : Promise.reject(new Error(t(`${FORM}.validation.tablenameformat`)))
  }
])

/** 命名空间前缀校验：必填 + 帕斯卡命名 */
const namePrefixPascalRules = computed(() => [
  { required: true, message: t(`${FORM}.rules.nameprefix`) },
  {
    validator: (_rule: unknown, v: string) =>
      !v || isPascalCase(v)
        ? Promise.resolve()
        : Promise.reject(new Error(t(`${FORM}.validation.nameprefixpascal`)))
  }
])

/** 根据 genTemplateCategory 决定：主子表时 subTableName、subTableFkName 必填 */
const subTableNameRules = computed(() =>
  formState.value.genTemplateCategory === 'sub' ? [{ required: true, message: t(`${FORM}.rules.subTableName`) }] : []
)
const subTableFkNameRules = computed(() =>
  formState.value.genTemplateCategory === 'sub' ? [{ required: true, message: t(`${FORM}.rules.subTableFkName`) }] : []
)
/** 根据 genTemplateCategory 决定：树表时 treeCode、treeName、treeParentCode 必填 */
const treeCodeRules = computed(() =>
  formState.value.genTemplateCategory === 'tree' ? [{ required: true, message: t(`${FORM}.rules.treeCode`) }] : []
)
const treeNameRules = computed(() =>
  formState.value.genTemplateCategory === 'tree' ? [{ required: true, message: t(`${FORM}.rules.treeName`) }] : []
)
const treeParentCodeRules = computed(() =>
  formState.value.genTemplateCategory === 'tree' ? [{ required: true, message: t(`${FORM}.rules.treeParentCode`) }] : []
)
/** 生成方式：选中「自定义路径」(1) 时生成路径必填，zip(0)、当前项目(2) 时可空 */
const genPathRules = computed(() =>
  Number(formState.value.genMethod) === 1 ? [{ required: true, message: t(`${FORM}.rules.genPath`) }] : []
)
/** 是否生成菜单：选「是」(1) 时上级菜单必填，选「否」(0) 时可空 */
const parentMenuIdRules = computed(() =>
  Number(formState.value.isGenMenu) === 1 ? [{ required: true, message: t(`${FORM}.rules.parentMenuId`) }] : []
)
/** 是否生成仓储：选「是」(1) 时仓储相关字段必填 */
const repositoryInterfaceNamespaceRules = computed(() =>
  Number(formState.value.isRepository) === 1
    ? [{ required: true, message: t(`${FORM}.rules.repositoryInterfaceNamespace`) }]
    : []
)
const iRepositoryClassNameRules = computed(() =>
  Number(formState.value.isRepository) === 1
    ? [{ required: true, message: t(`${FORM}.rules.iRepositoryClassName`) }]
    : []
)
const repositoryNamespaceRules = computed(() =>
  Number(formState.value.isRepository) === 1
    ? [{ required: true, message: t(`${FORM}.rules.repositoryNamespace`) }]
    : []
)
const repositoryClassNameRules = computed(() =>
  Number(formState.value.isRepository) === 1
    ? [{ required: true, message: t(`${FORM}.rules.repositoryClassName`) }]
    : []
)
/** 是否使用 Tabs：选「是」(1) 时 Tabs 字段数必填 */
const tabsFieldCountRules = computed(() =>
  Number(formState.value.isUseTabs) === 1 ? [{ required: true, message: t(`${FORM}.rules.tabsFieldCount`) }] : []
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

const columnTableColumns = computed<TableColumnsType>(() => {
  return [
    { title: t(`${FORM}.column.dragsort`), key: 'dragSort', width: 36, align: 'center', class: 'column-drag-cell' },
    {
      title: t('entity.gentablecolumn.databasecolumnname'),
      dataIndex: 'databaseColumnName',
      key: 'databaseColumnName',
      width: 130,
      ellipsis: true
    },
    { title: t('entity.gentablecolumn.columncomment'), dataIndex: 'columnComment', key: 'columnComment', width: 100 },
    { title: t('entity.gentablecolumn.databasedatatype'), dataIndex: 'databaseDataType', key: 'databaseDataType', width: 88 },
    { title: t('entity.gentablecolumn.cshardatatype'), dataIndex: 'csharpDataType', key: 'csharpDataType', width: 88 },
    { title: t('entity.gentablecolumn.csharcolumnname'), dataIndex: 'csharpColumnName', key: 'csharpColumnName', width: 110 },
    { title: t('entity.gentablecolumn.length'), dataIndex: 'length', key: 'length', width: 64 },
    { title: t('entity.gentablecolumn.decimaldigits'), dataIndex: 'decimalDigits', key: 'decimalDigits', width: 64 },
    { title: t('entity.gentablecolumn.ispk'), dataIndex: 'isPk', key: 'isPk', width: 64 },
    { title: t('entity.gentablecolumn.isincrement'), dataIndex: 'isIncrement', key: 'isIncrement', width: 64 },
    { title: t('entity.gentablecolumn.isrequired'), dataIndex: 'isRequired', key: 'isRequired', width: 64 },
    { title: t('entity.gentablecolumn.isquery'), dataIndex: 'isQuery', key: 'isQuery', width: 64 },
    { title: t('entity.gentablecolumn.iscreate'), dataIndex: 'isCreate', key: 'isCreate', width: 64 },
    { title: t('entity.gentablecolumn.isupdate'), dataIndex: 'isUpdate', key: 'isUpdate', width: 64 },
    { title: t('entity.gentablecolumn.isunique'), dataIndex: 'isUnique', key: 'isUnique', width: 64 },
    { title: t('entity.gentablecolumn.islist'), dataIndex: 'isList', key: 'isList', width: 64 },
    { title: t('entity.gentablecolumn.isexport'), dataIndex: 'isExport', key: 'isExport', width: 64 },
    { title: t('entity.gentablecolumn.issort'), dataIndex: 'isSort', key: 'isSort', width: 64 },
    { title: t('entity.gentablecolumn.querytype'), dataIndex: 'queryType', key: 'queryType', width: 88 },
    { title: t('entity.gentablecolumn.htmltype'), dataIndex: 'htmlType', key: 'htmlType', width: 88 },
    { title: t('entity.gentablecolumn.dicttype'), dataIndex: 'dictType', key: 'dictType', width: 95 },
    { title: t('entity.gentablecolumn.ordernum'), dataIndex: 'orderNum', key: 'orderNum', width: 72 },
    { title: t('common.action.operation'), key: 'action', width: 72, fixed: 'right' }
  ]
})

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
    genTemplateCategory: 'crud',
    namePrefix: 'Takt',
    entityNamespace: 'Takt.Domain.Entities',
    entityClassName: undefined,
    dtoNamespace: 'Takt.Application.Dtos',
    dtoClassName: undefined,
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
    genFunction: 'Query,Create,Update,Delete,Status,Sort,Template,Import,Export',
    genMethod: 1,
    isRepository: 1,
    genPath: '/',
    parentMenuId: undefined,
    isGenMenu: 1,
    isGenTranslation: 1,
    sortType: 'asc',
    sortField: undefined,
    permsPrefix: undefined,
    menuButtonGroup: 'query,create,update,delete,detail,preview,print,import,export,template,approve,revoke,authorize,allocate,resetpwd,changepwd,empty,truncate,unlock,disable,generate,download,sync,columns,tables,databases,initialize,clone,copy,suspend,resume,submit,withdraw,transfer,delegate,return,urge,addsign,reducesign,progress,history,publish,enable,version,design,config,validate,start,terminate,field,permission,datasource,theme,data,archive,clean,draft,deletedraft,send,forward,reply,read,unread,circulate,sign,confirm,like,unlike,favorite,unfavorite,share,unshare,comment,uncomment,flagging,unflagging,follow,unfollow,upload,destroy,run,stop,restart,refresh,reset,calculate,book,closing,reconcile,payment,depreciation,reimburse,reversal,accrual,period,carryforward,cancel,change',
    frontUi: 2,
    frontFormLayout: 24,
    frontBtnStyle: 1,
    isUseTabs: 1,
    tabsFieldCount: 10,
    genAuthor: undefined,
    otherGenOptions: undefined,
    columns: undefined
  }
}

async function loadColumns(tableId: string) {
  columnLoading.value = true
  try {
    const list = await getColumnsByTableId(tableId)
    /** API 的 GenTableColumn 与表单行字段一致；GenTableColumnRow 继承 Record<string,unknown>，TS 不允许与无索引签名的 DTO 直接互断 */
    const rows = (list ?? []) as unknown as GenTableColumnRow[]
    columnList.value = normalizeColumnOrderNum(rows)
  } catch {
    columnList.value = []
  } finally {
    columnLoading.value = false
  }
}

/** genTemplateCategory 与 sub/tree 字段相斥：切换模板时清空另一类字段 */
watch(
  () => formState.value.genTemplateCategory,
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

/** isRepository 与仓储相关字段相斥：选「否」(0) 时清空仓储命名空间/类名 */
watch(
  () => formState.value.isRepository,
  (next) => {
    if (next === 0) {
      formState.value.repositoryInterfaceNamespace = undefined
      formState.value.iRepositoryClassName = undefined
      formState.value.repositoryNamespace = undefined
      formState.value.repositoryClassName = undefined
    }
  }
)

/** isGenMenu 与上级菜单相斥：选「否」(0) 时清空上级菜单 */
watch(
  () => formState.value.isGenMenu,
  (next) => {
    if (next === 0) formState.value.parentMenuId = undefined
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

/** 模块名转权限段：Accounting.Controlling -> accounting:controlling；logistics_materials -> logistics:materials（点/下划线均拆段，小写、冒号连接） */
function moduleNameToPermsSegment(genModuleName: string | null | undefined): string {
  if (genModuleName == null || String(genModuleName).trim() === '') return ''
  return String(genModuleName)
    .trim()
    .split('.')
    .flatMap(seg =>
      seg
        .trim()
        .split('_')
        .map(s => s.trim().toLowerCase())
        .filter(s => s.length > 0)
    )
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
    if (next === 0) formState.value.tabsFieldCount = undefined
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
      if (Number(formState.value.isUseTabs) === 1 && formState.value.tabsFieldCount == null) {
        formState.value.tabsFieldCount = 10
      }
      formState.value.genFunctionName = formState.value.tableComment != null ? String(formState.value.tableComment).trim() || undefined : undefined
      if (!formState.value.genAuthor) {
        formState.value.genAuthor =
          asTrimmedString(userStore.userInfo?.realName) || asTrimmedString(userStore.userInfo?.userName)
      }
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
      formState.value.genAuthor =
        asTrimmedString(userStore.userInfo?.realName) || asTrimmedString(userStore.userInfo?.userName)
      const genOpts = dictDataStore.getDictOptions('gen_function')
      const btnOpts = dictDataStore.getDictOptions('gen_button_category')
      if (genOpts.length > 0) formState.value.genFunction = genOpts.map(o => String(o.value)).join(',')
      if (btnOpts.length > 0) formState.value.menuButtonGroup = btnOpts.map(o => String(o.value)).join(',')
      genFunctionCheckAll.value = true
      genFunctionIndeterminate.value = false
      menuButtonGroupCheckAll.value = true
      menuButtonGroupIndeterminate.value = false
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
      throw new Error(t(`${FORM}.validation.columnsnake`, { row: rowNum, value: colName }))
    }
    if (csharpName != null && String(csharpName).trim() !== '' && !isPascalCase(String(csharpName))) {
      throw new Error(t(`${FORM}.validation.columnpascal`, { row: rowNum, value: csharpName }))
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
  /** 上级菜单 id 按字符串保留，避免前端 Number 强转导致 int64 精度丢失；无上级菜单时传 "0" */
  if (raw.parentMenuId != null && String(raw.parentMenuId).trim() !== '') {
    raw.parentMenuId = String(raw.parentMenuId)
  } else {
    raw.parentMenuId = '0'
  }
  return raw
}

/** 整个组件重置：表单、字段列表、Tab、全选状态等全部恢复初始，供取消/提交后由父组件调用 */
function reset() {
  formState.value = defaultFormState()
  formState.value.genPath = defaultGenPath.value || '/'
  formState.value.genAuthor =
    asTrimmedString(userStore.userInfo?.realName) || asTrimmedString(userStore.userInfo?.userName)
  const genOpts = dictDataStore.getDictOptions('gen_function')
  const btnOpts = dictDataStore.getDictOptions('gen_button_category')
  if (genOpts.length > 0) formState.value.genFunction = genOpts.map(o => String(o.value)).join(',')
  if (btnOpts.length > 0) formState.value.menuButtonGroup = btnOpts.map(o => String(o.value)).join(',')
  columnList.value = []
  activeTab.value = 'table'
  tableSubTab.value = 'basic'
  columnDragRowIndex.value = null
  genFunctionCheckAll.value = true
  genFunctionIndeterminate.value = false
  menuButtonGroupCheckAll.value = true
  menuButtonGroupIndeterminate.value = false
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
