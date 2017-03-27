using NUnit.Framework;
using System;
using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Tests.Models
{
    [TestFixture]
    public class JobTest
    {

        [Test]
        public void ConstructorShouldCreateJob_WithoutParams()
        {
            // Act & Assert
            var job = new Job();

            Assert.IsInstanceOf<Job>(job);
        }

        [Test]
        public void ConstructorShouldNotThrow_WithoutParams()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => new Job());
        }

        [Test]
        public void JobShouldCreate_HashsetOfApplicationUsers_WhenInitialized()
        {
            // Arrange
            var job = new Job();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<ApplicationUser>), job.Users);
        }

        [Test]
        public void JobShouldCreate_EmptyHashsetOfApplicationUsers_WhenInitialized()
        {
            // Arrange
            var job = new Job();
            var expectedCollection = new HashSet<ApplicationUser>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, job.Users);
        }

        [Test]
        public void JobShouldSet_CorrectProperties_WhenValidArgsPassed()
        {
            // Arrange
            int cityId = 1;
            string jobTitle = "JobTitle1";
            string jobDescription = "JobDescription1";
            int slots = 1;
            DateTime startDate = new DateTime(2020, 01, 01);
            DateTime endDate = new DateTime(2021, 01, 01);
            decimal wage = 10m;
            string companyName = "CompanyName1";
            decimal price = 10m;
            string imagePath = "/Image/Path";
            var expectedUsers = new HashSet<ApplicationUser>();

            var job = new Job(cityId, jobTitle, jobDescription, slots, startDate, endDate, wage, companyName, price, imagePath);

            // Act & Assert
            Assert.AreEqual(cityId, job.CityId);
            Assert.AreEqual(jobTitle, job.JobTitle);
            Assert.AreEqual(jobDescription, job.JobDescription);
            Assert.AreEqual(slots, job.Slots);
            Assert.AreEqual(startDate, job.StartDate);
            Assert.AreEqual(endDate, job.EndDate);
            Assert.AreEqual(wage, job.Wage);
            Assert.AreEqual(companyName, job.CompanyName);
            Assert.AreEqual(price, job.Price);
            Assert.AreEqual(imagePath, job.ImagePath);
            CollectionAssert.AreEqual(expectedUsers, job.Users);
        }
    }
}

