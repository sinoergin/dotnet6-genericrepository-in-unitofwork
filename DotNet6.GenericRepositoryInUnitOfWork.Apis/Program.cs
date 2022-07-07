using DotNet6.GenericRepositoryInUnitOfWork.Apis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGenericRepositoryInUnitOfWorkAppApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var services = app.Services.CreateAsyncScope())
{
    var context = services.ServiceProvider.GetRequiredService<MyDatabaseContext>();
    await context.Database.EnsureCreatedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCategoryApiRoutes();
app.MapProductApiRoutes();

app.Run();

