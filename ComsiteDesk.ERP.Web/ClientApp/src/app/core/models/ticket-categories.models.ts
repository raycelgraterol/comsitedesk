export class TicketCategories{
    id: number;
    name: string;
}

// Search Data
export interface SearchResult {
    tables: TicketCategories[];
    total: number;
}