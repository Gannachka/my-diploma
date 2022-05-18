import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { egretAnimations } from '../../shared/animations/egret-animations';
import { DisplayTransactionModel } from '../../shared/models/display.transaction.model';
import { AppLoaderService } from '../../shared/services/app-loader/app-loader.service';
import { NgxTablePopupComponent } from '../cruds/crud-ngx-table/ngx-table-popup/ngx-table-popup.component';
import { PacientsTablePopupComponent } from '../cruds/crud-ngx-table/pacients-table-popup/pacients-table-popup.component';
import { CrudPacientsService } from './crud-pacients.service';
import { PacientsAppointmentPopupComponent } from './pacients-appoitment-popup/pacients-appoitment-popup.component';

@Component({
  selector: 'app-crud-pacients',
  templateUrl: './crud-pacients.component.html',
  animations: egretAnimations
})
export class CrudPacientsComponent implements OnInit, OnDestroy  {
  public items: DisplayTransactionModel[];
  public getItemSub: Subscription;
  constructor(
    private crudService: CrudPacientsService,
    private snack: MatSnackBar,
    private loader: AppLoaderService,
    private dialog: MatDialog,
  ) {
  }

  ngOnInit(): void {
    this.getItems();
  }

  ngOnDestroy() {
    if (this.getItemSub) {
      this.getItemSub.unsubscribe();
    }
  }

  getItems() {
    this.getItemSub = this.crudService.getItems()
      .subscribe(
        data => {
          this.items = data;
        },
        error => {
          this.snack.open('Some Problems with loading', 'OK', { duration: 4000 })
        });
  }
  openAddingPacientPopUp(data: any = {}, isNew) {
    const title = 'Добавить нового пациента';
    const dialogRef: MatDialogRef<any> = this.dialog.open(PacientsTablePopupComponent, {
      width: '720px',
      disableClose: true,
      data: { title, payload: data }
    });
    dialogRef.afterClosed()
      .subscribe(res => {
        if (!res) {
          this.getItems();
          return;
        }
        this.loader.open();
        if (isNew) {
          this.crudService.addPacient(res)
            .subscribe(
              data => {
                this.items = data;
                this.loader.close();
                this.snack.open('Пациент добавлен!', 'OK', { duration: 4000 })
              },
              error => {
                this.loader.close();
                this.snack.open(error.error.message, 'OK', { duration: 4000 })
              });
        }
      });
  }
  openPopUp(data: any = {}, isNew?) {
    const title = 'Добавить лекарство';
    const dialogRef: MatDialogRef<any> = this.dialog.open(NgxTablePopupComponent, {
      width: '720px',
      disableClose: true,
      data: { title, payload: data }
    });
    dialogRef.afterClosed()
      .subscribe(res => {
        if (!res) {
          return;
        }
        this.loader.open();
        if (isNew) {
          this.crudService.addAppointment(res)
            .subscribe(
              data => {
                //this.items = data;
                this.loader.close();
                this.snack.open('Лекарство добавлено!', '', { duration: 4000 })
              },
              error => {
                this.loader.close();
                this.snack.open(error.error.message, '', { duration: 4000 })
              });
        } else {
          this.crudService.updateItem(data.id, res)
            .subscribe(
              data => {
                this.items = data;
                this.loader.close();
                this.snack.open('Transaction Updated!', 'OK', { duration: 4000 });
              },
              error => {
                this.loader.close();
                this.snack.open(error.error.message, 'OK', { duration: 4000 })
              });
        }
      });
  }
  //isNew?
  openAppoitmentPopUp(data: any = {},) {
    const title = 'Препараты пациента';
    const dialogRef: MatDialogRef<any> = this.dialog.open(PacientsAppointmentPopupComponent, {
      width: '720px',
      disableClose: true,
      data: { title, payload: data }
    });
    dialogRef.afterClosed()
      .subscribe(res => {
        if (!res) {
          return;
        }
        this.loader.open();
        //if (isNew) {
        //  this.crudService.addAppointment(res)
        //    .subscribe(
        //      data => {
        //        //this.items = data;
        //        this.loader.close();
        //        this.snack.open('Лекарство добавлено!', '', { duration: 4000 })
        //      },
        //      error => {
        //        this.loader.close();
        //        this.snack.open(error.error.message, '', { duration: 4000 })
        //      });
        //} else {
        //  this.crudService.updateItem(data.id, res)
        //    .subscribe(
        //      data => {
        //        this.items = data;
        //        this.loader.close();
        //        this.snack.open('Transaction Updated!', 'OK', { duration: 4000 });
        //      },
        //      error => {
        //        this.loader.close();
        //        this.snack.open(error.error.message, 'OK', { duration: 4000 })
        //      });
        //}
      });
  }
}
