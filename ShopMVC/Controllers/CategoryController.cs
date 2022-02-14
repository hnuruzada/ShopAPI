using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopMVC.DTOs;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.Controllers
{
    public class CategoryController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44338/api");
        HttpClient client;
        public CategoryController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public async Task<IActionResult> Index()
        {
            CategoryListDto listDto;

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:44338/admin/api/categories");

                var responseStr = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    listDto = JsonConvert.DeserializeObject<CategoryListDto>(responseStr);
                    return View(listDto);

                }
            }


            return RedirectToAction("index", "home");
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryListItemDto listItem)
        {
            string data=JsonConvert.SerializeObject(listItem);
            StringContent content=new StringContent(data ,Encoding.UTF8,"application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/category", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(listItem);
        }
        public IActionResult Edit(int id)
        {
            CategoryListItemDto listItem = new CategoryListItemDto();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/category" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listItem = JsonConvert.DeserializeObject<CategoryListItemDto>(data);
            }
            return View("Create", listItem);
        }
        [HttpPost]
        public IActionResult Edit(CategoryListItemDto listItem)
        {
            string data=JsonConvert.SerializeObject(listItem);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/category/"+listItem.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create", listItem ) ;
        }
    }
}
