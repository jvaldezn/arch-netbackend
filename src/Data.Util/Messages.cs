using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Util
{
    public static class Messages
    {
        public const string UnexpectedError = "Se produjo un error inesperado: {0}";

        public const string NoProductsFound = "No se encontraron productos.";
        public const string ProductNotFound = "El producto no existe.";
        public const string ProductCreated = "Producto creado exitosamente.";
        public const string ProductUpdated = "Producto actualizado exitosamente.";
        public const string ProductDeleted = "Producto eliminado exitosamente.";
        public const string ProductNoMatch = "Error, el id no coincide.";
        public const string ProductCreatedError = "Error al crear el producto.";
        public const string ProductUpdatedError = "Error al actualizar el producto con ID {0}";
        public const string ProductDeletedError = "Error al eliminar el producto con ID {0}";

        public const string NoUsersFound = "No se encontraron usuarios.";
        public const string UserNotFound = "El usuario no existe.";
        public const string UserCreated = "Usuario creado exitosamente.";
        public const string UserUpdated = "Usuario actualizado exitosamente.";
        public const string UserDeleted = "Usuario eliminado exitosamente.";
        public const string UserNoMatch = "Error, el id no coincide.";
        public const string UserCreatedError = "Error al crear el usuario.";
        public const string UserUpdatedError = "Error al actualizar el usuario con ID {0}";
        public const string UserDeletedError = "Error al eliminar el usuario con ID {0}";
        public const string UsernameAlreadyExists = "El nombre de usuario ya está en uso.";
        public const string EmailAlreadyExists = "El correo electrónico ya está en uso.";

        public const string InvalidCredentials = "Usuario o contraseña incorrectos.";

        public const string DidntSendInformationForConsume = "No se envio la información necesaria para realizar la operación solicitada.";
        public const string DidNotFindAnyResults = "No se encontraron resultados";
    }
}
