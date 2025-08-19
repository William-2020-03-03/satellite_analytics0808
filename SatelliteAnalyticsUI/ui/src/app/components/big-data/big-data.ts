import { Component, OnInit } from '@angular/core';
import { BigDataService, UserLogAppInfo } from '../../services/big-data-service';
import { MatListModule } from '@angular/material/list';
import { ScrollingModule } from '@angular/cdk/scrolling';

@Component({
  selector: 'app-big-data',
  imports: [MatListModule, ScrollingModule],
  templateUrl: './big-data.html',
  styleUrl: './big-data.scss'
})
export class BigData implements OnInit {
  displayedColumns: string[] = ['Module', 'Operation', "TriggerType", "Browser", "Build", "Platform", "Language", "Created"];
  userLogAppInfos: UserLogAppInfo[] = [];

  skip: number = 0;
  batchSize = 1000;
  loading = false;
  maxRecords = 3000000;

  constructor(private bigDataService: BigDataService) { }

  ngOnInit(): void {
    this.loadData();
  }


  loadData(): void {
     this.loading = true;

    this.bigDataService.loadData(this.skip, this.batchSize).subscribe({
      next: (res) => {
        this.userLogAppInfos = [...this.userLogAppInfos, ...res];
        this.loading = false;
      },
      error: (err) => {
        console.error('request failed:', err);
      }
    }
    )
  }

  onScroll(index: number): void {

    if (index + this.batchSize >= this.userLogAppInfos.length && !this.loading && this.userLogAppInfos.length < this.maxRecords) {
          console.log("...index....", index);
      this.skip += this.batchSize;
      this.loadData();
    }
  }

}
