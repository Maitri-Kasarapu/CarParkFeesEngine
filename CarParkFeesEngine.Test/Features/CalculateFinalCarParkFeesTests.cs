using CarParkFeesEngine.API.Dtos;
using CarParkFeesEngine.API.Enumerations;
using CarParkFeesEngine.API.Services.Implementations;
using CarParkFeesEngine.API.Services.Interfaces;
using CarParkFeesEngine.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;

namespace CarParkFeesEngine.Test.Features
{
    /// <summary>
    /// @Feature: Calculate Car Park Fees
    /// </summary>
    /// <remarks>
    /// As a user
    /// I want to get the total cost for the car park
    /// So that I can pay for the parking
    /// </remarks>
    [TestClass]
    public class CalculateFinalCarParkFeesTests
    {
        /// <summary>
        /// @Scenario: Validation Error Is Returned When Entry Time Is Not Supplied
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has not been supplied
        /// And an exit time has been supplied
        /// Then I expect a validation error to be returned
        /// </remarks>
        [TestMethod]
        public void ValidationErrorIsReturnedWhenEntryTimeIsNotSupplied()
        {
            // Arrange
            var exitDateTime = new DateTime(2021, 3, 29, 9, 0, 0);
            var carParkingTimeInputDto = new CarParkingTimeInputDto { ExitTime = exitDateTime };

            var packageRateCalculatorService = Substitute.For<IPackageRateCalculatorService>();
            var hourlyRateCalculatorService = Substitute.For<IHourlyRateCalculatorService>();

            var carParkCalculatorService = new CarParkFeesCalculatorService(packageRateCalculatorService, hourlyRateCalculatorService);

            // Act
            var actual = carParkCalculatorService.CalculateCarParkFees(carParkingTimeInputDto);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Errors.Count);
            Assert.AreEqual(ErrorCode.CAR_PARKING_ENTRY_TIME_IS_REQUIRED, actual.Errors.First().ErrorCode);
            packageRateCalculatorService.Received(0).CalculatePackageRate(Arg.Any<DateTime>(), Arg.Any<DateTime>());
            hourlyRateCalculatorService.Received(0).CalculateHourlyRate(Arg.Any<DateTime>(), Arg.Any<DateTime>());
        }

        /// <summary>
        /// @Scenario: Validation Errors Are Returned When Exit Time Is Not Supplied
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has not been supplied
        /// Then I expect a validation error to be returned
        /// </remarks>
        [TestMethod]
        public void ValidationErrorAreReturnedWhenExitTimeIsNotSupplied()
        {
            // Arrange
            var entryDateTime = new DateTime(2021, 3, 29, 9, 0, 0);
            var carParkingTimeInputDto = new CarParkingTimeInputDto { EntryTime = entryDateTime };

            var packageRateCalculatorService = Substitute.For<IPackageRateCalculatorService>();
            var hourlyRateCalculatorService = Substitute.For<IHourlyRateCalculatorService>();

            var carParkCalculatorService = new CarParkFeesCalculatorService(packageRateCalculatorService, hourlyRateCalculatorService);

            // Act
            var actual = carParkCalculatorService.CalculateCarParkFees(carParkingTimeInputDto);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Errors.Count);
            Assert.AreEqual(ErrorCode.CAR_PARKING_EXIT_TIME_IS_REQUIRED, actual.Errors.First().ErrorCode);
            Assert.AreEqual(ErrorCode.CAR_PARKING_DATE_RANGE_IS_NOT_VALID, actual.Errors[1].ErrorCode);
            packageRateCalculatorService.Received(0).CalculatePackageRate(Arg.Any<DateTime>(), Arg.Any<DateTime>());
            hourlyRateCalculatorService.Received(0).CalculateHourlyRate(Arg.Any<DateTime>(), Arg.Any<DateTime>());
        }

        /// <summary>
        /// @Scenario: Validation Error Is Returned When Entry Time Is Not Greater than Exit Time
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And entry time is greater than exit time
        /// Then I expect a validation error to be returned
        /// </remarks>
        [TestMethod]
        public void ValidationErrorIsReturnedWhenEntryTimeIsGreaterThanExitTime()
        {
            // Arrange
            var entryDateTime = new DateTime(2021, 3, 30, 9, 0, 0);
            var exitDateTime = new DateTime(2021, 3, 29, 9, 0, 0);
            var carParkingTimeInputDto = new CarParkingTimeInputDto { EntryTime = entryDateTime, ExitTime = exitDateTime };

            var packageRateCalculatorService = Substitute.For<IPackageRateCalculatorService>();
            var hourlyRateCalculatorService = Substitute.For<IHourlyRateCalculatorService>();

            var carParkCalculatorService = new CarParkFeesCalculatorService(packageRateCalculatorService, hourlyRateCalculatorService);

            // Act
            var actual = carParkCalculatorService.CalculateCarParkFees(carParkingTimeInputDto);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Errors.Count);
            Assert.AreEqual(ErrorCode.CAR_PARKING_DATE_RANGE_IS_NOT_VALID, actual.Errors.First().ErrorCode);
            packageRateCalculatorService.Received(0).CalculatePackageRate(Arg.Any<DateTime>(), Arg.Any<DateTime>());
            hourlyRateCalculatorService.Received(0).CalculateHourlyRate(Arg.Any<DateTime>(), Arg.Any<DateTime>());
        }

        /// <summary>
        /// @Scenario: Standard Hourly Rate Is Applied When Hourly Rate Is Cheaper Than Package Rate
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And date range is qualified for a package rate also
        /// And hourly rate is cheaper than the package rate
        /// Then I expect the total cost of the parking to be standard hourly rate
        /// </remarks>
        [TestMethod]
        public void StandardHourlyRateIsAppliedWhenHourlyRateIsCheaperThanPackageRate()
        {
            // Arrange
            var entryDateTime = new DateTime(2021, 3, 29, 18, 0, 0);
            var exitDateTime = new DateTime(2021, 3, 29, 19, 0, 0);
            var carParkingTimeInputDto = new CarParkingTimeInputDto { EntryTime = entryDateTime, ExitTime = exitDateTime };

            var packageRateCalculatorService = Substitute.For<IPackageRateCalculatorService>();
            packageRateCalculatorService.CalculatePackageRate(carParkingTimeInputDto.EntryTime, carParkingTimeInputDto.ExitTime).Returns(new CarParkingRateDto { PackageName = Constants.NightRatePackageRateName, TotalPrice = 6.50 });

            var hourlyRateCalculatorService = Substitute.For<IHourlyRateCalculatorService>();
            hourlyRateCalculatorService.CalculateHourlyRate(carParkingTimeInputDto.EntryTime, carParkingTimeInputDto.ExitTime).Returns(new CarParkingRateDto { PackageName = Constants.HourlyRatesName, TotalPrice = 5.00 });

            var carParkCalculatorService = new CarParkFeesCalculatorService(packageRateCalculatorService, hourlyRateCalculatorService);

            // Act
            var actual = carParkCalculatorService.CalculateCarParkFees(carParkingTimeInputDto);

            // Assert
            packageRateCalculatorService.Received(1).CalculatePackageRate(Arg.Any<DateTime>(), Arg.Any<DateTime>());
            hourlyRateCalculatorService.Received(1).CalculateHourlyRate(Arg.Any<DateTime>(), Arg.Any<DateTime>());
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreEqual(Constants.HourlyRatesName, actual.PackageName);
            Assert.AreEqual(5.00, actual.TotalPrice);
        }

        /// <summary>
        /// @Scenario: Package Rate Is Applied When Package Rate Is Cheaper Than Hourly Rate
        /// </summary>
        /// <remarks>
        /// Given a request is made to calculate the car park fee
        /// When an entry time has been supplied
        /// And an exit time has been supplied
        /// And date range is qualified for a package rate also
        /// And package rate is cheaper than the hourly rate
        /// Then I expect the total cost of the parking to be package rate
        /// </remarks>
        [TestMethod]
        public void PackageRateIsAppliedWhenPackageRateIsCheaperThanHourlyRate()
        {
            // Arrange
            var entryDateTime = new DateTime(2021, 3, 27, 0, 0, 0);
            var exitDateTime = new DateTime(2021, 3, 29, 0, 0, 0);
            var carParkingTimeInputDto = new CarParkingTimeInputDto { EntryTime = entryDateTime, ExitTime = exitDateTime };

            var packageRateCalculatorService = Substitute.For<IPackageRateCalculatorService>();
            packageRateCalculatorService.CalculatePackageRate(carParkingTimeInputDto.EntryTime, carParkingTimeInputDto.ExitTime).Returns(new CarParkingRateDto { PackageName = Constants.WeekendPackageRateName, TotalPrice = 10.00 });

            var hourlyRateCalculatorService = Substitute.For<IHourlyRateCalculatorService>();
            hourlyRateCalculatorService.CalculateHourlyRate(carParkingTimeInputDto.EntryTime, carParkingTimeInputDto.ExitTime).Returns(new CarParkingRateDto { PackageName = Constants.HourlyRatesName, TotalPrice = 48.00 });

            var carParkCalculatorService = new CarParkFeesCalculatorService(packageRateCalculatorService, hourlyRateCalculatorService);

            // Act
            var actual = carParkCalculatorService.CalculateCarParkFees(carParkingTimeInputDto);

            // Assert
            packageRateCalculatorService.Received(1).CalculatePackageRate(Arg.Any<DateTime>(), Arg.Any<DateTime>());
            hourlyRateCalculatorService.Received(1).CalculateHourlyRate(Arg.Any<DateTime>(), Arg.Any<DateTime>());
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Errors.Count);
            Assert.AreEqual(Constants.WeekendPackageRateName, actual.PackageName);
            Assert.AreEqual(10.00, actual.TotalPrice);
        }
    }
}
