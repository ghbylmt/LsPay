@echo off
echo ��ǰ�̷���·��%~dp0
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe %~dp0\LsPay.Service.WindowsService.exe
Net Start LsPayService
pause