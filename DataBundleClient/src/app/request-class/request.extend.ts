import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

export class RequestExtend {
  
  editId: string = ""
  editMode: boolean = false;
  
  requestPath:string;
  
  public responseElements?: any[];

  constructor(private http: HttpClient, requestPath:string) {
    this.requestPath = requestPath;
    this.getAll();
  }

  async getAll(): Promise<void> {
    this.responseElements = await firstValueFrom(this.http.get<any[]>(this.requestPath))
  }
  async create(body:{}): Promise<void> {
    await firstValueFrom(this.http.post<any>(this.requestPath, body));
  }
  async update(id: string, body:{}): Promise<void> {
    await firstValueFrom(this.http.put<any>(this.requestPath+id, body));        
  }

  async delete(id: string): Promise<void> {
    await firstValueFrom( this.http.delete<any>(this.requestPath+id))
    this.getAll();
  }
}
