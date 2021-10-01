import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AparmentFormComponent } from './aparment-form/aparment-form.component';

@NgModule({
  imports: [CommonModule, RouterModule, NgbModule],
  declarations: [SidebarComponent],
  exports: [SidebarComponent],
})
export class ComponentsModule {}
