using DataAccessLayers;
using Repositories.IRepository;
using Repositories.Repository;

var builder = WebApplication.CreateBuilder(args);

//Database
//DAO
builder.Services.AddScoped<AccountDAO>();
builder.Services.AddScoped<CategoryDAO>();
builder.Services.AddScoped<EventDAO>();
builder.Services.AddScoped(typeof(GenericDAO<>));
builder.Services.AddScoped<PromotionDAO>();
builder.Services.AddScoped<RoleDAO>();
builder.Services.AddScoped<SolvedTicketDAO>();
builder.Services.AddScoped<TicketDAO>();
builder.Services.AddScoped<TransactionDAO>();
builder.Services.AddScoped<TransactionHistoryDAO>();
builder.Services.AddScoped<TransactionTypeDAO>();

//UnitOfWork
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Repository
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IEventCategory, EventCategory>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ISolvedTicketRepository, SolvedTicketRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionHistoryRepository, TransactionHIstoryRepository>();
builder.Services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
