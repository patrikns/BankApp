using System;
using Azure;
using Azure.Search.Documents;

namespace Uppgift2BankApp.Services.SearchService
{
    public class AzureSearch : IAzureSearch
    {
        private readonly string _indexName;
        private readonly string _searchUrl;
        private readonly string _key;

        public AzureSearch()
        {
            _key = "151E0D94D2A09EFA66DD8185CEED806F";
            _searchUrl = "https://patrikspersonsearch.search.windows.net";
            _indexName = "kunder";
        }

        public SearchClient GetSearchClient()
        {
            var searchClient = new SearchClient(new Uri(_searchUrl), _indexName, new AzureKeyCredential(_key));

            return searchClient;
        }
    }
}