using System;
using System.Collections.Generic;
using System.Linq;
using Azure;
using Azure.Search.Documents;
using SearchApp;

namespace Uppgift2BankApp.Services.SearchService
{
    public class AzureSearch : IAzureSearch
    {
        private readonly string _indexName;
        private readonly string _searchUrl;
        private readonly string _key;

        public AzureSearch()
        {
            _key = "FCAAEEB5AC82008B944620ADA2749915";
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