import { A11yModule } from '@angular/cdk/a11y';
import { PortalModule } from '@angular/cdk/portal';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { CdkTableModule } from '@angular/cdk/table';
import { CdkTreeModule } from '@angular/cdk/tree';
import { NgModule } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTableModule } from '@angular/material/table';
import { ColorPickerService } from 'ngx-color-picker';
import { CrudPacientsComponent } from '../crud-pacients/crud-pacients.component';
import { CrudPacientsService } from '../crud-pacients/crud-pacients.service';
import { TransactionCategoryComponent } from '../cruds/crud-ngx-table/ngx-table-popup/transaction-category-popup/transaction-category-popup.component';
import { PacientsTablePopupComponent } from '../cruds/crud-ngx-table/pacients-table-popup/pacients-table-popup.component';
import { CrudService } from '../cruds/crud.service';
import { MatTableService } from './mattable.service';

@NgModule({
   exports: [
    A11yModule,
    CdkStepperModule,
    CdkTableModule,
    CdkTreeModule,
    MatCardModule,
    MatExpansionModule,
    MatTableModule,
    PortalModule,
    ScrollingModule
  ],
  declarations: [ ],
  providers: [MatTableService, ColorPickerService],
})
export class DemoMaterialModule { }
