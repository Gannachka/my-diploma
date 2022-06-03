import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";
import { CrudPacientsService } from '../crud-pacients.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { egretAnimations } from '../../../shared/animations/egret-animations';
import { registerLocaleData } from '@angular/common';
import localeRu from '@angular/common/locales/ru';

registerLocaleData(localeRu);

@Component({
  selector: 'app-pacients-appointment-table-popup',
  templateUrl: './pacients-appoitment-popup.component.html',
  animations: egretAnimations
})
export class PacientsAppointmentPopupComponent implements OnInit, OnDestroy {

  public getCategoriesSub: Subscription;
  public updateCategoriesSub: Subscription;
  public removeCategoriesSub: Subscription;
  public getItemSub: Subscription;
  public getCurrenciesSub: Subscription;
  public appointments: any[];
  public itemForm: FormGroup;
  public selectedValue: string;
  private snack: MatSnackBar;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<PacientsAppointmentPopupComponent>,
    private fb: FormBuilder,
    public dialog: MatDialog,
    private crudPacientsService: CrudPacientsService,
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer
  ) {
    this.matIconRegistry.addSvgIcon(
      "delete",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/images/svg-icons/close.svg")
    );
  }

  ngOnInit() {
    this.buildItemForm(this.data.payload);
    this.getPacientsAppointments(this.data.payload.userId)
  }

  getPacientsAppointments(userId) {
    this.getItemSub = this.crudPacientsService.getPacientsAppoitment(userId)
      .subscribe(
        data => {
          this.appointments = data;
        },
        error => {
          this.snack.open('Проблемы с загрузкой', 'OK', { duration: 4000 })
        });
  }

  ngOnDestroy() {
    if (this.getCategoriesSub) {
      this.getCategoriesSub.unsubscribe();
    }

    if (this.updateCategoriesSub) {
      this.updateCategoriesSub.unsubscribe();
    }

    if (this.removeCategoriesSub) {
      this.removeCategoriesSub.unsubscribe();
    }

    if (this.getCurrenciesSub) {
      this.getCurrenciesSub.unsubscribe();
    }
  }

  buildItemForm(item) {
    this.itemForm = this.fb.group({
      userId: [item.userId, Validators.required],
    });
  }

  submit() {
    this.dialogRef.close(this.itemForm.value);
  }
}
