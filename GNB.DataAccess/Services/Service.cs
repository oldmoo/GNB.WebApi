using GNB.Domain.InfrastructureContracts;
using GNB.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GNB.Infrastructure.Services;

public class Service<T> : IService<T> where T : class
{
     private readonly IHttpClientFactory _httpClientFactory;
     public string? ClientName { get; set; }
     
     public Service(IHttpClientFactory httpClientFactory)
     {
          _httpClientFactory = httpClientFactory;
     }

    

     public async Task<IEnumerable<T>> Get()
     {

          if (string.IsNullOrWhiteSpace(ClientName)) throw new ArgumentNullException(nameof(ClientName));
         
          using var client = _httpClientFactory.CreateClient(ClientName);
          using var response = await client.GetAsync($"{client.BaseAddress}");
          
          var responseBody = await response.Content.ReadAsStringAsync();
          return JsonConvert.DeserializeObject<List<T>>(responseBody);
     }
}