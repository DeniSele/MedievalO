using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using Service;

public class LoginClient
{
    private readonly Users.UsersClient client;
    private readonly Channel channel;
    private readonly string server = "users-service-medieval.herokuapp.com:18154";

    internal LoginClient()
    {
        channel = new Channel(server, ChannelCredentials.Insecure);
        client = new Users.UsersClient(channel);
    }


    internal User CreateUser(string email, string username, string password)
    {
        CreateUserRequest createUser = new CreateUserRequest
        {
            Email = email,
            Name = username,
            Password = password
        };

        var response = client.Create(createUser);
        return response.Result;
    }


    private void OnDisable()
    {
        channel.ShutdownAsync().Wait();
    }

}
