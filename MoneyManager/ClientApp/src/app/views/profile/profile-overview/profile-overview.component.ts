import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import * as xml2js from 'xml2js';


export interface NewsRss {
  rss: IRssObject;
}

export interface IRssObject {
  $: any;
  channel: Array<IRssChannel>;
}

export interface IRssChannel {
  "atom:link": Array<string>;
  description: Array<string>;
  image: Array<IRssImage>;
  item: Array<IRssItem>;
  language: Array<string>;
  lastBuildDate: Date;
  link: Array<string>;
  title: Array<string>;
}

export interface IRssImage {
  link: Array<string>;
  title: Array<string>;
  url: Array<string>;
}

export interface IRssItem {
  category: Array<string>;
  description: Array<string>;
  guid: any;
  link: Array<string>;
  pubDate: Date;
  title: Array<string>;
}

@Component({
  selector: 'app-profile-overview',
  templateUrl: './profile-overview.component.html',
  styleUrls: ['./profile-overview.component.css']
})
export class ProfileOverviewComponent implements OnInit {
  RssData: NewsRss;

  constructor(private http: HttpClient,
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer  ) { }

  ngOnInit() {
    this.route.queryParamMap.subscribe((params: any) => {
      var symbol = params.params.q ?? 'AAPL';
      this.GetRssFeedData(symbol);
    });
  }

  sanitize(url: string) {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

  GetRssFeedData(symbol) {
    const requestOptions: Object = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      }),
      observe: 'body',
      responseType: 'text'
    };
    const _url = "assets/profile-overview-config.xml";
    this.http
      .get<any>(
        _url,
        requestOptions
      )
      .subscribe((data) => {
        let parseString = xml2js.parseString;
        parseString(data, (err, result: NewsRss) => {
          this.RssData = result;
        });
      });
  }

  getDataDiff(endDate) {
    let setDate = new Date(endDate).toISOString();
    var diff = (new Date()).getTime() - new Date(setDate).getTime();
    var days = Math.floor(diff / (60 * 60 * 24 * 1000));
    var hours = Math.floor(diff / (60 * 60 * 1000)) - (days * 24);
    var minutes = Math.floor(diff / (60 * 1000)) - ((days * 24 * 60) + (hours * 60));
    let dayString = days == 0 ? "" : days + "days ";
    let hoursString = hours == 0 ? "" : hours + "hr ";
    let minutesString = minutes == 0 ? "" : minutes + "m ";
    return dayString + hoursString + minutesString;
  }
}
