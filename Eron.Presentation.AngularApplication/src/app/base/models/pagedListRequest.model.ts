export interface PagedListRequest {
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  order: string;
  filter: string;
}
