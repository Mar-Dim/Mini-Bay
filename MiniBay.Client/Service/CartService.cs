<<<<<<< HEAD
using Blazored.LocalStorage;
using MiniBay.Application.DTO;
using MiniBay.Shared.Feature.Products;
using System.Text.Json;

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

        private void NotifyDataChanged() => OnChange?.Invoke();
    }
=======
// Services/CartService.cs
using MiniBay.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

public class CartService
{
    // Evento que se disparará cuando el carrito cambie
    public event Action OnChange;

    // Lista privada para almacenar los productos en el carrito
    private List<ProductDto> _cartItems = new List<ProductDto>();

    // Propiedad pública para acceder a los items (solo lectura)
    public IReadOnlyList<ProductDto> CartItems => _cartItems.AsReadOnly();

    /// <summary>
    /// Agrega un producto al carrito.
    /// </summary>
    /// <param name="product">El producto a agregar.</param>
    public void AddToCart(ProductDto product)
    {
        if (product == null) return;

        // Aquí podrías agregar lógica para manejar cantidades si un producto ya existe.
        // Por simplicidad, este ejemplo solo lo agrega a la lista.
        _cartItems.Add(product);
           NotifyStateChanged();
    }


    public int GetCartItemCount()
    {
        return _cartItems.Count;
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
>>>>>>> 5b15da8012310410ce09a3a05875fe923a4e0a80
}