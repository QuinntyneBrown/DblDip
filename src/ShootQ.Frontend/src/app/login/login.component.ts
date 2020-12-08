import { Component, Output, EventEmitter, Renderer2, AfterContentInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements AfterContentInit {

  public form = new FormGroup({
    username: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required])
  });
  
  @Output() public tryToLogin: EventEmitter<{ username: string, password: string }> = new EventEmitter();

  constructor(private renderer: Renderer2) { }

  ngAfterContentInit(): void { this.renderer.selectRootElement('#username').focus(); }
}
