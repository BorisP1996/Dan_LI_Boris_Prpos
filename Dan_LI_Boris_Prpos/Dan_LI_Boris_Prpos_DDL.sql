--Creating database only if database is not created yet
IF DB_ID('Zadatak_51') IS NULL
CREATE DATABASE Zadatak_51
GO
USE Zadatak_51;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblAbsence')
drop table tblAbsence;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblPatient')
drop table tblPatient;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblDoctor')
drop table tblDoctor;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'vwPatient')
drop view vwPatient;

create table tblPatient (
PatientID int identity(1,1) primary key,
Firstname nvarchar (50) not null ,
Lastname nvarchar (50) not null,
JMBG nvarchar (13) unique not null ,
HealthNumber nvarchar(50) unique not null,
Username nvarchar(50) unique not null,
Pasword nvarchar (50) unique not null,
DoctorID int
)

create table tblDoctor(
DoctorID int identity(1,1) primary key,
Firstname nvarchar (50) not null ,
Lastname nvarchar (50) not null,
JMBG nvarchar (13) unique not null ,
AccountNumber nvarchar(50) unique not null,
Username nvarchar(50) unique not null,
Pasword nvarchar (50) unique not null
)

create table tblAbsence(
AbsenceID int identity(1,1) primary key,
RequestDate date not null,
Reason nvarchar (200),
Company nvarchar (100),
Urgent bit not null,
Responsed bit not null,
PatientID int not null
)

Alter table tblAbsence
add foreign key (PatientID) references tblPatient(PatientID)

GO
CREATE VIEW vwPatient AS
	SELECT	tblPatient.*, 
			tblAbsence.AbsenceID, tblAbsence.RequestDate, tblAbsence.Reason, 
			tblAbsence.Company, tblAbsence.Urgent, tblAbsence.Responsed
	FROM	tblPatient, tblAbsence 
	WHERE	tblPatient.PatientID = tblAbsence.PatientID







