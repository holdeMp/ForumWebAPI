import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Toast, ToastrService } from 'ngx-toastr';
import { LoginService } from 'src/app/Services/login.service';

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
    email:new FormControl(''),
    firstName:new FormControl(''),
    lastName:new FormControl(''),
    birthDate:new FormControl(''),
    imageFile:new FormControl(),
    phoneNumber:new FormControl('')
  });
  constructor(private loginService:LoginService,private toastr: ToastrService,
    private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.user=this.loginService.user;
  }
  UpdateProfile(userToUpdate:any){
    var formData: any = new FormData();
    formData.append('Id',userToUpdate.get('id').value);
    formData.append('Email',userToUpdate.get('email').value);
    formData.append('FirstName',userToUpdate.get('firstName').value);
    formData.append('LastName',userToUpdate.get('lastName').value);
    formData.append('BirthDate',userToUpdate.get('birthDate').value);
    formData.append('ImageFile',userToUpdate.get('imageFile').value);
    formData.append('PhoneNumber',userToUpdate.get('phoneNumber').value);
    formData.append('AvatarName',userToUpdate.get('imageFile').value.name);
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
    this.updateUserForm.get('imageFile').updateValueAndValidity()
    document.getElementById('output').setAttribute('src',URL.createObjectURL(this.image));
    
  }
}
