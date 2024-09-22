using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MockNet04G2.Business.DTOs.Authentication.Requests;
using MockNet04G2.Business.DTOs.Campaign.Requests;
using MockNet04G2.Business.DTOs.Users.Requests;
using MockNet04G2.Business.Services.Authentication;
using MockNet04G2.Business.Services.Authentication.Validators;
using MockNet04G2.Business.Services.Campagin;
using MockNet04G2.Business.Services.Campagin.Validators;
using MockNet04G2.Business.Services.Campaign;
using MockNet04G2.Business.Services.Organization;
using MockNet04G2.Business.Services.Payment;
using MockNet04G2.Business.Services.User;
using MockNet04G2.Business.Services.User.Validators;
using MockNet04G2.Core.Data;
using MockNet04G2.Core.Repositories;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MockDbContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseSqlServer(connectionString);
});

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MockProjectNet04G2 API",
        Version = "v1"
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var authOption = new AuthenticationOption();
var config = builder.Configuration.GetSection("Jwt");
config.Bind(authOption);

builder.Services.AddSingleton(authOption);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOption.Key)),
        ValidateAudience = false,
        ValidateIssuer = false,
        RequireExpirationTime = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddOptions<AuthenticationOption>().Bind(builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//service
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<GetAllUserService>();
builder.Services.AddScoped<FindUserService>();
builder.Services.AddScoped<ChangeUserRoleService>();
builder.Services.AddScoped<ChangePasswordService>();
builder.Services.AddScoped<UsersPagingService>();
builder.Services.AddScoped<CountUserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ResetPasswordService>();
builder.Services.AddScoped<UpdateUserService>();
builder.Services.AddScoped<FilterService>();
builder.Services.AddScoped<FilterUserCountService>();



builder.Services.AddScoped<GetAllCampaignsService>();
builder.Services.AddScoped<GetCampaignByIdService>();
builder.Services.AddScoped<FilterCampaignsByStatusService>();
builder.Services.AddScoped<CampaignsPagingService>();   
builder.Services.AddScoped<GetTotalCampaignsService>();
builder.Services.AddScoped<AddCampaignService>();
builder.Services.AddScoped<DeleteCampaignService>();
builder.Services.AddScoped<SearchCampaignService>();
builder.Services.AddScoped<GetCampaignsCountAfterFilterService>();
builder.Services.AddScoped<UpdateCampaignService>();
builder.Services.AddScoped<GetHomePageCampaignService>();
builder.Services.AddScoped<GetHomePageCampaignCountService>();
builder.Services.AddScoped<EndDateService>();   

builder.Services.AddScoped<OrganizationService>();

builder.Services.AddScoped<CreatePaymentService>();

// Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<ICooperateRepository,CooperateRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IDonateRepository, DonateRepository>();

// Validator
builder.Services.AddScoped<IValidator<LoginRequest>, LoginValidator>();
builder.Services.AddScoped<IValidator<RegisterRequest>, RegisterValidator>();
builder.Services.AddScoped<IValidator<ChangePasswordRequest>, ChangePasswordValidator>();
builder.Services.AddScoped<IValidator<CampaignDetailRequest>, CampaignDetailRequestValidator>();
builder.Services.AddScoped<IValidator<ExtendCampaignRequest>, UpdateCampaignValidator>();
builder.Services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserRequestValidator>();


builder.Services.AddControllers();



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

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
