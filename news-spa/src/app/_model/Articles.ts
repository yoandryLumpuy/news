import { ArticleSource } from './ArticleSource';

export interface Article
{
    source: ArticleSource;
    author : string;
    title: string;
    description: string;
    url : string;
    urlToImage : string;
    publishedAt? : Date;
}