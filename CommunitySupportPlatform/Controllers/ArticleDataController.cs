using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CommunitySupportPlatform.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http.Description;

namespace CommunitySupportPlatform.Controllers
{
    /// <summary>
    /// This controller handles the CRUD operations for articles.
    /// </summary>
    public class ArticlesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all articles in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: All articles in the database.
        /// </returns>
        /// <example>
        /// GET: api/ArticleData/ListArticles
        /// </example>
        [HttpGet]
        [Route("api/ArticleData/ListArticles")]
        public IEnumerable<ArticleDto> ListArticles()
        {
            List<Article> articles = db.Articles.ToList();
            List<ArticleDto> articleDtos = new List<ArticleDto>();

            articles.ForEach(c => articleDtos.Add(new ArticleDto()
            {
                Id = c.ArticleId,
                Title = c.Title,
                Content = c.Content,
                PublishedDate = c.PublishedDate
            }));

            return articleDtos;
        }

        /// <summary>
        /// Finds a specific article by ID.
        /// </summary>
        /// <param name="id">The ID of the article.</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A article matching the provided ID.
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// GET: api/ArticleData/FindArticle/5
        /// </example>
        [ResponseType(typeof(Article))]
        [HttpGet]
        [Route("api/ArticleData/FindArticle/{id}")]

        public IHttpActionResult FindArticle(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            ArticleDto articleDto = new ArticleDto()
            {
                Id = article.ArticleId,
                Title = article.Title,
                Content = article.Content,
                PublishedDate = article.PublishedDate
            };

            return Ok(articleDto);
        }

        /// <summary>
        /// Retrieves a list of articles related to a specific job.
        /// </summary>
        /// <param name="jobId">The ID of the job for which to retrieve articles.</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A list of ArticleDto objects related to the job.
        /// </returns>
        /// <example>
        /// GET: api/ArticleData/ListArticlesForJob/5
        /// </example>
        [Route("api/ArticleData/ListArticlesForJob/{jobId}")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ArticleDto>))]
        public IHttpActionResult ListArticlesForJob(int jobId)
        {
            // Assuming there is a relationship between jobs and articles in the database
            var articles = db.Articles.Where(a => a.JobId == jobId).ToList();

            List<ArticleDto> articleDtos = new List<ArticleDto>();

            articles.ForEach(a => articleDtos.Add(new ArticleDto()
            {
                Id = a.ArticleId,
                Title = a.Title,
                Content = a.Content,
                PublishedDate = a.PublishedDate
            }));

            return Ok(articleDtos);
        }


        /// <summary>
        /// Updates a article in the system with POST Data input.
        /// </summary>
        /// <param name="id">The ID of the article to be updated.</param>
        /// <param name="article">JSON form data of the article.</param>
        /// <returns>
        /// HEADER: 204 (No Content)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/ArticleData/UpdateArticle/5
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/ArticleData/UpdateArticle/{id}")]
        [Authorize]
        public IHttpActionResult UpdateArticle(int id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.ArticleId)
            {
                return BadRequest();
            }

            db.Entry(article).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds a new article to the system.
        /// </summary>
        /// <param name="article">JSON form data of the article.</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: The created article data.
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/ArticleData/AddArticle
        /// </example>
        [ResponseType(typeof(Article))]
        [HttpPost]
        [Route("api/ArticleData/AddArticle")]
        [Authorize]
        public IHttpActionResult AddArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Articles.Add(article);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Deletes a article from the system by ID.
        /// </summary>
        /// <param name="id">The ID of the article to be deleted.</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/ArticleData/DeleteArticle/5
        /// </example>
        [ResponseType(typeof(Article))]
        [HttpPost]
        [Route("api/ArticleData/DeleteArticle/{id}")]
        [Authorize]
        public IHttpActionResult DeleteArticle(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            db.Articles.Remove(article);
            db.SaveChanges();

            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(int id)
        {
            return db.Articles.Count(e => e.ArticleId == id) > 0;
        }
    }
}
