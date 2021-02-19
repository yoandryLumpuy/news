export interface NewsRequestEverything{
        page : number;
        pageSize : number;
        q: string;
        sources : string[];
        domains : string[];
        from? : Date;
        to? : Date;
        language? : number;
        sortBy? : number;        
}