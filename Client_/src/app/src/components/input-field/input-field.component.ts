import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-input-field',
  standalone: true,
  imports: [],
  templateUrl: './input-field.component.html',
  styleUrls: ['./input-field.component.css', '../../../styles/fonts.css']
})
export class InputFieldComponent {
      @Input() title =  "Email"
      @Input() placeholder = "example@gmail.com"

      @Input() type : String = "email"

      @Output() inputValueChange = new EventEmitter<string>(); // Event emitter for two-way binding
      inputValue: string = '';

      onInputChange(event: any) {
        this.inputValue = event.target.value;
        this.inputValueChange.emit(this.inputValue);
    }
}
