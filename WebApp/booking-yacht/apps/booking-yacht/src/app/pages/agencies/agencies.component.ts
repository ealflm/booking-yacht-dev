import { MessageService, ConfirmationService } from 'primeng/api';
import { AgenciesService } from './../../services/agencies.service';
import { AGENCY_STATUS } from './../../constants/STATUS';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'booking-yacht-agencies',
  templateUrl: './agencies.component.html',
  styleUrls: ['./agencies.component.scss'],
})
export class AgenciesComponent implements OnInit {
  agencies: [] = [];
  agencyStatus = AGENCY_STATUS;
  loading = true;
  status = [
    { id: '0', lable: 'None' },
    { id: '1', lable: 'Enable' },
    { id: '2', lable: 'Disable' },
  ];
  constructor(
    private agencyService: AgenciesService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.getAgencies();
    console.log(this.agencies);
  }
  getAgencies() {
    this.agencyService.getAgencies().subscribe((res) => {
      setTimeout(() => {
        this.loading = false;
      }, 1000);
      this.agencies = res.data;
    });
  }
  deleteAgency(id: string) {
    this.confirmationService.confirm({
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      message: 'Bạn có chắc chắn muốn chặn quyền truy cập tài khoản này ?',
      accept: () => {
        this.agencyService.deleteAgency(id).subscribe((res) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Successfull!',
            detail: 'chặn thành công!',
          });
          this.getAgencies();
        });
      },
      reject: () => {
        //reject action
      },
    });
  }
  onChangeStatus(id: string) {
    this.loading = true;
    if (id == '0') {
      this.agencyService.getAgencies().subscribe((res) => {
        setTimeout(() => {
          this.loading = false;
        }, 1000);
        this.agencies = res.data;
      });
    } else {
      this.agencyService.getAgencies(id).subscribe((res) => {
        setTimeout(() => {
          this.loading = false;
        }, 1000);
        this.agencies = res.data;
      });
    }
  }
  getValue(event: Event) {
    return (event.target as HTMLInputElement).value;
  }
}
