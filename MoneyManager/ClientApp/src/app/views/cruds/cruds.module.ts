import { NgModule } from '@angular/core';
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
    TranslateModule,
    SharedModule,
    RouterModule.forChild(CrudsRoutes),
    MatSelectModule,
    FormsModule
  ],
  declarations: [CrudNgxTableComponent, NgxTablePopupComponent, TransactionCategoryComponent, AppointmentTablePopupComponent, PacientsTablePopupComponent],
  providers: [CrudService, ColorPickerService],
  // entryComponents: [NgxTablePopupComponent]
})
export class CrudsModule { }
