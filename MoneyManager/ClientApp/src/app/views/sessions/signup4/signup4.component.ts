import { Component, OnInit } from '@angular/core';
import { Validators, FormControl, FormBuilder, FormGroup } from '@angular/forms';
import { CustomValidators } from 'ngx-custom-validators';
import { egretAnimations } from 'app/shared/animations/egret-animations';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup4',
  templateUrl: './signup4.component.html',
  styleUrls: ['./signup4.component.scss'],
  animations: egretAnimations
})
export class Signup4Component implements OnInit {

  signupForm: FormGroup;
  errorMsg = '';

  constructor(private fb: FormBuilder,
    private http: HttpClient,
    private snack: MatSnackBar,
    private router: Router) { }

  ngOnInit() {

    const password = new FormControl('', Validators.required);
    const confirmPassword = new FormControl('', CustomValidators.equalTo(password));

    this.signupForm = this.fb.group(
      {
        firstName: ["", Validators.required],
        lastName: ["", Validators.required],
        username: ["", Validators.required],
        age: ["", Validators.required],
        email: ["", [Validators.required, Validators.email]],
        password: password,
        agreed: [false, Validators.required]
      }
    );
  }

  onSubmit() {
    if (!this.signupForm.invalid) {
      return this.http.post('/api/register',
        {
          firstName: this.signupForm.value.firstName,
          lastName: this.signupForm.value.lastName,
          username: this.signupForm.value.username,
          email: this.signupForm.value.email,
          password: this.signupForm.value.password
        })
        .subscribe(response => {
          this.router.navigateByUrl('/sessions/signin');
        }, err => {
          this.errorMsg = err.message;
          this.snack.open('uncorrect e-mail or password', 'Error', { duration: 4000 })
        })
    };
  }
}
