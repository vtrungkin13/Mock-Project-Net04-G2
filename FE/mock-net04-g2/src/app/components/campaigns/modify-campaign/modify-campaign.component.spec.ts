import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyCampaignComponent } from './modify-campaign.component';

describe('ModifyCampaignComponent', () => {
  let component: ModifyCampaignComponent;
  let fixture: ComponentFixture<ModifyCampaignComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModifyCampaignComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModifyCampaignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
