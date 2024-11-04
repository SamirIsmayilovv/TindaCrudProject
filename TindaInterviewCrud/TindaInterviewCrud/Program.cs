using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.Data.Contexts;
using Project.Data.DTOs.Product;
using Project.Data.Repositories.Implementations;
using Project.Data.Repositories.Interfaces;
using Project.Service.Services.Implementations;
using Project.Service.Services.Interfaces;
using Project.Service.Validations.Account;
using Project.Service.Validations.Category;
using Project.Service.Validations.Tipe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//database
builder.Services.AddDbContext<AppDbcontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//SWT token
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

})
   .AddJwtBearer(opt =>
   {
       opt.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = "https://localhost:7199/",
           ValidAudience = "https://localhost:7199/",
           IssuerSigningKey = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])
           )
       };
   });



//fluent validation
// Add FluentValidation and register validators from the assembly
builder.Services.AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryPostDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryPutDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TipePostDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TipePutDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductPostDto>();


//Categories Injection
builder.Services.AddScoped<ICategoryReposity, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
//Type Injection
builder.Services.AddScoped<ITipeRepository, TipeRepository>();
builder.Services.AddScoped<ITipeService, TipeService>();
//Product Injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
//IIdentityService Injection
builder.Services.AddScoped<IIdentityService, IdentityService>();


//user
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
{

}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbcontext>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
