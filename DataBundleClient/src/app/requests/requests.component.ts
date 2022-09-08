import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RequestExtend } from '../request-class/request.extend';

@Component({
  selector: 'app-root',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})

export class RequestComponent extends RequestExtend{
 
  title = 'DataBundleClient';

  accountName: string = '';
  requestName: string = '';
  requestURL: string = '';

  constructor(http: HttpClient,) {
    super(http, "/api/APIRequests/");
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

  async getAccount(AccountName:string)
  {
    var response = await this.get(AccountName, "/api/APIAccounts/");
    console.log(response)
  }

  async accountPopulateInput(singleRequest: APIRequest)
  {
      this.accountName = singleRequest.accountName;
      this.requestName = singleRequest.requestName;
      this.requestURL = singleRequest.requestURL;

      this.editMode = true;
      this.editId = singleRequest.requestId;
  }

  clearInput()
  {
    this.accountName = "";
    this.requestName = "";
    this.requestURL = "";
  }

}





interface APIRequest {
  requestId: string;
  accountName: string;
  requestName: string;
  requestURL: string;
}

