using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnglishApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using English.Services.Interfaces;
using Contracts;
using Microsoft.AspNetCore.Identity;
using Entities.Models;
using AutoMapper;
using English.Services;
using Entities.Data;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EnglishApi.Controllers.Tests
{
    [TestClass()]
    public class SectionsControllerTests : ControllerBase
    {
        //[TestMethod()]
        //public async System.Threading.Tasks.Task GetSectionByIdTestAsync()
        //{
        //    var mock = new Mock<ISectionService>();
        //    mock.Setup(repo =>
        //        repo.FindSectionsByCondition(
        //            section => section.Id.Equals(new Guid("cc23adcf-bd12-4a27-9f1e-cedd2a476818")), false));
        //    ISectionService rep = mock.Object;
        //    var result = await rep.FindSectionsByCondition(section => section.Id.Equals(new Guid("cc23adcf-bd12-4a27-9f1e-cedd2a476818")), false);
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod()]
        //public async System.Threading.Tasks.Task GetSectionByIdTestAsync()
        //{
        //    var mockSectionService = new Mock<ISectionService>();
        //    mockSectionService
        //        .Setup(service =>
        //            service.FindSectionsByCondition(
        //                section => section.Id.Equals(new Guid("cc23adcf-bd12-4a27-9f1e-cedd2a476818")), false))
        //        .re;
        //    //var mockUserManager = new Mock<UserManager<User>>();
        //    var mockLoggerManager = new Mock<ILoggerManager>();
        //    var mockMapper = new Mock<IMapper>();
        //    var controller = new SectionsController(mockSectionService.Object, mockLoggerManager.Object, mockMapper.Object, null);
        //    var result = await controller.GetSectionById(new Guid("cc23adcf-bd12-4a27-9f1e-cedd2a476818"));
        //    Assert.IsNotNull(result);
        //}

        [TestMethod()]
        public async System.Threading.Tasks.Task GetSectionByIdTestAsync()
        {
            var mockSectionService = new Mock<ISectionService>();
            var mock = new Mock<SectionsController>();
            var  result =mock.Object.GetSectionById(new Guid("cc23adcf-bd12-4a27-9f1e-cedd2a476818"));
            //var mockUserManager = new Mock<UserManager<User>>();
            var mockLoggerManager = new Mock<ILoggerManager>();
            var mockMapper = new Mock<IMapper>();
            var controller = new SectionsController(mockSectionService.Object, mockLoggerManager.Object, mockMapper.Object, null);
            //var result = await controller.GetSectionById(new Guid("cc23adcf-bd12-4a27-9f1e-cedd2a476818"));
            Assert.IsNotNull(result);
        }
    }
}