import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../_core/auth.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnDestroy {

  private readonly _destroyed: Subject<void> = new Subject();

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  public handleTryToLogin($event: { username: string, password: string }) {
    this.authService
    .tryToLogin({
      username: $event.username,
      password: $event.password
    })
    .pipe(
      takeUntil(this._destroyed),
    )
    .subscribe(
      () => {
        this.router.navigateByUrl('/');
      },
      errorResponse => {
        // handle error response
      }
    );  
  }

  ngOnDestroy(): void {
    this._destroyed.next();
    this._destroyed.complete();
  }
}
