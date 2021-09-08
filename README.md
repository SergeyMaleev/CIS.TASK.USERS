Перейти в папку Api сервера
```
cd Backend\Api
```
Запустить миграции, создать базу данных MS SQL. При необходимости возможно задать свой путь и доступ в appsettings.json
```
dotnet ef database update
```
Запуск API сервера
```
dotnet run
```
Api сервер
```
https://localhost:5001
```

Запуск клиента
```
cd Frontend\cis-userTest-client
npm i
ng serve
```
клиент 
```
localhost:4200
```
