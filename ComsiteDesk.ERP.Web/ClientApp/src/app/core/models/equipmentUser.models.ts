export class EquipmentUserModel {
    id : number;
    name : string;
    phoneNumber : string;
    email : string;
}

// Search Data
export interface SearchResult {
    tables: EquipmentUserModel[];
    total: number;
}