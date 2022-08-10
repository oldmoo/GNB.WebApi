using GNB.Api.ProgramExtensions;
using GNB.Infrastructure;

var builder = WebApplication
     .CreateBuilder(args)
     .ConfigureBuilder();

var app = builder
     .Build()
     .ConfigureApplication();

app.Run();