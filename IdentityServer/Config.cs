// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("photos_app", new []
                {
                    "role", "subscription", "testing"
                })
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("photos_service", "Сервис фотографий")
            };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                new Client
                {
                    ClientId = "Photos App by OAuth",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "photos_service" }
                },
                new Client
                {
                    ClientId = "Photos App by OIDC",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    // NOTE: использовать ли Proof Key for Code Exchange
                    // Это полезно для SPA и мобильных приложений, где client secret хранится у пользователя
                    RequirePkce = true,
                    // NOTE: показывать ли пользователю страницу consent со списком запрошенных разрешений
                    RequireConsent = true,

                    // NOTE: куда отправлять после логина
                    RedirectUris = { "https://localhost:5001/signin-passport" },

                    // NOTE: куда предлагать перейти после логаута
                    PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-passport" },

                    AllowedScopes = new List<string>
                    {
                        // NOTE: Позволяет запрашивать id token
                        IdentityServerConstants.StandardScopes.OpenId,
                        // NOTE: Позволяет запрашивать профиль пользователя через id token
                        IdentityServerConstants.StandardScopes.Profile,
                        // NOTE: Позволяет запрашивать email пользователя через id token
                        IdentityServerConstants.StandardScopes.Email,
                        "photos_app",
                        "photos_service"
                    },

                    // NOTE: Надо ли добавлять информацию о пользователе в id token при запросе одновременно
                    // id token и access token, как это происходит в code flow.
                    // Либо придется ее получать отдельно с user info endpoint.
                    AlwaysIncludeUserClaimsInIdToken = true,
                }
            };
    }
}