import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestComponent {
  title = 'RequestComponent';
  
  public requests?: APIRequest[];
  constructor(private http: HttpClient) {
  }
}

interface APIRequest {
  requsetId: string;
  accountId: string;
  tequestName: string;
  requestURL: string;
}

