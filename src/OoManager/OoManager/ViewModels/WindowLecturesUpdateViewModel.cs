using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;
using Firebase.Database.Query;
using OoManager.Models;

namespace OoManager.ViewModels
{
    public partial class WindowLecturesUpdateViewModel : ViewModelBase, IParameterReceiver
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = new();
        [ObservableProperty]
        private MemberData _selectedMember = new();
        [ObservableProperty]
        private string _windowTitle = string.Empty;

        [ObservableProperty]
        private ObservableCollection<LectureData> _lectures = new();
        [ObservableProperty]
        private Dictionary<string, int> _lecturesDict = new();
        [ObservableProperty]
        private LectureData _lectureData = new();

        [ObservableProperty]
        private string _lectureGridTitle = "수업 정보";
        [ObservableProperty]
        private int _lectureGridZIndex = 0;
        [ObservableProperty]
        private Visibility _lectureGridVisibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _btnLectureOkAddVisibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _btnLectureOkUpdateVisibility = Visibility.Hidden;


        [ObservableProperty]
        private string _lectureDate = DateTime.Now.ToString("yyyy-MM-dd");
        [ObservableProperty]
        private string _lectureTimeIn = DateTime.Now.AddMinutes(-90).ToString("HH:mm");
        [ObservableProperty]
        private string _lectureTimeOut = DateTime.Now.ToString("HH:mm");
        [ObservableProperty]
        private string _lectureLecture = string.Empty;
        [ObservableProperty]
        private string _lectureHomework = string.Empty;
        [ObservableProperty]
        private string _lectureMemo = string.Empty;
        #endregion


        public WindowLecturesUpdateViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private async Task BtnAddAsync(object obj)
        {
            await Task.Run(() =>
            {
                LectureGridZIndex = 10;
                LectureGridVisibility = Visibility.Visible;
                BtnLectureOkAddVisibility = Visibility.Visible;
                BtnLectureOkUpdateVisibility = Visibility.Hidden;
                LectureGridTitle = $"{SelectedMember.Member.member_name} 수업 추가하기";

                LectureDate = DateTime.Now.ToString("yyyy-MM-dd");
                LectureTimeIn = DateTime.Now.AddMinutes(-90).ToString("HH:mm");
                LectureTimeOut = DateTime.Now.ToString("HH:mm");
                LectureLecture = $"{SelectedMember.Member.member_name}의 수업내용";
                LectureHomework = $"{SelectedMember.Member.member_name}의 숙제";
                LectureMemo = $"온 시간 {LectureTimeIn}, 간 시간 {LectureTimeOut}";
            });
        }

        [RelayCommand]
        private async Task BtnLectureOkAddAsync(object obj)
        {
            LectureGridZIndex = 0;
            LectureGridVisibility = Visibility.Hidden;

            // Lecture 객체 생성
            Lecture _lecture = new()
            {
                mid = SelectedMember.Member.mid,
                o2_class_date = LectureDate,
                o2_class_homework = LectureHomework,
                o2_class_lecture = LectureLecture,
                o2_class_memo = LectureMemo,
                o2_class_time_in = LectureTimeIn,
                o2_class_time_out = LectureTimeOut,
            };

            // LectureData 객체 생성
            LectureData _lectureData = new()
            {
                Key = string.Empty,
                Lecture = _lecture,
                o2_class_date = _lecture.o2_class_date,
                o2_class_homework = _lecture.o2_class_homework,
                o2_class_lecture = _lecture.o2_class_lecture,
                o2_class_memo = _lecture.o2_class_memo,
                o2_class_time_in = _lecture.o2_class_time_in,
                o2_class_time_out = _lecture.o2_class_time_out,
            };

            // 데이터베이스에 추가
            Task<FirebaseObject<Lecture>> returnLecture = AppData.FirebaseDB
                    .Child("lecture")
                    .Child(_lectureData.o2_class_date)
                    .PostAsync(_lectureData.Lecture);
            await returnLecture;
            _lectureData.Key = returnLecture.Result.Key;

            // 화면에 추가
            Lectures.Add(_lectureData);
        }

        [RelayCommand]
        private async Task BtnLectureCancelAsync(object obj)
        {
            await Task.Run(() =>
            {
                LectureGridZIndex = 0;
                LectureGridVisibility = Visibility.Hidden;
            });
        }

        [RelayCommand]
        private async Task BtnUpdateAsync(LectureData _lectureData)
        {
            LectureData = _lectureData;

            await Task.Run(() =>
            {
                LectureGridZIndex = 10;
                LectureGridVisibility = Visibility.Visible;
                BtnLectureOkAddVisibility = Visibility.Hidden;
                BtnLectureOkUpdateVisibility = Visibility.Visible;
                LectureGridTitle = $"{SelectedMember.Member.member_name} 수업 수정하기";

                LectureDate = LectureData.o2_class_date;
                LectureTimeIn = LectureData.Lecture.o2_class_time_in;
                LectureTimeOut = LectureData.Lecture.o2_class_time_out;
                LectureLecture = LectureData.Lecture.o2_class_lecture;
                LectureHomework = LectureData.Lecture.o2_class_homework;
                LectureMemo = LectureData.Lecture.o2_class_memo;
            });
        }

        [RelayCommand]
        private async Task BtnLectureOkUpdateAsync(object obj)
        {
            LectureGridZIndex = 0;
            LectureGridVisibility = Visibility.Hidden;

            // Lecture 객체 생성
            Lecture _lecture = new()
            {
                mid = SelectedMember.Member.mid,
                o2_class_date = LectureDate,
                o2_class_homework = LectureHomework,
                o2_class_lecture = LectureLecture,
                o2_class_memo = LectureMemo,
                o2_class_time_in = LectureTimeIn,
                o2_class_time_out = LectureTimeOut,
            };

            // LectureData 객체 생성
            LectureData.Lecture = _lecture;
            LectureData.o2_class_date = LectureDate;
            LectureData.o2_class_homework = LectureHomework;
            LectureData.o2_class_lecture = LectureLecture;
            LectureData.o2_class_memo = LectureMemo;
            LectureData.o2_class_time_in = LectureTimeIn;
            LectureData.o2_class_time_out = LectureTimeOut;

            // 데이터베이스에서 수정
            await AppData.FirebaseDB
                .Child("lecture")
                .Child(LectureData.o2_class_date)
                .Child(LectureData.Key)
                .PutAsync(LectureData.Lecture);

            // 화면에서 수정
            ObservableCollection<LectureData> _tempLectures = new();
            foreach (LectureData _lectureData in Lectures)
                _tempLectures.Add(_lectureData);
            Lectures.Clear();
            foreach (LectureData _lectureData in _tempLectures)
                Lectures.Add(_lectureData);

            LectureData = new();
        }

        [RelayCommand]
        private async Task BtnDeleteAsync(LectureData _lectureData)
        {
            // 데이터베이스에 수업 삭제
            MessageBoxResult messageBoxResult = MessageBox.Show(
                $"정말로 '{SelectedMember.Member.member_name}'의 '{_lectureData.o2_class_date}' 수업을 삭제하시겠습니까?{Environment.NewLine}(삭제하면 복구할 수 없습니다.)",
                "삭제 확인",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                await AppData.FirebaseDB!
                    .Child("lecture")
                    .Child(_lectureData.o2_class_date)
                    .Child(_lectureData.Key)
                    .DeleteAsync();
                // 화면에서 삭제
                Lectures.Remove(_lectureData);
            }
        }



        [RelayCommand]
        private async Task BtnTest4Async(object obj)
        {
            Console.WriteLine($"{AppData}");
            await Task.Delay(1000);
            Window?.Close();
        }



        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
        }

        void IParameterReceiver.ReceiveParameter(object parameter)
        {
            if (parameter is AppData _appData)
            {
                AppData = _appData;

                if (AppData.SelectedMember is not null)
                {
                    //SelectedMember = AppData.SelectedMember;
                    WindowTitle = $"{SelectedMember.Member.member_name} 수업 관리";

                    if (SelectedMember.Lectures.Count > 0)
                    {
                        int _idx = 0;
                        foreach (LectureData _lectureData in SelectedMember.Lectures)
                        {
                            Lectures.Add(_lectureData);
                            LecturesDict[_lectureData.Key] = _idx;
                            _idx++;
                        }

                    }

                }


            }
        }
    }
}
