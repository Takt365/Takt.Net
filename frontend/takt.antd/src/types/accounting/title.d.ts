// 会计科目相关类型定义
export interface AccountingTitle {
  titleId: string
  titleCode: string
  titleName: string
  titleType: number
  parentId: string
  titleLevel: number
  titleStatus: number
  createTime?: string
  updateTime?: string
}

export interface AccountingTitleQuery {
  pageIndex?: number
  pageSize?: number
  keyWords?: string
  titleCode?: string
  titleName?: string
  titleType?: number
}
