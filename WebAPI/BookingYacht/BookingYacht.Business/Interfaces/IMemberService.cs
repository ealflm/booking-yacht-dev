using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingYacht.Business.Interfaces
{
    public interface IMemberService
    {
        Task<List<MemberViewModel>> GetMembers();
        Task<List<MemberViewModel>> GetMembersBySql();
        Task<List<MemberViewModel>> SearchMembers(MemberSearchModel model);
        Task<MemberViewModel> GetMember(string code);
        Task<Guid> AddMember(MemberViewModel model);
        Task UpdateMember(Guid id, MemberViewModel model);
        Task DeleteMember(Guid id);
    }
}
