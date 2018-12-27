using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using User.Interfaces;
using Models;
using User.Database;

namespace User
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class User : Actor, IUser
    {
        /// <summary>
        /// Initializes a new instance of User
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public User(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        #region Queue
        public Task sendToQueue(string queueName, string data)
        {
            return ServiceBusHelper.SendMessageAsync(queueName, data);
        }
        #endregion

        #region User
        public Task<UserChunk> GetUserChunk()
        {
            return StateManager.GetStateAsync<UserChunk>("UserChunk");
        }

        public Task<UserMain> GetUserMain()
        {
            return StateManager.GetStateAsync<UserMain>("UserMain");
        }

        public Task<ExtendedUser> GetExtendedUser()
        {
            return StateManager.GetStateAsync<ExtendedUser>("ExtendedUser");
        }

        public Task<UserModel> GetUserModel()
        {
            UserModel m = new UserModel
            {
                UserChunk = GetUserChunk().Result,
                UserMain = GetUserMain().Result,
                ExtendedUser = GetExtendedUser().Result
            };

            return Task.FromResult(m);
        }

        public Task CreateUser(UserModel userModel)
        {
            StateManager.SetStateAsync("UserChunk", userModel.UserChunk);
            StateManager.SetStateAsync("UserMain", userModel.UserMain);
            StateManager.SetStateAsync("ExtendedUser", userModel.ExtendedUser);

            return Task.FromResult(true);
        }        
        #endregion

        #region Posts
        public Task<Post> CreatePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Task DeletePost(string postId)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPost(string postId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
