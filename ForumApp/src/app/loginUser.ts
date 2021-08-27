export class LoginUserModel{
    constructor(public usernameOrEmail: string, 
                public password: string,
                public rememberMe:boolean)
    {
      
    }
  }