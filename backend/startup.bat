@echo off
chcp 65001 >nul
echo ========================================
echo Takt.Net 后端服务启动
echo ========================================
echo.
echo 正在启动开发服务器（支持热重载）...
echo 按 Ctrl+C 停止服务
echo.
dotnet watch --project src/Takt.WebApi run