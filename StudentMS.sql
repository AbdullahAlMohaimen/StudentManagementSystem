create database StudentMS;
use StudentMS;

create table Student(
	student_ID int identity(180104081,1) not null primary key,
	student_Name varchar(50) not null,
	student_Dept varchar(50) not null,
	student_Semester float not null,
	student_CGPA float not null,
	student_DOB Date not null,
	student_Gender varchar(10) not null,
	student_Religion varchar(10) not null,
	student_Address varchar(100) not null,
	student_Email varchar(50) not null unique,
	student_PhoneNo varchar(20) not null unique,
	student_Passeord varchar(20) not null,
);

select * from Student;


create table Admin(
	Admin_ID int identity(10001,1) not null primary key,
	Admin_Name varchar(50) not null,
	Admin_Email varchar(40) not null unique,
	Admin_Password varchar(30) not null,
);

insert into Admin 
	values('Mohaimen Hasan','mohaimen@gmail.com','mohaimen01521'),
		  ('Abdullah Saleh','saleh@gmail.com','saleh01521'),
		  ('Mashfiq Rahman','mashfiq@gmail.com','mashfiq01521');
select * from Admin;

