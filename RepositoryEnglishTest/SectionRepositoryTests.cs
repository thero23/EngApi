using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;
using Entities.Data;

namespace Repository.Tests
{
    [TestClass()]
    public class SectionRepositoryTests
    {
        private EnglishContext _context;

        public SectionRepositoryTests(EnglishContext context)
        {
            _context = context;
        }

        [TestMethod()]
        public void GetSectionsByConditionAsyncTest()
        {
            Assert.Equals(
                _context.Sections.FirstOrDefault(s => s.Id.Equals(new Guid("cc23adcf-bd12-4a27-9f1e-cedd2a476818"))).Name,
                "First section122");
            Assert.Fail();
        }
    }
}