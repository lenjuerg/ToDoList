# ToDoListAPI
This is a classic To Do List App in the form of an ASP.Net Core API. It utilizes CQRS with Mediatr and the .Net8 Identity API for SPAs.
## How to run
### Setup
1. Add your connection information to the appsettings.json files in the DataAccess (for design time access) and ToDoListApi (for runtime access) projects.\
   Example:\
`"ConnectionStrings": {
   "DefaultConnection": "Server=EXAMPLE;Database=EXAMPLE;Trusted_Connection=true;TrustServerCertificate=true"
 }`
2. Run `dotnet ef database update --project DataAccess` command from within the solution folder.
### Use
1. Run the ToDoListApi project and wait for Swagger to open.
2. Register a new user with email and password and login with the new account using the identity endpoints. Alternatively login to an existing account. You can choose either cookie or token authentication on login.
3. (Optional) If you chose cookie authentication: copy the token, click the 'Authorize' button at the top of the swagger page and paste the token.
4. Use the api endpoints to view and manage your to do lists.
