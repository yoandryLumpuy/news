import { Article } from './Articles';

export interface ArticlesResult
{
    status : string;
    error: string;
    totalResults: number;
    articles : Array<Article>;
}

export const defaultArticlesResult : ArticlesResult =
{
    status : '',
    error: '',
    totalResults: 0,
    articles : []
}