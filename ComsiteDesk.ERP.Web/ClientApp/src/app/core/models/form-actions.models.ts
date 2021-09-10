export class FormActions {
    id: number;
    name: string;
    description: string;
}

// Search Data
export interface SearchResult {
    tables: FormActions[];
    total: number;
}