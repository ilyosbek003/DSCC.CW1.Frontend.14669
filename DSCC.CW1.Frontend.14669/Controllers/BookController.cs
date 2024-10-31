using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DSCC.CW1.Frontend._14669.Models;
using System.Text;


namespace DSCC.CW1.Frontend._14669.Controllers
{
    public class BookController : Controller
    {
        private readonly string baseUrl = "https://localhost:44366/api/Book";

        // GET: Book
        public async Task<ActionResult> Index()
        {
            List<Book> bookList = new List<Book>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    bookList = JsonConvert.DeserializeObject<List<Book>>(responseContent);
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to retrieve book items.";
                }
            }

            return View(bookList);
        }

        // GET: Book/Details/2
        public async Task<ActionResult> Details(int id)
        {
            Book book = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<Book>(responseContent);
                }
                else
                {
                    return HttpNotFound("Book item not found.");
                }
            }

            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public async Task<ActionResult> Create(Book book)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to create the book item.";
                    return View(book);
                }
            }
        }

        // GET: Book/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Book book = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<Book>(responseContent);
                }
                else
                {
                    return HttpNotFound("Book item not found.");
                }
            }

            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return new HttpStatusCodeResult(400, "ID mismatch between route and body.");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{baseUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to update the book item.";
                    return View(book);
                }
            }
        }

        // GET: Book/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Book book = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<Book>(responseContent);
                }
                else
                {
                    return HttpNotFound("Book item not found.");
                }
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to delete the book item.";
                    return RedirectToAction("Delete", new { id = id });
                }
            }
        }
    }
}
