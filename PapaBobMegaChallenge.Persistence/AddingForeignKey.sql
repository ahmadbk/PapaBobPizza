ALTER TABLE Orders
ADD CONSTRAINT FK_Customer_Order
FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)