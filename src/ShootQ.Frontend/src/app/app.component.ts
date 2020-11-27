import { Component } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthService } from './_core/auth.service';
import { LocalStorageService } from './_core/local-storage.service';

@Component({
  selector: 'app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  public isAdmin$: Observable<boolean> = of(false);

}
