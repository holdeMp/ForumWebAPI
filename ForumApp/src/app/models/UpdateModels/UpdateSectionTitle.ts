import { Section } from "../RequestModels/section";

export class UpdateSectionTitle{
    constructor(public id? : number,
                public name?: string,               
                public sections?: Section[],
                )
    {
      
    }
  }