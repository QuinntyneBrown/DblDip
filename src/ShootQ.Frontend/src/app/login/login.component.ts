import { Component, Output, EventEmitter, Renderer2, AfterContentInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements AfterContentInit {

  public username: string;
  public password: string;

  public form = new FormGroup({
    username: new FormControl(this.username, [Validators.required]),
    password: new FormControl(this.password, [Validators.required])
  });
  
  @Output() public tryToLogin: EventEmitter<{ username: string, password: string }> = new EventEmitter();

  constructor(private renderer: Renderer2) { }

  ngAfterContentInit(): void { this.renderer.selectRootElement('#username').focus(); }
}
