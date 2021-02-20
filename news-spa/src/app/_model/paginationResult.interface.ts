export interface PaginationResult<T>{
    totalItems: number;
    page : number;
    pageSize : number;
    totalPages : number;
    items : Array<T>
}

export const defaultPaginationResult : PaginationResult<any> 
    =  {
        totalPages: 0,
        items: [],
        page: 0,
        pageSize: 10,
        totalItems: 0
      };