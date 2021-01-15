import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { currentProfileKey } from '@core';
import { LocalStorageService } from '@core/local-storage.service';
import { DialogService } from '@shared/dialog.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SelectProfileComponent } from './select-profile/select-profile.component';

@Injectable({
  providedIn: 'root'
})
export class CurrentProfileGuard implements CanActivate {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const currentProfile = this._localStorageService.get({ name: currentProfileKey });
      if (currentProfile) {
        return true;
      }

      return this._dialogService.open<SelectProfileComponent>(SelectProfileComponent)
      .destroyed
      .pipe(
        map(x => true)
      );
  }

  constructor(private _dialogService: DialogService, private _localStorageService: LocalStorageService) {

  }
  
}
