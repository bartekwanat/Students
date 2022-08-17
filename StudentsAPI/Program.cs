using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using StudentsAPI.Database;
using StudentsAPI.HealthChecks;
using StudentsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddServiceLayer();
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        var response = new HealthCheckResponse
        {
            Status = report.Status.ToString(),
            Checks = report.Entries.Select(e => new HealthCheck
            {
                Component = e.Key,
                Status = e.Value.Status.ToString(),
                Description = e.Value.Description
           }),
        Duration = report.TotalDuration
        };
    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
    });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
