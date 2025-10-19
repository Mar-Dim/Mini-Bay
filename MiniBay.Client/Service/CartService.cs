using Blazored.LocalStorage;
using MiniBay.Application.DTO;
using MiniBay.Shared.Feature.Products;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniBay.Client.Services
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CartService
    {
        private readonly ILocalStorageService _localStorage;
        private const string CartKey = "userCart";

        public event Action? OnChange;

        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task AddToCartAsync(ProductDto product)
        {
            var cart = await GetCartItemsAsync();
            var cartItem = cart.FirstOrDefault(item => item.ProductId == product.Id_Pro);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem { ProductId = product.Id_Pro, Quantity = 1 });
            }

            await _localStorage.SetItemAsync(CartKey, cart);
            NotifyDataChanged();
        }

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>(CartKey);
            return cart ?? new List<CartItem>();
        }

        public async Task RemoveFromCartAsync(int productId)
        {
            var cart = await GetCartItemsAsync();
            var cartItem = cart.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync(CartKey, cart);
                NotifyDataChanged();
            }
        }

        public async Task UpdateQuantityAsync(int productId, int quantity)
        {
            var cart = await GetCartItemsAsync();
            var cartItem = cart.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.Quantity = quantity;
                }
                else
                {
                    cart.Remove(cartItem);
                }

                await _localStorage.SetItemAsync(CartKey, cart);
                NotifyDataChanged();
            }
        }

        public async Task ClearCartAsync()
        {
            await _localStorage.RemoveItemAsync(CartKey);
            NotifyDataChanged();
        }

        private void NotifyDataChanged() => OnChange?.Invoke();
    }
}