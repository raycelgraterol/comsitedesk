export class Actionsviews {
    id: number;
    formId: number;
    formName: string;
    actionId: number;
    actionName: string;
}

// Search Data
export interface SearchResult {
    tables: Actionsviews[];
    total: number;
}