import {HttpClient, HttpHeaders} from '@angular/common/http'
import {Component} from '@angular/core'
import {RequestExtend} from '../request-class/request.extend'
import {FormGroup, FormBuilder} from '@angular/forms'
import {Usage, APIUsage} from '../usage/usage-class'
import {APIAccounts} from '../accounts/accounts.component'
import {RequestMetadataValues,RequestMetadataMapping, MetaDataRequestBody, RequestType} from './requests_metadata'
import {firstValueFrom} from 'rxjs'
import {RequestTokens} from './requests_tokens'

@Component({
  selector: 'app-root',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})

export class RequestComponent extends RequestExtend{
 
  title = 'DataBundleClient';
  metadataForm: FormGroup;
  inputInstance:APIRequest;
  requestInstance:APIRequest;
  requestMetaData = new Map();
  currentAccount:APIAccounts;
  usageInstance = Usage.getInstance();
  requestTokens:RequestTokens;

  public RequestMetadataMapping = RequestMetadataMapping
  public requestMetadataValues = Object.values(RequestMetadataValues)
  
  constructor(private fb:FormBuilder, http: HttpClient) {
    super(http, "/api/APIRequests/");
    this.inputInstance = new APIRequest();
    this.requestInstance = new APIRequest();
    this.currentAccount = new APIAccounts();
    this.requestTokens = new RequestTokens(fb);
    
    this.metadataForm = this.fb.group({
       metadataName: [''],
       metadataValue: ['']
    });
  }
  
  async testRequest(request:APIRequest){
    await this.setRequestInstance(request)
    this.currentAccount = await this.get(request.accountName, "/api/APIAccounts/")
    this.requestTokens.createInputsFromTokens(this.requestInstance,this.currentAccount,this.requestMetaData)
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
    var submitHeader = this.requestMetaData.get(RequestMetadataValues.REQUEST_HEADER)
    var headers= new HttpHeaders() 

    //Parse the request URL/Header/Body for any tokens that need to be applied. 
    for(var token of this.requestTokens.getTokensForm().value.tokens)
    {
      submitURL = submitURL.replace("{"+token.tokenName+"}",token.token)
      if(submitBody)
      {
        submitBody = submitBody.replace("{"+token.tokenName+"}",token.token)
      }
      if(submitHeader)
      {
        submitHeader = submitHeader.replace("{"+token.tokenName+"}",token.token)
      }
    }

    //Construct the request header if one exists
    if(submitHeader)
    {
      var headersList = submitHeader.split(",")
    
      for(var header of headersList)
      {
        var splitHeader = header.split(":")        
        headers = headers.append(splitHeader[0].trim(), splitHeader[1].trim())
      }
    }

    var usage: APIUsage;
    usage = await this.usageInstance.getAccountUsage(this.currentAccount.accountName) as APIUsage

    //Verify the usage is not maxed out
    if(usage.currentUsage < usage.maxUsage)
    {      
      
      if(this.requestMetaData.get(RequestMetadataValues.REQUEST_TYPE) == RequestType.GET)
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
      else if(this.requestMetaData.get(RequestMetadataValues.REQUEST_TYPE) == RequestType.POST)
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

export class APIRequest {
  requestId: string ="00000000-0000-0000-0000-000000000000";
  accountName: string="";
  requestName: string="";
  requestURL: string="";
}


