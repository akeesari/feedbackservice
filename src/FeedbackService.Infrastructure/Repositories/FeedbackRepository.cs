using AutoMapper;
using FeedbackService.Core.Interfaces.Repositories;
using FeedbackService.Core.Models;
using FeedbackService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackService.Infrastructure.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FeedbackDbContext _dbcontext;
        private readonly IMapper _mapper;
        public FeedbackRepository(FeedbackDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            var feedbacks = await _dbcontext.Feedback.ToListAsync().ConfigureAwait(false);
            if (feedbacks != null)
            {
                return _mapper.Map<IEnumerable<Feedback>>(feedbacks);
            }
            return null;
        }
        public async Task<Feedback> GetFeedbackById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var feedback = await _dbcontext.Feedback.FindAsync(id);
            if (feedback != null)
            {
                return _mapper.Map<Feedback>(feedback);
            }
            return null;
        }
        public async Task<Feedback> CreateFeedback(Feedback feedback)
        {
            if (feedback == null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }

            var dbFeedback = _mapper.Map<Entities.Feedback>(feedback);
            await _dbcontext.AddAsync(dbFeedback);
            await _dbcontext.SaveChangesAsync();
            feedback.Id = dbFeedback.Id;
            return feedback;
        }
        public async Task<bool> DeleteFeedback(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            // Find the Feedback to be deleted in the repository
            var feedbackToDelete = await _dbcontext.Feedback.FindAsync(id);

            // If there is no such Feedback, return an error response with status code 404 (Not Found)
            if (feedbackToDelete == null)
            {
                return false;
            }
            // Remove the Feedback from the repository
            // The DeleteFeedback method returns true if the Feedback was successfully deleted

            if (feedbackToDelete != null)
            {
                //Delete that feedback
                _dbcontext.Entry(feedbackToDelete).State = EntityState.Modified;
                _dbcontext.Feedback.Remove(feedbackToDelete);
                //Commit the transaction
                await _dbcontext.SaveChangesAsync();
                // Return a response message with status code 204 (No Content), To indicate that the operation was successful
                return true;
            }
            // Otherwise return a 400 (Bad Request) error response
            return false;
        }
        public async Task<bool> UpdateFeedback(int id, Feedback feedback)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (feedback == null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }
            var feedbackToUpdate = await _dbcontext.Feedback.FindAsync(id);

            if (feedbackToUpdate == null)
            {
                return false;
            }

            if (feedbackToUpdate == null || feedbackToUpdate.Id != id)
            {
                return false;
            }
            _dbcontext.Entry(feedbackToUpdate).State = EntityState.Modified;
            feedbackToUpdate.Subject = feedback.Subject;
            feedbackToUpdate.Message = feedback.Message;
            feedbackToUpdate.Rating = feedback.Rating;
            feedbackToUpdate.CreatedBy = feedback.CreatedBy;
            feedbackToUpdate.CreatedDate = DateTime.Now;

            if (feedback != null)
            {
                //Update that feedback
                _dbcontext.Feedback.Update(feedbackToUpdate);
                //Commit the transaction
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
