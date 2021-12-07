﻿using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Domain.Consts;
using WebStore.Domain.Types;

namespace WebStore.Data.Mocks.UserMock
{
    public class UserMock : IUserMock
    {
        private readonly AppDbContext db;
        private readonly IValidator<User> userValidator;
        private readonly UserManager<User> userManager;

        public UserMock(AppDbContext db, IValidator<User> userValidator, UserManager<User> userManager)
        {
            this.db = db;
            this.userValidator = userValidator;
            this.userManager = userManager;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await db.Users.AnyAsync(cancellationToken))
                return true;

            List<Order> selectedOrders = await db.Orders.Where(order => order.Address.PostalCode == "602267").ToListAsync(cancellationToken);

            var admin = new User(
                    userName: "kakawkawww13",
                    orderHistory: new OrderHistory(selectedOrders.Take(1).ToList()),
                    listFavourites: new FavoritesList(new List<FavoritesListProduct>()),
                    cart: new Cart(new List<CartProduct>()),
                    email: "kakawkawww13@mail.ru",
                    dateTimeCreation: DateTime.Now
                )
            {
                DateOfBirth = new DateTime(2002, 11, 24),
                PhoneNumber = "79157675803",
                Gender = GenderType.Man,
                Firstname = "Александр",
                Surname = "Андрианов",
                Lastname = "Евгеньевич",
                Address = new Address("Россия", "Муром", "Ленина", "55а", "602267"),
            };
            var user = new User(
                    userName: "kakawkawww17",
                    orderHistory: new OrderHistory(selectedOrders.Skip(1).Take(2).ToList()),
                    listFavourites: new FavoritesList(new List<FavoritesListProduct>()),
                    cart: new Cart(new List<CartProduct>()),
                    email: "kakawkawww17@mail.ru",
                    dateTimeCreation: DateTime.Now
                );


            await userValidator.ValidateAndThrowAsync(admin, cancellationToken);
            await userValidator.ValidateAndThrowAsync(user, cancellationToken);

            IdentityResult adminIdentityCreateResult = await userManager.CreateAsync(admin, "21081990wwwWWW");
            IdentityResult userIdentityCreateResult = await userManager.CreateAsync(user, "79157734732wwwWWW");
            if (!adminIdentityCreateResult.Succeeded)
                throw new NotImplementedException($"Ошибка при добавление администратора c электронной почтой {admin.Email}, подробнее об ошибках: {string.Join("; ", adminIdentityCreateResult.Errors)}");
            if (!userIdentityCreateResult.Succeeded)
                throw new NotImplementedException($"Ошибка при добавление пользователя с электронной почтой {user.Email}, подробнее об ошибках: {string.Join("; ", userIdentityCreateResult.Errors)}");

            var receivedAdmin = await userManager.Users.SingleAsync(user => user.Email == "kakawkawww13@mail.ru", cancellationToken);
            var receivedUser = await userManager.Users.SingleAsync(user => user.Email == "kakawkawww17@mail.ru", cancellationToken);

            IdentityResult adminIdentityAddToRoleResult = await userManager.AddToRoleAsync(receivedAdmin, RoleConst.Admin);
            IdentityResult userIdentityAddToRoleResult = await userManager.AddToRoleAsync(receivedUser, RoleConst.User);
            if (!adminIdentityAddToRoleResult.Succeeded)
                throw new NotImplementedException($"Ошибка при добавление администратору роли администратора c электронной почтой {receivedAdmin.Email}, подробнее об ошибках: {string.Join("; ", adminIdentityAddToRoleResult.Errors)}");
            if (!userIdentityAddToRoleResult.Succeeded)
                throw new NotImplementedException($"Ошибка при добавление пользователю роли пользователя c электронной почтой {receivedUser.Email}, подробнее об ошибках: {string.Join("; ", userIdentityAddToRoleResult.Errors)}");

            IdentityResult adminIdentityConfrimEmailResult = await userManager.ConfirmEmailAsync(receivedAdmin, await userManager.GenerateEmailConfirmationTokenAsync(receivedAdmin));
            IdentityResult userIdentityConfrimEmailResult = await userManager.ConfirmEmailAsync(receivedUser, await userManager.GenerateEmailConfirmationTokenAsync(receivedUser));
            if (!adminIdentityConfrimEmailResult.Succeeded)
                throw new NotImplementedException($"Ошибка при подтверждения почты администратора c электронной почтой {receivedAdmin.Email}, подробнее об ошибках: {string.Join("; ", adminIdentityConfrimEmailResult.Errors)}");
            if (!userIdentityConfrimEmailResult.Succeeded)
                throw new NotImplementedException($"Ошибка при подтверждения почты пользователя c электронной почтой {receivedUser.Email}, подробнее об ошибках: {string.Join("; ", userIdentityConfrimEmailResult.Errors)}");

            var adminClaims = new Claim[]
            {
                new Claim(ClaimTypes.Role,RoleConst.Admin, ClaimValueTypes.String),
                new Claim(ClaimTypes.GivenName,receivedUser.UserName, ClaimValueTypes.String),
                new Claim(ClaimTypes.Email,receivedUser.Email, ClaimValueTypes.Email),
            };
            var userClaims = new Claim[]
            {
                new Claim(ClaimTypes.Role,RoleConst.Admin, ClaimValueTypes.String),
                new Claim(ClaimTypes.GivenName,receivedUser.UserName, ClaimValueTypes.String),
                new Claim(ClaimTypes.Email,receivedUser.Email, ClaimValueTypes.Email),
            };

            IdentityResult adminIdentityAddClaimsResult = await userManager.AddClaimsAsync(receivedAdmin, adminClaims);
            IdentityResult userIdentityAddClaimsResult = await userManager.AddClaimsAsync(receivedUser, userClaims);
            if (!adminIdentityAddClaimsResult.Succeeded)
                throw new NotImplementedException($"Ошибка при добавление администратору утверждения c электронной почтой {receivedAdmin.Email}, подробнее об ошибках: {string.Join("; ", adminIdentityAddClaimsResult.Errors)}");
            if (!userIdentityAddClaimsResult.Succeeded)
                throw new NotImplementedException($"Ошибка при добавление пользователю утверждения c электронной почтой {receivedUser.Email}, подробнее об ошибках: {string.Join("; ", userIdentityAddClaimsResult.Errors)}");

            return true;
        }
    }
}
