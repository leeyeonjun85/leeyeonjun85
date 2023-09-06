

// https://learn.microsoft.com/ko-kr/training/modules/dotnet-files/3-exercise-file-system

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//현재 폴더 변경
string dir = @"C:\Users\leeyeonjun\source\repos\mslearn-dotnet-files";
Directory.SetCurrentDirectory(dir);
Console.WriteLine(Directory.GetCurrentDirectory());


// https://learn.microsoft.com/ko-kr/training/modules/dotnet-files/4-paths

// 현재 폴더 위치 출력
Console.WriteLine(Directory.GetCurrentDirectory());

// `Path.DirectorySeparatorChar` 경로 구분자
Console.WriteLine($"stores{Path.DirectorySeparatorChar}201");

// 경로 조인
Console.WriteLine(Path.Combine("stores", "201")); // outputs: stores/201

// 파일 확장자 확인
Console.WriteLine(Path.GetExtension("sales.json")); // outputs: .json

// 파일정보 가져오기 `FileInfo()`
string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";
FileInfo info = new FileInfo(fileName);
Console.WriteLine($"Full Name: {info.FullName}\nDirectory: {info.Directory}");
Console.WriteLine($"Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}");



Console.WriteLine("===================================================");
Console.WriteLine("===================================================");



var currentDir = Directory.GetCurrentDirectory();
var sampledataDir = Path.Combine(currentDir, "sampledata");
var sampleJson = Path.Combine(sampledataDir, "sampleJons.json");

//sampledataDir 폴더 만들기
Console.WriteLine($"▣ \"{sampledataDir}\" 폴더를 만듧니다.");

if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), sampledataDir)))
{
    Console.WriteLine($"▲ \"{sampledataDir}\" 폴더가 이미 존재합니다.");
}
else
{
    Directory.CreateDirectory(sampledataDir);
}


// 파일에 데이터 쓰기
var json_data = new JObject();

var rowList = new List<int>();
for (int i = 1; i <= 20; i++) { rowList.Add(i); }

Random rand = new Random();
List<int> temperature = new List<int>();
for (int i = 1; i <= 20; i++) { temperature.Add(rand.Next(20, 80)); }

List<double> pressure = new List<double>();
for (double i = 1; i <= 20; i++) { pressure.Add(rand.NextDouble()); }

//foreach (var item in pressure) { Console.WriteLine(item); }

json_data.Add("rowNumber", JArray.FromObject(rowList));
json_data.Add("temperature", JArray.FromObject(temperature));
json_data.Add("pressure", JArray.FromObject(pressure));


File.WriteAllText(sampleJson, json_data.ToString());

Console.WriteLine(json_data.ToString());
Console.WriteLine(json_data["pressure"]);



Console.WriteLine("===================================================");
Console.WriteLine("===================================================");


