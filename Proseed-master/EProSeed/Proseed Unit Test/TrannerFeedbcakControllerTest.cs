using System;
using NUnit.Framework;
using Moq;
using System.Web;
using System.Security.Principal;
using System.Web.Routing;
using System.Collections.Specialized;
using System.Collections.Generic;
using EProSeed.Web.Controllers;
using EProSeed.Models;
using EProSeed.Lib.BLL.Repository;
using EProSeed.Lib.BLL;
using System.Web.Mvc;

namespace Proseed_Unit_Test
{
    [TestFixture]
    public class TrannerFeedbcakControllerTest
    {
        #region Variables
        ITrainerFeedback _trainerFeedbackRepo;
        IBatch _batchRepo;
        [SetUp]
        public void SetUp()
        {
            var batchRepo = new Mock<IBatch>(); ;
            var feedRepo = new Mock<ITrainerFeedback>(); ;
            List<BatchModel> modelList = new List<BatchModel>();
            for (int i = 0; i < 10; i++)
            {
                BatchModel model = new BatchModel();
                model.Id = i;
                modelList.Add(model);
            }
            batchRepo.Setup(r => r.GetAll()).Returns(modelList);
            _batchRepo = batchRepo.Object;
            _trainerFeedbackRepo = feedRepo.Object;
        }
        #endregion
        [Test]
        public void IndexForTrainer()
        {
            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            mockSession.SetupGet(s => s["UserId"]).Returns(4); //somevalue
            mockSession.SetupGet(s => s["UserType"]).Returns("Trainer"); //somevalue
            mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
          
            IList<BatchModel> batchList = new List<BatchModel>();
            var controller = new TrainerFeedbackController(_trainerFeedbackRepo, _batchRepo);
            controller.ControllerContext = mockControllerContext.Object;
            Assert.IsNotNull(controller.Index());

        }
        [Test]
        public void IndexForTrainee()
        {
            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            mockSession.SetupGet(s => s["UserId"]).Returns(7); //somevalue
            mockSession.SetupGet(s => s["UserType"]).Returns("Trainee"); //somevalue
            mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);

            IList<BatchModel> batchList = new List<BatchModel>();
            var controller = new TrainerFeedbackController(_trainerFeedbackRepo, _batchRepo);
            controller.ControllerContext = mockControllerContext.Object;
            Assert.IsNotNull(controller.Index());
        }
    }
}
