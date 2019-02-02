using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class GameBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region delete
        [TestMethod]
        public void Backend_GameBackend_Delete_Valid_Data_Should_Pass()
        {
            //arrange
            var test = GameBackend.Instance;
            var data = new GameModel();
            var createResult = test.Create(data);
            var expect = true;

            //act
            var deleteResult = test.Delete(data.Id);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, deleteResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_Delete_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = GameBackend.Instance;
            var expect = false;

            //act
            var result = test.Delete(null);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region update
        [TestMethod]
        public void Backend_GameBackend_Update_Valid_Data_Should_Pass()
        {
            //arrange
            var expectRunDate = DateTime.Parse("01/23/2018");
            var expectEnabled = true;
            var expectIterationNumber = 123;

            var test = GameBackend.Instance;
            var data = new GameModel();

            test.Create(data);

            data.RunDate = expectRunDate;
            data.Enabled = expectEnabled;
            data.IterationNumber = expectIterationNumber;

            //act
            var updateResult = test.Update(data);
            var result = test.Read(data.Id);

            //reset
            test.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
            Assert.AreEqual(expectRunDate, result.RunDate, "Run Date "+ TestContext.TestName);
            Assert.AreEqual(expectEnabled, result.Enabled, "Enabled " + TestContext.TestName);
            Assert.AreEqual(expectIterationNumber, result.IterationNumber, "Iteration Number "+TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_Update_With_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = GameBackend.Instance;

            //act
            var result = test.Update(null);

            //reset
            test.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion update

        #region index
        [TestMethod]
        public void Backend_GameBackend_Index_Valid_Should_Pass()
        {
            //arrange
            var test = GameBackend.Instance;

            //act
            var result = test.Index();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion index

        #region read
        [TestMethod]
        public void Backend_GameBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = GameBackend.Instance;

            //act
            var result = test.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_Read_Valid_ID_Should_Pass()
        {
            //arrange
            var test = GameBackend.Instance;
            var testID = test.GetDefault();

            //act
            var result = test.Read(testID.Id);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region create
        [TestMethod]
        public void Backend_GameBackend_Create_Valid_Data_Should_Pass()
        {
            //arrange
            var test = GameBackend.Instance;
            var data = new GameModel();

            //act
            var result = test.Create(data);

            //reset
            test.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
            Assert.AreEqual(data.Id, result.Id, TestContext.TestName);
        }
        #endregion create

        #region SetDataSourceDataSet
        [TestMethod]
        public void Backend_GameBackend_SetDataSourceDataSet_Uses_MockData_Should_Pass()
        {
            //arrange
            var test = GameBackend.Instance;
            var testDataSourceBackend = DataSourceBackend.Instance;
            var mockEnum = DataSourceDataSetEnum.Demo;

            //act
            testDataSourceBackend.SetDataSourceDataSet(mockEnum);

            //reset
            test.Reset();

            //assert
        }

        [TestMethod]
        public void Backend_GameBackend_SetDataSourceDatSet_Uses_SQLData_Should_Pass()
        {
            //arange
            var test = GameBackend.Instance;
            var testDataSourceBackend = DataSourceBackend.Instance;
            var SQLEnum = DataSourceEnum.SQL;

            //act
            testDataSourceBackend.SetDataSource(SQLEnum);

            //reset
            test.Reset();

            //asset
        }
        #endregion SetDataSourceDataSet

        #region GetResults

        [TestMethod]
        public void Backend_GameBackend_GetResults_Valid_ID_Should_Pass()
        {
            //arrange
            var test = GameBackend.Instance;

            var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            var StudentData = DataSourceBackend.Instance.StudentBackend.Read(id);

            //act
            var result = test.GetResult(id);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_GetResults_MoreTransactionItems_Should_Pass()
        {
            //arrange
            var test = GameBackend.Instance;

            StudentModel studentModel = DataSourceBackend.Instance.StudentBackend.GetDefault();

            for (int i = 0; i < 6; i++)
            {
                TransactionModel transaction = new TransactionModel
                {
                    Name = "testing",
                    Uri = null
                };
                studentModel.Truck.TransactionList.Add(transaction);
            }

            DataSourceBackend.Instance.StudentBackend.Update(studentModel);
            var id = studentModel.Id;

            //act
            var result = test.GetResult(id);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_GetResults_No_Tokens_Should_Pass()
        {
            //arrange
            var test = GameBackend.Instance;

            StudentModel studentModel = DataSourceBackend.Instance.StudentBackend.GetDefault();
            studentModel.Tokens = 0;

            DataSourceBackend.Instance.StudentBackend.Update(studentModel);
            var id = studentModel.Id;

            //act
            var result = test.GetResult(id);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_GetResults_More_Tokens_Should_Pass()
        {
            //arrange
            var test = GameBackend.Instance;

            StudentModel studentModel = DataSourceBackend.Instance.StudentBackend.GetDefault();
            studentModel.Tokens = 5;

            DataSourceBackend.Instance.StudentBackend.Update(studentModel);
            var id = studentModel.Id;

            //act
            var result = test.GetResult(id);

            // Reset
            DataSourceBackend.Instance.StudentBackend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_GetResults_Default_Should_Pass()
        {
            //arrange
            var test = GameBackend.Instance;

            //act
            var result = test.GetResult("test");

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion GetResults

        #region Simulation
        [TestMethod]
        public void Backend_GameBackend_Simulation_Valid_ID_Should_Pass()
        {
            //arrange
            var test = GameBackend.Instance;
            var data = test.GetDefault();

            //act
            var result = test.Simulation();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_Simulation_RunDate_UTCNow_Should_Skip()
        {
            //arrange
            var test = GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow;

            //act
            var result = test.Simulation();

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_Simulation_RunDate_UTCNow_Add_1_Should_Skip()
        {
            //arrange
            var test = GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow.AddTicks(1);
            test.Update(data);

            var expect = test.GetDefault().IterationNumber;

            //act
            var result = test.Simulation();

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_Simulation_RunDate_UTCNow_Minus_1_Should_Skip()
        {
            //arrange
            var test = GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow.AddSeconds(-10); // Move it back 10 Seconds in time
            test.Update(data);

            var expect = test.GetDefault().IterationNumber;

            //act
            var result = test.Simulation();

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        #endregion Simulation

        #region RunIteration
        [TestMethod]
        public void Backend_GameBackend_RunIteration_Student_Is_Null_should_Pass()
        {
            // arrange
            var test = GameBackend.Instance;

            // act
            test.RunIteration();

            // assert
            
        }

        [TestMethod]
        public void Backend_GameBackend_RunIteration_Student_Is_Not_Null_should_Pass()
        {
            // arrange
            var test = GameBackend.Instance;
            var data1 = new StudentModel();
            var data2 = new StudentModel();
            var student1 = DataSourceBackend.Instance.StudentBackend.Create(data1);
            var student2 = DataSourceBackend.Instance.StudentBackend.Create(data2);

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // act
            test.RunIteration();
            
            // assert
        }
        #endregion RunIteration

        #region CalculateStudentIteration
        [TestMethod]
        public void Backend_GameBackend_CalculateStudentIteration_should_Pass()
        {
            // arrange
            var test = GameBackend.Instance;
            var data = new StudentModel();
            var student = DataSourceBackend.Instance.StudentBackend.Create(data);

            // act         
            student.AvatarLevel = 90;
            test.CalculateStudentIteration(student);          

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // assert         
        }

        [TestMethod]
        public void Backend_GameBackend_CalculateStudentIteration_Truck_Is_Closed_should_Skip()
        {
            // arrange
            var test = GameBackend.Instance;
            var data = new StudentModel();
            var student = DataSourceBackend.Instance.StudentBackend.Create(data);
            student.Truck.IsClosed = true;

            // act         

            test.CalculateStudentIteration(student);

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // assert         
        }

        #endregion CalculateStudentIteration

        #region PayRentPerDay
        [TestMethod]
        public void Backend_GameBackend_PayRentPerDay_Valid_ID_Should_Pass()
        {
            //arrange
            var test = GameBackend.Instance;
            var data = test.GetDefault();
            var studentData = new StudentModel();
            var student = DataSourceBackend.Instance.StudentBackend.Create(studentData);

            //act
            test.PayRentPerDay(student);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
        }

        [TestMethod]
        public void Backend_GameBackend_PayRentPerDay_RunDate_UTCNow_Should_Skip()
        {
            //arrange
            var test = GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow;
            var studentData = new StudentModel();
            var student = DataSourceBackend.Instance.StudentBackend.Create(studentData);

            //act
            test.PayRentPerDay(student);

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
        }

        [TestMethod]
        public void Backend_GameBackend_PayRentPerDay_RunDate_UTCNow_Add_1_Should_Skip()
        {
            //arrange          
            var test = GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow.AddHours(1);
            test.Update(data);
            var studentData = new StudentModel();
            var student = DataSourceBackend.Instance.StudentBackend.Create(studentData);

            //act   
            test.PayRentPerDay(student);
            var myTokens = student.Tokens;

            DataSourceBackend.Instance.StudentBackend.Update(student);

            var expect = student.Tokens;

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, myTokens, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_PayRentPerDay_RunDate_UTCNow_Minus_25_Should_Pass()
        {
            //arrange          
            var test = GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow.AddHours(-25);
            test.Update(data);
            var studentData = new StudentModel();
            var student = DataSourceBackend.Instance.StudentBackend.Create(studentData);

            //act   
            var expect = student.Tokens - 1;
            var expectOutcome = student.Truck.Outcome + 1;
            test.PayRentPerDay(student);
            
            DataSourceBackend.Instance.StudentBackend.Update(student);

            var myTokens = student.Tokens;
            var myOutcome = student.Truck.Outcome;

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, myTokens, TestContext.TestName);
            Assert.AreEqual(expectOutcome, myOutcome, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_PayRentPerDay_StudentTokens_Less_than_1_Should_Skip()
        {
            //arrange          
            var test = GameBackend.Instance;
            var data = test.GetDefault();
            data.RunDate = DateTime.UtcNow.AddHours(-25);
            test.Update(data);
            var studentData = new StudentModel();
            var student = DataSourceBackend.Instance.StudentBackend.Create(studentData);
            student.Tokens = 0;
            //act   
            var expect = student.Tokens;
            test.PayRentPerDay(student);

            DataSourceBackend.Instance.StudentBackend.Update(student);

            var myTokens = student.Tokens;

            // Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, myTokens, TestContext.TestName);
        }

        #endregion PayRentPerDay

        #region CustomerPassBy
        [TestMethod]
        public void Backend_GameBackend_CustomerPassBy_Qualified_Data_Should_Display_Message()
        {
            //arrange
            var test = GameBackend.Instance;
            var data = test.GetDefault();
            var studentData = new StudentModel();
            var student = DataSourceBackend.Instance.StudentBackend.Create(studentData);

            //act
            student.AvatarLevel = 90;

            test.CustomerPassBy(student);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(student.Truck.TransactionList, TestContext.TestName);
        }

        #endregion CustomerPassBy

        #region WillCustomerBuyOrNot
        [TestMethod]
        public void Backend_GameBackend_WillCustomerBuyOrNot_Qualified_Data_Should_Return_True()
        {
            // arrange
            var test = GameBackend.Instance;
            var data = test.GetDefault();
            var studentData = new StudentModel();
            var student = DataSourceBackend.Instance.StudentBackend.Create(studentData);

            // act
            student.AvatarLevel = 90;

            var result = test.WillCustomerBuyOrNot(student);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            
            // Fails because random is not a fixed number
            Assert.IsTrue(result, TestContext.TestName);
        }

        #endregion WillCustomerBuyOrNot

        #region CustomerPurchase
        [TestMethod]
        public void Backend_GameBackend_CustomerPurchase_invalid_data_Should_Pass()
        {
            // arrange
            var test = GameBackend.Instance;
            var data = test.GetDefault();
            var studentData = new StudentModel();
            var student = DataSourceBackend.Instance.StudentBackend.Create(studentData);
            var item = new FactoryInventoryModel
            {
                Id = FactoryInventoryBackend.Instance.GetFirstFactoryInventoryId()
            };

            // act
            //student.Inventory.Remove(item);
            student.Inventory.Clear();
            DataSourceBackend.Instance.StudentBackend.Update(student);

            test.CustomerPurchase(student);

            var message = "No Inventory to Sell";
            var elementNumber = student.Truck.TransactionList.Count;
            var result = student.Truck.TransactionList.ElementAt(0).Name.Equals(message);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(1, elementNumber, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_GameBackend_CustomerPurchase_valid_data_Should_Pass()
        {
            // arrange
            var test = GameBackend.Instance;
            var data = test.GetDefault();
            var studentData = new StudentModel();
            var student = DataSourceBackend.Instance.StudentBackend.Create(studentData);
            student.Inventory = FactoryInventoryBackend.Instance.Index();
            student.Truck.CustomersTotal = 0;

            // act
            DataSourceBackend.Instance.StudentBackend.Update(student);

            var myTokens = student.Tokens;
            var myInventoryItemNumber = student.Inventory.Count;
            var myIncome = student.Truck.Income;
            test.CustomerPurchase(student);

            var expectCustomerTotal = 1;
            var expectInventoryItemNumber = student.Inventory.Count + 1;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(student.Truck.TransactionList, TestContext.TestName);
            Assert.AreNotEqual(myTokens, student.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCustomerTotal, student.Truck.CustomersTotal, TestContext.TestName);
            Assert.AreEqual(expectInventoryItemNumber, student.Inventory.Count, TestContext.TestName);
            Assert.AreNotEqual(myIncome, student.Truck.Income, TestContext.TestName);
            Assert.IsNotNull(student.Truck.BusinessList, TestContext.TestName);
        }
  
        #endregion CustomerPurchase
    }


}
