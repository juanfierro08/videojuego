using Castle.DynamicProxy;
using System;

namespace GameMaster
{
    // [PARADIGMA ORIENTADO A ASPECTOS]: Interceptor para Logging Automático.
    // Registra la entrada y salida de los métodos de los servicios.
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n[LOG - AOP] Entrando a {invocation.Method.Name} en {invocation.TargetType.Name}");
            Console.ResetColor();

            // Ejecuta el método real
            invocation.Proceed(); 

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[LOG - AOP] Saliendo de {invocation.Method.Name}");
            Console.ResetColor();
        }
    }
}
