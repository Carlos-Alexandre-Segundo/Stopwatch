using ConsoleApp1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StopwatchApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class StopwatchController : ControllerBase
    {
        private readonly StopwatchTimer _stopwatch;

        public StopwatchController(StopwatchTimer stopwatch)
        {
            _stopwatch = stopwatch;
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new
            {
                state = _stopwatch.StateName,
                elapsedTime = _stopwatch.GettingTimeElapsed()
            });
        }

        [HttpPost("start")]
        public IActionResult Start()
        {
            _stopwatch.Start();
            return Ok(new { message = "Stopwatch started." });
        }

        [HttpPost("pause")]
        public IActionResult Pause()
        {
            _stopwatch.Pause();
            return Ok(new { message = "Stopwatch paused." });
        }

        [HttpPost("resume")]
        public IActionResult Resume()
        {
            _stopwatch.Resume();
            return Ok(new { message = "Stopwatch resumed." });
        }

        [HttpPost("reset")]
        public IActionResult Reset()
        {
            _stopwatch.Reset();
            return Ok(new { message = "Stopwatch reset." });
        }
    }
}
