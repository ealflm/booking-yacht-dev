using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AdminModel = BookingYacht.Data.Models.Admin;

namespace BookingYacht.Business.Implement.Admin
{
    public class AdminService : BaseService, IAdminService
    {
        private readonly IConfiguration _configuration;
        private readonly FirebaseApp _firebaseApp;
        private readonly FirebaseAuth _firebaseAuth;

        public AdminService(IUnitOfWork unitOfWork,
            IConfiguration configuration,
            FirebaseApp firebaseApp) : base(unitOfWork)
        {
            _configuration = configuration;
            _firebaseApp = firebaseApp;
            _firebaseAuth = FirebaseAuth.GetAuth(_firebaseApp);
        }

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
                result = GetToken((AdminViewModel)model.Data);
            }

            message = model.Message;

            return new MessageResult(result, message);
        }

        private async Task<MessageResult> LoginEmailPassword(string email, string password)
        {
            AdminViewModel result = null;
            string message = null;
            var user = await _unitOfWork.AdminRepository.Query()
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
                        Password = user.Password,
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
                token = GetToken((AdminViewModel)model.Data);
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
                var user = await _unitOfWork.AdminRepository.Query()
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
                            Password = user.Password,
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

                    var model = await _unitOfWork.AdminRepository.Query()
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

                        _unitOfWork.AdminRepository.Update(model);
                    } else
                    {
                        await _unitOfWork.AdminRepository.Add(adminModel);
                    }

                    if (status != 1) { 
                        message = "The user doesn't have permission to access this resource";
                    } else
                    {
                        result = new AdminViewModel()
                        {
                            Id = model.Id,
                            Uid = model.Uid,
                            Name = model.Name,
                            EmailAddress = model.EmailAddress,
                            Password = model.Password,
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

        private async Task<AdminModel> GetUserInfo(string uid)
        {
            var userInfo = await _firebaseAuth.GetUserAsync(uid);

            var model = new AdminModel
            {
                Uid = userInfo.Uid,
                Name = userInfo.DisplayName,
                EmailAddress = userInfo.Email,
                PhoneNumber = userInfo.PhoneNumber,
                PhotoUrl = userInfo.PhotoUrl,
                Status = 0
            };

            return model;
        }


        private string GetToken(AdminViewModel model)
        {
            var authClaims = new List<Claim>
            {
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
            bool isExist = await _unitOfWork.AdminRepository.Query()
                                    .AnyAsync(x => x.EmailAddress == model.EmailAddress);
            if (!isExist)
            {
                CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var admin = new AdminModel
                {
                    Name = model.Name,
                    EmailAddress = model.EmailAddress,
                    Password = passwordHash,
                    Salt = passwordSalt
                };

                if (model.EmailAddress.Contains("@bookingyacht.site")) admin.Status = 1;

                await _unitOfWork.AdminRepository.Add(admin);
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
    }
}
