namespace JoVisionBackendTasks.Task46
{
    public static class BirthDateControllerTask46
    {
        public static async Task<IResult> BirthDate(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();

            string name = form["name"].ToString() ?? "anonymous";
            string years = form["years"].ToString();
            string months = form["months"].ToString();
            string days = form["days"].ToString();
            
            string birthDate = "";

            if (string.IsNullOrEmpty(years) || string.IsNullOrEmpty(months) || string.IsNullOrEmpty(days)) {
                birthDate = ", I can’t calculate your age without knowing your birthdate!";
            } else {
                birthDate = ", your age is " + CalculateAge(
                        DateTime.Parse($"{years}-{months}-{days}")
                    ).ToString();
            }
            
            string greeting = $"Hello {name}{birthDate}";

            return Results.Ok(greeting);
        }

        private static int CalculateAge(DateTime date)
        {
            DateTime curr = DateTime.UtcNow;
            int age = curr.Year - date.Year;
            age -= (date.AddYears(age) > curr) ? 1 : 0;
            return age;
        }
    }
}
