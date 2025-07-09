using GraphQL.Data;
using GraphQL.Data.Extensions.Book;
using GraphQL.Data.Extensions.Loan;
using GraphQL.Model.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "Allow",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    );
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContextFactory<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder
    .Services.AddGraphQLServer()
    .RegisterDbContextFactory<ApplicationDbContext>()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<BookQueries>()
    .AddTypeExtension<BookMutations>()
    .AddTypeExtension<LoanQueries>()
    .AddTypeExtension<LoanMutation>()
    .AddType<DateType>()
    .AddProjections()
    .AddSorting()
    .AddFiltering();

/* .AddProjections()
.AddFiltering()
.AddSorting();  */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("Allow");
app.UseHttpsRedirection();
app.MapGraphQL("/graphql");

app.Run();
