using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EProSeed.DAL;
using EProSeed.Lib.BLL;
using EProSeed.Lib.BLL.Contracts;
using EProSeed.Lib.BLL.Repository;
using EProSeed.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EProSeed.Business.Test
{
    [TestClass]
    public class ReportTest
    {
        private readonly Mock<ProDbContext> ctx;
        public ReportTest()
        {
            ctx = new Mock<ProDbContext>();
            var batchDates = new List<BatchDates>() 
            { 
                new BatchDates() { ID = 1, BatchID = 1, BatchDate = new DateTime(2016, 12, 19) },
                new BatchDates() { ID = 2, BatchID = 1, BatchDate = new DateTime(2016, 12, 20) }
            }.AsQueryable();

            var data = new List<BatchModel> 
            { 
                new BatchModel() { Id = 1, Name = "b1", BatchDates = batchDates.ToList(), TrainerId = 1}
            }.AsQueryable();

            var trainers = new List<TrainerModel>()
            {
                new TrainerModel() { Id = 1, Email="t@gmail.com", Name="t", Password="123"}
            }.AsQueryable();

            var inductees = new List<InducteeModel>()
            {
                new InducteeModel() { Id = 1, Email="i1@gmail.com", Name="i1", BatchID = 1, EmpId="123"},
                new InducteeModel() { Id = 2, Email="i2@gmail.com", Name="i2", BatchID = 1, EmpId="124"}
            }.AsQueryable();

            var properties = new List<PropertyModel>()
            {
                new PropertyModel() { ID = 1, CommitmentRating = 10 },
                new PropertyModel() { ID = 2, CommitmentRating= 10 }
            }.AsQueryable();

            var feedbacks = new List<FeedbackModel>() 
            {
                new FeedbackModel() { ID = 1, FeedbackDate = new DateTime(2016, 12, 19), InducteeID = 1, PropertyId = 1, TrainerId = 1 },
                new FeedbackModel() { ID = 2, FeedbackDate = new DateTime(2016, 12, 19), InducteeID = 2, PropertyId = 2, TrainerId = 1 }
            }.AsQueryable();

            var mockSet1 = new Mock<IDbSet<BatchModel>>();
            mockSet1.As<IQueryable<BatchModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet1.As<IQueryable<BatchModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet1.As<IQueryable<BatchModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet1.As<IQueryable<BatchModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSet2 = new Mock<IDbSet<BatchDates>>();
            mockSet2.As<IQueryable<BatchDates>>().Setup(m => m.Provider).Returns(batchDates.Provider);
            mockSet2.As<IQueryable<BatchDates>>().Setup(m => m.Expression).Returns(batchDates.Expression);
            mockSet2.As<IQueryable<BatchDates>>().Setup(m => m.ElementType).Returns(batchDates.ElementType);
            mockSet2.As<IQueryable<BatchDates>>().Setup(m => m.GetEnumerator()).Returns(batchDates.GetEnumerator());

            var mockSet3 = new Mock<IDbSet<TrainerModel>>();
            mockSet3.As<IQueryable<TrainerModel>>().Setup(m => m.Provider).Returns(trainers.Provider);
            mockSet3.As<IQueryable<TrainerModel>>().Setup(m => m.Expression).Returns(trainers.Expression);
            mockSet3.As<IQueryable<TrainerModel>>().Setup(m => m.ElementType).Returns(trainers.ElementType);
            mockSet3.As<IQueryable<TrainerModel>>().Setup(m => m.GetEnumerator()).Returns(trainers.GetEnumerator());

            var mockSet4 = new Mock<IDbSet<InducteeModel>>();
            mockSet4.As<IQueryable<InducteeModel>>().Setup(m => m.Provider).Returns(inductees.Provider);
            mockSet4.As<IQueryable<InducteeModel>>().Setup(m => m.Expression).Returns(inductees.Expression);
            mockSet4.As<IQueryable<InducteeModel>>().Setup(m => m.ElementType).Returns(inductees.ElementType);
            mockSet4.As<IQueryable<InducteeModel>>().Setup(m => m.GetEnumerator()).Returns(inductees.GetEnumerator());

            var mockSet5 = new Mock<IDbSet<PropertyModel>>();
            mockSet5.As<IQueryable<PropertyModel>>().Setup(m => m.Provider).Returns(properties.Provider);
            mockSet5.As<IQueryable<PropertyModel>>().Setup(m => m.Expression).Returns(properties.Expression);
            mockSet5.As<IQueryable<PropertyModel>>().Setup(m => m.ElementType).Returns(properties.ElementType);
            mockSet5.As<IQueryable<PropertyModel>>().Setup(m => m.GetEnumerator()).Returns(properties.GetEnumerator());

            var mockSet6 = new Mock<IDbSet<FeedbackModel>>();
            mockSet6.As<IQueryable<FeedbackModel>>().Setup(m => m.Provider).Returns(feedbacks.Provider);
            mockSet6.As<IQueryable<FeedbackModel>>().Setup(m => m.Expression).Returns(feedbacks.Expression);
            mockSet6.As<IQueryable<FeedbackModel>>().Setup(m => m.ElementType).Returns(feedbacks.ElementType);
            mockSet6.As<IQueryable<FeedbackModel>>().Setup(m => m.GetEnumerator()).Returns(feedbacks.GetEnumerator());

            ctx.Setup(x => x.Batch).Returns(mockSet1.Object);
            ctx.Setup(x => x.BatchDates).Returns(mockSet2.Object);
            ctx.Setup(x => x.Tranner).Returns(mockSet3.Object);
            ctx.Setup(x => x.Inductee).Returns(mockSet4.Object);
            ctx.Setup(x => x.Property).Returns(mockSet5.Object);
            ctx.Setup(x => x.Feedback).Returns(mockSet6.Object);
        }

        [TestMethod]
        public void TestBatchAverage()
        {
            IReport report = new Report(ctx.Object);
            var reportModel = report.GetReport(1, -1);
            Assert.AreEqual(0.5M, reportModel.BatchAverage);
        }

        [TestMethod]
        public void TestInducteeAverage()
        {
            IReport report = new Report(ctx.Object);
            var reportModel = report.GetReport(1, 1);
            Assert.AreEqual(0.5M, reportModel.InducteeAverage);
        }

        [TestMethod]
        public void TestNumInductee()
        {
            IReport report = new Report(ctx.Object);
            var reportModel = report.GetReport(1, -1);
            Assert.AreEqual(2, reportModel.NumberofInductees);
        }

        [TestMethod]
        public void TestDateWiseFeedbacksCount()
        {
            IReport report = new Report(ctx.Object);
            var reportModel = report.GetReport(1, 1);
            Assert.AreEqual(2, reportModel.FeedBacks.Count);
        }


    }
}
