export interface PageUpdateModel {
  id: number;
  title: string;
  slug: string;
  keywords: string;
  content: string;
  description: string[];
  views: string;
  createDateTime?: Date;
}
