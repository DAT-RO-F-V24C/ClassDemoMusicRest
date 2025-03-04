using ClassDemoMusicLib.repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<MusicRepository>(new MusicRepository(true));


// Tilføj CORS
builder.Services.AddCors(options =>
    {
    options.AddPolicy("AllowOnlyGetPut",
        builder => builder.AllowAnyOrigin().
                   WithMethods("GET", "PUT", "DELETE", "POST").
                   AllowAnyHeader()
     );
    }
);


builder.Services.AddControllers();
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

app.UseAuthorization();

app.UseCors("AllowOnlyGetPut");

app.MapControllers();

app.Run();
