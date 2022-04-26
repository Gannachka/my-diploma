import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { CustomValidators } from 'ngx-custom-validators';
import { egretAnimations } from 'app/shared/animations/egret-animations';
import { JwtAuthService } from '../../../shared/services/auth/jwt-auth.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-signin4',
  templateUrl: './signin4.component.html',
  styleUrls: ['./signin4.component.scss'],
  animations: egretAnimations
})
export class Signin4Component implements OnInit {

  signupForm: FormGroup;
  errorMsg = '';

  loading: boolean;
  constructor(
    private fb: FormBuilder,
    private jwtAuth: JwtAuthService,
    private snack: MatSnackBar,
    private http: HttpClient,
    private router: Router) {
    if (jwtAuth.isLoggedIn()) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {

    const password = new FormControl('', Validators.required);
    const confirmPassword = new FormControl('', CustomValidators.equalTo(password));

    this.signupForm = this.fb.group(
      {
        email: ["",[Validators.required,Validators.email]],
        password: password,
        agreed: [false,Validators.required]
      }
    );
  }

  onSubmit() {
    if (!this.signupForm.invalid) {
      this.jwtAuth.signin(this.signupForm.value.email, this.signupForm.value.password)
        .subscribe(response => {
          this.router.navigateByUrl(this.jwtAuth.return);
        }, err => {

          this.errorMsg = 'Uncorrect e-mail or password';
          this.snack.open(err.message, 'OK', { duration: 4000 })
        })
      console.log('SUBMIT FORM VALUE', this.signupForm.value);
    }
  }

}
