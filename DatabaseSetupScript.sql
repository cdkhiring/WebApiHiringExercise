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

CREATE TABLE dbo.RepairOrderStatus
(
	RepairOrderStatusID int primary key identity(1,1)
	,Name nvarchar(20) Not Null
);

CREATE TABLE dbo.RepairOrder
(
	RepairOrderID int primary key identity(1,1)
	,RepairOrderStatusID int NOT NULL
	,VehicleID int NOT NULL
	,RepairOrderDate datetime2(7) NOT NULL
);

ALTER TABLE dbo.RepairOrder ADD CONSTRAINT FK_RepairOrder_RepairOrderStatus
FOREIGN KEY (RepairOrderStatusID) REFERENCES RepairOrderStatus(RepairOrderStatusID);

ALTER TABLE dbo.RepairOrder ADD CONSTRAINT FK_RepairOrder_Vehicle
FOREIGN KEY (VehicleID) REFERENCES Vehicle(VehicleID);

CREATE TABLE dbo.OpCode
(
	OpCodeId int primary key identity(1,1)
	,OpCode nvarchar(10) NOT NULL
	,[Description] nvarchar(100) NULL
	,Category nvarchar(50) NULL
	,PartsCost money NOT NULL
	,LaborCost money NOT NULL
	,EstimatedHours decimal(4,2)
);

CREATE TABLE dbo.ServiceLine
(
	ServieLineId int primary key identity(1,1)
	,RepairOrderId int NOT NULL
	,OpCodeId int NOT NULL
	,MiscellaneousFee money NULL
);

ALTER TABLE dbo.ServiceLine ADD CONSTRAINT FK_ServiceLine_RepairOrder
FOREIGN KEY (RepairOrderId) REFERENCES RepairOrder(RepairOrderId);

ALTER TABLE dbo.ServiceLine ADD CONSTRAINT FK_ServiceLine_OpCode
FOREIGN KEY (OpCodeId) REFERENCES OpCode(OpCodeId);

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

INSERT INTO dbo.RepairOrderStatus (Name) VALUES
('Scheduled'),
('No Show'),
('Working'),
('Completed'),
('Canceled');

INSERT INTO dbo.RepairOrder (RepairOrderStatusID, VehicleID, RepairOrderDate) VALUES
(1, 1, @Now),
(1, 2, @Now),
(2, 3, DateAdd(Day, 1, @Now)),
(2, 4, DateAdd(Day, 1, @Now)),
(3, 5, DateAdd(Day, 3, @Now)),
(4, 6, DateAdd(Day, 2, @Now)),
(5, 7, @Now),
(1, 8, DateAdd(Day, 1, @Now)),
(3, 9, DateAdd(Day, 4, @Now)),
(1, 1, DateAdd(Month, 6, @Now)),
(1, 1, DateAdd(Month, 11, @Now));

INSERT INTO dbo.OpCode(OpCode, [Description], Category, PartsCost, LaborCost, EstimatedHours) VALUES
('OILCNS', 'Oil Change (Non-Synthetic)', 'Lubrication', 29.99, 30, 1),
('OILCPS', 'Oil Change (Partial Synthetic)', 'Lubrication', 39.99, 30, 1),
('OILCFS', 'Oil Change (Full Synthetic)', 'Lubrication', 49.99, 30, 1),
('BRKINSP', 'Brake Inspection', 'Brakes', 0, 0, 0.5),
('BRKMD', 'Machine Discs', 'Brakes', 0, 10, 0.5),
('BRKRS', 'Replace Shoes (1 Pair)', 'Brakes', 100, 50, 1),
('BRKRD', 'Replace Discs (1 Pair)', 'Brakes', 60, 50, 0.5);

INSERT INTO dbo.ServiceLine(RepairOrderID, OpCodeId, MiscellaneousFee) VALUES
(1, 1, 10),
(2, 3, 0),
(3, 4, 0),
(3, 6, 10),
(3, 7, 0),
(4, 1, 10),
(5, 3, 0),
(6, 4, 0),
(6, 6, 10),
(6, 7, 0),
(7, 1, 10),
(8, 3, 0),
(9, 4, 0),
(9, 6, 10),
(9, 7, 0),
(10, 3, 10),
(10, 4, 0),
(11, 3, 10);