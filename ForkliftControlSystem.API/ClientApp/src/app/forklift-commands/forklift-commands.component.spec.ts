import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForkliftCommandsComponent } from './forklift-commands.component';

describe('ForkliftCommandsComponent', () => {
  let component: ForkliftCommandsComponent;
  let fixture: ComponentFixture<ForkliftCommandsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForkliftCommandsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ForkliftCommandsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
