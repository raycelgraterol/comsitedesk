export class TicketProcesses {
    id: number;
    name: string;
    step: number;
}

// Search Data
export interface SearchResult {
    tables: TicketProcesses[];
    total: number;
}
