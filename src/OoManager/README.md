# Oo Manager

## 필요 NuGet
```bash
Install-Package CommunityToolkit.Mvvm
Install-Package Microsoft.Extensions.Hosting
Install-Package MaterialDesignThemes
Install-Package Microsoft.AspNetCore.SignalR.Client
Install-Package Microsoft.EntityFrameworkCore.Sqlite
```

## 프로그램 설명
- 프로그램 실행 시, 루트에 `EDCORE`라는 폴더가 자동으로 생성됩니다.
- `EDCORE`에는 PerformanceMonitor폴더가 생성되고, PerformanceMonitor 폴더에는 Logs 폴더와 MonitorData 폴더가 생성됩니다.
- Logs 폴더에 `log-{날짜}.txt`(log-20230911.txt) 로그파일이 자동으로 생성되어 로그가 기록됩니다.

## 프로그램 실행 
- 첫 화면에서 `Setup`단추를 클릭하여 모니터링에 필요한 각종 설정을 진행합니다.
- 설정화면에서 모니터링이 필요한 프로세스를 클릭하면, Selected Process로 선택됩니다.
- 모니터링 데이터를 저장하는 방법은 SQLite를 이용하여 로컬에 저장하는 방식과 Oracle을 이용하여 클라우드에 저장하는 방식 가운데 선택할 수 있습니다.
- SQLite를 선택한 경우 지정된 폴더에 오늘날짜이름으로 SQLite파일로 생성됩니다(예:20230911.db).
- Oracle을 선택한 경우 입력한 접속정보를 토대로 오라클에 접속을 시도하여 테이블을 만들고 모니터링 데이터를 저장합니다.
- 최소 한개 이상의 프로세스를 선택하고 OK를 누릅니다.
- Monitoring Start를 클릭하여 실행합니다.




