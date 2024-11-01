﻿using DSCC.CW1.Frontend._14669.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DCSS.CW1.Frontend._14669.Controllers
{
    public class CategoryController : Controller
    {
        private readonly string baseUrl = "https://localhost:44366/api/Category";

        // GET: Category
        public async Task<ActionResult> Index()
        {
            List<Category> categoryList = new List<Category>();

            using (var client = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true }))
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    categoryList = JsonConvert.DeserializeObject<List<Category>>(responseContent);
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to retrieve categories.";
                }
            }

            return View(categoryList);
        }

        // GET: Category/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Category category = null;

            using (var client = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true }))
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    category = JsonConvert.DeserializeObject<Category>(responseContent);
                }
                else
                {
                    return HttpNotFound("Category not found.");
                }
            }

            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            using (var client = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true }))
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to create the category.";
                    return View(category);
                }
            }
        }

        // GET: Category/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Category category = null;

            using (var client = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true }))
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    category = JsonConvert.DeserializeObject<Category>(responseContent);
                }
                else
                {
                    return HttpNotFound("Category not found.");
                }
            }

            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return new HttpStatusCodeResult(400, "ID mismatch between route and body.");
            }

            using (var client = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true }))
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{baseUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to update the category.";
                    return View(category);
                }
            }
        }

        // GET: Category/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Category category = null;

            using (var client = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true }))
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    category = JsonConvert.DeserializeObject<Category>(responseContent);
                }
                else
                {
                    return HttpNotFound("Category not found.");
                }
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true }))
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
                    ViewBag.ErrorMessage = "Failed to delete the category.";
                    return RedirectToAction("Delete", new { id = id });
                }
            }
        }
    }
}