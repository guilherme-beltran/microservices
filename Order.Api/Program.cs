using Orders.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddContext(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/Order", () =>
{
    return "Order Api on";
})
.WithName("online")
.WithOpenApi();

app.Run();
