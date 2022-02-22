# CoreDemoWebAPI

CoreDemoWebAPI is a an ASP.NET Core Application that does CRUD (Create, Read, Update and Delete) operations on a SQL Server Database using ADO.NET.


## Pre-Requisites
The following are mandatory for the CoreDemoWebAPI application to run:

1. Microsoft .NET Core 5.0 Runtime or SDK.
2. Microsoft SQL Server. 
3. Postman application.


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

	v. Also run the files, 'CoreDemoWebAPI\CoreDemoWebAPI\CoreDemoWebAPI\SQL Server Files\CreateTable_UserSecurity.sql'
			       'CoreDemoWebAPI\CoreDemoWebAPI\CoreDemoWebAPI\SQL Server Files\AddTestDataToTable_UserSecurity.sql'
			       	
	

## Usage

Press F5 or click on the Play button icon from the toolbar in Visual Studio 2019 for the above solution.

When you run the Web application it will allow the ability to Create, Update and Delete Staff Members.

NOTE: port 44351 may different on your machine, change accordingly.



AUTHENTICATE

--> Run Visual Studio 2019 project, and it displays browser.

Run Postman App
Click (+ for new tab)

Select POST
https://localhost:44351/api/staffmembers/authenticate

Body tab
raw - JSON
{"username":"test1","password":"password1"}

Headers tab
Key:Content-Type	Value:application/json

Click 'Send' button
---> this will return token string



GET RECORDS VIA HTTPGET

--> Run Visual Studio 2019 project, and it displays browser.

Run Postman App
Click (+ for new tab)

Select GET
https://localhost:44351/api/staffmembers/read

Headers tab
Key:Authorization	 Value:Bearer <Token String>

Click 'Send' button
---> this will return records from StaffMembers table :

[
 
   {
        "Id": 11,
        "FirstName": "Tim",
        "LastName": "Anand",
        "Title": "Mr"
    },
    {
        "Id": 12,
        "FirstName": "Tanya",
        "LastName": "Kaur",
        "Title": "Miss"
    }

]



ADD RECORDS VIA HTTPPOST

--> Run Visual Studio 2019 project, and it displays browser.

Run Postman App
Click (+ for new tab)

Select POST
https://localhost:44351/api/staffmembers/create

Headers tab
Key:Authorization	 Value:Bearer <Token String>

Body tab
raw - JSON
{
    "FirstName":"Tanya",
    "LastName":"Kaur",
    "Title":"Miss"
}


Click 'Send' button
---> this will add StaffMembers record 




UPDATE RECORDS VIA HTTPPUT

--> Run Visual Studio 2019 project, and it displays browser.

Run Postman App
Click (+ for new tab)

Select PUT
https://localhost:44351/api/staffmembers/update/12

Headers tab
Key:Authorization	 Value:Bearer <Token String>

Body Tab
raw - JSON
{
    "Id": 12,
    "FirstName":"Tanya",
    "LastName":"Kaur",
    "Title":"Mss"
}

Click 'Send' button
---> this will update StaffMembers record




DELETE RECORDS VIA HTTP DELETE

--> Run Visual Studio 2019 project, and it displays browser.

Run Postman App
Click (+ for new tab)

Select DELETE
https://localhost:44351/api/staffmembers/delete/16

Headers tab
Key:Authorization	 Value:Bearer <Token String>

Body Tab
raw - JSON
{
    "Id": 12,
    "FirstName":"Tanya",
    "LastName":"Kaur",
    "Title":"Mss"
}

Click 'Send' button
---> this will delete StaffMembers record



TO RUN VISUAL STUDIO 2019 PROJECT WITH SWAGGER

Run CoreDemoWebAPI project in Visual Studio 2019

https://localhost:44351/swagger/index.html

---> displays Swagger links etc.. including Authorize button


Click on 'Post' for /api/staffmembers/authenticate

Select 'application/json'
Click 'Try it out'

In Request Body, amend as follows :

{
  "username": "user1",
  "password": "password1"
}


Click 'Execute'

---> It displays token if username and password are valid in the 'StaffMembers' table.

Copy and Paste token string




Click on 'Authorize' button at top of web page

Paste token in 'Value' box
Click 'Authorize' button.
Click 'Close'



Click on any of the buttons like GET, POST, PUT, DELETE and they will work using token provided.




## License & Copyright

(c) 2021 Arvinder Anand (Tim)

 
