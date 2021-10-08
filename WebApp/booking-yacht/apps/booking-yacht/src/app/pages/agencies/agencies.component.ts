import { Router } from '@angular/router';
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
    { id: '0', lable: 'Tất cả' },
    { id: '1', lable: 'Đang hoạt động' },
    { id: '2', lable: 'Vô Hiệu' },
  ];
  constructor(
    private agencyService: AgenciesService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getAgencies();
  }

  getAgencies() {
    this.agencyService.getAgencies().subscribe((res) => {
      this.agencies = res.data;
      setTimeout(() => {
        this.loading = false;
      }, 1000);
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
  editStatus(id: string) {
    this.router.navigate([`agencies/form/${id}`]);
  }
}
