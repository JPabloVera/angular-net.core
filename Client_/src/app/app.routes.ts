import { Routes } from '@angular/router';
import {SignInComponent} from "./src/pages/sign-in/sign-in.component";
import {SignUpComponent} from "./src/pages/sign-up/sign-up.component";
import {InfoComponent} from "./src/pages/info/info.component";
import {UpdateComponent} from "./src/pages/update/update.component";

export const routes: Routes = [
  {path: 'login', component: SignInComponent},
  {path: 'register', component: SignUpComponent},
  {path: 'info', component: InfoComponent},
  {path: 'update', component: UpdateComponent}
];
