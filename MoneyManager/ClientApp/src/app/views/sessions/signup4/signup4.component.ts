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
        email: ["", [Validators.required, Validators.email]],
        password: password,
        confirmPassword: confirmPassword
      }
    );
  }

  onSubmit() {
    if (!this.signupForm.invalid) {
      return this.http.post('/api/setupcomplete',
        {
          email: this.signupForm.value.email,
          password: this.signupForm.value.password
        })
        .subscribe(response => {
          this.router.navigateByUrl('/sessions/signin');
        }, err => {
          this.errorMsg = err.message;
          if (err.message === 'Пользователь не найден') {
            this.snack.open('Пользователь не найден', 'Error', { duration: 4000 })
          }
          else {
            this.snack.open('uncorrect e-mail or password', 'Error', { duration: 4000 })
          }         
        })
    };
  }
}
