import { Component, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { MatListModule } from '@angular/material/list';
import { BreakpointObserver } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sidenav',
  standalone: true,
  imports: [MatSidenavModule, MatListModule, MatButtonModule, MatIconModule, RouterModule, MatToolbarModule, CommonModule],
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent {
  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;
  isMobile = true;
  isCollapsed = true;
  mobileOpened: boolean = false;
  constructor(private observer: BreakpointObserver) { }

  ngOnInit() {
    this.observer.observe(['(max-width: 800px)']).subscribe((screenSize) => {
      this.isMobile = screenSize.matches;
    });
  }

  toggleMenu() {
    if (this.isMobile) {
      this.sidenav.toggle();
      this.mobileOpened = this.sidenav.opened;
    } else {
      this.sidenav.toggle();
    }
  }

  logOut() {

  }


}