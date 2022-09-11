import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RequestExtend } from '../request-class/request.extend';
import { FormGroup, FormArray, FormBuilder} from '@angular/forms'
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})

export class RequestComponent extends RequestExtend{
 
  title = 'DataBundleClient';
  productForm: FormGroup;
  inputInstance:APIRequest;
  requestURL:string="";
  
  constructor(private fb:FormBuilder, http: HttpClient) {
    super(http, "/api/APIRequests/");
    this.inputInstance = new APIRequest();
    this.productForm = this.fb.group({
      tokens: this.fb.array([]) ,
    });
  }

//Dynamic token Element input creation based of Request URL
  tokens() : FormArray{
    return this.productForm.get("tokens") as FormArray
  }

  newToken(tokenName:string, tokenValue:string): FormGroup{
    return this.fb.group({
      tokenName: tokenName,
      token: tokenValue
    })
  }

  clearTokens(){
    this.tokens().clear();
  }

  async testRequest(request:APIRequest){
    var account = await this.get(request.accountName, "/api/APIAccounts/")
    this.requestURL = request.requestURL
    this.createInputsFromTokens(account)
  }

  createInputsFromTokens(apiAccount:any){
    this.clearTokens();
    var split = this.requestURL.split("{")
    for (var elm of split)
    {
      var tokenName = elm.substring(0,elm.indexOf("}"))
      if (tokenName.length>0)
      {
        var tokenValue = ""
        if(tokenName.includes("APIKEY"))
        {
          tokenValue = apiAccount.apiKey
        }
        else if(tokenName.includes("DATE"))
        {
          tokenValue = apiAccount.dateFormat
        }
        this.tokens().push(this.newToken(tokenName, tokenValue));
      }        
    }
  }

  async requestSubmit(){
    var submitURL = this.requestURL;
    for(var token of this.productForm.value.tokens)
    {
      submitURL = submitURL.replace("{"+token.tokenName+"}",token.token)
    }
    await firstValueFrom(this.http.get<any>(submitURL))
  }

  //Functions for request creation/editing 
  async submit(body:APIRequest){

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
 
  async requestPopulateInput(singleRequest: APIRequest){
      this.inputInstance.accountName = singleRequest.accountName;
      this.inputInstance.requestName = singleRequest.requestName;
      this.inputInstance.requestURL = singleRequest.requestURL;

      this.editMode = true;
      this.editId = singleRequest.requestId;
  }

  clearInput(){
    this.inputInstance.accountName = "";
    this.inputInstance.requestName = "";
    this.inputInstance.requestURL = "";
  }
}

class APIRequest {
  requestId: string ="00000000-0000-0000-0000-000000000000";
  accountName: string="";
  requestName: string="";;
  requestURL: string="";;
}

