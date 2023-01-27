var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(controller =>
{
    controller.CacheProfiles.Add("Pineapple", new()
    {
        Duration = 240
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCaching();
builder.Services.AddHttpCacheHeaders(
    (expirationModelOptions) =>
    {
        expirationModelOptions.MaxAge = 30;
        expirationModelOptions.CacheLocation = Marvin.Cache.Headers.CacheLocation.Private;
    },
    (validationModelOptions) =>
    {
        validationModelOptions.MustRevalidate = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCaching();
app.UseHttpCacheHeaders();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
