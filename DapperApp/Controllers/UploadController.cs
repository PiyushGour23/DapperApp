using DapperApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace DapperApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostenvironment;

        public UploadController(IWebHostEnvironment webHostenvironment)
        {
            _webHostenvironment = webHostenvironment ?? throw new ArgumentNullException(nameof(webHostenvironment));
        }


        [HttpPut("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file, string imagecode)
        {
            APIResponse response = new APIResponse();
            try
            {
                string Filepath = GetFilePath(imagecode);
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                string imagepath = Filepath + "\\" + imagecode + ".jpg";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (FileStream stream = System.IO.File.Create(imagepath))
                {
                    await file.CopyToAsync(stream);
                    response.ResponseCode = 200;
                    response.Result = "Uploaded Successfully";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 404;
            }
            return Ok(response);
        }

        [HttpPut("UploadMultipleImage")]
        public async Task<IActionResult> MultipleUploadImage(IFormFileCollection filecollection, string imagecode)
        {
            APIResponse response = new APIResponse();
            int passcount = 0;
            int errorcount = 0;
            try
            {
                string Filepath = GetFilePath(imagecode);
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                foreach (var file in filecollection)
                {
                    string imagepath = Filepath + "\\" +file.FileName;
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await file.CopyToAsync(stream);
                        passcount++;
                    }
                }

            }
            catch (Exception ex)
            {
                errorcount++;
                response.Error = ex.Message;
            }
            response.ResponseCode = 200;
            response.Result = passcount + " Uploaded file " + errorcount + " Failed to upload ";
            return Ok(response);
        }



        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage(string imagecode)
        {
            string ImageURL = string.Empty;
            string hostURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = GetFilePath(imagecode);
                string imagepath = Filepath + "\\" + imagecode + ".jpg";
                if (System.IO.File.Exists(imagepath))
                {
                    ImageURL = hostURL + "/Upload/image4/" + imagecode + ".jpg";
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(ImageURL);
        }


        [NonAction]
        private string GetFilePath(string imagecode)
        {
            return _webHostenvironment.WebRootPath + "\\Upload\\image" + imagecode;
        }
    }
 }


