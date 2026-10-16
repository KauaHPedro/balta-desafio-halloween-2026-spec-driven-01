using GeradorSenhas.Api.Aplicacao;
using GeradorSenhas.Api.Contratos;
using GeradorSenhas.Api.Dados;
using GeradorSenhas.Api.Dominio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddDbContext<ContextoSenhas>(opcoes =>
    opcoes.UseSqlite(builder.Configuration.GetConnectionString("Senhas")));
builder.Services.AddScoped<IGeradorDeSenhas, GeradorDeSenhas>();
builder.Services.AddScoped<ServicoDeSenhas>();

var app = builder.Build();

app.UseExceptionHandler();

using (var escopo = app.Services.CreateScope())
{
    var contexto = escopo.ServiceProvider.GetRequiredService<ContextoSenhas>();
    contexto.Database.EnsureCreated();
}

app.MapPost("/senhas", async (ServicoDeSenhas servico, CancellationToken cancelamento) =>
{
    var id = await servico.GerarAsync(cancelamento);

    return Results.Ok(new GerarSenhaResposta(id));
});

app.MapGet("/senhas/{id}", async (string id, ServicoDeSenhas servico, CancellationToken cancelamento) =>
{
    if (!Guid.TryParse(id, out var identificador))
    {
        return Results.Problem(
            title: "Identificador inválido",
            detail: "O identificador informado não é um GUID válido.",
            statusCode: StatusCodes.Status400BadRequest);
    }

    var senha = await servico.ObterPorGuidAsync(identificador, cancelamento);
    if (senha is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new RecuperarSenhaResposta(senha));
});

app.Run();

public partial class Program
{
}
