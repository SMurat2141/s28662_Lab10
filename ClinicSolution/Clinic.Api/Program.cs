using Clinic.Application.Commands.AddPrescription;
using Clinic.Application.Interfaces;
using Clinic.Infrastructure.Persistence;
using Clinic.Infrastructure.Repositories;
using Clinic.Api.Filters;
using Clinic.Api.Profiles;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClinicDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddPrescriptionCommand>());
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<AddPrescriptionCommand>();
builder.Services.AddControllers(opt => opt.Filters.Add<ValidationFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();