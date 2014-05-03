/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO [dbo].[People]([FirstName], [LastName], [Birthday], [Email], [Phone])
VALUES
('Gareth', 'Bale', '7-4-1983', 'gbale@gmail.com', '14081234578'),
('Peter', 'Crouch', '11-14-1981', 'pcrouch@gmail.com', '14081234581'),
('Cristiano', 'Ronaldo', '2-24-1988', 'cronaldo@gmail.com', '14081234592'),
('Andrei', 'Shevchenko', '1-16-1980', 'ashevchenko@gmail.com', '14081234603'),
('Lionel', 'Messi', '7-25-1985', 'lmessi@gmail.com', '14081234571'),
('Wayne', 'Rooney', '1-4-1983', 'wrooney@gmail.com', '14081234575'),
('Victor', 'Valdes', '8-15-1977', 'vvaldes@gmail.com', '14081234579'),
('Sergio', 'Ramos', '2-23-1984', 'sramos@gmail.com', '14081234581'),
('Yaya', 'Toure', '6-1-1979', 'ytoure@gmail.com', '14081234683'),
('David', 'Silva', '12-23-1984', 'dsilva@gmail.com', '14081234572');