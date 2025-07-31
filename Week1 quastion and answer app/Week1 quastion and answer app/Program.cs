var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//.net in kendi olu�turdu�u cs uzant�l� dosyad�r burada sayfan�n a��lmas�n� belirli bir local host verilmesini swagger kullan�m�n� yapabilir
//ve test ortam�nda test edilebilir(frondend olmad��� i�in test ortman� swagger ile yap�ld� herhangi bir yanl�� bulunamad� )

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
