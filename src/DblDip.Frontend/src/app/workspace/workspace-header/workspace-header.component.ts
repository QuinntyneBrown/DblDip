import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/auth.service';
import { RedirectService } from 'src/app/core/redirect.service';

@Component({
  selector: 'app-workspace-header',
  templateUrl: './workspace-header.component.html',
  styleUrls: ['./workspace-header.component.scss']
})
export class WorkspaceHeaderComponent implements OnInit {

  constructor(private authService: AuthService, private redirectService: RedirectService) { }

  ngOnInit(): void {
  }

  public logout() {
    this.authService.logout();
    this.redirectService.redirectToLogin();
  }

}
