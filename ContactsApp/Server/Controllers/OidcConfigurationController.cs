﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Server.Controllers
{
    [ApiController]
    public class OidcConfigurationController : ControllerBase
    {
        private readonly ILogger<OidcConfigurationController> _logger;

        public OidcConfigurationController(
            IClientRequestParametersProvider clientRequestParametersProvider,
            ILogger<OidcConfigurationController> logger)
        {
            ClientRequestParametersProvider = clientRequestParametersProvider;
            _logger = logger;
        }

        public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

        [HttpGet("_configuration/{clientId}")]
        public IActionResult GetClientRequestParamerters([FromRoute] string clientId)
        {
            var parameters = ClientRequestParametersProvider.GetClientParameters(
                HttpContext, clientId);
            return Ok(parameters);
        }
    }
}
