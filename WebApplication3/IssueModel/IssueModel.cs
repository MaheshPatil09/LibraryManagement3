using Newtonsoft.Json;

namespace LibraryManagement.IssueModel
{
    public class IssueModel
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }
        [JsonProperty(PropertyName = "documentType", NullValueHandling = NullValueHandling.Ignore)]
        public string BookId { get; set; }
        [JsonProperty(PropertyName = "issueDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime IssueDate { get; set; }
        [JsonProperty(PropertyName = "returnDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ReturnDate { get; set; }
    }
}
