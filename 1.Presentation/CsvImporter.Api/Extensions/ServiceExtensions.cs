using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace CsvImporter.Api.Extensions
{
	public static class ServiceExtensions
	{
		public static void AddSwaggerExtension(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.IncludeXmlComments(string.Format(@"{0}\CsvImporter.Api.xml", System.AppDomain.CurrentDomain.BaseDirectory));
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "CsvImporter - API",
					Description = "La API será responsable de la importación de los datos.",
					Contact = new OpenApiContact
					{
						Name = "Acme Corporation",
						Email = "contact@acmecorporation.com",
						Url = new Uri("https://acmecorporation.com/contact"),
					},
					License = new OpenApiLicense
					{
						Name = "OpenSource",
						Url = new Uri("https://acmecorporation.com/license"),
					}
				});
			});
		}
		public static void AddApiVersioningExtension(this IServiceCollection services)
		{
			services.AddApiVersioning(config =>
			{
				config.DefaultApiVersion = new ApiVersion(1, 0);
				config.AssumeDefaultVersionWhenUnspecified = true;
				config.ReportApiVersions = true;
			});
		}
	}
}
