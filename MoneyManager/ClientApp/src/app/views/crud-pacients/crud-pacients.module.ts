import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FlexLayoutModule } from "@angular/flex-layout";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatChipsModule } from "@angular/material/chips";
import { MatDialogModule } from "@angular/material/dialog";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatListModule } from "@angular/material/list";
import { MatMenuModule } from "@angular/material/menu";
import { MatSelectModule } from "@angular/material/select";
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { MatTooltipModule } from "@angular/material/tooltip";
import { RouterModule } from "@angular/router";
import { TranslateModule } from "@ngx-translate/core";
import { NgxDatatableModule } from "@swimlane/ngx-datatable";
import { ColorPickerService } from "ngx-color-picker";
import { SharedModule } from "../../shared/shared.module";
import { TransactionCategoryComponent } from "../cruds/crud-ngx-table/ngx-table-popup/transaction-category-popup/transaction-category-popup.component";
import { PacientsTablePopupComponent } from "../cruds/crud-ngx-table/pacients-table-popup/pacients-table-popup.component";
import { CrudService } from "../cruds/crud.service";
import { InvoiceService } from "../invoice/invoice.service";
import { CrudPacientsComponent } from "./crud-pacients.component";
import { CrudPacientsRoutes } from "./crud-pacients.routing";
import { CrudPacientsService } from "./crud-pacients.service";
import { PacientsAppointmentPopupComponent } from "./pacients-appoitment-popup/pacients-appoitment-popup.component";

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
    RouterModule.forChild(CrudPacientsRoutes),
    MatSelectModule,
    FormsModule
  ],
  declarations: [CrudPacientsComponent, TransactionCategoryComponent, PacientsTablePopupComponent, PacientsAppointmentPopupComponent],
  providers: [CrudPacientsService, CrudService, ColorPickerService, InvoiceService],
  // entryComponents: [NgxTablePopupComponent]
})
export class CrudPacientsModule { }
