using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
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

            var orders = db.Orders.Where(order => order.Address.PostalCode == "602267");

            var admin = new User(
                    address: new Address("Россия", "Муром", "Мечникова", "55", "602267"),
                    userName: "kakawkawww13",
                    firstname: "Александр",
                    surname: "Андрианов",
                    lastname: "Евгеньевич",
                    orderHistory: new OrderHistory(orders.Take(1)),
                    listFavourites: new FavoritesList(Enumerable.Empty<FavoritesListProduct>()),
                    cart: new Cart(Enumerable.Empty<CartProduct>()),
                    email: "kakawkawww13@mail.ru",
                    dateTimeCreation: DateTime.Now,
                    gender: UserGenderType.Man,
                    dateOfBirth: new DateTime(2002, 11, 24),
                    phoneNumber: "79157675803"
                );
            var user = new User(
                    address: new Address("Россия", "Муром", "Ленина", "55а", "602267"),
                    userName: "kakawkawww17",
                    firstname: "Роман",
                    surname: "Тарасов",
                    lastname: "Юрьевич",
                    orderHistory: new OrderHistory(orders.Skip(1).Take(2)),
                    listFavourites: new FavoritesList(Enumerable.Empty<FavoritesListProduct>()),
                    cart: new Cart(Enumerable.Empty<CartProduct>()),
                    email: "kakawkawww17@mail.ru",
                    dateTimeCreation: DateTime.Now,
                    dateOfBirth: new DateTime(2002, 11, 12),
                    gender: UserGenderType.Man,
                    phoneNumber: "79157675803"
                );


            await userValidator.ValidateAndThrowAsync(admin, cancellationToken);
            await userValidator.ValidateAndThrowAsync(user, cancellationToken);

            var adminIdentityCreateResult = await userManager.CreateAsync(admin, "21081990wwwWWW");
            var userIdentityCreateResult = await userManager.CreateAsync(user, "79157734732wwwWWW");
            if (!adminIdentityCreateResult.Succeeded)
                throw new NotImplementedException($"Ошибка при добавление администратора c электронной почтой {admin.Email}, подробнее об ошибках: {string.Join("; ", adminIdentityCreateResult.Errors)}");
            if (!userIdentityCreateResult.Succeeded)
                throw new NotImplementedException($"Ошибка при добавление пользователя с электронной почтой {user.Email}, подробнее об ошибках: {string.Join("; ", userIdentityCreateResult.Errors)}");

            var receivedAdmin = await userManager.Users.SingleAsync(user => user.Email == "kakawkawww13@mail.ru", cancellationToken);
            var receivedUser = await userManager.Users.SingleAsync(user => user.Email == "kakawkawww17@mail.ru", cancellationToken);

            var adminIdentityAddToRoleResult = await userManager.AddToRoleAsync(receivedAdmin, RoleConst.Admin);
            var userIdentityAddToRoleResult = await userManager.AddToRoleAsync(receivedUser, RoleConst.User);
            if (!adminIdentityAddToRoleResult.Succeeded)
                throw new NotImplementedException($"Ошибка при добавление администратору роли администратора c электронной почтой {receivedAdmin.Email}, подробнее об ошибках: {string.Join("; ", adminIdentityAddToRoleResult.Errors)}");
            if (!userIdentityAddToRoleResult.Succeeded)
                throw new NotImplementedException($"Ошибка при добавление пользователю роли пользователя c электронной почтой {receivedUser.Email}, подробнее об ошибках: {string.Join("; ", userIdentityAddToRoleResult.Errors)}");

            var adminIdentityConfrimEmailResult = await userManager.ConfirmEmailAsync(receivedAdmin, await userManager.GenerateEmailConfirmationTokenAsync(receivedAdmin));
            var userIdentityConfrimEmailResult = await userManager.ConfirmEmailAsync(receivedUser, await userManager.GenerateEmailConfirmationTokenAsync(receivedUser));
            if (!adminIdentityConfrimEmailResult.Succeeded)
                throw new NotImplementedException($"Ошибка при подтверждения почты администратора c электронной почтой {receivedAdmin.Email}, подробнее об ошибках: {string.Join("; ", adminIdentityConfrimEmailResult.Errors)}");
            if (!userIdentityConfrimEmailResult.Succeeded)
                throw new NotImplementedException($"Ошибка при подтверждения почты пользователя c электронной почтой {receivedUser.Email}, подробнее об ошибках: {string.Join("; ", userIdentityConfrimEmailResult.Errors)}");

            var adminClaims = new Claim[]
            {
                new Claim(ClaimTypes.StreetAddress, string.Join(", ",receivedAdmin.Address), ClaimValueTypes.String),
                new Claim(ClaimTypes.Surname, receivedAdmin.Surname, ClaimValueTypes.String),
                new Claim(ClaimTypes.PostalCode, receivedAdmin.Address.PostalCode, ClaimValueTypes.String),
                new Claim(ClaimTypes.MobilePhone,receivedAdmin.PhoneNumber, ClaimValueTypes.String),
                new Claim(ClaimTypes.Role,RoleConst.Admin, ClaimValueTypes.String),
                new Claim(ClaimTypes.GivenName,receivedAdmin.UserName, ClaimValueTypes.String),
                new Claim(ClaimTypes.DateOfBirth,receivedAdmin.DateOfBirth.ToString(), ClaimValueTypes.DateTime),
                new Claim(ClaimTypes.Country,receivedAdmin.Address.Country, ClaimValueTypes.String),
                new Claim(ClaimTypes.Gender,receivedAdmin.Gender.ToString(), ClaimValueTypes.String),
                new Claim(ClaimTypes.Email,receivedAdmin.Email, ClaimValueTypes.Email),
            };
            var userClaims = new Claim[]
            {
                new Claim(ClaimTypes.StreetAddress, string.Join(", ",receivedUser.Address), ClaimValueTypes.String),
                new Claim(ClaimTypes.Surname, receivedUser.Surname, ClaimValueTypes.String),
                new Claim(ClaimTypes.PostalCode, receivedUser.Address.PostalCode, ClaimValueTypes.String),
                new Claim(ClaimTypes.MobilePhone,receivedUser.PhoneNumber, ClaimValueTypes.String),
                new Claim(ClaimTypes.Role,RoleConst.Admin, ClaimValueTypes.String),
                new Claim(ClaimTypes.GivenName,receivedUser.UserName, ClaimValueTypes.String),
                new Claim(ClaimTypes.DateOfBirth,receivedUser.DateOfBirth.ToString(), ClaimValueTypes.DateTime),
                new Claim(ClaimTypes.Country,receivedUser.Address.Country, ClaimValueTypes.String),
                new Claim(ClaimTypes.Gender,receivedUser.Gender.ToString(), ClaimValueTypes.String),
                new Claim(ClaimTypes.Email,receivedUser.Email, ClaimValueTypes.Email),
            };

            var adminIdentityAddClaimsResult = await userManager.AddClaimsAsync(receivedAdmin, adminClaims);
            var userIdentityAddClaimsResult = await userManager.AddClaimsAsync(receivedUser, userClaims);
            if (!adminIdentityAddClaimsResult.Succeeded)
                throw new NotImplementedException($"Ошибка при добавление администратору утверждения c электронной почтой {receivedAdmin.Email}, подробнее об ошибках: {string.Join("; ", adminIdentityAddClaimsResult.Errors)}");
            if (!userIdentityAddClaimsResult.Succeeded)
                throw new NotImplementedException($"Ошибка при добавление пользователю утверждения c электронной почтой {receivedUser.Email}, подробнее об ошибках: {string.Join("; ", userIdentityAddClaimsResult.Errors)}");

            return true;
        }
    }
}
