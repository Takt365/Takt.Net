export interface paths {
    "/api/TaktEmployeeAttachments/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    FileId?: number;
                    AttachmentType?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktEmployeeAttachmentDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktEmployeeAttachmentDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktEmployeeAttachmentDto"];
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
    "/api/TaktEmployeeAttachments/{id}": {
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
                        "text/plain": components["schemas"]["TaktEmployeeAttachmentDto"];
                        "application/json": components["schemas"]["TaktEmployeeAttachmentDto"];
                        "text/json": components["schemas"]["TaktEmployeeAttachmentDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeAttachmentUpdateDto"];
                    "application/json": components["schemas"]["TaktEmployeeAttachmentUpdateDto"];
                    "text/json": components["schemas"]["TaktEmployeeAttachmentUpdateDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeAttachmentUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktEmployeeAttachmentDto"];
                        "application/json": components["schemas"]["TaktEmployeeAttachmentDto"];
                        "text/json": components["schemas"]["TaktEmployeeAttachmentDto"];
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
    "/api/TaktEmployeeAttachments": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5458\u5DE5\u9644\u4EF6"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktEmployeeAttachments/batch": {
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
    "/api/TaktEmployeeAttachments/template": {
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
    "/api/TaktEmployeeAttachments/import": {
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
    "/api/TaktEmployeeAttachments/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeAttachmentQueryDto"];
                    "application/json": components["schemas"]["TaktEmployeeAttachmentQueryDto"];
                    "text/json": components["schemas"]["TaktEmployeeAttachmentQueryDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeAttachmentQueryDto"];
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
    "/api/TaktEmployeeCareers/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    DeptId?: number;
                    PostId?: number;
                    IsPrimary?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktEmployeeCareerDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktEmployeeCareerDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktEmployeeCareerDto"];
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
    "/api/TaktEmployeeCareers/{id}": {
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
                        "text/plain": components["schemas"]["TaktEmployeeCareerDto"];
                        "application/json": components["schemas"]["TaktEmployeeCareerDto"];
                        "text/json": components["schemas"]["TaktEmployeeCareerDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeCareerUpdateDto"];
                    "application/json": components["schemas"]["TaktEmployeeCareerUpdateDto"];
                    "text/json": components["schemas"]["TaktEmployeeCareerUpdateDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeCareerUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktEmployeeCareerDto"];
                        "application/json": components["schemas"]["TaktEmployeeCareerDto"];
                        "text/json": components["schemas"]["TaktEmployeeCareerDto"];
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
    "/api/TaktEmployeeCareers": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5458\u5DE5\u804C\u4E1A\u4FE1\u606F"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktEmployeeCareers/batch": {
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
    "/api/TaktEmployeeCareers/template": {
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
    "/api/TaktEmployeeCareers/import": {
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
    "/api/TaktEmployeeCareers/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeCareerQueryDto"];
                    "application/json": components["schemas"]["TaktEmployeeCareerQueryDto"];
                    "text/json": components["schemas"]["TaktEmployeeCareerQueryDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeCareerQueryDto"];
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
    "/api/TaktEmployeeContracts/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    ContractStatus?: number;
                    ContractNo?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktEmployeeContractDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktEmployeeContractDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktEmployeeContractDto"];
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
    "/api/TaktEmployeeContracts/{id}": {
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
                        "text/plain": components["schemas"]["TaktEmployeeContractDto"];
                        "application/json": components["schemas"]["TaktEmployeeContractDto"];
                        "text/json": components["schemas"]["TaktEmployeeContractDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeContractUpdateDto"];
                    "application/json": components["schemas"]["TaktEmployeeContractUpdateDto"];
                    "text/json": components["schemas"]["TaktEmployeeContractUpdateDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeContractUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktEmployeeContractDto"];
                        "application/json": components["schemas"]["TaktEmployeeContractDto"];
                        "text/json": components["schemas"]["TaktEmployeeContractDto"];
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
    "/api/TaktEmployeeContracts": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5458\u5DE5\u5408\u540C"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktEmployeeContracts/batch": {
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
    "/api/TaktEmployeeContracts/template": {
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
    "/api/TaktEmployeeContracts/import": {
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
    "/api/TaktEmployeeContracts/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeContractQueryDto"];
                    "application/json": components["schemas"]["TaktEmployeeContractQueryDto"];
                    "text/json": components["schemas"]["TaktEmployeeContractQueryDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeContractQueryDto"];
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
    "/api/TaktEmployeeEducations/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    EducationLevel?: number;
                    IsHighest?: number;
                    SchoolName?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktEmployeeEducationDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktEmployeeEducationDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktEmployeeEducationDto"];
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
    "/api/TaktEmployeeEducations/{id}": {
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
                        "text/plain": components["schemas"]["TaktEmployeeEducationDto"];
                        "application/json": components["schemas"]["TaktEmployeeEducationDto"];
                        "text/json": components["schemas"]["TaktEmployeeEducationDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeEducationUpdateDto"];
                    "application/json": components["schemas"]["TaktEmployeeEducationUpdateDto"];
                    "text/json": components["schemas"]["TaktEmployeeEducationUpdateDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeEducationUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktEmployeeEducationDto"];
                        "application/json": components["schemas"]["TaktEmployeeEducationDto"];
                        "text/json": components["schemas"]["TaktEmployeeEducationDto"];
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
    "/api/TaktEmployeeEducations": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5458\u5DE5\u6559\u80B2\u7ECF\u5386"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktEmployeeEducations/batch": {
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
    "/api/TaktEmployeeEducations/template": {
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
    "/api/TaktEmployeeEducations/import": {
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
    "/api/TaktEmployeeEducations/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeEducationQueryDto"];
                    "application/json": components["schemas"]["TaktEmployeeEducationQueryDto"];
                    "text/json": components["schemas"]["TaktEmployeeEducationQueryDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeEducationQueryDto"];
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
    "/api/TaktEmployeeFamilies/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    RelationType?: number;
                    MemberName?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktEmployeeFamilyDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktEmployeeFamilyDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktEmployeeFamilyDto"];
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
    "/api/TaktEmployeeFamilies/{id}": {
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
                        "text/plain": components["schemas"]["TaktEmployeeFamilyDto"];
                        "application/json": components["schemas"]["TaktEmployeeFamilyDto"];
                        "text/json": components["schemas"]["TaktEmployeeFamilyDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeFamilyUpdateDto"];
                    "application/json": components["schemas"]["TaktEmployeeFamilyUpdateDto"];
                    "text/json": components["schemas"]["TaktEmployeeFamilyUpdateDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeFamilyUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktEmployeeFamilyDto"];
                        "application/json": components["schemas"]["TaktEmployeeFamilyDto"];
                        "text/json": components["schemas"]["TaktEmployeeFamilyDto"];
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
    "/api/TaktEmployeeFamilies": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5458\u5DE5\u5BB6\u5EAD\u6210\u5458"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktEmployeeFamilies/batch": {
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
    "/api/TaktEmployeeFamilies/template": {
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
    "/api/TaktEmployeeFamilies/import": {
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
    "/api/TaktEmployeeFamilies/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeFamilyQueryDto"];
                    "application/json": components["schemas"]["TaktEmployeeFamilyQueryDto"];
                    "text/json": components["schemas"]["TaktEmployeeFamilyQueryDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeFamilyQueryDto"];
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
    "/api/TaktEmployees/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    RealName?: string;
                    EmployeeCode?: string;
                    Phone?: string;
                    EmployeeStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktEmployeeDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktEmployeeDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktEmployeeDto"];
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
    "/api/TaktEmployees/{id}": {
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
                        "text/plain": components["schemas"]["TaktEmployeeDto"];
                        "application/json": components["schemas"]["TaktEmployeeDto"];
                        "text/json": components["schemas"]["TaktEmployeeDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeUpdateDto"];
                    "application/json": components["schemas"]["TaktEmployeeUpdateDto"];
                    "text/json": components["schemas"]["TaktEmployeeUpdateDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktEmployeeDto"];
                        "application/json": components["schemas"]["TaktEmployeeDto"];
                        "text/json": components["schemas"]["TaktEmployeeDto"];
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
    "/api/TaktEmployees": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5458\u5DE5"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktEmployees/delete": {
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
    "/api/TaktEmployees/template": {
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
    "/api/TaktEmployees/import": {
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
    "/api/TaktEmployees/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeQueryDto"];
                    "application/json": components["schemas"]["TaktEmployeeQueryDto"];
                    "text/json": components["schemas"]["TaktEmployeeQueryDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeQueryDto"];
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
    "/api/TaktEmployees/options": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    excludeBoundToUser?: boolean;
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
    "/api/TaktEmployees/{employeeId}/depts": {
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
                    employeeId: number;
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
                        "text/plain": components["schemas"]["TaktEmployeeDeptDto"][];
                        "application/json": components["schemas"]["TaktEmployeeDeptDto"][];
                        "text/json": components["schemas"]["TaktEmployeeDeptDto"][];
                    };
                };
            };
        };
        put: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    employeeId: number;
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
    "/api/TaktEmployees/{employeeId}/posts": {
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
                    employeeId: number;
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
                        "text/plain": components["schemas"]["TaktEmployeePostDto"][];
                        "application/json": components["schemas"]["TaktEmployeePostDto"][];
                        "text/json": components["schemas"]["TaktEmployeePostDto"][];
                    };
                };
            };
        };
        put: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    employeeId: number;
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
    "/api/TaktEmployeeSkills/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    SkillName?: string;
                    SkillLevel?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktEmployeeSkillDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktEmployeeSkillDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktEmployeeSkillDto"];
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
    "/api/TaktEmployeeSkills/{id}": {
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
                        "text/plain": components["schemas"]["TaktEmployeeSkillDto"];
                        "application/json": components["schemas"]["TaktEmployeeSkillDto"];
                        "text/json": components["schemas"]["TaktEmployeeSkillDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeSkillUpdateDto"];
                    "application/json": components["schemas"]["TaktEmployeeSkillUpdateDto"];
                    "text/json": components["schemas"]["TaktEmployeeSkillUpdateDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeSkillUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktEmployeeSkillDto"];
                        "application/json": components["schemas"]["TaktEmployeeSkillDto"];
                        "text/json": components["schemas"]["TaktEmployeeSkillDto"];
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
    "/api/TaktEmployeeSkills": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5458\u5DE5\u4E1A\u52A1\u6280\u80FD"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktEmployeeSkills/batch": {
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
    "/api/TaktEmployeeSkills/template": {
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
    "/api/TaktEmployeeSkills/import": {
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
    "/api/TaktEmployeeSkills/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeSkillQueryDto"];
                    "application/json": components["schemas"]["TaktEmployeeSkillQueryDto"];
                    "text/json": components["schemas"]["TaktEmployeeSkillQueryDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeSkillQueryDto"];
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
    "/api/TaktEmployeeTransfers/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    TransferType?: number;
                    TransferStatus?: number;
                    EffectiveDateFrom?: string;
                    EffectiveDateTo?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktEmployeeTransferDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktEmployeeTransferDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktEmployeeTransferDto"];
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
    "/api/TaktEmployeeTransfers/{id}": {
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
                        "text/plain": components["schemas"]["TaktEmployeeTransferDto"];
                        "application/json": components["schemas"]["TaktEmployeeTransferDto"];
                        "text/json": components["schemas"]["TaktEmployeeTransferDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeTransferUpdateDto"];
                    "application/json": components["schemas"]["TaktEmployeeTransferUpdateDto"];
                    "text/json": components["schemas"]["TaktEmployeeTransferUpdateDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeTransferUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktEmployeeTransferDto"];
                        "application/json": components["schemas"]["TaktEmployeeTransferDto"];
                        "text/json": components["schemas"]["TaktEmployeeTransferDto"];
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
    "/api/TaktEmployeeTransfers": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5458\u5DE5\u8C03\u52A8"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktEmployeeTransfers/batch": {
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
    "/api/TaktEmployeeTransfers/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeTransferStatusDto"];
                    "application/json": components["schemas"]["TaktEmployeeTransferStatusDto"];
                    "text/json": components["schemas"]["TaktEmployeeTransferStatusDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeTransferStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktEmployeeTransferDto"];
                        "application/json": components["schemas"]["TaktEmployeeTransferDto"];
                        "text/json": components["schemas"]["TaktEmployeeTransferDto"];
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
    "/api/TaktEmployeeTransfers/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeTransferQueryDto"];
                    "application/json": components["schemas"]["TaktEmployeeTransferQueryDto"];
                    "text/json": components["schemas"]["TaktEmployeeTransferQueryDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeTransferQueryDto"];
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
    "/api/TaktEmployeeWorks/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    CompanyName?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktEmployeeWorkDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktEmployeeWorkDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktEmployeeWorkDto"];
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
    "/api/TaktEmployeeWorks/{id}": {
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
                        "text/plain": components["schemas"]["TaktEmployeeWorkDto"];
                        "application/json": components["schemas"]["TaktEmployeeWorkDto"];
                        "text/json": components["schemas"]["TaktEmployeeWorkDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeWorkUpdateDto"];
                    "application/json": components["schemas"]["TaktEmployeeWorkUpdateDto"];
                    "text/json": components["schemas"]["TaktEmployeeWorkUpdateDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeWorkUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktEmployeeWorkDto"];
                        "application/json": components["schemas"]["TaktEmployeeWorkDto"];
                        "text/json": components["schemas"]["TaktEmployeeWorkDto"];
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
    "/api/TaktEmployeeWorks": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5458\u5DE5\u5DE5\u4F5C\u7ECF\u5386"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktEmployeeWorks/batch": {
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
    "/api/TaktEmployeeWorks/template": {
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
    "/api/TaktEmployeeWorks/import": {
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
    "/api/TaktEmployeeWorks/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktEmployeeWorkQueryDto"];
                    "application/json": components["schemas"]["TaktEmployeeWorkQueryDto"];
                    "text/json": components["schemas"]["TaktEmployeeWorkQueryDto"];
                    "application/*+json": components["schemas"]["TaktEmployeeWorkQueryDto"];
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
    "/api/TaktDepts/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    DeptName?: string;
                    DeptCode?: string;
                    ParentId?: number;
                    DeptStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktDeptDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktDeptDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktDeptDto"];
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
    "/api/TaktDepts/{id}": {
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
                        "text/plain": components["schemas"]["TaktDeptDto"];
                        "application/json": components["schemas"]["TaktDeptDto"];
                        "text/json": components["schemas"]["TaktDeptDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktDeptUpdateDto"];
                    "application/json": components["schemas"]["TaktDeptUpdateDto"];
                    "text/json": components["schemas"]["TaktDeptUpdateDto"];
                    "application/*+json": components["schemas"]["TaktDeptUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktDeptDto"];
                        "application/json": components["schemas"]["TaktDeptDto"];
                        "text/json": components["schemas"]["TaktDeptDto"];
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
    "/api/TaktDepts/tree-options": {
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
    "/api/TaktDepts": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u90E8\u95E8"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktDepts/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktDeptStatusDto"];
                    "application/json": components["schemas"]["TaktDeptStatusDto"];
                    "text/json": components["schemas"]["TaktDeptStatusDto"];
                    "application/*+json": components["schemas"]["TaktDeptStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktDeptDto"];
                        "application/json": components["schemas"]["TaktDeptDto"];
                        "text/json": components["schemas"]["TaktDeptDto"];
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
    "/api/TaktDepts/{deptId}/users": {
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
                    deptId: number;
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
                    deptId: number;
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
    "/api/TaktDepts/{deptId}/roles": {
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
                    deptId: number;
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
        post: {
            parameters: {
                query?: never;
                header?: never;
                path: {
                    deptId: number;
                };
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
                    content: {
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktRoleDeptDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktRoleDeptDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktRoleDeptDto"];
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
    "/api/TaktDepts/template": {
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
    "/api/TaktDepts/import": {
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
    "/api/TaktDepts/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktDeptQueryDto"];
                    "application/json": components["schemas"]["TaktDeptQueryDto"];
                    "text/json": components["schemas"]["TaktDeptQueryDto"];
                    "application/*+json": components["schemas"]["TaktDeptQueryDto"];
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
    "/api/TaktPosts/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    PostName?: string;
                    PostCode?: string;
                    DeptId?: number;
                    PostStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktPostDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktPostDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktPostDto"];
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
    "/api/TaktPosts/{id}": {
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
                        "text/plain": components["schemas"]["TaktPostDto"];
                        "application/json": components["schemas"]["TaktPostDto"];
                        "text/json": components["schemas"]["TaktPostDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktPostUpdateDto"];
                    "application/json": components["schemas"]["TaktPostUpdateDto"];
                    "text/json": components["schemas"]["TaktPostUpdateDto"];
                    "application/*+json": components["schemas"]["TaktPostUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktPostDto"];
                        "application/json": components["schemas"]["TaktPostDto"];
                        "text/json": components["schemas"]["TaktPostDto"];
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
    "/api/TaktPosts/options": {
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
    "/api/TaktPosts": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5C97\u4F4D"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktPosts/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktPostStatusDto"];
                    "application/json": components["schemas"]["TaktPostStatusDto"];
                    "text/json": components["schemas"]["TaktPostStatusDto"];
                    "application/*+json": components["schemas"]["TaktPostStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktPostDto"];
                        "application/json": components["schemas"]["TaktPostDto"];
                        "text/json": components["schemas"]["TaktPostDto"];
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
    "/api/TaktPosts/{postId}/users": {
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
                    postId: number;
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
                    postId: number;
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
    "/api/TaktPosts/template": {
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
    "/api/TaktPosts/import": {
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
    "/api/TaktPosts/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktPostQueryDto"];
                    "application/json": components["schemas"]["TaktPostQueryDto"];
                    "text/json": components["schemas"]["TaktPostQueryDto"];
                    "application/*+json": components["schemas"]["TaktPostQueryDto"];
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
    "/api/TaktAttendanceCorrections/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    TargetDateFrom?: string;
                    TargetDateTo?: string;
                    ApprovalStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktAttendanceCorrectionDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktAttendanceCorrectionDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktAttendanceCorrectionDto"];
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
    "/api/TaktAttendanceCorrections/{id}": {
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
                        "text/plain": components["schemas"]["TaktAttendanceCorrectionDto"];
                        "application/json": components["schemas"]["TaktAttendanceCorrectionDto"];
                        "text/json": components["schemas"]["TaktAttendanceCorrectionDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktAttendanceCorrectionUpdateDto"];
                    "application/json": components["schemas"]["TaktAttendanceCorrectionUpdateDto"];
                    "text/json": components["schemas"]["TaktAttendanceCorrectionUpdateDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceCorrectionUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAttendanceCorrectionDto"];
                        "application/json": components["schemas"]["TaktAttendanceCorrectionDto"];
                        "text/json": components["schemas"]["TaktAttendanceCorrectionDto"];
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
    "/api/TaktAttendanceCorrections": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u8865\u5361"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAttendanceCorrections/batch": {
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
    "/api/TaktAttendanceCorrections/template": {
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
    "/api/TaktAttendanceCorrections/import": {
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
    "/api/TaktAttendanceCorrections/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktAttendanceCorrectionQueryDto"];
                    "application/json": components["schemas"]["TaktAttendanceCorrectionQueryDto"];
                    "text/json": components["schemas"]["TaktAttendanceCorrectionQueryDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceCorrectionQueryDto"];
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
    "/api/TaktAttendanceDevices/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    DeviceCode?: string;
                    DeviceName?: string;
                    DeviceType?: string;
                    Manufacturer?: string;
                    DeviceStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktAttendanceDeviceDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktAttendanceDeviceDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktAttendanceDeviceDto"];
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
    "/api/TaktAttendanceDevices/{id}": {
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
                        "text/plain": components["schemas"]["TaktAttendanceDeviceDto"];
                        "application/json": components["schemas"]["TaktAttendanceDeviceDto"];
                        "text/json": components["schemas"]["TaktAttendanceDeviceDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktAttendanceDeviceUpdateDto"];
                    "application/json": components["schemas"]["TaktAttendanceDeviceUpdateDto"];
                    "text/json": components["schemas"]["TaktAttendanceDeviceUpdateDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceDeviceUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAttendanceDeviceDto"];
                        "application/json": components["schemas"]["TaktAttendanceDeviceDto"];
                        "text/json": components["schemas"]["TaktAttendanceDeviceDto"];
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
    "/api/TaktAttendanceDevices": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u8003\u52E4\u8BBE\u5907"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAttendanceDevices/batch": {
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
    "/api/TaktAttendanceDevices/template": {
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
    "/api/TaktAttendanceDevices/import": {
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
    "/api/TaktAttendanceDevices/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktAttendanceDeviceQueryDto"];
                    "application/json": components["schemas"]["TaktAttendanceDeviceQueryDto"];
                    "text/json": components["schemas"]["TaktAttendanceDeviceQueryDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceDeviceQueryDto"];
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
    "/api/TaktAttendanceExceptions/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    ExceptionDateFrom?: string;
                    ExceptionDateTo?: string;
                    ExceptionType?: number;
                    HandleStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktAttendanceExceptionDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktAttendanceExceptionDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktAttendanceExceptionDto"];
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
    "/api/TaktAttendanceExceptions/{id}": {
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
                        "text/plain": components["schemas"]["TaktAttendanceExceptionDto"];
                        "application/json": components["schemas"]["TaktAttendanceExceptionDto"];
                        "text/json": components["schemas"]["TaktAttendanceExceptionDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktAttendanceExceptionUpdateDto"];
                    "application/json": components["schemas"]["TaktAttendanceExceptionUpdateDto"];
                    "text/json": components["schemas"]["TaktAttendanceExceptionUpdateDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceExceptionUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAttendanceExceptionDto"];
                        "application/json": components["schemas"]["TaktAttendanceExceptionDto"];
                        "text/json": components["schemas"]["TaktAttendanceExceptionDto"];
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
    "/api/TaktAttendanceExceptions": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u8003\u52E4\u5F02\u5E38"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAttendanceExceptions/batch": {
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
    "/api/TaktAttendanceExceptions/template": {
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
    "/api/TaktAttendanceExceptions/import": {
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
    "/api/TaktAttendanceExceptions/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktAttendanceExceptionQueryDto"];
                    "application/json": components["schemas"]["TaktAttendanceExceptionQueryDto"];
                    "text/json": components["schemas"]["TaktAttendanceExceptionQueryDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceExceptionQueryDto"];
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
    "/api/TaktAttendanceIngest/push": {
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
                    "application/json-patch+json": components["schemas"]["TaktAttendancePushRequestDto"];
                    "application/json": components["schemas"]["TaktAttendancePushRequestDto"];
                    "text/json": components["schemas"]["TaktAttendancePushRequestDto"];
                    "application/*+json": components["schemas"]["TaktAttendancePushRequestDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAttendancePushHandleResultDto"];
                        "application/json": components["schemas"]["TaktAttendancePushHandleResultDto"];
                        "text/json": components["schemas"]["TaktAttendancePushHandleResultDto"];
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
    "/api/TaktAttendanceIngest/pull": {
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
                    "application/json-patch+json": components["schemas"]["TaktAttendancePullRequestDto"];
                    "application/json": components["schemas"]["TaktAttendancePullRequestDto"];
                    "text/json": components["schemas"]["TaktAttendancePullRequestDto"];
                    "application/*+json": components["schemas"]["TaktAttendancePullRequestDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAttendancePullResultDto"];
                        "application/json": components["schemas"]["TaktAttendancePullResultDto"];
                        "text/json": components["schemas"]["TaktAttendancePullResultDto"];
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
    "/api/TaktAttendanceIngest/devices/{deviceId}/status": {
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
                    deviceId: number;
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
                        "text/plain": components["schemas"]["TaktAttendanceDeviceStatusDto"];
                        "application/json": components["schemas"]["TaktAttendanceDeviceStatusDto"];
                        "text/json": components["schemas"]["TaktAttendanceDeviceStatusDto"];
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
    "/api/TaktAttendanceIngest/devices/{deviceId}/sync-users": {
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
                    deviceId: number;
                };
                cookie?: never;
            };
            requestBody: {
                content: {
                    "application/json-patch+json": components["schemas"]["TaktAttendanceUserSyncRequestDto"];
                    "application/json": components["schemas"]["TaktAttendanceUserSyncRequestDto"];
                    "text/json": components["schemas"]["TaktAttendanceUserSyncRequestDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceUserSyncRequestDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAttendanceUserSyncResultDto"];
                        "application/json": components["schemas"]["TaktAttendanceUserSyncResultDto"];
                        "text/json": components["schemas"]["TaktAttendanceUserSyncResultDto"];
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
    "/api/TaktAttendancePunches/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    PunchType?: number;
                    PunchTimeFrom?: string;
                    PunchTimeTo?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktAttendancePunchDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktAttendancePunchDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktAttendancePunchDto"];
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
    "/api/TaktAttendancePunches/{id}": {
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
                        "text/plain": components["schemas"]["TaktAttendancePunchDto"];
                        "application/json": components["schemas"]["TaktAttendancePunchDto"];
                        "text/json": components["schemas"]["TaktAttendancePunchDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktAttendancePunchUpdateDto"];
                    "application/json": components["schemas"]["TaktAttendancePunchUpdateDto"];
                    "text/json": components["schemas"]["TaktAttendancePunchUpdateDto"];
                    "application/*+json": components["schemas"]["TaktAttendancePunchUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAttendancePunchDto"];
                        "application/json": components["schemas"]["TaktAttendancePunchDto"];
                        "text/json": components["schemas"]["TaktAttendancePunchDto"];
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
    "/api/TaktAttendancePunches": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u6253\u5361"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAttendancePunches/batch": {
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
    "/api/TaktAttendancePunches/template": {
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
    "/api/TaktAttendancePunches/import": {
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
    "/api/TaktAttendancePunches/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktAttendancePunchQueryDto"];
                    "application/json": components["schemas"]["TaktAttendancePunchQueryDto"];
                    "text/json": components["schemas"]["TaktAttendancePunchQueryDto"];
                    "application/*+json": components["schemas"]["TaktAttendancePunchQueryDto"];
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
    "/api/TaktAttendanceResults/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    AttendanceDateFrom?: string;
                    AttendanceDateTo?: string;
                    AttendanceStatus?: number;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktAttendanceResultDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktAttendanceResultDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktAttendanceResultDto"];
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
    "/api/TaktAttendanceResults/{id}": {
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
                        "text/plain": components["schemas"]["TaktAttendanceResultDto"];
                        "application/json": components["schemas"]["TaktAttendanceResultDto"];
                        "text/json": components["schemas"]["TaktAttendanceResultDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktAttendanceResultUpdateDto"];
                    "application/json": components["schemas"]["TaktAttendanceResultUpdateDto"];
                    "text/json": components["schemas"]["TaktAttendanceResultUpdateDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceResultUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAttendanceResultDto"];
                        "application/json": components["schemas"]["TaktAttendanceResultDto"];
                        "text/json": components["schemas"]["TaktAttendanceResultDto"];
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
    "/api/TaktAttendanceResults": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u8003\u52E4\u7ED3\u679C"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAttendanceResults/batch": {
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
    "/api/TaktAttendanceResults/template": {
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
    "/api/TaktAttendanceResults/import": {
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
    "/api/TaktAttendanceResults/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktAttendanceResultQueryDto"];
                    "application/json": components["schemas"]["TaktAttendanceResultQueryDto"];
                    "text/json": components["schemas"]["TaktAttendanceResultQueryDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceResultQueryDto"];
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
    "/api/TaktAttendanceSettings/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    SettingCode?: string;
                    SettingName?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktAttendanceSettingDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktAttendanceSettingDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktAttendanceSettingDto"];
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
    "/api/TaktAttendanceSettings/{id}": {
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
                        "text/plain": components["schemas"]["TaktAttendanceSettingDto"];
                        "application/json": components["schemas"]["TaktAttendanceSettingDto"];
                        "text/json": components["schemas"]["TaktAttendanceSettingDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktAttendanceSettingUpdateDto"];
                    "application/json": components["schemas"]["TaktAttendanceSettingUpdateDto"];
                    "text/json": components["schemas"]["TaktAttendanceSettingUpdateDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceSettingUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAttendanceSettingDto"];
                        "application/json": components["schemas"]["TaktAttendanceSettingDto"];
                        "text/json": components["schemas"]["TaktAttendanceSettingDto"];
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
    "/api/TaktAttendanceSettings": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u8003\u52E4\u8BBE\u7F6E"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAttendanceSettings/batch": {
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
    "/api/TaktAttendanceSettings/template": {
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
    "/api/TaktAttendanceSettings/import": {
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
    "/api/TaktAttendanceSettings/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktAttendanceSettingQueryDto"];
                    "application/json": components["schemas"]["TaktAttendanceSettingQueryDto"];
                    "text/json": components["schemas"]["TaktAttendanceSettingQueryDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceSettingQueryDto"];
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
    "/api/TaktAttendanceSources/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    DeviceId?: number;
                    EmployeeId?: number;
                    RawPunchTimeFrom?: string;
                    RawPunchTimeTo?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktAttendanceSourceDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktAttendanceSourceDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktAttendanceSourceDto"];
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
    "/api/TaktAttendanceSources/{id}": {
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
                        "text/plain": components["schemas"]["TaktAttendanceSourceDto"];
                        "application/json": components["schemas"]["TaktAttendanceSourceDto"];
                        "text/json": components["schemas"]["TaktAttendanceSourceDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktAttendanceSourceUpdateDto"];
                    "application/json": components["schemas"]["TaktAttendanceSourceUpdateDto"];
                    "text/json": components["schemas"]["TaktAttendanceSourceUpdateDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceSourceUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktAttendanceSourceDto"];
                        "application/json": components["schemas"]["TaktAttendanceSourceDto"];
                        "text/json": components["schemas"]["TaktAttendanceSourceDto"];
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
    "/api/TaktAttendanceSources": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u8003\u52E4\u6E90\u8BB0\u5F55"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktAttendanceSources/batch": {
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
    "/api/TaktAttendanceSources/template": {
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
    "/api/TaktAttendanceSources/import": {
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
    "/api/TaktAttendanceSources/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktAttendanceSourceQueryDto"];
                    "application/json": components["schemas"]["TaktAttendanceSourceQueryDto"];
                    "text/json": components["schemas"]["TaktAttendanceSourceQueryDto"];
                    "application/*+json": components["schemas"]["TaktAttendanceSourceQueryDto"];
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
    "/api/TaktHolidays/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    Region?: string;
                    HolidayName?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktHolidayDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktHolidayDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktHolidayDto"];
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
    "/api/TaktHolidays/{id}": {
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
                        "text/plain": components["schemas"]["TaktHolidayDto"];
                        "application/json": components["schemas"]["TaktHolidayDto"];
                        "text/json": components["schemas"]["TaktHolidayDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktHolidayUpdateDto"];
                    "application/json": components["schemas"]["TaktHolidayUpdateDto"];
                    "text/json": components["schemas"]["TaktHolidayUpdateDto"];
                    "application/*+json": components["schemas"]["TaktHolidayUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktHolidayDto"];
                        "application/json": components["schemas"]["TaktHolidayDto"];
                        "text/json": components["schemas"]["TaktHolidayDto"];
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
    "/api/TaktHolidays/options": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    region?: string;
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
    "/api/TaktHolidays": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u5047\u65E5"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktHolidays/batch": {
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
    "/api/TaktHolidays/template": {
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
    "/api/TaktHolidays/import": {
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
    "/api/TaktHolidays/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktHolidayQueryDto"];
                    "application/json": components["schemas"]["TaktHolidayQueryDto"];
                    "text/json": components["schemas"]["TaktHolidayQueryDto"];
                    "application/*+json": components["schemas"]["TaktHolidayQueryDto"];
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
    "/api/TaktHolidays/theme": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    date?: string;
                    region?: string;
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
                        "text/plain": components["schemas"]["TaktHolidayDto"];
                        "application/json": components["schemas"]["TaktHolidayDto"];
                        "text/json": components["schemas"]["TaktHolidayDto"];
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
    "/api/TaktLeave/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    LeaveType?: string;
                    LeaveStatus?: number;
                    StartDateFrom?: string;
                    StartDateTo?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktLeaveDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktLeaveDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktLeaveDto"];
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
    "/api/TaktLeave/{id}": {
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
                        "text/plain": components["schemas"]["TaktLeaveDto"];
                        "application/json": components["schemas"]["TaktLeaveDto"];
                        "text/json": components["schemas"]["TaktLeaveDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktLeaveUpdateDto"];
                    "application/json": components["schemas"]["TaktLeaveUpdateDto"];
                    "text/json": components["schemas"]["TaktLeaveUpdateDto"];
                    "application/*+json": components["schemas"]["TaktLeaveUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktLeaveDto"];
                        "application/json": components["schemas"]["TaktLeaveDto"];
                        "text/json": components["schemas"]["TaktLeaveDto"];
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
    "/api/TaktLeave": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u8BF7\u5047"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktLeave/batch": {
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
    "/api/TaktLeave/submit": {
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
                    "application/json-patch+json": components["schemas"]["TaktLeaveSubmitDto"];
                    "application/json": components["schemas"]["TaktLeaveSubmitDto"];
                    "text/json": components["schemas"]["TaktLeaveSubmitDto"];
                    "application/*+json": components["schemas"]["TaktLeaveSubmitDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktLeaveSubmitResultDto"];
                        "application/json": components["schemas"]["TaktLeaveSubmitResultDto"];
                        "text/json": components["schemas"]["TaktLeaveSubmitResultDto"];
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
    "/api/TaktLeave/status": {
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
                    "application/json-patch+json": components["schemas"]["TaktLeaveStatusDto"];
                    "application/json": components["schemas"]["TaktLeaveStatusDto"];
                    "text/json": components["schemas"]["TaktLeaveStatusDto"];
                    "application/*+json": components["schemas"]["TaktLeaveStatusDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktLeaveDto"];
                        "application/json": components["schemas"]["TaktLeaveDto"];
                        "text/json": components["schemas"]["TaktLeaveDto"];
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
    "/api/TaktLeave/template": {
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
    "/api/TaktLeave/import": {
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
    "/api/TaktLeave/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktLeaveQueryDto"];
                    "application/json": components["schemas"]["TaktLeaveQueryDto"];
                    "text/json": components["schemas"]["TaktLeaveQueryDto"];
                    "application/*+json": components["schemas"]["TaktLeaveQueryDto"];
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
    "/api/TaktOvertimes/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    EmployeeId?: number;
                    OvertimeStatus?: number;
                    OvertimeDateFrom?: string;
                    OvertimeDateTo?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktOvertimeDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktOvertimeDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktOvertimeDto"];
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
    "/api/TaktOvertimes/{id}": {
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
                        "text/plain": components["schemas"]["TaktOvertimeDto"];
                        "application/json": components["schemas"]["TaktOvertimeDto"];
                        "text/json": components["schemas"]["TaktOvertimeDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktOvertimeUpdateDto"];
                    "application/json": components["schemas"]["TaktOvertimeUpdateDto"];
                    "text/json": components["schemas"]["TaktOvertimeUpdateDto"];
                    "application/*+json": components["schemas"]["TaktOvertimeUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktOvertimeDto"];
                        "application/json": components["schemas"]["TaktOvertimeDto"];
                        "text/json": components["schemas"]["TaktOvertimeDto"];
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
    "/api/TaktOvertimes": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u52A0\u73ED"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktOvertimes/batch": {
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
    "/api/TaktOvertimes/template": {
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
    "/api/TaktOvertimes/import": {
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
    "/api/TaktOvertimes/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktOvertimeQueryDto"];
                    "application/json": components["schemas"]["TaktOvertimeQueryDto"];
                    "text/json": components["schemas"]["TaktOvertimeQueryDto"];
                    "application/*+json": components["schemas"]["TaktOvertimeQueryDto"];
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
    "/api/TaktShiftSchedules/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ScheduleType?: number;
                    DeptId?: number;
                    EmployeeId?: number;
                    ShiftId?: number;
                    ScheduleDateFrom?: string;
                    ScheduleDateTo?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktShiftScheduleDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktShiftScheduleDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktShiftScheduleDto"];
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
    "/api/TaktShiftSchedules/{id}": {
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
                        "text/plain": components["schemas"]["TaktShiftScheduleDto"];
                        "application/json": components["schemas"]["TaktShiftScheduleDto"];
                        "text/json": components["schemas"]["TaktShiftScheduleDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktShiftScheduleUpdateDto"];
                    "application/json": components["schemas"]["TaktShiftScheduleUpdateDto"];
                    "text/json": components["schemas"]["TaktShiftScheduleUpdateDto"];
                    "application/*+json": components["schemas"]["TaktShiftScheduleUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktShiftScheduleDto"];
                        "application/json": components["schemas"]["TaktShiftScheduleDto"];
                        "text/json": components["schemas"]["TaktShiftScheduleDto"];
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
    "/api/TaktShiftSchedules": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u6392\u73ED"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktShiftSchedules/batch": {
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
    "/api/TaktShiftSchedules/template": {
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
    "/api/TaktShiftSchedules/import": {
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
    "/api/TaktShiftSchedules/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktShiftScheduleQueryDto"];
                    "application/json": components["schemas"]["TaktShiftScheduleQueryDto"];
                    "text/json": components["schemas"]["TaktShiftScheduleQueryDto"];
                    "application/*+json": components["schemas"]["TaktShiftScheduleQueryDto"];
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
    "/api/TaktWorkShifts/list": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get: {
            parameters: {
                query?: {
                    ShiftCode?: string;
                    ShiftName?: string;
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
                        "text/plain": components["schemas"]["TaktPagedResultOfTaktWorkShiftDto"];
                        "application/json": components["schemas"]["TaktPagedResultOfTaktWorkShiftDto"];
                        "text/json": components["schemas"]["TaktPagedResultOfTaktWorkShiftDto"];
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
    "/api/TaktWorkShifts/{id}": {
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
                        "text/plain": components["schemas"]["TaktWorkShiftDto"];
                        "application/json": components["schemas"]["TaktWorkShiftDto"];
                        "text/json": components["schemas"]["TaktWorkShiftDto"];
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
                    "application/json-patch+json": components["schemas"]["TaktWorkShiftUpdateDto"];
                    "application/json": components["schemas"]["TaktWorkShiftUpdateDto"];
                    "text/json": components["schemas"]["TaktWorkShiftUpdateDto"];
                    "application/*+json": components["schemas"]["TaktWorkShiftUpdateDto"];
                };
            };
            responses: {
                /** @description OK */
                200: {
                    headers: {
                        [name: string]: unknown;
                    };
                    content: {
                        "text/plain": components["schemas"]["TaktWorkShiftDto"];
                        "application/json": components["schemas"]["TaktWorkShiftDto"];
                        "text/json": components["schemas"]["TaktWorkShiftDto"];
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
    "/api/TaktWorkShifts/options": {
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
    "/api/TaktWorkShifts": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        get?: never;
        put?: never;
        post: operations["\u73ED\u6B21"];
        delete?: never;
        options?: never;
        head?: never;
        patch?: never;
        trace?: never;
    };
    "/api/TaktWorkShifts/batch": {
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
    "/api/TaktWorkShifts/template": {
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
    "/api/TaktWorkShifts/import": {
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
    "/api/TaktWorkShifts/export": {
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
                    "application/json-patch+json": components["schemas"]["TaktWorkShiftQueryDto"];
                    "application/json": components["schemas"]["TaktWorkShiftQueryDto"];
                    "text/json": components["schemas"]["TaktWorkShiftQueryDto"];
                    "application/*+json": components["schemas"]["TaktWorkShiftQueryDto"];
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
        TaktAttendanceCorrectionCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            targetDate?: string;
            /** Format: int32 */
            correctionKind?: number;
            /** Format: date-time */
            requestPunchTime?: string;
            reason?: string;
            /** Format: int32 */
            approvalStatus?: number;
            remark?: string | null;
        };
        TaktAttendanceCorrectionDto: {
            /** Format: int64 */
            correctionId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            targetDate?: string;
            /** Format: int32 */
            correctionKind?: number;
            /** Format: date-time */
            requestPunchTime?: string;
            reason?: string;
            /** Format: int32 */
            approvalStatus?: number;
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
        TaktAttendanceCorrectionQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: date-time */
            targetDateFrom?: string | null;
            /** Format: date-time */
            targetDateTo?: string | null;
            /** Format: int32 */
            approvalStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktAttendanceCorrectionUpdateDto: {
            /** Format: int64 */
            correctionId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            targetDate?: string;
            /** Format: int32 */
            correctionKind?: number;
            /** Format: date-time */
            requestPunchTime?: string;
            reason?: string;
            /** Format: int32 */
            approvalStatus?: number;
            remark?: string | null;
        };
        TaktAttendanceDeviceCreateDto: {
            deviceCode?: string;
            deviceName?: string;
            deviceType?: string;
            manufacturer?: string | null;
            ipAddress?: string | null;
            /** Format: int32 */
            port?: number | null;
            deviceModel?: string | null;
            apiSecret?: string | null;
            configJson?: string | null;
            /** Format: int32 */
            deviceStatus?: number;
            /** Format: int32 */
            isPushEnabled?: number;
            remark?: string | null;
        };
        TaktAttendanceDeviceDto: {
            /** Format: int64 */
            deviceId?: number;
            deviceCode?: string;
            deviceName?: string;
            deviceType?: string;
            manufacturer?: string | null;
            ipAddress?: string | null;
            /** Format: int32 */
            port?: number | null;
            deviceModel?: string | null;
            configJson?: string | null;
            /** Format: int32 */
            deviceStatus?: number;
            /** Format: int32 */
            isPushEnabled?: number;
            /** Format: date-time */
            lastPullAt?: string | null;
            /** Format: date-time */
            lastPushAt?: string | null;
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
        TaktAttendanceDeviceQueryDto: {
            deviceCode?: string | null;
            deviceName?: string | null;
            deviceType?: string | null;
            manufacturer?: string | null;
            /** Format: int32 */
            deviceStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktAttendanceDeviceStatusDto: {
            /** Format: int64 */
            deviceId?: number;
            isOnline?: boolean;
            message?: string;
        };
        TaktAttendanceDeviceUpdateDto: {
            /** Format: int64 */
            deviceId?: number;
            deviceCode?: string;
            deviceName?: string;
            deviceType?: string;
            manufacturer?: string | null;
            ipAddress?: string | null;
            /** Format: int32 */
            port?: number | null;
            deviceModel?: string | null;
            apiSecret?: string | null;
            configJson?: string | null;
            /** Format: int32 */
            deviceStatus?: number;
            /** Format: int32 */
            isPushEnabled?: number;
            remark?: string | null;
        };
        TaktAttendanceDeviceUserSyncItemDto: {
            /** Format: int64 */
            employeeId?: number;
            enrollNumber?: string;
            userName?: string;
            cardNo?: string | null;
            certificateNo?: string | null;
            enabled?: boolean;
            mobile?: string | null;
        };
        TaktAttendanceExceptionCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            exceptionDate?: string;
            /** Format: int32 */
            exceptionType?: number;
            summary?: string;
            /** Format: int32 */
            handleStatus?: number;
            remark?: string | null;
        };
        TaktAttendanceExceptionDto: {
            /** Format: int64 */
            exceptionId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            exceptionDate?: string;
            /** Format: int32 */
            exceptionType?: number;
            summary?: string;
            /** Format: int32 */
            handleStatus?: number;
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
        TaktAttendanceExceptionQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: date-time */
            exceptionDateFrom?: string | null;
            /** Format: date-time */
            exceptionDateTo?: string | null;
            /** Format: int32 */
            exceptionType?: number | null;
            /** Format: int32 */
            handleStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktAttendanceExceptionUpdateDto: {
            /** Format: int64 */
            exceptionId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            exceptionDate?: string;
            /** Format: int32 */
            exceptionType?: number;
            summary?: string;
            /** Format: int32 */
            handleStatus?: number;
            remark?: string | null;
        };
        TaktAttendancePullRequestDto: {
            /** Format: int64 */
            deviceId?: number;
            /** Format: date-time */
            startTime?: string;
            /** Format: date-time */
            endTime?: string;
        };
        TaktAttendancePullResultDto: {
            success?: boolean;
            /** Format: int32 */
            acceptedCount?: number;
            errors?: string[];
            updatedDeviceConfigJson?: string | null;
        };
        TaktAttendancePunchCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            punchTime?: string;
            /** Format: int32 */
            punchType?: number;
            /** Format: int32 */
            punchSource?: number;
            punchAddress?: string | null;
            remark?: string | null;
        };
        TaktAttendancePunchDto: {
            /** Format: int64 */
            punchId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            punchTime?: string;
            /** Format: int32 */
            punchType?: number;
            /** Format: int32 */
            punchSource?: number;
            punchAddress?: string | null;
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
        TaktAttendancePunchQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: int32 */
            punchType?: number | null;
            /** Format: date-time */
            punchTimeFrom?: string | null;
            /** Format: date-time */
            punchTimeTo?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktAttendancePunchUpdateDto: {
            /** Format: int64 */
            punchId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            punchTime?: string;
            /** Format: int32 */
            punchType?: number;
            /** Format: int32 */
            punchSource?: number;
            punchAddress?: string | null;
            remark?: string | null;
        };
        TaktAttendancePushHandleResultDto: {
            success?: boolean;
            /** Format: int32 */
            acceptedCount?: number;
            errors?: string[];
        };
        TaktAttendancePushRequestDto: {
            deviceCode?: string;
            deviceType?: string;
            rawPayloadJson?: string;
            signature?: string | null;
            /** Format: int64 */
            timestamp?: number | null;
        };
        TaktAttendanceResultCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            attendanceDate?: string;
            /** Format: int64 */
            shiftScheduleId?: number | null;
            /** Format: int32 */
            attendanceStatus?: number;
            /** Format: date-time */
            firstInTime?: string | null;
            /** Format: date-time */
            lastOutTime?: string | null;
            /** Format: int32 */
            workMinutes?: number;
            /** Format: date-time */
            calculatedAt?: string | null;
            remark?: string | null;
        };
        TaktAttendanceResultDto: {
            /** Format: int64 */
            resultId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            attendanceDate?: string;
            /** Format: int64 */
            shiftScheduleId?: number | null;
            /** Format: int32 */
            attendanceStatus?: number;
            /** Format: date-time */
            firstInTime?: string | null;
            /** Format: date-time */
            lastOutTime?: string | null;
            /** Format: int32 */
            workMinutes?: number;
            /** Format: date-time */
            calculatedAt?: string | null;
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
        TaktAttendanceResultQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: date-time */
            attendanceDateFrom?: string | null;
            /** Format: date-time */
            attendanceDateTo?: string | null;
            /** Format: int32 */
            attendanceStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktAttendanceResultUpdateDto: {
            /** Format: int64 */
            resultId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            attendanceDate?: string;
            /** Format: int64 */
            shiftScheduleId?: number | null;
            /** Format: int32 */
            attendanceStatus?: number;
            /** Format: date-time */
            firstInTime?: string | null;
            /** Format: date-time */
            lastOutTime?: string | null;
            /** Format: int32 */
            workMinutes?: number;
            /** Format: date-time */
            calculatedAt?: string | null;
            remark?: string | null;
        };
        TaktAttendanceSettingCreateDto: {
            settingCode?: string;
            settingName?: string;
            workStartTime?: string;
            workEndTime?: string;
            /** Format: int32 */
            lateGraceMinutes?: number;
            /** Format: int32 */
            earlyLeaveGraceMinutes?: number;
            /** Format: int32 */
            isDefault?: number;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktAttendanceSettingDto: {
            /** Format: int64 */
            settingId?: number;
            settingCode?: string;
            settingName?: string;
            workStartTime?: string;
            workEndTime?: string;
            /** Format: int32 */
            lateGraceMinutes?: number;
            /** Format: int32 */
            earlyLeaveGraceMinutes?: number;
            /** Format: int32 */
            isDefault?: number;
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
        TaktAttendanceSettingQueryDto: {
            settingCode?: string | null;
            settingName?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktAttendanceSettingUpdateDto: {
            /** Format: int64 */
            settingId?: number;
            settingCode?: string;
            settingName?: string;
            workStartTime?: string;
            workEndTime?: string;
            /** Format: int32 */
            lateGraceMinutes?: number;
            /** Format: int32 */
            earlyLeaveGraceMinutes?: number;
            /** Format: int32 */
            isDefault?: number;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktAttendanceSourceCreateDto: {
            /** Format: int64 */
            deviceId?: number;
            /** Format: int64 */
            employeeId?: number;
            enrollNumber?: string;
            /** Format: date-time */
            rawPunchTime?: string;
            /** Format: int32 */
            verifyMode?: number;
            externalRecordKey?: string | null;
            downloadBatchNo?: string | null;
            rawPayloadJson?: string | null;
            remark?: string | null;
        };
        TaktAttendanceSourceDto: {
            /** Format: int64 */
            sourceId?: number;
            /** Format: int64 */
            deviceId?: number;
            deviceCode?: string | null;
            /** Format: int64 */
            employeeId?: number;
            enrollNumber?: string;
            /** Format: date-time */
            rawPunchTime?: string;
            /** Format: int32 */
            verifyMode?: number;
            externalRecordKey?: string | null;
            downloadBatchNo?: string | null;
            rawPayloadJson?: string | null;
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
        TaktAttendanceSourceQueryDto: {
            /** Format: int64 */
            deviceId?: number | null;
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: date-time */
            rawPunchTimeFrom?: string | null;
            /** Format: date-time */
            rawPunchTimeTo?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktAttendanceSourceUpdateDto: {
            /** Format: int64 */
            sourceId?: number;
            /** Format: int64 */
            deviceId?: number;
            /** Format: int64 */
            employeeId?: number;
            enrollNumber?: string;
            /** Format: date-time */
            rawPunchTime?: string;
            /** Format: int32 */
            verifyMode?: number;
            externalRecordKey?: string | null;
            downloadBatchNo?: string | null;
            rawPayloadJson?: string | null;
            remark?: string | null;
        };
        TaktAttendanceUserSyncRequestDto: {
            users?: components["schemas"]["TaktAttendanceDeviceUserSyncItemDto"][];
        };
        TaktAttendanceUserSyncResultDto: {
            success?: boolean;
            /** Format: int32 */
            syncedCount?: number;
            errors?: string[];
        };
        TaktDeptCreateDto: {
            deptName?: string;
            deptCode?: string;
            /** Format: int64 */
            parentId?: number;
            /** Format: int64 */
            deptHeadId?: number;
            costCenterCode?: string | null;
            delegates?: components["schemas"]["TaktDeptDelegateItemDto"][] | null;
            /** Format: int32 */
            deptType?: number;
            deptPhone?: string | null;
            deptMail?: string | null;
            deptAddr?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            dataScope?: number;
            customScope?: string | null;
            remark?: string | null;
            userIds?: number[] | null;
            roleIds?: number[] | null;
        };
        TaktDeptDelegateItemDto: {
            /** Format: int64 */
            id?: number | null;
            /** Format: int32 */
            delegateMode?: number;
            /** Format: int64 */
            delegateEmployeeId?: number | null;
            /** Format: int64 */
            delegateDeptId?: number | null;
            /** Format: int64 */
            delegatePostId?: number | null;
            /** Format: int32 */
            orderNum?: number;
        };
        TaktDeptDto: {
            /** Format: int64 */
            deptId?: number;
            deptName?: string;
            deptCode?: string;
            /** Format: int64 */
            parentId?: number;
            /** Format: int64 */
            deptHeadId?: number;
            deptHead?: string | null;
            costCenterCode?: string | null;
            deptCostCenterName?: string | null;
            delegates?: components["schemas"]["TaktDeptDelegateItemDto"][] | null;
            /** Format: int32 */
            deptType?: number;
            deptPhone?: string | null;
            deptMail?: string | null;
            deptAddr?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            dataScope?: number;
            customScope?: string | null;
            /** Format: int32 */
            deptStatus?: number;
            userIds?: number[] | null;
            roleIds?: number[] | null;
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
        TaktDeptQueryDto: {
            deptName?: string | null;
            deptCode?: string | null;
            /** Format: int64 */
            parentId?: number | null;
            /** Format: int32 */
            deptStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktDeptStatusDto: {
            /** Format: int64 */
            deptId?: number;
            /** Format: int32 */
            deptStatus?: number;
        };
        TaktDeptUpdateDto: {
            /** Format: int64 */
            deptId?: number;
            deptName?: string;
            deptCode?: string;
            /** Format: int64 */
            parentId?: number;
            /** Format: int64 */
            deptHeadId?: number;
            costCenterCode?: string | null;
            delegates?: components["schemas"]["TaktDeptDelegateItemDto"][] | null;
            /** Format: int32 */
            deptType?: number;
            deptPhone?: string | null;
            deptMail?: string | null;
            deptAddr?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            dataScope?: number;
            customScope?: string | null;
            remark?: string | null;
            userIds?: number[] | null;
            roleIds?: number[] | null;
        };
        TaktEmployeeAttachmentCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            /** Format: int64 */
            fileId?: number;
            fileCode?: string;
            fileName?: string;
            filePath?: string;
            /** Format: int64 */
            fileSize?: number;
            fileType?: string | null;
            /** Format: int32 */
            attachmentType?: number;
            attachmentDescription?: string | null;
            /** Format: int32 */
            orderNum?: number;
        };
        TaktEmployeeAttachmentDto: {
            /** Format: int64 */
            attachmentId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: int64 */
            fileId?: number;
            fileCode?: string;
            fileName?: string;
            filePath?: string;
            /** Format: int64 */
            fileSize?: number;
            fileType?: string | null;
            /** Format: int32 */
            attachmentType?: number;
            attachmentDescription?: string | null;
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
        TaktEmployeeAttachmentQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: int64 */
            fileId?: number | null;
            /** Format: int32 */
            attachmentType?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktEmployeeAttachmentUpdateDto: {
            /** Format: int64 */
            attachmentId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: int64 */
            fileId?: number;
            fileCode?: string;
            fileName?: string;
            filePath?: string;
            /** Format: int64 */
            fileSize?: number;
            fileType?: string | null;
            /** Format: int32 */
            attachmentType?: number;
            attachmentDescription?: string | null;
            /** Format: int32 */
            orderNum?: number;
        };
        TaktEmployeeCareerCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            /** Format: int64 */
            deptId?: number;
            deptName?: string;
            /** Format: int64 */
            postId?: number | null;
            postName?: string | null;
            jobLevel?: string | null;
            jobTitle?: string | null;
            /** Format: date-time */
            joinDate?: string | null;
            /** Format: date-time */
            regularizationDate?: string | null;
            /** Format: date-time */
            leaveDate?: string | null;
            /** Format: double */
            workYears?: number | null;
            workLocation?: string | null;
            /** Format: int32 */
            workNature?: number;
            /** Format: int32 */
            employmentType?: number;
            /** Format: int32 */
            isPrimary?: number;
            /** Format: int64 */
            directManagerId?: number | null;
            directManagerName?: string | null;
        };
        TaktEmployeeCareerDto: {
            /** Format: int64 */
            careerId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: int64 */
            deptId?: number;
            deptName?: string;
            /** Format: int64 */
            postId?: number | null;
            postName?: string | null;
            jobLevel?: string | null;
            jobTitle?: string | null;
            /** Format: date-time */
            joinDate?: string | null;
            /** Format: date-time */
            regularizationDate?: string | null;
            /** Format: date-time */
            leaveDate?: string | null;
            /** Format: double */
            workYears?: number | null;
            workLocation?: string | null;
            /** Format: int32 */
            workNature?: number;
            /** Format: int32 */
            employmentType?: number;
            /** Format: int32 */
            isPrimary?: number;
            /** Format: int64 */
            directManagerId?: number | null;
            directManagerName?: string | null;
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
        TaktEmployeeCareerQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: int64 */
            deptId?: number | null;
            /** Format: int64 */
            postId?: number | null;
            /** Format: int32 */
            isPrimary?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktEmployeeCareerUpdateDto: {
            /** Format: int64 */
            careerId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: int64 */
            deptId?: number;
            deptName?: string;
            /** Format: int64 */
            postId?: number | null;
            postName?: string | null;
            jobLevel?: string | null;
            jobTitle?: string | null;
            /** Format: date-time */
            joinDate?: string | null;
            /** Format: date-time */
            regularizationDate?: string | null;
            /** Format: date-time */
            leaveDate?: string | null;
            /** Format: double */
            workYears?: number | null;
            workLocation?: string | null;
            /** Format: int32 */
            workNature?: number;
            /** Format: int32 */
            employmentType?: number;
            /** Format: int32 */
            isPrimary?: number;
            /** Format: int64 */
            directManagerId?: number | null;
            directManagerName?: string | null;
        };
        TaktEmployeeContractCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            contractNo?: string;
            /** Format: int32 */
            contractType?: number;
            /** Format: date-time */
            startDate?: string | null;
            /** Format: date-time */
            endDate?: string | null;
            /** Format: date-time */
            probationEndDate?: string | null;
            /** Format: date-time */
            signDate?: string | null;
            /** Format: int32 */
            contractStatus?: number;
            signCompany?: string | null;
        };
        TaktEmployeeContractDto: {
            /** Format: int64 */
            employeeContractId?: number;
            /** Format: int64 */
            employeeId?: number;
            contractNo?: string;
            /** Format: int32 */
            contractType?: number;
            /** Format: date-time */
            startDate?: string | null;
            /** Format: date-time */
            endDate?: string | null;
            /** Format: date-time */
            probationEndDate?: string | null;
            /** Format: date-time */
            signDate?: string | null;
            /** Format: int32 */
            contractStatus?: number;
            signCompany?: string | null;
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
        TaktEmployeeContractQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: int32 */
            contractStatus?: number | null;
            contractNo?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktEmployeeContractUpdateDto: {
            /** Format: int64 */
            employeeContractId?: number;
            /** Format: int64 */
            employeeId?: number;
            contractNo?: string;
            /** Format: int32 */
            contractType?: number;
            /** Format: date-time */
            startDate?: string | null;
            /** Format: date-time */
            endDate?: string | null;
            /** Format: date-time */
            probationEndDate?: string | null;
            /** Format: date-time */
            signDate?: string | null;
            /** Format: int32 */
            contractStatus?: number;
            signCompany?: string | null;
        };
        TaktEmployeeCreateDto: {
            employeeCode?: string;
            isSystemEmployeeCode?: boolean;
            realName?: string;
            formerName?: string | null;
            fullName?: string | null;
            nativeName?: string | null;
            displayName?: string | null;
            /** Format: int32 */
            gender?: number;
            /** Format: date-time */
            birthDate?: string;
            /** Format: int32 */
            age?: number | null;
            idCard?: string;
            phone?: string | null;
            email?: string | null;
            avatar?: string | null;
            nationality?: string;
            /** Format: int32 */
            politicalStatus?: number;
            /** Format: int32 */
            maritalStatus?: number;
            nativePlace?: string | null;
            currentAddress?: string | null;
            registeredAddress?: string | null;
            /** Format: int32 */
            employeeStatus?: number;
            remark?: string | null;
        };
        TaktEmployeeDeptDto: {
            /** Format: int64 */
            employeeDeptId?: number;
            /** Format: int64 */
            employeeId?: number;
            employeeName?: string;
            employeeCode?: string;
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
        TaktEmployeeDto: {
            /** Format: int64 */
            employeeId?: number;
            employeeCode?: string;
            realName?: string;
            formerName?: string | null;
            fullName?: string | null;
            nativeName?: string | null;
            displayName?: string | null;
            /** Format: int32 */
            gender?: number;
            /** Format: date-time */
            birthDate?: string;
            /** Format: int32 */
            age?: number | null;
            idCard?: string;
            phone?: string | null;
            email?: string | null;
            avatar?: string | null;
            nationality?: string;
            /** Format: int32 */
            politicalStatus?: number;
            /** Format: int32 */
            maritalStatus?: number;
            nativePlace?: string | null;
            currentAddress?: string | null;
            registeredAddress?: string | null;
            /** Format: int32 */
            employeeStatus?: number;
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
        TaktEmployeeEducationCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            /** Format: int32 */
            educationLevel?: number;
            schoolName?: string;
            majorName?: string | null;
            /** Format: int32 */
            degreeLevel?: number;
            /** Format: date-time */
            startDate?: string | null;
            /** Format: date-time */
            endDate?: string | null;
            /** Format: int32 */
            isHighest?: number;
            certificateNo?: string | null;
        };
        TaktEmployeeEducationDto: {
            /** Format: int64 */
            employeeEducationId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: int32 */
            educationLevel?: number;
            schoolName?: string;
            majorName?: string | null;
            /** Format: int32 */
            degreeLevel?: number;
            /** Format: date-time */
            startDate?: string | null;
            /** Format: date-time */
            endDate?: string | null;
            /** Format: int32 */
            isHighest?: number;
            certificateNo?: string | null;
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
        TaktEmployeeEducationQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: int32 */
            educationLevel?: number | null;
            /** Format: int32 */
            isHighest?: number | null;
            schoolName?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktEmployeeEducationUpdateDto: {
            /** Format: int64 */
            employeeEducationId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: int32 */
            educationLevel?: number;
            schoolName?: string;
            majorName?: string | null;
            /** Format: int32 */
            degreeLevel?: number;
            /** Format: date-time */
            startDate?: string | null;
            /** Format: date-time */
            endDate?: string | null;
            /** Format: int32 */
            isHighest?: number;
            certificateNo?: string | null;
        };
        TaktEmployeeFamilyCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            memberName?: string;
            /** Format: int32 */
            relationType?: number;
            phoneNumber?: string | null;
            workUnit?: string | null;
            jobTitle?: string | null;
            /** Format: date-time */
            birthDate?: string | null;
            /** Format: int32 */
            isEmergencyContact?: number;
        };
        TaktEmployeeFamilyDto: {
            /** Format: int64 */
            employeeFamilyId?: number;
            /** Format: int64 */
            employeeId?: number;
            memberName?: string;
            /** Format: int32 */
            relationType?: number;
            phoneNumber?: string | null;
            workUnit?: string | null;
            jobTitle?: string | null;
            /** Format: date-time */
            birthDate?: string | null;
            /** Format: int32 */
            isEmergencyContact?: number;
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
        TaktEmployeeFamilyQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: int32 */
            relationType?: number | null;
            memberName?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktEmployeeFamilyUpdateDto: {
            /** Format: int64 */
            employeeFamilyId?: number;
            /** Format: int64 */
            employeeId?: number;
            memberName?: string;
            /** Format: int32 */
            relationType?: number;
            phoneNumber?: string | null;
            workUnit?: string | null;
            jobTitle?: string | null;
            /** Format: date-time */
            birthDate?: string | null;
            /** Format: int32 */
            isEmergencyContact?: number;
        };
        TaktEmployeePostDto: {
            /** Format: int64 */
            employeePostId?: number;
            /** Format: int64 */
            employeeId?: number;
            employeeName?: string;
            employeeCode?: string;
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
        TaktEmployeeQueryDto: {
            realName?: string | null;
            employeeCode?: string | null;
            phone?: string | null;
            /** Format: int32 */
            employeeStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktEmployeeSkillCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            skillName?: string;
            /** Format: int32 */
            skillLevel?: number;
            certificateName?: string | null;
            certificateNo?: string | null;
            /** Format: date-time */
            obtainedDate?: string | null;
            /** Format: date-time */
            expiryDate?: string | null;
        };
        TaktEmployeeSkillDto: {
            /** Format: int64 */
            employeeSkillId?: number;
            /** Format: int64 */
            employeeId?: number;
            skillName?: string;
            /** Format: int32 */
            skillLevel?: number;
            certificateName?: string | null;
            certificateNo?: string | null;
            /** Format: date-time */
            obtainedDate?: string | null;
            /** Format: date-time */
            expiryDate?: string | null;
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
        TaktEmployeeSkillQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            skillName?: string | null;
            /** Format: int32 */
            skillLevel?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktEmployeeSkillUpdateDto: {
            /** Format: int64 */
            employeeSkillId?: number;
            /** Format: int64 */
            employeeId?: number;
            skillName?: string;
            /** Format: int32 */
            skillLevel?: number;
            certificateName?: string | null;
            certificateNo?: string | null;
            /** Format: date-time */
            obtainedDate?: string | null;
            /** Format: date-time */
            expiryDate?: string | null;
        };
        TaktEmployeeTransferCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            /** Format: int32 */
            transferType?: number;
            /** Format: int64 */
            fromDeptId?: number;
            fromDeptName?: string;
            /** Format: int64 */
            fromPostId?: number | null;
            fromPostName?: string | null;
            /** Format: int64 */
            toDeptId?: number;
            toDeptName?: string;
            /** Format: int64 */
            toPostId?: number | null;
            toPostName?: string | null;
            /** Format: date-time */
            effectiveDate?: string | null;
            reason?: string | null;
        };
        TaktEmployeeTransferDto: {
            /** Format: int64 */
            transferId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: int32 */
            transferType?: number;
            /** Format: int64 */
            fromDeptId?: number;
            fromDeptName?: string;
            /** Format: int64 */
            fromPostId?: number | null;
            fromPostName?: string | null;
            /** Format: int64 */
            toDeptId?: number;
            toDeptName?: string;
            /** Format: int64 */
            toPostId?: number | null;
            toPostName?: string | null;
            /** Format: date-time */
            effectiveDate?: string | null;
            reason?: string | null;
            /** Format: int64 */
            flowInstanceId?: number | null;
            /** Format: int32 */
            transferStatus?: number;
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
        TaktEmployeeTransferQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: int32 */
            transferType?: number | null;
            /** Format: int32 */
            transferStatus?: number | null;
            /** Format: date-time */
            effectiveDateFrom?: string | null;
            /** Format: date-time */
            effectiveDateTo?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktEmployeeTransferStatusDto: {
            /** Format: int64 */
            transferId?: number;
            /** Format: int32 */
            transferStatus?: number;
        };
        TaktEmployeeTransferUpdateDto: {
            /** Format: int64 */
            transferId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: int32 */
            transferType?: number;
            /** Format: int64 */
            fromDeptId?: number;
            fromDeptName?: string;
            /** Format: int64 */
            fromPostId?: number | null;
            fromPostName?: string | null;
            /** Format: int64 */
            toDeptId?: number;
            toDeptName?: string;
            /** Format: int64 */
            toPostId?: number | null;
            toPostName?: string | null;
            /** Format: date-time */
            effectiveDate?: string | null;
            reason?: string | null;
        };
        TaktEmployeeUpdateDto: {
            /** Format: int64 */
            employeeId?: number;
            employeeCode?: string;
            isSystemEmployeeCode?: boolean;
            realName?: string;
            formerName?: string | null;
            fullName?: string | null;
            nativeName?: string | null;
            displayName?: string | null;
            /** Format: int32 */
            gender?: number;
            /** Format: date-time */
            birthDate?: string;
            /** Format: int32 */
            age?: number | null;
            idCard?: string;
            phone?: string | null;
            email?: string | null;
            avatar?: string | null;
            nationality?: string;
            /** Format: int32 */
            politicalStatus?: number;
            /** Format: int32 */
            maritalStatus?: number;
            nativePlace?: string | null;
            currentAddress?: string | null;
            registeredAddress?: string | null;
            /** Format: int32 */
            employeeStatus?: number;
            remark?: string | null;
        };
        TaktEmployeeWorkCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            companyName?: string;
            positionName?: string | null;
            jobContent?: string | null;
            /** Format: date-time */
            startDate?: string | null;
            /** Format: date-time */
            endDate?: string | null;
            witnessName?: string | null;
            witnessPhone?: string | null;
        };
        TaktEmployeeWorkDto: {
            /** Format: int64 */
            employeeWorkId?: number;
            /** Format: int64 */
            employeeId?: number;
            companyName?: string;
            positionName?: string | null;
            jobContent?: string | null;
            /** Format: date-time */
            startDate?: string | null;
            /** Format: date-time */
            endDate?: string | null;
            witnessName?: string | null;
            witnessPhone?: string | null;
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
        TaktEmployeeWorkQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            companyName?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktEmployeeWorkUpdateDto: {
            /** Format: int64 */
            employeeWorkId?: number;
            /** Format: int64 */
            employeeId?: number;
            companyName?: string;
            positionName?: string | null;
            jobContent?: string | null;
            /** Format: date-time */
            startDate?: string | null;
            /** Format: date-time */
            endDate?: string | null;
            witnessName?: string | null;
            witnessPhone?: string | null;
        };
        TaktHolidayCreateDto: {
            region?: string;
            holidayName?: string;
            /** Format: int32 */
            holidayType?: number;
            /** Format: date-time */
            startDate?: string;
            /** Format: date-time */
            endDate?: string;
            /** Format: int32 */
            isWorkingDay?: number;
            holidayGreeting?: string | null;
            holidayQuote?: string | null;
            holidayTheme?: string;
            remark?: string | null;
        };
        TaktHolidayDto: {
            /** Format: int64 */
            holidayId?: number;
            region?: string;
            holidayName?: string;
            /** Format: int32 */
            holidayType?: number;
            /** Format: date-time */
            startDate?: string;
            /** Format: date-time */
            endDate?: string;
            /** Format: int32 */
            isWorkingDay?: number;
            holidayGreeting?: string | null;
            holidayQuote?: string | null;
            holidayTheme?: string;
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
        TaktHolidayQueryDto: {
            region?: string | null;
            holidayName?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktHolidayUpdateDto: {
            /** Format: int64 */
            holidayId?: number;
            region?: string;
            holidayName?: string;
            /** Format: int32 */
            holidayType?: number;
            /** Format: date-time */
            startDate?: string;
            /** Format: date-time */
            endDate?: string;
            /** Format: int32 */
            isWorkingDay?: number;
            holidayGreeting?: string | null;
            holidayQuote?: string | null;
            holidayTheme?: string;
            remark?: string | null;
        };
        TaktLeaveCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            leaveType?: string;
            /** Format: date-time */
            startDate?: string;
            /** Format: date-time */
            endDate?: string;
            reason?: string | null;
            proofAttachmentsJson?: string | null;
        };
        TaktLeaveDto: {
            /** Format: int64 */
            leaveId?: number;
            /** Format: int64 */
            employeeId?: number;
            leaveType?: string;
            /** Format: date-time */
            startDate?: string;
            /** Format: date-time */
            endDate?: string;
            reason?: string | null;
            proofAttachmentsJson?: string | null;
            /** Format: int64 */
            flowInstanceId?: number | null;
            /** Format: int32 */
            leaveStatus?: number;
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
        TaktLeaveQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            leaveType?: string | null;
            /** Format: int32 */
            leaveStatus?: number | null;
            /** Format: date-time */
            startDateFrom?: string | null;
            /** Format: date-time */
            startDateTo?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktLeaveStatusDto: {
            /** Format: int64 */
            leaveId?: number;
            /** Format: int32 */
            leaveStatus?: number;
            /** Format: int64 */
            flowInstanceId?: number | null;
        };
        TaktLeaveSubmitDto: {
            /** Format: int64 */
            employeeId?: number;
            leaveType?: string;
            /** Format: date-time */
            startDate?: string;
            /** Format: date-time */
            endDate?: string;
            reason?: string | null;
            proofAttachmentsJson?: string | null;
            processTitle?: string | null;
            frmData?: string | null;
        };
        TaktLeaveSubmitResultDto: {
            /** Format: int64 */
            leaveId?: number;
            /** Format: int64 */
            flowInstanceId?: number;
            instanceCode?: string;
            processKey?: string;
            processName?: string;
        };
        TaktLeaveUpdateDto: {
            /** Format: int64 */
            leaveId?: number;
            /** Format: int64 */
            employeeId?: number;
            leaveType?: string;
            /** Format: date-time */
            startDate?: string;
            /** Format: date-time */
            endDate?: string;
            reason?: string | null;
            proofAttachmentsJson?: string | null;
        };
        TaktOvertimeCreateDto: {
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            overtimeDate?: string;
            /** Format: double */
            plannedHours?: number;
            /** Format: double */
            actualHours?: number | null;
            reason?: string;
            /** Format: int32 */
            overtimeStatus?: number;
            remark?: string | null;
        };
        TaktOvertimeDto: {
            /** Format: int64 */
            overtimeId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            overtimeDate?: string;
            /** Format: double */
            plannedHours?: number;
            /** Format: double */
            actualHours?: number | null;
            reason?: string;
            /** Format: int32 */
            overtimeStatus?: number;
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
        TaktOvertimeQueryDto: {
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: int32 */
            overtimeStatus?: number | null;
            /** Format: date-time */
            overtimeDateFrom?: string | null;
            /** Format: date-time */
            overtimeDateTo?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktOvertimeUpdateDto: {
            /** Format: int64 */
            overtimeId?: number;
            /** Format: int64 */
            employeeId?: number;
            /** Format: date-time */
            overtimeDate?: string;
            /** Format: double */
            plannedHours?: number;
            /** Format: double */
            actualHours?: number | null;
            reason?: string;
            /** Format: int32 */
            overtimeStatus?: number;
            remark?: string | null;
        };
        TaktPagedResultOfTaktAttendanceCorrectionDto: {
            data?: components["schemas"]["TaktAttendanceCorrectionDto"][];
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
        TaktPagedResultOfTaktAttendanceDeviceDto: {
            data?: components["schemas"]["TaktAttendanceDeviceDto"][];
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
        TaktPagedResultOfTaktAttendanceExceptionDto: {
            data?: components["schemas"]["TaktAttendanceExceptionDto"][];
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
        TaktPagedResultOfTaktAttendancePunchDto: {
            data?: components["schemas"]["TaktAttendancePunchDto"][];
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
        TaktPagedResultOfTaktAttendanceResultDto: {
            data?: components["schemas"]["TaktAttendanceResultDto"][];
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
        TaktPagedResultOfTaktAttendanceSettingDto: {
            data?: components["schemas"]["TaktAttendanceSettingDto"][];
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
        TaktPagedResultOfTaktAttendanceSourceDto: {
            data?: components["schemas"]["TaktAttendanceSourceDto"][];
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
        TaktPagedResultOfTaktDeptDto: {
            data?: components["schemas"]["TaktDeptDto"][];
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
        TaktPagedResultOfTaktEmployeeAttachmentDto: {
            data?: components["schemas"]["TaktEmployeeAttachmentDto"][];
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
        TaktPagedResultOfTaktEmployeeCareerDto: {
            data?: components["schemas"]["TaktEmployeeCareerDto"][];
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
        TaktPagedResultOfTaktEmployeeContractDto: {
            data?: components["schemas"]["TaktEmployeeContractDto"][];
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
        TaktPagedResultOfTaktEmployeeDto: {
            data?: components["schemas"]["TaktEmployeeDto"][];
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
        TaktPagedResultOfTaktEmployeeEducationDto: {
            data?: components["schemas"]["TaktEmployeeEducationDto"][];
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
        TaktPagedResultOfTaktEmployeeFamilyDto: {
            data?: components["schemas"]["TaktEmployeeFamilyDto"][];
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
        TaktPagedResultOfTaktEmployeeSkillDto: {
            data?: components["schemas"]["TaktEmployeeSkillDto"][];
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
        TaktPagedResultOfTaktEmployeeTransferDto: {
            data?: components["schemas"]["TaktEmployeeTransferDto"][];
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
        TaktPagedResultOfTaktEmployeeWorkDto: {
            data?: components["schemas"]["TaktEmployeeWorkDto"][];
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
        TaktPagedResultOfTaktHolidayDto: {
            data?: components["schemas"]["TaktHolidayDto"][];
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
        TaktPagedResultOfTaktLeaveDto: {
            data?: components["schemas"]["TaktLeaveDto"][];
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
        TaktPagedResultOfTaktOvertimeDto: {
            data?: components["schemas"]["TaktOvertimeDto"][];
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
        TaktPagedResultOfTaktPostDto: {
            data?: components["schemas"]["TaktPostDto"][];
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
        TaktPagedResultOfTaktRoleDeptDto: {
            data?: components["schemas"]["TaktRoleDeptDto"][];
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
        TaktPagedResultOfTaktShiftScheduleDto: {
            data?: components["schemas"]["TaktShiftScheduleDto"][];
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
        TaktPagedResultOfTaktWorkShiftDto: {
            data?: components["schemas"]["TaktWorkShiftDto"][];
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
        TaktPostCreateDto: {
            postName?: string;
            postCode?: string;
            /** Format: int64 */
            deptId?: number;
            postCategory?: string | null;
            /** Format: int32 */
            postLevel?: number;
            postDuty?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            dataScope?: number;
            customScope?: string | null;
            remark?: string | null;
            userIds?: number[] | null;
        };
        TaktPostDto: {
            /** Format: int64 */
            postId?: number;
            postName?: string;
            postCode?: string;
            /** Format: int64 */
            deptId?: number;
            postCategory?: string | null;
            /** Format: int32 */
            postLevel?: number;
            postDuty?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            dataScope?: number;
            customScope?: string | null;
            /** Format: int32 */
            postStatus?: number;
            userIds?: number[] | null;
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
        TaktPostQueryDto: {
            postName?: string | null;
            postCode?: string | null;
            /** Format: int64 */
            deptId?: number | null;
            /** Format: int32 */
            postStatus?: number | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktPostStatusDto: {
            /** Format: int64 */
            postId?: number;
            /** Format: int32 */
            postStatus?: number;
        };
        TaktPostUpdateDto: {
            /** Format: int64 */
            postId?: number;
            postName?: string;
            postCode?: string;
            /** Format: int64 */
            deptId?: number;
            postCategory?: string | null;
            /** Format: int32 */
            postLevel?: number;
            postDuty?: string | null;
            /** Format: int32 */
            orderNum?: number;
            /** Format: int32 */
            dataScope?: number;
            customScope?: string | null;
            remark?: string | null;
            userIds?: number[] | null;
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
        TaktShiftScheduleCreateDto: {
            /** Format: int32 */
            scheduleType?: number;
            /** Format: int64 */
            deptId?: number | null;
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: date-time */
            scheduleDate?: string;
            /** Format: int64 */
            shiftId?: number;
            remark?: string | null;
        };
        TaktShiftScheduleDto: {
            /** Format: int64 */
            shiftScheduleId?: number;
            /** Format: int32 */
            scheduleType?: number;
            /** Format: int64 */
            deptId?: number | null;
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: date-time */
            scheduleDate?: string;
            /** Format: int64 */
            shiftId?: number;
            shiftName?: string | null;
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
        TaktShiftScheduleQueryDto: {
            /** Format: int32 */
            scheduleType?: number | null;
            /** Format: int64 */
            deptId?: number | null;
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: int64 */
            shiftId?: number | null;
            /** Format: date-time */
            scheduleDateFrom?: string | null;
            /** Format: date-time */
            scheduleDateTo?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktShiftScheduleUpdateDto: {
            /** Format: int64 */
            shiftScheduleId?: number;
            /** Format: int32 */
            scheduleType?: number;
            /** Format: int64 */
            deptId?: number | null;
            /** Format: int64 */
            employeeId?: number | null;
            /** Format: date-time */
            scheduleDate?: string;
            /** Format: int64 */
            shiftId?: number;
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
        TaktWorkShiftCreateDto: {
            shiftCode?: string;
            shiftName?: string;
            startTime?: string;
            endTime?: string;
            /** Format: int32 */
            crossMidnight?: number;
            /** Format: int32 */
            orderNum?: number;
            remark?: string | null;
        };
        TaktWorkShiftDto: {
            /** Format: int64 */
            shiftId?: number;
            shiftCode?: string;
            shiftName?: string;
            startTime?: string;
            endTime?: string;
            /** Format: int32 */
            crossMidnight?: number;
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
        TaktWorkShiftQueryDto: {
            shiftCode?: string | null;
            shiftName?: string | null;
            /** Format: int32 */
            pageIndex?: number;
            /** Format: int32 */
            pageSize?: number;
            keyWords?: string | null;
        };
        TaktWorkShiftUpdateDto: {
            /** Format: int64 */
            shiftId?: number;
            shiftCode?: string;
            shiftName?: string;
            startTime?: string;
            endTime?: string;
            /** Format: int32 */
            crossMidnight?: number;
            /** Format: int32 */
            orderNum?: number;
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
    "\u5458\u5DE5\u9644\u4EF6": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktEmployeeAttachmentCreateDto"];
                "application/json": components["schemas"]["TaktEmployeeAttachmentCreateDto"];
                "text/json": components["schemas"]["TaktEmployeeAttachmentCreateDto"];
                "application/*+json": components["schemas"]["TaktEmployeeAttachmentCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktEmployeeAttachmentDto"];
                    "application/json": components["schemas"]["TaktEmployeeAttachmentDto"];
                    "text/json": components["schemas"]["TaktEmployeeAttachmentDto"];
                };
            };
        };
    };
    "\u5458\u5DE5\u804C\u4E1A\u4FE1\u606F": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktEmployeeCareerCreateDto"];
                "application/json": components["schemas"]["TaktEmployeeCareerCreateDto"];
                "text/json": components["schemas"]["TaktEmployeeCareerCreateDto"];
                "application/*+json": components["schemas"]["TaktEmployeeCareerCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktEmployeeCareerDto"];
                    "application/json": components["schemas"]["TaktEmployeeCareerDto"];
                    "text/json": components["schemas"]["TaktEmployeeCareerDto"];
                };
            };
        };
    };
    "\u5458\u5DE5\u5408\u540C": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktEmployeeContractCreateDto"];
                "application/json": components["schemas"]["TaktEmployeeContractCreateDto"];
                "text/json": components["schemas"]["TaktEmployeeContractCreateDto"];
                "application/*+json": components["schemas"]["TaktEmployeeContractCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktEmployeeContractDto"];
                    "application/json": components["schemas"]["TaktEmployeeContractDto"];
                    "text/json": components["schemas"]["TaktEmployeeContractDto"];
                };
            };
        };
    };
    "\u5458\u5DE5\u6559\u80B2\u7ECF\u5386": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktEmployeeEducationCreateDto"];
                "application/json": components["schemas"]["TaktEmployeeEducationCreateDto"];
                "text/json": components["schemas"]["TaktEmployeeEducationCreateDto"];
                "application/*+json": components["schemas"]["TaktEmployeeEducationCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktEmployeeEducationDto"];
                    "application/json": components["schemas"]["TaktEmployeeEducationDto"];
                    "text/json": components["schemas"]["TaktEmployeeEducationDto"];
                };
            };
        };
    };
    "\u5458\u5DE5\u5BB6\u5EAD\u6210\u5458": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktEmployeeFamilyCreateDto"];
                "application/json": components["schemas"]["TaktEmployeeFamilyCreateDto"];
                "text/json": components["schemas"]["TaktEmployeeFamilyCreateDto"];
                "application/*+json": components["schemas"]["TaktEmployeeFamilyCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktEmployeeFamilyDto"];
                    "application/json": components["schemas"]["TaktEmployeeFamilyDto"];
                    "text/json": components["schemas"]["TaktEmployeeFamilyDto"];
                };
            };
        };
    };
    "\u5458\u5DE5": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktEmployeeCreateDto"];
                "application/json": components["schemas"]["TaktEmployeeCreateDto"];
                "text/json": components["schemas"]["TaktEmployeeCreateDto"];
                "application/*+json": components["schemas"]["TaktEmployeeCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktEmployeeDto"];
                    "application/json": components["schemas"]["TaktEmployeeDto"];
                    "text/json": components["schemas"]["TaktEmployeeDto"];
                };
            };
        };
    };
    "\u5458\u5DE5\u4E1A\u52A1\u6280\u80FD": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktEmployeeSkillCreateDto"];
                "application/json": components["schemas"]["TaktEmployeeSkillCreateDto"];
                "text/json": components["schemas"]["TaktEmployeeSkillCreateDto"];
                "application/*+json": components["schemas"]["TaktEmployeeSkillCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktEmployeeSkillDto"];
                    "application/json": components["schemas"]["TaktEmployeeSkillDto"];
                    "text/json": components["schemas"]["TaktEmployeeSkillDto"];
                };
            };
        };
    };
    "\u5458\u5DE5\u8C03\u52A8": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktEmployeeTransferCreateDto"];
                "application/json": components["schemas"]["TaktEmployeeTransferCreateDto"];
                "text/json": components["schemas"]["TaktEmployeeTransferCreateDto"];
                "application/*+json": components["schemas"]["TaktEmployeeTransferCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktEmployeeTransferDto"];
                    "application/json": components["schemas"]["TaktEmployeeTransferDto"];
                    "text/json": components["schemas"]["TaktEmployeeTransferDto"];
                };
            };
        };
    };
    "\u5458\u5DE5\u5DE5\u4F5C\u7ECF\u5386": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktEmployeeWorkCreateDto"];
                "application/json": components["schemas"]["TaktEmployeeWorkCreateDto"];
                "text/json": components["schemas"]["TaktEmployeeWorkCreateDto"];
                "application/*+json": components["schemas"]["TaktEmployeeWorkCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktEmployeeWorkDto"];
                    "application/json": components["schemas"]["TaktEmployeeWorkDto"];
                    "text/json": components["schemas"]["TaktEmployeeWorkDto"];
                };
            };
        };
    };
    "\u90E8\u95E8": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktDeptCreateDto"];
                "application/json": components["schemas"]["TaktDeptCreateDto"];
                "text/json": components["schemas"]["TaktDeptCreateDto"];
                "application/*+json": components["schemas"]["TaktDeptCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktDeptDto"];
                    "application/json": components["schemas"]["TaktDeptDto"];
                    "text/json": components["schemas"]["TaktDeptDto"];
                };
            };
        };
    };
    "\u5C97\u4F4D": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktPostCreateDto"];
                "application/json": components["schemas"]["TaktPostCreateDto"];
                "text/json": components["schemas"]["TaktPostCreateDto"];
                "application/*+json": components["schemas"]["TaktPostCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktPostDto"];
                    "application/json": components["schemas"]["TaktPostDto"];
                    "text/json": components["schemas"]["TaktPostDto"];
                };
            };
        };
    };
    "\u8865\u5361": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktAttendanceCorrectionCreateDto"];
                "application/json": components["schemas"]["TaktAttendanceCorrectionCreateDto"];
                "text/json": components["schemas"]["TaktAttendanceCorrectionCreateDto"];
                "application/*+json": components["schemas"]["TaktAttendanceCorrectionCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktAttendanceCorrectionDto"];
                    "application/json": components["schemas"]["TaktAttendanceCorrectionDto"];
                    "text/json": components["schemas"]["TaktAttendanceCorrectionDto"];
                };
            };
        };
    };
    "\u8003\u52E4\u8BBE\u5907": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktAttendanceDeviceCreateDto"];
                "application/json": components["schemas"]["TaktAttendanceDeviceCreateDto"];
                "text/json": components["schemas"]["TaktAttendanceDeviceCreateDto"];
                "application/*+json": components["schemas"]["TaktAttendanceDeviceCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktAttendanceDeviceDto"];
                    "application/json": components["schemas"]["TaktAttendanceDeviceDto"];
                    "text/json": components["schemas"]["TaktAttendanceDeviceDto"];
                };
            };
        };
    };
    "\u8003\u52E4\u5F02\u5E38": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktAttendanceExceptionCreateDto"];
                "application/json": components["schemas"]["TaktAttendanceExceptionCreateDto"];
                "text/json": components["schemas"]["TaktAttendanceExceptionCreateDto"];
                "application/*+json": components["schemas"]["TaktAttendanceExceptionCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktAttendanceExceptionDto"];
                    "application/json": components["schemas"]["TaktAttendanceExceptionDto"];
                    "text/json": components["schemas"]["TaktAttendanceExceptionDto"];
                };
            };
        };
    };
    "\u6253\u5361": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktAttendancePunchCreateDto"];
                "application/json": components["schemas"]["TaktAttendancePunchCreateDto"];
                "text/json": components["schemas"]["TaktAttendancePunchCreateDto"];
                "application/*+json": components["schemas"]["TaktAttendancePunchCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktAttendancePunchDto"];
                    "application/json": components["schemas"]["TaktAttendancePunchDto"];
                    "text/json": components["schemas"]["TaktAttendancePunchDto"];
                };
            };
        };
    };
    "\u8003\u52E4\u7ED3\u679C": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktAttendanceResultCreateDto"];
                "application/json": components["schemas"]["TaktAttendanceResultCreateDto"];
                "text/json": components["schemas"]["TaktAttendanceResultCreateDto"];
                "application/*+json": components["schemas"]["TaktAttendanceResultCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktAttendanceResultDto"];
                    "application/json": components["schemas"]["TaktAttendanceResultDto"];
                    "text/json": components["schemas"]["TaktAttendanceResultDto"];
                };
            };
        };
    };
    "\u8003\u52E4\u8BBE\u7F6E": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktAttendanceSettingCreateDto"];
                "application/json": components["schemas"]["TaktAttendanceSettingCreateDto"];
                "text/json": components["schemas"]["TaktAttendanceSettingCreateDto"];
                "application/*+json": components["schemas"]["TaktAttendanceSettingCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktAttendanceSettingDto"];
                    "application/json": components["schemas"]["TaktAttendanceSettingDto"];
                    "text/json": components["schemas"]["TaktAttendanceSettingDto"];
                };
            };
        };
    };
    "\u8003\u52E4\u6E90\u8BB0\u5F55": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktAttendanceSourceCreateDto"];
                "application/json": components["schemas"]["TaktAttendanceSourceCreateDto"];
                "text/json": components["schemas"]["TaktAttendanceSourceCreateDto"];
                "application/*+json": components["schemas"]["TaktAttendanceSourceCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktAttendanceSourceDto"];
                    "application/json": components["schemas"]["TaktAttendanceSourceDto"];
                    "text/json": components["schemas"]["TaktAttendanceSourceDto"];
                };
            };
        };
    };
    "\u5047\u65E5": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktHolidayCreateDto"];
                "application/json": components["schemas"]["TaktHolidayCreateDto"];
                "text/json": components["schemas"]["TaktHolidayCreateDto"];
                "application/*+json": components["schemas"]["TaktHolidayCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktHolidayDto"];
                    "application/json": components["schemas"]["TaktHolidayDto"];
                    "text/json": components["schemas"]["TaktHolidayDto"];
                };
            };
        };
    };
    "\u8BF7\u5047": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktLeaveCreateDto"];
                "application/json": components["schemas"]["TaktLeaveCreateDto"];
                "text/json": components["schemas"]["TaktLeaveCreateDto"];
                "application/*+json": components["schemas"]["TaktLeaveCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktLeaveDto"];
                    "application/json": components["schemas"]["TaktLeaveDto"];
                    "text/json": components["schemas"]["TaktLeaveDto"];
                };
            };
        };
    };
    "\u52A0\u73ED": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktOvertimeCreateDto"];
                "application/json": components["schemas"]["TaktOvertimeCreateDto"];
                "text/json": components["schemas"]["TaktOvertimeCreateDto"];
                "application/*+json": components["schemas"]["TaktOvertimeCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktOvertimeDto"];
                    "application/json": components["schemas"]["TaktOvertimeDto"];
                    "text/json": components["schemas"]["TaktOvertimeDto"];
                };
            };
        };
    };
    "\u6392\u73ED": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktShiftScheduleCreateDto"];
                "application/json": components["schemas"]["TaktShiftScheduleCreateDto"];
                "text/json": components["schemas"]["TaktShiftScheduleCreateDto"];
                "application/*+json": components["schemas"]["TaktShiftScheduleCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktShiftScheduleDto"];
                    "application/json": components["schemas"]["TaktShiftScheduleDto"];
                    "text/json": components["schemas"]["TaktShiftScheduleDto"];
                };
            };
        };
    };
    "\u73ED\u6B21": {
        parameters: {
            query?: never;
            header?: never;
            path?: never;
            cookie?: never;
        };
        requestBody: {
            content: {
                "application/json-patch+json": components["schemas"]["TaktWorkShiftCreateDto"];
                "application/json": components["schemas"]["TaktWorkShiftCreateDto"];
                "text/json": components["schemas"]["TaktWorkShiftCreateDto"];
                "application/*+json": components["schemas"]["TaktWorkShiftCreateDto"];
            };
        };
        responses: {
            /** @description OK */
            200: {
                headers: {
                    [name: string]: unknown;
                };
                content: {
                    "text/plain": components["schemas"]["TaktWorkShiftDto"];
                    "application/json": components["schemas"]["TaktWorkShiftDto"];
                    "text/json": components["schemas"]["TaktWorkShiftDto"];
                };
            };
        };
    };
}
