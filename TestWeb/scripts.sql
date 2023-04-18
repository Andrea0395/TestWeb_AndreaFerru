create database VacationManagement;

use database VacationManagement;

create table Employees (
    Id int not null primary key,
    Name nvarchar(128) not null,
    Surname nvarchar(128) not null,
    TotalVacationDays int not null
);

create table Vacations (
    Id int not null primary key identity,
    Start date not null,
    End date not null,
    EmployeeId int not null references Employees (Id)
);

insert into Employees (Id, Name, Surname, TotalVacationDays) values
(1, 'Mario', 'Rossi', 0),
(2, 'Luigi', 'Neri', 0),
(3, 'Anna', 'Gialli', 0),
(4, 'Barbara', 'Arancioni', 0),
(5, 'Carla', 'Bruni', 0),
(6, 'Elena', 'Rossi', 0);