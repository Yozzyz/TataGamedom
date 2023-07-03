using TataGamedom.Models.Dtos.InventoryItems;
using TataGamedom.Models.Dtos.Orders;
using TataGamedom.Models.Dtos.StockInSheets;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Services;

namespace TestProject
{
	public class TestGetIndex
	{
		[Test]
		public void 目前OrderIndexMax為300()
		{
			var dto = new OrderDto { CreatedAt = new DateTime(2023,06,30),ShipmemtMethodId = 1 , MemberId = 1 , Id = 1 };
			var orderIndexGenerator = new IndexGenerator(300);

			string expectedIndex = "2023063011301";
			string actualIndex = orderIndexGenerator.GetOrderIndex(dto);

			Assert.That(actualIndex, Is.EqualTo(expectedIndex));
		}


		//[Test]
		//public void 如果Id未被傳入() //會被導入0
		//{
		//	throw new NotImplementedException();
		//	//var dto = new OrderDto { CreatedAt = new DateTime(2023, 06, 30), ShipmemtMethodId = 1, MemberId = 1 };
		//	//var orderIndexGenerator = new OrderIndexGenerator();

		//	//string expectedIndex = "2023063010";
		//	//string actualIndex = orderIndexGenerator.GetIndex(dto);

		//	//Assert.AreEqual(expectedIndex, actualIndex);
		//}

		[Test]
		public void 目前InventoryItemId最大值為701_ProductId為1_Index為PC001()
		{
			var dto = new InventoryItemCreateDto { ProductId = 1, StockInSheetIndex = "進貨單編號50" };
			var orderIndexGenerator = new IndexGenerator(701);

			string expectedIndex = "PC001進貨單編號50702";
			string actualIndex = orderIndexGenerator.GetSKU(dto, "PC001");

			Assert.That(actualIndex, Is.EqualTo(expectedIndex));
		}

		[Test]
		public void 目前StockInSheetId最大值為50_SupplierId為10()
		{
			var dto = new StockInSheetDto { OrderRequestDate = new DateTime(2023,06,30),SupplierId = 10};
			var StockInSheetIndexGenerator = new IndexGenerator(50);

			string expectedIndex = "202306301051";
			string actualIndex = StockInSheetIndexGenerator.GetStockInSheetIndex(dto);

			Assert.That(actualIndex, Is.EqualTo(expectedIndex));
		}

	}
}