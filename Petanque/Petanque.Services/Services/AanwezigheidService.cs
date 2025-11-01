using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;
using Petanque.Services.Mapping;
using Petanque.Storage;

namespace Petanque.Services.Services;

public class AanwezigheidService(AanwezigheidRepository aanwezigheidRepository) : IAanwezigheidService
{
    public AanwezigheidResponseContract Create(AanwezigheidRequestContract request)
    {
        var createdEntity = aanwezigheidRepository.Create(request.AsModel().AsEntity());

        return createdEntity.AsModel().AsContract();
    }
    
    public AanwezigheidResponseContract? GetById(int id)
    {
        var entity = aanwezigheidRepository.GetById(id);
        
        return entity is null ? null : entity.AsModel().AsContract();
    }

    public IEnumerable<AanwezigheidResponseContract> GetAll() 
    {
        var entities = aanwezigheidRepository.GetAll();
        
        return entities.Select(a => a.AsModel().AsContract()).ToList(); 
    }
    
    public IEnumerable<AanwezigheidResponseContract> GetAanwezighedenOpSpeeldag(int id) 
    { 
        var entities = aanwezigheidRepository.GetAanwezighedenOpSpeeldag(id);
        
        return entities.Select(a => a.AsModel().AsContract()).ToList(); 
    }
    
    public void Delete(int id) 
    { 
        aanwezigheidRepository.DeleteAanwezigheid(id);
    }
    
    public IEnumerable<AanwezigheidResponseContract> GetAanwezighedenOpSpeler(int spelerId) 
    { 
        return aanwezigheidRepository.GetAanwezighedenOpSpeler(spelerId).Select(a => a.AsModel().AsContract()).ToList(); 
    }
}
