using Microsoft.AspNetCore.Mvc;

namespace DotNet6.GenericRepositoryInUnitOfWork.Apis
{
    public static class ApiEndpointExplorer
    {
        public static void AddMyDatabase(this IServiceCollection services)
        {
            services.AddDbContext<MyDatabaseContext>(opt =>
                opt.UseInMemoryDatabase("MyDatabase")
                .LogTo(Console.WriteLine)
                .EnableDetailedErrors());

            services.AddScoped<DbContext, MyDatabaseContext>();
            services.AddScoped(typeof(IRepository<Category, int>), typeof(Repository<Category, int>));
            services.AddScoped(typeof(IRepository<Product, int>), typeof(Repository<Product, int>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void MapToCategoryApi(this WebApplication app)
        {
            app.MapGet("/api/categories", 
                async (IUnitOfWork unitOfWork) =>
                {
                    return Results.Ok(await unitOfWork.CategoryRepository.GetAllAsync());
                })
                .Produces(200, contentType: "application/json")
                .ProducesProblem(404)
                .WithTags("Categories");

            app.MapGet("/api/categories/{id}",
                async (
                    int id,
                    IUnitOfWork unitOfWork) =>
                {
                    return Results.Ok(await unitOfWork.CategoryRepository.GetAsync(id));
                })
                .Produces(200, contentType: "application/json")
                .ProducesProblem(404)
                .WithTags("Categories");

            app.MapPost("/api/categories",
                async (
                    [FromBody] Category category,
                    IUnitOfWork unitOfWork) =>
                {
                    unitOfWork.CategoryRepository.Insert(category);
                    if (await unitOfWork.CompleteAsync())
                    {
                        return Results.Created(string.Empty, category);
                    }

                    return Results.BadRequest();
                })
                .Produces(201, contentType: "application/json")
                .ProducesProblem(404)
                .WithTags("Categories");

            app.MapPut("/api/categories/update",
                async (
                    [FromBody] Category category,
                    IUnitOfWork unitOfWork) =>
                {
                    unitOfWork.CategoryRepository.Update(category);
                    if (await unitOfWork.CompleteAsync())
                    {
                        return Results.NoContent();
                    }

                    return Results.BadRequest();
                })
                .Produces(204, contentType: "application/json")
                .ProducesProblem(404)
                .WithTags("Categories");

            app.MapDelete("/api/categories/delete",
                async (
                    [FromBody]Category category,
                    IUnitOfWork unitOfWork) =>
                {
                    unitOfWork.CategoryRepository.Delete(category);
                    if (await unitOfWork.CompleteAsync())
                    {
                        return Results.NoContent();
                    }

                    return Results.BadRequest();
                })
                .Produces(204, contentType: "application/json")
                .ProducesProblem(404)
                .WithTags("Categories");

            app.MapDelete("/api/categories/delete/{id}",
                async (
                    int id,
                    IUnitOfWork unitOfWork) =>
                {
                    var category = await unitOfWork.CategoryRepository.GetAsync(id);
                    unitOfWork.CategoryRepository.Delete(category);
                    if (await unitOfWork.CompleteAsync())
                    {
                        return Results.NoContent();
                    }

                    return Results.BadRequest();
                })
                .Produces(204, contentType: "application/json")
                .ProducesProblem(404)
                .WithTags("Categories");
        }

        public static void MapToProductApi(this WebApplication app)
        {
            app.MapGet("/api/products",
                async (IUnitOfWork unitOfWork) =>
                {
                    return Results.Ok(await unitOfWork.ProductRepository.GetAllAsync());
                })
                .Produces(200, contentType: "application/json")
                .ProducesProblem(404)
                .WithTags("Products");

            app.MapGet("/api/products/{id}",
                async (
                    int id,
                    IUnitOfWork unitOfWork) =>
                {
                    return Results.Ok(await unitOfWork.ProductRepository.GetAsync(id));
                })
                .Produces(200, contentType: "application/json")
                .ProducesProblem(404)
                .WithTags("Products");

            app.MapPost("/api/products",
                async (
                    [FromBody]Product product,
                    IUnitOfWork unitOfWork) =>
                {
                    unitOfWork.ProductRepository.Insert(product);
                    if (await unitOfWork.CompleteAsync())
                    {
                        return Results.Created(string.Empty, product);
                    }

                    return Results.BadRequest();
                })
                .Produces(201, contentType: "application/json")
                .ProducesProblem(404)
                .WithTags("Products");

            app.MapPut("/api/products/update",
                async (
                    [FromBody] Product product,
                    IUnitOfWork unitOfWork) =>
                {
                    unitOfWork.ProductRepository.Update(product);
                    if (await unitOfWork.CompleteAsync())
                    {
                        return Results.NoContent();
                    }

                    return Results.BadRequest();
                })
                .Produces(204, contentType: "application/json")
                .ProducesProblem(404)
                .WithTags("Products");

            app.MapDelete("/api/products/delete",
                async (
                    [FromBody]Product product,
                    IUnitOfWork unitOfWork) =>
                {
                    unitOfWork.ProductRepository.Delete(product);
                    if (await unitOfWork.CompleteAsync())
                    {
                        return Results.NoContent();
                    }

                    return Results.BadRequest();
                })
                .Produces(204, contentType: "application/json")
                .ProducesProblem(404)
                .WithTags("Products");

            app.MapDelete("/api/products/delete/{id}",
                async (
                    int id,
                    IUnitOfWork unitOfWork) =>
                {
                    var product = await unitOfWork.ProductRepository.GetAsync(id);
                    unitOfWork.ProductRepository.Delete(product);
                    if (await unitOfWork.CompleteAsync())
                    {
                        return Results.NoContent();
                    }

                    return Results.BadRequest();
                })
                .Produces(204, contentType: "application/json")
                .ProducesProblem(404)
                .WithTags("Products");
        }
    }
}
