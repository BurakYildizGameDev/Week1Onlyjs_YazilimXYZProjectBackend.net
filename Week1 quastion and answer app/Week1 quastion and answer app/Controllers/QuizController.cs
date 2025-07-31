using Microsoft.AspNetCore.Mvc;
using Week1_question_and_answer_app.Services;
using Week1_question_and_answer_app.Dtos;

namespace Week1_question_and_answer_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Bu controller "api/quiz" rotasına karşılık gelir
    public class QuizController : ControllerBase
    {
        private readonly IQuestionService _service;

        public QuizController()
        {
            // Servis örneği oluşturuluyor şimdilik
            _service = new QuestionService();
        }

        // Kullanıcının IP adresine göre userId üretir
        private string GetUserId() => HttpContext.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

        [HttpGet("questions")]
        public IActionResult GetAll()
        {
            // Tüm soruları getirir
            return Ok(_service.GetAllQuestions());
        }

        [HttpGet("question/random")]
        public IActionResult GetRandom()
        {
            // Rastgele bir soru döner, kullanıcı IP'si ile eşleştirilir
            var question = _service.GetRandomQuestion(GetUserId());
            return Ok(question);
        }

        // Cevap istemi için kullanılacak iç sınıf => post işlemi
        public class AnswerRequest
        {
            public int QuestionId { get; set; } // Cevaplanan sorunun ID'si
            public string SelectedOption { get; set; } // Kullanıcının seçtiği şık
        }

        [HttpPost("answer")]
        public IActionResult CheckAnswer([FromBody] AnswerRequest req)
        {
            // Gelen istek null veya şık boşsa, kötü istek hatası döndürülür
            if (req == null || string.IsNullOrEmpty(req.SelectedOption))
                return BadRequest(new { message = "Geçersiz istek." });

            // Kullanıcının cevabı kontrol edilir ve sonuç döndürülür
            var result = _service.CheckAnswer(GetUserId(), req.QuestionId, req.SelectedOption);
            return Ok(result);
        }

        [HttpGet("score")]
        public IActionResult GetScore()
        {
            // Kullanıcının skorunu alır ve döndürür
            int score = _service.GetScore(GetUserId());
            return Ok(new { score });
        }
    }
}
