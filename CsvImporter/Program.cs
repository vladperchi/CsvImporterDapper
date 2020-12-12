using CsvImporter.Application.Rest;
using CsvImporter.Application.Rest.Implementations;
using CsvImporter.DataAccess.RestClient;
using CsvImporter.Domain;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CsvImporter
{
	class Program
	{
		private static IServiceProvider _serviceProvider;
		private static BlobRequest blobRequest = new BlobRequest()
		{
			Folder = Constants.FOLDER,
			FileName = Constants.FILENAME,
			StorageUri = Constants.STORAGE_URI
		};
		static async Task Main(string[] args)
		{
			RegisterServices();
			try
			{
				Console.WriteLine("Bienvenido a CSVImporter\r");
				Console.WriteLine("-----------------------------------------\r");
				Console.WriteLine("Presione una tecla para iniciar el proceso\n");
				Console.ReadKey();
				DateTime startProcess = DateTime.Now;
				Console.WriteLine($"Inicio de Proceso: {startProcess}\r");
				Console.WriteLine("La ejecución tardará unos minutos...\n");
				await Configuration();
				await ProccessInfo();
				DateTime endProcess = DateTime.Now;
				Console.WriteLine($"Proceso Finalizado: {endProcess}");
				Console.WriteLine($"Total Tiempo: {endProcess.Subtract(startProcess).TotalMinutes}");
				Console.WriteLine("Presione una tecla para finalizar el proceso y cerrar el programa");
				Console.ReadKey();
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message, ex);
				throw ex;
			}
			DisposeService();
		}

		private static async Task Configuration()
		{
			Console.WriteLine("Configurando Base de Datos...");
			var configurationService = _serviceProvider.GetService<IConfigurationServiceRest>();
			await configurationService.CreateStockProductTableAsync();
		}

		private static async Task ProccessInfo()
		{

			var stockProductService = _serviceProvider.GetService<IStockProductServiceRest>();

			Console.WriteLine($"Descargando el archivo......Proceso Inicado ==> {DateTime.Now}");
			var filePath = await stockProductService.GetFileBlobAsync(blobRequest);
			Console.WriteLine($"Archivo descargado correctamente......Proceso Finalizado ==> {DateTime.Now}");
			Console.WriteLine("Verificando si existe data...");
			var totalRows = await stockProductService.CountStockDataAsync();
			if (totalRows > 0)
			{
				Console.WriteLine($"Eliminando data anterior......Proceso Inicado ==> {DateTime.Now}");
				var totalRowsDeleted = await stockProductService.DeleteStockDataAsync();
				Console.WriteLine($"{totalRowsDeleted} Registros eliminados......Proceso Finalizado ==> {DateTime.Now}");
			}
			Console.WriteLine($"Guardando informacion en la base de datos......Proceso Inicado ==> {DateTime.Now}");
			var totalRowsSaved = await stockProductService.SaveStockDataAsync(filePath);
			if (totalRowsSaved > 0)
			{
				Console.WriteLine($"{totalRowsSaved} Registros guardados......Proceso Finalizado ==> {DateTime.Now}");
			}
			Console.WriteLine("Proceso terminado con exito!!!...");
		}

		private static void RegisterServices()
		{
			var container = new ServiceCollection();
			container.RegisterDataAccessRestClientDependencies();
			container.AddScoped<IConfigurationServiceRest, ConfigurationServiceRest>();
			container.AddScoped<IStockProductServiceRest, StockProductServiceRest>();
			_serviceProvider = container.BuildServiceProvider();
		}

		private static void DisposeService()
		{
			if (_serviceProvider == null)
			{
				return;
			}
			if (_serviceProvider is IDisposable)
			{
				((IDisposable)_serviceProvider).Dispose();
			}
		}
		private static async Task SaveFile(Stream file)
		{
			var fileFullPath = Constants.FILE_FULL_PATH;
			FileStream fileStream = new FileStream(fileFullPath, FileMode.Create);
			file.CopyTo(fileStream);
			fileStream.Close();
			await Task.Yield();
		}
	}
}
