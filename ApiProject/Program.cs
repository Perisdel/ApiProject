using YourNamespace.Services;

var builder = WebApplication.CreateBuilder(args);

// HttpClient for making API calls
builder.Services.AddHttpClient<ConsolidatedApiService>();

// Register controllers
builder.Services.AddControllers();

// Swagger for API documentation
builder.Services.AddSwaggerGen();

var app = builder.Build();

//  HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Consolidated API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
