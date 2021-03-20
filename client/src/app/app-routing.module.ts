import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

//we will use this array to provide the routes
const routes: Routes = [
  {path:'',component: HomeComponent}, //when someone browses to localhost:4200, HomeComponent is viewed.
  {
   path:'',
   runGuardsAndResolvers:'always',
   canActivate: [AuthGuard],
   children: [
    {path:'members',component: MemberListComponent, canActivate: [AuthGuard]},
    {path:'members/:id',component: MemberDetailComponent}, //each one of our members is gonna have a route parameter. :id is a place holder for this parameter
    {path:'lists',component: ListsComponent},
    {path:'messages',component: MessagesComponent},
   ] //all of our children are covered by a single Authguard
  },
  {path:'**',component: HomeComponent, pathMatch:'full'} //Wildcard route. If the user's typed something that doesn't match anything in our route configuration, he will be redirected to Homecomponent
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
