using AutoFixture;
using FeedbackService.Core.Models;
using System;
using Xunit;

namespace FeedbackService.Core.Tests.Models
{
    public class FeedbackTests
    {
        private readonly IFixture fixture;
        public FeedbackTests()
        {
            fixture = new Fixture();
        }
		[Fact]
		public void Id_ReturnAssignedValue()
		{
			// Arrange
			var id = fixture.Create<int>();

			// Act
			var sut = new Feedback
			{
				Id = id
			};

			// Assert
			Assert.Equal(id, sut.Id);
		}
		[Fact]
		public void Subject_ReturnAssignedValue()
		{
			// Arrange
			var subject = fixture.Create<string>();

			// Act
			var sut = new Feedback
			{
				Subject = subject
			};

			// Assert
			Assert.Equal(subject, sut.Subject);
		}
		[Fact]
		public void Message_ReturnAssignedValue()
		{
			// Arrange
			var message = fixture.Create<string>();

			// Act
			var sut = new Feedback
			{
				Message = message
			};

			// Assert
			Assert.Equal(message, sut.Message);
		}
		[Fact]
		public void Rating_ReturnAssignedValue()
		{
			// Arrange
			var rating = fixture.Create<int>();

			// Act
			var sut = new Feedback
			{
				Rating = rating
			};

			// Assert
			Assert.Equal(rating, sut.Rating);
		}
		[Fact]
		public void CreatedBy_ReturnAssignedValue()
		{
			// Arrange
			var createdBy = fixture.Create<string>();

			// Act
			var sut = new Feedback
			{
				CreatedBy = createdBy
			};

			// Assert
			Assert.Equal(createdBy, sut.CreatedBy);
		}
		[Fact]
		public void CreatedDate_ReturnAssignedValue()
		{
			// Arrange
			var createdDate = fixture.Create<DateTime>();

			// Act
			var sut = new Feedback
			{
				CreatedDate = createdDate
			};

			// Assert
			Assert.Equal(createdDate, sut.CreatedDate);
		}
	}
}
