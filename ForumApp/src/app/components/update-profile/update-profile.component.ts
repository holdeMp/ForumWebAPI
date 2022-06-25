import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Toast, ToastrService } from 'ngx-toastr';
import { ImageService } from 'src/app/services/image.service';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrls: ['./update-profile.component.css']
})
export class UpdateProfileComponent implements OnInit {
  user:any;
  image:any;

  headerDict = {
    'Content-Type': 'application/json',
    'Accept': 'application/json',
    'Access-Control-Allow-Headers': 'Content-Type',
  }
  updateUserForm = new FormGroup({
    id:new FormControl(this.loginService.user.id),
    email:new FormControl(this.loginService.user.email),
    firstName:new FormControl(this.loginService.user.firstName),
    lastName:new FormControl(this.loginService.user.lastName),
    birthDate:new FormControl(this.loginService.user.birthDate),
    imageFile:new FormControl(),
    phoneNumber:new FormControl(this.loginService.user.phoneNumber)
  });
  constructor(public loginService:LoginService,private toastr: ToastrService,
    private httpClient: HttpClient , private imageService:ImageService) { }

  ngOnInit(): void {
    this.user = this.loginService.user;
    this.getImageFromService();
  }
  createImageFromBlob(imageBlob: Blob) {
       document.getElementById('output').setAttribute('src',URL.createObjectURL(imageBlob));
  }
  getImageFromService() {
    this.imageService.getImage('https://localhost:44381/api/Images/' + this.user.id).subscribe(data => {
      this.createImageFromBlob(data);      
    }, error => {
      
      console.log(error);
    });
}
public dateToString = (date) => `${date.year}-${date.month}-${date.day}`;
  UpdateProfile(userToUpdate:any){
    var formData: any = new FormData();
    formData.append('Id',userToUpdate.get('id').value);
    formData.append('Email',userToUpdate.get('email').value);
    formData.append('FirstName',userToUpdate.get('firstName').value);
    formData.append('LastName',userToUpdate.get('lastName').value);
    formData.append('BirthDate', this.dateToString(userToUpdate.get('birthDate').value));
    formData.append('ImageFile',userToUpdate.get('imageFile').value);
    formData.append('PhoneNumber',userToUpdate.get('phoneNumber').value);
     if(userToUpdate.get('imageFile').value){ 
      formData.append('AvatarName',userToUpdate.get('imageFile').value.name);
     }
    this.httpClient.put('https://localhost:44381/api/Register', formData).subscribe(
      (res) => this.toastr.success('Succesfully updated profile','Successful update'),
      (err) => this.toastr.error(err)
    );
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files[0];
    this.image = file;
    if(file.type!='image/png' && file.type!='image/jpg' && file.type!='image/jpeg' ){
      this.toastr.error('Only png , jpg and jpeg files are acceptable','Incorrect file type' );
      return;
    }
    this.updateUserForm.patchValue({
      imageFile: file
    });
    this.updateUserForm.get('imageFile').updateValueAndValidity();
    
    document.getElementById('output').setAttribute('src',URL.createObjectURL(this.image));
    
  }
}
