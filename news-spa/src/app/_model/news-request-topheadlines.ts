export interface NewsRequestTopheadlines{
    page: number;
    pageSize: number;
    q: string;
    sources: string[],
    category?: number;
    language?: number;
    country?: number; 
}