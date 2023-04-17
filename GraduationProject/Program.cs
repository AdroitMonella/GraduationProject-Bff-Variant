using GraduationProject;
using GraduationProject.CocktailDB;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddDbContext<IApplicationDbContext, GraduationProject.ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHttpClient<ICocktailDBApi, CocktailDBApi>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://www.thecocktaildb.com");
});
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// M� egentlig konfigureres til spesifik access <----------
app.UseCors(c =>{ c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });

app.MapControllers();
app.Run();
