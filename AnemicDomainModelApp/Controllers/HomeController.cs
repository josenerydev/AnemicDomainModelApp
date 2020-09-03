using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AnemicDomainModelApp.Models;
using AnemicDomainModelApp.Data;
using AnemicDomainModelApp.Domain;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;
using System.Text.Json;
using MediatR;
using AnemicDomainModelApp.Service.Commands;

namespace AnemicDomainModelApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WmsContext _context;
        private readonly IMediator _mediator;
        public HomeController(ILogger<HomeController> logger, WmsContext context, IMediator mediator)
        {
            _logger = logger;
            _context = context;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            //var dto = new UpdateProductCommandDto(1, "Açucar Mascavo", 999M, 2, 3);
            //var dto = new RegisterProductCommandDto("Café no bule", 10M, 1, 2);
            //{
            //    Id = 1,
            //    Description = "Arroz da Nery",
            //    NetWeight = 1000M,
            //    ProductStatusId = 2,
            //    UnitId = 4
            //};
            //var dto = new UpdatePackingCommandDto(1, 500M, 1, 1, 2);
            var validationResult = await _mediator.Send(new DeletePackingCommand(1));
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
