
After Downloading the project....

API
	1. I have already created Migration. At first, Change the ConnectionStrings from appsettings.json file. Then execute ( update-database ) command from Package Manager Console. 

	2. Run the api

	3. I have used angular 11 for the front-end. At first open the frontEnd Folder using any editor like Vs Code. Then execute ( npm install ) command from cmd/terminal. Then execute ( ng s -o / ng serve --open ) command to run the project on default port (http://localhost:4200) directly. 

	4. Now you can operate CRUD operation.


======Project Specification======  

	1. API has been created on code first approach using Asp.Net core 5.0. 
	2. I have created PersonsController where CRUD operation has been 	    performed. 
	3. Secondly, I have created AccountController where Login and Register action has been performed that can only be understand by using                     Swagger/Postman.  
	4. Thirdly, I have created AdministratorController where distributed Admin can Remove/Add users, Create,  Assign,Edit and Delete Roles.  
