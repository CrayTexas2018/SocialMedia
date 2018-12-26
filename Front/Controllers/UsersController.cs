using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Models;
using Newtonsoft.Json;
using User.Interfaces;

namespace Front.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/Users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        [HttpPost]
        public async Task<JsonResult> Post([FromBody] UserModel userModel)
        {
            // Create User Chunk
            UserChunk userChunk = new UserChunk
            {
                email = userModel.UserChunk.email,
            };

            // Create User Main
            UserMain userMain = new UserMain
            {
                bio = userModel.UserMain.bio,
                firstName = userModel.UserMain.firstName,
                handle = userModel.UserMain.handle,
                lastName = userModel.UserMain.lastName,
                totalFollowers = 0,
                totalLikes = 0,
                totalPosts = 0,
                userId = userChunk.userId
            };

            // Create Extened User
            ExtendedUser extendedUser = new ExtendedUser
            {
                birthday = userModel.ExtendedUser.birthday,
                created = DateTime.Now,
                password = ExtendedUser.hashPassword(userModel.ExtendedUser.password),
                phone = userModel.ExtendedUser.phone,
                updated = DateTime.Now,
                userId = userChunk.userId                
            };

            UserModel newUser = new UserModel
            {
                UserChunk = userChunk,
                UserMain = userMain,
                ExtendedUser = extendedUser
            };

            // Validate data
            if (!UserModel.userIsValid(newUser))
            {
                // Throw error
            }

            // Create actor and save data
            ActorId actorId = new ActorId(userModel.UserChunk.userId);
            IUser myActor = ActorProxy.Create<IUser>(actorId, new Uri("fabric:/SocialMedia/UserActorService"));
            await myActor.SaveUserInfo(newUser);

            // Send to database queue
            await myActor.sendToQueue("usercreated", JsonConvert.SerializeObject(newUser));

            return new JsonResult(newUser);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task Put(long id)
        {
            ActorId actorId = new ActorId(id);
            IUser myActor = ActorProxy.Create<IUser>(actorId, new Uri("fabric:/SocialMedia/UserActorService"));
            var asdf = await myActor.GetUserInfo();
        }

        // DELETE: api/ApiWithActions/5
        [HttpGet("{id}")]
        public async Task Delete(long id)
        {
            ActorId actorId = new ActorId(id);
            IUser myActor = ActorProxy.Create<IUser>(actorId, new Uri("fabric:/SocialMedia/UserActorService"));
            var asdf = await myActor.GetUserInfo();
        }
    }
}
