Пакеты:
 Слой Application : FluentValidation 11.10.0; FluentValidation.DependencyInjectionExtensions 11.10.0;
 Слой Domain : Microsoft.EntityFrameworkCore.Design 8.0.10;
 Слой DAL : Microsoft.EntityFrameworkCore 8.0.10; Microsoft.EntityFrameworkCore.Design 8.0.10; Microsoft.EntityFrameworkCore.Tools 8.0.10; Npgsql.EntityFrameworkCore.PostgreSQL 8.0.8;
 Слой WebApi : Microsoft.EntityFrameworkCore.Design 8.0.10;

Инструкиця по запуску - Вставьте вашу строку подключения к бд Postgresql в Appsetings.json либо в секреты пользователей в таком формате - "ConnectionStrings": {
  "PostgresSQL": "Ваша строка подлкючения"
} 

Далее перейдите в Nuget пакет менеджер и выберите слой DAL, дальше сделайте миграцию командой - add-migration ИмяМиграции , после этого обновите БД командой - update-database

Все должно работать


 forceii / robolainapp - добавил Бд + asp.net core в докер.
