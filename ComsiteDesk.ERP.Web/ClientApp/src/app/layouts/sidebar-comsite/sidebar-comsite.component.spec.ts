import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarComsiteComponent } from './sidebar-comsite.component';

describe('SidebarComsiteComponent', () => {
  let component: SidebarComsiteComponent;
  let fixture: ComponentFixture<SidebarComsiteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SidebarComsiteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SidebarComsiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
