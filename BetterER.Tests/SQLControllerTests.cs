using BetterER.Controller;
using BetterER.Controller.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BetterER.Tests
{
    //TODO: Implement later

    [TestClass]
    public class SQLControllerTests
    {
        private ISQLController _sqlController;

        public SQLControllerTests()
        {
            _sqlController = new SQLController();
        }

        [Ignore]
        [TestMethod]
        public void TestCreateStatementBuilder()
        {
            //var basicEntity = new BasicEntity()
            //{
            //    Name = "Test"
            //};
            //basicEntity.Attributes.Add(new BasicAttribute()
            //{
            //    Name = "Column1"
            //});
            //basicEntity.Attributes.Add(new BasicAttribute()
            //{
            //    Name = "Column2"
            //});

            //var result = _sqlController.CreateTableScript(basicEntity);
            //var expectedString =
            //    "CREATE TABLE Test(" + Environment.NewLine +
            //    "Column1," + Environment.NewLine +
            //    "Column2" + Environment.NewLine +
            //    ");";
            //Assert.AreEqual(expectedString, result);
        }

        [Ignore]
        [TestMethod]
        public void TestCreateStatementBuilderWithFullAttributes()
        {
            //var basicEntity = new BasicEntity()
            //{
            //    Name = "Test"
            //};
            //basicEntity.Attributes.Add(new BasicAttribute()
            //{
            //    Name = "Column1",
            //    DataType = "varchar(50)",
            //    NotNull = true,
            //    IsPrimary = false,
            //    Default = "Col1"
            //});
            //basicEntity.Attributes.Add(new BasicAttribute()
            //{
            //    Name = "Column2",
            //    DataType = "varchar(50)",
            //    NotNull = false,
            //    IsPrimary = false,
            //    Default = "Col2"
            //});

            //var result = _sqlController.CreateTableScriptWithFullAttributes(basicEntity);
            //var expectedString =
            //    "CREATE TABLE Test(" + Environment.NewLine +
            //    "Column1 varchar(50) NOT NULL Col1," + Environment.NewLine +
            //    "Column2 varchar(50) DEFAULT Col2" + Environment.NewLine +
            //    ");";
            //Assert.AreEqual(expectedString, result);
        }

        [TestCleanup]
        public void CleanUpControllerTest()
        {
            _sqlController = null;
        }
    }
}
