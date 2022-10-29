import { HttpClient, HttpXhrBackend } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

export class Usage {

  static http = new HttpClient(
    new HttpXhrBackend({ 
      build: () => new XMLHttpRequest()
    }));
 

  private static async getAccountUsage(accountName:string) {
    return await firstValueFrom(Usage.http.get<APIUsage>("/api/APIUsages/"+accountName))
  }

  //Return True if account usage is maxed out
  public static async usageIsMaxed(accountName:string)
  {
    var accountUsage = await Usage.getAccountUsage(accountName)
    Usage.usageDateCheck(accountUsage)
    if(accountUsage.currentUsage >= accountUsage.maxUsage)
    {
      return true
    }
    return false
  }
  
  static async updateUsageByObject(usage:APIUsage){
    await firstValueFrom(Usage.http.put<APIUsage>("/api/APIUsages/"+usage.usageID, usage))
  }
  static async createUsageForAccount(accountName:string){
    var usage:APIUsage = {usageID:"00000000-0000-0000-0000-000000000000", accountName, currentUsage:0, maxUsage:100, usageDuration:0, lastCallDate:new Date().toISOString().slice(0, 19)}
    await firstValueFrom(Usage.http.post<APIUsage>("/api/APIUsages/", usage))
  }

  static async usageDateCheck(usage:APIUsage)
  {
    var currentDateTime= new Date(new Date().toISOString().slice(0, 19));
    var usageDate = new Date(usage.lastCallDate)
    //TODO: Add day month year capablilty
    //If the days do not match restart the counter
    if(currentDateTime.getDay()!=usageDate.getDay() && usage.currentUsage != 0)
    {
      usage.currentUsage = 0
      await Usage.updateUsageByObject(usage)
    }
    return usage
  }

  static async incrementAccountUsage(accountName:string, amount:number=1){
    var usage = await this.getAccountUsage(accountName)
    usage.currentUsage += amount
    usage.lastCallDate = new Date().toISOString().slice(0, 19); 
    await Usage.updateUsageByObject(usage)
  }
}

interface APIUsage
{
  usageID: string,
  accountName: string,
  currentUsage: number,
  maxUsage: number,
  usageDuration: number,
  lastCallDate: string
}