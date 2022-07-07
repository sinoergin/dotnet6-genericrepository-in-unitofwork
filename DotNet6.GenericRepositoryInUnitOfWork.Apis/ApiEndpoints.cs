using Microsoft.AspNetCore.Mvc;

namespace DotNet6.GenericRepositoryInUnitOfWork.Apis
{
    public static class ApiEndpoints
    {
        public static void AddGenericRepositoryInUnitOfWorkAppApi(this IServiceCollection services)
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

        public static IEndpointRouteBuilder MapCategoryApiRoutes(this IEndpointRouteBuilder builder)
        {
            builder.MapGet("/api/categories",
                async (IUnitOfWork unitOfWork) =>
                {
                    return await unitOfWork.CategoryRepository.GetAllAsync();
                })
                .Produces(StatusCodes.Status200OK)
                .WithTags("Categories");

            builder.MapGet("/api/categories/{id}",
                async (
                    int id,
                    IUnitOfWork unitOfWork) =>
                {
                    var category = await unitOfWork.CategoryRepository.GetAsync(id);
                    return category is null ?
                        Results.Problem("Item not found", statusCode: StatusCodes.Status404NotFound) :
                        Results.Json(category);
                })
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithTags("Categories");

            builder.MapPost("/api/categories",
                async (
                    [FromBody] Category category,
                    IUnitOfWork unitOfWork) =>
                {
                    if (category is null)
                        return Results.Problem("Item cannot be null", statusCode: StatusCodes.Status400BadRequest);

                    unitOfWork.CategoryRepository.Insert(category);
                    var completed = await unitOfWork.CompleteAsync();
                    return completed switch
                    {
                        true => Results.Created($"/api/categories/{category.Id}", category),
                        false => Results.Problem("Item already completed", statusCode: StatusCodes.Status400BadRequest),
                    };
                })
                .Produces(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithTags("Categories");

            builder.MapPut("/api/categories/update",
                async (
                    [FromBody] Category category,
                    IUnitOfWork unitOfWork) =>
                {
                    if (category is null)
                        return Results.Problem("Item cannot be null", statusCode: StatusCodes.Status400BadRequest);

                    unitOfWork.CategoryRepository.Update(category);
                    var completed = await unitOfWork.CompleteAsync();

                    return completed switch
                    {
                        true => Results.NoContent(),
                        false => Results.Problem("Item already completed", statusCode: StatusCodes.Status400BadRequest),
                    };
                })
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithTags("Categories");

            builder.MapDelete("/api/categories/delete",
                async (
                    [FromBody] Category category,
                    IUnitOfWork unitOfWork) =>
                {
                    if (category is null)
                        return Results.Problem("Item cannot be null", statusCode: StatusCodes.Status400BadRequest);

                    unitOfWork.CategoryRepository.Delete(category);
                    var completed = await unitOfWork.CompleteAsync();

                    return completed switch
                    {
                        true => Results.NoContent(),
                        false => Results.Problem("Item already deleted", statusCode: StatusCodes.Status400BadRequest),
                    };
                })
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithTags("Categories");

            builder.MapDelete("/api/categories/delete/{id}",
                async (
                    int id,
                    IUnitOfWork unitOfWork) =>
                {
                    var category = await unitOfWork.CategoryRepository.GetAsync(id);
                    if (category is null)
                        return Results.Problem("Item not found", statusCode: StatusCodes.Status404NotFound);

                    unitOfWork.CategoryRepository.Delete(category);
                    var completed = await unitOfWork.CompleteAsync();

                    return completed switch
                    {
                        true => Results.NoContent(),
                        false => Results.Problem("Item already deleted", statusCode: StatusCodes.Status400BadRequest),
                    };
                })
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithTags("Categories");

            return builder;
        }

        public static IEndpointRouteBuilder MapProductApiRoutes(this IEndpointRouteBuilder builder)
        {
            builder.MapGet("/api/products",
                async (IUnitOfWork unitOfWork) =>
                {
                    return await unitOfWork.ProductRepository.GetAllAsync();
                })
                .Produces(StatusCodes.Status200OK)
                .WithTags("Products");

            builder.MapGet("/api/products/{id}",
                async (
                    int id,
                    IUnitOfWork unitOfWork) =>
                {
                    var product = await unitOfWork.ProductRepository.GetAsync(id);
                    return product is null ?
                        Results.Problem("Item not found", statusCode: StatusCodes.Status404NotFound) :
                        Results.Json(product);
                })
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithTags("Products");

            builder.MapPost("/api/products",
                async (
                    [FromBody] Product product,
                    IUnitOfWork unitOfWork) =>
                {
                    if (product is null)
                        return Results.Problem("Item cannot be null", statusCode: StatusCodes.Status400BadRequest);

                    unitOfWork.ProductRepository.Insert(product);
                    var completed = await unitOfWork.CompleteAsync();
                    return completed switch
                    {
                        true => Results.Created($"/api/products/{product.Id}", product),
                        false => Results.Problem("Item already completed", statusCode: StatusCodes.Status400BadRequest),
                    };
                })
                .Produces(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithTags("Products");

            builder.MapPut("/api/products/update",
                async (
                    [FromBody] Product product,
                    IUnitOfWork unitOfWork) =>
                {
                    if (product is null)
                        return Results.Problem("Item cannot be null", statusCode: StatusCodes.Status400BadRequest);

                    unitOfWork.ProductRepository.Update(product);
                    var completed = await unitOfWork.CompleteAsync();

                    return completed switch
                    {
                        true => Results.NoContent(),
                        false => Results.Problem("Item already completed", statusCode: StatusCodes.Status400BadRequest),
                    };
                })
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithTags("Products");

            builder.MapDelete("/api/products/delete",
                async (
                    [FromBody] Product product,
                    IUnitOfWork unitOfWork) =>
                {
                    if (product is null)
                        return Results.Problem("Item cannot be null", statusCode: StatusCodes.Status400BadRequest);

                    unitOfWork.ProductRepository.Delete(product);
                    var completed = await unitOfWork.CompleteAsync();

                    return completed switch
                    {
                        true => Results.NoContent(),
                        false => Results.Problem("Item already deleted", statusCode: StatusCodes.Status400BadRequest),
                    };
                })
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithTags("Products");

            builder.MapDelete("/api/products/delete/{id}",
                async (
                    int id,
                    IUnitOfWork unitOfWork) =>
                {
                    var product = await unitOfWork.ProductRepository.GetAsync(id);
                    if (product is null)
                        return Results.Problem("Item not found", statusCode: StatusCodes.Status404NotFound);

                    unitOfWork.ProductRepository.Delete(product);
                    var completed = await unitOfWork.CompleteAsync();

                    return completed switch
                    {
                        true => Results.NoContent(),
                        false => Results.Problem("Item already deleted", statusCode: StatusCodes.Status400BadRequest),
                    };
                })
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithTags("Products");

            return builder;
        }
    }
}
