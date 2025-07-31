Week1 Question and Answer App (.NET 7 Web API)

Bu proje, temel bir quiz uygulamasıdır. Kullanıcıya rastgele sorular sunulur, cevaplara göre skor tutulur ve doğru/yanlış ile zaman kontrolü yapılır. Kullanıcılar IP adresleriyle tanımlanır. Projede herhangi bir kullanıcı kaydı veya giriş sistemi yoktur.

Projeyi Çalıştırma:

Gereksinimler:

.NET 7 SDK veya üzeri

Visual Studio / VS Code / Rider

Adımlar:

Projeyi klonlayın:
git clone <repo-url>
cd Week1_question_and_answer_app

Soruların bulunduğu questions.json dosyasını kontrol edin:
Data/questions.json

Uygulamayı çalıştırın:
dotnet run

Tarayıcınızda aşağıdaki adrese gidin:
https://localhost:5001/swagger

API Endpoint Açıklamaları:

[GET] /api/quiz/questions
Tüm soruları listeler.
Yanıt örneği:
[
{
"id": 1,
"questionText": "C# dilinde bir değişken nasıl tanımlanır?",
"options": ["int x = 5;", "var = 5;", "let x = 5;", "x := 5"],
"correctAnswer": "int x = 5;"
}
]

[GET] /api/quiz/question/random
Rastgele bir soru getirir. IP adresi ile eşleştirme yapılır ve zaman başlatılır.

[POST] /api/quiz/answer
Kullanıcının cevabını kontrol eder.
İstek örneği:
{
"questionId": 1,
"selectedOption": "int x = 5;"
}
Yanıt örneği:
{
"correct": true,
"isLate": false,
"remainingTimeSeconds": 48
}

[GET] /api/quiz/score
Kullanıcının mevcut skorunu döndürür.
Yanıt örneği:
{
"score": 2
}

Sorular Nerede?
Tüm sorular Data/questions.json dosyasında tutulur. Örnek içerik:

[
{
"id": 1,
"questionText": "C# dilinde bir değişken nasıl tanımlanır?",
"options": ["int x = 5;", "var = 5;", "let x = 5;", "x := 5"],
"correctAnswer": "int x = 5;"
},
{
"id": 2,
"questionText": "Bir sınıf nasıl tanımlanır?",
"options": ["class MyClass {}", "function MyClass() {}", "def MyClass:", "struct MyClass"],
"correctAnswer": "class MyClass {}"
},
{
"id": 3,
"questionText": "C# dilinde liste oluşturmak için hangi sınıf kullanılır?",
"options": ["List<>", "ArrayList()", "Set()", "Vector<>"],
"correctAnswer": "List<>"
},
{
"id": 4,
"questionText": "Console uygulamasında çıktı almak için hangi komut kullanılır?",
"options": ["Console.WriteLine()", "System.out.print()", "print()", "cout <<"],
"correctAnswer": "Console.WriteLine()"
},
{
"id": 5,
"questionText": "C# dilinde bir döngü hangi anahtar kelime ile başlatılır?",
"options": ["for", "loop", "foreach", "repeat"],
"correctAnswer": "for"
}
]

Geliştirici Notu:
Proje sadece backend tarafını içerir. Frontend arayüz yoktur. Swagger arayüzü ile test yapılabilir. Kullanıcı IP’si üzerinden eşleştirme yapılarak skor ve zamanlama takip edilir. Cevaplar 60 saniye içerisinde verilmelidir, aksi takdirde “geç” olarak değerlendirilir.