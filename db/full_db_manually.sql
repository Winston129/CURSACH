INSERT INTO Client (FirstName, LastName, MiddleName, PassportData)
VALUES 
('�������', '������', '���������', '1234 567890'),
('�����', '��������', '��������', '5678 123456'),
('�����', '���������', '���������', '8765 432109'),
('�������', '�����', '����������', '3456 789012'),
('����', '��������', '�������������', '4321 987654'),
('������', '�������', '����������', '1234 876543'),
('���������', '�������', '����������', '6543 210987'),
('����', '�������', '��������', '9876 543210'),
('�������', '�������', '�����������', '3210 654321'),
('������', '�������', '���������', '2109 876543'),
('�����', '���������', '����������', '4321 098765'),
('������', '�������', '���������', '6543 219876'),
('���������', '�������', '����������', '7890 123456'),
('��������', '�������', '����������', '9012 345678'),
('����', '��������', '������������', '5678 901234'),
('���������', '�������', '����������', '8765 432123'),
('�����', '�������', '��������', '3456 789543'),
('����', '������', '����������', '7654 321098'),
('��������', '��������', '����������', '2345 678901'),
('�����', '��������', '��������������', '9876 543876'),
('�������', '���������', '�������', '8765 432098'),
('�����', '������', '������������', '4321 654321'),
('�������', '��������', '���������', '5678 123432'),
('������', '�����', '����������', '3456 789210'),
('��������', '��������', '�����������', '9012 345654'),
('�������', '������', '����������', '2109 654321'),
('�����', '������', '��������', '7654 321765'),
('����������', '������', '��������', '2345 678876'),
('�������', '������', '������������', '8765 432543'),
('�������', '�����������', '����������', '5432 109876');


insert into Available(DateListed)
values
('2024-12-02'),
('2023-02-22'),
('2024-09-18');


INSERT INTO Reserved ( ReservedDate, ExpirationDate, InterestRate)
VALUES
('2024-12-01', '2025-01-01', 5.00),
('2024-12-03', '2025-02-01', 4.50),
('2024-12-07', '2025-03-01', 6.00);

INSERT INTO Sold (SaleDate)
VALUES
('2024-12-01'),
('2024-12-05'),
('2024-12-10');

insert into ItemType (NameType)
values
('electronics'),
('watch'),
('expensive metal'),
('books'),
('archaeological item'),
('wine');

insert into Item(ItemName, ItemTypeID, Price, Status, AvailableID, ReservedID, SoldID, ClientID)
values
('The Hero with a Thousand Faces', 4, 333.3, 'Available', 4, null, null, null),
('IPhon21', 1, 9999.99, 'Reserved', null, null, 3, 12),
('Chateau Mouton Rothschild', 6, 200.2, 'Sold', null, 2, null, 14);


select * from Available;
select * from Reserved;
select * from Sold;

select * from Item;

/*
	delete from Available
	where DateListed = '2024-12-02';
*/