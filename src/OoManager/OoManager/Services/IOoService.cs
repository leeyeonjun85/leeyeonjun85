using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using OoManager.Models;

namespace OoManager.Services
{
    public interface IOoService
    {
        AppData GetFireBase(AppData AppData);
        Task<IReadOnlyCollection<FirebaseObject<OoMembers>>> GetMembersAsync(AppData AppData);
        AppData InitApp(AppData AppData);
        Task<AppData> InitMembers(AppData AppData);
        void RefreshMembersAsync(AppData AppData);
    }
}