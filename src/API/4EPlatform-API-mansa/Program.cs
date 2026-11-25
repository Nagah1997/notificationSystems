using _4EPlatform.Application.services;
using ControlPanel.Notifications.Protos;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors =
        builder.Environment.IsDevelopment();

    options.MaxReceiveMessageSize = 50 * 1024 * 1024;
    options.MaxSendMessageSize = 50 * 1024 * 1024;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCors", policy =>
        policy.WithOrigins(
                builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                    ?? new[] { "http://localhost:3000" })
            .AllowAnyHeader()
            .AllowAnyMethod());
});
//builder.Services.AddGrpcReflection();
//builder.WebHost.ConfigureKestrel(options =>
//{

//    // SC-04: gRPC port 7038 = HTTPS locally (dev certificate), HTTP/2 inside the container; REST port 5006 = HTTP (HTTPS via the Ingress)
//    global::_4EPlatform_API_Template.Extention.HttpsSettings.ListenGrpcAndRest(options, builder.Configuration, grpcPort: 7098, restPort: 5006);


//});


builder.Services.AddUserManagementGrpcClients(builder.Configuration["GrpcServiceSettings:UserManagementUrl"] ?? "UserManagementUrl Not Found");

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Notifications APIs Reference"
    });

    c.AddSecurityDefinition("token", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer",
        In = ParameterLocation.Header,
        Name = HeaderNames.Authorization
    });

    c.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("token", document)] = []
        });
});


builder.Services.AddSwaggerGen();
builder.Services.DatatBaseContext(builder, builder.Configuration?.GetValue<bool>("IsDevelopment") ?? false);
builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            // the name of your api resources   
            options.Audience = "UserManagementServer";
            /// identity server url                    
            options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
            options.RequireHttpsMetadata = false;
        });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ResourceOwnerPolicy", policy =>
    {
        policy.AddAuthenticationSchemes("Bearer");
        policy.RequireClaim("scope", "4EMicroServices"); // Require scope associated with Resource Owner Password token
    });

    options.AddPolicy("ClientCredentialsPolicy", policy =>
    {
        policy.AddAuthenticationSchemes("Bearer");
        policy.RequireClaim("scope", "CPAggregatorAPI"); // Require scope associated with Client Credentials token
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();
app.UseCors("AllowCors");
app.MapGrpcService<notificationServices>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();