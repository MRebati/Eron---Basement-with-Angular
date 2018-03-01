export interface PagedListResult<T> {
  result: T[];
  pageSize: number;
  pageNumber: number;
  totalCount: number;
}
