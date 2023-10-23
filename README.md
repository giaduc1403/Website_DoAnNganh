Hướng dẫn chạy project Đồ Án Ngành.
Bước 1: Thực hiện add CSDL Model vào bài.
+ Cách 1: Sử dụng Restore Database trong SQL Server để add Database vào bằng file "CHBHTH.
    - Sau đó mở project vào phần appsettings.json đổi đường dẫn ConnectionString bằng tên máy đang sử dụng trong SQL Server.
    - Tiếp theo bấm vào Tools -> NuGet Package Manager -> Package Manager Console.
    - Thực hiện nhập lệnh : Scaffold-DbContext "Data Source=MSI\\GIADUC14;Initial Catalog=CHBHTH;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
    * Lưu ý thay đổi Data Source= "Tên máy sử dụng trong SQL Server".
    - Enter để chạy Model.
+ Cách 2: Vào SQL Server tạo một Database rỗng tên CHBHTH
    - Tiếp theo bấm vào Tools -> NuGet Package Manager -> Package Manager Console.
    - Sau đó mở project vào phần appsettings.json đổi đường dẫn ConnectionString bằng tên máy đang sử dụng trong SQL Server.
    - Thực hiện lệnh: Update-Database.
    - Enter chạy sẽ lấy được dữ liệu bảng về SQL Server.
Bước 2: Nếu Visual Studio 2022 chưa có tools thực hiện cài:
    - bấm vào Tools -> NuGet Package Manager -> Manager NuGet Package Slution...
    - Cài những tools :
    + AspNetCoreHero.ToastNotification
    + Microsoft.AspNet.Mvc
    + Microsoft.EntityFrameworkCore
    + Microsoft.EntityFrameworkCore.Design
    + Microsoft.EntityFrameworkCore.SqlServer
    + Microsoft.EntityFrameworkCore.Tools
    + Microsoft.VisualStudio.Web.CodeGeneration.Design
    + PagedList.Core.Mvc
Vậy là đã chạy được bài.
    