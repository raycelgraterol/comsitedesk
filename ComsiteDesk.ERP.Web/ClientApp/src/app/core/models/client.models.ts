export class ClientModel {
    id: number;
    businessName: string;
    firstName: string;
    lastName: string;
    idNumer: string;
    email: string;
    phoneNumber: string;
    address: string;
    clientTypesId: number;
    clientTypeName: string;
    organizationId: number;
    organizationName: string;
}

// Search Data
export interface SearchResult {
    tables: ClientModel[];
    total: number;
}