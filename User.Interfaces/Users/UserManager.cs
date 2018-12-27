using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Interfaces;

namespace User.Users
{
    public class UserActorManager
    {
        public static IUser createActor(long userId)
        {
            ActorId actorId = new ActorId(userId);
            return ActorProxy.Create<IUser>(actorId, new Uri("fabric:/SocialMedia/UserActorService"));
        }

        public static async Task<UserModel> createUser(UserModel userModel)
        {
            // Create User Chunk
            UserChunk userChunk = new UserChunk
            {
                handle = userModel.UserChunk.handle,
            };

            // Create User Main
            UserMain userMain = new UserMain
            {
                bio = userModel.UserMain.bio,
                firstName = userModel.UserMain.firstName,
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
                email = userModel.ExtendedUser.email,
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

            return await Task.FromResult(newUser);
        }
    }
}
