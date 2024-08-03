using System.Collections.Generic;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CommunitySupportPlatform.Models;

namespace CommunitySupportPlatform.Controllers
{
    /// <summary>
    /// MVC controller for managing articles.
    /// Provides web views and actions for CRUD operations on articles.
    /// </summary>
    public class ArticleController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ArticleController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                //cookies are manually set in RequestHeader
                UseCookies = false
            };

            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44394/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Displays a list of articles.
        /// </summary>
        /// <returns>The view containing the list of articles.</returns>
        /// <example>GET: Article/List</example>
        public ActionResult List()
        {
            string url = "ArticleData/ListArticles";
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<ArticleDto> articles = response.Content.ReadAsAsync<IEnumerable<ArticleDto>>().Result;
                return View(articles);
            }
            else
            {
                Debug.WriteLine("Failed to retrieve articles. Error: " + response.StatusCode);
                return View("Error");
            }
        }

        /// <summary>
        /// Displays details of a specific article.
        /// </summary>
        /// <param name="id">The ID of the article to be displayed.</param>
        /// <returns>The view containing details of the specified article.</returns>
        /// <example>GET: Article/Details/5</example>
        public ActionResult Details(int id)
        {
            string url = "ArticleData/FindArticle/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                ArticleDto article = response.Content.ReadAsAsync<ArticleDto>().Result;
                return View(article);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return View("NotFound");
            }
            else
            {
                Debug.WriteLine("Failed to retrieve article details. Error: " + response.StatusCode);
                return View("Error");
            }
        }

        /// <summary>
        /// Grabs the authentication cookie sent to this controller.
        /// For proper WebAPI authentication, you can send a post request with login credentials to the WebAPI and log the access token from the response. The controller already knows this token, so we're just passing it up the chain.
        /// 
        /// Here is a descriptive article which walks through the process of setting up authorization/authentication directly.
        /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/individual-accounts-in-web-api
        /// </summary>
        private void GetApplicationCookie()
        {
            string token = "";
            //HTTP client is set up to be reused, otherwise it will exhaust server resources.
            //This is a bit dangerous because a previously authenticated cookie could be cached for
            //a follow-up request from someone else. Reset cookies in HTTP client before grabbing a new one.
            client.DefaultRequestHeaders.Remove("Cookie");
            if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            //collect token as it is submitted to the controller
            //use it to pass along to the WebAPI.
            Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }
        /// <summary>
        /// Displays a form for creating a new article.
        /// </summary>
        /// <returns>The view containing the form for creating a new article.</returns>
        /// <example>GET: Article/New</example>
        [Authorize]
        public ActionResult New()
        {
            GetApplicationCookie();//get token credentials
            return View();
        }

        /// <summary>
        /// Creates a new article.
        /// </summary>
        /// <param name="article">The article to be created.</param>
        /// <returns>Redirects to the list of articles upon successful creation, otherwise redirects to an error page.</returns>
        /// <example>POST: Article/Create</example>
        [HttpPost]
        [Authorize]
        public ActionResult Create(Article article)
        {
            GetApplicationCookie();//get token credentials
            string url = "ArticleData/AddArticle";
            string jsonpayload = jss.Serialize(article);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                Debug.WriteLine("Failed to add article. Error: " + response.StatusCode);
                return View("Error");
            }
        }

        /// <summary>
        /// Displays a form for editing a article.
        /// </summary>
        /// <param name="id">The ID of the article to be edited.</param>
        /// <returns>The view containing the form for editing the specified article.</returns>
        /// <example>GET: Article/Edit/5</example>
        [Authorize]
        public ActionResult Edit(int id)
        {
            GetApplicationCookie();//get token credentials
            string url = "ArticleData/FindArticle/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                ArticleDto article = response.Content.ReadAsAsync<ArticleDto>().Result;
                return View(article);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return View("NotFound");
            }
            else
            {
                Debug.WriteLine("Failed to retrieve article details for editing. Error: " + response.StatusCode);
                return View("Error");
            }
        }

        /// <summary>
        /// Updates a article.
        /// </summary>
        /// <param name="id">The ID of the article to be updated.</param>
        /// <param name="article">The updated article data.</param>
        /// <returns>Redirects to the details page of the updated article upon successful update, otherwise returns to the edit page.</returns>
        /// <example>POST: Article/Update/5</example>
        [HttpPost]
        [Authorize]
        public ActionResult Update(int id, Article article)
        {
            GetApplicationCookie();//get token credentials
            string url = "ArticleData/UpdateArticle/" + id;
            string jsonpayload = jss.Serialize(article);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                Debug.WriteLine("Failed to update article. Error: " + response.StatusCode);
                return View("Error");
            }
        }

        /// <summary>
        /// Displays a confirmation page for deleting a article.
        /// </summary>
        /// <param name="id">The ID of the article to be deleted.</param>
        /// <returns>The view containing the confirmation message for deleting the specified article.</returns>
        /// <example>GET: Article/Delete/5</example>
        [Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            GetApplicationCookie();//get token credentials
            string url = "ArticleData/FindArticle/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                ArticleDto article = response.Content.ReadAsAsync<ArticleDto>().Result;
                return View(article);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return View("NotFound");
            }
            else
            {
                Debug.WriteLine("Failed to retrieve article details for deletion confirmation. Error: " + response.StatusCode);
                return View("Error");
            }
        }

        /// <summary>
        /// Deletes a article.
        /// </summary>
        /// <param name="id">The ID of the article to be deleted.</param>
        /// <returns>Redirects to the list of articles upon successful deletion, otherwise redirects to an error page.</returns>
        /// <example>POST: Article/Delete/5</example>
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();//get token credentials
            string url = "ArticleData/DeleteArticle/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }
    }
}
