export class ProjectModel {
    id : number;
    title : string;
    description: string;
    startDate : Date;
    endDate : Date;
    organizationId: number;
    projectStatusId: number;
    projectStatusName: string;
    organizationName: string;
    totalTasks: number;
}

// Search Data
export interface SearchResult {
    tables: ProjectModel[];
    total: number;
}