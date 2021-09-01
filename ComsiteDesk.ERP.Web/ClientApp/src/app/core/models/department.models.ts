export class DepartmentModel {
    id: number;
    name: string;
    phoneNumber: string;
    headquarterId: number;
    headquarterName: string;
}

// Search Data
export interface SearchResult {
    tables: DepartmentModel[];
    total: number;
}