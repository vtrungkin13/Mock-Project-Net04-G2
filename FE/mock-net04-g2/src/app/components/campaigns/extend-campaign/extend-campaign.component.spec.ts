import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtendCampaignComponent } from './extend-campaign.component';

describe('ExtendCampaignComponent', () => {
  let component: ExtendCampaignComponent;
  let fixture: ComponentFixture<ExtendCampaignComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExtendCampaignComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExtendCampaignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
