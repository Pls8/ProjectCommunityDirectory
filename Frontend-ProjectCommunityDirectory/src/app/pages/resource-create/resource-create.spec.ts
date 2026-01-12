import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResourceCreate } from './resource-create';

describe('ResourceCreate', () => {
  let component: ResourceCreate;
  let fixture: ComponentFixture<ResourceCreate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResourceCreate]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ResourceCreate);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
