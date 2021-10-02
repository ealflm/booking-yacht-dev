@echo off
cd BookingYacht.Data
dotnet ef dbcontext scaffold "Server=booking-yacht-dev.database.windows.net;Database=BookingYacht;TrustServerCertificate=true;User Id=swd391gr5;Password=Password@3915" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context-dir Context --force --no-onconfiguring
if %errorlevel%==0 (echo Done!) else (echo Failed!)
pause