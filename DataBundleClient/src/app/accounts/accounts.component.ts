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
  
  instance:APIAccounts;

  constructor(http: HttpClient,) {
    super(http, "/api/APIAccounts/");
    this.instance = new APIAccounts();
  }

  async submit(body:APIAccounts)
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
      this.instance.accountName = account.accountName;
      this.instance.documenationLink = account.documenationLink;
      this.instance.apiKey = account.apiKey;
      this.instance.dateFormat = account.dateFormat;
      this.instance.delimiter = account.delimiter;
      this.instance.category = account.category;

      this.editMode = true;
      this.editId = account.accountName;
  }

  clearInput()
  {
    this.instance.accountName = "";
    this.instance.documenationLink = "";
    this.instance.apiKey = "";
    this.instance.category = 0;
  }

}

class APIAccounts {  
  accountName: string="";
  documenationLink: string="";
  apiKey: string="";
  dateFormat: string="";
  delimiter: string="";
  category: number=0;
}

