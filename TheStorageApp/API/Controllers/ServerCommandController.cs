using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TheStorageApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerCommandController : ControllerBase
    {
        [HttpPost]
        [Route("ExecuteBashCommand")]
        public string ExecuteBashCommand([FromBody] string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }

        [HttpPost]
        [Route("ExecuteCMDCommand")]
        public async Task ExecuteCMDCommand([FromBody] string cmd)
        {
            try
            {
                var escapedArgs = cmd.Replace("\"", "\\\"");

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"C:\Windows\System32\cmd.exe",
                        Arguments = $"/c \"{escapedArgs}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                process.Start();
                this.HttpContext.Response.ContentType = "text/stream";
                var sourceStream = process.StandardOutput.BaseStream;
                await sourceStream.CopyToAsync(this.HttpContext.Response.Body);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        Stream stream;
        StreamWriter streamWriter;

        [HttpPost]
        [Route("ExecuteCMDCommand1")]
        public async Task ExecuteCMDCommand1([FromBody] string cmd)
        {
            try
            {
                streamWriter = new StreamWriter(this.HttpContext.Response.Body);

                var escapedArgs = cmd.Replace("\"", "\\\"");

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"C:\Windows\System32\cmd.exe",
                        Arguments = $"/c \"{escapedArgs}\"",
                        RedirectStandardOutput = false,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };


                this.HttpContext.Response.ContentType = "text/stream";
                this.HttpContext.Response.Body = process.StandardOutput.BaseStream;
                process.Start();
                //process.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
