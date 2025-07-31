namespace Week1_question_and_answer_app.Models
{
    // Bir kullanıcının oturum bilgilerini tutar.Bu bilgiler kullanıcının cevaplama süresi ve mevcut skorudur
    public class UserSession
    {
        // Kullanıcının bir soruyu cevaplamaya başladığı zamandır bilgide zaman işlevi kullanılıp kullanılmadığına bakılır.
        public DateTime QuestionStartTime { get; set; }

        // Kullanıcının şu ana kadarki toplam skoru tutulur başlangıç için 0 tutulur.
        public int Score { get; set; } = 0;
    }
}
