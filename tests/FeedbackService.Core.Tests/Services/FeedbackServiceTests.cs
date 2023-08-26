using AutoFixture;
using Moq;
using FluentAssertions;
using FeedbackService.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FeedbackService.Core.Services;
using FeedbackService.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;

namespace FeedbackService.Core.Tests.Services
{
    public class FeedbackServiceTests
    {
        private readonly IFixture _fixture;        
        private readonly Mock<IFeedbackRepository> _repositoryMock;
        private readonly Mock<ILogger<FeedbacksService>> _loggerMock;
        private readonly FeedbacksService _sut;
        public FeedbackServiceTests()
        {
            _fixture = new Fixture();
            _repositoryMock = _fixture.Freeze<Mock<IFeedbackRepository>>();
            _loggerMock = _fixture.Freeze<Mock<ILogger<FeedbacksService>>>();
            _sut = new FeedbacksService(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void FeedbacksServiceConstructor_ShouldReturnNullReferenceException_WhenRepositoryIsNull()
        {
            // Arrange
            IFeedbackRepository feedbackRepository= null;

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new FeedbacksService(feedbackRepository, _loggerMock.Object));
        }

        [Fact]
        public void FeedbacksServiceConstructor_ShouldReturnNullReferenceException_WhenLoggerIsNull()
        {
            // Arrange            
            ILogger<FeedbacksService> logger = null;

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new FeedbacksService(_repositoryMock.Object, logger));
        }

        [Fact]
        public async Task GetAllFeedbacks_ShouldReturnData_WhenDataFound()
        {
            // Arrange
            var feedbacksMock = _fixture.Create<IEnumerable<Feedback>>();
            _repositoryMock.Setup(x => x.GetAllFeedbacks()).ReturnsAsync(feedbacksMock);

            // Act
            var result = await _sut.GetAllFeedbacks().ConfigureAwait(false);

            // Assert
            //Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<Feedback>>();
            _repositoryMock.Verify(x => x.GetAllFeedbacks(), Times.Once());
        }
        [Fact]
        public async Task GetAllFeedbacks_ShouldReturnNull_WhenDataNotFound()
        {
            // Arrange
            IEnumerable<Feedback> feedbacksMock = null;
            _repositoryMock.Setup(x => x.GetAllFeedbacks()).ReturnsAsync(feedbacksMock);

            // Act
            var result = await _sut.GetAllFeedbacks().ConfigureAwait(false);

            // Assert
            result.Should().BeNull();
            _repositoryMock.Verify(x => x.GetAllFeedbacks(), Times.Once());
        }
        [Fact]
        public async Task GetFeedbackById_ShouldReturnData_WhenDataFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var feedbacksMock = _fixture.Create<Feedback>();
            _repositoryMock.Setup(x => x.GetFeedbackById(id)).ReturnsAsync(feedbacksMock);

            // Act
            var result = await _sut.GetFeedbackById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Feedback>();
            _repositoryMock.Verify(x => x.GetFeedbackById(id), Times.Once());
        }
        [Fact]
        public async Task GetFeedbackById_ShouldReturnNull_WhenDataNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            Feedback feedbackMock = null;
            _repositoryMock.Setup(x => x.GetFeedbackById(id)).ReturnsAsync(feedbackMock);

            // Act
            var result = await _sut.GetFeedbackById(id).ConfigureAwait(false);

            // Assert            
            result.Should().BeNull();
            _repositoryMock.Verify(x => x.GetFeedbackById(id), Times.Once());
        }
        [Fact]
        public void GetFeedbackById_ShouldThrowNullReferenceException_WhenInputIsEqualsZero()
        {
            // Arrange
            Feedback feedback = null; ;
            var id = 0;
            _repositoryMock.Setup(x => x.GetFeedbackById(id)).ReturnsAsync(feedback);

            // Act
            Func<Feedback> result = () => _sut.GetFeedbackById(id).Result;

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }
        [Fact]
        public async Task CreateFeedback_ShouldReturnData_WhenDataIsInsertedSucessfully()
        {
            // Arrange
            var feedbackMock = _fixture.Create<Feedback>();
            _repositoryMock.Setup(x => x.CreateFeedback(feedbackMock)).ReturnsAsync(feedbackMock);

            // Act
            var result = await _sut.CreateFeedback(feedbackMock).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Feedback>();
            _repositoryMock.Verify(x => x.CreateFeedback(feedbackMock), Times.Once());
        }
        [Fact]
        public void CreateFeedback_ShouldThrowNullReferenceException_WhenInputIsNull()
        {
            // Arrange
            var feedbackMock = _fixture.Create<Feedback>();
            Feedback request = null;
            _repositoryMock.Setup(x => x.CreateFeedback(feedbackMock)).ReturnsAsync(feedbackMock);

            // Act
            Func<Feedback> result = () => _sut.CreateFeedback(request).Result;

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }
        [Fact]
        public async Task DeleteFeedback_ShouldReturnTrue_WhenDataIsDeletedSuccessfully()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _repositoryMock.Setup(x => x.DeleteFeedback(id)).ReturnsAsync(true);

            // Act
            var result = await _sut.DeleteFeedback(id).ConfigureAwait(false);

            // Assert
            result.Should().BeTrue();
            _repositoryMock.Verify(x => x.DeleteFeedback(id), Times.Once());
        }
        [Fact]
        public async Task DeleteFeedback_ShouldReturnFalse_WhenDataNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _repositoryMock.Setup(x => x.DeleteFeedback(id)).ReturnsAsync(false);

            // Act
            var result = await _sut.DeleteFeedback(id).ConfigureAwait(false);

            // Assert
            result.Should().BeFalse();
            _repositoryMock.Verify(x => x.DeleteFeedback(id), Times.Once());
        }
        [Fact]
        public void DeleteFeedback_ShouldThrowNullReferenceException_WhenInputIsEqualsZero()
        {
            // Arrange
            var response = _fixture.Create<bool>();
            var id = 0;
            _repositoryMock.Setup(x => x.DeleteFeedback(id)).ReturnsAsync(response);

            // Act
            Func<bool> result = () => _sut.DeleteFeedback(id).Result;

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }
        [Fact]
        public async Task UpdateFeedback_ShouldReturnTrue_WhenDataUpdatedSucessfully()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var feedbackMock = _fixture.Create<Feedback>();
            _repositoryMock.Setup(x => x.UpdateFeedback(id, feedbackMock)).ReturnsAsync(true);

            // Act
            var result = await _sut.UpdateFeedback(id, feedbackMock).ConfigureAwait(false);

            // Assert
            result.Should().BeTrue();
            _repositoryMock.Verify(x => x.UpdateFeedback(id, feedbackMock), Times.Once());
        }
        [Fact]
        public async Task UpdateFeedback_ShouldReturnFalse_WhenDataNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var feedbackMock = _fixture.Create<Feedback>();
            _repositoryMock.Setup(x => x.UpdateFeedback(id, feedbackMock)).ReturnsAsync(false);

            // Act
            var result = await _sut.UpdateFeedback(id, feedbackMock).ConfigureAwait(false);

            // Assert
            result.Should().BeFalse();
            _repositoryMock.Verify(x => x.UpdateFeedback(id, feedbackMock), Times.Once());
        }
        [Fact]
        public void UpdateFeedback_ShouldThrowNullReferenceException_WhenInputIsEqualsZero()
        {
            // Arrange
            var id =0;
            var feedbackMock = _fixture.Create<Feedback>();
            _repositoryMock.Setup(x => x.UpdateFeedback(id, feedbackMock)).ReturnsAsync(false);

            // Act
            Func<bool> result = () => _sut.UpdateFeedback(id, feedbackMock).Result;

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }
        [Fact]
        public void UpdateFeedback_ShouldThrowNullReferenceException_WhenRequestIsNull()
        {
            // Arrange
            var id = 0;
            var feedbackMock = _fixture.Create<Feedback>();
            Feedback request = null;
            _repositoryMock.Setup(x => x.UpdateFeedback(id, feedbackMock)).ReturnsAsync(false);

            // Act
            Func<bool> result = () => _sut.UpdateFeedback(id, request).Result;

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }
    }
}
