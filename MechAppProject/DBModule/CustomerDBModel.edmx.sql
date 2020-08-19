
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/18/2020 21:54:13
-- Generated from EDMX file: C:\Users\AstiaN\source\repos\MechApp\MechAppProject\DBModule\CustomerDBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MechAppDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Car__CustomerId__108B795B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Car] DROP CONSTRAINT [FK__Car__CustomerId__108B795B];
GO
IF OBJECT_ID(N'[dbo].[FK__Chat__CustomerId__09DE7BCC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Chat] DROP CONSTRAINT [FK__Chat__CustomerId__09DE7BCC];
GO
IF OBJECT_ID(N'[dbo].[FK__Chat__WorkshopId__0AD2A005]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Chat] DROP CONSTRAINT [FK__Chat__WorkshopId__0AD2A005];
GO
IF OBJECT_ID(N'[dbo].[FK__ServiceEv__Custo__2E1BDC42]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceEvent] DROP CONSTRAINT [FK__ServiceEv__Custo__2E1BDC42];
GO
IF OBJECT_ID(N'[dbo].[FK__ServiceEv__Servi__2F10007B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceEvent] DROP CONSTRAINT [FK__ServiceEv__Servi__2F10007B];
GO
IF OBJECT_ID(N'[dbo].[FK__WorkshopS__Works__1ED998B2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkshopServices] DROP CONSTRAINT [FK__WorkshopS__Works__1ED998B2];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[Customer].[Customers]', 'U') IS NOT NULL
    DROP TABLE [Customer].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Car]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Car];
GO
IF OBJECT_ID(N'[dbo].[Chat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chat];
GO
IF OBJECT_ID(N'[dbo].[ServiceEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceEvent];
GO
IF OBJECT_ID(N'[dbo].[WorkshopServices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkshopServices];
GO
IF OBJECT_ID(N'[Workshop].[Workshops]', 'U') IS NOT NULL
    DROP TABLE [Workshop].[Workshops];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [CustomerId] int IDENTITY(1,1) NOT NULL,
    [Login] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Surname] varchar(50)  NOT NULL,
    [City] varchar(50)  NOT NULL,
    [Street] varchar(50)  NOT NULL,
    [StreetNbr] varchar(50)  NOT NULL,
    [ZipCode] varchar(50)  NOT NULL,
    [PhoneNbr] varchar(50)  NOT NULL
);
GO

-- Creating table 'Workshops'
CREATE TABLE [dbo].[Workshops] (
    [WorkshopId] int IDENTITY(1,1) NOT NULL,
    [Login] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [WorkshopName] varchar(50)  NOT NULL,
    [OwerName] varchar(50)  NOT NULL,
    [PhoneNbr] varchar(50)  NOT NULL,
    [City] varchar(50)  NOT NULL,
    [Street] varchar(50)  NOT NULL,
    [StreetNbr] varchar(50)  NOT NULL,
    [ZipCode] varchar(50)  NOT NULL
);
GO

-- Creating table 'Chats'
CREATE TABLE [dbo].[Chats] (
    [ChatId] int IDENTITY(1,1) NOT NULL,
    [CustomerId] int  NOT NULL,
    [WorkshopId] int  NOT NULL,
    [Message] varchar(max)  NOT NULL
);
GO

-- Creating table 'Cars'
CREATE TABLE [dbo].[Cars] (
    [CarId] int IDENTITY(1,1) NOT NULL,
    [CustomerId] int  NOT NULL,
    [Brand] varchar(50)  NOT NULL,
    [Model] varchar(50)  NOT NULL,
    [EngineType] varchar(50)  NOT NULL
);
GO

-- Creating table 'WorkshopServices'
CREATE TABLE [dbo].[WorkshopServices] (
    [ServiceId] int IDENTITY(1,1) NOT NULL,
    [WorkshopId] int  NOT NULL,
    [Title] varchar(max)  NOT NULL,
    [Price] int  NOT NULL,
    [DurationInHrs] int  NOT NULL,
    [Description] varchar(max)  NOT NULL,
    [DurationInMinutes] int  NOT NULL,
    [PriceDecimal] int  NOT NULL
);
GO

-- Creating table 'ServiceEvents'
CREATE TABLE [dbo].[ServiceEvents] (
    [ServiceEventId] int IDENTITY(1,1) NOT NULL,
    [CustomerId] int  NOT NULL,
    [ServiceId] int  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [OrderStatus] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CustomerId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([CustomerId] ASC);
GO

-- Creating primary key on [WorkshopId] in table 'Workshops'
ALTER TABLE [dbo].[Workshops]
ADD CONSTRAINT [PK_Workshops]
    PRIMARY KEY CLUSTERED ([WorkshopId] ASC);
GO

-- Creating primary key on [ChatId] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [PK_Chats]
    PRIMARY KEY CLUSTERED ([ChatId] ASC);
GO

-- Creating primary key on [CarId] in table 'Cars'
ALTER TABLE [dbo].[Cars]
ADD CONSTRAINT [PK_Cars]
    PRIMARY KEY CLUSTERED ([CarId] ASC);
GO

-- Creating primary key on [ServiceId] in table 'WorkshopServices'
ALTER TABLE [dbo].[WorkshopServices]
ADD CONSTRAINT [PK_WorkshopServices]
    PRIMARY KEY CLUSTERED ([ServiceId] ASC);
GO

-- Creating primary key on [ServiceEventId] in table 'ServiceEvents'
ALTER TABLE [dbo].[ServiceEvents]
ADD CONSTRAINT [PK_ServiceEvents]
    PRIMARY KEY CLUSTERED ([ServiceEventId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerId] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [FK__Chat__CustomerId__09DE7BCC]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Chat__CustomerId__09DE7BCC'
CREATE INDEX [IX_FK__Chat__CustomerId__09DE7BCC]
ON [dbo].[Chats]
    ([CustomerId]);
GO

-- Creating foreign key on [WorkshopId] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [FK__Chat__WorkshopId__0AD2A005]
    FOREIGN KEY ([WorkshopId])
    REFERENCES [dbo].[Workshops]
        ([WorkshopId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Chat__WorkshopId__0AD2A005'
CREATE INDEX [IX_FK__Chat__WorkshopId__0AD2A005]
ON [dbo].[Chats]
    ([WorkshopId]);
GO

-- Creating foreign key on [CustomerId] in table 'Cars'
ALTER TABLE [dbo].[Cars]
ADD CONSTRAINT [FK__Car__CustomerId__108B795B]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Car__CustomerId__108B795B'
CREATE INDEX [IX_FK__Car__CustomerId__108B795B]
ON [dbo].[Cars]
    ([CustomerId]);
GO

-- Creating foreign key on [WorkshopId] in table 'WorkshopServices'
ALTER TABLE [dbo].[WorkshopServices]
ADD CONSTRAINT [FK__WorkshopS__Works__1ED998B2]
    FOREIGN KEY ([WorkshopId])
    REFERENCES [dbo].[Workshops]
        ([WorkshopId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__WorkshopS__Works__1ED998B2'
CREATE INDEX [IX_FK__WorkshopS__Works__1ED998B2]
ON [dbo].[WorkshopServices]
    ([WorkshopId]);
GO

-- Creating foreign key on [CustomerId] in table 'ServiceEvents'
ALTER TABLE [dbo].[ServiceEvents]
ADD CONSTRAINT [FK__ServiceEv__Custo__2E1BDC42]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ServiceEv__Custo__2E1BDC42'
CREATE INDEX [IX_FK__ServiceEv__Custo__2E1BDC42]
ON [dbo].[ServiceEvents]
    ([CustomerId]);
GO

-- Creating foreign key on [ServiceId] in table 'ServiceEvents'
ALTER TABLE [dbo].[ServiceEvents]
ADD CONSTRAINT [FK__ServiceEv__Servi__2F10007B]
    FOREIGN KEY ([ServiceId])
    REFERENCES [dbo].[WorkshopServices]
        ([ServiceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ServiceEv__Servi__2F10007B'
CREATE INDEX [IX_FK__ServiceEv__Servi__2F10007B]
ON [dbo].[ServiceEvents]
    ([ServiceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------