using FluentValidation;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Data.Identity;
using WebStore.Data.Repositories.AppIdentityUserRepository;
using WebStore.Data.Repositories.OrderRepository;
using WebStore.Domain.Consts;
using WebStore.Services.RoleService;
using WebStore.Services.UserService;

namespace WebStore.Data.Mocks.UserMock
{
    public class UserMock : IUserMock
    {
        private readonly IUserRepository userRepository;
        private readonly IValidator<User> userValidator;
        private readonly IOrderRepository orderRepository;
        private readonly IRoleService roleService;
        private readonly IUserService userService;

        public UserMock(IUserRepository userRepository, IValidator<User> userValidator, IOrderRepository orderRepository,
            IRoleService roleService, IUserService userService)
        {
            this.userRepository = userRepository;
            this.userValidator = userValidator;
            this.orderRepository = orderRepository;
            this.roleService = roleService;
            this.userService = userService;
        }

        public async ValueTask<bool> InitAsync(CancellationToken cancellationToken = default)
        {
            if (await userRepository.AnyAsync(cancellationToken))
            {
                return await new ValueTask<bool>(true);
            }

            var orders = await orderRepository.GetAllAsync(order => order.Address.PostalCode == "602267", false, cancellationToken);

            var users = new User[]
            {
                new User("Александр","Андрианов","Евгеньевич",new OrderHistory(orders.Take(1)),
                new FavoritesList(Enumerable.Empty<FavoritesListProduct>()),
                new Cart(Enumerable.Empty<CartProduct>()),
                "kakawkawww13@mail.ru","21081990wwwWWW",DateTime.Now,"79157675803"),

                new User("Роман","Тарасов","Юрьевич",new OrderHistory(orders.Skip(1).Take(2)),
                new FavoritesList(Enumerable.Empty<FavoritesListProduct>()),
                new Cart(Enumerable.Empty<CartProduct>()),
                "kakawkawww17@mail.ru","21081990wwwWWW",DateTime.Now,"79157675803"),
            };

            users.Select(async user => await userValidator.ValidateAndThrowAsync(user, cancellationToken));

            await userRepository.AddRangeAsync(users, cancellationToken);

            users.Select(async user => user switch
            {
                User { Email: "kakawkawww13@mail.ru" } => await roleService.AddToRoleAsync(user, RoleConst.Admin, cancellationToken),
                User { Email: "kakawkawww17@mail.ru" } => await roleService.AddToRoleAsync(user, RoleConst.User, cancellationToken),
                _ => throw new NotImplementedException("Данного пользователя не существует"),
            });

            users.Select(async user => user switch
            {
                User { Email: "kakawkawww13@mail.ru" } => await userService
                .ConfirmEmailAsync(user, await userService
                .GenerateEmailConfirmationTokenAsync(user, cancellationToken), cancellationToken),
                User { Email: "kakawkawww17@mail.ru" } => await userService
                .ConfirmEmailAsync(user, await userService
                .GenerateEmailConfirmationTokenAsync(user, cancellationToken), cancellationToken),
                _ => throw new NotImplementedException("Данного пользователя не существует"),
            });

            var adminClaims = new Claim[]
            {
                new Claim(ClaimTypes.Role,RoleConst.Admin),
                new Claim(ClaimTypes.GivenName,"GuardianDead"),
                new Claim(ClaimTypes.DateOfBirth,"24.11.2002"),
                new Claim(ClaimTypes.Country,"Россия"),
                new Claim(ClaimTypes.Gender,"Мужской"),
            };
            var userClaims = new Claim[]
            {
                new Claim(ClaimTypes.Role,RoleConst.User),
                new Claim(ClaimTypes.GivenName,"HaUKoK"),
                new Claim(ClaimTypes.DateOfBirth,"24.11.2006"),
                new Claim(ClaimTypes.Country,"Россия"),
                new Claim(ClaimTypes.Gender,"Мужской"),
            };

            users.Select(async user => user switch
            {
                User { Email: "kakawkawww13@mail.ru" } => await userService.AddClaimsAsync(user, adminClaims, cancellationToken),
                User { Email: "kakawkawww17@mail.ru" } => await userService.AddClaimsAsync(user, userClaims, cancellationToken),
                _ => throw new NotImplementedException("Данного пользователя не существует"),
            });

            return await new ValueTask<bool>(true);
        }
    }
}
