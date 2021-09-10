export class HeadquarterModel {
    id: number;
    name: string;
    phoneNumber: string;
    address: string;

    clientId: number;
}

// Search Data
export interface SearchResult {
    tables: HeadquarterModel[];
    total: number;
}