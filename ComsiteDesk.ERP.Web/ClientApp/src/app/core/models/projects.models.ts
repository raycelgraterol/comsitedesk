export class Projects {
    id : number;
    title : string;
    description: string;
    startDate : Date;
    endDate : Date;
    organizationId: number;
    projectStatusId: number;
    projectStatusName: string;
}

// Search Data
export interface SearchResult {
    tables: Projects[];
    total: number;
}