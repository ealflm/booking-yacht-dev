import { BookingNowComponent } from './booking-now/booking-now.component';
import { DestinationsDetailsComponent } from './destinations/destinations-details/destinations-details.component';
import { DestinationsComponent } from './destinations/destinations.component';
import { ServicesComponent } from './services/services.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { GalleryComponent } from './gallery/gallery.component';
import { HomePagesComponent } from './home-pages/home-pages.component';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { PagesComponent } from './pages.component';

const routes: Routes = [

    {
        path: '', component: PagesComponent,
        children: [
            { path: '', component: HomePagesComponent },
            { path: 'gallery', component: GalleryComponent },
            { path: 'about-us', component: AboutUsComponent },
            { path: 'services', component: ServicesComponent },
            { path: 'destinations', component: DestinationsComponent },
            { path: 'destinations-details/:id', component: DestinationsDetailsComponent },
            { path: 'booking-now', component: BookingNowComponent }
       
        ]
    },
]

@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
})
export class PagesRoutingModule { }
