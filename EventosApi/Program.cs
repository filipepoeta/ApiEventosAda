using AutoMapper;
using EventosApi.Filter;
using EventosInfra.Repository;
using EventosServices.Dto;
using EventosServices.Entity;
using EventosServices.Interfaces;
using EventosServices.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var chaveCripto = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));

//Adicionando JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chaveCripto),
                    ValidateIssuer = true,
                    ValidIssuer = "APIPessoa.com",
                    ValidateAudience = true,
                    ValidAudience = "EventosApi.com"
                };
            });

//Adicionando Filtros
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(ExcecaoGeralFilter));
});

// Add services to the container.
builder.Services.AddScoped<ICityEventRepository,CityEventRepository>();
builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();
builder.Services.AddScoped<ICityEventService, CityEventService>();
builder.Services.AddScoped<IEventReservationService, EventReservationService>();
builder.Services.AddSingleton<IDbConnection>((sp) => new MySqlConnection(Environment.GetEnvironmentVariable("DATABASE_CONFIG")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adicionando autoMapper
MapperConfiguration mapperConfig = new(mc =>
{
    mc.CreateMap<EventReservation, EventReservationDto>().ReverseMap();
    mc.CreateMap<CityEvent, CityEventDto>().ReverseMap();
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
