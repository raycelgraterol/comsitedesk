export class ClientModel {
    id : number;
    businessName : string;
    RIF : string;
    email : string;
    phoneNumber : string;
    address : string;
    clientTypeId : number;
}

// Search Data
export interface SearchResult {
    tables: ClientModel[];
    total: number;
}