using JoVisionBackendTasks.Task44;
using JoVisionBackendTasks.Task45;
using JoVisionBackendTasks.Task46;
using JoVisionBackendTasks.Task47;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//// Task44
//var task44Greeting = new GreetingControllerTask44();

//app.MapGet("/", task44Greeting.Greeting);

//// Task45
//var task45Greeting = new GreetingControllerTask45();
//var task45Birthdate = new BirthDateControllerTask45();

//app.MapGet("/greet", task45Greeting.Greeting);
//app.MapGet("/birthdate", task45Birthdate.BirthDate);

//// Task46
//var task46Greeting = new GreetingControllerTask46(); 
//var task46Birthdate = new BirthDateControllerTask46(); 

//app.MapGet("/greet", (Func<HttpContext, Task<IResult>>)task46Greeting.Greeting);
//app.MapGet("/birthdate", (Func<HttpContext, Task<IResult>>)task46Birthdate.BirthDate);

//// Task47
var task47Create = new CreateControllerTask47();
var task47Delete = new DeleteControllerTask47();
string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Task47Uploads");
if (!Directory.Exists(uploadDirectory)) Directory.CreateDirectory(uploadDirectory);

IResult HandleCreate(HttpContext context) {
    return task47Create.Create(context, uploadDirectory).Result;
}

IResult HandleDelete(HttpContext context, string? fileName, string? fileOwner) {
    return task47Delete.Delete(context, uploadDirectory, fileName, fileOwner);
}
app.MapPost("/create", HandleCreate);
app.MapGet("/delete", HandleDelete);

app.Run();