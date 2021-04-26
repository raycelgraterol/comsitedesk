export class Rol {
    id: number;
    name: string;
}

// Search Data
export interface SearchResult {
    tables: Rol[];
    total: number;
}