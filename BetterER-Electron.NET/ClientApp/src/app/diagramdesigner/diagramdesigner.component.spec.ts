import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DiagramdesignerComponent } from './diagramdesigner.component';

describe('DiagramdesignerComponent', () => {
  let component: DiagramdesignerComponent;
  let fixture: ComponentFixture<DiagramdesignerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DiagramdesignerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DiagramdesignerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
