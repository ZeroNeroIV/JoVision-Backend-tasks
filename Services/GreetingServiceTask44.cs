﻿namespace JoVisionBackendTasks.Services
{
    public class GreetingServiceTask44
    {
        public string Greeting(string? name)
        {

            string greeting = "Hello ";
            greeting += string.IsNullOrWhiteSpace(name) ? "anonymous" : name.Trim('\"').Trim('\'').Trim();
            return greeting;
        }
    }
}
