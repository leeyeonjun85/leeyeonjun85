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
        AppData InitApp(AppData AppData);
        Task<AppData> InitMembers(AppData AppData);
        void RefreshMembersAsync(AppData AppData);
        Task UpdateMemberAsync(AppData AppData);
    }
}