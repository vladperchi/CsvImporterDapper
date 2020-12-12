using CsvImporter.DataAccess;
using System.Threading.Tasks;

namespace CsvImporter.Application.Implementation
{
	public class DataBaseStructureService : IDataBaseStructureService
	{
		private IDataBaseStructureRepository _dataBaseStructureRepository;

		public DataBaseStructureService(IDataBaseStructureRepository dataBaseStructureRepository)
		{
			_dataBaseStructureRepository = dataBaseStructureRepository;
		}

		public async Task<int> CreateStockProductTableAsync()
		{
			var result = await _dataBaseStructureRepository.CreateStockProductTableAsync();
			return result;

		}
	}
}
