using System.Net.Cache;
using System.Text.Json.Serialization;
using System.Threading.Channels;

namespace FlexiSourceCodingTest.Models
{
    public class UserProfileParam
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public double Weight {  get; set; }
        public double Height {  get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActionAdd { get; set; }
    }

    public class UserRunningActivityParam
    {
        public string UserID { get; set; }
        public string Location { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double Distance { get; set; }
    }

    public class DeleteuserParam
    {
        public string UserID { get; set; }
    }
    public class DeleteUserRunningActiviyParam
    {
        public int UserID { get; set; }
        public int RunningActivityID { get; set; }
    }

    public class GetUserProfileReturn
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public double BMI { get; set; }
    }

    public class GetUserListReturn
    {
        [JsonIgnore]
        public string StatusCode { get; set; }
        [JsonIgnore]
        public string StatusMessage { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public double BMI { get; set; }
    }

    public class GetUserRunningActivityReturn
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public int  RunningActivityID { get; set; }
        public int  UserID { get; set; }
        public string Location { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double Distance { get; set; }
        public TimeSpan Duration { get; set; }
        public double AveragePace { get; set; }
    }

    public class GenericReturn
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }

    }

    public class GenericReturnStatusData<T>
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<T> Data { get; set; }
    }
}
