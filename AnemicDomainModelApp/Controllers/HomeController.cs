using AnemicDomainModelApp.Domain;
using AnemicDomainModelApp.Models;
using AnemicDomainModelApp.Service.Commands;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Diagnostics;
using System.Threading.Tasks;

namespace AnemicDomainModelApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        public HomeController(ILogger<HomeController> logger, WmsContext context, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var registerProductCommandDto = new RegisterProductCommandDto("Fubá", 999M, 2, 2);
            var result = await _mediator.Send(new RegisterProductCommand(registerProductCommandDto));
            //var registerPackingCommandDto = new RegisterPackingCommandDto(55, 2, 1, 1);
            //var validationResult = await _mediator.Send(new RegisterPackingCommand(registerPackingCommandDto));

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
