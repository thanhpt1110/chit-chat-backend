using ChitChat.Application.MachineLearning.Services.Interface;
using ChitChat.DataAccess.Repositories.Interface;

using Microsoft.AspNetCore.Mvc;

namespace ChitChat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingModelController : ControllerBase
    {
        private readonly ITrainingModelService _trainingModelService;
        public TrainingModelController(ITrainingModelService trainingModelService
            , IUserRepository userRepository)
        {
            _trainingModelService = trainingModelService;
        }
        //[HttpGet]
        //[Route("{userId}")]
        //public async Task<IActionResult> GetRecommendationPostModel(Guid userId)
        //{
        //    var userInteractionModelItems = await _trainingModelService.GetRecommendationPostModel(userId.ToString());
        //    return Ok(userInteractionModelItems);
        //}
    }
}
