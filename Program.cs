using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using WebAPI.DAO;
using WebAPI.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(120);//You can set Time   
});
builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling
        = ReferenceLoopHandling.Ignore;
    opt.SerializerSettings.ContractResolver
        = new CamelCasePropertyNamesContractResolver();
});
builder.Services.AddControllers();
SysConfigModel.Configuration = builder.Configuration;

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
// Configure the HTTP request pipeline.

// app.UseAuthorization();

app.MapControllers();

builder.Services.AddCors();
app.UseCors(options => options
         .AllowAnyHeader()               // ȷ�����������κα�ͷ
         .AllowAnyMethod()               // ȷ�����������κη���
         .SetIsOriginAllowed(o => true)  // ����ָ����isOriginAllowedΪ��������
         .AllowCredentials());
app.UseSession();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapFallbackToFile("/index.html");
});
new InitDAO().InitDatabase();

app.Run();
