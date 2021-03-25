using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TesteCacheEmMemoria.Controllers
{

    public class CountryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache; //Dependencia do memory cache
        private const string RESTCOUNTRY = "https://restcountries.eu/rest/v2/all";//USL consultada
        private const string COUNTRIES_KEY = "countries";//Chave do cache

        public CountryController(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory)
        {
            _memoryCache = memoryCache;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("api/paises")]
        public async Task<IActionResult> GetCountries()
        {

            var request = new HttpRequestMessage(HttpMethod.Get, RESTCOUNTRY);
            if (_memoryCache.TryGetValue(COUNTRIES_KEY, out List<Country> countriesObject))
            {
                // countriesObject.RemoveAll(e => e.Name.StartsWith('A'));

                //Pode fazer qualquer regra de negocios dentro do cache
                var listAlternativa = new List<Country>(countriesObject);
                var paisesComBr = new List<Country>();
                foreach (var item in listAlternativa)
                {
                    if (item.Name.Equals("Brazil"))
                    {
                        countriesObject.Remove(item);
                    }

                    if (item.Capital.Contains("Bra"))
                    {
                        paisesComBr.Add(item);
                    }
                }

                if (paisesComBr.Count > 0)
                {
                    return Ok(paisesComBr);
                }

                return Ok(countriesObject);
            }

            //using (var httpCliente = new HttpClient())
            //{
            //    var response = await httpCliente.GetAsync(RESTCOUNTRY);
            //    var responseData = await response.Content.ReadAsStringAsync();
            //    var countries = JsonConvert.DeserializeObject<List<Country>>(responseData);

            //    //Configurações do memory cache
            //    var memorycaheOptions = new MemoryCacheEntryOptions
            //    {
            //        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),// durante uma hora o cache é removido
            //        SlidingExpiration = TimeSpan.FromSeconds(1200)//se nao ouver nenhuma requisicao, apaga o cache em 20min
            //    };

            //    _memoryCache.Set(COUNTRIES_KEY, countries, memorycaheOptions);
            //    return Ok(countries);
            //}
            using (var httpCliente = _httpClientFactory.CreateClient())
            {
                var response = await httpCliente.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    var countries = JsonConvert.DeserializeObject<List<Country>>(responseData);
                    //Configurações do memory cache
                    var memorycaheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),// durante uma hora o cache é removido
                        SlidingExpiration = TimeSpan.FromSeconds(1200)//se nao ouver nenhuma requisicao, apaga o cache em 20min
                    };

                    _memoryCache.Set(COUNTRIES_KEY, countries, memorycaheOptions);
                    return Ok(countries);
                }
                return NoContent();
            }
        }
    }
}
