using Blog_Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private static readonly List<Blog>? _BlogList = new List<Blog>()
            {
                new Blog
                {
                    Id = 1,
                    Category = "BlogApi Yapımı",
                    Title = "Öğrenme",
                    Content = "Öğrenme aşamasında yaptıklarım",
                    Author = "Emin Hakkoymaz",
                    Comment = new List<string> {"Deniyorum:)" }
                },
                new Blog
                {
                    Id = 2,
                    Category = "BlogApi Yapımı",
                    Title = "Yapım",
                    Content = "Yapmaya çalışıyorum",
                    Author = "Emin Hakkoymaz",
                    Comment = new List<string> {"Deniyorum:)" }
                },
                new Blog
                {
                    Id = 3,
                    Category = "BlogApi Yapımı",
                    Title = "Sonuç",
                    Content = "Sonuç",
                    Author = "Emin Hakkoymaz",
                    Comment = new List<string> {"Deniyorum:)" }
                },
                new Blog
                {
                    Id = 4,
                    Category = "BlogApi Geliştirme",
                    Title = "Geliştirme",
                    Content = "Öğrenme aşamasında yaptıklarımın devamı",
                    Author = "Emin Hakkoymaz",
                    Comment = new List<string> {"Deniyorum:)" }
                },
            };

        [HttpPost]
        public IActionResult CreateBlog([FromBody] Blog new_blog)
        {
            var existingBlog = _BlogList.Find(x => x.Id == new_blog.Id);
            if (existingBlog != null)
            {
                return Conflict("Aynı ID'ye sahip bir blog zaten mevcut.");
            }

            _BlogList.Add(new_blog);
            return Ok();
        }

        [HttpPost("{id}/comment")]
        public IActionResult AddComment(int id, [FromBody] string comment)
        {
            var blogPost = _BlogList.Find(p => p.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            blogPost.Comment.Add(comment);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, [FromBody] Blog update_blog)
        {
            var existBlog = _BlogList.Find(x => x.Id == id);
            if (existBlog == null)
            {
                return NotFound();
            }
            existBlog.Title = update_blog.Title;
            existBlog.Content = update_blog.Content;
            existBlog.Author = update_blog.Author;
            existBlog.Category = update_blog.Category;
            existBlog.Comment = update_blog.Comment;
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_BlogList);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var blogpost = _BlogList.Find(x => x.Id == id);
            if (blogpost == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(blogpost);
            }
        }

        [HttpGet("search")]
        /// Sadece başlık, yazar ve içerik için arama yapabilirsiniz.
        public IActionResult SearchBlogPosts(string keyword)
        {
            var matchingPosts = _BlogList.Where(p => p.Author.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                     p.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                     p.Category.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                     p.Content.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                                        .ToList();
            
            return Ok(matchingPosts);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var deleteBlog = _BlogList.Find(x => x.Id == id);
            if (deleteBlog == null)
            {
                return NotFound();
            }
            _BlogList.Remove(deleteBlog);
            return Ok();
        }
    }
}