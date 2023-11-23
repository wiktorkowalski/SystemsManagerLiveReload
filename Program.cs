using Amazon.Extensions.NETCore.Setup;
using SystemsManagerLiveReload;
using SystemsManagerLiveReload.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddSystemsManager(o =>
{
    o.ReloadAfter = TimeSpan.FromSeconds(5);
    o.Optional = false;
    o.Path = "/live-reload";
    o.AwsOptions = new AWSOptions
    {
        Profile = "wiktorkowalski",
    };
});

builder.Services.Configure<ParamstoreOptions>(builder.Configuration);
builder.Services.Configure<WeatherOptions>(builder.Configuration);

builder.Services.AddSingleton<OptionsSingleton>();
builder.Services.AddScoped<WeatherService>();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();