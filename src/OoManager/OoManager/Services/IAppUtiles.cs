using System.Threading.Tasks;
using OoManager.Models;

namespace OoManager.Services
{
    public interface IAppUtiles
    {
        Task<AppData> AddMemberAsync(AppData AppData);
        int ConvertGradeOld(string GradeString);
        AppData GetFireBase(AppData AppData);
        Task<AppData> GetLecturesAsync(AppData AppData);
        Task<AppData> GetMembersAsync(AppData AppData);
        Task<AppData> InitAppAsync(AppData AppData);
        Task<AppData> InitMembers(AppData AppData);
        Task<AppData> OpenPageHomeAsync(AppData AppData);
        Task<AppData> OpenPageLectureAsync(AppData AppData);
        Task<AppData> OpenPageMembersAsync(AppData AppData);
        Task<AppData> RefreshDataAsync(AppData AppData);
        Task UpdateMemberAsync(AppData AppData);
    }
}