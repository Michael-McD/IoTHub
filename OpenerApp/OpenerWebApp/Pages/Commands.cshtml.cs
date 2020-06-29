using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OpenerService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace OpenerWebApp.Pages
{
    public class CommandsModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly string serviceConnStr;

        public CommandsModel(IConfiguration config, IHostEnvironment env)
        {
            if (env.IsProduction())
            {
                serviceConnStr = config["ServiceConnStr"];
            }
            else
            {
                var secrets = config.GetSection("Opener.Models.SecretOptions").Get<Secrets>();
                serviceConnStr = secrets.ServiceConnStr;
            }
            this.config = config;
        }

        public void OnGet()
        {
        }

        [Microsoft.AspNetCore.Mvc.NonAction]
        public IActionResult OnPostDoorAction(string data)
        {           
            var device = new IoTService(serviceConnStr);

            switch (data)
            {    case "up":                    
                    device.DoorUp();
                    break;
                case "down":
                    device.DoorDown();
                    break;
                case "stop":
                    device.DoorStop();
                    break;
                default:
                    return BadRequest();
            }
            
            return Page();
        }
    }
}
