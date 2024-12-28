SELECT 
    Item.ItemID,
    Item.ItemName,
    ItemType.NameType AS ItemType,
    Item.Price,
    Item.Status,
    Available.DateListed AS AvailableDate,
    Reserved.ReservedDate,
    Reserved.ExpirationDate,
    Reserved.InterestRate,
    Sold.SaleDate,
    Client.FirstName AS ClientFirstName,
    Client.LastName AS ClientLastName
FROM 
    Item
LEFT JOIN 
    ItemType ON Item.ItemTypeID = ItemType.ItemTypeID
LEFT JOIN 
    Available ON Item.AvailableID = Available.AvailableID
LEFT JOIN 
    Reserved ON Item.ReservedID = Reserved.ReservedID
LEFT JOIN 
    Sold ON Item.SoldID = Sold.SoldID
LEFT JOIN 
    Client ON Item.ClientID = Client.ClientID;
