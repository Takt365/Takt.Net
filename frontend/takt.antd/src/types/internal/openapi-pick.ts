import type {
  AccountingComponents,
  GeneratorComponents,
  HumanResourceComponents,
  IdentityComponents,
  LogisticsComponents,
  RoutineComponents,
  StatisticsComponents,
  WorkflowComponents
} from '@/types/generated'

/** OpenAPI components.schemas 键存在则取真实类型，否则为宽松对象（便于在后端尚未出现在 swagger 时仍能编译）。 */
export type PickSchema<C extends { schemas: Record<string, unknown> }, K extends string> = K extends keyof C['schemas']
  ? C['schemas'][K]
  : Record<string, unknown>

export type AccS<K extends string> = PickSchema<AccountingComponents, K>
export type GenS<K extends string> = PickSchema<GeneratorComponents, K>
export type IdS<K extends string> = PickSchema<IdentityComponents, K>
export type LogS<K extends string> = PickSchema<StatisticsComponents, K>
export type LgsS<K extends string> = PickSchema<LogisticsComponents, K>
/** 人力资源 Swagger 分组（文档名 HumanResource）；别名 OrgS 与路径 human-resource/organization 等沿用。 */
export type OrgS<K extends string> = PickSchema<HumanResourceComponents, K>
export type RouS<K extends string> = PickSchema<RoutineComponents, K>
export type WfS<K extends string> = PickSchema<WorkflowComponents, K>
