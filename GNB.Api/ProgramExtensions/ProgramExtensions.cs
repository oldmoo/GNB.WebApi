using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using GNB.Application;
using GNB.Application.ApplicationServicesContracts.TransactionBySku;
using GNB.Application.ApplicationServicesImplementations.TransactionBySku;
using GNB.Infrastructure;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;

namespace GNB.Api.ProgramExtensions;

public static class ProgramExtensions
{
          public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
     {

          #region Serialisation

          _ = builder.Services.Configure<JsonOptions>(opt =>
          {
               opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
               opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
               opt.SerializerOptions.PropertyNameCaseInsensitive = true;
               opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
               //opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
          });

          #endregion Serialisation
          
          #region Swagger
          
               var ti = CultureInfo.CurrentCulture.TextInfo;
          
               _ = builder.Services.AddEndpointsApiExplorer();
               _ = builder.Services.AddSwaggerGen(options =>
               {
                    options.SwaggerDoc("v1",
                         new OpenApiInfo
                         {
                              Version = "v1",
                              Title = $"GnbApi API - {ti.ToTitleCase(builder.Environment.EnvironmentName)} ",
                              Description = "International Business Men api",
                              Contact = new OpenApiContact
                              {
                                   Name = "GNB.WepApi",
                                   Email = "pape@mamadou.dev",
                                   Url = new Uri("https://github.com/oldmoo/GNB.WebApi")
                              },
          
                         });
               });
               #endregion Swagger
          
          #region Add Services to the container

          builder.Services.AddScoped(typeof(ITransactionAppBySkuService), typeof(TransactionAppBySkuService));
               _ = builder.Services.AddControllers();
               _ = builder.Services.AddServicesInfrastructure();
               _ = builder.Services.AddServicesApplication();
          
          #endregion

          #region ConfigureExternalServicesApi
          
               _ = builder.Host.ConfigureServices((context, services) =>
               {
                    services.AddHttpClient(context.Configuration["HttpClient:Rate"], client =>
                    {
                         client.BaseAddress = new Uri($"{context.Configuration["HttpClient:BaseAddress"]}/rates.json");
                    });
                    services.AddHttpClient(context.Configuration["HttpClient:Transaction"], client =>
                    {
                         client.BaseAddress = new Uri($"{context.Configuration["HttpClient:BaseAddress"]}/transactions.json");
                    });
               });
          
          #endregion

          return builder;
     }
          public static WebApplication ConfigureApplication(this WebApplication app)
          {
               #region Swagger

               var ti = CultureInfo.CurrentCulture.TextInfo;

               _ = app.UseSwagger();
               _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"GNB.API - {ti.ToTitleCase(app.Environment.EnvironmentName)} - V1"));

               #endregion Swagger
     
               // Configure the HTTP request pipeline.
               if (app.Environment.IsDevelopment())
               {
                    app.UseSwagger();
                    app.UseSwaggerUI();
               }

               app.UseHttpsRedirection();

               app.UseAuthorization();

               app.MapControllers();
          
               return app;
          }
}