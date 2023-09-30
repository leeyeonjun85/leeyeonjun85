using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using OoManager.Models;

namespace OoManager.Services
{
    public interface IOoService
    {
        Task<AppData> AddMemberAsync(AppData AppData);
        AppData ConvertGradeOld(AppData AppData);
        AppData GetFireBase(AppData AppData);
        Task<IReadOnlyCollection<FirebaseObject<Member>>> GetMembersAsync(AppData AppData);
        Task<AppData> InitAppAsync(AppData AppData);
        Task<AppData> InitMembers(AppData AppData);
        Task<AppData> OpenPageHomeAsync(AppData AppData);
        Task<AppData> OpenPageLectureAsync(AppData AppData);
        Task<AppData> OpenPageMembersAsync(AppData AppData);
        Task<AppData> RefreshMembersAsync(AppData AppData);
        Task UpdateMemberAsync(AppData AppData);
    }
}