import { Routes } from '@angular/router';
import { RentComponent } from './pages/rent/rent.component';
import { LoginComponent } from './pages/login/login.component';
import { HomeComponent } from './pages/home/home.component';

export const routes: Routes = [
    // {
    //     path: '',
    //     redirectTo: 'home',
    //     pathMatch: 'full',
    // },
    {
        path: 'home',
        component: HomeComponent,
    },
    {
        path: 'login',
        component: LoginComponent,
    },

    {
        path: 'rent',
        component: RentComponent,
    },


];


