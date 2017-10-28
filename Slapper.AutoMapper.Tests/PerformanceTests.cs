using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace Slapper.Tests
{
    [TestFixture]
    [Explicit]
    public class PerformanceTests : TestBase
    {
        public class Customer
        {
            public int CustomerId;
            public string FirstName;
            public string LastName;
            public IList<Order> Orders;

            public string Parameter1;
            public string Parameter2;
            public string Parameter3;
            public string Parameter4;
            public string Parameter5;
            public string Parameter6;
            public string Parameter7;
            public string Parameter8;
            public string Parameter9;
            public decimal DecimalParameter1;
            public decimal DecimalParameter2;
            public decimal DecimalParameter3;
            public decimal DecimalParameter4;
            public decimal DecimalParameter5;
            public decimal DecimalParameter6;
            public decimal DecimalParameter7;
            public decimal DecimalParameter8;
            public decimal DecimalParameter9;
        }

        public class Order
        {
            public int OrderId;
            public decimal OrderTotal;
            public IList<OrderDetail> OrderDetails;

            public string Parameter1;
            public string Parameter2;
            public string Parameter3;
            public string Parameter4;
            public string Parameter5;
            public string Parameter6;
            public string Parameter7;
            public string Parameter8;
            public string Parameter9;
            public decimal DecimalParameter1;
            public decimal DecimalParameter2;
            public decimal DecimalParameter3;
            public decimal DecimalParameter4;
            public decimal DecimalParameter5;
            public decimal DecimalParameter6;
            public decimal DecimalParameter7;
            public decimal DecimalParameter8;
            public decimal DecimalParameter9;
        }

        public class OrderDetail
        {
            public int OrderDetailId;
            public decimal OrderDetailTotal;

            public string Parameter1;
            public string Parameter2;
            public string Parameter3;
            public string Parameter4;
            public string Parameter5;
            public string Parameter6;
            public string Parameter7;
            public string Parameter8;
            public string Parameter9;
            public decimal DecimalParameter1;
            public decimal DecimalParameter2;
            public decimal DecimalParameter3;
            public decimal DecimalParameter4;
            public decimal DecimalParameter5;
            public decimal DecimalParameter6;
            public decimal DecimalParameter7;
            public decimal DecimalParameter8;
            public decimal DecimalParameter9;
        }

        /// <summary>
        /// Simple performance test mapping 50,000 objects.
        /// </summary>
        /// <remarks>
        /// Historical Test Results
        ///     v1.0.0.1: Mapped 50000 objects in 1755 ms.
        ///     v1.0.0.2: Mapped 50000 objects in 1918 ms.
        ///     v1.0.0.3: Mapped 50000 objects in 1819 ms.
        ///     v1.0.0.4: Mapped 50000 objects in 1683 ms.
        ///     v1.0.0.4: Mapped 50000 objects in 1683 ms.
        ///     v1.0.0.5: Mapped 50000 objects in 1877 ms.
        ///     v1.0.0.6: Mapped 50000 objects in 1642 ms.
        /// </remarks>
        [Test]
        public void Simple_Performance_Test()
        {
            // Arrange
            const int iterations = 50000;

            var list = new List<Dictionary<string, object>>();

            for (int i = 0; i < iterations; i++)
            {
                var dictionary = new Dictionary<string, object>
                {
                    { "CustomerId", i },
                    { "FirstName", "Bob" },
                    { "LastName", "Smith" }
                };

                list.Add(dictionary);
            }

            // Act
            Stopwatch stopwatch = Stopwatch.StartNew();
            var customers = AutoMapper.Map<Customer>(list);
            stopwatch.Stop();

            // Assert
            Assert.NotNull(customers);
            Assert.That(customers.Count() == iterations);

            Trace.WriteLine(string.Format("Mapped {0} objects in {1} ms.", iterations, stopwatch.ElapsedMilliseconds));
        }

        /// <summary>
        /// Complex performance test mapping 50,000 objects with with nested child objects.
        /// </summary>
        /// <remarks>
        /// Historical Test Results
        ///     v1.0.0.1: Mapped 50000 objects in 5913 ms.
        ///     v1.0.0.2: Mapped 50000 objects in 5911 ms.
        ///     v1.0.0.3: Mapped 50000 objects in 5327 ms.
        ///     v1.0.0.4: Mapped 50000 objects in 5349 ms.
        ///     v1.0.0.4: Mapped 50000 objects in 5349 ms.
        ///     v1.0.0.5: Mapped 50000 objects in 5896 ms.
        ///     v1.0.0.6: Mapped 50000 objects in 5539 ms.
        ///     v1.0.0.8: Mapped 50000 objects in 4185 ms.
        /// </remarks>
        [Test]
        public void Complex_Performance_Test()
        {
            // Arrange
            const int iterations = 50000;

            var list = new List<Dictionary<string, object>>();

            for (int i = 0; i < iterations; i++)
            {
                var dictionary = new Dictionary<string, object>
                {
                    { "CustomerId", i },
                    { "FirstName", "Bob" },
                    { "LastName", "Smith" },
                    { "Orders_OrderId", i },
                    { "Orders_OrderTotal", 50.50m },
                    { "Orders_OrderDetails_OrderDetailId", i },
                    { "Orders_OrderDetails_OrderDetailTotal", 50.50m },
                    { "Orders_OrderDetails_Product_Id", 546 },
                    { "Orders_OrderDetails_Product_ProductName", "Black Bookshelf" }
                };

                list.Add(dictionary);
            }

            // Act
            Stopwatch stopwatch = Stopwatch.StartNew();
            var customers = AutoMapper.Map<Customer>(list);
            stopwatch.Stop();

            // Assert
            Assert.NotNull(customers);
            Assert.That(customers.Count() == iterations);

            Trace.WriteLine(string.Format("Mapped {0} objects in {1} ms.", iterations, stopwatch.ElapsedMilliseconds));
        }

        [Test]
        /// <summary>
        /// Complex performance test mapping 50,000 objects with with nested child objects.
        /// </summary>
        /// <remarks>
        /// Historical Test Results
        ///     v1.0.0.9: Mapped 50000 objects in cca 18 sec
        ///     After disabling custom attributes: cca 12 sec
        ///     
        /// </remarks>
        public void Complex_Performance_Test_More_Data()
        {
            // Arrange
            AutoMapper.Configuration.DisableCustomAttributes = true;
            const int iterations = 50000;

            var list = new List<Dictionary<string, object>>();

            for (int i = 0; i < iterations; i++)
            {
                var dictionary = new Dictionary<string, object>
                {
                    { "CustomerId", i },
                    { "FirstName", "Bob" },
                    { "LastName", "Smith" },
                    { "Orders_OrderId", i },
                    { "Parameter1", "TestParam" },
                    { "Parameter2", "TestParam" },
                    { "Parameter3", "TestParam" },
                    { "Parameter4", "TestParam" },
                    { "Parameter5", "TestParam" },
                    { "Parameter6", "TestParam" },
                    { "Parameter7", "TestParam" },
                    { "Parameter8", "TestParam" },
                    { "Parameter9", "TestParam" },
                    { "DecimalParameter1", 123.4567M },
                    { "DecimalParameter2", 123.4567M },
                    { "DecimalParameter3", 123.4567M },
                    { "DecimalParameter4", 123.4567M },
                    { "DecimalParameter5", 123.4567M },
                    { "DecimalParameter6", 123.4567M },
                    { "DecimalParameter7", 123.4567M },
                    { "DecimalParameter8", 123.4567M },
                    { "DecimalParameter9", 123.4567M },
                    { "DecimalParameter10", 123.4567M },
                    { "Orders_OrderTotal", 50.50m },
                    { "Orders_OrderDetails_OrderDetailId", i },
                    { "Orders_OrderDetails_OrderDetailTotal", 50.50m },
                    { "Orders_OrderDetails_Product_Id", 546 },
                    { "Orders_OrderDetails_Product_ProductName", "Black Bookshelf" },
                    { "Orders_OrderDetails_Parameter1", "TestParam" },
                    { "Orders_OrderDetails_Parameter2", "TestParam" },
                    { "Orders_OrderDetails_Parameter3", "TestParam" },
                    { "Orders_OrderDetails_Parameter4", "TestParam" },
                    { "Orders_OrderDetails_Parameter5", "TestParam" },
                    { "Orders_OrderDetails_Parameter6", "TestParam" },
                    { "Orders_OrderDetails_Parameter7", "TestParam" },
                    { "Orders_OrderDetails_Parameter8", "TestParam" },
                    { "Orders_OrderDetails_Parameter9", "TestParam" },
                    { "Orders_OrderDetails_DecimalParameter1", 123.4567M },
                    { "Orders_OrderDetails_DecimalParameter2", 123.4567M },
                    { "Orders_OrderDetails_DecimalParameter3", 123.4567M },
                    { "Orders_OrderDetails_DecimalParameter4", 123.4567M },
                    { "Orders_OrderDetails_DecimalParameter5", 123.4567M },
                    { "Orders_OrderDetails_DecimalParameter6", 123.4567M },
                    { "Orders_OrderDetails_DecimalParameter7", 123.4567M },
                    { "Orders_OrderDetails_DecimalParameter8", 123.4567M },
                    { "Orders_OrderDetails_DecimalParameter9", 123.4567M },
                    { "Orders_OrderDetails_DecimalParameter10", 123.4567M },
                    { "Orders_Parameter1", "TestParam" },
                    { "Orders_Parameter2", "TestParam" },
                    { "Orders_Parameter3", "TestParam" },
                    { "Orders_Parameter4", "TestParam" },
                    { "Orders_Parameter5", "TestParam" },
                    { "Orders_Parameter6", "TestParam" },
                    { "Orders_Parameter7", "TestParam" },
                    { "Orders_Parameter8", "TestParam" },
                    { "Orders_Parameter9", "TestParam" },
                    { "Orders_DecimalParameter1", 123.4567M },
                    { "Orders_DecimalParameter2", 123.4567M },
                    { "Orders_DecimalParameter3", 123.4567M },
                    { "Orders_DecimalParameter4", 123.4567M },
                    { "Orders_DecimalParameter5", 123.4567M },
                    { "Orders_DecimalParameter6", 123.4567M },
                    { "Orders_DecimalParameter7", 123.4567M },
                    { "Orders_DecimalParameter8", 123.4567M },
                    { "Orders_DecimalParameter9", 123.4567M },
                    { "Orders_DecimalParameter10", 123.4567M }
                };

                list.Add(dictionary);
            }

            // Act
            Stopwatch stopwatch = Stopwatch.StartNew();
            var customers = AutoMapper.Map<Customer>(list);
            stopwatch.Stop();

            // Assert
            Assert.NotNull(customers);
            Assert.That(customers.Count() == iterations);

            Trace.WriteLine(string.Format("Mapped {0} objects in {1} ms.", iterations, stopwatch.ElapsedMilliseconds));
        }
    }
}