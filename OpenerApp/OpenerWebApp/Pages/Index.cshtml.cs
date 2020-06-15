﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OpenerService;
using Microsoft.Extensions.Configuration;

namespace OpenerWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Secrets secrets;
        public string Txt;

        public IndexModel(IConfiguration config)
        {            
            secrets = config.GetSection("Opener.Models.SecretOptions").Get<Secrets>();
        }

        public void OnGet()
        {
            string connStr = secrets.ServiceConnStr ?? "Not Found";
            Txt = $"Secret Conn Str: {connStr}";
        }

        [Microsoft.AspNetCore.Mvc.NonAction]
        public IActionResult OnPostDoorAction(string data)
        {           
            var device = new IoTService(secrets.ServiceConnStr);

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
            
            return new RedirectToPageResult("Index");
        }
    }
}
