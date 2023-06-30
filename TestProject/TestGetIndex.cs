using TataGamedom.Models.Dtos.Orders;
using TataGamedom.Models.Services;

namespace TestProject
{
	public class TestGetIndex
	{
		[Test]
		public void 目前OrderIndexMax為300()
		{
			var dto = new OrderDto { CreatedAt = new DateTime(2023,06,30),ShipmemtMethodId = 1 , MemberId = 1 , Id = 1 };
			var orderIndexGenerator = new OrderIndexGenerator(300);

			string expectedIndex = "2023063011301";
			string actualIndex = orderIndexGenerator.GetIndex(dto);

			Assert.That(actualIndex, Is.EqualTo(expectedIndex));
		}


		[Test]
		public void 如果Id未被傳入() //會被導入0
		{
			throw new NotImplementedException();
			//var dto = new OrderDto { CreatedAt = new DateTime(2023, 06, 30), ShipmemtMethodId = 1, MemberId = 1 };
			//var orderIndexGenerator = new OrderIndexGenerator();

			//string expectedIndex = "2023063010";
			//string actualIndex = orderIndexGenerator.GetIndex(dto);

			//Assert.AreEqual(expectedIndex, actualIndex);
		}

	}
}