import { Component, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { Profile } from '../profile';
import { ProfilesService } from '../profiles.service';
import {  } from "@angular/cdk";
import { OverlayRef } from '@angular/cdk/overlay';
@Component({
  selector: 'app-select-profile',
  templateUrl: './select-profile.component.html',
  styleUrls: ['./select-profile.component.scss']
})
export class SelectProfileComponent implements OnDestroy {
  public destroyed: Subject<void> = new Subject();
  selectedProfile$: Subject<Profile> = new Subject();

  constructor(private profilesService: ProfilesService, private _overlayRef: OverlayRef) { }

  profiles$: Observable<Profile[]> = this.profilesService.getByCurrentAccount();

  handleProfileSelect(profile: Profile) {
    this.selectedProfile$.next(profile);
    this._overlayRef.dispose();
  }

  
  ngOnDestroy() {
    this.destroyed.complete();
    this.destroyed.next();
  }
}
