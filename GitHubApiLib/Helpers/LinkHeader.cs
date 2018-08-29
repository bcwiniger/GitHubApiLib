using System;

namespace GitHubApiLib.Helpers
{
    public class LinkHeader
    {
        public string FirstPage { get; set; }

        public string PreviousPage { get; set; }

        public string NextPage { get; set; }

        public string LastPage { get; set; }
        
        public LinkHeader Parse(string unparsedLinkHeader)
        {
            try
            {
                EnsureThat.ValueIsNotEmpty(unparsedLinkHeader);
                var links = unparsedLinkHeader.Split(',');
                EnsureThat.CollectionIsNotEmpty(links);

                var linkHeader = new LinkHeader();
                
                foreach(var link in links)
                {
                    var linkComponents = link.Split(';');
                    EnsureThat.CollectionIsNotEmpty(linkComponents);

                    //TODO: ensure that collection equals 2
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
                return null;
            }
        }
    }
}
