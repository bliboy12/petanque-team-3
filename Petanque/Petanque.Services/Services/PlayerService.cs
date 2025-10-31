using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;
using Petanque.Storage;
using Petanque.Storage.Entity;

namespace Petanque.Services.Services;

public class PlayerService(SpelerRepository spelerRepository) : IPlayerService
{
    public PlayerResponseContract Create(PlayerRequestContract request)
    {
        var entity = new Speler()
        {
            Voornaam = request.Voornaam,
            Naam = request.Naam
        };
        spelerRepository.Create(entity);  

        return MapToContract(entity);
    }

    public PlayerResponseContract? GetById(int id)
    {
        var entity = spelerRepository.GetById(id);

        //var entity = context.Spelers.Find(id);
        return entity is null ? null : MapToContract(entity);
    }
    public IEnumerable<PlayerResponseContract> GetAll()
    {
        return spelerRepository.GetAll().OrderBy(a => a.Naam).ThenBy(a => a.Voornaam).Select(a => MapToContract(a)).ToList();
    }

    public void Update(int id, string voornaam, string naam)
    {
        spelerRepository.Update(id, voornaam, naam);
    }

    public void Delete(int id)
    {
        spelerRepository.Delete(id);
    }

    private static PlayerResponseContract MapToContract(Speler entity)
    {
        return new PlayerResponseContract()
        {
            SpelerId = entity.SpelerId,
            Voornaam = entity.Voornaam,
            Naam = entity.Naam
        };
    }
}