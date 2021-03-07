--Create and update database shema 
set INSTANCE=.\SQLEXPRESS
set DATABASE=DataApi
sqlcmd -S %INSTANCE%  -d master -i DatabaseInit.sql
sqlcmd -S %INSTANCE%  -d %DATABASE% -i Users.sql
sqlcmd -S %INSTANCE%  -d %DATABASE% -i Claims.sql


