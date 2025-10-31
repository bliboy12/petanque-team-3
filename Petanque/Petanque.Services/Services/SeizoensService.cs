using Petanque.Contracts.Requests;
using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;
using Petanque.Storage;
using Petanque.Storage.Entity;

namespace Petanque.Services.Services;

public class SeizoensService(SeizoenRepository seizoenRepository)  : ISeizoensService
{
    public IEnumerable<SeizoenResponseContract> GetAll()
    {
        return seizoenRepository.GetAll().OrderByDescending(s => s.Startdatum) // Meest recente seizoenen eerst
            .Select(s => MapToContract(s))
            .ToList();
    }

    public SeizoenResponseContract Create(SeizoenRequestContract request)
    {
        var entity = new Seizoen
        {
            Startdatum = request.Startdatum,
            Einddatum = request.Einddatum
        };

        var overlappingSeizoen = seizoenRepository.GetOverlappendeSeizoenen(entity.Startdatum, entity.Einddatum);

        if (overlappingSeizoen != null)
        {
            throw new InvalidOperationException($"Er bestaat al een seizoen dat overlapt met deze periode, namelijk seizoen {overlappingSeizoen.SeizoensId} ({overlappingSeizoen.Startdatum:dd/MM/yyyy}-{overlappingSeizoen.Einddatum:dd/MM/yyyy})");
        }

        seizoenRepository.Create(entity);

        return MapToContract(entity);
    }

    private static SeizoenResponseContract MapToContract(Seizoen entity)
    {
        return new SeizoenResponseContract
        {
            SeizoensId = entity.SeizoensId,
            Startdatum = entity.Startdatum,
            Einddatum = entity.Einddatum,
        };
    }
}