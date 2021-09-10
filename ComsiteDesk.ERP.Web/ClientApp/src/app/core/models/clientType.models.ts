export class ClientTypeModel {
    id: number;
    name: string;
    code: string;
}

// Search Data
export interface SearchResult {
    tables: ClientTypeModel[];
    total: number;
}