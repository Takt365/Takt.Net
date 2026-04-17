export interface paths {
    "/api/TaktWordFilters/check": {
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
                    "application/json-patch+json": components["schemas"]["CheckTextDto"];
                    "application/json": components["schemas"]["CheckTextDto"];
                    "text/json": components["schemas"]["CheckTextDto"];
                    "application/*+json": components["schemas"]["CheckTextDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktApiResultOfCheckTextResultDto"];
                        "application/json": components["schemas"]["TaktApiResultOfCheckTextResultDto"];
                        "text/json": components["schemas"]["TaktApiResultOfCheckTextResultDto"];
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
    "/api/TaktWordFilters/find": {
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
                    "application/json-patch+json": components["schemas"]["FindWordsDto"];
                    "application/json": components["schemas"]["FindWordsDto"];
                    "text/json": components["schemas"]["FindWordsDto"];
                    "application/*+json": components["schemas"]["FindWordsDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktApiResultOfFindWordsResultDto"];
                        "application/json": components["schemas"]["TaktApiResultOfFindWordsResultDto"];
                        "text/json": components["schemas"]["TaktApiResultOfFindWordsResultDto"];
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
    "/api/TaktWordFilters/replace": {
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
                    "application/json-patch+json": components["schemas"]["ReplaceWordsDto"];
                    "application/json": components["schemas"]["ReplaceWordsDto"];
                    "text/json": components["schemas"]["ReplaceWordsDto"];
                    "application/*+json": components["schemas"]["ReplaceWordsDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktApiResultOfReplaceWordsResultDto"];
                        "application/json": components["schemas"]["TaktApiResultOfReplaceWordsResultDto"];
                        "text/json": components["schemas"]["TaktApiResultOfReplaceWordsResultDto"];
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
    "/api/TaktWordFilters/highlight": {
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
                    "application/json-patch+json": components["schemas"]["HighlightWordsDto"];
                    "application/json": components["schemas"]["HighlightWordsDto"];
                    "text/json": components["schemas"]["HighlightWordsDto"];
                    "application/*+json": components["schemas"]["HighlightWordsDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktApiResultOfHighlightWordsResultDto"];
                        "application/json": components["schemas"]["TaktApiResultOfHighlightWordsResultDto"];
                        "text/json": components["schemas"]["TaktApiResultOfHighlightWordsResultDto"];
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
    "/api/TaktWordFilters/stats": {
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
                        "text/plain": components["schemas"]["TaktApiResultOfWordFilterStatsDto"];
                        "application/json": components["schemas"]["TaktApiResultOfWordFilterStatsDto"];
                        "text/json": components["schemas"]["TaktApiResultOfWordFilterStatsDto"];
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
    "/api/TaktWordFilters/groups": {
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
                        "text/plain": components["schemas"]["TaktApiResultOfListOfWordLibraryFileDto"];
                        "application/json": components["schemas"]["TaktApiResultOfListOfWordLibraryFileDto"];
                        "text/json": components["schemas"]["TaktApiResultOfListOfWordLibraryFileDto"];
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
    "/api/TaktWordFilters/words": {
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
                        "text/plain": components["schemas"]["TaktApiResultOfListOfstring"];
                        "application/json": components["schemas"]["TaktApiResultOfListOfstring"];
                        "text/json": components["schemas"]["TaktApiResultOfListOfstring"];
                    };
                };
            };
        };
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
                    "application/json-patch+json": components["schemas"]["AddWordsDto"];
                    "application/json": components["schemas"]["AddWordsDto"];
                    "text/json": components["schemas"]["AddWordsDto"];
                    "application/*+json": components["schemas"]["AddWordsDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktApiResultOfAddWordsResultDto"];
                        "application/json": components["schemas"]["TaktApiResultOfAddWordsResultDto"];
                        "text/json": components["schemas"]["TaktApiResultOfAddWordsResultDto"];
                    };
                };
            };
        };
        delete: {
            parameters: {
                query?: never;
                header?: never;
                path?: never;
                cookie?: never;
            };
            requestBody: {
                content: {
                    "application/json-patch+json": components["schemas"]["RemoveWordsDto"];
                    "application/json": components["schemas"]["RemoveWordsDto"];
                    "text/json": components["schemas"]["RemoveWordsDto"];
                    "application/*+json": components["schemas"]["RemoveWordsDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktApiResultOfRemoveWordsResultDto"];
                        "application/json": components["schemas"]["TaktApiResultOfRemoveWordsResultDto"];
                        "text/json": components["schemas"]["TaktApiResultOfRemoveWordsResultDto"];
                    };
                };
            };
        };
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktWordFilters/clear": {
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
            requestBody?: never;
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
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktMessages/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    FromUserName?: string;
                    ToUserName?: string;
                    MessageType?: string;
                    MessageGroup?: string;
                    ReadStatus?: number;
                    SendTimeStart?: string;
                    SendTimeEnd?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktMessageDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktMessageDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktMessageDto"];
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
    "/api/TaktMessages/{id}": {
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
                        "text/plain": components["schemas"]["TaktMessageDto"];
                        "application/json": components["schemas"]["TaktMessageDto"];
                        "text/json": components["schemas"]["TaktMessageDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktMessageUpdateDto"];
                    "application/json": components["schemas"]["TaktMessageUpdateDto"];
                    "text/json": components["schemas"]["TaktMessageUpdateDto"];
                    "application/*+json": components["schemas"]["TaktMessageUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktMessageDto"];
                        "application/json": components["schemas"]["TaktMessageDto"];
                        "text/json": components["schemas"]["TaktMessageDto"];
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
    "/api/TaktMessages": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5728\u7EBF\u6D88\u606F"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktMessages/read": {
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
                    "application/json-patch+json": components["schemas"]["TaktMessageReadDto"];
                    "application/json": components["schemas"]["TaktMessageReadDto"];
                    "text/json": components["schemas"]["TaktMessageReadDto"];
                    "application/*+json": components["schemas"]["TaktMessageReadDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktMessageDto"];
                        "application/json": components["schemas"]["TaktMessageDto"];
                        "text/json": components["schemas"]["TaktMessageDto"];
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
    "/api/TaktMessages/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktMessageQueryDto"];
                    "application/json": components["schemas"]["TaktMessageQueryDto"];
                    "text/json": components["schemas"]["TaktMessageQueryDto"];
                    "application/*+json": components["schemas"]["TaktMessageQueryDto"];
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
    "/api/TaktOnlines/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ConnectionId?: string;
                    UserName?: string;
                    UserId?: number;
                    OnlineStatus?: number;
                    ConnectTimeStart?: string;
                    ConnectTimeEnd?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktOnlineDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktOnlineDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktOnlineDto"];
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
    "/api/TaktOnlines/{id}": {
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
                        "text/plain": components["schemas"]["TaktOnlineDto"];
                        "application/json": components["schemas"]["TaktOnlineDto"];
                        "text/json": components["schemas"]["TaktOnlineDto"];
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
    "/api/TaktOnlines/connection/{connectionId}": {
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
                    connectionId: string;
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
                        "text/plain": components["schemas"]["TaktOnlineDto"];
                        "application/json": components["schemas"]["TaktOnlineDto"];
                        "text/json": components["schemas"]["TaktOnlineDto"];
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
                    connectionId: string;
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
    "/api/TaktOnlines": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5728\u7EBF\u7528\u6237"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktOnlines/batch": {
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
    "/api/TaktOnlines/active/{connectionId}": {
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
                    connectionId: string;
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
        post?: never;
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktOnlines/export": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ConnectionId?: string;
                    UserName?: string;
                    UserId?: number;
                    OnlineStatus?: number;
                    ConnectTimeStart?: string;
                    ConnectTimeEnd?: string;
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
    "/api/TaktSettings/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    SettingKey?: string;
                    SettingGroup?: string;
                    SettingStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktSettingsDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktSettingsDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktSettingsDto"];
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
    "/api/TaktSettings/{id}": {
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
                        "text/plain": components["schemas"]["TaktSettingsDto"];
                        "application/json": components["schemas"]["TaktSettingsDto"];
                        "text/json": components["schemas"]["TaktSettingsDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktSettingsUpdateDto"];
                    "application/json": components["schemas"]["TaktSettingsUpdateDto"];
                    "text/json": components["schemas"]["TaktSettingsUpdateDto"];
                    "application/*+json": components["schemas"]["TaktSettingsUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktSettingsDto"];
                        "application/json": components["schemas"]["TaktSettingsDto"];
                        "text/json": components["schemas"]["TaktSettingsDto"];
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
    "/api/TaktSettings/key/{settingKey}": {
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
                    settingKey: string;
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
                        "text/plain": components["schemas"]["TaktSettingsDto"];
                        "application/json": components["schemas"]["TaktSettingsDto"];
                        "text/json": components["schemas"]["TaktSettingsDto"];
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
    "/api/TaktSettings/options": {
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
    "/api/TaktSettings": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u8BBE\u7F6E"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktSettings/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktSettingsStatusDto"];
                    "application/json": components["schemas"]["TaktSettingsStatusDto"];
                    "text/json": components["schemas"]["TaktSettingsStatusDto"];
                    "application/*+json": components["schemas"]["TaktSettingsStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktSettingsDto"];
                        "application/json": components["schemas"]["TaktSettingsDto"];
                        "text/json": components["schemas"]["TaktSettingsDto"];
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
    "/api/TaktSettings/template": {
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
    "/api/TaktSettings/import": {
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
    "/api/TaktSettings/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktSettingsQueryDto"];
                    "application/json": components["schemas"]["TaktSettingsQueryDto"];
                    "text/json": components["schemas"]["TaktSettingsQueryDto"];
                    "application/*+json": components["schemas"]["TaktSettingsQueryDto"];
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
    "/api/TaktNumberingRules/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    RuleCode?: string;
                    RuleName?: string;
                    CompanyCode?: string;
                    DeptCode?: string;
                    RuleStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktNumberingRuleDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktNumberingRuleDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktNumberingRuleDto"];
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
    "/api/TaktNumberingRules/{id}": {
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
                        "text/plain": components["schemas"]["TaktNumberingRuleDto"];
                        "application/json": components["schemas"]["TaktNumberingRuleDto"];
                        "text/json": components["schemas"]["TaktNumberingRuleDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktNumberingRuleUpdateDto"];
                    "application/json": components["schemas"]["TaktNumberingRuleUpdateDto"];
                    "text/json": components["schemas"]["TaktNumberingRuleUpdateDto"];
                    "application/*+json": components["schemas"]["TaktNumberingRuleUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktNumberingRuleDto"];
                        "application/json": components["schemas"]["TaktNumberingRuleDto"];
                        "text/json": components["schemas"]["TaktNumberingRuleDto"];
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
    "/api/TaktNumberingRules": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u7F16\u7801\u89C4\u5219"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktNumberingRules/batch": {
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
    "/api/TaktNumberingRules/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktNumberingRuleStatusDto"];
                    "application/json": components["schemas"]["TaktNumberingRuleStatusDto"];
                    "text/json": components["schemas"]["TaktNumberingRuleStatusDto"];
                    "application/*+json": components["schemas"]["TaktNumberingRuleStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktNumberingRuleDto"];
                        "application/json": components["schemas"]["TaktNumberingRuleDto"];
                        "text/json": components["schemas"]["TaktNumberingRuleDto"];
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
    "/api/TaktNumberingRules/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktNumberingRuleQueryDto"];
                    "application/json": components["schemas"]["TaktNumberingRuleQueryDto"];
                    "text/json": components["schemas"]["TaktNumberingRuleQueryDto"];
                    "application/*+json": components["schemas"]["TaktNumberingRuleQueryDto"];
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
    "/api/TaktLanguages/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    CultureCode?: string;
                    LanguageStatus?: number;
                    IsDefault?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktLanguageDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktLanguageDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktLanguageDto"];
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
    "/api/TaktLanguages/{id}": {
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
                        "text/plain": components["schemas"]["TaktLanguageDto"];
                        "application/json": components["schemas"]["TaktLanguageDto"];
                        "text/json": components["schemas"]["TaktLanguageDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktLanguageUpdateDto"];
                    "application/json": components["schemas"]["TaktLanguageUpdateDto"];
                    "text/json": components["schemas"]["TaktLanguageUpdateDto"];
                    "application/*+json": components["schemas"]["TaktLanguageUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktLanguageDto"];
                        "application/json": components["schemas"]["TaktLanguageDto"];
                        "text/json": components["schemas"]["TaktLanguageDto"];
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
    "/api/TaktLanguages/options": {
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
    "/api/TaktLanguages": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u8BED\u8A00"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktLanguages/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktLanguageStatusDto"];
                    "application/json": components["schemas"]["TaktLanguageStatusDto"];
                    "text/json": components["schemas"]["TaktLanguageStatusDto"];
                    "application/*+json": components["schemas"]["TaktLanguageStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktLanguageDto"];
                        "application/json": components["schemas"]["TaktLanguageDto"];
                        "text/json": components["schemas"]["TaktLanguageDto"];
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
    "/api/TaktLanguages/template": {
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
    "/api/TaktLanguages/import": {
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
    "/api/TaktLanguages/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktLanguageQueryDto"];
                    "application/json": components["schemas"]["TaktLanguageQueryDto"];
                    "text/json": components["schemas"]["TaktLanguageQueryDto"];
                    "application/*+json": components["schemas"]["TaktLanguageQueryDto"];
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
    "/api/TaktTranslations/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    LanguageId?: number;
                    ResourceKey?: string;
                    CultureCode?: string;
                    ResourceType?: string;
                    ResourceGroup?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktTranslationDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktTranslationDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktTranslationDto"];
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
    "/api/TaktTranslations/list/transposed": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    LanguageId?: number;
                    ResourceKey?: string;
                    CultureCode?: string;
                    ResourceType?: string;
                    ResourceGroup?: string;
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
                        "text/plain": components["schemas"]["TaktTranslationTransposedResult"];
                        "application/json": components["schemas"]["TaktTranslationTransposedResult"];
                        "text/json": components["schemas"]["TaktTranslationTransposedResult"];
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
    "/api/TaktTranslations/{id}": {
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
                        "text/plain": components["schemas"]["TaktTranslationDto"];
                        "application/json": components["schemas"]["TaktTranslationDto"];
                        "text/json": components["schemas"]["TaktTranslationDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktTranslationUpdateDto"];
                    "application/json": components["schemas"]["TaktTranslationUpdateDto"];
                    "text/json": components["schemas"]["TaktTranslationUpdateDto"];
                    "application/*+json": components["schemas"]["TaktTranslationUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktTranslationDto"];
                        "application/json": components["schemas"]["TaktTranslationDto"];
                        "text/json": components["schemas"]["TaktTranslationDto"];
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
    "/api/TaktTranslations/options": {
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
    "/api/TaktTranslations": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u7FFB\u8BD1"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktTranslations/template": {
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
    "/api/TaktTranslations/import": {
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
    "/api/TaktTranslations/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktTranslationQueryDto"];
                    "application/json": components["schemas"]["TaktTranslationQueryDto"];
                    "text/json": components["schemas"]["TaktTranslationQueryDto"];
                    "application/*+json": components["schemas"]["TaktTranslationQueryDto"];
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
    "/api/TaktFiles/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    FileCode?: string;
                    FileName?: string;
                    FileCategory?: number;
                    StorageType?: number;
                    FileStatus?: number;
                    CreatedAtStart?: string;
                    CreatedAtEnd?: string;
                    FileExtension?: string;
                    IsPublic?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktFileDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktFileDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktFileDto"];
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
    "/api/TaktFiles/{id}": {
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
                        "text/plain": components["schemas"]["TaktFileDto"];
                        "application/json": components["schemas"]["TaktFileDto"];
                        "text/json": components["schemas"]["TaktFileDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktFileUpdateDto"];
                    "application/json": components["schemas"]["TaktFileUpdateDto"];
                    "text/json": components["schemas"]["TaktFileUpdateDto"];
                    "application/*+json": components["schemas"]["TaktFileUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktFileDto"];
                        "application/json": components["schemas"]["TaktFileDto"];
                        "text/json": components["schemas"]["TaktFileDto"];
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
    "/api/TaktFiles/code/{fileCode}": {
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
                    fileCode: string;
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
                        "text/plain": components["schemas"]["TaktFileDto"];
                        "application/json": components["schemas"]["TaktFileDto"];
                        "text/json": components["schemas"]["TaktFileDto"];
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
    "/api/TaktFiles": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u6587\u4EF6\u7BA1\u7406"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktFiles/batch": {
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
    "/api/TaktFiles/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktFileStatusDto"];
                    "application/json": components["schemas"]["TaktFileStatusDto"];
                    "text/json": components["schemas"]["TaktFileStatusDto"];
                    "application/*+json": components["schemas"]["TaktFileStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktFileDto"];
                        "application/json": components["schemas"]["TaktFileDto"];
                        "text/json": components["schemas"]["TaktFileDto"];
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
    "/api/TaktFiles/change": {
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
                    "application/json-patch+json": components["schemas"]["TaktFileChangeDto"];
                    "application/json": components["schemas"]["TaktFileChangeDto"];
                    "text/json": components["schemas"]["TaktFileChangeDto"];
                    "application/*+json": components["schemas"]["TaktFileChangeDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktFileDto"];
                        "application/json": components["schemas"]["TaktFileDto"];
                        "text/json": components["schemas"]["TaktFileDto"];
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
    "/api/TaktFiles/upload": {
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
                        /**
                         * Format: int32
                         * @default 2
                         */
                        fileType?: number;
                    } & {
                        /** @default null */
                        targetFileName?: string;
                    };
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["FileUploadResult"];
                        "application/json": components["schemas"]["FileUploadResult"];
                        "text/json": components["schemas"]["FileUploadResult"];
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
    "/api/TaktFiles/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktFileQueryDto"];
                    "application/json": components["schemas"]["TaktFileQueryDto"];
                    "text/json": components["schemas"]["TaktFileQueryDto"];
                    "application/*+json": components["schemas"]["TaktFileQueryDto"];
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
    "/api/TaktFiles/increment-download-count": {
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
                    "application/json-patch+json": components["schemas"]["TaktFileIncrementDownloadCountDto"];
                    "application/json": components["schemas"]["TaktFileIncrementDownloadCountDto"];
                    "text/json": components["schemas"]["TaktFileIncrementDownloadCountDto"];
                    "application/*+json": components["schemas"]["TaktFileIncrementDownloadCountDto"];
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
    "/api/TaktFiles/{id}/download": {
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
    "/api/TaktDictTypes/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    DictTypeName?: string;
                    DictTypeCode?: string;
                    DictTypeStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktDictTypeDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktDictTypeDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktDictTypeDto"];
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
    "/api/TaktDictTypes/{id}": {
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
                        "text/plain": components["schemas"]["TaktDictTypeDto"];
                        "application/json": components["schemas"]["TaktDictTypeDto"];
                        "text/json": components["schemas"]["TaktDictTypeDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktDictTypeUpdateDto"];
                    "application/json": components["schemas"]["TaktDictTypeUpdateDto"];
                    "text/json": components["schemas"]["TaktDictTypeUpdateDto"];
                    "application/*+json": components["schemas"]["TaktDictTypeUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktDictTypeDto"];
                        "application/json": components["schemas"]["TaktDictTypeDto"];
                        "text/json": components["schemas"]["TaktDictTypeDto"];
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
    "/api/TaktDictTypes/options": {
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
    "/api/TaktDictTypes": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5B57\u5178\u7C7B\u578B"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktDictTypes/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktDictTypeStatusDto"];
                    "application/json": components["schemas"]["TaktDictTypeStatusDto"];
                    "text/json": components["schemas"]["TaktDictTypeStatusDto"];
                    "application/*+json": components["schemas"]["TaktDictTypeStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktDictTypeDto"];
                        "application/json": components["schemas"]["TaktDictTypeDto"];
                        "text/json": components["schemas"]["TaktDictTypeDto"];
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
    "/api/TaktDictTypes/template": {
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
    "/api/TaktDictTypes/import": {
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
    "/api/TaktDictTypes/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktDictTypeQueryDto"];
                    "application/json": components["schemas"]["TaktDictTypeQueryDto"];
                    "text/json": components["schemas"]["TaktDictTypeQueryDto"];
                    "application/*+json": components["schemas"]["TaktDictTypeQueryDto"];
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
    "/api/TaktDictDatas/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    DictTypeId?: number;
                    DictTypeCode?: string;
                    DictLabel?: string;
                    DictValue?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktDictDataDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktDictDataDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktDictDataDto"];
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
    "/api/TaktDictDatas/{id}": {
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
                        "text/plain": components["schemas"]["TaktDictDataDto"];
                        "application/json": components["schemas"]["TaktDictDataDto"];
                        "text/json": components["schemas"]["TaktDictDataDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktDictDataUpdateDto"];
                    "application/json": components["schemas"]["TaktDictDataUpdateDto"];
                    "text/json": components["schemas"]["TaktDictDataUpdateDto"];
                    "application/*+json": components["schemas"]["TaktDictDataUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktDictDataDto"];
                        "application/json": components["schemas"]["TaktDictDataDto"];
                        "text/json": components["schemas"]["TaktDictDataDto"];
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
    "/api/TaktDictDatas/options": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    dictTypeCode?: string;
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
    "/api/TaktDictDatas": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5B57\u5178\u6570\u636E"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktDictDatas/template": {
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
    "/api/TaktDictDatas/import": {
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
    "/api/TaktDictDatas/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktDictDataQueryDto"];
                    "application/json": components["schemas"]["TaktDictDataQueryDto"];
                    "text/json": components["schemas"]["TaktDictDataQueryDto"];
                    "application/*+json": components["schemas"]["TaktDictDataQueryDto"];
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
    "/api/TaktCache/info": {
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
                        "text/plain": components["schemas"]["TaktApiResultOfTaktCacheInfoDto"];
                        "application/json": components["schemas"]["TaktApiResultOfTaktCacheInfoDto"];
                        "text/json": components["schemas"]["TaktApiResultOfTaktCacheInfoDto"];
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
    "/api/TaktCache/statistics": {
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
                        "text/plain": components["schemas"]["TaktApiResultOfTaktCacheStatisticsDto"];
                        "application/json": components["schemas"]["TaktApiResultOfTaktCacheStatisticsDto"];
                        "text/json": components["schemas"]["TaktApiResultOfTaktCacheStatisticsDto"];
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
    "/api/TaktCache/exists": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    key?: string;
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
                        "text/plain": components["schemas"]["TaktApiResultOfTaktCacheExistsDto"];
                        "application/json": components["schemas"]["TaktApiResultOfTaktCacheExistsDto"];
                        "text/json": components["schemas"]["TaktApiResultOfTaktCacheExistsDto"];
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
    "/api/TaktCache": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post?: never;
        delete: operations["\u7F13\u5B58\u7BA1\u7406"];
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAnnouncements/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    AnnouncementCode?: string;
                    AnnouncementTitle?: string;
                    AnnouncementType?: number;
                    AnnouncementStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktAnnouncementDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktAnnouncementDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktAnnouncementDto"];
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
    "/api/TaktAnnouncements/{id}": {
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
                        "text/plain": components["schemas"]["TaktAnnouncementDto"];
                        "application/json": components["schemas"]["TaktAnnouncementDto"];
                        "text/json": components["schemas"]["TaktAnnouncementDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktAnnouncementUpdateDto"];
                    "application/json": components["schemas"]["TaktAnnouncementUpdateDto"];
                    "text/json": components["schemas"]["TaktAnnouncementUpdateDto"];
                    "application/*+json": components["schemas"]["TaktAnnouncementUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAnnouncementDto"];
                        "application/json": components["schemas"]["TaktAnnouncementDto"];
                        "text/json": components["schemas"]["TaktAnnouncementDto"];
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
    "/api/TaktAnnouncements": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u516C\u544A\u901A\u77E5"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAnnouncements/batch": {
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
    "/api/TaktAnnouncements/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktAnnouncementStatusDto"];
                    "application/json": components["schemas"]["TaktAnnouncementStatusDto"];
                    "text/json": components["schemas"]["TaktAnnouncementStatusDto"];
                    "application/*+json": components["schemas"]["TaktAnnouncementStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAnnouncementDto"];
                        "application/json": components["schemas"]["TaktAnnouncementDto"];
                        "text/json": components["schemas"]["TaktAnnouncementDto"];
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
    "/api/TaktAnnouncements/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktAnnouncementQueryDto"];
                    "application/json": components["schemas"]["TaktAnnouncementQueryDto"];
                    "text/json": components["schemas"]["TaktAnnouncementQueryDto"];
                    "application/*+json": components["schemas"]["TaktAnnouncementQueryDto"];
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
        AddWordsDto: {
            words?: string[];
            group?: string | null;
        };
        AddWordsResultDto: {
            /** Format: int32 */
            addedCount?: number;
            /** Format: int32 */
            totalCount?: number;
        } | null;
        CheckTextDto: {
            text?: string;
        };
        CheckTextResultDto: {
            containsIllegalWords?: boolean;
            illegalWords?: string[];
        } | null;
        FileUploadResult: {
            fileCode?: string;
            fileName?: string;
            fileOriginalName?: string;
            filePath?: string;
            /** Format: int64 */
            fileSize?: number;
            fileType?: string | null;
            fileExtension?: string | null;
            fileHash?: string | null;
            /** Format: int32 */
            fileCategory?: number;
        };
        FindWordsDto: {
            text?: string;
            includeDetails?: boolean;
        };
        FindWordsResultDto: {
            illegalWords?: string[];
            illegalWordDetails?: components["schemas"]["IllegalWordDetailDto"][];
        } | null;
        HighlightWordsDto: {
            text?: string;
            highlightClass?: string;
        };
        HighlightWordsResultDto: {
            originalText?: string;
            highlightedText?: string;
            /** Format: int32 */
            highlightedCount?: number;
        } | null;
        /** Format: binary */
        IFormFile: string;
        IllegalWordDetailDto: {
            keyword?: string;
            /** Format: int32 */
            start?: number;
            /** Format: int32 */
            end?: number;
        };
        RemoveWordsDto: {
            words?: string[];
        };
        RemoveWordsResultDto: {
            /** Format: int32 */
            removedCount?: number;
            /** Format: int32 */
            remainingCount?: number;
        } | null;
        ReplaceWordsDto: {
            text?: string;
            replaceChar?: string | null;
            replaceText?: string | null;
        };
        ReplaceWordsResultDto: {
            originalText?: string;
            replacedText?: string;
            /** Format: int32 */
            replacedCount?: number;
        } | null;
        TaktAnnouncementCreateDto: {
            announcementCode?: string;
            announcementTitle?: string;
            announcementContent?: string;
            /** Format: int32 */
            announcementType?: number;
            /** Format: int64 */
            deptId?: number | null;
            deptName?: string | null;
            /** Format: int32 */
            publishScope?: number;
            publishScopeConfig?: string | null;
            /** Format: int32 */
            isTop?: number;
            /** Format: int32 */
            isUrgent?: number;
            /** Format: date-time */
            effectiveTime?: string | null;
            /** Format: date-time */
            expireTime?: string | null;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktAnnouncementDto: {
            /** Format: int64 */
            announcementId?: number;
            announcementCode?: string;
            announcementTitle?: string;
            announcementContent?: string;
            /** Format: int32 */
            announcementType?: number;
            /** Format: int64 */
            publisherId?: number;
            publisherName?: string;
            /** Format: int64 */
            deptId?: number | null;
            deptName?: string | null;
            /** Format: int32 */
            publishScope?: number;
            publishScopeConfig?: string | null;
            /** Format: int32 */
            isTop?: number;
            /** Format: int32 */
            isUrgent?: number;
            /** Format: date-time */
            publishTime?: string | null;
            /** Format: date-time */
            effectiveTime?: string | null;
            /** Format: date-time */
            expireTime?: string | null;
            /** Format: int32 */
            readCount?: number;
            /** Format: int32 */
            attachmentCount?: number;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            announcementStatus?: number;
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
        TaktAnnouncementQueryDto: {
            announcementCode?: string | null;
            announcementTitle?: string | null;
            /** Format: int32 */
            announcementType?: number | null;
            /** Format: int32 */
            announcementStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktAnnouncementStatusDto: {
            /** Format: int64 */
            announcementId?: number;
            /** Format: int32 */
            announcementStatus?: number;
        };
        TaktAnnouncementUpdateDto: {
            /** Format: int64 */
            announcementId?: number;
            announcementCode?: string;
            announcementTitle?: string;
            announcementContent?: string;
            /** Format: int32 */
            announcementType?: number;
            /** Format: int64 */
            deptId?: number | null;
            deptName?: string | null;
            /** Format: int32 */
            publishScope?: number;
            publishScopeConfig?: string | null;
            /** Format: int32 */
            isTop?: number;
            /** Format: int32 */
            isUrgent?: number;
            /** Format: date-time */
            effectiveTime?: string | null;
            /** Format: date-time */
            expireTime?: string | null;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktApiResult: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: unknown;
            success?: boolean;
        };
        TaktApiResultOfAddWordsResultDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["AddWordsResultDto"];
            success?: boolean;
        };
        TaktApiResultOfCheckTextResultDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["CheckTextResultDto"];
            success?: boolean;
        };
        TaktApiResultOfFindWordsResultDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["FindWordsResultDto"];
            success?: boolean;
        };
        TaktApiResultOfHighlightWordsResultDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["HighlightWordsResultDto"];
            success?: boolean;
        };
        TaktApiResultOfListOfstring: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: string[] | null;
            success?: boolean;
        };
        TaktApiResultOfListOfWordLibraryFileDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["WordLibraryFileDto"][] | null;
            success?: boolean;
        };
        TaktApiResultOfObject: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: unknown;
            success?: boolean;
        };
        TaktApiResultOfRemoveWordsResultDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["RemoveWordsResultDto"];
            success?: boolean;
        };
        TaktApiResultOfReplaceWordsResultDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["ReplaceWordsResultDto"];
            success?: boolean;
        };
        TaktApiResultOfTaktCacheExistsDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["TaktCacheExistsDto"];
            success?: boolean;
        };
        TaktApiResultOfTaktCacheInfoDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["TaktCacheInfoDto"];
            success?: boolean;
        };
        TaktApiResultOfTaktCacheStatisticsDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["TaktCacheStatisticsDto"];
            success?: boolean;
        };
        TaktApiResultOfWordFilterStatsDto: {
            code?: components["schemas"]["TaktResultCode"];
            message?: string;
            data?: components["schemas"]["WordFilterStatsDto"];
            success?: boolean;
        };
        TaktCacheExistsDto: {
            key?: string;
            exists?: boolean;
        } | null;
        TaktCacheInfoDto: {
            provider?: string;
            /** Format: int32 */
            defaultExpirationMinutes?: number;
            enableSlidingExpiration?: boolean;
            enableMultiLevelCache?: boolean;
            redisEnabled?: boolean;
            redisInstanceName?: string;
        } | null;
        TaktCacheStatisticsDto: {
            supported?: boolean;
            message?: string | null;
            /** Format: int64 */
            totalHits?: number;
            /** Format: int64 */
            totalMisses?: number;
            /** Format: int64 */
            currentEntryCount?: number;
            /** Format: int64 */
            currentEstimatedSizeBytes?: number | null;
            /** Format: double */
            hitRate?: number | null;
        } | null;
        TaktDictDataCreateDto: {
            /** Format: int64 */
            dictTypeId?: number;
            dictTypeCode?: string;
            dictLabel?: string;
            dictL10nKey?: string | null;
            dictValue?: string;
            /** Format: int32 */
            cssClass?: number;
            /** Format: int32 */
            listClass?: number;
            extLabel?: string | null;
            extValue?: string | null;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktDictDataDto: {
            /** Format: int64 */
            dictDataId?: number;
            /** Format: int64 */
            dictTypeId?: number;
            dictTypeCode?: string;
            dictLabel?: string;
            dictL10nKey?: string | null;
            dictValue?: string;
            /** Format: int32 */
            cssClass?: number;
            /** Format: int32 */
            listClass?: number;
            extLabel?: string | null;
            extValue?: string | null;
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
        TaktDictDataQueryDto: {
            /** Format: int64 */
            dictTypeId?: number | null;
            dictTypeCode?: string | null;
            dictLabel?: string | null;
            dictValue?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktDictDataUpdateDto: {
            /** Format: int64 */
            dictDataId?: number;
            /** Format: int64 */
            dictTypeId?: number;
            dictTypeCode?: string;
            dictLabel?: string;
            dictL10nKey?: string | null;
            dictValue?: string;
            /** Format: int32 */
            cssClass?: number;
            /** Format: int32 */
            listClass?: number;
            extLabel?: string | null;
            extValue?: string | null;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktDictTypeCreateDto: {
            dictTypeCode?: string;
            dictTypeName?: string;
            /** Format: int32 */
            dataSource?: number;
            dataConfigId?: string;
            sqlScript?: string | null;
            /** Format: int32 */
            isBuiltIn?: number;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
            dictDataList?: components["schemas"]["TaktDictDataCreateDto"][] | null;
        };
        TaktDictTypeDto: {
            /** Format: int64 */
            dictTypeId?: number;
            dictTypeCode?: string;
            dictTypeName?: string;
            /** Format: int32 */
            dataSource?: number;
            dataConfigId?: string;
            sqlScript?: string | null;
            /** Format: int32 */
            isBuiltIn?: number;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            dictTypeStatus?: number;
            dictDataList?: components["schemas"]["TaktDictDataDto"][] | null;
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
        TaktDictTypeQueryDto: {
            dictTypeName?: string | null;
            dictTypeCode?: string | null;
            /** Format: int32 */
            dictTypeStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktDictTypeStatusDto: {
            /** Format: int64 */
            dictTypeId?: number;
            /** Format: int32 */
            dictTypeStatus?: number;
        };
        TaktDictTypeUpdateDto: {
            /** Format: int64 */
            dictTypeId?: number;
            dictTypeCode?: string;
            dictTypeName?: string;
            /** Format: int32 */
            dataSource?: number;
            dataConfigId?: string;
            sqlScript?: string | null;
            /** Format: int32 */
            isBuiltIn?: number;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
            dictDataList?: components["schemas"]["TaktDictDataCreateDto"][] | null;
        };
        TaktFileChangeDto: {
            /** Format: int64 */
            fileId?: number;
            /** Format: int32 */
            isPublic?: number;
        };
        TaktFileCreateDto: {
            fileCode?: string;
            fileName?: string;
            fileOriginalName?: string;
            filePath?: string;
            /** Format: int64 */
            fileSize?: number;
            fileType?: string | null;
            fileExtension?: string | null;
            fileHash?: string | null;
            /** Format: int32 */
            fileCategory?: number;
            /** Format: int32 */
            storageType?: number;
            storageConfig?: string | null;
            accessUrl?: string | null;
            /** Format: int32 */
            isPublic?: number;
            accessPermissionConfig?: string | null;
            fileDescription?: string | null;
            fileTags?: string | null;
            remark?: string | null;
            ipAddress?: string | null;
            location?: string | null;
        };
        TaktFileDto: {
            /** Format: int64 */
            fileId?: number;
            fileCode?: string;
            fileName?: string;
            fileOriginalName?: string;
            filePath?: string;
            /** Format: int64 */
            fileSize?: number;
            fileType?: string | null;
            fileExtension?: string | null;
            fileHash?: string | null;
            /** Format: int32 */
            fileCategory?: number;
            /** Format: int32 */
            storageType?: number;
            storageConfig?: string | null;
            accessUrl?: string | null;
            /** Format: int32 */
            downloadCount?: number;
            /** Format: date-time */
            lastDownloadTime?: string | null;
            /** Format: int32 */
            fileStatus?: number;
            /** Format: int32 */
            isPublic?: number;
            accessPermissionConfig?: string | null;
            fileDescription?: string | null;
            fileTags?: string | null;
            ipAddress?: string | null;
            location?: string | null;
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
        TaktFileIncrementDownloadCountDto: {
            /** Format: int64 */
            fileId?: number;
        };
        TaktFileQueryDto: {
            fileCode?: string | null;
            fileName?: string | null;
            /** Format: int32 */
            fileCategory?: number | null;
            /** Format: int32 */
            storageType?: number | null;
            /** Format: int32 */
            fileStatus?: number | null;
            /** Format: date-time */
            createdAtStart?: string | null;
            /** Format: date-time */
            createdAtEnd?: string | null;
            fileExtension?: string | null;
            /** Format: int32 */
            isPublic?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktFileStatusDto: {
            /** Format: int64 */
            fileId?: number;
            /** Format: int32 */
            fileStatus?: number;
        };
        TaktFileUpdateDto: {
            /** Format: int64 */
            fileId?: number;
            fileCode?: string;
            fileName?: string;
            fileOriginalName?: string;
            filePath?: string;
            /** Format: int64 */
            fileSize?: number;
            fileType?: string | null;
            fileExtension?: string | null;
            fileHash?: string | null;
            /** Format: int32 */
            fileCategory?: number;
            /** Format: int32 */
            storageType?: number;
            storageConfig?: string | null;
            accessUrl?: string | null;
            /** Format: int32 */
            isPublic?: number;
            accessPermissionConfig?: string | null;
            fileDescription?: string | null;
            fileTags?: string | null;
            remark?: string | null;
            ipAddress?: string | null;
            location?: string | null;
        };
        TaktLanguageCreateDto: {
            languageName?: string;
            cultureCode?: string;
            nativeName?: string;
            languageIcon?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            languageStatus?: number;
            /** Format: int32 */
            isDefault?: number;
            /** Format: int32 */
            isRtl?: number;
            remark?: string | null;
            translationList?: components["schemas"]["TaktTranslationCreateDto"][] | null;
        };
        TaktLanguageDto: {
            /** Format: int64 */
            languageId?: number;
            languageName?: string;
            cultureCode?: string;
            nativeName?: string;
            languageIcon?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            languageStatus?: number;
            /** Format: int32 */
            isDefault?: number;
            /** Format: int32 */
            isRtl?: number;
            translationList?: components["schemas"]["TaktTranslationDto"][] | null;
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
        TaktLanguageQueryDto: {
            cultureCode?: string | null;
            /** Format: int32 */
            languageStatus?: number | null;
            /** Format: int32 */
            isDefault?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktLanguageStatusDto: {
            /** Format: int64 */
            languageId?: number;
            /** Format: uint8 */
            languageStatus?: number;
        };
        TaktLanguageUpdateDto: {
            /** Format: int64 */
            languageId?: number;
            languageName?: string;
            cultureCode?: string;
            nativeName?: string;
            languageIcon?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            languageStatus?: number;
            /** Format: int32 */
            isDefault?: number;
            /** Format: int32 */
            isRtl?: number;
            remark?: string | null;
            translationList?: components["schemas"]["TaktTranslationCreateDto"][] | null;
        };
        TaktMessageCreateDto: {
            fromUserName?: string;
            /** Format: int64 */
            fromUserId?: number | null;
            toUserName?: string;
            /** Format: int64 */
            toUserId?: number | null;
            messageTitle?: string | null;
            messageContent?: string;
            messageType?: string;
            messageGroup?: string | null;
            /** Format: int32 */
            readStatus?: number;
            /** Format: date-time */
            readTime?: string | null;
            /** Format: date-time */
            sendTime?: string | null;
            messageExtData?: string | null;
            remark?: string | null;
        };
        TaktMessageDto: {
            /** Format: int64 */
            messageId?: number;
            fromUserName?: string;
            /** Format: int64 */
            fromUserId?: number | null;
            toUserName?: string;
            /** Format: int64 */
            toUserId?: number | null;
            messageTitle?: string | null;
            messageContent?: string;
            messageType?: string;
            messageGroup?: string | null;
            /** Format: int32 */
            readStatus?: number;
            /** Format: date-time */
            readTime?: string | null;
            /** Format: date-time */
            sendTime?: string;
            messageExtData?: string | null;
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
        TaktMessageQueryDto: {
            fromUserName?: string | null;
            toUserName?: string | null;
            messageType?: string | null;
            messageGroup?: string | null;
            /** Format: int32 */
            readStatus?: number | null;
            /** Format: date-time */
            sendTimeStart?: string | null;
            /** Format: date-time */
            sendTimeEnd?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktMessageReadDto: {
            /** Format: int64 */
            messageId?: number;
        };
        TaktMessageUpdateDto: {
            /** Format: int64 */
            messageId?: number;
            fromUserName?: string;
            /** Format: int64 */
            fromUserId?: number | null;
            toUserName?: string;
            /** Format: int64 */
            toUserId?: number | null;
            messageTitle?: string | null;
            messageContent?: string;
            messageType?: string;
            messageGroup?: string | null;
            /** Format: int32 */
            readStatus?: number;
            /** Format: date-time */
            readTime?: string | null;
            /** Format: date-time */
            sendTime?: string | null;
            messageExtData?: string | null;
            remark?: string | null;
        };
        TaktNumberingRuleCreateDto: {
            ruleCode?: string;
            ruleName?: string;
            companyCode?: string | null;
            deptCode?: string | null;
            prefix?: string | null;
            dateFormat?: string | null;
            /** Format: int32 */
            numberLength?: number;
            suffix?: string | null;
            /** Format: int32 */
            step?: number;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktNumberingRuleDto: {
            /** Format: int64 */
            numberingRuleId?: number;
            ruleCode?: string;
            ruleName?: string;
            companyCode?: string | null;
            deptCode?: string | null;
            prefix?: string | null;
            dateFormat?: string | null;
            /** Format: int32 */
            numberLength?: number;
            suffix?: string | null;
            /** Format: int64 */
            currentNumber?: number;
            /** Format: int32 */
            step?: number;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            ruleStatus?: number;
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
        TaktNumberingRuleQueryDto: {
            ruleCode?: string | null;
            ruleName?: string | null;
            companyCode?: string | null;
            deptCode?: string | null;
            /** Format: int32 */
            ruleStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktNumberingRuleStatusDto: {
            /** Format: int64 */
            numberingRuleId?: number;
            /** Format: int32 */
            ruleStatus?: number;
        };
        TaktNumberingRuleUpdateDto: {
            /** Format: int64 */
            numberingRuleId?: number;
            ruleCode?: string;
            ruleName?: string;
            companyCode?: string | null;
            deptCode?: string | null;
            prefix?: string | null;
            dateFormat?: string | null;
            /** Format: int32 */
            numberLength?: number;
            suffix?: string | null;
            /** Format: int32 */
            step?: number;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktOnlineCreateDto: {
            connectionId?: string;
            userName?: string;
            /** Format: int64 */
            userId?: number | null;
            /** Format: int32 */
            onlineStatus?: number;
            connectIp?: string | null;
            connectLocation?: string | null;
            userAgent?: string | null;
            deviceType?: string | null;
            browserType?: string | null;
            operatingSystem?: string | null;
            /** Format: date-time */
            connectTime?: string | null;
            remark?: string | null;
        };
        TaktOnlineDto: {
            /** Format: int64 */
            onlineId?: number;
            connectionId?: string;
            userName?: string;
            /** Format: int64 */
            userId?: number | null;
            /** Format: int32 */
            onlineStatus?: number;
            connectIp?: string | null;
            connectLocation?: string | null;
            userAgent?: string | null;
            deviceType?: string | null;
            browserType?: string | null;
            operatingSystem?: string | null;
            /** Format: date-time */
            connectTime?: string;
            /** Format: date-time */
            lastActiveTime?: string | null;
            /** Format: date-time */
            disconnectTime?: string | null;
            /** Format: int32 */
            connectionDuration?: number | null;
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
        TaktPagedResultOfTaktAnnouncementDto: {
            data?: components["schemas"]["TaktAnnouncementDto"][];
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
        TaktPagedResultOfTaktDictDataDto: {
            data?: components["schemas"]["TaktDictDataDto"][];
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
        TaktPagedResultOfTaktDictTypeDto: {
            data?: components["schemas"]["TaktDictTypeDto"][];
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
        TaktPagedResultOfTaktFileDto: {
            data?: components["schemas"]["TaktFileDto"][];
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
        TaktPagedResultOfTaktLanguageDto: {
            data?: components["schemas"]["TaktLanguageDto"][];
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
        TaktPagedResultOfTaktMessageDto: {
            data?: components["schemas"]["TaktMessageDto"][];
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
        TaktPagedResultOfTaktNumberingRuleDto: {
            data?: components["schemas"]["TaktNumberingRuleDto"][];
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
        TaktPagedResultOfTaktOnlineDto: {
            data?: components["schemas"]["TaktOnlineDto"][];
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
        TaktPagedResultOfTaktSettingsDto: {
            data?: components["schemas"]["TaktSettingsDto"][];
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
        TaktPagedResultOfTaktTranslationDto: {
            data?: components["schemas"]["TaktTranslationDto"][];
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
        TaktPagedResultOfTaktTranslationTransposedDto: {
            data?: components["schemas"]["TaktTranslationTransposedDto"][];
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
        TaktResultCode: number;
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
        TaktSettingsCreateDto: {
            settingKey?: string;
            settingValue?: string | null;
            settingName?: string | null;
            settingGroup?: string | null;
            /** Format: int32 */
            isBuiltIn?: number;
            /** Format: int32 */
            isEncrypted?: number;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktSettingsDto: {
            /** Format: int64 */
            settingId?: number;
            settingKey?: string;
            settingValue?: string | null;
            settingName?: string | null;
            settingGroup?: string | null;
            /** Format: int32 */
            isBuiltIn?: number;
            /** Format: int32 */
            isEncrypted?: number;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            settingStatus?: number;
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
        TaktSettingsQueryDto: {
            settingKey?: string | null;
            settingGroup?: string | null;
            /** Format: int32 */
            settingStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktSettingsStatusDto: {
            /** Format: int64 */
            settingId?: number;
            /** Format: int32 */
            settingStatus?: number;
        };
        TaktSettingsUpdateDto: {
            /** Format: int64 */
            settingId?: number;
            settingKey?: string;
            settingValue?: string | null;
            settingName?: string | null;
            settingGroup?: string | null;
            /** Format: int32 */
            isBuiltIn?: number;
            /** Format: int32 */
            isEncrypted?: number;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktTranslationCreateDto: {
            resourceKey?: string;
            /** Format: int64 */
            languageId?: number;
            cultureCode?: string;
            translationValue?: string;
            resourceType?: string;
            resourceGroup?: string | null;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktTranslationDto: {
            /** Format: int64 */
            translationId?: number;
            resourceKey?: string;
            /** Format: int64 */
            languageId?: number;
            cultureCode?: string;
            translationValue?: string;
            resourceType?: string;
            resourceGroup?: string | null;
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
        TaktTranslationQueryDto: {
            /** Format: int64 */
            languageId?: number | null;
            resourceKey?: string | null;
            cultureCode?: string | null;
            resourceType?: string | null;
            resourceGroup?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktTranslationTransposedDto: {
            resourceKey?: string;
            resourceType?: string;
            resourceGroup?: string | null;
            /** Format: int32 */
            orderNum?: number;
            translations?: {
                [key: string]: string;
            };
        };
        TaktTranslationTransposedResult: {
            paged?: components["schemas"]["TaktPagedResultOfTaktTranslationTransposedDto"];
            cultureCodeOrder?: string[];
        };
        TaktTranslationUpdateDto: {
            /** Format: int64 */
            translationId?: number;
            resourceKey?: string;
            /** Format: int64 */
            languageId?: number;
            cultureCode?: string;
            translationValue?: string;
            resourceType?: string;
            resourceGroup?: string | null;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        WordFilterStatsDto: {
            /** Format: int32 */
            totalCount?: number;
            isInitialized?: boolean;
        } | null;
        WordLibraryFileDto: {
            fileName?: string;
            displayName?: string;
            /** Format: int64 */
            fileSize?: number;
            /** Format: int32 */
            wordCount?: number;
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
    "\u5728\u7EBF\u6D88\u606F": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktMessageCreateDto"];
                "application/json": components["schemas"]["TaktMessageCreateDto"];
                "text/json": components["schemas"]["TaktMessageCreateDto"];
                "application/*+json": components["schemas"]["TaktMessageCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktMessageDto"];
                    "application/json": components["schemas"]["TaktMessageDto"];
                    "text/json": components["schemas"]["TaktMessageDto"];
                };
            };
        };
    };
    "\u5728\u7EBF\u7528\u6237": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktOnlineCreateDto"];
                "application/json": components["schemas"]["TaktOnlineCreateDto"];
                "text/json": components["schemas"]["TaktOnlineCreateDto"];
                "application/*+json": components["schemas"]["TaktOnlineCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktOnlineDto"];
                    "application/json": components["schemas"]["TaktOnlineDto"];
                    "text/json": components["schemas"]["TaktOnlineDto"];
                };
            };
        };
    };
    "\u8BBE\u7F6E": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktSettingsCreateDto"];
                "application/json": components["schemas"]["TaktSettingsCreateDto"];
                "text/json": components["schemas"]["TaktSettingsCreateDto"];
                "application/*+json": components["schemas"]["TaktSettingsCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktSettingsDto"];
                    "application/json": components["schemas"]["TaktSettingsDto"];
                    "text/json": components["schemas"]["TaktSettingsDto"];
                };
            };
        };
    };
    "\u7F16\u7801\u89C4\u5219": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktNumberingRuleCreateDto"];
                "application/json": components["schemas"]["TaktNumberingRuleCreateDto"];
                "text/json": components["schemas"]["TaktNumberingRuleCreateDto"];
                "application/*+json": components["schemas"]["TaktNumberingRuleCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktNumberingRuleDto"];
                    "application/json": components["schemas"]["TaktNumberingRuleDto"];
                    "text/json": components["schemas"]["TaktNumberingRuleDto"];
                };
            };
        };
    };
    "\u8BED\u8A00": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktLanguageCreateDto"];
                "application/json": components["schemas"]["TaktLanguageCreateDto"];
                "text/json": components["schemas"]["TaktLanguageCreateDto"];
                "application/*+json": components["schemas"]["TaktLanguageCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktLanguageDto"];
                    "application/json": components["schemas"]["TaktLanguageDto"];
                    "text/json": components["schemas"]["TaktLanguageDto"];
                };
            };
        };
    };
    "\u7FFB\u8BD1": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktTranslationCreateDto"];
                "application/json": components["schemas"]["TaktTranslationCreateDto"];
                "text/json": components["schemas"]["TaktTranslationCreateDto"];
                "application/*+json": components["schemas"]["TaktTranslationCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktTranslationDto"];
                    "application/json": components["schemas"]["TaktTranslationDto"];
                    "text/json": components["schemas"]["TaktTranslationDto"];
                };
            };
        };
    };
    "\u6587\u4EF6\u7BA1\u7406": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktFileCreateDto"];
                "application/json": components["schemas"]["TaktFileCreateDto"];
                "text/json": components["schemas"]["TaktFileCreateDto"];
                "application/*+json": components["schemas"]["TaktFileCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktFileDto"];
                    "application/json": components["schemas"]["TaktFileDto"];
                    "text/json": components["schemas"]["TaktFileDto"];
                };
            };
        };
    };
    "\u5B57\u5178\u7C7B\u578B": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktDictTypeCreateDto"];
                "application/json": components["schemas"]["TaktDictTypeCreateDto"];
                "text/json": components["schemas"]["TaktDictTypeCreateDto"];
                "application/*+json": components["schemas"]["TaktDictTypeCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktDictTypeDto"];
                    "application/json": components["schemas"]["TaktDictTypeDto"];
                    "text/json": components["schemas"]["TaktDictTypeDto"];
                };
            };
        };
    };
    "\u5B57\u5178\u6570\u636E": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktDictDataCreateDto"];
                "application/json": components["schemas"]["TaktDictDataCreateDto"];
                "text/json": components["schemas"]["TaktDictDataCreateDto"];
                "application/*+json": components["schemas"]["TaktDictDataCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktDictDataDto"];
                    "application/json": components["schemas"]["TaktDictDataDto"];
                    "text/json": components["schemas"]["TaktDictDataDto"];
                };
            };
        };
    };
    "\u7F13\u5B58\u7BA1\u7406": {
        parameters: {
            query?: {
                key?: string;
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
                    "text/plain": components["schemas"]["TaktApiResult"];
                    "application/json": components["schemas"]["TaktApiResult"];
                    "text/json": components["schemas"]["TaktApiResult"];
                };
            };
        };
    };
    "\u516C\u544A\u901A\u77E5": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktAnnouncementCreateDto"];
                "application/json": components["schemas"]["TaktAnnouncementCreateDto"];
                "text/json": components["schemas"]["TaktAnnouncementCreateDto"];
                "application/*+json": components["schemas"]["TaktAnnouncementCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktAnnouncementDto"];
                    "application/json": components["schemas"]["TaktAnnouncementDto"];
                    "text/json": components["schemas"]["TaktAnnouncementDto"];
                };
            };
        };
    };
}
