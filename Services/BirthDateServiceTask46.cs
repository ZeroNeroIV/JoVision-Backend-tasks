namespace JoVisionBackendTasks.Services
{
    public class BirthDateServiceTask46
    {
        public string BirthDate(string? name, int? years, int? months, int? days)
        {
            string greeting = "Hello ";

            greeting += string.IsNullOrWhiteSpace(name) ? "anonymous" : name;

            string birthDate;

            if (years == null || months == null || days == null)
                birthDate = ", I can’t calculate your age without knowing your birthdate!";
            else
                birthDate = ", your age is " + CalculateAge(
                        DateTime.Parse($"{years}-{months}-{days}")
                    ).ToString();

            greeting += birthDate;

            return greeting;
        }
        private int CalculateAge(DateTime date)
        {
            DateTime curr = DateTime.UtcNow;
            int age = curr.Year - date.Year;
            age -= date.AddYears(age) > curr ? 1 : 0;
            return age;
        }
    }
}
