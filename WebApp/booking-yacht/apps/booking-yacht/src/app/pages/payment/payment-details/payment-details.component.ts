import * as moment from 'moment';
import { PaymentService } from './../../../services/payment.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { BusinessAccountService } from '../../../services/business-account.service';

@Component({
  selector: 'booking-yacht-payment-details',
  templateUrl: './payment-details.component.html',
  styleUrls: ['./payment-details.component.scss'],
})
export class PaymentDetailsComponent implements OnInit {
  currentID = '';
  paymentDetails: any = {
    // id: '68554b5a-817b-453c-992c-149662a8e710',
    // name: 'Nguyễn Lê Mẫn Đạt',
    // phoneNumber: '0912387464',
    // emailAddress: 'nguyenlemandat@gmail.com',
    // address: 'Vĩnh Long',
    // status: 1,
    // vnpTmnCode: '104S9O6F',
    // vnpHashSecret: 'WAIHCILWTDOAERGSTKMUYIRDGOCROIHW',
    // businessTours: [
    //   {
    //     id: 'ff13d778-3a98-42a2-9a86-f767c743cefc',
    //     idBusiness: '68554b5a-817b-453c-992c-149662a8e710',
    //     idTour: 'e8495fe1-8121-42ec-b36e-0b13ee3ad5a9',
    //     status: 1,
    //     tour: {
    //       id: 'e8495fe1-8121-42ec-b36e-0b13ee3ad5a9',
    //       title: 'Tour 6 đảo',
    //       descriptions:
    //         '<p>Tour 6 đảo bao gồm: hòn Thơm, hòn Rơi, hòn Vang, hòn Dăm Trong, hòn Dăm Ngoài, hòn Mây Rút</p>',
    //       status: 1,
    //       imageLink:
    //         'https://swd3915.blob.core.windows.net/images/c8t3c31635382191710tour1.jpg',
    //     },
    //     trips: [
    //       {
    //         id: '105f0c2e-199b-46a9-8598-18a09572c2df',
    //         time: '2021-11-25T10:00:00',
    //         idBusinessTour: 'ff13d778-3a98-42a2-9a86-f767c743cefc',
    //         idVehicle: '193e3b50-e809-4c92-a65c-9ad4bc4ebc63',
    //         status: 1,
    //         amountTicket: 5,
    //         orders: [
    //           {
    //             id: '0f5c566a-ec82-48f7-b01d-8a4daa3a6128',
    //             agencyName: 'Tran Gia Nguyen',
    //             quantityOfPerson: 4,
    //             totalPrice: 20000000,
    //             idAgency: '48f1c115-b9da-49ba-8fb4-4494fee4da44',
    //             status: 9,
    //             dateOrder: '2021-11-03T00:00:00',
    //             idTrip: '105f0c2e-199b-46a9-8598-18a09572c2df',
    //             tickets: [
    //               {
    //                 id: '3d50fa3e-fa13-4378-b8b1-abe454fe8f22',
    //                 nameCustomer: 'Nguyễn Văn A',
    //                 phone: '0123452025',
    //                 idOrder: '0f5c566a-ec82-48f7-b01d-8a4daa3a6128',
    //                 idTicketType: 'b53965e1-b610-42a3-ba44-f16bb38cf24f',
    //                 idTrip: '105f0c2e-199b-46a9-8598-18a09572c2df',
    //                 price: 5000000,
    //                 status: 6,
    //                 idOrderNavigation: null,
    //                 idTicketTypeNavigation: {
    //                   id: 'b53965e1-b610-42a3-ba44-f16bb38cf24f',
    //                   name: 'Vé V.I.P',
    //                   price: 5000000,
    //                   status: 5,
    //                   commissionFeePercentage: 15,
    //                   serviceFeePercentage: 5,
    //                   idBusinessTour: 'ff13d778-3a98-42a2-9a86-f767c743cefc',
    //                   idBusinessTourNavigation: null,
    //                 },
    //                 idTripNavigation: null,
    //               },
    //               {
    //                 id: 'f6536868-6e7d-4717-85fd-b065f619d0b8',
    //                 nameCustomer: 'Trần Gia C',
    //                 phone: '0123452027',
    //                 idOrder: '0f5c566a-ec82-48f7-b01d-8a4daa3a6128',
    //                 idTicketType: 'b53965e1-b610-42a3-ba44-f16bb38cf24f',
    //                 idTrip: '105f0c2e-199b-46a9-8598-18a09572c2df',
    //                 price: 5000000,
    //                 status: 6,
    //                 idOrderNavigation: null,
    //                 idTicketTypeNavigation: {
    //                   id: 'b53965e1-b610-42a3-ba44-f16bb38cf24f',
    //                   name: 'Vé V.I.P',
    //                   price: 5000000,
    //                   status: 5,
    //                   commissionFeePercentage: 15,
    //                   serviceFeePercentage: 5,
    //                   idBusinessTour: 'ff13d778-3a98-42a2-9a86-f767c743cefc',
    //                   idBusinessTourNavigation: null,
    //                 },
    //                 idTripNavigation: null,
    //               },
    //               {
    //                 id: '2779bbf4-8337-4303-8438-b7885ed758ab',
    //                 nameCustomer: 'Dương Thanh B',
    //                 phone: '0123452026',
    //                 idOrder: '0f5c566a-ec82-48f7-b01d-8a4daa3a6128',
    //                 idTicketType: 'b53965e1-b610-42a3-ba44-f16bb38cf24f',
    //                 idTrip: '105f0c2e-199b-46a9-8598-18a09572c2df',
    //                 price: 5000000,
    //                 status: 6,
    //                 idOrderNavigation: null,
    //                 idTicketTypeNavigation: {
    //                   id: 'b53965e1-b610-42a3-ba44-f16bb38cf24f',
    //                   name: 'Vé V.I.P',
    //                   price: 5000000,
    //                   status: 5,
    //                   commissionFeePercentage: 15,
    //                   serviceFeePercentage: 5,
    //                   idBusinessTour: 'ff13d778-3a98-42a2-9a86-f767c743cefc',
    //                   idBusinessTourNavigation: null,
    //                 },
    //                 idTripNavigation: null,
    //               },
    //             ],
    //           },
    //         ],
    //         totalPrice: 12000000,
    //       },
    //       {
    //         id: '105f0c2e-199b-46a9-8598-18a09572c2df',
    //         time: '2021-11-25T10:00:00',
    //         idBusinessTour: 'ff13d778-3a98-42a2-9a86-f767c743cefc',
    //         idVehicle: '193e3b50-e809-4c92-a65c-9ad4bc4ebc63',
    //         status: 1,
    //         amountTicket: 5,
    //         orders: [
    //           {
    //             id: '0f5c566a-ec82-48f7-b01d-8a4daa3a6128',
    //             agencyName: 'Tran Gia Nguyen',
    //             quantityOfPerson: 4,
    //             totalPrice: 20000000,
    //             idAgency: '48f1c115-b9da-49ba-8fb4-4494fee4da44',
    //             status: 9,
    //             dateOrder: '2021-11-03T00:00:00',
    //             idTrip: '105f0c2e-199b-46a9-8598-18a09572c2df',
    //             tickets: [
    //               {
    //                 id: '3d50fa3e-fa13-4378-b8b1-abe454fe8f22',
    //                 nameCustomer: 'Nguyễn Văn A',
    //                 phone: '0123452025',
    //                 idOrder: '0f5c566a-ec82-48f7-b01d-8a4daa3a6128',
    //                 idTicketType: 'b53965e1-b610-42a3-ba44-f16bb38cf24f',
    //                 idTrip: '105f0c2e-199b-46a9-8598-18a09572c2df',
    //                 price: 5000000,
    //                 status: 6,
    //                 idOrderNavigation: null,
    //                 idTicketTypeNavigation: {
    //                   id: 'b53965e1-b610-42a3-ba44-f16bb38cf24f',
    //                   name: 'Vé V.I.P',
    //                   price: 5000000,
    //                   status: 5,
    //                   commissionFeePercentage: 15,
    //                   serviceFeePercentage: 5,
    //                   idBusinessTour: 'ff13d778-3a98-42a2-9a86-f767c743cefc',
    //                   idBusinessTourNavigation: null,
    //                 },
    //                 idTripNavigation: null,
    //               },
    //               {
    //                 id: 'f6536868-6e7d-4717-85fd-b065f619d0b8',
    //                 nameCustomer: 'Trần Gia C',
    //                 phone: '0123452027',
    //                 idOrder: '0f5c566a-ec82-48f7-b01d-8a4daa3a6128',
    //                 idTicketType: 'b53965e1-b610-42a3-ba44-f16bb38cf24f',
    //                 idTrip: '105f0c2e-199b-46a9-8598-18a09572c2df',
    //                 price: 5000000,
    //                 status: 6,
    //                 idOrderNavigation: null,
    //                 idTicketTypeNavigation: {
    //                   id: 'b53965e1-b610-42a3-ba44-f16bb38cf24f',
    //                   name: 'Vé V.I.P',
    //                   price: 5000000,
    //                   status: 5,
    //                   commissionFeePercentage: 15,
    //                   serviceFeePercentage: 5,
    //                   idBusinessTour: 'ff13d778-3a98-42a2-9a86-f767c743cefc',
    //                   idBusinessTourNavigation: null,
    //                 },
    //                 idTripNavigation: null,
    //               },
    //               {
    //                 id: '2779bbf4-8337-4303-8438-b7885ed758ab',
    //                 nameCustomer: 'Dương Thanh B',
    //                 phone: '0123452026',
    //                 idOrder: '0f5c566a-ec82-48f7-b01d-8a4daa3a6128',
    //                 idTicketType: 'b53965e1-b610-42a3-ba44-f16bb38cf24f',
    //                 idTrip: '105f0c2e-199b-46a9-8598-18a09572c2df',
    //                 price: 5000000,
    //                 status: 6,
    //                 idOrderNavigation: null,
    //                 idTicketTypeNavigation: {
    //                   id: 'b53965e1-b610-42a3-ba44-f16bb38cf24f',
    //                   name: 'Vé V.I.P',
    //                   price: 5000000,
    //                   status: 5,
    //                   commissionFeePercentage: 15,
    //                   serviceFeePercentage: 5,
    //                   idBusinessTour: 'ff13d778-3a98-42a2-9a86-f767c743cefc',
    //                   idBusinessTourNavigation: null,
    //                 },
    //                 idTripNavigation: null,
    //               },
    //             ],
    //           },
    //         ],
    //         totalPrice: 12000000,
    //       },
    //     ],
    //     totalPrice: 12000000,
    //   },
    //   {
    //     id: '21486f26-9b49-443f-a62d-662f502c175f',
    //     idBusiness: '68554b5a-817b-453c-992c-149662a8e710',
    //     idTour: '6babf42f-735d-4832-a718-bc52602a0adb',
    //     status: 1,
    //     tour: {
    //       id: '6babf42f-735d-4832-a718-bc52602a0adb',
    //       title: 'Tour Bãi Dài',
    //       descriptions:
    //         '<p>Tour Bãi Dài bao gồm các khu du lịch, nghỉ dưỡng trải dài theo bờ biển Bãi Dài như: Vinpearl Grand World Phú Quốc, VinWonders Phú Quốc, Vinpearl Safari Phú Quốc.</p>',
    //       status: 1,
    //       imageLink:
    //         'https://swd3915.blob.core.windows.net/images/kk4gb51635908771484tour2.jpg',
    //     },
    //     trips: [
    //       {
    //         id: 'cc344de2-2644-4dec-a16b-0fa76bb752f1',
    //         time: '2021-11-25T15:00:00',
    //         idBusinessTour: '21486f26-9b49-443f-a62d-662f502c175f',
    //         idVehicle: '5ce3ec04-39bb-4ce7-ac45-2647981a38bc',
    //         status: 1,
    //         amountTicket: 21,
    //         orders: [
    //           {
    //             id: '13417a52-c9ac-4b01-a196-08083e2e9083',
    //             agencyName: 'Tran Gia Nguyen',
    //             quantityOfPerson: 1,
    //             totalPrice: 15300000,
    //             idAgency: '48f1c115-b9da-49ba-8fb4-4494fee4da44',
    //             status: 9,
    //             dateOrder: '2021-10-28T00:00:00',
    //             idTrip: 'cc344de2-2644-4dec-a16b-0fa76bb752f1',
    //             tickets: [
    //               {
    //                 id: 'ae896ac3-b515-4809-8bb8-18aa5410441a',
    //                 nameCustomer: 'Tran gia nguyen a',
    //                 phone: '0378794677',
    //                 idOrder: '13417a52-c9ac-4b01-a196-08083e2e9083',
    //                 idTicketType: 'a76c78a9-f131-4cbf-beb5-4af5bab24b64',
    //                 idTrip: 'cc344de2-2644-4dec-a16b-0fa76bb752f1',
    //                 price: 3200000,
    //                 status: 7,
    //                 idOrderNavigation: null,
    //                 idTicketTypeNavigation: {
    //                   id: 'a76c78a9-f131-4cbf-beb5-4af5bab24b64',
    //                   name: 'Vé V.I.P',
    //                   price: 3200000,
    //                   status: 3,
    //                   commissionFeePercentage: 13,
    //                   serviceFeePercentage: 10,
    //                   idBusinessTour: '21486f26-9b49-443f-a62d-662f502c175f',
    //                   idBusinessTourNavigation: null,
    //                 },
    //                 idTripNavigation: null,
    //               },
    //               {
    //                 id: 'c3ed22be-0a8e-4677-b781-342281a8e41b',
    //                 nameCustomer: 'Tran gia nguyen e',
    //                 phone: '0378794671',
    //                 idOrder: '13417a52-c9ac-4b01-a196-08083e2e9083',
    //                 idTicketType: 'ed680f61-6b20-4be2-a74e-f39fac3659de',
    //                 idTrip: 'cc344de2-2644-4dec-a16b-0fa76bb752f1',
    //                 price: 2500000,
    //                 status: 6,
    //                 idOrderNavigation: null,
    //                 idTicketTypeNavigation: {
    //                   id: 'ed680f61-6b20-4be2-a74e-f39fac3659de',
    //                   name: 'Vé người lớn',
    //                   price: 2500000,
    //                   status: 5,
    //                   commissionFeePercentage: 15,
    //                   serviceFeePercentage: 5,
    //                   idBusinessTour: 'ff13d778-3a98-42a2-9a86-f767c743cefc',
    //                   idBusinessTourNavigation: null,
    //                 },
    //                 idTripNavigation: null,
    //               },
    //               {
    //                 id: 'e2f9aa13-5dc5-463a-a1b2-44b651555520',
    //                 nameCustomer: 'Tran gia nguyen d',
    //                 phone: '0378794670',
    //                 idOrder: '13417a52-c9ac-4b01-a196-08083e2e9083',
    //                 idTicketType: 'a76c78a9-f131-4cbf-beb5-4af5bab24b64',
    //                 idTrip: 'cc344de2-2644-4dec-a16b-0fa76bb752f1',
    //                 price: 3200000,
    //                 status: 7,
    //                 idOrderNavigation: null,
    //                 idTicketTypeNavigation: {
    //                   id: 'a76c78a9-f131-4cbf-beb5-4af5bab24b64',
    //                   name: 'Vé V.I.P',
    //                   price: 3200000,
    //                   status: 3,
    //                   commissionFeePercentage: 13,
    //                   serviceFeePercentage: 10,
    //                   idBusinessTour: '21486f26-9b49-443f-a62d-662f502c175f',
    //                   idBusinessTourNavigation: null,
    //                 },
    //                 idTripNavigation: null,
    //               },
    //               {
    //                 id: 'c2f690a0-9fd8-46c7-bf2d-82e042624107',
    //                 nameCustomer: 'Tran gia nguyen c',
    //                 phone: '0378794679',
    //                 idOrder: '13417a52-c9ac-4b01-a196-08083e2e9083',
    //                 idTicketType: 'a76c78a9-f131-4cbf-beb5-4af5bab24b64',
    //                 idTrip: 'cc344de2-2644-4dec-a16b-0fa76bb752f1',
    //                 price: 3200000,
    //                 status: 6,
    //                 idOrderNavigation: null,
    //                 idTicketTypeNavigation: {
    //                   id: 'a76c78a9-f131-4cbf-beb5-4af5bab24b64',
    //                   name: 'Vé V.I.P',
    //                   price: 3200000,
    //                   status: 3,
    //                   commissionFeePercentage: 13,
    //                   serviceFeePercentage: 10,
    //                   idBusinessTour: '21486f26-9b49-443f-a62d-662f502c175f',
    //                   idBusinessTourNavigation: null,
    //                 },
    //                 idTripNavigation: null,
    //               },
    //               {
    //                 id: 'f1a450a8-3def-4aaf-bab2-da1a3cc1ef4a',
    //                 nameCustomer: 'Tran gia nguyen b',
    //                 phone: '0378794678',
    //                 idOrder: '13417a52-c9ac-4b01-a196-08083e2e9083',
    //                 idTicketType: 'a76c78a9-f131-4cbf-beb5-4af5bab24b64',
    //                 idTrip: 'cc344de2-2644-4dec-a16b-0fa76bb752f1',
    //                 price: 3200000,
    //                 status: 7,
    //                 idOrderNavigation: null,
    //                 idTicketTypeNavigation: {
    //                   id: 'a76c78a9-f131-4cbf-beb5-4af5bab24b64',
    //                   name: 'Vé V.I.P',
    //                   price: 3200000,
    //                   status: 3,
    //                   commissionFeePercentage: 13,
    //                   serviceFeePercentage: 10,
    //                   idBusinessTour: '21486f26-9b49-443f-a62d-662f502c175f',
    //                   idBusinessTourNavigation: null,
    //                 },
    //                 idTripNavigation: null,
    //               },
    //             ],
    //           },
    //         ],
    //         totalPrice: 11856000,
    //       },
    //     ],
    //     totalPrice: 11856000,
    //   },
    // ],
    // totalPrice: 23856000,
  };

  constructor(
    private route: ActivatedRoute,
    private paymentService: PaymentService,
    private router: Router,
    private bussinessService: BusinessAccountService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.currentID = params.id;
    });
    this.route.queryParams.subscribe((params) => {
      const vnpOrderInfo = params.vnp_OrderInfo;
      const currentMonth = moment(new Date()).format('YYYY-MM');
      if (
        this.currentID == params.vnp_OrderInfo &&
        params.vnp_ResponseCode != 24
      ) {
        this.getBussinessID(vnpOrderInfo, currentMonth);
        this.router.navigate([
          `payment/${this.currentID}`,
          { relativeTo: this.route },
        ]);
      } else if (params.vnp_ResponseCode == 24) {
        this.router.navigate([`payment`]);
      }
    });
  }
  getBussinessID(id: string, date: string) {
    const orderID: any = [];
    this.bussinessService.getPaymentID(id, date).subscribe((res) => {
      this.paymentDetails = res.data;
      console.log(this.paymentDetails);

      res.data.businessTours.map((businessRes: any) => {
        // console.log('Business:', businessRes);
        businessRes.trips?.map((tripRes: any) => {
          // console.log('tripRes:', tripRes);
          tripRes.orders?.map((order: any) => {
            // console.log('Order:', order.id);
            orderID.push(order.id);
            console.log(orderID);
            this.paymentService
              .updateStatusOrder(this.currentID, orderID)
              .subscribe((res) => {
                console.log('Đã cập nhật trạng thái order');
              });
          });
        });
      });
    });
  }
}
