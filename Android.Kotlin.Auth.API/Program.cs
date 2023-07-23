using Android.Kotlin.Auth.API.Contexts;
using Android.Kotlin.Auth.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);
var builderOdata = new ODataConventionModelBuilder();
builderOdata.EntitySet<Product>("Products");
builderOdata.EntitySet<Category>("Categories");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.Authority = "http://localhost:5001";
        opt.Audience = "resource_product_api";
        opt.RequireHttpsMetadata = false;
        
    });
// Add services to the container.

builder.Services.AddControllers().AddOData(opt =>
{
    opt.Select().Expand().OrderBy().Count().Filter().AddRouteComponents("odata",builderOdata.GetEdmModel());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<BaseDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

/*builder.Services.AddSwaggerGen();*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    /*app.UseSwagger();
    app.UseSwaggerUI();*/
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(enp => enp.MapControllers());

app.Run();