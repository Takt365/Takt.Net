export interface paths {
    "/api/TaktAccountingTitles/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    TitleName?: string;
                    TitleCode?: string;
                    ParentId?: number;
                    TitleType?: number;
                    TitleStatus?: number;
                    PageIndex?: number;
                    PageSize?: number;
                    KeyWords?: string;
                };
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktAccountingTitleDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktAccountingTitleDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktAccountingTitleDto"];
                    };
                };
            };
        };
        put?: never;
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAccountingTitles/{id}": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    id: number;
                };
                cookie?: never;
            };
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAccountingTitleDto"];
                        "application/json": components["schemas"]["TaktAccountingTitleDto"];
                        "text/json": components["schemas"]["TaktAccountingTitleDto"];
                    };
                };
            };
        };
        put: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    id: number;
                };
                cookie?: never;
            };
            requestBody: {
                content: {
                    "application/json-patch+json": components["schemas"]["TaktAccountingTitleUpdateDto"];
                    "application/json": components["schemas"]["TaktAccountingTitleUpdateDto"];
                    "text/json": components["schemas"]["TaktAccountingTitleUpdateDto"];
                    "application/*+json": components["schemas"]["TaktAccountingTitleUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAccountingTitleDto"];
                        "application/json": components["schemas"]["TaktAccountingTitleDto"];
                        "text/json": components["schemas"]["TaktAccountingTitleDto"];
                    };
                };
            };
        };
        post?: never;
        delete: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    id: number;
                };
                cookie?: never;
            };
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content?: never;
                };
            };
        };
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAccountingTitles/tree-options": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: never;
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktTreeSelectOption"][];
                        "application/json": components["schemas"]["TaktTreeSelectOption"][];
                        "text/json": components["schemas"]["TaktTreeSelectOption"][];
                    };
                };
            };
        };
        put?: never;
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAccountingTitles/tree": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    parentId?: number;
                    includeDisabled?: boolean;
                };
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAccountingTitleTreeDto"][];
                        "application/json": components["schemas"]["TaktAccountingTitleTreeDto"][];
                        "text/json": components["schemas"]["TaktAccountingTitleTreeDto"][];
                    };
                };
            };
        };
        put?: never;
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAccountingTitles/children": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    parentId?: number;
                    includeDisabled?: boolean;
                };
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAccountingTitleDto"][];
                        "application/json": components["schemas"]["TaktAccountingTitleDto"][];
                        "text/json": components["schemas"]["TaktAccountingTitleDto"][];
                    };
                };
            };
        };
        put?: never;
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAccountingTitles": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u4F1A\u8BA1\u79D1\u76EE"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAccountingTitles/status": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put: {
            parameters: {
                query?: never;
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody: {
                content: {
                    "application/json-patch+json": components["schemas"]["TaktAccountingTitleStatusDto"];
                    "application/json": components["schemas"]["TaktAccountingTitleStatusDto"];
                    "text/json": components["schemas"]["TaktAccountingTitleStatusDto"];
                    "application/*+json": components["schemas"]["TaktAccountingTitleStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAccountingTitleDto"];
                        "application/json": components["schemas"]["TaktAccountingTitleDto"];
                        "text/json": components["schemas"]["TaktAccountingTitleDto"];
                    };
                };
            };
        };
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAccountingTitles/template": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    sheetName?: string;
                    fileName?: string;
                };
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content?: never;
                };
            };
        };
        put?: never;
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAccountingTitles/import": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: {
            parameters: {
                query?: never;
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody: {
                content: {
                    "multipart/form-data": {
                        file?: components["schemas"]["IFormFile"];
                    } & {
                        /** @default null */
                        sheetName?: string;
                    };
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content?: never;
                };
            };
        };
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAccountingTitles/export": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: {
            parameters: {
                query?: {
                    sheetName?: string;
                    fileName?: string;
                };
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody: {
                content: {
                    "application/json-patch+json": components["schemas"]["TaktAccountingTitleQueryDto"];
                    "application/json": components["schemas"]["TaktAccountingTitleQueryDto"];
                    "text/json": components["schemas"]["TaktAccountingTitleQueryDto"];
                    "application/*+json": components["schemas"]["TaktAccountingTitleQueryDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content?: never;
                };
            };
        };
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktCostCenters/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    CostCenterName?: string;
                    CostCenterCode?: string;
                    ParentId?: number;
                    CostCenterType?: number;
                    CostCenterStatus?: number;
                    PageIndex?: number;
                    PageSize?: number;
                    KeyWords?: string;
                };
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktCostCenterDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktCostCenterDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktCostCenterDto"];
                    };
                };
            };
        };
        put?: never;
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktCostCenters/{id}": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    id: number;
                };
                cookie?: never;
            };
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktCostCenterDto"];
                        "application/json": components["schemas"]["TaktCostCenterDto"];
                        "text/json": components["schemas"]["TaktCostCenterDto"];
                    };
                };
            };
        };
        put: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    id: number;
                };
                cookie?: never;
            };
            requestBody: {
                content: {
                    "application/json-patch+json": components["schemas"]["TaktCostCenterUpdateDto"];
                    "application/json": components["schemas"]["TaktCostCenterUpdateDto"];
                    "text/json": components["schemas"]["TaktCostCenterUpdateDto"];
                    "application/*+json": components["schemas"]["TaktCostCenterUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktCostCenterDto"];
                        "application/json": components["schemas"]["TaktCostCenterDto"];
                        "text/json": components["schemas"]["TaktCostCenterDto"];
                    };
                };
            };
        };
        post?: never;
        delete: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    id: number;
                };
                cookie?: never;
            };
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content?: never;
                };
            };
        };
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktCostCenters/options": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: never;
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktSelectOption"][];
                        "application/json": components["schemas"]["TaktSelectOption"][];
                        "text/json": components["schemas"]["TaktSelectOption"][];
                    };
                };
            };
        };
        put?: never;
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktCostCenters": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u6210\u672C\u4E2D\u5FC3"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktCostCenters/status": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put: {
            parameters: {
                query?: never;
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody: {
                content: {
                    "application/json-patch+json": components["schemas"]["TaktCostCenterStatusDto"];
                    "application/json": components["schemas"]["TaktCostCenterStatusDto"];
                    "text/json": components["schemas"]["TaktCostCenterStatusDto"];
                    "application/*+json": components["schemas"]["TaktCostCenterStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktCostCenterDto"];
                        "application/json": components["schemas"]["TaktCostCenterDto"];
                        "text/json": components["schemas"]["TaktCostCenterDto"];
                    };
                };
            };
        };
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktCostCenters/template": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    sheetName?: string;
                    fileName?: string;
                };
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content?: never;
                };
            };
        };
        put?: never;
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktCostCenters/import": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: {
            parameters: {
                query?: never;
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody: {
                content: {
                    "multipart/form-data": {
                        file?: components["schemas"]["IFormFile"];
                    } & {
                        /** @default null */
                        sheetName?: string;
                    };
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content?: never;
                };
            };
        };
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktCostCenters/export": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: {
            parameters: {
                query?: {
                    sheetName?: string;
                    fileName?: string;
                };
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody: {
                content: {
                    "application/json-patch+json": components["schemas"]["TaktCostCenterQueryDto"];
                    "application/json": components["schemas"]["TaktCostCenterQueryDto"];
                    "text/json": components["schemas"]["TaktCostCenterQueryDto"];
                    "application/*+json": components["schemas"]["TaktCostCenterQueryDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content?: never;
                };
            };
        };
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
}
export type webhooks = Record<string, never>;
export interface components {
    schemas: {
        /** Format: binary */
        IFormFile: string;
        TaktAccountingTitleCreateDto: {
            titleCode?: string;
            titleName?: string;
            /** Format: int64 */
            parentId?: number;
            /** Format: int32 */
            titleType?: number;
            /** Format: int32 */
            balanceDirection?: number;
            /** Format: int32 */
            isLeaf?: number;
            /** Format: int32 */
            isAuxiliary?: number;
            /** Format: int32 */
            auxiliaryType?: number;
            /** Format: int32 */
            isQuantity?: number;
            /** Format: int32 */
            isCurrency?: number;
            /** Format: int32 */
            isCash?: number;
            /** Format: int32 */
            isBank?: number;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktAccountingTitleDto: {
            /** Format: int64 */
            titleId?: number;
            titleCode?: string;
            titleName?: string;
            /** Format: int64 */
            parentId?: number;
            /** Format: int32 */
            titleType?: number;
            /** Format: int32 */
            balanceDirection?: number;
            /** Format: int32 */
            titleLevel?: number;
            /** Format: int32 */
            isLeaf?: number;
            /** Format: int32 */
            isAuxiliary?: number;
            /** Format: int32 */
            auxiliaryType?: number;
            /** Format: int32 */
            isQuantity?: number;
            /** Format: int32 */
            isCurrency?: number;
            /** Format: int32 */
            isCash?: number;
            /** Format: int32 */
            isBank?: number;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            titleStatus?: number;
            configId?: string;
            extFieldJson?: string | null;
            remark?: string | null;
            /** Format: int64 */
            createdById?: number;
            createdBy?: string;
            /** Format: date-time */
            createdAt?: string;
            /** Format: int64 */
            updatedById?: number | null;
            updatedBy?: string | null;
            /** Format: date-time */
            updatedAt?: string | null;
            /** Format: int32 */
            isDeleted?: number;
            /** Format: int64 */
            deletedById?: number | null;
            deletedBy?: string | null;
            /** Format: date-time */
            deletedAt?: string | null;
        };
        TaktAccountingTitleQueryDto: {
            titleName?: string | null;
            titleCode?: string | null;
            /** Format: int64 */
            parentId?: number | null;
            /** Format: int32 */
            titleType?: number | null;
            /** Format: int32 */
            titleStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktAccountingTitleStatusDto: {
            /** Format: int64 */
            titleId?: number;
            /** Format: int32 */
            titleStatus?: number;
        };
        TaktAccountingTitleTreeDto: {
            children?: components["schemas"][];
            /** Format: int64 */
            titleId?: number;
            titleCode?: string;
            titleName?: string;
            /** Format: int64 */
            parentId?: number;
            /** Format: int32 */
            titleType?: number;
            /** Format: int32 */
            balanceDirection?: number;
            /** Format: int32 */
            titleLevel?: number;
            /** Format: int32 */
            isLeaf?: number;
            /** Format: int32 */
            isAuxiliary?: number;
            /** Format: int32 */
            auxiliaryType?: number;
            /** Format: int32 */
            isQuantity?: number;
            /** Format: int32 */
            isCurrency?: number;
            /** Format: int32 */
            isCash?: number;
            /** Format: int32 */
            isBank?: number;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            titleStatus?: number;
            configId?: string;
            extFieldJson?: string | null;
            remark?: string | null;
            /** Format: int64 */
            createdById?: number;
            createdBy?: string;
            /** Format: date-time */
            createdAt?: string;
            /** Format: int64 */
            updatedById?: number | null;
            updatedBy?: string | null;
            /** Format: date-time */
            updatedAt?: string | null;
            /** Format: int32 */
            isDeleted?: number;
            /** Format: int64 */
            deletedById?: number | null;
            deletedBy?: string | null;
            /** Format: date-time */
            deletedAt?: string | null;
        };
        TaktAccountingTitleUpdateDto: {
            /** Format: int64 */
            titleId?: number;
            titleCode?: string;
            titleName?: string;
            /** Format: int64 */
            parentId?: number;
            /** Format: int32 */
            titleType?: number;
            /** Format: int32 */
            balanceDirection?: number;
            /** Format: int32 */
            isLeaf?: number;
            /** Format: int32 */
            isAuxiliary?: number;
            /** Format: int32 */
            auxiliaryType?: number;
            /** Format: int32 */
            isQuantity?: number;
            /** Format: int32 */
            isCurrency?: number;
            /** Format: int32 */
            isCash?: number;
            /** Format: int32 */
            isBank?: number;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktCostCenterCreateDto: {
            costCenterCode?: string;
            costCenterName?: string;
            /** Format: int64 */
            parentId?: number;
            /** Format: int32 */
            costCenterType?: number;
            /** Format: int64 */
            managerId?: number | null;
            managerName?: string | null;
            /** Format: int64 */
            deptId?: number | null;
            deptName?: string | null;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktCostCenterDto: {
            /** Format: int64 */
            costCenterId?: number;
            costCenterCode?: string;
            costCenterName?: string;
            /** Format: int64 */
            parentId?: number;
            /** Format: int32 */
            costCenterType?: number;
            /** Format: int64 */
            managerId?: number | null;
            managerName?: string | null;
            /** Format: int64 */
            deptId?: number | null;
            deptName?: string | null;
            /** Format: int32 */
            costCenterLevel?: number;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            costCenterStatus?: number;
            configId?: string;
            extFieldJson?: string | null;
            remark?: string | null;
            /** Format: int64 */
            createdById?: number;
            createdBy?: string;
            /** Format: date-time */
            createdAt?: string;
            /** Format: int64 */
            updatedById?: number | null;
            updatedBy?: string | null;
            /** Format: date-time */
            updatedAt?: string | null;
            /** Format: int32 */
            isDeleted?: number;
            /** Format: int64 */
            deletedById?: number | null;
            deletedBy?: string | null;
            /** Format: date-time */
            deletedAt?: string | null;
        };
        TaktCostCenterQueryDto: {
            costCenterName?: string | null;
            costCenterCode?: string | null;
            /** Format: int64 */
            parentId?: number | null;
            /** Format: int32 */
            costCenterType?: number | null;
            /** Format: int32 */
            costCenterStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktCostCenterStatusDto: {
            /** Format: int64 */
            costCenterId?: number;
            /** Format: int32 */
            costCenterStatus?: number;
        };
        TaktCostCenterUpdateDto: {
            costCenterCode?: string;
            costCenterName?: string;
            /** Format: int64 */
            parentId?: number;
            /** Format: int32 */
            costCenterType?: number;
            /** Format: int64 */
            managerId?: number | null;
            managerName?: string | null;
            /** Format: int64 */
            deptId?: number | null;
            deptName?: string | null;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktPagedResultOfTaktAccountingTitleDto: {
            data?: components["schemas"]["TaktAccountingTitleDto"][];
            /** Format: int32 */
            total?: number;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            /** Format: int32 */
            totalPages?: number;
            hasPreviousPage?: boolean;
            hasNextPage?: boolean;
        };
        TaktPagedResultOfTaktCostCenterDto: {
            data?: components["schemas"]["TaktCostCenterDto"][];
            /** Format: int32 */
            total?: number;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            /** Format: int32 */
            totalPages?: number;
            hasPreviousPage?: boolean;
            hasNextPage?: boolean;
        };
        TaktSelectOption: {
            dictLabel?: string;
            dictValue?: unknown;
            extLabel?: string | null;
            extValue?: unknown;
            dictL10nKey?: string | null;
            dictTypeCode?: string | null;
            /** Format: int32 */
            cssClass?: number | null;
            /** Format: int32 */
            listClass?: number | null;
            /** Format: int32 */
            orderNum?: number;
        };
        TaktTreeSelectOption: {
            children?: components["schemas"][];
            dictLabel?: string;
            dictValue?: unknown;
            extLabel?: string | null;
            extValue?: unknown;
            dictL10nKey?: string | null;
            dictTypeCode?: string | null;
            /** Format: int32 */
            cssClass?: number | null;
            /** Format: int32 */
            listClass?: number | null;
            /** Format: int32 */
            orderNum?: number;
        };
    };
    responses: never;
    parameters: never;
    requestBodies: never;
    headers: never;
    pathItems: never;
}
export type $defs = Record<string, never>;
export interface operations {
    "\u4F1A\u8BA1\u79D1\u76EE": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktAccountingTitleCreateDto"];
                "application/json": components["schemas"]["TaktAccountingTitleCreateDto"];
                "text/json": components["schemas"]["TaktAccountingTitleCreateDto"];
                "application/*+json": components["schemas"]["TaktAccountingTitleCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktAccountingTitleDto"];
                    "application/json": components["schemas"]["TaktAccountingTitleDto"];
                    "text/json": components["schemas"]["TaktAccountingTitleDto"];
                };
            };
        };
    };
    "\u6210\u672C\u4E2D\u5FC3": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktCostCenterCreateDto"];
                "application/json": components["schemas"]["TaktCostCenterCreateDto"];
                "text/json": components["schemas"]["TaktCostCenterCreateDto"];
                "application/*+json": components["schemas"]["TaktCostCenterCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktCostCenterDto"];
                    "application/json": components["schemas"]["TaktCostCenterDto"];
                    "text/json": components["schemas"]["TaktCostCenterDto"];
                };
            };
        };
    };
}
