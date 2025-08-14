import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { PostComponent } from './post/post.component';

export const routes: Routes = [
    {path:'Home', component:HomeComponent},
    {path:'Profile/:id', component:ProfileComponent},
    {path:'Post/:id', component:PostComponent,},
    {path:'', redirectTo:'Home', pathMatch:'full',}
];
