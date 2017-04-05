USE [master]
GO

CREATE DATABASE WebApiHiringExorcise
GO

USE [WebApiHiringExorcise]
GO

CREATE TABLE dbo.Vehicle
(
	VehicleID int primary key identity(1,1)
	,VIN nvarchar(17) NULL
	,[Year] int NULL
	,Make nvarchar(20) NULL
	,Model nvarchar(20) NULL
	,Color nvarchar(20) NULL
);

CREATE TABLE dbo.AppointmentStatus
(
	AppointmentStatusID int primary key identity(1,1)
	,Name nvarchar(20) Not Null
);

CREATE TABLE dbo.Appointment
(
	AppointmentID int primary key identity(1,1)
	,AppointmentStatusID int NOT NULL
	,VehicleID int NOT NULL
	,AppointmentDate datetime2(7) NOT NULL
);

ALTER TABLE dbo.Appointment ADD CONSTRAINT FK_Appointment_AppointmentStatus
FOREIGN KEY (AppointmentStatusID) REFERENCES AppointmentStatus(AppointmentStatusID);

ALTER TABLE dbo.Appointment ADD CONSTRAINT FK_Appointment_Vehicle
FOREIGN KEY (VehicleID) REFERENCES Vehicle(VehicleID);

GO

DECLARE @Now datetime2(7) = SYSDATETIME();

INSERT INTO dbo.Vehicle (VIN, [Year], Make, Model, Color) VALUES
('1FM5K8D80EGB50022', 2014, 'Ford', 'Explorer', 'Black'),
('1P3ES47CXWD636305', 1998, 'Plymouth', 'Neon', 'Red'),
('JM1GJ1U60F1165419', 2015, 'Mazda', 'Mazda6', 'White'),
('3GNAL4EK1ES542499', 2014, 'Chevrolet', 'Captiva', 'Blue'),
('1GNLRFED4AS133312', 2010, 'Chevrolet', 'Traverse', 'Black'),
('4S4BSBDC2F3240401', 2015, 'Suburu', 'Outback', 'White'),
('5N1AT2MT3FC788433', 2015, 'Nissan', 'Rogue', 'Green'),
('3MEHM0JA0AR612164', 2010, 'Mercury', 'Milan', 'White'),
('5J8TB1H28CA001688', 2012, 'Acura', 'RDX', 'Charcoal');

INSERT INTO dbo.AppointmentStatus (Name) VALUES
('Scheduled'),
('No Show'),
('Working'),
('Completed'),
('Canceled');

INSERT INTO dbo.Appointment (AppointmentStatusID, VehicleID, AppointmentDate) VALUES
(1, 1, @Now),
(1, 2, @Now),
(2, 3, DateAdd(Day, 1, @Now)),
(2, 4, DateAdd(Day, 1, @Now)),
(3, 5, DateAdd(Day, 3, @Now)),
(4, 6, DateAdd(Day, 2, @Now)),
(5, 7, @Now),
(1, 8, DateAdd(Day, 1, @Now)),
(3, 9, DateAdd(Day, 4, @Now));