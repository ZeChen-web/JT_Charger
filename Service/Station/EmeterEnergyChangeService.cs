using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using Repository.Station;

namespace Service.Station;

[Scope("SingleInstance")]
public class EmeterEnergyChangeService : BaseServices<EmeterEnergyChange>
{
    private readonly EmeterEnergyChangeRepository _emeterEnergyChangeRep;

    public EmeterEnergyChangeService(EmeterEnergyChangeRepository emeterEnergyChangeRep)
    {
        _emeterEnergyChangeRep = emeterEnergyChangeRep;
    }
}