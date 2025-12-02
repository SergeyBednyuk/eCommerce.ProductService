-- Insert a test user
INSERT INTO "ApplicationUsers"
("UserId", "Email", "Password", "FirstName", "LastName", "Gender")
VALUES
    (uuid_generate_v4(), 'admin@ecommerce.com', 'Secret123$', 'Siarhei', 'Admin', 'Male');

-- Insert a second user with some null values (since your C# model allows nulls)
INSERT INTO "ApplicationUsers"
("UserId", "Email", "Password", "FirstName")
VALUES
    (uuid_generate_v4(), 'customer@ecommerce.com', 'Pass456!', 'John');