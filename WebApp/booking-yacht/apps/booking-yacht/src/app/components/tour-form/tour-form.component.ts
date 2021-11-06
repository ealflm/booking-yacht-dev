import { async } from '@angular/core/testing';
import { DestinationsService } from './../../services/destinations.service';
import { SECONDARY_STATUS, AGENCY_STATUS } from './../../constants/STATUS';
import { BehaviorSubject, Subject, timer } from 'rxjs';
import { Tour } from './../../models/tours';
import { ActivatedRoute } from '@angular/router';
import { ToursService } from './../../services/tours.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { MessageService } from 'primeng/api';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { CdkDragDrop, moveItemInArray, CdkDrag } from '@angular/cdk/drag-drop';
import { DestinationsTours } from '../../models/destinations-tours';
import { Destination } from '../../models/destinations';

@Component({
  selector: 'booking-yacht-tour-form',
  templateUrl: './tour-form.component.html',
  styleUrls: ['./tour-form.component.scss'],
})
export class TourFormComponent implements OnInit {
  editMode = false;
  loading = true;
  form!: FormGroup;
  isSubmit = false;
  status: any[] = [];
  currentTour!: string;
  tour: [] = [];
  selectedDes!: unknown[];
  destiations?: any[];
  previewImage?: string | ArrayBuffer | null =
    '../../../assets/img/noimage.png';
  imageLink?: string;
  progress!: number;
  ListDes: Destination[] = [];
  ListDesID: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private tourService: ToursService,
    private route: ActivatedRoute,
    private location: Location,
    private messageService: MessageService,
    private desService: DestinationsService
  ) {
    // this.destiations = [
    //   { name: 'Đi từ Hà Tiên đến Phú Quốc', code: 'NY' },
    //   { name: 'Đi từ Hà Tiên đến đảo Cát Bà', code: 'RM' },
    //   { name: 'Đi từ Hà Tiên đến đảo Hòn Bà', code: 'LDN' },
    //   { name: 'Istanbul', code: 'IST' },
    //   { name: 'Paris', code: 'PRS' },
    // ];
  }

  ngOnInit(): void {
    this._initForm();
    this._checkEditMode();
    this._mapTourStatus();
    this.desService.getDestinations('1').subscribe((desRes) => {
      // console.log(desRes);
      this.destiations = desRes.data;
    });
    setTimeout(() => {
      this.loading = false;
    }, 500);
  }
  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.ListDes, event.previousIndex, event.currentIndex);
    // console.log(this.ListDes);
    this.ListDesID = [];
    this.ListDes.map((destinationID) => {
      this.ListDesID.push(destinationID.id);
    });
    console.log(this.ListDesID);
  }

  onChange() {
    // console.log(this.selectedDes);
    const arrDeS: any[] = [];
    this.selectedDes.map((desSelect: any) => {
      arrDeS.push(desSelect);
    });
    console.log(arrDeS);
    this.ListDes = arrDeS;
    this.ListDesID = [];
    arrDeS.map((res) => {
      this.ListDesID.push(res.id);
    });
    console.log(this.ListDesID);
  }
  onFileChanged(event: any) {
    const file = event.target.files[0];
    // const fileExtension = '.' + event.target.files[0].name.split('.').pop();
    // event.target.files[0].name =
    //   Math.random().toString(36).substring(7) +
    //   new Date().getTime() +
    //   fileExtension;
    // event.target.files[0].name = 'sang' + fileExtension;
    // console.log();

    if (file) {
      this.form.patchValue({ image: file });
      // this.form.get('image').updateValueAndValidity();
      const fileReader = new FileReader();
      fileReader.onload = () => {
        this.previewImage = fileReader.result;
      };
      fileReader.readAsDataURL(file);
      const formData = new FormData();
      formData.append(
        'ImageFile',
        file,
        Math.random().toString(36).substring(7) +
          new Date().getTime() +
          file.name
      );
      this.tourService
        .uploadTourImage(formData)
        .subscribe((res: HttpEvent<any>) => {
          // console.log(this.imageLink);
          console.log(res);

          switch (res.type) {
            case HttpEventType.Sent:
              // console.log('Request has been made!');
              break;
            case HttpEventType.ResponseHeader:
              // console.log('Response header has been received!');
              break;
            case HttpEventType.UploadProgress:
              // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
              this.progress = Math.round((res.loaded / res.total!) * 100);
              // console.log(`Uploaded! ${this.progress}%`);
              break;
            case HttpEventType.Response:
              // console.log('User successfully created!', res.body);
              this.messageService.add({
                severity: 'success',
                summary: 'Upload hình thành công',
              });
              this.imageLink = res.body.data;
              console.log(res.body.data);

              setTimeout(() => {
                this.progress = 0;
              }, 1500);
          }
        });
    }
  }
  _checkEditMode() {
    this.route.params.subscribe((params) => {
      if (params.id) {
        this.currentTour = params.id;
        this.editMode = true;
        this.tourService.getTour(this.currentTour).subscribe((res) => {
          this.tourForm.id.setValue(res.data.id);
          this.tourForm.tittle.setValue(res.data.tittle);
          this.tourForm.status.setValue(res.data.status.toString());
          this.tourForm.descriptions.setValue(res.data.descriptions);
          this.previewImage = res.data.imageLink;
          // console.log(this.previewImage);
        });
        this.desService
          .getDestinationTours(this.currentTour)
          .subscribe((desti: any) => {
            desti.data.map((res: any) => {
              this.ListDes.push({
                id: res.idDestination,
                name: res.idDestinationNavigation.name,
              });
              this.ListDesID.push(res.idDestination);
              console.log(this.ListDesID);

              // this.selectedDes = res.idDestination;
              // console.log('selected Des', this.selectedDes);
              // console.log(this.ListDes);
            });
          });
      }
    });
  }
  _initForm() {
    this.form = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      tittle: ['', [Validators.required]],
      descriptions: ['', Validators.required],
      status: [''],
    });
  }
  _mapTourStatus() {
    this.status = Object.keys(AGENCY_STATUS).map((key) => {
      return {
        id: key,
        name: AGENCY_STATUS[key].lable,
      };
    });
  }
  onSubmit() {
    this.isSubmit = true;
    if (this.form.invalid && this.form.hasValidator) {
      return;
    }

    if (this.editMode !== true) {
      const tour: Tour = {
        tittle: this.tourForm.tittle.value,
        status: '1',
        descriptions: this.tourForm.descriptions.value,
        imageLink: this.imageLink,
      };
      // console.log(tour);

      this.tourService.createTour(tour).subscribe(
        async (res) => {
          console.log(res.data);

          await this.desService
            .createDesTour(res.data, this.ListDesID)
            .subscribe((res) => {});
          this.messageService.add({
            severity: 'success',
            summary: 'Update tour successfull',
          });
          timer(500)
            .toPromise()
            .then(() => {
              this.location.back();
            });
        },
        (error) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Some thing wrong!',
          });
          timer(500)
            .toPromise()
            .then(() => {
              this.location.back();
            });
        }
      );
    } else {
      const tour: Tour = {
        tittle: this.tourForm.tittle.value,
        status: this.tourForm.status.value,
        descriptions: this.tourForm.descriptions.value,
        imageLink: this.imageLink,
      };
      console.log(tour);
      this.desService
        .createDesTour(this.currentTour, this.ListDesID)
        .subscribe((res) => {
          // console.log(res);
        });
      this.tourService.updateTour(tour, this.currentTour).subscribe(
        (res) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Update tour successfull',
          });
          timer(500)
            .toPromise()
            .then(() => {
              this.location.back();
            });
        },
        (error) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Some thing wrong!',
          });
          timer(500)
            .toPromise()
            .then(() => {
              this.location.back();
            });
        }
      );
    }
  }
  // removeListDes(i: number, id: any) {
  //   // console.log(i, id);

  //   this.ListDes.splice(i, 1);
  //   this.ListDesID = [];
  //   this.ListDes.map((listDesRes) => {
  //     this.ListDesID.push(listDesRes.id);
  //   });
  //   console.log(this.ListDesID);

  //   if (!this.editMode) {
  //     this.selectedDes.splice(id, 1);
  //   }
  //   // console.log(this.selectedDes);
  // }
  onCancle() {
    this.location.back();
  }
  get tourForm() {
    return this.form.controls;
  }
}
