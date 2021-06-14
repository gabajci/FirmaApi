USE [FirmaDb]
GO
CREATE TABLE [Employee](
	[Id] [int] NOT NULL,
	[Surname] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[SignInDate] [date] NOT NULL,
	[LeaveDate] [date],
	[Title] [char](20),
	[PhoneNumber] [char](12),
	[Mail] [char](50),
	PRIMARY KEY (Id)
);
GO
CREATE TABLE [Company](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[LeaderId] [int] NOT NULL,	
	[PhoneNumber] [char](50),
	PRIMARY KEY (Id),
	FOREIGN KEY (LeaderId) REFERENCES Employee(Id),
);
GO
CREATE TABLE [Division](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[LeaderId] [int] NOT NULL,	
	[CompanyId] [int] NOT NULL,
	[PhoneNumber] [char](50),
	PRIMARY KEY (Id),
	FOREIGN KEY (CompanyId) REFERENCES Company(Id),
	FOREIGN KEY (LeaderId) REFERENCES Employee(Id),
);
CREATE TABLE [Project](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[LeaderId] [int] NOT NULL,	
	[DivisionId] [int] NOT NULL,
	[PhoneNumber] [char](50),
	PRIMARY KEY (Id),
	FOREIGN KEY (DivisionId) REFERENCES Division(Id),
	FOREIGN KEY (LeaderId) REFERENCES Employee(Id),
);
GO
CREATE TABLE [Department](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[LeaderId] [int] NOT NULL,	
	[ProjectId] [int] NOT NULL,
	[PhoneNumber] [char](50),
	PRIMARY KEY (Id),
	FOREIGN KEY (ProjectId) REFERENCES Project(Id),
	FOREIGN KEY (LeaderId) REFERENCES Employee(Id),
);
GO
CREATE TABLE [WorkAssignment](
	[Id] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date],
	PRIMARY KEY (Id),
	FOREIGN KEY (EmployeeId) REFERENCES Employee(Id),
	FOREIGN KEY (DepartmentId) REFERENCES Department(Id),
);


--create trigger employee_mail_missingDot
--on [dbo].[Employee]
--for insert
--as	 
--	IF EXISTS(select * from Employee where Mail NOT LIKE '%.%')
--	begin
--		RAISERROR('v maili chyba bodka',10,1)
--		ROLLBACK TRANSACTION;
--	end
--Go
--toto je zakomentovane naschval, nakoniec som zvolil constraint ktory je o 2 riadky nizsie

GO
alter table dbo.Employee
	add constraint employee_mail_missingDot CHECK (Mail LIKE '%.%')
GO


create trigger employee_still_working
on [dbo].[WorkAssignment]
instead of insert
as
BEGIN
	declare @newId int;
	declare @newEmpl int;
	declare @newDepId int;
	declare @newStartDate date;
	declare @newEndDate date;

	select @newId = inserted.Id from inserted;
	select @newEmpl = inserted.EmployeeId from inserted;
	select @newDepId = inserted.DepartmentId from inserted;
	select @newStartDate = inserted.StartDate from inserted;
	select @newEndDate = inserted.EndDate from inserted;
	IF EXISTS(select * from WorkAssignment where EmployeeId = @newEmpl and EndDate is null) 
		BEGIN
			RAISERROR('zamestnanec ma neukoncenu zmluvu',10,1)
			ROLLBACK TRANSACTION
		END
	ELSE
	BEGIN
		INSERT INTO [WorkAssignment]
			([Id],[EmployeeId],[DepartmentId],[StartDate],[EndDate])
			VALUES (@newId,@newEmpl,@newDepId,@newStartDate,@newEndDate)
	END
END
GO


INSERT INTO [Employee] 
			([Id],[Surname],[LastName],[SignInDate],[LeaveDate],[Title],[PhoneNumber],[Mail]) 
			VALUES (1,'Martin','Ratkovský',SYSDATETIME(),null,'Bc.','0950460795','martin@gmail.com')
GO

INSERT INTO [Employee] 
			([Id],[Surname],[LastName],[SignInDate],[LeaveDate],[Title],[PhoneNumber],[Mail]) 
			VALUES (2,'Jozef','Kysel',SYSDATETIME(),null,null,'0950460789','jozef@gmail.com')
GO

INSERT INTO [Employee] 
			([Id],[Surname],[LastName],[SignInDate],[LeaveDate],[Title],[PhoneNumber],[Mail]) 
			VALUES (3,'Patrik','Bodnár',SYSDATETIME(),null,'Mgr.','0950460555','patrik@gmail.com')
GO

INSERT INTO [Employee] 
			([Id],[Surname],[LastName],[SignInDate],[LeaveDate],[Title],[PhoneNumber],[Mail]) 
			VALUES (4,'Andrej','Suchár',SYSDATETIME(),null,'Ing.','0950460456','andrej@gmail.com')
GO
INSERT INTO [Employee] 
			([Id],[Surname],[LastName],[SignInDate],[LeaveDate],[Title],[PhoneNumber],[Mail]) 
			VALUES (5,'Marek','Matiaško',SYSDATETIME(),null,null,'0950460852','marek@gmail.com')
GO
INSERT INTO [Employee] 
			([Id],[Surname],[LastName],[SignInDate],[LeaveDate],[Title],[PhoneNumber],[Mail]) 
			VALUES (6,'Vladimír','Šprla',SYSDATETIME(),null,'Ing. Phdr.','0950460123','vladimir@gmail.com')
GO
INSERT INTO [Employee] 
			([Id],[Surname],[LastName],[SignInDate],[LeaveDate],[Title],[PhoneNumber],[Mail]) 
			VALUES (7,'Roman','Veľký',SYSDATETIME(),null,null,'0950460999','roman@gmail.com')
GO
INSERT INTO [Company]
			([Id],[Name],[LeaderId],[PhoneNumber])
			VALUES (1,'It-produkty',1,'+421526/725')
GO
INSERT INTO [Division]
			([Id],[Name],[LeaderId],[CompanyId],[PhoneNumber])
			VALUES (1,'Management',2,1,'+421526/725')
GO
INSERT INTO [Project]
			([Id],[Name],[LeaderId],[DivisionId],[PhoneNumber])
			VALUES (1,'FrontEnd',3,1,'+421526/725')
GO
INSERT INTO [Department]
			([Id],[Name],[LeaderId],[ProjectId],[PhoneNumber])
			VALUES (1,'Internet/Intranet',4,1,'+421526/725')
GO

INSERT INTO [WorkAssignment]
			([Id],[EmployeeId],[DepartmentId],[StartDate],[EndDate])
			VALUES (1,5,1,SYSDATETIME(),null)
GO
INSERT INTO [WorkAssignment]
			([Id],[EmployeeId],[DepartmentId],[StartDate],[EndDate])
			VALUES (2,1,1,SYSDATETIME(),null)
GO
INSERT INTO [WorkAssignment]
			([Id],[EmployeeId],[DepartmentId],[StartDate],[EndDate])
			VALUES (3,6,1,SYSDATETIME(),null)
GO
INSERT INTO [WorkAssignment]
			([Id],[EmployeeId],[DepartmentId],[StartDate],[EndDate])
			VALUES (4,7,1,SYSDATETIME(),null)
GO







