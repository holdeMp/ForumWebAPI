import { SectionModel } from "../sectionModel";

export class UpdateSectionModel{
    constructor(public sectionModel: SectionModel, 
                public sectionTitle?: string,
                )
    {
      
    }
  }