import { timer } from 'rxjs';
import { MessageService } from 'primeng/api';
import { Agency } from './../../models/agencies';
import { ActivatedRoute } from '@angular/router';
import { AgenciesService } from './../../services/agencies.service';
import { AGENCY_STATUS } from './../../constants/STATUS';
import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'booking-yacht-agencies-form',
  templateUrl: './agencies-form.component.html',
  styleUrls: ['./agencies-form.component.scss'],
})
export class AgenciesFormComponent implements OnInit {
  loading = true;
  isSubmit = false;
  status: any[] = [];
  selectedSatus: any;
  currentUser: any;
  agency?: Agency;
  constructor(
    private location: Location,
    private angencyService: AgenciesService,
    private route: ActivatedRoute,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    setTimeout(() => {
      this.loading = false;
    }, 1000);
    this._mapStatus();
    this.route.params.subscribe((params) => {
      if (params.id) {
        this.currentUser = params.id;

        this.angencyService.getAgency(this.currentUser).subscribe((res) => {
          this.agency = res.data;
          this.selectedSatus = res.data.status.toString();
        });
      }
    });
  }
  onChangeStatus(selected: string) {
    this.angencyService.updateStatus(this.currentUser, selected).subscribe(
      (res) => {
        this.messageService.add({
          severity: 'success',
          summary: 'Update status successfull!',
        });
        timer(500)
          .toPromise()
          .then(() => {
            this.location.back();
          });
      },
      (error) => {
        if (error.status == 500) {
          this.messageService.add({
            severity: 'error',
            detail: error.message,
          });
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'ERROR',
            detail: error.error,
          });
        }
        timer(500)
          .toPromise()
          .then(() => {
            this.location.back();
          });
      }
    );
  }
  _mapStatus() {
    this.status = Object.keys(AGENCY_STATUS).map((key) => {
      return {
        id: key,
        name: AGENCY_STATUS[key].lable,
      };
    });
  }
  onCancle() {
    this.location.back();
  }
}
