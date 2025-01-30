import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeComponent } from './home.component';
import { Component, NO_ERRORS_SCHEMA, provideExperimentalZonelessChangeDetection } from '@angular/core';
import { RentComponent } from '../../components/rent/rent.component';
import { provideRouter } from '@angular/router';
//import { RentComponent } from '../../components/rent/rent.component';

@Component({
    selector: 'app-rent',
    template: ``,
    standalone: true,
})
class RentComponentMock { }

// @Component({
//     selector: 'app-rent',
//     imports: [],
//     template: '',
// })
// export class RentComponent { }

describe('HomeComponent', () => {
    let component: HomeComponent;
    let fixture: ComponentFixture<HomeComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [HomeComponent, RentComponentMock],

            providers: [
                provideExperimentalZonelessChangeDetection(),
                provideRouter([]),
                //{ provide: RentComponent, useValue: RentComponentMock }
            ],
        })
            .overrideComponent(HomeComponent, {
                remove: { imports: [RentComponent] },
                add: { imports: [RentComponentMock] }
            })
            .compileComponents();

        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
        await fixture.whenStable();
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
