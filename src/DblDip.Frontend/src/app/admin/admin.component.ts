import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../core/auth.service';
import { RedirectService } from '../core/redirect.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  constructor(public router: Router, public authService: AuthService, public redirectService: RedirectService) { }

  ngOnInit(): void {
  }

  logout() {
    this.authService.logout();
    this.redirectService.redirectToLogin();
  }
}
