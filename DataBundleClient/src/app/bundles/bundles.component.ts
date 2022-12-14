import {HttpClient, HttpHeaders, HttpResponse} from '@angular/common/http'
import {Component, Input} from '@angular/core'
import {RequestExtend} from '../request-class/request.extend'
import {BundlesPropertiesWrapper} from './bundles_properties_wrapper'
import {FormGroup, FormBuilder, FormArray} from '@angular/forms'
import {firstValueFrom} from 'rxjs'

@Component({
  selector: 'app-root',
  templateUrl: './bundles.component.html',
  styleUrls: ['./bundles.component.css']
})



export class BundlesComponent extends RequestExtend{
 
  title = 'DataBundleClient';
  inputBundleInstance:APIBundle= new APIBundle();
  currentBundleInstance?:APIBundle;
  inputBundleRequestInstance:APIBundleRequst= new APIBundleRequst();


  properyWrapper:BundlesPropertiesWrapper = new BundlesPropertiesWrapper();




  bundleRequests?: APIBundleRequst[];

  bundleRequestEditMode=false;

  constructor(http: HttpClient, private fb:FormBuilder) {
    super(http, "/api/APIBundles/");
  }



  loadBundleRequestProperties(bundle:APIBundleRequst)
  {
    this.editMode = true;
    this.editId = bundle.bundleId;
    this.inputBundleRequestInstance.requestId = bundle.requestId;
    this.inputBundleRequestInstance.requestProperties = bundle.requestProperties;
    this.inputBundleRequestInstance.requestPriority = bundle.requestPriority;
    this.properyWrapper.obtainPropertiesFromString(bundle.requestProperties);


    
  }

  async applyPropertiesToBundleRequest(){
    var tempList = []

    for(var property of this.properyWrapper.obtainAlteredProperties())
    {
      tempList.push((JSON.parse(property)))
    }
    this.inputBundleRequestInstance.requestProperties = JSON.stringify(tempList);
    await this.submit(this.inputBundleRequestInstance,"/api/APIBundleRequests/")
  }

  //Functions for request creation/editing 
  async submit(body:any,path:string="/api/APIBundles/"){
    if(!this.editMode)
    {
      await this.create(body, path)
    }
    else
    {
      await this.update(this.editId, body, path);
      this.editMode = false;
    }
    if(path == "/api/APIBundles/")
    {
      this.getAll();
      this.clearInput();
    }
    else if(path == "/api/APIBundleRequests/")
    {
      if(this.currentBundleInstance)
      this.getBundleRequests(this.currentBundleInstance)
    }
  }

  async getBundleRequests(bundle:APIBundle){
    this.currentBundleInstance = bundle
    this.bundleRequests = await this.get(bundle.bundleId,"/api/APIBundleRequests/")
    this.inputBundleRequestInstance.bundleId = bundle.bundleId
  }

  clearInput(){
    this.inputBundleInstance.bundleName = "";
    this.inputBundleInstance.bundleDescription = "";
  }

  async bundlePopulateInput(bundle: APIBundle){
    this.inputBundleInstance.bundleName = bundle.bundleName;
    this.inputBundleInstance.bundleDescription = bundle.bundleDescription;

    this.editMode = true;
    this.editId = bundle.bundleId;
  }   

  async bundleRequstPopulateInput(bundleRequest: APIBundleRequst){
    this.inputBundleRequestInstance.requestId = bundleRequest.requestId;
    this.inputBundleRequestInstance.requestProperties = bundleRequest.requestProperties;
    this.inputBundleRequestInstance.requestPriority = bundleRequest.requestPriority;

    this.editMode = true;
    this.editId = bundleRequest.bundleId;
  }

}

class APIBundle {  
  bundleId:string ="00000000-0000-0000-0000-000000000000";
  bundleName: string="";
  bundleDescription: string="";
}

class APIBundleRequst {  
  bundleId:string ="00000000-0000-0000-0000-000000000000";
  requestId:string ="00000000-0000-0000-0000-000000000000";
  requestProperties: string="";
  requestPriority: number=0;
}

