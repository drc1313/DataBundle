import {HttpClient, HttpHeaders, HttpResponse} from '@angular/common/http'
import {Component, Input} from '@angular/core'
import {RequestExtend} from '../request-class/request.extend'
import {FormGroup, FormBuilder, FormArray} from '@angular/forms'
import {firstValueFrom} from 'rxjs'
import {RequestTokens} from '../requests/requests_tokens'
import * as RequestMetadataImport from '../requests/requests_metadata'
import {APIAccounts} from '../accounts/accounts.component'
import { Usage } from '../usage/usage-class'




@Component({
  selector: 'app-root',
  templateUrl: './bundle-request.component.html',
  styleUrls: ['./bundle-request.component.css']
})



export class BundleRequestComponent extends RequestExtend{
 
  title = 'DataBundleClient';


  //bundleRequestList?: APIBundleRequest[];
  
  //bundledRequests = Array<RequestBundled>();
  bundledRequests = new Map<number, RequestBundled>();


  requestTokens = new RequestTokens(this.fb);


  constructor(private fb:FormBuilder, http: HttpClient) {
    super(http, "/api/APIBundles/");
  }

  async submitBundleRequest(bundle:APIBundleRequest){

    var bundleRequests = await this.get(bundle.bundleId, "api/APIBundleRequests/") as [APIBundleRequest]

    for(var bundleRequest of bundleRequests){
      var request = await this.get(bundleRequest.requestId, "api/APIRequests/") as APIRequest
      var requestMetadata = await this.get(bundleRequest.requestId, "api/APIRequestMetadatas/") as [RequestMetadataImport.MetaDataRequestBody]
      var account =  await this.get(request.accountName, "api/APIAccounts/") as APIAccounts

      //create metadata map
      var metadataMap = new Map<string,string>(); 
      for(var metadata of requestMetadata){
        metadataMap.set(metadata.key,metadata.value)
      }

      var bundledRequest = { bundle: bundleRequest, request: request, metadata: metadataMap, account:account} as RequestBundled
      this.bundledRequests.set(bundledRequest.bundle.requestPriority, bundledRequest)
      //this.bundledRequests.push({ bundle: bundleRequest, request: request, metadata:requestMetadata} as RequestBundled)
    }

    if(this.bundledRequests){
      for(let priority of Array.from(this.bundledRequests.keys()).sort())
      {
        var currentRequestObject = this.bundledRequests.get(priority)
        if(currentRequestObject)
          this.requestTokens.createInputsFromManyTokens(currentRequestObject.request,currentRequestObject.account,currentRequestObject.metadata)
      }
    }
  }

  async requestSubmit(){
    
    var currentRequestObject 
    var requestSuccess = false

    if(this.bundledRequests){
      for(let priority of Array.from(this.bundledRequests.keys()).sort())
      {
        currentRequestObject = this.bundledRequests.get(priority)

        if(currentRequestObject){

          var submitURL = currentRequestObject.request.requestURL;
          var submitBody = currentRequestObject.metadata.get(RequestMetadataImport.RequestMetadataValues.REQUEST_BODY)
          var submitHeader = currentRequestObject.metadata.get(RequestMetadataImport.RequestMetadataValues.REQUEST_HEADER)
          var headers= new HttpHeaders() 

          //Parse the request URL/Header/Body for any tokens that need to be applied. 
          for(var token of this.requestTokens.getTokensForm().value.tokens)
          {
            if(token.tokenName == "APIKEY")
            {
              token.token = currentRequestObject.account.apiKey;
            }
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

          //Verify the usage is not maxed out
          if(!await Usage.usageIsMaxed(currentRequestObject.account.accountName))
          {
            
            if(currentRequestObject.metadata.get(RequestMetadataImport.RequestMetadataValues.REQUEST_TYPE) == RequestMetadataImport.RequestType.GET)
            {
              //Send the request
              this.http.get<any>(submitURL,{headers:headers, observe: 'response'}).subscribe((res)=>{
              //  if(this.validateRequestResponse(res))
              // {
                // Usage.incrementAccountUsage(currentRequestObject.account.accountName, 1)
              // }
              })
            }
            else if(currentRequestObject.metadata.get(RequestMetadataImport.RequestMetadataValues.REQUEST_TYPE) == RequestMetadataImport.RequestType.POST)
            {        
              headers = headers.append("Content-Type", currentRequestObject.metadata.get(RequestMetadataImport.RequestMetadataValues.REQUEST_FORMAT) as string)     
              //Send the request
              this.http.post<any>(submitURL,submitBody,{headers:headers, observe: 'response'}).subscribe((res)=>{
              // if(this.validateRequestResponse(res))
              // {
                // Usage.incrementAccountUsage(this.currentAccount.accountName, 1)
              // }
              })
            }
            else
            {
              console.log("You have exceened your quota for " + currentRequestObject.account.accountName)
            }
          }
        }
      }
    }
    return requestSuccess    
  }

  // private validateRequestResponse(res:HttpResponse<any>)
  // {
  //   var wasExpected = false
  //   //If we do not find the expected property then the request failed.        
  //   if(res.body[this.requestMetaData.get(RequestMetadataValues.REQUEST_EXPECTED_RESPONSE)]){
  //     wasExpected = true
  //   }
  //   else
  //   {
  //     //If it was not found in the body attempt to iterate though elements of the body
  //     for(var elm of res.body)
  //     {
  //       if(elm[this.requestMetaData.get(RequestMetadataValues.REQUEST_EXPECTED_RESPONSE)]){
  //         wasExpected = true
  //         break;
  //       }
  //     }
  //   } 
  //   return wasExpected
  // }


}

class APIBundle {  
  bundleId:string ="00000000-0000-0000-0000-000000000000";
  bundleName: string="";
  bundleDescription: string="";
}

class APIBundleRequest {  
  bundleId:string ="00000000-0000-0000-0000-000000000000";
  requestId:string ="00000000-0000-0000-0000-000000000000";
  requestProperties: string="";
  requestPriority: number=0;
}

class APIRequest {
  requestId: string ="00000000-0000-0000-0000-000000000000";
  accountName: string="";
  requestName: string="";
  requestURL: string="";
}

interface RequestBundled{
  bundle: APIBundleRequest;
  request: APIRequest;
  metadata: Map<string,string>;
  account: APIAccounts;
}

