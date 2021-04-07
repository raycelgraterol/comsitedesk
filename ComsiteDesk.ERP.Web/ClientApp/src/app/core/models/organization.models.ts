export class OrganizationModel {
    id : number;
    businessName : string;        
    RIF : string;
    email : string;
    phoneNumber : string;
    address : string;
}

// Search Data
export interface SearchResult {
    tables: OrganizationModel[];
    total: number;
}
