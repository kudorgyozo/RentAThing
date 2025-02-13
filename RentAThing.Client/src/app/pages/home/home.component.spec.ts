import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeComponent } from './home.component';
import { Component, provideExperimentalZonelessChangeDetection } from '@angular/core';
import { RentComponent } from '../../components/rent/rent.component';
import { provideRouter } from '@angular/router';
//import { RentComponent } from '../../components/rent/rent.component';

@Component({
    selector: 'app-rent',
    template: ``,
    standalone: true,
})
class RentMockComponent { }

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
            imports: [HomeComponent, RentMockComponent],

            providers: [
                provideExperimentalZonelessChangeDetection(),
                provideRouter([]),
                //{ provide: RentComponent, useValue: RentComponentMock }
            ],
        })
            .overrideComponent(HomeComponent, {
                remove: { imports: [RentComponent] },
                add: { imports: [RentMockComponent] }
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
