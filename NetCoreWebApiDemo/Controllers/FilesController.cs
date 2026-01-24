using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreWebApiDemo.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public FilesController(IWebHostEnvironment env) => _env = env;

        private string UploadsRoot => Path.Combine(_env.ContentRootPath, "uploads");

        private static string SanitizeFileName(string fileName)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                fileName = fileName.Replace(c, '_');
            return fileName;
        }

        private static bool IsAllowedExtension(string fileName)
        {
            var allowed = new[] { ".jpg", ".png", ".pdf", ".zip" };
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return allowed.Contains(ext);
        }

        // Buffered: Tek dosya upload
        [HttpPost("upload-single")]
        [RequestSizeLimit(50_000_000)]
        public async Task<IActionResult> UploadSingle(IFormFile file, CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0) return BadRequest("Dosya boş.");
            if (!IsAllowedExtension(file.FileName)) return BadRequest("Geçersiz dosya türü.");

            Directory.CreateDirectory(UploadsRoot);

            var safeName = $"{Guid.NewGuid():N}_{SanitizeFileName(file.FileName)}";
            var fullPath = Path.Combine(UploadsRoot, safeName);

            await using var fs = System.IO.File.Create(fullPath);
            await file.CopyToAsync(fs, cancellationToken);

            return Ok(new { savedAs = safeName, size = file.Length });
        }

        // Buffered: Çoklu upload
        [HttpPost("upload-multiple")]
        [RequestSizeLimit(150_000_000)]
        public async Task<IActionResult> UploadMultiple(List<IFormFile> files, CancellationToken cancellationToken)
        {
            if (files is null || files.Count == 0) return BadRequest("Dosya yok.");

            Directory.CreateDirectory(UploadsRoot);
            var results = new List<object>();

            foreach (var file in files)
            {
                if (file.Length == 0) { results.Add(new { file = file.FileName, error = "Boş" }); continue; }
                if (!IsAllowedExtension(file.FileName)) { results.Add(new { file = file.FileName, error = "Tür" }); continue; }

                var safeName = $"{Guid.NewGuid():N}_{SanitizeFileName(file.FileName)}";
                var fullPath = Path.Combine(UploadsRoot, safeName);

                await using var fs = System.IO.File.Create(fullPath);
                await file.CopyToAsync(fs, cancellationToken);

                results.Add(new { file = file.FileName, savedAs = safeName, size = file.Length });
            }

            return Ok(results);
        }

        // Download
        [HttpGet("download/{fileName}")]
        public IActionResult Download(string fileName)
        {
            Directory.CreateDirectory(UploadsRoot);

            var safe = SanitizeFileName(fileName);
            var fullPath = Path.Combine(UploadsRoot, safe);

            if (!System.IO.File.Exists(fullPath)) return NotFound();

            var stream = System.IO.File.OpenRead(fullPath);
            return File(stream, "application/octet-stream", safe);
        }

        // Download + Range
        [HttpGet("download-range/{fileName}")]
        public IActionResult DownloadRange(string fileName)
        {
            Directory.CreateDirectory(UploadsRoot);

            var safe = SanitizeFileName(fileName);
            var fullPath = Path.Combine(UploadsRoot, safe);

            if (!System.IO.File.Exists(fullPath)) return NotFound();

            var stream = System.IO.File.OpenRead(fullPath);
            return File(stream, "application/octet-stream", safe, enableRangeProcessing: true);
        }

    }
}
