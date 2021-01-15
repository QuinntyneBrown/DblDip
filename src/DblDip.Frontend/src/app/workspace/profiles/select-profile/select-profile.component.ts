import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { Profile } from '../profile';
import { ProfilesService } from '../profiles.service';

@Component({
  selector: 'app-select-profile',
  templateUrl: './select-profile.component.html',
  styleUrls: ['./select-profile.component.scss']
})
export class SelectProfileComponent implements OnInit {
  public destroyed: Subject<void> = new Subject();
  
  

  constructor(private profilesService: ProfilesService) { }

  profiles$: Observable<Profile[]> = this.profilesService.getByCurrentAccount();
  ngOnInit(): void {

    this.profiles$.subscribe();
  }

}
