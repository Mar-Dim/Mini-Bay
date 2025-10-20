namespace MiniBay.Application.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string email) 
            : base($"El usuario con el correo '{email}' ya existe.") { }
    }
}