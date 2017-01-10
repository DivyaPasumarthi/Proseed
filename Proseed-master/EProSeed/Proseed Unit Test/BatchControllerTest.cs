using EProSeed.Lib.BLL;
using EProSeed.Models;
using EProSeed.Web.Controllers;
using EProSeed.Web.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Proseed_Unit_Test
{
    [TestFixture]
    public class BatchControllerTest
    {
        #region Variables
        ITrainerFeedback _trainerFeedbackRepo;
        IBatch _batchRepo;
        ITrainer _trainerRepo;
        [SetUp]
        public void SetUp()
        {
            var batchRepo = new Mock<IBatch>(); ;
            var feedRepo = new Mock<ITrainerFeedback>();
            var trainerRepo = new Mock<ITrainer>();
            List<BatchModel> modelList = new List<BatchModel>();
            for (int i = 0; i < 10; i++)
            {
                BatchModel model = new BatchModel();
                model.Id = i;
                modelList.Add(model);
            }
            List<TrainerModel> trainerModelList = new List<TrainerModel>();
            for (int i = 0; i < 10; i++)
            {
                TrainerModel model = new TrainerModel();
                model.Id = i;
                model.Name = i.ToString();
                trainerModelList.Add(model);
            }
            batchRepo.Setup(r => r.GetAll()).Returns(modelList);
            trainerRepo.Setup(t => t.GetAll()).Returns(trainerModelList);
            _batchRepo = batchRepo.Object;
            _trainerFeedbackRepo = feedRepo.Object;
            _trainerRepo = trainerRepo.Object;
        }
        #endregion
        [Test]
        public void DuplicateBatchNotCreated()
        {
            vmBatch _batch = new vmBatch();
            _batch.Id = 0;
            _batch.BatchDates = "12/05/2016,12/06/2016,12/07/2016,12/08/2016,12/09/2016,12/13/2016,12/12/2016,12/14/2016,12/15/2016";
            _batch.TrainerId = 1;
            _batch.trainer = new TrainerModel();
            _batch.Name = "Knight Riders";
            var controller = new BatchController(_batchRepo, _trainerRepo);
            ViewResult result= controller.Create(_batch) as ViewResult;
            Assert.AreEqual("The Batch already exists with the same name.", result.ViewData["ErrorMsg"]);

        }
    }
}
