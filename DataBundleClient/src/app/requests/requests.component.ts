import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { RequestExtend } from '../request-class/request.extend';
import { FormGroup, FormArray, FormBuilder} from '@angular/forms'
import {Usage, APIUsage} from '../usage/usage-class'
import {APIAccounts} from '../accounts/accounts.component'
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})

export class RequestComponent extends RequestExtend{
 
  title = 'DataBundleClient';
  productForm: FormGroup;
  metadataForm: FormGroup;
  inputInstance:APIRequest;
  requestInstance:APIRequest;
  requestMetaData = new Map();
  currentAccount:APIAccounts;
  usageInstance = Usage.getInstance();
  public RequestMetadataMapping = RequestMetadataMapping
  public requestMetadataValues = Object.values(RequestMetadataValues)
  
  constructor(private fb:FormBuilder, http: HttpClient) {
    super(http, "/api/APIRequests/");
    this.inputInstance = new APIRequest();
    this.requestInstance = new APIRequest();
    this.currentAccount = new APIAccounts();

    this.productForm = this.fb.group({
      tokens: this.fb.array([]) ,
    });
    
    this.metadataForm = this.fb.group({
       metadataName: [''],
       metadataValue: ['']
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
    await this.setRequestInstance(request)
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

      if(Array.from(this.requestMetaData.keys()).includes(RequestMetadataMapping.REQUEST_BODY)){
        var split = String(this.requestMetaData.get(RequestMetadataMapping.REQUEST_BODY)).split("{")
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
  }

  //TODO: AFTER SETTING EXPECTED RESPONSE, THE REQUEST WILL NOT TRACK USAGE UNTIL PAGE GETS REFRESHED
  //POSTs user defined request metadata
  async requstMetadataSubmit()
  {
    var requestURL = '/api/APIRequestMetadatas';
    var requestbody = new MetaDataRequestBody(this.requestInstance.requestId, this.metadataForm.value.metadataName,this.metadataForm.value.metadataValue)
    this.http.post<any>(requestURL,requestbody).subscribe((res)=>{
    })
  }

  async requestSubmit(){
    
    var submitURL = this.requestInstance.requestURL;
    var submitBody = this.requestMetaData.get(RequestMetadataValues.REQUEST_BODY)
   
    for(var token of this.productForm.value.tokens)
    {
      submitURL = submitURL.replace("{"+token.tokenName+"}",token.token)
      if(submitBody)
      {
        submitBody = submitBody.replace("{"+token.tokenName+"}",token.token)
      }
    }

    var usage: APIUsage;
    usage = await this.usageInstance.getAccountUsage(this.currentAccount.accountName) as APIUsage

    //Verify the usage is not maxed out
    if(usage.currentUsage < usage.maxUsage)
    {      
      //Check for any headers configured with the account and apply them to the request
      var headers= new HttpHeaders() 
     
      if(Array.from(this.requestMetaData.keys()).includes(RequestMetadataMapping.REQUEST_HEADER))
      {
        var headerStr = this.requestMetaData.get(RequestMetadataValues.REQUEST_HEADER)
        
        if(headerStr && headerStr.length > 0){

          headerStr = headerStr.replace("{APIKEY}", this.currentAccount.apiKey)

          var headersList = headerStr.split(",")

          for(var header of headersList)
          {
            var splitHeader = header.split(":")
            console.log(splitHeader)
            headers = headers.append(splitHeader[0].trim(), splitHeader[1].trim())
          }
        }
      }
      if(this.requestMetaData.get(RequestMetadataValues.REQUEST_TYPE) == "GET")
      {
        //Send the request
        this.http.get<any>(submitURL,{headers:headers, observe: 'response'}).subscribe((res)=>{
          
          //If we do not find the expected property then the request failed.        
          if(res.body[this.requestMetaData.get(RequestMetadataValues.REQUEST_EXPECTED_RESPONSE)]){
            
            this.usageInstance.incrementAccountUsage(usage);
          }
          else
          {
            //If it was not found in the body attempt to iterate though elements of the body
            for(var elm of res.body)
            {
              if(elm[this.requestMetaData.get(RequestMetadataValues.REQUEST_EXPECTED_RESPONSE)]){
                
                this.usageInstance.incrementAccountUsage(usage);
                break;
              }
            }
          } 
        })
      }
      //TODO: WILL NOT SEARCH WITHIN RESPONSE (IF LISTED) FOR REQUEST_EXPECTED_RESPONSE LIKE THE GET ROUTE DOES
      else if(this.requestMetaData.get(RequestMetadataValues.REQUEST_TYPE) == "POST")
      {        
        headers = headers.append("Content-Type",this.requestMetaData.get(RequestMetadataValues.REQUEST_FORMAT))     
        //Send the request
        this.http.post<any>(submitURL,submitBody,{headers:headers, observe: 'response'}).subscribe((res)=>{
          //If we do not find the expected property then the request failed.          
          if(res.body[this.requestMetaData.get(RequestMetadataValues.REQUEST_EXPECTED_RESPONSE)]){            
            this.usageInstance.incrementAccountUsage(usage);            
          }
        })
      }
      else
      {
        console.log("You have exceened your quota for " + this.currentAccount.accountName)
      }
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

  async setRequestInstance(selectedRequest:APIRequest)
  {
    this.requestMetaData.clear()
    
    this.requestInstance = selectedRequest;
    var res = await firstValueFrom(this.http.get<any>("/api/APIRequestMetadatas/"+this.requestInstance.requestId))
    for(var elm of res)
    {
      this.requestMetaData.set(elm.key, elm.value)
    }
  }

  async requestPopulateInput(singleRequest: APIRequest){
      await this.setRequestInstance(singleRequest)
      this.inputInstance.accountName = this.requestInstance.accountName;
      this.inputInstance.requestName = this.requestInstance.requestName;
      this.inputInstance.requestURL = this.requestInstance.requestURL;

      this.editMode = true;
      this.editId = this.requestInstance.requestId;
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
  requestName: string="";
  requestURL: string="";
}

class MetaDataRequestBody
{
  requestid:string="";
  key:string="";
  value:string="";
  constructor(id:string, key:string, value:string) {
    this.requestid = id;
    this.key = key;
    this.value = value;
  }
}

// The internet says this is what I am supposed to do for selection options...
export enum RequestMetadataValues
{
  REQUEST_HEADER = "REQUEST_HEADER",
  REQUEST_BODY = "REQUEST_BODY",
  REQUEST_FORMAT= "REQUEST_FORMAT",
  REQUEST_TYPE= "REQUEST_TYPE",
  REQUEST_EXPECTED_RESPONSE= "REQUEST_EXPECTED_RESPONSE"
}
export const RequestMetadataMapping: Record<RequestMetadataValues, string> = {
  [RequestMetadataValues.REQUEST_HEADER]: RequestMetadataValues.REQUEST_HEADER,
  [RequestMetadataValues.REQUEST_BODY]: RequestMetadataValues.REQUEST_BODY,
  [RequestMetadataValues.REQUEST_FORMAT]: RequestMetadataValues.REQUEST_FORMAT,
  [RequestMetadataValues.REQUEST_TYPE]: RequestMetadataValues.REQUEST_TYPE,
  [RequestMetadataValues.REQUEST_EXPECTED_RESPONSE]: RequestMetadataValues.REQUEST_EXPECTED_RESPONSE
};

// enum RequestType
// {
//   GET,
//   POST,
//   PUT,
//   DELETE    
// }
