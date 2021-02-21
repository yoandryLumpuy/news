export interface UserForRegister{
    userName: string; 
    password: string; 
    country: number; 
    language: number
}

export const defaultUserForRegister : UserForRegister = 
    {userName:'', password: '', country: 0, language: 0}