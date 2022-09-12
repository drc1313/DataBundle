import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';


export class Usage {
 
  title = 'DataBundleClient';

  public allUsages: APIUsage[] = [];
  
  constructor(private http: HttpClient) {
    this.getAllUsages();
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

