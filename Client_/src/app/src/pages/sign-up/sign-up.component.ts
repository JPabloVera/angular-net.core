import { Component } from '@angular/core';
import {AuthenticationFormComponent} from "../../components/authentication-form/authentication-form.component";
import {ApiService} from "../../../services/api.service";
import {Router} from "@angular/router";
import {Title} from "@angular/platform-browser";

let apiService : ApiService
let routerService : Router
@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [AuthenticationFormComponent],
  template: '<app-authentication-form [clickHandler]="handleRegister" title="Create Account"></app-authentication-form>'
})
export class SignUpComponent {
  constructor(private api_service : ApiService, private router: Router, private titleService : Title) {
    localStorage.removeItem("token")
    localStorage.removeItem("email")
    titleService.setTitle("Register")
    apiService = api_service
    routerService = router;
  }
  handleRegister(email: string, password: string) {
    apiService.Register(email, password).subscribe(response => {
      window.alert("Success")
      routerService.navigate(["/login"])
    }, error => {
      window.alert(`Email already used`)
    })
  }
}
