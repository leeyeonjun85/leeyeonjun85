using System;
using System.Collections.Generic;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Media;
using Firebase.Database;
using Firebase.Database.Query;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using OoManager.Models;
using Utiles;

namespace OoManager.Services
{
    public class AppUtiles : IAppUtiles
    {
        public async Task<AppData> InitAppAsync(AppData AppData)
        {
            // Color Theme
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Indigo];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Amber];
            PaletteHelper paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetPrimaryColor(primaryColor);
            theme.SetSecondaryColor(accentColor);
            paletteHelper.SetTheme(theme);

            // Init FireBase
            AppData = GetFireBase(AppData);

            // Open PageHome
            Task<AppData> _appData = OpenPageHomeAsync(AppData);
            await _appData; AppData = _appData.Result;

            return AppData;
        }

        public AppData GetFireBase(AppData AppData)
        {
            JsonModel jsonModel = MyUtiles.GetJsonModel();
            string FireBaseUrl = jsonModel.OoManager.FireBaseUrl;
            string FireBaseAuth = jsonModel.OoManager.FireBaseAuth;

            FirebaseClient client = new(FireBaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FireBaseAuth) });

            AppData.FirebaseDB = client.Child(AppData.FireBaseDbName);

            return AppData;
        }

        public async Task<AppData> OpenPageHomeAsync(AppData AppData)
        {
            // Init PageHome
            AppData.SelectedPageIndex = 0;
            AppData.SelectedPage = AppData.NavigationList[0];
            AppData.SelectedPageTitle = "프로그램 / 회원 정보";

            Task<AppData> _appData = RefreshDataAsync(AppData);
            await _appData; AppData = _appData.Result;

            return AppData;
        }

        public async Task<AppData> OpenPageMembersAsync(AppData AppData)
        {
            // Init PageMembers
            AppData.SelectedPageIndex = 1;
            AppData.SelectedPage = AppData.NavigationList[1];
            AppData.SelectedPageTitle = "회원 관리";

            Task<AppData> _appData = RefreshDataAsync(AppData);
            await _appData; AppData = _appData.Result;

            return AppData;
        }

        public async Task<AppData> OpenPageLectureAsync(AppData AppData)
        {
            // Init PageLecture
            AppData.SelectedPageIndex = 2;
            AppData.SelectedPage = AppData.NavigationList[2];
            AppData.SelectedPageTitle = "출석부";

            Task<AppData> _appData = RefreshDataAsync(AppData);
            await _appData; AppData = _appData.Result;

            return AppData;
        }

        

        public async Task<AppData> RefreshDataAsync(AppData AppData)
        {
            // Init Members
            Task<AppData> _appData1 = GetMembersAsync(AppData);
            await _appData1; AppData = _appData1.Result;

            // Init Lectures
            Task<AppData> _appData2 = GetLecturesAsync(AppData);
            await _appData2; AppData = _appData2.Result;

            // Init Members Information
            AppData.MembersTotal = AppData.Members.Count;
            AppData.MembersNormal = AppData.MembersRest = AppData.MembersPutOff = AppData.MembersTotalMoney = 0;
            foreach (MemberData member in AppData.Members)
            {
                // Lectures Init
                foreach (LectureData memberLecture in member.Lectures)
                {
                    if (!AppData.LectureHeaderList.Contains(memberLecture.o2_class_date))
                        AppData.LectureHeaderList.Add(memberLecture.o2_class_date);
                }

                if (AppData.LectureHeaderList.Count > 7)
                {
                    int _removeCount = AppData.LectureHeaderList.Count - 7;
                    AppData.LectureHeaderList.RemoveRange(0, _removeCount);
                }

                switch (AppData.LectureHeaderList.Count)
                {
                    case 0:
                        {
                            break;
                        }
                    case 1:
                        {

                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    case 5:
                        {
                            break;
                        }
                    case 6:
                        {
                            break;
                        }
                    case 7:
                        {
                            break;
                        }

                    default: throw new Exception();
                }


                switch (member.Member.member_status)
                {
                    case "재원":
                        {
                            AppData.MembersNormal += 1;
                            AppData.MembersTotalMoney += Convert.ToInt32(member.Member.member_money);
                            break;
                        }
                    case "휴원":
                        {
                            AppData.MembersRest += 1;
                            break;
                        }
                    case "보류":
                        {
                            AppData.MembersPutOff += 1;
                            break;
                        }

                    default: throw new Exception();
                }
            }

            // Init Program Information
            if (AppData.MembersTotal > 0)
                AppData.FireBaseState = "연결 성공";
            else AppData.FireBaseState = "연결 실패";


            return AppData;
        }

        public async Task<AppData> GetMembersAsync(AppData AppData)
        {
            AppData.Members = new();
            AppData.MembersDict = new();
            int _idx = 0;

            // Get members
            IReadOnlyCollection<FirebaseObject<Member>> members = await AppData.FirebaseDB
                    .Child("member")
                    .OnceAsync<Member>();

            foreach (var member in members)
            {
                AppData.MemberData = new();
                AppData.MemberData.Key = member.Key;
                AppData.MemberData.Member = member.Object;
                AppData.MemberData.SelectedGrade = member.Object.member_grade_str;
                AppData.MemberData.SelectedState = member.Object.member_status;
                AppData.MemberData.XpUpdateToolTip = $"{AppData.MemberData.Member.member_name} XP 수정하기";
                AppData.MemberData.BonusToolTip = $"{AppData.MemberData.Member.member_name}에게 +5xp";
                AppData.MemberData.MemberUpdateToolTip = $"{AppData.MemberData.Member.member_name} 회원정보 수정하기";
                AppData.MemberData.LectureUpdateToolTip = $"{AppData.MemberData.Member.member_name} 수업 수정하기";
                AppData.Members.Add(AppData.MemberData);
                AppData.MembersDict[member.Object.mid] = _idx;
                _idx++;
            }

            return AppData;
        }

        public async Task<AppData> GetLecturesAsync(AppData AppData)
        {
            AppData.LecturesTotal = new();
            int _idx = 0;

            // Init Lectures
            IReadOnlyCollection<FirebaseObject<object>> _lecturesDates = await AppData.FirebaseDB
                    .Child("lecture")
                    .OnceAsync<object>();

            foreach (FirebaseObject<object> _lecturesDate in _lecturesDates)
            {
                if (_lecturesDate.Object is IEnumerable<JToken> _lecturesDateJToken)
                {
                    foreach (JToken _lectureJToken in _lecturesDateJToken)
                    {
                        Lecture _lecture = new()
                        {
                            mid = _lectureJToken.First!.Value<int>("mid"),
                            o2_class_date = _lectureJToken.First.Value<string>("o2_class_date")!,
                            o2_class_homework = _lectureJToken.First.Value<string>("o2_class_homework")!,
                            o2_class_lecture = _lectureJToken.First.Value<string>("o2_class_lecture")!,
                            o2_class_memo = _lectureJToken.First.Value<string>("o2_class_memo")!,
                            o2_class_time_in = _lectureJToken.First.Value<string>("o2_class_time_in")!,
                            o2_class_time_out = _lectureJToken.First.Value<string>("o2_class_time_out")!,
                        };

                        LectureData _lectureData = new()
                        {
                            Key = _lectureJToken.Path,
                            Lecture = _lecture,
                            mid = _lectureJToken.First!.Value<int>("mid"),
                            o2_class_date = _lectureJToken.First.Value<string>("o2_class_date")!,
                            o2_class_homework = _lectureJToken.First.Value<string>("o2_class_homework")!,
                            o2_class_lecture = _lectureJToken.First.Value<string>("o2_class_lecture")!,
                            o2_class_memo = _lectureJToken.First.Value<string>("o2_class_memo")!,
                            o2_class_time_in = _lectureJToken.First.Value<string>("o2_class_time_in")!,
                            o2_class_time_out = _lectureJToken.First.Value<string>("o2_class_time_out")!,
                        };
                        AppData.LecturesTotal.Add(_lectureData);
                        AppData.LecturesTotalDict[_lectureData.Key] = _idx;
                        AppData.Members[AppData.MembersDict[_lectureJToken.First!.Value<int>("mid")]].Lectures.Add(_lectureData);
                        _idx++;
                    }
                }
            }

            return AppData;
        }





        public async Task<AppData> InitMembers(AppData AppData)
        {
            int mid = 0;

            List<Tuple<string, string, int>> memeberList = new()
            {
                new Tuple<string, string, int>("고1", "이연준", 10),
                new Tuple<string, string, int>("고2", "이채은", 100)

                //new Tuple<string, string, int>("초1", "도연우", 735),
                //new Tuple<string, string, int>("초1", "박재현", 950),
                //new Tuple<string, string, int>("초1", "도연우", 735),
                //new Tuple<string, string, int>("초1", "임수현", 185),
                //new Tuple<string, string, int>("초1", "서동인", 45),
                //new Tuple<string, string, int>("초2", "김윤", 80),
                //new Tuple<string, string, int>("초2", "박서연", 885),
                //new Tuple<string, string, int>("초2", "도화랑", 375),
                //new Tuple<string, string, int>("초3", "유희건", 1585),
                //new Tuple<string, string, int>("초3", "이윤지", 990),
                //new Tuple<string, string, int>("초4", "임서진", 775),
                //new Tuple<string, string, int>("초4", "김민", 435),
                //new Tuple<string, string, int>("초4", "박시연", 925),
                //new Tuple<string, string, int>("초5", "이세연", 460),
                //new Tuple<string, string, int>("초5", "유지흔", 1210),
                //new Tuple<string, string, int>("초5", "정윤후", 185),
                //new Tuple<string, string, int>("초5", "서민지", 905),
                //new Tuple<string, string, int>("초5", "서아인", 420),
                //new Tuple<string, string, int>("중1", "김태연", 40),
                //new Tuple<string, string, int>("중1", "윤미래", 10),
                //new Tuple<string, string, int>("중1", "도하율", 10),
                //new Tuple<string, string, int>("중2", "오채은", 10),
                //new Tuple<string, string, int>("중2", "서아민", 10),
                //new Tuple<string, string, int>("중3", "김태은", 10),
                //new Tuple<string, string, int>("중3", "지예도", 10),
                //new Tuple<string, string, int>("중3", "구영우", 55),
                //new Tuple<string, string, int>("중3", "이신아", 10),
                //new Tuple<string, string, int>("중3", "이우주", 10),
                //new Tuple<string, string, int>("고3", "오예은", 10)
        };

            foreach (var _memeber in memeberList)
            {
                mid += 1;
                AppData.MemberData.Member = new();
                AppData.MemberData.Member.member_grade_str = _memeber.Item1;
                AppData.MemberData.Member.member_grade = ConvertGradeOld(AppData.MemberData.Member.member_grade_str);
                AppData.MemberData.Member.member_name = _memeber.Item2;
                AppData.MemberData.Member.member_xp = _memeber.Item3;
                AppData.MemberData.Member.mid = mid;

                //await AppData.FirebaseDB
                //        .Child("member")
                //        .PostAsync(AppData.MemberData.Member);

                Task<FirebaseObject<Member>> returnMember = AppData.FirebaseDB
                        .Child("member")
                        .PostAsync(AppData.MemberData.Member);
                await returnMember;
                AppData.MemberData.Key = returnMember.Result.Key;
                AppData.Members.Add(AppData.MemberData);
                AppData.MemberData = new();
            }
            return AppData;
        }

        //public AppData ConvertGradeOld(AppData AppData)
        //{
        //    if (AppData.MemberData.Member.member_grade_str == "6살")
        //        AppData.MemberData.Member.member_grade = 6;
        //    else if (AppData.MemberData.Member.member_grade_str == "7살")
        //        AppData.MemberData.Member.member_grade = 7;
        //    else if (AppData.MemberData.Member.member_grade_str == "초1")
        //        AppData.MemberData.Member.member_grade = 8;
        //    else if (AppData.MemberData.Member.member_grade_str == "초2")
        //        AppData.MemberData.Member.member_grade = 9;
        //    else if (AppData.MemberData.Member.member_grade_str == "초3")
        //        AppData.MemberData.Member.member_grade = 10;
        //    else if (AppData.MemberData.Member.member_grade_str == "초4")
        //        AppData.MemberData.Member.member_grade = 11;
        //    else if (AppData.MemberData.Member.member_grade_str == "초5")
        //        AppData.MemberData.Member.member_grade = 12;
        //    else if (AppData.MemberData.Member.member_grade_str == "초6")
        //        AppData.MemberData.Member.member_grade = 13;
        //    else if (AppData.MemberData.Member.member_grade_str == "중1")
        //        AppData.MemberData.Member.member_grade = 14;
        //    else if (AppData.MemberData.Member.member_grade_str == "중2")
        //        AppData.MemberData.Member.member_grade = 15;
        //    else if (AppData.MemberData.Member.member_grade_str == "중3")
        //        AppData.MemberData.Member.member_grade = 16;
        //    else if (AppData.MemberData.Member.member_grade_str == "고1")
        //        AppData.MemberData.Member.member_grade = 17;
        //    else if (AppData.MemberData.Member.member_grade_str == "고2")
        //        AppData.MemberData.Member.member_grade = 18;
        //    else if (AppData.MemberData.Member.member_grade_str == "고3")
        //        AppData.MemberData.Member.member_grade = 19;

        //    return AppData;
        //}

        public int ConvertGradeOld(string GradeString)
        {
            int GradeInt;
            switch (GradeString)
            {
                case "6살": { GradeInt = 6; break; }
                case "7살": { GradeInt = 7; break; }
                case "초1": { GradeInt = 8; break; }
                case "초2": { GradeInt = 9; break; }
                case "초3": { GradeInt = 10; break; }
                case "초4": { GradeInt = 11; break; }
                case "초5": { GradeInt = 12; break; }
                case "초6": { GradeInt = 13; break; }
                case "중1": { GradeInt = 14; break; }
                case "중2": { GradeInt = 15; break; }
                case "중3": { GradeInt = 16; break; }
                case "고1": { GradeInt = 17; break; }
                case "고2": { GradeInt = 18; break; }
                case "고3": { GradeInt = 19; break; }
                default: throw new Exception("입력된 학년 문자열에 문제가 있습니다.");
            }

            return GradeInt;
        }

        public async Task<AppData> AddMemberAsync(AppData AppData)
        {
            Task<FirebaseObject<Member>> returnMember = AppData.FirebaseDB
                       .Child("member")
                       .PostAsync(AppData.MemberData.Member);
            await returnMember;
            AppData.MemberData.Key = returnMember.Result.Key;
            AppData.Members.Add(AppData.MemberData);
            AppData.MemberData = new();

            return AppData;
        }

        public async Task UpdateMemberAsync(AppData AppData)
        {
            // 데이터베이스에 멤버 업데이트
            await AppData.FirebaseDB
                .Child("member")
                .Child(AppData.MemberData.Key)
                .PutAsync(AppData.MemberData.Member);

            // 화면에 멤버 업데이트
            foreach (var member in AppData.Members)
            {
                if (member.Key == AppData.MemberData.Key)
                {
                    member.Member = AppData.MemberData.Member;
                    member.SelectedGrade = AppData.MemberData.SelectedGrade;
                    member.SelectedState = AppData.MemberData.SelectedState;
                    member.BonusToolTip = AppData.MemberData.BonusToolTip;

                    AppData.MemberData = new();

                    Task<AppData> _appData = AppData.OoService!.GetMembersAsync(AppData);
                    await _appData;
                    AppData = _appData.Result;
                }
            }
        }
    }
}
