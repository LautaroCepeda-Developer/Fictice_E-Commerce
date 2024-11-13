using Swashbuckle.AspNetCore.Swagger;
using Automappers;
using DTOs.BannedUser;
using DTOs.Category;
using DTOs.CompanyData;
using DTOs.Payment;
using DTOs.PaymentMethod;
using DTOs.Product;
using DTOs.Sale;
using DTOs.ProductSale;
using DTOs.User;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository;
using Repository.Interfaces;
using Services.Ban;
using Services.Category;
using Services.CompanyData;
using Services.Interfaces;
using Services.Payment;
using Services.Product;
using Services.Sale;
using Services.User;
using Validators.BannedUser;
using Validators.Category;
using Validators.CompanyData;
using Validators.Order;
using Validators.Payment;
using Validators.PaymentMethod;
using Validators.Product;
using Validators.ProductOrder;
using Validators.User;
using Services.ProductSale;
using Services.PaymentMethod;

var builder = WebApplication.CreateBuilder(args);

#region Services
builder.Services.AddKeyedScoped<IUserService, UserService>("userService");
builder.Services.AddKeyedScoped<ICommonPublicService<UserDTO, UserGetPublicDTO>, PublicUserService>("publicUserService");
builder.Services.AddKeyedScoped<ICommonService<UserBankAccountDTO, UserBankAccountInsertDTO, UserBankAccountUpdateDTO>, UserBankAccountService>("userBankAccountService");
builder.Services.AddKeyedScoped<ICommonService<PaymentMethodDTO, PaymentMethodInsertDTO, PaymentMethodUpdateDTO>, PaymentMethodService>("paymentMethodService");
builder.Services.AddScoped<IUserPersonalInformationService, UserPersonalInformationService>();

builder.Services.AddScoped<IInfractionService, InfractionService>();
builder.Services.AddKeyedScoped<IBannedUserService, BannedUserService>("bannedUserService");
builder.Services.AddKeyedScoped<IBannedBankAccountService, BannedBankAccountService>("bannedBankAccountService");

builder.Services.AddScoped<ICommonService<CategoryDTO, CategoryInsertDTO, CategoryUpdateDTO>, CategoryService>();
builder.Services.AddScoped<ICommonService<ProductDTO, ProductInsertDTO, ProductUpdateDTO>, ProductService>();

builder.Services.AddScoped<ICompanyDataService, CompanyDataService>();

builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IProductSaleService, ProductSaleService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddTransient<IOrderNumberService, OrderNumberService>();
builder.Services.AddTransient<ITransactionNumberService, TransactionNumberService>();
#endregion

#region Repositories
// CRUD repositories
builder.Services.AddScoped<IRepository<UserBankAccount>, UserBankAccountRepository>();
builder.Services.AddScoped<IRepository<BannedUser>, BannedUserRepository>();
builder.Services.AddScoped<IRepository<BannedBankAccount>, BannedBankAccountRepository>();
builder.Services.AddScoped<IRepository<Category>,CategoryRepository>();
builder.Services.AddScoped<IRepository<PaymentMethod>, PaymentMethodRepository>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();

// Custom repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserPersonalInformationRepository, UserPersonalInformationRepository>();
builder.Services.AddScoped<ICompanyDataRepository, CompanyDataRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IProductSaleRepository, ProductSaleRepository>();
#endregion

#region Database
// Database connection string (getted from user secrets)
var connectionString = builder.Configuration["Database:ConnectionString"];

// Database context injection
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(connectionString);
});
#endregion

#region Validators
// User
builder.Services.AddScoped<IValidator<UserInsertDTO>, UserInsertValidator>();
builder.Services.AddScoped<IValidator<UserUpdateDTO>, UserUpdateValidator>();
builder.Services.AddScoped<IValidator<UserPersonalInformationUpdateDTO>, UserPersonalInformationUpdateValidator>();
builder.Services.AddScoped<IValidator<UserBankAccountInsertDTO>, UserBankAccountInsertValidator>();
builder.Services.AddScoped<IValidator<UserBankAccountUpdateDTO>, UserBankAccountUpdateValidator>();

// Banned User
builder.Services.AddScoped<IValidator<BannedUserInsertDTO>, BannedUserInsertValidator>();
builder.Services.AddScoped<IValidator<BannedUserUpdateDTO>, BannedUserUpdateValidator>();

// Banned Bank Account
builder.Services.AddScoped<IValidator<BannedBankAccountInsertDTO>, BannedBankAccountInsertValidator>();
builder.Services.AddScoped<IValidator<BannedBankAccountUpdateDTO>, BannedBankAccountUpdateValidator>();

// Category
builder.Services.AddScoped<IValidator<CategoryInsertDTO>, CategoryInsertValidator>();
builder.Services.AddScoped<IValidator<CategoryUpdateDTO>, CategoryUpdateValidator>();

// Company Data
builder.Services.AddScoped<IValidator<CompanyDataInsertDTO>, CompanyDataInsertValidator>();
builder.Services.AddScoped<IValidator<CompanyDataUpdateDTO>, CompanyDataUpdateValidator>();

// Order
builder.Services.AddScoped<IValidator<SaleInsertDTO>, SaleInsertValidator>();
builder.Services.AddScoped<IValidator<PaymentInsertDTO>, PaymentInsertValidator>();

// Payment
builder.Services.AddScoped<IValidator<PaymentMethodInsertDTO>, PaymentMethodInsertValidator>();
builder.Services.AddScoped<IValidator<PaymentMethodUpdateDTO>, PaymentMethodUpdateValidator>();

// Product
builder.Services.AddScoped<IValidator<ProductInsertDTO>, ProductInsertValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateDTO>, ProductUpdateValidator>();

// Product Order
builder.Services.AddScoped<IValidator<ProductSaleInsertDTO>, ProductSaleInsertValidator>();
#endregion

#region Mappers
builder.Services.AddAutoMapper(typeof(UserMappingProfile));
builder.Services.AddAutoMapper(typeof(BannedUserMappingProfile));
builder.Services.AddAutoMapper(typeof(CategoryMappingProfile));
builder.Services.AddAutoMapper(typeof(CompanyDataMappingProfile));
builder.Services.AddAutoMapper(typeof(PaymentMappingProfile));
builder.Services.AddAutoMapper(typeof(PaymentMethodMappingProfile));
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));
builder.Services.AddAutoMapper(typeof(ProductSaleMappingProfile));
builder.Services.AddAutoMapper(typeof(SaleMappingProfile));
#endregion


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
