export interface QueryObjectTopheadingRequest {
    id: number;
    createdByUser: string;
    createdAt: Date;
    q: string;
    sources: string;
    category: string;
    language: string;
    country: string;
    page: number;
    pageSize: number;
}
