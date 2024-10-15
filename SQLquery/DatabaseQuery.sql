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
(1, 'Juice', 'Desc', 0, 1, GETDATE(), null, null),
(1, 'Sprite', 'Desc of Sprite', 0, 1, GETDATE(), null, null),
(1, 'Tea', 'Desc of Ice Tea', 0, 1, GETDATE(), null, null),
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
(7, 'Ice Cream', 25000, 100, 'ice_cream_gelato.jpg', 0, 1, GETDATE(), null, null),
(6,	'Chessy Pizza',	115000,	30,	'Pizza.jpg',	0,	1,	GETDATE(),	NULL,	NULL),
(5,	'Hamburger Beef',	35000,	50,	'Hamburger.jpg',	0,	1,	GETDATE(),	NULL,	NULL),
(7,	'Ice Cream Choco',	25000,	50,	'Ice_choco.jpg',	0,	1,	GETDATE(),	NULL,	NULL);

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
	IdHeader		int not null,
	IdProduct		int not null,
	Qty				int not null,
	SumPrice		decimal (18,2) not null,

	IsDelete		bit,
	CreateBy		int not null,
	CreateDate		datetime not null,
	UpdateBy		int,
	UpdateDate		datetime
)

drop table if exists TblCustomer
create table TblCustomer
(
	Id				int primary key identity(1,1),
	NameCustomer	nvarchar(50) not null,
	Email			nvarchar(50) not null,
	Password		nvarchar(50) not null,
	Address			nvarchar(max) not null,
	Phone			nvarchar(15) not null,
	IdRole			nvarchar(50) not null,

	IsDelete		bit,
	CreateBy		int not null,
	CreateDate		datetime not null,
	UpdateBy		int,
	UpdateDate		datetime
)

drop table if exists TblRole
create table TblRole
(
	Id				int primary key identity(1,1),
	RoleName		nvarchar(80) not null,
	
	IsDelete		bit,
	CreatedBy		int not null,
	CreatedDate		datetime not null,
	UpdatedBy		int,
	UpdatedDate		datetime

)

insert into TblRole VALUES
('Administrator', 0, 1, GETDATE(), null, null),
('Customer', 0, 1, GETDATE(), null, null);

SELECT * FROM TblRole

drop table if exists TblCustomer
create table TblCustomer
(
	Id				int primary key identity(1,1),
	NameCustomer	nvarchar(50) not null,
	Email			nvarchar(50) not null,
	Password		nvarchar(50) not null,
	Address			nvarchar(max) not null,
	Phone			nvarchar(15) not null,
	IdRole			int not null,

	IsDelete		bit,
	CreateBy		int not null,
	CreateDate		datetime not null,
	UpdateBy		int,
	UpdateDate		datetime
)

insert into TblCustomer values
('Admin', 'admin@mail.com', 'admin', 'Jakarta', '081-xxx-xxx' , 1, 0, 1, GETDATE(), null, null),
('Buyer', 'buyer@mail.com', 'buyer', 'Jakarta', '082-xxx-xxx' , 1, 0, 1, GETDATE(), null, null)

SELECT * FROM TblCustomer

drop table if exists TblMenu
create table TblMenu(
	Id				int primary key identity(1,1) not null,
	MenuName		varchar(80) not null,
	MenuAction		varchar(80) not null,
	MenuController	varchar(80) not null,
	MenuIcon		varchar(80) null,
	MenuSorting		int null,
	IsParent		bit null,
	MenuParent		int null,
	IsDelete		bit null,
	CreatedBy		int not null,
	CreatedDate		datetime not null,
	UpdatedBy		int null,
	UpdatedDate		datetime null
)
drop table if exists TblMenuAccess
create table TblMenuAccess(
	Id				int primary key identity(1,1) not null,
	RoleId			int null,
	MenuId			int null,
	IsDelete		bit null,
	CreatedBy		int not null,
	CreatedDate		datetime not null,
	UpdatedBy		int null,
	UpdatedDate		datetime null
)

INSERT INTO TblMenu (MenuName, MenuAction, MenuController, MenuIcon, MenuSorting, IsParent, MenuParent, IsDelete, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate) 
VALUES 
(N'Home', N'Index', N'Home', N'home', 1, 0, 15, 0, 1, GETDATE(), NULL, NULL),
(N'Friends', N'Index', N'Friends', N'tag', 2, 0, 15, 0, 1, GETDATE(), NULL, NULL),
(N'CategoryTry', N'Index', N'CategoryTry', N'calendar', 3, 0, 15, 0, 1, GETDATE(), NULL, NULL),
(N'VariantTry', N'Index', N'VariantTry', N'message-square', 4, 0, 15, 0, 1, GETDATE(), NULL, NULL),
(N'Category', N'Index', N'Category', N'tag', 5, 0, 15, 0, 1, GETDATE(), NULL, NULL),
(N'Variant', N'Index', N'Variant', N'calendar', 6, 0, 15, 0, 1, GETDATE(), NULL, NULL),
(N'Product', N'Index', N'Product', N'message-square', 7, 0, 15, 0, 1, GETDATE(), NULL, NULL),
(N'Menu Catalog', N'Catalog', N'Order', N'tag', 8, 0, 16, 0, 1, GETDATE(), NULL, NULL),
(N'History Order', N'HistoryOrder', N'Order', N'message-square', 9, 0, 16, 0, 1, GETDATE(), NULL, NULL),
(N'Product Kalbe', N'Index', N'ProductKalbe', N'tag', 13, 0, 16, 0, 1, GETDATE(), NULL, NULL),
(N'Customer Kalbe', N'Index', N'CustomerKalbe', N'calendar', 14, 0, 16, 0, 1, GETDATE(), NULL, NULL),
(N'Penjualan Kalbe', N'Index', N'PenjualanKalbe', N'message-square', 15, 0, 16, 0, 1, GETDATE(), NULL, NULL),
(N'Role', N'Index', N'Role', N'tag', 10, 0, 15, 0, 1, GETDATE(), NULL, NULL),
(N'Customer', N'Index', N'Customer', N'calendar', 12, 0, 15, 0, 1, GETDATE(), NULL, NULL),
(N'Master', N'', N'', N'tag', 1, 1, 0, 0, 1, GETDATE(), NULL, NULL),
(N'Transaction', N'', N'', N'calendar', 2, 1, 0, 0, 1, GETDATE(), NULL, NULL),
(N'Atur Menu Access', N'Index_MenuAccess', N'Role', N'tag', 11, 0, 15, 0, 1, GETDATE(), NULL, NULL),
(N'Spesialisasi Dokter', N'Index', N'MSpecialization', N'calendar', 16, 0, 15, 0, 1, GETDATE(), NULL, NULL);

INSERT INTO TblMenuAccess (RoleId, MenuId, IsDelete, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate) VALUES 
(1, 2, 0, 1, GETDATE(), NULL, NULL),
(1, 3, 0, 1, GETDATE(), NULL, NULL),
(1, 1, 0, 1, GETDATE(), NULL, NULL),
(1, 4, 0, 1, GETDATE(), NULL, NULL),
(1, 5, 0, 1, GETDATE(), NULL, NULL),
(1, 6, 0, 1, GETDATE(), NULL, NULL),
(1, 7, 0, 1, GETDATE(), NULL, NULL),
(1, 8, 0, 1, GETDATE(), NULL, NULL),
(1, 9, 0, 1, GETDATE(), NULL, NULL),
(1, 10, 0, 1, GETDATE(), NULL, NULL),
(1, 11, 0, 1, GETDATE(), NULL, NULL),
(1, 12, 0, 1, GETDATE(), NULL, NULL),
(1, 13, 0, 1, GETDATE(), NULL, NULL),
(1, 14, 0, 1, GETDATE(), NULL, NULL),
(1, 15, 0, 1, GETDATE(), NULL, NULL),
(1, 16, 0, 1, GETDATE(), NULL, NULL),
(1, 17, 0, 1, GETDATE(), NULL, NULL);
