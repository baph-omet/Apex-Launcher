@ECHO off
REG ADD 'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts' %1 /t REG_SZ %2