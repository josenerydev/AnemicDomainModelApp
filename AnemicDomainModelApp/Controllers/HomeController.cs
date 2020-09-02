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
            var dto = new RegisterProductCommandDto
            {
                Description = "Arroz da Samiry",
                NetWeight = 200M,
                ProductStatusId = 1,
                UnitId = 1
            };

            await _mediator.Send(new RegisterProductCommand(dto));

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
