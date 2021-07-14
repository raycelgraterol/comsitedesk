export class Tickets {
    id : number;
    title : string;
    ticketDate : Date;
    hoursWorked : number;
    reportedFailure : string;
    technicalFailure : string;
    solutionDone : string;
    notes : string;
    startTime : Date;
    endTime : Date;
    ticketStatusId : number;
    ticketCategoryId : number;    
    ticketTypeId : number;
    ticketProcessId : number;
    ticketStatusName: number;
    ticketCategoryName: number;    
    ticketTypeName: number;
    ticketProcessName: number;
    organizationId : number;
    usersIds: Array<number>;
    users: any;
}

export interface CardData {
    icon: string;
    tickets: number;
    title: string;
    text: string;
}

// Search Data
export interface SearchResult {
    tables: Tickets[];
    total: number;
}