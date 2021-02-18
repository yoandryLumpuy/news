export interface QueryObjectEverythingRequest {
    id: number;
    createdByUser: string;
    createdAt: Date;
    q: string;
    sources: string;
    domains: string;    
    language: string;       
    sortBy: string;
    page: number;
    pageSize: number;
    from?: Date;
    to?: Date;
}
