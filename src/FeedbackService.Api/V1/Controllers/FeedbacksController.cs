using FeedbackService.Core.Interfaces.Services;
using FeedbackService.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FeedbackService.Api.V1.Controllers
{
    [Produces(Application.Json)]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService ?? throw new ArgumentNullException(nameof(feedbackService));
        }
        /// <summary>
        /// Get all the feedbacks.
        /// </summary>
        /// <returns>Feedbacks</returns>
        /// <remarks>        
        /// - Tables used. => Feedback
        /// - == Description ==
        /// - This endpoint will be used in Contact / Feedback UI screen
        /// - This endpoint will be accesable only for roles like Admin, developer etc.. 
        /// 
        /// </remarks>
        [HttpGet(Name = "GetFeedbacks")]
        //[SwaggerOperation("GetFeedbacks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            //throw new Exception($"Error while trying to call GetFeedbacks method.");
            var response = await _feedbackService.GetAllFeedbacks().ConfigureAwait(false);
            return response != null ? Ok(response) : NotFound();
        }

        /// <summary>
        /// Get feedback by Id.
        /// </summary>
        /// <param name="id">4</param>
        /// <returns>Feedback</returns>
        /// <remarks>        
        /// - Tables used. => Feedbacks
        /// </remarks>
        [HttpGet("{id}", Name = "GetFeedbackById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Feedback>> GetFeedbackById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var response = await _feedbackService.GetFeedbackById(id).ConfigureAwait(false);

            return response != null ? Ok(response) : NotFound();
        }

        /// <summary>
        /// Create feedback.
        /// </summary>
        /// <returns>Feedback</returns>       
        /// <remarks>        
        /// - Tables used. => Feedbacks
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Feedback>> CreateFeedback(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _feedbackService.CreateFeedback(feedback).ConfigureAwait(false);

            return CreatedAtRoute(nameof(GetFeedbackById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Delete feedback.
        /// </summary>
        /// <returns>true/false</returns>
        /// <remarks>        
        /// - Tables used. => Feedbacks
        /// </remarks>

        [HttpDelete("{id}", Name = "DeleteFeedback")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteFeedback(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var response = await _feedbackService.DeleteFeedback(id).ConfigureAwait(false);

            return response ? NoContent() : NotFound();
        }
        /// <summary>
        /// Update feedback.
        /// </summary>
        /// <returns>true/false</returns>
        /// <remarks>        
        /// - Tables used. => Feedbacks
        /// </remarks>

        [HttpPut("{id}", Name = "UpdateFeedback")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateFeedback(int id, Feedback feedback)
        {
            if (!ModelState.IsValid || id <= 0)
            {
                return BadRequest();
            }
            var response = await _feedbackService.UpdateFeedback(id, feedback).ConfigureAwait(false);
            return response ? Ok(response) : NotFound();
        }
    }
}
