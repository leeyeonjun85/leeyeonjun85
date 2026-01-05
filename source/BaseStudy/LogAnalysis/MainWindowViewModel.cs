using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyUtiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace LogAnalysis
{
    public partial class MainWindowViewModel : ViewModelBase
    {

        
        [ObservableProperty]
        private string _tbxDirecotyPathText = "C:\\Test1\\Test2";
        [ObservableProperty]
        private string _statusBar1 = "Status : Ready";
        [ObservableProperty]
        private string _statusBar2 = "Please Connect Server first!";
        [ObservableProperty]
        private int _statusBarProgressBar = 0;

        [ObservableProperty]
        private ObservableCollection<LogModel> _dataGridList = new();
        [ObservableProperty]
        private string _textBoxText = "=== Information ===";

        int totalCreate = 0;
        int totalDelete = 0;
        int totalWatchCreated = 0;
        int totalWatchDeleted = 0;
        int totalQueueUp = 0;
        int totalQueueDown = 0;
        int totalException = 0;
        int totalSubDelete = 0;

        public MainWindowViewModel()
        {
        }



        [RelayCommand]
        private void BtnFilePath(object obj)
        {
            TextBoxText = "=== Information ===";
            totalCreate = 0;
            totalDelete = 0;
            totalWatchCreated = 0;
            totalWatchDeleted = 0;
            totalQueueUp = 0;
            totalQueueDown = 0;
            totalException = 0;
            totalSubDelete = 0;

            TbxDirecotyPathText = Uteils.OpenFile();
            ReadTextStream(TbxDirecotyPathText);
            OutInfo();
        }

        void OutInfo()
        {
            TimeSpan timeSpan = DataGridList[^1].DateTime - DataGridList[0].DateTime;
            TextBoxText += $"{Environment.NewLine}- 총 작업 시간 : {timeSpan}";

            TextBoxText += $"{Environment.NewLine}============ Test Generator ============";
            TextBoxText +=$"{Environment.NewLine}- 총 생산 파일 : {totalCreate} 개";
            TextBoxText +=$"{Environment.NewLine}- 삭제한 파일 : {totalDelete} 개";

            TextBoxText += $"{Environment.NewLine}============ Pub ============";
            TextBoxText +=$"{Environment.NewLine}- Watch Created : {totalWatchCreated} 개";
            TextBoxText +=$"{Environment.NewLine}- Watch Deleted : {totalWatchDeleted} 개";
            TextBoxText +=$"{Environment.NewLine}- Queue Up : {totalQueueUp} 개";
            TextBoxText +=$"{Environment.NewLine}- Exception : {totalException} 개";

            TextBoxText += $"{Environment.NewLine}============ Sub ============";
            TextBoxText += $"{Environment.NewLine}- Queue Dwon : {totalQueueDown} 개";
            TextBoxText += $"{Environment.NewLine}- Sub Delete : {totalSubDelete} 개";

        }

        void ReadTextStream(string path)
        {
            StreamReader reader = new StreamReader(path);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (ParsingToModel(line) != null)
                {
                    DataGridList.Add(ParsingToModel(line));
                }
            }
        }

        LogModel ParsingToModel(string str)
        {
            LogModel dataModel = new();

            if (str.Length > 0 && str[..4] == "2023")
            {
                dataModel.Category = str[35..40];
                dataModel.DateTime = Convert.ToDateTime(str[..27]);
                dataModel.Message = str[41..];


                if (dataModel.Message.Contains("자동생성"))
                    totalCreate++;
                if (dataModel.Message.Contains("최대 용량(2000) 제한"))
                    totalDelete++;
                if (dataModel.Message.Contains("Watch Created"))
                    totalWatchCreated++;
                if (dataModel.Message.Contains("Deleted"))
                    totalWatchDeleted++;
                if (dataModel.Message.Contains("Solace Manager Queue Up"))
                    totalQueueUp++;
                if (dataModel.Message.Contains("Solace Manager Queue Down"))
                    totalQueueDown++;
                if (dataModel.Message.Contains("System.IO.IOException"))
                    totalException++;
                if (dataModel.Message.Contains("최대 용량(1000) 제한"))
                    totalSubDelete++;


                return dataModel;
            }
            else return null;
            
        }

    }
}
