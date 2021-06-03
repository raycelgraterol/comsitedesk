export class TicketTypes {
    id: number;
    name: string;
}

// Search Data
export interface SearchResult {
    tables: TicketTypes[];
    total: number;
}