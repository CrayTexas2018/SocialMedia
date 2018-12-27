using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Models;
using User.Interfaces;
using User.Users;

namespace Front.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("/api/users/chunk/{id}")]
        public async Task<JsonResult> GetUserChunk(long id)
        {
            // Get actor
            IUser u = UserActorManager.createActor(id);
            return new JsonResult(await u.GetUserChunk());
        }

        [HttpGet("/api/users/main/{id}")]
        public async Task<JsonResult> GetUserMain(long id)
        {
            // Get actor
            IUser u = UserActorManager.createActor(id);
            return new JsonResult(await u.GetUserMain());
        }

        [HttpGet("/api/users/extended/{id}")]
        public async Task<JsonResult> GetExtendedUser(long id)
        {
            // Get actor
            IUser u = UserActorManager.createActor(id);
            return new JsonResult(await u.GetExtendedUser());
        }

        [HttpGet("/api/users/model/{id}")]
        public async Task<JsonResult> GetUserModel(long id)
        {
            // Get actor
            IUser u = UserActorManager.createActor(id);
            return new JsonResult(await u.GetUserModel());
        }

        // POST: api/Users
        [HttpPost]
        public async Task<JsonResult> Post([FromBody] UserModel userModel)
        {
            UserModel newUser = await UserActorManager.createUser(userModel);

            // Validate data
            if (!UserModel.userIsValid(newUser))
            {
                // Throw error
            }
            
            // Send to database to create userId

            // Create actor and save data
            ActorId actorId = new ActorId(userModel.UserChunk.userId);
            IUser u = ActorProxy.Create<IUser>(actorId, new Uri("fabric:/SocialMedia/UserActorService"));
            await u.CreateUser(newUser);           

            return new JsonResult(newUser);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task Put(long id)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpGet("{id}")]
        public async Task Delete(long id)
        {

        }
    }
}
