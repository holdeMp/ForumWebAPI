import { SectionTitle } from "./sectionTitle";

export class Section{
    constructor( 
                public sectionId? : number,
                public name? : string,
                public sectionTitleId? : string,
                public sectionTitle? : SectionTitle,
                public subSectionsIds? : number[])
    {
      
    }
}