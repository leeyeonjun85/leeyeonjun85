using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Models;
using DataBaseTools.Services;
using Edcore.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.ApplicationServices;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;
using Utiles;
using static System.Net.WebRequestMethods;

namespace DataBaseTools.ViewModels
{
    public partial class SftpViewModel : ViewModelBase
    {
        private SftpClient? client;
        private string _root = "/D:/SFTP";

        [ObservableProperty]
        private string _statusBar1 = "Status : Ready";
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotRoot))]
        [NotifyCanExecuteChangedFor(nameof(BtnBackCommand))]
        private string _statusBar2 = "Hellow world!";
        [ObservableProperty]
        private int _statusBarProgressBar = 0;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanConnect))]
        [NotifyPropertyChangedFor(nameof(CanMakeDirectory))]
        [NotifyCanExecuteChangedFor(nameof(BtnConnectCommand))]
        [NotifyCanExecuteChangedFor(nameof(AddDirectoryCommand))]
        private bool _isConnected;

        [ObservableProperty]
        private ObservableCollection<SftpModel> _yeonjunTestItemsSource = new();
        [ObservableProperty]
        private SftpModel _selectedData = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanMakeDirectory))]
        [NotifyCanExecuteChangedFor(nameof(AddDirectoryCommand))]
        private string _addDirectoryText = "";

        private bool CanConnect => !IsConnected;
        private bool IsNotRoot => (StatusBar2 != "Hellow world!" && StatusBar2 != $"{_root[3..]}");
        private bool CanMakeDirectory => (IsConnected == true && AddDirectoryText != "");

        public SftpViewModel( ) 
        { 

        }

        [RelayCommand]
        private void BtnDirectoryUpload(object? obj)
        {
            FolderBrowserDialog folderBrowserDialog = new();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string[] files = Directory.GetFiles(folderBrowserDialog.SelectedPath, "*", SearchOption.AllDirectories);
                string[] dirs = Directory.GetDirectories(folderBrowserDialog.SelectedPath, "*", SearchOption.AllDirectories);
                string selectedDir = folderBrowserDialog.SelectedPath.Split(Path.DirectorySeparatorChar)[^1];
                client?.CreateDirectory(selectedDir);
                client?.ChangeDirectory(selectedDir);
                foreach (string dir in dirs) 
                {
                    List<string> makeDirList = dir.Split(Path.DirectorySeparatorChar).ToList<string>();
                    string[] makeDirList1 = dir.Split(Path.DirectorySeparatorChar);
                    int idx = makeDirList.IndexOf(selectedDir);
                    var makeDirList2 = makeDirList1[idx..];

                    //client?.CreateDirectory(dir[4..]);
                    //client?.CreateDirectory(makeDir);
                }

                IEnumerable<FileSystemInfo> fileSystemInfos = new DirectoryInfo(folderBrowserDialog.SelectedPath).EnumerateFileSystemInfos();
                foreach (FileSystemInfo info in fileSystemInfos)
                {
                    if (info.Attributes.HasFlag(FileAttributes.Directory))
                    {
                        string subPath = client?.WorkingDirectory + "/" + info.Name;
                        if (!client!.Exists(subPath))
                        {
                            client.CreateDirectory(subPath);
                        }
                    }
                    else
                    {
                        using (Stream fileStream = new FileStream(info.FullName, FileMode.Open))
                        {
                            client?.UploadFile(fileStream, client?.WorkingDirectory + "/" + info.Name);
                        }
                    }
                }
            }
        }


        [RelayCommand]
        private void BtnFileUpload(object? obj)
        {
            OpenFileDialog openFileDialog = new()
            {
                //Multiselect = true,
                //InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                //Filter = "Text files (*.txt)|*.txt|Json files (*.json)|*.json|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                DialogResult okCancle = System.Windows.Forms.MessageBox.Show(
                    text: $"이 파일을 업로드 하시겠습니까?" +
                        $"{openFileDialog.FileName}",
                    caption: "파일 선택", MessageBoxButtons.OKCancel,
                    icon: MessageBoxIcon.Question);
                if (okCancle == DialogResult.OK)
                {
                    FileStream infile = System.IO.File.Open(openFileDialog.FileName, FileMode.Open);
                    client?.UploadFile(infile, $"./{Path.GetFileName(infile.Name)}");
                    GetListDirectory();
                }
            }
        }

        [RelayCommand]
        private void BtnDelete(object? obj)
        {
            if (SelectedData is null) return;

            if (SelectedData.IsDirectory)
            {
                client?.ChangeDirectory("..");
                client?.DeleteDirectory(SelectedData.Name);
                GetListDirectory();
                SelectedData = new();
            }
            else
            {
                client?.DeleteFile(SelectedData.Name);
                GetListDirectory();
                SelectedData = new();
            }
        }


        [RelayCommand(CanExecute = nameof(CanMakeDirectory))]
        private void AddDirectory(object? obj)
        {
            client?.CreateDirectory(AddDirectoryText);
            client?.ChangeDirectory(AddDirectoryText);
            GetListDirectory();
        }

        [RelayCommand(CanExecute = nameof(IsNotRoot))]
        private void BtnBack(object? obj)
        {
            SelectedData = new();
            client?.ChangeDirectory("..");
            GetListDirectory();
        }

        [RelayCommand]
        private void MouseLeftButtonUp(object? obj)
        {
            if (obj is not MouseButtonEventArgs args) return;
            if (args!.OriginalSource is not TextBlock textBlock) return;
            if (textBlock.DataContext is not SftpModel yeonjunTest) return;
            SelectedData = yeonjunTest;

            if (SelectedData.IsDirectory)
            {
                client?.ChangeDirectory(SelectedData.Name);
                GetListDirectory();
            }
        }

        [RelayCommand(CanExecute = nameof(CanConnect))]
        private void BtnConnect(object? obj)
        {
            try
            {
                JsonModel jsonModel = MyUtiles.GetJsonModel();
                var connectInfo = new ConnectionInfo(jsonModel.Edcore.SFTP.host,
                    Convert.ToInt32(jsonModel.Edcore.SFTP.port),
                    jsonModel.Edcore.SFTP.username,
                    new PasswordAuthenticationMethod(jsonModel.Edcore.SFTP.username, jsonModel.Edcore.SFTP.password));
                client = new SftpClient(connectInfo);

                client.KeepAliveInterval = TimeSpan.FromSeconds(60);
                client.ConnectionInfo.Timeout = TimeSpan.FromMinutes(180);
                client.OperationTimeout = TimeSpan.FromMinutes(180);

                client.Connect();

                IsConnected = client.IsConnected;
                if (IsConnected)
                {
                    client.ChangeDirectory(_root);
                    GetListDirectory();
                    StatusBar1 = "Status : Connected";
                }
                else { System.Windows.Forms.MessageBox.Show($"SFTP 서버 연결에 문제가 있습니다. {client.IsConnected}"); }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"{ex.Message}");
                throw;
            }
        }

        private void GetListDirectory()
        {
            if ( client is not null)
            {
                YeonjunTestItemsSource.Clear();
                
                foreach (SftpFile f in client.ListDirectory("."))
                {
                    if (f.IsDirectory)
                        YeonjunTestItemsSource.Add(new SftpModel() { Name = f.Name, IsDirectory = true});
                    else
                        YeonjunTestItemsSource.Add(new SftpModel() { Name = f.Name, FileSize =  Math.Round((double)f.Length/1000, 1)});
                }

                StatusBar2 = client.WorkingDirectory[3..];
            }
        }


        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            App.LOGGER!.LogInformation("SQLite가 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.LOGGER!.LogInformation("SQLite가 종료되었습니다.");
            client?.Disconnect();
        }
    }
}
