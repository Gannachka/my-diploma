import { LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AppChatsComponent } from './app-chats.component';
import { ChatsRoutes } from './app-chats.routing';
import { ChatService } from './chat.service';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { SharedPipesModule } from 'app/shared/pipes/shared-pipes.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatSidenavModule,
    MatMenuModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatToolbarModule,
    MatCardModule,
    FlexLayoutModule,
    PerfectScrollbarModule,
    SharedPipesModule,
    RouterModule.forChild(ChatsRoutes)
  ],
  declarations: [AppChatsComponent],
  providers: [ChatService, { provide: LOCALE_ID, useValue: "ru" } ]
})
export class AppChatsModule {}
