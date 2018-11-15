create database WeCharMark_DataBase;
use WeCharMark_DataBase;
create table subjects
(
	sID int identity(1,1) primary key,
	sSubject varchar(128)
)
create table students
(
	sNo varchar(32) primary key,
	sName varchar(32),
	sPass varchar(256)
)
create table marks
(
	sNo varchar (32) not null foreign key references students (sNo) on  delete cascade ,
	sID int not null foreign key references subjects (sID) on  delete cascade,
	mMark int ,
	constraint mark_pk primary key (sNO,sID)
)

insert into subjects(sSubject) values ('c#'),('·NetWeb'),('软件工程'),('大学英语'),('高等数学');
insert into students (sNo,sName,sPass) values (123,'Admin',123),(0001,'wx',1234),(0002,'ph',1234),(0003,'zzb',1234),(0004,'wds',1234);


