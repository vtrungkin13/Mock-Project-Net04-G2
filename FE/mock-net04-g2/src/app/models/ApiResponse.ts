export interface ApiResponse<T, E> {
  status: number;
  body: T;
  error: E;
}
