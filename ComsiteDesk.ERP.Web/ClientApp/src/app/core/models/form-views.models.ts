export class FormViews {
    id: number;
    name: string;
    description: string;
    uri: string;
    moduleId: number;
    moduleName: string;
}

// Search Data
export interface SearchResult {
    tables: FormViews[];
    total: number;
}