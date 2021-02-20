export interface NewsRequestTopheadlines{
    page: number;
    pageSize: number;
    q: string;
    sources: string[],
    category?: number;
    language?: number;
    country?: number; 
}

export const defaultNewsRequestTopheadlines : NewsRequestTopheadlines 
  = {
        page: 0,
        pageSize: 10,
        q: '',
        sources: [] 
    }