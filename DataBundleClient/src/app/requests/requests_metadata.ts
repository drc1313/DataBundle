export class MetaDataRequestBody
{
  requestid:string="";
  key:string="";
  value:string="";
  constructor(id:string, key:string, value:string) {
    this.requestid = id;
    this.key = key;
    this.value = value;
  }
}

// The internet says this is what I am supposed to do for selection options...
export enum RequestMetadataValues
{
  REQUEST_HEADER = "REQUEST_HEADER",
  REQUEST_BODY = "REQUEST_BODY",
  REQUEST_FORMAT= "REQUEST_FORMAT",
  REQUEST_TYPE= "REQUEST_TYPE",
  REQUEST_EXPECTED_RESPONSE= "REQUEST_EXPECTED_RESPONSE"
}
export const RequestMetadataMapping: Record<RequestMetadataValues, string> = {
  [RequestMetadataValues.REQUEST_HEADER]: RequestMetadataValues.REQUEST_HEADER,
  [RequestMetadataValues.REQUEST_BODY]: RequestMetadataValues.REQUEST_BODY,
  [RequestMetadataValues.REQUEST_FORMAT]: RequestMetadataValues.REQUEST_FORMAT,
  [RequestMetadataValues.REQUEST_TYPE]: RequestMetadataValues.REQUEST_TYPE,
  [RequestMetadataValues.REQUEST_EXPECTED_RESPONSE]: RequestMetadataValues.REQUEST_EXPECTED_RESPONSE
};

export enum RequestType
{
  GET = "GET",
  POST = "POST",
  PUT = "PUT",
  DELETE = "DELETE"    
}