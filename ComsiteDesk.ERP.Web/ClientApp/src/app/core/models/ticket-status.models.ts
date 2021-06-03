export class TicketStatus {
    id: number;
    name: string;
}

// Search Data
export interface SearchResult {
    tables: TicketStatus[];
    total: number;
}