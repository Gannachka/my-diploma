import { Component, OnInit } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource, MatTable } from '@angular/material/table';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
  children?: any[];
}

const ELEMENT_DATA: PeriodicElement[] = [
  { name: "Hydrogen", weight: 1.0079, symbol: "H", position: 1 },
  { name: "Helium", weight: 4.0026, symbol: "He", position: 2 },
  { name: "Lithium", weight: 6.941, symbol: "Li", position: 3 },
  {
    
    name: "Beryllium",
    weight: 9.0122,
    symbol: "Be",
    position: 4,
    children: [
      { id: "4.1", text: "abc 4.1" },
      { id: "4.2", text: "abc 4.2" },
      { id: "4.3", text: "abc 4.3" },
    ]
  },
 
];

/**
 * @title Basic use of `<table mat-table>`
 */

@Component({
  selector: 'app-mattable',
  templateUrl: './mattable.component.html',
  styleUrls: ['./mattable.component.scss'],
  animations: [
   
  ],
})
export class MattableComponent implements OnInit {

  displayedColumns: string[] = ["name", "weight", "symbol", "position"];
  dataSource = ELEMENT_DATA;

  expandedRows: { [key: number]: boolean } = {};

  expand(element: PeriodicElement) {
    this.expandedRows[element.position] = !this.expandedRows[element.position]
  }
  constructor() { }

  ngOnInit(): void {
  }
}
