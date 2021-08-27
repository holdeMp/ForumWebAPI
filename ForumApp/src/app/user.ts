export class RegisterUserModel{
    constructor(public username: string, 
                public email: string, 
                public password: string,
                public passwordConfirm:string)
    { 
    }
}

