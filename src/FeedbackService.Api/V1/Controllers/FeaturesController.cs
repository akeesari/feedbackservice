using ProductService.Core.Interfaces.Services;
using ProductService.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ProductService.Api.V1.Controllers
{
    [Produces(Application.Json)]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeaturesController(IFeatureService featureservice)
        {
            _featureService = featureservice ?? throw new ArgumentNullException(nameof(featureservice));
        }
        /// <summary>
        /// Get all features.
        /// </summary>
        /// <returns>Features</returns>
        /// <remarks>        
        /// - Tables used. => Feature
        /// - == Description ==
        /// - This endpoint will be used in Features UI screen        
        /// 
        /// </remarks>
        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Feature>>> GetAll()
        {
            var response = await _featureService.GetAll().ConfigureAwait(false);
            return response != null ? Ok(response) : NotFound();
        }
        /// <summary>
        /// Get feature by Id.
        /// </summary>
        /// <param name="id">1</param>
        /// <returns>Feature</returns>
        /// <remarks>        
        /// - Tables used. => Feature, FeatureDetails
        /// </remarks>
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Feature>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var response = await _featureService.GetById(id).ConfigureAwait(false);

            return response != null ? Ok(response) : NotFound();
        }
       
        /// <summary>
        /// Get feature details by feature Id.
        /// </summary>
        /// <param name="id">1</param>
        /// <returns>FeatureDetails</returns>
        /// <remarks>        
        /// - Tables used. => FeatureDetails
        /// </remarks>
        [HttpGet("{id}/details", Name = "GetFeatureDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FeatureDetails>>> GetFeatureDetailsByFeatureId(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var response = await _featureService.GetFeatureDetailsByFeatureId(id).ConfigureAwait(false);

            return response != null ? Ok(response) : NotFound();
        }
    }
}
