using FeedbackService.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackService.Core.Interfaces.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacks();
        Task<Feedback> GetFeedbackById(int id);
        Task<Feedback> CreateFeedback(Feedback feedback);
        Task<bool> DeleteFeedback(int id);
        Task<bool> UpdateFeedback(int id, Feedback feedback);
    }
}
