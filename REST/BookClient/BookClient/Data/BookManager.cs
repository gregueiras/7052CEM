using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace BookClient.Data
{
    public class BookManager
    {
        private const string URL = "https://bookserver2512.azurewebsites.net/api/books/";
        private string authToken;

        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            if (string.IsNullOrEmpty(authToken))
            {
                authToken = await client.GetStringAsync(URL + "login");
                authToken = JsonConvert.DeserializeObject<string>(authToken);
            }

            client.DefaultRequestHeaders.Add("Authorization", authToken);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            HttpClient client = await GetClient();
            string books = await client.GetStringAsync(URL);
            return JsonConvert.DeserializeObject<IEnumerable<Book>>(books);
        }

        public async Task<Book> Add(string title, string author, string genre)
        {
            HttpClient client = await GetClient();

            Book book = new Book()
            {
                Title = title,
                Genre = genre,
                Authors = new List<string>(new[] { author }),
                ISBN = string.Empty,
                PublishDate = DateTime.Now.Date,
            };

            StringContent msg = new StringContent(
                JsonConvert.SerializeObject(book),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage response = await client.PostAsync(URL, msg);

            Book respBook = JsonConvert.DeserializeObject<Book>(
                await response.Content.ReadAsStringAsync());

            return respBook;

        }

        public async Task Update(Book book)
        {
            HttpClient client = await GetClient();

            StringContent msg = new StringContent(
                JsonConvert.SerializeObject(book),
                Encoding.UTF8,
                "application/json");

            await client.PutAsync($"{URL}/{book.ISBN}", msg);
        }

        public async Task Delete(string isbn)
        {
            HttpClient client = await GetClient();

            await client.DeleteAsync($"{URL}/{isbn}");
        }
    }
}

