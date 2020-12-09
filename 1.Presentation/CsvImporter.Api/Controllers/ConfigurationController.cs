using CsvImporter.Application.Definitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CsvImporter.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ApiVersion("1.0")]
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
