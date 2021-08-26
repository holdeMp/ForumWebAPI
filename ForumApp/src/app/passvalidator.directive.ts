import { AbstractControl,FormGroup, FormControl, ValidationErrors, ValidatorFn } from '@angular/forms'
 
import { Observable, of } from 'rxjs';
export function match(controlName: string, checkControlName: string): ValidatorFn {
  return (controls: AbstractControl) => {
    const control = controls.get(controlName);
    const checkControl = controls.get(checkControlName);


    if (controls!==null && control!==null && checkControl!==null && control.value !== checkControl.value) {
      controls.get(checkControlName).setErrors({notmatch: true});
      return { "notmatch": true };
    } else {
      return null;
    }
  }
}
export function passwordValidator():ValidatorFn{ 
  return (control: AbstractControl):{[key : string]:boolean} | null=>{
    
        const password:string=control.value;
        if (!stringContainsNumber(password)) {
          return ({"notnumber": true})
        }
        if(!stringContainsUpperCase(password)){
          return ({"notupper":true})
        }
        if(!stringContainsLowerCase(password)){
          return ({"notlower":true})
        }
        return null;
    
  }
}
function stringContainsLowerCase(_input:string){
  let string1 = String(_input);
  for( let i = 0; i < string1.length; i++){
      if(string1.charAt(i)==string1.charAt(i).toLowerCase() && !isNumber(string1.charAt(i)) && !(string1.charAt(i) === " ") ){
        return true;
      }
  }
  return false;
}

function stringContainsUpperCase(_input:string){
  let string1 = String(_input);
  for( let i = 0; i < string1.length; i++){
      if(string1.charAt(i)==string1.charAt(i).toUpperCase() && !isNumber(string1.charAt(i)) && !(string1.charAt(i) === " ") ){
        return true;
      }
  }
  return false;
}

function stringContainsNumber(_input:string){
  let string1 = String(_input);
  for( let i = 0; i < string1.length; i++){
      if(isNumber(string1.charAt(i)) && !(string1.charAt(i) === " ") ){
        return true;
      }
  }
  return false;
}

function isNumber(value: string | number): boolean
{
   return ((value != null) &&
           (value !== '') &&
           !isNaN(Number(value.toString())));
}