using Newtonsoft.Json;

namespace LibraryManagement.StudentModel
{
    public class StudentModel
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }
        public string Name { get; set; }
        [JsonProperty(PropertyName = "dateOfBirth", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
    }
}
