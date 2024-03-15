import {Component, Input} from '@angular/core';
import {InputFieldComponent} from "../input-field/input-field.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-authentication-form',
  standalone: true,
  imports: [
    InputFieldComponent,
    CommonModule
  ],
  templateUrl: './authentication-form.component.html',
  styleUrls: ['./authentication-form.component.css', '../../../styles/fonts.css']
})
export class AuthenticationFormComponent {
  @Input() title = '';

  input_title_1 = "Email";

  input_placeholder_1 = "example@gmail.com";

  input_title_2 = "Password";

  input_placeholder_2 = "*******";

  email : String = "";

  password : String = ""

  onEmailChange(value: string) {
    this.email = value;
  }

  onPasswordChange(value: string) {
    this.password = value;
  }

  // @ts-ignore
  @Input() clickHandler: Function;

  handleSubmit() {
    if (typeof(this.email) !== 'string' || typeof(this.password) !== 'string')
      return

    // @ts-ignore
    if (this.email.length > 3 && this.password.length > 3) {
      this.clickHandler(this.email, this.password);
    }
  }
}
