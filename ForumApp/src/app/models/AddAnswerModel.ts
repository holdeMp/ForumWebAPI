export class AddAnswerModel{
    constructor( 
                public authorId?: string,
                public content?:string,
                public referenceAnswerId?:string,
                public themeId?:number)
    {
      
    }
}