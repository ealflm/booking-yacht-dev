using System.Threading.Tasks;
using BookingYacht.Business.FileModels;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IFileManagerLogic
    {
        Task Upload(FileModel model);
        Task<byte[]> Get(string imageName);
    }
}