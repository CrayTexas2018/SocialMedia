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

        public Task<UserModel> GetUserInfo()
        {
            return this.StateManager.GetStateAsync<UserModel>("userinfo");
        }

        public Task SaveUserInfo(UserModel userModel)
        {
            return this.StateManager.SetStateAsync<UserModel>("userinfo", userModel);
        }

        public Task sendToQueue(string queueName, string data)
        {
            return ServiceBusHelper.SendMessageAsync(queueName, data);
        }
    }
}
