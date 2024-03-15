import { Component } from '@angular/core';
import {AuthenticationFormComponent} from "../../components/authentication-form/authentication-form.component";
import {ApiService} from "../../../services/api.service";
import {Router} from "@angular/router";
import {Title} from "@angular/platform-browser";

let apiService : ApiService
let routerService : Router
@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [AuthenticationFormComponent],
  template: '<app-authentication-form title="Login" [clickHandler]="handleLogin"></app-authentication-form>'
})
export class SignInComponent {
    constructor(private api_service : ApiService, private router: Router, private titleService : Title) {
      titleService.setTitle("Login")
      localStorage.removeItem("token")
      localStorage.removeItem("email")
      apiService = api_service
      routerService = router
    }
    handleLogin(email: string, password: string) {
      apiService.Login(email, password).subscribe(response => {
        console.log('Login Response:', response);
        if (!response?.token || !response?.email) window.alert("Failure")
        else {
          localStorage.setItem("token", response.token)
          localStorage.setItem("email", response.email)
          routerService.navigate(['/info'])
        }
      }, error => {
        window.alert(`Email already used`)
      })
    }
}
