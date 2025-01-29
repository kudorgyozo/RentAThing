
import { bootstrapApplication } from '@angular/platform-browser';
import { withInterceptorsFromDi, provideHttpClient, withInterceptors } from '@angular/common/http';
import { routes } from './app/routes';
import { AppComponent } from './app/app.component';
import { provideExperimentalZonelessChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { tokenInterceptor } from './app/services/token.interceptor';

bootstrapApplication(AppComponent, {
    providers: [
        provideExperimentalZonelessChangeDetection(),
        provideRouter(routes),
        //importProvidersFrom(BrowserModule, AppRoutingModule),
        provideHttpClient(withInterceptorsFromDi(), withInterceptors([tokenInterceptor]))
        //provideLoggingServiceConfig({ logLevel: LogLevel.Info, consoleLogOnly: true }),
    ]
})
    .catch(err => console.error(err));
