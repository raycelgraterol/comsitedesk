export class HeadquarterModel {
    id: number;
    name: string;
    phoneNumber: string;
    address: string;

    organizationsId: number;
}

// Search Data
export interface SearchResult {
    tables: HeadquarterModel[];
    total: number;
}