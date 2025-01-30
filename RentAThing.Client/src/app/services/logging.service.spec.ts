import { TestBed } from '@angular/core/testing';

import { LoggingService } from './logging.service';
import { provideExperimentalZonelessChangeDetection } from '@angular/core';

describe('LoggingService', () => {
    let service: LoggingService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [
                //LoggingService,
                provideExperimentalZonelessChangeDetection()
            ]
        });
        service = TestBed.inject(LoggingService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
