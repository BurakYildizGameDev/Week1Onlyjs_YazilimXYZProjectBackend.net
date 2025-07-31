namespace Week1_question_and_answer_app.Dtos
{
    // Kullanıcının cevap verdikten sonra geri dönen sonuçları temsil eden DTO (Data Transfer Object) sınıfı
    public class AnswerResponseDto
    {
        // Cevabın doğru olup olmadığını belirtir (true: doğru, false: yanlış)
        public bool Correct { get; set; }

        // Cevabın zamanında mı yoksa geç mi verildiğini belirtir (true: geç kaldı, false: zamanında)
        public bool IsLate { get; set; }

        // Cevap verirken kalan süreyi saniye cinsinden tutar(60 saniyeden eksilerek) ve bu tutlan saniye kullanıcya gösterilir
        public int RemainingTimeSeconds { get; set; }
    }
}
