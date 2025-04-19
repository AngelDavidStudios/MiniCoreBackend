using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using MiniCore.API.Models;
using MiniCore.API.Repository;
using MiniCore.API.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

// Services
var awsOptions = builder.Configuration.GetAWSOptions();
awsOptions.Profile = "AdminAccess";
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

// Repositories
builder.Services.AddScoped<IRepository<Empleado>, EmpleadoRepository>();
builder.Services.AddScoped<IRepository<Project>, ProjectRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(static builder => 
    builder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());
app.UseAuthorization();
app.MapControllers();
app.Run();