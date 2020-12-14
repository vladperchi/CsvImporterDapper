using CsvImporter.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CsvImporter.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfigurationController : ControllerBase
	{
		private readonly IDataBaseStructureService _dataBaseStructureService;

		public ConfigurationController(IDataBaseStructureService dataBaseStructureService)
		{
			_dataBaseStructureService = dataBaseStructureService;
		}

		[HttpPost("CreateStockProductTable")]
		[AllowAnonymous]
		public async Task<int> CreateStockProductTableAsync()
		{
			var result = await _dataBaseStructureService.CreateStockProductTableAsync();
			return result;
		}
	}
}
