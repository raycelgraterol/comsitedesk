export class RoleFormAction {
    formActionId: number;
    roleId: number;
    formName: string;
    actionName: string;
}

export class RoleFormArrayActions {
    roleId: number;
    formId: number;
    formActionIds: Array<number>;
}

// Search Data
export interface SearchResult {
    tables: RoleFormAction[];
    total: number;
}
