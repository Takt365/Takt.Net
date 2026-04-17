export type DataBoardModuleKey =
  | 'overview'
  | 'change'
  | 'online'
  | 'sales'
  | 'production'
  | 'custom'

export interface DataBoardModuleMeta {
  key: DataBoardModuleKey
  titleKey: string
  defaultSpan: number
}

export interface DataBoardModuleItem {
  id: string
  moduleKey: DataBoardModuleKey
  span: number
}
