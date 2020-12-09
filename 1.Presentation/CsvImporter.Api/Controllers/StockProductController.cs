using CsvImporter.Application.Definitions;
using CsvImporter.Domain.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace CsvImporter.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ApiVersion("1.0")]
	public class StockProductController : ControllerBase
	{
		private readonly IStockProductService _stockProductService;
		private readonly IBlobService _blobService;

		public StockProductController(IStockProductService stockProductService, IBlobService blobService)
		{
			_stockProductService = stockProductService;
			_blobService = blobService;
		}

		/// <summary>
		/// Descarga archivo Blob Storage
		/// </summary>
		[HttpPost("GetFileBlob")]
		[AllowAnonymous]
		public async Task<Stream> GetFileBlobAsync(BlobRequest blobRequest)
		{
			return await _blobService.GetFileFromBlobStorage(blobRequest);
		}

		/// <summary>
		/// Salva datos de forma masiva
		/// </summary> 
		[HttpPost("SaveStockData")]
		[AllowAnonymous]
		public async Task<int> SaveStockDataAsync([FromBody] string stringPath)
		{
			return await _stockProductService.SaveMassStockAsync(stringPath);
		}

		/// <summary>
		/// Conteo filas existentes
		/// </summary>
		[HttpGet("CountStockData")]
		[AllowAnonymous]
		public async Task<int> CountStockDataAsync()
		{
			return await _stockProductService.CountRowsInStock();
		}

		/// <summary>
		/// Elimina toda la data existente
		/// </summary>
		[HttpPost("DeleteStockData")]
		[AllowAnonymous]
		public async Task<int> DeleteStockDataAsync()
		{
			return await _stockProductService.DeleteMassAsync();
		}
	}
}
