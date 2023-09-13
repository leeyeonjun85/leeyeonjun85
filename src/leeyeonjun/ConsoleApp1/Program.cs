using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var connectInfo = new ConnectionInfo("61.99.119.105",
                                        5114,
                                        "edcoresftp",
                                        new PasswordAuthenticationMethod("edcoresftp", "0330"));

            

            using (var client = new SftpClient(connectInfo))
            {
                client.KeepAliveInterval = TimeSpan.FromSeconds(60);
                client.ConnectionInfo.Timeout = TimeSpan.FromMinutes(180);
                client.OperationTimeout = TimeSpan.FromMinutes(180);

                // SFTP 서버 연결
                client.Connect();

                bool IsConnected = client.IsConnected;
                var remoteDir = "/d:/SFTP";
                client.ChangeDirectory(remoteDir);
                Console.WriteLine($"SFTP 폴더 : {client.WorkingDirectory}");
                

                // 현재 디렉토리 내용 표시
                foreach (SftpFile f in client.ListDirectory("."))
                {
                    Console.WriteLine(f.Name);
                }

                //// SFTP 다운로드
                //using (var outfile = File.Create("ftptest.txt"))
                //{
                //    client.DownloadFile("./ftptest.txt", outfile);
                //}

                // SFTP 업로드
                using (var infile = File.Open("D:\\Edcore_Test\\logs\\Edcore-Log-20230912.txt", FileMode.Open))
                {
                    //client.UploadFile(infile, "./Edcore-Log-20230912.txt");
                    client.UploadFile(infile, "./Edcore-Log-20230912.txt");
                }

                Console.ReadKey();

                client.Disconnect();
                System.Environment.Exit(0);
            }
        }
    }
}