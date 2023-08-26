using AutoFixture;
using FeedbackService.Api.V1.Controllers;
using FeedbackService.Core.Interfaces.Services;
using FeedbackService.Core.Models;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackService.Api.Tests.V1.Controllers
{
    public class FeedbacksControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IFeedbackService> _serviceMock;
        private readonly FeedbacksController _sut;        

        public FeedbacksControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IFeedbackService>>();
            _sut = new FeedbacksController(_serviceMock.Object);//creates the implementation in-memory
        }

        [Fact]
        public void FeedbacksControllerConstructor_ShouldReturnNullReferenceException_WhenServiceIsNull()
        {
            // Arrange
            IFeedbackService feedbackService = null;

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new FeedbacksController(feedbackService));
        }

        [Fact]
        public async Task GetFeedbacks_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var feedbacksMock = _fixture.Create<IEnumerable<Feedback>>();
            _serviceMock.Setup(x => x.GetAllFeedbacks()).ReturnsAsync(feedbacksMock);

            // Act
            var result = await _sut.GetFeedbacks().ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<Feedback>>>();
            //result.Result.Should().BeAssignableTo<OkResult>(); //Technically there is no difference between the two approaches.
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(feedbacksMock.GetType());
            _serviceMock.Verify(x => x.GetAllFeedbacks(), Times.Once());
        }
        [Fact]
        public async Task GetFeedbacks_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            List<Feedback> response = null;
            _serviceMock.Setup(x => x.GetAllFeedbacks()).ReturnsAsync(response);

            // Act
            var result = await _sut.GetFeedbacks().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            _serviceMock.Verify(x => x.GetAllFeedbacks(), Times.Once());
        }
        [Fact]
        public async Task GetFeedbackById_ShouldReturnOkResponse_WhenValidInput()
        {
            // Arrange
            var feedbackMock = _fixture.Create<Feedback>();
            var id = _fixture.Create<int>();
            _serviceMock.Setup(x => x.GetFeedbackById(id)).ReturnsAsync(feedbackMock);

            // Act
            var result = await _sut.GetFeedbackById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Feedback>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(feedbackMock.GetType());
            _serviceMock.Verify(x => x.GetFeedbackById(id), Times.Once());
        }
        [Fact]
        public async Task GetFeedbackById_ShouldReturnNotFound_WhenNoDataFound()
        {
            // Arrange
            Feedback response = null;
            var id = _fixture.Create<int>();
            _serviceMock.Setup(x => x.GetFeedbackById(id)).ReturnsAsync(response);

            // Act
            var result = await _sut.GetFeedbackById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            _serviceMock.Verify(x => x.GetFeedbackById(id), Times.Once());
        }
        [Fact]
        public async Task GetFeedbackById_ShouldReturnBadRequest_WhenInputIsEqualsZero()
        {
            // Arrange
            var response = _fixture.Create<Feedback>();
            int id = 0;

            // Act
            var result = await _sut.GetFeedbackById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
        }
        [Fact]
        public async Task GetFeedbackById_ShouldReturnBadRequest_WhenInputIsLessThanZero()
        {
            // Arrange
            var response = _fixture.Create<Feedback>();
            int id = -1;

            // Act
            var result = await _sut.GetFeedbackById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
        }
        [Fact]
        public async Task CreateFeedback_ShouldReturnOkResponse_WhenValidRequest()
        {
            // Arrange
            var request = _fixture.Create<Feedback>();
            var response = _fixture.Create<Feedback>();
            _serviceMock.Setup(x => x.CreateFeedback(request)).ReturnsAsync(response);

            // Act
            var result = await _sut.CreateFeedback(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Feedback>>();
            result.Result.Should().BeAssignableTo<CreatedAtRouteResult>();
            _serviceMock.Verify(x => x.CreateFeedback(response), Times.Never());
        }
        [Fact]
        public async Task CreateFeedback_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Arrange
            var request = _fixture.Create<Feedback>();
            _sut.ModelState.AddModelError("Subject", "The Subject field is required.");
            var response = _fixture.Create<Feedback>();

            // Act
            var result = await _sut.CreateFeedback(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
        }
        [Fact]
        public async Task DeleteFeedback_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange
            var id = _fixture.Create<int>();            
            _serviceMock.Setup(x => x.DeleteFeedback(id)).ReturnsAsync(true);

            // Act
            var result = await _sut.DeleteFeedback(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();

        }
        [Fact]
        public async Task DeleteFeedback_ShouldReturnNotFound_WhenRecordNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _serviceMock.Setup(x => x.DeleteFeedback(id)).ReturnsAsync(false);

            // Act
            var result = await _sut.DeleteFeedback(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
        }
        [Fact]
        public async Task DeleteFeedback_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange
            int id = 0;
            _serviceMock.Setup(x => x.DeleteFeedback(id)).ReturnsAsync(false);

            // Act
            var result = await _sut.DeleteFeedback(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.DeleteFeedback(id), Times.Never());
        }
        [Fact]
        public async Task UpdateFeedback_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange
            int id = 0;
            var request = _fixture.Create<Feedback>();            

            // Act
            var result = await _sut.UpdateFeedback(id, request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
        }
        [Fact]
        public async Task UpdateFeedback_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var request = _fixture.Create<Feedback>();
            _sut.ModelState.AddModelError("Subject", "The Subject field is required.");
            var response = _fixture.Create<Feedback>();
            _serviceMock.Setup(x => x.UpdateFeedback(id, request)).ReturnsAsync(false);


            // Act
            var result = await _sut.UpdateFeedback(id, request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.UpdateFeedback(id, request), Times.Never());
        }
        [Fact]
        public async Task UpdateFeedback_ShouldReturnOkResponse_WhenRecordIsUpdated()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var request = _fixture.Create<Feedback>();
            _serviceMock.Setup(x => x.UpdateFeedback(id, request)).ReturnsAsync(true);

            // Act
            var result = await _sut.UpdateFeedback(id, request).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            _serviceMock.Verify(x => x.UpdateFeedback(id, request), Times.Once());

        }
        [Fact]
        public async Task UpdateFeedback_ShouldReturnNotFound_WhenRecordNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var request = _fixture.Create<Feedback>();
            _serviceMock.Setup(x => x.UpdateFeedback(id, request)).ReturnsAsync(false);

            // Act
            var result = await _sut.UpdateFeedback(id, request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _serviceMock.Verify(x => x.UpdateFeedback(id, request), Times.Once());
        }
        [Fact]
        public async Task TestMethodName_WhatshouldHappens_WhenScenario()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
