export interface paths {
    "/api/TaktAuth/connect/token": {
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
            requestBody?: never;
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "application/json": unknown;
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
    "/api/TaktAuth/login": {
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
                    "application/json-patch+json": components["schemas"]["TaktLoginDto"];
                    "application/json": components["schemas"]["TaktLoginDto"];
                    "text/json": components["schemas"]["TaktLoginDto"];
                    "application/*+json": components["schemas"]["TaktLoginDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktApiResultOfTaktLoginResponseDto"];
                        "application/json": components["schemas"]["TaktApiResultOfTaktLoginResponseDto"];
                        "text/json": components["schemas"]["TaktApiResultOfTaktLoginResponseDto"];
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
    "/api/TaktAuth/refresh-token": {
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
                    "application/json-patch+json": components["schemas"]["TaktRefreshTokenDto"];
                    "application/json": components["schemas"]["TaktRefreshTokenDto"];
                    "text/json": components["schemas"]["TaktRefreshTokenDto"];
                    "application/*+json": components["schemas"]["TaktRefreshTokenDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktApiResultOfTaktLoginResponseDto"];
                        "application/json": components["schemas"]["TaktApiResultOfTaktLoginResponseDto"];
                        "text/json": components["schemas"]["TaktApiResultOfTaktLoginResponseDto"];
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
    "/api/TaktAuth/logout": {
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
                    "application/json-patch+json": string;
                    "application/json": string;
                    "text/json": string;
                    "application/*+json": string;
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktApiResultOfObject"];
                        "application/json": components["schemas"]["TaktApiResultOfObject"];
                        "text/json": components["schemas"]["TaktApiResultOfObject"];
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
    "/api/TaktAuth/userinfo": {
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
                        "text/plain": components["schemas"]["TaktApiResultOfTaktUserInfoDto"];
                        "application/json": components["schemas"]["TaktApiResultOfTaktUserInfoDto"];
                        "text/json": components["schemas"]["TaktApiResultOfTaktUserInfoDto"];
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
    "/api/TaktHealth/check": {
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
    "/api/TaktHealth": {
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
    "/api/TaktHealth/detailed": {
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
    "/api/TaktHealth/signalr": {
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
    "/api/TaktMenus/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    MenuName?: string;
                    MenuCode?: string;
                    ParentId?: number;
                    MenuType?: number;
                    MenuStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktMenuDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktMenuDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktMenuDto"];
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
    "/api/TaktMenus/{id}": {
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
                        "text/plain": components["schemas"]["TaktMenuDto"];
                        "application/json": components["schemas"]["TaktMenuDto"];
                        "text/json": components["schemas"]["TaktMenuDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktMenuUpdateDto"];
                    "application/json": components["schemas"]["TaktMenuUpdateDto"];
                    "text/json": components["schemas"]["TaktMenuUpdateDto"];
                    "application/*+json": components["schemas"]["TaktMenuUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktMenuDto"];
                        "application/json": components["schemas"]["TaktMenuDto"];
                        "text/json": components["schemas"]["TaktMenuDto"];
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
    "/api/TaktMenus/tree-options": {
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
                        "text/plain": components["schemas"]["TaktApiResultOfListOfTaktTreeSelectOption"];
                        "application/json": components["schemas"]["TaktApiResultOfListOfTaktTreeSelectOption"];
                        "text/json": components["schemas"]["TaktApiResultOfListOfTaktTreeSelectOption"];
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
    "/api/TaktMenus/module-name-options": {
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
                        "text/plain": components["schemas"]["TaktApiResultOfListOfTaktMenuTreeDto"];
                        "application/json": components["schemas"]["TaktApiResultOfListOfTaktMenuTreeDto"];
                        "text/json": components["schemas"]["TaktApiResultOfListOfTaktMenuTreeDto"];
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
    "/api/TaktMenus/current-tree": {
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
                        "text/plain": components["schemas"]["TaktApiResultOfListOfTaktMenuTreeDto"];
                        "application/json": components["schemas"]["TaktApiResultOfListOfTaktMenuTreeDto"];
                        "text/json": components["schemas"]["TaktApiResultOfListOfTaktMenuTreeDto"];
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
    "/api/TaktMenus": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u83DC\u5355\u7BA1\u7406"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktMenus/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktMenuStatusDto"];
                    "application/json": components["schemas"]["TaktMenuStatusDto"];
                    "text/json": components["schemas"]["TaktMenuStatusDto"];
                    "application/*+json": components["schemas"]["TaktMenuStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktMenuDto"];
                        "application/json": components["schemas"]["TaktMenuDto"];
                        "text/json": components["schemas"]["TaktMenuDto"];
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
    "/api/TaktMenus/template": {
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
    "/api/TaktMenus/import": {
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
    "/api/TaktMenus/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktMenuQueryDto"];
                    "application/json": components["schemas"]["TaktMenuQueryDto"];
                    "text/json": components["schemas"]["TaktMenuQueryDto"];
                    "application/*+json": components["schemas"]["TaktMenuQueryDto"];
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
    "/api/TaktRoles/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    RoleName?: string;
                    RoleCode?: string;
                    RoleStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktRoleDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktRoleDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktRoleDto"];
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
    "/api/TaktRoles/{id}": {
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
                        "text/plain": components["schemas"]["TaktRoleDto"];
                        "application/json": components["schemas"]["TaktRoleDto"];
                        "text/json": components["schemas"]["TaktRoleDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktRoleUpdateDto"];
                    "application/json": components["schemas"]["TaktRoleUpdateDto"];
                    "text/json": components["schemas"]["TaktRoleUpdateDto"];
                    "application/*+json": components["schemas"]["TaktRoleUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktRoleDto"];
                        "application/json": components["schemas"]["TaktRoleDto"];
                        "text/json": components["schemas"]["TaktRoleDto"];
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
    "/api/TaktRoles/options": {
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
    "/api/TaktRoles": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u89D2\u8272\u7BA1\u7406"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktRoles/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktRoleStatusDto"];
                    "application/json": components["schemas"]["TaktRoleStatusDto"];
                    "text/json": components["schemas"]["TaktRoleStatusDto"];
                    "application/*+json": components["schemas"]["TaktRoleStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktRoleDto"];
                        "application/json": components["schemas"]["TaktRoleDto"];
                        "text/json": components["schemas"]["TaktRoleDto"];
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
    "/api/TaktRoles/{roleId}/menus": {
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
                    roleId: number;
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
                        "text/plain": components["schemas"]["TaktRoleMenuDto"][];
                        "application/json": components["schemas"]["TaktRoleMenuDto"][];
                        "text/json": components["schemas"]["TaktRoleMenuDto"][];
                    };
                };
            };
        };
        put: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    roleId: number;
                };
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
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktRoles/{roleId}/depts": {
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
                    roleId: number;
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
                        "text/plain": components["schemas"]["TaktRoleDeptDto"][];
                        "application/json": components["schemas"]["TaktRoleDeptDto"][];
                        "text/json": components["schemas"]["TaktRoleDeptDto"][];
                    };
                };
            };
        };
        put: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    roleId: number;
                };
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
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktRoles/{roleId}/users": {
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
                path: {
                    roleId: number;
                };
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
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktRoles/template": {
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
    "/api/TaktRoles/import": {
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
    "/api/TaktRoles/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktRoleQueryDto"];
                    "application/json": components["schemas"]["TaktRoleQueryDto"];
                    "text/json": components["schemas"]["TaktRoleQueryDto"];
                    "application/*+json": components["schemas"]["TaktRoleQueryDto"];
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
    "/api/TaktTenants/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    TenantName?: string;
                    TenantCode?: string;
                    ConfigId?: string;
                    TenantStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktTenantDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktTenantDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktTenantDto"];
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
    "/api/TaktTenants/{id}": {
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
                        "text/plain": components["schemas"]["TaktTenantDto"];
                        "application/json": components["schemas"]["TaktTenantDto"];
                        "text/json": components["schemas"]["TaktTenantDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktTenantUpdateDto"];
                    "application/json": components["schemas"]["TaktTenantUpdateDto"];
                    "text/json": components["schemas"]["TaktTenantUpdateDto"];
                    "application/*+json": components["schemas"]["TaktTenantUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktTenantDto"];
                        "application/json": components["schemas"]["TaktTenantDto"];
                        "text/json": components["schemas"]["TaktTenantDto"];
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
    "/api/TaktTenants/options": {
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
    "/api/TaktTenants": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u79DF\u6237"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktTenants/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktTenantStatusDto"];
                    "application/json": components["schemas"]["TaktTenantStatusDto"];
                    "text/json": components["schemas"]["TaktTenantStatusDto"];
                    "application/*+json": components["schemas"]["TaktTenantStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktTenantDto"];
                        "application/json": components["schemas"]["TaktTenantDto"];
                        "text/json": components["schemas"]["TaktTenantDto"];
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
    "/api/TaktTenants/{tenantId}/users": {
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
                    tenantId: number;
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
                        "text/plain": components["schemas"]["TaktUserTenantDto"][];
                        "application/json": components["schemas"]["TaktUserTenantDto"][];
                        "text/json": components["schemas"]["TaktUserTenantDto"][];
                    };
                };
            };
        };
        put: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    tenantId: number;
                };
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
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktTenants/template": {
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
    "/api/TaktTenants/import": {
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
    "/api/TaktTenants/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktTenantQueryDto"];
                    "application/json": components["schemas"]["TaktTenantQueryDto"];
                    "text/json": components["schemas"]["TaktTenantQueryDto"];
                    "application/*+json": components["schemas"]["TaktTenantQueryDto"];
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
    "/api/TaktUsers/list": {
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
                    UserEmail?: string;
                    UserPhone?: string;
                    UserStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktUserDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktUserDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktUserDto"];
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
    "/api/TaktUsers/{id}": {
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
                        "text/plain": components["schemas"]["TaktUserDto"];
                        "application/json": components["schemas"]["TaktUserDto"];
                        "text/json": components["schemas"]["TaktUserDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktUserUpdateDto"];
                    "application/json": components["schemas"]["TaktUserUpdateDto"];
                    "text/json": components["schemas"]["TaktUserUpdateDto"];
                    "application/*+json": components["schemas"]["TaktUserUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktUserDto"];
                        "application/json": components["schemas"]["TaktUserDto"];
                        "text/json": components["schemas"]["TaktUserDto"];
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
    "/api/TaktUsers/options": {
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
    "/api/TaktUsers": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u7528\u6237"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktUsers/delete": {
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
    "/api/TaktUsers/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktUserStatusDto"];
                    "application/json": components["schemas"]["TaktUserStatusDto"];
                    "text/json": components["schemas"]["TaktUserStatusDto"];
                    "application/*+json": components["schemas"]["TaktUserStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktUserDto"];
                        "application/json": components["schemas"]["TaktUserDto"];
                        "text/json": components["schemas"]["TaktUserDto"];
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
    "/api/TaktUsers/reset-password": {
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
                    "application/json-patch+json": components["schemas"]["TaktUserResetPwdDto"];
                    "application/json": components["schemas"]["TaktUserResetPwdDto"];
                    "text/json": components["schemas"]["TaktUserResetPwdDto"];
                    "application/*+json": components["schemas"]["TaktUserResetPwdDto"];
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
    "/api/TaktUsers/change-password": {
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
                    "application/json-patch+json": components["schemas"]["TaktUserChangePwdDto"];
                    "application/json": components["schemas"]["TaktUserChangePwdDto"];
                    "text/json": components["schemas"]["TaktUserChangePwdDto"];
                    "application/*+json": components["schemas"]["TaktUserChangePwdDto"];
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
    "/api/TaktUsers/forgot-password": {
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
                    "application/json-patch+json": components["schemas"]["TaktUserForgotPasswordDto"];
                    "application/json": components["schemas"]["TaktUserForgotPasswordDto"];
                    "text/json": components["schemas"]["TaktUserForgotPasswordDto"];
                    "application/*+json": components["schemas"]["TaktUserForgotPasswordDto"];
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
    "/api/TaktUsers/unlock": {
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
                    "application/json-patch+json": components["schemas"]["TaktUserUnlockDto"];
                    "application/json": components["schemas"]["TaktUserUnlockDto"];
                    "text/json": components["schemas"]["TaktUserUnlockDto"];
                    "application/*+json": components["schemas"]["TaktUserUnlockDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktUserDto"];
                        "application/json": components["schemas"]["TaktUserDto"];
                        "text/json": components["schemas"]["TaktUserDto"];
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
    "/api/TaktUsers/avatar": {
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
                    "application/json-patch+json": components["schemas"]["TaktUserAvatarUpdateDto"];
                    "application/json": components["schemas"]["TaktUserAvatarUpdateDto"];
                    "text/json": components["schemas"]["TaktUserAvatarUpdateDto"];
                    "application/*+json": components["schemas"]["TaktUserAvatarUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktUserDto"];
                        "application/json": components["schemas"]["TaktUserDto"];
                        "text/json": components["schemas"]["TaktUserDto"];
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
    "/api/TaktUsers/{userId}/roles": {
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
                    userId: number;
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
                        "text/plain": components["schemas"]["TaktUserRoleDto"][];
                        "application/json": components["schemas"]["TaktUserRoleDto"][];
                        "text/json": components["schemas"]["TaktUserRoleDto"][];
                    };
                };
            };
        };
        put: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    userId: number;
                };
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
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktUsers/{userId}/depts": {
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
                    userId: number;
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
                        "text/plain": components["schemas"]["TaktUserDeptDto"][];
                        "application/json": components["schemas"]["TaktUserDeptDto"][];
                        "text/json": components["schemas"]["TaktUserDeptDto"][];
                    };
                };
            };
        };
        put: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    userId: number;
                };
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
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktUsers/{userId}/posts": {
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
                    userId: number;
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
                        "text/plain": components["schemas"]["TaktUserPostDto"][];
                        "application/json": components["schemas"]["TaktUserPostDto"][];
                        "text/json": components["schemas"]["TaktUserPostDto"][];
                    };
                };
            };
        };
        put: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    userId: number;
                };
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
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktUsers/{userId}/tenants": {
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
                    userId: number;
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
                        "text/plain": components["schemas"]["TaktUserTenantDto"][];
                        "application/json": components["schemas"]["TaktUserTenantDto"][];
                        "text/json": components["schemas"]["TaktUserTenantDto"][];
                    };
                };
            };
        };
        put: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    userId: number;
                };
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
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktUsers/template": {
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
    "/api/TaktUsers/import": {
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
    "/api/TaktUsers/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktUserQueryDto"];
                    "application/json": components["schemas"]["TaktUserQueryDto"];
                    "text/json": components["schemas"]["TaktUserQueryDto"];
                    "application/*+json": components["schemas"]["TaktUserQueryDto"];
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
    "/api/TaktCaptcha/generate": {
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
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktCaptcha/verify": {
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
                    "application/json-patch+json": components["schemas"]["CaptchaVerifyRequest"];
                    "application/json": components["schemas"]["CaptchaVerifyRequest"];
                    "text/json": components["schemas"]["CaptchaVerifyRequest"];
                    "application/*+json": components["schemas"]["CaptchaVerifyRequest"];
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
        CaptchaVerifyRequest: {
            captchaId?: string;
            userInput?: unknown;
        };
        /** Format: binary */
        IFormFile: string;
        TaktApiResultOfListOfTaktMenuTreeDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["TaktMenuTreeDto"][] | null;
            success?: boolean;
        };
        TaktApiResultOfListOfTaktTreeSelectOption: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["TaktTreeSelectOption"][] | null;
            success?: boolean;
        };
        TaktApiResultOfObject: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: unknown;
            success?: boolean;
        };
        TaktApiResultOfTaktLoginResponseDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["TaktLoginResponseDto"];
            success?: boolean;
        };
        TaktApiResultOfTaktUserInfoDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["TaktUserInfoDto"];
            success?: boolean;
        };
        TaktLoginDto: {
            userName?: string;
            password?: string;
            rememberMe?: boolean;
        };
        TaktLoginResponseDto: {
            accessToken?: string;
            refreshToken?: string;
            tokenType?: string;
            /** Format: int32 */
            expiresIn?: number;
            userInfo?: components["schemas"]["TaktUserInfoDto"];
        } | null;
        TaktMenuCreateDto: {
            menuName?: string;
            menuCode?: string;
            menuL10nKey?: string | null;
            /** Format: int64 */
            parentId?: number;
            path?: string | null;
            component?: string | null;
            menuIcon?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            menuType?: number;
            permission?: string | null;
            /** Format: int32 */
            isVisible?: number;
            /** Format: int32 */
            isCache?: number;
            /** Format: int32 */
            isExternal?: number;
            linkUrl?: string | null;
            remark?: string | null;
        };
        TaktMenuDto: {
            /** Format: int64 */
            menuId?: number;
            menuName?: string;
            menuCode?: string;
            menuL10nKey?: string | null;
            /** Format: int64 */
            parentId?: number;
            path?: string | null;
            component?: string | null;
            menuIcon?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            menuType?: number;
            permission?: string | null;
            /** Format: int32 */
            isVisible?: number;
            /** Format: int32 */
            isCache?: number;
            /** Format: int32 */
            isExternal?: number;
            linkUrl?: string | null;
            /** Format: int32 */
            menuStatus?: number;
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
        TaktMenuQueryDto: {
            menuName?: string | null;
            menuCode?: string | null;
            /** Format: int64 */
            parentId?: number | null;
            /** Format: int32 */
            menuType?: number | null;
            /** Format: int32 */
            menuStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktMenuStatusDto: {
            /** Format: int64 */
            menuId?: number;
            /** Format: int32 */
            menuStatus?: number;
        };
        TaktMenuTreeDto: {
            children?: components["schemas"][];
            /** Format: int64 */
            menuId?: number;
            menuName?: string;
            menuCode?: string;
            menuL10nKey?: string | null;
            /** Format: int64 */
            parentId?: number;
            path?: string | null;
            component?: string | null;
            menuIcon?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            menuType?: number;
            permission?: string | null;
            /** Format: int32 */
            isVisible?: number;
            /** Format: int32 */
            isCache?: number;
            /** Format: int32 */
            isExternal?: number;
            linkUrl?: string | null;
            /** Format: int32 */
            menuStatus?: number;
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
        TaktMenuUpdateDto: {
            /** Format: int64 */
            menuId?: number;
            menuName?: string;
            menuCode?: string;
            menuL10nKey?: string | null;
            /** Format: int64 */
            parentId?: number;
            path?: string | null;
            component?: string | null;
            menuIcon?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            menuType?: number;
            permission?: string | null;
            /** Format: int32 */
            isVisible?: number;
            /** Format: int32 */
            isCache?: number;
            /** Format: int32 */
            isExternal?: number;
            linkUrl?: string | null;
            remark?: string | null;
        };
        TaktPagedResultOfTaktMenuDto: {
            data?: components["schemas"]["TaktMenuDto"][];
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
        TaktPagedResultOfTaktRoleDto: {
            data?: components["schemas"]["TaktRoleDto"][];
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
        TaktPagedResultOfTaktTenantDto: {
            data?: components["schemas"]["TaktTenantDto"][];
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
        TaktPagedResultOfTaktUserDto: {
            data?: components["schemas"]["TaktUserDto"][];
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
        TaktRefreshTokenDto: {
            refreshToken?: string;
        };
        TaktResultCode: number;
        TaktRoleCreateDto: {
            roleName?: string;
            roleCode?: string;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            dataScope?: number;
            customScope?: string | null;
            remark?: string | null;
            menuIds?: number[] | null;
            userIds?: number[] | null;
            deptIds?: number[] | null;
        };
        TaktRoleDeptDto: {
            /** Format: int64 */
            roleDeptId?: number;
            /** Format: int64 */
            roleId?: number;
            roleName?: string;
            roleCode?: string;
            /** Format: int64 */
            deptId?: number;
            deptName?: string;
            deptCode?: string;
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
        TaktRoleDto: {
            /** Format: int64 */
            roleId?: number;
            roleName?: string;
            roleCode?: string;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            dataScope?: number;
            customScope?: string | null;
            /** Format: int32 */
            roleStatus?: number;
            menuIds?: number[] | null;
            userIds?: number[] | null;
            deptIds?: number[] | null;
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
        TaktRoleMenuDto: {
            /** Format: int64 */
            roleMenuId?: number;
            /** Format: int64 */
            roleId?: number;
            roleName?: string;
            roleCode?: string;
            /** Format: int64 */
            menuId?: number;
            menuName?: string;
            menuCode?: string;
            path?: string | null;
            /** Format: int32 */
            menuType?: number;
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
        TaktRoleQueryDto: {
            roleName?: string | null;
            roleCode?: string | null;
            /** Format: int32 */
            roleStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktRoleStatusDto: {
            /** Format: int64 */
            roleId?: number;
            /** Format: int32 */
            roleStatus?: number;
        };
        TaktRoleUpdateDto: {
            /** Format: int64 */
            roleId?: number;
            roleName?: string;
            roleCode?: string;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            dataScope?: number;
            customScope?: string | null;
            remark?: string | null;
            menuIds?: number[] | null;
            userIds?: number[] | null;
            deptIds?: number[] | null;
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
        TaktTenantCreateDto: {
            tenantName?: string;
            tenantCode?: string;
            configId?: string;
            /** Format: date-time */
            startTime?: string | null;
            /** Format: date-time */
            endTime?: string | null;
            remark?: string | null;
        };
        TaktTenantDto: {
            /** Format: int64 */
            tenantId?: number;
            tenantName?: string;
            tenantCode?: string;
            /** Format: date-time */
            startTime?: string;
            /** Format: date-time */
            endTime?: string;
            /** Format: int32 */
            tenantStatus?: number;
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
        TaktTenantQueryDto: {
            tenantName?: string | null;
            tenantCode?: string | null;
            configId?: string | null;
            /** Format: int32 */
            tenantStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktTenantStatusDto: {
            /** Format: int64 */
            tenantId?: number;
            /** Format: int32 */
            tenantStatus?: number;
        };
        TaktTenantUpdateDto: {
            /** Format: int64 */
            tenantId?: number;
            tenantName?: string;
            tenantCode?: string;
            configId?: string;
            /** Format: date-time */
            startTime?: string | null;
            /** Format: date-time */
            endTime?: string | null;
            remark?: string | null;
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
        TaktUserAvatarUpdateDto: {
            /** Format: int64 */
            userId?: number;
            avatar?: string | null;
        };
        TaktUserChangePwdDto: {
            oldPassword?: string;
            newPassword?: string;
        };
        TaktUserCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            userName?: string;
            nickName?: string;
            /** Format: int32 */
            userType?: number;
            userEmail?: string;
            userPhone?: string;
            passwordHash?: string;
            /** Format: int32 */
            userStatus?: number;
            roleIds?: number[] | null;
            deptIds?: number[] | null;
            postIds?: number[] | null;
            tenantIds?: number[] | null;
            remark?: string | null;
        };
        TaktUserDeptDto: {
            /** Format: int64 */
            userDeptId?: number;
            /** Format: int64 */
            userId?: number;
            userName?: string;
            realName?: string;
            /** Format: int64 */
            deptId?: number;
            deptName?: string;
            deptCode?: string;
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
        TaktUserDto: {
            /** Format: int64 */
            userId?: number;
            userName?: string;
            nickName?: string;
            /** Format: int64 */
            employeeId?: number;
            /** Format: int32 */
            userType?: number;
            userEmail?: string;
            userPhone?: string;
            /** Format: int32 */
            loginCount?: number;
            lockReason?: string | null;
            /** Format: date-time */
            lockTime?: string | null;
            lockBy?: string | null;
            /** Format: int32 */
            errorLimit?: number;
            /** Format: int32 */
            userStatus?: number;
            roleIds?: number[] | null;
            deptIds?: number[] | null;
            postIds?: number[] | null;
            tenantIds?: number[] | null;
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
        TaktUserForgotPasswordDto: {
            userEmail?: string;
        };
        TaktUserInfoDto: {
            userId?: string;
            userName?: string;
            nickName?: string;
            realName?: string;
            avatar?: string;
            roles?: string[];
            permissions?: string[];
            /** Format: int32 */
            userType?: number;
            employeeId?: string;
        } | null;
        TaktUserPostDto: {
            /** Format: int64 */
            userPostId?: number;
            /** Format: int64 */
            userId?: number;
            userName?: string;
            realName?: string;
            /** Format: int64 */
            postId?: number;
            postName?: string;
            postCode?: string;
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
        TaktUserQueryDto: {
            userName?: string | null;
            userEmail?: string | null;
            userPhone?: string | null;
            /** Format: int32 */
            userStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktUserResetPwdDto: {
            /** Format: int64 */
            userId?: number;
            newPassword?: string;
        };
        TaktUserRoleDto: {
            /** Format: int64 */
            userRoleId?: number;
            /** Format: int64 */
            userId?: number;
            userName?: string;
            realName?: string;
            /** Format: int64 */
            roleId?: number;
            roleName?: string;
            roleCode?: string;
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
        TaktUserStatusDto: {
            /** Format: int64 */
            userId?: number;
            /** Format: int32 */
            userStatus?: number;
        };
        TaktUserTenantDto: {
            /** Format: int64 */
            userTenantId?: number;
            /** Format: int64 */
            userId?: number;
            userName?: string;
            realName?: string;
            /** Format: int64 */
            tenantId?: number;
            tenantName?: string;
            tenantCode?: string;
            tenantConfigId?: string;
            /** Format: int32 */
            tenantStatus?: number;
            /** Format: date-time */
            startTime?: string | null;
            /** Format: date-time */
            endTime?: string | null;
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
        TaktUserUnlockDto: {
            /** Format: int64 */
            userId?: number;
            /** Format: int32 */
            userStatus?: number;
        };
        TaktUserUpdateDto: {
            /** Format: int64 */
            userId?: number;
            /** Format: int64 */
            employeeId?: number;
            userName?: string;
            nickName?: string;
            /** Format: int32 */
            userType?: number;
            userEmail?: string;
            userPhone?: string;
            passwordHash?: string;
            /** Format: int32 */
            userStatus?: number;
            roleIds?: number[] | null;
            deptIds?: number[] | null;
            postIds?: number[] | null;
            tenantIds?: number[] | null;
            remark?: string | null;
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
    "\u83DC\u5355\u7BA1\u7406": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktMenuCreateDto"];
                "application/json": components["schemas"]["TaktMenuCreateDto"];
                "text/json": components["schemas"]["TaktMenuCreateDto"];
                "application/*+json": components["schemas"]["TaktMenuCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktMenuDto"];
                    "application/json": components["schemas"]["TaktMenuDto"];
                    "text/json": components["schemas"]["TaktMenuDto"];
                };
            };
        };
    };
    "\u89D2\u8272\u7BA1\u7406": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktRoleCreateDto"];
                "application/json": components["schemas"]["TaktRoleCreateDto"];
                "text/json": components["schemas"]["TaktRoleCreateDto"];
                "application/*+json": components["schemas"]["TaktRoleCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktRoleDto"];
                    "application/json": components["schemas"]["TaktRoleDto"];
                    "text/json": components["schemas"]["TaktRoleDto"];
                };
            };
        };
    };
    "\u79DF\u6237": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktTenantCreateDto"];
                "application/json": components["schemas"]["TaktTenantCreateDto"];
                "text/json": components["schemas"]["TaktTenantCreateDto"];
                "application/*+json": components["schemas"]["TaktTenantCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktTenantDto"];
                    "application/json": components["schemas"]["TaktTenantDto"];
                    "text/json": components["schemas"]["TaktTenantDto"];
                };
            };
        };
    };
    "\u7528\u6237": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktUserCreateDto"];
                "application/json": components["schemas"]["TaktUserCreateDto"];
                "text/json": components["schemas"]["TaktUserCreateDto"];
                "application/*+json": components["schemas"]["TaktUserCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktUserDto"];
                    "application/json": components["schemas"]["TaktUserDto"];
                    "text/json": components["schemas"]["TaktUserDto"];
                };
            };
        };
    };
}
