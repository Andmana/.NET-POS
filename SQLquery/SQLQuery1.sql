--create database XPOS_341;

use XPOS_341;

drop table if exists TblCategory;
create table TblCategory
(
	Id				int Primary key identity(1,1),
	NameCategory	varchar(50) not null,
	Description		varchar(max),
	IsDelete		bit,
	CreateBy		int not null,
	CreateDate		datetime not null,
	UpdateBy		int,
	UpdateDate		datetime
);

select * from TblCategory;

insert into TblCategory(NameCategory, Description, IsDelete, CreateBy, CreateDate, UpdateBy,UpdateDate) values
('Drink', 'Desc of drinks', 0, 1, GETDATE(), null, null),
('Food', 'Desc of Food', 0, 2, GETDATE(), null, null),
('Dessert', 'Desc of Dessert', 0, 1, GETDATE(), null, null);

drop table if exists TblVariant;
create table TblVariant
(
	Id				int primary key identity(1,1),
	IdCategory		int not null,
	NameVariant		varchar(50) not null,
	Description		varchar(max),
	IsDelete		bit,
	CreateBy		int not null,
	CreateDate		datetime not null,
	UpdateBy		int,
	UpdateDate		datetime
)

insert into TblVariant values
(1 , 'Melon Juice', 'Desc of Jus Melon', 0, 1, GETDATE(), null, null),
(2 , 'Ice Cream', 'Desc of Ice Cream', 0, 3, GETDATE(), null, null),
(3 , 'Fried Chicken', 'Desc of Fried Chicken', 0, 2, GETDATE(), null, null),
(4 , 'Soda', 'Desc of Soda', 0, 1, GETDATE(), null, null),
(5 , 'Sprite', 'Desc of Sprite', 0, 1, GETDATE(), null, null),
(6 , 'Fanta', 'Desc of Fanta', 0, 1, GETDATE(), null, null),
(7 , 'Pizza', 'Desc of Pizza', 0, 2, GETDATE(), null, null),
(8 , 'Burger', 'Desc of Burger', 0, 2, GETDATE(), null, null),
(9 , 'Hamburger', 'Desc of Hamburger', 0, 2, GETDATE(), null, null),
(10, 'Cake', 'Desc of Cake', 0, 3, GETDATE(), null, null),
(11, 'Candy', 'Desc of Candy', 0, 3, GETDATE(), null, null),
(12, 'Ice Tea', 'Desc of Ice Tea', 0, 1, GETDATE(), null, null),
(13, 'Fries', 'Desc of Fries', 0, 2, GETDATE(), null, null),
(14, 'Pork', 'Desc of Pork', 0, 2, GETDATE(), null, null),
(15, 'Pie', 'Desc of Pie', 0, 3, GETDATE(), null, null),
(16, 'Banana Fried', 'Desc of Banana Fried', 0, 3, GETDATE(), null, null);

select * from TblVariant;


