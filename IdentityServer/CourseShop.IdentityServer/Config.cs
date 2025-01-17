﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace CourseShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                 new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
                 new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
                 new ApiResource("resource_basket"){Scopes={"basket_fullpermission"}},
                 new ApiResource("resource_discount"){Scopes={"discount_fullpermission"}},
                 new ApiResource("resource_order"){Scopes={"order_fullpermission"}},
                 new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
            };
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                 new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){ Name="roles", DisplayName="Roles", Description="Kullanıcı rolleri", UserClaims=new []{ "role"} }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","Catalog Api için full erişim"),
                new ApiScope("photo_stock_fullpermission","Photo Stock Api için full erişim"),
                new ApiScope("basket_fullpermission","Basket API için full erişim"),
                new ApiScope("discount_fullpermission","Discount API için full erişim"),
                new ApiScope("order_fullpermission","Order API için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "WebMvcClient",
                    ClientName = "WebMvcClient Project",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "catalog_fullpermission" , "photo_stock_fullpermission" , IdentityServerConstants.LocalApi.ScopeName}
                },
                new Client
                {
                    ClientName="Asp.Net Core MVC",
                    ClientId="WebMvcClientForUser",
                    AllowOfflineAccess=true,
                    ClientSecrets= {new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256())},
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={  "basket_fullpermission"
                                    ,"order_fullpermission"
                                    ,"discount_fullpermission"
                                    ,"gateway_fullpermission"
                                    ,IdentityServerConstants.StandardScopes.Email
                                    ,IdentityServerConstants.StandardScopes.OpenId
                                    ,IdentityServerConstants.StandardScopes.Profile
                                    ,IdentityServerConstants.StandardScopes.OfflineAccess
                                    ,IdentityServerConstants.LocalApi.ScopeName
                                    ,"roles" 
                    },
                    AccessTokenLifetime=1*60*60,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime= (int) (DateTime.Now.AddDays(60)- DateTime.Now).TotalSeconds,
                    RefreshTokenUsage= TokenUsage.ReUse
                },
                new Client
                {
                    ClientName="Token Exchange Client",
                    ClientId="TokenExhangeClient",
                    ClientSecrets= {new Secret("secret".Sha256())},
                    AllowedGrantTypes= new []{ "urn:ietf:params:oauth:grant-type:token-exchange" },
                    AllowedScopes={ "discount_fullpermission", "payment_fullpermission", IdentityServerConstants.StandardScopes.OpenId }
                },
            };
    }
}