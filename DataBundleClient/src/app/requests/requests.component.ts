import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RequestExtend } from '../request-class/request.extend';
import { FormGroup, FormArray, FormBuilder} from '@angular/forms'
import {Usage, APIUsage} from '../usage/usage-class'
import {APIAccounts} from '../accounts/accounts.component'

@Component({
  selector: 'app-root',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})

export class RequestComponent extends RequestExtend{
 
  title = 'DataBundleClient';
  productForm: FormGroup;
  inputInstance:APIRequest;
  requestInstance:APIRequest;
  currentAccount:APIAccounts;
  usageInstance = new Usage(this.http);
  
  constructor(private fb:FormBuilder, http: HttpClient) {
    super(http, "/api/APIRequests/");
    this.inputInstance = new APIRequest();
    this.requestInstance = new APIRequest();
    this.currentAccount = new APIAccounts();

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
    this.requestInstance = request;
    this.currentAccount = await this.get(request.accountName, "/api/APIAccounts/")
    this.createInputsFromTokens()
  }

  createInputsFromTokens(){
    this.clearTokens();
      var split = this.requestInstance.requestURL.split("{")
      for (var elm of split)
      {
        var tokenName = elm.substring(0,elm.indexOf("}"))
        if (tokenName.length>0)
        {
          var tokenValue = ""
          if(tokenName.includes("APIKEY"))
          {
            tokenValue = this.currentAccount.apiKey
          }
          else if(tokenName.includes("DATE"))
          {
            tokenValue = this.currentAccount.dateFormat
          }
          this.tokens().push(this.newToken(tokenName, tokenValue));
        }        
      }
  }

  async requestSubmit(){
    var requestSuccess = false;
    var submitURL = this.requestInstance.requestURL;
    for(var token of this.productForm.value.tokens)
    {
      submitURL = submitURL.replace("{"+token.tokenName+"}",token.token)
    }

    var usage: APIUsage;
    usage = await this.usageInstance.getAccountUsage(this.currentAccount.accountName) as APIUsage

    //Verify the usage is not maxed out
    if(usage.currentUsage < usage.maxUsage)
    {
        this.http.get<any>(submitURL,{observe: 'response'}).subscribe((res)=>{
          //If we do not find the expected property then the request failed.
          if(res.body[this.requestInstance.expectedProperty]){
            requestSuccess = true;
            this.usageInstance.incrementAccountUsage(usage);
          }
        })
    }
    else
    {
      console.log("You have exceened your quota for " + this.currentAccount.accountName)
    }
    
    if(!requestSuccess){
      console.log("The Request has failed")
    }
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
      this.inputInstance.expectedProperty = singleRequest.expectedProperty;

      this.editMode = true;
      this.editId = singleRequest.requestId;
  }

  clearInput(){
    this.inputInstance.accountName = "";
    this.inputInstance.requestName = "";
    this.inputInstance.requestURL = "";
    this.inputInstance.expectedProperty = "";
  }
}

class APIRequest {
  requestId: string ="00000000-0000-0000-0000-000000000000";
  accountName: string="";
  requestName: string="";
  requestURL: string="";
  expectedProperty: string="";
}

