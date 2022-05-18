import { LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FlexLayoutModule } from '@angular/flex-layout';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '../../shared/shared.module';
import { CrudNgxTableComponent } from './crud-ngx-table/crud-ngx-table.component';

import { CrudsRoutes } from './cruds.routing';
import { CrudService } from './crud.service';
import { NgxTablePopupComponent } from './crud-ngx-table/ngx-table-popup/ngx-table-popup.component'
import { TranslateModule } from '@ngx-translate/core';
import { MatSelectModule } from '@angular/material/select';
import { TransactionCategoryComponent } from
  "./crud-ngx-table/ngx-table-popup/transaction-category-popup/transaction-category-popup.component";
import { AppCalendarService } from '../app-calendar/app-calendar.service';
import { ColorPickerService } from 'ngx-color-picker';
import { AppointmentTablePopupComponent } from './crud-ngx-table/appointment-table-popup/appointment-table-popup.component';
import { PacientsTablePopupComponent } from './crud-ngx-table/pacients-table-popup/pacients-table-popup.component';
import { DoctorsTablePopupComponent } from './crud-ngx-table/doctors-table-popup/doctors-table-popup.component';
import { MatRadioModule } from '@angular/material/radio';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatExpansionModule } from '@angular/material/expansion';
import { MattableComponent } from '../mattable/mattable.component';
import { MatTableService } from '../mattable/mattable.service';
import { MatTableModule } from '@angular/material/table';
import { A11yModule } from '@angular/cdk/a11y';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { CdkTreeModule } from '@angular/cdk/tree';
import { CdkTableModule } from '@angular/cdk/table';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { PortalModule } from '@angular/cdk/portal';
import { MatSortModule } from '@angular/material/sort';
import { CdkDetailRowDirective } from "./crud-ngx-table/cdk-detail-row.directive";
import { MatNativeDateModule } from '@angular/material/core';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    NgxDatatableModule,    
    MatInputModule,
    MatIconModule,
    MatCardModule,
    MatMenuModule,
    MatButtonModule,
    MatChipsModule,
    MatListModule,
    MatTooltipModule,
    MatDialogModule,
    MatSnackBarModule,
    MatSlideToggleModule,
    MatRadioModule,
    MatToolbarModule,
    MatSidenavModule,
    MatInputModule,
    MatCheckboxModule,
    MatExpansionModule,
    TranslateModule,
    SharedModule,
    RouterModule.forChild(CrudsRoutes),
    MatSelectModule,
    MatSortModule,
    FormsModule,
    MatTableModule,
    A11yModule,
    CdkStepperModule,
    CdkTableModule,
    CdkTreeModule,
    PortalModule,
    ScrollingModule,
    MatNativeDateModule
  ],
  declarations: [CrudNgxTableComponent, NgxTablePopupComponent, CdkDetailRowDirective, AppointmentTablePopupComponent, DoctorsTablePopupComponent, MattableComponent],
  providers: [CrudService, MatTableService, ColorPickerService,  { provide: LOCALE_ID, useValue: "ru" } ],
  // entryComponents: [NgxTablePopupComponent]
})
export class CrudsModule { }
