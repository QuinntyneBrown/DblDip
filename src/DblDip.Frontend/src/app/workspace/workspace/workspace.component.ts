import { Component, OnInit } from '@angular/core';
import { AuthService, RedirectService } from '@core';

@Component({
  selector: 'app-workspace',
  templateUrl: './workspace.component.html',
  styleUrls: ['./workspace.component.scss']
})
export class WorkspaceComponent implements OnInit {

  constructor(private authService: AuthService, private redirectService: RedirectService) { }

  ngOnInit(): void {
  }

  public logout() {
    this.authService.logout();
    this.redirectService.redirectToLogin();
  }
}
