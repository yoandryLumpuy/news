import { Article } from './Articles';

export interface ArticlesResult
{
    status : string;
    error: string;
    totalResults: number;
    articles : Array<Article>;
}