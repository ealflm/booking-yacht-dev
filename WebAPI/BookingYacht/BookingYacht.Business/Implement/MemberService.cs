using BookingYacht.Business.Interfaces;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingYacht.Business.Implement
{
    public class MemberService : BaseService, IMemberService
    {
        public MemberService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<MemberViewModel>> GetMembers()
        {
            Console.WriteLine("I'm here");
            var members = await _unitOfWork.MemberRepository
                .Query()
                .Select(x => new MemberViewModel
            {
                
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Age = x.Age,
                Phone = x.Phone,
                Address = x.Address
            }).ToListAsync();

            return members;
        }

        public async Task<List<MemberViewModel>> GetMembersBySql()
        {
            string sql = "SELECT Id, Code, Name, Age, Phone, Address FROM Member";
            var members = await _unitOfWork.MemberRepository.Query().FromSqlRaw(sql)
            .Select(x => new MemberViewModel
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Age = x.Age,
                Phone = x.Phone,
                Address = x.Address
            }).ToListAsync();

            return members;
        }

        public async Task<List<MemberViewModel>> SearchMembers(MemberSearchModel model)
        {
            var members = await _unitOfWork.MemberRepository.Query()
                .Where(x => model.Code == null | x.Code.Contains(model.Code))
                .Where(x => model.Name == null | x.Name.Contains(model.Name))
                .Where(x => model.Age == null | x.Age == model.Age)
                .Where(x => model.Phone == null | x.Phone.Contains(model.Phone))
                .Where(x => model.Address == null | x.Address.Contains(model.Address))
                .Select(x => new MemberViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Age = x.Age,
                    Phone = x.Phone,
                    Address = x.Address
                }).ToListAsync();

            return members;
        }

        public async Task<MemberViewModel> GetMember(string code)
        {
            var member = await _unitOfWork.MemberRepository.Query()
                .Where(x => x.Code.Equals(code))
                .Select(x => new MemberViewModel()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Age = x.Age,
                    Phone = x.Phone,
                    Address = x.Address
                }).FirstOrDefaultAsync();

            return member;
        }

        public async Task<Guid> AddMember(MemberViewModel model)
        {
            var member = new Member()
            {
                Id = model.Id,
                Code = model.Code,
                Name = model.Name,
                Age = model.Age,
                Phone = model.Phone,
                Address = model.Address
            };

            await _unitOfWork.MemberRepository.Add(member);
            await _unitOfWork.SaveChangesAsync();

            return member.Id;
        }

        public async Task UpdateMember(Guid id, MemberViewModel model)
        {
            var member = new Member()
            {
                Id = id,
                Code = model.Code,
                Name = model.Name,
                Age = model.Age,
                Phone = model.Phone,
                Address = model.Address
            };

            _unitOfWork.MemberRepository.Update(member);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMember(Guid id)
        {
            await _unitOfWork.MemberRepository.Remove(id);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
