namespace Week1_question_and_answer_app.Models
{

    // Soru model sınıfı.
    //id ile kacıncı soru olduğu tutuldu 

    public class Question
    {
        public int Id { get; set; }                      // Soru ID
        public string? QuestionText { get; set; }        // Soru metni
        public List<string>? Options { get; set; }       // Şıklar
        public string? CorrectAnswer { get; set; }       // Doğru şık
    }
}
