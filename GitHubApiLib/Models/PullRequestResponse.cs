using Newtonsoft.Json;
using System;

namespace GitHubApiLib.Models
{
    public class PullRequestResponse
    {
        public int Id { get; set; }

        public string State { get; set; }

        public string Title { get; set; }

        [JsonProperty("merged_at")]
        public DateTime? MergedAt { get; set; }
    }
}
