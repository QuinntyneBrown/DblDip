import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PortfolioFeaturedComponent } from './portfolio-featured.component';

describe('PortfolioFeaturedComponent', () => {
  let component: PortfolioFeaturedComponent;
  let fixture: ComponentFixture<PortfolioFeaturedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PortfolioFeaturedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PortfolioFeaturedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
