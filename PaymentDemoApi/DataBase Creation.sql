--j'ai utilisee la database first approach 

create database paymentDemo
go
use paymentDemo
go

Create table CoOwner(
Id int identity(1,1) primary key,
Name varchar(50) not null,
Balance money default 0 ,
MonthlyFee money not null ,
)


Create table MonthDetails(
TransactionId int identity(1,1) primary key,
CoOwnerId int foreign key references CoOwner(Id),
MonthNum int not null check (MonthNum between 1 and 12),
isPaid varchar(5) not null check (isPaid in ('true','false')), 
AmmountPaid money default 0,

)