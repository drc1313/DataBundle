import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css']
})
export class AccountComponent {
  
  accountName: string = '';
  docLink: string = '';
  apikey: string = '';
  category: number = 0;
  
  status = '';

  public accounts?: APIAccounts[];
  constructor(private http: HttpClient) {
    this.accountGetAll();
  }

  
  async accountGetAll(){    
    this.http.get<APIAccounts[]>('/api/APIAccounts').subscribe(result => {
      this.accounts = result;
    }, error => console.error(error));
  }

  accountUpdate() {
    const body = { title: 'Angular PUT Request Example' };
    this.http.put<any>('https://posts/1', body);        
  }

  async accountCreate() {
    const body = { ApiHostName: this.accountName, DocumenationLink: this.docLink, ApiKey: this.apikey, Category: this.category };
    await firstValueFrom(this.http.post<any>('/api/APIAccounts/', body));
    this.accountGetAll();
  }

  async accountDelete(id: string){
    await firstValueFrom( this.http.delete<any>('/api/APIAccounts/'+id))
    this.accountGetAll();
  }


  title = 'DataBundleClient';
}

interface APIAccounts {
  
  accountId: string;
  apiHostName: string;
  documenationLink: string;
  apiKey: string;
  category: number;
}

