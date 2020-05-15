using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bothniabladet.Data;
using Bothniabladet.Models.Checkout;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bothniabladet.Services
{
    public class CheckoutService
    {
        AppDbContext _context;
        readonly ILogger _logger;
        //constructor
        public CheckoutService(AppDbContext context, ILoggerFactory factory)
        {
            _context = context;
            _logger = factory.CreateLogger<ImageService>();

        }


    }
}
