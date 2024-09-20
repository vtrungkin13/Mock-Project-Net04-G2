export interface ApiResponse<T, E> {
  status: string;
  body: T;
  error: E;
}
