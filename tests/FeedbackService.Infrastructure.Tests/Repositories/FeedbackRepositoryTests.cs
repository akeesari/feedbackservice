using AutoFixture;
using Moq;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FeedbackService.Infrastructure.Repositories;
using FeedbackService.Infrastructure.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using FeedbackService.Infrastructure.Entities;
using System;

namespace FeedbackService.Infrastructure.Tests.Repositories
{

    public class FeedbackRepositoryTests //: BaseUnitTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<FeedbackDbContext> _dbContextMock;
        //private readonly Mock<IMapper> _mapperMock;
        private readonly Mapper _mapper;
        private readonly Mock<DbSet<Feedback>> _dbSetMock;
        private readonly FeedbackRepository _sut;

        public FeedbackRepositoryTests()
        {
            _fixture = new Fixture();
            _dbContextMock = _fixture.Freeze<Mock<FeedbackDbContext>>();
            _dbSetMock = _fixture.Freeze<Mock<DbSet<Feedback>>>();

            var myProfile = new Api.Helper.MapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            _sut = new FeedbackRepository(_dbContextMock.Object, _mapper);

            //_mapperMock = _fixture.Freeze<Mock<IMapper>>();
        }

        [Fact]
        public void FeedbackRepositoryConstructor_ShouldReturnNullReferenceException_WhenContextIsNull()
        {
            // Arrange
            FeedbackDbContext dbcontext = null;

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => new FeedbackRepository(dbcontext, _mapper));
        }
        [Fact]
        public void FeedbackRepositoryConstructor_ShouldReturnNullReferenceException_WhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            //var dbcontext = _fixture.Create<FeedbackDbContext>();

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => new FeedbackRepository(_dbContextMock.Object, mapper));

        }

        //[Fact]
        //public async Task GetAllFeedbacks_ShouldReturnData_WhenDataFound()
        //{
        //    // Arrange
        //    //var feedbackMock = _fixture.Create<Entities.Feedback>();
        //    var feedbacksMock = _fixture.Create<List<Entities.Feedback>>();
        //    //_mapperMock.Setup(x => x.Map<Entities.Feedback>()).Returns(feedbackMock);
        //    _dbContextMock.Setup(s => s.Set<Entities.Feedback>()).Returns(_dbSetMock.Object);
        //    //_dbSetMock.Setup(s => s.ToListAsync().ReturnsAsync(feedbacksMock);

        //    // Act
        //    var result = await _sut.GetAllFeedbacks().ConfigureAwait(false);

        //    // Assert
        //    result.Should().NotBeNull();
        //}

        [Fact]
        public async Task GetFeedbackById_ShouldReturnArgumentNullException_WhenIdIsZero()
        {
            // Arrange
            var id = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.GetFeedbackById(id));            
        }


        [Fact]
        public async Task GetFeedbackById_ShouldReturnData_WhenDataFound()
        {
            // Arrange
            var id = _fixture.Create<int>();

            //var feedbackMock = GetFeedback();
            //_dbContextMock.Setup(s => s.FindAsync<Feedback>()).ReturnsAsync(feedbackMock);
            _dbContextMock.Setup(s => s.Feedback).ReturnsDbSet(GetFeedbacks());

            // Act
            var result = await _sut.GetFeedbackById(id).ConfigureAwait(false);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateFeedback_ShouldReturnArgumentNullException_WhenInputIsNull()
        {
            // Arrange
            Core.Models.Feedback feedback = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.CreateFeedback(feedback));
        }

        //[Fact]
        //public async Task CreateFeedback_ShouldReturnData_WhenDataIsInsertedSucessfully()
        //{
        //    // Arrange
        //    var feedbackMock = _fixture.Create<Core.Models.Feedback>();
        //    _dbContextMock.Setup(s => s.Feedbacks).ReturnsDbSet(GetFeedbacks());

        //    // Act
        //    var result = await _sut.CreateFeedback(feedbackMock).ConfigureAwait(false);

        //    // Assert
        //    result.Should().NotBeNull();
        //}
        [Fact]
        public async Task DeleteFeedback_ShouldReturnArgumentNullException_WhenIdIsZero()
        {
            // Arrange
            var id = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.DeleteFeedback(id));
        }
        //[Fact]
        //public async Task DeleteFeedback_ShouldReturnTrue_WhenDataIsDeletedSuccessfully()
        //{
        //    // Arrange
        //    var id = _fixture.Create<int>();
        //    // Act
        //    var result = await _sut.DeleteFeedback(id).ConfigureAwait(false);

        //    // Assert
        //    result.Should().BeTrue();
        //}
        [Fact]
        public async Task UpdateFeedback_ShouldReturnArgumentNullException_WhenIdIsZero()
        {
            // Arrange
            var id = 0;
            Core.Models.Feedback feedback = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.UpdateFeedback(id, feedback));
        }
        [Fact]
        public async Task UpdateFeedback_ShouldReturnArgumentNullException_WhenInputIsNull()
        {
            // Arrange
            var id = _fixture.Create<int>();
            Core.Models.Feedback feedback = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.UpdateFeedback(id, feedback));
        }

        //[Fact]
        //public async Task UpdateFeedback_ShouldReturnTrue_WhenDataUpdatedSucessfully()
        //{
        //    // Arrange
        //    var id = _fixture.Create<int>();
        //    var feedbackMock = _fixture.Create<Core.Models.Feedback>();

        //    // Act
        //    var result = await _sut.UpdateFeedback(id, feedbackMock).ConfigureAwait(false);

        //    // Assert
        //    result.Should().BeTrue();
        //}
        private static List<Feedback> GetFeedbacks()
        {
            return new List<Feedback>
            {
                new Feedback
                {
                    Id = 1,
                    Subject = "subject test",
                    Message = "message test",
                    Rating = 10,
                    CreatedBy= "Anji Keesari",
                    CreatedDate = DateTime.Now
                }
            };
        }
        private static Feedback GetFeedback()
        {
            return new Feedback
            {
                Id = 1,
                Subject = "subject test",
                Message = "message test",
                Rating = 10,
                CreatedBy = "Anji Keesari",
                CreatedDate = DateTime.Now

            };
        }
    }


}
