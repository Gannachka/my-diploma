import { Component, OnInit, OnDestroy } from '@angular/core';
import { CrudService } from '../crud.service';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppConfirmService } from '../../../shared/services/app-confirm/app-confirm.service';
import { AppLoaderService } from '../../../shared/services/app-loader/app-loader.service';
import { NgxTablePopupComponent } from './ngx-table-popup/ngx-table-popup.component';
import { Subject, Subscription } from 'rxjs';
import { egretAnimations } from "../../../shared/animations/egret-animations";
import { DisplayTransactionModel } from '../../../shared/models/display.transaction.model';
import { CalendarFormDialogComponent } from '../../app-calendar/calendar-form-dialog/calendar-form-dialog.component';
import { AppCalendarService } from '../../app-calendar/app-calendar.service';
import { EgretCalendarEvent } from '../../../shared/models/event.model';
import { CalendarEvent, CalendarEventAction } from 'angular-calendar';
import { LocalStoreService } from '../../../shared/services/local-store.service';
import { AppointmentTablePopupComponent } from './appointment-table-popup/appointment-table-popup.component';

@Component({
  selector: 'app-crud-ngx-table',
  templateUrl: './crud-ngx-table.component.html',
  animations: egretAnimations
})
export class CrudNgxTableComponent implements OnInit, OnDestroy {
  public minage;
  public maxage;
  public items: DisplayTransactionModel[];
  public questionaries: any[];
  public appointments: any[];
  public getItemSub: Subscription;
  private dialogRef: MatDialogRef<NgxTablePopupComponent>;
  public events: EgretCalendarEvent[];
  public refresh: Subject<any> = new Subject();
  constructor(
    private dialog: MatDialog,
    private snack: MatSnackBar,
    private crudService: CrudService,
    private confirmService: AppConfirmService,
    private loader: AppLoaderService,
    private calendarService: AppCalendarService,
    private ls: LocalStoreService
  ) {
  }

  ngOnInit() {
    if (this.getUserRole() === 'Admin') {
      this.getItems();
    } else if (this.getUserRole() === 'User') {
      this.getAppointments();
      this.getMyQuestionaries();
    }
  }
  ngOnDestroy() {
    if (this.getItemSub) {
      this.getItemSub.unsubscribe();
    }
  }

  search() {
    this.items = this.items.filter(x => x.age >= this.minage && x.age <= this.maxage);
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

  getAppointments() {
    this.getItemSub = this.crudService.getApp()
      .subscribe(
        data => {
          this.appointments = data;
        },
        error => {
          this.snack.open('Some Problems with loading', 'OK', { duration: 4000 })
        });
  }

  getMyQuestionaries() {
    this.getItemSub = this.crudService.getQuestionaries()
      .subscribe(
        data => {
          this.questionaries = data;
        },
        error => {
          this.snack.open('Some Problems with loading', 'OK', { duration: 4000 })
        });
  }

  openQuestionairePopUp(data: any = {}, isNew) {
    const title = 'Add new questionary';
    const dialogRef: MatDialogRef<any> = this.dialog.open(AppointmentTablePopupComponent, {
      width: '720px',
      disableClose: true,
      data: { title, payload: data }
    });
    dialogRef.afterClosed()
      .subscribe(res => {
        if (!res) {
          this.getMyQuestionaries();
          return;
        }
        this.loader.open();
        if (isNew) {
          this.crudService.addQuestioary(res)
            .subscribe(
              data => {
                this.questionaries = data;
                this.loader.close();
                this.snack.open('Questionarie Added!', 'OK', { duration: 4000 })
              },
              error => {
                this.loader.close();
                this.snack.open(error.error.message, 'OK', { duration: 4000 })
              });
        } else {
          this.crudService.updateItem(data.id, res)
            .subscribe(
              data => {
                this.items = data;
                this.loader.close();
                this.snack.open('Questionarie Updated!', 'OK', { duration: 4000 });
              },
              error => {
                this.loader.close();
                this.snack.open(error.error.message, 'OK', { duration: 4000 })
              });
        }
      });
  }

  openPopUp(data: any = {}, isNew?) {
    const title = 'Add new appointment';
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
                this.snack.open('Appointment Added!', 'OK', { duration: 4000 })
              },
              error => {
                this.loader.close();
                this.snack.open(error.error.message, 'OK', { duration: 4000 })
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

  getUserRole(): string {
    return this.ls.getItem('EGRET_USER').role;
  }
}
