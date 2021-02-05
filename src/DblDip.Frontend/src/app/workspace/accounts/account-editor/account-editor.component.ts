import { Component, forwardRef } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormControl, FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR, ValidationErrors, Validator, Validators } from '@angular/forms';
import { Account } from '../account';

@Component({
  selector: 'app-account-editor',
  templateUrl: './account-editor.component.html',
  styleUrls: ['./account-editor.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => AccountEditorComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => AccountEditorComponent),
      multi: true
    }       
  ]
})
export class AccountEditorComponent implements ControlValueAccessor,  Validator  {
  validate(control: AbstractControl): ValidationErrors {
    const error = { validate: true };
      
    if (!control.value && !control.pristine) {
      return error;
    }
    
    return null as any;
  }
  
  public form = new FormGroup({
    accountId: new FormControl(),
    name: new FormControl(null, [Validators.required]),
  });
  
  writeValue(account: Account): void {   
    this.form.patchValue(account || {}, { emitEvent: false });
  }

  registerOnChange(fn: any): void {
    this.form.valueChanges.subscribe(fn);
  }
  
  onTouched = () => {
  
  };

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.form.disable() : this.form.enable();
  }
}
