﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;
using Models;

[assembly: FabricTransportActorRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace User.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IUser : IActor
    {
        Task sendToQueue(string queueName, string data);

        // USER METHODS
        Task CreateUser(UserModel userModel);
        Task<UserChunk> GetUserChunk();
        Task<UserMain> GetUserMain();
        Task<ExtendedUser> GetExtendedUser();
        Task<UserModel> GetUserModel();

        // POST METHODS
        Task<Post> CreatePost(Post post);
        Task<Post> GetPost(string postId);
        Task DeletePost(string postId);

        //COMMENT METHODS
    }
}
