using Xunit;
using System.Reflection;
using System.Linq;
using ConsoleApp1; // O namespace do teu projeto principal

namespace ConsoleApp1.Tests
{
    public class ArquiteturaTests
    {
        [Fact]
        public void Model_NaoDeveTerDependenciasParaViewOuController()
        {
            // Arrange
            var modelType = typeof(Model);
            var viewType = typeof(View);
            var controllerType = typeof(Controller);

            // Act & Assert 1: O Model não deve ter campos/variáveis do tipo View ou Controller
            var campos = modelType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var campo in campos)
            {
                Assert.NotEqual(viewType, campo.FieldType);
                Assert.NotEqual(controllerType, campo.FieldType);
            }

            // Act & Assert 2: O Model não deve ter Propriedades do tipo View ou Controller
            var propriedades = modelType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var prop in propriedades)
            {
                Assert.NotEqual(viewType, prop.PropertyType);
                Assert.NotEqual(controllerType, prop.PropertyType);
            }

            // Act & Assert 3: Os métodos do Model não devem receber nem devolver View ou Controller
            var metodos = modelType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var metodo in metodos)
            {
                // Verifica o tipo de retorno
                Assert.NotEqual(viewType, metodo.ReturnType);
                Assert.NotEqual(controllerType, metodo.ReturnType);

                // Verifica os parâmetros de entrada
                var parametros = metodo.GetParameters();
                foreach (var param in parametros)
                {
                    Assert.NotEqual(viewType, param.ParameterType);
                    Assert.NotEqual(controllerType, param.ParameterType);
                }
            }
        }
    }
}