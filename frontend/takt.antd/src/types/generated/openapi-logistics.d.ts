export interface paths {
    "/api/TaktPlant/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    PlantCode?: string;
                    PlantName?: string;
                    PlantShortName?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktPlantDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktPlantDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktPlantDto"];
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
    "/api/TaktPlant/{id}": {
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
                        "text/plain": components["schemas"]["TaktPlantDto"];
                        "application/json": components["schemas"]["TaktPlantDto"];
                        "text/json": components["schemas"]["TaktPlantDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktPlantUpdateDto"];
                    "application/json": components["schemas"]["TaktPlantUpdateDto"];
                    "text/json": components["schemas"]["TaktPlantUpdateDto"];
                    "application/*+json": components["schemas"]["TaktPlantUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktPlantDto"];
                        "application/json": components["schemas"]["TaktPlantDto"];
                        "text/json": components["schemas"]["TaktPlantDto"];
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
    "/api/TaktPlant": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5DE5\u5382\u8868"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktPlant/batch": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post?: never;
        delete: {
            parameters: {
                query?: never;
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody: {
                content: {
                    "application/json-patch+json": number[];
                    "application/json": number[];
                    "text/json": number[];
                    "application/*+json": number[];
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
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktPlant/template": {
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
    "/api/TaktPlant/import": {
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
    "/api/TaktPlant/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktPlantQueryDto"];
                    "application/json": components["schemas"]["TaktPlantQueryDto"];
                    "text/json": components["schemas"]["TaktPlantQueryDto"];
                    "application/*+json": components["schemas"]["TaktPlantQueryDto"];
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
    "/api/TaktPurchaseOrders/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    OrderCode?: string;
                    SupplierCode?: string;
                    SupplierName?: string;
                    RequestId?: number;
                    PurchaseUserId?: number;
                    OrderStatus?: number;
                    PaymentStatus?: number;
                    OrderDateStart?: string;
                    OrderDateEnd?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktPurchaseOrderDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktPurchaseOrderDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktPurchaseOrderDto"];
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
    "/api/TaktPurchaseOrders/{id}": {
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
                        "text/plain": components["schemas"]["TaktPurchaseOrderDto"];
                        "application/json": components["schemas"]["TaktPurchaseOrderDto"];
                        "text/json": components["schemas"]["TaktPurchaseOrderDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktPurchaseOrderUpdateDto"];
                    "application/json": components["schemas"]["TaktPurchaseOrderUpdateDto"];
                    "text/json": components["schemas"]["TaktPurchaseOrderUpdateDto"];
                    "application/*+json": components["schemas"]["TaktPurchaseOrderUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktPurchaseOrderDto"];
                        "application/json": components["schemas"]["TaktPurchaseOrderDto"];
                        "text/json": components["schemas"]["TaktPurchaseOrderDto"];
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
    "/api/TaktPurchaseOrders": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u91C7\u8D2D\u8BA2\u5355"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktPurchaseOrders/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktPurchaseOrderStatusDto"];
                    "application/json": components["schemas"]["TaktPurchaseOrderStatusDto"];
                    "text/json": components["schemas"]["TaktPurchaseOrderStatusDto"];
                    "application/*+json": components["schemas"]["TaktPurchaseOrderStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktPurchaseOrderDto"];
                        "application/json": components["schemas"]["TaktPurchaseOrderDto"];
                        "text/json": components["schemas"]["TaktPurchaseOrderDto"];
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
    "/api/TaktPurchaseOrders/template": {
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
    "/api/TaktPurchaseOrders/import": {
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
    "/api/TaktPurchaseOrders/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktPurchaseOrderQueryDto"];
                    "application/json": components["schemas"]["TaktPurchaseOrderQueryDto"];
                    "text/json": components["schemas"]["TaktPurchaseOrderQueryDto"];
                    "application/*+json": components["schemas"]["TaktPurchaseOrderQueryDto"];
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
    "/api/TaktPurchasePrices/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    SupplierCode?: string;
                    PlantCode?: string;
                    PriceType?: number;
                    PriceStatus?: number;
                    IsEnabled?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktPurchasePriceDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktPurchasePriceDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktPurchasePriceDto"];
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
    "/api/TaktPurchasePrices/{id}": {
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
                        "text/plain": components["schemas"]["TaktPurchasePriceDto"];
                        "application/json": components["schemas"]["TaktPurchasePriceDto"];
                        "text/json": components["schemas"]["TaktPurchasePriceDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktPurchasePriceUpdateDto"];
                    "application/json": components["schemas"]["TaktPurchasePriceUpdateDto"];
                    "text/json": components["schemas"]["TaktPurchasePriceUpdateDto"];
                    "application/*+json": components["schemas"]["TaktPurchasePriceUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktPurchasePriceDto"];
                        "application/json": components["schemas"]["TaktPurchasePriceDto"];
                        "text/json": components["schemas"]["TaktPurchasePriceDto"];
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
    "/api/TaktPurchasePrices": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u91C7\u8D2D\u4EF7\u683C"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktPurchasePrices/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktPurchasePriceStatusDto"];
                    "application/json": components["schemas"]["TaktPurchasePriceStatusDto"];
                    "text/json": components["schemas"]["TaktPurchasePriceStatusDto"];
                    "application/*+json": components["schemas"]["TaktPurchasePriceStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktPurchasePriceDto"];
                        "application/json": components["schemas"]["TaktPurchasePriceDto"];
                        "text/json": components["schemas"]["TaktPurchasePriceDto"];
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
    "/api/TaktPurchasePrices/template": {
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
    "/api/TaktPurchasePrices/import": {
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
    "/api/TaktPurchasePrices/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktPurchasePriceQueryDto"];
                    "application/json": components["schemas"]["TaktPurchasePriceQueryDto"];
                    "text/json": components["schemas"]["TaktPurchasePriceQueryDto"];
                    "application/*+json": components["schemas"]["TaktPurchasePriceQueryDto"];
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
        TaktPagedResultOfTaktPlantDto: {
            data?: components["schemas"]["TaktPlantDto"][];
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
        TaktPagedResultOfTaktPurchaseOrderDto: {
            data?: components["schemas"]["TaktPurchaseOrderDto"][];
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
        TaktPagedResultOfTaktPurchasePriceDto: {
            data?: components["schemas"]["TaktPurchasePriceDto"][];
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
        TaktPlantCreateDto: {
            plantCode?: string;
            plantName?: string;
            plantShortName?: string | null;
            registrationAddress?: string | null;
            registrationRegion?: string | null;
            registrationProvince?: string | null;
            registrationCity?: string | null;
            businessRegion?: string | null;
            businessProvince?: string | null;
            businessCity?: string | null;
            businessAddress?: string | null;
            plantAddress?: string | null;
            plantPhone?: string | null;
            plantEmail?: string | null;
            plantManager?: string | null;
            enterpriseNature?: string | null;
            industryAttribute?: string | null;
            enterpriseScale?: string | null;
            businessScope?: string | null;
            relatedCompany?: string | null;
            /** Format: int32 */
            plantStatus?: number;
            /** Format: int32 */
            orderNum?: number;
            extFieldJson?: string | null;
            remark?: string | null;
        };
        TaktPlantDto: {
            /** Format: int64 */
            plantId?: number;
            plantCode?: string;
            plantName?: string;
            plantShortName?: string | null;
            registrationAddress?: string | null;
            registrationRegion?: string | null;
            registrationProvince?: string | null;
            registrationCity?: string | null;
            businessRegion?: string | null;
            businessProvince?: string | null;
            businessCity?: string | null;
            businessAddress?: string | null;
            plantAddress?: string | null;
            plantPhone?: string | null;
            plantEmail?: string | null;
            plantManager?: string | null;
            enterpriseNature?: string | null;
            industryAttribute?: string | null;
            enterpriseScale?: string | null;
            businessScope?: string | null;
            relatedCompany?: string | null;
            /** Format: int32 */
            plantStatus?: number;
            /** Format: int32 */
            orderNum?: number;
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
        TaktPlantQueryDto: {
            plantCode?: string;
            plantName?: string;
            plantShortName?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktPlantUpdateDto: {
            /** Format: int64 */
            plantId?: number;
            plantCode?: string;
            plantName?: string;
            plantShortName?: string | null;
            registrationAddress?: string | null;
            registrationRegion?: string | null;
            registrationProvince?: string | null;
            registrationCity?: string | null;
            businessRegion?: string | null;
            businessProvince?: string | null;
            businessCity?: string | null;
            businessAddress?: string | null;
            plantAddress?: string | null;
            plantPhone?: string | null;
            plantEmail?: string | null;
            plantManager?: string | null;
            enterpriseNature?: string | null;
            industryAttribute?: string | null;
            enterpriseScale?: string | null;
            businessScope?: string | null;
            relatedCompany?: string | null;
            /** Format: int32 */
            plantStatus?: number;
            /** Format: int32 */
            orderNum?: number;
            extFieldJson?: string | null;
            remark?: string | null;
        };
        TaktPurchaseOrderCreateDto: {
            orderCode?: string;
            /** Format: int64 */
            requestId?: number | null;
            requestCode?: string | null;
            /** Format: int64 */
            supplierId?: number;
            supplierCode?: string;
            supplierName?: string;
            supplierContact?: string | null;
            supplierPhone?: string | null;
            supplierAddress?: string | null;
            /** Format: date-time */
            orderDate?: string;
            /** Format: date-time */
            requiredArrivalDate?: string | null;
            /** Format: int64 */
            purchaseUserId?: number;
            purchaseUserName?: string;
            /** Format: int64 */
            purchaseDeptId?: number | null;
            purchaseDeptName?: string | null;
            /** Format: int32 */
            paymentMethod?: number;
            /** Format: int32 */
            deliveryMethod?: number;
            deliveryAddress?: string | null;
            remark?: string | null;
            items?: components["schemas"]["TaktPurchaseOrderItemCreateDto"][];
        };
        TaktPurchaseOrderDto: {
            /** Format: int64 */
            orderId?: number;
            orderCode?: string;
            /** Format: int64 */
            requestId?: number | null;
            requestCode?: string | null;
            /** Format: int64 */
            supplierId?: number;
            supplierCode?: string;
            supplierName?: string;
            supplierContact?: string | null;
            supplierPhone?: string | null;
            supplierAddress?: string | null;
            /** Format: date-time */
            orderDate?: string;
            /** Format: date-time */
            requiredArrivalDate?: string | null;
            /** Format: date-time */
            actualArrivalDate?: string | null;
            /** Format: int64 */
            purchaseUserId?: number;
            purchaseUserName?: string;
            /** Format: int64 */
            purchaseDeptId?: number | null;
            purchaseDeptName?: string | null;
            /** Format: double */
            totalQuantity?: number;
            /** Format: double */
            totalAmount?: number;
            /** Format: double */
            discountAmount?: number;
            /** Format: double */
            taxAmount?: number;
            /** Format: double */
            actualAmount?: number;
            /** Format: double */
            receivedQuantity?: number;
            /** Format: double */
            receivedAmount?: number;
            /** Format: double */
            paidAmount?: number;
            /** Format: int32 */
            orderStatus?: number;
            /** Format: int32 */
            paymentStatus?: number;
            /** Format: int32 */
            paymentMethod?: number;
            /** Format: int32 */
            deliveryMethod?: number;
            deliveryAddress?: string | null;
            items?: components["schemas"]["TaktPurchaseOrderItemDto"][];
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
        TaktPurchaseOrderItemCreateDto: {
            /** Format: int64 */
            materialId?: number;
            materialCode?: string;
            materialName?: string;
            materialSpecification?: string | null;
            purchaseUnit?: string;
            /** Format: double */
            orderQuantity?: number;
            /** Format: double */
            unitPrice?: number;
            /** Format: double */
            discountRate?: number;
            /** Format: double */
            taxRate?: number;
            /** Format: int32 */
            lineNumber?: number;
        };
        TaktPurchaseOrderItemDto: {
            /** Format: int64 */
            itemId?: number;
            /** Format: int64 */
            orderId?: number;
            orderCode?: string;
            /** Format: int64 */
            materialId?: number;
            materialCode?: string;
            materialName?: string;
            materialSpecification?: string | null;
            purchaseUnit?: string;
            /** Format: double */
            orderQuantity?: number;
            /** Format: double */
            receivedQuantity?: number;
            /** Format: double */
            unitPrice?: number;
            /** Format: double */
            discountRate?: number;
            /** Format: double */
            discountAmount?: number;
            /** Format: double */
            taxRate?: number;
            /** Format: double */
            taxAmount?: number;
            /** Format: double */
            subtotalAmount?: number;
            /** Format: int32 */
            lineNumber?: number;
        };
        TaktPurchaseOrderQueryDto: {
            orderCode?: string | null;
            supplierCode?: string | null;
            supplierName?: string | null;
            /** Format: int64 */
            requestId?: number | null;
            /** Format: int64 */
            purchaseUserId?: number | null;
            /** Format: int32 */
            orderStatus?: number | null;
            /** Format: int32 */
            paymentStatus?: number | null;
            /** Format: date-time */
            orderDateStart?: string | null;
            /** Format: date-time */
            orderDateEnd?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktPurchaseOrderStatusDto: {
            /** Format: int64 */
            orderId?: number;
            /** Format: int32 */
            orderStatus?: number;
        };
        TaktPurchaseOrderUpdateDto: {
            /** Format: int64 */
            orderId?: number;
            orderCode?: string;
            /** Format: int64 */
            requestId?: number | null;
            requestCode?: string | null;
            /** Format: int64 */
            supplierId?: number;
            supplierCode?: string;
            supplierName?: string;
            supplierContact?: string | null;
            supplierPhone?: string | null;
            supplierAddress?: string | null;
            /** Format: date-time */
            orderDate?: string;
            /** Format: date-time */
            requiredArrivalDate?: string | null;
            /** Format: int64 */
            purchaseUserId?: number;
            purchaseUserName?: string;
            /** Format: int64 */
            purchaseDeptId?: number | null;
            purchaseDeptName?: string | null;
            /** Format: int32 */
            paymentMethod?: number;
            /** Format: int32 */
            deliveryMethod?: number;
            deliveryAddress?: string | null;
            remark?: string | null;
            items?: components["schemas"]["TaktPurchaseOrderItemCreateDto"][];
        };
        TaktPurchasePriceCreateDto: {
            plantCode?: string | null;
            supplierCode?: string;
            /** Format: int32 */
            priceType?: number;
            /** Format: date-time */
            effectiveDate?: string;
            /** Format: date-time */
            expiryDate?: string | null;
            remark?: string | null;
            items?: components["schemas"]["TaktPurchasePriceItemCreateDto"][];
        };
        TaktPurchasePriceDto: {
            /** Format: int64 */
            priceId?: number;
            plantCode?: string | null;
            supplierCode?: string;
            /** Format: int32 */
            priceType?: number;
            /** Format: date-time */
            effectiveDate?: string;
            /** Format: date-time */
            expiryDate?: string | null;
            /** Format: int32 */
            priceStatus?: number;
            /** Format: int32 */
            isEnabled?: number;
            items?: components["schemas"]["TaktPurchasePriceItemDto"][];
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
        TaktPurchasePriceItemCreateDto: {
            materialCode?: string;
            purchaseUnit?: string;
            /** Format: double */
            purchasePrice?: number;
            /** Format: double */
            minPurchaseQuantity?: number;
            /** Format: double */
            maxPurchaseQuantity?: number;
            /** Format: int32 */
            orderNum?: number;
            scales?: components["schemas"]["TaktPurchasePriceScaleCreateDto"][];
        };
        TaktPurchasePriceItemDto: {
            /** Format: int64 */
            itemId?: number;
            /** Format: int64 */
            priceId?: number;
            materialCode?: string;
            purchaseUnit?: string;
            /** Format: double */
            purchasePrice?: number;
            /** Format: double */
            minPurchaseQuantity?: number;
            /** Format: double */
            maxPurchaseQuantity?: number;
            /** Format: int32 */
            orderNum?: number;
            scales?: components["schemas"]["TaktPurchasePriceScaleDto"][];
        };
        TaktPurchasePriceQueryDto: {
            supplierCode?: string | null;
            plantCode?: string | null;
            /** Format: int32 */
            priceType?: number | null;
            /** Format: int32 */
            priceStatus?: number | null;
            /** Format: int32 */
            isEnabled?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktPurchasePriceScaleCreateDto: {
            /** Format: double */
            startQuantity?: number;
            /** Format: double */
            endQuantity?: number;
            /** Format: double */
            scalePrice?: number;
            /** Format: int32 */
            orderNum?: number;
        };
        TaktPurchasePriceScaleDto: {
            /** Format: int64 */
            scaleId?: number;
            /** Format: int64 */
            itemId?: number;
            /** Format: double */
            startQuantity?: number;
            /** Format: double */
            endQuantity?: number;
            /** Format: double */
            scalePrice?: number;
            /** Format: int32 */
            orderNum?: number;
        };
        TaktPurchasePriceStatusDto: {
            /** Format: int64 */
            priceId?: number;
            /** Format: int32 */
            priceStatus?: number;
        };
        TaktPurchasePriceUpdateDto: {
            /** Format: int64 */
            priceId?: number;
            plantCode?: string | null;
            supplierCode?: string;
            /** Format: int32 */
            priceType?: number;
            /** Format: date-time */
            effectiveDate?: string;
            /** Format: date-time */
            expiryDate?: string | null;
            remark?: string | null;
            items?: components["schemas"]["TaktPurchasePriceItemCreateDto"][];
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
    "\u5DE5\u5382\u8868": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktPlantCreateDto"];
                "application/json": components["schemas"]["TaktPlantCreateDto"];
                "text/json": components["schemas"]["TaktPlantCreateDto"];
                "application/*+json": components["schemas"]["TaktPlantCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktPlantDto"];
                    "application/json": components["schemas"]["TaktPlantDto"];
                    "text/json": components["schemas"]["TaktPlantDto"];
                };
            };
        };
    };
    "\u91C7\u8D2D\u8BA2\u5355": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktPurchaseOrderCreateDto"];
                "application/json": components["schemas"]["TaktPurchaseOrderCreateDto"];
                "text/json": components["schemas"]["TaktPurchaseOrderCreateDto"];
                "application/*+json": components["schemas"]["TaktPurchaseOrderCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktPurchaseOrderDto"];
                    "application/json": components["schemas"]["TaktPurchaseOrderDto"];
                    "text/json": components["schemas"]["TaktPurchaseOrderDto"];
                };
            };
        };
    };
    "\u91C7\u8D2D\u4EF7\u683C": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktPurchasePriceCreateDto"];
                "application/json": components["schemas"]["TaktPurchasePriceCreateDto"];
                "text/json": components["schemas"]["TaktPurchasePriceCreateDto"];
                "application/*+json": components["schemas"]["TaktPurchasePriceCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktPurchasePriceDto"];
                    "application/json": components["schemas"]["TaktPurchasePriceDto"];
                    "text/json": components["schemas"]["TaktPurchasePriceDto"];
                };
            };
        };
    };
}
