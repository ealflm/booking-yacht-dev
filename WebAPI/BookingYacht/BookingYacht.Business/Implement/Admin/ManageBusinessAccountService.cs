using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;
using BookingYacht.Business.PaymentModels;
using BookingYacht.Data.Models;
using Microsoft.Extensions.Configuration;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace BookingYacht.Business.Implement.Admin
{
    public class ManageBusinessAccountService : BaseService, IManageBusinessAccountService
    {

        private readonly IConfiguration _configuration;
        private readonly FirebaseApp _firebaseApp;
        private readonly FirebaseAuth _firebaseAuth;

        public ManageBusinessAccountService(IUnitOfWork unitOfWork,
            IConfiguration configuration,
            FirebaseApp firebaseApp) : base(unitOfWork)
        {
            _configuration = configuration;
            _firebaseApp = firebaseApp;
            _firebaseAuth = FirebaseAuth.GetAuth(_firebaseApp);
        }



        #region Authorization

        public async Task<MessageResult> Login(LoginSearchModel loginModel)
        {
            MessageResult model = null;
            string result = null;
            string message;

            if (!string.IsNullOrEmpty(loginModel.EmailAddress) && !string.IsNullOrEmpty(loginModel.Password))
            {
                model = await LoginEmailPassword(loginModel.EmailAddress, loginModel.Password);
            }

            if (model != null && model.Data != null)
            {
                result = GetToken((BusinessViewModel)model.Data);
            }

            message = model.Message;

            return new MessageResult(result, message);
        }

        private async Task<MessageResult> LoginEmailPassword(string email, string password)
        {
            AdminViewModel result = null;
            string message = null;
            var user = await _unitOfWork.BusinessRepository.Query()
                .Where(x => x.EmailAddress == email && x.Password != null)
                .FirstOrDefaultAsync();

            if (user != null && VerifyPassword(password, user.Password, user.Salt))
            {
                if (user.Status == 1)
                {
                    result = new AdminViewModel()
                    {
                        Id = user.Id,
                        Uid = user.Uid,
                        Name = user.Name,
                        EmailAddress = user.EmailAddress,
                        //Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        PhotoUrl = user.PhotoUrl,
                        Status = user.Status
                    };
                }
                else
                {
                    message = "The user doesn't have permission to access this resource";
                }
            }
            else
            {
                message = "Invalid user name or password";
            }

            return new MessageResult(result, message);
        }


        public async Task<MessageResult> OpenLogin(OpenLoginSearchModel loginModel)
        {
            MessageResult model = null;
            string token = null;
            string message;

            if (!string.IsNullOrEmpty(loginModel.IdToken))
            {
                model = await LoginGoogle(loginModel.IdToken);
            }

            if (model != null && model.Data != null)
            {
                token = GetToken((BusinessViewModel)model.Data);
            }
            message = model.Message;

            return new MessageResult(token, message);
        }

        private async Task<MessageResult> LoginGoogle(string idToken)
        {
            AdminViewModel result = null;
            string message = null;

            string uid = null;
            try
            {
                var decodedToken = await _firebaseAuth
                    .VerifyIdTokenAsync(idToken);
                uid = decodedToken.Uid;
            }
            catch
            {
            }

            if (uid != null)
            {
                var user = await _unitOfWork.BusinessRepository.Query()
                    .Where(x => x.Uid == uid)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    if (user.Status == 1)
                    {
                        result = new AdminViewModel()
                        {
                            Id = user.Id,
                            Uid = user.Uid,
                            Name = user.Name,
                            EmailAddress = user.EmailAddress,
                            //Password = user.Password,
                            PhoneNumber = user.PhoneNumber,
                            PhotoUrl = user.PhotoUrl,
                            Status = user.Status
                        };
                    }
                    else
                    {
                        message = "The user doesn't have permission to access this resource";
                    }
                }
                else
                {
                    var adminModel = await GetUserInfo(uid);
                    var status = 0;

                    var model = await _unitOfWork.BusinessRepository.Query()
                    .Where(x => x.EmailAddress == adminModel.EmailAddress)
                    .FirstOrDefaultAsync();

                    if (model != null)
                    {
                        model.Uid = adminModel.Uid;
                        model.Name = adminModel.Name;
                        model.EmailAddress = adminModel.EmailAddress;
                        model.PhoneNumber = adminModel.PhoneNumber;
                        model.PhotoUrl = adminModel.PhotoUrl;
                        status = model.Status;

                        _unitOfWork.BusinessRepository.Update(model);
                    }
                    else
                    {
                        await _unitOfWork.BusinessRepository.Add(adminModel);
                    }

                    if (status != 1)
                    {
                        message = "The user doesn't have permission to access this resource";
                    }
                    else
                    {
                        result = new AdminViewModel()
                        {
                            Id = model.Id,
                            Uid = model.Uid,
                            Name = model.Name,
                            EmailAddress = model.EmailAddress,
                            //Password = model.Password,
                            PhoneNumber = model.PhoneNumber,
                            PhotoUrl = model.PhotoUrl,
                            Status = model.Status
                        };
                    }

                    await _unitOfWork.SaveChangesAsync();
                }
            }
            else
            {
                message = "Invalid ID token";
            }

            return new MessageResult(result, message);
        }

        private async Task<Data.Models.Business> GetUserInfo(string uid)
        {
            var userInfo = await _firebaseAuth.GetUserAsync(uid);

            var model = new Data.Models.Business
            {
                Uid = userInfo.Uid,
                Name = userInfo.DisplayName,
                EmailAddress = userInfo.Email,
                PhoneNumber = userInfo.PhoneNumber,
                PhotoUrl = userInfo.PhotoUrl,
                Status = 1
            };

            return model;
        }


        private string GetToken(BusinessViewModel model)
        {
            var authClaims = new List<Claim>
            {
                new Claim("Id", model.Id.ToString()),
                new Claim("Name", model.Name ?? ""),
                new Claim("EmailAdress", model.EmailAddress ?? ""),
                new Claim("PhoneNumber", model.PhoneNumber ?? ""),
                new Claim("Role", model.Status == 1 ? "admin" : "non-admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Guid?> Register(RegisterSearchModel model)
        {
            Guid? result = null;
            bool isExist = await _unitOfWork.BusinessRepository.Query()
                                    .AnyAsync(x => x.EmailAddress == model.EmailAddress);
            if (!isExist)
            {
                CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var admin = new Data.Models.Business
                {
                    Name = model.Name,
                    EmailAddress = model.EmailAddress,
                    Password = passwordHash,
                    Salt = passwordSalt,
                    Status = 1,
                };

              //  if (model.EmailAddress.Contains("@bookingyacht.site")) admin.Status = 1;

                await _unitOfWork.BusinessRepository.Add(admin);
                await _unitOfWork.SaveChangesAsync();
                result = admin.Id;
            }
            return result;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i]) return false;
            }
            return true;
        }

        #endregion




        public async Task<Guid> AddBusiness(BusinessViewModel model)
        {
            var business = new Data.Models.Business()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                Status = model.Status,
                VnpTmnCode= "104S9O6F",
                VnpHashSecret= "WAIHCILWTDOAERGSTKMUYIRDGOCROIHW"
            };
            business.Status = (int)Status.ENABLE;
            await _unitOfWork.BusinessRepository.Add(business);
            await _unitOfWork.SaveChangesAsync();
            return business.Id;
        }

        public async Task DeleteBusiness(Guid id)
        {
            var business = await _unitOfWork.BusinessRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new Data.Models.Business()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status,
                    VnpTmnCode = x.VnpTmnCode,
                    VnpHashSecret = x.VnpHashSecret
                }).FirstOrDefaultAsync();
            business.Status =(int) Status.DISABLE;
            _unitOfWork.BusinessRepository.Update(business);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<BusinessViewModel> GetBusiness(Guid id)
        {
            var business = await _unitOfWork.BusinessRepository.Query()
                .Where(x=> x.Id.Equals(id))
                .Select(x => new BusinessViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status,
                    VnpTmnCode= x.VnpTmnCode,
                    VnpHashSecret=x.VnpHashSecret
                }).FirstOrDefaultAsync();
            return business;
        }

        public async Task<List<BusinessViewModel>> SearchBusinesses(BusinessSearchModel model=null)
        {
            if(model== null)
            {
                model = new BusinessSearchModel();
            }
            var businesses = await _unitOfWork.BusinessRepository.Query()
                .Where(x => model.Name == null | x.Name.Contains(model.Name))
                .Where(x => model.PhoneNumber == null | x.PhoneNumber.Contains(model.PhoneNumber))
                .Where(x => model.Address == null | x.Address.Contains(model.Address))
                .Where(x => model.EmailAddress == null | x.EmailAddress.Contains(model.EmailAddress))
                .Where(x => model.Status==Status.ALL|x.Status == (int)model.Status)
                .Select(x => new BusinessViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status
                })
                .OrderBy(x => x.Name)
                .Skip(model.AmountItem * ((model.Page!=0)?(model.Page-1):model.Page))
                .Take((model.Page!=0)? model.AmountItem: _unitOfWork.BusinessRepository.Query().Count())
                .ToListAsync();
            return businesses;
        }

        public async Task UpdateBusiness(Guid id, BusinessViewModel model)
        {
            var business = new Data.Models.Business()
            {
                Id = id,
                Name = model.Name,
                Address = model.Address,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                Status = model.Status,
                VnpTmnCode= "104S9O6F",
                VnpHashSecret= "WAIHCILWTDOAERGSTKMUYIRDGOCROIHW"
            };
            _unitOfWork.BusinessRepository.Update(business);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task<List<BusinessPaymentModel>> GetPayment(PaymentSearchModel model)
        {
            var businesses = await _unitOfWork.BusinessRepository.Query()
                .Where(x => x.Status != (int)Status.DISABLE)
                .Select(x => new BusinessPaymentModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status,
                    VnpHashSecret = x.VnpHashSecret,
                    VnpTmnCode = x.VnpTmnCode,
                    BusinessTours = _unitOfWork.BusinessTourRepository.Query()
                    .Where(y => y.IdBusiness.Equals(x.Id))
                    .Select(z => new BusinessTourPaymentModel()
                    {
                        Id=z.Id,
                        IdBusiness=z.IdBusiness,
                        IdTour=z.IdTour,
                        Status=z.Status,
                        Tour= _unitOfWork.TourRepository.Query()
                        .Where(t=>t.Id.Equals(z.IdTour))
                        .Select(t=> new Tour() 
                        {
                            Id=t.Id, 
                            Descriptions=t.Descriptions,
                            Title=t.Title, 
                            ImageLink= t.ImageLink, 
                            Status=t.Status 
                        }).FirstOrDefault(),
                        Trips= _unitOfWork.TripRepository.Query()
                .Where(t=> t.IdBusinessTour.Equals(z.Id))
                .Where(t=> t.Time.Year.Equals(model.DateTime.Year) && t.Time.Month.Equals(model.DateTime.Month))
                .Select(t => new TripPaymentModel()
                {
                    Id = t.Id,
                    Time = t.Time,
                    IdBusinessTour = t.IdBusinessTour,
                    IdVehicle = t.IdVehicle,
                    Status = t.Status,
                    AmountTicket = t.AmountTicket,
                    Orders = _unitOfWork.OrderRepository.Query()
                    .Where(v=> v.IdTrip.Equals(t.Id))
                    .Where(v=> v.Status==(int)Status.COMPLETELY_PAYMENT)
                    .Select(v=> new OrderPaymentModel() 
                    {
                        Id=v.Id,
                        AgencyName=v.AgencyName,
                        DateOrder=v.DateOrder,
                        IdAgency=v.IdAgency,
                        IdTrip = v.IdTrip,
                        QuantityOfPerson=v.QuantityOfPerson,
                        Status= v.Status,
                        TotalPrice=v.TotalPrice,
                        Tickets= _unitOfWork.TicketRepository.Query().Include(u=> u.IdTicketTypeNavigation).Where(u=> u.IdOrder.Equals(v.Id)).ToList()
                    }).ToList()
        })
                .OrderBy(t => t.Time)
                .ToList()
        }).OrderBy(z => z.IdTour).ToList()
                })
                .OrderBy(x => x.Name)
                .Skip(model.AmountItem * ((model.Page != 0) ? (model.Page - 1) : model.Page))
                .Take((model.Page != 0) ? model.AmountItem : _unitOfWork.BusinessRepository.Query().Count())
                .ToListAsync();
            foreach(BusinessPaymentModel business in businesses)
            {
                double businessTotalPrice = 0;
                for (int j=0; j < business.BusinessTours.Count;)
                {
                   
                    double businessTourTotalPrice = 0;
                    for(int i=0; i< business.BusinessTours[j].Trips.Count; )
                    {
                        
                        double tripTotalPrice = 0;
                        foreach(OrderPaymentModel order in business.BusinessTours[j].Trips[i].Orders)
                        {
                            double orderTotalPrice = 0;
                            foreach(Ticket ticket in order.Tickets)
                            {
                                orderTotalPrice += ticket.Price * (100 - ticket.IdTicketTypeNavigation.ServiceFeePercentage.Value - ticket.IdTicketTypeNavigation.CommissionFeePercentage.Value) / 100;
                            }
                            tripTotalPrice += orderTotalPrice;
                        }
                        business.BusinessTours[j].Trips[i].TotalPrice = tripTotalPrice;
                        businessTourTotalPrice += business.BusinessTours[j].Trips[i].TotalPrice;
                        if (business.BusinessTours[j].Trips[i].Orders.Count == 0)
                        {
                            business.BusinessTours[j].Trips.RemoveAt(i);

                        }
                        else
                        {
                            i++;
                        }
                    }
                    business.BusinessTours[j].TotalPrice = businessTourTotalPrice;
                    businessTotalPrice += business.BusinessTours[j].TotalPrice;
                    if (business.BusinessTours[j].Trips.Count == 0)
                    {
                        business.BusinessTours.RemoveAt(j);
                    }
                    else
                    {
                        j++;
                    }                    
                }
                business.TotalPrice = businessTotalPrice;

            }
            return businesses;
        }





        public async Task<BusinessPaymentModel> GetPaymentById(Guid id , PaymentSearchModel model)
        {
            var business = await _unitOfWork.BusinessRepository.Query()
                .Where(x=> x.Status!=(int) Status.DISABLE)
                .Where(x=> x.Id.Equals(id))
                .Select(x => new BusinessPaymentModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status,
                    VnpHashSecret = x.VnpHashSecret,
                    VnpTmnCode = x.VnpTmnCode,
                    BusinessTours = _unitOfWork.BusinessTourRepository.Query()
                    .Where(y => y.IdBusiness.Equals(x.Id))
                    .Select(z => new BusinessTourPaymentModel()
                    {
                        Id = z.Id,
                        IdBusiness = z.IdBusiness,
                        IdTour = z.IdTour,
                        Status = z.Status,
                        Tour = _unitOfWork.TourRepository.Query()
                        .Where(t => t.Id.Equals(z.IdTour))
                        .Select(t => new Tour()
                        {
                            Id = t.Id,
                            Descriptions = t.Descriptions,
                            Title = t.Title,
                            ImageLink = t.ImageLink,
                            Status = t.Status
                        }).FirstOrDefault(),
                        Trips = _unitOfWork.TripRepository.Query()
                .Where(t => t.IdBusinessTour.Equals(z.Id))
                .Where(t => t.Time.Year.Equals(model.DateTime.Year) && t.Time.Month.Equals(model.DateTime.Month))
                .Select(t => new TripPaymentModel()
                {
                    Id = t.Id,
                    Time = t.Time,
                    IdBusinessTour = t.IdBusinessTour,
                    IdVehicle = t.IdVehicle,
                    Status = t.Status,
                    AmountTicket = t.AmountTicket,
                    Orders = _unitOfWork.OrderRepository.Query()
                    .Where(v => v.IdTrip.Equals(t.Id))
                    .Select(v => new OrderPaymentModel()
                    {
                        Id = v.Id,
                        AgencyName = v.AgencyName,
                        DateOrder = v.DateOrder,
                        IdAgency = v.IdAgency,
                        IdTrip = v.IdTrip,
                        QuantityOfPerson = v.QuantityOfPerson,
                        Status = v.Status,
                        TotalPrice = v.TotalPrice,
                        Tickets = _unitOfWork.TicketRepository.Query().Include(u => u.IdTicketTypeNavigation).Where(u => u.IdOrder.Equals(v.Id)).ToList()
                    }).ToList()
                })
                .OrderBy(t => t.Time)
                .ToList()
                    }).OrderBy(z => z.IdTour).ToList()
                }).FirstOrDefaultAsync();

            double businessTotalPrice = 0;
            for (int j = 0; j < business.BusinessTours.Count;)
            {

                double businessTourTotalPrice = 0;
                for (int i = 0; i < business.BusinessTours[j].Trips.Count;)
                {

                    double tripTotalPrice = 0;
                    foreach (OrderPaymentModel order in business.BusinessTours[j].Trips[i].Orders)
                    {
                        double orderTotalPrice = 0;
                        foreach (Ticket ticket in order.Tickets)
                        {
                            orderTotalPrice += ticket.Price * (100 - ticket.IdTicketTypeNavigation.ServiceFeePercentage.Value - ticket.IdTicketTypeNavigation.CommissionFeePercentage.Value) / 100;
                        }
                        tripTotalPrice += orderTotalPrice;
                    }
                    business.BusinessTours[j].Trips[i].TotalPrice = tripTotalPrice;
                    businessTourTotalPrice += business.BusinessTours[j].Trips[i].TotalPrice;
                    if (business.BusinessTours[j].Trips[i].Orders.Count == 0)
                    {
                        business.BusinessTours[j].Trips.RemoveAt(i);

                    }
                    else
                    {
                        i++;
                    }
                }
                business.BusinessTours[j].TotalPrice = businessTourTotalPrice;
                businessTotalPrice += business.BusinessTours[j].TotalPrice;
                if (business.BusinessTours[j].Trips.Count == 0)
                {
                    business.BusinessTours.RemoveAt(j);
                }
                else
                {
                    j++;
                }
            }
            business.TotalPrice = businessTotalPrice;
            return business;
        }

    }
}
