using System.ComponentModel.DataAnnotations;

namespace GitHubApiLib.Parameters
{
    public class PaginationOptions
    {
        [Range(1, 100)]
        public int ResultsPerPage { private get; set; }

        public string QueryString => BuildQueryString();

        private string BuildQueryString()
        {
            return $"per_page={ResultsPerPage}";
        }
    }
}
