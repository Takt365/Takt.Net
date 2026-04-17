import type { LgsS } from '@/types/internal/openapi-pick'

export type Plant = LgsS<'Takt.Application.Dtos.Logistics.Materials.TaktPlantDto'>
export type PlantQuery = LgsS<'Takt.Application.Dtos.Logistics.Materials.TaktPlantQueryDto'>
export type PlantCreate = LgsS<'Takt.Application.Dtos.Logistics.Materials.TaktPlantCreateDto'>
export type PlantUpdate = LgsS<'Takt.Application.Dtos.Logistics.Materials.TaktPlantUpdateDto'>
/** 导入/导出/模板接口在 swagger 中无独立 schema，占位为宽松对象。 */
export type PlantTemplate = Record<string, unknown>
export type PlantImport = Record<string, unknown>
export type PlantExport = Record<string, unknown>
