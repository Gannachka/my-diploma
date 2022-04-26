import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Validators, FormControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.css']
})
export class ProfileSettingsComponent implements OnInit {
  settingsForm: FormGroup;
  errorMsg = '';
  public uploader: FileUploader = new FileUploader({ url: 'upload_url' });
  public hasBaseDropZoneOver: boolean = false;
  constructor(private fb: FormBuilder,
    private http: HttpClient,
    private snack: MatSnackBar,
    private router: Router  ) { }

  ngOnInit() {
    this.settingsForm = this.fb.group(
      {
        firstName: ["", Validators.required],
        lastName: ["", Validators.required],
        age: ["", Validators.required],
        email: ["", [Validators.required, Validators.email]],
      });
  }
  onSubmit() {
    if (!this.settingsForm.invalid) {
      return this.http.put('/api/update',
        {
          firstName: this.settingsForm.value.firstName,
          lastName: this.settingsForm.value.lastName,
          email: this.settingsForm.value.email,
          age: this.settingsForm.value.age
        }).subscribe(response => {
          
        }, err => {

        })
      };
  }
  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }
}
