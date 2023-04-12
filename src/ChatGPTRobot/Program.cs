using ChatGPTRobot.Domains.Feishu;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

app.MapGet("/", () => "hello world");

app.MapPost("/chat", (RecieveMessageEvent request) => {
    return "success";
});

app.Run("http://0.0.0.0:9527");

