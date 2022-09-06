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

  editId: string = ""
  editMode: boolean = false;
  
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

  async accountUpdate(id: string) {
    const body = { AccountID: id, ApiHostName: this.accountName, DocumenationLink: this.docLink, ApiKey: this.apikey, Category: this.category };
    await firstValueFrom(this.http.put<any>('/api/APIAccounts/'+id, body));        
  }

  async accountCreate() {
    if(!this.editMode)
    {
      const body = { ApiHostName: this.accountName, DocumenationLink: this.docLink, ApiKey: this.apikey, Category: this.category };
      await firstValueFrom(this.http.post<any>('/api/APIAccounts/', body));
    }
    else
    {
      await this.accountUpdate(this.editId);
      this.editMode = false;
    }
    this.accountGetAll();
    this.clearInput();
  }

  async accountDelete(id: string){
    await firstValueFrom( this.http.delete<any>('/api/APIAccounts/'+id))
    this.accountGetAll();
  }

  async accountPopulateInput(account: APIAccounts)
  {
      this.accountName = account.apiHostName;
      this.docLink = account.documenationLink;
      this.apikey = account.apiKey;
      this.category = account.category;
      this.editMode = true;
      this.editId = account.accountId;
  }

  clearInput()
  {
    this.accountName = "";
    this.docLink = "";
    this.apikey = "";
    this.category = 0;
    this.editId = "";

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

