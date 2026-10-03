using ShopexCoreV2;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages(options => { })
    .AddRazorPagesOptions(options => {
        options.Conventions.AddPageRoute("/Login", "");
    });

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(40000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.UseSession();
app.UseStaticFiles();
// Custom Exception handle 
app.UseExceptionHandler(new ExceptionHandlerOptions
{
    ExceptionHandler = new CustomExceptionHandler(app.Environment).Invoke
});
app.Run();
