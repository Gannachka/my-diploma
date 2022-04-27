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
import { DisplayDoctorModel } from '../../../shared/models/display.doctor.model';
import { AppCalendarService } from '../../app-calendar/app-calendar.service';
import { EgretCalendarEvent } from '../../../shared/models/event.model';
import { LocalStoreService } from '../../../shared/services/local-store.service';
import { AppointmentTablePopupComponent } from './appointment-table-popup/appointment-table-popup.component';
import { PacientsTablePopupComponent } from './pacients-table-popup/pacients-table-popup.component';
import { DoctorsTablePopupComponent } from './doctors-table-popup/doctors-table-popup.component';

@Component({
  selector: 'app-crud-ngx-table',
  templateUrl: './crud-ngx-table.component.html',
  animations: egretAnimations
})
export class CrudNgxTableComponent implements OnInit, OnDestroy {
  public minage;
  public maxage;
  public items: DisplayTransactionModel[];
  public doctors: DisplayDoctorModel[];
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
    if (this.getUserRole() === 'Doctor') {
      this.getItems();
    } else if (this.getUserRole() === 'User') {
      this.getAppointments();
      this.getMyQuestionaries();
    } else if (this.getUserRole() === 'Admin') {
      this.getDoctors();
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

  getDoctors() {
    this.getItemSub = this.crudService.getDoctors()
      .subscribe(
        data => {
          this.doctors = data;
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
    const title = 'Самочувствие';
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
                this.snack.open('Сегоднешнее состояние добавлено', 'OK', { duration: 4000 })
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
                this.snack.open('Сегодняшнее состояние обновлено', 'OK', { duration: 4000 });
              },
              error => {
                this.loader.close();
                this.snack.open(error.error.message, 'OK', { duration: 4000 })
              });
        }
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

  openAddingDoctorPopUp(data: any = {}, isNew) {
    const title = 'Добавить нового доктора';
    const dialogRef: MatDialogRef<any> = this.dialog.open(DoctorsTablePopupComponent, {
      width: '720px',
      disableClose: true,
      data: { title, payload: data }
    });
    dialogRef.afterClosed()
      .subscribe(res => {
        if (!res) {
          this.getDoctors();
          return;
        }
        this.loader.open();
        if (isNew) {
          this.crudService.addDoctor(res)
            .subscribe(
              data => {
                this.doctors = data;
                this.loader.close();
                this.snack.open('Доктор добавлен!', 'OK', { duration: 4000 })
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
