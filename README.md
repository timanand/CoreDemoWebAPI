# CoreDemoWebAPI

CoreDemoWebAPI is a an ASP.NET Core Application that does CRUD (Create, Read, Update and Delete) operations on a SQL Server Database using ADO.NET.


## Pre-Requisite
The following are mandatory for the CoreDemoWebAPI application to run :

1. Microsoft .NET Core 5.0 Runtime or SDK.
2. Microsoft SQL Server. 


## Installation

1. Run Visual Studio 2019

2. Select 'Clone a repository'

 	Repository location: 
 	https://github.com/timanand/CoreDemoWebAPI.git

 	Path:
 	This is the location on your computer where Repository shall be copied to. For example: 'C:\DEVELOP\CoreDemoWebAPI\'.

 	Click on 'Clone' button.




3. On the right side, you will see the Solution Explorer window. Double click on 'CoreDemoWebAPI.sln'.



4. From 'Build' menu, select 'Rebuild Solution'. 
	--> It will say : 
		
		- Rebuild All: 3 succeeded


5. Goto Solution Explorer

	Right mouse click on 'CoreDemoWebAPI'
	Select 'Manage User Secrets'
	Paste the following :

	  "ConnectionStrings": {
    		"StaffConnex": "Data Source=; Initial Catalog=CoreDemoADONet; 
				integrated security=false;user id=;password=;"
  			       }

			Update the following based on your SQL Server Management Studio settings:
			
			Data Source=
			User Id=
			Password=


6. From 'Build' menu, select 'Rebuild Solution'.
	--> It will say : 
		
		- Rebuild All: 3 succeeded


7. Run SQL Script into CoreDemoADONet SQL Server Database by following the instructions:

	i. Run SQL Server Management Studio or Equivalent

	ii. Sign onto System

	iii. In Explorer, double click on file, 'CoreDemoWebAPI\CoreDemoWebAPI\CoreDemoWebAPI\SQL Server Files\CreateDatabaseAndTable.sql'
	

	iv. Run the Execute command or equivalent to run the query which will create Database, 'CoreDemoADONet' and Table : 'dbo.StaffMembers' in SQL Server.



## Usage

Press F5 or click on the Play button icon from the toolbar in Visual Studio 2019 for the above solution.

When you run the Web application it will allow the ability to Create, Update and Delete Staff Members.

NOTE: port 44351 may different on your machine, change accordingly.


GET RECORDS VIA HTTPGET

--> Run Visual Studio 2019 project, and it displays browser.
Type the following in the browser,

https://localhost:44351/api/StaffMembers

It displays the following :

[{"Id":1,"FirstName":"Tim","LastName":"Anand","Title":"Mr"},{"Id":2,"FirstName":"Lucy","LastName":"Smith","Title":"Miss"}]

NOTE: Make sure there is data in the Staff Members table before running this otherwise it will display [].


ADD RECORDS VIA HTTPPOST

Run CoreDemoWebAPI Visual Studio 2019 application

Run Postman App
Click (+ for new tab)

Select POST from dropdown

https://localhost:44351/api/staffmembers/create

Click Body
Click Raw
Click Text and select JSON

{"FirstName":"Katrina","LastName":"Patel","Title":"Miss"}

Click 'Send' button.

--> Record will be written to StaffMembers table on CoreDemoADONet Database.





UPDATE RECORDS VIA HTTPPUT

Select PUT from dropdown

https://localhost:44351/api/staffmembers/update/3

Click Body
Click Raw
Click Text and select JSON

{"Id": 3,"FirstName":"Tanya","LastName":"Anand","Title":"Miss"}


Click 'Send' button.

--> Update Id=3 StaffMembers record on CoreDemoADONet Database.



DELETE RECORDS VIA HTTP DELETE

Select DELETE from dropdown

https://localhost:44351/api/staffmembers/delete/3

NOTE: Data in Body is not required!

Click 'Send' button.

It will display Status: 200 OK.





## License & Copyright

(c) 2021 Arvinder Anand (Tim)

 
