import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './accounts/accounts.component';
import { RequestComponent } from './requests/requests.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { BundlesComponent } from './bundles/bundles.component';
import { BundleRequestComponent } from './bundle-request/bundle-request.component';


const routes: Routes = [
  { path: '', redirectTo: '/', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'accounts', component: AccountComponent },
  { path: 'requests', component: RequestComponent },
  { path: 'bundles', component: BundlesComponent },
  { path: 'bundle-request', component: BundleRequestComponent },

];




@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
