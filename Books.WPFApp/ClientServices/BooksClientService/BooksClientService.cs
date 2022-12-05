using AutoMapper;
using System.Net.Http;

namespace Books.WPFApp.ClientServices.BooksClientService
{
    public class BooksClientService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;


        public BooksClientService(IMapper mapper, HttpClient httpClient)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri("BooksApi");
        }
    }
}