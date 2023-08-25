using TaskExecutor.Events;
using TaskExecutor.Services;
using TaskExecutor.Services.Impl;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<INodeService, NodeService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IRestService, RestService>();

builder.Services.AddSingleton<NodeAvailableEvent>(_ => new NodeAvailableEvent((sender, e) =>
{
    var scope = builder.Services.BuildServiceProvider().CreateScope();
    var scheduleService = scope.ServiceProvider.GetRequiredService<IScheduleService>();

    scheduleService.ScheduleNextTaskInQueue();
}));

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