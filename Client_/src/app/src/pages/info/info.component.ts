import { Component } from '@angular/core';
import {ApiService} from "../../../services/api.service";
import { Title } from '@angular/platform-browser';


@Component({
  selector: 'app-info',
  standalone: true,
  imports: [],
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css', '../../../styles/fonts.css']
})
export class InfoComponent {

    totalSupply : string = ""
    name : string = ""
    circulatingSupply : string = ""
    constructor(private apiService : ApiService, private titleService : Title) {
      titleService.setTitle("Info")
      apiService.GetData().subscribe(response => {
        this.totalSupply = response.totalSupply;
        this.name = response.name;
        this.circulatingSupply = response.circulatingSupply
        console.log('Info Response:', response);
      }, error => {
        window.alert(`Server Error`)
      })
    }
}
