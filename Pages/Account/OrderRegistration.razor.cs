using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Data.Entities;
using WebStore.Domain.Types;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Pages.Account
{
    public class OrderRegistrationBase : ComponentBase
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public AppDbContext Db { get; set; }
        [Inject] public IValidator<OrderRegistrationViewModel> OrderRegistrationViewModelValidator { get; set; }
        [Inject] public IValidator<Order> OrderValidator { get; set; }
        [Inject] public ITrackNumberService TrackNumberService { get; set; }

        public bool IsEmailInputValid { get; set; } = true;
        public bool IsPasswordInputValid { get; set; } = true;

        public decimal deliveryCost;
        public decimal productsCost;
        public User currentUser;
        public ClaimsPrincipal currentUserState;
        public OrderRegistrationViewModel OrderRegistrationViewModel { get; set; } = new OrderRegistrationViewModel()
        {
            Address = new Address()
        };
        public List<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();


        public List<Delivery> allDeliviries;
        public List<Delivery> selectedDeliveries;
        public DeliveryMethodType[] AllDeliveryMethods = Enum.GetValues<DeliveryMethodType>();
        public PaymentMethodType[] AllPaymentMethods = Enum.GetValues<PaymentMethodType>();

        protected override async Task OnInitializedAsync()
        {
            currentUserState = (await AuthenticationStateTask).User;
            string userEmail = currentUserState.Claims.Single(claim => claim.Type == ClaimTypes.Email).Value;
            currentUser = await Db.Users
                .Include(user => user.OrderHistory.Orders)
                .Include(user => user.Cart.Products)
                    .ThenInclude(cartProduct => cartProduct.Article.Model)
                .SingleOrDefaultAsync(user => user.Email == userEmail);
            productsCost = currentUser.Cart.Products
                .Where(cartProduct => cartProduct.IsSelected)
                .Sum(cartProduct => cartProduct.Article.Model.Price * cartProduct.Count);
            allDeliviries = await Db.Deliveries.ToListAsync();
        }

        public void SelectPaymentMethod(PaymentMethodType paymentMethod)
        {
            Errors.Clear();
            OrderRegistrationViewModel.PaymentMethodType = paymentMethod;
        }
        public void SelectDeliveryMethod(DeliveryMethodType deliveryMethod)
        {
            Errors.Clear();
            OrderRegistrationViewModel.DeliveryMethod = deliveryMethod;
            selectedDeliveries = allDeliviries.Where(delivery => delivery.DeliveryMethod == OrderRegistrationViewModel.DeliveryMethod).ToList();
            OrderRegistrationViewModel.Delivery = null;
            deliveryCost = 0;
        }
        public void SelectDelivery(Delivery delivery)
        {
            Errors.Clear();
            OrderRegistrationViewModel.Delivery = delivery;
            deliveryCost = delivery.Cost;
        }

        public async Task SubmitAsync(EditContext editContext)
        {
            Errors.Clear();

            bool editContextValidateResult = editContext.Validate();
            ValidationResult validateResult = await OrderRegistrationViewModelValidator.ValidateAsync(OrderRegistrationViewModel);
            if (editContextValidateResult)
                Errors.AddRange(editContext.GetValidationMessages().Select(error => new ValidationFailure("Form", error)));
            if (!validateResult.IsValid)
                Errors.AddRange(validateResult.Errors);

            if (validateResult.IsValid && editContextValidateResult)
                await CreateOrderAsync();
        }
        public async Task CreateOrderAsync()
        {
            List<OrderProduct> addedOrderProducts = new List<OrderProduct>();
            List<CartProduct> selectedCartProducts = currentUser.Cart.Products.Where(cartProduct => cartProduct.IsSelected).ToList();

            foreach (var cartProduct in selectedCartProducts)
            {
                var addedProducts = await Db.Products
                    .Include(product => product.Article.Model.Subcategory.Category)
                    .Where(product => product.Article.Id == cartProduct.Article.Id)
                    .Take(cartProduct.Count)
                    .ToListAsync();
                Db.Products.RemoveRange(addedProducts);
                addedOrderProducts.AddRange(addedProducts.ConvertAll(product => new OrderProduct(product)));
            }

            var order = new Order()
            {
                Address = OrderRegistrationViewModel.Address,
                CustomerFullName = OrderRegistrationViewModel.FullName,
                DateTimeCreation = DateTime.Now,
                Email = OrderRegistrationViewModel.Email,
                PaymentMethod = OrderRegistrationViewModel.PaymentMethodType.Value,
                PhoneNumber = OrderRegistrationViewModel.PhoneNumber,
                Products = addedOrderProducts,
                Status = OrderStatusType.AwaitingProcessing,
                TotalCost = productsCost + deliveryCost,
                TrackNumber = TrackNumberService.GenerateTrackNumber(),
                Delivery = OrderRegistrationViewModel.Delivery
            };

            await OrderValidator.ValidateAndThrowAsync(order);

            currentUser.OrderHistory.Orders.Add(order);
            currentUser.Cart.Products.RemoveAll(cartProduct => cartProduct.IsSelected);
            Db.Products.RemoveRange(order.Products.ConvertAll(orderProduct => orderProduct.Product));

            if (await Db.SaveChangesAsync() != -1)
                NavigationManager.NavigateTo($"{NavigationManager.BaseUri}account/orders", true);
            else
                Errors.Add(new ValidationFailure("DB", "Неудалось создать заказ по неизвестной ошибке"));
        }
    }
}
