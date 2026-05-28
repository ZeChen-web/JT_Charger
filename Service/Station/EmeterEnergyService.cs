using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using Repository.Station;

namespace Service.Station;

[Scope("SingleInstance")]
public class EmeterEnergyService : BaseServices<EmeterEnergy>
{
    private readonly EmeterEnergyRepository _emeterEnergyRep;

    public EmeterEnergyService(EmeterEnergyRepository emeterEnergyRep)
    {
        _emeterEnergyRep = emeterEnergyRep;
    }
}