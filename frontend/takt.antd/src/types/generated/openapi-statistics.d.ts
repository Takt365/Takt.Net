export interface paths {
    "/api/TaktAopLogs/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    UserName?: string;
                    OperType?: string;
                    TableName?: string;
                    PrimaryKeyId?: number;
                    OperTimeStart?: string;
                    OperTimeEnd?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktAopLogDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktAopLogDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktAopLogDto"];
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
    "/api/TaktAopLogs/{id}": {
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
                        "text/plain": components["schemas"]["TaktAopLogDto"];
                        "application/json": components["schemas"]["TaktAopLogDto"];
                        "text/json": components["schemas"]["TaktAopLogDto"];
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
    "/api/TaktAopLogs": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5DEE\u5F02\u65E5\u5FD7"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAopLogs/batch": {
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
    "/api/TaktAopLogs/export": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    UserName?: string;
                    OperType?: string;
                    TableName?: string;
                    PrimaryKeyId?: number;
                    OperTimeStart?: string;
                    OperTimeEnd?: string;
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
    "/api/TaktLoginLogs/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    UserName?: string;
                    LoginIp?: string;
                    LoginType?: string;
                    LoginStatus?: number;
                    LoginTimeStart?: string;
                    LoginTimeEnd?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktLoginLogDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktLoginLogDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktLoginLogDto"];
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
    "/api/TaktLoginLogs/{id}": {
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
                        "text/plain": components["schemas"]["TaktLoginLogDto"];
                        "application/json": components["schemas"]["TaktLoginLogDto"];
                        "text/json": components["schemas"]["TaktLoginLogDto"];
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
    "/api/TaktLoginLogs": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u767B\u5F55\u65E5\u5FD7"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktLoginLogs/batch": {
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
    "/api/TaktLoginLogs/export": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    UserName?: string;
                    LoginIp?: string;
                    LoginType?: string;
                    LoginStatus?: number;
                    LoginTimeStart?: string;
                    LoginTimeEnd?: string;
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
    "/api/TaktOperLogs/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    UserName?: string;
                    OperModule?: string;
                    OperType?: string;
                    OperStatus?: number;
                    OperTimeStart?: string;
                    OperTimeEnd?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktOperLogDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktOperLogDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktOperLogDto"];
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
    "/api/TaktOperLogs/{id}": {
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
                        "text/plain": components["schemas"]["TaktOperLogDto"];
                        "application/json": components["schemas"]["TaktOperLogDto"];
                        "text/json": components["schemas"]["TaktOperLogDto"];
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
    "/api/TaktOperLogs": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u64CD\u4F5C\u65E5\u5FD7"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktOperLogs/batch": {
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
    "/api/TaktOperLogs/export": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    UserName?: string;
                    OperModule?: string;
                    OperType?: string;
                    OperStatus?: number;
                    OperTimeStart?: string;
                    OperTimeEnd?: string;
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
    "/api/TaktQuartzLogs/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    UserName?: string;
                    JobName?: string;
                    JobGroup?: string;
                    TriggerName?: string;
                    TriggerGroup?: string;
                    ExecuteStatus?: number;
                    ExecuteTimeStart?: string;
                    ExecuteTimeEnd?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktQuartzLogDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktQuartzLogDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktQuartzLogDto"];
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
    "/api/TaktQuartzLogs/{id}": {
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
                        "text/plain": components["schemas"]["TaktQuartzLogDto"];
                        "application/json": components["schemas"]["TaktQuartzLogDto"];
                        "text/json": components["schemas"]["TaktQuartzLogDto"];
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
    "/api/TaktQuartzLogs": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u4EFB\u52A1\u65E5\u5FD7"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktQuartzLogs/batch": {
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
    "/api/TaktQuartzLogs/export": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    UserName?: string;
                    JobName?: string;
                    JobGroup?: string;
                    TriggerName?: string;
                    TriggerGroup?: string;
                    ExecuteStatus?: number;
                    ExecuteTimeStart?: string;
                    ExecuteTimeEnd?: string;
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
}
export type webhooks = Record<string, never>;
export interface components {
    schemas: {
        TaktAopLogDto: {
            /** Format: int64 */
            aopLogId?: number;
            userName?: string;
            operType?: string;
            tableName?: string;
            /** Format: int64 */
            primaryKeyId?: number | null;
            beforeData?: string | null;
            afterData?: string | null;
            diffData?: string | null;
            sqlStatement?: string | null;
            /** Format: date-time */
            operTime?: string;
            /** Format: int32 */
            costTime?: number;
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
        TaktCreateAopLogDto: {
            userName?: string;
            operType?: string;
            tableName?: string;
            /** Format: int64 */
            primaryKeyId?: number | null;
            beforeData?: string | null;
            afterData?: string | null;
            diffData?: string | null;
            sqlStatement?: string | null;
            /** Format: date-time */
            operTime?: string | null;
            /** Format: int32 */
            costTime?: number;
        };
        TaktCreateLoginLogDto: {
            userName?: string;
            loginIp?: string | null;
            loginLocation?: string | null;
            loginType?: string | null;
            userAgent?: string | null;
            /** Format: int32 */
            loginStatus?: number;
            loginMsg?: string | null;
            /** Format: date-time */
            loginTime?: string | null;
            /** Format: date-time */
            logoutTime?: string | null;
            /** Format: int32 */
            sessionDuration?: number | null;
        };
        TaktCreateOperLogDto: {
            userName?: string;
            operModule?: string | null;
            operType?: string | null;
            operMethod?: string | null;
            requestMethod?: string | null;
            operUrl?: string | null;
            requestParam?: string | null;
            jsonResult?: string | null;
            /** Format: int32 */
            operStatus?: number;
            errorMsg?: string | null;
            operIp?: string | null;
            operLocation?: string | null;
            /** Format: date-time */
            operTime?: string | null;
            /** Format: int32 */
            costTime?: number;
        };
        TaktCreateQuartzLogDto: {
            userName?: string;
            jobName?: string;
            jobGroup?: string;
            triggerName?: string;
            triggerGroup?: string;
            /** Format: int32 */
            executeStatus?: number;
            executeResult?: string | null;
            errorMsg?: string | null;
            /** Format: date-time */
            executeTime?: string | null;
            /** Format: int32 */
            costTime?: number;
            jobData?: string | null;
            /** Format: date-time */
            nextFireTime?: string | null;
            /** Format: date-time */
            previousFireTime?: string | null;
        };
        TaktLoginLogDto: {
            /** Format: int64 */
            loginLogId?: number;
            userName?: string;
            loginIp?: string | null;
            loginLocation?: string | null;
            loginType?: string | null;
            userAgent?: string | null;
            /** Format: int32 */
            loginStatus?: number;
            loginMsg?: string | null;
            /** Format: date-time */
            loginTime?: string;
            /** Format: date-time */
            logoutTime?: string | null;
            /** Format: int32 */
            sessionDuration?: number | null;
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
        TaktOperLogDto: {
            /** Format: int64 */
            operLogId?: number;
            userName?: string;
            operModule?: string | null;
            operType?: string | null;
            operMethod?: string | null;
            requestMethod?: string | null;
            operUrl?: string | null;
            requestParam?: string | null;
            jsonResult?: string | null;
            /** Format: int32 */
            operStatus?: number;
            errorMsg?: string | null;
            operIp?: string | null;
            operLocation?: string | null;
            /** Format: date-time */
            operTime?: string;
            /** Format: int32 */
            costTime?: number;
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
        TaktPagedResultOfTaktAopLogDto: {
            data?: components["schemas"]["TaktAopLogDto"][];
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
        TaktPagedResultOfTaktLoginLogDto: {
            data?: components["schemas"]["TaktLoginLogDto"][];
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
        TaktPagedResultOfTaktOperLogDto: {
            data?: components["schemas"]["TaktOperLogDto"][];
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
        TaktPagedResultOfTaktQuartzLogDto: {
            data?: components["schemas"]["TaktQuartzLogDto"][];
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
        TaktQuartzLogDto: {
            /** Format: int64 */
            quartzLogId?: number;
            userName?: string;
            jobName?: string;
            jobGroup?: string;
            triggerName?: string;
            triggerGroup?: string;
            /** Format: int32 */
            executeStatus?: number;
            executeResult?: string | null;
            errorMsg?: string | null;
            /** Format: date-time */
            executeTime?: string;
            /** Format: int32 */
            costTime?: number;
            jobData?: string | null;
            /** Format: date-time */
            nextFireTime?: string | null;
            /** Format: date-time */
            previousFireTime?: string | null;
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
    };
    responses: never;
    parameters: never;
    requestBodies: never;
    headers: never;
    pathItems: never;
}
export type $defs = Record<string, never>;
export interface operations {
    "\u5DEE\u5F02\u65E5\u5FD7": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktCreateAopLogDto"];
                "application/json": components["schemas"]["TaktCreateAopLogDto"];
                "text/json": components["schemas"]["TaktCreateAopLogDto"];
                "application/*+json": components["schemas"]["TaktCreateAopLogDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktAopLogDto"];
                    "application/json": components["schemas"]["TaktAopLogDto"];
                    "text/json": components["schemas"]["TaktAopLogDto"];
                };
            };
        };
    };
    "\u767B\u5F55\u65E5\u5FD7": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktCreateLoginLogDto"];
                "application/json": components["schemas"]["TaktCreateLoginLogDto"];
                "text/json": components["schemas"]["TaktCreateLoginLogDto"];
                "application/*+json": components["schemas"]["TaktCreateLoginLogDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktLoginLogDto"];
                    "application/json": components["schemas"]["TaktLoginLogDto"];
                    "text/json": components["schemas"]["TaktLoginLogDto"];
                };
            };
        };
    };
    "\u64CD\u4F5C\u65E5\u5FD7": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktCreateOperLogDto"];
                "application/json": components["schemas"]["TaktCreateOperLogDto"];
                "text/json": components["schemas"]["TaktCreateOperLogDto"];
                "application/*+json": components["schemas"]["TaktCreateOperLogDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktOperLogDto"];
                    "application/json": components["schemas"]["TaktOperLogDto"];
                    "text/json": components["schemas"]["TaktOperLogDto"];
                };
            };
        };
    };
    "\u4EFB\u52A1\u65E5\u5FD7": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktCreateQuartzLogDto"];
                "application/json": components["schemas"]["TaktCreateQuartzLogDto"];
                "text/json": components["schemas"]["TaktCreateQuartzLogDto"];
                "application/*+json": components["schemas"]["TaktCreateQuartzLogDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktQuartzLogDto"];
                    "application/json": components["schemas"]["TaktQuartzLogDto"];
                    "text/json": components["schemas"]["TaktQuartzLogDto"];
                };
            };
        };
    };
}
