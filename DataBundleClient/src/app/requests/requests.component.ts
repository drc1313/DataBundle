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

  instance:APIRequest;

  constructor(http: HttpClient,) {
    super(http, "/api/APIRequests/");
    this.instance = new APIRequest();
  }

  async submit(body:APIRequest)
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

  testRequest(request:APIRequest)
  {
    console.log(request.requestURL);

  }

  async accountPopulateInput(singleRequest: APIRequest)
  {
      this.instance.accountName = singleRequest.accountName;
      this.instance.requestName = singleRequest.requestName;
      this.instance.requestURL = singleRequest.requestURL;

      this.editMode = true;
      this.editId = singleRequest.requestId;
  }

  clearInput()
  {
    this.instance.accountName = "";
    this.instance.requestName = "";
    this.instance.requestURL = "";
  }
}

class APIRequest {
  requestId: string ="00000000-0000-0000-0000-000000000000";
  accountName: string="";
  requestName: string="";;
  requestURL: string="";;
}

