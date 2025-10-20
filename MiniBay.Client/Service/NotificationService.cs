using System;
using System.Timers;

public class NotificationService : IDisposable
{
    // Evento que se dispara para mostrar una notificación
    public event Action<string, NotificationType> OnShow;

    // Evento que se dispara para ocultar la notificación
    public event Action OnHide;

    private System.Timers.Timer _timer;

    /// <summary>
    /// Muestra una notificación con un mensaje y un tipo.
    /// </summary>
    /// <param name="message">El texto a mostrar.</param>
    /// <param name="type">El tipo de notificación (Success, Error, etc.).</param>
    /// <param name="durationInMilliseconds">Cuánto tiempo se mostrará la notificación.</param>
    public void ShowNotification(string message, NotificationType type = NotificationType.Success, int durationInMilliseconds = 4000)
    {
        OnShow?.Invoke(message, type);

        // Inicia un temporizador para ocultar la notificación automáticamente
        StartHideTimer(durationInMilliseconds);
    }

    private void StartHideTimer(int durationInMilliseconds)
    {
        // Si ya hay un temporizador, lo detenemos para empezar de nuevo
        _timer?.Stop();
        _timer?.Dispose();

        _timer = new System.Timers.Timer(durationInMilliseconds);
        _timer.Elapsed += (sender, e) => OnHide?.Invoke();
        _timer.AutoReset = false;
        _timer.Start();
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}

/// <summary>
/// Define los tipos de notificaciones para aplicar diferentes estilos CSS.
/// </summary>
public enum NotificationType
{
    Success,
    Error,
    Info,
    Warning
}