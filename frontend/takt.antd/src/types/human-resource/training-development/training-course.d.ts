// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/training-development/training-course
// 文件名称：training-course.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：training-course相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * TrainingCourse类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingCourseDto）
 */
export interface TrainingCourse extends TaktEntityBase {
  /** 对应后端字段 trainingCourseId */
  trainingCourseId: string
  /** 对应后端字段 courseCode */
  courseCode: string
  /** 对应后端字段 courseName */
  courseName: string
  /** 对应后端字段 courseType */
  courseType: string
  /** 对应后端字段 courseLevel */
  courseLevel: string
  /** 对应后端字段 courseDescription */
  courseDescription: string
  /** 对应后端字段 courseObjectives */
  courseObjectives: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 applicablePosition */
  applicablePosition: string
  /** 对应后端字段 trainingHours */
  trainingHours: number
  /** 对应后端字段 trainingDays */
  trainingDays: number
  /** 对应后端字段 mainInstructor */
  mainInstructor: string
  /** 对应后端字段 trainingMethod */
  trainingMethod: string
  /** 对应后端字段 assessmentMethod */
  assessmentMethod: string
  /** 对应后端字段 passingScore */
  passingScore: number
  /** 对应后端字段 isCertification */
  isCertification: number
  /** 对应后端字段 courseOutline */
  courseOutline: string
  /** 对应后端字段 materialList */
  materialList: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 status */
  status: number
}

/**
 * TrainingCourseQuery类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingCourseQueryDto）
 */
export interface TrainingCourseQuery extends TaktPagedQuery {
  /** 对应后端字段 courseCode */
  courseCode?: string
  /** 对应后端字段 courseName */
  courseName?: string
  /** 对应后端字段 courseType */
  courseType?: string
  /** 对应后端字段 courseLevel */
  courseLevel?: string
  /** 对应后端字段 courseDescription */
  courseDescription?: string
  /** 对应后端字段 courseObjectives */
  courseObjectives?: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment?: string
  /** 对应后端字段 applicablePosition */
  applicablePosition?: string
  /** 对应后端字段 trainingHours */
  trainingHours?: number
  /** 对应后端字段 trainingDays */
  trainingDays?: number
  /** 对应后端字段 mainInstructor */
  mainInstructor?: string
  /** 对应后端字段 trainingMethod */
  trainingMethod?: string
  /** 对应后端字段 assessmentMethod */
  assessmentMethod?: string
  /** 对应后端字段 passingScore */
  passingScore?: number
  /** 对应后端字段 isCertification */
  isCertification?: number
  /** 对应后端字段 courseOutline */
  courseOutline?: string
  /** 对应后端字段 materialList */
  materialList?: string
  /** 对应后端字段 status */
  status?: number
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 createdById */
  createdById?: string
  /** 对应后端字段 createdBy */
  createdBy?: string
  /** 对应后端字段 createdAt */
  createdAt?: string
  /** 对应后端字段 createdAtStart */
  createdAtStart?: string
  /** 对应后端字段 createdAtEnd */
  createdAtEnd?: string
}

/**
 * TrainingCourseCreate类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingCourseCreateDto）
 */
export interface TrainingCourseCreate {
  /** 对应后端字段 courseCode */
  courseCode: string
  /** 对应后端字段 courseName */
  courseName: string
  /** 对应后端字段 courseType */
  courseType: string
  /** 对应后端字段 courseLevel */
  courseLevel: string
  /** 对应后端字段 courseDescription */
  courseDescription: string
  /** 对应后端字段 courseObjectives */
  courseObjectives: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 applicablePosition */
  applicablePosition: string
  /** 对应后端字段 trainingHours */
  trainingHours: number
  /** 对应后端字段 trainingDays */
  trainingDays: number
  /** 对应后端字段 mainInstructor */
  mainInstructor: string
  /** 对应后端字段 trainingMethod */
  trainingMethod: string
  /** 对应后端字段 assessmentMethod */
  assessmentMethod: string
  /** 对应后端字段 passingScore */
  passingScore: number
  /** 对应后端字段 isCertification */
  isCertification: number
  /** 对应后端字段 courseOutline */
  courseOutline: string
  /** 对应后端字段 materialList */
  materialList: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * TrainingCourseUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingCourseUpdateDto）
 */
export interface TrainingCourseUpdate extends TrainingCourseCreate {
  /** 对应后端字段 trainingCourseId */
  trainingCourseId: string
}

/**
 * TrainingCourseStatus类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingCourseStatusDto）
 */
export interface TrainingCourseStatus {
  /** 对应后端字段 trainingCourseId */
  trainingCourseId: string
  /** 对应后端字段 status */
  status: number
}

/**
 * TrainingCourseSort类型（对应后端 Takt.Application.Dtos.HumanResource.TrainingDevelopment.TaktTrainingCourseSortDto）
 */
export interface TrainingCourseSort {
  /** 对应后端字段 trainingCourseId */
  trainingCourseId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
