import { Routes } from '@angular/router';
import { ForkliftListComponent } from './forklift-list/forklift-list.component';
import { ForkliftCommandsComponent } from './forklift-commands/forklift-commands.component';

export const routes: Routes = [
    { path : "forklift", component : ForkliftListComponent},
    { path: "forklift-commands", component: ForkliftCommandsComponent },
    { path: '', redirectTo: '/forklift', pathMatch: 'full' },
    { path: '**', redirectTo: '/forklift' }
];
