import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RequestExtend } from '../request-class/request.extend';

@Component({
  selector: 'app-root',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css']
})

export class AccountComponent extends RequestExtend{
 
  title = 'DataBundleClient';

  accountName: string = '';
  documenationLink: string = '';
  apiKey: string = '';
  dateFormat: string = '';
  delimiter: string = '';
  category: number = 0;

  constructor(http: HttpClient,) {
    super(http, "/api/APIAccounts/");
  }

  async submit(body:{})
  {

    if(!this.editMode)
    {
      await this.create(body)
    }
    else
    {
      await this.update(this.editId, body);
      this.editMode = false;
    }
    this.getAll();
    this.clearInput();
  }

  async accountPopulateInput(account: APIAccounts)
  {
      this.accountName = account.accountName;
      this.documenationLink = account.documenationLink;
      this.apiKey = account.apiKey;
      this.dateFormat = account.dateFormat;
      this.delimiter = account.delimiter;
      this.category = account.category;

      this.editMode = true;
      this.editId = account.accountName;
  }

  clearInput()
  {
    this.accountName = "";
    this.documenationLink = "";
    this.apiKey = "";
    this.category = 0;
    this.editId = "";
  }

}

interface APIAccounts {  
  accountName: string;
  documenationLink: string;
  apiKey: string;
  dateFormat: string;
  delimiter: string;
  category: number;
}

