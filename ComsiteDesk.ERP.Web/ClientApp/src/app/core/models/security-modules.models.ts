export class SecurityModules {
    id: number;
    name: string;
    uri: string;
    description: string;
}

// Search Data
export interface SearchResult {
    tables: SecurityModules[];
    total: number;
}
