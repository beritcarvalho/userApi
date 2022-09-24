using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserApi.Api.Extensions;
using UserApi.Applications;
using UserApi.Applications.Services;
using UserApi.Infrastructure.IoC.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

#region JwtConfiguration
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Jwt").GetSection("key").Value);
builder.Services.AddAuthentication(authenOptions =>
{
    authenOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authenOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
#endregion



// Add services to the container.
builder.Services.AddServices();


//Extension
builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("SendInBlue"));
builder.Services.LoadConfiguration(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
