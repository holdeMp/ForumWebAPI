export class User{
    constructor(
        public id?:string ,
        public firstName?:string ,
        public lastName?:string ,
        public userName?:string ,
        public email?:string, 
        public avatarPath?:string ,
        public phoneNumber?:string ,
        public registrationDate?:Date ,
        public birthDate? : Date,
        public phoneNumberConfirmed? :boolean,
        public confirmedEmail?:boolean,
        public image?:any,
        public roles?:string[],
        public token?:string
    ){
    }

}