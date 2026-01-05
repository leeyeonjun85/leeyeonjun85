//기타
////주석 추가           Ctrl+KC
////주석 제거           Ctrl+KU
////솔루션 창 열기      Ctrl+Alt+L
////Tool Window 전환    Shift+Alt+F6
////책갈피              Ctrl+KK




//Hello World - C# 소개 대화형 C# 자습서
//https://learn.microsoft.com/ko-kr/dotnet/csharp/tour-of-csharp/tutorials/hello-world
using System;

Console.WriteLine("■■■■  첫 번째 C# 프로그램 실행  ■■■■");
Console.WriteLine("Hello World!");


Console.WriteLine("■■■■  변수 선언 및 사용  ■■■■");
string aFriend = "Bill";
Console.WriteLine(aFriend);

aFriend = "Maira";
Console.WriteLine(aFriend);

Console.WriteLine("Hello " + aFriend);

////문자열 보간 사용
Console.WriteLine($"Hello {aFriend}");


Console.WriteLine("■■■■  문자열 작업  ■■■■");

string firstFriend = "Maria";
string secondFriend = "Sage";
Console.WriteLine($"My friends are {firstFriend} and {secondFriend}");
Console.WriteLine($"The name {firstFriend} has {firstFriend.Length} letters.");     // .Length 는 문자열의 길이를 정수로 반환
Console.WriteLine($"The name {secondFriend} has {secondFriend.Length} letters.");


Console.WriteLine("■■■■  더 많은 문자열 작업  ■■■■");

//Trim
string greeting = "      Hello World!       ";      // C#의 문자열은 공백을 모두 표현한다
Console.WriteLine($"[{greeting}]");

string trimmedGreeting = greeting.TrimStart();      // 왼쪽 공백 제거
Console.WriteLine($"[{trimmedGreeting}]");

trimmedGreeting = greeting.TrimEnd();               // 오른쪽 공백 제거
Console.WriteLine($"[{trimmedGreeting}]");

trimmedGreeting = greeting.Trim();                  // 양쪽 공백 제거
Console.WriteLine($"[{trimmedGreeting}]");

//Replace
string sayHello = "Hello World!";
Console.WriteLine(sayHello);
sayHello = sayHello.Replace("Hello", "Greetings");  // Hello를 Greeings로 변경
Console.WriteLine(sayHello);

Console.WriteLine(sayHello.ToUpper());              // 문자열을 대문자로 변경
Console.WriteLine(sayHello.ToLower());              // 문자열을 소문자로 변경


Console.WriteLine("■■■■  검색 문자열  ■■■■");

string songLyrics = "You say goodbye, and I say hello";
Console.WriteLine(songLyrics.Contains("goodbye"));          // 있으니깐 True
Console.WriteLine(songLyrics.Contains("greetings"));        // 없으니깐 False

Console.WriteLine(songLyrics.StartsWith("You"));            // You로 시작하니깐 True
Console.WriteLine(songLyrics.EndsWith("hello"));            // hello로 끝나니깐 True





//C#에서 정수 및 부동 소수점 수 조작
//https://learn.microsoft.com/ko-kr/dotnet/csharp/tour-of-csharp/tutorials/numbers-in-csharp

Console.WriteLine("■■■■  정수 계산 살펴보기  ■■■■");

int int_a = 18;
int int_b = 6;

int int_c = int_a + int_b;
Console.WriteLine(int_c);   // 24
int_c = int_a - int_b;
Console.WriteLine(int_c);   // 12
int_c = int_a * int_b;
Console.WriteLine(int_c);   // 108
int_c = int_a / int_b;
Console.WriteLine(int_c);   // 3


Console.WriteLine("■■■■  연산 순서 알아보기  ■■■■");

int_a = 5;
int_b = 4;
int_c = 2;
int int_d = int_a + int_b * int_c;                          // 13
Console.WriteLine(int_d);

int_a = 5;
int_b = 4;
int_c = 2;
int_d = (int_a + int_b) * int_c;
Console.WriteLine(int_d);                       // 18

int_d = (int_a + int_b) - 6 * int_c + (12 * 4) / 3 + 12;    
Console.WriteLine(int_d);                       // 25

int_a = 7;
int_b = 4;
int_c = 3;
int_d = (int_a + int_b) / int_c;
Console.WriteLine(int_d);                       // 3


Console.WriteLine("■■■■  정수 전체 자릿수 및 한도 살펴보기  ■■■■");

int_a = 7;
int_b = 4;
int_c = 3;
int_d = (int_a + int_b) / int_c;
int e = (int_a + int_b) % int_c;
Console.WriteLine($"quotient: {int_d}");        // 몫 3
Console.WriteLine($"remainder: {e}");       // 나머지 2

int int_max = int.MaxValue;
int int_min = int.MinValue;
Console.WriteLine($"The range of integers is {int_min} to {int_max}");

int what = int_max + 3;
Console.WriteLine($"An example of overflow: {what}");


Console.WriteLine("■■■■  double 형식 작업  ■■■■");

double double_a = 5;
double double_b = 4;
double double_c = 2;
double double_d = (double_a + double_b) / double_c;
Console.WriteLine(double_d);

double_a = 19;
double_b = 23;
double_c = 8;
double_d = (double_a + double_b) / double_c;
Console.WriteLine(double_d);

double double_max = double.MaxValue;
double double_min = double.MinValue;
Console.WriteLine($"The range of double is {double_min} to {double_max}");

double double_third = 1.0 / 3.0;
Console.WriteLine(double_third);

Console.WriteLine("■■■■  10진 형식으로 작업  ■■■■");

decimal decimal_min = decimal.MinValue;
decimal decimal_max = decimal.MaxValue;
Console.WriteLine($"The range of the decimal type is {decimal_min} to {decimal_max}");

double decimal_a = 1.0;
double decimal_b = 3.0;
Console.WriteLine(decimal_a / decimal_b);

decimal decimal_c = 1.0M;
decimal decimal_d = 3.0M;
Console.WriteLine(decimal_c / decimal_d);

double radius = 2.50;
double area = Math.PI * radius * radius;
Console.WriteLine(area);







//분기 및 루프 문이 포함된 조건부 논리 알아보기
//https://learn.microsoft.com/ko-kr/dotnet/csharp/tour-of-csharp/tutorials/branches-and-loops

Console.WriteLine("■■■■  if 문을 사용하여 결정하기  ■■■■");

int_a = 5;
int_b = 6;
if (int_a + int_b > 10)
    Console.WriteLine("The answer is greater than 10.");

int_b = 3;
Console.WriteLine($"int_a + int_b = {(int_a + int_b)}");



Console.WriteLine("■■■■  if와 else를 함께 사용하기  ■■■■");

int_a = 5;
int_b = 3;
Console.WriteLine($"int_a + int_b = {(int_a + int_b)}");
if (int_a + int_b > 10)
    Console.WriteLine("The answer is greater than 10");
else
    Console.WriteLine("The answer is not greater than 10");

int_a = 5;
int_b = 3;
Console.WriteLine($"int_a + int_b = {(int_a + int_b)}");
if (int_a + int_b > 10)
{
    Console.WriteLine("The answer is greater than 10");
}
else
{
    Console.WriteLine("The answer is not greater than 10");
}

int_a = 5;
int_b = 3;
int_c = 4;
Console.WriteLine($"int_a + int_b + int_c = {(int_a + int_b + int_c)}");
Console.WriteLine($"int_a : {int_a} / int_b : {int_b}");
if ((int_a + int_b + int_c > 10) && (int_a == int_b))
{
    Console.WriteLine("The answer is greater than 10");
    Console.WriteLine("And the first number is equal to the second");
}
else
{
    Console.WriteLine("The answer is not greater than 10");
    Console.WriteLine("Or the first number is not equal to the second");
}

int_a = 5;
int_b = 3;
int_c = 4;
Console.WriteLine($"int_a + int_b + int_c = {(int_a + int_b + int_c)}");
Console.WriteLine($"int_a : {int_a} / int_b : {int_b}");
if ((int_a + int_b + int_c > 10) || (int_a == int_b))
{
    Console.WriteLine("The answer is greater than 10");
    Console.WriteLine("Or the first number is equal to the second");
}
else
{
    Console.WriteLine("The answer is not greater than 10");
    Console.WriteLine("And the first number is not equal to the second");
}


Console.WriteLine("■■■■  루프를 사용하여 작업 반복  ■■■■");

int counter = 0;
while (counter < 10)
{
    Console.WriteLine($"Hello World! The counter is {counter}");
    counter++;
}

counter = 0;
do
{
    Console.WriteLine($"Hello World! The counter is {counter}");
    counter++;
} while (counter < 10);


Console.WriteLine("■■■■  for 루프 작업  ■■■■");

for (counter = 0; counter < 10; counter++)
{
    Console.WriteLine($"Hello World! The counter is {counter}");
}


Console.WriteLine("■■■■  중첩 루프 만들기  ■■■■");

for (int row = 1; row < 11; row++)
{
    Console.WriteLine($"The row is {row}");
}

for (char column = 'a'; column < 'k'; column++)
{
    Console.WriteLine($"The column is {column}");
}

for (int row = 1; row < 11; row++)
{
    for (char column = 'a'; column < 'k'; column++)
    {
        Console.WriteLine($"The cell is ({row}, {column})");
    }
}


Console.WriteLine("■■■■  중첩 루프 만들기  ■■■■");

int sum = 0;
for (int number = 1; number < 21; number++)
{
    if (number % 3 == 0)
    {
        sum = sum + number;
    }
}
Console.WriteLine($"The sum is {sum}");









//일반 목록 형식을 사용하여 데이터 컬렉션을 관리하는 방법 알아보기
//https://learn.microsoft.com/ko-kr/dotnet/csharp/tour-of-csharp/tutorials/list-collection


Console.WriteLine("■■■■  목록 만들기  ■■■■");

var names = new List<string> { "<name>", "Ana", "Felipe" };
foreach (var name in names)
{
    Console.WriteLine($"Hello {name.ToUpper()}!");
}


Console.WriteLine("■■■■  목록 콘텐츠 수정  ■■■■");

Console.WriteLine();
names.Add("Maria");
names.Add("Bill");
names.Remove("Ana");
foreach (var name in names)
{
    Console.WriteLine($"Hello {name.ToUpper()}!");
}

Console.WriteLine($"My name is {names[0]}.");
Console.WriteLine($"I've added {names[2]} and {names[3]} to the list.");

Console.WriteLine($"The list has {names.Count} people in it");


Console.WriteLine("■■■■  목록 검색 및 정렬  ■■■■");

var index = names.IndexOf("Felipe");
if (index != -1)
{
    Console.WriteLine($"The name {names[index]} is at index {index}");
}
var notFound = names.IndexOf("Not Found");
Console.WriteLine($"When an item is not found, IndexOf returns {notFound}");

names.Sort();
foreach (var name in names)
{
    Console.WriteLine($"Hello {name.ToUpper()}!");
}


Console.WriteLine("■■■■  다른 형식 목록  ■■■■");

var fibonacciNumbers = new List<int> { 1, 1 };

for (int i = 0; i<10; i++)
{
    var previous = fibonacciNumbers[fibonacciNumbers.Count - 1];
    var previous2 = fibonacciNumbers[fibonacciNumbers.Count - 2];

    fibonacciNumbers.Add(previous + previous2);
}

foreach (var item in fibonacciNumbers)
{
    Console.WriteLine(item);
}


Console.WriteLine("■■■■  과제  ■■■■");

fibonacciNumbers = new List<int> { 1, 1 };

while (fibonacciNumbers.Count < 20)
{
    var previous = fibonacciNumbers[fibonacciNumbers.Count - 1];
    var previous2 = fibonacciNumbers[fibonacciNumbers.Count - 2];

    fibonacciNumbers.Add(previous + previous2);
}
foreach (var item in fibonacciNumbers)
{
    Console.WriteLine(item);
}


Console.WriteLine($"{123}, {"최강기아"}");
Console.WriteLine($"{123,-10:D5}, {"최강기아",20}");




Console.ReadKey();







