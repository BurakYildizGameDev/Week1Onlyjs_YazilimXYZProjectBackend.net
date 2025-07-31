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
//.net in kendi oluþturduðu cs uzantýlý dosyadýr burada sayfanýn açýlmasýný belirli bir local host verilmesini swagger kullanýmýný yapabilir
//ve test ortamýnda test edilebilir(frondend olmadýðý için test ortmaný swagger ile yapýldý herhangi bir yanlýþ bulunamadý )

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
