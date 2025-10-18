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
}