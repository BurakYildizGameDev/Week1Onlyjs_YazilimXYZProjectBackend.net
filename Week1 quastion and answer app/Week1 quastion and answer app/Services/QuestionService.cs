using System.Text.Json;
using Week1_question_and_answer_app.Models;
using Week1_question_and_answer_app.Dtos;

namespace Week1_question_and_answer_app.Services
{
    // Bu sınıf, quiz uygulamasındaki soru/cevap işlemlerini yöneten servistir
    public class QuestionService : IQuestionService
    {
        // JSON dosyasının yolu  (Data/questions.json)
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "questions.json");

        // Kullanıcılara özel oturumları ve skorları takip etmek için dictionary 
        private readonly Dictionary<string, UserSession> _userSessions = new();

        // Sistemdeki tüm soruları JSON dosyası üzerinden okur ve döner
        public List<Question> GetAllQuestions()
        {
            try
            {
                // Eğer dosya yoksa boş liste döndürülür
                if (!File.Exists(_filePath))
                    return new List<Question>();

                // JSON içeriği okunuyor
                var json = File.ReadAllText(_filePath);

                // JSON büyük/küçük harfe duyarsız şekilde deserialize edilir
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // Sorular deserialize edilip döndürülür, null ise boş liste döner
                return JsonSerializer.Deserialize<List<Question>>(json, options) ?? new List<Question>();
            }
            catch
            {
                // Hata durumunda da boş liste döndürülür
                return new List<Question>();
            }
        }

        // Kullanıcıya rastgele bir soru döner ve o kullanıcı için zaman başlatılır
        public Question GetRandomQuestion(string userId)
        {
            var questions = GetAllQuestions();

            // Eğer hiç soru yoksa, sahte "bulunamadı" sorusu döndürülür
            if (questions.Count == 0)
                return new Question
                {
                    Id = 0,
                    QuestionText = "Soru bulunamadı",
                    Options = new List<string> { "-" },
                    CorrectAnswer = "-"
                };

            // Rastgele bir soru seçilir
            var random = new Random();
            var selected = questions[random.Next(questions.Count)];

            // Kullanıcının yeni sorusu için başlangıç zamanı kaydedilir
            _userSessions[userId] = new UserSession
            {
                QuestionStartTime = DateTime.UtcNow
            };

            return selected;
        }

        // Kullanıcının cevabını kontrol eder, geç cevap mı kontrol eder ve sonucu döner
        public AnswerResponseDto CheckAnswer(string userId, int questionId, string selectedOption)
        {
            // İlgili soru ID'ye göre bulunur
            var question = GetAllQuestions().FirstOrDefault(q => q.Id == questionId);

            // Soru bulunamadıysa false döner
            if (question == null)
                return new AnswerResponseDto { Correct = false, IsLate = false };

            // Eğer oturum bulunamazsa, varsayılan olarak süresi geçmiş gibi işlem yapılır
            if (!_userSessions.TryGetValue(userId, out var session))
                session = new UserSession { QuestionStartTime = DateTime.UtcNow.AddMinutes(-2) };

            // Geçen süre hesaplanır
            var elapsed = (DateTime.UtcNow - session.QuestionStartTime).TotalSeconds;

            // 60 saniyeden fazlaysa geç kalınmış sayılır
            bool isLate = elapsed > 60;

            // Eğer cevap doğruysa ve geç kalınmamışsa true
            bool isCorrect = !isLate && question.CorrectAnswer == selectedOption;

            // Eğer doğruysa skor artırılır
            if (isCorrect)
                session.Score++;

            // Güncellenmiş oturum bilgisi tekrar saklanır
            _userSessions[userId] = session;

            // Cevap sonucu DTO olarak döndürülür
            return new AnswerResponseDto
            {
                Correct = isCorrect,
                IsLate = isLate,
                RemainingTimeSeconds = Math.Max(0, 60 - (int)elapsed)
            };
        }

        // Belirli bir kullanıcının skorunu döner
        public int GetScore(string userId)
        {
            // Eğer kullanıcı oturumu varsa skoru döndürülür, yoksa 0 döner
            return _userSessions.TryGetValue(userId, out var session) ? session.Score : 0;
        }
    }
}
