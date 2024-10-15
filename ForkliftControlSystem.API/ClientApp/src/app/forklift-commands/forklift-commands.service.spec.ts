import { TestBed } from '@angular/core/testing';
import { ForkliftCommandsService } from './forklift-commands.service';


describe('ForkliftCommandsService', () => {
  let service: ForkliftCommandsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ForkliftCommandsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
