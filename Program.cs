using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAO;
using WebAPI.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(120);//You can set Time   
});
builder.Services.AddControllers();
SysConfigModel.Configuration = builder.Configuration;

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

builder.Services.AddCors();
app.UseCors(options => options
         .AllowAnyHeader()               // ȷ�����������κα�ͷ
         .AllowAnyMethod()               // ȷ�����������κη���
         .SetIsOriginAllowed(o => true)  // ����ָ����isOriginAllowedΪ��������
         .AllowCredentials());
app.UseSession();

new InitDAO().InitDatabase();

app.Run();
