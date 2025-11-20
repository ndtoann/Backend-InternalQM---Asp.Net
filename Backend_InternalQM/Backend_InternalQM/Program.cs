using Backend_InternalQM.Entities;
using Backend_InternalQM.Models;
using Backend_InternalQM.Modules.EmployeeModule;
using Backend_InternalQM.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// connect db
var connnStr = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connnStr, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

// config jwt
var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(new { status = false, message = "Phiên đăng nhập không hợp lệ" });
        },
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(new { status = false, message = "Yêu cầu đăng nhập, thông tin đăng nhập không hợp lệ" });
        }
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Quang Minh",
        Version = "v1",
        Description = "Api dữ liệu quản lý nội bộ Quang Minh"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập JWT Token. Ví dụ: Bearer [Token]"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthorization(options =>
{
    //thông báo
    options.AddPolicy("EditNotifi", policy =>
        policy.RequireClaim("Permission", "Notification.Edit"));

    options.AddPolicy("DeleteNotifi", policy =>
        policy.RequireClaim("Permission", "Notification.Delete"));

    //tài khoản
    options.AddPolicy("ViewAccount", policy =>
        policy.RequireClaim("Permission", "Account.View"));

    options.AddPolicy("AddAccount", policy =>
        policy.RequireClaim("Permission", "Account.Add"));

    options.AddPolicy("EditAccount", policy =>
        policy.RequireClaim("Permission", "Account.Edit"));

    options.AddPolicy("DeleteAccount", policy =>
        policy.RequireClaim("Permission", "Account.Delet"));

    //các quyền nhân viên
    options.AddPolicy("ViewPermission", policy =>
       policy.RequireClaim("Permission", "Permission.View"));

    options.AddPolicy("AddPermission", policy =>
       policy.RequireClaim("Permission", "Permission.Add"));

    options.AddPolicy("EditPermission", policy =>
       policy.RequireClaim("Permission", "Permission.Edit"));

    options.AddPolicy("DeletePermission", policy =>
       policy.RequireClaim("Permission", "Permission.Delete"));

    options.AddPolicy("EmplPermission", policy =>
       policy.RequireClaim("Permission", "EmplPermission.Update"));

    //bộ phận
    options.AddPolicy("ViewDepartment", policy =>
       policy.RequireClaim("Permission", "Department.View"));

    options.AddPolicy("AddDepartment", policy =>
       policy.RequireClaim("Permission", "Department.Add"));

    options.AddPolicy("EditDepartment", policy =>
       policy.RequireClaim("Permission", "Department.Edit"));

    options.AddPolicy("DeleteDepartment", policy =>
       policy.RequireClaim("Permission", "Department.Delete"));

    //nhân viên
    options.AddPolicy("ViewEmployee", policy =>
        policy.RequireClaim("Permission", "ManageEmployee.View"));

    options.AddPolicy("AddEmployee", policy =>
        policy.RequireClaim("Permission", "ManageEmployee.Add"));

    options.AddPolicy("EditEmployee", policy =>
        policy.RequireClaim("Permission", "ManageEmployee.Edit"));

    options.AddPolicy("DeleteEmployee", policy =>
       policy.RequireClaim("Permission", "ManageEmployee.Delet"));

    options.AddPolicy("DataEmpl", policy =>
       policy.RequireClaim("Permission", "DataEmpl.Update"));

    options.AddPolicy("WorkHistoryEmpl", policy =>
       policy.RequireClaim("Permission", "WorkHistoryEmpl.Update"));

    options.AddPolicy("EmplViolation5S", policy =>
       policy.RequireClaim("Permission", "EmplViolation5S.Update"));

    //bài kiểm tra
    options.AddPolicy("ViewExam", policy =>
       policy.RequireClaim("Permission", "Exam.View"));

    options.AddPolicy("AddExam", policy =>
       policy.RequireClaim("Permission", "Exam.Add"));

    options.AddPolicy("EditExam", policy =>
       policy.RequireClaim("Permission", "Exam.Edit"));

    options.AddPolicy("DeleteExam", policy =>
       policy.RequireClaim("Permission", "Exam.Delete"));

    options.AddPolicy("UpdateQuestion", policy =>
       policy.RequireClaim("Permission", "Question.Update"));


    //kết quả kiểm tra
    options.AddPolicy("ViewResultExam", policy =>
       policy.RequireClaim("Permission", "ResultExam.View"));

    options.AddPolicy("EditResultExam", policy =>
       policy.RequireClaim("Permission", "ResultExam.Edit"));

    options.AddPolicy("DeleteResultExam", policy =>
       policy.RequireClaim("Permission", "ResultExam.Delete"));

    //thống kê lỗi sx
    options.AddPolicy("ViewProductionDefect", policy =>
       policy.RequireClaim("Permission", "ProductionDefect.View"));

    options.AddPolicy("AddProductionDefect", policy =>
       policy.RequireClaim("Permission", "ProductionDefect.Add"));

    options.AddPolicy("EditProductionDefect", policy =>
       policy.RequireClaim("Permission", "ProductionDefect.Edit"));

    options.AddPolicy("DeleteProductionDefect", policy =>
       policy.RequireClaim("Permission", "ProductionDefect.Delete"));

    //kaizen
    options.AddPolicy("ViewKaizen", policy =>
       policy.RequireClaim("Permission", "ManageKaizen.View"));

    options.AddPolicy("AddKaizen", policy =>
       policy.RequireClaim("Permission", "ManageKaizen.Add"));

    options.AddPolicy("EditKaizen", policy =>
       policy.RequireClaim("Permission", "ManageKaizen.Edit"));

    options.AddPolicy("DeleteKaizen", policy =>
       policy.RequireClaim("Permission", "ManageKaizen.Delete"));

    //năng suất
    options.AddPolicy("ViewProductivity", policy =>
       policy.RequireClaim("Permission", "ManageProductivity.View"));

    options.AddPolicy("AddProductivity", policy =>
       policy.RequireClaim("Permission", "ManageProductivity.Add"));

    options.AddPolicy("EditProductivity", policy =>
       policy.RequireClaim("Permission", "ManageProductivity.Edit"));

    options.AddPolicy("DeleteProductivity", policy =>
       policy.RequireClaim("Permission", "ManageProductivity.Delete"));

    //lỗi 5S
    options.AddPolicy("ViewViolation5S", policy =>
       policy.RequireClaim("Permission", "ManageViolation5S.View"));

    options.AddPolicy("AddViolation5S", policy =>
       policy.RequireClaim("Permission", "ManageViolation5S.Add"));

    options.AddPolicy("EditViolation5S", policy =>
       policy.RequireClaim("Permission", "ManageViolation5S.Edit"));

    options.AddPolicy("DeleteViolation5S", policy =>
       policy.RequireClaim("Permission", "ManageViolation5S.Delete"));

    //quản lý máy
    options.AddPolicy("ViewMachine", policy =>
       policy.RequireClaim("Permission", "ManageMachine.View"));

    options.AddPolicy("AddMachine", policy =>
       policy.RequireClaim("Permission", "ManageMachine.Add"));

    options.AddPolicy("EditMachine", policy =>
       policy.RequireClaim("Permission", "ManageMachine.Edit"));

    options.AddPolicy("DeleteMachine", policy =>
       policy.RequireClaim("Permission", "ManageMachine.Delete"));

    //nhóm máy
    options.AddPolicy("ViewMachineGroup", policy =>
       policy.RequireClaim("Permission", "ManageGroupMachine.View"));

    options.AddPolicy("AddMachineGroup", policy =>
       policy.RequireClaim("Permission", "ManageGroupMachine.Add"));

    options.AddPolicy("EditMachineGroup", policy =>
       policy.RequireClaim("Permission", "ManageGroupMachine.Edit"));

    options.AddPolicy("DeleteMachineGroup", policy =>
       policy.RequireClaim("Permission", "ManageGroupMachine.Delete"));

    options.AddPolicy("UpdateGroupMachine", policy =>
       policy.RequireClaim("Permission", "ManageGroupMachine.AddMachine"));

    //sữa chữa thiết bị máy móc
    options.AddPolicy("ViewMachineRepair", policy =>
       policy.RequireClaim("Permission", "ManageRepairMachine.View"));

    options.AddPolicy("AddMachineRepair", policy =>
       policy.RequireClaim("Permission", "ManageRepairMachine.Add"));

    options.AddPolicy("EditMachineRepair", policy =>
       policy.RequireClaim("Permission", "ManageRepairMachine.Edit"));

    options.AddPolicy("DeleteMachineRepair", policy =>
       policy.RequireClaim("Permission", "ManageRepairMachine.Delete"));

    //bảo dưỡng thiết bị máy móc
    options.AddPolicy("ViewMachineMaintenance", policy =>
       policy.RequireClaim("Permission", "ManageMaintenance.View"));

    options.AddPolicy("AddMachineMaintenance", policy =>
       policy.RequireClaim("Permission", "ManageMaintenance.Add"));

    options.AddPolicy("EditMachineMaintenance", policy =>
       policy.RequireClaim("Permission", "ManageMaintenance.Edit"));

    options.AddPolicy("DeleteMachineMaintenance", policy =>
       policy.RequireClaim("Permission", "ManageMaintenance.Delete"));

    //lương nhân viên
    options.AddPolicy("ViewSalary", policy =>
       policy.RequireClaim("Permission", "ManageSalary.View"));

    options.AddPolicy("AddSalary", policy =>
       policy.RequireClaim("Permission", "ManageSalary.Add"));

    options.AddPolicy("EditSalary", policy =>
       policy.RequireClaim("Permission", "ManageSalary.Edit"));

    options.AddPolicy("DeleteSalary", policy =>
       policy.RequireClaim("Permission", "ManageSalary.Delete"));

    options.AddPolicy("ViewMonthlyPayroll", policy =>
       policy.RequireClaim("Permission", "ManageMonthlySalary.View"));

    options.AddPolicy("UpdateMonthlyPayroll", policy =>
       policy.RequireClaim("Permission", "ManageMonthlySalary.Update"));

    //client
    options.AddPolicy("ClientViewEmpl", policy =>
       policy.RequireClaim("Permission", "EmployeeOnlyDepartment.View"));

    options.AddPolicy("ClientAddFeedbackEmpl", policy =>
       policy.RequireClaim("Permission", "Employee.AddFeedbackEmpl"));

    options.AddPolicy("ClientViewMachine", policy =>
       policy.RequireClaim("Permission", "Machine.View"));

    options.AddPolicy("ClientViewMachineGroup", policy =>
       policy.RequireClaim("Permission", "MachineGroup.View"));

    options.AddPolicy("ClientViewRepair", policy =>
       policy.RequireClaim("Permission", "MachineRepair.View"));

    options.AddPolicy("ClientViewMaintenance", policy =>
       policy.RequireClaim("Permission", "MachineMaintenance.View"));

    options.AddPolicy("ClientViewProductivity", policy =>
       policy.RequireClaim("Permission", "Productivity.View"));

    options.AddPolicy("ClientViewViolation5S", policy =>
       policy.RequireClaim("Permission", "EmplViolation5S.View"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
