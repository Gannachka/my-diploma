import { Routes } from "@angular/router";

import { ForgotPasswordComponent } from "./forgot-password/forgot-password.component";
import { LockscreenComponent } from "./lockscreen/lockscreen.component";
import { NotFoundComponent } from "./not-found/not-found.component";
import { ErrorComponent } from "./error/error.component";
import { Signup4Component } from "./signup4/signup4.component";
import { Signin4Component } from "./signin4/signin4.component";

export const SessionsRoutes: Routes = [
  {
    path: "",
    children: [
      {
        path: "completesetup",
        component: Signup4Component,
      },
      {
        path: "signin",
        component: Signin4Component,
      },
      {
        path: "forgot-password",
        component: ForgotPasswordComponent,
      },
      {
        path: "lockscreen",
        component: LockscreenComponent,
      },
      {
        path: "404",
        component: NotFoundComponent,
      },
      {
        path: "error",
        component: ErrorComponent,
      }
    ]
  }
];
