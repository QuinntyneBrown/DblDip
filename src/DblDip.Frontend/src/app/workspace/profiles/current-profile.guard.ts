import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { accessTokenKey, currentProfileKey } from '@core';
import { LocalStorageService } from '@core/local-storage.service';
import { DialogService } from '@shared/dialog.service';
import { Observable } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';
import { AccountsService } from '../account/accounts.service';
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

      const component = this._dialogService.open<SelectProfileComponent>(SelectProfileComponent);

      return component
      .selectedProfile$
      .pipe(
        switchMap(profile => {          
          return this._accountService.setCurrentProfile({ profileId: profile.profileId })
          .pipe(
            map(x => {
              return {
                profile,
                accessToken: x.accessToken
              }
            })
          )
        }),
        map(x => {
          this._localStorageService.put({ name: accessTokenKey, value: x.accessToken });
          this._localStorageService.put({ name: currentProfileKey, value: x.profile });
          return true;
        })
      )
  }

  constructor(
    private _accountService: AccountsService,
    private _dialogService: DialogService, 
    private _localStorageService: LocalStorageService) {
  }
  
}
