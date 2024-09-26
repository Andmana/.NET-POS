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
('Food', 'Desc of Food', 0, 2, GETDATE(), null, null);