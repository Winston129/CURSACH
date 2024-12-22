-- CREATE DATABASE cursach_client_add_item;
-- DROP DATABASE cursach_client_add_item;
GO
USE cursach_client_add_item;
GO

-- Тип
CREATE TABLE ItemType (
    ItemTypeID INT PRIMARY KEY IDENTITY(1,1),
    NameType NVARCHAR(50)              -- Название типа вещи
);
GO

-- Клиент
CREATE TABLE Client (
    ClientID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) ,          -- Имя
    LastName NVARCHAR(50) ,           -- Фамилия
    MiddleName NVARCHAR(50) ,         -- Отчество
    PassportData NVARCHAR(50)         -- Паспортные данные
);
GO


/*------------*/
/*-- STATUS --*/
/*------------*/

-- Вещи, выставленных на продажу
CREATE TABLE Available (
    AvailableID INT PRIMARY KEY IDENTITY(1,1),
    DateListed DATE                    -- Дата
);
GO

-- Вещи в залоге
CREATE TABLE Reserved (
    ReservedID INT PRIMARY KEY IDENTITY(1,1),
    ReservedDate DATE ,                -- Когда был сделан залог
    ExpirationDate DATE ,              -- Когда заканчивается срок залога
    InterestRate DECIMAL(5, 2) ,       -- Процентная ставка
);
GO

-- Проданные вещи
CREATE TABLE Sold (
    SoldID INT PRIMARY KEY IDENTITY(1,1),
    SaleDate DATE ,                     -- Когда вещь была продана
);
GO

-- Вещи
CREATE TABLE Item (
    ItemID INT PRIMARY KEY IDENTITY(1,1),
    ItemName NVARCHAR(100) ,           -- Имя
    ItemTypeID INT ,                   -- Идентификатор типа
    Price DECIMAL(10, 2) ,             -- Цена вещи
    Status NVARCHAR(20) CHECK (Status IN 
		('Available', 'Reserved', 'Sold')),  --  Статус вещи --
    AvailableID INT NULL,              -- Идентификатор продажи ? NULL
    ReservedID INT NULL,               -- Идентификатор залога, может быть ? NULL
    SoldID INT NULL,                   -- Идентификатор проданной вещи ? NULL
    ClientID INT NULL,				   -- Идентификатор клиента
);
GO


-- Item.ItemType 
ALTER TABLE Item 
ADD CONSTRAINT FK_Item_ItemType 
FOREIGN KEY (ItemTypeID) REFERENCES ItemType(ItemTypeID)
ON DELETE CASCADE;
GO

-- Item.Available
ALTER TABLE Item
ADD CONSTRAINT FK_Item_Available
FOREIGN KEY (AvailableID) REFERENCES Available(AvailableID)
ON DELETE CASCADE;
GO

-- Item.Reserved
ALTER TABLE Item
ADD CONSTRAINT FK_Item_Reserved
FOREIGN KEY (ReservedID) REFERENCES Reserved(ReservedID)
ON DELETE CASCADE;
GO

-- Item.Sold
ALTER TABLE Item
ADD CONSTRAINT FK_Item_Sold
FOREIGN KEY (SoldID) REFERENCES Sold(SoldID)
ON DELETE CASCADE;
GO

-- Item.Client
ALTER TABLE Item
ADD CONSTRAINT FK_Item_Client
FOREIGN KEY (ClientID) REFERENCES Client(ClientID)
ON DELETE CASCADE;
GO