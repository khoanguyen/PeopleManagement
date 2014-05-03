You should have:
 - Visual Studio 2012.
 - SQL Server 2012

Open the project with Visual Studio 2012

1. Initialize database:
----------------------------------------------------------
	- Drop down PeopleDB project
	- Double-click on PeopleDB.Dev.publish.xml
	- Configure "Target Database Connection"  field to you SQL 2012 database server
	- Click "Publish"
	- Wait until the publish process is done


2. Change Connection string in web.config:
----------------------------------------------------------
	- Open web.config of PeopleManagement project
	- Change connect string with name PeopleContext to your SQL 2012 database server


3. Build and Run
----------------------------------------------------------
	- The solution has Nuget Package Restoration enabled just ensure that you can connect to Nuget for downloading the packages
	- Hit F5  :)


4. UnitTest
----------------------------------------------------------
	I provide some basic UnitTest written in NUnit for testing my Business Logic layer (PeopleManagement.Domain.Services namespace).
	You can use NUnit runner or NUnit Adapter (VS add-in) to run the tests.
