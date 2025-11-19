using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Models;
using Petanque.Models.Enums;
using Petanque.Services.Interfaces;
using Petanque.Services.Mapping;
using Petanque.Storage;
using Petanque.Storage.Entity;
using Petanque.Storage.Interfaces;

namespace Petanque.Services.Services;

public class PlayerService(ISpelerRepository playerRepository) : IPlayerService
{
    public PlayerResponseContract Create(PlayerRequestContract request)
    {

        var model = new PlayerModel()
        {
            Firstname = request.Voornaam,
            Lastname = request.Naam
        };

        var entity = model.AsEntity();

        playerRepository.Create(entity);

        return entity.AsModel().AsContract();
    }

    public PlayerResponseContract? GetById(int id)
    {
        var entity = playerRepository.GetById(id);
        
        return entity is null ? null : entity.AsModel().AsContract();
    }
    
    public IEnumerable<PlayerResponseContract> GetAll()
    {
        return playerRepository.GetAll().OrderBy(a => a.Naam).ThenBy(a => a.Voornaam).Select(a => a.AsModel().AsContract()).ToList();
    }

    public void Update(int id, string voornaam, string naam)
    {
        playerRepository.Update(id, voornaam, naam);
    }

    public void Delete(int id)
    {
        playerRepository.Delete(id);
    }
}