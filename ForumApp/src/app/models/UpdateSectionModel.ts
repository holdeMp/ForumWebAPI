import { SectionModel } from "./sectionModel";

export class UpdateSectionModel{
    constructor(public sectionId?:number,
                public name?: string,               
                public sectionTitle?: string,
                )
    {
      
    }
  }