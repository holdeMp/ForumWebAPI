import { AddAnswerModel } from "./AddAnswerModel";

export class AddThemeModel{
    constructor( 
                public name?: string,
                public subSectionId?:string,
                public addAnswerModel?:AddAnswerModel)
    {
      
    }
}