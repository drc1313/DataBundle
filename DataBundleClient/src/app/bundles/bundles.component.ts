import {HttpClient, HttpHeaders, HttpResponse} from '@angular/common/http'
import {Component} from '@angular/core'
import {RequestExtend} from '../request-class/request.extend'
import {FormGroup, FormBuilder} from '@angular/forms'
import {firstValueFrom} from 'rxjs'

@Component({
  selector: 'app-root',
  templateUrl: './bundles.component.html',
  styleUrls: ['./bundles.component.css']
})

export class BundlesComponent extends RequestExtend{
 
  title = 'DataBundleClient';
  
  
  constructor(private fb:FormBuilder, http: HttpClient) {
    super(http, "/api/APIBundles/");
    
  
  }
  


  



}



