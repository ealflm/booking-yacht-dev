using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookingYacht.Business.Implement.Admin
{
    internal static partial class Mapper
    {

        public static async Task<AgencyViewModels> CreateEntity(Data.Models.Agency x)
        {
            return await Task.Run(() => new AgencyViewModels
            {
                Id = x.Id,
                Address = x.Address,
                EmailAddress = x.EmailAddress,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                Status = x.Status,
            });
        }
        public static async Task<Data.Models.Agency> CreateNewEntity(AgencyViewModels model)
        {
            return await Task.Run(() =>
                new Data.Models.Agency
                {
                    Address = model.Address,
                    EmailAddress = model.EmailAddress,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Status = (int) Status.ENABLE,
                }
            );
        }
        
        public static async Task<Data.Models.Agency> ModelToEntity(Guid id, AgencyViewModels model)
        {
            return await Task.Run(() =>
                new Data.Models.Agency
                {
                    Id = id,
                    Address = model.Address,
                    EmailAddress = model.EmailAddress,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Status = model.Status ?? 0
                }
            );
        }

        
    }
    public class AgencyService : BaseService, IAgencyService
    {

        private readonly IConfiguration _configuration;
        private readonly FirebaseApp _firebaseApp;
        private readonly FirebaseAuth _firebaseAuth;
        public AgencyService(IUnitOfWork unitOfWork, IConfiguration configuration,
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
                result = GetToken((AgencyViewModels)model.Data);
            }

            message = model.Message;

            return new MessageResult(result, message);
        }

        private async Task<MessageResult> LoginEmailPassword(string email, string password)
        {
            AgencyViewModels result = null;
            string message = null;
            var user = await _unitOfWork.AgencyRepository.Query()
                .Where(x => x.EmailAddress == email && x.Password != null)
                .FirstOrDefaultAsync();

            if (user != null && VerifyPassword(password, user.Password, user.Salt))
            {
                if (user.Status == 1)
                {
                    result = new AgencyViewModels()
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
                token = GetToken((AgencyViewModels)model.Data);
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
                var user = await _unitOfWork.AgencyRepository.Query()
                    .Where(x => x.Uid == uid)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    //if (user.Status == 1)
                    //{
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
                    //}
                    //else
                    //{
                        //message = "The user doesn't have permission to access this resource";
                    //}
                }
                else
                {
                    var adminModel = await GetUserInfo(uid);
                    var status = 0;

                    var model = await _unitOfWork.AgencyRepository.Query()
                    .Where(x => x.EmailAddress == adminModel.EmailAddress)
                    .FirstOrDefaultAsync();

                    if (model != null)
                    {
                        model.Uid = adminModel.Uid;
                        model.Name = adminModel.Name;
                        model.EmailAddress = adminModel.EmailAddress;
                        model.PhoneNumber = adminModel.PhoneNumber;
                        model.PhotoUrl = adminModel.PhotoUrl;
                        model.Status = 1;
                        status = 1;

                        _unitOfWork.AgencyRepository.Update(model);
                    }
                    else
                    {
                        await _unitOfWork.AgencyRepository.Add(adminModel);
                    }

                    //if (status != 1)
                    //{
                        //message = "The user doesn't have permission to access this resource";
                    //}
                    //else
                    //{
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
                    //}

                    await _unitOfWork.SaveChangesAsync();
                }
            }
            else
            {
                message = "Invalid ID token";
            }

            return new MessageResult(result, message);
        }

        private async Task<Agency> GetUserInfo(string uid)
        {
            var userInfo = await _firebaseAuth.GetUserAsync(uid);

            var model = new Agency
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


        private string GetToken(AgencyViewModels model)
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
            bool isExist = await _unitOfWork.AgencyRepository.Query()
                                    .AnyAsync(x => x.EmailAddress == model.EmailAddress);
            if (!isExist)
            {
                CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var admin = new Agency
                {
                    Name = model.Name,
                    EmailAddress = model.EmailAddress,
                    Password = passwordHash,
                    Salt = passwordSalt,
                    Status= (int)Status.ENABLE
                };

             //   if (model.EmailAddress.Contains("@bookingyacht.site")) admin.Status = 1;

                await _unitOfWork.AgencyRepository.Add(admin);
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



        public async  Task<List<AgencyViewModels>> SearchAgencies(AgencySearchModel model = null)
        {
            model ??= new AgencySearchModel();
            var agency = await _unitOfWork.AgencyRepository.Query()
                .Where(x => model.Address == null | x.Address.Contains(model.Address))
                .Where(x => model.Name == null | x.Name.Contains(model.Name))
                .Where(x => model.PhoneNumber == null | x.PhoneNumber.Contains(model.PhoneNumber))
                .Where(x => model.EmailAddress == null | x.EmailAddress.Contains(model.EmailAddress))
                .Where(x => model.Status == null | x.Status == (int)model.Status)
                .OrderBy(x => x.Name)
                .Skip(model.AmountItem * (model.Page != 0 ? model.Page - 1 : 0))
                .Take(model.Page != 0 ? model.AmountItem : _unitOfWork.AgencyRepository.Query().Count())
                .Select(x => Mapper.CreateEntity(x).Result)
                .ToListAsync();
            return agency;
        }

        public async Task<AgencyViewModels> GetAgency(Guid id)
        {
            var agency = await _unitOfWork.AgencyRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => Mapper.CreateEntity(x).Result)
                .FirstOrDefaultAsync();
            return agency;
        }

        public async Task<Guid> AddAgency(AgencyViewModels model)
        {
            var newEntity = Mapper.CreateNewEntity(model);
            var viewModel = _unitOfWork.AgencyRepository.Query().Add(newEntity.Result);
            await _unitOfWork.SaveChangesAsync();
            return viewModel.Entity.Id;
        }

        public async Task UpdateAgency(Guid id, AgencyViewModels model)
        {
            var entity = Mapper.ModelToEntity(id, model);
            _unitOfWork.AgencyRepository.Update(entity.Result);
            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task<bool> DeleteAgency(Guid id)
        {
            var first = await _unitOfWork.AgencyRepository.Query()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (first is null) return false;
            
            first.Status = (int) Status.DISABLE;
            await _unitOfWork.SaveChangesAsync();
            
            return first.Status == 2;
        }

        public async Task<int> Count()
        {
            return await _unitOfWork.Context().Agencies.CountAsync();
        }
    }

   
}