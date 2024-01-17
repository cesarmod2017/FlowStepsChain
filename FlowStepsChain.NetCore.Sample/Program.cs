using FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne;
using FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepOne;
using FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepTwo;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<StepOneHandler, StepOneHandler>();
builder.Services.AddTransient<StepTwoHandler, StepTwoHandler>();
builder.Services.AddTransient<IModuleOneHandlerServices, ModuleOneHandlerServices>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
