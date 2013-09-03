using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using groshevoy.com.Core.Context;
using groshevoy.com.Core.Objects;
using Ninject.Infrastructure.Language;

namespace groshevoy.com.Core
{
    public class BlogRepository : IBlogRepository
    {
        // NHibernate object
        private readonly BlogContext _context;

        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public IList<Post> Posts(int pageNo, int pageSize)
        {
            var query = _context.Posts
                            .Where(p => p.Published)
                            .OrderByDescending(p => p.PostedOn)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Include(p => p.Category);

            //query.Include(p => p.Tags);

            return query.ToList();
        }

        public int TotalPosts()
        {
            return _context.Posts.Where(p => p.Published).Count();
        }

        public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            var query = _context.Posts
                            .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                            .OrderByDescending(p => p.PostedOn)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Include(p => p.Category).Include(p => p.Tags);

           

            return query.ToList();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return _context.Posts
                        .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                        .Count();
        }

        public Category Category(string categorySlug)
        {
            return _context.Categories.FirstOrDefault(t => t.UrlSlug.Equals(categorySlug));
        }

        public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
            var query = _context.Posts
                                .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                                .OrderByDescending(p => p.PostedOn)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .Include(p => p.Category).Include(p => p.Tags);


            return query.ToList();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return _context.Posts
                        .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                        .Count();
        }

        public Tag Tag(string tagSlug)
        {
            return _context.Tags.FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }

		  public IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
		  {
			  var query = _context.Posts
										 .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
										 .OrderByDescending(p => p.PostedOn)
										 .Skip(pageNo * pageSize)
										 .Take(pageSize)
										 .Include(p => p.Category).Include(p => p.Tags);

			  

			  return query.ToList();
		  }

		  public int TotalPostsForSearch(string search)
		  {
			  return _context.Posts
						 .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
						 .Count();
		  }

		  public Post Post(int year, int month, string titleSlug)
		  {
			  var query = _context.Posts
										 .Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug))
										 .Include(p => p.Category).Include(p => p.Tags);


			  return query.Single();
		  }

		  public IList<Category> Categories()
		  {
			  return _context.Categories.OrderBy(p => p.Name).ToList();
		  }

		  public IList<Tag> Tags()
		  {
			  return _context.Tags.OrderBy(p => p.Name).ToList();
		  }
    }
}
