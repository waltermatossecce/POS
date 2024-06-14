using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS.Application.Dtos.Category.Request;
using POS.Application.Interfaces;
using POS.Utilities.Static;
using System.Threading.Tasks;

namespace POS.Test.Category
{
    [TestClass]
    public class CategoryApplicationTest
    {
        private static WebApplicationFactory<Program> _factory = null;
        
        //Nos a permitir recuperar y almacenar injeciones de dependencias
        private static IServiceScopeFactory? _scopeFactory = null;

        //definir o decorarla e iniciliciarse
        [ClassInitialize]
        public static void Initialize(TestContext _testContext)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
        }

        //metodos de pruebas
        [TestMethod]
        public async Task RegisterCategory_WhenSendingNullValuesOrEmpty_ValidationErrors()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();


            //Arrange
            var name = "";
            var description = "";
            var state = 1;
            var espected = ReplyMessage.MESSAGE_VALIDATE;

            //act = actuar

            var result = await context!.RegisterCategory(new CategoryRequestDto()
            {
                Name = name,
                Description = description,
                State = state,
            });
            var current = result.Message;
            //assert = va a validar o no valido
            Assert.AreEqual(espected, current);
        }
        [TestMethod]
        public async Task RegisterCategory_WebSendingCorrectValues_RegisteredSuccessfully()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();


            //Arrange
            var name = "Nuevo registro";
            var description = "Nueva descripción";
            var state = 1;
            var espected = ReplyMessage.MESSAGE_SAVE;

            //act = actuar

            var result = await context!.RegisterCategory(new CategoryRequestDto()
            {
                Name = name,
                Description = description,
                State = state,
            });
            var current = result.Message;
            //assert = va a validar o no valido
            Assert.AreEqual(espected, current);
        }

    }
}
