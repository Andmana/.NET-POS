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


insert into TblCategory(NameCategory, Description, IsDelete, CreateBy, CreateDate, UpdateBy,UpdateDate) values
('Drink', 'Desc of drinks', 0, 1, GETDATE(), null, null),
('Food', 'Desc of Food', 0, 2, GETDATE(), null, null),
('Dessert', 'Desc of Dessert', 0, 1, GETDATE(), null, null),
('Fruit', 'Desc of Fruit', 0, 2, GETDATE(), null, null),
('Vegetables', 'Desc of Vegetables', 0, 2, GETDATE(), null, null);
select * from TblCategory;

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
(3 , 'Ice Cream', 'Desc of Ice Cream', 0, 1, GETDATE(), null, null),
(2 , 'Fried Chicken', 'Desc of Fried Chicken', 0, 2, GETDATE(), null, null),
(1 , 'Soda', 'Desc of Soda', 0, 1, GETDATE(), null, null),
(1 , 'Sprite', 'Desc of Sprite', 0, 1, GETDATE(), null, null),
(1 , 'Fanta', 'Desc of Fanta', 0, 1, GETDATE(), null, null),
(2 , 'Pizza', 'Desc of Pizza', 0, 2, GETDATE(), null, null),
(2 , 'Burger', 'Desc of Burger', 0, 2, GETDATE(), null, null),
(2 , 'Hamburger', 'Desc of Hamburger', 0, 2, GETDATE(), null, null),
(3, 'Cake', 'Desc of Cake', 0, 1, GETDATE(), null, null),
(3, 'Candy', 'Desc of Candy', 0, 2, GETDATE(), null, null),
(1, 'Ice Tea', 'Desc of Ice Tea', 0, 1, GETDATE(), null, null),
(2, 'Fries', 'Desc of Fries', 0, 1, GETDATE(), null, null),
(2, 'Pork', 'Desc of Pork', 0, 2, GETDATE(), null, null),
(3, 'Pie', 'Desc of Pie', 0, 1, GETDATE(), null, null),
(3, 'Banana Fried', 'Desc of Banana Fried', 0, 1, GETDATE(), null, null);

select * from TblVariant;


