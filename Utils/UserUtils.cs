namespace FlexiSourceCodingTest.Utils
{
    public class UserUtils
    {
        public double GetBMI(double weight, double height)
        {
            return weight / (height * height);
        }

        public int GetAge(DateTime birthdate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthdate.Year;

            if (birthdate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        public TimeSpan GetDuration(DateTime startDateTime, DateTime endDateTime)
        {
            // Calculate the duration
            return endDateTime - startDateTime;
        }

        public double CalculateAveragePace(DateTime startDateTime, DateTime endDateTime, double distance)
        {
            TimeSpan duration = endDateTime - startDateTime;

            double totalDurationInSeconds = duration.TotalSeconds;

            double averagePace = totalDurationInSeconds / distance;

            return averagePace;
        }
    }
}
