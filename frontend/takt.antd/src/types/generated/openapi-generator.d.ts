export interface paths {
    "/api/TaktGenTableColumns/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    TableId?: number;
                    DatabaseColumnName?: string;
                    IsPk?: number;
                    IsQuery?: number;
                    IsUnique?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktGenTableColumnDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktGenTableColumnDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktGenTableColumnDto"];
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
    "/api/TaktGenTableColumns/{id}": {
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
                        "text/plain": components["schemas"]["TaktGenTableColumnDto"];
                        "application/json": components["schemas"]["TaktGenTableColumnDto"];
                        "text/json": components["schemas"]["TaktGenTableColumnDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktGenTableColumnUpdateDto"];
                    "application/json": components["schemas"]["TaktGenTableColumnUpdateDto"];
                    "text/json": components["schemas"]["TaktGenTableColumnUpdateDto"];
                    "application/*+json": components["schemas"]["TaktGenTableColumnUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktGenTableColumnDto"];
                        "application/json": components["schemas"]["TaktGenTableColumnDto"];
                        "text/json": components["schemas"]["TaktGenTableColumnDto"];
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
    "/api/TaktGenTableColumns/table/{tableId}": {
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
                    tableId: number;
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
                        "text/plain": components["schemas"]["TaktGenTableColumnDto"][];
                        "application/json": components["schemas"]["TaktGenTableColumnDto"][];
                        "text/json": components["schemas"]["TaktGenTableColumnDto"][];
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
                    tableId: number;
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
    "/api/TaktGenTableColumns": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u4EE3\u7801\u751F\u6210\u5B57\u6BB5\u914D\u7F6E"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktGenTableColumns/batch": {
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
                    "application/json-patch+json": components["schemas"]["TaktGenTableColumnCreateDto"][];
                    "application/json": components["schemas"]["TaktGenTableColumnCreateDto"][];
                    "text/json": components["schemas"]["TaktGenTableColumnCreateDto"][];
                    "application/*+json": components["schemas"]["TaktGenTableColumnCreateDto"][];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktGenTableColumnDto"][];
                        "application/json": components["schemas"]["TaktGenTableColumnDto"][];
                        "text/json": components["schemas"]["TaktGenTableColumnDto"][];
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
    "/api/TaktGenTables/database-configs": {
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
    "/api/TaktGenTables/database-tables": {
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
                        "text/plain": components["schemas"]["TaktDatabaseTableInfoDto"][];
                        "application/json": components["schemas"]["TaktDatabaseTableInfoDto"][];
                        "text/json": components["schemas"]["TaktDatabaseTableInfoDto"][];
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
    "/api/TaktGenTables/import": {
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
                    "application/json-patch+json": components["schemas"]["TaktImportTableRequestDto"];
                    "application/json": components["schemas"]["TaktImportTableRequestDto"];
                    "text/json": components["schemas"]["TaktImportTableRequestDto"];
                    "application/*+json": components["schemas"]["TaktImportTableRequestDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktGenTableDto"];
                        "application/json": components["schemas"]["TaktGenTableDto"];
                        "text/json": components["schemas"]["TaktGenTableDto"];
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
    "/api/TaktGenTables/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    TableName?: string;
                    EntityClassName?: string;
                    GenModuleName?: string;
                    GenBusinessName?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktGenTableDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktGenTableDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktGenTableDto"];
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
    "/api/TaktGenTables/{id}": {
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
                        "text/plain": components["schemas"]["TaktGenTableDto"];
                        "application/json": components["schemas"]["TaktGenTableDto"];
                        "text/json": components["schemas"]["TaktGenTableDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktGenTableUpdateDto"];
                    "application/json": components["schemas"]["TaktGenTableUpdateDto"];
                    "text/json": components["schemas"]["TaktGenTableUpdateDto"];
                    "application/*+json": components["schemas"]["TaktGenTableUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktGenTableDto"];
                        "application/json": components["schemas"]["TaktGenTableDto"];
                        "text/json": components["schemas"]["TaktGenTableDto"];
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
    "/api/TaktGenTables": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u4EE3\u7801\u751F\u6210\u8868\u914D\u7F6E"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktGenTables/{id}/initialize": {
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
    "/api/TaktGenTables/default-gen-path": {
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
    "/api/TaktGenTables/{id}/generate-preview": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    genPath?: string;
                };
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
    "/api/TaktGenTables/{id}/generate": {
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
            requestBody?: {
                content: {
                    "application/json-patch+json": components["schemas"]["GenerateCodeRequest"];
                    "application/json": components["schemas"]["GenerateCodeRequest"];
                    "text/json": components["schemas"]["GenerateCodeRequest"];
                    "application/*+json": components["schemas"]["GenerateCodeRequest"];
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
        /** @default null */
        GenerateCodeRequest: {
            genPath?: string | null;
        } | null;
        TaktDatabaseInfo: {
            configId?: string;
            displayName?: string;
            dataBaseName?: string;
        };
        TaktDatabaseTableInfoDto: {
            tableName?: string;
            tableComment?: string | null;
        };
        TaktGenTableColumnCreateDto: {
            /** Format: int64 */
            tableId?: number;
            databaseColumnName?: string;
            columnComment?: string | null;
            databaseDataType?: string;
            csharpDataType?: string;
            csharpColumnName?: string;
            /** Format: int32 */
            length?: number;
            /** Format: int32 */
            decimalDigits?: number;
            /** Format: int32 */
            isPk?: number;
            /** Format: int32 */
            isIncrement?: number;
            /** Format: int32 */
            isRequired?: number;
            /** Format: int32 */
            isCreate?: number;
            /** Format: int32 */
            isUpdate?: number;
            /** Format: int32 */
            isUnique?: number;
            /** Format: int32 */
            isList?: number;
            /** Format: int32 */
            isExport?: number;
            /** Format: int32 */
            isSort?: number;
            /** Format: int32 */
            isQuery?: number;
            queryType?: string;
            htmlType?: string;
            dictType?: string | null;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktGenTableColumnDto: {
            /** Format: int64 */
            columnId?: number;
            /** Format: int64 */
            tableId?: number;
            databaseColumnName?: string;
            columnComment?: string | null;
            databaseDataType?: string;
            csharpDataType?: string;
            csharpColumnName?: string;
            /** Format: int32 */
            length?: number;
            /** Format: int32 */
            decimalDigits?: number;
            /** Format: int32 */
            isPk?: number;
            /** Format: int32 */
            isIncrement?: number;
            /** Format: int32 */
            isRequired?: number;
            /** Format: int32 */
            isCreate?: number;
            /** Format: int32 */
            isUpdate?: number;
            /** Format: int32 */
            isUnique?: number;
            /** Format: int32 */
            isList?: number;
            /** Format: int32 */
            isExport?: number;
            /** Format: int32 */
            isSort?: number;
            /** Format: int32 */
            isQuery?: number;
            queryType?: string;
            htmlType?: string;
            dictType?: string | null;
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
        TaktGenTableColumnUpdateDto: {
            /** Format: int64 */
            columnId?: number;
            /** Format: int64 */
            tableId?: number;
            databaseColumnName?: string;
            columnComment?: string | null;
            databaseDataType?: string;
            csharpDataType?: string;
            csharpColumnName?: string;
            /** Format: int32 */
            length?: number;
            /** Format: int32 */
            decimalDigits?: number;
            /** Format: int32 */
            isPk?: number;
            /** Format: int32 */
            isIncrement?: number;
            /** Format: int32 */
            isRequired?: number;
            /** Format: int32 */
            isCreate?: number;
            /** Format: int32 */
            isUpdate?: number;
            /** Format: int32 */
            isUnique?: number;
            /** Format: int32 */
            isList?: number;
            /** Format: int32 */
            isExport?: number;
            /** Format: int32 */
            isSort?: number;
            /** Format: int32 */
            isQuery?: number;
            queryType?: string;
            htmlType?: string;
            dictType?: string | null;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktGenTableCreateDto: {
            configId?: string;
            dataSource?: string | null;
            tableName?: string;
            tableComment?: string | null;
            subTableName?: string | null;
            subTableFkName?: string | null;
            treeCode?: string | null;
            treeParentCode?: string | null;
            treeName?: string | null;
            /** Format: int32 */
            inDatabase?: number;
            genTemplate?: string;
            namePrefix?: string | null;
            entityNamespace?: string | null;
            entityClassName?: string;
            dtoNamespace?: string | null;
            dtoClassName?: string | null;
            dtoCategory?: string | null;
            serviceNamespace?: string | null;
            iServiceClassName?: string | null;
            serviceClassName?: string | null;
            controllerNamespace?: string | null;
            controllerClassName?: string | null;
            repositoryInterfaceNamespace?: string | null;
            iRepositoryClassName?: string | null;
            repositoryNamespace?: string | null;
            repositoryClassName?: string | null;
            genModuleName?: string | null;
            genBusinessName?: string;
            genFunctionName?: string | null;
            genFunction?: string | null;
            /** Format: int32 */
            genMethod?: number;
            /** Format: int32 */
            isRepository?: number;
            genPath?: string;
            /** Format: int64 */
            parentMenuId?: number;
            /** Format: int32 */
            isGenMenu?: number;
            /** Format: int32 */
            isGenTranslation?: number;
            sortType?: string;
            sortField?: string;
            permsPrefix?: string;
            /** Format: int32 */
            frontTemplate?: number;
            /** Format: int32 */
            frontStyle?: number;
            /** Format: int32 */
            btnStyle?: number;
            /** Format: int32 */
            isGenCode?: number;
            /** Format: int32 */
            genCodeCount?: number;
            /** Format: int32 */
            isUseTabs?: number;
            /** Format: int32 */
            tabsFieldCount?: number;
            genAuthor?: string;
            options?: string | null;
            remark?: string | null;
            columns?: components["schemas"]["TaktGenTableColumnDto"][] | null;
        } | null;
        TaktGenTableCreateDto2: {
            configId?: string;
            dataSource?: string | null;
            tableName?: string;
            tableComment?: string | null;
            subTableName?: string | null;
            subTableFkName?: string | null;
            treeCode?: string | null;
            treeParentCode?: string | null;
            treeName?: string | null;
            /** Format: int32 */
            inDatabase?: number;
            genTemplate?: string;
            namePrefix?: string | null;
            entityNamespace?: string | null;
            entityClassName?: string;
            dtoNamespace?: string | null;
            dtoClassName?: string | null;
            dtoCategory?: string | null;
            serviceNamespace?: string | null;
            iServiceClassName?: string | null;
            serviceClassName?: string | null;
            controllerNamespace?: string | null;
            controllerClassName?: string | null;
            repositoryInterfaceNamespace?: string | null;
            iRepositoryClassName?: string | null;
            repositoryNamespace?: string | null;
            repositoryClassName?: string | null;
            genModuleName?: string | null;
            genBusinessName?: string;
            genFunctionName?: string | null;
            genFunction?: string | null;
            /** Format: int32 */
            genMethod?: number;
            /** Format: int32 */
            isRepository?: number;
            genPath?: string;
            /** Format: int64 */
            parentMenuId?: number;
            /** Format: int32 */
            isGenMenu?: number;
            /** Format: int32 */
            isGenTranslation?: number;
            sortType?: string;
            sortField?: string;
            permsPrefix?: string;
            /** Format: int32 */
            frontTemplate?: number;
            /** Format: int32 */
            frontStyle?: number;
            /** Format: int32 */
            btnStyle?: number;
            /** Format: int32 */
            isGenCode?: number;
            /** Format: int32 */
            genCodeCount?: number;
            /** Format: int32 */
            isUseTabs?: number;
            /** Format: int32 */
            tabsFieldCount?: number;
            genAuthor?: string;
            options?: string | null;
            remark?: string | null;
            columns?: components["schemas"]["TaktGenTableColumnDto"][] | null;
        };
        TaktGenTableDto: {
            /** Format: int64 */
            tableId?: number;
            dataSource?: string | null;
            tableName?: string;
            tableComment?: string | null;
            subTableName?: string | null;
            subTableFkName?: string | null;
            treeCode?: string | null;
            treeParentCode?: string | null;
            treeName?: string | null;
            /** Format: int32 */
            inDatabase?: number;
            genTemplate?: string;
            namePrefix?: string | null;
            entityNamespace?: string | null;
            entityClassName?: string;
            dtoNamespace?: string | null;
            dtoClassName?: string | null;
            dtoCategory?: string | null;
            serviceNamespace?: string | null;
            iServiceClassName?: string | null;
            serviceClassName?: string | null;
            controllerNamespace?: string | null;
            controllerClassName?: string | null;
            repositoryInterfaceNamespace?: string | null;
            iRepositoryClassName?: string | null;
            repositoryNamespace?: string | null;
            repositoryClassName?: string | null;
            genModuleName?: string | null;
            genBusinessName?: string;
            genFunctionName?: string | null;
            genFunction?: string | null;
            /** Format: int32 */
            genMethod?: number;
            /** Format: int32 */
            isRepository?: number;
            genPath?: string;
            /** Format: int64 */
            parentMenuId?: number;
            /** Format: int32 */
            isGenMenu?: number;
            /** Format: int32 */
            isGenTranslation?: number;
            sortType?: string;
            sortField?: string;
            permsPrefix?: string;
            /** Format: int32 */
            frontTemplate?: number;
            /** Format: int32 */
            frontStyle?: number;
            /** Format: int32 */
            btnStyle?: number;
            /** Format: int32 */
            isGenCode?: number;
            /** Format: int32 */
            genCodeCount?: number;
            /** Format: int32 */
            isUseTabs?: number;
            /** Format: int32 */
            tabsFieldCount?: number;
            genAuthor?: string;
            options?: string | null;
            columns?: components["schemas"]["TaktGenTableColumnDto"][] | null;
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
        TaktGenTableUpdateDto: {
            /** Format: int64 */
            tableId?: number;
            configId?: string;
            dataSource?: string | null;
            tableName?: string;
            tableComment?: string | null;
            subTableName?: string | null;
            subTableFkName?: string | null;
            treeCode?: string | null;
            treeParentCode?: string | null;
            treeName?: string | null;
            /** Format: int32 */
            inDatabase?: number;
            genTemplate?: string;
            namePrefix?: string | null;
            entityNamespace?: string | null;
            entityClassName?: string;
            dtoNamespace?: string | null;
            dtoClassName?: string | null;
            dtoCategory?: string | null;
            serviceNamespace?: string | null;
            iServiceClassName?: string | null;
            serviceClassName?: string | null;
            controllerNamespace?: string | null;
            controllerClassName?: string | null;
            repositoryInterfaceNamespace?: string | null;
            iRepositoryClassName?: string | null;
            repositoryNamespace?: string | null;
            repositoryClassName?: string | null;
            genModuleName?: string | null;
            genBusinessName?: string;
            genFunctionName?: string | null;
            genFunction?: string | null;
            /** Format: int32 */
            genMethod?: number;
            /** Format: int32 */
            isRepository?: number;
            genPath?: string;
            /** Format: int64 */
            parentMenuId?: number;
            /** Format: int32 */
            isGenMenu?: number;
            /** Format: int32 */
            isGenTranslation?: number;
            sortType?: string;
            sortField?: string;
            permsPrefix?: string;
            /** Format: int32 */
            frontTemplate?: number;
            /** Format: int32 */
            frontStyle?: number;
            /** Format: int32 */
            btnStyle?: number;
            /** Format: int32 */
            isGenCode?: number;
            /** Format: int32 */
            genCodeCount?: number;
            /** Format: int32 */
            isUseTabs?: number;
            /** Format: int32 */
            tabsFieldCount?: number;
            genAuthor?: string;
            options?: string | null;
            remark?: string | null;
            columns?: components["schemas"]["TaktGenTableColumnDto"][] | null;
        };
        TaktImportTableRequestDto: {
            configId?: string;
            tableName?: string;
            tableOverrides?: components["schemas"]["TaktGenTableCreateDto"];
        };
        TaktPagedResultOfTaktGenTableColumnDto: {
            data?: components["schemas"]["TaktGenTableColumnDto"][];
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
        TaktPagedResultOfTaktGenTableDto: {
            data?: components["schemas"]["TaktGenTableDto"][];
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
    };
    responses: never;
    parameters: never;
    requestBodies: never;
    headers: never;
    pathItems: never;
}
export type $defs = Record<string, never>;
export interface operations {
    "\u4EE3\u7801\u751F\u6210\u5B57\u6BB5\u914D\u7F6E": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktGenTableColumnCreateDto"];
                "application/json": components["schemas"]["TaktGenTableColumnCreateDto"];
                "text/json": components["schemas"]["TaktGenTableColumnCreateDto"];
                "application/*+json": components["schemas"]["TaktGenTableColumnCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktGenTableColumnDto"];
                    "application/json": components["schemas"]["TaktGenTableColumnDto"];
                    "text/json": components["schemas"]["TaktGenTableColumnDto"];
                };
            };
        };
    };
    "\u4EE3\u7801\u751F\u6210\u8868\u914D\u7F6E": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktGenTableCreateDto2"];
                "application/json": components["schemas"]["TaktGenTableCreateDto2"];
                "text/json": components["schemas"]["TaktGenTableCreateDto2"];
                "application/*+json": components["schemas"]["TaktGenTableCreateDto2"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktGenTableDto"];
                    "application/json": components["schemas"]["TaktGenTableDto"];
                    "text/json": components["schemas"]["TaktGenTableDto"];
                };
            };
        };
    };
}
