using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Interfaces;
using POS.Infraestructura.Commons.Bases.Request;

namespace POS.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderApplication _providerApplication;

        public ProviderController(IProviderApplication providerApplication)
        {
            _providerApplication = providerApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListProvider([FromBody] BaseFiltersRequest filters)
        {
            var response = await _providerApplication.ListProvider(filters);
            return Ok(response);
        }

        [HttpGet("{providerId:int}")]
        public async Task<IActionResult> ProviderById(int providerId)
        {
            var response = await _providerApplication.ProviderById(providerId);
            return Ok(response);    
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterProvider([FromBody] ProviderRequestDto request)
        {
            var response = await _providerApplication.RegisterProvider(request);
            return Ok(response);
        }
        [HttpPut("Edit/{providerId:int}")]
        public async Task<IActionResult> EditProvider(int providerId,[FromBody] ProviderRequestDto requestDto)
        {
            var response = await _providerApplication.EditProvider(providerId, requestDto);
            return Ok(response);
        }
        [HttpPut("Remove/{providerId:int}")]
        public async Task<IActionResult> RemoveCategory(int providerId)
        {
            var response = await _providerApplication.RemoveProvider(providerId);
            return Ok(response);
        }

    }
}
