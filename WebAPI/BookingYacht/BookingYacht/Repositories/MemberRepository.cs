using BookingYacht.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingYacht.Repositories
{
    public class MemberRepository
    {
        public IEnumerable<Member> GetMembers()
        {
            using (var context = new BookingYachtContext())
            {
                return context.Members.ToList();
            }
        }

        public Member GetMember(string Code)
        {
            using (var context = new BookingYachtContext())
            {
                return context.Members.Where(x => x.Code == Code).FirstOrDefault();
            }
        }

    }
}
