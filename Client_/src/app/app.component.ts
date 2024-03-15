import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {ApiService} from "./services/api.service";
import {HttpClientModule} from "@angular/common/http";
import {NavComponent} from "./src/components/nav/nav.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NavComponent
  ],
  providers: [ApiService],
  templateUrl: 'app.component.html',
})
export class AppComponent { }
