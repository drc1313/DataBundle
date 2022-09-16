import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RequestExtend } from '../request-class/request.extend';
import {Usage, APIUsage} from '../usage/usage-class'

@Component({
  selector: 'app-root',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css']
})

export class AccountComponent extends RequestExtend{
 
  title = 'DataBundleClient';
  
  instance:APIAccounts;
  usageInstance = Usage.getInstance();

  constructor(http: HttpClient,) {
    super(http, "/api/APIAccounts/");
    this.instance = new APIAccounts();
  }

  async submit(body:APIAccounts)
  {

    if(!this.editMode)
    {
      await this.create(body)
      await this.usageInstance.createUsageForAccount(body.accountName)
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
      this.instance.headers = account.headers;
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
    this.instance.headers = "";
    this.instance.category = 0;
  }

}

export class APIAccounts {  
  accountName: string="";
  documenationLink: string="";
  apiKey: string="";
  headers: string="";
  dateFormat: string="";
  delimiter: string="";
  category: number=0;
}

