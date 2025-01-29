using JoVisionBackendTasks.Task44;
using JoVisionBackendTasks.Task45;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//// Task44
//app.MapGet("/", GreetingControllerTask44.Greeting);

//// Task45
app.MapGet("/greet", GreetingControllerTask45.Greeting);
app.MapGet("/birthdate", BirthDateControllerTask45.BirthDate);

app.Run();