using System;
using System.Linq;
using System.Net.Http.Headers;

namespace GitHubApiLib.Helpers
{
    public class LinkHeader
    {
        public string FirstPage { get; set; }

        public string PreviousPage { get; set; }

        public string NextPage { get; set; }

        public string LastPage { get; set; }
        
        public LinkHeader Parse(HttpResponseHeaders headers)
        {
            var linkHeader = new LinkHeader();

            try
            {
                if (!headers.TryGetValues("link", out var tempLinks)) return linkHeader;
                EnsureThat.CollectionCountEquals(tempLinks, 1);

                var unparsedLinkHeader = tempLinks.ToList()[0];
                EnsureThat.ValueIsNotEmpty(unparsedLinkHeader);
                var links = unparsedLinkHeader.Split(',');
                
                foreach(var link in links)
                {
                    var linkComponents = link.Split(';');
                    EnsureThat.CollectionCountEquals(linkComponents, 2);

                    var url = linkComponents[0].Replace("<", "").Replace(">", "");
                    var rel = linkComponents[1].Replace("rel=", "").Replace("\"", "");
                    switch(rel.ToLower().Trim())
                    {
                        case "first":
                            linkHeader.FirstPage = url;
                            break;
                        case "prev":
                            linkHeader.PreviousPage = url;
                            break;
                        case "next":
                            linkHeader.NextPage = url;
                            break;
                        case "last":
                            linkHeader.LastPage = url;
                            break;
                    }
                }

                return linkHeader;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }

            return linkHeader;
        }
    }
}
