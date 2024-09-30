using CsvHelper;
using ForkliftControlSystem.Application.DTOs;
using ForkliftControlSystem.Application.Services;
using ForkliftControlSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;

namespace ForkliftControlSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForkliftController : ControllerBase
    {
        private readonly IForkliftService _forkliftService;
        private readonly string? _uploadFolder;

        public ForkliftController(IForkliftService forkliftService, IConfiguration configuration)
        {
            _forkliftService = forkliftService;
            _uploadFolder = configuration["UploadFolder"];
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForklifts()
        {
            try
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), _uploadFolder);

                if (!Directory.Exists(uploadPath))
                {
                    return NotFound("Upload folder does not exist.");
                }

                var csvFiles = Directory.GetFiles(uploadPath, "*.csv");

                if (csvFiles.Length == 0)
                {
                    return NotFound("No CSV files found in the uploads folder.");
                }

                var allForklifts = new List<ForkliftDTO>();

                foreach (var file in csvFiles)
                {
                    
                    allForklifts.AddRange(await _forkliftService.GetAllForklifts(file));
                }

                return Ok(allForklifts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error reading files: {ex.Message}");
            }
        }

       

        [HttpPost("import")]
        public async Task<IActionResult> ImportForklifts(IFormFile file)
        {
            try
            {

                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Create the upload folder if it doesn't exist
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), _uploadFolder);

                // Check if the upload folder exists, if not create it
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Get the full file path
                var filePath = Path.Combine(uploadPath, file.FileName);


                // Check if file already exists
                if (System.IO.File.Exists(filePath))
                {
                    return StatusCode(StatusCodes.Status302Found, $"File '{file.FileName}' already exists.");
                }

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { message = "Forklifts imported and saved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing file: {ex.Message}");
            }
        }

        [HttpPost("executeCommands")]
        public IActionResult ExecuteForkliftCommands([FromBody] ForkliftCommandDTO commandDto)
        {
            try
            {
                var actions = _forkliftService.ParseForkliftCommands(commandDto.Command);
                return Ok(actions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
