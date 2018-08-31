namespace GitHubApiLib.Parameters
{
    public class PaginationOptions
    {
        private int? _resultsPerPage = null;
        public int? ResultsPerPage
        {
            private get { return _resultsPerPage; }

            set
            {
                if (value < 1) _resultsPerPage = 1;
                else if (value > 100) _resultsPerPage = 100;
                else _resultsPerPage = value;
            }
        }

        public string QueryString => BuildQueryString();

        private string BuildQueryString()
        {
            return ResultsPerPage.HasValue
                ? $"?per_page={ResultsPerPage.Value}"
                : "";
        }
    }
}
