import { Component } from '@angular/core';
import { ForkliftCommandsService } from './forklift-commands.service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFormModule } from 'ng-zorro-antd/form';

@Component({
  selector: 'app-forklift-commands',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NzInputModule,
    NzButtonModule,
    NzFormModule,
    ReactiveFormsModule
    
  ],
  templateUrl: './forklift-commands.component.html',
  styleUrl: './forklift-commands.component.css'
})
export class ForkliftCommandsComponent {
  commandInput: string = '';
  commandActions: string[] = [];
  validateForm: FormGroup;
  showComp: boolean = false;
  hasError: boolean = false;
  constructor(private forkliftCommandsService: ForkliftCommandsService, private fb: NonNullableFormBuilder) {
    this.validateForm = this.fb.group({
      command: [undefined, [Validators.required]]
    });
    this.showComp = true;
    this.hasError = false;
  }


  submitCommand(): void {
    this.hasError = false;
    if (this.validateForm.valid) {
      this.forkliftCommandsService.executeCommands(this.validateForm.value)
        .subscribe((response: string[]) => {
          this.commandActions = response;
        }, (error) => {
          console.error('Error executing commands', error);
        });
    }
    else
      this.hasError = true;
  }
}
