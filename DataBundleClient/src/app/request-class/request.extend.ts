import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

export class RequestExtend {
  
  editId: string = ""
  editMode: boolean = false;
  
  requestPath:string;
  
  public responseElements?: any[];

  constructor(public http: HttpClient, requestPath:string) {
    this.requestPath = requestPath;
    this.getAll();
  }

  async getAll(path = this.requestPath): Promise<any> {
    this.responseElements = await firstValueFrom(this.http.get<any[]>(path))
    return this.responseElements;
  }
  async get(id: string, path = this.requestPath): Promise<any> {
    return await firstValueFrom(this.http.get<any>(path+id))
  }
  async create(body:any, path = this.requestPath): Promise<void> {
    await firstValueFrom(this.http.post<any>(path, body));
  }
  async update(id: string, body:any, path = this.requestPath): Promise<void> {
    await firstValueFrom(this.http.put<any>(path+id, body));        
  }

  async delete(id: string, path = this.requestPath): Promise<void> {
   // await firstValueFrom( this.http.delete<any>(path+id))
    this.getAll();
  }
}
