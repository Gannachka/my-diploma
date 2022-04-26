import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Validators, FormControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { LocalStoreService } from '../../../shared/services/local-store.service';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.css']
})
export class ProfileSettingsComponent implements OnInit {
  settingsForm: FormGroup;
  doctorSettingsForm: FormGroup;
  errorMsg = '';
  public uploader: FileUploader = new FileUploader({ url: 'upload_url' });
  public hasBaseDropZoneOver: boolean = false;
  constructor(private fb: FormBuilder,
    private http: HttpClient,
    private snack: MatSnackBar,
    private router: Router,
    private ls: LocalStoreService  ) { }

  ngOnInit() {
    this.settingsForm = this.fb.group(
      {
        fullName: ["", Validators.required],
        age: ["", Validators.required],
        email: ["", [Validators.required, Validators.email]],
      });

    this.doctorSettingsForm = this.fb.group(
      {
        fullName: ["", Validators.required],
        workExperience: ["", Validators.required],
        email: ["", [Validators.required, Validators.email]],
        phoneNumber: ["", [Validators.required]]
      });
  }
  onSubmit() {
    if (!this.settingsForm.invalid) {
      return this.http.put('/api/pacient',
        {
          fullName: this.settingsForm.value.fullName,
          email: this.settingsForm.value.email,
          age: this.settingsForm.value.age
        }).subscribe(response => {
          
        }, err => {

        })
      };
  }

  onDoctorSubmit() {
    if (!this.doctorSettingsForm.invalid) {
      return this.http.put('/api/doctor',
        {
          fullName: this.doctorSettingsForm.value.fullName,
          workExperience: this.doctorSettingsForm.value.workExperience,
          email: this.doctorSettingsForm.value.email,
          phoneNumber: this.doctorSettingsForm.value.phoneNumber,
        }).subscribe(response => {

        }, err => {

        })
    };
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  getUserRole(): string {
    return this.ls.getItem('EGRET_USER').role;
  }
}
