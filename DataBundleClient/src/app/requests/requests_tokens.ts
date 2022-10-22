import { FormArray, FormBuilder, FormGroup } from "@angular/forms";
import {RequestMetadataMapping } from './requests_metadata'
import {APIRequest} from './requests.component'
import {APIAccounts} from '../accounts/accounts.component'

export class RequestTokens{

    private tokensForm: FormGroup;    

    constructor(private fb:FormBuilder) {
        this.tokensForm = this.fb.group({
            tokens: this.fb.array([]) ,
        });
    }
    
    public getTokensAsFromArray() : FormArray{
        return this.tokensForm.get("tokens") as FormArray
    }

    public getTokensForm(){
        return this.tokensForm
    }
    
    public createInputsFromTokens(requestInstance:APIRequest, currentAccount:APIAccounts, requestMetaData:Map<string,string>){
        this.clearTokens();

        this.parseStringForTokens(requestInstance.requestURL, currentAccount)

        if(Array.from(requestMetaData.keys()).includes(RequestMetadataMapping.REQUEST_HEADER)){
            this.parseStringForTokens(String(requestMetaData.get(RequestMetadataMapping.REQUEST_HEADER)), currentAccount)            
        }

        if(Array.from(requestMetaData.keys()).includes(RequestMetadataMapping.REQUEST_BODY)){
            this.parseStringForTokens(String(requestMetaData.get(RequestMetadataMapping.REQUEST_BODY)), currentAccount)            
        }
    }

    private newToken(tokenName:string, tokenValue:string): FormGroup{
        return this.fb.group({
            tokenName: tokenName,
            token: tokenValue
        })
    }

    private clearTokens(){
        this.getTokensAsFromArray().clear();
    }

    //TODO: Add way to not use hardcoded token names. (APKEY,DATE,...)
    private parseStringForTokens(tokenString:string, currentAccount:APIAccounts){
        var split = tokenString.split("{")
        console.log(split)
            for (var elm of split)
            {
                var tokenName = elm.substring(0, elm.indexOf("}"))
                if (tokenName.length>0)
                {
                    var tokenValue = ""
                    if(tokenName.includes("APIKEY"))
                    {
                        tokenValue = currentAccount.apiKey
                    }
                    else if(tokenName.includes("DATE"))
                    {
                        tokenValue = currentAccount.dateFormat
                    }
                    this.getTokensAsFromArray().push(this.newToken(tokenName, tokenValue));
                }        
            }
    }
}