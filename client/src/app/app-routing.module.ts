import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
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
    {path:'members/:username',component: MemberDetailComponent}, //each one of our members is gonna have a route parameter. :id is a place holder for this parameter
    {path:'lists',component: ListsComponent},
    {path:'messages',component: MessagesComponent},
   ] //all of our children are covered by a single Authguard
  },
  {path: 'errors', component: TestErrorsComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path:'**',component: NotFoundComponent, pathMatch:'full'} //Wildcard route. If the user's typed something that doesn't match anything in our route configuration, he will be redirected to NotFoundComponent
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
