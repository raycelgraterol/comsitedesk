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
    organizationId : number;
}

// Search Data
export interface SearchResult {
    tables: Tickets[];
    total: number;
}