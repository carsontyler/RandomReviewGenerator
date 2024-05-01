using RandomReviewSite.Options;

    var builder = WebApplication.CreateBuilder(args);

// Add options
var options = builder.Services.AddOptions<ApplicationOptions>().BindConfiguration(nameof(ApplicationOptions));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Does this need to be removed for Docker?
//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
