using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.Implement.Admin
{
    public class AdminService : BaseService, IAdminService
    {
        private readonly IConfiguration _configuration;
        private readonly FirebaseApp _firebaseApp;

        public AdminService(IUnitOfWork unitOfWork,
            IConfiguration configuration,
            FirebaseApp firebaseApp) : base(unitOfWork)
        {
            _configuration = configuration;
            _firebaseApp = firebaseApp;
        }

        public async Task<string> Login(LoginSearchModel model)
        {
            bool ok = false;

            if (!string.IsNullOrEmpty(model.IdToken))
            {
                ok = await LoginGoogle(model.IdToken);
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(model.EmailAddress) && !string.IsNullOrEmpty(model.Password))
                {
                    ok = await LoginEmailPassword(model.EmailAddress, model.Password);
                }
            }

            return ok ? GetToken(model) : null;
        }

        private async Task<bool> LoginEmailPassword(string email, string password)
        {
            var user = await _unitOfWork.AdminRepository.Query()
                .Where(x => x.EmailAddress == email && x.Password == password && x.Status == 1)
                .FirstOrDefaultAsync();
            return user != null;
        }

        private async Task<bool> LoginGoogle(string idToken)
        {
            bool result = false;
            var firebaseAuth = FirebaseAuth.GetAuth(_firebaseApp);

            string uid = null;
            try {
                var decodedToken = await firebaseAuth
                    .VerifyIdTokenAsync(idToken);
                uid = decodedToken.Uid;
            } catch
            {
            }

            if (uid != null) {
                var user = await _unitOfWork.AdminRepository.Query()
                    .Where(x => x.Uid == uid && x.Status == 1)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    result = true;
                } else
                {
                    var userInfo = await firebaseAuth.GetUserAsync(uid);

                    var model = new Data.Models.Admin()
                    {
                        Uid = userInfo.Uid,
                        Name = userInfo.DisplayName,
                        EmailAddress = userInfo.Email,
                        PhoneNumber = userInfo.PhoneNumber,
                        PhotoUrl = userInfo.PhotoUrl,
                        Status = 0
                    };

                    await _unitOfWork.AdminRepository.Add(model);
                    await _unitOfWork.SaveChangesAsync();
                }
            }

            return result;
        }


        private string GetToken(LoginSearchModel model)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.EmailAddress),
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

        public async Task<bool> Register(RegisterSearchModel model)
        {
            bool result = false;
            bool isExist = await _unitOfWork.AdminRepository.Query()
                                    .AnyAsync(x => x.EmailAddress == model.EmailAddress);
            if (!isExist)
            {
                var admin = new Data.Models.Admin()
                {
                    EmailAddress = model.EmailAddress,
                    Password = model.Password
                };

                await _unitOfWork.AdminRepository.Add(admin);
                await _unitOfWork.SaveChangesAsync();
                result = true;
            }
            return result;
        }
    }
}
