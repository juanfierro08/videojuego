using Castle.DynamicProxy;
using System;

namespace GameMaster
{
    // [PARADIGMA ORIENTADO A ASPECTOS]: Interceptor para Validación y Manejo Centralizado de Errores.
    // Captura cualquier excepción no controlada en la capa de persistencia y la reporta limpiamente.
    public class ErrorHandlingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                // Intenta ejecutar el método real
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ERROR CENTRALIZADO - AOP] Ocurrió una excepción en {invocation.Method.Name}: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
