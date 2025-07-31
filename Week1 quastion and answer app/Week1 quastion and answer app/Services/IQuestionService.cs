using Week1_question_and_answer_app.Dtos;
using Week1_question_and_answer_app.Models;

namespace Week1_question_and_answer_app.Services
{
    // Quiz uygulamasında soru ve cevap işlemlerini tanımlayan servis arayüzüdür
    //Soruları rastegel bir şekilde getirir,kullanıcının cevabı doğruluğunu kontrol eder ve skor bilgisini tutar.
    public interface IQuestionService
    {
        // Sistemde tanımlı olan tüm sorular (list halinde).
        List<Question> GetAllQuestions();

        // Verilen kullanıcı için rastgele bir soru getirir.
        // Kullanıcının ister ise soru seçimi yapılabilir.
        // userId: Kullanıcıyı benzersiz tanımlayan ID.
        Question GetRandomQuestion(string userId);

        // Kullanıcının gönderdiği cevabı kontrol eder ve sonucu döner. Skor güncellemesi yapar
        // userId: Cevabı gönderen kullanıcının ID'si.
        // questionId: Cevaplanan sorunun ID'si.
        // selectedOption: Kullanıcının seçtiği şık (cevap).
        // Dönen değer: Doğru/yanlış bilgisini ve ek bilgileri içeren DTO.
        AnswerResponseDto CheckAnswer(string userId, int questionId, string selectedOption);

        // Belirtilen kullanıcının mevcut skorunu döner.
        // userId: Skoru sorgulanan kullanıcının ID'si.
        int GetScore(string userId);
    }
}
