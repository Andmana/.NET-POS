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
(1, 'Melon Juice', 'Desc of Jus Melon', 0, 1, GETDATE(), null, null),
(1, 'Sprite', 'Desc of Sprite', 0, 1, GETDATE(), null, null),
(1, 'Ice Tea', 'Desc of Ice Tea', 0, 1, GETDATE(), null, null),
(2, 'Brerad', 'Desc of Bread', 0, 2, GETDATE(), null, null),
(2, 'Hamburger', 'Desc of Hamburger', 0, 2, GETDATE(), null, null),
(2, 'Pizza', 'Desc of Pizza', 0, 2, GETDATE(), null, null),
(3, 'Ice Cream', 'Desc of Ice Cream', 0, 1, GETDATE(), null, null),
(3, 'Cake', 'Desc of Cake', 0, 1, GETDATE(), null, null),
(4, 'Mango', 'Desc of Mango', 0, 1, GETDATE(), null, null),
(5, 'Tomtato', 'Desc of Pie', 0, 1, GETDATE(), null, null);

select * from TblVariant;


drop table if exists TblProduct;
create table TblProduct
(
	Id				int primary key identity(1,1),
	IdVariant		int not null,
	NameProduct		varchar(50) not null,
	Price			decimal(18,0) not null,
	Stock			int not null,
	Image			varchar(max),


	IsDelete		bit,
	CreateBy		int not null,
	CreateDate		datetime not null,
	UpdateBy		int,
	UpdateDate		datetime
)

insert into TblProduct values
(6, 'Bread Garlic', 15000, 100, 'bread_garlic.jpeg', 0, 1, GETDATE(), null, null),
(7, 'Ice Cream', 25000, 100, 'ice_cream_gelato.jpg', 0, 1, GETDATE(), null, null);

select * from TblProduct;

drop table if exists TblOrderHeader
create table TblOrderHeader
(
	Id				int primary key identity(1,1),
	CodeTransaction nvarchar(20) not null,
	IdCostumer		int not null,
	Amount			decimal (18,2) not null,
	TotalQty		int not null,
	IsCheckout		bit not null,

	IsDelete		bit,
	CreateBy		int not null,
	CreateDate		datetime not null,
	UpdateBy		int,
	UpdateDate		datetime
)

drop table if exists TblOrderDetail
create table TblOrderDetail
(
	Id				int primary key identity(1,1),
	IdHeader		nvarchar(20) not null,
	IdProduct		int not null,
	Qty				decimal (18,2) not null,
	SumPrice		int not null,

	IsDelete		bit,
	CreateBy		int not null,
	CreateDate		datetime not null,
	UpdateBy		int,
	UpdateDate		datetime
)
