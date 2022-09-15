using Newtonsoft.Json.Converters;
using NotinoHomeWork.Api;
using NotinoHomeWork.Application;
using NotinoHomeWork.Application.Configurations;
using NotinoHomeWork.Application.Providers.EmailProvider;
using NotinoHomeWork.Application.Providers.SerializerProvider;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDataTypeProvider, DataTypeProvider>();
builder.Services.AddScoped<IHomeWorkModule, HomeWorkModule>();
builder.Services.AddScoped<IEmailProvider, EmailProvider>();

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("Email"));

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport();


var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
