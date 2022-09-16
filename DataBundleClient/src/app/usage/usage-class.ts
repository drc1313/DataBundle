import { HttpClient, HttpXhrBackend } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { APIAccounts } from '../accounts/accounts.component';

export class Usage {

  private static usageInstance:Usage;
  private allUsages: APIUsage[] = [];

  private constructor(private http: HttpClient) {}

  public static getInstance()
  {
    if(this.usageInstance==null){      
      this.usageInstance = new Usage(
        new HttpClient(
          new HttpXhrBackend({ 
            build: () => new XMLHttpRequest() 
          })));
    }
    return Usage.usageInstance;
  }

  async getAllUsages() {
    this.allUsages = await firstValueFrom(this.http.get<APIUsage[]>("/api/APIUsages"))
  }
  
  async getAccountUsage(accountName:string)
  {
   await this.getAllUsages();

    if(this.allUsages != null){
      for(var usage of this.allUsages){
        if(usage.accountName == accountName){
          await this.usageDateCheck(usage)
          return usage
        }
      }
    }
    return {};
  }

  async updateUsageByObject(usage:APIUsage){
    await firstValueFrom(this.http.put<APIUsage>("/api/APIUsages/"+usage.usageID, usage))
    this.getAllUsages();
  }
  async createUsageForAccount(accountName:string){
    var usage:APIUsage = {usageID:"00000000-0000-0000-0000-000000000000", accountName, currentUsage:0, maxUsage:0, usageDuration:0, lastCallDate:new Date().toISOString().slice(0, 19)}
    await firstValueFrom(this.http.post<APIUsage>("/api/APIUsages/", usage))
    this.getAllUsages();
  }

 async usageDateCheck(usage:APIUsage)
  {
    var currentDateTime= new Date(new Date().toISOString().slice(0, 19));
    var usageDate = new Date(usage.lastCallDate)
    //TODO: Add day month year capablilty
    //If the days do not match restart the counter
    if(currentDateTime.getDay()!=usageDate.getDay() && usage.currentUsage != 0)
    {
      usage.currentUsage = 0
      await this.updateUsageByObject(usage)
    }
    return usage
  }

  async incrementAccountUsage(usage:APIUsage, amount:number=1){    
    usage.currentUsage += amount
    usage.lastCallDate = new Date().toISOString().slice(0, 19); 
    await this.updateUsageByObject(usage)
  }
}

export interface APIUsage
{
  usageID: string,
  accountName: string,
  currentUsage: number,
  maxUsage: number,
  usageDuration: number,
  lastCallDate: string
}