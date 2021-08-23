export class AssignmentStatus {
    id : number;
    name : string;
}

// Search Data
export interface SearchResult {
    tables: AssignmentStatus[];
    total: number;
}