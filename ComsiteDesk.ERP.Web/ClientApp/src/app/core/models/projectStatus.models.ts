export class ProjectStatusModel {
    id : number;
    name : string;
}

// Search Data
export interface SearchResult {
    tables: ProjectStatusModel[];
    total: number;
}