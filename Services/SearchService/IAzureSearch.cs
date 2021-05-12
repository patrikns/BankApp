using System.Collections.Generic;
using Azure.Search.Documents;

namespace Uppgift2BankApp.Services.SearchService
{
    public interface IAzureSearch
    {
        SearchClient GetSearchClient();
    }
}
