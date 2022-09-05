import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public accounts?: APIAccounts[];

  constructor(http: HttpClient) {
    http.get<APIAccounts[]>('/api/APIAccounts').subscribe(result => {
      this.accounts = result;
    }, error => console.error(error));
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
