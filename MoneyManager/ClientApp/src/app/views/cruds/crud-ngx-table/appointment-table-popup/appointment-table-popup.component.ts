import {Component, OnInit, Inject, OnDestroy} from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";
import { CrudService } from '../../crud.service';

@Component({
  selector: 'app-appointment-table-popup',
  templateUrl: './appointment-table-popup.component.html'
})
export class AppointmentTablePopupComponent implements OnInit, OnDestroy {

  public getCategoriesSub: Subscription;
  public updateCategoriesSub: Subscription;
  public removeCategoriesSub: Subscription;
  public getCurrenciesSub: Subscription;
  public itemForm: FormGroup;
  public selectedValue: string;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<AppointmentTablePopupComponent>,
    private fb: FormBuilder,
    private crudService: CrudService,
    public dialog: MatDialog,
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
      temperature: ['', [Validators.required, Validators.min(0)]],
      qDate: ['', Validators.required],
      comments: ['', Validators.required]
    });
  }

  submit() {
    this.dialogRef.close(this.itemForm.value);
  }
}
