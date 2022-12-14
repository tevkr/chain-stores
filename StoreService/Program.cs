using Microsoft.EntityFrameworkCore;
using StoreService.AsyncDataServices;
using StoreService.Data;
using StoreService.SyncDataServices.Grpc;
using StoreService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
if (builder.Environment.IsProduction())
{
    Console.WriteLine("--> Using SqlServer Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("StoresConn")));
}
else
{
    Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseInMemoryDatabase("InMem"));
}
builder.Services.AddScoped<IStoreRepo, StoreRepo>();

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.WriteLine($"--> EmployeeService Endpoint {builder.Configuration["EmployeeService"]}");

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

app.MapGrpcService<GrpcStoreService>();
app.MapGet("/protos/stores.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("Protos/stores.proto"));
});

PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.Run();
