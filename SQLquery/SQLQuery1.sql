--create database XPOS_341;

use XPOS_341;

drop table if exists TblCategory;
create table TblCategory
(
	Id				int Primary key,
	NameCategory	varchar(50) not null,
	Description		varchar(max),
	IsDelete		bit,
	CreateBy		int not null,
	CreateDate		datetime not null,
	UpdateBy		int,
	UpdateDate		datetime
);

select * from TblCategory;

insert into TblCategory values
(1, 'Drink', 'Desc of drinks', 0, 1, GETDATE(), null, null),
(2, 'Food', 'Desc of Food', 0, 2, GETDATE(), null, null);