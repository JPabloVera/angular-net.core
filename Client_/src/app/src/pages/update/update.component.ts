import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {Title} from "@angular/platform-browser";
import {ApiService} from "../../../services/api.service";

let api_service : ApiService
let router_service : Router
@Component({
  selector: 'app-update',
  standalone: true,
  imports: [],
  templateUrl: './update.component.html',
  styleUrl: './update.component.css'
})
export class UpdateComponent {
    constructor(private router: Router, private titleService : Title, private apiService : ApiService) {

      titleService.setTitle("Update")

      const token = localStorage.getItem("token")
      const email = localStorage.getItem("email")

      api_service = apiService;
      router_service = router;

      if (!token || !email) {
          router.navigate(['/login'])
          window.alert("Please Log In")
          return;
      }

      apiService.RenewToken(token, email).subscribe(response => {
        console.log("response: ",response)
      }, error => {
        window.alert("Please Log In")
        router.navigate(["/login"])
      })
    }

    updateData(){
      const token = localStorage.getItem("token")
      if (!token) return

      api_service.UpdateData(token).subscribe(response => {
        console.log(response)
        router_service.navigate(["/info"])
      }, error => {
        router_service.navigate(["/login"])
      })
    }
}
