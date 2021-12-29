:: Set Propmt
@echo off
Color 0F
cls

:: Set the console to use another codepage
chcp 1252>nul

:: Parameters
IF "%~1"=="" (
echo Banco de dados nao informado!
timeout /t 5
exit
)

IF "%~2"=="" (
echo Diretorio onde sera salvo o backup nao informado!
timeout /t 5
exit
)

IF "%~3"=="" (
echo Sufixo do nome do arquivo de backup nao informado!
timeout /t 5
exit
)

set database="%~1"
set directory="%~2"
set nickname=%~3-Database
set user=%4
set pass=%5

:: Get DateTime Format
FOR /f %%a IN ('WMIC OS GET LocalDateTime ^| FIND "."') DO SET DTS=%%a
SET DateTime=%DTS:~0,4%%DTS:~4,2%%DTS:~6,2%%DTS:~8,2%%DTS:~10,2%%DTS:~12,2%

:: Filename
set filename=%directory%\%DateTime%-%nickname%

:: Set firebird path
set path=%PATH%;%ProgramFiles%\Firebird\Firebird_2_5\bin;%ProgramFiles(x86)%\Firebird\Firebird_2_5\bin

::Backup
echo.
echo ------------------------------------------------------
echo Iniciando Backup do Banco de Dados. Aguarde o termino.
echo ------------------------------------------------------
gbak -backup -verbose -user %user% -pass %pass% %database% %filename%.fbk


if exist %filename%.fbk (
	:: Show message
	echo.
	echo ------------------------------------------------------
	echo Backup realizado com sucesso!
	echo ------------------------------------------------------
	echo.
	echo.
	echo ------------------------------------------------------
	echo Iniciando compactacao do arquivo. Aguarde o termino.
	echo ------------------------------------------------------
	echo.
	makecab %filename%.fbk %filename%.cab

	if exist %filename%.cab (
		:: Show message
		echo.
		echo ------------------------------------------------------
		echo Compactacao realizada com sucesso!
		echo ------------------------------------------------------
		echo.
		:: Delete file (.fbk)
		del %filename%.fbk
	) else (
		:: Show message
		echo.
		echo ------------------------------------------------------
		echo Erro ao realizar compactacao!
		echo ------------------------------------------------------
		echo.)
) else (
	:: Show message
	echo.
	echo ------------------------------------------------------
	echo Erro ao realizar backup!
	echo ------------------------------------------------------
	echo.)

:: Show Timeout
@echo off
timeout /t 30
