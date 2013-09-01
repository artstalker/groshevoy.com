using System.Collections.Generic;
using groshevoy.com.Core.Context;
using groshevoy.com.Core.Objects;

namespace groshevoy.com.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<groshevoy.com.Core.Context.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            
        }

        protected override void Seed(groshevoy.com.Core.Context.BlogContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var tags = new List<Tag>
            {
                new Tag() {Description = "Default tag", Name = "Default", UrlSlug = "default"}
            };

            tags.ForEach(s => context.Tags.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var categories = new List<Category>
            {
                new Category() {Description = "Default category", Name = "Default", UrlSlug = "default"}
            };

            categories.ForEach(s => context.Categories.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
            var posts = new List<Post>
            {
                new Post()
                {
                    Category = categories.First(),
                    Description = "This is a test post",
                    Meta = "txt",
                    Modified = DateTime.Now,
                    PostedOn = DateTime.Now.AddDays(-1),
                    Published = true,
                    ShortDescription = "Test",
                    Title = "Test post",
                    UrlSlug = "test_post",
                    Tags = tags
                }

            };
            posts.ForEach(s => context.Posts.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();
        }
    }
}

