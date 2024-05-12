using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AutomationUsingMSTest
{
    public class TestDataRepository
    {
        public List<TestData> GetLoginTestData(string filePath)
        {
            List<TestData> testDataList = new List<TestData>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                testDataList = csv.GetRecords<TestData>().ToList();
            }

            return testDataList;
        }

        public List<CheckoutTestData> GetCheckoutTestData(string filePath)
        {
            List<CheckoutTestData> checkoutDataList = new List<CheckoutTestData>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                checkoutDataList = csv.GetRecords<CheckoutTestData>().ToList();
            }

            return checkoutDataList;
        }
    }

    public class TestData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CheckoutTestData
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string zipCode { get; set; }
    }
}
