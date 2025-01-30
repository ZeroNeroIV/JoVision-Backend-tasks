using JoVisionBackendTasks.Task44;
using JoVisionBackendTasks.Task45;
using JoVisionBackendTasks.Task46;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//// Task44
//app.MapGet("/", GreetingControllerTask44.Greeting);

//// Task45
//app.MapGet("/greet", GreetingControllerTask45.Greeting);
//app.MapGet("/birthdate", BirthDateControllerTask45.BirthDate);

//// Task46
app.MapGet("/greet", (Func<HttpContext, Task<IResult>>)GreetingControllerTask46.Greeting);
app.MapGet("/birthdate", (Func<HttpContext, Task<IResult>>)BirthDateControllerTask46.BirthDate);

app.Run();