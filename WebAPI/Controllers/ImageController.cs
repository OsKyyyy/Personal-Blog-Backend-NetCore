using Business.Constant;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        [Route("UploadTemp")]
        [HttpPost]
        public async Task<ActionResult> UploadTempAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                var failures = new List<ValidationFailure>
                {
                    new ValidationFailure("Dosya", "Görsel alanı boş olamaz")
                };
                throw new ValidationException("Validation Error", failures);

                //return BadRequest(new { status = false, message = "Görsel seçilmedi!" });
            }

            // Boyut ve format kontrolü
            if (file.Length > 2 * 1024 * 1024)
            {
                var failures = new List<ValidationFailure>
                {
                    new ValidationFailure("DosyaBoyutu", "Görsel en fazla 2MB olmalıdır")
                };
                throw new ValidationException("Validation Error", failures);

                //return BadRequest(new { status = false, message = "Görsel boyutu 2MB'yi geçemez!" });
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                var failures = new List<ValidationFailure>
                {
                    new ValidationFailure("DosyaFormatı", "Sadece JPG, JPEG ve PNG formatları desteklenmektedir")
                };
                throw new ValidationException("Validation Error", failures);

                //return BadRequest(new { status = false, message = "Sadece JPG, JPEG, PNG formatları desteklenmektedir." });
            }

            // Geçici dizine kaydetme
            var tempFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp");
            Directory.CreateDirectory(tempFolder);

            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(tempFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"/uploads/temp/{fileName}";
            return Ok(new { status = true, url = imageUrl, message = "Görsel başarılı bir şekilde eklendi" });

        }

        [Route("DeleteTemp")]
        [HttpPost]
        public IActionResult DeleteTemp([FromBody] ImageDeleteRequest request)
        {
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", request.Url.TrimStart('/'));
            Uri uri = new Uri(request.Url);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", uri.AbsolutePath.TrimStart('/'));

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return Ok(new { status = true , message = "Görsel başarılı bir şekilde silindi" });
            }

            return BadRequest(new { status = false, message = "Görsel bulunamadı" });
        }

        public class ImageDeleteRequest
        {
            public string Url { get; set; }
        }
    }
}
