--UC-1-Create-Payroll-Service-Database--
create database Payroll_Service;
use Payroll_Service;

--UC2 Employee Payroll Table------------
Create Table employee_payroll(
	Id int identity (1,1) primary key,
	Name varchar(200),
	Salary float,
	StartDate date);

--UC3-- Create employee_payroll data as part of CURD Operation
insert into employee_payroll (Name, Salary, StartDate) values
('Dhoni', 70000.00, '2007-02-03'),
('Virat', 50000.00, '2010-05-04'),
('Rohit', 60000.00, '2009-06-09'),
('Smrithi', 40000.00, '2018-03-05'),
('Mithai', 50000.00, '2020-08-02');

--UC4-- Retrieve employee_payroll data
select * from employee_payroll;

--UC5-- Retrieve salary of particular employee and particular date range
select salary from employee_payroll where Name = 'Virat';
select * from employee_payroll where StartDate between cast ('2018-01-01' as date) and GETDATE();

--UC6-- add Gender to Employee_Payroll Table and Update the Rows to reflect the correct Employee Gender
alter table employee_payroll add Gender char(1);
update employee_payroll set Gender = 'M' where Id in (1,2,3);
update employee_payroll set Gender = 'F' where Id in (4,5);

--UC7-- find sum, average, min, max and number of male and female employees
select sum(Salary) as sumsalary,Gender from employee_payroll group by Gender;
select avg(Salary) as avgsalary,Gender from employee_payroll group by Gender; 
select max(Salary) as maxsalary,Gender from employee_payroll group by Gender; 
select min(Salary) as minsalary,Gender from employee_payroll group by Gender; 
select count(Name) as EmployeeCount,Gender from employee_payroll group by Gender;  

--UC8-- add employee phone, department(not null), Address (using default values)
alter table employee_payroll add Phone bigint;
update employee_payroll set Phone = 1234567890; 
update employee_payroll set Phone = 1234569874 where ID IN (2,4); 
alter table employee_payroll add Address varchar(100) not null default 'Bangalore';
alter table employee_payroll add Department varchar(250) not null default 'IT';

--UC9-- Extend table to have Basic Pay, Deductions, Taxable Pay, Income Tax, Net Pay.
alter table employee_payroll add Basic_Pay float not null default 0.00;
update employee_payroll set Basic_Pay=70000.00 where Id=1; 
update employee_payroll set Basic_Pay=50000.00 where Id=2;
update employee_payroll set Basic_Pay=60000.00 where Id=3;
update employee_payroll set Basic_Pay=40000.00 where Id=4;
update employee_payroll set Basic_Pay=50000.00 where Id=5;
alter table employee_payroll add 
Deductions float not null default 0.00,
Taxable_Pay float not null default 0.00, 
Income_Tax float not null default 0.00,
Net_Pay float not null default 0.00;
update employee_payroll set Net_Pay = (Basic_Pay-Deductions-Taxable_Pay-Income_Tax);
select * from employee_payroll;

--UC10-- Two departments for same employee
insert into employee_payroll (Name, Salary, StartDate, Gender, Phone, Address, Department, Basic_Pay, Deductions, Taxable_Pay, Income_Tax)
values ('Virat', 60000.00, '2011-05-05', 'M', 7894561230, 'Bangalore', 'RCB', 60000.00, 1000.00, 2000.00, 1500.00);
update employee_payroll set Net_Pay = (Basic_Pay-Deductions-Taxable_Pay-Income_Tax);

--ER diagram--
create table Department
(
Dept_Id int foreign key references employee_payroll(Id),
Department varchar(100)
);

--drop table Department;
insert into employee_payroll (Name, Basic_pay, StartDate, Gender, Phone, Address, Department, Deductions, Taxable_Pay, Income_Tax,Net_pay)
values ('Akshay', 60000.00, '2011-05-05', 'M', 7999837990, 'Bangalore', 'SD',1000.00, 59000.00, 2000.00,0);
update employee_payroll set Net_Pay = (Basic_Pay-Deductions-Taxable_Pay-Income_Tax);

--Stored procedure--
create procedure spAddEmployees
@Name varchar(100),
@Startdate Date,
@Gender char(1),
@Phone bigint,
@Department varchar(100),
@Address varchar(100),
@Basic_Pay float,
@Deductions float,
@Taxable_pay float,
@Income_tax float,
@Net_pay float
as
insert into employee_payroll (Name, Startdate, Gender, Phone, Address, Department, Basic_Pay, Deductions, Taxable_pay, Income_tax, Net_pay)
	values(@Name, @Startdate, @Gender, @Phone, @Address, @Department, @Basic_Pay, @Deductions, @Taxable_pay, @Income_tax, @Net_pay);

--Update procedure--
create procedure spUpdateEmployee
@Name varchar(100),
@Id int,
@Basic_Pay float
as
update employee_payroll set Basic_pay = @Basic_Pay where Id=@Id and Name= @Name;

--delete employee
create procedure spDeleteEmployee
@Name varchar(100),
@Id int
as
delete from employee_payroll where Id=@Id and Name = @Name;

--Payroll-Details-Table--
create table Payroll_Details
(
SalaryId int identity(1,1) primary key,
EmpId int FOREIGN KEY REFERENCES employee_payroll(Id),
Salary float
);

--Insert-Into-Two-Tables-Procedure--
create procedure spInsertIntoTwoTables
(
@Name varchar(100),
@Address varchar(100),
@Gender char(1),
@id int output
)
as 
insert into employee_payroll(Name, Address, Gender) values(@Name, @Address,@Gender);
set @id = SCOPE_IDENTITY()
return @id;
