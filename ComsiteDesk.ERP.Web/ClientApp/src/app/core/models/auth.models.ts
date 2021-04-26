export class User {
    id: number;
    userName: string;
    password: string;
    firstName: string;
    lastName: string;
    email: string;
    roles: string[];
    rolName: string;
    phoneNumber: string;
    fullName: string;
    organizationId: number;
    organization: any;
}

// Search Data
export interface SearchResult {
    tables: User[];
    total: number;
}