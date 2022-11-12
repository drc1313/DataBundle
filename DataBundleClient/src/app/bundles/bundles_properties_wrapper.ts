
export class BundlesPropertiesWrapper {

    currentBundleProperties?:Map<string,[any]>;
  
    private parsedProperties = Array<string>();
    private parsedPropertiesClone = Array<string>();

    
    getParsedProperties()
    {
        return this.parsedProperties
    }
    getParsedPropertiesClone()
    {
        return this.parsedPropertiesClone
    }

    obtainAlteredProperties()
    {
        this.parsedProperties = this.parsedPropertiesClone.slice()
        return this.parsedProperties
    }
    

    obtainPropertiesFromString(propertes:string)
    {
        this.clearPropertyLists()
        if(propertes!=null){
            var bundleProperties = JSON.parse(propertes)

            for(let properties of bundleProperties)
            {
                this.parsedProperties.push(JSON.stringify(properties))
            }
            this.parsedPropertiesClone = this.parsedProperties.slice();
        }
    }
      
    private clearPropertyLists(){
        this.parsedProperties = []
        this.parsedPropertiesClone = []
    }

    deleteBundleProperty(index:number)
    {
        this.parsedPropertiesClone?.splice(index, 1);
        this.parsedProperties = this.parsedPropertiesClone?.slice()
    }

    // getBundlePropertiesSerialized()
    // {
    //     this.clearPropertyLists()
    //     var returnList = []
    //     if (this.currentBundleProperties)
    //     {
    //         for(let key of this.currentBundleProperties.keys())
    //         {
    //         returnList.push(JSON.stringify([key, this.currentBundleProperties.get(key)]))
    //         }
    //     }
    //     return (returnList)
    // }

    addPropertyToBundle()
    {
        this.obtainAlteredProperties()
        if (this.parsedProperties)
        {
            this.parsedProperties.push("[\"value\",[]]")
            this.parsedPropertiesClone = this.parsedProperties.slice()
        }

    }
    

}
 