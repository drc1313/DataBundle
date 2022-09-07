export interface RequestInterface {
  getAll(): Promise<void>
  create(): Promise<void>
  update(id: string): Promise<void>
  delete(id: string): Promise<void>
}