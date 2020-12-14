using CsvImporter.Application;
using CsvImporter.Application.Interfaces;
using CsvImporter.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CsvImporter.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StockProductController : ControllerBase
	{
		private readonly IBlobService _blobService;
		private readonly IStockProductService _stockProductService;

		public StockProductController(IBlobService blobService, IStockProductService stockProductService)
		{
			_blobService = blobService;
			_stockProductService = stockProductService;
		}
		[HttpPost("GetFileBlob")]
		[AllowAnonymous]
		public async Task<string> GetFileBlobAsync(BlobRequest blobRequest)
		{
			return await _blobService.GetFileFromBlobStorage(blobRequest);
		}

		[HttpPost("SaveStockData")]
		[AllowAnonymous]
		public async Task<int> SaveStockDataAsync([FromBody] string stringPath)
		{
			return await _stockProductService.SaveMassStockAsync(stringPath);
		}

		[HttpGet("CountStockData")]
		[AllowAnonymous]
		public async Task<int> CountStockDataAsync()
		{
			return await _stockProductService.CountRowsInStock();
		}

		[HttpPost("DeleteStockData")]
		[AllowAnonymous]
		public async Task<int> DeleteStockDataAsync()
		{
			return await _stockProductService.DeleteMassAsync();
		}
	}
}
