export class Assignments {
    id : number;
    title : string;
    description: string;
    dueDate : Date;
    projectsId: number;
    taskStatusId: number;
}

// Search Data
export interface SearchResult {
    tables: Assignments[];
    total: number;
}