using CarParkFeesEngine.API.Repositories.Implementations;
using CarParkFeesEngine.API.Services.Implementations;
using CarParkFeesEngine.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace CarParkFeesEngine.Test.Features
{
    /// <summary>
    /// @Feature: Calculate Package Rate Car Park Fees
    /// </summary>
    /// <remarks>
    /// As a user
    /// I want to get the package rate for the car park
    /// So that I can pay for the parking
    /// </remarks>
    [TestClass]
    public class PackageRateCarParkFeesTests
    {
        /// <summary>
        /// @Scenario: Early Bird Fees Applied When Car Is Parked In Early Bird Hours
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And entry and exit date range fall in early bird range
        /// Then I expect the total cost of the parking to be early bird rate
        /// </remarks>
        [TestMethod]
        public void EarlyBirdFeesAppliedWhenCarIsParkedInEarlyBirdHours()
        {
            // Arrange
            var entryDateTime = DateTimeHelpers.GetRandomDateTimeBetweenRange(new DateTime(2021, 3, 29, 6, 0, 0), new DateTime(2021, 3, 29, 9, 0, 0));
            var exitDateTime = DateTimeHelpers.GetRandomDateTimeBetweenRange(new DateTime(2021, 3, 29, 15, 30, 0), new DateTime(2021, 3, 29, 21, 30, 0));

            var packageRates = Entities.GetSamplePackageRates();

            var pakageRatesRepository = Substitute.For<IPackageRatesRepository>();
            pakageRatesRepository.GetPackageRates().Returns(packageRates);

            var packageRateCalculatorService = new PackageRateCalculatorService(pakageRatesRepository);

            // Act
            var actual = packageRateCalculatorService.CalculatePackageRate(entryDateTime, exitDateTime);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreEqual(Constants.EarlyBirdPackageRateName, actual.PackageName);
            Assert.AreEqual(13.0, actual.TotalPrice);
        }

        /// <summary>
        /// @Scenario: Early Bird Fees Not Applied When Car Is Parked In Early Bird Hours But Not A WeekDay
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And entry and exit date range fall in early bird range
        /// And it is not a weekday
        /// Then I expect the total cost of the parking fee is not the early bird rate
        /// </remarks>
        [TestMethod]
        public void EarlyBirdFeesNotAppliedWhenCarIsParkedInEarlyBirdHoursButNotAWeekDay()
        {

            // Arrange
            var entryDateTime = new DateTime(2021, 3, 27, 7, 0, 0);
            var exitDateTime = new DateTime(2021, 3, 27, 20, 0, 0);

            var packageRates = Entities.GetSamplePackageRates();

            var pakageRatesRepository = Substitute.For<IPackageRatesRepository>();
            pakageRatesRepository.GetPackageRates().Returns(packageRates);

            var packageRateCalculatorService = new PackageRateCalculatorService(pakageRatesRepository);

            // Act
            var actual = packageRateCalculatorService.CalculatePackageRate(entryDateTime, exitDateTime);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreNotEqual(Constants.EarlyBirdPackageRateName, actual.PackageName);
            Assert.AreNotEqual(13.00, actual.TotalPrice);
        }

        /// <summary>
        /// @Scenario: Night Rate Fees Applied When Car Is Parked In Night Rate Hours Range
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And entry and exit date range fall in night rate range
        /// Then I expect the total cost of the parking to be night rate
        /// </remarks>
        [TestMethod]
        public void NightRateFeesAppliedWhenCarIsParkedInNightRateHoursRange()
        {
            // Arrange
            var entryDateTime = DateTimeHelpers.GetRandomDateTimeBetweenRange(new DateTime(2021, 3, 29, 18, 0, 0), new DateTime(2021, 3, 30, 0, 0, 0));
            var exitDateTime = DateTimeHelpers.GetRandomDateTimeBetweenRange(new DateTime(2021, 3, 29, 18, 0, 0), new DateTime(2021, 3, 30, 8, 0, 0));

            var packageRates = Entities.GetSamplePackageRates();

            var pakageRatesRepository = Substitute.For<IPackageRatesRepository>();
            pakageRatesRepository.GetPackageRates().Returns(packageRates);

            var packageRateCalculatorService = new PackageRateCalculatorService(pakageRatesRepository);

            // Act
            var actual = packageRateCalculatorService.CalculatePackageRate(entryDateTime, exitDateTime);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreEqual(Constants.NightRatePackageRateName, actual.PackageName);
            Assert.AreEqual(6.50, actual.TotalPrice);
        }

        /// <summary>
        /// @Scenario: Weekend Rate Fees Applied When Car Is Parked In Weekend Hours
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And car is parked in weekend hours
        /// Then I expect the total cost of the parking to be weekend rate
        /// </remarks>
        [TestMethod]
        public void WeekendRateesAppliedWhenCarIsParkedInWeekendHours()
        {
            // Arrange
            var entryDateTime = DateTimeHelpers.GetRandomDateTimeBetweenRange(new DateTime(2021, 3, 27, 0, 0, 0), new DateTime(2021, 3, 28, 23, 0, 0));
            var exitDateTime = DateTimeHelpers.GetRandomDateTimeBetweenRange(new DateTime(2021, 3, 28, 23, 0, 0), new DateTime(2021, 3, 29, 0, 0, 0));

            var packageRates = Entities.GetSamplePackageRates();

            var pakageRatesRepository = Substitute.For<IPackageRatesRepository>();
            pakageRatesRepository.GetPackageRates().Returns(packageRates);

            var packageRateCalculatorService = new PackageRateCalculatorService(pakageRatesRepository);

            // Act
            var actual = packageRateCalculatorService.CalculatePackageRate(entryDateTime, exitDateTime);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreEqual(Constants.WeekendPackageRateName, actual.PackageName);
            Assert.AreEqual(10.00, actual.TotalPrice);
        }

        /// <summary>
        /// @Scenario: Night Rate Fees Applied When Car Is Parked In Weekend And Night Rate Hours Range
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And car is parked in weekend hours
        /// And entry and exit date range fall in night rate range
        /// Then I expect the total cost of the parking to be night rate
        /// </remarks>
        [TestMethod]
        public void NightRateFeesAppliedWhenCarIsParkedInWeekendAndNightRateHoursRange()
        {
            // Arrange
            var entryDateTime = DateTimeHelpers.GetRandomDateTimeBetweenRange(new DateTime(2021, 3, 27, 18, 0, 0), new DateTime(2021, 3, 28, 0, 0, 0));
            var exitDateTime = DateTimeHelpers.GetRandomDateTimeBetweenRange(new DateTime(2021, 3, 27, 18, 0, 0), new DateTime(2021, 3, 28, 8, 0, 0));

            var packageRates = Entities.GetSamplePackageRates();

            var pakageRatesRepository = Substitute.For<IPackageRatesRepository>();
            pakageRatesRepository.GetPackageRates().Returns(packageRates);

            var packageRateCalculatorService = new PackageRateCalculatorService(pakageRatesRepository);

            // Act
            var actual = packageRateCalculatorService.CalculatePackageRate(entryDateTime, exitDateTime);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreEqual(Constants.NightRatePackageRateName, actual.PackageName);
            Assert.AreEqual(6.50, actual.TotalPrice);
        }
    }
}
