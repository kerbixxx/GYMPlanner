using GymPlanner.Domain.Entities.Plan;
using GymPlanner.Infrastructure.Contexts;
using GymPlanner.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=gym;Trusted_Connection=True;Encrypt=False;");
});

//using (var context = new ApplicationDbContext()) //��� ����� ������ EFCore
//{
//    //var users = context.Users.ToList();

//    //�������� ����������
//    //Excersise excersise = new() { Name = "���������� 1" };
//    //Frequency frequency = new() { Name = "���� 1" };
//    //Plan plan = new() { Name = "���� 1" };
//    //PlanExcersiseFrequency frequencyExcersise = new() { Frequency = frequency, Plan = plan, Excersise = excersise, Description = "���������� ����������" };
//    //plan.planExcersiseFrequencies = new List<PlanExcersiseFrequency>();
//    //plan.planExcersiseFrequencies.Add(frequencyExcersise);
//    //plan.UserId = users[0].Id;
//    //context.Plans.Add(plan);

//    //�������� ����������
//    PlanRepository pr = new(context);
//    var plan = pr.Get(5);
//    var pef = plan.planExcersiseFrequencies.ToList();
//    pef[0].Excersise.Name = "���������� 100";
//    plan.planExcersiseFrequencies = pef;
//    context.Update(plan);
//    context.SaveChanges();
//}
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
