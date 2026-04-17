export interface paths {
    "/api/TaktFlowForms/form-config-from-table": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    configId?: string;
                    tableName?: string;
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
                        "text/plain": string;
                        "application/json": string;
                        "text/json": string;
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
    "/api/TaktFlowForms/database-configs": {
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
                        "text/plain": components["schemas"]["TaktDatabaseInfo"][];
                        "application/json": components["schemas"]["TaktDatabaseInfo"][];
                        "text/json": components["schemas"]["TaktDatabaseInfo"][];
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
    "/api/TaktFlowForms/database-tables": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    configId?: string;
                    requiredColumn?: string;
                    excludedColumn?: string;
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
                        "text/plain": components["schemas"]["TaktDatabaseTableInfo"][];
                        "application/json": components["schemas"]["TaktDatabaseTableInfo"][];
                        "text/json": components["schemas"]["TaktDatabaseTableInfo"][];
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
    "/api/TaktFlowForms/table-columns": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    configId?: string;
                    tableName?: string;
                    includeAuditColumns?: boolean;
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
                        "text/plain": components["schemas"]["TaktDatabaseColumnInfo"][];
                        "application/json": components["schemas"]["TaktDatabaseColumnInfo"][];
                        "text/json": components["schemas"]["TaktDatabaseColumnInfo"][];
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
    "/api/TaktFlowForms/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    FormCode?: string;
                    FormName?: string;
                    FormCategory?: number;
                    FormStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktFlowFormDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktFlowFormDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktFlowFormDto"];
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
    "/api/TaktFlowForms/{id}": {
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
                        "text/plain": components["schemas"]["TaktFlowFormDto"];
                        "application/json": components["schemas"]["TaktFlowFormDto"];
                        "text/json": components["schemas"]["TaktFlowFormDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktFlowFormUpdateDto"];
                    "application/json": components["schemas"]["TaktFlowFormUpdateDto"];
                    "text/json": components["schemas"]["TaktFlowFormUpdateDto"];
                    "application/*+json": components["schemas"]["TaktFlowFormUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktFlowFormDto"];
                        "application/json": components["schemas"]["TaktFlowFormDto"];
                        "text/json": components["schemas"]["TaktFlowFormDto"];
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
    "/api/TaktFlowForms/by-code/{formCode}": {
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
                    formCode: string;
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
                        "text/plain": components["schemas"]["TaktFlowFormDto"];
                        "application/json": components["schemas"]["TaktFlowFormDto"];
                        "text/json": components["schemas"]["TaktFlowFormDto"];
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
    "/api/TaktFlowForms": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowFormCreateDto"];
                    "application/json": components["schemas"]["TaktFlowFormCreateDto"];
                    "text/json": components["schemas"]["TaktFlowFormCreateDto"];
                    "application/*+json": components["schemas"]["TaktFlowFormCreateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktFlowFormDto"];
                        "application/json": components["schemas"]["TaktFlowFormDto"];
                        "text/json": components["schemas"]["TaktFlowFormDto"];
                    };
                };
            };
        };
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktFlowForms/delete": {
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
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktFlowForms/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowFormStatusDto"];
                    "application/json": components["schemas"]["TaktFlowFormStatusDto"];
                    "text/json": components["schemas"]["TaktFlowFormStatusDto"];
                    "application/*+json": components["schemas"]["TaktFlowFormStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktFlowFormDto"];
                        "application/json": components["schemas"]["TaktFlowFormDto"];
                        "text/json": components["schemas"]["TaktFlowFormDto"];
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
    "/api/TaktFlowForms/template": {
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
    "/api/TaktFlowForms/import": {
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
    "/api/TaktFlowForms/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowFormQueryDto"];
                    "application/json": components["schemas"]["TaktFlowFormQueryDto"];
                    "text/json": components["schemas"]["TaktFlowFormQueryDto"];
                    "application/*+json": components["schemas"]["TaktFlowFormQueryDto"];
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
    "/api/TaktFlowInstances/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ProcessKey?: string;
                    InstanceCode?: string;
                    InstanceStatus?: number;
                    MyStartedOnly?: boolean;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktFlowInstanceDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktFlowInstanceDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktFlowInstanceDto"];
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
    "/api/TaktFlowInstances/{id}": {
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
                        "text/plain": components["schemas"]["TaktFlowInstanceDetailDto"];
                        "application/json": components["schemas"]["TaktFlowInstanceDetailDto"];
                        "text/json": components["schemas"]["TaktFlowInstanceDetailDto"];
                    };
                };
            };
        };
        put?: never;
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
    "/api/TaktFlowInstances/{id}/histories": {
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
                        "text/plain": components["schemas"]["TaktFlowOperationHistoryItemDto"][];
                        "application/json": components["schemas"]["TaktFlowOperationHistoryItemDto"][];
                        "text/json": components["schemas"]["TaktFlowOperationHistoryItemDto"][];
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
    "/api/TaktFlowInstances/todo": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ProcessKey?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktFlowTodoItemDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktFlowTodoItemDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktFlowTodoItemDto"];
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
    "/api/TaktFlowInstances/my": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ProcessKey?: string;
                    InstanceCode?: string;
                    InstanceStatus?: number;
                    MyStartedOnly?: boolean;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktFlowInstanceDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktFlowInstanceDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktFlowInstanceDto"];
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
    "/api/TaktFlowInstances/processed": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ProcessKey?: string;
                    InstanceCode?: string;
                    InstanceStatus?: number;
                    MyStartedOnly?: boolean;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktFlowInstanceDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktFlowInstanceDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktFlowInstanceDto"];
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
    "/api/TaktFlowInstances/export": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ProcessKey?: string;
                    InstanceCode?: string;
                    InstanceStatus?: number;
                    MyStartedOnly?: boolean;
                    PageIndex?: number;
                    PageSize?: number;
                    KeyWords?: string;
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
    "/api/TaktFlowInstances/todo/export": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ProcessKey?: string;
                    PageIndex?: number;
                    PageSize?: number;
                    KeyWords?: string;
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
    "/api/TaktFlowInstances/my/export": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ProcessKey?: string;
                    InstanceCode?: string;
                    InstanceStatus?: number;
                    MyStartedOnly?: boolean;
                    PageIndex?: number;
                    PageSize?: number;
                    KeyWords?: string;
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
    "/api/TaktFlowInstances/processed/export": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ProcessKey?: string;
                    InstanceCode?: string;
                    InstanceStatus?: number;
                    MyStartedOnly?: boolean;
                    PageIndex?: number;
                    PageSize?: number;
                    KeyWords?: string;
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
    "/api/TaktFlowInstances/start": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowStartDto"];
                    "application/json": components["schemas"]["TaktFlowStartDto"];
                    "text/json": components["schemas"]["TaktFlowStartDto"];
                    "application/*+json": components["schemas"]["TaktFlowStartDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktFlowStartResultDto"];
                        "application/json": components["schemas"]["TaktFlowStartResultDto"];
                        "text/json": components["schemas"]["TaktFlowStartResultDto"];
                    };
                };
            };
        };
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktFlowInstances/create-draft": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowStartDto"];
                    "application/json": components["schemas"]["TaktFlowStartDto"];
                    "text/json": components["schemas"]["TaktFlowStartDto"];
                    "application/*+json": components["schemas"]["TaktFlowStartDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktFlowStartResultDto"];
                        "application/json": components["schemas"]["TaktFlowStartResultDto"];
                        "text/json": components["schemas"]["TaktFlowStartResultDto"];
                    };
                };
            };
        };
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktFlowInstances/start-from-draft/{id}": {
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
                        "text/plain": components["schemas"]["TaktFlowStartResultDto"];
                        "application/json": components["schemas"]["TaktFlowStartResultDto"];
                        "text/json": components["schemas"]["TaktFlowStartResultDto"];
                    };
                };
            };
        };
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktFlowInstances/complete": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowCompleteDto"];
                    "application/json": components["schemas"]["TaktFlowCompleteDto"];
                    "text/json": components["schemas"]["TaktFlowCompleteDto"];
                    "application/*+json": components["schemas"]["TaktFlowCompleteDto"];
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
    "/api/TaktFlowInstances/revoke": {
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
                    "application/json-patch+json": components["schemas"]["RevokeRequest"];
                    "application/json": components["schemas"]["RevokeRequest"];
                    "text/json": components["schemas"]["RevokeRequest"];
                    "application/*+json": components["schemas"]["RevokeRequest"];
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
    "/api/TaktFlowInstances/suspend": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowSuspendDto"];
                    "application/json": components["schemas"]["TaktFlowSuspendDto"];
                    "text/json": components["schemas"]["TaktFlowSuspendDto"];
                    "application/*+json": components["schemas"]["TaktFlowSuspendDto"];
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
    "/api/TaktFlowInstances/resume": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowResumeDto"];
                    "application/json": components["schemas"]["TaktFlowResumeDto"];
                    "text/json": components["schemas"]["TaktFlowResumeDto"];
                    "application/*+json": components["schemas"]["TaktFlowResumeDto"];
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
    "/api/TaktFlowInstances/terminate": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowTerminateDto"];
                    "application/json": components["schemas"]["TaktFlowTerminateDto"];
                    "text/json": components["schemas"]["TaktFlowTerminateDto"];
                    "application/*+json": components["schemas"]["TaktFlowTerminateDto"];
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
    "/api/TaktFlowInstances/transfer": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowTransferDto"];
                    "application/json": components["schemas"]["TaktFlowTransferDto"];
                    "text/json": components["schemas"]["TaktFlowTransferDto"];
                    "application/*+json": components["schemas"]["TaktFlowTransferDto"];
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
    "/api/TaktFlowInstances/add-sign": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowAddApproversDto"];
                    "application/json": components["schemas"]["TaktFlowAddApproversDto"];
                    "text/json": components["schemas"]["TaktFlowAddApproversDto"];
                    "application/*+json": components["schemas"]["TaktFlowAddApproversDto"];
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
    "/api/TaktFlowInstances/reduce-sign": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowReduceApprovalDto"];
                    "application/json": components["schemas"]["TaktFlowReduceApprovalDto"];
                    "text/json": components["schemas"]["TaktFlowReduceApprovalDto"];
                    "application/*+json": components["schemas"]["TaktFlowReduceApprovalDto"];
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
    "/api/TaktFlowInstances/update": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowInstanceUpdateDto"];
                    "application/json": components["schemas"]["TaktFlowInstanceUpdateDto"];
                    "text/json": components["schemas"]["TaktFlowInstanceUpdateDto"];
                    "application/*+json": components["schemas"]["TaktFlowInstanceUpdateDto"];
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
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktFlowInstances/undo-verification": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowUndoVerificationDto"];
                    "application/json": components["schemas"]["TaktFlowUndoVerificationDto"];
                    "text/json": components["schemas"]["TaktFlowUndoVerificationDto"];
                    "application/*+json": components["schemas"]["TaktFlowUndoVerificationDto"];
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
    "/api/TaktFlowInstances/delete": {
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
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktFlowInstances/verify-ccflow": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    processKey?: string;
                    processTitle?: string;
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
                        "text/plain": components["schemas"]["TaktFlowVerifyCcflowReportDto"];
                        "application/json": components["schemas"]["TaktFlowVerifyCcflowReportDto"];
                        "text/json": components["schemas"]["TaktFlowVerifyCcflowReportDto"];
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
    "/api/TaktFlowInstances/verify": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    processKey?: string;
                    processTitle?: string;
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
                        "text/plain": components["schemas"]["WorkflowVerifyResult"];
                        "application/json": components["schemas"]["WorkflowVerifyResult"];
                        "text/json": components["schemas"]["WorkflowVerifyResult"];
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
    "/api/TaktFlowSchemes/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ProcessKey?: string;
                    ProcessName?: string;
                    ProcessStatus?: number;
                    ProcessCategory?: number;
                    FormCode?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktFlowSchemeDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktFlowSchemeDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktFlowSchemeDto"];
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
    "/api/TaktFlowSchemes/{id}": {
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
                        "text/plain": components["schemas"]["TaktFlowSchemeDto"];
                        "application/json": components["schemas"]["TaktFlowSchemeDto"];
                        "text/json": components["schemas"]["TaktFlowSchemeDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktFlowSchemeUpdateDto"];
                    "application/json": components["schemas"]["TaktFlowSchemeUpdateDto"];
                    "text/json": components["schemas"]["TaktFlowSchemeUpdateDto"];
                    "application/*+json": components["schemas"]["TaktFlowSchemeUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktFlowSchemeDto"];
                        "application/json": components["schemas"]["TaktFlowSchemeDto"];
                        "text/json": components["schemas"]["TaktFlowSchemeDto"];
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
    "/api/TaktFlowSchemes/by-key/{processKey}": {
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
                    processKey: string;
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
                        "text/plain": components["schemas"]["TaktFlowSchemeDto"];
                        "application/json": components["schemas"]["TaktFlowSchemeDto"];
                        "text/json": components["schemas"]["TaktFlowSchemeDto"];
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
    "/api/TaktFlowSchemes": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowSchemeCreateDto"];
                    "application/json": components["schemas"]["TaktFlowSchemeCreateDto"];
                    "text/json": components["schemas"]["TaktFlowSchemeCreateDto"];
                    "application/*+json": components["schemas"]["TaktFlowSchemeCreateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktFlowSchemeDto"];
                        "application/json": components["schemas"]["TaktFlowSchemeDto"];
                        "text/json": components["schemas"]["TaktFlowSchemeDto"];
                    };
                };
            };
        };
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktFlowSchemes/delete": {
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
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktFlowSchemes/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowSchemeStatusDto"];
                    "application/json": components["schemas"]["TaktFlowSchemeStatusDto"];
                    "text/json": components["schemas"]["TaktFlowSchemeStatusDto"];
                    "application/*+json": components["schemas"]["TaktFlowSchemeStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktFlowSchemeDto"];
                        "application/json": components["schemas"]["TaktFlowSchemeDto"];
                        "text/json": components["schemas"]["TaktFlowSchemeDto"];
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
    "/api/TaktFlowSchemes/template": {
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
    "/api/TaktFlowSchemes/import": {
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
    "/api/TaktFlowSchemes/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktFlowSchemeQueryDto"];
                    "application/json": components["schemas"]["TaktFlowSchemeQueryDto"];
                    "text/json": components["schemas"]["TaktFlowSchemeQueryDto"];
                    "application/*+json": components["schemas"]["TaktFlowSchemeQueryDto"];
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
        RevokeRequest: {
            instanceCode?: string | null;
            /** Format: int64 */
            flowInstanceId?: number | null;
            description?: string | null;
        };
        TaktDatabaseColumnInfo: {
            dbColumnName?: string;
            columnDescription?: string | null;
            dataType?: string;
            /** Format: int32 */
            length?: number;
            /** Format: int32 */
            decimalDigits?: number;
            isPrimarykey?: boolean;
            isIdentity?: boolean;
            isNullable?: boolean;
        };
        TaktDatabaseInfo: {
            configId?: string;
            displayName?: string;
            dataBaseName?: string;
        };
        TaktDatabaseTableInfo: {
            tableName?: string;
            tableComment?: string | null;
        };
        TaktFlowAddApproverDto: {
            /** Format: int64 */
            addApproverId?: number;
            /** Format: int64 */
            instanceId?: number;
            activityId?: string;
            /** Format: int64 */
            approverUserId?: number;
            approverUserName?: string;
            approveType?: string | null;
            /** Format: int32 */
            orderNo?: number | null;
            /** Format: int32 */
            status?: number;
            verifyComment?: string | null;
            /** Format: date-time */
            verifyTime?: string | null;
            reason?: string | null;
            /** Format: int64 */
            createUserId?: number | null;
            createUserName?: string | null;
            returnToSignNode?: boolean | null;
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
        TaktFlowAddApproverItemDto: {
            /** Format: int64 */
            approverUserId?: number;
            approverUserName?: string;
            /** Format: int32 */
            orderNo?: number | null;
        };
        TaktFlowAddApproversDto: {
            approvers?: components["schemas"]["TaktFlowAddApproverItemDto"][];
            approveType?: string;
            reason?: string | null;
            returnToSignNode?: boolean;
            /** Format: int64 */
            flowInstanceId?: number | null;
            instanceCode?: string | null;
        };
        TaktFlowCompleteDto: {
            /** Format: int64 */
            flowInstanceId?: number | null;
            instanceCode?: string | null;
            comment?: string | null;
            approved?: boolean;
            frmData?: string | null;
            nodeRejectStep?: string | null;
            selectedAssigneeIds?: string | null;
        };
        TaktFlowFormCreateDto: {
            formCode?: string;
            formName?: string;
            /** Format: int32 */
            formCategory?: number;
            /** Format: int32 */
            formType?: number;
            formConfig?: string | null;
            formTemplate?: string | null;
            formVersion?: string;
            /** Format: int32 */
            isDatasource?: number;
            relatedDataBaseName?: string | null;
            relatedTableName?: string | null;
            relatedFormField?: string | null;
            formTheme?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            formStatus?: number;
        };
        TaktFlowFormDto: {
            /** Format: int64 */
            formId?: number;
            formCode?: string;
            formName?: string;
            /** Format: int32 */
            formCategory?: number;
            /** Format: int32 */
            formType?: number;
            formConfig?: string | null;
            formTemplate?: string | null;
            formVersion?: string;
            /** Format: int32 */
            isDatasource?: number;
            relatedDataBaseName?: string | null;
            relatedTableName?: string | null;
            relatedFormField?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            formStatus?: number;
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
        TaktFlowFormQueryDto: {
            formCode?: string | null;
            formName?: string | null;
            /** Format: int32 */
            formCategory?: number | null;
            /** Format: int32 */
            formStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktFlowFormStatusDto: {
            /** Format: int64 */
            formId?: number;
            /** Format: int32 */
            formStatus?: number;
            remark?: string | null;
        };
        TaktFlowFormUpdateDto: {
            /** Format: int64 */
            formId?: number;
            formCode?: string;
            formName?: string;
            /** Format: int32 */
            formCategory?: number;
            /** Format: int32 */
            formType?: number;
            formConfig?: string | null;
            formTemplate?: string | null;
            formVersion?: string;
            /** Format: int32 */
            isDatasource?: number;
            relatedDataBaseName?: string | null;
            relatedTableName?: string | null;
            relatedFormField?: string | null;
            formTheme?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            formStatus?: number;
        };
        TaktFlowHistoryItemDto: {
            fromNodeName?: string;
            toNodeName?: string;
            transitionUserName?: string;
            /** Format: date-time */
            transitionTime?: string;
            transitionComment?: string | null;
        };
        TaktFlowInstanceDetailDto: {
            history?: components["schemas"]["TaktFlowHistoryItemDto"][];
            canVerify?: boolean;
            canUndoVerify?: boolean;
            pendingAddApprovers?: components["schemas"]["TaktFlowAddApproverDto"][];
            /** Format: int64 */
            instanceId?: number;
            instanceCode?: string;
            processKey?: string;
            processName?: string;
            /** Format: int64 */
            schemeId?: number;
            businessKey?: string | null;
            businessType?: string | null;
            /** Format: int64 */
            startUserId?: number;
            startUserName?: string;
            /** Format: int64 */
            startDeptId?: number | null;
            startDeptName?: string | null;
            /** Format: date-time */
            startTime?: string;
            /** Format: date-time */
            endTime?: string | null;
            currentNodeId?: string | null;
            currentNodeName?: string | null;
            activityName?: string | null;
            previousNodeId?: string | null;
            makerList?: string | null;
            frmData?: string | null;
            /** Format: int32 */
            instanceStatus?: number;
            /** Format: int32 */
            isSuspended?: number;
            /** Format: date-time */
            suspendTime?: string | null;
            suspendReason?: string | null;
            /** Format: int32 */
            priority?: number;
            processTitle?: string | null;
            /** Format: int64 */
            formId?: number | null;
            formCode?: string | null;
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
        TaktFlowInstanceDto: {
            /** Format: int64 */
            instanceId?: number;
            instanceCode?: string;
            processKey?: string;
            processName?: string;
            /** Format: int64 */
            schemeId?: number;
            businessKey?: string | null;
            businessType?: string | null;
            /** Format: int64 */
            startUserId?: number;
            startUserName?: string;
            /** Format: int64 */
            startDeptId?: number | null;
            startDeptName?: string | null;
            /** Format: date-time */
            startTime?: string;
            /** Format: date-time */
            endTime?: string | null;
            currentNodeId?: string | null;
            currentNodeName?: string | null;
            activityName?: string | null;
            previousNodeId?: string | null;
            makerList?: string | null;
            frmData?: string | null;
            /** Format: int32 */
            instanceStatus?: number;
            /** Format: int32 */
            isSuspended?: number;
            /** Format: date-time */
            suspendTime?: string | null;
            suspendReason?: string | null;
            /** Format: int32 */
            priority?: number;
            processTitle?: string | null;
            /** Format: int64 */
            formId?: number | null;
            formCode?: string | null;
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
        TaktFlowInstanceUpdateDto: {
            /** Format: int64 */
            id?: number;
            processTitle?: string | null;
            frmData?: string | null;
        };
        TaktFlowOperationHistoryItemDto: {
            content?: string;
            /** Format: int64 */
            createUserId?: number;
            createUserName?: string;
            /** Format: date-time */
            createdAt?: string;
        };
        TaktFlowReduceApprovalDto: {
            /** Format: int64 */
            addApproverId?: number;
            /** Format: int64 */
            flowInstanceId?: number | null;
            instanceCode?: string | null;
        };
        TaktFlowResumeDto: {
            /** Format: int64 */
            flowInstanceId?: number | null;
            instanceCode?: string | null;
        };
        TaktFlowSchemeCreateDto: {
            processKey?: string;
            processName?: string;
            /** Format: int32 */
            processCategory?: number;
            processDescription?: string | null;
            /** Format: int64 */
            formId?: number | null;
            formCode?: string | null;
            processContent?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            processStatus?: number;
        };
        TaktFlowSchemeDto: {
            /** Format: int64 */
            schemeId?: number;
            processKey?: string;
            processName?: string;
            /** Format: int32 */
            processCategory?: number;
            /** Format: int32 */
            processVersion?: number;
            processDescription?: string | null;
            /** Format: int64 */
            formId?: number | null;
            formCode?: string | null;
            processContent?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            processStatus?: number;
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
        TaktFlowSchemeQueryDto: {
            processKey?: string | null;
            processName?: string | null;
            /** Format: int32 */
            processStatus?: number | null;
            /** Format: int32 */
            processCategory?: number | null;
            formCode?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktFlowSchemeStatusDto: {
            /** Format: int64 */
            schemeId?: number;
            /** Format: int32 */
            processStatus?: number;
            remark?: string | null;
        };
        TaktFlowSchemeUpdateDto: {
            /** Format: int64 */
            schemeId?: number;
            processKey?: string;
            processName?: string;
            /** Format: int32 */
            processCategory?: number;
            processDescription?: string | null;
            /** Format: int64 */
            formId?: number | null;
            formCode?: string | null;
            processContent?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            processStatus?: number;
        };
        TaktFlowStartDto: {
            processKey?: string;
            businessKey?: string | null;
            businessType?: string | null;
            processTitle?: string | null;
            frmData?: string | null;
            /** Format: int64 */
            flowInstanceId?: number | null;
        };
        TaktFlowStartResultDto: {
            instanceCode?: string;
            /** Format: int64 */
            instanceId?: number;
            processKey?: string;
            processName?: string;
        };
        TaktFlowSuspendDto: {
            reason?: string | null;
            /** Format: int64 */
            flowInstanceId?: number | null;
            instanceCode?: string | null;
        };
        TaktFlowTerminateDto: {
            reason?: string | null;
            /** Format: int64 */
            flowInstanceId?: number | null;
            instanceCode?: string | null;
        };
        TaktFlowTodoItemDto: {
            /** Format: int64 */
            instanceId?: number;
            instanceCode?: string;
            processKey?: string;
            processName?: string;
            nodeId?: string;
            nodeName?: string;
            processTitle?: string | null;
            startUserName?: string;
            /** Format: date-time */
            startTime?: string;
        };
        TaktFlowTransferDto: {
            /** Format: int64 */
            toUserId?: number;
            toUserName?: string;
            comment?: string | null;
            /** Format: int64 */
            flowInstanceId?: number | null;
            instanceCode?: string | null;
        };
        TaktFlowUndoVerificationDto: {
            /** Format: int64 */
            flowInstanceId?: number;
        };
        TaktFlowVerifyCcflowReportDto: {
            processKey?: string;
            /** Format: date-time */
            verifyTime?: string;
            scenarios?: components["schemas"]["TaktFlowVerifyScenarioResultDto"][];
            allPassed?: boolean;
        };
        TaktFlowVerifyScenarioResultDto: {
            scenarioName?: string;
            ok?: boolean;
            message?: string | null;
            steps?: string[];
        };
        TaktPagedResultOfTaktFlowFormDto: {
            data?: components["schemas"]["TaktFlowFormDto"][];
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
        TaktPagedResultOfTaktFlowInstanceDto: {
            data?: components["schemas"]["TaktFlowInstanceDto"][];
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
        TaktPagedResultOfTaktFlowSchemeDto: {
            data?: components["schemas"]["TaktFlowSchemeDto"][];
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
        TaktPagedResultOfTaktFlowTodoItemDto: {
            data?: components["schemas"]["TaktFlowTodoItemDto"][];
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
        WorkflowVerifyResult: {
            ok?: boolean;
            message?: string | null;
            instanceCode?: string | null;
            /** Format: int64 */
            instanceId?: number;
            steps?: string[];
        };
    };
    responses: never;
    parameters: never;
    requestBodies: never;
    headers: never;
    pathItems: never;
}
export type $defs = Record<string, never>;
export type operations = Record<string, never>;
