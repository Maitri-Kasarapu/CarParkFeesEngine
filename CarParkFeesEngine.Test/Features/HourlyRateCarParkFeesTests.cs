using CarParkFeesEngine.API.Repositories.Implementations;
using CarParkFeesEngine.API.Services.Implementations;
using CarParkFeesEngine.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace CarParkFeesEngine.Test.Features
{
    /// <summary>
    /// @Feature: Calculate Car Park Fees
    /// </summary>
    /// <remarks>
    /// As a user
    /// I want to get the hourly rate for the car park
    /// So that I can pay for the parking
    /// </remarks>
    [TestClass]
    public class HourlyRateCarParkFeesTests
    {
        /// <summary>
        /// @Scenario: Standard One Hour Fees Applied When Car Is Parked For One Hour
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And can is parked only for one hour
        /// Then I expect the total cost of the parking to be standard one our rate
        /// </remarks>
        [TestMethod]
        public void StandardOneHourFeesAppliedWhenCarIsParkedForOneHour()
        {
            // Arrange
            var entryDateTime = new DateTime(2021, 3, 29, 8, 0, 0);
            var exitDateTime = new DateTime(2021, 3, 29, 9, 0, 0);

            var hourlyRates = Entities.GetSampleHourlyRates();

            var hourlyRatesRepository = Substitute.For<IHourlyRatesRepository>();
            hourlyRatesRepository.GetHourlyRates().Returns(hourlyRates);

            var hourlyRateCalculatorService = new HourlyRateCalculatorService(hourlyRatesRepository);

            // Act
            var actual = hourlyRateCalculatorService.CalculateHourlyRate(entryDateTime, exitDateTime);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreEqual(Constants.HourlyRatesName, actual.PackageName);
            Assert.AreEqual(5.00, actual.TotalPrice);
        }

        /// <summary>
        /// @Scenario: Standard One Hour Fees Applied When Car Is Parked For Less Than One Hour
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And can is parked only for less than one hour
        /// Then I expect the total cost of the parking to be standard one our rate
        /// </remarks>
        [TestMethod]
        public void StandardOneHourFeesAppliedWhenCarIsParkedForLessThanOneHour()
        {
            // Arrange
            var entryDateTime = new DateTime(2021, 3, 29, 8, 0, 0);
            var exitDateTime = new DateTime(2021, 3, 29, 8, 59, 0);

            var hourlyRates = Entities.GetSampleHourlyRates();

            var hourlyRatesRepository = Substitute.For<IHourlyRatesRepository>();
            hourlyRatesRepository.GetHourlyRates().Returns(hourlyRates);

            var hourlyRateCalculatorService = new HourlyRateCalculatorService(hourlyRatesRepository);

            // Act
            var actual = hourlyRateCalculatorService.CalculateHourlyRate(entryDateTime, exitDateTime);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreEqual(Constants.HourlyRatesName, actual.PackageName);
            Assert.AreEqual(5.00, actual.TotalPrice);
        }

        /// <summary>
        /// @Scenario: Standard Two Hour Fees Applied When Car Is Parked For Two Hours
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And can is parked only for two hours
        /// Then I expect the total cost of the parking to be standard two our rate
        /// </remarks>
        [TestMethod]
        public void StandardTwoHourFeesAppliedWhenCarIsParkedForTwoHours()
        {
            // Arrange
            var entryDateTime = new DateTime(2021, 3, 29, 8, 0, 0);
            var exitDateTime = new DateTime(2021, 3, 29, 10, 0, 0);

            var hourlyRates = Entities.GetSampleHourlyRates();

            var hourlyRatesRepository = Substitute.For<IHourlyRatesRepository>();
            hourlyRatesRepository.GetHourlyRates().Returns(hourlyRates);

            var hourlyRateCalculatorService = new HourlyRateCalculatorService(hourlyRatesRepository);

            // Act
            var actual = hourlyRateCalculatorService.CalculateHourlyRate(entryDateTime, exitDateTime);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreEqual(Constants.HourlyRatesName, actual.PackageName);
            Assert.AreEqual(10.00, actual.TotalPrice);
        }

        /// <summary>
        /// @Scenario: Standard Two Hour Fees Applied When Car Is Parked For More Than One Hour And Less Than Two Hours
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And can is parked only for more than one hour and less than two hours
        /// Then I expect the total cost of the parking to be standard two our rate
        /// </remarks>
        [TestMethod]
        public void StandardTwoHourFeesAppliedWhenCarIsParkedForMoreThanOneHourAndLessThanTwoHours()
        {
            // Arrange
            var entryDateTime = new DateTime(2021, 3, 29, 8, 0, 0);
            var exitDateTime = new DateTime(2021, 3, 29, 9, 30, 0);

            var hourlyRates = Entities.GetSampleHourlyRates();

            var hourlyRatesRepository = Substitute.For<IHourlyRatesRepository>();
            hourlyRatesRepository.GetHourlyRates().Returns(hourlyRates);

            var hourlyRateCalculatorService = new HourlyRateCalculatorService(hourlyRatesRepository);

            // Act
            var actual = hourlyRateCalculatorService.CalculateHourlyRate(entryDateTime, exitDateTime);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreEqual(Constants.HourlyRatesName, actual.PackageName);
            Assert.AreEqual(10.00, actual.TotalPrice);
        }

        /// <summary>
        /// @Scenario: Standard Two Hour Fees Applied When Car Is Parked For Three Hours
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And can is parked only for three hours
        /// Then I expect the total cost of the parking to be standard three our rate
        /// </remarks>
        [TestMethod]
        public void StandardThreeHourFeesAppliedWhenCarIsParkedForThreeHours()
        {
            // Arrange
            var entryDateTime = new DateTime(2021, 3, 29, 8, 0, 0);
            var exitDateTime = new DateTime(2021, 3, 29, 11, 0, 0);

            var hourlyRates = Entities.GetSampleHourlyRates();

            var hourlyRatesRepository = Substitute.For<IHourlyRatesRepository>();
            hourlyRatesRepository.GetHourlyRates().Returns(hourlyRates);

            var hourlyRateCalculatorService = new HourlyRateCalculatorService(hourlyRatesRepository);

            // Act
            var actual = hourlyRateCalculatorService.CalculateHourlyRate(entryDateTime, exitDateTime);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreEqual(Constants.HourlyRatesName, actual.PackageName);
            Assert.AreEqual(15.00, actual.TotalPrice);
        }

        /// <summary>
        /// @Scenario: Standard Three Hour Fees Applied When Car Is Parked For More Than Two Hours And Less Than Two Hours
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And can is parked only for more than two hours and less than three hours
        /// Then I expect the total cost of the parking to be standard three our rate
        /// </remarks>
        [TestMethod]
        public void StandardThreeHourFeesAppliedWhenCarIsParkedForMoreThanTwoHoursAndLessThanThreeHours()
        {
            // Arrange
            var entryDateTime = new DateTime(2021, 3, 29, 8, 0, 0);
            var exitDateTime = new DateTime(2021, 3, 29, 10, 30, 0);

            var hourlyRates = Entities.GetSampleHourlyRates();

            var hourlyRatesRepository = Substitute.For<IHourlyRatesRepository>();
            hourlyRatesRepository.GetHourlyRates().Returns(hourlyRates);

            var hourlyRateCalculatorService = new HourlyRateCalculatorService(hourlyRatesRepository);

            // Act
            var actual = hourlyRateCalculatorService.CalculateHourlyRate(entryDateTime, exitDateTime);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreEqual(Constants.HourlyRatesName, actual.PackageName);
            Assert.AreEqual(15.00, actual.TotalPrice);
        }
    }
}
