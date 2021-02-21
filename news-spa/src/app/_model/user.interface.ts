export interface User{
    id: number;
    userName : string;
    country: number;
    language: number;
    roles : Array<string>
}

export const defaultUser : User = {
    id: 0,
    userName: '',
    country: 0,
    language: 0,
    roles: new Array<string>()
}