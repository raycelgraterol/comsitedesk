export class EquipmentModel {
    id: number;
    name: string;
    type: string;
    make: string;
    model: string;
    serial: string;
    features: string;
    notes: string;
    departmentId: number;
    departmentName: string;
    equipmentUserId: number;
    equipmentUserName: string;
}

// Search Data
export interface SearchResult {
    tables: EquipmentModel[];
    total: number;
}